<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_category_coefficient.aspx.cs" Inherits="Q_Admin_product_helper_category_coefficient" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <h2>
            <asp:Label ID="lbl_menu_child_name" runat="server"></asp:Label>
        </h2>
        <hr size="1" />
        <div style="text-align:center; font-size: 12pt;">
            Cost * <asp:TextBox ID="txt_category_coefficient" runat="server"></asp:TextBox> = Price
            <asp:Button ID="btn_save"
                runat="server" Text="Save" onclick="btn_save_Click" /> 
            
        </div>
</asp:Content>

