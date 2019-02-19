<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sales_customer_msg.aspx.cs" Inherits="Q_Admin_sales_customer_msg" Title="Order Message" %>

<%@ Register src="UC/main_menus.ascx" tagname="main_menus" tagprefix="uc1" %>
<%@ Register src="UC/CustomerMsgList.ascx" tagname="CustomerMsgList" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc2:CustomerMsgList ID="CustomerMsgList1" runat="server" />

</asp:Content>

