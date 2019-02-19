<%@ Page Language="C#" AutoEventWireup="true" CodeFile="email_simple_invoice.aspx.cs" Inherits="Q_Admin_email_simple_invoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LU Computers</title>
    <script src="JS/Alert.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: center; background: #f2f2f2; border-bottom: 1px solid #cccccc">
            <table>
                <tr>
                    <td style="text-align: right;">TO:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox1" runat="server" Columns="50"></asp:TextBox></td>
                    <td style="text-align: left;"></td>
                </tr>
                <tr>
                    <td style="text-align: right;">Subject:</td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="TextBox_email_subject" runat="server"
                            Columns="100"></asp:TextBox></td>
                    <td style="text-align: left;">
                        <asp:Button ID="btn_accessories" Visible="false" runat="server" OnClick="btn_accessories_Click"
                            Text="Attach Invoice(PDF)" Width="150px" /><br />
                        <asp:Button ID="btn_attach_order" runat="server" Visible="false"
                            Text="Attach Order(PDF)" OnClick="btn_attach_order_Click" Width="150px" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <center>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="3">
                                <asp:ListItem Text="None" Value="0" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="Attach Invoice(PDF)" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Attach Order form(PDF)" Value="2"></asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:Button ID="Button1" runat="server"
                                OnClick="Button1_Click" Text="Send" Width="150px" />
                            <br />
                            <asp:Button ID="btn_view_invoice" runat="server"
                                Text="View Invoice Pdf" Width="150px" OnClick="btn_view_invoice_Click" />
                            <asp:Button ID="btn_view_order_form" runat="server"
                                Text="View Order form Pdf" Width="150px"
                                OnClick="btn_view_order_form_Click" />
                        </center>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:TextBox runat="server" ID="txtNote" TextMode="MultiLine" Rows ="5" Columns="100"></asp:TextBox>
                        <i>email body</i>
                    </td>
                </tr>
            </table>

        </div>

        <asp:Label ID="lb_content" runat="server"></asp:Label><div style="display: none">
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">LU COMPUTERS: INVOICE (#229883) &nbsp;&nbsp;&nbsp;THANK YOU FOR YOUR BUSINESS!&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">LU COMPUTERS &nbsp; &nbsp;&nbsp; </span></font>
            </p>            
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Tel: (416)446-7743&nbsp; Toll Free: (866)999-7828 &nbsp;&nbsp;
                </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Email: sales@lucomputers.com&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'"></span></font>&nbsp;
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; INVOICE No.:&nbsp; 229883</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; CUSTOMER No.:&nbsp; 888507</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; 25/10/2007</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; SALE TO:</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; DOUG / BRIAN SIM</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; 5737 56TH STREET</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; ROCKY MOUTAIN HOUSE</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; ALBERTA T4T 1J8</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; SHIP TO:</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; (Same as Above)</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; TELEPHONE:&nbsp; 403 845 4086</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; EMAIL ADDRESS:&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">________________________________ &nbsp;&nbsp; </span>
                </font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Canadian Dollars&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; $669.99&nbsp; &nbsp; &nbsp;&nbsp; Sub-total</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; $40.00&nbsp; &nbsp; &nbsp; &nbsp; Shipping
                &amp; Handling</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp;</span></font><font color="navy" face="Courier New"
                    size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'"> &nbsp;
                        $42.60&nbsp; &nbsp; &nbsp; &nbsp; GST</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; $752.59&nbsp; &nbsp; &nbsp;&nbsp; Total</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; $752.59&nbsp; &nbsp; &nbsp;&nbsp; Grand
                Total in CAD</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">________________________________ &nbsp;&nbsp; </span>
                </font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">$489.99 x 1 &nbsp;&nbsp; 260171817189 &nbsp;&nbsp; Intel
                Core 2 Duo Conroe E6550 2.33GHz FSB 1333, 4MB CPU &nbsp;&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; Intel Original Socket 775 CPU Fan </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; ASUS P5KPL-VM Motherboard &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; 2GB DDR II 667 Memory 240 Pin (Kingston) &nbsp;
                &nbsp;&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; Western Digital 160GB 7200RPM 8MB Cache Serial
                ATA II &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; LG 20X DVD RW + Dual Layer SATA w/ Lightscribe
                Technology </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; nVidia GeForce 7200GS 512MB Turbo Cache PCI-E
                DVI TV-Out 62609&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; Realtek 6 Channel High-Definition Audio </span>
                </font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; 10/100/1000 Gigabit Lan Network Card &nbsp;&nbsp;
                </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; 6 USB 2.0 Ports, 1 Parallel, 1 COM &nbsp; &nbsp;&nbsp;
                </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; Deluxe 8197B Case &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; 550 Watt Optimax Dual Fan Power Supply&nbsp;
                </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; 3 Years Labor, 1 Year Parts Warranty &amp; Life
                Time Toll Free Support &nbsp;&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">$-33 x 1 &nbsp; &nbsp; &nbsp;&nbsp; Remove nVidia GeForce
                7200GS 512MB Turbo Cache PCI-E DVI TV-Out 62609 &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">$145 x 1 &nbsp; &nbsp; &nbsp;&nbsp; nVidia XFX 8600GT
                512MB DDR2 PCI-E Dual DVI HDTV&nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">$68 x 1 &nbsp; &nbsp; &nbsp;&nbsp; Western Digital 250GB
                7200RPM 16MB Cache Serial ATA II &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Thank you,</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Benson </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Lu Computers</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">1866.999.7828</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Monday – Friday 10.00 – 7.30 EST</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'">Saturday 11.00 – 4.30 EST</span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="#999999" face="Courier New" size="2"><span style="font-size: 10pt; color: #999999; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="#999999" face="Courier New" size="2"><span style="font-size: 10pt; color: #999999; font-family: 'Courier New'">______________________________________________________&nbsp;
                &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <font color="#999999" face="Courier New" size="2"><span style="font-size: 10pt; color: #999999; font-family: 'Courier New'">&nbsp; &nbsp; &nbsp; </span></font>
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699">Sales are subject to terms and LU Computers’ policies.&nbsp; </span>
                </font></span>
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699"></span></font></span>&nbsp;
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699">No credit for any item that can be replaced. Any returned product
                must be complete and unused.&nbsp; All returns must be in their original packing
                material, and be in re-saleable condition. Credit will not be issued unless the
                above conditions are met.&nbsp; All returns are subject to a 15% restocking charge.
                Software and consumable items cannot be returned for credit or replacement. </span>
                </font></span>
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699"></span></font></span>&nbsp;
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699">Returned check subject to $20 charge. Late payment shall result
                in interest charge of two percent for any calendar month or part thereof for which
                payment or partial payment remains due.&nbsp; All responsible costs and expenses
                suffered by LU in collecting monies due including but not limited to attorney's
                fees and collection agency fees shall be paid by the purchaser.&nbsp; </span></font>
                </span>
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699"></span></font></span>&nbsp;
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699">Warranty claimed items must be shipped /carried in at customer's
                cost.&nbsp; Returned shipment without a LU issued RMA (Return Merchandise Authorization)
                number will be rejected. Warranty does not cover services completed by an unauthorized
                third party. &nbsp; </span></font></span>
            </p>
            <p class="EC_MsoNormal">
                <font face="Times New Roman" size="1"><span style="font-size: 8pt"></span></font>
                &nbsp;
            </p>
            <p class="EC_MsoNormal">
                <span class="EC_fontfoot1"><font color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699">Copyright © 2006 LU Computers. All Rights Reserved.</span></font></span><font
                    color="#666699" face="Arial" size="1"><span style="font-size: 7.5pt; color: #666699; font-family: Arial"><br />
                        <span class="EC_fontfoot1"><font color="#666699" face="Arial"><span style="color: #666699">Designated trademarks and brands are the property of their respective owners.</span></font></span></span></font><font
                            color="navy" face="Courier New" size="2"><span style="font-size: 10pt; color: navy; font-family: 'Courier New'"></span></font>
            </p>
            <br />
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
