<!--#include virtual="site/inc/inc_page_top.asp"-->

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td><td id="page_main_center" valign="top" style="width:600px" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class='page_main_nav' id="page_main_nav">
                	<span class="nav1">home</span>
                    <span class="nav1"><%= "Keywords:&nbsp;"& request("keywords") %></span>
                </div>

            	<div id="page_main_area">
                    
                	<%							
						Dim rsc
						Dim page_category_request
						Dim split_line_id			
						Dim grrs
						Dim is_display
						Dim X
						Dim krs
						dim keyword, menu_child_serial_no, parent_name
						dim product_lists
						Dim logo_count
						Dim system_cpu_logo_filename
						Dim system_cpu_logo_filename_vc
						Dim logo_image_filename_sys
						Dim cpu_logo_path
						Dim cpurs2
						Dim rebate_save_price
                        Dim Categories
                        Dim Manufacturer
                        Dim priceBegin
                        Dim priceEnd

						parent_name = ""
						keyword         = trim(SQLescape(request("keywords")))
                        Categories      = trim(SQLescape(request("Categories")))
                        Manufacturer    = trim(SQLescape(request("Manufacturer")))
                        priceBegin      = trim(SQLescape(request("priceBegin")))
                        priceEnd        = trim(SQLescape(request("priceEnd")))
						'page = trim(SQLescape(request("page")))
						
						'
						'	if keywords length is = 12 , and go to ebay system.
						'
						'
                      
						If len(trim(keyword)) = 8 and isnumeric(trim(keyword)) then 
							Response.Redirect("/site/load_quote.asp?quote="& keyword)
							Response.End()
						end if

                        If len(trim(keyword)) = 12 and isnumeric(trim(keyword)) then 

                            set rs = conn.execute("select SKU from tb_ebay_code_and_luc_sku where ebay_code = '"& keyword &"' limit 0,1")
                            if not rs.eof then
                                keyword = rs(0)
                            end if
							rs.close : set rs = nothing
						end if
					%>
                	<table width="600" height="750" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                            <tr>
                                <td>                                    
                                    <table border="0" cellpadding="3">
                                        <form action="Search.asp" method="get" id="formSearchDetail" name="formSearchDetail">
                                        <tr><td>Keyword:</td><td><input id="Text1" type="text" name="keywords" style="width: 195px;" value="<%=keyword %>"/></td>
                                        <td>
                                            
                                           </td></tr>
                                        <tr>
                                            <td>Categories</td>
                                            <td>                                                
                                                <select style="width: 200px;" name="categories">
                                                    <option value="-1">--ALL--</option>
                                                    
                                                    <% 
                                                       set rs = conn.execute("select menu_child_serial_no, menu_child_name from tb_product_category where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc ")
                                                       if not rs.eof then
                                                            do while not rs.eof 
                                                                Response.Write "<optgroup Label='"& rs("menu_child_name") &"'>"&vblf
                                                                    set crs = conn.execute("select menu_child_serial_no, menu_child_name from tb_product_category where menu_pre_serial_no='"& rs("menu_child_serial_no") &"' and tag=1 and is_view_menu=1 order by menu_child_order asc ")
                                                                    if not crs.eof then
                                                                        do while not crs.eof 
                                                                            Response.Write "<option value="""& crs("menu_child_serial_no") &""""
                                                                            if categories = cstr(crs("menu_child_serial_no")) then response.Write " Selected='selected' "
                                                                            Response.Write ">"& crs("menu_child_name") &"</option>"
                                                                        crs.movenext
                                                                        loop
                                                                    end if
                                                                    crs.close : set crs = nothing
                                                                     
                                                                Response.Write "</optgroup>" &vblf
                                                            rs.movenext
                                                            loop
                                                       end if
                                                       rs.close : set rs = nothing 
                                                    %>
                                                </select>
                                            </td>
                                            <td>                                                
                                                &nbsp;</td>
                                        </tr>
                                        <tr><td>Manufacturer:</td><td>
                                            <select style="width: 200px;" name="manufacturer">
                                                    <option value="-1">--ALL--</option>                                                    
                                                    <% 
                                                       set rs = conn.execute("select distinct producter_serial_no from tb_product where tag=1 and producter_serial_no <> '' and length(producter_serial_no)>1 order by producter_serial_no asc ")
                                                       if not rs.eof then
                                                            do while not rs.eof                                                                                                                                  
                                                                Response.Write "<option value="""& rs("producter_serial_no") &"""" 
                                                                if manufacturer = rs("producter_serial_no") then response.Write " Selected='selected' "
                                                                response.Write ">"& rs("producter_serial_no") &"</option>" &vblf                                                                 
                                                            rs.movenext
                                                            loop
                                                       end if
                                                       rs.close : set rs = nothing 
                                                    %>
                                                </select>
                                        </td><td>
                                               <table title="compare" class="btn_table" align="right" border="0" cellpadding="0" cellspacing="0" height="24" width="115">
                                              <tbody><tr>
                                                <td width="6"><img src="/soft_img/app/3232.gif" alt="" height="24" width="6"></td>
                                                <td class="btn_middle">
                                                    <a style="color: rgb(255, 255, 255);" class="btn_img" onclick="clearForm();$('#formSearchDetail').submit();return false;">Clear</a>
                                                </td>
                                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" alt="" height="24" width="6"></td>
                                             </tr>
                                           </tbody></table></td></tr>
                                        <tr><td>Price range:</td><td><input type="text" name="priceBegin" size="12" value="<%= priceBegin %>"/> to 
                                            <input type="text" name="priceEnd" size="12"  value="<%= priceEnd %>"/></td><td>
                                             <table title="compare" class="btn_table" align="right" border="0" cellpadding="0" cellspacing="0" height="24" width="115">
                                              <tbody><tr>
                                                <td width="6"><img src="/soft_img/app/3232.gif" alt="" height="24" width="6"></td>
                                                <td class="btn_middle">
                                                    <a style="color: rgb(255, 255, 255);" class="btn_img" onclick="$('#formSearchDetail').submit();return false;">Search</a>
                                                </td>
                                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" alt="" height="24" width="6"></td>
                                             </tr>
                                           </tbody></table></td></tr>
                                        <tr><td colspan="2">
                                            
                                        </td><td>
                                               </td></tr>
                                        </form>
                                    </table>                                    
                                </td>
                            </tr>
                            <tr>
                              <td style="border:#E3E3E3 1px solid;  padding-top:0px; height:600px;" valign="top">
                              <%
                                dim params_sql
                                dim ebay_code, ebay_price
                                dim search_sql_1 ,search_split_page, search_sql_2
                                Dim is_Virtual, product_params_sql, sys_params_sql
                                Dim partTableName 
                                Dim sysSql1, sysSql2, partSql1, partSql2
                                sysSql1 = ""
                                sysSql2 = ""
                                partSql1= ""
                                partSql2= ""
                                'Dim page_category
                                page_category = 0
                                product_params_sql = ""
                                sys_params_sql = ""
                                partTableName = " (Select * from tb_product )"
                                set rs = conn.execute("Select is_virtual,page_category from tb_product_category where menu_child_serial_no='"& categories &"'")
                                if not rs.eof then
                                    is_virtual = rs("is_virtual")
                                    page_category = rs("page_category")
                                end if

                                if keyword = "sli" then 
                                    params_sql = " system_templete_name like '%"& keyword &"%' or "
                                end if

                                ''
                                ' part
                                if categories <> "-1" and categories<>"" and  is_virtual = 0 then
                                    product_params_sql = product_params_sql & " and p.menu_child_serial_no = '"& categories &"'"
                                end if
                                if manufacturer <> "-1" and manufacturer<>"" then
                                    product_params_sql = product_params_sql & " and p.producter_serial_no = '"& manufacturer &"'"
                                end if
                                if priceBegin <> "" then
                                    product_params_sql = product_params_sql & " and p.product_current_price-product_current_discount>= '"& priceBegin &"'"
                                end if
                                if priceEnd <> "" then
                                    product_params_sql = product_params_sql & " and p.product_current_price-product_current_discount<= '"& priceEnd &"'"
                                end if
                                if is_virtual = 1 then
                                    partTableName = " (select pp.* from tb_product_virtual pv inner join tb_product pp on pp.product_serial_no=pv.lu_sku and pv.menu_child_serial_no='"&categories&"') "
                                end if

                                ''
                                ' system
                                if page_category = 0 then
                                    if categories <> "-1" and categories<>"" and  is_virtual = 0 then
                                        sys_params_sql = sys_params_sql & " and (esac.ebaySysCategoryID = '"& categories &"' or esac.ebaySysCategoryID in (Select menu_child_Serial_no from tb_product_category where menu_pre_serial_no='"& categories &"'))"
                                    end if
                                    if manufacturer <> "-1" and manufacturer<>"" then
                                        sys_params_sql = sys_params_sql & " and sysCode.producter_serial_no = '"& manufacturer &"'"
                                    end if
                                    if priceBegin <> "" then
                                        sys_params_sql = sys_params_sql & " and st.tmp_sell >= '"& priceBegin &"'"
                                    end if
                                    if priceEnd <> "" then
                                        sys_params_sql = sys_params_sql & " and st.tmp_sell<= '"& priceEnd &"'"
                                    end if

                                    sysSql1 = " select distinct 0, 2 as product_category,'0' as product_type, '0' as split_line, st.ebay_system_name product_short_name,st.ebay_system_name product_name_long_en,st.id product_serial_no,0 as system_templete_price, st.ebay_system_name product_name, "&_
                                            " '0' hot,'0' new, '0' as img_sku,'' logo_image_filename,'' logo_image_filename_vc,'' logo_image_filename_sys, -1 ltd_stock, ep.ebay_code,ep.ebay_price "&_
                                            " from tb_ebay_system st left join tb_ebay_store_page ep on ep.lu_sku=st.id and ep.is_system=1 "&_
                                            " inner join (select distinct es.id, pp.* from tb_ebay_system es inner join (select distinct luc_sku,system_sku from tb_ebay_system_parts) esp on es.id=esp.system_sku  "&_
                                            " inner join tb_product pp on pp.product_serial_no  = esp.luc_sku"&_
                                            " inner join tb_ebay_selling ebay on ebay.sys_sku = es.id ) sysCode on sysCode.id=st.id "&_
                                            " inner join tb_ebay_system_and_category esac on esac.systemsku = st.id "&_
                                            " where ((ep.ebay_code='"& keyword &"') or ( st.showit=1  and (" & params_sql & " st.id = '"& keyword &"'or st.system_title1 like '%"& keyword &"%' or st.keywords like '%"& keyword &"%')) or (sysCode.product_name like '%"& keyword &"%' or sysCode.product_name_long_en like '%"& keyword &"%' or sysCode.manufacturer_part_number like '%"& keyword &"%' or sysCode.keywords like '%"& keyword &"%')) "& sys_params_sql 
                                            
                                    sysSql2 = " select distinct 2 as product_category,'0' as product_type, 0 as split_line, ebay_system_name product_short_name,ebay_system_name product_name_long_en,st.id product_serial_no,'0' as system_templete_price "&_
                                            " from tb_ebay_system st left join tb_ebay_store_page ep on ep.lu_sku=st.id and ep.is_system=1 "&_
                                            " inner join (select distinct es.id, pp.* from tb_ebay_system es inner join (select distinct luc_sku,system_sku from tb_ebay_system_parts) esp on es.id=esp.system_sku  "&_
                                            " inner join tb_product pp on pp.product_serial_no  = esp.luc_sku"&_
                                            " inner join tb_ebay_selling ebay on ebay.sys_sku = es.id ) sysCode on sysCode.id=st.id "&_
                                            " inner join tb_ebay_system_and_category esac on esac.systemsku = st.id "&_
                                            " where ((ep.ebay_code='"& keyword &"') or ( st.showit=1  and (" & params_sql & " st.id = '"& keyword &"'or st.system_title1 like '%"& keyword &"%' or st.keywords like '%"& keyword &"%')) or (sysCode.product_name like '%"& keyword &"%' or sysCode.product_name_long_en like '%"& keyword &"%' or sysCode.manufacturer_part_number like '%"& keyword &"%' or sysCode.keywords like '%"& keyword &"%'))  "& sys_params_sql 
                                            
                                else
                                        partSql1= "select p.product_current_discount, 1 product_category,p.menu_child_serial_no as product_type, p.split_line,p.product_short_name,p.product_name_long_en,p.product_serial_no, p.product_current_price, p.product_name, p.hot, p.new , case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end as img_sku,0 logo_image_filename,0 logo_image_filename_vc,0 logo_image_filename_sys, "&_
                                            "case when product_store_sum >2 then 2 "&_
                                            "when ltd_stock >2 then 2  "&_
                                            "when product_store_sum + ltd_stock >2 then 2  "&_
                                            "when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
                                            "when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
                                            "when ltd_stock <=2 and ltd_stock >0 then 3 "&_
                                            "when product_store_sum +ltd_stock =0 then 4 "&_
                                            "when product_store_sum+ltd_stock <0 then 5 end as ltd_stock, '' ebay_code, '' ebay_price  from "& partTableName & " p inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where pc.tag=1 "& product_params_sql &" and pc.menu_pre_serial_no in (select menu_child_serial_no from tb_product_category pc2 where pc2.menu_child_serial_no=pc.menu_pre_serial_no and pc2.tag=1) and p.tag=1  and p.is_non=0 and p.split_line=0 and p.product_current_price >0 and (p.product_serial_no like '%"& keyword &"%' or p.product_name like '%"& keyword &"%' or p.product_name_long_en like '%"& keyword &"%' or p.manufacturer_part_number like '%"& keyword &"%' or p.keywords like '%"& keyword &"%') " 
            
                                        partSql2= "select  1 as product_category,p.menu_child_serial_no as product_type, p.split_line,p.product_short_name,p.product_name_long_en,p.product_serial_no,p.product_current_price from "& partTableName & "  p inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where pc.tag=1 "& product_params_sql &" and pc.menu_pre_serial_no in (select menu_child_serial_no from tb_product_category pc2 where pc2.menu_child_serial_no=pc.menu_pre_serial_no and pc2.tag=1) and p.tag=1 and  p.is_non=0 and p.split_line=0 and (p.product_serial_no like '%"& keyword &"%' or p.product_name like '%"& keyword &"%' or p.product_name_long_en like '%"& keyword &"%' or p.manufacturer_part_number like '%"& keyword &"%' or p.keywords like '%"& keyword &"%') " 
                                end if

                                ' sys
                               if page_category = 0 then

                                    search_sql_1  = "select * from "&_
                                                " ( "& sysSql1 &_
                                                "    ) t"
                                    search_sql_2    = " select count(product_category) from "&_
                                                " ( "& sysSql2 &_
                                                " ) t "

                                ' part
                               elseif page_category = 1 then

                                    search_sql_1 = "select * from "&_
                                                " ("& partSql1 &_                                               
                                                "    ) t"
                                    search_sql_2 = " select count(product_category) from "&_
                                                " ( "& partSql2 &_                                              
                                                " ) t "
                                ' all
                               else
                                    search_sql_1 = "select * from "&_
                                                " ("& partSql1 &_
                                                " union all "&_
                                                " "& sysSql1 &_
                                                "    ) t"
                                    search_sql_2 = " select count(product_category) from "&_
                                                " ( "& partSql2 &_
                                                " union all "&_
                                                " "& sysSql2 &_
                                                " ) t "
                               end if

                               
            
            
                                
            	'response.write search_sql_2
            
                                if  (keyword <> "" ) then
            							page_category_request = 1
										if page_category_request = 1  then
					
											dim ps,rspagecount, line_list, rscount
											ps = 20
					
											' split page
											set rsc = conn.execute(search_sql_2)
											
											' no data match
											if (rsc(0) = 0 ) then 
											%>
												<p style="padding: 5em; height:400px;">
												No Products where found.<br/>You may try searching with less or partial keywords.
												
												</p>
											<%
											end if
											
											
											
											
											rscount = rsc(0)
											if SQLescape(request("keywords")) <> "" then ps = 20
											rspagecount=-int(-rsc(0)/ps)
											page=SQLescape(request("page"))
											if not isnumeric(page) or page = "" then page=1
											page=cint(page)
											if page>rspagecount then page=rspagecount
											if page<1 then page=1
										'	response.write int(rsc(0)/ps)
											search_split_page = " limit "& ps*(page-1) &","&ps
											
											'response.Write(search_sql_1 & search_split_page)
											set rs = conn.execute( search_sql_1 & search_split_page)
											
											if not rs.eof then
											split_line_id = 0
											do while not rs.eof 
											
											ebay_code = rs("ebay_code")
											ebay_price = rs("ebay_price")
											
											' split
											'response.write rs("split_line")

											' if SKU and to page detail.
											if(rsc(0) = 1) then 
												if rs("product_category") = 1 then 
												    Response.Redirect("/site/product_parts_detail.asp?class=2&pro_class="& rs("product_type") & "&id="& rs("product_serial_no") &"&cid="& rs("product_type"))
												else
													Response.Redirect("/site/system_view.asp?cid="& rs("product_type") &"&id="& rs("product_serial_no") &"&class=52")
												end if
											
												Response.End()
											end if
											
                                    if rs("product_category") = 1 then 
									
									
                                    
                              %>
                                                    <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #dddddd;">
                                                      <tr>
                                                        <td width="70"><a href="<%= LAYOUT_HOST_URL %>Product_parts_detail.asp?pro_class=<%=rs("product_type")%>&id=<%=rs("product_serial_no")%>&cid=<%=rs("product_type")%>" target="_blank">
                                                        
                                                        <%
                                                        'if FileIsExist("pro_img/COMPONENTS/"&rs("product_serial_no")&"_t.jpg")  then 
                                                        dim part_image_t
                                                        part_image_t = rs("img_sku")

                                                        %>
                                                        <img src="<%= GetImgMinURL(HTTP_PART_GALLERY , part_image_t) %>" width="50" height="50" hspace="10" border="0" >
                                                        <%
                                                            
                                                        'end if
                                                        %>
                                                        </a></td>
                                                        <td style="padding-right:10px; "><table width="100%"  border="0" cellpadding="0" cellspacing="0">
                                                            <tr>
                                                              <td valign="top"><a class="hui-orange-12" href="<%= LAYOUT_HOST_URL %>product_parts_detail.asp?class=<%=rs("product_type")%>&pro_class=<%=rs("product_type")%>&id=<%=rs("product_serial_no")%>&cid=<%=rs("product_type")%>"  target="_blank"><%=rs("product_name")%></a>
                                                              <%
                                                                ' if on sale
                                                                 'set drs = conn.execute(sql_sale_promotion(rs("product_serial_no")))
                                                                    
                                                                    
                                                                    GetHotImg(rs("hot"))
                                                                    
                                                                    GetNewImg(rs("new"))
                                                                    if cstr(rs("product_current_discount")) <> "0" then 
                                                                        GetSaleImg(1)
                                                                    end if
                                                                    set grrs = conn.execute(sql_sale_promotion_rebate_sign(rs("product_serial_no")))
                                                                    if not grrs.eof then
                                                                        GetRebateImg(1)
                                                                    end if
                                                                    grrs.close : set grrs = nothing
                                                                %>
                                                            <br/>
                                                               <span class="text_hui2_11">
                                                              
                                                              <%=rs("product_name_long_en")%> 
                                            </span></td>
                                                            </tr>
                                                        </table></td>
                                                        <td width="80" valign="bottom"><table width="94%"  border="0" cellspacing="0" cellpadding="3">
                                                            <tr>
                                                              <td align="right" class="text_orange_11">
                                                              <span class="price_big">
                                                              <%
                                                             
                                    
                                                             dim single_save_price
                                                             single_save_price = cdbl(rs("product_current_discount"))
                                                             'response.write sql_sale_promotion(rs("product_serial_no"))
                                                             ' card_rate = 1.022
                                                             'response.write single_save_price
                                                              if cstr(single_save_price) <> "0" then
                                                                    response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"& formatcurrency(rs("product_current_price"))& "</span><br/>"
                                                                    response.write  ConvertDecimalUnit(Current_system,cdbl(rs("product_current_price") ) - cdbl(single_save_price))
                                                              else
                                                                    'response.write  price_unit & formatcurrency(cdbl(rs("product_current_price")) * card_rate )
                                                                    response.write  ConvertDecimalUnit(Current_system, rs("product_current_price"))
																   'response.Write(rs("product_current_price"))
                                                              end if
															 ' response.write (TypeName(rs("product_current_price")))
                                                             ' drs.close : set drs = nothing
                                                              %></span>	</td>
                                                            </tr>
                                                            <tr>
                                                              <td align="right" class="text_orange_11">
                                                              SKU#<%= rs("product_serial_no") %>
                                                               </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align:right" nowrap="true"><%=FindPartStoreStatus2(rs("product_serial_no"), rs("ltd_stock")) %></td>
                                                            </tr>
                                                            <tr align="right">
                                                              <td> <a href="<%= LAYOUT_HOST_URL %>Shopping_Cart_pre.asp?cid=<%=rs("product_type")%>&Pro_Id=<%=rs("product_serial_no")%>" target="_blank"><img src="/soft_img/app/buyNow_bt.gif" width="56" height="13" border="0"></a> </td>
                                                            </tr>
                                                        </table></td>
                                                      </tr>
                                </table>
            
                                  <% 
                                    elseif rs("product_category")  = 2 then 
                                    ' ------system product begin -----------------
                                                %>
                                                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                      <td width="22%" valign="top"><table width="100"  border="0" align="center" cellpadding="0" cellspacing="0">
                                                          <tr>
                                                            <td><a href="/site/system_view.asp?class=<%=request("class")%>&id=<%=rs("product_serial_no")%>&cid=<%= rs("product_type") %>"><img src="<%= GetSystemPhotoByID2(rs("product_serial_no"))%>"  border="0" ></a>
                                                            
                                                            </td>
                                                          </tr>
                                                          <tr>
                                                            <td align="center" class="text_blue_13">[ <%=rs("product_serial_no")%> ]</td>
                                                          </tr>
                                                      </table></td>
                                                      <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                                          <tr>
                                                            <td>
                                                              <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                <%
                                             
                                            logo_count = 0
                                            system_cpu_logo_filename = rs("logo_image_filename")
                                            system_cpu_logo_filename_vc =  rs("logo_image_filename_vc")
                                            logo_image_filename_sys = rs("logo_image_filename_sys")
                                            
                                            if len(system_cpu_logo_filename )  > 4 then 
                                                logo_count = logo_count + 1
                                            end if
                                            if len(system_cpu_logo_filename_vc )  > 4 then 
                                                logo_count = logo_count + 1
                                            end if
                                            if len(logo_image_filename_sys )  > 4 then 
                                                logo_count = logo_count + 1
                                            end if
                                             
                                            response.Write( "<td style=""padding-bottom:3px;")								
                                            response.write "width: "& logo_count * 40& "px;"								
                                            response.write """>"
                                            
                                             if system_cpu_logo_filename <> "" then 
                                                response.write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& system_cpu_logo_filename &"""  >"
                                                
                                             end if								
                                            
                                             if system_cpu_logo_filename_vc <> "" then 
                                                response.Write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& system_cpu_logo_filename_vc &""" >"
                                             end if
                                             
                                              if len(logo_image_filename_sys) > 4 then 
                                                 response.Write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& logo_image_filename_sys &"""   >"
                                             end if
                                            response.write ("</td>")
											
											response.Write("<td style='min-height: 40px;'><a  href=""/site/system_view.asp?cid="& rs("product_type") &"&id="& rs("product_serial_no")&"&class="&request("class")&"""><span id='logo_cpu_name'  class=""system_title100"" >")
													
												if trim(rs("product_short_name")) <> "" then 
														response.Write rs("product_short_name")
												else
													set cpurs2 = conn.execute("select p.product_serial_no, p.product_short_name,p.menu_child_serial_no from tb_ebay_system_parts sp inner join tb_product p on   sp.luc_sku=p.product_serial_no where p.tag=1 and p.is_non=0 and system_sku="& SQLquote(rs("product_serial_no"))&" and menu_child_serial_no in (select menu_child_serial_no from tb_product_category where tag=1 and  menu_pre_serial_no in (select computer_cpu_category from tb_computer_cpu)) limit 0,1")
													if not cpurs2.eof then 
															response.Write cpurs2("product_short_name")	
													end if
													cpurs2.close :set cpurs2 = nothing									
													End if
												response.Write("&nbsp;System</span></a></td>")	
											
											
                                            
                                               
            
                                          %>
                                    </tr>
                                </table></td>
                              </tr>
                              <tr>
                                <td height="10"><table width="100%" height="1"  border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td style="background: url(/soft_img/app/line2.gif) repeat-x 0 50%"><img src="/soft_img/app/line2.gif" width="3" height="1"></td>
                                    </tr>
                                </table></td>
                              </tr>
                              <tr>
                                <td height="110" valign="top" class="text_hui_11">
							  <%
                        ' account the parts quantity.
                        dim sys_product_order_sum
						
                        set sRS  = conn.execute("select count(s.system_sku) "&_
                                                    " from tb_ebay_system_parts s,tb_product p "&_
													" where s.system_sku="&rs("product_serial_no")&" "&_
													" and p.tag=1 and  p.menu_child_serial_no not in("&LAYOUT_NONE_DISPLAY_PRODUCT_CATEGORY&")"&_
													" and (p.is_non=0 or p.product_name like '%onboard%') "&_
													" and p.product_serial_no=s.luc_sku order by s.id asc")
						if not srs.eof then
                        	sys_product_order_sum = srs(0)
						end if
                        srs.close : set srs = nothing
        
                        set crs = conn.execute("select p.product_short_name, p.product_serial_no, s.part_quantity from tb_ebay_system_parts s,tb_product p where s.system_sku="& SQLquote(rs("product_serial_no"))&" and p.tag=1 and  p.menu_child_serial_no not in("&LAYOUT_NONE_DISPLAY_PRODUCT_CATEGORY&")  and (p.is_non=0 or p.product_name like '%onboard%') and p.product_serial_no=s.luc_sku order by s.id asc")
                        if not crs.eof then
                            response.write "<table width=""100%""><tr><td valign='top'>"
                            dim sys_product_order , is_split_table
                            sys_product_order = 0 
                             is_split_table = 0
                            do while not crs.eof
                            sys_product_order = sys_product_order + 1
                            
                            if is_split_table = 1 then 
                                response.write "</td><td valign='top'>"
                                is_split_table = 0
                            end if
                            if sys_product_order = (int(sys_product_order_sum /2) + (sys_product_order_sum mod 2) ) then 
                                is_split_table = 1
                            end if
                        %>
                              <div  class="system_part">
                              <%
                              if cstr(crs("part_quantity")) <> "1" then 
                                    response.write crs("part_quantity") & "x"
                                end if
                              %> <a class="hui-orange-s" onclick="js_callpage_cus('/site/view_part.asp?id=<%= crs("product_serial_no")%>','view_price', 602, 600); return false;"><%= replace(crs("product_short_name") , "(Verify availability on motherboard)", "") %> </a></div>
                              <%
                            crs.movenext
                            loop
                            response.write ("</td></tr></table>")
                        end if
                        crs.close:set crs = nothing
                      %>
                                           </td>
                                          </tr>
                                          <tr>
                                            <td height="30"><table  border="0" align="right" cellpadding="2" cellspacing="0">
                                                <tr>
                                                  <td class="text_red_12b" style="padding-right:8px;">

                                                   <span class="price_big">
                                   <%
                                 
                                 
                                 
                                 dim tmp_system_price, tmp_system_save_price, tmp_system_price_first
                                 dim price_and_save
                                   
                                  rebate_save_price = 0 'FindSystemRebatePrice(rs("product_serial_no"))
                                  price_and_save = GetSystemPriceAndSave(rs("product_serial_no"))
                                  
                                  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
                                  tmp_system_price_first = splitConfigurePrice(price_and_save,0)						  
                                  tmp_system_price= tmp_system_price_first - tmp_system_save_price
                                  
                                  
                                  if tmp_system_save_price <> 0 then 		
                                    response.write "<span class='price_dis' style='font-size:11pt;'>" & formatcurrency(cdbl(tmp_system_price_first),2) &"</span>&nbsp;&nbsp;"
                                  end if
                                  response.write appendUnit( tmp_system_price, true)
