<%@ Page Language="C#" MasterPageFile="~/Site.master" EnableViewState="false" AutoEventWireup="true" CodeFile="list_brand.aspx.cs" Inherits="list_brand" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <a name="ttop"></a>
        <div class="mg-t">
            <a href="<%= BrandHref %>" target="_blank">
                <img src="<%= BrandLogo %>" />
            </a>
        </div>
        <hr />
        <asp:Repeater runat="server" ID="rptList2">
            <ItemTemplate>
                <a class="btn " href="#<%# DataBinder.Eval(Container.DataItem, "menu_child_name") %>">
                    <i class="iconfont">
                        <%# DataBinder.Eval(Container.DataItem, "menu_child_name_f") %>
                    </i>
                    <%# DataBinder.Eval(Container.DataItem, "menu_child_name") %>
                </a>
            </ItemTemplate>
        </asp:Repeater>
        <asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_ItemDataBound">
            <ItemTemplate>
                <div class="title1">
                    <i class="iconfont">
                        <%# DataBinder.Eval(Container.DataItem, "menu_child_name_f") %>
                    </i>
                    <%# DataBinder.Eval(Container.DataItem, "menu_child_name") %>
                    <a name="<%# DataBinder.Eval(Container.DataItem, "menu_child_name") %>"></a>
                    <a class="btn pull-right btn-link" href="#ttop" style="margin-top: -6px;">TOP</a>
                </div>
                <asp:HiddenField runat="server" ID="brandName" Value='<%# DataBinder.Eval(Container.DataItem, "menu_child_name") %>' />
                <asp:HiddenField runat="server" ID="cateId" Value='<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>' />
                <div class="row part-list-box">
                    <asp:Repeater runat="server" ID="_rptSub">
                        <HeaderTemplate>
                        </HeaderTemplate>
                        <ItemTemplate>

                            <%--<div class="row " itemscope itemtype="http://schema.org/Product">
                                <div class="col-md-1">
                                    <img class="lazy" itemprop="image"
                                        src='https://www.lucomputers.com/pro_img/Components/999999_t.jpg'
                                        data-original="<%# DataBinder.Eval(Container.DataItem, "logo") %>" />
                                </div>
                                <div class="col-md-6 pd-t">
                                    <a href='<%# DataBinder.Eval(Container.DataItem, "webHref") %>' itemprop="name"><%# DataBinder.Eval(Container.DataItem, "name") %></a>
                                </div>
                                <div class="col-md-2 text-right pd-t" itemprop="offers" itemscope itemtype="http://schema.org/Offer">
                                    <span class="price" itemprop="price">$<%# DataBinder.Eval(Container.DataItem, "price") %></span><span class="price-unit" style="display: inline-block; margin-left: 5px;">CAD</span>
                                </div>
                                <div class="col-md-3 text-right pd-t">
                                    <span class="<%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "eBayCode").ToString())?" hidden":"" %>">
                                        <a href="<%# DataBinder.Eval(Container.DataItem, "eBayHref") %> " target="_blank">
                                            <%= LU.BLL.Util.eBayFont() %> :
                                     <%# DataBinder.Eval(Container.DataItem, "eBayCode") %> 
                                        </a>
                                        (<span class="price">$<%# DataBinder.Eval(Container.DataItem, "eBayPrice") %></span>) 
                                    </span>
                                </div>
                            </div>--%>
                            <div class="col-xs-12 col-sm-6 col-md-6">

                                <div class="part-list-item cursor" onclick="window.location.href='<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem,"Sku").ToString()), DataBinder.Eval(Container.DataItem,"MFP").ToString()) %>';">
                                    <div itemscope itemtype="http://schema.org/Product">
                                        <div class=" pd " style="display: flex; width: 100%; overflow: hidden; padding: 9px;">
                                            <div class="text-center" style="width: 40%;">
                                                <img class="lazy" itemprop="image" src='<%= LU.BLL.Config.ResHost %>pro_img/ebay_gallery/9/999999_ebay_list_t_1.jpg'
                                                    data-original="<%# DataBinder.Eval(Container.DataItem,"logo") %>" height="135" alt="...">
                                            </div>
                                            <div style="width: 60%">
                                                <%-- <div class="task prod-long-title">
                                                
                                                    <div class="buy-button-box">

                                                        <a class="btn <%# string.IsNullOrEmpty(DataBinder.Eval(Container.DataItem, "eBayCode").ToString())?" disabled btn-default":" btn-warning" %>"
                                                            href="<%# LU.BLL.eBayProvider.GetEBayHref(DataBinder.Eval(Container.DataItem, "eBayCode").ToString()) %>">Buy on eBay.ca</a>
                                                        <a class="btn btn-danger" onclick="util.addToCart('<%# DataBinder.Eval(Container.DataItem,"Sku") %>');"><span class="glyphicon glyphicon-shopping-cart"></span>&nbsp; Add to Cart</a>
                                                        <a class="btn btn-default"
                                                            href="<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem,"Sku").ToString()), DataBinder.Eval(Container.DataItem,"MFP").ToString()) %>">
                                                            <span class="glyphicon glyphicon-th"></span>&nbsp;Detail
                                                        </a>
                                                    </div>
                                                </div>--%>
                                                <h5 class="cate-title">
                                                    <%= ReqBrandName %>
                                                    <small itemprop="offers" itemscope itemtype="http://schema.org/Offer" itemprop="price" class="pull-right price"><%# DataBinder.Eval(Container.DataItem, "price") %></small>
                                                </h5>
                                                <div class="part-title-line part-name-box"><%# DataBinder.Eval(Container.DataItem,"name") %></div>
                                                <div style="display: flex; justify-content: flex-end;">
                                                    <div>
                                                        <a class="btnBuy"
                                                            href="<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem,"Sku").ToString()), DataBinder.Eval(Container.DataItem,"MFP").ToString()) %>">Buy
                                                        </a>
                                                    </div>
                                                </div>
                                                <a class='row ebay-btn-box' style="display: flex;" href="<%# LU.BLL.eBayProvider.GetEBayHref(DataBinder.Eval(Container.DataItem, "eBayCode").ToString()) %>" target="_blank">
                                                    <div class="" style="flex-grow: 1;">
                                                        <%= LU.BLL.Util.eBayFont() %>
                                                    </div>
                                                    <div style="flex-grow: 1;">
                                                        <%# DataBinder.Eval(Container.DataItem, "eBayCode") %>
                                                    </div>
                                                    <div class="price-color-ebay text-right" style="flex-grow: 1;">
                                                        <%# DataBinder.Eval(Container.DataItem, "eBayPrice").ToString() =="0"?"--":DataBinder.Eval(Container.DataItem, "eBayPrice").ToString() %>
                                                    </div>
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <%# (1 == Container.ItemIndex%2 )?"</div><div class=\"row part-list-box\">":"" %>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script>
        $(function () {
            $("img.lazy").lazyload();
        })
    </script>
</asp:Content>
