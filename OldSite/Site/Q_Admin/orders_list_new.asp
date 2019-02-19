<!--#include virtual="site/inc/validate_admin.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Orders List</title>
    <script type="text/javascript" src="/q_admin/js/winOpen.js"></script>
    <script type="text/javascript" src="/q_admin/js/helper.js"></script>
    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.tools.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.expose.1.0.5.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.core.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.draggable.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.pager.js" type="text/javascript"></script>
    <link href="/js_css/pager.css" rel="stylesheet" type="text/css" />
    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/default/admin.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="/js_css/jquery_lab/popup.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popupclass.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery.float.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/jQuery.cookie.js"></script>
    <style type="text/css">
        .tdOver {
            background-color: #e0e0e0;
        }

        .tdOut {
            background-color: #ffffff;
        }

        .row1 {
            padding: 0px;
            margin: 2px;
            clear: both;
            background: #DAECFF;
        }

        .row2 {
            padding: 0px;
            margin: 2px;
            clear: both;
            background: #f2f2f2;
        }

        .row3 {
            padding: 0px;
            margin: 2px;
            clear: both;
            background: #FFE3D1;
        }

        .rowParent {
            border: 0px solid #90C7FF;
            width: 100%;
        }

        .cell {
            float: left;
            padding: 2px;
        }

        .tableParent1 {
            background: #90C7FF;
            margin-bottom: 5px;
            margin-left: 2px;
            margin-right: 2px;
        }

        .tableParent2 {
            background: #cccccc;
            margin-bottom: 5px;
            margin-left: 2px;
            margin-right: 2px;
        }

        .tableParent3 {
            background: #FF9E5D;
            margin-bottom: 5px;
            margin-left: 2px;
            margin-right: 2px;
        }

        .title {
            color: Black;
        }

        #pageTable {
            padding: 0px;
            margin: 0px;
            position: absolute;
            top: 0px;
            left: 5px;
            background: #fff;
        }

        #showInStoreArea {
            padding: 3px;
            margin: 0px;
            position: absolute;
            top: 0px;
            left: 805px;
            background: #f2f2f2;
            border: 1px solid #ccc;
            width: 100px;
            min-height: 200px;
        }

            #showInStoreArea div {
                margin-top: 3px;
                padding: 3px;
            }

                #showInStoreArea div img {
                    display: none;
                }

                #showInStoreArea div:hover {
                    margin-top: 3px;
                    border: 1px solid #ccc;
                    padding: 2px;
                }

                    #showInStoreArea div:hover img {
                        display: inline;
                    }
    </style>
