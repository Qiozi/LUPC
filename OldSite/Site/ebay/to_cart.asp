<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

    
    Dim         curr_product_code       :       curr_product_code   =   Session("curr_proudct_code")
    Dim         tmp_order_code          :       tmp_order_code      =   null
    Dim         system_price            :       system_price        =   null
    Dim  		system_name				:		system_name 		= 	null
	Dim 		price_unit				:		price_unit			=	null
    'Response.write VEmptyNull(curr_product_code)
    if VEmptyNull(curr_product_code) <> ""  then 
    
		if LAYOUT_CCID = "" then LAYOUT_CCID = -1
		
        tmp_order_code = GetNewOrderCode()

		if len(Request.Cookies("ebay_order_code"))<> 6 then 
        	Response.Cookies("ebay_order_code") = tmp_order_code        
		else
			tmp_order_code		=Request.Cookies("ebay_order_code")
		end if
		
        
        Set rs = conn.execute("Select sys_tmp_price, sys_tmp_product_name,price_unit  from tb_sp_tmp where sys_tmp_code="& SQLquote(curr_product_code))
        if not rs.eof then
            system_price = rs(0)
			system_name	=	rs("sys_tmp_product_name")
			price_unit	=	rs("price_unit")
        end if
        rs.close : set rs = nothing
		
		'
		' delete from CA or USA order.
		'
		conn.execute("Delete from tb_cart_temp where cart_temp_code="& SQLquote(tmp_order_code) &" and  current_system<>"& SQLquote(current_system))
		
		conn.execute("insert into tb_cart_temp "&_
                    "	( cart_temp_code, product_serial_no, "&_
	                "   create_datetime, "&_
	                "	ip,"&_
	                "	cart_temp_Quantity,"&_
	                "	customer_serial_no,"&_
                    "   is_noebook, "&_
                    "	country_id "&_
					"	,product_name"&_
					" 	,price"&_
					"	,price_rate"&_
					"	,price_unit"&_
					"	,current_system"&_
                    "   ) values ("&_
                    "   "&  SQLquote(tmp_order_code)    &_
                    "   ,"&  SQLquote(curr_product_code) &_
                    "   ,now()" &_
                    "   ,"&  SQLquote(LAYOUT_HOST_IP)  &_
                    "   ,1" &_
                    "   ,"&  SQLquote(LAYOUT_CCID)  &_
                    "   ,0"&_
                    "   ,"&  SQLquote(Current_System) &_
					"	,"&	 SQLquote(system_name) &_
					"	,"&	 SQLquote(system_price) &_
					"	,"&	 SQLquote(system_price) &_
					"	,"&  SQLquote(price_unit)&_
					"   ,"&  SQLquote(Current_System) &_
                    "   )"                    	)
                    
	
	   	Set rs = conn.execute("select count(tmp_price_serial_no) c from tb_cart_temp_price where order_code="&SQLquote(tmp_order_code))
		
		if not rs.eof then

			if cstr(rs(0)) = "0" then 
				conn.execute("Insert into tb_cart_temp_price(sub_total, create_datetime, order_code, price_unit) values "&_
						"("& SQLquote(system_price) &", now(), "& SQLquote(tmp_order_code) &", "& SQLquote(price_unit)&")")  

			end if      
		end if
		rs.close : set rs = nothing

        closeConn()
		'response.write price_unit
		'response.End()
        response.write "<script>window.location.replace('/ebay/cart.asp');</script>"
    else
        Response.write ("Params is Error!!!")
		closeConn()
	end if
 %>

</body>
</html>
