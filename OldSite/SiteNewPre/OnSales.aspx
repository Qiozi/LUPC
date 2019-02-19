<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true"
    CodeFile="OnSales.aspx.cs" Inherits="OnSales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .affix
        {
            position: fixed;
            top: 60px;
        }
        .affix-bottom
        {
            position: absolute;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background-color: White;">
        <div class="panel panel-default">
            <div class="panel-heading" style="padding-bottom: 0px;">
                <ul class="list-inline">
                    <li>
                        <div>
                            <a href="/Default.aspx"><span class="glyphicon glyphicon-home"></span> Home <span
                                class="glyphicon glyphicon-menu-right"></span></a>
                        </div>
                    </li>
                    <li>On Sales </li>
                </ul>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2">
                <div data-spy="affix" data-offset-top="70" data-offset-bottom="200">
                    <asp:Repeater runat="server" ID="rptList">
                        <HeaderTemplate>
                            <ul class="list-group">
                                <li class="list-group-item list-group-item-success"> Shortcut</li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="list-group-item"><a href="?#<%# Eval("CateName") %>">
                                <%# Eval("CateName") %></a> </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-lg-10">
            <div class="panel panel-default">
                    <!-- Default panel contents -->
                    <div class="panel-heading">
                        Onsale Center</div>
                   <%-- <div class="panel-body note">
                        <p>
                          
                        </p>
                    </div>--%>
                <asp:Repeater runat="server" ID="rptList2" OnItemDataBound="rptList2_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table table-striped">
                            <thead>
                                <tr>
                                    <th>SKU</th>
                                    <th>Save</th>
                                    <th>Title</th>
                                    <th>date</th>
                                </tr>
                            </thead>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr class="success">
                            <td colspan="4"><h5>
                                    <a id="<%# Eval("CateName") %>"></a>
                                    <asp:Literal runat="server" ID="ltCateName" Text='<%# Eval("CateName") %>'></asp:Literal></h5></td>
                        </tr>
                                
                        </div>
                        <asp:Repeater runat="server" ID="rptSubList">
                            <ItemTemplate>
                                <tr>
                                    <td title="SKU">
                                        <%# Eval("SKU") %>
                                    </td>
                                    <td class="text-right" title="Save">
                                        $<%# Eval("Price") %>
                                    </td>
                                    <td>
                                        <%# Eval("ProdName") %>
                                    </td>
                                    <td>
                                        <%# Eval("BeginDate", "{0:yyyy-MM-dd}") %>
                                        -
                                        <%# Eval("EndDate", "{0:yyyy-MM-dd}")%>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server"></asp:Content>