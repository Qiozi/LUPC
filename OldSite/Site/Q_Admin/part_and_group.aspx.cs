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

public partial class Q_Admin_part_and_group : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindLbl();

            // BindPartGroupCBL();
            BindPartGroupEbay();
            BindISContain();
        }
    }

    #region Methods

    /// <summary>
    /// bind label control
    /// </summary>
    private void BindLbl()
    {
        this.lbl_sku.Text = this.PartID.ToString();
        var pm = ProductModel.GetProductModel(DBContext,  this.PartID);
        this.lbl_short_name.Text = pm.product_short_name;
        this.lbl_middle_name.Text = pm.product_name;
        this.txt_ebay_cost.Text = pm.part_ebay_cost.ToString();
        this.txt_ebay_price.Text = pm.part_ebay_price.ToString();

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="autoUpdate"></param>
    private void BindPartGroupCBL()
    {
        this.cbl_group_list.DataSource = PartGroupModel.FindGroupByPartID(this.PartID, false, true);
        this.cbl_group_list.DataTextField = "part_group_comment";
        this.cbl_group_list.DataValueField = "part_group_id";
        this.cbl_group_list.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    private void BindPartGroupEbay()
    {
        this.cbl_group_list_ebay.DataSource = PartGroupModel.FindGroupByPartID(this.PartID, true, true);
        this.cbl_group_list_ebay.DataTextField = "part_group_comment";
        this.cbl_group_list_ebay.DataValueField = "part_group_id";
        this.cbl_group_list_ebay.DataBind();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="autoUpdate"></param>
    private void BindISContain()
    {
        DataTable dt = PartGroupModel.FindPartGroupIDByPart(this.PartID);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string part_group_id = dt.Rows[i]["part_group_id"].ToString();
            //
            // eBay price.
            for (int j = 0; j < this.cbl_group_list_ebay.Items.Count; j++)
            {
                if (part_group_id == this.cbl_group_list_ebay.Items[j].Value)
                {
                    this.cbl_group_list_ebay.Items[j].Selected = true;
                    if (dt.Rows[i]["showit"].ToString() == "0")
                    {
                        this.cbl_group_list_ebay.Items[j].Text = " <span style='color:#cccccc'>" + this.cbl_group_list_ebay.Items[j].Text + "</span>";
                    }
                }
            }
            //
            // Web Sys
            for (int j = 0; j < this.cbl_group_list.Items.Count; j++)
            {
                if (part_group_id == this.cbl_group_list.Items[j].Value)
                {
                    this.cbl_group_list.Items[j].Selected = true;
                    if (dt.Rows[i]["showit"].ToString() == "0")
                    {
                        this.cbl_group_list.Items[j].Text = " <span style='color:#cccccc'>" + this.cbl_group_list.Items[j].Text + "</span>";
                    }
                }

            }

        }
    }
    #endregion

    #region Properties
    public int PartID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "partid", -1); }
    }

    public int ProductCategory
    {
        get { return (int)ViewState["ProductCategory"]; }
        set { ViewState["ProductCategory"] = value; }
    }
    #endregion

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        this.Label2.Text = "";
        this.Label1.Text = "";
        try
        {
            for (int i = 0; i < this.cbl_group_list.Items.Count; i++)
            {
                int part_group_id = int.Parse(this.cbl_group_list.Items[i].Value);
                if (this.cbl_group_list.Items[i].Selected)
                {


                    if (!PartGroupDetailModel.IsExistProduct(part_group_id, this.PartID))
                    {

                        var pgd = new LU.Data.tb_part_group_detail(); // PartGroupDetailModel();
                        pgd.showit = sbyte.Parse("1");
                        pgd.product_serial_no = this.PartID;
                        pgd.part_group_id = part_group_id;

                        DBContext.tb_part_group_detail.Add(pgd);
                        DBContext.SaveChanges();
                    }
                }
                else
                {
                    PartGroupDetailModel.DelByPartIDAndGroupID(part_group_id, this.PartID);
                }
            }
            BindPartGroupCBL();
            BindISContain();

            this.Label1.Text = KeyFields.SaveIsOK;
        }
        catch (Exception ex)
        {
            this.Label1.Text = ex.Message;
        }
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Label2.Text = "";
        this.Label1.Text = "";
        decimal cost;
        decimal.TryParse(this.txt_ebay_cost.Text, out cost);

        decimal price;
        decimal.TryParse(this.txt_ebay_price.Text, out price);

        int CategoryID = 0;
        if (this.PartID > 0)
        {
            var pm = ProductModel.GetProductModel(DBContext, this.PartID);
            pm.part_ebay_cost = cost;
            pm.part_ebay_price = price;
            DBContext.SaveChanges();
            CategoryID = pm.menu_child_serial_no.Value;
        }

        for (int i = 0; i < this.cbl_group_list_ebay.Items.Count; i++)
        {
            int part_group_id = int.Parse(this.cbl_group_list_ebay.Items[i].Value);
            if (this.cbl_group_list_ebay.Items[i].Selected)
            {


                if (!PartGroupDetailModel.IsExistProduct(part_group_id, this.PartID))
                {

                    var pgd = new LU.Data.tb_part_group_detail();// PartGroupDetailModel();
                    pgd.showit = sbyte.Parse("1");
                    pgd.product_serial_no = this.PartID;
                    pgd.part_group_id = part_group_id;

                    DBContext.tb_part_group_detail.Add(pgd);
                    DBContext.SaveChanges();
                }
            }
            else
            {
                PartGroupDetailModel.DelByPartIDAndGroupID(part_group_id, this.PartID);
            }
        }
        BindPartGroupEbay();
        BindISContain();

        if (CategoryID == Config.part_motherboard_ID)
        {
            if (this.cbl_group_list_ebay.SelectedIndex < 0)
                return;
            if (Config.ExecuteScalarInt32("select count(id) from tb_part_relation_motherboard_video_audio_port where mb_sku='" + PartID + "'") == 0)
                this.Label2.Text = "<a href='/q_admin/ebayMaster/lu/part_relation_motherboard.asp?sku=" + PartID + "' onclick='window.open(this.href,\"editMBR" + PartID + "\",\"width=800px; height=300px;\");return false;' style='color:red;'> 请配置主板的关联显卡，声卡，网卡 </a>";
            else
                this.Label2.Text = KeyFields.SaveIsOK;
        }
        else
            this.Label2.Text = KeyFields.SaveIsOK;
    }
}
