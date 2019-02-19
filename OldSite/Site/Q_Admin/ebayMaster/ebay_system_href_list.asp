<%@  language="VBSCRIPT" codepage="65001" %>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<% response.clear() %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Ebay System Href</title>
    <script type="text/javascript" src="../../js_css/jquery_lab/jquery-1.3.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="../../js_css/b_lu.css" />
    <script src="/Q_admin/JS/helper.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/winOpen.js"></script>
    <style>
        .div_top div {
            float: left;
            background: #ccc;
            line-height: 20px;
        }
    </style>
    <script type="text/javascript">

        // array
        var InfoArray = null;
        //    for (var i = 0; i < 1000; i++) {
        //        InfoArray[i] = new Array();
        //        for (var j = 0; j < 5; j++)
        //            InfoArray[i][j] = "";
        //    }

        function loadData() {
            if (InfoArray == null) {
                InfoArray = new Array();

                $('tr[name=itemdata]').each(function (e) {
                    var the = $(this);
                    InfoArray[e] = new Array();
                    InfoArray[e][0] = the.attr("adjustment");
                    InfoArray[e][1] = the.attr("buyitnowprice");
                    InfoArray[e][2] = the.attr("rid");
                    InfoArray[e][3] = the.attr("itemid");
                    InfoArray[e][4] = the.attr("is_shrink");
                });
            }
        }


        function GetEbayPrice(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {
            $('#modifyEbayPrice' + luc_sku).html("<a id='modifyEbayPriceHref" + luc_sku + "' href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + Cost + "&Profit=" + Profit + "&eBayFee=" + eBayFee + "&ShippingFee=" + ShippingFee + "&Price=" + price + "&IsDesc=0&onlyprice=1&itemid=" + itemid + "&issystem=1' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200); $(this).css({'color':'white', 'background':'black'});}return false;\">(" + Profit + ")eBay Price </a>");
        }
        function GetEbayDesc(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {
            $('#modifyEbayDesc' + luc_sku).html("<a id='modifyEbayDescHref" + luc_sku + "' href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + Cost + "&Profit=" + Profit + "&eBayFee=" + eBayFee + "&ShippingFee=" + ShippingFee + "&Price=" + price + "&IsDesc=1&onlyprice=0&itemid=" + itemid + "&issystem=1' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200);  $(this).css({'color':'white', 'background':'black'});}return false;\">eBay Desc </a>");
        }

        function WarnShow(w, the) {
            if (w == 'True') {
                the.html("<img src='/soft_img/tags/(38,41).png' title='part disabled'>");
            }

        }

        function Sleep(obj, iMinSecond) {
            if (window.eventList == null)
                window.eventList = new Array();
            var ind = -1;
            for (var i = 0; i < window.eventList.length; i++) {
                if (window.eventList[i] == null) {
                    window.eventList[i] = obj;
                    ind = i;
                    break;
                }
            }
            if (ind == -1) {
                ind = window.eventList.length;
                window.eventList[ind] = obj;
            }
            setTimeout("GoOn(" + ind + ")", iMinSecond);
        }

        function GoOn(ind) {
            var obj = window.eventList[ind];
            window.eventList[ind] = null;
            if (obj.NextStep) obj.NextStep();
            else obj();
        }

        // array 
        //    var InfoArray = new Array();
        //    for (var i = 0; i < 500; i++) {
        //        InfoArray[i] = new Array();
        //    }

        //    function Amount(cost, adjustment, buyItNowPrice, the, the2, itemid, luc_sku, is_shrink) {
        // InfoArray["& JsArrayIndex &"][0] = '"& srs("adjustment") &"'; "&vblf
        //s =     s & "       InfoArray["& JsArrayIndex &"][1] = '"& sRs("BuyItNowPrice") &"'; "&vblf
        //s =     s & "       InfoArray["& JsArrayIndex &"][2] = '"& sRs("ID") &"'; " &vblf 
        //s =     s & "       InfoArray["& JsArrayIndex &"][3] = '"& sRS("ItemID") &"'; "&vblf 
        //s =     s & "       InfoArray["& JsArrayIndex &"][4] = '"& is_shrink & "'; "&vblf

        function Warn(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;


            var luc_sku = InfoArray[index][2];


            var the = $('#newEbayPrice' + luc_sku);
            var the2 = $('#newDiff' + luc_sku);

            the.html("<img src='/soft_img/tags/loading.gif'/>");

            Sleep(this, 50);
            this.NextStep = function () {
                // alert(luc_sku);
                $.ajax({
                    type: "Get",
                    url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                    data: "cmd=GetEBaySysWarn&systemsku=" + luc_sku + "&" + rnd(),
                    success: function (msg) {
                        //                   the.html(msg);
                        //                   var diff = (parseFloat(msg) - parseFloat(buyItNowPrice)).toFixed(2);
                        //                   
                        //                   if(diff>0)
                        //                        the2.css('color','green');
                        //                   else 
                        //                        the2.css('color','red');
                        //                   the2.html(diff);

                        var json = eval(msg);
                        $.each(json, function (idx, item) {
                            WarnShow(item.warn, $('#warn' + luc_sku));
                        });

                        Warn(index + 1);
                    }
                    , error: function (msg) { Warn(index + 1); the2.html(msg); }
                });
                the.html(".");
            }
        }

        function Amount(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;

            var adjustment = InfoArray[index][0];

            var buyItNowPrice = InfoArray[index][1];
            var luc_sku = InfoArray[index][2];
            var itemid = InfoArray[index][3];
            var is_shrink = InfoArray[index][4];
            var the = $('#newEbayPrice' + luc_sku);
            var the2 = $('#newDiff' + luc_sku);

            the.html("<img src='/soft_img/tags/loading.gif'/>");

            Sleep(this, 50);
            this.NextStep = function () {
                // alert(luc_sku);
                $.ajax({
                    type: "Get",
                    url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                    data: "cmd=GetSysEbayPriceBySysCost&is_shrink=" + is_shrink + "&Adjustment=" + adjustment + "&systemsku=" + luc_sku + "&" + rnd(),
                    success: function (msg) {
                        //                   the.html(msg);
                        //                   var diff = (parseFloat(msg) - parseFloat(buyItNowPrice)).toFixed(2);
                        //                   
                        //                   if(diff>0)
                        //                        the2.css('color','green');
                        //                   else 
                        //                        the2.css('color','red');
                        //                   the2.html(diff);

                        var json = eval(msg);
                        $.each(json, function (idx, item) {
                            the2.html(msg);
                            var newPrice = parseFloat(item.ebayPrice);
                            var diff = (newPrice - buyItNowPrice).toFixed(2);
                            the.html(newPrice);
                            if (diff > 0)
                                the2.css('color', 'red');
                            else
                                the2.css('color', 'green');
                            the2.html(diff);
                            WarnShow(item.warn, $('#warn' + luc_sku));
                            $('#cost_' + luc_sku).html(item.cost);
                            GetEbayPrice(newPrice, itemid, luc_sku, item.cost, item.profit, item.ebay_fee, item.shipping_fee)
                            GetEbayDesc(newPrice, itemid, luc_sku, item.cost, item.profit, item.ebay_fee, item.shipping_fee)

                            var autoPayHref = $('#a' + itemid).attr('href');
                            $('#a' + itemid).attr('href', autoPayHref + "&price=" + newPrice);
                        });

                        //getItemCate(luc_sku, itemid, the2);

                        Amount(index + 1);
                    }
                    , error: function (msg) { Amount(index + 1); the2.html(msg); }
                });
            }
        }

        function getItemCate(sku, itemid, the) {
            $.ajax({
                type: "get",
                url: "ebay_cmd.aspx?cmd=getEbayItemInfo&sku=" + sku + "&itemid=" + itemid,
                data: "",
                error: function (r, t, s) {
                    the2.html(msg);
                },
                success: function (msg) {
                    the.parent().find('.noteText').eq(0).text(msg);
                }
            });
        }

        function ChangePrice(index) {
            loadData();
            if (InfoArray[index] == undefined)
                return;
            if (InfoArray[index][2] == "")
                return;

            var luc_sku = InfoArray[index][2];

            var the2 = $('#newDiff' + luc_sku);
            var diff = parseInt(the2.html());

            var href = $('#modifyEbayPriceHref' + luc_sku).attr("href");

            if (href.indexOf("&") == -1)
                return;

            if (diff < 3 && diff > -3) {
                ChangePrice(index + 1);
                return;
            }


            $.ajax({
                type: "Get",
                url: href,
                success: function (msg) {

                    ChangePrice(index + 1);
                    the2.css({ 'color': 'white', 'background': 'black' })
                }
                , error: function (msg) { ChangePrice(index + 1); the2.html(msg); }
            });

        }

        function ChangeLogo(index) {
            loadData();

            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;
            var luc_sku = InfoArray[index][2];

            var the2 = $('#modifyLogo' + luc_sku);
            the2.css("color", "red");
            var href = $('#modifyLogo' + luc_sku).attr("href");
            $.ajax({
                type: "Get",
                url: href,
                success: function (msg) {

                    ChangeLogo(index + 1);
                    the2.css({ 'color': 'white', 'background': 'black' })
                }
                , error: function (msg) { ChangeLogo(index + 1); the2.html(msg); }
            });
        }

        function ChangeDescSingle(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;
            var luc_sku = InfoArray[index][2];

            var the2 = $('#modifyEbayDesc' + luc_sku);
            var diff = parseInt(the2.html());


            the2.css({ 'color': 'white', 'background': 'red' })
            var href = $('#modifyEbayDescHref' + luc_sku).attr("href");
            $.ajax({
                type: "Get",
                url: href,
                success: function (msg) {

                    the2.css({ 'color': 'white', 'background': 'black' })
                }
                , error: function (msg) { the2.html(msg); }
            });
        }

        function ChangeDesc(index, isBarebone) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;
            var luc_sku = InfoArray[index][2];

            var the2 = $('#modifyEbayDesc' + luc_sku);
            var diff = parseInt(the2.html());

            if (isBarebone) {
                if ($('#barebone_' + luc_sku).html().indexOf("B") == -1) {
                    ChangeDesc(index + 1, isBarebone);
                    return;
                }
            }
            the2.css({ 'color': 'white', 'background': 'red' });
            var href = $('#modifyEbayDescHref' + luc_sku).attr("href");
            $.ajax({
                type: "Get",
                url: href,
                success: function (msg) {
                    ChangeDesc(index + 1, isBarebone);
                    the2.css({ 'color': 'white', 'background': 'black' })
                }
                , error: function (msg) { ChangeDesc(index + 1, isBarebone); the2.html(msg); }
            });
        }

        function ChangeShipping(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;
            var luc_sku = InfoArray[index][2];

            var the = $('#modifyShipping_' + luc_sku);
            var href = the.attr("href");
            the.css("color", "red");
            $.ajax({
                type: "Get",
                url: href,
                success: function (msg) {
                    the.css("color", "green");
                    ChangeShipping(index + 1);
                }
                , error: function (msg) { the.html(msg); the.css("backgroup", "green"); ChangeShipping(index + 1); }
            });
        }

        function GenerateXmlFile(index) {

            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;

            if (index == 0) {
                $.get("/q_admin/ebaymaster/Online/get_system_configuration.aspx", { cmd: 'delall' }, function () { });
            }


            var adjustment = InfoArray[index][0];

            var buyItNowPrice = InfoArray[index][1];
            var luc_sku = InfoArray[index][2];
            var itemid = InfoArray[index][3];
            var is_shrink = InfoArray[index][4];
            /*
            if (parseInt(luc_sku) < 202000) {
            GenerateXmlFile(index + 1);
            return;
            } */

            if (typeof (luc_sku) == "undefined") {
                return;
            }

            var the = $('#generateXmlFile' + luc_sku);
            the.html("<img src='/soft_img/tags/loading.gif'/>");

            $.ajax({
                type: "Get",
                url: "/q_admin/ebaymaster/Online/get_system_configuration.aspx",
                data: "cmd=GenerateXmlFile&Version=3&system_sku=" + luc_sku + "&" + rnd(),
                success: function (msg) {
                    the.html("OK");
                    GenerateXmlFile(index + 1);
                }
                , error: function (msg) { GenerateXmlFile(index + 1); the.html(msg); }
            });

        }

        function validPricture(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;

            var sku = InfoArray[index][2];
            var itemid = InfoArray[index][3];
            var the = null;
            $('span[name=prictureHref]').each(function () {
                if ($(this).attr("sku") == sku) {
                    the = $(this);

                }
            });

            $.ajax({
                type: "Get",
                url: "/q_admin/ebaymaster/Online/ValideBaySysLogo.aspx",
                data: "cmd=qiozi@msn.com&sku=" + sku + "&itemid=" + itemid + "&" + rnd(),
                success: function (msg) {
                    if (msg == "1") {
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:green;'>OK</span>");
                    }
                    else
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:red;'>No</span>");

                    validPricture(index + 1);
                }
                , error: function (msg) { the.html(msg); }
            });

        }

        function WatchSysStoreCategory(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;

            var sku = InfoArray[index][2];
            var itemid = InfoArray[index][3];
            var the = null;
            $('span[name=prictureHref]').each(function () {
                if ($(this).attr("sku") == sku) {
                    the = $(this);

                }
            });

            $.ajax({
                type: "Get",
                url: "/q_admin/ebaymaster/Online/GetItemStoreCategory.aspx",
                data: "cmd=qiozi@msn.com&sku=" + sku + "&itemid=" + itemid + "&" + rnd(),
                success: function (msg) {
                    if (msg == "1") {
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:green;'>OK</span>");
                    }
                    else
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:red;'>No</span>");

                    WatchSysStoreCategory(index + 1);
                }
                , error: function (msg) { the.html(msg); }
            });
        }

        function findSysByPartSku(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;

            var sku = InfoArray[index][2];
            $('span[name=prictureHref][sku=' + sku + ']').html("...");
            $.ajax({
                type: "Get",
                url: "ebay_system_cmd.aspx",
                data: "cmd=SysFindPart&systemsku=" + sku + "&oldPartSku=" + $("#oldPartSku").val(),
                success: function (msg) {

                    if (msg == "1")
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:red;'>OK</span>");
                    else
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:green;'>N</span>");
                    findSysByPartSku(index + 1);
                },
                error: function (msg) {


                    $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:blue;'>error</span>");
                    findSysByPartSku(index + 1);
                }
            });
        }
        function findSysByPartSkuAndReplace(index) {
            loadData();
            if (InfoArray[index][2] == "" || InfoArray[index][2] == undefined)
                return;

            var sku = InfoArray[index][2];
            $('span[name=prictureHref][sku=' + sku + ']').html("...");
            $.ajax({
                type: "Get",
                url: "ebay_system_cmd.aspx",
                data: "cmd=SysReplacePart&systemsku=" + sku + "&oldPartSku=" + $("#oldPartSku").val() + "&newPartSku=" + $("#newPartSku").val(),
                success: function (msg) {

                    if (msg == "1") {
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:red;'>OK</span>");
                        ChangeDescSingle(index);

                    } else if (msg == "4") {
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:red;'>error..</span>");
                    }
                    else
                        $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:green;'>N</span>");
                    findSysByPartSkuAndReplace(index + 1);
                },
                error: function (msg) {


                    $('span[name=prictureHref][sku=' + sku + ']').html("<span style='color:blue;'>error</span>");
                    findSysByPartSkuAndReplace(index + 1);
                }
            });
        }
        $(document).ready(function () {
            $('#btnFindPart').bind("click", function () {

                findSysByPartSku(0);
            });
            $('#btnReplacePart').bind("click", function () {

                findSysByPartSkuAndReplace(0);
            });
        });

        function uploadAutoPayToEbay(index) {
            loadData();
            $('a[title=modify auto pay]').each(function (i) {
                if (index > i)
                    return;
                if (index == i) {
                    var the = $(this);
                    the.css({ "color": "white", "background": "green" });

                    $.ajax({
                        type: "get",
                        url: the.attr("href"),
                        data: "",
                        success: function (msg) {
                            the.css({ "color": "white", "background": "#000000" });

                            uploadAutoPayToEbay(index + 1);
                        },
                        error: function (msg) {
                            the.html(sku);
                            the.css({ "color": "white", "background": "red" });
                            uploadAutoPayToEbay(index + 1);
                        }
                    });
                }
            });
        }

        function closeFlash(index) {
            loadData();
            $('a[title=modify auto pay]').each(function (i) {
                if (index > i)
                    return;
                if (index == i) {
                    var the = $(this);
                    the.css({ "color": "white", "background": "green" });

                    $.ajax({
                        type: "get",
                        url: the.attr("href"),
                        data: "",
                        success: function (msg) {
                            the.css({ "color": "white", "background": "#000000" });
                            closeFlash(index + 1);
                        },
                        error: function (msg) {
                            the.html(sku);
                            the.css({ "color": "white", "background": "red" });
                            closeFlash(index + 1);
                        }
                    });
                }
            });
        }

        /**
        *   
        **/
        function generateSysWarnInfo() {
            $.ajax({
                type: "Get",
                url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                data: "cmd=GenerateSysWarnInfo&" + rnd(),
                success: function (msg) {
                    alert(msg);
                },
                error: function (msg) { alert(msg); }
            });
        }

        /**
         * 获取警告信息
         */
        function generateSysWarnInfoByDesc(index) {

            $('tr').each(function (index, item) {
                var _this = $(this);
                _this.css({ 'background': '#f2f2f2' });
                var sid = $(this).attr('rid');
                if (parseInt(sid) > 0) {

                    $.ajax({
                        type: "Get",
                        url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                        data: "cmd=generateSysWarnInfoByDesc&luc_sku=" + sid + "&" + rnd(),
                        success: function (msg) {
                            if (msg.indexOf("OK") > -1)
                                _this.css({ 'background': 'green' });
                            else {
                                _this.css({ 'background': '#ccccff' });
                            }
                        },
                        error: function (msg) { _this.css({ 'background': '#ccccff' }); }
                    });
                }
            });
        }

    </script>
</head>

<body>
    <div style="border-bottom: 1px solid #cccccc; padding: 5px; background: #cccccc; height: 20px; display: block;" class="div_top">
        <div>
            Sort: 
    <input type="radio" name="sort" value="comment" onclick="SubmitRadio();" />Comment<input type="radio" name="sort" value="sku" onclick="    SubmitRadio();" />SKU
        </div>
        <div style="margin-left: 5em;">

            <!-- <input type="button" name="s" value="Upload Excel For Show" onclick="return js_callpage_cus('ebay_system_updata_excel.aspx', 'ebay_sys_excel', 700, 550);" /> 
    
            <input type="button" name="s" value="Upload Excel For New" onclick="return js_callpage_cus('ebay_system_updata_excel_create.aspx', 'ebay_sys_excel', 700, 550);" /> 
    -->
        </div>
        <div style="margin-left: 5em;">
            当前已用最大SKU：
            <% set rs = conn.execute("Select max(id) from tb_ebay_system")
				if not rs.eof then
					response.write rs(0)
				end if
				rs.close : set rs = nothing
            %>
        </div>
        <div style="padding-left: 10px;">
            <input type="button" value="Generate New BuyItNewPrice" onclick="Amount(0);" />
            <input type="button" value="Generate eBay System Xml Configuration File" onclick="if (confirm('are you sure!')) GenerateXmlFile(0);" />
            <input type="button" value="Generate Warn" onclick="Warn(0);" />
            <input type="button" value="Change Price" onclick="ChangePrice(0);" />
            <input type="button" value="Change All Desc" onclick="ChangeDesc(0, false);" />
            <input type="button" value="Change Barebone Desc" onclick="ChangeDesc(0, true);" />
            <input type="button" value="Change eBay Logo" onclick="ChangeLogo(0);" />
            <input type="button" value="Chagne Shipping " onclick="ChangeShipping(0);" />
            <input type="button" value="Valid Logo Pricture" onclick="validPricture(0);" />
            <input type="button" value="Watch Sys Store Category" onclick="WatchSysStoreCategory(0);" />
            <input type="button" onclick="uploadAutoPayToEbay(0);" value="upload Auto Pay to eBay" />
            <input type="button" onclick="closeFlash(0);" value="Close Flash" />
            <input type="button" value="Generate Warn Sys Info" onclick="generateSysWarnInfo();" />
            <input type="button" value="Show Warn Sys Info" onclick="window.location.href = '/q_admin/ebayMaster/ebay_system_href_list.asp?isShowWarnSys=1';" />
            <input type="button" value="Show ALL" onclick="window.location.href = '/q_admin/ebayMaster/ebay_system_href_list.asp';" />
            <input type="button" value="Show Today" onclick="window.location.href = '/q_admin/ebayMaster/ebay_system_href_list.asp?isShowWarnSys=2';" />
            <input type="button" value="Generate Warn Sys Desc" onclick="generateSysWarnInfoByDesc(0);" />
        </div>

    </div>
    <div style='padding: 1em; clear: both;'>
        old part sku:<input type="text" id="oldPartSku" />
        new part sku:<input type="text" id="newPartSku" />
        <input type="button" id='btnFindPart' value="Find" />
        <input type="button" id='btnReplacePart' value="Replace" />
    </div>
    <div style='padding: 1em; clear: both; background: #FFF1E8; border: 1px solid #FF6600'>
        更新系统时，请先下载所有订单，以防数据不同步。
    </div>

    <br style="clear: both;" />
    <%
'response.End()
	Dim cmdSort	:	cmdSort = SQLescape(request("sort"))
	dim sqlSort : 	sqlSort = ""
	Dim SubSku  :   SubSku  = SQLescape(request("SubSku"))
    Dim isShowWarnSys : isShowWarnSys = SQLescape(request("isShowWarnSys"))
    Dim is_shrink
    Dim JsArrayIndex : JsArrayIndex = 0
    Dim QuantityAvailable
	
	if(cmdSort = "sku") then
		sqlSort = " order by id desc "
	else
		sqlSort = " order by cutom_label, id asc"
	end if
	
    set Rs = Conn.execute("select category_id, category_name from tb_product_category_new where parent_category_id=0 and showit=1")
    If Not Rs.eof Then
            s   = " <ul id=""browser"" class=""filetree"">"  &vblf
            Do While Not Rs.eof 
                        s   = s & "<li><span class=""folder"" id=""treeview_category_id_"& rs("category_id") &""">"& rs("category_name")& "<ul>" &vblf
                        set cRs = conn.execute ( "select category_id, category_name from tb_product_category_new where parent_category_id='"& rs("category_id") &"' and showit=1 order by category_name asc")
                        if not cRs.eof then
                                Do While not cRs.eof
                                        s   =      s & "<li><span class=""folder"" id=""treeview_category_id_"& cRs("category_id") &""" style='font-weight:600;'>"& crs("category_name") &"</span>" & vblf
                                        s   =      s & "&nbsp;&nbsp; <a  href='' onclick=""ChangeDesc("& JsArrayIndex &", false);return false;"">change desc</a>"&vblf
                                        s   =       s & "       <ul id=""subList"&cRs("category_id")&""">"


                                        s   =       s & "       </ul> "&vblf &_
                                                            "   </li>"
                                cRs.movenext
                                loop 
                        end if
                        cRs.close : set cRs = nothing
                        s   =       s &"</ul></li>"
            Rs.movenext
            Loop
            s   =    s &                      "</ul>"
    End If


	response.write s
    ' Response.write "<script>Amount(0);</script>"
CloseConn()
    %>
    <script type="text/javascript">

        $(document).ready(function () {
            $('ul').each(function () {
                var the = $(this);
                if (the.attr('id').indexOf('subList') > -1) {
                    $.ajax({
                        type: "get",
                        url: "ebay_system_cmd.asp",
                        data: "cmd=getEbaySysListByCid&isShowWarnSys=<%= isShowWarnSys %>&category_id=" + the.attr('id').replace(/subList/i, ''),
                        error: function (r, t, x) {
                            alert(t);
                        },
                        success: function (msg) {
                            the.html(msg);

                        }
                    });
                }
            });
        });

        function SubmitRadio() {
            $('input[type=radio]').each(function () {
                if ($(this).attr("checked"))
                    window.location.href = "ebay_system_href_list.asp?sort=" + $(this).val();
            });
        }

        function setRadio(str) {
            $('input[name=sort]').each(function () {
                if ($(this).val() == str)
                    $(this).attr("checked", "checked");

            });
        }

        setRadio('<%= cmdSort %>');
    </script>
</body>
</html>
