var haveProduct = false;
var selectedBgColor = "#D1ECD1";
var paymentJson = "";
var shippingCompanyJson = "";
var currCountry = "CA";
var currPaymentID = 0;
var currShippingID = 0;
var currStateID = 0;
var currIsPickup = false;

$(function () {
    loadList();
    $('#btnCheckout').on('click', function () {
        //alert(currPaymentID)
        window.location.href = "/ShoppingCartGo.aspx?payment=" + currPaymentID + "&shipid=" + currShippingID + "&stateid=" + currStateID;
    });
    $('#btnCheckoutPaypal').on('click', function () {
        //alert(currPaymentID)
        window.location.href = "/ShoppingCartGo.aspx?payment=" + currPaymentID + "&shipid=" + currShippingID + "&stateid=" + currStateID;
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


    //alert(currPaymentID)
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

            // modify font color
            try {
                if (currPaymentID == 22 ) {
                    $('#pay_21').css({ 'color': 'blue', 'font-weight': 'bold' });
                }
                if (currPaymentID == 23 || currPaymentID == 24) {
                    $('#pay_25').css({ 'color': 'blue', 'font-weight': 'bold' });
                }
                else {
                    $('#pay_' + currPaymentID).css({ 'color': 'blue', 'font-weight': 'bold' });
                }
            } catch (e) { console.log(e); }

        });
    else {
        $('#price-area').css({ display: 'none' });
    }
}

// show Checkout Button
function showCheckOutBtn() {
    if (haveProduct) {
        $('.nextBtnAreaPaypal').addClass("hidden");
        if (currIsPickup) {
            if (currPaymentID > 0)
                $('#btnCheckout').removeClass("disabled").addClass("btn-success");
            else
                $('#btnCheckout').removeClass("btn-success").addClass("disabled");
        }
        else if (parseInt(currPaymentID) == 15) {
            $('.nextBtnAreaPaypal').removeClass("hidden");
            $('#btnCheckout').removeClass("btn-success").addClass("disabled");
            console.log(15);
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
            setTimeout(function () {

                util.getShippingQty($('.cart-badge'));
            }, 500);
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
                
                if (('' + item.SKU).length == 8) {
                    result += "<div class='col-md-4'><a href='detail_sys.aspx?sku=" + item.SKU+"'>";
                }
                else {
                    result += "<div class='col-md-4'><a href='detail_part.aspx?sku=" + item.SKU + "'>";
                }
                result += "" + (item.Title);
                result += "</a></div>";
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


            if (!list.row.length) {
                $('#btn-pickup').addClass('disabled');
                $('#btn-shipmy').addClass('disabled');
            }
        }
    });
}