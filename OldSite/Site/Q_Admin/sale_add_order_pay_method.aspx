<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="sale_add_order_pay_method.aspx.cs" Inherits="Q_Admin_sale_add_order_pay_method" Title="Untitled Page" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="UC/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" 
        NavigationText="Select Order Pay Method" /> 
    <p style="text-align:center">
    <anthem:DropDownList ID="ddl_pay_method" runat="server">
    </anthem:DropDownList>
    </p>
     <p style="text-align:center"><anthem:Button ID="btn_submit" runat="server" 
             Text="Submit" onclick="btn_submit_Click" /></p>
    
</asp:Content>

