<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_factory.aspx.cs" Inherits="Q_Admin_product_helper_factory" Title="Untitled Page" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" />
   <div style="text-align: center;">
       <anthem:Button ID="btnCreate" runat="server" OnClick="btnCreate_Click" Text="Create" />
         <anthem:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" /> 
   </div>
  
    <anthem:datagrid id="dgFactory" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <Columns>
            <asp:BoundColumn DataField="producter_serial_no" HeaderText="ID"></asp:BoundColumn>
            <asp:TemplateColumn HeaderText="factory name">
                <itemtemplate>
<anthem:TextBox id="_txt_factory_name" runat="server" Text='<%# DataBinder.Eval(Container.DataItem , "producter_name") %>' Width="250px" __designer:wfdid="w5" CssClass="input"></anthem:TextBox> 
</itemtemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="factory web site">
                <itemtemplate>
<anthem:TextBox id="_txt_factory_web" runat="server" Text='<%# DataBinder.Eval(Container.DataItem , "producter_web_address") %>' Width="250px" __designer:wfdid="w6" CssClass="input"></anthem:TextBox> 
</itemtemplate>
            </asp:TemplateColumn>
        </Columns>
        <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <EditItemStyle BackColor="#999999" />
        <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
    </anthem:datagrid>

</asp:Content>

