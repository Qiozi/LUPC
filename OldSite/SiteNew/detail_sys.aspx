<%@ Page Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="detail_sys.aspx.cs" Inherits="detail_sys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <asp:Literal runat="server" ID="metaKeyword"></asp:Literal>
    <style>
        .list-group-item > .badge {
            background: white;
            color: #333;
        }

        .list-group-item > .price {
            color: #FF5D13;
        }

        .priceArea li {
            padding: 3px 10px 3px 10px;
        }

        .affix {
            position: fixed;
            top: 50px;
        }

        .dropdown-menu li {
            padding: 5px;
        }
    </style>
    <link href="/Content/share.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="container" style="background: white;" itemscope itemtype="http://schema.org/Product">
            <div id="topKeyArea" style="z-index: 100;">
                <div class="panel panel-default">
                    <div class="panel-heading" style="padding-bottom: 0px;">
                        <ul class="list-inline">
                            <li>
                                <div>
                                    <span class="glyphicon glyphicon-home"></span>
                                    <a href="<%= string.Concat(LU.BLL.Config.Host, "Default.aspx") %>">Home <span class="glyphicon glyphicon-menu-right"></span></a>                                    
                                    <asp:Literal runat="server" ID="ltCateNameParent"></asp:Literal>
                                    <span class="glyphicon glyphicon-menu-right"></span>
                                </div>
                            </li>
                            <li>
                                <div class="dropdown">
                                    <a id="A1" data-target="#" href="<%= string.Concat(LU.BLL.Config.Host, "list_sys.aspx?cid=", CateID) %>" data-toggle="dropdown"
                                        aria-haspopup="true" role="button" aria-expanded="false">
                                        <asp:Literal runat="server" ID="ltCateName"></asp:Literal>
                                        <span class="caret"></span><span class="glyphicon glyphicon-menu-right"></span>
                                    </a>SKU:
                                    <%= ReqSKU %>
                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
                                        <asp:Literal runat="server" ID="ltCates"></asp:Literal>
                                    </ul>
                                </div>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="page-header">
                <h1 itemprop="name" style="font-size: 2rem; font-weight: bold;">
                    <asp:Literal runat="server" ID="ltTitle"></asp:Literal>
                </h1>
            </div>
            <div class="row">
                <div class="col-md-5">
                    <div class="text-center" style="margin: 0em -1em 0 0; border: 1px solid #ccc;">
                        <%--<img src="https://lucomputers.com/pro_img/COMPONENTS/<%= ImgSku %>_g_1.jpg" width="420"
                            border='1' />--%>
                        <%=LogoGallery %>
                        <p>
                            <a onclick="$('#someBigImage').ekkoLightbox();" style='cursor: pointer;'>Gallery</a>
                        </p>
                    </div>
                </div>
                <div class="col-md-7">
                    <ul class="list-group">
                        <li class="list-group-item">LUC SKU number <span class="badge">
                            <%= ReqSKU %></span></li>
                    </ul>
                </div>
                <div class="col-md-7 ">
                    <ul class="list-group" itemtype="http://schema.org/Offer">
                        <asp:Literal runat="server" ID="ltPriceList"></asp:Literal>
                    </ul>
                    <p>
                        This item is available within 24-48 hours. If you place an order before 12 AM we
                        may ship your order the same day (Mon-Fri only).
                    </p>
                    <p>
                        SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash,Interact,bank
                        transfer,money order, etc. Cash price does not waive sales taxes if applicable.

                    </p>
                    <div class="share-component pd text-right"
                        data-disabled="weibo,tencent,qzone,qq,douban,diandian"
                        data-image="<%= LU.BLL.QiNiuImgHelper.Get(ImgSku, 500, 500, 0) %>">
                    </div>
                    <p>
                        <a class="btn btn-default hide">
                            <span class="glyphicon glyphicon-envelope"></span>&nbsp;Email to Me</a>
                        <a class="btn btn-default" data-toggle="modal" data-target="#myQuestionModal">
                            <span class="glyphicon glyphicon-question-sign"></span>&nbsp;Ask a Question</a>
                    </p>

                    <a class="btn btn-default btn-danger" id="A2" href="/detail_sys_customize.aspx?sku=<%= ReqSKU %>">
                        <span class="glyphicon glyphicon-cog"></span>&nbsp;Customize It</a>
                    <a class="btn btn-default btn-danger"
                        id="btnToCart">
                        <span class="glyphicon glyphicon-shopping-cart"></span>&nbsp;Add to Shopping Cart</a>
                </div>
            </div>
            <div class="well" style="margin: 1em auto; text-align: center;">
                <div class="btn-group text-center">
                    <%= AllQtyCateView %>
                </div>
            </div>
            <div class="panel panel-default">
                <!-- Default panel contents -->
                <div class="panel-heading">
                    System Detail
                </div>
                <div class="panel-body">
                    <p class="note">
                        For more options on this configuration please select the "Customize" button. You
                        have the option to add, remove, or change most components in this configuration
                        to assure that all of your technology needs are fulfilled. To speak to a live consultant
                        regarding your needs please call 1866-999-7828. You can submit your order online
                        or call 1866-999-7828 416-446-7323(local). Local pick up is welcomed.
                    </p>
                </div>
                <%= SysListString %>
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel"
        id="myQuestionModal" aria-hidden="true">
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
                        <input type="text" id="questEmail" class="form-control" placeholder="@" aria-describedby="sizing-addon3">
                    </div>
                    <div class="input-group input-group-sm" style="margin: 1rem;">
                        <span class="input-group-addon" id="Span1" itemprop="SKU">Subject : (SKU:<%= ReqSKU %>)<input type="hidden"
                            name="sku" value="<%= ReqSKU %>" /></span>
                        <input type="text" id="questSubject" class="form-control" aria-describedby="sizing-addon3">
                    </div>
                    <span class="text-left  mg-l mg-r">Message Body - Remaining characters:</span>
                    <textarea rows="4" class="mg-l mg-r" cols="70" style="width: 90%" id="questBody"></textarea>
                    <p class="text-center">
                        <input type="button" class="btn btn-default" value="Submit" onclick="saveQuestion();" />
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="/Scripts/ekko-lightbox.js"></script>
    <script type="text/javascript">
        $(function () {
            $('#btnToCart').on('click', function () {
                $.get("/ShoppingCartTo.aspx?sku=<%= ReqSKU %>", {}, function (data) {
                    var data = JSON.parse(data);
                    if (data.Success) {
                        window.location.href = data.ToUrl;
                    }
                });
            });

            $('#topKeyArea').affix({
                offset: {
                    top: 50,
                    bottom: function () {
                        //alert((this.bottom = $('#page-bottom').outerHeight(true)));
                        return (this.bottom = $('#page-bottom').outerHeight(true));
                    }
                }
            });
            $('#topKeyArea').find('table').eq(0)
            .css({ width: $('.container').outerWidth() - 64 });
        });
        function saveQuestion() {
            $.ajax({
                type: "post",
                url: "/cmds/SaveQuestion.aspx",
                data: { sku: '<%= ReqSKU %>', username: $('#questEmail').val(), subject: $('#questSubject').val(), questBody: $('#questBody').val() },
                error: function (r, s, t) { alert(r); },
                success: function (msg, s) {
                    if (s == "success") {
                        util.alertSuccess(msg);
                        $('#questEmail').val('');
                        $('#questSubject').val('');
                        $('#questBody').val('');
                        $('#myQuestionModal').modal('hide')
                    }
                }
            });
        }
    </script>

    <script src="/Scripts/jquery.share.min.js"></script>
</asp:Content>
