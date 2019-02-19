<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sale_print_order.aspx.cs" Inherits="Q_Admin_sale_print_order" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Print Order</title>
    <style type="text/css" media="print">
       .noPrint
        {
            display: none;
        }
        }</style>
</head>
<body>
    <form id="form1" runat="server">
    <OBJECT  id="WebBrowser"  classid="CLSID:8856F961-340A-11D0-A96B-00C04FD705A2"  height="0"  width="0">
  </OBJECT>
    <div id="divContent" runat="server">
        <div class="noPrint">
        <hr size="1" />
        <div style="text-align:center"><!--document.all.WebBrowser.ExecWB(7,1);-->
        <input type="button" value="Print" onclick="window.print();"/>&nbsp;
        <anthem:Button runat="server" ID="btn_gene_pdf" Text="Download PDF" OnClick="btn_gene_pdf_Click" />&nbsp;
        <input type="button" value="Close Window" onclick="window.close();" />
        </div> 
        <hr size="1" /></div>
        <asp:Literal runat="server" ID="liteInvoice"></asp:Literal>
        <asp:Panel runat="server" ID="panelC" >
        <table style="width: 8in;" cellspacing="0" >
            <tr>
                <td valign="top" >
                    <div style="text-align: left">
                        <table style="width: 94%; height: 100%" align="right" border="0">
                            <tr>
                                <td style="font-weight: bold; font-size: 20pt; font-family: Tahoma; ">LU COMPUTERS</td>
                            </tr>
                            <tr>
                                <td style="font-weight: bold; font-size: 14pt; font-family: Arial; ">www.lucomputers.com</td>
                            </tr>
                           
                            <tr>
                                <td style="font-size: 11pt; font-family: 'Times New Roman';">Tel:(866)999-7828 &nbsp; (416)446-7743</td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td style="width: 29px; height: 126px;">
                </td>
                <td valign="top" style="padding-top: 5pt" >
                    <div style="background:#000000">                 
                    <table style="width: 100%" cellspacing="2">
                        <tr>
                            <td style="background: white;font-weight: bold; font-size: 11pt; color: black; vertical-align: middle; text-transform: capitalize; position: static; text-align: center; font-family: 'Times New Roman';">
                                Customer NO.</td>
                            <td style="background: white;font-weight: bold; font-size: 11pt; width: 114px; color: black; vertical-align: middle; text-transform: capitalize; position: static; text-align: center; font-family: 'Times New Roman';">
                                Date</td>
                            <td style="background: white;font-weight: bold; font-size: 11pt; color: black; vertical-align: middle; text-transform: capitalize; position: static; text-align: center; font-family: 'Times New Roman';">
                                <asp:Literal runat="server" ID="literal_invoice_title" Text="Invoice No."></asp:Literal></td>
                        </tr>
                        <tr>
                            <td style="background: white;text-align: center;">
                               
                                <asp:Label ID="lbl_customer_no" runat="server" Text="0"></asp:Label></td>
                            <td style="background: white;text-align: center;">
                                <asp:Label ID="lbl_date" runat="server" Text="Label"></asp:Label></td>
                            <td style="background: white;text-align: center;">
                                <asp:Label ID="lbl_order_no" runat="server" Text="Label"></asp:Label></td>
                        </tr>
                    </table>
                    </div><br />
                    <table style="width: 100%; text-align: right">
                        <tr>
                            <td style="font-weight: bold; font-size: 32pt; font-style: italic; font-family: 'Bookman Old Style'">
                                <asp:Literal runat="server" ID="literal_invoice_title2" Text="INVOICE"></asp:Literal>&nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="width: 193px; font-weight: bold; color: black;"><br /><br />
                    </td>
                <td style="width: 29px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="padding-left: 2em;">
                    Sale to:</td>
                <td style="height: 16px; font-weight: bold;" colspan="2">
                    Ship to:</td>
            </tr>
            <tr>
                <td style="padding-left: 2em">
                    <asp:Label ID="lbl_customer_company" runat="server"></asp:Label>
                    <asp:Label ID="lbl_customer_name_sale" runat="server" Text="0"></asp:Label></td>
                <td colspan="2">
                    <asp:Label ID="lbl_customer_company0" runat="server"></asp:Label>
                    <asp:Label ID="lbl_customer_name_ship" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="padding-left: 2em;">
                    <asp:Label ID="lbl_address_sale" runat="server"></asp:Label></td>
                <td style="height: 16px" colspan="2">
                    <asp:Label ID="lbl_address_ship" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="padding-left: 2em;">
                    <asp:Label ID="lbl_city_state_zipcode_sale" runat="server"></asp:Label></td>
                <td style="height: 16px" colspan="2">
                    <asp:Label ID="lbl_city_state_zipcode_ship" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="padding-left: 2em;">
                    <asp:Label ID="lbl_tel_sale" runat="server"></asp:Label></td>
                <td style="height: 16px" colspan="2">
                    <asp:Label ID="lbl_tel_ship" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="padding-left: 2em;">
                </td>
                <td style="width: 29px; height: 16px">
                </td>
                <td style="width: 100px; height: 16px">
                </td>
            </tr>
            <tr>
                <td style="width: 193px; height: 16px">
                </td>
                <td style="width: 29px; height: 16px">
                </td>
                <td style="width: 100px; height: 16px">
                </td>
            </tr>
            <tr>
                <td style="width: 193px; height: 16px; border-bottom: 2px solid #000000;">
                    GST# 855961975RT0001</td>
                <td style="width: 29px; height: 16px;border-bottom: 2px solid #000000;">
                    &nbsp;</td>
                <td style="width: 100px; height: 16px;border-bottom: 2px solid #000000;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style=" border-bottom: 2px solid #000000; height:18pt" colspan="2">P.O.No: &nbsp;&nbsp; 
                    <asp:Label ID="lbl_p_o_no" runat="server"></asp:Label>
                    &nbsp; &nbsp; Tax Exemption No: &nbsp;&nbsp;
                    <asp:Label ID="lbl_tax_exemption_no" runat="server" Text="88888888"></asp:Label></td>
               
                <td style=" border-bottom: 2px solid #000000;">
                    Email address: 
                    <asp:Label ID="lbl_email_address" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 350px" colspan="3" valign="top">
                    <table style="width: 100%;border-bottom: 2px solid #000000; " cellpadding="0">
                        <tr>
                            <td style="font-size: 11pt; width: 50px; font-family: 'Times New Roman'">
                                Qnt</td>
                            <td style="font-size: 11pt; width: 80px; font-family: 'Times New Roman'">
                                Item#</td>
                            <td style="font-size: 11pt; width: 650px; font-family: 'Times New Roman'">
                                Description</td>
                            <td style="width: 100px; text-align: center">
                                Unit Price</td>
                            <td style="width: 100px; text-align: center">
                                Extension</td>
                        </tr>
                   </table>
                   <br />
                    <asp:Literal runat="server" ID="liteProductList"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    <table style="width: 96%">
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="text-align: right">
                                Sub-total</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_sub_total" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <asp:Literal runat="server" ID="literal_splecial_cash_discount"></asp:Literal>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="text-align: right">
                                Shipping &amp; Handling</td>
                            <td style="width: 150px; text-align: right">
                                <asp:Label ID="lbl_shipping_handling" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="text-align: right">
                                <asp:Literal ID="lt_pst_gst" runat="server"></asp:Literal></td>
                             <td style=" text-align: right">
                                <asp:Label ID="lbl_sale_charge" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 100px">
                            </td>
                            <td style="text-align: right">
                                Total<asp:Literal ID="lt_price_unit" runat="server" Text="()"></asp:Literal>
                            </td>
                            <td style="width: 150px; text-align: right">
                                 <asp:Label ID="lbl_total" runat="server" Text="0"></asp:Label></td>
                        </tr>
                        <tr>
                            <td style="width: 60%">
                                Customer Signature <span style="text-decoration: underline">&nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;</span>&nbsp; Date: <span style="text-decoration: underline">&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; </span>
                            </td>
                            <td style="width: 100px">
                            </td>
                            <td style="width: 100px">
                            </td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3" valign="top">
                    All sales are subject
                            to LU Computers' terms and policies. No credit for any item that can be replaced.
                            Any returned product must be complete and unused.&nbsp;
                            All returns must be in their original packing material, and be in re-saleable
                            condition. Credit will not be issued unless the above conditions are met.&nbsp;
                            All returns are subject to a 15% restocking charge. Notebooks, software and
                            consumable items cannot be returned for credit. Returned check subject to $20 charge.&nbsp; Late payment shall result in interest
                            charge of two percent for any calendar month or part thereof for which payment or
                            partial payment remains due.&nbsp; All responsible
                            costs and expenses suffered by LU in collecting monies due including but not limited
                            to attorney's fees and collection agency fees shall be paid by the purchaser.&nbsp;Warranty claimed items must be shipped/carried
                            in at customer's cost.&nbsp; Returned shipment
                            without a LU issued&nbsp;RMA (Return Merchandise
                            Authorization) number will be rejected. Warranty does not cover services completed
                            by an unauthorized third party.
                </td>
            </tr>
        </table>
    </asp:Panel>
    </div>
    </form>
</body>
</html>
