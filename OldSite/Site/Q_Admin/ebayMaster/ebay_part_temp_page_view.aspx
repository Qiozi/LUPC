<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ebay_part_temp_page_view.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_part_temp_page_view" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head id="Head1" runat="server">
    <meta http-equiv="Content-Language" content="utf-8" />
    <title>Untitled Page</title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery.float.js"></script>
</head>
<body>
    &nbsp;<br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <div id='shippingArea'>
    </div>
    <div>
        <b id="ebayPrice"></b>
    </div>
    <div id="btn_cmd_list" style="width: 100%; line-height: 30px; background: #f2f2f2; border-bottom: 1px solid #cccccc; text-align: center">
        <input type="button" onclick="Down()" value="View Source Code" /><textarea id="content" rows='2' cols='50'></textarea>
    </div>

    <form id="form1" runat="server">

        <div>
            <asp:Literal runat="server" ID="literal_page" EnableViewState="false"></asp:Literal>
            <asp:Literal ID="Literal1" runat="server" EnableViewState="false"></asp:Literal>
        </div>
    </form>

    <script type="text/javascript">
        $().ready(function () {
            $("#btn_cmd_list").floatdiv("lefttop");
            $('#shippingArea').load("/q_admin/ebayMaster/Online/ModifyOnlineShippingFee.aspx?sku=<%= ReqSku %>&isclose=0", "", "");
            GetNewBuyItNow("<%= ReqSku %>");
        });

        function Down() {
            $('#content').val($('#form1').html());
        }

        function GetNewBuyItNow(luc_sku) {

            $.ajax({
                type: "get",
                url: "/q_admin/ebayMaster/ebay_notebook_get_ebayPrice.aspx",
                data: "OnlyEbayPrice=1&LUC_Sku=" + luc_sku,
                success: function (msg) {
                    $('#ebayPrice').html("eBay Price: " + msg);
                },
                error: function (msg) { $('#ebayPrice').html("eBay Price: " + msg); }
            });
        }
    </script>
</body>
</html>
