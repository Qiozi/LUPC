<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryDropDownLoadAndGreenSplitMenu.ascx.cs" Inherits="Q_Admin_UC_CategoryDropDownLoadAndGreenSplitMenu" %>
<asp:HiddenField ID="category_id" runat="server" 
    onvaluechanged="txt_id_ValueChanged"></asp:HiddenField>
<asp:TextBox ID="category_text" runat="server" Columns="25" AutoPostBack="True" 
    ontextchanged="txt_text_TextChanged"></asp:TextBox>
<asp:LinkButton runat="server" ID="lb_openwin" Text="select" 
    onclick="lb_openwin_Click"></asp:LinkButton><br />
    <asp:HiddenField ID="menu_id" runat="server" />
    <asp:TextBox runat="server" ID="menu_text"></asp:TextBox>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>