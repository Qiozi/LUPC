<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="UC/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-web-home">
        <!-- Carousel
    ================================================== -->
        <div id="myCarousel" class="carousel slide" data-ride="carousel">
            <!-- Indicators -->
            <ol class="carousel-indicators">
                <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                <li data-target="#myCarousel" data-slide-to="1"></li>
            </ol>
            <div class="carousel-inner text-center" role="listbox">
                <div class="item active">

                    <a href="/detail_part.aspx?sku=30279">
                        <img src="../images-ad/m3.jpg" alt="First slide" height="500" border="0">
                    </a>

                    <div class="container">
                        <div class="carousel-caption">
                            <%--<a href="http://feedback.ebay.ca/ws/eBayISAPI.dll?ViewFeedback2&userid=dpowerseller&ftab=AllFeedback" target='_blank'><img style="border: none;" src="/images/ebay-count.png"/></a>--%>
                        </div>
                    </div>

                </div>
                <div class="item ">
                    <a href="/detail_part.aspx?sku=30619">
                        <img src="../images-ad/30619.jpg" alt="Second slide" border="0">
                    </a>
                    <div class="container">
                        <div class="carousel-caption">
                            <%--<a href="http://feedback.ebay.ca/ws/eBayISAPI.dll?ViewFeedback2&userid=dpowerseller&ftab=AllFeedback" target='_blank'>
                            <img style="border: none;" src="/images/ebay-count.png" /></a>--%>
                        </div>
                    </div>

                </div>
            </div>

            <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
                <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span><span class="sr-only">Previous</span> </a><a class="right carousel-control" href="#myCarousel" role="button"
                    data-slide="next"><span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span><span class="sr-only">Next</span> </a>
        </div>
        <!-- /.carousel -->
        <%--
<div class="bs-docs-header" style="background:url('../images-ad/ad1.jpg');height:500px; margin-top:-30px;">
    <div class="container">
        
    </div>
</div>--%>
        <div class="topButtonArea">
            <p class="text-center">
                <a class="btn btn-link" onclick="window.location.href='/OnSales.aspx';">
                    <span class="glyphicon glyphicon-star"></span>
                    On Sales
                </a>
                <b>|</b>
                <a class="btn btn-link" onclick="window.location.href='/Rebate.aspx';">
                    <span class="glyphicon glyphicon-list"></span>
                    Rebate
                </a>
                <b>|</b>
                <a class="btn btn-link" onclick="window.location.href='/bContactUs.aspx';">
                    <span class="glyphicon glyphicon-earphone"></span>
                    Contact Us
                </a>
            </p>
        </div>
        <div class="container mg-t">
            <div class="row prodListArea" id="prodListArea">
                <%= GetCateString()  %>
            </div>
        </div>
        <div class="container pd-0">
            <div class="row mg-t">
                <div class="col-md-10">
                    <%--  <div class="text-center" style="background-color: #8BD18B;">
                        <img src="images/systitle1.png" />
                    </div>--%>
                    <div class="sys-list">
                        <%= GetSysString() %>
                    </div>

                    <div class=" text-center mg-0" style="background-color: #8BD18B; padding: 10px;">
                        <img src="images/parttitle1.png" alt="" />
                    </div>
                    <div class="part-list">
                        <asp:Literal runat="server" ID="ltProdList"></asp:Literal>
                  
                    </div>
                </div>
                <div class="col-md-2">
                    <div class="" id="leftCateList" data-spy="affix" data-offset-top="860" data-offset-bottom="200">
                        <ul class="nav">
                            <li><a href='#prodListArea' data-toggle="tooltip" data-placement="left" title="ALL Category"><i class="iconfont">&#xe612;</i> System PC</a></li>
                            <asp:Literal runat="server" ID="lblNavCateName"></asp:Literal>
                            <li><a href='#myCarousel' data-toggle="tooltip" data-placement="left" title="TOP"><span
                                class="glyphicon glyphicon-menu-up"></span>TOP</a></li>
                        </ul>
                    </div>
                </div>
            </div>


        </div>

        <%--  <div id="sysList">
            <div class="container" style="background-color: #8BD18B;">


                <div class="row" style="background: white;">
                    <div class="col-md-4">
                        <asp:Literal runat="server" ID="ltSysPartLogo1"></asp:Literal>
                    </div>
                    <div class="col-md-6 ">
                        <asp:Literal runat="server" ID="ltSystemPartList1"></asp:Literal>
                    </div>
                    <div class="col-md-2 ">
                        <asp:Literal runat="server" ID="ltSystemPrice1"></asp:Literal>
                    </div>
                </div>
                <div class="row" style="background: #E8F6E8;">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
                <div class="row" style="background: white;">
                    <div class="col-md-4">
                        <asp:Literal runat="server" ID="ltSysPartLogo2"></asp:Literal>
                    </div>
                    <div class="col-md-6">
                        <asp:Literal runat="server" ID="ltSystemPartList2"></asp:Literal>
                    </div>
                    <div class="col-md-2">
                        <asp:Literal runat="server" ID="ltSystemPrice2"></asp:Literal>
                    </div>
                </div>
                <div class="row" style="background: white;">
                    <div class="col-md-12">
                        &nbsp;
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        var listCount = 0;
        function getPrice() {
            $('.itemprice').each(function () {
                var the = $(this);
                if (the.html() == "...") {
                    the.html("....");
                    $.get("/cmds/prod.aspx?cmd=getSinglePrice&sku=" + the.attr('sku'), function (data) {
                        var cont = eval("(" + data + ")");
                        the.html("$" + cont.price);
                        the.prev().html('');
                    });
                }
            });
        }

        $(document).ready(function () {
            function getCateHref(cid, isSys) {
                return isSys ? "list_sys.aspx?cid=" + cid : "list_part.aspx?cid=" + cid;
            }

            $('body').scrollspy({ target: '#leftCateList' })

            $('body').on('activate.bs.scrollspy', function () {

                //var sTop = $(window).scrollTop();
                //if (sTop > 20) {
                //    if ($('#leftCateList').hide())
                //        $('#leftCateList').show();
                //}
                //else {
                //    if ($('#leftCateList').show())
                //        $('#leftCateList').hide();
                //}
            });

            $('.catelist').each(function () {
                var the = $(this);
                $.get('/Computer/home_cate_list_detail_' + the.attr('cateid') + '.txt', function (data) {
                    the.html(data);
                    getPrice();
                    $("img.lazy").lazyload();
                    $('[data-toggle="tooltip"]').tooltip();
                    $('[data-toggle="popover"]').popover()
                });
            });
            //$('#prodListArea').parent().append("<div style=\"float:left; position: relative; top:-200px;left:40px;\">" +
            //        "<a href=\"http://feedback.ebay.ca/ws/eBayISAPI.dll?ViewFeedback2&userid=dpowerseller&ftab=AllFeedback\" target='_blank'>" +
            //        "<img style=\"border: none;\" src=\"/images/ebay-count.png\" /></a></div>");

            $('.ebaycount').css({ left: $(document).width() / 2 - 500 });

        });
    </script>
</asp:Content>
