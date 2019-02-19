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
        
        if( instr(ebay_system_sku,",")>0)then
            ebay_system_sku = left(ebay_system_sku, instr(ebay_system_sku, ",")-1)
            response.write ebay_system_sku & "<br>"
        end if

        dim ebay_part_group, ebay_part_group_detail, ebay_part_groups, ebay_part_group_details, comment_id, comment_ids
        Dim luc_part_part_quantity
		Dim luc_part_part_quantitys
		Dim luc_part_max_quantity
		Dim luc_part_max_quantitys
		Dim p_id			' detail id
		Dim p_ids
		Dim Is_Label_On_Flash, Is_Label_On_Flashs
		Dim is_shrink
        Dim is_belong_price, cb_inside_totals
        Dim IsParent
				
		is_shrink					= 0
        is_belong_price             = SQLescape(request("cb_inside_total"))
		Is_Label_On_Flash           = SQLescape(request("cb_label_of_flash"))
        ebay_part_group 			= SQLescape(request("ebay_part_group"))
        ebay_part_group_detail     	= SQLescape(request("ebay_part_group_detail"))
        comment_id 			        = SQLescape(request("comment_id"))
		luc_part_part_quantity      = SQLescape(request("luc_part_part_quantity"))
		luc_part_max_quantity       = SQLescape(request("luc_part_max_quantity"))
        p_id				        = SQLescape(request("p_id"))
        IsParent                    = SQLescape(request("IsParent"))
		
        ebay_part_groups            = split(ebay_part_group, ",")
        ebay_part_group_details     = split(ebay_part_group_detail, ",")
        comment_ids 		        = split(comment_id, ",")
		luc_part_max_quantitys      = split(luc_part_max_quantity, ",")
		luc_part_part_quantitys     = split(luc_part_part_quantity, ",")
        p_ids				        = split(p_id, ",")
        Is_Label_On_Flashs          = split(Is_Label_On_Flash, ",")
		cb_inside_totals            = split(is_belong_price, ",")

		Response.write Is_Label_On_Flash
		
		'if len(Is_Label_On_Flash)>0 then 
  		'	is_shrink=1
        'end if
				
				
		'response.end
        if(cmd = "create")then
                conn.execute("insert into tb_ebay_system "&_
	                                        "   ( ebay_system_name, ebay_system_price,  showit  , category_id, sub_part_quantity "&_
		                                        "   ) "&_
		                                        "   values "&_
		                                        "   ( 'new system', 0,  1 ,"& SQLquote(category_id) &", "& ubound(comment_ids)+1 &"  "&_
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
                
                for i=lbound(ebay_part_group_details) to ubound(ebay_part_group_details)                
                        call InertPartToSystem(trim(ebay_part_groups(i)), comment_ids(i), trim(replace(ebay_part_group_details(i), "[qiozi-comma]", ",")), category_id, new_id, trim(luc_part_part_quantitys(i)), trim(luc_part_max_quantitys(i)), trim(p_ids(i)), Is_Label_On_Flashs)
						response.write i & "==" & ebay_part_groups(i) & "==" & ebay_part_group_details(i) & "<br/>"
                next
        end if
        
        if  cmd = "modify"  then
                if(len(trim(ebay_system_sku))) >0 then
                    'response.Write "Delete from tb_ebay_system_parts where system_sku="& SQLquote(ebay_system_sku) &" and ebayshowit=1 "
                        conn.execute("Delete from tb_ebay_system_parts where system_sku="& SQLquote(ebay_system_sku) &" and ebayshowit=1 ")
                        
                        for i=lbound(ebay_part_group_details) to ubound(ebay_part_group_details) 
                                if( len(trim(ebay_part_group_details(i))) > 0 ) then  
									Response.write "<br>"&trim(ebay_part_groups(i)) & "::"& comment_ids(i) &"::" & replace(ebay_part_group_details(i), "[qiozi-comma]", ",") & "::" & luc_part_part_quantitys(i) 
                                    call InertPartToSystem(trim(ebay_part_groups(i)), comment_ids(i), trim(replace(ebay_part_group_details(i), "[qiozi-comma]", ",")), category_id, ebay_system_sku, trim(luc_part_part_quantitys(i)), trim(luc_part_max_quantitys(i)), trim(p_ids(i)) ,Is_Label_On_Flashs,cb_inside_totals)
                                else
                                    conn.execute("Delete from tb_ebay_system_parts where id='"& trim(p_ids(i)) &"'")
                                end if
                        next

                        conn.execute("Update tb_ebay_system set sub_part_quantity = "& ubound(comment_ids)+1 &" where id = '"& ebay_system_sku &"'") 
                else
                    Response.write ("<script> alert('params is error');</script>")
                    response.end()
                end if
                'conn.execute("Update tb_ebay_system set is_shrink='"& is_shrink &"' where id ='"&ebay_system_sku&"'")
        end if
        
        function InertPartToSystem(ebay_part_group, comment_id, ebay_part_group_detail, parent_id, ebay_system_sku, part_quantity, max_quantity, p_id, Is_Label_On_Flash, cb_inside_totals)
                dim rs, srs
                dim isLabel : isLabel = 0
                Dim isBelong: isBelong = 0
                Dim i
                for i = lbound(Is_Label_On_Flashs) to ubound(Is_Label_On_Flashs)
                    if cstr(trim(Is_Label_On_Flashs(i))) = cstr(trim(comment_id)) then 
                        isLabel=1
                    end if
                next
                
                for i=lbound(cb_inside_totals) to ubound(cb_inside_totals)
                    if cstr(trim(cb_inside_totals(i))) = cstr(trim(comment_id)) then 
                        isBelong=1
                    end if
                next

                set rs = conn.execute("select product_current_cost cost, product_current_price - product_current_discount price from tb_product where product_serial_no='"& ebay_part_group_detail &"'")
                if not rs.eof then

						response.write "Hello"
						'
						'	insert
                       	conn.execute("insert into tb_ebay_system_parts "&_
	                                " ( luc_sku, comment_id, price, cost, system_sku, part_quantity, max_quantity, part_group_id, is_label_of_flash, is_belong_price) "&_
	                                " values "&_
	                                " ( "& SQLquote( ebay_part_group_detail )&", "& SQLquote(comment_id) &", "& SQLquote(rs("price")) &", "& SQLquote(rs("cost")) &", "& SQLquote(ebay_system_sku) &", "& SQLquote(part_quantity) &", "& SQLquote(max_quantity) &", "& ebay_part_group &", '"& isLabel &"', '"& isBelong &"')")
	                                

	                response.write luc_sku & "<br/>"
				else
					response.write "<br/>error: "& luc_sku
                end if
                rs.close : set rs = nothing
        end function
        
        
        if cmd = "modify" or cmd="create" then
        '    response.write "<iframe src='/q_admin/netcmd/Create_Part_Json.aspx?qiozicommand=qiozi@msn.com' style='width:0px; height: 0px;'></iframe>"
        end if
        
        closeconn()
%>
<div>

</div>
<script type="text/javascript">
    //alert("it is ok");
    if ('<%= IsParent %>' == 'false')
        parent.location.href = "ebay_system_edit_2.asp?IsParent=false&ebay_system_sku=<%=  ebay_system_sku %>&category_id=<%= category_id %>&cmd=modify";
    else
        parent.parent.$("#ifr_main_frame1").attr("src", "ebay_system_edit_2.asp?ebay_system_sku=<%=  ebay_system_sku %>&category_id=<%= category_id %>&cmd=modify");
</script>


</body>
</html>
