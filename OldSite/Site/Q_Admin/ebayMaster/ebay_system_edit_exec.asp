<html>
<head>
    <title></title>
</head>
<body>

<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
        

        dim category_id, cmd, ebay_system_sku
        ebay_system_sku 	= SQLescape(request("ebay_system_sku"))
        category_id 		= SQLescape(request("category_id"))
        cmd 				= SQLescape(request("cmd"))
        
        dim luc_sku, luc_part_name, luc_skus, luc_part_names, comment_id, comment_ids
        Dim luc_part_part_quantity
		Dim luc_part_part_quantitys
		Dim luc_part_max_quantity
		Dim luc_part_max_quantitys
		Dim p_id			' detail id
		Dim p_ids
		
        luc_sku 			= SQLescape(request("luc_sku"))
        luc_part_name     	= SQLescape(request("luc_part_name"))
        comment_id 			= SQLescape(request("comment_id"))
		luc_part_part_quantity=SQLescape(request("luc_part_part_quantity"))
		luc_part_max_quantity=SQLescape(request("luc_part_max_quantity"))
        p_id				= SQLescape(request("p_id"))
		
        luc_skus            = split(luc_sku, ",")
        luc_part_names      = split(luc_part_name, ",")
        comment_ids 		= split(comment_id, ",")
		luc_part_max_quantitys=split(luc_part_max_quantity, ",")
		luc_part_part_quantitys=split(luc_part_part_quantity, ",")
        p_ids				= split(p_id, ",")
		
        if(cmd = "create")then
                conn.execute("insert into tb_ebay_system "&_
	                                        "   ( ebay_system_name, ebay_system_price,  showit  , category_id "&_
		                                        "   ) "&_
		                                        "   values "&_
		                                        "   ( 'new system', 0,  1 ,"& SQLquote(category_id) &"  "&_
		                                        "   ) ")
				' add default shipping charge
				Set rs = conn.execute("Select max(id) from tb_ebay_system")
				if not rs.eof then
					ebay_system_sku = rs(0)
				End if
				rs.close : set rs = nothing

				conn.execute("Insert into tb_ebay_system_shipping(ebay_system_sku, shipping_company_id, shipping_charge) "&_
								" select "& SQLquote(ebay_system_sku)&" , shipping_company_id, default_charge from tb_shipping_company where is_sales_promotion=0")
                dim new_id
                new_id = 0
                set rs = conn.execute("select max(id) from tb_ebay_system")
                if not rs.eof then
                    new_id = rs(0)
                end if
                rs.close : set rs = nothing
                
                ebay_system_sku = new_id
                
                for i=lbound(luc_skus) to ubound(luc_skus)                
                        call InertPartToSystem(trim(luc_skus(i)), comment_ids(i), trim(replace(luc_part_names(i), "[qiozi-comma]", ",")), category_id, new_id, trim(luc_part_part_quantitys(i)), trim(luc_part_max_quantitys(i)), trim(p_ids(i)) )
                next
        end if
        
        if  cmd = "modify"  then
                if(len(trim(ebay_system_sku))) >0 then
                        'conn.execute("Delete from tb_ebay_system_parts where system_sku = "& SQLquote(ebay_system_sku) &"")
                        
                        for i=lbound(comment_ids) to ubound(comment_ids) 
                                if( len(trim(luc_skus(i))) > 0 ) then              
                                    call InertPartToSystem(trim(luc_skus(i)), comment_ids(i), trim(replace(luc_part_names(i), "[qiozi-comma]", ",")), category_id, ebay_system_sku, trim(luc_part_part_quantitys(i)), trim(luc_part_max_quantitys(i)), trim(p_ids(i)) )
                                else
                                    conn.execute("Delete from tb_ebay_system_parts where id='"& trim(p_ids(i)) &"'")
                                end if
                        next
                else
                    Response.write ("<script> alert('params is error');</script>")
                    response.end()
                end if
                
        end if
        
        function InertPartToSystem(luc_sku, comment_id, luc_part_name, parent_id, ebay_system_sku, part_quantity, max_quantity, p_id)
                dim rs, srs
                set rs = conn.execute("select product_current_cost cost, product_current_price - product_current_discount price from tb_product where product_serial_no='"& luc_sku &"'")
                if not rs.eof then
					if (p_id = "") then
						'
						'	insert
                    	conn.execute("insert into tb_ebay_system_parts "&_
	                                " ( luc_sku, comment_id, price, cost, system_sku, part_quantity, max_quantity, regdate) "&_
	                                " values "&_
	                                " ( "& SQLquote( luc_sku )&", "& SQLquote(comment_id) &", "& SQLquote(rs("price")) &", "& SQLquote(rs("cost")) &", "& SQLquote(ebay_system_sku) &", "& SQLquote(part_quantity) &", "& SQLquote(max_quantity) &", now())")
	                                
					else
						'
						' update
						conn.execute("Update tb_ebay_system_parts Set"&_
									" luc_sku="&SQLquote( luc_sku )&_
									" ,comment_id="& SQLquote(comment_id)&_
									" ,price="&SQLquote(rs("price"))&_
									" ,cost="& SQLquote(rs("cost"))&_
									" ,system_sku="& SQLquote(ebay_system_sku)&_
									" ,part_quantity="& SQLquote(part_quantity)&_
									" ,max_quantity="& SQLquote(max_quantity) &_
									" where id ="& SQLquote(p_id))								
									
					end if
	                conn.execute("update tb_product set product_ebay_name = "& SQLquote( luc_part_name ) &",is_modify=1 where  product_serial_no="&SQLquote( luc_sku ) )
	                response.write luc_sku & "<br/>"
				else
					response.write "<br/>error: "& luc_sku
                end if
                rs.close : set rs = nothing
        end function
        
        
        if cmd = "modify" or cmd="create" then
            response.write "<iframe src='/q_admin/netcmd/Create_Part_Json.aspx?qiozicommand=qiozi@msn.com' style='width:0px; height: 0px;'></iframe>"
        end if
        
        closeconn()
%>
<div>

</div>
<script type="text/javascript">
		alert("it is ok");
       parent.parent.$("#ifr_main_frame1").attr("src", "ebay_system_edit.asp?ebay_system_sku=<%=  ebay_system_sku %>&category_id=<%= category_id %>&cmd=modify");
</script>


</body>
</html>
