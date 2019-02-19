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

public partial class Q_Admin_ebay_detail_part : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!IsPostBack)
        {
            StoreID = Util.GetInt32SafeFromQueryString(Page, "id", -1);
            BindPageTempleteRadio();
            BindStore();
            CH.CloseParentWatting(this.btn_save);
          
        }
        
    }
    

    #region Methods

    private void BindStore()
    {
        if (StoreID != -1)
        {
            EbayStoreModel esm = EbayStoreModel.GetEbayStoreModel(StoreID);
            SKU = esm.lu_ebay_sku;
            this.txt_new_sku.Text = SKU.ToString();
            GeneratePage(esm.lu_ebay_sku);

            int store_templete_id;
            DataTable dt = Config.ExecuteDataTable("select ebay_templete_id from tb_ebay_store_templete where ebay_store_id = '" + StoreID.ToString() + "'");
            if (dt.Rows.Count == 1)
                int.TryParse(dt.Rows[0][0].ToString(), out store_templete_id);
            else
                store_templete_id = -1;
            TempleteID = store_templete_id;
        }
    }

    private void BindPageTempleteRadio()
    {
        this.radioButtonList_page_templete.DataSource = Config.ExecuteDataTable(@"select id, templete_comment from tb_ebay_templete where templete_type=1");
        this.radioButtonList_page_templete.DataTextField = "templete_comment";
        this.radioButtonList_page_templete.DataValueField = "id";
        this.radioButtonList_page_templete.DataBind();
    }

    private void LoadPageTemplete(int lu_sku)
    {
        string templete = "";
        DataTable tmp_dt = Config.ExecuteDataTable(string.Format(@"
select et.id, et.templete_comment, et.templete_content,et.templete_info from tb_ebay_templete et 
inner join tb_ebay_default_templete edt on edt.default_id=et.id and default_type=1"));
        if (tmp_dt.Rows.Count == 1)
        {
            templete = tmp_dt.Rows[0]["templete_content"].ToString();
            TempleteID = int.Parse(tmp_dt.Rows[0]["id"].ToString());
            DataTable part_desc_dt = Config.ExecuteDataTable(string.Format(@"
select part_comment from tb_part_comment where part_sku='{0}'", lu_sku));

            if (part_desc_dt.Rows.Count == 1)
            {
               templete = templete.Replace("[lu_web_info_row]", part_desc_dt.Rows[0][0].ToString()).Replace("[lu_web_flash_view]", "<iframe src=\""+ Config.http_domain +"view_in_flash.asp?lu_sku="+lu_sku+"\" style=\"width: 750px; height: 400px; border:0px;\" frameborder=\"0\" scrolling=\"no\"></iframe>");
            }
            else
            {
               templete = templete.Replace("[lu_web_info_row]", "").Replace("[lu_web_flash_view]", "");
                CH.Alert("Part is not info..", this.btn_save);
            }
            this.Editor1.Text = templete;
        }
        else
        {
            CH.Alert("No Match Default templete", this.btn_save);
        }
    }

    private void GeneratePage(int sku)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select es.id,ebay_store_comment, est.ebay_templete_after_filter  from tb_ebay_store es
inner join tb_ebay_store_templete est on est.ebay_store_id=es.id where ebay_store_type=1 and lu_ebay_sku='{0}'", sku));
        if (dt.Rows.Count > 0)
        {
            this.txt_part_name.Text = dt.Rows[0]["ebay_store_comment"].ToString();
            this.Editor1.Text = dt.Rows[0]["ebay_templete_after_filter"].ToString();
        }
        else
        {

          

//            DataTable tmp_dt = Config.ExecuteDataTable(string.Format(@"
//select et.id, et.templete_comment, et.templete_content,et.templete_info from tb_ebay_templete et 
//inner join tb_ebay_default_templete edt on edt.default_id=et.id and default_type=1"));
//            if (tmp_dt.Rows.Count == 1)
//            {
                 
//                string templete = tmp_dt.Rows[0]["templete_content"].ToString();
//                DataTable part_desc_dt = Config.ExecuteDataTable(string.Format(@"
//select part_comment from tb_part_comment where part_sku='{0}'", sku));
//                if (part_desc_dt.Rows.Count == 1)
//                {
//                    templete.Replace("[lu_web_info_row]", part_desc_dt.Rows[0][0].ToString()).Replace("[lu_web_flash_view]", "<iframe src=\"http://localhost/view_in_flash.asp?lu_skus=" + sku_list + "\" style=\"width: 750px; height: 400px; border:0px;\" frameborder=\"0\" scrolling=\"no\"></iframe>"); ;
//                }
//                else
//                {
//                    templete.Replace("[lu_web_info_row]", "").Replace("[lu_web_flash_view]", "<iframe src=\"http://localhost/view_in_flash.asp?cate=1&id=\" style=\"width: 750px; height: 400px; border:0px;\" frameborder=\"0\" scrolling=\"no\"></iframe>"); ;
//                }
//                this.Editor1.Text = templete;

//            }
//            else
//            {
//                CH.Alert("no macth default templete", this.btn_save);
//            }
        }
    }


    #endregion

    #region Properties

    public int StoreID
    {
        get { return (int)ViewState["StoreID"]; }
        set { ViewState["StoreID"] = value; }
    }

    public int TempleteID
    {
        get { return (int)ViewState["TempleteID"]; }
        set { ViewState["TempleteID"] = value; }
    }

    public int SKU
    {
        get { return (int)ViewState["SKU"]; }
        set { ViewState["SKU"] = value; }
    }
    #endregion

    #region Events
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (SKU > 0)
            {
                if (int.Parse(Config.ExecuteScalar(string.Format(@"
select count(id) from tb_ebay_store where ebay_store_type=1 and lu_ebay_sku=1543", SKU)).ToString()) == 0)
                {
                    EbayStoreModel esm = new EbayStoreModel();
                    esm.ebay_store_comment = this.txt_part_name.Text.Trim();
                    esm.ebay_store_type = 1;
                    esm.last_regdate = DateTime.Now;
                    esm.lu_ebay_sku = SKU;
                    esm.regdate = DateTime.Now;
                    esm.Create();

                    // load templete
                    Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_store_templete 
	( ebay_templete_id, ebay_templete_comment, ebay_templete_content, ebay_store_id, 
	regdate, 
	last_regdate,
ebay_templete_after_filter
	)
select 	id, templete_comment, templete_content,{0}, now(), now() , '" + this.Editor1.Text.Replace("'", "''") + @"' after_filter
	from 
	tb_ebay_templete et where et.id ='{1}'
	 ", esm.id, TempleteID));


                }
                else
                {
                    EbayStoreModel[] esms = EbayStoreModel.FindAllByProperty("lu_ebay_sku", SKU);
                    for (int i = 0; i < esms.Length; i++)
                    {
                        esms[i].ebay_store_comment = this.txt_part_name.Text.Trim();
                        esms[i].last_regdate = DateTime.Now;
                        esms[i].Update();

                        Config.ExecuteNonQuery("Update tb_ebay_store_templete set ebay_templete_after_filter='" + this.Editor1.Text.Replace("'", "''") + @"' 
where ebay_store_id='" + esms[i].id + "'");
                    }
                }
            }
            CH.CloseParentWatting(this.btn_save);
            CH.Alert(KeyFields.SaveIsOK, this.btn_save);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.btn_save);
            CH.Alert(ex.Message, this.btn_save);
        }
    }

    protected void btn_load_page_from_templete_Click(object sender, EventArgs e)
    {

    }

   

    protected void btn_set_in_Click(object sender, EventArgs e)
    {
        this.txt_part_name.Text = "";
        this.Editor1.Text = "";

        DataTable dt = Config.ExecuteDataTable("select product_name, tag from tb_product where product_serial_no='" + this.txt_new_sku.Text.Trim() + "'");
      // CH.Alert("select product_name, tag from tb_product where product_serial_no='" + this.txt_new_sku.Text.Trim() + "'", this.btn_save);
        if (dt.Rows.Count == 1)
        {
            if (dt.Rows[0][1].ToString() == "0")
            {
                CH.Alert("this part is invalid", this.btn_save);
            }
            else
            {
                this.txt_part_name.Text = dt.Rows[0][0].ToString();
                SKU = int.Parse(this.txt_new_sku.Text.Trim());
                GeneratePage(SKU);
            }
        }
        else
        {
            CH.Alert("No Match Data.", this.btn_save);
        }
    }
    #endregion

    protected void btn_reload_templete_Click(object sender, EventArgs e)
    {
        LoadPageTemplete(SKU);
    }
}
