<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
Response.Cookies("CurrentOrder") = "ebay"

	Dim     system_sku      :       system_sku      =   SQLescape(request.form("system_sku"))
	Dim     ebay_number     :       ebay_number     =   SQLescape(Request("number"))
	Dim     part_skus       :       part_skus       =   SQLescape(request.form("part_sku"))
	Dim     part_price      :       part_price      =   SQLescape(request.form("part_price"))
	Dim     part_quantity   :       part_quantity   =   SQLescape(request.form("part_quantity"))
	Dim 	comment_id		:		comment_id		=	SQLescape(request.Form("comment_id"))
	Dim     cmd             :       cmd             =   SQLescape(request.form("cmd"))
	Dim     ebay_system_sell:       ebay_system_sell=   SQLescape(request.form("ebay_system_sell"))
	Dim     priority        :       priority        =   SQLescape(request.form("priority"))
	Dim 	product_name	:		product_name	=	SQLescape(request.Form("product_name"))
	Dim 	cate_name		:		cate_name		=	SQLescape(request.Form("cate_name"))
	Dim 	is_view_system	:		is_view_system	=	SQLescape(request.Form("is_view_system"))
	
	Dim     newSystemCode   :       newSystemCode   =   null
	Dim 	CpuSku			:		CpuSku			=	null
	
	Dim     part_skus_g     :       part_skus_g     =   null
	Dim     part_price_g    :       part_price_g    =   null
	Dim     part_quantity_g :       part_quantity_g =   null
	Dim     priority_g      :       priority_g      =   null
	Dim 	comment_id_g	:		comment_id_g	=	null
	Dim 	product_name_g	:		product_name_g	=	null
	Dim 	cate_name_g		:		cate_name_g		=	null
	
	Dim 	shipping_and_handling:	shipping_and_handling = null
	Dim 	ebay_system_name	:	ebay_system_name	=	null
	if cmd = "customize" then
        part_skus_g     =   split(part_skus, ",")
        part_price_g    =   split(part_price, ",")
        part_quantity_g =   split(part_quantity, ",")  
        priority_g      =   split(priority, ",")
		comment_id_g	=	split(comment_id, ",")
		product_name_g	=	split(product_name, ",")
		cate_name_g		=	split(cate_name, ",")
		
        newSystemCode = GetNewSystemCode()
        'Response.write SQLquote(system_sku)
		
		'
		' get system name
		'
		Set rs = conn.execute("select id from tb_ebay_system_part_comment where is_cpu=1")
		if not rs.eof then	
				for i=lbound(comment_id_g) to ubound(comment_id_g)
					if cstr(trim(comment_id_g(i))) = cstr(rs(0)) then 
							CpuSku = trim(part_skus_g(i))
					end if
				next
		end if
		rs.close : set rs = nothing
		
		
		
        Conn.execute("insert into tb_sp_tmp "&_
                     "	( sys_tmp_code, sys_tmp_price, create_datetime, "&_
                     "	tag,"                           &_
                     "  ip, "                           &_
                     "	system_templete_serial_no, "    &_
                     "	is_noebook "                    &_
					 "	,ebay_number"&_
					 "	,sys_tmp_product_name"&_
					 "  ,is_customize"&_
					 "	,price_unit"&_
                     "	)   "                           &_
                     " values   "                       &_
                     "  (   "                           &_
                     "  "& SQLquote(newSystemCode)      &_
                     "  ,"& SQLquote(ebay_system_sell)  &_
                     "  ,now()"                         &_
                     "  ,1"                             &_
                     "  ,"& SQLquote(LAYOUT_HOST_IP)    &_
                     "  ,"& SQLquote(system_sku)        &_
                     "  ,0"                             &_					 
					 "  ,"& SQLquote(ebay_number)    	&_
					 "	,"&	SQLquote(GetPartShortName(CpuSku) & " System") &_
					 "  ,1"&_
					 "  ,"& SQLquote(CCUN) &_
                     "  )")
        
        
        for i=lbound(part_skus_g) to ubound(part_skus_g)
        
            Set rs = conn.execute("select product_current_price-product_current_discount sell, product_current_cost from tb_product where product_serial_no="& SQLquote(trim(part_skus_g(i))))
            if not rs.eof then
               	
			   
                conn.execute("insert into tb_sp_tmp_detail 				"&_
                             "	( sys_tmp_code, product_serial_no, part_quantity,"&_
                             "	part_max_quantity,                      "&_
                             "	product_current_price,                  "&_
                             "	product_current_cost,                   "&_
                             "	product_order,                          "&_
                             "	system_templete_serial_no,              "&_
                             "	product_current_sold                    "&_
							 "	,ebay_number"&_
							 "	,part_group_id"&_
							 "	,product_name"&_
							 "	,cate_name"&_
                             "	)                                       "&_
                             "	values                                  "&_
                             "	("& SQLquote(newSystemCode)             &","&_
                             "  "&  SQLquote(trim(part_skus_g(i)))      &","&_
                             "  "&  SQLquote(trim(part_quantity_g(i)))  &","&_
                             "	"&  SQLquote(trim(part_quantity_g(i)))  &","&_
                             "	"&  SQLquote(rs("sell"))                &","&_
                             "	"&  SQLquote(rs("product_current_cost"))&","&_
                             "	"&  SQLquote(trim(priority_g(i)))        &","&_
                             "	"&  SQLquote(system_sku)                &","&_
                             "	"&  SQLquote(trim(part_price_g(i))) &_
							 "	,"& SQLquote(ebay_number)&_
							 "	,"& SQLquote(trim(comment_id_g(i)))&_
							 "	,"& SQLquote(trim(product_name_g(i)))&_
							 "	,"& SQLquote(trim(cate_name_g(i)))&_
							 "  )")
                             
                             
							 
				'REsponse.write ("insert into tb_sp_tmp_detail 				"&_
