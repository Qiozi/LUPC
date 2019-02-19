<%@ Page Title="Computer part - LU Computers" Language="C#" MasterPageFile="~/Site.master" EnableViewState="false" AutoEventWireup="true"
    CodeFile="list_part.aspx.cs" Inherits="list_part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container prod-list">
        <div id="topKeyArea" style="z-index: 100; border-radius: 0;">
            <div class="panel panel-default">
                <div class="panel-heading" style="padding-bottom: 0px;">
                    <ul class="list-inline">
                        <li>
                            <div>
                                <span class="glyphicon glyphicon-home"></span>
                                <a href="<%= string.Concat(LU.BLL.Config.Host, "Default.aspx") %>">Home <span class="glyphicon glyphicon-menu-right"></span></a>
                                <a href="<%= LU.BLL.HrefHelper.GetCateList(ParentCid, string.Empty) %>">
                                    <asp:Literal runat="server" ID="ltCateNameParent"></asp:Literal>
                                </a>
                                <span class="glyphicon glyphicon-menu-right"></span>
                            </div>
                        </li>
                        <li>
                            <div class="dropdown">
                                <a id="A1" style="cursor: pointer;" data-target="#" data-toggle="dropdown" aria-haspopup="true"
                                    role="button" aria-expanded="false">
                                    <asp:Literal runat="server" ID="ltCateName"></asp:Literal>
                                    <span class="caret"></span></a>
                                <ul class="dropdown-menu cate-list-dropdown" role="menu" aria-labelledby="dLabel">
                                    <asp:Literal runat="server" ID="ltCates"></asp:Literal>
                                </ul>
                            </div>
                        </li>
                    </ul>
                </div>
                <div class="panel-body">
                    <asp:Literal runat="server" ID="ltKeys"></asp:Literal>
                    <div class="row" id="navBtnArea">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6 text-right">
                            <a class="btn btn-default showKeyAreaBtn" onclick="showFilter($(this));"><span class="glyphicon glyphicon-chevron-up"></span></a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="panel panel-default" style="margin-top: -21px; border-radius: 0;">
            <div class="panel-body">
                <div id="keywordHidden" class="hidden">
                    <asp:Literal runat="server" ID="ltKeyHidden"></asp:Literal>
                </div>
                <div class="row part-list-box">
                    <asp:Repeater runat="server" ID="rptProd">
                        <ItemTemplate>
                            <div class="col-xs-12 col-sm-6 col-md-6 ">
                                <span class="label label-warning onsaletag <%#DataBinder.Eval(Container.DataItem,"Discount").ToString()!="0.00"?"":" hidden" %>">On Sale</span>
                                <div class="part-list-item cursor" onclick="window.location.href='<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem,"Sku").ToString()), DataBinder.Eval(Container.DataItem,"MFP").ToString()) %>';">
                                    <div>
                                        <div class=" pd " style="display: flex; width: 100%; overflow: hidden; padding: 9px;">
                                            <div class="text-center" style="width: 40%;">
                                                <img class="lazy" src='<%= LU.BLL.Config.ResHost %>pro_img/ebay_gallery/9/999999_ebay_list_t_1.jpg'
                                                    data-original="<%# DataBinder.Eval(Container.DataItem,"Avatar") %>" height="135" alt="...">
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
                                                    <%# DataBinder.Eval(Container.DataItem, "Brand") %>
                                                    <small class="pull-right price"><%# DataBinder.Eval(Container.DataItem, "SoldText") %></small>
                                                </h5>
                                                <div class="part-title-line part-name-box"><%# DataBinder.Eval(Container.DataItem,"ProduName") %></div>
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
                                                        <%# DataBinder.Eval(Container.DataItem, "eBayPriceText") %>
                                                    </div>
                                                </a>
                                            </div>

                                        </div>
                                    </div>
                                </div>

                            </div>

                            <%# (1 == Container.ItemIndex%2 )?"</div><div class=\"row part-list-box\">":"" %>
                        </ItemTemplate>
                        <FooterTemplate>
                            <asp:Label ID="lblEmpty" Text="No product data." runat="server" Visible='<%#bool.Parse((rptProd.Items.Count==0).ToString())%>'></asp:Label>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>

            </div>
        </div>
    </div>
    <input type="hidden" id="storeListStyle" value='1' />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript" src="/Scripts/scrollpagination-part-list.js"></script>
    <script type="text/javascript" src="/Scripts/blocksit.min.js"></script>
    <script type="text/javascript" src="/Scripts/listPart.es5.min.js"></script>
</asp:Content>
