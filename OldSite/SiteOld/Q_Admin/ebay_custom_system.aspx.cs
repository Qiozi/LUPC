using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_ebay_custom_system : PageBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindGV();
    }

    #region Methods
    private void BindGV()
    {

        this.gv_custom_sys.DataSource = PartInfoDT;
        this.gv_custom_sys.DataBind();
    }

    public void InitDT(DataTable dt)
    {
        //DataTable dt = new DataTable();
        dt.Columns.Add("id");
        dt.Columns.Add("text");
        for (int i = 0; i < 30; i++)
        {
            DataRow dr = dt.NewRow();
            dr["id"] = (i+1).ToString();
            dt.Rows.Add(dr);
        }
       
    }

    private string PartGroupName(int part_group_id)
    {
        string name = string.Empty;
        for (int i = 0; i < PartGroup.Rows.Count; i++)
        {
            DataRow dr = PartGroup.Rows[i];
            if (part_group_id.ToString() == dr["part_group_id"].ToString())
            {
                name = dr["part_group_name"].ToString();
                break;

            }
        }
        return name;
    }
    #endregion

    #region properties
    public DataTable PartInfoDT
    {
        get
        {
            if (ViewState["PartInfoDT"] == null)
            {
                DataTable _system_part = new DataTable();
                InitDT(_system_part);
                ViewState["PartInfoDT"] = _system_part;
            }
            return (DataTable)ViewState["PartInfoDT"];
        }
        set { ViewState["PartInfoDT"] = value; }

    }

    public DataTable PartGroup
    {
        get
        {
            if (ViewState["PartGroup"] == null)
            {
                PartGroupModel pgm = new PartGroupModel();
                ViewState["PartGroup"] = pgm.FindModelByAll(); ;
            }
            return (DataTable)ViewState["PartGroup"];
        }
        set { ViewState["PartGroup"] = value; }

    }

    #endregion

    protected void btn_add_part_Click(object sender, EventArgs e)
    {
        DataTable dt = PartInfoDT;
        DataRow dr = dt.NewRow();
        dr["id"] = 0;
        dt.Rows.Add(dr);
        PartInfoDT = dt;
        BindGV();
    }

    protected void _ddl_part_group_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList _ddl = (DropDownList)sender;
        for (int i = 0; i < this.gv_custom_sys.Rows.Count; i++)
        {
            DropDownList ddl = (DropDownList)this.gv_custom_sys.Rows[i].Cells[2].FindControl("_ddl_part_group");
            if (_ddl == ddl)
            {
                int part_id;
                int.TryParse(_ddl.SelectedValue.ToString(), out part_id);

                DropDownList ddl_part = (DropDownList)this.gv_custom_sys.Rows[i].Cells[3].FindControl("_ddl_part");
                PartGroupDetailModel pgdm = new PartGroupDetailModel();
                ddl_part.DataSource = pgdm.FindPartIDNameByGroupID(part_id);
                ddl_part.DataValueField = "product_serial_no";
                ddl_part.DataTextField = "product_name";
                ddl_part.DataBind();

                //
                // get part group name 
                //
                this.gv_custom_sys.Rows[i].Cells[1].Text = PartGroupName(part_id);
                //CH.Alert(part_id.ToString(), this.gv_custom_sys);
               // this.btn_add_part.Text = part_id.ToString();
            }
            
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        object o = Config.ExecuteScalar(@"insert into nicklu2.tb_ebay_store 
	(   is_modify_price, regdate, last_regdate	)
	values
	(   0, now(), now()
	);
select LAST_INSERT_ID()");
        int parent_id;
        int.TryParse(o.ToString(), out parent_id);

        if (parent_id != 0)
        {
            //
            // load default templete;
            //
            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_store_templete 
	( ebay_templete_id, ebay_templete_comment, ebay_templete_content, ebay_store_id, 
	regdate, 
	last_regdate
	)
select 	id, templete_comment, templete_content,{0}, now(), now() 
	from 
	tb_ebay_templete et inner join tb_ebay_default_templete ed on ed.default_id=et.id and default_type=0
	 ", parent_id));

            //
            // load detail
            // 
            for (int i = 0; i < this.gv_custom_sys.Rows.Count; i++)
            {
                DropDownList ddl_part = (DropDownList)this.gv_custom_sys.Rows[i].Cells[3].FindControl("_ddl_part");
                int lu_sku;
                int.TryParse(ddl_part.SelectedValue.ToString(), out lu_sku);

                string part_comment = this.gv_custom_sys.Rows[i].Cells[1].Text;
                ProductModel pm = ProductModel.GetProductModel(lu_sku);
                string part_name = pm.product_name;
                if (lu_sku > 0)
                {
                    EbayStoreDetailModel esdm = new EbayStoreDetailModel();
                    esdm.ebay_store_id = parent_id;
                    esdm.last_regdate = DateTime.Now;
                    esdm.lu_sku = lu_sku;
                    esdm.part_comment = part_comment;
                    esdm.part_name = part_name;
                    esdm.regdate = DateTime.Now;
                    esdm.Create();

                }
            }
            CH.RunJavaScript(string.Format("parent.window.ViewSystemDetail({0});", parent_id), this.gv_custom_sys);
        }
        else
        {
            CH.Alert("创建system ，失改", this.gv_custom_sys);
        }
    }
}
