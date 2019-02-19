<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ebay_system_updata_excel_create.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_system_updata_excel_create" Title="Ebay Sys Updata" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <h2>Ebay System Add</h2>
    <div style="text-align:center; line-height: 30px; padding:1em; border-bottom: 1px solid #ccc;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button_updata" runat="server" Text="Updata" 
            onclick="Button_updata_Click" />
    </div><br />
     <asp:GridView ID="GridView2" runat="server">
    </asp:GridView>
    <asp:GridView ID="GridView1" runat="server">
    </asp:GridView>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

