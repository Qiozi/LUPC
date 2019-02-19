<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="list_sys.aspx.cs" Inherits="list_sys" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .price_list {
            list-style-type: none;
        }

            .price_list li {
                font-size: 14px;
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

        #prodListArea h4 {
            padding: 15px;
            background-color: #8BD18B;
            color: White;
        }

        .affix {
            position: fixed;
            top: 50px;
        }

        .dropdown-menu li {
            padding: 5px;
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
                                    <a id="dLabel" data-target="#" aria-haspopup="true" role="button" aria-expanded="false"
                                        href="default.aspx"><span class="glyphicon glyphicon-filter"></span>
                                        <asp:Literal runat="server" ID="ltCateNameParent"></asp:Literal>
                                        <span>&gt;</span> </a>
                                    <!--ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
                                       <asp:Literal runat="server" ID="ltCatesParent"></asp:Literal>
                                  </ul-->
                                </div>
                            </li>
                            <li>
                                <div class="dropdown">
                                    <a id="A1" data-target="#" data-toggle="dropdown" aria-haspopup="true" role="button"
                                        aria-expanded="false">
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
                            </div>
                            <div class="col-md-6 text-right">
                                <a class="btn btn-default btn-sm" onclick="showFilter($(this));"><span class="glyphicon glyphicon-chevron-up"></span></a>
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
                    <div id="prodListArea">
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script>

        var listCount = 0;

        $(document).ready(function () {

            loadList();

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

        });

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

        function loadLogoList() {
            $('.sysLogoList').each(function () {
                var the = $(this);
                if ($.trim(the.html()) == "") {
                    $.get("Computer/systems/detail/" + the.attr("sku") + ".txt"
                , {}
                , function (msg) {
                    var cont = eval("(" + msg + ")");
                    var logoString = "";
                    var partString = "<table class='table table-condensed table-striped'>";

                    $.each(cont, function (index, item) {
                        logoString += "<img class=\"lazy\" src='../images/logo1.png' data-original=\"<%= setting.ImgHost %>/pro_img/ebay_gallery/" + item.PartImgSku.toString().substr(0, 1) + "/" + item.PartImgSku + "_ebay_list_t_1.jpg\" width=\"75\" alt=\"\">";
                        partString += "<tr><td><a class='' style='color:#000;'>" + item.Comment + "</a></td>";
                        partString += "<td><a class='' style='color:#666;'>" + item.PartTitle + "</a></td></tr>";
                    });
                    partString += "</table>";
                    partString += "<div class='row'>";
                    //                    partString += " <div class='col-md-6 '>";
                    //                    if (the.parent().find('.sysPartList').eq(0).attr("discount") != "0") {
                    //                        partString += "     <span class='price'><del>" + the.parent().find('.sysPartList').eq(0).attr("price") + "</del></span>";
                    //                    }

                    //                    partString += "     <span class='priceBig'>$" + the.parent().find('.sysPartList').eq(0).attr("sold") + "</span>";
                    //                    partString += " </div>";
                    partString += "<div class='col-md-6' id='sys-price-area-" + the.attr("sku") + "'>";
                    partString += "<img src='/images/loaderc.gif'>";
                    partString += "</div>";
                    partString += " <div class='col-md-6'>";
                    partString += "     <a class='btn btn-default' href='ShoppingCartTo.aspx?sku=" + the.attr("sku") + "'><span class='glyphicon glyphicon-shopping-cart'> Buy It Now</a>";
                    partString += "     <a class='btn btn-default' href='detail_sys_customize.aspx?sku=" + the.attr("sku") + "'><span class='glyphicon glyphicon-wrench'></span> Customize It</a>";
                    partString += "     <a class='btn btn-default' href='detail_sys.aspx?sku=" + the.attr("sku") + "'><span class='glyphicon glyphicon-calendar'></span> Detail</a>";
                    partString += " </div>";
                    partString += "</div>";

                    the.html(logoString);
                    the.parent().find('.sysPartList').eq(0).html(partString);
                    $("img.lazy").lazyload();

                    $
                    // get price
                    $.get("/cmds/systemProd.aspx", { cmd: 'getSingleSysPrice', sku: the.attr("sku"), isformat: '1' }, function (msg) {
                        var cont = eval('(' + msg + ')');
                        var resultStr = "";
                        if (parseFloat(cont.Discount) != 0) {
                            resultStr += "     <span class='price'><del>" + cont.Price + "</del></span>";
                        }
                        resultStr += "     <span class='priceBig'>$" + cont.Sold + "</span><span class='price-unit'>" + cont.Unit + "</span>";
                        $('#sys-price-area-' + the.attr("sku")).html(resultStr);
                    });
                });
                }
            });
        }
        function loadList() {

            $.ajax({
                type: "get",
                url: "Computer/systems/<%=ReqCateId %>.txt",
                data: {},
                error: function (r, s, t) { },
                success: function (m, s) {

                    var result = "";

                    var cont = eval("(" + m + ")");
                    var keys = $('#keywordHidden').text().split(']');

                    var keyArray = new Array();
                    var n = 0;
                    for (var i = 0; i < keys.length; i++) {
                        if ($.trim(keys[i]) == "")
                            break;
                        keyArray[n] = $.trim(keys[i].replace("[", ""));
                        n++;
                        //alert(keyArray[n - 1]);
                    }

                    $.each(cont, function (index, item) {

                        var keyexist = true; // keys.length == 1 ? true : false;
                        for (var y = 0; y < keyArray.length; y++) {

                            var exist = false;
                            if (item.eBayTitle.indexOf(keyArray[y]) > -1)
                                exist = true;
                            if (exist == false) {
                                keyexist = false;
                                break;
                            }
                        }

                        if (keyexist && listCount <= 20) {
                            result += "<h4>[SKU.<span class='skuColor' >" + item.SysSKU + "</span>] " + item.eBayTitle + "</h4>";
                            result += "<div class=\"row\">";
                            result += "   <div class=\"col-md-3 sysLogoList\" sku='" + item.SysSKU + "'>";

                            result += "   </div>";
                            result += "   <div class=\"col-md-9 sysPartList\" price='" + item.Price + "' discount='" + item.Discount + "' sold='" + item.Sold + "'>";
                            result += "       ";
                            result += "   </div>";
                            result += "</div>";

                            listCount++;
                        }
                    });

                    if (result == "") {
                        result = "<div  class=\"alert alert-warning\" role=\"alert\">Sorry, No data.</div>";
                    }
                    else {
                        result += "<div  class=\"alert alert-success text-center\" role=\"alert\">End.</div>";
                    }

                    $('#prodListArea').html(result);

                    loadLogoList();
                }
            });
        }
    </script>
</asp:Content>