'                             "	( sys_tmp_code, product_serial_no, part_quantity,"&_
'                             "	part_max_quantity,                      "&_
'                             "	product_current_price,                  "&_
'                             "	product_current_cost,                   "&_
'                             "	product_order,                          "&_
'                             "	system_templete_serial_no,              "&_
'                             "	product_current_sold                    "&_
'							 "	,ebay_number"&_
'							 "	,part_group_id"&_
'							 "	,product_name"&_
'							 "	,cate_name"&_
'                             "	)                                       "&_
'                             "	values                                  "&_
'                             "	("& SQLquote(newSystemCode)             &","&_
'                             "  "&  SQLquote(trim(part_skus_g(i)))      &","&_
'                             "  "&  SQLquote(trim(part_quantity_g(i)))  &","&_
'                             "	"&  SQLquote(trim(part_quantity_g(i)))  &","&_
'                             "	"&  SQLquote(rs("sell"))                &","&_
'                             "	"&  SQLquote(rs("product_current_cost"))&","&_
'                             "	"&  SQLquote(trim(priority_g(i)))        &","&_
'                             "	"&  SQLquote(system_sku)                &","&_
'                             "	"&  SQLquote(trim(part_price_g(i))) &_
'							 "	,"& SQLquote(ebay_number)&_
'							 "	,"& SQLquote(trim(comment_id_g(i)))&_
'							 "	,"& SQLquote(trim(product_name_g(i)))&_
'							 "	,"& SQLquote(trim(cate_name_g(i)))&_
'							 "  )<br>")
            end if
            rs.close : set rs = nothing        
        next
		
		'
		' view system
		'
		if is_view_system = "1" or is_view_system = "2" or is_view_system = "3" then 
			if is_view_system = "2" then 
				cmd = "print"
			End if
			if is_view_system = "3" then 
				cmd = "email"
			End if
			response.write "<script>parent.js_callpage_cus('/ebay/system_view_mini.asp?cmd="&cmd&"&system_code="&newSystemCode&"&"& second(now()) &"', 'view_system', '620', 600);"
			response.write " function buy(){ parent.document.getElementById('form1').submit(); }"
            Response.write "</script>"
			Response.End()
		End if
