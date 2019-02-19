using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_ebayMaster_change_ebay_category :PageBase
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

        InitPage();
    }

    void InitPage()
    {
        if (ReqSku > 0
            && ReqItemId.Trim().Length == 12)
        {
            LabelSKU.Text = ReqSku.ToString();
            LabelItemid.Text = ReqItemId.ToString();

            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("txt");
            DataRow dr = dt.NewRow();
            dr["id"] = 0;
            dr["txt"] = "Please Selected;";
            dt.Rows.Add(dr);

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            List<eBayCategory> scList = new GetEbayCategoryIDs().GetStoreCategory(Server.MapPath("~/soft_img/eBayXml/GetStoreResponse.xml"));
        
            foreach (var m in scList)
            {
                DataRow ndr = dt.NewRow();
                ndr["id"] = m.Id;
                ndr["txt"] = m.Name;
                dt.Rows.Add(ndr);

            }
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "txt";
            DropDownList1.DataValueField = "id";
            DropDownList1.DataBind();
            if (Session["ebayCategoryId"] != null)
                DropDownList1.SelectedValue = Session["ebayCategoryId"].ToString();
            this.ButtonSubmit.Text = "Change";
        }
    }

    string ReqItemId
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }

    int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ButtonSubmit_Click(object sender, EventArgs e)
    {
        long neweBayCateid;
        long.TryParse(DropDownList1.SelectedValue.ToString(), out neweBayCateid);

        if (neweBayCateid > 0)
        {
            // 用标题参数来传category id值
            eBayCmdReviseItem.ReviseItem(ReqItemId, ReqSku.ToString().Length == 6
                , null
                , 0M
                , ReqSku
                , null
                , null
                , null
                , neweBayCateid.ToString()
                , eBayModifyType.storeCategory);

            DataTable subDT = Config.ExecuteDataTable("select count(Sku) from tb_ebay_category_and_product where Sku='" + ReqSku + "'");
            if (ReqSku.ToString().Length == 6)
            {
                Config.ExecuteNonQuery("update tb_ebay_system set ebayCategoryName='" + DropDownList1.SelectedItem.Text + "' where id='" + ReqSku.ToString() + "'");

               
                if (subDT.Rows.Count >0)
                {
                    Config.ExecuteNonQuery("update tb_ebay_category_and_product set eBayCateID_1='" + DropDownList1.SelectedValue.ToString() + "', eBayCateText_1='" + DropDownList1.SelectedItem.Text + "' where sku='" + ReqSku + "'");
                }
                else
                {
                    Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_category_and_product 
	( Sku, ProdType, eBayCateID_1, eBayCateText_1,   regdate
	)
	values
	( '{0}', '{1}', '{2}', '{3}',   now()	)"
                                      , ReqSku
                                      , "S"
                                      , DropDownList1.SelectedValue.ToString()
                                      , DropDownList1.SelectedItem.Text));
                }
            }

            ProductModel pm = ProductModel.GetProductModel(ReqSku);
            if (pm != null)
            {
                ProductCategoryModel pc = ProductCategoryModel.GetProductCategoryModel(pm.menu_child_serial_no);
                if (pc != null)
                {
                    if (subDT.Rows.Count > 0)
                    {
                        Config.ExecuteNonQuery("update tb_ebay_category_and_product set eBayCateID_1='" + DropDownList1.SelectedValue.ToString() + "', eBayCateText_1='" + DropDownList1.SelectedItem.Text + "' where sku='" + ReqSku + "'");
                    }
                    else
                    {
                        Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_category_and_product 
	( Sku, ProdType, eBayCateID_1, eBayCateText_1,   regdate
	)
	values
	( '{0}', '{1}', '{2}', '{3}', now())"
                            , ReqSku
                            , pc.is_noebook == 1 ? "N" : "P"
                            , DropDownList1.SelectedValue.ToString()
                            , DropDownList1.SelectedItem.Text));
                    }
                }
            }
            Session["ebayCategoryId"] = neweBayCateid.ToString();

            Response.Write("<script>this.close();</script>");
        }
    }
}