<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_on_sale.aspx.cs" Inherits="Q_Admin_product_helper_on_sale" Title="Untitled Page" %>

<%@ Register src="UC/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <div style="text-align:center"><uc1:Navigation ID="Navigation1" runat="server" NavigationText="On Sale" />
   SKU <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox><asp:Button ID="Button1"
        runat="server" Text="Add" />
        </div> 
        <hr size="1" />
            <div style="text-align:center">
                begin datetime 
                <asp:TextBox ID="txt_begin_datetime" runat="server"  onFocus="calendar()"></asp:TextBox>
                end datetime
                <asp:TextBox ID="txt_end_datetime" runat="server"  onFocus="calendar()"></asp:TextBox>
                <asp:Button ID="btn_Change_date"
        runat="server" Text="Change Datetime" onclick="btn_Change_date_Click" />
            </div>        
         <hr size="1" />
</asp:Content>

