using LU.Data;
using System;
using System.Data;

/// <summary>
/// Summary description for OrderMailContent
/// </summary>
public class OrderMailContent
{
    LU.Data.nicklu2Entities _context;

    public OrderMailContent(nicklu2Entities context)
    {
        _context = context;
        //
        // TODO: Add constructor logic here
        //
    }



    public string SendContent(string order_code)
    {
        var email = string.Empty;
        return SendContent(order_code, string.Empty, out email);
    }
    public string SendContent(string order_code, string noteString)
    {
        var email = string.Empty;
        return SendContent(order_code, noteString, out email);
    }
    public string SendContent(string order_code, string noteString, out string email)
    {
        var ohm = OrderHelperModel.GetModelsByOrderCode(_context, int.Parse(order_code));
        if (ohm.Length != 1)
            throw new Exception("Order Code is error!");
        var csm = CustomerStoreModel.FindModelsByOrderCode(_context, order_code);
        if (csm.Length != 1)
            throw new Exception("CustomerStore Is not Exist");

        string invoice = ohm[0].order_code.ToString();
        string customer_no = Code.FilterCustomerCode(ohm[0].customer_serial_no.ToString());

        string saleAddress = (csm[0].customer_address1 ?? "").Trim();
        string saleZipCode = "";
        string saleCity = "";
        string saleSate = "";
        email = csm[0].customer_email1;

        if (saleAddress.Length > 2)
        {
            saleZipCode = csm[0].zip_code;
            saleCity = csm[0].customer_city;
            saleSate = csm[0].state_code;
        }

        // pay method 
        string pay_method_name = "";
        string special_cash_discount = "";
        if (ohm[0].input_order_discount > 0)
        {

            special_cash_discount = @"<div class=""EC_MsoNormal"">
                        <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                            font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                            font-family: 'Courier New'"">-" + Config.ConvertPrice((ohm[0].input_order_discount.HasValue && ohm[0].input_order_discount.Value > 0) ? ohm[0].input_order_discount.Value : ohm[0].input_order_discount.Value) + @"</span></td><td style=""font-size: 10pt; color: navy;
                            font-family: 'Courier New'"">SPECIAL CASH DISCOUNT</td></tr></table></span></font></div>";
        }
        //
        // pay method name
        //
        int pay_method_id = 0;
        int.TryParse(ohm[0].pay_method, out pay_method_id);
        pay_method_name = @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;Pay Method:&nbsp;" + PayMethodNewModel.GetPayMethodNewModel(_context, pay_method_id).pay_method_name + @"</span></font>
        </div>";

        //
        //
        // pst, gst, hst
        string pst_gst_hst = string.Empty;

        if (ohm[0].gst_rate > 0M)
        {
            pst_gst_hst = @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""></span></font><font color=""navy"" face=""Courier New""
                    size=""2""><span style=""font-size: 10pt; color: navy; font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].gst.Value) + @"</span></td><td><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">GST(" + ohm[0].gst_rate.Value.ToString("###") + @"%)</span></td></tr></table></span></font></div>";
        }

