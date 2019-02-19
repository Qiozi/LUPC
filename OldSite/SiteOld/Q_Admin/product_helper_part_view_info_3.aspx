<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_part_view_info_3.aspx.cs" Inherits="Q_Admin_product_helper_part_view_info_3" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js_css/jquery-ui-1.10.2.custom.min.js"></script>
    <link rel="stylesheet" href="../js_css/ui-lightness/jquery-ui-1.10.2.custom.min.css" />
    <style type="text/css">
        #keywords_area {
            position: absolute;
            top: 0px;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#search_cmd_btn td').css("background", "#cccccc");
            $(window).scroll(function () {
                var offsetTop = $(window).scrollTop() + 0 + "px";
                $("#keywords_area").animate({ top: offsetTop }, { duration: 500, queue: false });
            });
        });

        function AddPartToEbaySale(sku) {
            $.get("http://webapi.lucomputers.com/Api/SetProdToEbaySale/Get?t=" + $('#Token').val() + "&sku=" + sku, {}, function (data) {
                alert(data.ErrMsg);
            });
        }

        function GeteBayOnSalePrice() {
            $('.ebayOnSalePrice').each(function () {
                var _this = $(this);
                var sku = _this.attr('data-sku');

                $.get("http://webapi.lucomputers.com/Api/GetProdOnEbaySalePrice/Get?t=" + $('#Token').val() + "&sku=" + sku, {}, function (data) {
                    _this.html(data.Data);
                });
            });
        }

        function LoadKeyword(is_split) {
            showLoading();
            var cid = $('#txt_id_ValueChanged').val();
            if (cid.length == 0) {
                alert('Please select category.');
                $('#txt_id_ValueChanged').focus();
            }
            else {
                //alert(cid);
                //$('#key_area').html('/q_admin/inc/get_category_keyword_area.aspx?cid='+cid);
                $('#key_area').load('/q_admin/inc/get_category_keyword_area.aspx?cid=' + cid,
                    function () {
                        if ($('input[name=is_split_value]').val() == "0")
                            $('input[name*=keyword]').bind('click', function () { queryProd(); });
                        else
                            $('input[name*=keyword]').bind('click', function () { queryProd(null, false, null, is_split); });

                        $('#key_area').show("slow");
                        $('#div_blank').css("height", 90 + $('#keywords_area').height());

                        $('#key_area').find('a').unbind("click");
                        //
                        //  keyword press
                        //
                        if ($('input[name=is_split_value]').val() == "0")
                            $('#key_area').find('a').bind("click", function () {
                                $(this).parent().find('input').val($(this).text().replace(/,/gi, ''));
                                $(this).parent().find('a').attr('class', 'unselected');
                                $(this).attr('class', 'selected');
                                queryProd();
                            }).css("float", "left");
                        else
                            $('#key_area').find('a').bind("click", function () {
                                $(this).parent().find('input').val($(this).text().replace(/,/gi, ''));
                                $(this).parent().find('a').attr('class', 'unselected');
                                $(this).attr('class', 'selected');
                                queryProd(null, false, null, is_split);
                            }).css("float", "left");

                        if ($('input[name=is_split_value]').val() == "0")
                            queryProd();
                        else
                            queryProd(null, false, null, is_split, null);

                    }
                );

            }
        }

        function setNullToKeywords() {
            $('input[type=radio]').attr('checked', '');
        }

        function queryProd(page, is_down, single_keyword, is_split, split_name, is_down_result) {
            showLoading();

            // sort
            var sort = $('#sort').val();


            if ("SKU or MFP#" == single_keyword)
                single_keyword = "";
            var showit = $("input[name=showit]").is(":checked") ? 1 : 0;

            if (typeof (page) == "undefined" || page == null)
                page = 1;
            var cPage = page; //$('input[name=current_page]').val();

            var is_single = false;
            if (typeof (single_keyword) != "undefined" && single_keyword != "undefined" && single_keyword != "" && single_keyword != null) {
                is_single = true;
            }
            else
                single_keyword = "";


            var cid = $('#txt_id_ValueChanged').val();
            if (cid.length == 0 && typeof (single_keyword) == "undefined") {
                alert('Please select category.');
                $('#txt_id_ValueChanged').focus();
            }
            else {

                var keyword = "";
                if (!is_single) {
                    $('input[name*=keyword_value_]').each(function () {
                        if ($.trim($(this).val()).length > 0 && $.trim($(this).val()) != 'ALL')
                            keyword += "|" + $(this).val();
                    });
                }


                //
                // other inc
                //
                var other_inc = $('input[name=keyword_other_inc]').val();


                // $('#query_result').html("Loading...");//('/q_admin/inc/get_part_list_area.aspx?showit='+ showit +'&cid='+ cid+'&keywords='+ keyword)

                if (is_down) {
                    //$('#query_result').html('/q_admin/inc/get_part_list_area.aspx?down='+is_down+'&showit='+ showit +'&cid='+ cid+'&keywords='+ keyword);
                    $('#iframe1').attr('src', '/q_admin/inc/get_part_list_area.aspx?down=' + is_down + '&showit=' + showit + '&cid=' + cid);
                    closeLoading();
                }
                else {
                    //
                    // keyword is null
                    //
                    var is_null_keyword = $('input[name=is_null_keyword]').attr('checked');
                    //
                    //
                    //            var NoneWholesaler = $('input[name=NoneWholesaler]').attr('checked');
                    var PageViewCmd = $('#PageViewCmd').val();
                    $('#PageViewCmdValue').val(PageViewCmd);
                    //return;
                    //alert(showit);
                    //$('#query_result').html('/q_admin/inc/get_part_list_area.aspx?keyword_single='+single_keyword+'&page='+ cPage +'&showit='+ showit +'&cid='+ cid+'&keywords='+ keyword);

                    if (is_down_result) {
                        var hidden_form_string = "<form id='form_down_result' method='post' action='/q_admin/inc/get_part_list_area.aspx' target='iframe1'>";
                        hidden_form_string += "<input type='text' name='split_name' value='" + split_name + "'/>";
                        hidden_form_string += "<input type='text' name='is_split' value='" + is_split + "'/>";
                        hidden_form_string += "<input type='text' name='is_null_keyword' value='" + is_null_keyword + "'/>";
                        hidden_form_string += "<input type='text' name='key_single' value='" + single_keyword + "'/>";
                        hidden_form_string += "<input type='text' name='page' value='" + cPage + "'/>";
                        hidden_form_string += "<input type='text' name='showit' value='" + showit + "'/>";
                        hidden_form_string += "<input type='text' name='cid' value='" + cid + "'/>";
                        hidden_form_string += "<input type='text' name='other_inc' value='" + other_inc + "'/>";
                        hidden_form_string += "<input type='text' name='keywords' value='" + keyword + "'/>";
                        hidden_form_string += "<input type='text' name='down' value='true'/>";
                        hidden_form_string += "<input type='text' name='sort' value='" + sort + "'/>";
                        hidden_form_string += "</form>";
                        $('#hidden_form_area').html(hidden_form_string);
                        $('#form_down_result').submit();
                        $('#hidden_form_area').html('');
                    }

                    $.ajax({
                        type: "POST",
                        url: "/q_admin/inc/get_part_list_area.aspx",
                        data: {
                            'split_name': split_name
                                , 'is_split': is_split
                                , 'is_null_keyword': is_null_keyword
                                , 'keyword_single': single_keyword
                                , 'page': cPage
                                , 'showit': showit
                                , 'cid': cid
                                , 'other_inc': other_inc
                                , 'keywords': keyword
                                , 'sort': sort
                                , 'PageViewCmd': PageViewCmd
                        },
                        success: function (msg) {
                            $('#query_result').html(msg);

                            $('i').css("font-size", "7.5pt").css("color", "#999999");
                            $('tr[tag=0]').find("td").css("background", "#f2f2f2");
                            $('#part_list').css("width", "100%");
                            $('#query_result').show("slow");
                            $('b[title=sku]').css("color", "#660000").append("&nbsp;");
                            $('span[title=mfp]').css("color", "#999999");
                            //alert($('b[title=sku]').html());
                            $('td[title=line]').css("border-bottom", "1px dotted #ff6600");
                            $('div[title=date]').css("color", "#999900");

                            //
                            // shopbot
                            //
                            $('td[title=shopbot_area]').each(function () {
                                var sku = $(this).attr('tag');
                                //$(this).html('/q_admin/inc/get_part_shopbot_price.aspx?cid='+ cid +'&sku='+ sku +'&'+rand(1000));                        
                                $(this).load('/q_admin/inc/get_part_shopbot_price.aspx?cid=' + cid + '&sku=' + sku + '&' + rand(1000));
                            });
                            //
                            // group detail
                            //
                            $('span[title=groups_detail]').each(function () {
                                var sku = $(this).attr('tag');
                                $(this).load('/site/inc/inc_get_part_groups_name.asp?id=' + sku + '&' + rand(1000));
                            });
                            //
                            // del btn
                            //
                            $('span.part_delete_btn').each(function () {
                                var sku = $(this).attr('tag');
                                $(this).load('/q_admin/inc/del_part.aspx?cmd=viewdel&sku=' + sku + '&r=' + rand(1000));
                            });

                            $('span[title=page]').click(function () {
                                //$('input[name=current_page]').val($(this).html());
                                queryProd($(this).html(), false, single_keyword)
                            });

                            //
                            // on sale
                            //
                            $('span.on_sale').each(function () {
                                //$(this).html("<img src='/soft_img/app/sale.gif' border='0'/>");
                                //alert($(this).html());
                            });
                            //
                            //// show On Sale, Rebate, webSys, eBaySys ICON;
                            showTagImg();

                            //
                            // edit cash price
                            //
                            $('td.part_edit_price_area').hover(
                                function () {
                                    //alert('y');
                                    $(this).find('span').css("display", "");
                                },
                                function () {
                                    //alert('n');
                                    $(this).find('span').css("display", "none");
                                });
                            $('td.part_edit_price_area >img').hover(
                                function () {
                                    //alert('y');
                                    $(this).css("border", "1px solid red");
                                },
                                function () {
                                    //alert('n');
                                    $(this).css("border", "1px solid blue");
                                });

                            // show Connect ICO.
                            connect_change();

                            //
                            // Modify Namess.
                            if (PageViewCmd == 4
                            || PageViewCmd == 5
                            || PageViewCmd == 6
                            || PageViewCmd == 7) {
                                $('input[type=button]').each(function () {
                                    $(this).attr("disabled", "");
                                    if ($(this).val() == "Edit") {
                                        $(this).bind('click', function () {
                                            js_callpage_cus("/q_admin/editPartDetail.aspx?id=" + $(this).attr('tag'), 'modify_detail' + $(this).attr('tag'), 880, 800);
                                        });
                                    }
                                    if ($(this).val() == "Save") {
                                        $(this).bind('click', function () {
                                            SavePartModifyNames($(this).attr('tag'), new Array());
                                        });
                                    }
                                    if ($(this).attr("name") == "SaveALL") {
                                        $(this).bind('click', function () {
                                            SavePartModifyNamesALL();
                                        });
                                    }
                                });
                            }

                            $('input[name=adjustment_enddate]').each(function () {
                                var the = $(this);
                                the.datepicker({
                                    numberOfMonths: 2,
                                    showButtonPanel: true,
                                    dateFormat: 'yy-mm-dd'
                                });
                            });

                            $('td[name=ebayPriceInfo]').each(function () {
                                var the = $(this);
                                refreshEbayPriceArea(the);
                            });
                            GeteBayOnSalePrice();
                            closeLoading();
                        }
                    , error: function (errMsg) { alert(errMsg); closeLoading(); }
                    });
                }
            }
        }

        function refreshEbayPriceArea(the) {
            the.html("......");
            $.ajax({
                type: "get",
                url: "/q_admin/inc/get_ebay_price_info.aspx?sku=" + the.attr("sku"),
                data: "",
                error: function (r, s, t) {
                    the.html(s);
                },
                success: function (msg) {
                    the.html(msg);
                }

            });
        }

        function viewInfoUploadPartInfo() {
            $('#query_result').html("<iframe src=\"/q_admin/inc/upload_part_info.aspx\" name=\"iframe_upload_part_info\" id=\"iframe_upload_part_info\" style=\"width: 100%; height: 100px;\" frameborder=\"0\"></iframe>");
            var _attr = parseInt(document.body.clientHeight);
            $('#key_area').html('');
            $('#page_area').html('');
            $('#div_blank').css("height", 40 + $('#keywords_area').height());
            $('#iframe_upload_part_info').css("height", isNaN(_attr) || _attr <= 45 ? "100%" : (_attr - 45) + "px");

        }


        function upload_inc_cost() {
            $('#query_result').html("<iframe src=\"product_helper_import_store_price_2.aspx\" name=\"iframe_upload_part_info\" id=\"iframe_upload_part_info\" style=\"width: 100%; height: 100px;\" frameborder=\"0\"></iframe>");
            var _attr = parseInt(document.body.clientHeight);
            $('#key_area').html('');
            $('#page_area').html('');
            $('#div_blank').css("height", 40 + $('#keywords_area').height());
            $('#iframe_upload_part_info').css("height", isNaN(_attr) || _attr <= 45 ? "100%" : (_attr - 45) + "px");
        }


        var resizeTimer = null;
        $(window).bind("resize", function () {
            if (resizeTimer)
                clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () {
                var _attr = parseInt(document.body.clientHeight); //alert(_attr);
                // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";
                $('#iframe_upload_part_info').css("height", isNaN(_attr) || _attr <= 45 ? "100%" : (_attr - 45) + "px");
            }, 100);

        });

        function setPartListSplitName() {
            queryProd(null, false, null, true, $('input[name=split_name]').val());
        }


        function addNullPart() {
            var cid = $('#txt_id_ValueChanged').val();
            if (cid.length == 0) {
                alert('please select category'); return;
            }
            return js_callpage_cus('/q_admin/inc/new_part_null.aspx?cid=' + cid + '&qty=' + $('input[name=new_part_quantity]').val(), 'addpart', 200, 200);
        }

        function partShowGray(sku, is_close) {


            $('b[title=sku]').each(function () {

                if ($.trim($(this).text()) == '[' + sku + ']') {
                    var color = is_close ? "#f2f2f2" : "#ffffff";
                    $(this).parent().parent().find('td').css('background', color);
                }
                //alert($(this).text() +"|"+ '['+ sku +']');
            });
        }



        function openEditRebate() {
            var keyword = $.trim($('input[name=key_single]').val());
            if (keyword != "SKU or MFP#" && keyword != "") {
                return js_callpage_cus('/q_admin/product_part_rebate.aspx?keyword=' + keyword, 'part_cmd', 520, 500);
            }
            else
                alert('please input Keyword.');
        }



        function part_save_cash_price(the, sku) {
            var cash_price = 0;
            the.parent().parent().find("input").each(function () {

                if ($(this).attr('name') == 'cash_price') {
                    cash_price = $(this).val();

                }

            });

            //
            // save
            //
            $.ajax({
                type: "POST",
                url: "product_part_cmd.aspx",
                data: "cmd=save_cash_price&sku=" + sku + "&cash_price=" + cash_price,
                success: function (msg) {
                    if (msg.toString().indexOf('|') != -1) {
                        var price_group = msg.toString().split(/\|/ig);
                        the.parent().parent().find("input").each(function () {

                            if ($(this).attr('name') == 'cash_price') {
                                $(this).val(price_group[2]);

                            }
                            if ($(this).attr('name') == 'regular_price') {
                                $(this).val(price_group[0]);

                            }
                            if ($(this).attr('name') == 'card_price') {
                                $(this).val(price_group[1]);
                            }
                            if ($(this).attr('name') == 'adjustment') {
                                $(this).val(price_group[3]);
                            }
                            if ($(this).attr('name') == 'discount_price') {
                                $(this).val(price_group[4]);
                            }
                            if ($(this).attr('name') == 'import_price') {
                                $(this).val(price_group[5]);
                            }
                        });
                        alert("save is success.");
                    }
                    else
                        alert(msg);
                }
                    , error: function (errorMsg) { alert(errorMsg); }
            });
            closeLoading();
        }



        function part_save_discount_price(the, sku) {
            showLoading();

            var ds = prompt('请输入促销的天数\r\n如果天数为0,将删除on sale.', '7');
            var f = ds;

            var discount_price = 0;
            the.parent().parent().find("input").each(function () {

                if ($(this).attr('name') == 'discount_price') {
                    discount_price = $(this).val();

                }

            });
            // alert(f +"|"+discount_price+"|"+sku);
            //
            // save
            //
            $.ajax({
                type: "POST",
                url: "product_part_cmd.aspx",
                data: "cmd=onsale&day_qty=" + f + "&sku=" + sku + "&discount_price=" + discount_price,
                success: function (msg) {
                    if (msg.toString().indexOf('|') != -1) {
                        var price_group = msg.toString().split(/\|/ig);
                        the.parent().parent().find("input").each(function () {

                            if ($(this).attr('name') == 'cash_price') {
                                $(this).val(price_group[2]);

                            }
                            if ($(this).attr('name') == 'regular_price') {
                                $(this).val(price_group[0]);

                            }
                            if ($(this).attr('name') == 'card_price') {
                                $(this).val(price_group[1]);
                            }
                            if ($(this).attr('name') == 'adjustment') {
                                $(this).val(price_group[3]);
                            }
                            if ($(this).attr('name') == 'discount_price') {
                                $(this).val(price_group[4]);
                            }
                            if ($(this).attr('name') == 'import_price') {
                                $(this).val(price_group[5]);
                            }
                        });
                        alert("save is success.");
                    }
                    else
                        alert(msg);
                    closeLoading();

                }
            });

        }

        function part_save_cost_price(the, sku) {
            showLoading();

            var cash_price = 0;
            the.parent().parent().find("input").each(function () {

                if ($(this).attr('name') == 'import_price') {
                    cash_price = $(this).val();

                }

            });

            //
            // save
            //
            $.ajax({
                type: "POST",
                url: "product_part_cmd.aspx",
                data: "cmd=save_cost_price&sku=" + sku + "&cost_price=" + cash_price,
                success: function (msg) {
                    if (msg.toString().indexOf('|') != -1) {
                        var price_group = msg.toString().split(/\|/ig);
                        the.parent().parent().find("input").each(function () {
                            if ($(this).attr('name') == 'cash_price') {
                                $(this).val(price_group[2]);

                            }
                            if ($(this).attr('name') == 'regular_price') {
                                $(this).val(price_group[0]);

                            }
                            if ($(this).attr('name') == 'card_price') {
                                $(this).val(price_group[1]);
                            }
                            if ($(this).attr('name') == 'adjustment') {
                                $(this).val(price_group[3]);
                            }
                            if ($(this).attr('name') == 'discount_price') {
                                $(this).val(price_group[4]);
                            }
                            if ($(this).attr('name') == 'import_price') {
                                $(this).val(price_group[5]);
                            }
                        });
                        alert("save is success.");

                    }
                    else
                        alert(msg);
                    closeLoading();
                }
            });

        }

        function part_save_adjustment(the, sku) {

            showLoading();

            var adjustment = 0;
            var adjustmentEndDate = "";
            the.parent().parent().find("input").each(function () {

                if ($(this).attr('name') == 'adjustment') {
                    adjustment = $(this).val();
                }
                if ($(this).attr('name') == 'adjustment_enddate')
                    adjustmentEndDate = $(this).val();
            });
            if (adjustmentEndDate == '') {
                alert("please input adjust end date.");
                return;
            }
            // alert(adjustmentEndDate);
            //
            // save
            //
            $.ajax({
                type: "POST",
                url: "product_part_cmd.aspx",
                data: "cmd=save_adjust_price&sku=" + sku + "&adjust=" + adjustment + "&adjustEnddate=" + adjustmentEndDate,
                success: function (msg) {
                    if (msg.toString().indexOf('|') != -1) {
                        var price_group = msg.toString().split(/\|/ig);
                        the.parent().parent().find("input").each(function () {

                            if ($(this).attr('name') == 'cash_price') {
                                $(this).val(price_group[2]);

                            }
                            if ($(this).attr('name') == 'regular_price') {
                                $(this).val(price_group[0]);

                            }
                            if ($(this).attr('name') == 'card_price') {
                                $(this).val(price_group[1]);
                            }
                            if ($(this).attr('name') == 'adjustment') {
                                $(this).val(price_group[3]);
                            }
                            if ($(this).attr('name') == 'discount_price') {
                                $(this).val(price_group[4]);
                            }
                            if ($(this).attr('name') == 'import_price') {
                                $(this).val(price_group[5]);
                            }
                        });
                        alert("save is success.");

                    }
                    else
                        alert(msg);
                    closeLoading();
                }
            });

        }

        // show On Sale, Rebate, webSys, eBaySys ICON;
        function showTagImg() {
            $('span[title=eBaysys]').each(function () {
                if ($(this).html() == "0")
                    $(this).html('');
                else {
                    var sku = $(this).html();
                    $(this).html("<img src='/soft_img/app/eBay_sys.jpg' style='cursor:pointer;' onclick='js_callpage_cus(\"/q_admin/eBayMaster/ebay_view_online.asp?luc_sku=" + sku + "\", \"ebay_online_view\", 780, 500);return false;' border='0'/>");
                    //alert();
                }
            });

            $('span[title=eBaySysCount]').each(function () {
                if ($(this).html() == "0" || $(this).html() == "")
                    $(this).html('');
                else {
                    var sku = $(this).html();
                    $(this).html("<img src='/soft_img/app/ebay_logo_c.jpg' style='cursor:pointer;' onclick='js_callpage_cus(\"/q_admin/eBayMaster/ebay_view_online.asp?luc_sku=" + sku + "\", \"ebay_online_view\", 780, 500);return false;' border='0'/>");
                    //alert();
                }
            });

            $('span[title=webSys]').each(function () {
                if ($(this).html() == "0")
                    $(this).html('');
                else
                    $(this).html("<img src='/soft_img/app/web_sys.jpg' border='0'/>");
                //alert($(this).html());
            });
            $('span[title=rebate]').each(function () {
                if ($(this).html() == "0")
                    $(this).html('');
                else
                    $(this).html("<img src='/soft_img/app/rebate.gif' border='0'/>");
                //alert($(this).html());
            });
            $('span[title=SysTopView]').each(function () {
                var sku = $(this).attr("sku");

                if ($(this).html() == "0")
                    $(this).html("<img src='/soft_img/app/top1.gif' border='0' style='cursor:pointer;' onclick='top2(\"" + sku + "\", $(this));';/>");
                else
                    $(this).html("<img src='/soft_img/app/top2.gif' border='0' style='cursor:pointer;' onclick='unTop(\"" + sku + "\", $(this));';/>");
                //alert($(this).html());
            });
            $('span[title=eBayOnline]').each(function () {
                if ($(this).html() == "0")
                    $(this).html('');
                else {
                    var sku = $(this).html();
                    $(this).html("<img style='cursor:pointer;' onclick='js_callpage_cus(\"/q_admin/eBayMaster/ebay_view_online.asp?luc_sku=" + sku + "\", \"ebay_online_view\", 780, 500);return false;' src='/soft_img/app/ebay_logo.jpg' border='0'/>");
                    //alert($(this).html());
                }
            });
        }

        //
        // is change price
        //
        function connect_change() {


            $('span[name=connect_change]').each(function () {
                var sku = $(this).attr('tag');
                if ($(this).html() == "1") {

                    $(this).html("<img src='/soft_img/app/split_conn.jpg' style='cursor:pointer;' onclick=\"splitConnectPartChange('" + sku + "');\">");
                }
                else
                    $(this).html("<img src='/soft_img/app/connect_c.jpg' style='cursor:pointer;' onclick=\"connectPartChange('" + sku + "');\">");

            });
        }

        function splitConnectPartChange(sku) {
            var ds = prompt('请输入有效的天数\r\n', '7');
            var f = ds;

            if ($.isNumeric(f)) {

            }
            else
                return;

            $.ajax({
                type: "get",
                url: "product_part_cmd.aspx",
                data: "cmd=splitConnectPartChange&sku=" + sku + "&days=" + f,
                success: function (msg) {
                    if (msg == "True") {
                        alert("OK");

                        $('span[name=connect_change]').each(function () {
                            var _sku = $(this).attr('tag');
                            if (_sku == sku) {
                                $(this).html("<img src='/soft_img/app/connect_c.jpg' style='cursor:pointer;' onclick=\"connectPartChange('" + sku + "');\">");
                                var the = $(this);
                                $.ajax({
                                    type: "get",
                                    url: "product_part_cmd.aspx",
                                    data: "cmd=getconnectiondays&sku=" + sku,
                                    error: function (r, t, s) {

                                    },
                                    success: function (msg) {
                                        the.next().html(msg);
                                    }
                                });
                            }
                        });
                    }
                    else
                        alert("error.");
                }
                    , error: function (msg) { alert(msg); }
            });
        }

        function connectPartChange(sku) {

            $.ajax({
                type: "get",
                url: "product_part_cmd.aspx",
                data: "cmd=connectPartChange&sku=" + sku,
                success: function (msg) {
                    if (msg == "True") {
                        alert("OK");
                        $('span[name=connect_change]').each(function () {
                            var _sku = $(this).attr('tag');
                            if (_sku == sku) {
                                $(this).html("<img src='/soft_img/app/split_conn.jpg' style='cursor:pointer;' onclick=\"splitConnectPartChange('" + sku + "');\">");

                                var the = $(this);
                                $.ajax({
                                    type: "get",
                                    url: "product_part_cmd.aspx",
                                    data: "cmd=getconnectiondays&sku=" + sku,
                                    error: function (r, t, s) {

                                    },
                                    success: function (msg) {
                                        the.next().html(msg);
                                    }
                                });
                            }
                        });
                    }
                    else
                        alert("error.");
                }
                    , error: function (msg) { alert(msg); }
            });
        }

        function SavePartModifyNames(sku, skus) {
            if (skus.length > 0) {
                sku = skus[0];
            }

            if (!(parseInt(sku) > 0))
                return;
            $('input[name=part_modify_name][tag=' + sku + ']').each(function () {
                var sku = $(this).attr("tag");
                var name = $(this).val();
                var e = $(this);
                $('span[name=loading][tag=' + sku + ']').each(function () { $(this).html('<img src="/soft_img/tags/loading.gif" />'); });
                $.ajax({
                    type: "POST",
                    url: "product_part_cmd.aspx",
                    data: "cmd=ModifyNamesEbayShortMiddleLong&sku=" + sku + "&PageViewCmd=" + $('#PageViewCmdValue').val() + "&modifyName=" + name,
                    success: function (msg) {
                        if (msg == "OK") {
                            e.parent().parent().parent().css("background", "#f2f2f2");
                        }
                        $('span[name=loading][tag=' + sku + ']').each(function () { $(this).html(''); });
                        if (skus.length > 0) {
                            skus.splice(0, 1);
                            SavePartModifyNames(0, skus);
                        }
                        return true;
                    }
                   , error: function (msg) {
                       alert(msg);
                       $('span[name=loading][tag=' + sku + ']').each(function () { $(this).html(''); });
                       if (skus.length > 0) {
                           skus.splice(0, 1);
                           SavePartModifyNames(0, skus);
                       }
                   }
                });
            });
            return false;

        }

        function SavePartModifyNamesALL() {
            if (!confirm("Are you sure???"))
                return;
            var skus = new Array();
            $('input[name=part_modify_name]').each(function (i) {
                skus[i] = $(this).attr('tag');

            });

            SavePartModifyNames(0, skus);
        }


        function top2(sku, e) {
            $.ajax({
                type: "POST",
                url: "product_part_cmd.aspx",
                data: "cmd=SetToSysTop&sku=" + sku,
                success: function (msg) {
                    if (msg == "OK") {
                        e.parent().html("<img src='/soft_img/app/top2.gif' border='0' style='cursor:pointer;' onclick='unTop(\"" + sku + "\", $(this));';/>");
                    }

                }
                , error: function (msg) {
                    alert(msg);
                }
            });
        }

        function unTop(sku, e) {
            $.ajax({
                type: "POST",
                url: "product_part_cmd.aspx",
                data: "cmd=SetToSysTop&sku=" + sku,
                success: function (msg) {
                    if (msg == "OK") {
                        e.parent().html("<img src='/soft_img/app/top1.gif' border='0' style='cursor:pointer;' onclick='top2(\"" + sku + "\", $(this));';/>");
                    }

                }
                , error: function (msg) {
                    alert(msg);
                }
            });
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 230px; border: 0px solid blue;" id='div_blank'>&nbsp;</div>
    <div style="border-bottom: 1px solid #cccccc; background: #f2f2f2; width: 100%;" id='keywords_area'>
        <input type="hidden" name="current_page" value="1" />
        <input type="hidden" name="Token" id="Token" value="<%= Token %>" />
        <table id="search_cmd_btn" cellpadding="0" cellspacing="0" width="100%" border="0">
            <tr>
                <td width="530" valign="top">

                    <select name='sort' id='sort'>
                        <option value='-1'>Select Sort</option>
                        <option value='1'>Highest Price First </option>
                        <option value='2'>Lowest Price First </option>
                        <option value='3'>Highest Stock Quantity First</option>
                        <option value='4'>Lowest Stock Quantity First</option>
                        <option value='5'>Lowest Last Modify First</option>
                        <option value='6'>eBay Diff BuyItNowPrice</option>
                        <option value='7'>ETA </option>
                        <option value='8'>DeleteNoneWholesaler</option>
                    </select>
                    <input type="hidden" name="is_split_value" value="0" />
                    <input type="checkbox" name="showit" checked="checked" />showit
                    <input type="checkbox" name="is_null_keyword" />Keyword is Null
  <%--                  <input type="checkbox" name="NoneWholesaler" />None Wholesaler--%>
                    <select name='PageViewCmd' id='PageViewCmd'>
                        <option value='-1'>Select Cmd</option>
                        <option value='1'>None Wholesaler</option>
                        <option value='2'>Not eBay Site</option>
                        <option value='3'>On Sale</option>
                        <%--                        <option value='4'>Modify Short Name</option> 
                        <option value='5'>Modify Middle Name</option>
                        <option value='6'>Modify Long Name</option>--%>
                        <option value='7'>Modify eBay Name</option>
                        <option value='8'>View Top</option>
                        <option value='9'>eBay in the past</option>
                    </select>
                    <input type="hidden" name='PageViewCmdValue' id='PageViewCmdValue' />

                    <br />
                    Keyword:<input type="text" name="key_single" value="SKU or MFP#" onfocus="if(this.value=='SKU or MFP#') this.value='';" />
                    <input type="button" value="Find it" onclick="setNullToKeywords(); queryProd(1, false, $('input[name=key_single]').val());" />
                    <input type="button" value="rebate" title="edit rebate" onclick="openEditRebate();" />
                </td>
                <td width="200" valign="top">
                    <ul class="ul_parent_2">
                        <li>
                            <table cellpadding="0" cellspacing="0" style="border: 0px solid #ccc; width: 150px;">
                                <tr>
                                    <td>Category</td>
                                    <td>
                                        <input type="hidden" id="txt_id_ValueChanged" />
                                        <input id="txt_text" type="text" size="25" readonly="true" />
                                        <div style="left: auto; top: 16px; display: none" id="uc_dropDownList_category_selected">
                                            <iframe frameborder="0" src="/q_admin/asp/category_selected_not_sys.asp?div_id=uc_dropDownList_category_selected&id=txt_id_ValueChanged&textid=txt_text" style="width: 300px; height: 300px; border: 1px solid #ccc;"></iframe>
                                        </div>
                                    </td>
                                    <td>
                                        <img src="http://www.lucomputers.com/images/arrow_5.gif" alt="Press" title="Press" style="height: 19px; width: 15px; cursor: pointer" onclick="document.getElementById('uc_dropDownList_category_selected').style.display = '';" /></td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </td>
                <td valign="top">
                    <input type="button" value="Load" onclick="$('input[name=is_split_value]').val(0); LoadKeyword(false);" />
                    <input type="button" name="part_sort" value="split line" onclick="$('input[name=is_split_value]').val(1); LoadKeyword(true);" />
                    <input type="button" name="download_result" value="Down Result" onclick="$('input[name=is_split_value]').val(0); queryProd(1, false, '', false, '', true);" />
                    <a href="/q_admin/manager/product/syspartforfixedlist.aspx" target="_blank">fixed list for sys</a>
                </td>
                <td style="text-align: right">
                    <input type="button" value="delete product that it none wholesaler " />
                    <%-- <input type="text" name="new_part_quantity" value="1" maxlength='3' size='3'/>
                <input type="button" name="new_part_submit" value="New Part(null)" onclick="addNullPart();" />
                <input type='button' name='download' value='Down List' onclick="queryProd(1,true);"/>
                <input type='button' name='Upload' value='Upload' onclick="viewInfoUploadPartInfo();"/>
               
                <input type="button" name="upload_pice" value="Upload Price" onclick="upload_inc_cost();" />--%>
                </td>
            </tr>



        </table>
        <div id="key_area">
        </div>
        <div id='query_keyword_list'>
        </div>
        <span id="page_area"></span>
    </div>
    <div id='query_result'>
    </div>
    <div id='hidden_form_area' style="display: none"></div>
    <iframe src="/site/blank.html" name="iframe1" id="iframe1" style="width: 0px; height: 0px;" frameborder="0"></iframe>

</asp:Content>

