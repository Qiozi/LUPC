<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<% response.clear %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ebay part list</title>
    <script type="text/javascript" src="../../js_css/jquery-1.9.1.js"></script>
    <style>
        td{ padding:3px;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.switch').click(function () {
                var the = $(this);
                var lusku = prompt("please type LU sku", "");
                var etcItemid = the.parent().next().html();
                if (lusku.length > 3) {
                    $.ajax({
                        type: "get",
                        url: "ebay_system_cmd.aspx",
                        data: "cmd=matchLUAndETCNotebook&luc_sku=" + lusku + "&etcitemid=" + etcItemid,
                        error: function (r, s, t) { },
                        success: function (msg) {
                            the.css({ 'color': 'green' });
                        }
                    });
                }
            });

            $('.luprice').each(function () {
                var the = $(this);
                if (the.attr("sku").length > 3) {
                    $.ajax({
                        type: "get",
                        url: "ebay_notebook_get_ebayPrice.aspx",
                        data: "LUC_Sku=" + the.attr('sku') + "&OnlyEbayPrice=0",
                        error: function (r, s, t) {
                            alert(s);
                        },
                        success: function (msg, s) {
                            //var m = msg.ebayPrice;
                            //the.html(msg.toString());
                            //                            var shippingFee = msg.shipping_fee;
                            //                            var profit = msg.profit;
                            //                       
                            //                            var etcprice = parseFloat(the.parent().find('.etcprice').eq(0).html());
                            //                            var diff = etcprice - parseFloat(m) - parseFloat(shippingFee);
                            //                            the.next().html(diff);
                            //                            if (diff < 0) {
                            //                                the.next().css({ "color": "red" });
                            //                            } else {
                            //                                the.next().css({ "color": "green" });
                            //                            }
                            var json = eval(msg);
                            $.each(json, function (idx, item) {
                                var m = item.ebayPrice;
                                var shippingFee = item.shipping_fee;
                                var profit = item.profit;

                                var luprice = parseFloat(m) - parseFloat(shippingFee);
                                the.html(luprice.toFixed(2));
                               
                                the.parent().find('.shippingfee').eq(0).html(shippingFee);
                                the.parent().find('.profit').eq(0).html(profit);
                                //the.parent().find('.etcprice').eq(0).html()

                                var etcprice = parseFloat(the.parent().find('.etcprice').eq(0).html());
                                var diff = etcprice - luprice;
                                the.next().html(diff.toFixed(2));
                                if (diff < 0) {
                                    the.next().css({ "color": "red" });
                                } else {
                                    the.next().css({ "color": "green" });
                                }
                            });
                        }
                    });
                }
                else {
                    the.html("");
                }
            });

            $('.switchMatch').click(function () {
                var the = $(this);
                var lusku = prompt("please type LU sku", the.parent().parent().find('.lusku').eq(0).html());
                var etcItemid = the.parent().next().html();
                if (lusku.length > 3) {
                    $.ajax({
                        type: "get",
                        url: "ebay_system_cmd.aspx",
                        data: "cmd=matchLUAndETCNotebook&luc_sku=" + lusku + "&etcitemid=" + etcItemid,
                        error: function (r, s, t) { },
                        success: function (msg) {
                            the.css({ 'color': 'green' });
                        }
                    });
                }
            });

            $('tr').hover(
	 		function () {
	 		    $(this).find("td").css("border-bottom", "1px solid #999999").css("padding-bottom", "0px");
	 		}
	  	  , function () {
	  	      $(this).find("td").css("border-bottom", "0px solid #000000").css("padding-bottom", "1px");
	  	  }

	  )
      .each(function (i) {
          if (i % 2 == 1) {
              $(this).find("td").css("background", "#ffffff").css("font-size", "8pt").css("padding-bottom", "1px");

          } else {
              $(this).find("td").css("background", "#f2f2f2").css("font-size", "8pt").css("padding-bottom", "1px");
          }
      });


        });
    </script>
