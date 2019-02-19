<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>eBay offline</title>
    <script type="text/javascript" src="../../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../JS/WinOpen.js"></script>
    <style>
        td { font-size:9pt;}
    </style>
    
   <script type="text/javascript">
       $(document).ready(function () {
           var partCount = 0;
           var skuList = new Array();

           $('.olditemid').each(function (e) {
               var the = $(this);
               var sku = the.parent().find('.sku').eq(0).text();
               
               partCount++;
               skuList[e] = sku;
           });
           alert(partCount);

           function getOldItemId(index) {
               if (skuList[index] == undefined)
                   return;
               $('.olditemid').eq(index).css({ "background": "green" });
               $('.olditemid').eq(index).text("....");
               //alert($('.olditemid').eq(index).html());
               $.ajax({
                   type: "get",
                   url: "ebay_cmd.aspx?cmd=getTopOneItemid&sku=" + skuList[index],
                   data: "",
                   error: function (r, t, s) {

                       $('.olditemid').eq(index).text(t);
                       $('.olditemid').eq(index).css({ "background": "red" });
                       getOldItemId(index + 1);
                   },
                   success: function (msg) {
                       $('.olditemid').eq(index).html("<a href=\"http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + msg + "\" target=\"_blank\">" + msg + "</a>");
                       $('.olditemid').eq(index).css({ "background": "white" });
                       getOldItemId(index + 1);
                   }
               });
           }
           function getStock(index) {
               if (skuList[index] == undefined)
                   return;
               $('.stockQty').eq(index).css({ "background": "green" });
               $('.stockQty').eq(index).html("....");
               //alert($('.olditemid').eq(index).html());
               $.ajax({
                   type: "get",
                   url: "ebay_cmd.aspx?cmd=getStockQty&sku=" + skuList[index],
                   data: "",
                   error: function (r, t, s) {

                       $('.stockQty').eq(index).html(t);
                       $('.stockQty').eq(index).css({ "background": "red" });
                       getStock(index + 1);
                   },
                   success: function (msg) {
                       if (parseInt(msg) >= 5) {
                           $('.stockQty').eq(index).text("" + msg + "").css({ "color": "green", "background": "#f2f2f2" });
                       }
                       else
                           $('.stockQty').eq(index).text("" + msg + "").css({ "background": "white" });

                       getStock(index + 1);
                   }
               });
           }

           function getStockText(index) {
               if (skuList[index] == undefined)
                   return;
               $('.stockQtyText').eq(index).css({ "background": "green" });
               $('.stockQtyText').eq(index).text("....");
               //alert($('.olditemid').eq(index).html());
               $.ajax({
                   type: "get",
                   url: "ebay_cmd.aspx?cmd=getStockQtyText&sku=" + skuList[index],
                   data: "",
                   error: function (r, t, s) {

                       $('.stockQtyText').eq(index).text(t).css({ "background": "red" });
                       getStockText(index + 1);
                   },
                   success: function (msg) {
                       $('.stockQtyText').eq(index).html("" + msg + "").css({ "background": "white" });

                       getStockText(index + 1);
                   }
               });
           }

           function getEbayInfoFromEbaySite(index) {
               if (skuList[index] == undefined)
                   return;

               if ("" != $('.ebayCateValue').eq(index).text()) {
                   getEbayInfoFromEbaySite(index + 1);
                   return;
               }
               $('.ebayCateebayCateValueText').eq(index).css({ "background": "green" });
               $('.ebayCateValue').eq(index).text("....");
               //alert($('.olditemid').eq(index).html());
               if ($('.olditemid').eq(index).text().length != 12) {
                   getEbayInfoFromEbaySite(index + 1);
                   return;
               }

               var stock = $('.stockQty').eq(index).html();
               if ($.isNumeric(stock)) {
                   if (parseInt(stock) < 5) {
                       getEbayInfoFromEbaySite(index + 1);
                       return;
                   }
               }
               else {
                   getEbayInfoFromEbaySite(index + 1);
                   return;
               }

               var itemid = $('.olditemid').eq(index).text();
               $.ajax({
                   type: "get",
                   url: "ebay_cmd.aspx?cmd=getEbayItemInfo&sku=" + skuList[index] + "&itemid=" + itemid,
                   data: "",
                   error: function (r, t, s) {
                       $('.ebayCateValue').eq(index).text(t).css({ "background": "red" });
                       getEbayInfoFromEbaySite(index + 1);
                   },
                   success: function (msg) {
                       if (msg.indexOf("|") > -1) {
                           $('.ebayCateValue').eq(index).html("" + msg.split('|')[0] + "").css({ "background": "white" });
                           $('.ebayMyCateValue').eq(index).html("" + msg.split('|')[1] + "").css({ "background": "white" });
                       }
                       else
                           $('.ebayCateText').eq(index).html("" + msg + "");
                       getEbayInfoFromEbaySite(index + 1);
                   }
               });
           }

           $('#getolditeminfofromebay').click(function () {

               getEbayInfoFromEbaySite(0);
           });

           $('.sku').click(function () {
               js_callpage_cus("/q_admin/editPartDetail.aspx?id=" + $(this).text(), 'modify_detail' + $(this).text(), 880, 800);
           });

           $('.Summary').click(function (i) {

               var sku = $(this).parent().find(".sku").eq(0).text();
               js_callpage_cus("/q_admin/ebayMaster/ebay_part_comment_edit.asp?sku=" + sku, 'ebay_part_comment_edit' + sku, 880, 600);
           });

           function issueProduct(the) {

               var sku = the.parent().find(".sku").eq(0).text();
               var eBayCategoryID = the.parent().find(".ebayCateValue").eq(0).text();
               var storeCategory1 = the.parent().find(".ebayMyCateValue").eq(0).text();

               the.html("load....");
               if (eBayCategoryID == "" || storeCategory1 == "")
                   return;
               $.ajax({
                   type: "Get",
                   url: "online/ebay_part_to_ebay_transit.asp",
                   data: "sku=" + sku + "&ebayCategoryID=" + eBayCategoryID + "&ebayCategoryName=&storeCategory1=" + storeCategory1 + "&storeCategory2=",
                   success: function (msg) {
                       the.parent().find('.newitemid').eq(0).html("<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + msg + "' target='_blank'>" + msg + "</a> &nbsp;&nbsp;&nbsp;<a href='online/modifyOnlineShippingFee.aspx?sku=" + sku + "&itemid=" + msg + "' target='_blank' class='modifyShipping'>modify shipping</a> ");
                       the.text("OK");

                       if (the.parent().find('.isnotebook').eq(0).text() == "1") {
                           // alert("Y");
                           if ($.isNumeric(msg))
                               js_callpage_cus("online/modifyOnlineShippingFee.aspx?sku=" + sku + "&itemid=" + msg, "modifyShipping" + sku, 400, 400);
                       }

                   },
                   error: function (msg) {
                       the.html(msg);
                   }
               });
           }

           $('.issue').click(function (i) {
               var the = $(this);
               issueProduct(the);
           });

           getOldItemId(0);
           // getStock(0);
           getStockText(0);
       });
   </script>
</head>
<body>
<input type="button" id='getolditeminfofromebay' value="Get old item info from eBay"/>
<p>
    <span id="getItemAreaInfo"></span>
</p>
<%

set rs = conn.execute("select product_ebay_name, p.product_serial_no, ecap.eBayCateID_1,p.ltd_stock,pc.is_noebook, ecap.eBayCateText_1,ecap.eBayMyCateID_1,ecap.eBayMyCateText_1 from tb_product p inner join ("&_
                        " select distinct sku from tb_ebay_code_and_luc_sku where is_sys=0 and is_online=0"&_
                        " ) t on t.sku=p.product_serial_no left join (select sku from tb_ebay_code_and_luc_sku where is_online=1) t2 on t2.sku=t.sku  "&_
                        " left join tb_ebay_category_and_product ecap on ecap.sku=p.product_serial_no  "&_
                        " inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no " &_
                        " where p.tag=1 and t2.sku is null and p.ltd_stock>3 order by pc.menu_child_serial_no asc,product_ebay_name asc ")
                        if not rs.eof then
                            response.Write("<table>")
                            do while not rs.eof 
                                response.Write("<tr onmouseover=""this.bgColor='#cccccc';"" onmouseout=""this.bgColor='white';"">")
                                response.Write("    <td width='50' class='sku'>"& rs("product_serial_no") &"</td>")
                                response.Write("    <td></td>")
                                response.Write("    <td width='120' class='olditemid'></td>")
                                response.Write("    <td width='60' class='Summary'>Summary</td>")
                                response.Write("    <td width='80' class='issue'>Issue</td>")
                                response.Write("    <td width='20' class='isnotebook'>"& rs("is_noebook") &"</td>")
                                response.Write("    <td width='750'>")
                                response.Write(rs("product_ebay_name")&"")
                                response.Write("    </td>")
                                response.Write("    <td width='350' class='ebayCateText'>"& rs("eBayCateText_1") &"</td>")
                                response.Write("    <td width='80' class='ebayCateValue'>"& rs("eBayCateID_1") &"</td>")
                                response.Write("    <td width='150' class='ebayMyCateText'>"& rs("eBayMyCateText_1") &"</td>")
                                response.Write("    <td width='80' class='ebayMyCateValue'>"& rs("eBayMyCateID_1") &"</td>")
                                response.Write("    <td width='220' class='newitemid'></td>")
                                response.Write("    <td width='50' class='stockQty'>"& rs("ltd_stock") & "</td>")
                                response.Write("    <td width='500' class='stockQtyText'></td>")
                                response.Write("</tr>")
                            rs.movenext
                            loop
                            response.Write("</table>")
                        end if
                        rs.close : set rs = nothing
closeconn %>
</body>
</html>
