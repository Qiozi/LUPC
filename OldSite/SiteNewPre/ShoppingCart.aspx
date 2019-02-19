<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .delete {
            display: none;
        }

        #prodListArea .row {
            border-bottom: 1px solid #f2f2f2;
        }

            #prodListArea .row > div {
                padding-top: 1em;
            }

            #prodListArea .row:hover {
                background: #f2f2f2;
            }

                #prodListArea .row:hover .delete {
                    display: inline-block;
                }

        .input-group {
            border: 0px solid red;
            padding-top: 0px;
            vertical-align: top;
        }

        #price-area table {
            width: 99%;
        }

        #price-area td {
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-home"></span><a href='default.aspx'>&nbsp;Home <span class="glyphicon glyphicon-menu-right"></span></a>Shopping Cart Contents</div>
            <div class="panel-body">
                <div id="prodListArea">Loading...</div>
                <hr size="1" />
                <h3 class="text-center">How would you like to receive your order?</h3>
                <p class="well" style="width: 400px; margin: 0 auto 10px;">
                    <a class="btn btn-default btn-block " onclick="pickUp();return false;" id="btn-pickup">I will pick up my order and pay at your store.</a>
                    <a class="btn btn-default btn-block" onclick="shipMy();return false;" id="btn-shipmy">Please ship my items to me.</a>
                </p>
                <p id="country-stat-area"></p>
                <div id="payment-area-parent" style="display: none;">
                    <p class="text-center"><span class="glyphicon glyphicon-chevron-down"></span></p>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <div id="shippingCompayListArea" class="well" style="width: 400px; margin: 0 auto;"></div>
                        </div>
                        <div class="col-md-5">
                            <div id="paymentListArea" class="well" style="width: 400px; margin: 0 auto;"></div>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                    <p class="text-center"><span class="glyphicon glyphicon-chevron-down"></span></p>
                </div>
                <p id="price-area"></p>
                <p class="well text-center">

                    <a class="btn btn-default" href="default.aspx"><span class="glyphicon glyphicon-home"></span>&nbsp;Continue Shopping</a>
                    <a class="btn btn-default disabled " id="btnCheckout">Check Out&nbsp;<span class="glyphicon glyphicon-arrow-right"></span></a>

                </p>
                <div class="note">
                    <h4 class="note">You can find out the total amount BEFORE you check out.</h4>
                    <p class="note">
                        Please select your destination state / province and shipping method below.
    Orders are processed and shipped Monday through Friday. In-stock items and special orders (when available) are usually shipped immediately. You will be notified for any items if not shipped right away. Computer systems are usually shipped in 1-7 business days. But fast shipping is not guaranteed. LU Computers is a fast shipper; we take every effort to ship your item as soon as possible.
                    </p>
                    <h4 class="note">Shopping with LU Computers is safe and secure!</h4>
                    <p class="note">
                        To protect your transaction, we use GeoTrust's service and 128-bit Secure Sockets Layer (SSL) technology, thereby offering the highest level of encryption or security possible. This means you can rest assured that communications between your browser and this site's web servers are private and secure, and your personal information is also stored securely in our server.
    LU Computers reserves the right to change above shipping fees if the actual shipping costs are significantly greater than above estimate.
                    </p>
                </div>
            </div>
        </div>
    </div>
    <div id="backupBtnList1" style="display: none;">
        <p class="text-center"><span class=" glyphicon glyphicon-chevron-down"></span></p>
        <div class="well" style="width: 400px; margin: 0 auto 10px;">

            <div class="btn-group">
                <a class="btn btn-default btn-block payBtn" onclick="payment(22, $(this));return false;">Local Pick up and <b>Cash</b> Paymenth</a>
                <a class="btn btn-default btn-block payBtn" onclick="payment(23, $(this));return false;">Local Pick up and <b>Debit Card</b> Paymenth</a>
                <a class="btn btn-default btn-block payBtn" onclick="payment(24, $(this));return false;">Local Pick up and <b>Credit Card</b> Paymenth</a>
            </div>
        </div>
    </div>
    <div id="backupBtnList2" style="display: none;">
        <p class="text-center"><span class="glyphicon glyphicon-chevron-down"></span></p>
        <div class="panel panel-default" style="width: 900px; margin: 0 auto 10px;">
            <div class="panel-heading">Please select ship to location.  <span id="currStateLocation" style="color: Green;"></span></div>
            <div class="panel-body">
                <ul class="nav nav-tabs">
                    <li role="presentation" class="active"><a href="#tabStateCanada" data-toggle="tab">Canada</a></li>
                    <li role="presentation"><a href="#tabStateUS" data-toggle="tab">United States</a></li>
                    <%--  <li role="presentation"><a href="#tabStateOther" data-toggle="tab">Other</a></li>--%>
                </ul>
                <div class="tab-content" style="border-left: 1px solid #ccc; border-right: 1px solid #ccc; border-bottom: 1px solid #ccc; padding: 1em;">
                    <div class="tab-pane active" id='tabStateCanada'></div>
                    <div class="tab-pane " id='tabStateUS'></div>
                    <div class="tab-pane " id='tabStateOther'>
                    </div>
                </div>
                <div style="display: none;" id="tabSelectedState"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        var haveProduct = false;
        var selectedBgColor = "#D1ECD1";
        var paymentJson = "";
        var shippingCompanyJson = "";
        var currCountry = "CA";
        var currPaymentID = 0;
        var currShippingID = 0;
        var currStateID = 0;
        var currIsPickup = false;

        $(document).ready(function () {

            loadList();
            $('#btnCheckout').click(function () {
                window.location.href = "ShoppingCartGo.aspx?payment=" + currPaymentID + "&shipid=" + currShippingID + "&stateid=" + currStateID;
            });
        });

        function pickUp() {
            $('#payment-area-parent').css({ display: 'none' });
            $('#btn-pickup').addClass("disabled");
            $('#btn-shipmy').removeClass("disabled");

            currStateID = 8;
            currCountry = "CA";
            currPaymentID = 0;
            currShippingID = 0;
            currIsPickup = true;

            $('#btn-pickup').removeClass("btn-default").addClass("btn-primary");
            $('#btn-shipmy').removeClass("btn-primary").addClass("btn-default");

            var pickupString = $('#country-stat-area').html();
            if ($.trim(pickupString) != "")
                $('#backupBtnList2').html(pickupString);

            pickupString = $('#backupBtnList1').html();
            $('#backupBtnList1').html('');

            $('#country-stat-area').html(pickupString);

            getPriceArea();
        }

        function selectPriceArea(left) {
            if (left) {
                $('#panelPrice1').addClass("panel-success");
                $('#panelPrice2').removeClass("panel-success");
            }
            else {
                $('#panelPrice2').addClass("panel-success");
                $('#panelPrice1').removeClass("panel-success");
            }
        }

        function payment(id, the) {
            currPaymentID = id;
            if (id == 22) {
                currStateID = 8;
                selectPriceArea(true);
            }
            else if (id == 23) {
                currStateID = 8;
                selectPriceArea(false);
            }
            else if (id == 24) {
                currStateID = 8;
                selectPriceArea(false);
            }
            $('#btnCheckout').removeClass("disabled").addClass("btn-success");

            $('.payBtn').each(function () {
                // $(this).css({ background: "#ffffff"});
                $(this).removeClass("btn-success").removeClass("disabled");
            });
            //the.css({ background: selectedBgColor });
            the.addClass("btn-success").addClass("disabled");
            showCheckOutBtn();
            getPriceArea();
        }

        function getPriceArea() {

            if (currPaymentID > 0 && currStateID > 0)
                $.get("cmds/orders.aspx", { cmd: 'getPriceListForCart', stateid: currStateID, shippingid: currShippingID }, function (msg) {
                    $('#price-area').html(msg).css({ display: '' });
                });
            else
                $('#price-area').css({ display: 'none' });
        }

        // show Checkout Button
        function showCheckOutBtn() {

            if (haveProduct) {
                if (currIsPickup) {
                    if (currPaymentID > 0)
                        $('#btnCheckout').removeClass("disabled").addClass("btn-success");
                    else
                        $('#btnCheckout').removeClass("btn-success").addClass("disabled");
                }
                else {
                    if (currShippingID > 0 && currPaymentID > 0)
                        $('#btnCheckout').removeClass("disabled").addClass("btn-success");
                    else
                        $('#btnCheckout').removeClass("btn-success").addClass("disabled");
                }
            }
            else {
                $('#btnCheckout').removeClass("btn-success").addClass("disabled");
            }
        }

        // ship it to my
        function shipMy() {

            $('#btn-shipmy').addClass("disabled");
            $('#btn-pickup').removeClass("disabled");


            var areaString = $('#country-stat-area').html(); // $('#backupBtnList1').html();
            if ($.trim(areaString) != "")
                $('#backupBtnList1').html(areaString);

            areaString = $('#backupBtnList2').html();
            $('#backupBtnList2').html('');

            $('#country-stat-area').html(areaString);
            currStateID = 8;
            currCountry = "CA";
            currPaymentID = 0;
            currShippingID = 0;
            currIsPickup = false;
            $('#price-area').css({ display: 'none' });

            $('#btn-pickup').removeClass("btn-primary").addClass("btn-default");
            $('#btn-shipmy').removeClass("btn-default").addClass("btn-primary");

            showCheckOutBtn();

            if ($.trim($('#tabStateCanada').html()).length == 0) {
                $.get("cmds/state.aspx", { cmd: "getStates", cc: "CA" }, function (msg) {
                    var result = "<div class=\"btn-group\" role=\"group\" aria-label=\"...\">";
                    var cont = eval("(" + msg + ")");
                    $.each(cont, function (index, item) {
                        result += "<a class='btn btn-default btn-state' country='" + item.Country + "' state='" + item.ID + "' style='width:168px;' onclick='btnSelectState($(this));return false;'>" + item.Name + "</a>";
                    });
                    result += "</div>";

                    $('#tabStateCanada').html(result);
                });
            }
            else {
                $('.btn-state').removeClass("btn-success");
            }
            if ($.trim($('#tabStateUS').html()).length == 0) {
                $.get("cmds/state.aspx", { cmd: "getStates", cc: "US" }, function (msg) {
                    var result = "<div class=\"btn-group\" role=\"group\" aria-label=\"...\">";
                    var cont = eval("(" + msg + ")");
                    $.each(cont, function (index, item) {
                        result += "<a class='btn btn-default btn-state' country='" + item.Country + "' state='" + item.ID + "' style='width:160px;' onclick='btnSelectState($(this));return false;'>" + item.Name + "</a>";
                    });
                    result += "</div>";

                    $('#tabStateUS').html(result);
                });
            }
            else {
                $('.btn-state').removeClass("btn-success");
            }

            getPriceArea();

            $('#price-area').css({ display: 'none' });
        }



        function loadPriceArea() {
            $.get("cmds/orders.aspx", { cmd: 'getPriceArea', t: Math.random() },
            function (msg) {
                $(this).html(msg);
            });
        }

        function btnSelectState(the) {
            var country = the.attr('country');
            // country
            currCountry = country == "Canada" ? "CA" : (country == "United States" ? "US" : "Other");

            currStateID = the.attr('state');

            $('.btn-state').each(function () {
                $(this).removeClass("btn-success").removeClass("disabled");
            });
            the.addClass("btn-success").addClass("disabled");

            $('#currStateLocation').html(country + " <span class='glyphicon glyphicon-menu-right'></span> " + the.text());



            if (shippingCompanyJson == "") {
                $.get("cmds/Shopping.aspx", { cmd: "getShippingCompanyALL" }, function (msg) {
                    shippingCompanyJson = msg;
                    showPayment();

                });
            }
            else {
                showPayment();
            }

            if (paymentJson == "") {
                $.get("cmds/Shopping.aspx", { cmd: "getpaymentAll" }, function (msg) {
                    paymentJson = msg;
                    showPayment();

                });
            }
            else
                showPayment();

            $('#price-area').css({ display: 'none' });
        }

        function showPayment() {
            $('#payment-area-parent').css({ display: '' });
            if (paymentJson != "") {
                var cont = eval("(" + paymentJson + ")");
                var result = "<div class=\"btn-group\" role=\"group\" aria-label=\"...\">";
                $.each(cont, function (index, item) {
                    if (item.SupperCountry.indexOf(currCountry) > -1)
                        result += "<a class='btn btn-default payBtn' onclick='payment(" + item.ID + ", $(this));return false;' style='width:100%;text-align:left;'>" + item.Name + "</a>";
                });
                result += "</div>"
                $('#paymentListArea').html(result);

            }
            if (shippingCompanyJson != "") {
                var cont = eval("(" + shippingCompanyJson + ")");
                var result = "<div class=\"btn-group\" role=\"group\" aria-label=\"...\">";
                $.each(cont, function (index, item) {
                    if (item.Country.indexOf(currCountry) > -1)
                        result += "<a class='btn btn-default shippingCharge' sid='" + item.ID + "' onclick='selectedShipping($(this));return false;' style=' width:100%; text-align:left;'>" + item.Name + " <span class=\"badge\">...</span></a>";
                });
                result += "</div>"
                $('#shippingCompayListArea').html(result);
                getShippingCharge();
            }
        }

        function selectedShipping(the) {
            currShippingID = the.attr('sid');
            $('.shippingCharge').each(function () {
                $(this).removeClass("btn-success").removeClass("disabled");
            });
            the.addClass("btn-success").addClass("disabled");
            showCheckOutBtn();
            getPriceArea();
        }

        function getShippingCharge() {
            $('.shippingCharge').each(function () {
                var the = $(this);
                var shippingid = the.attr('sid');
                $.get("cmds/shopping.aspx", { cmd: 'getShippingCharge', payment: currPaymentID, stateid: currStateID, shippingid: shippingid, t: Math.random() }, function (data) {
                    the.find('span').eq(0).html("$" + data);
                });
            });
        }

        function del(id) {
            $.ajax({
                type: "get",
                url: "cmds/Shopping.aspx",
                data: { cmd: 'del', id: id },
                error: function (g, s, t) {

                },
                success: function (m, s) {
                    loadList();
                }
            });
        }

        function addQty(the, sku, isAdd) {
            var qty = isAdd ? the.parent().prev().val() : the.parent().next().val();

            qty = isAdd ? parseInt(qty) + 1 : parseInt(qty) - 1;

            if (qty > 20)
                qty = 20;
            if (isNaN(qty))
                qty = 1;

            the.parent().prev().val(qty);

            $.ajax({
                type: "get",
                url: "cmds/Shopping.aspx",
                data: { cmd: 'ChangeQty', id: sku, qty: qty },
                error: function (g, s, t) {

                },
                success: function (m, s) {
                    loadList();
                }
            });
            return false;
        }

        function loadList() {
            $.ajax({
                type: 'get',
                url: 'cmds/Shopping.aspx',
                data: { cmd: 'getShoppingList' },
                error: function (g, s, t) {

                },
                success: function (m, s) {
                    var list = eval('(' + m + ")");
                    var result = "";
                    var priceUnit = "";
                    // alert(list.total);
                    haveProduct = false; // no product and no show checkout button.
                    $.each(list.row, function (index, item) {
                        result += "<div class='row'>"
                        result += "<div class='col-md-2 text-center'>";
                        result += "<img class=\"lazy\" src='../images/logo1.png' data-original=\"" + item.ImgUrl + "\" width=\"100\" alt=\"...\">";
                        result += "<p class='text-center'>[SKU: " + (item.SKU) + "]</p>";
                        result += "</div>";
                        result += "<div class='col-md-4'>";
                        result += "" + (item.Title);
                        result += "</div>";
                        result += "<div class='col-md-2'>";
                        result += "" + (item.SoldString);
                        result += "</div>";
                        result += "<div class='col-md-2'>";
                        result += " <div class=\"input-group input-group-sm\">";
                        result += "     <span class=\"input-group-btn\"><a class='btn btn-default' onclick=\"addQty($(this),'" + item.ID + "', false);return false;\"><span class='glyphicon glyphicon-minus'></span></a></span>";
                        result += "         <input type='text' class=\"form-control text-center\" value='" + (item.Qty) + "' size=1 disabled/>";
                        result += "     <span class=\"input-group-btn\"><a class='btn btn-default' onclick=\"addQty($(this),'" + item.ID + "', true);return false;\"><span class='glyphicon glyphicon-plus'></span></a></span>";
                        result += " </div>";
                        result += "</div>";
                        result += "<div class='col-md-1'>";
                        result += "<span class='price'>$" + (item.SubSoldString) + "</span>";
                        result += "</div>";
                        result += "<div class='col-md-1'>";
                        result += "<a class='btn btn-default delete' title='Delete' onclick=\"del('" + item.ID + "');\"><span class='glyphicon glyphicon-trash'></span></a>";
                        result += "</div>";
                        result += "</div>";

                        priceUnit = item.PriceUnit;
                        haveProduct = true;
                    });
                    result += "<p><h4 class='text-right '>Sub Total: <span class='priceBig'>$" + list.total + "</span> <small>" + priceUnit + "</small></h4></p>";
                    $('#prodListArea').html(result);

                    $("img.lazy").lazyload();

                    getPriceArea();
                    showCheckOutBtn();
                }
            });
        }
    </script>
</asp:Content>

