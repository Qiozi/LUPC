using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GDI = System.Drawing;

public partial class Q_Admin_sale_print_order : OrderPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Response.Write(this.divContent.c.ToString());
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.add_order);

            SetControlsValue();
        }
    }
    

    #region Methods
    public void SetControlsValue()
    {
        if (this.OrderCodeRequest.ToString().Trim().Length == 6
            || this.OrderCodeRequest.ToString().Trim().Length == 5)
        {
            var context = new LU.Data.nicklu2Entities();
            var oh = new OrderHelper(context);
            var ohms = OrderHelperModel.GetModelsByOrderCode(context, this.OrderCodeRequest);
            var csm = CustomerStoreModel.FindModelsByOrderCode(context, this.OrderCodeRequest.ToString());

            for (int i = 0; i < csm.Length; i++)
            {
                var model = csm[i];
                if (string.IsNullOrEmpty(ohms[0].order_invoice))
                    continue;
                string price_unit = "";// string.Format("<span class='price_unit'>{0}</span>", ohms[0].price_unit);
                this.lbl_address_sale.Text = model.customer_address1;
                this.lbl_address_ship.Text = model.customer_shipping_address;
                this.lbl_city_state_zipcode_sale.Text = (model.customer_address1 ?? "").Trim().Length < 2 ? "" : model.customer_city + "&nbsp;" + model.state_code + "&nbsp;" + model.zip_code;
                this.lbl_city_state_zipcode_ship.Text = (model.customer_shipping_address ?? "").Trim().Length < 2 ? "" : model.customer_shipping_city + "&nbsp;" + model.shipping_state_code + "&nbsp;" + model.customer_shipping_zip_code;
                this.lbl_customer_name_sale.Text = model.customer_first_name + "&nbsp;" + model.customer_last_name;
                this.lbl_customer_name_ship.Text = model.customer_shipping_first_name + "&nbsp;" + model.customer_shipping_last_name;
                this.lbl_date.Text = model.store_create_datetime.Value.ToShortDateString();
                this.lbl_email_address.Text = model.customer_email1;
                this.lbl_order_no.Text = oh.FilterOrderCode(ohms[0].order_helper_serial_no.ToString());// this.OrderCodeRequest.ToString();
                this.lbl_p_o_no.Text = model.my_purchase_order;
                this.lbl_tax_exemption_no.Text = model.tax_execmtion;
                this.lbl_customer_no.Text = Code.FilterCustomerCode(model.customer_serial_no.ToString());
                this.CustomerState = model.customer_card_state.Value;
                if (model.customer_company != "")
                {
                    this.lbl_customer_company.Text = model.customer_company + "<br/>";
                    this.lbl_customer_company0.Text = model.customer_company + "<br/>";
                }
                if (ohms.Length == 1)
                {
                    this.lbl_shipping_handling.Text = Config.ConvertPrice(ohms[0].shipping_charge.Value) + price_unit;
                    this.lbl_sub_total.Text = Config.ConvertPrice(ohms[0].sub_total.Value) + price_unit;
                    this.lbl_total.Text = Config.ConvertPrice(ohms[0].grand_total.Value) + price_unit;
                    this.lt_price_unit.Text = string.Format("({0})", ohms[0].price_unit);
                    Tax(ohms[0].weee_charge.Value, ohms[0].gst.Value, ohms[0].gst_rate.Value, ohms[0].pst.Value, ohms[0].pst_rate.Value, ohms[0].hst.Value, ohms[0].hst_rate.Value, price_unit);
                }
                if (ohms[0].input_order_discount > 0)
                {
                    this.literal_splecial_cash_discount.Text = string.Format(@"<tr>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""text-align: right"">
                                Special Cash Discount</td>
                            <td style=""width: 100px; text-align: right"">{0}
                                </td>
                        </tr>", ohms[0].input_order_discount + price_unit);

                }

                if (ohms[0].order_invoice.ToString().Length == Config.OrderInvoiceLength && ohms[0].is_download_invoice.Value)
                {
                    this.lbl_order_no.Text = ohms[0].order_invoice.ToString();
                }
                else
                {
                    this.literal_invoice_title.Text = "Order Code";
                    this.literal_invoice_title2.Text = "ORDER FORM";
                    this.lbl_order_no.Text = ohms[0].order_code.ToString();
                }
            }


            this.liteProductList.Text = GetProductList(this.OrderCodeRequest.ToString());
        }
    }
    //private void Tax(int tax_rate, decimal tax_charge)
    //{
    //    this.lt_pst_gst.Text = "Sales Tax("+tax_rate.ToString()+"%)";
    //    this.lbl_sale_charge.Text = Config.ConvertPrice(tax_charge);
    //}
    private void Tax(decimal weee_charge, decimal gst, decimal gst_rate, decimal pst, decimal pst_rate, decimal hst, decimal hst_rate, string price_unit)
    {
        if (hst_rate > 0M)
        {
            this.lt_pst_gst.Text = "HST(" + hst_rate.ToString("###") + "%)";
            this.lbl_sale_charge.Text = Config.ConvertPrice(hst) + price_unit;
        }
        else
        {
            if (gst_rate > 0M)
            {
                this.lt_pst_gst.Text = "GST(" + gst_rate.ToString("##") + "%)";
                this.lbl_sale_charge.Text = Config.ConvertPrice(gst) + price_unit;
            }
            if (pst_rate > 0M)
            {
                if (gst_rate > 0M)
                {
                    this.lt_pst_gst.Text += "<br/>PST(" + pst_rate.ToString("##") + "%)";
                    this.lbl_sale_charge.Text += "<br/>" + Config.ConvertPrice(pst) + price_unit;
                }
                else
                {
                    this.lt_pst_gst.Text = "PST(" + pst_rate.ToString("##") + "%)";
                    this.lbl_sale_charge.Text = Config.ConvertPrice(pst) + price_unit;
                }
            }
        }

        if (weee_charge > 0M)
        {
            this.lt_pst_gst.Text += "<br/>WEEE";
            this.lbl_sale_charge.Text += "<br/>" + Config.ConvertPrice(weee_charge) + price_unit;
        }

    }

    public string GetOrderString(int order_code)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("<table style=\"width: 576pt;\" cellspacing=\"0\" border=\"0\">");
        //
        sb.Append("<tr>");
        sb.Append("<td valign=\"top\" style=\"width: 193px\">");
        sb.Append("<div style=\"text-align: left; padding-left: 3em;\">");
        sb.Append(@"<table style=""width: 96%; height: 100%"" align=""right"">
                        <tr>
                            <td style=""font-weight: bold; font-size: 20pt;height:20px; font-family: Tahoma; width: 215px;text-align:left"">LU COMPUTERS</td>
                        </tr>
                        <tr>
                            <td style=""font-weight: bold; font-size: 14pt; font-family: Arial; width: 215px;text-align:left"">www.lucomputers.com</td>
                        </tr>
                       
                        <tr>
                            <td style=""font-size: 11pt; font-family: 'Times New Roman'; width: 215px;text-align:left"">Tel:(866)999-7828 &nbsp; (416)446-7743</td>
                        </tr>
                    </table>
                    </div>
                </td>
                <td style=""width: 29px; height: 126px;"">
                </td>
                <td valign=""top"" style=""padding-top: 5pt"" >
                    <div style=""background:#000000"">
                    <table style=""width: 100%"" cellspacing=""2"" >
                        <tr>
                            <td style=""background: white;font-weight: bold; font-size: 11pt; color: black; vertical-align: middle; text-transform: capitalize; position: static; text-align: center; font-family: 'Times New Roman';"">
                                Customer NO.</td>
                            <td style=""background: white;font-weight: bold; font-size: 11pt; width: 114px; color: black; vertical-align: middle; text-transform: capitalize; position: static; text-align: center; font-family: 'Times New Roman';"">
                                Date</td>
                            <td style=""background: white;font-weight: bold; font-size: 11pt; color: black; vertical-align: middle; text-transform: capitalize; position: static; text-align: center; font-family: 'Times New Roman';"">
                                Invoice No.</td>
                        </tr>
                        <tr>
                            <td style=""background: white;text-align: center;"">");
        sb.Append("889010");        // customer_no
        sb.Append("</td>");
        sb.Append("<td style=\"background: white;text-align: center;\">");
        sb.Append("date");          // date                 
        sb.Append("</td>");
        sb.Append("<td style=\"background: white;text-align: center;\">");
        sb.Append("order_no");      // order_no  
        sb.Append(@"</td>
                        </tr>
                    </table>
                    </div><br />
                    <table style=""width: 100%; text-align: right"">
                        <tr>
                            <td style=""font-weight: bold; font-size: 36pt; font-style: italic; font-family: 'Bookman Old Style'"">
                                INVOICE&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style=""width: 193px; font-weight: bold; color: black;"">&nbsp;
                    </td>
                <td style=""width: 29px"">&nbsp;
                </td>
                <td style=""width: 100px"">&nbsp;
                </td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px; font-weight: bold;"">
                    Sale to:</td>
                <td style=""height: 16px; font-weight: bold;"" colspan=""2"">
                    Ship to:</td>
            </tr> 
            <tr>
                <td>");
        sb.Append("MAX WYNNE");      // customer_name_sale            
        sb.Append(@"</td> 
                <td colspan=""2"">");
        sb.Append("MAX WYNNE");      // customer_name_ship            <asp:Label ID="Label8" runat="server" Text="MAX WYNNE"></asp:Label>
        sb.Append(@"</td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px"">");
        sb.Append("1535 LAUREN ASHLEY DR.");      // address_sale           <asp:Label ID="lbl_address_sale" runat="server" Text="1535 LAUREN ASHLEY DR."></asp:Label>
        sb.Append(@"</td>
                <td style=""height: 16px"" colspan=""2"">");
        sb.Append("1535 LAUREN ASHLEY DR.");      // address_ship             <asp:Label ID="lbl_address_ship" runat="server" Text="1535 LAUREN ASHLEY DR."></asp:Label>
        sb.Append(@"</td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px"">");
        sb.Append("CHESAPEKE VA 23323");      // city_state_zipcode_sale          <asp:Label ID="lbl_city_state_zipcode_sale" runat="server" Text="CHESAPEKE VA 23323"></asp:Label>
        sb.Append(@"</td>
                <td style=""height: 16px"" colspan=""2"">");
        sb.Append("CHESAPEKE VA 23323");      // city_state_zipcode_ship            <asp:Label ID="lbl_city_state_zipcode_ship" runat="server" Text="CHESAPEKE VA 23323"></asp:Label>
        sb.Append(@"</td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px"">");
        sb.Append("Tel: 757 673 0480");      // tel_sale            <asp:Label ID="lbl_tel_sale" runat="server" Text="Tel: 757 673 0480"></asp:Label>
        sb.Append(@"</td>
                <td style=""height: 16px"" colspan=""2"">");
        sb.Append("Tel: 757 673 0480");      // tel_ship              <asp:Label ID="lbl_tel_ship" runat="server" Text="Tel: 757 673 0480"></asp:Label>
        sb.Append(@"</td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px"">&nbsp;
                </td>
                <td style=""width: 29px; height: 16px"">&nbsp;
                </td>
                <td style=""width: 100px; height: 16px"">&nbsp;
                </td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px"">&nbsp;
                </td>
                <td style=""width: 29px; height: 16px"">&nbsp;
                </td>
                <td style=""width: 100px; height: 16px"">&nbsp;
                </td>
            </tr>
            <tr>
                <td style=""width: 193px; height: 16px; border-bottom: 2px solid #000000;"">
                    GST# 855961975RT0001</td>
                <td style=""width: 29px; height: 16px;border-bottom: 2px solid #000000;"">&nbsp;
                </td>
                <td style=""width: 100px; height: 16px;border-bottom: 2px solid #000000;"">&nbsp;
                </td>
            </tr>
            <tr>
                <td style=""width: 193px; border-bottom: 2px solid #000000; height:18pt"">
                    P.O.No: &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp; 
                    <asp:Label ID=""lbl_p_o_no"" runat=""server"" Text=""77889900""></asp:Label>
                    &nbsp; &nbsp; &nbsp;&nbsp; Tax Exemption No: &nbsp;&nbsp;
                    <asp:Label ID=""lbl_tax_exemption_no"" runat=""server"" Text=""88888888""></asp:Label></td>
                <td style=""width: 29px;border-bottom: 2px solid #000000;"">&nbsp;
                </td>
                <td style=""width: 100px; border-bottom: 2px solid #000000;"">
                    Email address: 
                    <asp:Label ID=""lbl_email_address"" runat=""server"" Text=""cdstevenson@telus.net""></asp:Label></td>
            </tr>
            <tr>
                <td style=""height: 350px"" colspan=""3"" valign=""top"">
                    <table style=""width: 100%;border-bottom: 2px solid #000000; "" cellpadding=""0"">
                        <tr>
                            <td style=""font-size: 11pt; width: 50px; font-family: 'Times New Roman'"">
                                Qnt</td>
                            <td style=""font-size: 11pt; width: 80px; font-family: 'Times New Roman'"">
                                Item#</td>
                            <td style=""font-size: 11pt; width: 650px; font-family: 'Times New Roman'"">
                                Description</td>
                            <td style=""width: 100px; text-align: center"">
                                Unit Price</td>
                            <td style=""width: 100px; text-align: center"">
                                Extension</td>
                        </tr>
                   </table>
                   <br />
                   <table style=""width: 96%"">
                       
                        <tr>
                            <td style=""font-size: 11pt; width: 50px; font-family: 'Times New Roman'"">
                                4</td>
                            <td style=""font-size: 11pt; width: 80px; font-family: 'Times New Roman'"">
                            </td>
                            <td style=""font-size: 11pt; width: 650px; font-family: 'Times New Roman'"">
                                Intel P4 Processor 3.4 GHz LGA 775 FSB800 2MB 650</td>
                            <td style=""width: 100px; text-align: right"">
                                660.00</td>
                            <td style=""width: 100px; text-align: right"">
                                2640.00</td>
                        </tr>
                        <tr>
                            <td>
                                4</td>
                            <td>
                            </td>
                            <td style=""font-size: 11pt; font-family: 'Times New Roman'"">
                                Remove Antec Lifestyle Series Aria Alum.Small Form Factor w/300 W PS</td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan=""3"" valign=""top"">
                    <table style=""width: 96%"">
                        <tr>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""text-align: right"">
                                Sub-total</td>
                            <td style=""width: 100px; text-align: right"">
                                <asp:Label ID=""lbl_sub_total"" runat=""server"" Text=""2756.00""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""text-align: right"">
                                Shipping &amp; Handling</td>
                            <td style=""width: 100px; text-align: right"">
                                <asp:Label ID=""lbl_shipping_handling"" runat=""server"" Text=""200.00""></asp:Label></td>
                        </tr>
                        <tr>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""text-align: right"">
                                PST</td>
                            <td style=""width: 100px; text-align: right"">
                                177.36</td>
                        </tr>
                        <tr>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""text-align: right"">
                                GST</td>
                            <td style=""width: 100px; text-align: right"">
                                3133.36</td>
                        </tr>
                        <tr>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""text-align: right"">
                                Total</td>
                            <td style=""width: 100px; text-align: right"">
                                62.67</td>
                        </tr>
                        <tr>
                            <td style=""width: 60%"">
                                Customer Signature &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; Date:</td>
                            <td style=""width: 100px"">
                            </td>
                            <td style=""width: 100px"">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan=""3"" valign=""top"">
                    &nbsp;<p style=""margin-top: 0pt; text-justify: inter-ideograph; margin-bottom: 0pt;
                        margin-left: 0in; direction: ltr; text-indent: 0in; unicode-bidi: embed; text-align: justify;
                        language: zh-CN"">
                        <span style=""font-weight: normal; font-size: 9pt; color: black; font-style: normal;
                            font-family: 'Times New Roman'; language: en-US; mso-ascii-font-family: 'MS Sans Serif';
                            mso-fareast-font-family: 宋体; mso-bidi-font-family: +mn-cs"">All sales are subject
                            to LU Computers' terms and policies. No credit for any item that can be replaced.
                            Any returned product must be complete and unused.<span style=""mso-spacerun: yes"">&nbsp;
                            </span>All returns must be in their original packing material, and be in re-saleable
                            condition. Credit will not be issued unless the above conditions are met.<span style=""mso-spacerun: yes"">&nbsp;
                            </span>All returns are subject to a 15% restocking charge. Notebooks, software and
                            consumable items cannot be returned for credit. Returned check subject to $20 charge.<span
                                style=""mso-spacerun: yes"">&nbsp; </span>Late payment shall result in interest
                            charge of two percent for any calendar month or part thereof for which payment or
                            partial payment remains due.<span style=""mso-spacerun: yes"">&nbsp; </span>All responsible
                            costs and expenses suffered by LU in collecting monies due including but not limited
                            to attorney's fees and collection agency fees shall be paid by the purchaser.<span
                                style=""mso-spacerun: yes"">&nbsp; </span>Warranty claimed items must be shipped/carried
                            in at customer's cost.<span style=""mso-spacerun: yes"">&nbsp; </span>Returned shipment
                            without a LU issued<span style=""mso-spacerun: yes"">&nbsp; </span>RMA (Return Merchandise
                            Authorization) number will be rejected. Warranty does not cover services completed
                            by an unauthorized third party.<span style=""mso-spacerun: yes"">&nbsp; </span></span>
                    </p>
                </td>
            </tr>
        </table>");
        return sb.ToString();

    }
    #endregion

    protected void btn_gene_pdf_Click(object sender, EventArgs e)
    {
        AnthemHelper.Redirect("download_order_pdf.aspx?order_code=" + this.OrderCodeRequest.ToString() + "&customer_state=" + this.CustomerState.ToString());

    }

    /// <summary>
    /// 取得产品列表
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public DataTable FintProductList(string order_code)
    {
        if (order_code.Length != 6)
            return null;
        DataTable dt = new DataTable();
        var context = new LU.Data.nicklu2Entities();
        dt.Columns.Add("qnt");
        dt.Columns.Add("sku");
        dt.Columns.Add("name");
        dt.Columns.Add("unit_price");
        dt.Columns.Add("extension");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        var opms = OrderProductModel.GetModelsByOrderCode(context, order_code);
        //sb.Append(@"<table style=""width: 96%"">");
        for (int i = 0; i < opms.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = opms[i].order_product_sum.Value.ToString();
            dr[1] = opms[i].product_serial_no.Value.ToString();
            dr[2] = opms[i].product_name;
            dr[3] = Config.ConvertPrice2(opms[i].order_product_sold.Value);
            dr[4] = Config.ConvertPrice2(opms[i].order_product_sold.Value * opms[i].order_product_sum.Value);

            dt.Rows.Add(dr);

            if (opms[i].product_serial_no.ToString().Trim().Length == 8)
            {
                DataTable ms = SpTmpDetailModel.GetModelsBySysCode(opms[i].product_serial_no.ToString());
                for (int j = 0; j < ms.Rows.Count; j++)
                {
                    DataRow sdr = ms.Rows[j];
                    DataRow dr2 = dt.NewRow();
                    dr2[0] = "";
                    dr2[1] = "";
                    dr2[2] = "" + sdr["product_name"].ToString();
                    dr2[3] = "";
                    dr2[4] = "";// "x" + sdr["part_quantity"].ToString();

                    dt.Rows.Add(dr2);
                }
            }

        }

        return dt;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    private string GetProductList(string order_code)
    {
        var context = new LU.Data.nicklu2Entities();

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        var opms = OrderProductModel.GetModelsByOrderCode(context, order_code);
        sb.Append(@"<table style=""width: 96%"">");
        for (int i = 0; i < opms.Length; i++)
        {
            sb.Append("<tr><td style=\"font-size: 8pt; width: 50px; font-family: 'Times New Roman'\">");
            sb.Append(opms[i].order_product_sum.ToString());
            sb.Append("</td><td style=\"font-size: 8pt; width: 80px; font-family: 'Times New Roman'\">");
            sb.Append(opms[i].product_serial_no.ToString());
            sb.Append("</td><td style=\"font-size: 8pt; width: 650px; font-family: 'Times New Roman'\">");
            sb.Append(opms[i].product_name);
            sb.Append("</td><td style=\"width: 100px;font-size: 8pt; text-align: right\">");
            sb.Append(Config.ConvertPrice(opms[i].order_product_sold.Value));
            sb.Append("</td><td style=\"width: 100px; font-size: 8pt;text-align: right\">");
            sb.Append(Config.ConvertPrice(opms[i].order_product_sold.Value * opms[i].order_product_sum.Value));
            sb.Append("</td></tr>");

            if (opms[i].product_serial_no.ToString().Trim().Length == 8)
            {
                DataTable ms = OrderProductSysDetailModel.GetModelsBySysCode(opms[i].product_serial_no.ToString());
                for (int j = 0; j < ms.Rows.Count; j++)
                {
                    DataRow dr = ms.Rows[j];
                    if (dr["product_name"].ToString().ToLower().IndexOf("onboard") == -1

                        && dr["product_name"].ToString().ToLower().IndexOf("none selected") == -1)
                    {
                        sb.Append("<tr><td style=\"font-size: 8pt; width: 50px; font-family: 'Times New Roman'\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td><td style=\"font-size: 8pt; width: 70px; text-align:left; font-family: 'Times New Roman';\">");
                        sb.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;(" + dr["product_serial_no"].ToString() + ")&nbsp;&nbsp;" + (dr["part_quantity"].ToString() == "1" ? "" : dr["part_quantity"].ToString() + "x"));
                        sb.Append("</td><td style=\"font-size: 8pt; width: 650px; font-family: 'Times New Roman'\">");
                        sb.Append(dr["product_name"].ToString());
                        sb.Append("</td><td style=\"width: 100px;font-size: 8pt; text-align: right\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td><td style=\"width: 100px;font-size: 8pt; text-align: right\">");
                        sb.Append("&nbsp;");
                        sb.Append("</td></tr>");
                    }
                }
            }
        }
        sb.Append("</table>");

        return sb.ToString();
    }
    /// <summary>
    /// 
    /// </summary>
    public int CustomerState
    {
        get
        {
            object o = ViewState["CustomerState"];
            if (o != null)
                return int.Parse(o.ToString());
            return -1;
        }
        set { ViewState["CustomerState"] = value; }
    }

}