</head>
<body style="padding:20px; font-size: 10pt; line-height: 20px;">
    <a href="?cmd=match">Match</a>
     <a href="etc_notebook_list.asp">List</a>
    <%
    if request("cmd") <> "match" then 
        
        set rs = conn.execute("Select e.*, p.product_ebay_name, es.itemid lucItemid from tb_ebay_etc_items e left join tb_product p on p.product_Serial_no=e.LUC_eBay_Sys_Sku "&_
                            "   left join tb_ebay_selling es on es.luc_sku=p.product_serial_no "&_
                            "  where itemtype='N' order by itemtitle asc")
        if not rs.eof then 
            response.Write ("<table>")
                do while not rs.eof 
                    response.Write "<tr>"
                    response.write "    <td> <a class='switch'>switch</a> </td>"&vblf
                    response.Write "    <td onclick=''>" & rs("itemid") &"</td>"&vblf
                    response.Write "    <td>" & rs("itemtitle") & "</td>"&vblf
                    response.write "    <td class='etcprice'>" & rs("itemprice") & "</td>"&vblf
                    response.write "    <td>" & rs("LUC_eBay_Sys_Sku") & "</td>"&vblf
                    response.Write "    <td>" & rs("product_ebay_name") & "</td>"&vblf
                    response.Write "    <td class='luprice' sku='"& rs("LUC_eBay_Sys_Sku") &"'>...</td>"&vblf
                    response.Write "    <td></td>"&vblf
                    response.Write "    <td class='shippingfee' title='Shipping Fee' sku='"& rs("LUC_eBay_Sys_Sku") &"'>...</td>"&vblf
                    response.Write "    <td class='profit' title='profit' sku='"& rs("LUC_eBay_Sys_Sku") &"'>...</td>"&vblf
                    if len(rs("lucItemid")) >10 then  
                        response.Write "    <td><a href=""http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item="&rs("lucItemid")&""" target=""_blank""><img src=""/soft_img/app/ebay_logo.jpg"" border=""0""></a></td>"&vblf
                    else
                        response.Write "    <td></td>"&vblf
                    end if
                    response.Write "</tr>"
                rs.movenext
                loop
            response.Write ("</table>")
        else
            response.Write "No data."
        end if
        rs.close : set rs = nothing

    else
        set rs = conn.execute("select p.product_serial_no, p.product_ebay_name, p.manufacturer_part_number, es.itemid from tb_product p left join tb_ebay_selling es on es.luc_sku=p.product_serial_no where p.product_serial_no>0 and menu_child_serial_no=350 order by product_ebay_name asc")
        if not rs.eof then 
            response.Write ("<table>")
                do while not rs.eof 
                    set srs = conn.execute("Select * from tb_ebay_etc_items where itemtitle like '%"& rs("manufacturer_part_number") &"%' and (LUC_eBay_Sys_Sku = '' or LUC_eBay_Sys_Sku is null) limit 1")
                    if not srs.eof then 
                        response.Write "<tr>"
                        response.write "    <td> <a class='switchMatch'>switch</a> </td>"&vblf
                        response.Write "    <td>" & srs("itemid") &"</td>"&vblf
                        response.Write "    <td><b>" & rs("manufacturer_part_number") & "<b></td>"&vblf
                        response.Write "    <td>" & replace(srs("itemtitle"), rs("manufacturer_part_number"), "<b>"&rs("manufacturer_part_number")&"</b>") & "</td>"&vblf
                        response.write "    <td class='etcprice'>" & srs("itemprice") & "</td>"&vblf
                        response.write "    <td class='lusku'>" & rs("product_serial_no") & "</td>"&vblf
                        
                        response.Write "    <td></td>"&vblf
                        response.Write "</tr>"
                    end if
                rs.movenext
                loop
            response.Write ("</table>")
        else
            response.Write "No data."
        end if
        rs.close : set rs = nothing

    end if
    closeconn() %>

</body>
</html>
