<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_edit_frame_left.aspx.cs" Inherits="Q_Admin_product_edit_frame_left" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <ul>
        <li style="padding-left:10px">
            &nbsp;<a href="/q_admin/product_helper_import_store_price_2.aspx" onclick="JSAParent(this);return false;">Upload Cost/Store</a>
        </li>
    </ul>
    <hr size="1" />
    <div id="div_tree_menu">
        <asp:Literal runat="server" ID="literal_menu"></asp:Literal>  
    </div>
</asp:Content>

