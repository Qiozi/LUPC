<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="detail_part.aspx.cs" Inherits="detail_part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <asp:Literal runat="server" ID="metaKeyword"></asp:Literal>
    <asp:Literal runat="server" ID="metaDesc"></asp:Literal>
    <link href="/Content/ekko-lightbox.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="container" style="background: white;" itemscope itemtype="http://schema.org/Product">
            <input type="hidden" name="sku" value="<%= ReqSKU %>" />
            <input type="hidden" name="cateId" value="<%= CateID %>" />
            <div id="topKeyArea">
                <div class="panel panel-default" style="border-radius: 0;">
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
                                    <a data-target="#" href="<%= string.Concat(LU.BLL.Config.Host, "list_part.aspx?cid=", CateID) %>" data-toggle="dropdown"
                                        aria-haspopup="true" role="button" aria-expanded="false">
                                        <asp:Literal runat="server" ID="ltCateName"></asp:Literal>
                                        <span class="caret"></span>
                                    </a>
                                    <span class="glyphicon glyphicon-menu-right"></span>
                                    SKU:
                                    <%= ReqSKU %>
                                    <ul class="dropdown-menu cate-list-dropdown" role="menu" aria-labelledby="dLabel">
                                        <asp:Literal runat="server" ID="ltCates"></asp:Literal>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="page-header">
                <h1 style="font-size: 2.5rem;" itemprop="name"><%=PartTitle %>
                    <small class="label label-warning " style="<%= IsDiscount ?" font-size:0.7rem; ": " display:none;" %>">On Sale</small></h1>

            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="text-center" style="margin-right: -1em; margin-top: 5px; border: 1px solid #ccc;">
                        <%= LogoGallery %>
                    </div>
                    <%= LogoGallerySumHtml %>
                </div>
                <div class="col-md-7" itemtype="http://schema.org/Offer">
                    <div style="margin: 5px; border: 1px solid #fff;">
                        <ul class="list-group">
                            <li class="list-group-item">LUC SKU number <span class="badge" itemprop="SKU">
                                <%= ReqSKU %></span></li>
                            <li class="list-group-item">Manufacturer<span class="badge" itemprop="manufacturer"><%= Manufacturer%></span></li>

                            <li class="list-group-item">Manufacturer Part#<span class="badge" itemprop="MPN"><%= ManufacturerCode%></span></li>
                        </ul>
                        <div class="">
                            <ul class="list-group" id="ltPriceList">
                                <%= PriceListString %>
                            </ul>
                            <p>
                                This item is available within 24-48 hours. If you place an order before 12 AM we
                                may ship your order the same day (Mon-Fri only).
                            </p>
                            <p>
                                SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash,Interact,bank
                                transfer,money order, etc. Cash price does not waive sales taxes if applicable.
                            </p>

                            <div class="share-component pd"
                                data-disabled="weibo,tencent,qzone,qq,douban,diandian"
                                data-image="<%= LU.BLL.QiNiuImgHelper.Get(ImgSku, 500, 500, 0) %>">
                            </div>

                            <a class="btn btn-default" data-toggle="modal" data-target="#myQuestionModal"><span
                                class="glyphicon glyphicon-question-sign"></span>Ask a Question

                            </a>
                            <a class="btn btn-default btn-danger"
                                id="btnToCart">
                                <span class="glyphicon glyphicon-shopping-cart"></span>Add to Shopping Cart

                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="well" style="margin: 1em auto; text-align: center;">
                <div class="btn-group text-center" id="partCateAllQtyArea">
                    <%= AllQtyCateView %>
                </div>
            </div>
            <%= SuggestString %>
            <div class="row appNoDisplay mg-b">
                <div class="col-md-10">
                    <%= PartSpecificationString %>
                </div>
                <div class="col-md-2">
                </div>
            </div>
        </div>

        <div class="modal fade" tabindex="-1"
            role="dialog"
            id="myQuestionModal">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span></button>
                        <h4 class="modal-title" id="H1">
                            <span class="glyphicon glyphicon-question-sign"></span>Question</h4>
                    </div>
                    <div class="modal-body">
                        <div class="input-group input-group-sm mg-l mg-r">
                            <span class="input-group-addon" id="sizing-addon3">Your Email: (required)</span>
                            <input type="email" id="questEmail" class="form-control" placeholder="@" aria-describedby="sizing-addon3">
                        </div>
                        <div class="input-group input-group-sm" style="margin: 1rem;">
                            <span class="input-group-addon" id="Span1">Subject : (SKU:<%= ReqSKU %>)<input type="hidden"
                                name="sku" value="<%= ReqSKU %>" /></span>
                            <input type="text" id="questSubject" class="form-control" aria-describedby="sizing-addon3">
                        </div>

                        <span class=" text-left mg-l mg-r">Message Body - Remaining characters:</span>
                        <textarea class="mg-l mg-r" rows="4" style="width: 90%;" id="questBody"></textarea>

                        <p class="text-center">
                            <input type="button" class="btn btn-default question-submit" value="Submit" />
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="/Scripts/ekko-lightbox.js"></script>
    <script src="/Scripts/detailPart.es5.min.js"></script>
</asp:Content>
