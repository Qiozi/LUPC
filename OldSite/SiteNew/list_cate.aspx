<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="list_cate.aspx.cs" Inherits="list_cate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container page-cate-list">
        <div class="cate-list-box">
            <asp:Repeater runat="server" ID="rptList">
                <ItemTemplate>
                    <a class="item" href="<%# LU.BLL.Util.CateUrl(
                                int.Parse(DataBinder.Eval(Container.DataItem,"menu_child_serial_no").ToString()), 
                                (DataBinder.Eval(Container.DataItem,"menu_child_name_logogram")??"").ToString(), 
                                int.Parse(DataBinder.Eval(Container.DataItem,"page_category").ToString())) %>"><%# DataBinder.Eval(Container.DataItem, "menu_child_name") %></a>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>

