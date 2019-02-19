<%
'
'
'	categoryQueryKeywordGetIsExist(category_query_keywords, key)
'	categoryQueryKeywordGetIsExistGetV(category_query_keywords, id)
'	GetEbayKeywordArea(category_id, category_query_keywords)
'	GetEbaySystemList(id, page, keywords, sortby, PS)
'	GetEbaySystemURL(page)
'	WritePageButton(recordC, page, pageC)
'	ViewSystemLogo(str)
'	GetEbayKeywordIdByPrice()
'	WriteSortBy(sortby, category_id)
'
'
'
'
'
'
'
'
'
'


'--------------------------------------------------------------------------------------
function categoryQueryKeywordGetIsExist(category_query_keywords, key)
'--------------------------------------------------------------------------------------
            categoryQueryKeywordGetIsExist = ""
            if( not isempty(category_query_keywords) and category_query_keywords <>"" and not isnull(category_query_keywords) )then
                if instr(category_query_keywords, ","& key )>0 then
                    categoryQueryKeywordGetIsExist = "selected"
                end if
            end if
            
            if len(category_query_keywords)<1 and instr(key, "-1")>0 then
                    categoryQueryKeywordGetIsExist = "selected"
            end if          
            
            if categoryQueryKeywordGetIsExist = "" then categoryQueryKeywordGetIsExist = "unselected"
 end function
 
 
 
'--------------------------------------------------------------------------------------
 function categoryQueryKeywordGetIsExistGetV(category_query_keywords, id)
'--------------------------------------------------------------------------------------
            categoryQueryKeywordGetIsExistGetV = ""
            dim category_query_keywords_group, i, j, cqkg
            if( not isempty(category_query_keywords) and category_query_keywords <>"" and not isnull(category_query_keywords) )then
                if instr(category_query_keywords, ",")>0 then
                    category_query_keywords_group = split(category_query_keywords, ",")
                    for i = lbound(category_query_keywords_group) to ubound(category_query_keywords_group)
                            if instr(category_query_keywords_group(i), "|") >0 then 
                                    cqkg = split(category_query_keywords_group(i), "|")
                                    if cstr(id) = cstr(cqkg(1)) then
                                       ' response.write category_query_keywords
                                        categoryQueryKeywordGetIsExistGetV = cqkg(0)
                                    end if
                            end if
                    next
                end if
            end if
            if categoryQueryKeywordGetIsExistGetV = "" then categoryQueryKeywordGetIsExistGetV = "-1"
 end function
 
 
 
'--------------------------------------------------------------------------------------
 Function GetEbayKeywordArea(category_id, category_query_keywords)
