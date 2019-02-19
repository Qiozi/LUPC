<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/no_express.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	Dim country_code	:	country_code	=	SQLescape(request("country_code"))
	Dim pay_type
	Dim order_code		:	order_code		= 	SQLescape(request("order_code"))
	Dim sub_total		:	sub_total		=	SQLescape(request("sub_total"))
	
	dim sur_charge
	if len(sub_total)>0 and sub_total <> "undefine" then 
		sur_charge = cdbl(sub_total) - cdbl(sub_total) / LAYOUT_CARD_RATE
	end if

	'response.write sub_total &"D"
	if (len(country_code) = 2) or country_code="Other"	then
		set rs = conn.execute("select * from tb_pay_method_new where tag=1 and supper_country like '%"& country_code &"%' order by taxis asc")
		
		If not rs.eof then
	
			pay_type	=	""
			do while not rs.eof
				if pay_type <> rs("is_card") then 
					'if(rs("is_card") = 0) then
'						Response.write "<div style='padding-left:5px;color:#EF5412;'>Cash Discounted Payment Methods&nbsp;"&_
'										" <span id='sub_charge_span' style='color:blue;'></span></div>"
'					else
'						Response.write "<div style='padding-left:5px;color:#EF5412;'>Regular Pricing Payment Methods</div>"
'					end if
					
					pay_type = rs("is_card")
					'response.write rs("is_card")
				else
					'response.write "="
				End if			
				
				response.write "<input type='radio' name='pay_method' value='"& rs("pay_method_serial_no") &"'/>"
				'if lcase(rs("pay_method_name")) = "paypal" then 
				'	response.write "<img src=""https://www.paypal.com/en_US/i/logo/PayPal_mark_37x23.gif""  style=""margin-right:7px;""/>"
				'else
					'Response.write rs("pay_method_name") 
				'end if
				if(rs("is_card") = 0) then
					if instr(lcase(rs("pay_method_name")), "canada")>0 then
						response.write replace(rs("pay_method_name") , ")",",") & " qualified for cash discount: <span class='price1'>"& formatcurrency(sur_charge , 2)&"</span>)"
					else
						response.write rs("pay_method_name")  & " (Qualified for cash discount: <span class='price1'>"& formatcurrency(sur_charge, 2) &"</span>)"
					end if
				else
					if rs("pay_method_name") = "Paypal" then 
						Response.Write "<img  src=""https://www.paypal.com/en_US/i/logo/PayPal_mark_37x23.gif"" border=""0"" alt=""Paypal""> <a href=""#"" onclick=""javascript:window.open('https://www.paypal.com/cgi-bin/webscr?cmd=xpt/popup/OLCWhatIsPayPal-outside', 'paypal', 'width=500;height=400;'); return false;"">What is PayPal?</a> "
					elseif instr(rs("pay_method_name"), "Credit Card")>0 then
						Response.Write "<img  src=""/soft_img/app/visa_master_card.jpg"" border=""0"" alt=""Paypal Credit Card"">"
					else 
						Response.write rs("pay_method_name") 
					end if
					
				end if
				response.write "<br/>"
			rs.movenext
			loop
		End if
		rs.close : set rs = nothing
	End if
	
'	dim tt
'	
'	if( tt = 0 )then
'		response.write "Y"
'	else
'		response.write "N"
'	end if
	
closeconn()
%>
