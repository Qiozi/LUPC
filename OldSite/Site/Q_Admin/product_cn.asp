<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>china list</title>
    <script src="../js_css/jquery-1.9.1.js" type="text/javascript"></script>
    <style>
     td{ font-size:9pt; background:#fff;}
     th{ background:#f2f2f2;}
     .price{ text-align:right }
     .txtCenter { text-align:center;}
    </style>
</head>
<body>
<%

    dim sku
    sku = SQLescape(request("sku"))

    dim hide, hideSku
    hide = SQLescape(request("hide"))
    hideSku = SQLescape(request("hideSku"))

    dim rate, curr_rate
    curr_rate = 0
    rate = SQLescape(request("rate"))

    dim newPrice, newSku
    newPrice= SQLescape(request("newPrice"))
    newSku  = SQLescape(request("newSku"))

    if newPrice <> "" and newSku <> "" then
        conn.execute("Update tb_product_cn set price='"+ newPrice +"', regdate=now() where luc_sku='"+ newSku +"'")

    end if

    if hideSku <> "" and hide = "1" then 
        conn.execute("update tb_product_cn set showit=0 where luc_sku='"& hideSku &"'")
    end if

    if rate <> "" then
        conn.execute("update tb_currency_convert_cn set is_current=0")
        conn.execute("insert into tb_currency_convert_cn(currency_cn, currency_ca, is_current, is_auto, regdate) values ('"&rate&"','1', '1', '1',now())")
    end if

    set rs = conn.execute("select * from tb_currency_convert_cn where is_current=1 limit 1")
    if not rs.eof then
        curr_rate = rs("currency_cn")
    end if
    rs.close : set rs = nothing
    
    if (sku <> "" ) then
        set rs = conn.execute("Select count(id) from tb_product_cn where luc_sku='"& sku &"'")
        if not rs.eof then
            if(rs(0) = 0)then
                conn.execute("insert into tb_product_cn (luc_sku, price,showit, regdate) values ('"& sku &"', 0, 1,now()) ")
            else
                conn.execute("update tb_product_cn set  price=0, regdate=now(), showit=1 where luc_sku='"& sku &"' ")
            end if
        end if
        rs.close : set rs = nothing
        
    end if

 %>
1CAD  = <input type="text" id="rate" value="<%= curr_rate %>"/> <input type="button" value="Save"  onclick="window.location.href='product_cn.asp?rate='+$('#rate').val();" /> 
<input type="text" id="inputSKU" /> <input type="button" value="Add to list" onclick="window.location.href='product_cn.asp?sku='+$('#inputSKU').val();" />
<b>/* only notebook */</b>
<hr size="1" />

<div style="background:#ccc;">
<table cellpadding="3" cellspacing="1" width="100%">
    <thead>
        <tr>
            <th>SKU</th>
            <th>eBay Name</th>
            <th>eBay Itemid</th>
            <th>Curr eBay Price</th>
            <th>eBay Price</th>
            <th>Cost</th>
            <th>Bank Fee</th>
            <th>Adjustment</th>
            <th>Shipping Fee</th>
            <th>Profit</th>
            <th>eBay Fee</th>
            <th>Wholesaler</th>
            <th>Wholesaler Cost</th>
            <th>Wholesaler Qty</th>
            <th>China Price New</th>
            <th>China Price Current</th>
            <th>CMD</th>
        </tr>
    </thead>
<% 

    set rs = conn.execute("Select * from tb_product_cn where showit=1 order by showit desc ")
    if not rs.eof then
        do while not rs.eof
%>
       <tr>
            <td class="sku" sku='<%= rs("luc_sku") %>'><%= rs("luc_sku") %></td>            
            <td class="eBay_name">...</td>            
            <td class="eBay_Itemid"></td>
            <td class="Curr_eBay_Price price"></td>
            <td class="eBay_Price price"></td>
            <td class="Cost price"></td>
            <td class="Bank_Fee price"></td>
            <td class="Adjustment price"></td>
            <td class="Shipping_Fee price"></td>
            <td class="Profit price"></td>
            <td class="eBay_Fee price"></td>
            <td class="Wholesaler txtCenter"></td>
            <td class="Wholesaler_Cost price"></td>
            <td class="Wholesaler_Qty txtCenter"></td>
            <td class="China_Price_New price"></td>
            <td class="China_Price_Current price"></td>
            <td class="cmd"></td>
       </tr>     
<%

        rs.movenext
        loop
%>
<%
    else
 %>
   <tr>
        <td>no data.</td>
   </tr> 
<%
    end if
    rs.close : set rs = nothing

closeconn()
%>

</table>
</div>
<script language="javascript">
    $(document).ready(function () {
        var skus = [];
        $('.sku').each(function (i) {
            skus[i] = $(this).text();
        });

        function runGet(index) {
            if (index >= skus.length)
                return;

            var curr_obj = $('td[sku=' + skus[index] + ']');
            $(curr_obj).parent().find('.eBay_name').eq(0).html("......");
            var sku = skus[index];
            $.ajax({
                type: "get",
                url: "/q_admin/inc/get_ebay_price_info.aspx",
                data: { sku: skus[index], cmd: 'getChinaPrice' },
                error: function (s, r, t) { curr_obj.css({ 'color': 'red' }); runGet(index + 1); },
                success: function (m, s) {
                    curr_obj.css({ 'color': 'green' });
                    runGet(index + 1);
                    var item = eval("(" + m + ")");
                    $(curr_obj).parent().find('.eBay_name').eq(0).html(item.eBay_name);
                    $(curr_obj).parent().find('.eBay_Itemid').eq(0).html("<a href=\"http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + item.eBay_Itemid + "\" target='_blank'>" + item.eBay_Itemid + "</a>");
                    $(curr_obj).parent().find('.Curr_eBay_Price').eq(0).html(item.Curr_eBay_Price);
                    $(curr_obj).parent().find('.eBay_Price').eq(0).html(item.eBay_Price);
                    $(curr_obj).parent().find('.Cost').eq(0).html(item.Cost);
                    $(curr_obj).parent().find('.Bank_Fee').eq(0).html(item.Bank_Fee);
                    $(curr_obj).parent().find('.Adjustment').eq(0).html(item.Adjustment);
                    $(curr_obj).parent().find('.Shipping_Fee').eq(0).html(item.Shipping_Fee);
                    $(curr_obj).parent().find('.Profit').eq(0).html(item.Profit);
                    $(curr_obj).parent().find('.eBay_Fee').eq(0).html(item.eBay_Fee);
                    $(curr_obj).parent().find('.Wholesaler').eq(0).html(item.Wholesaler);
                    $(curr_obj).parent().find('.Wholesaler_Cost').eq(0).html(item.Wholesaler_Cost);
                    $(curr_obj).parent().find('.Wholesaler_Qty').eq(0).html(item.Wholesaler_Qty);
                    $(curr_obj).parent().find('.China_Price_New').eq(0).html("<input type='text' value='" + item.China_Price_New + "' size='7' ><input type='button' value='S' onclick=\"window.location.href='product_cn.asp?newPrice=" + item.China_Price_New + "&newSku=" + skus[index] + "';\">");
                    $(curr_obj).parent().find('.China_Price_Current').eq(0).html(item.China_Price_Current);
                    if (item.China_Price_New != item.China_Price_Current)
                        $(curr_obj).parent().find('.China_Price_Current').eq(0).css({ 'color': 'red' });
                    $(curr_obj).parent().find('.cmd').eq(0).html("<a href='product_cn.asp?hideSku=" + skus[index] + "&hide=1'>Hide</a>");

                }
            });

        }

        runGet(0);
    });
</script>
</body>
</html>
