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

public partial class Q_Admin_ebay_list_part : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            PageSize = 5;

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
    
    private void BindLV(string keyword)
    {
        EbayStoreModel esm = new EbayStoreModel();
        int count = 0;
        this.ListView1.DataSource = esm.FindModelsBySystem(PageSize, StartRecord, keyword, ref count, 1);
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

                EbayStoreModel esd = EbayStoreModel.GetEbayStoreModel(id);
                esd.ebay_code = ebaycode;
                esd.price = price;
                esd.Update();
                //CH.Alert(string.Format("{0}|{1}|{2}|{3}", id, ebaycode, price, _cb_is_templete.Checked.ToString()), this.ListView1);
                BindLV(this.txt_keyword.Text.Trim());
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
    protected void btn_clear_search_Click(object sender, EventArgs e)
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
