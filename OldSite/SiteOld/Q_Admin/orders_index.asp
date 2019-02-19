<!--#include virtual="site/inc/validate_admin.asp"-->
<html>
<head>
    <script type="text/javascript" src="/q_admin/js/winOpen.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery.float.js"></script>
    <title>Order Index</title>
    <link href="/App_Themes/default/admin.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <table style="width: 100%;" border="0">
        <tr>
            <td style="width: 285px;">
                <div>
                    <input type="text" name="keyword" style="width: 120px;" />

                    <select name="select_field" style="width: 100px;">
                        <option value="-1">Select</option>
                        <option value="order_invoice">Invoice</option>
                        <option value="oh.order_code">Order#</option>
                        <option value="cs.customer_serial_no">CUT#</option>
                        <option value="customer_shipping_first_name">First Name</option>
                        <option value="customer_shipping_last_name">Last Name</option>
                        <option value="luc_sku">LUC SKU</option>
                        <option value="eBay_Userid">eBay Userid</option>
                        <option value="eBay_phone">eBay phone</option>
                        <option value="cs.customer_company">Company</option>
                    </select>

                    <input type="button" value="Go" onclick='goSelectField();' />
                </div>
                <div>
                    <input type="text" name="sdf" style="border: 3px solid #ffffff; width: 120px;" />
                    <select name="out_status" style="width: 100px">
                        <option value='-1'>Select</option>
                    </select>
                    <input type="button" value="Go" onclick='goOutStatus();' />
                </div>
                <div>
                    <select name="order_source" style="width: 120px;">
                        <option value="0,1,2">LUWeb, Input, None</option>
                        <option value="0,1,2,3" selected="selected">ALL</option>
                        <option value="1">LUWeb</option>
                        <option value="2">Input</option>
                        <option value="3">Ebay</option>
                    </select>
                    <select name="pay_method" style="width: 100px">
                        <option value='-1'>Select</option>

                    </select>
                    <input type="button" value="Go" onclick='goPaymethod();' />
                </div>
            </td>
            <td style="width: 70px">
                <input type="button" id="btn_clear_search" onclick='clearList();' value="Clear Search"
                    style="width: 90px; height: 74px;" />
            </td>
            <td style="width: 90px">
                <input type="button" id="Button1" value="Only View Me"
                    onclick='onlyShowMeOrder();'
                    style="width: 90px; height: 74px;" />

            </td>
            <td style="width: 90px">
                <input type="button" id="btn_default_order" value="General Order"
                    onclick='newDefaultOrder();'
                    style="width: 90px; height: 74px;" />

            </td>
            <td valign="top">
                <input type="button" value="New Orders"
                    onclick="document.getElementById('iframe1').src = 'orders_add_customer.aspx';"
                    style="width: 90px; height: 74px;" />
                <input type="button" value="Customers" onclick="document.getElementById('iframe1').src = 'orders_customer_list.aspx'"
                    style="width: 90px; height: 74px;" />
                <input type="button" value="Void Orders" onclick="document.getElementById('iframe1').src = 'sale_order_list_invalid.aspx'"
                    style="width: 90px; height: 74px;" />
                <input type="button" value="Paypal Payment" onclick="document.getElementById('iframe1').src = 'paypal_card_do_direct_payment.asp'"
                    style="width: 90px; height: 74px;" />
                <input type="button" value="Import Ebay" onclick="window.open('netcmd/UploadEbayOrderInfo.aspx?sku=all', 'order_getinfo', 'height=100,width=400,top=300,left=300,toolbar=no,menubar=no,scrollbars=no, resizable=no,location=no, status=no'); return false;"
                    style="width: 90px; height: 74px;" />
                <input type="button" value="paypal Transaction detail" onclick="document.getElementById('iframe1').src = 'paypal_transaction_detail.asp'"
                    style="width: 130px; height: 74px;" />
                <input type="button" value="eBay Product" title="match ebay product" onclick="matchEBayProduct();"
                    style="height: 74px;" />

            </td>
            <td>
                <!--<iframe src="/q_admin/orders_list_timer.aspx" id="iframe_timer" name="iframe_timer" style="width: 150px; height: 74px; " frameborder="0"></iframe>-->
            </td>
        </tr>
    </table>

    <!-- list area -->
    <iframe src="/q_admin/orders_list_new.asp" id="iframe1" name="iframe1" frameborder="0" style="width: 100%; height: 300px; border-top: 1px solid #ccc"></iframe>
    <!-- list area end -->

    <script type="text/javascript">

        $().ready(function () {

            // window resize
            var _attr = parseInt(document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight);

            $('#iframe1').css("height", isNaN(_attr) || _attr <= 87 ? "100%" : (_attr - 87) + "px");
            initStyle();
            getOutStatusDTC();
            getPaymethodDTC();


        });
        var resizeTimer = null;

        $(window).bind("resize", function () {

            if (resizeTimer)
                clearTimeout(resizeTimer);

            resizeTimer = setTimeout(function () {
                var _attr = parseInt(document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight);

                // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";
                $('#iframe1').css("height", isNaN(_attr) || _attr <= 87 ? "100%" : (_attr - 87) + "px");

            }, 100);
        });


        //
        // initial Style.
        //
        function initStyle() {
            $('input[value=Go]').css({ "margin-right": "10px" });
        }
        //
        //out_status
        //
        function getOutStatusDTC() {

            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                , data: { "cmd": "get_out_status_DTC" }
                , type: "post"
                , success: function (msg) {
                    $('select[name=out_status]').html(msg);
                }
                , error: function (msg) { alert(msg); }
            });
        }
        //
        //pay_method
        //
        function getPaymethodDTC() {
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                , data: { "cmd": "get_pay_method_DTC" }
                , type: "post"
                , success: function (msg) {
                    $('select[name=pay_method]').html(msg);
                }
                , error: function (msg) { alert(msg); }
            });
        }
        //
        // clear 
        //
        function clearList() {
            $('input[name=keyword]').val('');
            var key = $('select[name=order_source]').val();

            $('iframe[name=iframe1]').attr('src', '/q_admin/orders_list_new.asp?searchIndex=4&order_source=' + key);
        }

        //
        // only show me Order.
        //
        function onlyShowMeOrder() {
            var key = $('select[name=order_source]').val();
            $('iframe[name=iframe1]').attr('src', '/q_admin/orders_list.asp?searchIndex=5&order_source=' + key);
        }

        //
        // create New Default Order.
        //
        function newDefaultOrder() {
            if (!confirm("Are you sure."))
                return;
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                , data: { "cmd": "create_default_order" }
                , type: "post"
                , success: function (msg) {
                    //alert(msg.substr(0,6));
                    if (msg.indexOf("OK") > 0)
                        $('iframe[name=iframe1]').attr('src', '/q_admin/orders_edit_detail_selected.aspx?order_code=' + msg.substr(0, 6));
                        ///q_admin/orders_edit_detail.aspx?order_code=" + OrderCode.ToString() + "&order_source=" + this.ddl_order_source.SelectedValue.ToString() +
                    else
                        alert(msg);

                }
                , error: function (msg) { alert(msg); }
            });
        }

        //
        // Search by Fields
        //
        function goSelectField() {
            var order_source = $('select[name=order_source]').val();
            var keyword = $('input[name=keyword]').val();
            var field_name = $('select[name=select_field]').val();
            $('iframe[name=iframe1]').attr('src', "/q_admin/orders_list_new.asp?searchIndex=1&keyword=" + keyword + "&field_name=" + field_name + "&order_source=" + order_source);

        }

        //
        // Search by out status
        //
        function goOutStatus() {
            var order_source = $('select[name=order_source]').val();
            var out_status = $('select[name=out_status]').val();

            $('iframe[name=iframe1]').attr('src', "/q_admin/orders_list_new.asp?searchIndex=2&out_status=" + out_status + "&order_source=" + order_source);

        }

        //
        // Search by paymethod
        //
        function goPaymethod() {
            var order_source = $('select[name=order_source]').val();
            var paymethod = $('select[name=pay_method]').val();

            $('iframe[name=iframe1]').attr('src', "/q_admin/orders_list_new.asp?searchIndex=3&PayMethodID=" + paymethod + "&order_source=" + order_source);

        }

        function matchEBayProduct() {
            $.ajax({
                url: "/q_admin/orders_cmd.aspx"
                , data: { "cmd": "matchEBayProduct" }
                , type: "post"
                , success: function (msg) {
                    //alert(msg.substr(0,6));

                    alert("服务器正在执行...");


                }
                , error: function (msg) { alert(msg); }
            });
        }
    </script>
</body>
</html>
