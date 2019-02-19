<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_selected.aspx.cs" Inherits="Q_Admin_orders_edit_detail_selected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
    <div style="width:300px; height: 300px; margin:auto; padding:100px;">
    <asp:Button runat="server" ID="btn_new" Text = "New " Height="28px" Width="52px" 
            onclick="btn_new_Click" />
    <asp:Button runat="server" ID="btn_old" Text = "Old " Height="28px" Width="52px" 
            onclick="btn_old_Click" />
            <br />
            新版还未完成，但可编辑订单。
    </div>
</asp:Content>