'--------------------------------------------------------------------------------------
	Dim parent_id	:	parent_id=10000
	Dim rs 
	Dim subrs
	Dim crs
	Dim category_name :	category_name = null
	
	Set rs = conn.execute("select category_name from tb_product_category_new where category_id="& SQLquote(category_id))
	if not rs.eof then
		category_name = rs(0)
	end if
	rs.close : set rs = nothing
	
	 set rs = conn.execute("select * from tb_product_category_keyword where keyword<>'' and is_ebay=1")
        if not rs.eof then
                response.write "<table id='part_list_query_keyword' cellpadding=""0"" cellspecing=""0"" >"&vblf
                response.write "<tr><td colspan='2' style='padding: 5px;text-align:left;-+'><h2><b style='color: #4598d2;font-size: 11pt;'>Filter</b></h2></td></tr>"&vblf
				' first write Category 
				response.write "<tr>"&vblf
				response.write         "<td style='text-align:right;width: 100px;'>"&vblf
				response.write             "<b>Category:</b>"&vblf
				response.write         "</td>"&vblf
				response.write         "<td  style='text-align:left;'>"&vblf
				if  len(category_query_keywords) >0  then 
					response.write "			<input type='hidden' name='category_query_keyword_id' id='category_query_keyword_id_0' value='0'>"
					response.write "			<input type='hidden' name='category_query_keyword' id='category_query_keyword_0' value='"& categoryQueryKeywordGetIsExistGetV(category_query_keywords, 0) &"'>"
					response.write "			<a onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_0"", ""-1"", """&page_category_request&""", """& parent_class &""", """& category_id &""");'  class='"& categoryQueryKeywordGetIsExist (category_query_keywords, "-1|0")&"'> ALL </a>"
				else
					response.write "			<input type='hidden' name='category_query_keyword_id' id='category_query_keyword_id_0' value='0'>"
					response.write "			<input type='hidden' name='category_query_keyword' id='category_query_keyword_0' value='"& category_name &"'>"
					 response.write "			<a onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_0"", ""-1"", """&page_category_request&""", """& parent_class &""", """& category_id &""");' > ALL </a>"
				end if
                'response.write "			<a onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_0"", ""-1"", """&page_category_request&""", """& parent_class &""", """& category_id &""");'  class='"& categoryQueryKeywordGetIsExist (category_query_keywords, "-1|0")&"'> ALL </a>"
				
				Set crs = conn.execute("   Select category_id               "&_
	                                              " ,category_name                  "&_
	                                              " From tb_product_category_new    "&_
	                                              " where parent_category_id='"& parent_id & "' and showit=1 and is_ebay=1 order by priority asc ")
				If not crs.eof then
					Do while not crs.eof 
					
						if len(category_query_keywords) >0 then
							response.write "<a  onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_0"", """&crs("category_name")&""", """&page_category_request&""", """& parent_class &""", """& category_id &""");' class='"& categoryQueryKeywordGetIsExist (category_query_keywords, crs("category_name")&"|0")&"'>"
						else
							response.write "<a  onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_0"", """&crs("category_name")&""", """&page_category_request&""", """& parent_class &""", """& category_id &""");' class='"& categoryQueryKeywordGetIsExist (","&category_name&"|0,", crs("category_name")&"|0")&"'>"
						end if
													
						response.write         crs("category_name")
						response.write "</a>"
					crs.movenext
					loop
					
				end if
				crs.close : set crs = nothing
				
				response.write "		</td>"&vblf
                response.write "</tr>"&vblf
				
                do while not rs.eof                        
                        response.write "<tr>"&vblf
                        response.write         "<td style='text-align:right;width: 100px;'>"&vblf
                        response.write             "<b>"& rs("keyword") &":</b>"&vblf
                        response.write         "</td>"&vblf
                        response.write         "<td  style='text-align:left;'>"&vblf
                                    set subrs = conn.execute("select * from tb_product_category_keyword_sub where parent_id='"& rs("id") &"'")
                                    if not subrs.eof then 
                                                    response.write "<input type='hidden' name='category_query_keyword_id' id='category_query_keyword_id_"&rs("id")&"' value='"& rs("id") &"'>"
                                                    response.write "<input type='hidden' name='category_query_keyword' id='category_query_keyword_"&rs("id")&"' value='"& categoryQueryKeywordGetIsExistGetV(category_query_keywords, rs("id")) &"'>"
                                                    response.write "<a onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_"&rs("id")&""", ""-1"", """&page_category_request&""", """& parent_class &""", """& category_id &""");'  class='"& categoryQueryKeywordGetIsExist (category_query_keywords, "-1|"&rs("id")) &"'> ALL </a>"
                                            do while not subrs.eof 
                                                    response.write "<a  onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", ""category_query_keyword_"&rs("id")&""", """&subrs("keyword")&""", """&page_category_request&""", """& parent_class &""", """& category_id &""");' class='"& categoryQueryKeywordGetIsExist (category_query_keywords, subrs("keyword")&"|"&rs("id")) &"'>"
													
                                                    response.write         subrs("keyword")
                                                    response.write "</a>"
                                            subrs.movenext
                                            loop
                                            
                                    end if
                        subrs.close : set subrs = nothing
                        response.write         "</td>"&vblf
                        response.write "</tr>"&vblf
                rs.movenext
                loop
                response.write "</table>"
        end if
        rs.close : set rs = nothing


End Function




