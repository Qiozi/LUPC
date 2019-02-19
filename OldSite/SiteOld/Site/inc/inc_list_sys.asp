<!--#include virtual="/site/inc/inc_helper.asp"-->
<div id="page_main_list_head" style="height: 40px; overflow: hidden; border-bottom: 1px solid #cccccc;">
</div>
<div id="parent_main">
    <%
	Dim sort_by				:	sort_by				=	SQLescape(request("sort_by"))' price up(1) or down(2)'
	Dim sort_by_sql			:	sort_by_sql			=	""
	page				= 	SQLescape(request("page"))
	Dim keywords			: 	keywords			= 	SQLescape(request("keywords"))
	
	Dim keywords_g			:	keywords_g			=	null
	Dim price_area			:	price_area			=	SQLescape(request("price_area"))	'12~1222	
	
	Dim menu_child_serial_no:	menu_child_serial_no= 	SQLescape(request("cid"))
	dim sql_where_params	:	sql_where_params	=	null
	
	Dim sql_stock			:	sql_stock = ""
	Const LAYOUTMAXTIMESPAN							=	180	
	Dim timespan			:	timespan 			= 1000
	Dim tmp_system_price
	Dim tmp_system_save_price
	Dim tmp_system_price_first
	Dim price_and_save
	Dim view_stock_filter	:	view_stock_filter 	=	SQLescape(request("stock"))
	
	dim ebay_code, ebay_price
	dim keyword, parent_category, parent_name, page_category_request, rebate_save_price, rebate_comment,rebate_comment2
	rebate_save_price = 0
	'page_category_request = SQLescape(request("page_category"))
	dim product_lists
	parent_name = ""
	parent_category = SQLescape(request("class"))
	if parent_category = "Search" then 
		parent_category = 1
	end if



      if instr(Session("search_keywords"), "|")>0 then 
            Session("search_keywords") = replace(Session("search_keywords"), "|", "[qiozi]")           
      end if  
      if instr(keywords, "|")>0 then 
            keywords = replace(keywords, "|", "[qiozi]")           
      end if  
      
      if keywords = "" and Session("search_keywords") <> "" then 
            keywords = Session("search_keywords")
            if instr(keywords, "=") >0 then 
                keywords = right(keywords, len(keywords) - instr(keywords, "="))
            end if
      end if
      Session("search_keywords") = menu_child_serial_no & "=" & keywords

      'response.Write "<br>"&Session("search_keywords")
	
	' is virtual category
	
	    dim category_query_keywords, category_query_keywords_group
        category_query_keywords =  keywords
        if instr(category_query_keywords, "|")>0 then 
            category_query_keywords = replace(category_query_keywords, "|", "[qiozi]")
        end if   

       
        if instr(category_query_keywords, "[qiozi]")>0 then 
                category_query_keywords = replace(category_query_keywords, "[qiozi]", "+")
        end if

        
		dim is_virtual		:		is_virtual =	false
		Dim virtual_keywords, virtual_keyword_sql
		dim vir_key_sql, vir_key_sqls
		dim tmp_split_line
		
        tmp_split_line= ""		        
		virtual_keywords = ""
		
		'
		' virtual 
		'
		'
		Set rs = conn.execute("Select is_virtual, page_category, ifnull(round(now()-update_price_date),1000) t"&_
							"  from tb_product_category where menu_child_serial_no="& SQLquote(menu_child_serial_no))
		
		if not rs.eof then
			if rs(0)=1 then 
				is_virtual = true
				
			end if		
			
			page_category_request = rs(1)
			if len(rs(2))>8 then 
				timespan	=	1000
			else
				timespan	=	clng(rs(2))
			end if

		end if
		rs.close : set rs = nothing

				  	
	if (menu_child_serial_no <> "" and isnumeric(menu_child_serial_no)) or (parent_category="Search" ) then
	
	if page_category_request = "1" then
		dim ps,rspagecount, line_list
		
		ps = 20

		if product_id_list <> "0" then 
			split_line_sql = " and product_serial_no in ("& product_id_list & ") "
		end if
		
