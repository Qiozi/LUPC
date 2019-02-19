<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="get_categories.aspx.cs" Inherits="Q_Admin_ebayMaster_Online_get_categories" Title="Get Ebay Categories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<h2>
Category
</h2>
 <asp:ListBox ID="lb_category1" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="lb_category1_SelectedIndexChanged" Rows="8">
                    </asp:ListBox>
                    <asp:ListBox ID="lb_category2" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="lb_category2_SelectedIndexChanged" Rows="8">
                    </asp:ListBox>
                    <asp:ListBox ID="lb_category3" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="lb_category3_SelectedIndexChanged" Rows="8">
                    </asp:ListBox>
                    <asp:ListBox ID="lb_category4" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="lb_category4_SelectedIndexChanged" Rows="8">
                    </asp:ListBox>
                    <asp:ListBox ID="lb_category5" runat="server" AutoPostBack="True" 
                        onselectedindexchanged="lb_category5_SelectedIndexChanged" Rows="8">
                    </asp:ListBox>
                    <asp:ListBox ID="lb_category6" runat="server" Rows="8"></asp:ListBox>
                    <hr size='1' />
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    <asp:Button runat="server" ID="btn_Next" Text="Next" 
                     Enabled="false"
        onclick="btn_Next_Click" />

</asp:Content>

