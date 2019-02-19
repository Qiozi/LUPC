<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<!--#include virtual="/q_admin/funs.asp"-->

     <%
     
     
        dim part_group_id, cmd, part_group_detail
		dim part_group_name, part_group_comment, part_group_showit, part_group_is_ebay
		dim categoryID, comment_ids, system_sku
		dim array_commentIDS
		Dim part_name, luc_sku, part_ebay_price
		Dim luc_sku_array, priority_array, priority
		Dim ebay_cost
		Dim ebay_describe
		Dim Is_Disable_Flash
        Dim ebay_system_short_name

        Dim etc_item_id
        Dim etc_item_title
        Dim etc_item_price

        etc_item_id                         =       SQLescape(request("etc_item_id"))
        etc_item_title                      =       SQLescape(request("etc_item_title"))
        etc_item_price                      =       SQLescape(request("etc_item_price"))
        

		ebay_system_short_name              =       SQLescape(request("ebay_system_short_name"))
		ebay_cost                           =       SQLescape(request("ebay_cost"))
		priority                            =       SQLescape(request("priority"))
		part_name 							=		SQLescape(request("part_name"))
		luc_sku								=		SQLescape(request("luc_sku"))
		part_ebay_price						= 		SQLescape(request("part_ebay_price"))
       
        cmd                            		=      SQLescape(request("cmd"))
        part_group_id                     	=      SQLescape(request("part_group_id"))
		part_group_detail	=	""
		part_group_detail					=		SQLescape(request("part_group_detail"))
		
		part_group_name						=		SQLescape(request("group_name"))
		part_group_comment					=		SQLescape(request("group_comment"))
		part_group_showit					=		SQLescape(request("showit"))
		part_group_is_ebay					=		SQLescape(request("is_ebay"))
		categoryID							=		SQLescape(request("categoryID"))
		
		comment_ids							=		SQLescape(request("comment_ids")) ' 当前系统所选中的Comment ID.
		system_sku							=		SQLescape(request("system_sku"))
		
		ebay_describe                       =       SQLescape(request("ebay_describe"))
		

		Is_Disable_Flash                    =       SQLescape(request("disableFlash"))


		
        '
		';;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;;
        if(cmd = "PartGroupDetail") then
			REsponse.write "<Select name='ebay_part_group_detail' id='ebay_part_group_detail_"&part_group_id&"'  style='width:550px;'>"
            set rs = conn.execute("Select p.product_current_cost, p.product_serial_no, "&_
                            " p.ltd_stock, "&_
							" case when length(p.product_ebay_name)>4 then p.product_ebay_name "&_
							"      when length(p.product_name_long_en )>4 then p.product_name_long_en "&_
							"      when length(p.product_name)>4 then p.product_name "&_
							"	   else p.product_short_name end as product_name, p.product_current_price price, pd.part_group_id "&_
						" from tb_part_group_detail pd inner join tb_product p on p.product_serial_no=pd.product_serial_no "&_
						" where part_group_id='"& part_group_id &"' and p.split_line=0 and p.tag=1 and pd.showit=1 and p.for_sys=1 order by p.product_ebay_name asc, p.product_current_cost asc ")
		
			if not rs.eof then
			    dim isWarn 
                isWarn = null
				do while not rs.eof
					Response.write ("<option value='"& rs("product_serial_no") &"' ")
					if not isnull(part_group_detail) then
					if cstr(part_group_detail) = cstr(rs("product_serial_no")) then response.write " selected='selected' "
					end if
                    set srs = conn.execute("select count(part_group_id) from tb_part_group "&_
                                    " where part_group_id = '"& part_group_id &"' and product_category in "&_
                                    "(select replace(category_ids, '|', ',') from tb_ebay_system_part_comment where is_mb=1)")
                    
                    if not srs.eof then
                        if srs(0) >0 then 
                            isWarn = IsWarnPart(rs("product_serial_no"))
                        end if
                    end if
                    srs.close: set srs = nothing
                    
                    if not isnull(isWarn) then
                        response.write ">"& right("00000"& rs("product_serial_no") , 5) & ":" & isWarn & "::" & rs("product_name") &"</option>"
                    else                   
					    response.write (" >"& right("00000"& rs("product_serial_no") , 5) & ":" & right("000000"& cint(rs("product_current_cost")) , 6) & ":" & right("000000"& rs("ltd_stock") , 6) & "::" & rs("product_name") &"</option>")
				    end if
                   
                rs.movenext
				loop
					
			else
				Response.write ("<option value='-1' selected='selected'>None</option>")
			end if
			rs.close : set rs = nothing	
        	response.write "</select>"
        end if
		
		
		if( cmd = "editGroupComment") then
            response.Clear()

		        if instr(part_group_comment, "[and]")>0 then part_group_comment = replace(part_group_comment, "[and]" , "&")
		        if instr(part_group_name, "[and]")>0 then part_group_name = replace(part_group_name, "[and]" , "&")
		        
				conn.execute("Update tb_part_group Set "&_
										"part_group_name='"& trim(part_group_name)&"'"&_
										",part_group_comment='"&trim(part_group_comment)&"'"&_
										",is_ebay='"&part_group_is_ebay&"'"&_
										",showit ='"&part_group_showit&"'"&_
										" where part_group_id='"& part_group_id &"'")
				'response.write "OK."
				
				response.write  "<div class='part_group_name'"&_
				                " id='"& part_group_id &"' "&_
				 				" name='"&trim(part_group_name)&"' "&_
								" showit='"&part_group_showit&"' "&_
								" comment='"& trim(part_group_comment) &"' "&_
								" is_ebay='"& part_group_is_ebay &"'>"&_
								" <span name='duplicate' onclick=""duplicateGroup('"&part_group_id&"');"">Duplicate</span>"&_
								" | <span name='edit' onclick=""editGroup('"&part_group_id&"', $(this));"">Edit</span>"&_
								" | <span name='hide' onclick=""hideGroup('"&part_group_id&"');"">Hide</span>"&_
								" <span> "& trim(part_group_comment)
				Response.write 	" </span><span style='color:red;'>"& writeGroupOfSysSKU(part_group_id) &"</span>"
				Response.write  " </div>"	
				response.write  "<span class='e' id='"& part_group_id &"'> "& part_group_is_ebay & "</span>"	
									
		end if
		
		if (cmd = "newPartGroup") then
				conn.execute("Insert into tb_part_group(part_group_name,part_group_comment,showit,product_category)"&_
							" values ('"& trim(part_group_name) &"', '"& trim(part_group_comment) &"', '1', '"& trim(categoryID) &"')")
				set rs = conn.execute("Select max(part_group_id) from tb_part_group")
				if not rs.eof then
				    conn.execute("insert into tb_part_group_detail "&_
				                "( part_group_id, product_serial_no, showit, priority)"&_
				                " values ( '"& rs(0) &"', 1049, 1,	0)")
				end if
				rs.close : set rs = nothing
				
				Response.write "OK."
		end if
		
		if (cmd = "saveGroupComment") then
			conn.execute("Delete from tb_ebay_system_parts where comment_id not in ("& comment_ids &") and system_sku='"& system_sku &"'")
			
			array_commentIDS = split(comment_ids, ",")

			for i=lbound(array_commentIDS) to ubound(array_commentIDS)
				
				if trim(array_commentIDS(i)) <> "0" then 
					set rs = conn.execute("select comment_id from tb_ebay_system_parts where comment_id='"& trim(array_commentIDS(i)) &"' and system_sku='"& system_sku &"'")
					'response.write "<br>"& ("select comment_id from tb_ebay_system_parts where comment_id='"& trim(array_commentIDS(i)) &"' and system_sku='"& system_sku &"'")
					if rs.eof or rs.bof then
					'response.write array_commentIDS(i)
					conn.execute("insert into tb_ebay_system_parts "&_
									"( system_sku, comment_id, comment_name,  "&_
									" part_quantity, "&_
									" max_quantity"&_
                                    " , regdate"&_
									" )"&_
									" select '"&system_sku&"', id, comment, 1,1 , now() from tb_ebay_system_part_comment where id='"& trim (array_commentIDS(i)) &"'")
									
					end if
				
					rs.close : set rs = nothing
				end if
			next			
			Response.write "OK."
		end if
		
		if (cmd = "savePartOfGroupList") then
			if(luc_sku <>"")then
				conn.execute("Update tb_product set product_ebay_name='"& part_name &"'"&_
								" ,is_modify=1, part_ebay_price='"& part_ebay_price &"' "&_
								" , part_ebay_cost='"& ebay_cost &"' "&_
								" , part_ebay_describe='"& ebay_describe &"' "&_
                                " , ebay_system_short_name='"& ebay_system_short_name &"' "&_
								"  where product_serial_no='"& luc_sku &"'")
				Response.write "OK."
			end if
		
		end if
		
		
		if (cmd = "delPartOfGroupList") then
			if(luc_sku<>"" and part_group_id <>"" ) then
			
				conn.execute("Delete from tb_part_group_detail "&_
								" where product_serial_no='"& luc_sku &"' "&_
								" and part_group_id='"& part_group_id &"'")
								Response.write ("Delete from tb_part_group_detail "&_
								" where product_serial_no='"& luc_sku &"' "&_
								" and part_group_id='"& part_group_id &"'")
				Response.write "OK."
			end if
		end if
        
        if(cmd = "savePartOfGroupPriority")then
            luc_sku_array = split(SQLescape(request("skus")), ",")
            priority_array  =   split(SQLescape(request("prioritys")),",")
            
            for i=lbound(luc_sku_array) to ubound(luc_sku_array)
                conn.execute("Update tb_part_group_detail set priority='"& priority_array(i) &"'"&_
                             " where product_serial_no='"& trim(luc_sku_array(i)) &"' and part_group_id='"& part_group_id &"'")
            next
            response.Write "savePartOfGroupPriority OK."
        
        end if
        
        if(cmd="duplicateGroup")then
            conn.execute("insert into tb_part_group "&_
                        "( product_category, part_group_name, showit, "&_
                        "part_group_comment, "&_
                        "priority, "&_
                        "none_option, "&_
                        "is_ebay"&_
                        ")"&_
                        "select product_category, part_group_name, showit, "&_
                        "'"& part_group_comment &"', "&_
                        "priority, "&_
                        "none_option, "&_
                        "is_ebay from tb_part_group where part_group_id='"&part_group_id&"'")
            set rs = conn.execute("Select max(part_group_id) from tb_part_group")
            if not rs.eof then
                response.Write(rs(0))
                conn.execute("insert into tb_part_group_detail "&_
                        "( part_group_id, product_serial_no, "&_
                        "showit, "&_
                        "priority, "&_
                        "nominate"&_
                        ")"&_
                        "select '"&rs(0)&"', product_serial_no, "&_
                        "showit, "&_
                        "priority, "&_
                        "nominate from tb_part_group_detail where part_group_id='"& part_group_id &"'")
            end if
            rs.close : set rs = nothing
            response.Write "duplicateGroup OK"
        end if
        
        if(cmd="hideEbaySysPart")then
            conn.execute("delete from tb_ebay_system_part_zero_price where luc_sku='"& luc_sku &"'")
            conn.execute("Insert into tb_ebay_system_part_zero_price(luc_sku) values ('"& luc_sku &"')")
        
            response.Write "OK"
        end if

         if(cmd="SaveETCItemInfo")then

            set rs = conn.execute("Select * from tb_ebay_etc_items where LUC_eBay_Sys_Sku='"& system_sku&"'")
            if not rs.eof then 
                conn.execute("update tb_ebay_etc_items set LUC_eBay_Sys_Sku = '0' where id='"& rs("id") &"'")
            end if
            rs.close : set rs = nothing
            conn.execute("update tb_ebay_etc_items set LUC_eBay_Sys_Sku = '"&system_sku&"' where ItemID='"& etc_item_id &"'")
                       
            response.Write "OK"

         end if


         if (cmd="HidePartGroup") then 
            conn.execute("update tb_part_group set showit=0 where part_group_id='"& part_group_id &"'")
            Response.Write("OK")
         end if

          if (cmd="ShowPartGroup") then 
            conn.execute("update tb_part_group set showit=1 where part_group_id='"& part_group_id &"'")
            Response.Write("OK")
         end if
        closeconn() 
        
        
        
        '
        'If no motherboard relation, tip warned, if not ebay_name tip warning. 
        function IsWarnPart(sku)
            dim rs
            IsWarnPart = null
            set rs = conn.execute("select count(id) from tb_part_relation_motherboard_video_audio_port where mb_sku='"& sku &"'")
            if not rs.eof then
                if rs(0) = 0 then 
                    IsWarnPart = "-----relation warn-----"
                end if
            end if
            rs.close : set rs = nothing

            if IsWarnPart = null then 
                set rs = conn.execute("select length(product_ebay_name) from tb_product where product_serial_no='"& sku &"'")
                if not rs.eof then
                    if rs(0) <3 then 
                        IsWarnPart = "-----name warn-----"
                    end if
                end if
                rs.close  : set rs = nothing
            end if
        end function
        %>