'
		'
		' 	update price
		'
		If timespan > LAYOUTMAXTIMESPAN Then 
			UpdateSystemPriceByCategoryID(menu_child_serial_no)
		End if
		
		'
		' price total
		'
		if len(price_area)>=4 then 
			If instr(price_area, "~") >0 then 
				if Current_System = CSUS then
					sql_where_params	=	sql_where_params & " and (p.product_current_price-p.product_current_discount) * "& getCurrencyConvert() &" > "& SQLescape(split(price_area,"~")(0)) &" and (p.product_current_price-p.product_current_discount) < "& SQLescape(split(price_area,"~")(1))
				else
					sql_where_params	=	sql_where_params & " and (p.product_current_price-p.product_current_discount) > "& SQLescape(split(price_area,"~")(0)) &" and (p.product_current_price-p.product_current_discount) < "& SQLescape(split(price_area,"~")(1))
				end if
			End if
		end if
		'
		'
		' 	keywords
		'
		if len(keywords)>0 then 
			If instr(keywords, "[qiozi]")>0 then 
				keywords_g = split(keywords, "[qiozi]")
				
				for i=lbound(keywords_g) to ubound(keywords_g)
					if ucase(SQLescape(trim(keywords_g(i)))) <> "ALL"  and SQLescape(trim(keywords_g(i))) <>"-1" then 
						'sql_where_params	=	sql_where_params & " and (keywords like '%"& trim(keywords_g(i)) & "%' or p.product_name_long_en like '%"& trim(keywords_g(i)) & "%')"
						sql_where_params	=	sql_where_params & " and (keywords like '%["& trim(keywords_g(i)) & "]%' or keywords like '%"& trim(keywords_g(i)) & "%' or p.product_name_long_en like '%"&  trim(keywords_g(i))  & "%')"
					End if
				Next
			else
				if ucase(SQLescape(keywords)) <> "ALL"  and SQLescape(trim(keywords)) <>"-1" then 
					'sql_where_params	=	sql_where_params & " and (keywords like '%"& keywords & "%' or p.product_name_long_en like '%"& trim(keywords) & "%')"	
					sql_where_params	=	sql_where_params & " and (keywords like '%["& keywords & "]%' or keywords like '%"& keywords & "%' or p.product_name_long_en like '%"& keywords & "%' )"					
				end if
			end if															
		End if
		'REsponse.write Keywords
		'response.write "<br>"& sql_where_params
		'
		'
		'	sort by 
		'
		'
		if sort_by	=	"1" or sort_by  = 	"2" then 
			if sort_by	=	"1"  then 
				sort_by_sql	=	" order by p.product_current_price-p.product_current_discount desc"
			else
				sort_by_sql	=	" order by p.product_current_price-p.product_current_discount asc"
			end if 
		else
			sort_by_sql		=	" order by p.product_current_discount desc, p.product_serial_no desc "
		end if
		
		'
		'	stock
		'	
		'
		if view_stock_filter = "In Stock" then 
			sql_stock = " and (p.product_store_sum>0 or p.ltd_stock>0) "
		elseif view_stock_filter = "Out of Stock" then 
			sql_stock = " and (p.product_store_sum<=0 and p.ltd_stock<=0) "
		else
		
		end if

			if is_virtual then 
			    if virtual_keywords <> "" then 
			        set rsc = conn.execute("select count(p.product_serial_no) from tb_product p where 1=1 "& virtual_keyword_sql &"  and tag=1  and  split_line=0 and is_non<> 1 "&  sql_where_params & sql_stock & " order by product_current_price-product_current_discount asc")
			    else
                    set rsc = conn.execute("select count(p.product_serial_no) from (Select distinct lu_sku, menu_child_serial_no, showit from tb_product_virtual where menu_child_serial_no="&menu_child_serial_no&" and showit=1) pv inner join tb_product p on pv.lu_sku=p.product_serial_no where  p.is_non<> 1 and p.tag=1 and  p.split_line=0 "&  sql_where_params & sql_stock &" order by pv.lu_sku desc")
			    end if
			else
				set rsc = conn.execute("select count(p.product_serial_no) from tb_product p where menu_child_serial_no="&menu_child_serial_no&" and tag=1 and  split_line=0  and is_non<> 1 "& sql_where_params  & sql_stock &" order by product_order asc")
			end if
			'if SQLescape(request("keywords")) <> "" then ps = 10
			rspagecount=-int(-rsc(0)/ps)
			rscount = rsc(0)
			page=SQLescape(request("page"))
			if not isnumeric(page) or page = "" then page=1
			page=cint(page)
			if page>rspagecount then page=rspagecount
			if page<1 then page=1
		
		
		if is_virtual then 
		    if virtual_keywords <> "" then   
		        set rs= conn.execute("select p.product_serial_no,p.menu_child_serial_no, p.product_name,p.product_short_name, p.product_name_long_en,p.product_current_price ,p.split_line, p.hot, p.new, p.other_product_sku,p.product_current_discount, "&_
    "case when p.product_store_sum >2 then 2 "&_
    "when ltd_stock >2 then 2  "&_
    "when product_store_sum + ltd_stock >2 then 2  "&_
    "when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
    "when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
    "when ltd_stock <=2 and ltd_stock >0 then 3 "&_
    "when product_store_sum +ltd_stock =0 then 4 "&_
    "when product_store_sum +ltd_stock <0 then 5 end as ltd_stock"&_
			    
			    " "&_
                ",p.prodType, p.img_url,p.href_url from tb_product p  where 1=1 and split_line=0 "& virtual_keyword_sql &" and ( p.tag=1 "& sql_not_issue & ") and p.is_non<> 1 and p.product_serial_no>0 "& sql_where_params & sql_stock  & sort_by_sql&"  limit "& ps*(page-1) &","&ps)
    		
		    else

			    set rs= conn.execute("select product_serial_no,p.menu_child_serial_no, product_name,product_short_name, product_name_long_en,product_current_price ,split_line, hot, new, other_product_sku,product_current_discount, "&_
    "case when product_store_sum >2 then 2 "&_
    "when ltd_stock >2 then 2  "&_
    "when product_store_sum + ltd_stock >2 then 2  "&_
    "when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
    "when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
    "when ltd_stock <=2 and ltd_stock >0 then 3 "&_
    "when product_store_sum +ltd_stock =0 then 4 "&_
    "when product_store_sum +ltd_stock <0 then 5 end as ltd_stock"&_			   
			    " "&_
                " ,p.prodType, p.img_url,p.href_url  from tb_product_virtual pv inner join tb_product p on p.product_serial_no=pv.lu_sku  where pv.menu_child_serial_no="&menu_child_serial_no&" and pv.showit=1 and ( p.tag=1 "& sql_not_issue & ")  and split_line=0  and is_non<> 1 and p.product_serial_no>0  "& sql_where_params & sql_stock & sort_by_sql &"   limit "& ps*(page-1) &","&ps)
		    end if
		else
			
			set rs= conn.execute("select p.product_serial_no,p.menu_child_serial_no, p.product_name,p.product_short_name, p.product_name_long_en,p.product_current_price ,p.split_line, p.hot, p.new, p.other_product_sku,p.product_current_discount, "&_
"case when p.product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock "&_
			
			", 0 is_view_stock "&_
            " ,p.prodType, p.img_url, eonline.itemid,p.href_url from tb_product p left join tb_ebay_selling eonline on eonline.luc_sku=p.product_serial_no  where menu_child_serial_no="& SQLquote(menu_child_serial_no)&" and split_line=0  and ( p.tag=1 "& sql_not_issue & ") and p.is_non<> 1 and p.product_serial_no>0   "& sql_where_params& sql_stock & sort_by_sql &" limit "& ps*(page-1) &","&ps)
		
        	end if
	
       
       
		if not rs.eof then
		split_line_id = 0
		do while not rs.eof 