        if (ohm[0].pst_rate > 0M)
        {
            pst_gst_hst += @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""></span></font><font color=""navy"" face=""Courier New""
                    size=""2""><span style=""font-size: 10pt; color: navy; font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].pst.Value) + @"</span></td><td><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">PST(" + ohm[0].pst_rate.Value.ToString("###") + @"%)</span></td></tr></table></span></font></div>";
        }

        if (ohm[0].hst_rate > 0M)
        {
            pst_gst_hst += @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""></span></font><font color=""navy"" face=""Courier New""
                    size=""2""><span style=""font-size: 10pt; color: navy; font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].hst.Value) + @"</span></td><td><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">HST(" + ohm[0].hst_rate.Value.ToString("###") + @"%)</span></td></tr></table></span></font></div>";
        }

        if (ohm[0].weee_charge > 0M)
        {
            pst_gst_hst += @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""></span></font><font color=""navy"" face=""Courier New""
                    size=""2""><span style=""font-size: 10pt; color: navy; font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].weee_charge.Value) + @"</span></td><td><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">WEEE</span></td></tr></table></span></font></div>";
        }


        //
        // shipping content
        //
        string shipping_string = "";
        if (Config.pay_method_pick_up_ids.IndexOf("[" + pay_method_id.ToString() + "]") == -1)
        {
            if ((csm[0].customer_shipping_address ?? "").Trim().Length > 5)
            {
                shipping_string = @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;SHIP TO:</span></font></div>
" + ((csm[0].customer_company ?? "") == "" ? "" : @"<div class=""EC_MsoNormal"">
            <span style=""font-size: 10pt; color: navy;font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_company.Trim()) + @"</span></div>" + @"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_shipping_first_name + "&nbsp;" + csm[0].customer_shipping_last_name + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_shipping_address + @"</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_shipping_city + "&nbsp;" + csm[0].shipping_state_code + "&nbsp;" + csm[0].customer_shipping_zip_code + @" </span></font>
        </div>";
            }
            else
            {
                shipping_string = @"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;SHIP TO:</span></font></div>
" + ((csm[0].customer_company ?? "") == "" ? "" : @"<div class=""EC_MsoNormal"">
            <span style=""font-size: 10pt; color: navy;font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_company.Trim()) + @"</span></div>" + @"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_shipping_first_name + "&nbsp;" + csm[0].customer_shipping_last_name + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp; </span></font>
        </div>";
            }

        }

        var noteHtml = string.Empty;
        if (!string.IsNullOrEmpty(noteString))
        {
            noteHtml = string.Format("<div style='padding:1em;'>{0}</div><hr size=1>", noteString);
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();


        sb.Append(@"<html><head><title>LU Computers</title></head><body><div class=""EC_MsoNormal"">
            " + noteHtml + @"
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">LU COMPUTERS ORDER(#" + invoice + @") CONFIRMATION &nbsp;&nbsp;THANK YOU FOR YOUR BUSINESS!&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">LU COMPUTERS&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <!--div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">1875 Leslie Street.</span></font><font color=""navy"" face=""Courier New""
                    size=""2""><span style=""font-size: 10pt; color: navy; font-family: 'Courier New'"">Unit 24</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Toronto</span></font><font color=""navy"" face=""Courier New""
                    size=""2""><span style=""font-size: 10pt; color: navy; font-family: 'Courier New'"">,Ontario,
                        M3B 2M5, Canada</span></font> &nbsp; <font color=""navy"" face=""Courier New"" size=""2"">
                            <span style=""font-size: 10pt; color: navy; font-family: 'Courier New'""></span></font>
        </div-->
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Tel: (416)446-7743&nbsp;Toll Free: (866)999-7828&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Email: sales@lucomputers.com&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""></span></font>&nbsp;</div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;Order No.:&nbsp;" + invoice + @"</span></font></div>
 <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;eBay User id:&nbsp;" + (csm[0].EBay_ID ?? "") + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;CUSTOMER No.:&nbsp;" + customer_no + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + ohm[0].create_datetime.ToString("dd/MM/yyyy") + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;SALE TO:</span></font></div>
" + ((csm[0].customer_company ?? "") == "" ? "" : @"<div class=""EC_MsoNormal"">
            <span style=""font-size: 10pt; color: navy;font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_company.Trim()) + @"</span></div>" + @"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + csm[0].customer_first_name + "&nbsp;" + csm[0].customer_last_name + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + (csm[0].customer_address1 ?? "") + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + saleCity + "&nbsp;&nbsp;" + saleSate + "&nbsp;&nbsp;" + saleZipCode + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font></div>
        " + shipping_string + @"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;" + ((csm[0].phone_d ?? "").Trim().Length > 2 ? "Business&nbsp;Phone:&nbsp;" + csm[0].phone_d : "") + "&nbsp;&nbsp;" + ((csm[0].phone_n ?? "").Trim().Length > 2 ? "Home&nbsp;Phone:&nbsp;" + csm[0].phone_n : "") + "&nbsp;&nbsp;" + ((csm[0].phone_c ?? "").Trim().Length > 2 ? "Mobile&nbsp;Phone:&nbsp;" + csm[0].phone_c : "") + @"</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;EMAIL ADDRESS:&nbsp;" + csm[0].customer_email1 + @"</span></font>
        </div>" + pay_method_name);
        sb.Append(@" <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""><hr size='1'></span>
            </font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Canadian Dollars&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].sub_total_rate.Value) + @"</span></td><td style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Sub-Total</td></tr></table></span></font></div>
        " + special_cash_discount + @"        
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].shipping_charge.Value) + @"</span></td><td style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Shipping Insurance
                &amp; Handling</td></tr></table></span></font></div>
        " + pst_gst_hst + @"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].grand_total.Value) + @"</span></td><td style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Total</td></tr></table></span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""><table><tr><td width='40'>&nbsp;</td><td width='150'><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(ohm[0].grand_total.Value) + @"</span></td><td style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Grand Total in " + ohm[0].price_unit + @"</td></tr></table></span></font></div>
        ");
        sb.Append(@"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'""><hr size='1'></span>
            </font>
        </div>");
        sb.Append(@"
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font></div>");

        var opm = OrderProductModel.GetModelsByOrderCode(_context, order_code);
        for (int i = 0; i < opm.Length; i++)
        {

            sb.Append(@"<div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + Config.ConvertPrice(opm[i].order_product_sold.Value) + @" x " + opm[i].order_product_sum.ToString() + "&nbsp;&nbsp;" + (order_code.Length == 5 ? ((opm[i].ebayItemID == null || opm[i].ebayItemID.Length < 7) ? opm[i].product_serial_no.ToString() : opm[i].ebayItemID) : opm[i].product_serial_no.ToString()) + @"&nbsp;&nbsp;" + opm[i].product_name + @"&nbsp;&nbsp;</span></font>
        </div>");
            if (opm[i].product_serial_no.ToString().Length == 8)
            {
                //DataTable spm = SpTmpDetailModel.GetModelsBySysCode(opm[i].product_serial_no.ToString());
                DataTable spm = OrderProductSysDetailModel.GetModelsBySysCode(opm[i].product_serial_no.ToString());

                sb.Append("<table cellpadding=\"0\" cellspacing=\"0\" width=\"98%\">");
                for (int j = 0; j < spm.Rows.Count; j++)
                {
                    if (spm.Rows[j]["product_name"].ToString().ToLower() != "none selected" || spm.Rows[j]["product_name"].ToString().ToLower().IndexOf("onboard") != -1)
                    {
                        sb.Append(@"<tr><td style=""width: 20px;font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;&nbsp;(" + spm.Rows[j]["product_serial_no"].ToString() + @")&nbsp;</td><td style=""width: 20px;font-size:10pt; color: navy;font-family: 'Courier New'""> " + (spm.Rows[j]["part_quantity"].ToString() == "1" ? "" : spm.Rows[j]["part_quantity"].ToString() + "x ") + @"</td><td style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">" + spm.Rows[j]["product_name"].ToString() + @" &nbsp;</td></tr>
       ");
                    }
                }
                sb.Append("</table>");
            }
        }
        sb.Append(@"
        
      
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Thank you,</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Lu Computers</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Toll Free 1(866)999-7828  Local (416)446-7743</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Monday – Friday 10.30 – 7.30 EST</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                font-family: 'Courier New'"">Saturday 11.00 – 4.30 EST</span></font></div>
        <div class=""EC_MsoNormal"">
            <font color=""#999999"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: #999999;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""#999999"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: #999999;
                font-family: 'Courier New'"">______________________________________________________&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <font color=""#999999"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: #999999;
                font-family: 'Courier New'"">&nbsp;&nbsp;&nbsp;</span></font>
        </div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699"">Sales are subject to LU Computers Sales terms and policy.&nbsp;</span>
            </font></span>
        </div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699""></span></font></span>&nbsp;</div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699"">No credit for any item that can be replaced. Any returned product must be complete and unused. All returns must be in their original packing material, and be in re-saleable condition. Credit will not be issued unless the above conditions are met.  All returns are subject to  15% or $40 CAD minimum whichever is greater. Software and consumable items cannot be returned for credit or replacement.</span>
            </font></span>
        </div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699""></span></font></span>&nbsp;</div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699"">Returned check subject to $20 CAD charge. Late payment shall result in interest charge of two percent for any calendar month or part thereof for which payment or partial payment remains due. All responsible costs and expenses suffered by LU in collecting monies due including but not limited to attorney's fees and collection agency fees shall be paid by the purchaser.&nbsp;</span></font>
            </span>
        </div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699""></span></font></span>&nbsp;</div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699"">Warranty claimed items must be shipped /carried in at customer's cost. Returned shipment without a LU issued RMA (Return Merchandise Authorization) number will be rejected. Warranty does not cover any third party costs or fees incurred by customer for any reason including but not limited to labor, delivery, diagnosis, technical support, product repair, product installation etc..&nbsp;</span></font></span>
        </div>
        <div class=""EC_MsoNormal"">
            <font face=""Times New Roman"" size=""1""><span style=""font-size: 8pt""></span></font>
            &nbsp;</div>
        <div class=""EC_MsoNormal"">
            <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt;
                color: #666699"">Copyright © " + DateTime.Now.Year.ToString() + @" LU Computers. All Rights Reserved.</span></font></span><font
                    color=""#666699"" face=""Arial"" size=""1""><span style=""font-size: 7.5pt; color: #666699;
                        font-family: Arial""><br />
                        <span class=""EC_fontfoot1""><font color=""#666699"" face=""Arial""><span style=""color: #666699"">
                            Designated trademarks and brands are the property of their respective owners.</span></font></span></span></font><font
                                color=""navy"" face=""Courier New"" size=""2""><span style=""font-size: 10pt; color: navy;
                                    font-family: 'Courier New'""></span></font></div></body></html>");
        return sb.ToString();
    }
}