<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="indexsys.aspx.cs" Inherits="SiteApp.indexsys" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="height: 100px"></div>
    <div class="list-group" id="prod-list-group">      
        
    </div>
    <div class="loading alert alert-danger" id="loading">Wait a moment... it's loading!</div>
    <div class="loading alert alert-warning" id="nomoreresults">Sorry, list is end.</div>

    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScript" runat="server">

<script type="text/javascript">
    var page = 0;
    $(document).ready(function () {

        function pageRun() {
            $('#prod-list-group').scrollPagination({
                'contentPage': 'cmds/getProdList.aspx', // the url you are fetching the results
                'contentData': { cmd: 'getSysList', cid: '<%= ReqCateId %>', page: page }, // these are the variables you can pass to the request, for example: children().size() to know which page you are
                'scrollTarget': $(window), // who gonna scroll? in this example, the full window
                'heightOffset': 10, // it gonna request when scroll is 10 pixels before the page ends
                'beforeLoad': function () { // before load function, you can display a preloader div
                    $('#loading').fadeIn();
                    $('#nomoreresults').fadeOut();
                },
                'afterLoad': function (elementsLoaded, isend) { // after loading content, you can use this function to animate your new elements
                    $('#loading').fadeOut();
                    if (isend) {
                        $('#nomoreresults').fadeIn();
                        $('#prod-list-group').stopScrollPagination();
                    }
                    $(elementsLoaded).fadeInWithDelay();
                    if ($('#prod-list-group').children().size() > 300) { // if more than 100 results already loaded, then stop pagination (only for testing)
                        $('#nomoreresults').fadeIn();
                        $('#prod-list-group').stopScrollPagination();

                    }
                }
            });

            // code for fade in element by element
            $.fn.fadeInWithDelay = function () {
                var delay = 0;
                return this.each(function () {
                    $(this).delay(delay).animate({ opacity: 1 }, 200);
                    delay += 100;
                });
            };
        }
        pageRun();

    });

    function toShopCart(sku) {
        $.ajax({
            type: 'get',
            url: 'cmds/setShopCart.aspx',
            data: { cmd: 'toCart', sku: sku, t: Math.random() },
            error: function (r, s, t) {

            },
            success: function (msg, s) {
                if (s == "success")
                    $('.cartBadge').text(msg);
            }
        });

    }
    </script>

</asp:Content>