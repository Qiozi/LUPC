using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyOrderView : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsLogin || !IsLocalHostFrom)
            {
                Response.Write("NO...");
                Response.End();
            }

            #region prod list
            var oc = ReqOrderCode.ToString();
            var cartTemp = db.tb_order_product.Where(p => p.order_code.Equals(oc)).ToList();

            var haveSysProd = cartTemp.FirstOrDefault(p => p.product_serial_no.HasValue
                && p.product_serial_no.Value.ToString().Length == 8) != null;

            var oh = db.tb_order_helper.FirstOrDefault(p => p.order_code.HasValue
                && p.order_code.Value.Equals(ReqOrderCode));
            if (oh == null)
            {
                Response.Write("Order is not find.");
                Response.End();
            }

            var custStore = db.tb_customer_store.FirstOrDefault(p => p.order_code.HasValue
                && p.order_code.Value.Equals(ReqOrderCode));
            ShippingString = "";
            if (custStore != null)
            {
                ShippingString += string.Format(@"
<h4>Shipping Address:</h4>
{0}<br>
{1}<br>
{2} {3} {4}<br>
{5}<br>
"
                    , custStore.customer_shipping_first_name + " " + custStore.customer_shipping_last_name
                    , custStore.customer_shipping_address
                    , custStore.customer_shipping_city
                    , custStore.shipping_state_code
                    , custStore.shipping_country_code
                    , custStore.customer_shipping_zip_code);
            }

            string prodListString = "<table class=\"table table-striped\" width='100%' id='tableProdList'><thead><tr><th></th><th>Description</th><th>QTY</th><th nowrap>Unit Price</th><th>Total</th></thead><tbody>";

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

                prodListString += "<tr " + (haveSysProd ? " class='success'" : "") + "><td>" + logostr + "</td><td>" + ct.product_name + "</td><td width='50'>" + ct.order_product_sum + "</td><td width='80' class='text-right'>$" + ct.order_product_price + "</td><td width='90' class='text-right'>$" + PriceRate.Format(ct.order_product_price.Value * ct.order_product_sum.Value) + "</td></tr>";
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
                                                
                                                <td colspan='5'>{0}</td>
                                                </tr>"
                        , partDetailString
                        );
                    #endregion
                }
                int pid;
                int.TryParse(oh.pay_method, out pid);
                PaymentID = pid;
                PayMent = Payment.GetPaymentText(PaymentID, db);

                OrderDate = DateTime.Now.ToShortDateString();// ct.create_datetime.Value.ToShortDateString();

                if (setting.LocalAll.Contains(pid))
                {
                    if (oh.prick_up_datetime1 != null)
                        ltShipCompany.Text = "<p class='text-center'>Local pick up time : <strong>" + oh.prick_up_datetime1.Value.ToString("MM/dd/yyyy HH:mm") + "</strong></p>";
                    ShippingString = "";// 不显示运输地址
                }
                else
                {
                    if (oh.shipping_company.HasValue
                        && oh.shipping_company.Value > 0)
                    {
                        var scid = oh.shipping_company.Value;

                        var shipComp = db.tb_shipping_company.FirstOrDefault(p => p.shipping_company_id.Equals(scid));
                        if (shipComp != null)
                        {
                            ltShipCompany.Text = "<p class='text-center'>Shipping: <strong>" + shipComp.shipping_company_name + "</strong></p>";
                        }
                    }
                }
            }
            prodListString += "</table>";
            ltOrderProdList.Text = prodListString;

            #endregion

            #region order price area
            
            //var tempPrice = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));

            decimal specialCashDiscount = 0M;
            decimal subTotal = 0M;
            decimal shippingAndHandling = 0M;
            decimal taxable_total = 0M;
            decimal grand_total = 0M;
            decimal tax = 0M;
           // if (tempPrice != null)
            {
                subTotal = oh.sub_total.Value;
                specialCashDiscount = !setting.PriceIsCard.Contains(PaymentID) ? PriceRate.Multiply(subTotal, setting.CardRate) : 0M;

                shippingAndHandling = oh.shipping_charge.Value;
                taxable_total = subTotal + shippingAndHandling - specialCashDiscount;
                tax = (PriceRate.Multiply(taxable_total, oh.pst_rate.Value / 100))
                    + (PriceRate.Multiply(taxable_total, oh.gst_rate.Value / 100))
                    + (PriceRate.Multiply(taxable_total, oh.hst_rate.Value / 100));

                grand_total = taxable_total + tax;
            }


            string priceString = string.Format(@"            <div class=""row"">
                <div class=""col-sm-6""></div>
                <div class=""col-sm-3 text-right"">SUBTOTAL</div>
                <div class=""col-sm-3 text-right price"">${0} <small>{4}</small></div>
            </div>
            <div class=""row"">
                <div class=""col-sm-6""></div>
                <div class=""col-sm-3 text-right"">SPECIAL CASH DISCOUNT</div>
                <div class=""col-sm-3 text-right price"" style='color:blue;'>$-{6} <small>{4}</small></div>
            </div>

            <div class=""row"">
                <div class=""col-sm-6""></div>
                <div class=""col-sm-3 text-right"">SHIPPING AND HANDLING</div>
                <div class=""col-sm-3 text-right price"">${1} <small>{4}</small></div>
            </div>
            <div class=""row"">
                <div class=""col-sm-3""></div>
                <div class=""col-sm-6 text-right"">TAXABLE TOTAL (If applicable)</div>
                <div class=""col-sm-3 text-right price"">${5} <small>{4}</small></div>
            </div>
            {2}
            <div class=""row"">
                <div class=""col-sm-6""></div>
                <div class=""col-sm-3 text-right"">GRAND TOTAL</div>
                <div class=""col-sm-3 text-right price"">${3} <small>{4}</small></div>
            </div>"
                , PriceRate.Format(ConvertPrice(subTotal))
                , PriceRate.Format(ConvertPrice(shippingAndHandling))
                , GetTaxString(oh, ConvertPrice(taxable_total))
                , PriceRate.Format(ConvertPrice(grand_total))
                , oh != null ? oh.price_unit : ""
                , PriceRate.Format(ConvertPrice(taxable_total))
                , PriceRate.Format(ConvertPrice(specialCashDiscount))
                );

            ltOrderPriceArea.Text = priceString;
            #endregion
        }
    }
    /// <summary>
    /// Tax  界面字符串
    /// </summary>
    /// <param name="cp"></param>
    /// <returns></returns>
    string GetTaxString(nicklu2Model.tb_order_helper cp, decimal taxable_total)
    {
        if (cp == null)
            return "";
        if (cp.hst_rate > 0)
        {
            return @"<div class=""row"">
                <div class=""col-sm-6""></div>
                <div class=""col-sm-3 text-right"">HST(" + cp.hst_rate.Value.ToString("0") + @"%)</div>
                <div class=""col-sm-3 text-right price"">$" + PriceRate.Multiply(taxable_total, cp.hst_rate.Value / 100) + @" <small>" + cp.price_unit + @"</small></div>
            </div>";
        }

        string result = "";
        if (cp.gst_rate > 0)
        {
            result += @"<div class=""row"">
                        <div class=""col-sm-6""></div>
                        <div class=""col-sm-3 text-right"">GST(" + cp.gst_rate.Value.ToString("0") + @"%)</div>
                        <div class=""col-sm-3 text-right price"">$" + PriceRate.Multiply(taxable_total, cp.gst_rate.Value / 100) + @" <small>" + cp.price_unit + @"</small></div>
                    </div>";
        }

        if (cp.pst_rate > 0)
        {
            result += @"<div class=""row"">
                        <div class=""col-sm-6""></div>
                        <div class=""col-sm-3 text-right"">PST(" + cp.pst_rate.Value.ToString("0") + @"%)</div>
                        <div class=""col-sm-3 text-right price"">$" + PriceRate.Multiply(taxable_total, cp.pst_rate.Value / 100) + @" <small>" + cp.price_unit + @"</small></div>
                </div>";
        }

        return result;
    }

    int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "oc", 0); }
    }

    public string OrderDate { get; set; }

    public string PayMent { get; set; }

    public int PaymentID { get; set; }

    public string ShippingString { get; set; }

}