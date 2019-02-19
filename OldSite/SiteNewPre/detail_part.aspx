<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="detail_part.aspx.cs" Inherits="detail_part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <asp:Literal runat="server" ID="metaKeyword"></asp:Literal>
    <asp:Literal runat="server" ID="metaDesc"></asp:Literal>

    <link href="/Content/ekko-lightbox.min.css" rel="stylesheet" />
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

        .suggestList table {
            float: left;
            width:222px;
        }
        .suggestList table td{padding:5px;}
        .suggestList {
            border-top: 1px solid #ccc;
            border-bottom:1px solid #ccc;
            height:290px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="container" style="background: white;">
            <div id="topKeyArea" style="z-index: 100;">
                <div class="panel panel-default">
                    <div class="panel-heading" style="padding-bottom: 0px;">
                        <ul class="list-inline">
                            <li>
                                <div>
                                    <span class="glyphicon glyphicon-home"></span>
                                    <a href="/Default.aspx">Home
                                         <span
                                             class="glyphicon glyphicon-menu-right"></span></a>
                                    <a id="dLabel" data-target="#"
                                        href="/default.aspx"></a>
                                    <asp:Literal runat="server" ID="ltCateNameParent"></asp:Literal>
                                    <span class="glyphicon glyphicon-menu-right"></span>
                                </div>
                            </li>
                            <li>
                                <div class="dropdown">
                                    <a id="A1" data-target="#" href="/list_part.aspx?cid=<%= CateID %>" data-toggle="dropdown"
                                        aria-haspopup="true" role="button" aria-expanded="false">
                                        <asp:Literal runat="server" ID="ltCateName"></asp:Literal>
                                        <span class="caret"></span>
                                    </a>
                                    <span class="glyphicon glyphicon-menu-right"></span>
                                    SKU:
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
                <h3><%=PartTitle %></h3>
            </div>
            <div class="row">
                <div class="col-md-6">
                    <div class="text-center" style="margin-right: -1em; margin-top: 5px; border: 1px solid #ccc;">
                        <%= LogoGallery %>
                    </div>
                    <%= LogoGallerySumHtml %>
                </div>
                <div class="col-md-6 ">
                    <div style="margin: 5px; border: 1px solid #fff;">
                        <ul class="list-group">
                            <li class="list-group-item">Manufacturer Part#<span class="badge"><%= ManufacturerCode%></span></li>
                            <li class="list-group-item">Manufacturer<span class="badge"><%= Manufacturer%></span></li>
                            <li class="list-group-item">LUC SKU number <span class="badge">
                                <%= ReqSKU %></span></li>
                        </ul>
                        <div class="priceArea">
                            <p class="list-group" id="ltPriceList">
                                <img src="/images/loaderc.gif" />
                            </p>
                            <p>
                                This item is available within 24-48 hours. If you place an order before 12 AM we
                                may ship your order the same day (Mon-Fri only).
                            </p>
                            <p>
                                SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash,Interact,bank
                                transfer,money order, etc. Cash price does not waive sales taxes if applicable.
                            </p>
                            <a class="btn btn-default" data-toggle="modal" data-target="#myQuestionModal"><span
                                class="glyphicon glyphicon-question-sign"></span>Ask a Question</a> <a class="btn btn-default btn-danger"
                                    id="btnToCart" href="/ShoppingCartTo.aspx?sku=<%= ReqSKU %>"><span class="glyphicon glyphicon-shopping-cart"></span>Add to Shopping Cart</a>
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
            <div class="row">
                <div class="col-md-10">
                    <%= PartSpecificationString %>
                    
                </div>
                <div class="col-md-2">
                </div>
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
                    <p>
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="sizing-addon3">Your Email: (required)</span>
                            <input type="text" id="questEmail" class="form-control" placeholder="@" aria-describedby="sizing-addon3">
                        </div>
                    </p>
                    <p>
                        <div class="input-group input-group-sm">
                            <span class="input-group-addon" id="Span1">Subject : (SKU:<%= ReqSKU %>)<input type="hidden"
                                name="sku" value="<%= ReqSKU %>" /></span>
                            <input type="text" id="questSubject" class="form-control" aria-describedby="sizing-addon3">
                        </div>
                    </p>
                    <p>
                        <span class="input-group-addon text-left ">Message Body - Remaining characters:</span>
                        <textarea rows="4" cols="70" style="width: 100%" id="questBody"></textarea>
                    </p>
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

        $(document).ready(function () {

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

            $.get("/cmds/prod.aspx", { cmd: 'getPartCateAllQty', cid: '<%= CateID %>', sku: '<%= ReqSKU %>' }, function (data) {
                $('#partCateAllQtyArea').html(data);
            });

            $.get("/cmds/prod.aspx", { cmd: 'getPartPriceArea', cid: '<%= CateID %>', sku: '<%= ReqSKU %>' }, function (data) {
                $('#ltPriceList').html(data);
                if (data.indexOf('Out of Stock') > -1) {
                    $('#btnToCart').addClass('disabled');
                }
            });

        });

        function saveQuestion() {
            $.ajax({
                type: "post",
                url: "/cmds/SaveQuestion.aspx",
                data: { sku: '<%= ReqSKU %>', username: $('#questEmail').val(), subject: $('#questSubject').val(), questBody: $('#questBody').val() },
                error: function (r, s, t) { alert(r); },
                success: function (msg, s) {
                    if (s == "success") {
                        alert(msg);
                        $('#questEmail').val('');
                        $('#questSubject').val('');
                        $('#questBody').val('');
                        $('#myQuestionModal').modal('hide')
                    }
                }
            });

        }
    </script>
</asp:Content>
