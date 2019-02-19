<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_keywords.aspx.cs" Inherits="Q_Admin_product_helper_keywords" Title="Part Search Keyword Edit" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" />
    <anthem:button id="btn_new" runat="server" OnClick="btn_new_Click" Text="New"></anthem:button>
    <anthem:Button ID="btn_save" runat="server" OnClick="btn_save_Click" Text="Save" />
    <hr size="1" />
    注: 自动删除keyword为空的纪录
    <br /><br />
    <anthem:datagrid id="DataGrid1" runat="server" autogeneratecolumns="False"><Columns>
<asp:BoundColumn DataField="id" HeaderText="id"></asp:BoundColumn>
<asp:TemplateColumn HeaderText="Keyword"><ItemTemplate>
<anthem:TextBox id="_txt_keywords" runat="server" __designer:wfdid="w4" Text='<%# DataBinder.Eval(Container.DataItem, "keyword") %>'></anthem:TextBox>
</ItemTemplate>
</asp:TemplateColumn>

</Columns>
</anthem:datagrid>
</asp:Content>

