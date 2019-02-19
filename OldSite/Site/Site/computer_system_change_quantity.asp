<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
dim part_group_id            : part_group_id = SQLescape(request("part_group_id"))
dim product_serial_no       : product_serial_no = SQLescape(request("product_serial_no"))
dim part_quantity             : part_quantity = SQLescape(request("part_quantity"))
dim templete_system_info : templete_system_info =   session("templete_system_info")

for i = lbound(templete_system_info) to ubound(templete_system_info)
    if cstr(templete_system_info(i, 2)) =  cstr(part_group_id)  then 
        templete_system_info(i, 4) = part_quantity 
        response.Write "Hello "   
    end if
    'response.Write cstr(templete_system_info(i, 2)) &"|"&  cstr(part_group_id)   &"|"& cstr(templete_system_info(i, 1))  &"|"& cstr(product_serial_no) &"<br>"
next
session("templete_system_info")  = templete_system_info

 %>
 <!-- begin prine area -->
<%
	dim  current_price, current_price_rate, now_low_price, price_and_save, save_cost
	
	price_and_save = GetSystemConfigurePrice()
	current_price = splitConfigurePrice(price_and_save, 0)
	save_cost = splitConfigurePrice(price_and_save, 1)
	current_price_rate = current_price
	now_low_price = cdbl(current_price_rate) - save_cost
	'ebay_price = ConvertToEbayPrice(now_low_price) 

	%>
  <div id="custem_system_price_area">
  <table width="100%"  border="0" cellspacing="2" cellpadding="0">
	 <% if save_cost <> "0" then %>
   <tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong>&nbsp;Regular&nbsp;Price: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong><%=ConvertDecimalUnit(Current_system, cdbl(current_price_rate) )%></strong>&nbsp;&nbsp;</td>
	</tr>
	<tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong>&nbsp;Discount: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="color:red;text-align: right;"><strong><%=ConvertDecimalUnit(Current_system, 0-save_cost)%></strong>&nbsp;&nbsp;</td>
	</tr>
   <% end if %>
	<tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong>&nbsp;Now&nbsp;Low&nbsp;Price: </strong>&nbsp;&nbsp;</td>
	  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong><%=ConvertDecimalUnit(Current_system, now_low_price)%></strong>&nbsp;&nbsp;</td>
	</tr>
    
    <tr bgcolor="#f2f2f2" >
      <%
        ' 滑动显示价格
       dim special_cash_price
       special_cash_price = ChangeSpecialCashPrice(cdbl(current_price) - save_cost) %>
        <td class="text_hui_11"><strong>&nbsp;Special&nbsp;Cash&nbsp;Price: </strong></td>
        <td class="text_hui_11" style="text-align: right;"><strong><%=ConvertDecimalUnit(Current_system, cdbl(special_cash_price) )%></strong>&nbsp;&nbsp;</td>
    </tr>
<!--	<tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong>&nbsp;Ebay&nbsp;&nbsp;Price: </strong>&nbsp;&nbsp;</td>
	  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="text-align: right;"><strong><%=ConvertDecimalUnit(Current_system, ebay_price)%></strong>&nbsp;&nbsp;</td>
	</tr>-->
	<tr bgcolor="#f2f2f2">
	  <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Quote Number: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" class="text_hui_11"  style="text-align: center;">&nbsp;<!--<a style="cursor:pointer" onClick="return js_callpage_win('/computer_system_quote.asp?1=1', 'system quote', 450, 300, 300 , 300)" >--><a onClick="js_callpage_win('computer_system_quote.asp?1=1', 'system_quote', 450, 300, 300 , 300);" style="cursor:pointer;">Press here to obtain</a></td>
	</tr>
  </table>
  </div>
  <!-- end prine area -->
  <script type="text/javascript">
      if ("" != "<%=special_cash_price%>") {
          parent.document.getElementById("currentprice_1").innerHTML = "<%=ConvertDecimal(now_low_price)%>";
          parent.document.getElementById("currentprice_2").innerHTML = "<%=ConvertDecimal(cdbl(special_cash_price) )%>";
          parent.document.getElementById("currentprice_3").innerHTML = "<%=ConvertDecimal(now_low_price)%>";
          //parent.document.getElementById("currentprice_4").innerHTML = "<%=ConvertDecimal(ebay_price) %>";
          parent.document.getElementById("custem_system_price_area").innerHTML = document.getElementById("custem_system_price_area").innerHTML;
      }
  </script>
</body>
</html>
