<!--#include virtual="site/no_express.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->

<%

	Dim id				:	id				=	SQLescape(request("cid"))
	Dim is_system		:	is_system		=	false
    Dim is_Notebook     :   is_Notebook     =   false
	Dim rCount			:	rCount			=	0
	Dim is_view_stock_filter:is_view_stock_filter=false
	
	set rs = conn.execute("Select page_category, is_view_stock_filter, is_noebook from tb_product_category where menu_child_serial_no='"& id &"'")
	if not rs.eof then
		if rs(0) = 0 then 
			is_system	=	true
		else
			is_system 	= 	false
		end if
		if rs(1) = 1 then
			is_view_stock_filter 	= 	true
		end if
        if rs("is_noebook") = 1 then 
            is_Notebook = true
        end if
	End if
	rs.close : set rs = nothing
	
	response.write "<table id='part_list_query_keyword' cellpadding=""0"" cellspecing=""0"" width='595'>"
    response.write "<tr><td colspan='2' style='padding: 5px;'><h2><b style='color: #4598d2;font-size: 11pt;'>Filter</b></h2></td></tr>"
	set rs = conn.execute("select * from tb_product_category_keyword where keyword<>'' and category_id="& SQLquote(id) & " order by priority asc" )
        if not rs.eof then
                
                do while not rs.eof                        
                        response.write "<tr>"
                        response.write         "<td style='text-align:right;width: 100px;vertical-align: top'>"
                        response.write             "<b>"& rs("keyword") &":</b>"
                        response.write         "</td>"
                        response.write         "<td >"
						
									set subrs = conn.execute("select count(parent_id) c from tb_product_category_keyword_sub where parent_id='"& rs("id") &"' order by priority, id asc")
									if not subrs.eof then
										rCount = subrs(0)
									end if
									subrs.close 
                                    set subrs = conn.execute("select * from tb_product_category_keyword_sub where parent_id='"& rs("id") &"' order by priority asc, id asc ")
                                    if not subrs.eof then 
                                                    response.write "<input type='hidden' name='category_query_keyword' id='category_query_keyword_"&rs("id")&"'  value='ALL'>"
                                                    response.write "<a class='selected' onclick='press_category_keyword( ""ALL"",""category_query_keyword_"&rs("id")&""");'> ALL </a>"
											i = 0
                                            do while not subrs.eof 
                                                    
                                                    response.write "<a name='part_key' "
                                                    if (is_system) then response.write " style='clear:both;' "
                                                    Response.write " class='unselected' onclick='press_category_keyword( """& JSescape(subrs("keyword")) & """,""category_query_keyword_"&rs("id")&""");'>"									
													i = i + 1			
													if i<>rCount then 
                                                        if not is_system then 
														response.write        subrs("keyword")&","
                                                        else
                                                        response.write        subrs("keyword") '&","
                                                        end if
													else
														response.write        subrs("keyword")
													end if
                                                    
                                                    response.write "</a>"
													
                                            subrs.movenext
                                            loop                                            
                                    end if
                        subrs.close : set subrs = nothing
                        response.write         "</td>"
                        response.write "</tr>"
						
                rs.movenext
                loop

				

        end if
        rs.close : set rs = nothing
		Response.write "	<tr>"&vblf
		response.write         "<td style='text-align:right;width: 100px;'>"&vblf
		response.write             "<b>Price:</b>"&vblf
		response.write "		</td>"&vblf
		response.write "		<td >"&vblf
		response.write "			<input type='hidden' name='category_query_keyword_price' id='category_query_keyword_price' value='ALL'>"
		response.write "		<a class='selected' onclick='press_category_keyword( ""ALL"", ""category_query_keyword_price"");'>"&vblf
		response.write "ALL"
		response.write "		</a>"&vblf

        if is_system then 

        response.write "		<a class='unselected' onclick='press_category_keyword( ""100~499.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "100~499.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""500~999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "500~999.99"
		response.write "		</a>"&vblf
        response.write "		<a class='unselected' onclick='press_category_keyword( ""1000~1499.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "1000~1499.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""1500~1999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "1500~1999.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""2000~9999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "2000~9999.99"
		response.write "		</a>"&vblf

        elseif is_Notebook then 

        response.write "		<a class='unselected' onclick='press_category_keyword( ""100~399.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "100~399.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""400~999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "400~999.99"
		response.write "		</a>"&vblf
        response.write "		<a class='unselected' onclick='press_category_keyword( ""1000~1499.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "1000~1499.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""1500~1999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "1500~1999.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""2000~9999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "2000~9999.99"
		response.write "		</a>"&vblf

		else
		response.write "		<a class='unselected' onclick='press_category_keyword( ""5~99.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "5~99.99"
		response.write "		</a>"&vblf
        response.write "		<a class='unselected' onclick='press_category_keyword( ""100~199.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "100~199.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""200~399.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "200~399.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""400~999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "400~999.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""1000~1999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "1000~1999.99"
		response.write "		</a>"&vblf
		response.write "		<a class='unselected' onclick='press_category_keyword( ""2000~9999.99"", ""category_query_keyword_price"");'>"&vblf
		response.write "2000~9999.99"
		response.write "		</a>"&vblf

		End if
		
		response.write "		</td>"&vblf
		Response.write "	</tr>"&vblf
		
		'
		'	stock
		'
		if is_view_stock_filter then 
			Response.write "	<tr>"&vblf
			response.write         "<td style='text-align:right;width: 100px;'>"&vblf
			response.write             "<b>Stock:</b>"&vblf
			response.write "		</td>"&vblf
			response.write "		<td >"&vblf
			response.write "			<input type='hidden' name='category_query_keyword_stock' id='category_query_keyword_stock' value='ALL'>"
			response.write "		<a class='selected' onclick='press_category_keyword( ""ALL"", ""category_query_keyword_stock"");'>"&vblf
			response.write "ALL"
			response.write "		</a>"&vblf
		
			response.write "		<a class='unselected' onclick='press_category_keyword( ""In Stock"", ""category_query_keyword_stock"");'>"&vblf
			response.write "In Stock"
			response.write "		</a>"&vblf
			response.write "		<a class='unselected' onclick='press_category_keyword( ""Out of Stock"", ""category_query_keyword_stock"");'>"&vblf
			response.write "Out of Stock"
			response.write "		</a>"&vblf
			response.write "		</td>"&vblf
			Response.write "	</tr>"&vblf
		
		end if
		response.write "</table>"		
		
		Call WriteSortBy("1", id)