</head>
<body>
    <%

    Dim pageSize 
    pageSize = request("pageSize")

    if(pageSize = "") then 
        pageSize = request.Cookies("orderPageSize")
    else
        response.Cookies("orderPageSize") = pageSize
    end if
    if pageSize = "" then 
        pageSize = 10
    end if
    %>
    <table id="pageTable">
        <tr>
            <td>
                <div id="pager"></div>
            </td>
            <td>Page Size:
                <select name="pageSize" onchange="changePageSize($(this));">
                    <option value="10" <% if pageSize = 10 then response.write " selected='true' " %>>10</option>
                    <option value="20" <% if pageSize = 20 then response.write " selected='true' " %>>20</option>
                    <option value="30" <% if pageSize = 30 then response.write " selected='true' " %>>30</option>
                </select>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <div id="order_list" style="clear: both; padding-top: 1px;">
        <div style="line-height: 30px; text-align: center; padding: 5em;">Loading...</div>
    </div>

    <div id='editArea' style="display: none; width: 400px; min-height: 150px; height: auto; background: #ffffff; position: absolute; z-index: 10000; border: 1px solid #ccc;">
        <b id='expose_sku'></b>

        <div style="text-align: right">
            <input type='button' onclick="$('#editArea').css('display', 'none');" value="Close 1">
        </div>
        <hr size="1" />
        <table>
            <tr>
                <td>Order ID:</td>
                <td id='td_curr_order_id'></td>
            </tr>
            <tr>
                <td>Order Code:</td>
                <td id='td_curr_order_code'></td>
            </tr>
            <!-- <tr>
                    <td>BK STAT:</td><td><select name="bk_stat"></select></td>
            </tr>-->
            <tr>
                <td>Web STATUS:</td>
                <td>
                    <select name="frt_stat"></select></td>
            </tr>
            <tr style="display: none;">
                <td>Note:</td>
                <td>
                    <input type="hidden" id="current_id" /><input type="text" id="ipt_note" /></td>
            </tr>
        </table>
        <hr size='1' />
        <div style="text-align: right">
            <input type='button' value='Save' onclick="saveStatus($(this));" /><span style='color: red;' id='save_result'></span>
        </div>

    </div>

    <div id="showInStoreArea" style='display: none;'></div>

    <script type="text/javascript">
        var pagerCount = 100;
        var currPager = 1;

        $().ready(function () {

            LoadList();
            $('#editArea').draggable();

            $("#pager").pager({ pagenumber: 1, pagecount: 1, buttonClickCallback: PageClick });

            $(window).scroll(function () {
                var offsetTop = $(window).scrollTop() + 0 + "px";
                $("#pageTable").animate({ top: offsetTop }, { duration: 500, queue: false });
                $("#showInStoreArea").animate({ top: offsetTop }, { duration: 500, queue: false });
            });
            showStore();


        });

        //
        // load list db.
        //
        function LoadListSplit(data) {

            //    data = eval(data);
            //    var html = "<table id=\"tab_order_list\" cellspacing='0' cellpadding=\"3\" id='tab_order_list' width='100%'>";

            //    for(var i=0; i<data.length; i++)
            //    {
            //        //alert(json[i].customer_serial_no+" " + json[i].customer_serial_no)
            //        html += "<tr id='o_"+ data[i].order_helper_serial_no +"'>";
            //        html += "   <td> Order#: "+ data[i].order_code
            //        html += "   <br>Invoice#:"+ data[i].order_invoice +"  </td>";
            //        // html += "   <td name='up_btn' id='"+data[i].order_helper_serial_no+"' tag='"+data[i].order_code+"'> "+ getUHref()+" </td>";
            //        //html += "   <td> "+ getOrderDetail(data[i].order_code,data[i].order_source ,data[i].order_invoice, false, data[i].order_date) +"</td>";        
            //        //html += "   <td>";
            //       // html += "           "+ getOrderDetail(data[i].order_code,data[i].order_source ,data[i].order_invoice, true, data[i].order_date);
            //        //html += "   </td>";
            //        html += "   <td>"+ data[i].shipping_date +"</td>";
            //        html += "   <td>"+ data[i].order_date +"</td>";
            //        html += "   <td name='td_group_total'>$"+ data[i].grand_total +"</td>";
            //        html += "   <td name='td_balance'>$"+ data[i].balance +"</td>";
            //        html += "   <td name='td_price_unit'>"+ data[i].price_unit +"</td>";
            //        html += "   <td nowrap='nowrap' style='width:180px; overflow:hidden;'>"+ data[i].pay_method +"</td>";
            //        html += "   <td nowrap='nowrap' style='width:180px; overflow:hidden;'>"+ getNameHref(data[i].customer_serial_no, data[i].name) +"</td>";
            //        html += "   <td name='bak_name' tag='"+ data[i].out_status +"'>"+ data[i].facture_state_name +"</td>";
            //        html += "   <td name='fre_name' tag='"+ data[i].pre_status_serial_no +"' style='background:"+data[i].pre_back_color+"'>"+ data[i].pre_status_name +"</td>";
            //        html += "   <td>"+ getPaidStatusString(data[i].order_pay_status_id, data[i].order_code) +"</td>";
            //        html += "   <td>"+ data[i].assigned_to_staff_name +"</td>";
            //        html += "   <td>"+ getOrderSourceName(data[i].order_source) +"</td>";
            //        html += "</tr>";
            //        if(data[i].out_note.length>0)
            //        {
            //            html += "<tr><td colspan='16' name='td_note'>"+ data[i].out_note +"</td>";
            //        }
            //    }

            //    if(data.length <1)
            //        html += "<tr><td colspan='16' name='td_note' style='padding:10em; text-align:center;'>No Match Data.</td>";
            //    else
            //    {
            //        getPageCount();
            //    }

            data = eval(data);
            var html = "";
            if (data.length < 1)
                html += "<div style='padding: 300px;'>No Match Data.</div>";
            else {
                getPageCount();
            }

            for (var i = 0; i < data.length; i++) {
                var pclass = data[i].pre_status_name == "Submitted" ? "tableParent3" : (data[i].pre_back_color == "LightGreen" ? "tableParent2" : "tableParent1");
                var rowClass = data[i].pre_status_name == "Submitted" ? "row3" : (data[i].pre_back_color == "LightGreen" ? "row2" : "row1");


                html += "<div class='" + pclass + "' title='" + data[i].order_helper_serial_no + "' ><table id='table_" + data[i].order_code + "' class='rowParent' cellpadding='0' cellspacing='1' border='0'  onmouseout='listOut($(this));' onmouseover='listOver($(this));'>";
                html += "   <tr class='" + rowClass + "'  title='" + data[i].order_helper_serial_no + "'>";
                html += "       <td colspan='6' style='padding: 3px;'>";
                html += "       <div class='cell' style='width: 250px;'>";
                html += "           Order#: " + getOrderDetail(data[i].order_code, data[i].order_source, data[i].order_invoice, true, data[i].order_date);
                html += "           &nbsp;&nbsp;&nbsp;&nbsp;Invoice#: " + getOrderDetail(data[i].order_code, data[i].order_source, data[i].order_invoice, false, data[i].order_date);
                html += "       </div>";
                html += "       <div class='cell' style='width: 300px;'>";
                html += "           Regdate: " + data[i].order_date;
                html += "       </div>";
                html += "       <div class='cell' style='width: 150px;'>";
                html += "           Customer#: " + data[i].customer_serial_no;
                html += "       </div>";
                html += "       <div class='cell' style='width: 300px;'>";
                html += "           Paymeht: <span>" + data[i].pay_method + "</span>";
                html += "       </div>";
                html += "       <div class='cell' >";
                html += "           <span name='prodName' tag='" + data[i].order_code + "'>......</span>";
                html += "       </div>";
                html += "       <div style='float:right;' name='cmdList'>";



                if (data[i].notepadCount != "0") {
                    html += "           <a href='/q_admin/orders_edit_detail_notepad.aspx?order_code=" + data[i].order_code + "' onclick=\"ShowIframe('Order (" + data[i].order_code + ") Notepad',this.href,800,450);return false;\"><span title='notepad' class='chat3'>" + data[i].notepadCount + "</span></a>";
                }
                else
                    html += "           <a href='/q_admin/orders_edit_detail_notepad.aspx?order_code=" + data[i].order_code + "' onclick=\"ShowIframe('Order (" + data[i].order_code + ") Notepad',this.href,800,450);return false;\"><span title='notepad' class='chat3'>&nbsp;</span></a>";

                if (data[i].msgCount != "0") {
                    html += "           <a href='/q_admin/orders_edit_detail_customer_msg.aspx?order_code=" + data[i].order_code + "' onclick=\"ShowIframe('Order (" + data[i].order_code + ") Message',this.href,800,450);return false;\"><span title='talk' class='chat1'>" + data[i].msgCount + "</span></a>";
                }
                if (data[i].isGetIn == "0") {
                    //html += "           <input type='checkbox' name='showInStore_"+data[i].order_code+"' id='showInStore_"+data[i].order_code+"' onclick=\"addOrders('"+data[i].order_code+"');\" >";
                    html += "           <a name='showInStore_" + data[i].order_code + "' id='showInStore_" + data[i].order_code + "' onclick=\"addOrders('" + data[i].order_code + "');return false;\"><img src='../../soft_img/app/(03,34).png'></a>";

                }


                if ($.trim(data[i].order_code).length == 5) {

                    html += "               " + getEBayRefresh($.trim(data[i].order_code));
                }


                html += "       </div>";

                html += "       </td>";
                html += "   </tr>";
                html += "   <tr>";
                html += "       <td style='background:white;height: 50px;width: 100px;text-align:center;padding:3px;'>";
                html += "           " + getE(data[i].order_code, data[i].order_source);
                html += "          <br> <a href='" + data[i].customer_serial_no + "' onclick='newOrder(" + data[i].customer_serial_no + ");return false;'>New</a>";
                html += "          <br> <a href='/q_admin/paypal_card_do_direct_payment.asp?order_code=" + data[i].order_code + "'>Paypal</a>";
                html += "       </td>";
                html += "       <td style='background:white;width: 150px; text-align:center;padding:3px;'>";
                html += "           " + data[i].customerName;
                html += "       </td>";
                html += "       <td style='background:white;width: 220px;padding:3px;'>";
                if (data[i].phone_d != "") {
                    html += "Business: " + data[i].phone_d + "<br>";
                }
                if (data[i].phone_n != "" && data[i].phone_n != data[i].phone_d) {
                    html += "Home: " + data[i].phone_n + "<br>";
                }
                if (data[i].phone_c != "" && (data[i].phone_c != data[i].phone_d && data[i].phone_c != data[i].phone_n)) {
                    html += "Mobile: " + data[i].phone_c;
                }
                html += "       </td>";
                html += "       <td style='background:white;width: 150px; text-align:right;padding:3px;'><a href='' name='paypal' style='display:none;'>paypal</a>";
                html += "            " + getPaidStatusString(data[i].order_pay_status_id, data[i].order_code) + " " + data[i].grand_total + " " + data[i].price_unit;
                html += "       </td>";
                html += "       <td style='background:white;width: 250px; text-align:center;padding:3px;'  name='up_btn' id='" + data[i].order_helper_serial_no + "' tag='" + data[i].order_code + "'>";
                html += "          " + getUHref(data[i].pre_status_name, data[i].order_helper_serial_no) + "";
                html += "       </td>";
                html += "       <td style='background:white;padding:3px;'>";
                if (data[i].customer_shipping_address.length > 5) {
                    html += "           <table><tr>";
                    html += "               <td class='title'>Ship To: </td><td>" + getNameHref(data[i].customer_serial_no, data[i].name) + "</td>";
                    html += "               </tr>";
                    html += "               <tr>";
                    html += "                   <td class='title'>Ship Address:</td><td>" + data[i].customer_shipping_address + ", " + data[i].customer_shipping_city + ", " + data[i].shipping_state_code + ", " + data[i].customer_shipping_zip_code + ", " + data[i].shipping_country_code + "</td>";
                    html += "               </tr>";
                    html += "               <tr>";
                    html += "               <td class='title'>Ship Date: </td><td>" + data[i].shipping_date + "</td>";
                    html += "               </tr>";
                    html += "           </table>";
                }
                html += "       </td>";
                html += "   </tr>";

                if (data[i].notepadCount != "0") {
                    html += "<tr><td name='notepad' tag='" + data[i].order_code + "' colspan=7 style='background:white'></td></tr>";
                }
                html += "</table></div>";

            }

            $('#order_list').html(html);


            //
            // initial Style
            initListStyle();
            loadOrderNotepad();

            GetEBayLogoHref();

            getEbayOrderProductName();

            $('.paypalTag').each(function () {
                var _this = $(this);
                var oc = _this.attr('data-order-code');
                $.get('orders_cmd.aspx', { order_code: oc, cmd: 'getOrderPaypalRecordAMT' }, function (data) {
                    var color = data == "0.00" ? "red" : "green";
                    _this.after('<span style="color:' + color + ';">&nbsp;&nbsp;&nbsp;&nbsp;(' + data + ')</span>');
                });
            });
        }

        function loadOrderNotepad(Oid) {

            if (typeof (Oid) == "string" && Oid != "") {
                var exist = false;
                $('td[name=notepad]').each(function () {
                    var the = $(this);

                    var orderid = the.attr("tag");
                    if (Oid == orderid)
                        exist = true;
                });
                if (!exist)
                    $("<tr><td name='notepad' tag='" + Oid + "' colspan=7 style='background:white'></td></tr>").appendTo($('#table_' + Oid));
            }

            $('td[name=notepad]').each(function () {
                var the = $(this);

                var orderid = the.attr("tag");

                if (Oid == null || Number(Oid) == Number(orderid)) {
                    the.html("...");
                    $.ajax({
                        url: "orders_cmd.aspx?cmd=getOrderNotepad&OrderID=" + orderid + "&d=" + rnd()
                           , type: "get"
                           , data: ""
                           , success: function (msg) {
                               the.html("<a href='/q_admin/orders_edit_detail_notepad.aspx?order_code=" + orderid + "' onclick=\"ShowIframe('Order (" + orderid + ") Notepad',this.href,800,450);return false;\">" + msg + "</a>");
                           }
                    });/* */
                }
                // $(this).html($(this).attr("tag"));
            });
        }


        function LoadList() {
            var searchIndex = '<%=request("searchIndex") %>';
            var payMethodId = '<%=request("PayMethodID") %>';
            var orderSource = '<%= request("order_source") %>';
            var outStatus = '<%=request("out_status") %>';
            var keyword = '<%=request("keyword") %>';
            var fieldName = '<%=request("field_name") %>';
            //alert(currPager);
            $.getJSON("/q_admin/orders_cmd.aspx?cmd=getOrderList&pageSize=<%= pageSize %>&PageID=" + currPager + "&searchIndex=" + searchIndex + "&PayMethodID=" + payMethodId + "&order_source=" + orderSource + "&out_status=" + outStatus + "&keyword=" + keyword + "&field_name=" + fieldName + "&d=" + rnd()
                    , {}
                    , function (msg) {
                        LoadListSplit(msg);
                    });
        }

        function getPageCount() {
            pagerCount = 50;
            $("#pager").pager({ pagenumber: currPager, pagecount: pagerCount, buttonClickCallback: PageClick });
        }

        //
        // pager Click Event.
        //
        PageClick = function (pageclickednumber) {
            currPager = pageclickednumber;
            $('#order_list').html("<div style=\"line-height:30px; text-align:center; padding:5em;\">Loading...</div>");
            LoadList();
            $("#pager").pager({ pagenumber: pageclickednumber, pagecount: pagerCount, buttonClickCallback: PageClick });
            //$("#result").html("Clicked Page " + pageclickednumber);
        }


        //
        // init list style.
        //
        function initListStyle() {
            $('#tr').find('td').css({
                "font-weight": "bold"
                            , "text-align": "center"
                            , "background": "#DAB5A2"
            });
            var bgc = "";       //  Row Background.
            var statusBgc = ""; //  pre status Background

            $('#tab_order_list tr').hover(
                            function (e) {
                                bgc = $(this).find('td').css("background");

                                $(this).find('td').each(function () {
                                    if ($(this).attr('name') == 'fre_name')
                                        statusBgc = $(this).css("background");
                                });
                                $(this).find('td').css({ "background": "#e0e0e0" });

                            }
                            , function (e) {
                                //alert($(e).html());
                                $(this).find('td').css({ "background": bgc });
                                $(this).find('td').each(function () {
                                    if ($(this).attr('name') == 'fre_name')
                                        $(this).css("background", statusBgc);
                                });


                            }).each(function (i) {

                                if (i % 2 == 0) {
                                    if ($(this).attr('id') != 'tr') {
                                        $(this).find('td').each(function () {
                                            if ($(this).attr('name') != 'fre_name')
                                                $(this).css({ "background": "#f2f2f2" });
                                        });
                                    }
                                }
                                $(this).find('td').css({ "border-bottom": "1px solid #ccc" });
                            });
            $('td[name=td_group_total]').css({ "text-align": "right" });
            $('td[name=td_balance]').css({ "text-align": "right" });
            $('td[name=td_price_unit]').each(function () {
                if ($(this).html() == "USD")
                    $(this).css("color", "blue");
            });
        }

        //
        //
        //
        function getE(order_code, order_source) {
            if (order_source != 3 && order_code.length > 2) {
                return "<a href=\"orders_edit_detail_selected.aspx?menu_id=2&order_code=" + order_code + "\">Edit</a>"
            }
            else {
                return "<span name='ebayLogoArea' title='" + order_code + "'><img src='/soft_img/app/ebay_logo.jpg' border='0'></span><br><a href=\"orders_edit_detail_selected.aspx?menu_id=2&order_code=" + order_code + "\">Edit</a>";
            }
        }

        function GetEBayLogoHref() {
            $('span[name=ebayLogoArea]').each(function () {
                var the = $(this);
                $.ajax({
                    url: "orders_cmd.aspx"
                    , type: "get"
                    , data: { "cmd": "GetEbayItemIDByOrderCode", "OrderID": the.attr('title') }
                    , success: function (msg) {

                        if (msg.indexOf("error:") != -1)
                            alert(msg);
                        else
                            the.html("<a href='http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + msg + "' target='_blank'><img src='/soft_img/app/ebay_logo.jpg' border='0'></a>");
                    }
                    , error: function (msg) { alert(msg); }
                });
            });
        }

        //
        // get Order Detail href
        //
        function getOrderDetail(order_code, order_source, order_invoice, is_show_order_code, order_date) {
            if (order_date.indexOf('2009') != -1)
                return "";

            if (!is_show_order_code) {
                // if(order_source != 3)
                //  return "<span onclick=\"OpenOrderDetail('" + order_code + "')\" style=\"cursor:pointer\">"+ order_invoice +"</span>";
                //  else
                return "<span onclick=\"OpenOrderDetail('" + order_code + "')\" style=\"cursor:pointer\">" + order_invoice + "</span>";
            }
            else {
                // if(order_source != 3)
                return "<span onclick=\"OpenOrderDetail('" + order_code + "')\" style=\"cursor:pointer\">" + order_code + "</span>";
                // else
                //     return "<span onclick=\"winOpen('orders_ebay_view.aspx?sales_record_number=" + order_invoice + "', 'ebay_view', 720, 700, 120, 200);\" style=\"cursor:pointer\">"+ order_code +"</span>";
            }
        }
        //
        // get Name Href String.
        //
        function getNameHref(customer_serial_no, name) {
            return "<a href='#'  onclick=\"winOpen('sales_customer_history.aspx?customer_id=" + customer_serial_no + "','order_history', 1000, 600, 300, 300);return false;\">" + name + "</a>";
        }

        //
        //  get Paid PNG
        //
        function getPaidStatusString(order_pay_status_id, order_code) {
            if (order_pay_status_id == 1)
                return "";
            if (order_pay_status_id == 2)
                return "<span class='paypalTag' data-order-code='" + order_code + "' style=\"width: 16px; height: 16px; background: url('/soft_img/tags/(15,47).png'); \" >&nbsp;&nbsp;&nbsp;&nbsp;</span>";
            if (order_pay_status_id == 3)
                return "<span style=\"width: 16px; height: 16px; background: url('/soft_img/tags/(31,36).png'); \">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
            if (order_pay_status_id == 4)
                return "<span onclick=\"winOpen('order_paypal_error_info.aspx?order_code=" + order_code + "', 'paypal_error', 420, 400, 120, 200);\" style=\"cursor : pointer; width: 16px; height: 16px; background: url('/soft_img/tags/(14,45).png'); \">&nbsp;&nbsp;&nbsp;&nbsp;</span>";

            return "";
        }
        //
        // get Order Source name.
        //
        function getOrderSourceName(order_source) {
            if (order_source == 1)
                return "WebSite";
            if (order_source == 2)
                return "Input";
            if (order_source == 3)
                return "eBay";
            return "None";
        }

        //
        // get U Href
        //
        function getUHref(str, orderID) {
            return "<a href='#' onclick=\"clickUButton($(this));return false;\"><span id='web_status_" + orderID + "'>" + str + "</span></a>";
        }
        //
        // click U 
        // 
        function clickUButton(e) {
            //$(this).offset().top
            var orderID = '';
            var orderCode = "";
            var orderFriID = "";    //  FRT STAT
            var orderBakID = "";    //  out status 

            e.parent().parent().find('td').each(function () {
                if ($(this).attr('name') == 'up_btn') {
                    orderID = $(this).attr('id');
                    orderCode = $(this).attr('tag');
                }
                if ($(this).attr('name') == "fre_name") {
                    orderFriID = $(this).attr('tag');
                }
                //        if($(this).attr('name') =="bak_name")
                //        {
                //            orderBakID = $(this).attr('tag');
                //        }
            });
            $('#td_curr_order_code').html(orderCode).css("font-weight", "bold");
            $('#td_curr_order_id').html(orderID).css("font-weight", "bold");
            //    $('select[name=bk_stat]').html("<option>Loading...</option>");
            $('select[name=frt_stat]').html("<option>Loading...</option>");

            getFriStat(orderFriID);
            //    getBKStat(orderBakID);
            var top = ($(window).height() - $('#editArea').height()) / 2;
            var left = ($(window).width() - $('#editArea').width()) / 2;
            var scrollTop = $(document).scrollTop();
            var scrollLeft = $(document).scrollLeft();
            $('#editArea').css({ 'top': top + scrollTop - 50, 'left': left + scrollLeft, 'display': '' });
        }

        //
        // get 
        //
        function getFriStat(currID) {
            $.ajax({
                url: "orders_cmd.aspx"
                , type: "get"
                , data: { "cmd": "get_frt_stat", "currentID": currID }
                , success: function (msg) {
                    if (msg.indexOf("error:") != -1)
                        alert(msg);
                    else
                        $('select[name=frt_stat]').html(msg);
                }
                , error: function (msg) { alert(msg); }
            });
        }

        //
        // get 
        //
        function getBKStat(currID) {
            $.ajax({
                url: "orders_cmd.aspx"
                , type: "get"
                , data: { "cmd": "get_bak_stat", "currentID": currID }
                , success: function (msg) {
                    if (msg.indexOf("error:") != -1)
                        alert(msg);
                    else {
                        //alert(currID);
                        $('select[name=bk_stat]').html(msg);
                        //alert(msg);
                    }
                }
                , error: function (msg) { alert(msg); }
            });
        }
        //
        // Save Order Status
        //
        function saveStatus(e) {
            //    var bk_stat = $('select[name=bk_stat]').val();
            var frt_stat = $('select[name=frt_stat]').val();
            var oid = $('#td_curr_order_id').html();
            var bk_name = "";
            var frt_name = "";
            var frt_bgC = "";
            $.ajax({
                url: "orders_cmd.aspx"
                , type: "get"
                , data: { "cmd": "save_order_status", "OrderID": oid, "frt_stat": frt_stat }
                , success: function (msg) {

                    if (msg.indexOf("error:") != -1) {
                        alert(msg);
                    }
                    else {
                        frt_stat = msg.split("|")[0];
                        $('select[name=frt_stat]').find('option').each(function () {
                            if (parseInt($(this).attr("value")) == parseInt(frt_stat)) {
                                frt_name = $(this).html();
                                frt_bgC = $(this).css('background');
                            }
                        });
                        $('#web_status_' + oid).html(frt_name);
                        //                if( frt_bgC.indexOf('LightGreen')>-1)
                        //                    $('#web_status_'+ oid).css('color','red');
                        if (frt_bgC.indexOf("LightGreen") > -1) {
                            $('div[title=' + oid + ']').attr('class', "tableParent2");
                            $('tr[title=' + oid + ']').attr('class', "row2");
                        }
                        else {
                            $('div[title=' + oid + ']').attr('class', "tableParent1");
                            $('tr[title=' + oid + ']').attr('class', "row1");
                        }

                    }

                    $('#editArea').css('display', 'none');
                }
                , error: function (msg) { alert(msg); }
            });
        }

        function changeBgLineColor(oid, frt_name, isFinishedColor) {
            $('#web_status_' + oid).html(frt_name);
            if (isFinishedColor) {
                $('div[title=' + oid + ']').attr('class', "tableParent2");
                $('tr[title=' + oid + ']').attr('class', "row2");
            }
            else {
                $('div[title=' + oid + ']').attr('class', "tableParent1");
                $('tr[title=' + oid + ']').attr('class', "row1");
            }
        }

        //function ChangeParentAreaBgcolor()
        //{
        //    $('
        //}
        // 完成的订单标题背景色为灰色
        //function ChangeParentAreaBgcolor(orderCode, className)
        //{
        //    $('div[title='+orderCode+']').attr('class', className);
        //}

        var currBgColor = "";
        function listOut(the) {
            the.css({ "background": currBgColor });
        }

        function listOver(the) {
            currBgColor = the.css("background");
            the.css({ "background": "green" });
        }

        function newOrder(customerID) {
            if (!confirm("Are you create new order?"))
                return;
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                , data: "cmd=createNewByCustomerID&customerID=" + customerID
                , type: "get"
                , success: function (msg) {
                    //alert(msg.substr(0,6));
                    if (msg.indexOf("OK") > 0)
                        window.location.href = '/q_admin/orders_edit_detail_selected.aspx?order_code=' + msg.substr(0, 6);
                        ///q_admin/orders_edit_detail.aspx?order_code=" + OrderCode.ToString() + "&order_source=" + this.ddl_order_source.SelectedValue.ToString() +
                    else
                        alert(msg);
                }
                , error: function (msg) { alert(msg); }
            });
        }

        function getEbayOrderProductName() {
            $('span[name=prodName]').each(function () {
                var the = $(this);
                $.ajax({
                    url: "/q_admin/orders_cmd.aspx"
                        , data: "cmd=getEbayOrderProductName&OrderID=" + the.attr("tag")
                        , type: "get"
                        , success: function (msg) {
                            the.html(msg);
                        }
                        , error: function (msg) { alert(msg); }
                });
            });
        }

        function getEBayRefresh(orderCode) {
            return "<a href='' onclick=\"winOpen('netcmd/UploadEbayOrderInfo.aspx?sku=" + orderCode + "','order_getinfo', 300, 300, 300, 300);return false;\"><img src='../soft_img/app/(26,13).png' style='border=0;'></a>";
        }

        function changePageSize(the) {
            var url = location.href;
            var paraString = url.substring(url.indexOf("?") + 1, url.length).split("&");
            //alert(url.replace("pageSize=", ""));
            if (url.indexOf("pageSize=") > -1) {
                location.href = url.replace("pageSize=", "pp=") + "&pageSize=" + the.val();
            }
            else {
                if (url.indexOf("?") > -1)
                    location.href = url + "&pageSize=" + the.val();
                else
                    location.href = url + "?pageSize=" + the.val();
            }
        }

        function showStore(ordercode) {
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                        , data: "cmd=getOrderListStoreCodes&order_code="
                        , type: "get"
                        , success: function (msg) {
                            showOrderListSelected(msg);
                        }
                        , error: function (msg) { alert(msg); }
            });
        }

        function clearOrders(the) {
            var order_code = the.parent().text();
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                       , data: "cmd=removeOrderListStoreCodes&order_code=" + order_code
                       , type: "get"
                       , success: function (msg) {
                           showOrderListSelected(msg);
                       }
                       , error: function (msg) { alert(msg); }
            });

        }
        function addOrders(order_code) {
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                       , data: "cmd=setOrderListStoreCodes&order_code=" + order_code
                       , type: "get"
                       , success: function (msg) {
                           showOrderListSelected(msg);
                       }
                       , error: function (msg) { alert(msg); }
            });
        }
        function showOrderListSelected(msg) {
            var arr = msg.split('|');
            var str = "<input type=\"button\" value=\"View\" onclick=\"goViewOrderPartList($(this));\"/>";
            for (var i = 0; i < arr.length; i++) {
                str += "<div>" + arr[i] + "<img src=\"../soft_img/tags/(02,41).png\" onclick=\"clearOrders($(this));\" /></div>"

            }

            if (msg != "") {
                $("#showInStoreArea").css("display", "inline");
            }
            else
                $("#showInStoreArea").css("display", "none");
            $("#showInStoreArea").html(str);
        }

        function goViewOrderPartList(the) {
            var codes = "";
            $("#showInStoreArea").find("div").each(function () {
                codes += "|" + $(this).text();
            });
            //alert(codes);
            winOpen("orders_list_new_view_for_instore.asp?codes=" + codes, 'order_part_view', 1100, 600, 300, 300);
        }
    </script>

</body>
</html>
