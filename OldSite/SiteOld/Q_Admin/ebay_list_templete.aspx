<%@ Page Language="C#" MasterPageFile="~/Q_Admin/ebay.master" AutoEventWireup="true" CodeFile="ebay_list_templete.aspx.cs" Inherits="Q_Admin_ebay_list_templete" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="btn_list">
        <a href="ebay_edit_templete.aspx">New</a>       
    </div>
    <hr size="1" style="clear:both" />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
    Width="780px">
        <Columns>
<asp:BoundField DataField="id" HeaderText="ID">
    <HeaderStyle Height="20px" />
    <ItemStyle Height="25px" HorizontalAlign="Center" Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="templete_type" HeaderText="Type">
                <ItemStyle Width="80px" />
            </asp:BoundField>
            <asp:BoundField DataField="templete_comment" HeaderText="comment" >
                <ItemStyle Width="400px" />
            </asp:BoundField>
            <asp:BoundField DataField="last_regdate" HeaderText="last modify" >
                <ItemStyle Width="150px" />
            </asp:BoundField>
            <asp:HyperLinkField DataNavigateUrlFields="id" 
                DataNavigateUrlFormatString="ebay_edit_templete.aspx?id={0}" Text="edit" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:HyperLinkField>
        </Columns>
    </asp:GridView>
</asp:Content>