CloseConn()	

'--------------------------------------------------------------------------------------
Function WriteSortBy(sortby, category_id)
'--------------------------------------------------------------------------------------
	Dim price_high
	Dim price_low

	price_low	= " class='unselected' "
	price_high 	= " class='unselected' "

	
	Response.write "<div style='background:#f2f2f2;text-align:left;height:20px;' class='sort_by_area'>"&vblf
	Response.write "	<ul>"&vblf
	Response.write "		<li style='width:102px;text-align:right;'>"&vblf	
	Response.Write "			<input type='hidden' name='sort_by' id='sort_by' value='-1'/>"&vblf
	Response.write "			<B >SORT BY:</B>"&vblf
	Response.write "		</li>"&vblf
	Response.write "		<li style='width: 460px;'>"&vblf
	Response.write "			<a id='price_high' onclick='$(""#price_low"").attr(""class"", ""unselected"");$(""#price_high"").attr(""class"", ""selected"");press_category_keyword(""1"", ""sort_by"");' "& price_high &">Highest Price First</a>	"&vblf
	Response.write "	|	"&vblf
	Response.Write "			<a id='price_low' onclick='$(""#price_low"").attr(""class"", ""selected"");$(""#price_high"").attr(""class"", ""unselected"");press_category_keyword(""2"", ""sort_by"");' "& price_low &">Lowest Price First</a>	"&vblf
	Response.write "		</li>"&vblf
	Response.write "	</ul>"&vblf
	Response.write "</div>"&vblf
	
	Response.write "<div id='main_list_head_area'></div>"&vblf	
End Function 
		
function categoryQueryKeywordGetIsExist(category_query_keywords, key)
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

function categoryQueryKeywordGetIsExistGetV(category_query_keywords, id)
	categoryQueryKeywordGetIsExistGetV = ""
	dim category_query_keywords_group, i, j, cqkg
	if( not isempty(category_query_keywords) and category_query_keywords <>"" and not isnull(category_query_keywords) )then
		if instr(category_query_keywords, ",")>0 then
			category_query_keywords_group = split(category_query_keywords, ",")
			for i = lbound(category_query_keywords_group) to ubound(category_query_keywords_group)
					if instr(category_query_keywords_group(i), "|") >0 then 
							cqkg = split(category_query_keywords_group(i), "|")
							if cstr(SQLescape(id)) = cstr(SQLescape(cqkg(1))) then
							   ' response.write category_query_keywords
								categoryQueryKeywordGetIsExistGetV = cqkg(0)
							end if
					end if
			next
		end if
	end if
	if categoryQueryKeywordGetIsExistGetV = "" then categoryQueryKeywordGetIsExistGetV = "-1"
end function
%>
<script type="text/javascript">
$().ready(function(){
		$('div.sort_by_area >ul').css("width", "99%");
		$('div.sort_by_area').find('li').css("float", "left").css("border", "0px solid red").css("line-height", "20px");
});
</script>