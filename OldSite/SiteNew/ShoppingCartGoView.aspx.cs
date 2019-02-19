using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartGoView : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsSignIn();
            if (!IsLogin || !IsLocalHostFrom)
            {
                Response.Write("NO...");
                Response.End();
            }
            int paymentCardDoDirect = 0;


            #region prod list
            var cartTemp = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                && p.cart_temp_code.Value.Equals(this.cookiesHelper.CurrOrderCode)).ToList();

            var haveSysProd = cartTemp.FirstOrDefault(p => p.product_serial_no.HasValue
                && p.product_serial_no.Value.ToString().Length == 8) != null;

            string prodListString = "<table class=\"table table-striped\" width='100%' id='tableProdList'><thead><tr><th></th><th>Description</th><th>QTY</th><th>Unit Price</th><th>Total</th></thead><tbody>";

            foreach (var ct in cartTemp)
            {
                string logostr = "";

                #region 取得 logo 字符串
                if (ct.product_serial_no.Value.ToString().Length != 8)
                {
                    // 零件 logo
                    int partID = ct.product_serial_no.Value;

                    var img = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(partID));
                    if (img != null)
                    {
                        logostr = "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + (img.other_product_sku.Value > 0 ? img.other_product_sku.Value : img.product_serial_no) + "_t.jpg' alt='...' width='40'>";
                    }
                }
                else
                {
                    // 机箱 logo 
                    string sysID = ct.product_serial_no.Value.ToString();
                    var caseModel = db.tb_sp_tmp_detail.FirstOrDefault(p => p.sys_tmp_code.Equals(sysID)
                        && p.cate_name.ToLower() == "case");
                    if (caseModel != null)
                    {
                        int caseid = caseModel.product_serial_no.Value;
                        var img = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(caseid));
                        if (img != null)
                        {
                            logostr = "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + (img.other_product_sku.Value > 0 ? img.other_product_sku.Value : img.product_serial_no) + "_t.jpg' alt='...' width='40'>";
                        }
                    }
                }
                #endregion

                prodListString += "<tr " + (haveSysProd ? " class='success'" : "") + "><td>" + logostr + "</td><td>" + ct.product_name + "</td><td width='50'>" + ct.cart_temp_Quantity + "</td><td width='80' class='text-right'>$" + ct.price + "</td><td width='90' class='text-right'>$" + LU.BLL.FormatProvider.Price(ct.price.Value * ct.cart_temp_Quantity.Value) + "</td></tr>";
                if (ct.product_serial_no.Value.ToString().Length == 8)
                {
                    #region 系统列表明细
                    string sysCode = ct.product_serial_no.Value.ToString();
                    var sysPartList = (from sp in db.tb_sp_tmp_detail
                                       join p in db.tb_product on sp.product_serial_no.Value equals p.product_serial_no
                                       where sp.sys_tmp_code.Equals(sysCode)
                                       select new
                                       {
                                           PartSKU = p.product_serial_no,
                                           CommName = sp.cate_name,
                                           Title = sp.product_name,
                                           ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no
                                       }).ToList();

                    string partDetailString = "<table class='table'>";
                    for (int i = 0; i < sysPartList.Count; i++)
                    {
                        if (sysPartList[i].PartSKU != setting.NoneSelectedID)
                        {
                            partDetailString += string.Format(@"<tr><td>{0}</td><td>{1}</td><td>{2}</td></tr>"
                                , "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + sysPartList[i].ImgSku + "_t.jpg' alt='...' width='20'>"
                                , sysPartList[i].CommName
                                , sysPartList[i].Title);
                        }
                    }
                    partDetailString += "</table>";

                    prodListString += string.Format(@"<tr>
                                                <td></td>
                                                <td colspan='4'>{0}</td>
                                                </tr>"
                        , partDetailString
                        );
                    #endregion
                }

                PaymentID = ct.pay_method.Value;
                paymentCardDoDirect = PaymentID;
                PayMent = Payment.GetPaymentText(ct.pay_method.Value, db);

                OrderDate = DateTime.Now.ToShortDateString();// ct.create_datetime.Value.ToShortDateString();

                if (setting.LocalAll.Contains(ct.pay_method.Value))
                {
                    if (ct.pick_datetime_1 != null)
                        ltShipCompany.Text = "<p class='text-center'>Local pick up time : <strong>" + ct.pick_datetime_1.Value.ToString("MM/dd/yyyy HH:mm") + "</strong></p>";
                }
                else
                {
                    if (ct.shipping_company.HasValue
                        && ct.shipping_company.Value > 0)
                    {
                        var scid = ct.shipping_company.Value;

                        var shipComp = db.tb_shipping_company.FirstOrDefault(p => p.shipping_company_id.Equals(scid));
                        if (shipComp != null)
                        {
                            string shipcompanystr = "<p class='text-center'>Shipping: <strong>" + shipComp.shipping_company_name + "</strong></p>";

                            int csn;
                            var customer = CurrCustomer;
                            if (customer != null && !IsToPaypal)
                            {
                                shipcompanystr += string.Format(@"<div style='width:500px; margin:auto auto'><address>{0} {1}<br>{2}<br>{3} {4} {5}<br>{6}</address></div>"
                                    , customer.customer_shipping_first_name
                                    , customer.customer_shipping_last_name
                                    , customer.customer_shipping_address
                                    , customer.customer_shipping_city
                                    , customer.shipping_state_code
                                    , customer.shipping_country_code
                                    , customer.customer_shipping_zip_code

                                    );
                            }
                            ltShipCompany.Text = shipcompanystr;

                        }
                    }
                }
            }
            prodListString += "</table>";
            ltOrderProdList.Text = prodListString;

            #endregion

            #region order price area
            string oc = this.cookiesHelper.CurrOrderCode.ToString();
            var tempPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));

            decimal specialCashDiscount = 0M;
            decimal subTotal = 0M;
            decimal shippingAndHandling = 0M;
            decimal taxable_total = 0M;
            decimal grand_total = 0M;
            decimal tax = 0M;
            if (tempPrice != null)
            {
                subTotal = tempPrice.sub_total.Value;
                specialCashDiscount = !setting.PriceIsCard.Contains(PaymentID) ? LU.BLL.PRateProvider.Multiply(subTotal, setting.CardRate) : 0M;

                shippingAndHandling = tempPrice.shipping_and_handling.Value;
                taxable_total = subTotal + shippingAndHandling - specialCashDiscount;
                tax = (LU.BLL.PRateProvider.Multiply(taxable_total, tempPrice.pst_rate.Value / 100))
                    + (LU.BLL.PRateProvider.Multiply(taxable_total, tempPrice.gst_rate.Value / 100))
                    + (LU.BLL.PRateProvider.Multiply(taxable_total, tempPrice.hst_rate.Value / 100));


                grand_total = taxable_total + tax;
                tempPrice.sales_tax = tax;
                tempPrice.taxable_total = taxable_total;
                tempPrice.grand_total = grand_total;
                tempPrice.grand_total_rate = grand_total;
                db.SaveChanges();
            }


            string priceString = string.Format(@"<div class=""row"">
                <div class=""col-md-6""></div>
                <div class=""col-md-3 text-right"">SUBTOTAL</div>
                <div class=""col-md-3 text-right price"">${0} <small>{4}</small></div>
            </div>
            <div class=""row"">
                <div class=""col-md-6""></div>
                <div class=""col-md-3 text-right"">SPECIAL CASH DISCOUNT</div>
                <div class=""col-md-3 text-right price"" style='color:blue;'>$-{6} <small>{4}</small></div>
            </div>

            <div class=""row"">
                <div class=""col-md-6""></div>
                <div class=""col-md-3 text-right"">SHIPPING AND HANDLING</div>
                <div class=""col-md-3 text-right price"">${1} <small>{4}</small></div>
            </div>
            <div class=""row"">
                <div class=""col-md-6""></div>
                <div class=""col-md-3 text-right"">TAXABLE TOTAL (If applicable)</div>
                <div class=""col-md-3 text-right price"">${5} <small>{4}</small></div>
            </div>
            {2}
            <div class=""row"">
                <div class=""col-md-6""></div>
                <div class=""col-md-3 text-right"">GRAND TOTAL</div>
                <div class=""col-md-3 text-right price"">${3} <small>{4}</small></div>
            </div>"
                , LU.BLL.FormatProvider.Price((subTotal))
                , LU.BLL.FormatProvider.Price((shippingAndHandling))
                , GetTaxString(tempPrice, (taxable_total))
                , LU.BLL.FormatProvider.Price((grand_total))
                , tempPrice != null ? tempPrice.price_unit : ""
                , LU.BLL.FormatProvider.Price((taxable_total))
                , LU.BLL.FormatProvider.Price((specialCashDiscount))
                );

            ltOrderPriceArea.Text = priceString;
            #endregion

            if (IsToPaypal)
            {
                Session["payment_amt"] = grand_total.ToString();
                Session["payment_unit"] = tempPrice.price_unit;
                Session["paypalOrderCode"] = this.cookiesHelper.CurrOrderCode.ToString();
                btnNext.HRef = "/CheckOutPaypal.aspx";
                btnNext.Attributes.Remove("class");
                btnNext.InnerHtml = "<img src='https://www.paypalobjects.com/webstatic/en_US/btn/btn_buynow_pp_142x27.png' border='0' align='top' alt='Buy Now with PayPal'/>";
                //btnNext.Visible = false;
                if (IsAutoToPaypal)
                    Response.Redirect(btnNext.HRef, true);
            }
            if (paymentCardDoDirect == 25)
            {
                btnNext.HRef = "/CheckOutCreditCardToPaypal.aspx";
                btnNext.Attributes.Remove("class");
                btnNext.InnerHtml = "<img src='https://www.paypalobjects.com/webstatic/en_US/btn/btn_buynow_pp_142x27.png' border='0' align='top' style='height:34px;' alt='Buy Now with PayPal'/>";
            }
        }
    }
    /// <summary>
    /// Tax  界面字符串
    /// </summary>
    /// <param name="cp"></param>
    /// <returns></returns>
    string GetTaxString(LU.Data.tb_cart_temp_price cp, decimal taxable_total)
    {
        if (cp == null)
            return "";
        if (cp.hst_rate > 0)
        {
            return @"<div class=""row"">
                <div class=""col-md-6""></div>
                <div class=""col-md-3 text-right"">HST(" + cp.hst_rate.Value.ToString("0") + @"%)</div>
                <div class=""col-md-3 text-right price"">$" + LU.BLL.PRateProvider.Multiply(taxable_total, cp.hst_rate.Value / 100) + @" <small>" + cp.price_unit + @"</small></div>
            </div>";
        }

        string result = "";
        if (cp.gst_rate > 0)
        {
            result += @"<div class=""row"">
                        <div class=""col-md-6""></div>
                        <div class=""col-md-3 text-right"">GST(" + cp.gst_rate.Value.ToString("0") + @"%)</div>
                        <div class=""col-md-3 text-right price"">$" + LU.BLL.PRateProvider.Multiply(taxable_total, cp.gst_rate.Value / 100) + @" <small>" + cp.price_unit + @"</small></div>
                    </div>";
        }

        if (cp.pst_rate > 0)
        {
            result += @"<div class=""row"">
                        <div class=""col-md-6""></div>
                        <div class=""col-md-3 text-right"">PST(" + cp.pst_rate.Value.ToString("0") + @"%)</div>
                        <div class=""col-md-3 text-right price"">$" + LU.BLL.PRateProvider.Multiply(taxable_total, cp.pst_rate.Value / 100) + @" <small>" + cp.price_unit + @"</small></div>
                </div>";
        }

        return result;
    }

    public string OrderDate { get; set; }

    public string PayMent { get; set; }

    public int PaymentID { get; set; }

    /// <summary>
    /// 返回的地址
    /// </summary>
    public string ReqParentUrl
    {
        get { return Util.GetStringSafeFromQueryString(Page, "purl"); }
    }

    /// <summary>
    /// 跳转到 paypal
    /// </summary>
    public bool IsToPaypal
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "istopaypal", 0) == 1; }
    }
    public bool IsAutoToPaypal
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "auto", 0) == 1; }
    }
}