%>
                                  </span>
                                                  </td>
                                                  <td></td>
                                                  <td><a href="/site/system_view.asp?cid=<%= rs("product_type") %>&class=1&id=<%=rs("product_serial_no")%>"><img src="/soft_img/app/select_bt.gif" width="56" height="13" border="0"></a></td>
                                                </tr>
                                            </table></td>
                                          </tr>
                                      </table></td>
                                    </tr>
                                     
                                    <tr>
                                      <td height="20" colspan="2" <%  if len(ebay_code) > 10 then response.write " valign=""top""" %>><table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#D0DAE1">
                                          <tr>
                                            <td height="3" bgcolor="#FFFFFF"></td>
                                          </tr>
                                      </table></td>
                                    </tr>
                                  </table>
                                  <%
                                                ' ------system product end -----------------
                                    end if
                                    rs.movenext
                                    loop
                                end if
                                rs.close : set rs = nothing
                            end if
                            end if
                             %>
                                  <br>
                                  <table width="100%" height="50"  border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                      <td align="right">
                                        <%
                                        dim url_path
										url_path = Request.ServerVariables("HTTP_url")
                                        url_path = replace(url_path, "keywords="&keyword, "")
                                        if instr(url_path, "&page=")>0 then 
											url_path = replace(replace(url_path, "&page="& page, ""), "&page=", "")
										end if
										if instr(url_path, "?page=")>0 then 
											url_path = replace(replace(url_path, "page="& page&"&", ""), "page=&", "")
										end if	
										if right(url_path, 1) <> "&" then 
											url_path = url_path &"&"
										end if
                                        Keyword = trim(Keyword)
                                         %>
                                        <table border="0" cellspacing="0" cellpadding="2" >
                                          <tr>
                                            <td  valign="middle">Records:&nbsp;<%= rscount %>&nbsp;</td>
                                            <td width="12" > <a href="<%=url_path%>keywords=<%=Keyword%>&page=1" style="display:block;background:url(/soft_img/app/arrow_left_2.gif) no-repeat 0 55%;width:12px;">&nbsp;</a></td>
                                            <td width="12"  style="display:block;background:url(/soft_img/app/arrow_left_1.gif) no-repeat 0 55%;width:12px;"> <a href="<%=url_path%>keywords=<%=Keyword%>&page=<%if page=1 then
                                        response.write 1
                                        else
                                        response.write page-1
                                        end if%>" >&nbsp;</a></td>
                                          
                                              <td nowrap="nowrap">
											  <%
                                              '	dim more_page
            '								    if  rspagecount > 20 then 
            '										rspagecount = 20
            '										more_page = "..."
            '									end if
			
                                                for x=1 to rspagecount 
                                                    response.write "<div style=""float:left;"">"
                                                    if x = page then 
                                                        response.write "&nbsp;&nbsp;<a href='"&url_path&"keywords="& Keyword&"&page="& x &"' style='color: blue;font-weight: bold;'>" & x & "</a>&nbsp;&nbsp;"
                                                    else
                                                        response.write "&nbsp;&nbsp;<a href='"&url_path&"keywords="& Keyword&"&page="& x &"' style=''>" & x & "</a>&nbsp;&nbsp;"
                                                    end if
                                                    response.write "</div>"
                                                next
                                                
                                              %></td>
                                          
                                            <td width="12"> <a href="<%=url_path%>keywords=<%=Keyword%>&page=<%if page=rspagecount then
                                        response.write rspagecount
                                        else
                                        response.write page+1
                                        end if%>" style="display:block;background:url(/soft_img/app/arrow_right_1.gif) no-repeat 0 55%;width:12px;">&nbsp;</a></td>
                                            <td width="12"><a href="<%=url_path%>keywords=<%=Keyword%>&page=<%=rspagecount%>"  style="display:block;background:url(/soft_img/app/arrow_right_2.gif) no-repeat 0 55%;width:18px;">&nbsp;</a></td>
                                            <td width="70">Pages:&nbsp;<%=rspagecount%> &nbsp;</td>
                                          </tr>
                                        </table>
                                        <%' end if%>
                                        
                                        <hr size="1">
                                        <div style="text-align:left;padding-left: 1em; display:none">HOST KEYWORDS：
                                              <% 
'                                              set krs = conn.execute("select keyword from tb_keyword")
'                                              if not krs.eof then 
'                                              do while not krs.eof
'                                                    response.write "<a href=""product_search.asp?class=Search&keywords="&krs(0)&""" style='padding: 5px;'>" & krs(0) & "</a>"
'                                              krs.movenext
'                                              loop
'                                              end if
'                                              
'                                              krs.close : set krs = nothing
                                                
                                            %>
                                        </div>
                                      </td>
                                    </tr>
                                </table>
                              </td>
                            </tr>
                          </table>
                </div>
            <!-- main end 	-->
        </td>
        <td id="page_main_right" valign="top" class='page_frame'>
        	<!-- right begin -->                   	
            	<div id="page_main_right_html"><!--#include virtual="/Site/inc/inc_right.asp"--></div>
            <!-- right end 	-->
        </td>
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
function clearForm() {
    $("input[name=keywords]").val("");
    $("input[name=priceBegin]").val("")
    $("input[name=priceEnd]").val("")
    $("select[name=categories]").val("")
    $("select[name=manufacturer]").val("")
}
</script>