else
' unCustomize 
		
	
		newSystemCode = GetNewSystemCode()
		
		Set rs = conn.execute("select id ,category_id, ebay_system_name, ebay_system_price, ebay_system_current_number	"&_						
								" from tb_ebay_system where id in (select luc_sku from tb_ebay_item_number where item_number="& SQLquote(ebay_number) &")")
		If not rs.eof then
			ebay_system_sell = ConvertDecimal(rs("ebay_system_price"))	
			system_sku		= rs("id")		
			ebay_system_name = rs("ebay_system_name")
					
			'Response.write 	newSystemCode
	
			IF len(ebay_number) > 0 then 
				Conn.execute("Delete from tb_sp_tmp Where sys_tmp_code="& SQLquote(newSystemCode))
				Conn.execute("insert into tb_sp_tmp "&_
						 "	( sys_tmp_code, sys_tmp_price, create_datetime, "&_
						 "	tag,"                           &_
						 "  ip, "                           &_
						 "	system_templete_serial_no, "    &_
						 "	is_noebook "                    &_
						 "	,sys_tmp_product_name"			&_
						 "	,ebay_number"&_
						 "	,is_customize"&_
						 "  ,price_unit"&_
						 "	)   "                           &_
						 " values   "                       &_
						 "  (   "                           &_
						 "  "& SQLquote(newSystemCode)      &_
						 "  ,"& SQLquote(ebay_system_sell)  &_
						 "  ,now()"                         &_
						 "  ,1"                             &_
						 "  ,"& SQLquote(LAYOUT_HOST_IP)    &_
						 "  ,"& SQLquote(system_sku)        &_
						 "  ,0"                             &_
						 "	,"& SQLquote(ebay_system_name) 	&_
						 "	,"& SQLquote(ebay_number)		&_	
						 "  ,0"&_		
						 "  ,"& SQLquote(CCUN) &_
						 "  )")			
				'
				'	parts 
				'
				Conn.execute("Delete from tb_sp_tmp_detail Where sys_tmp_code="& SQLquote(newSystemCode))
			
				Conn.execute("insert into tb_sp_tmp_detail 				"&_
						 "	( system_product_serial_no,sys_tmp_code, product_serial_no, part_quantity,"&_
						 "	part_max_quantity,                      "&_
						 "	product_current_price,                  "&_
						 "	product_current_cost,                   "&_
						 "	product_order,                          "&_
						 "	system_templete_serial_no              "&_
						 "	,ebay_number"&_
						 "	,part_group_id"&_
						 "	,product_name"&_
						 "	,cate_name"&_
						 "	)                                       "&_
						 " select es.id, "& SQLquote(newSystemCode)             &", luc_sku,es.part_quantity"&_
						 " , es.max_quantity"&_
						 " ,p.product_current_price-p.product_current_discount sell"&_
						 " ,p.product_current_cost "&_
						 " ,epc.priority"    &_
						 " ,es.system_sku"&_
						 " ,"& SQLquote(ebay_number) &_
						 " ,epc.id comment_id"   &_
						 " , p.product_ebay_name"&_
						 " , epc.comment"&_												
						" from tb_product p "&_
						" inner join tb_ebay_system_parts es on p.product_serial_no=es.luc_sku "&_
						" inner join tb_ebay_system_part_comment epc on epc.id=es.comment_id "&_
						" where es.system_sku in (select luc_sku from tb_ebay_item_number where item_number="& SQLquote(ebay_number) &") order by epc.priority asc"					 )
			End if	
		else
			Response.write "number is valid"
			response.End()
		End If
		rs.close : set rs = nothing		
end if




if len(newSystemCode)>0 then
    Session("curr_proudct_code") = newSystemCode
    'Call IsLoginWeb("1")    
end if
'Response.write GetNewSystemCode() &"B"

closeconn()
Response.redirect("/ebay/to_cart.asp?")

 %>
 <script type="text/javascript">
 

 </script>
</body>
</html>
