<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%

	Dim     current_part_quantity   :       current_part_quantity   =   SQLescape(request.form("current_part_quantity"))
	Dim     current_part     		:       current_part     		=   SQLescape(Request("current_part"))
	Dim     current_max_quantity    :       current_max_quantity    =   SQLescape(request.form("current_max_quantity"))
	Dim     current_part_name      	:       current_part_name      	=   SQLescape(request.form("current_part_name"))
	Dim     current_part_group   	:       current_part_group   	=   SQLescape(request.form("current_part_group"))
	Dim 	current_part_priority	:		current_part_priority	=	SQLescape(request.Form("current_part_priority"))	
	Dim     current_part_price		:       current_part_price		=   SQLescape(request.form("current_part_price"))
	Dim     current_part_discount   :       current_part_discount   =   SQLescape(request.form("current_part_discount"))
	Dim 	current_part_cost		:		current_part_cost		=	SQLescape(request.Form("current_part_cost"))	
	Dim 	cate_name				:		cate_name				=	SQLescape(request.Form("cate_name"))	
	'Dim 	part_group_id			:		part_group_id			=	SQLescape(request.Form("part_group_id"))
	Dim 	system_product_serial_no:		system_product_serial_no=	SQLescape(request.Form("system_product_serial_no"))
	
	Dim 	system_discount			:		system_discount			=	SQLescape(request.Form("system_discount"))
	Dim     cmd             		:       cmd             		=   SQLescape(request("cmd"))
	Dim 	system_sell				:		system_sell				=	SQLescape(request.Form("system_sell"))
	Dim 	system_sku				:		system_sku				=	SQLescape(request.Form("system_sku"))
	Dim 	is_view_system			:		is_view_system			=	SQLescape(request.Form("is_view_system"))
	Dim		old_system_code			:		old_system_code			=	SQLescape(request.Form("system_code"))
	
	Dim     current_part_quantity_g :       current_part_quantity_g =   null
	Dim     current_part_g    		:       current_part_g    		=   null
	Dim     current_max_quantity_g 	:       current_max_quantity_g 	=   null
	Dim     current_part_name_g     :       current_part_name_g     =   null
	Dim 	current_part_group_g	:		current_part_group_g	=	null
	Dim 	current_part_priority_g	:		current_part_priority_g	=	null
	Dim 	cate_name_g				:		cate_name_g				=	null
	Dim 	current_part_price_g	:		current_part_price_g 	= 	null
	Dim 	current_part_discount_g	:		current_part_discount_g	=	null
	Dim 	current_part_cost_g 	:		current_part_cost_g 	= 	null
	Dim 	system_product_serial_no_g
	'Dim 	part_group_id_g
	
	Dim 	shipping_and_handling	:	shipping_and_handling 		= 	null
	Dim 	system_name				:	system_name					=	" System"
	Dim 	newSystemCode			:	newSystemCode = SQLescape(request("newSystemCode"))
	if cmd = "customize" then
        current_part_quantity_g     =   split(current_part_quantity, ",")
        current_part_g    			=   split(current_part, ",")
        current_max_quantity_g 		=   split(current_max_quantity, ",")  
        current_part_name_g      	=   split(current_part_name, ",")
		current_part_group_g		=	split(current_part_group, ",")
		current_part_priority_g		=	split(current_part_priority, ",")
		cate_name_g					=	split(cate_name, ",")		
		current_part_price_g      	=   split(current_part_price, ",")
		current_part_discount_g		=	split(current_part_discount, ",")
		current_part_cost_g			=	split(current_part_cost, ",")
		system_product_serial_no_g	=	split(system_product_serial_no, ",")
		part_group_id_g				=	split(part_group_id, ",")
		
        newSystemCode = GetNewSystemCode()
        'Response.write SQLquote(system_sku)
		Set rs = conn.execute("Select count(id) c From tb_system_code_store Where system_code="& SQLquote(newSystemCode))
		if not rs.eof then
			if rs(0) = 0 then
				response.write old_system_code &"DD"
				if len(SQLescape(old_system_code)) = 0 then
					old_system_code = 0
				end if
				conn.execute("INSERT INTO tb_system_code_store "&_
							"	( "&_
							" system_templete_serial_no, "&_
							"	system_code, "&_
							"	create_datetime, "&_
							"	ip, "&_
							"	old_system_code, "&_
							"	is_buy, "&_
							"	is_ebay_custom"&_
							"	)"&_
							"	VALUES"&_
							"	("&_
							"	'"& SQLescape(system_sku) &"', "&_
							"	'"& SQLescape(newSystemCode) &"', "&_
							"	now(), "&_
							"	'"&	SQLescape(LAYOUT_HOST_IP) &"', "&_
							"	"& SQLquote(old_system_code)&", "&_
							"	'0', "&_
							"	'0'"&_
							"	)")
			end if 
		end if
		rs.close : set rs = nothing
		'
		' get system name
		'

		for i=lbound(cate_name_g) to ubound(cate_name_g)
			if lcase(cstr(trim(cate_name_g(i)))) = lcase("CPU") then 
					system_name = GetPartShortName(trim(current_part_g(i))) & system_name
			end if
		next

		
		
		
        Conn.execute("insert into tb_sp_tmp "&_
                     "	( sys_tmp_code, sys_tmp_price, create_datetime, "&_
                     "	tag,"                           &_
                     "  ip, "                           &_
                     "	system_templete_serial_no, "    &_
                     "	is_noebook "                    &_
					 "	,sys_tmp_product_name"&_
					 "  ,is_customize"&_
					 "	,price_unit"&_
					 "  ,save_price"&_
					 "	,old_price"&_
                     "	)   "                           &_
                     " values   "                       &_
                     "  (   "                           &_
                     "  "& SQLquote(newSystemCode)      &_
                     "  ,"& SQLquote(system_sell)  &_
                     "  ,now()"                         &_
                     "  ,1"                             &_
                     "  ,"& SQLquote(LAYOUT_HOST_IP)    &_
                     "  ,"& SQLquote(system_sku)        &_
                     "  ,0"                             &_					 
					 "	,"&	SQLquote(system_name) &_
					 "  ,1"&_
					 "  ,"& SQLquote(CCUN) &_
					 "  ,"& SQLquote(system_discount) &_
					 "  ,"& system_sell & "+"& system_discount&_
                     "  )")
        
        
        for i=lbound(current_part_g) to ubound(current_part_g)
        
          		conn.execute("insert into tb_sp_tmp_detail("&_
								"sys_tmp_code"&_
								" ,product_serial_no"&_
								" ,product_current_price"&_
								" ,product_current_cost"&_
								" ,product_order"&_
								" ,system_templete_serial_no"&_
								" , system_product_serial_no"&_
								" , part_group_id"&_
								" , save_price"&_
								" , old_price"&_
								" , product_current_price_rate"&_
								" ,product_current_sold"&_
								" , part_quantity"&_
								" , part_max_quantity"&_
								" , product_name,cate_name"&_
								" ) values "&_
								" ( "&SQLquote(newSystemCode)	&_
								" ,"& SQLquote(trim(current_part_g(i)))	&_
								" ,"& SQLquote(trim(current_part_price_g(i))) &_
								" ,"& SQLquote(trim(current_part_cost_g(i))) &_
								" ,"& SQLquote(trim(current_part_priority_g(i))) &_
								" ,"& SQLquote(system_sku) &_
								" ,"& SQLquote(trim(system_product_serial_no_g(i))) &_
								" ,"& SQLquote(trim(current_part_group_g(i))) &_
								" ,"& SQLquote(trim(current_part_discount_g(i))) &_
								" ,"& SQLquote(trim(current_part_price_g(i))) &_
								" ,"& SQLquote(trim(current_part_price_g(i))) &_
								" ,"& SQLquote(cdbl(trim(current_part_price_g(i))) - cdbl( trim(current_part_discount_g(i)) ) ) &_
								" ,"& SQLquote(trim(current_part_quantity_g(i))) &_
								" ,"& SQLquote(trim(current_max_quantity_g(i))) &_
								" ,"& SQLquote(trim(CommaDecode(current_part_name_g(i)))) &_
								" ,"& SQLquote(trim(cate_name_g(i))) &")")	
			   
          
        next
		
		'
		' view system
		'
		if is_view_system = "1" or is_view_system = "2" or is_view_system = "3" or is_view_system="4" or is_view_system = "5" then 
			if is_view_system = "2" then 
				cmd = "print"
			End if
			if is_view_system = "3" then 
				cmd = "email"
			End if
						
			if is_view_system = "4" then 
				response.write "<script> parent.ViewQuote('"&newSystemCode&"');"				
				Response.write "</script>"		
			elseif is_view_system = "5" then
				response.write "<script> parent.viewAskQuestion('"&newSystemCode&"');"				
				Response.write "</script>"	
			elseif is_view_system = "1" or is_view_system = "2" or is_view_system = "3" then
				response.write "<script> parent.js_callpage_cus('/site/view_configure_system.asp?cmd="&cmd&"&system_code="&newSystemCode&"&"& second(now()) &"', 'view_system', '620', 600);"
				response.write " function buy(){ parent.document.getElementById('form1').submit(); }"
				Response.write "</script>"
			End if
			response.write is_view_system
			Response.End()
		End if
elseif cmd = "go" then 
	Session("SystemSku8") = newSystemCode
	'system_sku
	
else
' unCustomize 

end if




if len(newSystemCode)>0 then
    Session("SystemSku8") = newSystemCode
	  
end if
'Response.write GetNewSystemCode() &"B"

closeconn()

response.write "<script>"
response.write "parent.window.location.replace('/site/computer_system_to_cart.asp?cmd="& cmd &"&system_sku="& system_sku&"');"
Response.write "</script>"

 %>
 <script type="text/javascript">
 

 </script>
</body>
</html>
