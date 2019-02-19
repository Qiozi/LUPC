<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="list_part.aspx.cs" Inherits="list_part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .price_list {
            list-style-type: none;
        }

            .price_list li {
                font-size: 14px;
            }

        body {
            position: relative;
        }

        .breadcrumb {
            margin: 0px;
        }

        #keyListArea a {
            float: left;
            display: inline-block;
            padding: 3px;
            margin-left: 10px;
        }

            #keyListArea a:hover {
                background: #AC745D;
                color: White;
                cursor: pointer;
                text-decoration: none;
            }

        #keyListArea .closebutton {
            border: 1px solid red;
            padding: 3px 5px 3px 5px;
        }

            #keyListArea .closebutton:hover {
                border: 1px solid blue;
                background: white;
                color: Blue;
                padding: 3px 5px 3px 5px;
            }

        #prodListArea {
            position: relative;
            width: 1100px;
            margin: 0 auto 25px;
            padding-bottom: 10px;
        }

            #prodListArea a {
                color: #666;
            }

                #prodListArea a:hover {
                    text-decoration: none;
                }

            #prodListArea li:hover {
                background-color: #f2f2f2;
                color: #333;
            }

                #prodListArea li:hover a {
                    color: #333;
                }

                #prodListArea li:hover img {
                    border: 1px solid #B5DAA2;
                }

        .affix {
            position: fixed;
            top: 50px;
        }

        .dropdown-menu li {
            padding: 5px;
        }


        .grid {
            width: 205px;
            min-height: 100px;
            padding: 15px;
            background: #fff;
            margin: 8px;
            font-size: 12px;
            float: left;
            box-shadow: 0 1px 3px rgba(34,25,25,0.4);
            -moz-box-shadow: 0 1px 3px rgba(34,25,25,0.4);
            -webkit-box-shadow: 0 1px 3px rgba(34,25,25,0.4);
            -webkit-transition: top 1s ease, left 1s ease;
            -moz-transition: top 1s ease, left 1s ease;
            -o-transition: top 1s ease, left 1s ease;
            -ms-transition: top 1s ease, left 1s ease;
        }

            .grid strong {
                border-bottom: 1px solid #ccc;
                margin: 0px 0;
                display: block;
                padding: 0 0 5px;
                font-size: 17px;
            }

            .grid .meta {
                text-align: right;
                color: #777;
                font-style: italic;
            }

            .grid .imgholder img {
                max-width: 100%;
                background: #ccc;
                display: block;
                background: url(images/loaderc.gif) no-repeat center;
            }
    </style>
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
                                    <span class="glyphicon glyphicon-home"></span>
                                    <a href="Default.aspx">Home <span class="glyphicon glyphicon-menu-right"></span></a>
                                    <a id="dLabel" data-target="#" href="default.aspx"></a>
                                    <asp:Literal runat="server" ID="ltCateNameParent"></asp:Literal>
                                    <span class="glyphicon glyphicon-menu-right"></span>
                                </div>
                            </li>
                            <li>
                                <div class="dropdown">
                                    <a id="A1" style="cursor: pointer;" data-target="#" data-toggle="dropdown" aria-haspopup="true"
                                        role="button" aria-expanded="false">
                                        <asp:Literal runat="server" ID="ltCateName"></asp:Literal>
                                        <span class="caret"></span></a>
                                    <ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
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
                                <div class="btn-group" role="group" aria-label="...">
                                    <div class="btn-group" role="group">
                                        <button id='btn-list-style-1' type="button" class="btn btn-default" onclick="loadList(1);">
                                            <span class="glyphicon glyphicon-th-list"></span>
                                        </button>
                                        <button id='btn-list-style-2' type="button" class="btn btn-default" onclick="loadList(2);">
                                            <span class="glyphicon glyphicon-th"></span>
                                        </button>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-6 text-right">
                                <a class="btn btn-default btn" onclick="showFilter($(this));"><span class="glyphicon glyphicon-chevron-up"></span></a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="panel panel-default" style="margin-top: -21px;">
                <div class="panel-body">
                    <div id="keywordHidden" class="hidden">
                        <asp:Literal runat="server" ID="ltKeyHidden"></asp:Literal>
                    </div>
                    <div id="wrapper">
                        <div id="prodListArea">
                        </div>
                    </div>
                    <div class="loading alert alert-danger" id="loading">
                        Wait a moment... it's loading!
                    </div>
                    <div class="loading alert alert-warning text-center hide" id="nomoreresults">
                        End.
                    </div>
                </div>
            </div>
        </div>
    </div>
    <input type="hidden" id="storeListStyle" value='1' />

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="/js/scrollpagination-part-list.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js/blocksit.min.js"></script>
    <script type="text/javascript">

        var listCount = 0;
        var page = 1;

        $(document).ready(function () {
            //$('#btn-list-style-1').addClass("active");
            var listStyle = $.cookie('prodListStyle');
            if (listStyle == null)
                listStyle = 1;
            //alert(listStyle);
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
            $('#navBtnArea').css({ width: $('.container').outerWidth() - 32 })

            $(window).scroll(function () {
                if ($('#storeListStyle').val() == 2) {
                    if ($(document).height() - $(this).scrollTop() - $(this).height() < 50) {

                        loadList($('#storeListStyle').val());


                    }
                }

            });
            loadList(listStyle);
        });

        function navAffix() {

        }

        function showFilter(the) {
            if ($('#keyListArea').css('display') == 'none') {
                $('#keyListArea').css({ display: '' })
                the.html("<span class=\"glyphicon glyphicon-chevron-up\"></span>");
            }
            else {
                $('#keyListArea').css({ display: 'none' })
                the.html("<span class=\"glyphicon glyphicon-chevron-down\"></span>");
            }
            $('#topKeyArea').css({ width: $(this).parent().css('width') });
        }

        function loadListScroll() {
            $('#storeListStyle').val('2')
            var key = $('#keywordHidden').text();
            $.ajax({
                type: 'get',
                url: 'cmds/prod.aspx',
                data: { cmd: 'list', cid: '<%= ReqCateId %>', page: page, key: key },
                error: function (r, s, t) {
                },
                success: function (m, s) {

                    if (page < 2)
                        $('#prodListArea').html(m);
                    else
                        $('#prodListArea').append(m);
                    page += 1;
                    $("img.lazy").lazyload();
                    $('#prodListArea').BlocksIt({
                        numOfCol: 5,
                        offsetX: 8,
                        offsetY: 8
                    });

                    $('#loading').css({ display: 'none' });
                    $('#nomoreresults').css({ display: '' });
                }
            });
        }

        function loadList(t) {

            $.cookie('prodListStyle', t, { expires: 700 });
            $('#loading').css({ display: '' });
            $('#nomoreresults').addClass('hide');
            if ($('#storeListStyle').val() != t)
                page = 1;
            if (t == 2) {
                loadListScroll()
                return;
            }

            var url = "Computer/parts/<%=ReqCateId %>/list.txt";
            if (window.location.href.toString().indexOf("http://us.") > -1 || window.location.href.toString().indexOf("https://us.") > -1) {
                url = "Computer/parts/<%=ReqCateId %>/list-us.txt";
            }

            $('#storeListStyle').val(t);
            $.ajax({
                type: "get",
                url: url,
                data: {},
                error: function (r, s, tt) { },
                success: function (m, s) {
                    var list = t == 1;
                    var result = "";

                    var cont = eval("(" + m + ")");
                    var keys = $('#keywordHidden').text().split(']');

                    var keyArray = new Array();
                    var n = 0;
                    for (var i = 0; i < keys.length; i++) {
                        if ($.trim(keys[i]) == "")
                            break;
                        keyArray[n] = $.trim(keys[i]) + "]";
                        n++;
                        //alert(keyArray[n - 1]);
                    }

                    if (list) {
                        var result = ("<ul class=\"list-group\">");
                        $.each(cont, function (index, item) {

                            var keyexist = true; // keys.length == 1 ? true : false;
                            for (var y = 0; y < keyArray.length; y++) {
                                var exist = false;
                                if (item.Keyword.indexOf(keyArray[y]) > -1)
                                    exist = true;
                                if (exist == false) {
                                    keyexist = false;
                                    break;
                                }
                            }

                            if (keyexist) {
                                var pageUrl = item.PageUrl == "" ? "detail_part.aspx?sku=" + item.SKU : "/Computer/parts_detail/" + item.PageUrl;
                                result += "<li class='list-group-item row'><a href='" + pageUrl + "'>";
                                result += "<div class='col-xs-5 col-sm-2 col-md-2'><img class=\"lazy\" src='../images/logo1.png' data-original=\"https://o9ozc36tl.qnssl.com/" + item.ImgSku + ".jpg?imageView/3/w/120/h/120\" width=\"120\" alt=\"...\"></div>";
                                result += "<div class='col-xs-5 col-sm-7 col-md-7'>";
                                result += "<h4 class='list-group-item-heading'>" + item.ShortName + "</h4>";
                                result += "<p class='list-group-item-text'>" + item.Name + "</p>";
                                result += "</div>";
                                result += "<div class='col-xs-2 col-sm-3 col-md-3'>";
                                result += "<p class='list-group-item-text'> ";
                                result += "<ul class='price_list'><li>SKU: " + item.SKU + "</li>";
                                if (item.Discount == 0) {
                                    result += "<li>Special: <span class='price'>$" + parseFloat(item.Sold).toFixed(2) + "</span><span class='price-unit'> " + item.priceUnit + "</span></li>";
                                } else {

                                    result += "<li>Save: <span class='price' style='color:blue;'>$" + parseFloat(item.Discount).toFixed(2) + "</span> <span class='price-unit'> " + item.priceUnit + "</span></li>";
                                    result += "<li>Special: <span class='price'>$" + parseFloat(item.Sold).toFixed(2) + "</span><span class='price-unit'> " + item.priceUnit + "</span></li>";
                                }
                                result += "<li>&nbsp;</li>";
                                result += "<li><a href='ShoppingCartTo.aspx?sku=" + item.SKU + "' class='btn btn-default'><span class='glyphicon glyphicon-shopping-cart'></span> Add to Shopping Cart</a></li>";
                                result += "</ul>";
                                result += "</p>";
                                result += "</div>";
                                result += ("</a></li>");
                            }
                        });
                        //result += "<li class='list-group-item row text-center active' style='color:white;'> ---- end ----</li>";
                        result += ("</ul>");
                    }
                    else {
                        // other method.

                    }
                    $('#prodListArea').html(result);
                    $("img.lazy").lazyload();
                    $('#loading').css({ display: 'none' });
                    $('#nomoreresults').removeClass('hide');
                }
            });

        }

    </script>
</asp:Content>
