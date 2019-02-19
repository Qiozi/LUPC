<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
Response.Clear()
'
'	http://localhost/get_price.asp?category=3&product_id=2861&is_sold=1&is_char=1&is_card_rate=1
'	
'
'


' 
' product cateory: 1 part, 2 noebook, 3 system
'
dim category
category = 1 
category = SQLescape(request("category"))

if len(category)>0 then
	if clng(category) > 3 then 
		category = 1
	end if
end if

'
' is sold: true Sold, false Price;
'
dim is_sold
is_sold = true
is_sold = SQLescape(request("is_sold"))
if  is_sold = "1" then 
	is_sold = true
else
	is_sold = false
end if

'
' Is Char: true $, false null.
'
dim is_char
is_char = true
is_char = SQLescape(request("is_char"))
if  is_char = "1" then 
	is_char = true
else
	is_char = false
end if

'
' is Rate: 1 = 1.022, 0 = 1
'
dim is_card_rate
is_card_rate = SQLescape(request("is_card_rate"))
if  is_card_rate <> "1" then 
	card_rate = 1
end if

dim is_js
is_js = SQLescape(request("is_js"))
if  is_js = "0" then 
	is_js = 0
else
	is_js = 1
end if

dim product_id, price 
price = 0
product_id = SQLescape(request("product_id"))

if isnumeric(product_id ) then 
	if(category = "1")then 
		
		set rs = conn.execute("Select product_current_price from tb_product where tag=1 and product_serial_no="& SQLquote(product_id))
		if not rs.eof then 
			if not isnull(rs(0)) and rs(0) <> "" then
				price = cdbl(rs(0)) 
			end if
		end if
		rs.close : set rs = nothing
		
		
		if is_sold then 
			set drs = conn.execute(FindOnSaleSingle(product_id))
			if not drs.eof then 
				price = cdbl(price) - cdbl(drs("save_cost"))
			end if
			drs.close : set drs = nothing
		end if
	end if
	
	if(category = "3") then 
	 	  dim tmp_system_price, tmp_system_save_price, tmp_system_price_first

		  dim price_and_save
		  price_and_save = GetSystemPriceAndSave(product_id)
		  
		  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
		  tmp_system_price_first = splitConfigurePrice(price_and_save,0)
		  tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
		  price = tmp_system_price_first

		if is_sold then 
			dim save_price 
			save_price = tmp_system_save_price
			if save_price <> "" then 
				 price = cdbl(price ) - cdbl(save_price)
			end if
		end if		 
	end if
	
	if is_js = 1 then 
		if is_char then 
			response.write "document.writeln('"& replace(replace(formatcurrency( price, 2) & "<span class='price_unit'>"& CCUN &"</span>", "'", "\'"), """", "\""") &"');document.close();"
		else
			'response.write "document.write('"& price &"');"
			response.write "document.writeln('"& replace(replace(formatcurrency( price, 2)& "<span class='price_unit'>"& CCUN &"</span>", "'", "\'"), """", "\""") &"');document.close();"
		end if
	else
		if is_char then 
			response.write  ConvertDecimalUnit(current_system,formatcurrency( price, 2))
		else			
			response.write ConvertDecimalUnit(current_system,formatnumber( price , 2))
		end if		
	end if
end if
closeconn()
%>
