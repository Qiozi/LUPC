using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_orders : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin)
            {
                switch (ReqCmd)
                {

                    case "getPriceListForCart":
                        WritePriceListForCart();
                        break;

                    case "getPickupTimeArea":
                        #region get pickup time
                        List<KeyValuePair<string, string>> timearea = new List<KeyValuePair<string, string>>();

                        for (int i = 0; i < 15; i++)
                        {
                            timearea.Add(new KeyValuePair<string, string>(DateTime.Now.AddDays(i).ToString("dd/MM/yyyy"), DateTime.Now.AddDays(i).ToString("yyyy-MM-dd")));// (DateTime.Now.AddDays(i).ToString("dd/MM/yyyy"));
                        }
                        string timeJson = Newtonsoft.Json.JsonConvert.SerializeObject(timearea);
                        Response.Write(timeJson);
                        #endregion
                        break;

                    case "SaveMsg":
                        int oc;
                        int.TryParse(Util.GetStringSafeFromQueryString(Page, "orderCode"), out oc);
                        OrderHelper.SaveOrderNote(Util.GetStringSafeFromQueryString(Page, "cont")
                            , oc
                            , false
                            , db);
                        Response.Write("OK");
                        break;
                }
            }
        }
        Response.End();
    }

    /// <summary>
    /// 购物车价格列表
    /// 
    /// 显示订单的两种价格， 一种是信用卡价，一种是现金价
    ///    
    /// 
    /// </summary>
    void WritePriceListForCart()
    {
        // 计算现金价
        OrderHelper.AccountOrderPrice(CurrOrderCode
            , ReqStateID
            , ReqShippingCompanyID
            , CurrSiteCountry
            , PRate
            , db);

        string result = "";
        string oc = CurrOrderCode.ToString();
        var tempCart = db.tb_cart_temp_price.FirstOrDefault(p => p.order_code.Equals(oc));
        if (tempCart != null)
        {
            decimal taxable = tempCart.sub_total.Value - tempCart.sur_charge.Value + tempCart.shipping_and_handling.Value;
            decimal taxAll = 0M;

            // 现金价
            result += "<div class='row'>";
            result += "<div class='col-md-6'>";
            result += @"<div class=""panel panel-default"" id='panelPrice1'>
  <div class=""panel-heading"">Cash/Bank Transfer/Money Order payment</div>
  <div class=""panel-body"">";
            result += "<table >";

            result += string.Format("<tr><td class='text-right'>{0}</td><td width=120 class='text-right price'>${1}</td><td width=50><small>{2}</small></td></tr>", "SUBTOTAL", PriceRate.Format(tempCart.sub_total.Value), tempCart.price_unit);
            result += string.Format("<tr><td class='text-right'>{0}</td><td width=120 class='text-right price' style='color:blue;'>$-{1}</td><td width=50><small>{2}</small></td></tr>", "SPECIAL CASH DISCOUNT", PriceRate.Format(tempCart.sur_charge.Value), tempCart.price_unit);
            result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "SHIPPING AND HANDLING", PriceRate.Format(tempCart.shipping_and_handling.Value), tempCart.price_unit);
            result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "TAXABLE TOTAL (If applicable)", PriceRate.Format(taxable), tempCart.price_unit);

            if (setting.HstStates.Contains(ReqStateID))
            {
                taxAll = (taxable) * tempCart.hst_rate.Value / 100;
                result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "HST(" + tempCart.hst_rate + "%)", PriceRate.Format(taxAll), tempCart.price_unit);
            }
            else
            {
                taxAll = (taxable) * tempCart.gst_rate.Value / 100 + (taxable) * tempCart.pst_rate.Value / 100;
                result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "PST", PriceRate.Format(taxable * tempCart.pst_rate.Value / 100), tempCart.price_unit);
                result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "GST", PriceRate.Format(taxable * tempCart.gst_rate.Value / 100), tempCart.price_unit);
            }
            result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "GRAND TOTAL", PriceRate.Format(taxable + taxAll), tempCart.price_unit);
            result += "</table>";

            result += "</div></div></div>";


            // 信用卡价
            result += "<div class='col-md-6'>";
            result += @"<div class=""panel panel-default"" id='panelPrice2'>
  <div class=""panel-heading"">Paypal/Credit Card/Personal Check/Company Check payment</div>
  <div class=""panel-body"">";
            result += "<table >";

            tempCart.sur_charge = 0M;
            tempCart.taxable_total = tempCart.sub_total + tempCart.shipping_and_handling;

            result += string.Format("<tr><td class='text-right'>{0}</td><td width=120 class='text-right price'>${1}</td><td width=50><small>{2}</small></td></tr>", "SUBTOTAL", PriceRate.Format(tempCart.sub_total.Value), tempCart.price_unit);
            result += string.Format("<tr><td class='text-right'>{0}</td><td width=120 class='text-right price'>${1}</td><td width=50><small>{2}</small></td></tr>", "SPECIAL CASH DISCOUNT", "0.00", tempCart.price_unit);
            result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "SHIPPING AND HANDLING", PriceRate.Format(tempCart.shipping_and_handling.Value), tempCart.price_unit);
            result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "TAXABLE TOTAL (If applicable)", PriceRate.Format(tempCart.taxable_total.Value), tempCart.price_unit);
            if (setting.HstStates.Contains(ReqStateID))
            {
                taxAll = (tempCart.taxable_total.Value) * tempCart.hst_rate.Value / 100;
                result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>${1}</td><td><small>{2}</small></td></tr>", "HST(" + tempCart.hst_rate + "%)", PriceRate.Format(taxAll), tempCart.price_unit);

            }
            else
            {
                decimal pst = (tempCart.taxable_total.Value) * tempCart.pst_rate.Value / 100;
                decimal gst = (tempCart.taxable_total.Value) * tempCart.gst_rate.Value / 100;
                taxAll = pst + gst;
                result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>{1}</td><td><small>{2}</small></td></tr>", "PST", PriceRate.Format(pst), tempCart.price_unit);
                result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>{1}</td><td><small>{2}</small></td></tr>", "GST", PriceRate.Format(gst), tempCart.price_unit);
            }
            result += string.Format("<tr><td class='text-right'>{0}</td><td class='text-right price'>{1}</td><td><small>{2}</small></td></tr>", "GRAND TOTAL", PriceRate.Format(tempCart.taxable_total.Value + taxAll), tempCart.price_unit);
            result += "</table>";
            result += "</div></div></div>";


            result += "</div>";
        }
        else
        {
            result = "<p class='well' style='width:500px;'>NO NO</p>";
        }
        //Response.Write(tempCart.sub_total.ToString());
        Response.Write(result);
    }


    /// <summary>
    /// 
    /// </summary>
    int ReqStateID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "stateid", 0); }
    }

    /// <summary>
    /// 
    /// </summary>
    int ReqShippingCompanyID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shippingid", 0); }
    }
}