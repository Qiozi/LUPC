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

public partial class Q_Admin_ebay_list_system : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageSize = 4;
            
            StartRecord = 0;
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindLV(string.Empty);
    }

    #region methods

    private string GetSystemPartHTML(int ebay_store_id)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataTable dt = Config.ExecuteDataTable(string.Format(@"select esd.part_group_id,id,part_comment,product_serial_no, p.product_short_name,menu_child_serial_no,manufacturer_part_number,p.tag,
case when other_product_sku > 0 then other_product_sku
    else product_serial_no end as img_sku from tb_ebay_store_detail esd inner join tb_product p on p.product_serial_no=esd.lu_sku where ebay_store_id='{0}'", ebay_store_id));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string name = dt.Rows[i]["product_short_name"].ToString();
            sb.Append(string.Format(@"<div style=""float: left; color:#666; width: 350px; border: 0px solid #ccc;
background: url('http://www.lucomputers.com/images/products/arrow3.gif') no-repeat  scroll 2px 5pt; padding-left: 15px;line-height: 15px"">{0}</div>",
             dt.Rows[i]["tag"].ToString() == "0" ? "<span style='display:block; background:#F6DADA;'>" + name + "</span>" : name));
        }
        return sb.ToString();
    }
    private void BindLV(string keyword)
    {
        EbayStoreModel esm = new EbayStoreModel();
        int count = 0;
        // 2: system
        this.ListView1.DataSource = esm.FindModelsBySystem(PageSize, StartRecord, keyword, ref count, 2);
        this.ListView1.DataBind();

        this.AspNetPager1.PageSize = PageSize;
        this.AspNetPager1.RecordCount = count;

    }
    #endregion

    #region preporties
    public int PageSize
    {
        get { return (int)ViewState["PageSize"]; }
        set { ViewState["PageSize"] = value; }
    }

    public int StartRecord
    {
        get { return (int)ViewState["StartRecord"]; }
        set { ViewState["StartRecord"] = value; }
    }

    #endregion

    protected void lb_save_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.ListView1.Items.Count; i++)
            {
                int id;
                int.TryParse(((Label)this.ListView1.Items[i].FindControl("_lbl_id")).Text, out id);

                TextBox _tb_ebay_code = (TextBox)this.ListView1.Items[i].FindControl("_txt_ebay_code");
                string ebaycode = _tb_ebay_code.Text.Trim();

                TextBox _tb_ebay_price = (TextBox)this.ListView1.Items[i].FindControl("_txt_ebay_price");
                decimal price;
                decimal.TryParse(_tb_ebay_price.Text.Trim(), out price);

                CheckBox _cb_is_templete = (CheckBox)this.ListView1.Items[i].FindControl("_cb_is_templete");

                EbayStoreModel esd = EbayStoreModel.GetEbayStoreModel(id);
                esd.ebay_code = ebaycode;
                esd.price = price;
                esd.is_templete = _cb_is_templete.Checked;
                esd.Update();
                //CH.Alert(string.Format("{0}|{1}|{2}|{3}", id, ebaycode, price, _cb_is_templete.Checked.ToString()), this.ListView1);
                
            }
            CH.CloseParentWatting(this.lb_save);
            CH.Alert(KeyFields.SaveIsOK, this.lb_save);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.lb_save);
            CH.Alert(ex.Message, this.lb_save);
        }
    }
    protected void ListView1_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        int id;
        int.TryParse(((Label)e.Item.FindControl("_lbl_id")).Text, out id);

        string s = GetSystemPartHTML(id);
        Literal li = (Literal)e.Item.FindControl("_literal_system_part");
        li.Text = s;

        TextBox txt_e_sold = (TextBox)e.Item.FindControl("_txt_ebay_price");
        TextBox txt_price = (TextBox)e.Item.FindControl("_txt_price");
        decimal dec_e_sold;
        decimal dec_price;
        decimal.TryParse(txt_price.Text, out dec_price);
        decimal.TryParse(txt_e_sold.Text, out dec_e_sold);
        if (dec_price < dec_e_sold)
        {
            txt_e_sold.CssClass = "input_right_line_red";
        }
    }
    
    protected void AspNetPager1_PageChanged(object sender, EventArgs e)
    {
       // PageSize = int.Parse(this.DropDownList1.SelectedValue.ToString());
        StartRecord = this.AspNetPager1.StartRecordIndex - 1;
        this.BindLV(this.txt_keyword.Text.Trim());
    }
    protected void btn_search_Click(object sender, EventArgs e)
    {
        try
        {
            this.BindLV(this.txt_keyword.Text.Trim());
            CH.CloseParentWatting(this.btn_search);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.btn_search);
            CH.Alert(ex.Message, this.btn_search);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            this.txt_keyword.Text = "";
            this.BindLV(this.txt_keyword.Text.Trim());
            CH.CloseParentWatting(this.btn_search);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.btn_search);
            CH.Alert(ex.Message, this.btn_search);
        }
    }
}
