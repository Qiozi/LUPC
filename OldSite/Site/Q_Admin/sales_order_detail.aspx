<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sales_order_detail.aspx.cs" Inherits="Q_Admin_sales_order_detail" %>

<%@ Register Src="UC/CustomerMsg.ascx" TagName="CustomerMsg" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Order Detail</title>
    <link href="js_css/admin.css" rel="stylesheet" />
    <script type="text/javascript" src="JS/WinOpen.js"></script>
    <style type="text/css" media="print">
        .noPrint {
            display: none;
        }

        ul li {
            list-style: none;
        }

        .ispan {
            font-size: 8.5pt;
        }

        td i {
            font-size: 8.5pt;
        }

        .style1 {
            width: 33%;
        }

        pre {
            margin: 0px;
        }

        td span div {
            font-family: Tahoma;
        }

        #order_charge_area {
            width: 100%;
        }

            #order_charge_area  td {
                line-height: 18px;
                font-size: 7.5pt;
                color: #000;
                border-bottom: 1px dotted #cccccc;
                text-align: right;
                background:red;
            }

         .price_unit {
                font-size: 7pt;
                color: Blue;
            }

        .style2 {
            height: 17px;
        }
    </style>
    <script src="../js_css/jquery-1.9.1.js"></script>
    <script>
        function toNewWin(key) {
            window.location.href = '/q_admin/sales_order_detail.aspx?order_code=' + key;
            
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="padding: 1em; border: 1px solid #ccc; background: #f2f2f2" class="noPrint">
            Order#<input type="text" name="key" id="keyword" value="<%=ReqOrderCode%>" /><input type="button" value="Go" id="go" onclick="toNewWin($(this).prev().val());" />
        </div>

        <div id="showarea">  
            <div>
                <div style="text-align: right" class="noPrint">
                    <asp:DropDownList runat="server" ID="ddl_order_status" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_order_status_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:CheckBox runat="server" ID="cb_show_history" Text="Show History"
                        AutoPostBack="true" Checked="False"
                        OnCheckedChanged="cb_show_history_CheckedChanged" />
                    <asp:CheckBox runat="server" ID="cb_show_price_print" Text="Show Price"
                        AutoPostBack="true" Checked="False"
                        OnCheckedChanged="cb_show_price_print_CheckedChanged" />
                    <asp:CheckBox runat="server" ID="cb_show_customer_msg" Text="Show Message"
                        AutoPostBack="true" Checked="true"
                        OnCheckedChanged="cb_show_customer_msg_CheckedChanged" />
                    <input type="button" value="Print" onclick="window.print();" />
                    <input type="button" value="Close Window" onclick="window.close();" />

                </div>

                <div id="email_area">
                    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table width="80%" height="30" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="text_hui_11">&nbsp;</td>
                        </tr>
                        <tr>
                            <td><span style="font-family: Tahoma; letter-spacing: 0px; text-decoration: none; line-height: 18px">
                                <strong><font style="font-size: 16pt">LU COMPUTERS ORDER 
					FORM</font></strong></span></td>
                        </tr>
                    </table>
                    <table width="80%" border="0" align="center" cellpadding="2" cellspacing="0">
                        <tr>
                            <td width="50%" class="text_hui_11" rowspan="2">
                               
                                    Tel: (866)999-7828 (416)446-7743</span>
                            </td>
                            <td align="right" class="style1">
                                <strong>
                                    <span style="font-family: Tahoma; letter-spacing: 0px">
                                        <asp:Label runat="server" ID="lbl_invoice_number_title" Font-Bold="True" Text="Invoice Number:"></asp:Label></span></strong></span></td>
                            <td style="width: 15%" align="right">

                                <span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px">

                                    <asp:Label ID="lbl_invoice_number" runat="server"></asp:Label>
                                </span></td>
                        </tr>
                        <tr>
                            <td align="right" class="style1">
                                <span style="font-family: Tahoma; letter-spacing: 0px">
                                    <strong><span style="font-size: 9pt">Date:</span></strong></span></td>
                            <td style="width: 35%" align="right" nowrap="nowrap">
                                <span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px">
                                    <asp:Label ID="lbl_order_date" runat="server"></asp:Label>
                                </span></td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="2">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                    <tr>
                                        <td width="50%" style="padding-left: 35px">
                                            <span style="font-family: Tahoma; letter-spacing: 0px">
                                                <strong><span style="font-size: 9pt">Customer 
							Number:</span></strong></span></td>
                                        <td width="50%"><span style="font-family: Tahoma; letter-spacing: 0px">
                                            <span style="font-size: 9pt" id="Span1">
                                                <asp:Label ID="lbl_customer_number" runat="server"></asp:Label>
                                            </span></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 35px">

                                            <span style="font-family: Tahoma; letter-spacing: 0px">
                                                <strong><span style="font-size: 9pt">Order Number:</span></strong></span></td>
                                        <td><span style="font-family: Tahoma; letter-spacing: 0px">
                                            <span style="font-size: 9pt" id="Span2">
                                                <asp:Literal ID="lbl_order_number" runat="server"></asp:Literal>
                                            </span></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 35px">
                                            <span style="font-family: Tahoma; letter-spacing: 0px">
                                                <strong><span style="font-size: 9pt">Shipping 
							Method:</span></strong></span></td>
                                        <td>
                                            <span style="font-family: Tahoma; letter-spacing: 0px">
                                                <asp:Label ID="lbl_shipping_company" runat="server"></asp:Label>

                                            </span></td>
                                    </tr>
                                    <tr>
                                        <td style="padding-left: 35px">
                                            <span style="font-family: Tahoma; letter-spacing: 0px">
                                                <strong><span style="font-size: 9pt">Customer Name:</span></strong></span></td>
                            </td>
                            <td>
                                <span style="font-family: Tahoma; letter-spacing: 0px">
                                    <asp:Label ID="lbl_customer_name" runat="server"></asp:Label>

                                </span></td>
                        </tr>
                    </table>
                    </td>
                    <td colspan="2" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                            <tr>
                                <td class="style2"><strong>eBay User Id:</strong></td>
                                <td class="style2"><span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px">
                                    <asp:Label ID="lbl_ebayUserId" runat="server"></asp:Label>
                                </span></td>

                            </tr>
                            <tr>
                                <td><span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px"><strong>Payment: </strong></span></td>
                                <td><span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px">
                                    <asp:Label ID="lbl_payment" runat="server"></asp:Label>
                                </span></td>

                            </tr>
                            <tr>
                                <td colspan="2"><span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px"><strong></strong>
                                    <asp:Label ID="lbl_card_info" runat="server"></asp:Label>
                                </span></td>
                            </tr>
                        </table>
                    </td>
                    </tr>
                </table>
                <table width="98%" style="border-bottom: #327AB8 1px solid;" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                    <table width="80%" border="0" align="center" cellpadding="2" cellspacing="0">
                        <tr>
                            <td width="50%">&nbsp;</td>
                            <td width="50%">&nbsp;</td>
                        </tr>
                        <tr>
                            <td><span style="font-family: Tahoma; letter-spacing: 0px">
                                <strong><span style="font-size: 9pt">Billing Address:</span> </strong></span></td>
                            <td><span style="font-family: Tahoma; letter-spacing: 0px">
                                <strong><span style="font-size: 9pt">Shipping Address:</span></strong></span></td>
                        </tr>
                        <tr>
                            <td valign="top"><span style="font-family: Tahoma; letter-spacing: 0px">
                                <asp:Label ID="lbl_billing_address" runat="server" ForeColor="Black"></asp:Label>
                                <br />
                                <br>
                                <asp:Label ID="lbl_billing_phone" runat="server" Visible="False"
                                    ForeColor="Black"></asp:Label>
                            </span></td>
                            <td><span style="font-family: Tahoma; letter-spacing: 0px">
                                <asp:Label ID="lbl_shipping_address" runat="server" ForeColor="Black"></asp:Label>
                                <br />
                                <br />

                                <asp:Label ID="lbl_shipping_service" runat="server" ForeColor="Black"></asp:Label>
                                <br>

                                <asp:Label ID="lbl_shipping_phone" runat="server" Visible="False"
                                    ForeColor="Black"></asp:Label>
                            </span></td>
                        </tr>


                        <tr>
                            <td colspan="2">
                            &nbsp;
                                </td>
                        </tr>


                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_prick_time" runat="server"></asp:Label></td>
                        </tr>


                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_email" runat="server"></asp:Label></td>
                        </tr>


                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lbl_phone" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <table width="98%" style="border-bottom: #327AB8 1px solid;" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <table width="98%" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="height: 46px">
                                <table width="100%" height="32" border="0" cellpadding="2" cellspacing="1">
                                    <tr align="center" bgcolor="#FFFFFF">
                                        <td><span style="font-family: Tahoma; letter-spacing: 0px; font-size: 9pt"><strong>Description</strong></span></td>
                                        <td width="9%"><span style="font-family: Tahoma; letter-spacing: 0px; font-size: 9pt"><strong>QTY </strong></span></td>
                                        <td width="11%"><span style="font-family: Tahoma; letter-spacing: 0px; font-size: 9pt"><strong>Unit Price </strong></span></td>
                                        <td width="9%"><span style="font-family: Tahoma; letter-spacing: 0px; font-size: 9pt"><strong>Total </strong></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <!--order begin-->
                                <asp:Label runat="server" ID="lbl_order_list"></asp:Label>

                                <!--order end-->
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table width="98%" border="0" align="center" cellpadding="2" cellspacing="0" bgcolor="#eeeeee">
                        <tr bgcolor="#FFFFFF">
                            <td width="60%" valign="top" style="margin-top: 10px">
                                <uc1:CustomerMsg ID="CustomerMsg1" runat="server" />
                            </td>
                            <td valign="top">
                                <table border="0" cellpadding="0" width="100%" cellspacing="0" id="order_charge_area">
                                    <tr>
                                        <td style="text-align:right;"><b>SUBTOTAL</b></td>
                                        <td style="text-align:right;">
                                            <asp:Literal ID="lbl_sub_total" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <asp:Panel runat="server" ID="panel_special_cash_discount">
                                        <tr>
                                            <td style="text-align:right;"><b>DISCOUNT</b></td>
                                            <td style="color: Blue;text-align:right">-<asp:Literal ID="lbl_special_cash_discount" runat="server"></asp:Literal></td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td style="text-align:right;"><b>SHIPPING AND HANDLING</b></td>
                                        <td style="text-align:right;">
                                            <asp:Literal ID="lbl_shipping_and_handling" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;"><b>TAXABLE TOTAL</b></td>
                                        <td style="text-align:right;">
                                            <asp:Literal ID="lbl_taxable_total" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">
                                            <asp:Literal ID="lbl_tax_rate" runat="server"></asp:Literal>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">
                                            <b>WEEE</b>
                                        </td>
                                        <td style="text-align:right;">
                                            <asp:Literal ID="lbl_weee_charge" runat="server"></asp:Literal></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right;">
                                            <b>GRAND TOTAL</b>
                                        </td>
                                        <td style="text-align:right;">
                                            <asp:Literal ID="lbl_grand_total" runat="server"></asp:Literal></td>
                                    </tr>
                                </table>
                            </td>

                        </tr>
                    </table>

                    <table width="98%" style="border-bottom: #327AB8 0px solid;" border="0" align="center" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </div>
            </div>
            <p>
                <span style="font: 11px/16Px; font-family: tahoma; color: #000000; letter-spacing: 0px">
                    <asp:Label ID="lbl_is_old_order" runat="server"
                        ForeColor="#CC3300"></asp:Label>
                </span>
                <div style="text-align: right" class="noPrint">
                    <input type="button" value="Close Window" onclick="window.close();" />

                </div>
            </p>
            <asp:GridView ID="gv_order_product_history" runat="server"
                AutoGenerateColumns="False"
                OnRowDataBound="gv_order_product_history_RowDataBound" Width="100%">
                <Columns>
                    <asp:BoundField HeaderText="SKU" DataField="product_serial_no" />
                    <asp:BoundField HeaderText="name" DataField="product_name" />
                    <asp:BoundField HeaderText="unit price$" DataField="order_product_sold">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="order_product_sum" HeaderText="Sum" />
                    <asp:BoundField HeaderText="Cmd" DataField="add_del">
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Datetime" DataField="create_datetime" />
                </Columns>
            </asp:GridView>

            <asp:DataList
                ID="dl_msg_list" runat="server" Width="100%" BackColor="White"
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px" CellPadding="4"
                ForeColor="Black" GridLines="Vertical">
                <FooterStyle BackColor="#CCCC99" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#F7F7DE" />
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <ItemTemplate>
                    <div style="color: #000000;">
                        <div style="float: left; color: #000">
                            <%# DataBinder.Eval(Container.DataItem,"Author").ToString() %>
                        </div>
                        <div style="float: right; color: #000">
                            <%# ViewDateFormat.View(DateTime.Parse(DataBinder.Eval(Container.DataItem, "regdate").ToString()))%>
                        </div>
                        <div style="min-height: 40px; color: #000000; clear: both; vertical-align: middle; padding-left: 2em; margin-top: 5px;">
                            <%# System.Web.HttpUtility.HtmlDecode( DataBinder.Eval(Container.DataItem,"msg").ToString().Replace("\r\n", "<br/>")) %>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:DataList>
        </div>
        <asp:Literal runat="server" ID="ltNote"></asp:Literal>
    </form>


</body>
</html>
