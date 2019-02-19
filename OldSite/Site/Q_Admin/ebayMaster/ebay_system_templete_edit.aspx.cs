using LU.Data;
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

public partial class Q_Admin_ebayMaster_ebay_system_templete_edit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ReqID = TID;
            BindTempleteTOControl(TID);

            BindTempleteList();
        }
    }
    

    private void BindTempleteTOControl(int tid)
    {
        if (tid >0)
        {
            ReqID = tid;
            var etm = DBContext.tb_ebay_templete.Single(me => me.id.Equals(tid));// EbayTempleteModel.GetEbayTempleteModel(tid);
            this.txt_templete_content.Text = etm.templete_content;
            this.txt_templete_content2.Text = etm.templete_content2;
            this.txt_templete_summ_1.Text = etm.templete_summ_1;
            this.txt_templete_summ_2.Text = etm.templete_summ_2;
            this.txt_comment.Text = etm.templete_comment;
        }
    }

    private void BindTempleteList()
    {
        DataTable dt = Config.ExecuteDataTable("Select id, templete_comment, '' category_Names from tb_eBay_templete order by id desc");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string names = "0";
            DataTable sdt = Config.ExecuteDataTable(string.Format(@"select category_name 
            from tb_ebay_templete_and_category e inner join tb_product_category_new pc
on pc.category_id=e.sys_category_id where templete_id='{0}'
union
select concat(menu_child_name, '--', e.part_brand) from tb_ebay_templete_and_category e inner join tb_product_category pc
on pc.menu_child_serial_no=e.part_category_id where templete_id='{0}'", dt.Rows[i]["id"].ToString()));
            foreach (DataRow dr in sdt.Rows)
            {
                names += ", " + dr["category_name"].ToString();
            }
            dt.Rows[i]["category_names"] = names.Length > 2 ? names.Substring(2) : ""; ;
        }
        this.DataList1.DataSource = dt;
        this.DataList1.DataBind();
    }

    #region preporites
    public int TID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "tid", -1); }
    }
   
    public int ReqID
    {
        get
        {
            if (ViewState["id"] == null)
                return -1;
            return int.Parse(ViewState["id"].ToString());
        }
        set { ViewState["id"] = value; }
    }
    #endregion

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (ReqID > 0)
        {
            var etm = DBContext.tb_ebay_templete.Single(me => me.id.Equals(ReqID)); //EbayTempleteModel.GetEbayTempleteModel(ReqID);
            etm.templete_comment = this.txt_comment.Text.Trim();
            etm.templete_content = this.txt_templete_content.Text.Trim();
            etm.templete_content2 = this.txt_templete_content2.Text.Trim();
            etm.templete_summ_1 = this.txt_templete_summ_1.Text.Trim();
            etm.templete_summ_2 = this.txt_templete_summ_2.Text.Trim();
            DBContext.SaveChanges();
        }
        else
        {
            var etm = new tb_ebay_templete();// EbayTempleteModel();
            etm.templete_comment = this.txt_comment.Text.Trim();
            etm.templete_content = this.txt_templete_content.Text.Trim();
            etm.templete_content2 = this.txt_templete_content2.Text.Trim();
            etm.templete_summ_1 = this.txt_templete_summ_1.Text.Trim();
            etm.templete_summ_2 = this.txt_templete_summ_2.Text.Trim();
            DBContext.tb_ebay_templete.Add(etm);
            DBContext.SaveChanges();
        }

        this.txt_comment.Text = "";
        this.txt_templete_content.Text = "";
        this.txt_templete_content2.Text = "";
        this.txt_templete_summ_1.Text = "";
        this.txt_templete_summ_2.Text = "";
        ReqID = -1;
        BindTempleteList();
        this.Label_note.Text = "It is OK";
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        this.Label_note.Text = "";
        switch (e.CommandName)
        {
            case "EditComment":
                int id;
                int.TryParse(e.CommandArgument.ToString(), out id);
                ReqID = id;
                BindTempleteTOControl(id);
                //this.btn_submit.Text = id.ToString();

                break;
        }
    }
}