'--------------------------------------------------------------------------------------
Function GetEbaySystemList(id, page, keywords, sortby, PS, ebay_number)
'--------------------------------------------------------------------------------------
    Dim s
	Dim rs
	Dim recordC
	Dim pageC
	Dim SqlKeywords			:	SqlKeywords	=	" and (  "
	Dim SqlKeywords2		:	SqlKeywords2	=	null
	Dim SqlLimit			:	SqlLimit		=	null
	Dim SqlPrice			:	SqlPrice		=	null	
	Dim PriceKeywordId		:	PriceKeywordId	=	GetEbayKeywordIdByPrice()
	Dim PriceKeyword		:	PriceKeyword	=	null
	Dim PriceKeyword_g		:	PriceKeyword_g	=	null
	Dim categoryKeyword		:	categoryKeyword	=	null
	Dim SqlNumber 			:	SqlNumber		=	null
	Dim sql_categoryKeyword	:	sql_categoryKeyword	=	"" 
	Dim keywords_g
	Dim keywords_g_g
	dim i
	Dim SqlSortby			:	SqlSortby	= ""
	
	if cstr(sortby) = "1" then 
		SqlSortby	=	" order by ebay_system_price desc "
	else
		SqlSortby	=	" order by ebay_system_price asc "
	end if

	If len(keywords) > 0 then
		if(instr(keywords, ",")>0) then 
			keywords_g  = split (keywords, ",")
			
			for i=lbound(keywords_g) to ubound(keywords_g)
				if (instr(keywords_g(i), "|")) then 
					keywords_g_g 	=	split (keywords_g(i),"|")
					if trim(keywords_g_g(0)) <> "-1" and trim(keywords_g_g(0)) <> "" then 
						'
						' price 
						if cstr(PriceKeywordId) = cstr(keywords_g_g(1)) Then
							PriceKeyword	= trim(keywords_g_g(0))		
						elseif 	"0" = cstr(keywords_g_g(1)) Then	
							categoryKeyword = trim(keywords_g_g(0))
						else
							if isnull(SqlKeywords2) then 
								SqlKeywords2 	=	SqlKeywords2	&	"  keywords like '%["&SQLescape(trim(keywords_g_g(0))) &"]%'" 
							else
								SqlKeywords2 	=	SqlKeywords2	&	" and  keywords like '%["&SQLescape(trim(keywords_g_g(0)))&"]%'" 
							end if
						end if
					elseif trim(keywords_g_g(0)) = "-1" and "0" = cstr(keywords_g_g(1)) then 
						categoryKeyword = "-1"
					end if
				end if
			next
		end if
	End if

	'
	' 	price
	'
	if not isNull(PriceKeyword) then 
		if(instr(PriceKeyword, "~") >0)then 
			PriceKeyword_g = split(PriceKeyword, "~")
			if trim(PriceKeyword_g(0)) <> "" then 
				SqlPrice = " and ebay_system_price >= "& SQLquote(trim(PriceKeyword_g(0)))
			end if
			if trim(PriceKeyword_g(1)) <> "" then 
				SqlPrice = SqlPrice & " and ebay_system_price <= "& SQLquote(trim(PriceKeyword_g(1)))
			end if
		end if
	end if
	
	'
	'	keywords
	'
	if not isNull(SqlKeywords2) then
		SqlKeywords 	=	SqlKeywords	& SqlKeywords2 &	" )"
	else
		SqlKeywords = ""
	end if

	'
	'	category id of keyword
	'
	if not isNull(categoryKeyword) then 	
		if cstr(categoryKeyword) = "-1" then 
			sql_categoryKeyword = ""
		else
			Set rs = conn.execute("Select category_id from tb_product_category_new where category_name="&SQLquote(trim(categoryKeyword)))
			if not rs.eof then
					sql_categoryKeyword  = " and category_id=" & SQLquote(rs(0))
			end if
			rs.close : set rs = nothing
		end if
	else
		if trim(id) <> "" then
			sql_categoryKeyword  = " and category_id=" & SQLquote(trim(id))
		End If
	end if

	'
	'	record count
	'
	if ebay_number <> "" then 
	
		SqlNumber	=	" and id = (select max(luc_sku) from tb_ebay_item_number where item_number="& SQLquote(ebay_number) &")"
		sql_categoryKeyword = ""
	end if
	'response.write sql_categoryKeyword
	Set rs = conn.execute("Select count(id) "&_
                          " from tb_ebay_system "&_
                          " where showit=1 "& sql_categoryKeyword & SqlNumber &" and ebay_system_current_number is not null "& SqlKeywords & SqlPrice & SqlSortby & "")
	if not rs.eof then
		recordC = rs(0)
		pageC=-int(-rs(0)/PS)

		if len(page) = 0 or not isnumeric(page) then 
			page=1
		elseif isnumeric(page) then
			page=cint(page)		
		end if
		
		if page>pageC then page=pageC
		if page<1 then page=1
		
		SqlLimit	=	" limit "& ps*(page-1) &","& PS
	end if
	rs.close 
	
    Set rs = conn.execute("Select id, ebay_system_name , ebay_system_price , ebay_system_current_number"&_
                          " ,logo_filenames "&_
                          " from tb_ebay_system "&_
                          " where showit=1 "& sql_categoryKeyword & SqlNumber &" and ebay_system_current_number is not null "& SqlKeywords & SqlPrice & SqlSortby & SqlLimit&"")

    If not rs.eof then
            Response.write "<table class='ebay_system_list' cellpadding=""0"" cellspacing=""0"" style='clear:both'>" &vblf
            do while not rs.eof 
                    Set crs = conn.execute("select luc_sku, product_ebay_name, epc.comment, is_case "&_
											" , case when p.other_product_sku >0 then p.other_product_sku else p.product_serial_no end as img_sku"&_
											
                                            " from tb_product p "&_
                                            " inner join tb_ebay_system_parts es on p.product_serial_no=es.luc_sku "&_
                                            " inner join tb_ebay_system_part_comment epc on epc.id=es.comment_id "&_
                                            " where es.system_sku='"& rs("id") &"'")
                                            
                    Response.write "<tr>"  &vblf
                    Response.write "    <td title='system_case_img' rowspan=""3"" style='width:22%;text-align:center;'>"
                    if not crs.eof then
                            do while not crs.eof 
                                    if  (crs("is_case")=1) then
										Response.write "<a href='/ebay/system_view.asp?id="& rs("ebay_system_current_number") &"'>"&vblf
                                        Response.write "	<img src='"& GetImgCaseMiddle( HTTP_PART_GALLERY, crs("img_sku"))&"' border='0' style='cursor:pointer;' />"	&vblf
										Response.write "</a>"&vblf
                                    end if
                            crs.movenext
                            loop                    
                            crs.movefirst
                    end if
                    
                                        
                    Response.write "["    & rs("ebay_system_current_number") &"]</td>" &vblf
                    Response.write "    <td title='system_title' class='system_title1'>"&vblf
					Response.write "            <a href='/ebay/system_view.asp?id="& rs("ebay_system_current_number") &"'  style='color:#006699;'> "   &vblf
                    Response.write      ViewSystemLogo(rs("logo_filenames"))
                    Response.write "        "& rs("ebay_system_name") 
					Response.write "            </a>"   &vblf
                    Response.write "    </td>" &vblf
                    Response.write "</tr>"  &vblf
                    Response.write "<tr>"  &vblf
                    Response.write "    <td title='system_parts'>"&vblf
                    
                    
                    
                    if not crs.eof then
                            Response.write "    <ul>"&vblf
                            do while not crs.eof                                     
                                    Response.write "        <li class='system_part'>"& crs("product_ebay_name") &"</li>"&vblf
                            crs.movenext
                            loop
                            Response.write "    </lu>"&vblf
                    end if
                    crs.close : set crs = nothing
					
                    Response.write "		<span style='float:left;'>"&vblf
					Response.write "			<!--input type='checkbox' />Compare <a href=''>[Reset]</a-->"
					Response.write "		</span>"&vblf
                    Response.write "    </td>" &vblf
                    Response.write "</tr>"  &vblf
                    Response.write "<tr>"  &vblf
                    Response.write "    <td title='system_price_line'>" &vblf
					Response.write "		<span>"&vblf					
                    Response.write "        " & ConvertDecimalUnit(Current_System, rs("ebay_system_price")) 
                    Response.write "            <a href='/ebay/system_view.asp?id="& rs("ebay_system_current_number") &"' > "   &vblf
                    Response.write "                <img src=""/soft_img/app/detail_bt.gif"" width=""56"" height=""13"" border=""0"" align=""absmiddle"">"  &vblf
                    Response.write "            </a>"   &vblf
                    Response.write "            <a href='/ebay/ebay_system_customize.asp?id="& rs("ebay_system_current_number") &"' > "   &vblf
                    Response.write "                <img src=""/soft_img/app/custmize_bt.gif"" width=""66"" height=""13"" border=""0"" align=""absmiddle"">"  &vblf
                    Response.write "            </a>"   &vblf
					Response.write "			<a href='/ebay/BuyS.asp?number="& rs("ebay_system_current_number") &"'><img src=""/soft_img/app/buyNow_bt.gif""  height=""13"" border=""0"" align=""absmiddle""></a>"	&vblf
					Response.write "		</span>"&vblf
                    Response.write "    </td>" &vblf
                    Response.write "</tr>" &vblf
                    
                    Response.write "<tr>"   &vblf
                    Response.write "    <td colspan='2' style='border:1px solid #D0DAE1; font-size:3px;'>&nbsp;</td>"   &vblf
                    Response.write "</tr>"  &vblf
            rs.movenext
            loop
            Response.write "</table>" &vblf
			
			'
			' page floor
			Response.write "<div title='list_floor_area'>"&vblf
			Call WritePageButton(recordC, page, pageC)
			Response.write "</div>"&vblf
	else
		Response.write "<div style='line-height: 50px;'>No Match Data.</div>"
    end if
    rs.close : set rs = nothing

