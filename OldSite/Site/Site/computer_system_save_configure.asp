<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<html>
<head>
<%= LAYOUT_CONTENT_TYPE &vblf %>
<title>
</title>
</head>
<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	'
	'
	' vildate this product is window os.
	'
	function ValidateIsWindwoProd(product_id)
		dim wp_s, i
		ValidateIsWindwoProd = false
		wp_s = split(application("window_product_skus"), ",")
		for i = lbound(wp_s) to ubound(wp_s)
			if cstr(wp_s(i)) = cstr(product_id) then
				ValidateIsWindwoProd = true			
			end if
		next	
	end function
	

	dim current_selected_control_value
	current_selected_control_value = SQLescape(request("current_selected_control_value"))
	
		dim qn_area_count
		qn_area_count = SQLescape(request("qn_area_count"))
	' 系统产品已显示过，更新Quote号
		dim old_sys_sku, view_system_print
		

	 sys_tmp_detail = SQLescape(request("sys_tmp_detail"))

	dim product_serial_no, total_price
	total_price = 0 
	product_serial_no = SQLescape(request("id"))
	if product_serial_no = "" then 
		product_serial_no = -1
	end if
	dim save_cost
	save_cost = 0

	dim templete_system_infos, current_group_part_id
	templete_system_infos =	session("templete_system_info")
	'response.write session("templete_system_info")
'	if templete_system_infos = "" then
'		Response.write "Error: session is lost ."
'		response.End()
'	End if
	
	for i=lbound(templete_system_infos,1) to ubound(templete_system_infos,1)
		if (cstr(sys_tmp_detail) = cstr(templete_system_infos(i,0))) then 

			current_group_part_id = templete_system_infos(i,2)
			templete_system_infos(i,1) = product_serial_no
			templete_system_infos(i,4) = current_selected_control_value
			session("templete_system_info") = templete_system_infos	

		end if

	next
	
	'SystemChangePrice(Session("sys_tmp_sku"))
	function SystemChangePrice(sys_code)
		dim rs, price, cost
		price = 0
		cost = 0
		set rs = conn.execute("select product_current_price * part_quantity ,product_current_cost * part_quantity from tb_sp_tmp_detail where sys_tmp_code='"&Session("sys_tmp_sku")&"'")
		if not rs.eof then
			do while not rs.eof 
			price = price + cdbl(rs(0))
			cost = cost + cdbl(rs(1))
			rs.movenext
			loop
		end if
		rs.close : set rs = nothing
		conn.execute("update tb_sp_tmp set sys_tmp_price= "&price&", sys_tmp_cost="&cost&" where sys_tmp_code='"&Session("sys_tmp_sku")&"'")	
	end function
		%>
	<!-- begin prine area -->
	<%
	dim  current_price, current_price_rate, now_low_price, price_and_save, ebay_price
	
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
	  <td width="35%" bgcolor="#f2f2f2" style='text-align:left'><strong>&nbsp;Regular&nbsp;Price: </strong></td>
	  <td bgcolor="#f2f2f2" style='text-align:right'><strong><span class="price_dis"><%=ConvertDecimal( cdbl(current_price_rate) )%></span><span class="price_unit"><%= CCUN %></span></strong></td>
	</tr>
	<tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2" style='text-align:left'><strong>&nbsp;Discount: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" style='color:blue;text-align:right'>
      		<strong>
            	<%=ConvertDecimalUnit(current_system,0-save_cost)%>
            </strong>
      </td>
	</tr>
   <% end if %>
	<tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2"  style='text-align:left'><strong>&nbsp;Now&nbsp;Low&nbsp;Price: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" style='text-align:right'><strong><%=ConvertDecimalUnit(current_system,now_low_price)%></strong></td>
	</tr>
    
    <tr bgcolor="#f2f2f2" >
      <%
        ' 滑动显示价格
       dim special_cash_price
       special_cash_price = ChangeSpecialCashPrice(cdbl(current_price) - save_cost) %>
        <td class="text_hui_11"><strong>&nbsp;Special&nbsp;Cash&nbsp;Price: </strong></td>
        <td style='text-align:right'><strong><%=ConvertDecimalUnit(current_system,cdbl(special_cash_price) )%></strong></td>
    </tr>
    <% if is_view_ebay_price then  %>
	<tr bgcolor="#f2f2f2" >
	  <td width="35%" bgcolor="#f2f2f2" style='text-align:left'><strong>&nbsp;Ebay&nbsp;&nbsp;Price: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" style='text-align:right'><strong><%=ConvertDecimalUnit(current_system,ebay_price)%></strong></td>
	</tr>
	<% end if %>
	<tr bgcolor="#f2f2f2">
	  <td valign="top" bgcolor="#f2f2f2" style='text-align:left'><strong>&nbsp;Quote Number: </strong></td>
	  <td align="left" bgcolor="#f2f2f2" style='text-align:right'>&nbsp;<a onClick="js_callpage_cus('computer_system_quote.asp?1=1', 'system_quote', 450, 300);" style="cursor:pointer;">Press here to obtain</a></td>
	</tr>
  </table>
  </div>
  <!-- end prine area -->
<%
	'------------------------------
	' 机箱图
	'------------------------------
	dim case_sku , product_image_url, product_image_1, product_image_1_g, casers
	case_sku = 0

	set casers = conn.execute("select max(product_serial_no) from (select p.menu_child_serial_no as product_category, p.product_serial_no from tb_product p  where product_serial_no in ("&GetConfigureProductIds()&")) a1 , (  select pc.* from tb_product_category pc , tb_computer_case cc where pc.menu_child_serial_no=cc.computer_case_category or pc.menu_pre_serial_no=cc.computer_case_category ) a2 where a1.product_category=a2.menu_child_serial_no and a1.product_serial_no= "&product_serial_no)
	
	if not casers.eof then
		case_sku = casers(0)	
	end if
	casers.close : set casers = nothing
	' 图
	
	closeconn()

%>
<script language="javascript">
<!--

try{
	if ("" != "<%=special_cash_price%>")
	{
		parent.document.getElementById("currentprice_1").innerHTML = "<%=ConvertDecimal(now_low_price)%>";
		parent.document.getElementById("currentprice_2").innerHTML = "<%=ConvertDecimal(cdbl(special_cash_price) )%>";
		parent.document.getElementById("currentprice_3").innerHTML = "<%=ConvertDecimalUnit(current_system,now_low_price)%>";
		<% if is_view_ebay_price then  %>
		//parent.document.getElementById("currentprice_4").innerHTML = "<%=formatcurrency(ebay_price) %>";
		<% end if %>
		parent.document.getElementById("custem_system_price_area").innerHTML = document.getElementById("custem_system_price_area").innerHTML ;
	}
	try
	{
		parent.document.getElementById("open_datetime").value = '<%= now() %>';
	}
	catch(e)
	{
		alert(e);
	}
}
catch(e){}
<%
	if ValidateIsWindwoProd(product_serial_no) then 
		response.Write("parent.displayProductWarrary();")	
		
	elseif instr( window_system_group_ids,"["& current_group_part_id &"]") > 0 then 
		response.Write("parent.displayProductWarrary();")
		response.Write("parent.revertProductWarrary();")	
		
	end if
	'response.Write(instr(window_system_group_ids,"["& current_group_part_id &"]" ) & current_group_part_id)
%>
	<% if len(case_sku) > 0 then %>
	parent.writeCaseImg('<%= case_sku %>');
	<% end if %>
//-->
</script>
<%= GetConfigureProductIds() %>


</body>
</html>
