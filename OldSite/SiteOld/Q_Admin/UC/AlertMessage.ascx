<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AlertMessage.ascx.cs" Inherits="Q_Admin_UC_AlertMessage" %>

<style type="text/css">
    .style1
    {
        width: 186px;
    }
</style>

<table width="500" style=" border: 1px solid #D1DAF6; ">
    <%--<tr>
        <td align="right" width="200" style="padding:5px;"><asp:Label ID="Label4" runat="server" Text="未完成订单数量"></asp:Label></td>
        <td align="center" style="padding:5px;">
            <asp:LinkButton ID="lbl_order_sum" runat="server" Text="0" 
                onclick="lbl_order_sum_Click" ></asp:LinkButton></td>
    </tr>--%>
<%--    <tr>
        <td align="right" width="200" style="padding:5px;">订单(Submited)</td>
        <td align="center" style="padding:5px;">
            <asp:LinkButton ID="lbl_order_submited_sum" runat="server" Text="0" 
                onclick="lbl_order_submited_sum_Click"></asp:LinkButton></td>
    </tr>--%>
    <tr>
        <td align="right" style="padding:5px;" class="style1"><asp:Label ID="Label5" runat="server">未配置完整的系统数量</asp:Label></td>
        <td align="center" style="padding:5px;"><asp:Label ID="lbl_system_sum" runat="server"  Text="0"></asp:Label></td>
    </tr>
    <tr>
        <td align="right" style="padding:5px;" class="style1"><asp:Label ID="Label1" runat="server"> Current Currency Converter:</asp:Label></td>
        <td align="center" style="padding:5px;"><asp:Label runat="server" ID="lbl_currency_converter" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <a href="change_currency_converter.aspx" class="blue" onclick="return js_callpage_cus(this.href, 'change_currency', 500, 400);">Change</a></td>
    </tr>
   <%-- <tr>
        <td align="right" style="padding:5px;"><asp:Label ID="Label6" runat="server">未回复的问题数量</asp:Label></td>
        <td align="center" style="padding:5px;"><asp:Label ID="lbl_question_sum" runat="server" Text="0"></asp:Label></td>
    </tr>--%>
</table>