End Function



'--------------------------------------------------------------------------------------
Function GetEbaySystemURL(page)
'--------------------------------------------------------------------------------------
	Dim url
	url = Request.ServerVariables("HTTP_url")
	if instr(url, "&page=")>0 then 
		url = replace(replace(url, "&page="& page, ""), "&page=", "")
	end if
	if instr(url, "?page=")>0 then 
		url = replace(replace(url, "page="& page&"&", ""), "page=&", "")
	end if	
	if instr(url, "inc_product_list")>0 then
		url = replace(url, "inc/inc_product_list", "list")
	end if
	
	GetEbaySystemURL	=	url & "&"
End Function



'--------------------------------------------------------------------------------------
Function GetEbayKeywordIdByPrice()
'--------------------------------------------------------------------------------------
	DIm rs
	
	Set rs = conn.execute("Select id from tb_product_category_keyword where is_price=1 and is_ebay=1")
	If not rs.eof then
		GetEbayKeywordIdByPrice = rs(0)
	else
		GetEbayKeywordIdByPrice	=	"Params is Error."
	end if
	
End Function



'--------------------------------------------------------------------------------------
Function ViewSystemLogo(str)
'--------------------------------------------------------------------------------------
    Dim i 
    Dim strs
    str = trim(str)
    if str = "" or isnull(str) or isempty(str) then
        ViewSystemLogo  =   ""
    else
        if instr(str, "|")>0 then
            strs = split(str , "|")
            for i = lbound(strs) to ubound(strs)
                    ViewSystemLogo = ViewSystemLogo & "<img title=""system_logo"" src="""& HTTP_PART_GALLERY_CPU_LOGO_PATH & trim(strs(i)) &""" style='width:31px;'/>"
            next
        else
            ViewSystemLogo = "<img title=""system_logo"" src="""& HTTP_PART_GALLERY_CPU_LOGO_PATH & str &""" style='width:31px;'/>"
        end if
    end if
    
End Function




'--------------------------------------------------------------------------------------
Function WritePageButton(recordC, page, pageC)
'--------------------------------------------------------------------------------------

		Dim url_path 
		url_path	=	GetEbaySystemURL(page)
		'Response.Write(url_path & "dd") 
        Response.write "<table border=""0"" cellspacing=""0"" cellpadding=""1"" title='page'>"		&vblf
        Response.write "	<tr>"&vblf
        Response.write "	                      <td width=""12"" valign=""middle"">Records:<span  style='margin: 5px'>"& recordC &"</span></td>"		&vblf
        Response.write "	                      <td width=""12"" valign=""middle""> <a href="""& url_path &"page=1""> <img src=""/soft_img/app/arrow_left_2.gif"" width=""8"" height=""8"" border=""0""></a></td>"		&vblf
        Response.write "	                      <td width=""12"" valign=""middle"" style=""display:none; margin: 4px""> <a href="""& url_path&"page="
			if page=1 then
				response.write 1
			else
				response.write page-1					
			end if
	Response.write " ><img src=""/soft_img/app/arrow_left_1.gif"" width=""8"" height=""8"" border=""0""></a></td>"		&vblf
	 
	Response.write "	<td class=""text_blue2_12"" valign=""middle"">"		&vblf

			for x=1 to pageC 
				if x = page then
					Response.write "		<span style='margin: 5px'><a href='"&url_path&"page="& x &"' style='color: blue;font-weight: bold;'  target=""ifr_product_sub_list""  >" & x & "</a></span>"		&vblf
				else
					Response.write "		<span style='margin: 5px'><a href='"&url_path&"page="& x &"' style=''>" & x & "</a></span>"		&vblf
				end if
			next

        Response.write "      </td>"		&vblf
        Response.write "      <td width=""12"" valign=""middle"" style=""display:none;""> <span  style='margin: 5px'><a href="""& url_path& "page="
		if page=rspagecount then
			response.write rspagecount
		else
			response.write page+1
		end if
		Response.write "  ><img src=""/soft_img/app/arrow_right_1.gif"" width=""8"" height=""8"" border=""0""></a> </span></td>"		&vblf
        Response.write "	                       <td width=""12"" valign=""middle""><a href="""& url_path &"page="& pageC &""" ><img src=""/soft_img/app/arrow_right_2.gif"" width=""8"" height=""8"" border=""0""></a></td>"		&vblf
        Response.write "	                        <td  valign=""middle"">Pages:&nbsp;"& recordC&" &nbsp;</td>"		&vblf
        Response.write "	                      </tr>"		&vblf
        Response.write "	                    </table>"		&vblf
End Function



'--------------------------------------------------------------------------------------
Function WriteSortBy(sortby, category_id)
'--------------------------------------------------------------------------------------
	Dim price_high
	Dim price_low
	if cstr(sortby) = "1" then
		price_high	= " class='selected' "
		price_low	= " class='unselected' "
	else
		price_high 	= " class='unselected' "
		price_low	= " class='selected' "
	end if
	
	Response.write "<div style='background:#f2f2f2;text-align:left;height:20px;' class='sort_by_area'>"&vblf
	Response.write "	<ul>"&vblf
	Response.write "		<li style='width:102px;text-align:right;'>"&vblf	
	Response.Write "			<input type='hidden' name='sort_by' value='"& request("sortby")&"'/>"&vblf
	Response.write "			<B >SORT BY:</B>"&vblf
	Response.write "		</li>"&vblf
	Response.write "		<li style='width: 460px;'>"&vblf
	Response.write "			<a  onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", null, 1, null, null, """& category_id &""");' "& price_high &">Price(high)</a>	"&vblf
	Response.write "	|	"&vblf
	Response.Write "	<a  onclick='ViewPartListByCategoryKeyword(""category_query_keyword_id"", ""category_query_keyword"", null, 2, null, null, """& category_id &""");' "& price_low &">Price(low)</a>	"&vblf
	Response.write "		</li>"&vblf
	Response.write "	</ul>"&vblf
	Response.write "</div>"&vblf
	
	Response.write "<div id='main_list_head_area'></div>"&vblf
	
	
	
End Function 
%>