'		ebay_code = rs("ebay_code")
'		ebay_price = rs("ebay_price")
		
		If not (WARRARY_CATEGORY_ID = menu_child_serial_no and cdbl(rs("product_current_price")) = 0 and rs("split_line")=0) then 
            
			' 分割?
			'response.write rs("split_line")
			if 1 = rs("split_line") then
				split_line_id = rs("split_line")
				rscount = rscount -1
    %>
    <table width="100%" border="0" cellpadding="2" cellspacing="0" bgcolor="#E7E7E7"
        align="center">
        <tr>
            <td height="70" class="text_green_11" style="padding-bottom: 4px; min-height: 70;
                overflow: hidden;">
                <strong>
                    <%= ucase(rs("product_short_name")) %></strong>
                <br />
                <%=rs("product_name_long_en")%>
                <% if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and not is_virtual then %>
                <br />
                <a href="/q_admin/product_part_move_or_copy.aspx?cmd=Move&sku=<%=rs("product_serial_no")%>"
                    onclick="js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;"
                    title="Move">Move</a>|<a href="/q_admin/product_part_move_or_copy.aspx?cmd=Copy&sku=<%=rs("product_serial_no") %>"
                        onclick="js_callpage_cus(this.href, 'move_  _part', 520, 300);return false;"
                        title="Copy">Copy</a>
                <% end if%>
            </td>
        </tr>
    </table>
    <%
				else
    %>
    <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #dddddd;">
        <tr>
            <td rowspan="2" width="70">
                <a href="<%= LAYOUT_HOST_URL %>Product_parts_detail.asp?pro_class=<%=rs("menu_child_serial_no")%>&id=<%=rs("product_serial_no")%>&cid=<%=menu_child_serial_no%>"
                    target="_blank">
                    <% if len(rs("img_url"))<6 or isnull(rs("img_url")) then  %>
                    <img src="<%= GetImgMinURL(HTTP_PART_GALLERY , PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku"))) %>"
                        width="50" hspace="10" border="0" onerror="this.src='http://www.lucomputers.com/soft_img/shuyin/2.gif'; this.width='40';">
                    <% else
                                        response.Write "<img src="""& rs("img_url") & """ "&_
                                	                    " width=""50"" hspace=""10"" border=""0"" onerror=""this.src='http://www.lucomputers.com/soft_img/shuyin/2.gif'; this.width='40';"" >"
                                   end if
                    %>
                </a>
                <% 
							if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and not is_virtual then %>
                <a href="/part_product_photo_manage.aspx?ProductID=<%= rs("product_serial_no") %>"
                    onclick="js_callpage_cus(this.href, 'right_manage', 400,400);return false;">M</a>
                <br />
                <a href="/q_admin/product_part_move_or_copy.aspx?cmd=Move&sku=<%=rs("product_serial_no")%>"
                    onclick="js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;"
                    title="Move">Move</a>|<a href="/q_admin/product_part_move_or_copy.aspx?cmd=Copy&sku=<%=rs("product_serial_no") %>"
                        onclick="js_callpage_cus(this.href, 'move_copy_part', 520, 300);return false;"
                        title="Copy">Copy</a>
                <% end if%>
            </td>
            <td colspan="3" style="text-align: right">
            </td>
        </tr>
        <tr>
            <td style="padding-right: 10px;" valign="top">
                <div style="height: 65px; min-height: 65px; overflow: hidden; text-overflow: ellipsis">
                    <%
                                        if len(rs("href_url"))> 5 then
                    %>
                    <a class="hui-orange-12" href="<%= rs("href_url") %>" target="_blank">
                        <%
                                        else  
                        %>
                        <a class="hui-orange-12" href="<%= LAYOUT_HOST_URL %>product_parts_detail.asp?class=<%=parent_category%>&pro_class=<%=rs("menu_child_serial_no")%>&id=<%=rs("product_serial_no")%>&cid=<%=menu_child_serial_no%>"
                            target="_blank">
                            <%
                                        end if


                      				 ' 给关键字加颜?
									  dim tmp_product_title
									  tmp_product_title = rs("product_name")
									  if tmp_product_title = "" then 
										tmp_product_title = rs("product_short_name")
									  end if
									  if memory_size <> "" or memory_size <> "-1" then 
										 tmp_product_title = FontColor(tmp_product_title, memory_size, "red")
									  end if
									  if product_size <> "" or product_size <> "-1" then 
										tmp_product_title = FontColor(tmp_product_title, product_size, "blue")
										
									  end if
									  if factory <> "" or factory <> "-1" then 
										tmp_product_title = FontColor(tmp_product_title, factory, "green")
										
									  end if
									  response.write tmp_product_title

                                      if LCase(trim(rs("prodType")))<>LCase("NEW") then response.Write "&nbsp;" & rs("prodType")

                                      if len(trim(rs("itemid"))) = 12 then 
                                          response.Write "<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&rd=1&item="& rs("itemid") &"' target='_blank'><img src ='/soft_img/app/ebay_logo.jpg' style='border:0px;'></a>"
                                      end if	

                            %>
                        </a>
                        <%
        
                                    GetHotImg(rs("hot"))
                                    
                                    GetNewImg(rs("new"))
                                    ' on sale
                                    if cstr(rs("product_current_discount")) <> "0" then 
                                        GetSaleImg(1)
                                    end if
                                    
                                    rebate_save_price = 0
                                    set grrs = conn.execute(sql_sale_promotion_rebate_sign(rs("product_serial_no")))
                                    if not grrs.eof then
                                        GetRebateImg(rs("product_serial_no"))
                                        rebate_save_price = grrs("save_cost")
                                    end if
                                    grrs.close : set grrs = nothing
                                    
        
                        %>
                        <br />
                        <span class="text_hui2_11">
                            <% if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and not is_virtual then %>
                            <%= "<span style='color:blue;'>"& getPartVirtualCategoryName(rs("product_serial_no")) & "</span><br>"%>
                            <% end if %>
                            <%=rs("product_name_long_en")%>
                        </span>
                </div>
            </td>
            <td width="80">
                <table width="94%" border="0" cellspacing="0" cellpadding="1">
                    <tr>
                        <td align="right" class="text_orange_11">
                            <span class="price_big">
                                <%
                     dim single_save_price
					 single_save_price = rs("product_current_discount")
					  
					  if cdbl(single_save_price) <> 0  and cdbl(rebate_save_price) > 0  then
							response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"& formatcurrency(ConvertDecimal(rs("product_current_price")),2)& "</span><br/>"
							'response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"& formatcurrency(ChangePrice(removeSavePrice(rs("product_current_price") , single_save_price) , card_rate ))& "</span><br/>"
							response.write rebate_comment & ConvertDecimalUnit(CURRENT_SYSTEM, cdbl(rs("product_current_price"))-cdbl(single_save_price)- cdbl(rebate_save_price))
					  elseif cdbl(single_save_price) <> 0 then
							response.write  "<span  class='price_dis'>"& formatcurrency(ConvertDecimal(rs("product_current_price")),2)& "</span><br/>"
							response.write  ConvertDecimalUnit(CURRENT_SYSTEM, cdbl(rs("product_current_price"))- cdbl(single_save_price))
					  elseif  cdbl(rebate_save_price) > 0 then 
							response.write  "<span  class='price_dis'>"& formatcurrency(ConvertDecimal(rs("product_current_price")),2)& "</span><br/>"
							response.write rebate_comment & ConvertDecimalUnit(CURRENT_SYSTEM, cdbl(rs("product_current_price"))- cdbl(rebate_save_price))
					  else
							response.write  ConvertDecimalUnit(CURRENT_SYSTEM, cdbl(rs("product_current_price"))  )
					  end if
                                %></span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="text_orange_11">
                            SKU#<%= rs("product_serial_no") %>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right" class='stockstatus' stock='<%= rs("ltd_stock") %>'
                            sku='<%= rs("product_serial_no") %>'>
                            <%
                        'if cint(rs("is_view_stock"))>0  or cint(rs("ltd_stock"))>0 then                                                                     
                        '    response.Write `(rs("product_serial_no"), rs("ltd_stock")) 
                        'end if
                            %>
                        </td>
                    </tr>
                    <tr align="right">
                        <td>
                            <a href="/site/Shopping_Cart_pre.asp?cid=<%=rs("menu_child_serial_no")%>&Pro_Id=<%=rs("product_serial_no")%>">
                                <img src="/soft_img/app/buyNow_bt.gif" width="56" height="13" border="0"></a>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <% end if
                
            end if ' warrary id 
        rs.movenext
        loop
        else
            Response.write "<div style='text-align:center; font-weight:bold;font-size:10pt;padding-top: 20px;'>No Match Data.</div>"
        end if
        rs.close 
    end if
    
    dim div_compare_price
    div_compare_price = "0"
					
					if page_category_request = "0"  then
						
							'
							'
							' 	update price
							'
							If  timespan > LAYOUTMAXTIMESPAN Then 
								UpdateSystemPriceByCategoryID(menu_child_serial_no)
							End if
							'response.Write timespan
							'
							' price total
							'
							if len(price_area)>=4 then 
								If instr(price_area, "~") >0 then 
									if Current_System = CSUS then
										sql_where_params	=	sql_where_params & " and tmp_sell * "& getCurrencyConvert() &" > "& SQLescape(split(price_area,"~")(0)) &" and tmp_sell * "& getCurrencyConvert() &" < "& SQLescape(split(price_area,"~")(1))
									else
										sql_where_params	=	sql_where_params & " and tmp_sell > "& SQLescape(split(price_area,"~")(0)) &" and tmp_sell < "& SQLescape(split(price_area,"~")(1))
									end if
								End if
							end if
							'response.write Current_system
							'
							'
							' 	keywords
							'
							if len(keywords)>0 then 
								If instr(keywords, "[qiozi]")>0 then 
									keywords_g = split(keywords, "[qiozi]")
									
									for i=lbound(keywords_g) to ubound(keywords_g)
										if ucase(SQLescape(trim(keywords_g(i)))) <> "ALL"  and SQLescape(trim(keywords_g(i))) <>"-1" and keywords_g(i)<>"" then 
											sql_where_params	=	sql_where_params & " and keywords like '%["& trim(keywords_g(i)) & "]%'"
										End if
									Next
								else
									if ucase(SQLescape(keywords)) <> ucase("ALL") and SQLescape(keywords) <>"-1" and keywords<>"" then 
										sql_where_params	=	sql_where_params & " and keywords like '%["& keywords & "]%'"					
									end if
								end if															
							End if
                           ' response.Write sql_where_params
							'
							'
							'	sort by 
							'
							'
							if sort_by	=	"1" or sort_by  = 	"2" then 
								if sort_by	=	"1"  then 
									sort_by_sql	=	" order by tmp_sell desc"
								else
									sort_by_sql	=	" order by tmp_sell asc"
								end if 
							else
								sort_by_sql		=	" order by view_count desc "
							end if
							
							
							ps = 5
							
							set rsc = conn.execute("select count(st.id) from  tb_ebay_system st inner join tb_ebay_system_and_category ec on ec.SystemSku = st.id where st.showit=1 and ec.eBaySysCategoryId="&SQLquote(menu_child_serial_no)& sql_where_params )
							
							
							rspagecount=-int(-rsc(0)/ps)
							rscount = rsc(0)
							'
							if not isnumeric(page) or page = "" then page=1
							page=cint(page)
							if page>rspagecount then page=rspagecount
							if page<1 then page=1
						
							dim system_cpu_logo_filename,system_cpu_logo_filename_vc, logo_image_filename_sys, logo_count
							system_cpu_logo_filename = ""
							system_cpu_logo_filename_vc = ""
							

							set rs= conn.execute("select st.*, eonline.itemid from tb_ebay_system st inner join tb_ebay_system_and_category ec on ec.SystemSku = st.id "&_
                            " left join tb_ebay_selling eonline on eonline.sys_sku=st.id "&_
                            " where st.showit=1 and ec.eBaySysCategoryId="&SQLquote(menu_child_serial_no) &" "& sql_where_params & sort_by_sql &"  limit "& ps*(page-1) &","&ps )
												
                     	if not rs.eof then
						logo_count = 0
						do while not rs.eof     						
						    
						    div_compare_price 			= div_compare_price & "," & rs("id")
						    logo_count 					= 0
						    'system_cpu_logo_filename 	= rs("logo_image_filename")
						    'system_cpu_logo_filename_vc = rs("logo_image_filename_vc")
						    'logo_image_filename_sys 	= rs("logo_image_filename_sys")
    						
						    if len(system_cpu_logo_filename )  > 4 then 
							    logo_count = logo_count + 1
						    end if
						    if len(system_cpu_logo_filename_vc )  > 4 then 
							    logo_count = logo_count + 1
						    end if
						    if len(logo_image_filename_sys )  > 4 then 
							    logo_count = logo_count + 1
						    end if
    						
    						
						    ''
						    ' compare sku
						    '
						    dim ids
						    ids = split(session("sys_compare_skus"), ",")
    %>
    <table width="100%" border="0" cellspacing="0" cellpadding="0" align="center">
        <tr>
            <td width="130" valign="top">
                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                        <td style="text-align: center">
                            <a href="<%= LAYOUT_HOST_URL %>system_view.asp?cid=<%= menu_child_serial_no %>&class=<%=request("class")%>&id=<%=rs("id")%>">
                                <img src="<%=GetSystemPhotoByID2(rs("id"))%>" border="0"></a>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" class="text_blue_13">
                            <p>
                                [System No.
                                <%= rs("id") %>]</p>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table style="width: 100%" border="0" cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <%
											 response.Write( "<td style=""padding-bottom:3px;")
											
											response.write "width: "& logo_count * 40& "px;"
											
											response.write """>"
											
											 if system_cpu_logo_filename <> "" then 
												response.write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& system_cpu_logo_filename &"""   >"
												
											 end if								
											
											 if system_cpu_logo_filename_vc <> "" then 
												response.Write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& system_cpu_logo_filename_vc &"""  >"
											 end if
											 
											  if len(logo_image_filename_sys) > 4 then 
												 response.Write "&nbsp;<img  width=""31"" height=""35"" src="""&HTTP_PART_GALLERY_CPU_LOGO_PATH& logo_image_filename_sys &"""   >"
											 end if
											
											response.write ("</td>")
												
												
												response.Write("<td style='min-height: 40px;'><a  href=""/site/system_view.asp?cid="& menu_child_serial_no &"&id="& rs("id")&"&class="&request("class")&"""><span id='logo_cpu_name'  class=""system_title100"" >")
													
												if trim(rs("ebay_system_name")) <> "" then 
														response.Write rs("ebay_system_name")
												else
													set cpurs2 = conn.execute("select p.product_short_name from tb_ebay_system_parts sp "&_
                                                    " inner join tb_product p on  sp.luc_sku=p.product_serial_no "&_
                                                    " inner join tb_ebay_system_part_comment espc on espc.id=sp.comment_id and is_cpu=1"&_
                                                    " where p.tag=1 and p.is_non=0 and sp.system_sku="& SQLquote(rs("id"))&" ")
													if not cpurs2.eof then 
															response.Write cpurs2("product_short_name")	
                                                            response.Write("&nbsp;System</span></a></td>")		
													end if
													cpurs2.close :set cpurs2 = nothing									
												End if

											    if len(trim(rs("itemid"))) = 12 then 
                                                    response.Write "<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&rd=1&item="& rs("itemid") &"' target='_blank'><img src ='/soft_img/app/ebay_logo.jpg' style='border:0px;'></a>"
                                                end if				
                                    %>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
                            <table width="100%" height="1" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="background: url(/soft_img/app/line2.gif) repeat-x 0 50%">
                                        <img src="/soft_img/line2.gif" width="3" height="1">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" class="text_hui_11" style="height: 120px;">
                            <%

							' 列表共有多少个零??
							dim sys_product_order_sum
							set countrs  = conn.execute("select count(s.id) from tb_ebay_system_parts s,tb_product p where s.system_sku="& SQLquote(rs("id")) &" and p.tag=1 and (p.is_non=0 or p.product_name like '%onboard%' or  p.product_name like '%default basic fan%') and p.menu_child_serial_no not in("&LAYOUT_none_display_product_category&") and p.product_serial_no=s.luc_sku order by s.id asc")
							sys_product_order_sum = countrs(0)
							set countrs = nothing
							
							' 读取零件
						  	set crs = conn.execute("select p.product_short_name, p.product_serial_no, s.part_quantity from tb_ebay_system_parts s,tb_product p where s.system_sku="& SQLquote(rs("id"))&" and p.tag=1 and (p.is_non=0 or p.product_name like '%onboard%' or  p.product_name like '%default basic fan%') and p.menu_child_serial_no not in("&LAYOUT_none_display_product_category&") and p.product_serial_no=s.luc_sku order by s.id asc")
							
							if not crs.eof then
								response.write "<table width=""100%"" border='0' cellpadding='0' cellspacing='0'><tr><td width=""48%"" height=""20px;"" valign='top'>"
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
                            <div style="font-size: 8pt; background: url(/soft_img/app/arrow3.gif) no-repeat 0% 55%;
                                padding-left: 12px;">
                                <% if cint(crs("part_quantity"))<> 1 then response.write crs("part_quantity") & "x&nbsp;"%><a
                                    href="/Site/view_part.asp?id=<%= crs("product_serial_no")%>" onclick="javascript:js_callpage_cus(this.href, 'view_part', 610, 616);return false;"><%= replace(crs("product_short_name"), "(Verify availability on motherboard)", "") %>
                                </a>
                            </div>
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
                        <td>
                            <table border="0" align="right" cellpadding="2" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td width="120">
                                        <span>
                                            <input type="checkbox" name="compare_system" value="<%=rs("id")%>" />Compare <a href="/product_list_sys_compare_set_sku_null.asp"
                                                target="ifr_product_sub_list">[Reset]</a> </span>
                                    </td>
                                    <td class="text_red_12b" style="text-align: right">
                                        <span class="price_big"><span class="text_red_12b" style="padding-right: 8px;">
                                            <%
									  
									   
									  rebate_save_price = 0 'FindSystemRebatePrice(rs("system_templete_serial_no"))
									  'price_and_save = GetSystemPriceAndSave(rs("system_templete_serial_no"))
									  
									  tmp_system_save_price 	= cdbl(rs("tmp_discount"))
									  tmp_system_price			= cdbl(rs("tmp_sell"))
									  tmp_system_price_first 	= tmp_system_price + tmp_system_save_price					  
									  
									 
									  if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
									  	response.write "<span  class=""price_dis"">" & formatcurrency( cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
									  end if
									  
									  Response.write "<span class='price'>"
									  if isnumeric(tmp_system_price) then 
									  	if rebate_save_price > 0 then 
											response.write rebate_comment2 & "&nbsp;" & formatcurrency( tmp_system_price - rebate_save_price, 2)
										else
									  		response.write formatcurrency( tmp_system_price, 2)
										end if
									  end if
									  Response.write "</span>"
                                            %><span class="price_unit"><%= CCUN %></span> </span></span>
                                    </td>
                                    <td width="61">
                                        <a href="<%= LAYOUT_HOST_URL %>system_view.asp?cid=<%= menu_child_serial_no %>&class=<%=request("class")%>&id=<%=rs("id")%>">
                                            <img src="/soft_img/app/detail_bt.gif" width="56" height="13" border="0"></a>
                                    </td>
                                    <td width="73">
                                        <a href="<%= LAYOUT_HOST_URL %>computer_system.asp?id=<%=rs("id")%>">
                                            <img src="/soft_img/app/custmize_bt.gif" width="66" height="13" border="0"></a>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="20" colspan="2" <%  if len(ebay_code) > 10 then response.write " valign=""top""" %>>
                <table width="100%" border="0" cellpadding="0" cellspacing="1" bgcolor="#D0DAE1">
                    <tr>
                        <td height="3" bgcolor="#FFFFFF">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <%
				  	rs.movenext:loop
					
					Else
						Response.write "<div style='line-height:50px; text-align:center'>No Match Data.</div>"
					end if
					rs.close : set rs = nothing
					
				  end if
				  'parentrs.close : set parentrs = nothing
			 end if
'				  rs.movenext
'				  loop%>
    <br>
    <% if page_category_request = "0"  then %>
    <table width="115" height="24" border="0" cellpadding="0" cellspacing="0" align="right"
        title='compare' class="btn_table">
        <tr>
            <td width="6">
                <img src="/soft_img/app/3232.gif" width="6" height="24" alt="">
            </td>
            <td class="btn_middle">
                <a class="btn_img">Compare Price</a>
            </td>
            <td width="6">
                <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="">
            </td>
        </tr>
    </table>
    <br>
    <div style="text-align: right; clear: both">
        (Please select maximum 5 items from this category.)</div>
    <%  end if  %>
    <input type='hidden' id="page_main_prod_page_v" value="1" />
    <div id="page_list_floor" style="height: 60px; padding-top: 30px; vertical-align: top">
        <table width="100%" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td valign="top">
                    <%
							dim url_path

							url_path = Request.ServerVariables("HTTP_url")
							if instr(url_path, "&page=")>0 then 
								url_path = replace(replace(url_path, "&page="& page, ""), "&page=", "")
							end if
							if instr(url_path, "?page=")>0 then 
								url_path = replace(replace(url_path, "page="& page&"&", ""), "page=&", "")
							end if	
							if instr(url_path, "inc_list_sys")>0 then
								url_path = replace(url_path, "inc/inc_list_sys", "product_list")
							end if
							if instr(url_path, "inc_list_part")>0 then
								url_path = replace(url_path, "inc/inc_list_part", "product_list")
							end if
                    %>
                    <table border="0" cellspacing="0" cellpadding="1" align="right">
                        <tr>
                            <td valign="middle" style="height: 15px;">
                                Records:<span style='margin: 5px'><%= rscount %></span>
                            </td>
                            <td valign="middle">
                                <a style="display: block; background: url(/soft_img/app/arrow_left_2.gif) no-repeat 0 50%;
                                    width: 12px;" onclick="press_category_keyword('1', 'page_main_prod_page_v');">&nbsp;</a>
                            </td>
                            <td width="12" valign="middle" style="margin: 4px">
                                <a onclick="press_category_keyword('<%if page=1 then
							response.write 1
							else
							response.write page-1
							end if%>', 'page_main_prod_page_v');" style="display: block; background: url(/soft_img/app/arrow_left_1.gif) no-repeat 0 50%;
                                    width: 12px;">&nbsp;</a>
                            </td>
                            <td class="text_blue2_12" valign="middle">
                                <%
								  	for x=1 to rspagecount 
										if x = page then 
											response.write "<a onclick=""press_category_keyword('"& x&"', 'page_main_prod_page_v');"" style='color: blue;margin:0px 2px 0px 2px;float:left;'>" & x & "</a>"
										else
											response.write "<a onclick=""press_category_keyword('"& x&"', 'page_main_prod_page_v');"" style='margin:0px 2px 0px 2px;float:left;'  >" & x & "</a>"
										end if
									next
                                %>
                            </td>
                            <td width="12" valign="middle">
                                <a onclick="press_category_keyword('<%if page=rspagecount then
							response.write rspagecount
							else
							response.write page+1
							end if%>', 'page_main_prod_page_v');" style="display: block; background: url(/soft_img/app/arrow_right_1.gif) no-repeat 0 50%;
                                    width: 12px;">&nbsp;</a>
                            </td>
                            <td width="12" valign="middle">
                                <a onclick="press_category_keyword('<%= rspagecount%>', 'page_main_prod_page_v');"
                                    style="display: block; background: url(/soft_img/app/arrow_right_2.gif) no-repeat 0 50%;
                                    width: 18px;">&nbsp;</a>
                            </td>
                            <td style="padding-right: 5px;" valign="middle">
                                Pages:&nbsp;<%=rspagecount%>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</div>
<%
	
	function ResponseSelected(a, b)
		if (a = b) then 
			response.write " selected "
		end if
	end function 

	function FontColor(str, keyword, color)
		FontColor = str
		if(str<>"") then
			FontColor = replace(str, keyword, "<span style='color:"& color &"'>"& keyword &"</span>")
			
		end if
	end function 
	
	
	call setViewCount(false, LAYOUT_HOST_IP, menu_child_serial_no)
%>
<script language="javascript" type="text/javascript">
    $().ready(function () {
        $('#parent_main').css("text-align", "left");
        $('#page_main_list_head').html($('#page_list_floor').html()).css("line-height", "20px");

        bindHoverBTNTable();
        $('table[title=compare]').click(function () { compareSys(); });
    });

    function compareSys() {
        var v = "0";
        $('input[name=compare_system][checked]').each(function () {
            v += "|" + $(this).val();
        });
        if (v == "0") {
            alert("Please select System.");
            return;
        }
        window.location.href = "/site/system_compare.asp?ids=" + v;
    }
</script>
</body> </html> 