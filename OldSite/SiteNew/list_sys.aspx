<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="list_sys.aspx.cs" Inherits="list_sys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="/Content/listSys.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="container">
            <div id="topKeyArea" style="z-index: 100;">
                <div class="panel panel-default">
                    <div class="panel-heading" style="padding-bottom: 0px;">
                        <ul class="list-inline">
                            <li>
                                <div>
                                    <a id="dLabel" data-target="#" aria-haspopup="true" role="button" aria-expanded="false"
                                        href="<%= string.Concat(LU.BLL.Config.Host, "Default.aspx") %>"><span class="glyphicon glyphicon-home"></span> Home 
                                    <span>&gt;</span></a>
                                    <a href="<%= LU.BLL.HrefHelper.GetCateList(ParentCid, string.Empty) %>">
                                    <asp:literal runat="server" id="ltCateNameParent"></asp:literal>

                                    </a>
                                    <span>&gt;</span>

                                    <!--ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
                                       <asp:Literal runat="server" ID="ltCatesParent"></asp:Literal>
                                  </ul-->
                                </div>
                            </li>
                            <li>
                                <div class="dropdown">
                                    <a id="A1" data-target="#" data-toggle="dropdown" aria-haspopup="true" role="button"
                                        aria-expanded="false">
                                        <asp:literal runat="server" id="ltCateName"></asp:literal>
                                        <span class="caret"></span></a>
                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
                                        <asp:literal runat="server" id="ltCates"></asp:literal>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                    </div>
                    <div class="panel-body">
                        <asp:literal runat="server" id="ltKeys"></asp:literal>
                        <div class="row" id="navBtnArea">
                            <div class="col-md-6">
                            </div>
                            <div class="col-md-6 text-right">
                                <a class="btn btn-default btn-sm showFilterBtn"><span class="glyphicon glyphicon-chevron-up"></span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default" style="margin-top: -21px;">
                <div class="panel-body">
                    <div id="keywordHidden" class="hidden">
                        <asp:literal runat="server" id="ltKeyHidden"></asp:literal>
                    </div>
                    <div id="prodListArea">
                        <asp:repeater runat="server" id="rptList" onitemdatabound="rptList_ItemDataBound">
                            <ItemTemplate>
                                <h4>[SKU.<span class='skuColor'><%# DataBinder.Eval(Container.DataItem, "SysSku") %></span>] <%# DataBinder.Eval(Container.DataItem, "eBayTitle") %></h4>
                                <div class="row">
                                    <div class="col-md-3 sysLogoList" data-sku='<%# DataBinder.Eval(Container.DataItem, "SysSku") %>'>
                                        <img src='<asp:Literal runat="server" ID="_ltLogo"></asp:Literal>' />
                                    </div>
                                    <div class="col-md-9 sysPartList"
                                        data-price='<%# DataBinder.Eval(Container.DataItem, "Price") %>'
                                        data-discount='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'
                                        data-sold='<%# DataBinder.Eval(Container.DataItem, "Sold") %>'>
                                        <asp:Repeater runat="server" ID="_rptSubParts">
                                            <HeaderTemplate>
                                                <table class='table table-condensed table-striped'>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td>
                                                        <a class='' style='color: #000;'><%# DataBinder.Eval(Container.DataItem, "Comment") %></a>
                                                    </td>
                                                    <td>
                                                        <a class='' target="_blank" href="<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem, "PartSku").ToString()), "") %>" style='color: #666;'><%# DataBinder.Eval(Container.DataItem, "PartTitle") %></a>
                                                    </td>
                                                    <td>
                                                        <a class=' <%# string.IsNullOrEmpty((DataBinder.Eval(Container.DataItem, "eBayCode")??"").ToString())?" hidden":"" %>' href='<%# DataBinder.Eval(Container.DataItem, "eBayHref") %>' target='_blank'>
                                                            <img src='<%= string.Concat(LU.BLL.Config.ResHost, "images/ebay_logo.jpg") %>'>
                                                        </a>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                       <div class="row">
                                            <div class="col-md-6">
                                                <asp:Literal runat="server" ID="_ltPrice"></asp:Literal>
                                            </div>
                                            <div class="col-md-6">
                                                <a class="btn btn-default"
                                                    onclick="util.addToCart($(this).data('sku'))"
                                                    data-sku='<%# DataBinder.Eval(Container.DataItem, "SysSku") %>'>
                                                    <span class='glyphicon glyphicon-shopping-cart'> Buy It Now</a>
                                                <a class="btn btn-default" href='/detail_sys_customize.aspx?sku=<%# DataBinder.Eval(Container.DataItem, "SysSku") %>'>
                                                    <span class='glyphicon glyphicon-wrench'></span> Customize It
                                                </a>
                                                <a class="btn btn-default" href='/computer/system/<%# DataBinder.Eval(Container.DataItem, "SysSku") %>.html'>
                                                    <span class='glyphicon glyphicon-calendar'></span> Detail
                                                </a>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:repeater>
                        <asp:literal runat="server" id="ltRptNote"></asp:literal>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="/Scripts/listSys.es5.min.js"></script>
</asp:Content>
