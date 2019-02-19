<%@ Page Language="C#"  AutoEventWireup="true" CodeFile="AccountCharge.aspx.cs" Inherits="AccountCharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
    <link href="lu.css" rel="stylesheet" type="text/css"/>
</head>
<body>
    <form id="form1" runat="server">
    <div id="shipping_area">
        <table  border="0" width="592" cellpadding="0" cellspacing="0">
                          <tr>
                            <td align="right" bgcolor="#EFEFEF" style="border-top:#DDDDDD 1px solid; border-bottom:#DDDDDD 

1px solid; padding:8px;"><table border="0">
                                <tr>
                                  <td align="right" class="text_hui_11" ><strong>Sub Total：</strong></td>
                                  <td align="right"><span class="text_red_12b">
                                      <asp:label id="lbl_sub_total" runat="server"></asp:label>&nbsp; &nbsp; &nbsp;</span></td>
                                </tr>
                                <tr>
                                  <td align="right" class="text_hui_11"><strong>Shipping, Handling &amp; Insurance (Not Adjusted)：</strong></td>
                                  <td align="right"><span 

class="text_red_12b">
                                      <asp:label id="lbl_shipping_charge" runat="server">0</asp:label>&nbsp; &nbsp; &nbsp;</span></td>
                                </tr>
                                <tr>
                                  <td align="right" class="text_hui_11"><strong><asp:Label runat="server" ID="lbl_state_tax"></asp:Label></strong></td>
                                  <td align="right" class="text_red_12b">
                                      <asp:label id="lbl_sales_tax" runat="server"></asp:label>&nbsp; &nbsp; &nbsp;</td>
                                </tr>
                                <tr>
                                  <td align="right" class="text_hui_11"><strong>Total (Canadian Dollars)：</strong></td>
                                  <td align="right" class="text_red_12b"><span id="grand_total">
                                      <asp:label id="lbl_total" runat="server"></asp:label>&nbsp; &nbsp; &nbsp;</span></td>
                                </tr>
                                <tr>
                                  <td colspan="2" class="text_hui_11" align="left">
                                <br />
                                    <table align="right" cellpadding="0" cellspacing="0">
                                        <tr >
                                            <td style="padding-left: 12em;border-top: 0px dotted #000000;FONT: 11px/16Px tahoma; COLOR: #4C4C4C; letter-spacing:0px">1.</td>
                                            <td style="border-top: 0px dotted #000000;FONT: 11px/16Px tahoma; COLOR: #4C4C4C; letter-spacing:0px">We reserve right to adjust the shipping fees;</td>
                                        </tr>
                                        <tr>
                                            <td valign="top" style="padding-left: 12em;FONT: 11px/16Px tahoma; COLOR: #4C4C4C; letter-spacing:0px">2.</td>
                                            <td style="FONT: 11px/16Px tahoma; COLOR: #4C4C4C; letter-spacing:0px">Prices are promotional cash discount price.  Regular price will be applied on check out if other pay methods selected.</td>
                                        </tr>
                                    </table>
                                     
                                   

                                 </td>
                                </tr>
                            </table>
						
							</td>
                          </tr>
                      </table>
    </div>
   <script type="text/javascript">
        //parent.document.getElementById("charge_area").innerHTML = document.getElementById("shipping_area").innerHTML;
   </script> 
    </form>
</body>
</html>
