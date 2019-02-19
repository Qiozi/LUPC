using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_Manager_eBay_eBayPromoitionalSaleManage : PageBase
{
    public string Token { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Token = WebapiToken.GetToken();

            InitialDatabase();

            DataTable dt = Config.ExecuteDataTable(@"select p.product_serial_no Sku,e.SaleName Title,
 p.product_ebay_name PartName, 
 e.SaleID eBaySaleId,
 e.StartTime StartDate,
 e.EndTime EndDate,
 p.product_current_cost Cost,
 p.product_current_price-p.product_current_discount price,
 Concat(s.BuyItNowPrice, s.BuyitNowPrice_currencyID) eBayPrice,
 e.DiscountValue Discount,
 s.ItemId
from tb_ebay_promotional_sale_details e
inner join tb_ebay_selling s on e.SaleItemId=s.ItemId
inner join tb_product p on p.product_serial_no = s.luc_sku order by e.SaleName asc, p.product_ebay_name asc");
            this.rptList.DataSource = dt;
            this.rptList.DataBind();

            this.txtBeginSale.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd");
            this.txtEndDate.Text = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            BindSaleIds();
        }
    }

    void BindSaleIds()
    {
        this.ddlSaleIds.DataSource = Config.ExecuteDataTable("select promotionalSaleId, concat(promotionalSaleId, '-(',price, ')-', enddate) txt from tb_ebay_promotional_sale_id order by id desc ");
        this.ddlSaleIds.DataTextField = "txt";
        this.ddlSaleIds.DataValueField = "promotionalSaleId";
        this.ddlSaleIds.DataBind();
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
    }

    protected void btnCreateSaleId_Click(object sender, EventArgs e)
    {
        this.lblCreateOnsaleNote.Text = "";
        var title = this.txtSaleTitle.Text.Trim();
        var cost = string.IsNullOrEmpty(this.txtSaleCost.Text) ? 0M : decimal.Parse(this.txtSaleCost.Text.Trim());

        if (cost > 0M && !string.IsNullOrEmpty(title) &&
            !string.IsNullOrEmpty(this.txtBeginSale.Text) &&
            !string.IsNullOrEmpty(this.txtEndDate.Text))
        {
            var token = WebapiToken.GetToken();
            var result = new HttpHelper().HttpPost("http://webapi.lucomputers.com/Api/SetPromotionalSale"
                 , string.Format("t={0}&BeginDate={1}&EndDate={2}&Title={3}&Discount={4}&AutoRun=false"
                    , token
                    , this.txtBeginSale.Text
                    , this.txtEndDate.Text.Trim()
                    , title
                    , cost)
                 , "80"
                 , string.Empty);
            this.lblCreateOnsaleNote.Text = result;
            BindSaleIds();
        }
    }

    protected void btnAddSKU_Click(object sender, EventArgs e)
    {
        this.lblAddItemNote.Text = "";
        var sku = this.txtSku.Text.Trim();
        var saleid = this.ddlSaleIds.SelectedValue.ToString();

        if (!string.IsNullOrEmpty(sku))
        {

            var dt = Config.ExecuteDataTable("select itemid from tb_ebay_selling where luc_sku = '" + sku + "' or sys_sku ='" + sku + "'");
            if (dt.Rows.Count != 1)
            {
                this.lblAddItemNote.Text = "ebay selling info is error.";
            }
            var discount = Config.ExecuteScalar("Select price from tb_ebay_promotional_sale_id where PromotionalSaleID='" + saleid + "'");

            Config.ExecuteNonQuery(string.Format(@"insert into tb_ebay_promotional_items 
	(luc_sku, IsSys, Regdate, SaleId, ItemId, SavePrice)
	values
	('{0}', '{1}', now(), '{2}', '{3}', '{4}')"
                , sku
                , sku.ToString().Length == 6
                , saleid
                , dt.Rows[0][0].ToString()
                , discount));
            this.lblAddItemNote.Text = "success";
        }
    }
}