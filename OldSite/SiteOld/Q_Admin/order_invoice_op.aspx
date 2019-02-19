<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="order_invoice_op.aspx.cs" Inherits="Q_Admin_order_invoice_op" Title="Invoice" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="background:#ccc; width: 300px;">
    <table width="300" align="center" height="100" cellspacing="1" class="table_td_white">
        <tr>
                <td>
                    当前订单Invoice No.<asp:Label runat="server" ID="lbl_invoice_no" Font-Bold="true"></asp:Label>
                    <br /><asp:Button runat="server" ID="btn_cancel_order" Text="Cancel" 
                        onclick="btn_cancel_order_Click" />
                </td>
                <td>
                <asp:Button runat="server" ID="Button2" Text="给订单一个新Invoice" onclick="Button2_Click" 
                        /><br />
                    <asp:Button runat="server" ID="Button1" Text="给订单一个新Invoice(void)" 
                        onclick="Button1_Click" />
                </td>
        </tr>
</table></div><asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>


