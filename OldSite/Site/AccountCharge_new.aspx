<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountCharge_new.aspx.cs" Inherits="AccountCharge_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
        <table  border="0" width="592" cellpadding="0" cellspacing="0">
              <tr>
                <td align="right" bgcolor="#EFEFEF" style="padding:8px;"><table border="0">
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
                      <td align="right" class="text_hui_11"><strong>Total：</strong></td>
                      <td align="right" class="text_red_12b"><span id="grand_total">
                          <asp:label id="lbl_total" runat="server"></asp:label>&nbsp; &nbsp; &nbsp;</span></td>
                    </tr>
                </table>						
				</td>
              </tr>
          </table>
    </form>
</body>
</html>
