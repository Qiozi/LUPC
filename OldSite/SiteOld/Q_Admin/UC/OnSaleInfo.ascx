<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OnSaleInfo.ascx.cs" Inherits="Q_Admin_UC_OnSaleInfo" %>
<table width="500" style="border: 1px solid #D1DAF6;">
    <tr>
        <td colspan="2" style="background:#D1DAF6; text-align:left; padding: 5px">On Sale:</td>
    </tr>
    <tr>
        <td align="right"  width="200" style="padding:5px;">今天到期產品數量</td><td style="padding:5px;text-align:center"><asp:Label runat="server" ID="lbl_today_sum"></asp:Label></td>
    </tr>
    <tr>
        <td align="right" style="padding:5px;">昨天到期產品數量</td><td style="padding:5px;text-align:center"><asp:Label runat="server" ID="lbl_yestoday_sum"></asp:Label></td>
    </tr>
</table>