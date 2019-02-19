<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>

<!--#include virtual="site/inc/inc_helper.asp"-->
<!-- #include virtual ="/paypal/CallerService.asp" -->
<%

	Response.Buffer = True
	
	
'-----------------------------------------------------------------------------
'	save to lucomputers.com
'
'
'
'
''
'
	'dim customerID
	call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 
	Call ValidateOrder_Code("")
	
	set rs = server.createobject("adodb.recordset")
	rs.open "select * from tb_customer where customer_serial_no='"& LAYOUT_CCID &"'", conn, 2,3
'	response.write "select * from tb_customer where customer_serial_no="& customerID
'	'response.end
	
	rs("customer_card_type") 		= request.form("card_type")
	rs("customer_credit_card") 		= request.form("card_number")
	rs("customer_expiry") 			= request.form("card_expiry_month")&request.form("card_expiry_year")
	rs("customer_card_issuer") 		= request.form("card_issuer")
	rs("customer_card_phone")		= request.form("card_issuer_telephone")
	rs("customer_first_name") 		= request.form("cart_first_name")
	rs("customer_last_name") 		= request.form("cart_last_name")
	rs("customer_card_first_name")	= request.form("cart_first_name")
	rs("customer_card_last_name")	= request.form("cart_last_name")
	rs("customer_card_billing_shipping_address") = request.form("card_billing_shipping_address")
	rs("customer_card_city") 		= request.form("card_city")
	rs("customer_card_state_code") 		= request.form("card_state")
	rs("customer_card_zip_code") 	= request.form("card_zip_code")
	rs("customer_shipping_address") = request.form("shipping_address")
	rs("customer_shipping_city") 	= request.form("shipping_city")
	rs("customer_email2") 			= request.form("secondary_email")
	rs("phone_d") 					= request.form("phone_d")
	rs("phone_n") 					= request.form("phone_n")
	rs("customer_card_country_code") 	= request.form("card_country")
	rs("shipping_country_code") = LAYOUT_SHIPPING_COUNTRY_CODE
	rs("customer_shipping_first_name") = request.form("shipping_first_name")
	rs("customer_shipping_last_name") = request.form("shipping_last_name")
	rs("customer_shipping_zip_code") =  request.form("shipping_zip_code")
	rs("shipping_state_code") 	= LAYOUT_SHIPPING_STATE_CODE
	rs("customer_shipping_city") 	= request.form("shipping_city")
	rs("card_verification_number") 	= request.form("card_verification_number")
	rs("pay_method") 				=	LAYOUT_PAYPAL_METHOD_CREDIT_CARD

    COUNTRYCODE = rs("customer_card_country_code")


	rs.update
	rs.close : set rs = nothing
	'
	' save Msg
	order_code = LAYOUT_ORDER_CODE
	content = request.form("note")
	call CustomerSendMsg(order_code, content)	

if not Session("IsExistOrder") = true then 
	Call CopyCartToOrder(order_code, LAYOUT_CCID, "")
end if
	Dim amount
	Dim state
	
	amount = ""
	state = ""
	
	set rs = conn.execute("select taxable_total, grand_total_rate from tb_cart_temp_price where order_code='"&LAYOUT_ORDER_CODE&"'")
	if Session("IsExistOrder") = true then 
		rs.close : set rs = nothing
		set rs = conn.execute("select taxable_total, grand_total, price_unit from tb_order_helper where order_code='"&LAYOUT_ORDER_CODE&"'")
		response.write ("select taxable_total, grand_total, price_unit from tb_order_helper where order_code='"&LAYOUT_ORDER_CODE&"'")
	end if
	if not rs.eof then
		amount = rs(1)
	end if
	rs.close : set rs = nothing
	
	'set rs = conn.execute("select state_code from tb_state_shipping where state_serial_no='"& request.form("card_state") &"'")
	'if not rs.eof then 
		state = request.form("card_state")
	'end if
	'rs.close : set rs = nothing
	
	
	
	
	if amount = "" or state = "" then 
		if amount = "" then 
		
			response.Write("<script > alert('amount is error.'); window.history.go(-1);</script>")
			
		end if
		if state = "" then 
			response.Write("<script > alert('please select State.'); window.history.go(-1);</script>")
		end if
		response.End()	
	end if	
'	response.write state
'	response.end
	
'-----------------------------------------------------------------------------
' DoDirectPaymentReceipt.asp

' Submits a credit card transaction to PayPal using a
' DoDirectPayment request.

' The code collects transaction parameters from the form
' displayed by DoDirectPayment.asp then constructs and sends
' the DoDirectPayment request string to the PayPal server.
' The paymentType variable becomes the PAYMENTACTION parameter
' of the request string.

' After the PayPal server returns the response, the code
' displays the API request and response in the browser.
' If the response from PayPal was a success, it displays the
' response parameters. If the response was an error, it
' displays the errors.

' Called by DoDirectPayment.asp.

' Calls CallerService.asp and APIError.asp.

'-----------------------------------------------------------------------------
	Dim firstName
	Dim lastName
	Dim creditCardType
	Dim creditCardNumber
	Dim expDateMonth
	Dim expDateYear
	Dim padDate
	Dim cvv2Number
	Dim address1
	Dim address2
	Dim city

	Dim zip
	
	Dim currencyCode
	Dim paymentType
	Dim nvpstr
	Dim resArray
	Dim ack
	Dim message

	firstName			= request.form("cart_first_name") 'Request.Form("firstName")
	lastName			= request.form("cart_last_name") 'Request.Form("lastName")
	
	'if request.form("card_type") = "MC" then 
	'	creditCardType		= "MasterCard" 'Request.Form("creditCardType")
	'end if
	'if request.form("card_type") = "VS" then 
	'	creditCardType		= "Visa" 'Request.Form("creditCardType")
	'end if
	creditCardType		= Request.Form("card_type")
	
	creditCardNumber	= request.form("card_number") 'Request.Form("creditCardNumber")
	expDateMonth		= request.form("card_expiry_month") 'Request.Form("expDateMonth")
	expDateYear			= request.form("card_expiry_year") 'Request.Form("expDateYear")
	padDate				= expDateMonth&expDateYear
	cvv2Number			= request.form("card_verification_number") 'Request.Form("cvv2Number")
	if len(request.form("card_billing_shipping_address")) > 100 then
		address1			= left(request.form("card_billing_shipping_address"), 100) 'Request.Form("address1")
		address2			= right(request.form("card_billing_shipping_address"), len(request.form("card_billing_shipping_address")) - 100)
	else
		address1			= request.form("card_billing_shipping_address")
		address2			= ""'request.form("") 'Request.Form("address2")
	end if
	
	city				= request.form("card_city") 'Request.Form("city")
	state				= state 'request.form("card_state") 'Request.Form("state")
	zip					= request.form("card_zip_code") 'Request.Form("zip")
	amount				= amount 
	
	
	Set rs = conn.execute("select price_unit from tb_order_helper where order_code="& SQLquote(LAYOUT_ORDER_CODE))
	if not rs.eof then
		currencyCode	=	rs(0)
	End if
	rs.close : set rs = nothing
	
	
	
	paymentType			= "Sale" 

	  
'-----------------------------------------------------------------------------
' Construct the request string that will be sent to PayPal.
' The variable $nvpstr contains all the variables and is a
' name value pair string with &as a delimiter
'-----------------------------------------------------------------------------
	nvpstr	=	"&PAYMENTACTION=" &paymentType & _
				"&AMT="&amount &_
				"&CREDITCARDTYPE="&creditCardType &_
				"&ACCT="&creditCardNumber & _
				"&EXPDATE=" & padDate &_
				"&CVV2=" & cvv2Number &_
				"&FIRSTNAME=" & firstName &_
				"&LASTNAME=" & lastName &_
				"&STREET=" & address1 &_
				"&CITY=" & city &_
				"&STATE=" & state &_
				"&ZIP=" &zip &_
				"&COUNTRYCODE="& COUNTRYCODE &_
				"&CURRENCYCODE=" & currencyCode
	
	nvpstr	=	URLEncode(nvpstr)
    conn.execute("insert into tb_order_paypal_send_info(regdate, OrderCode, content) values (now(), '"&LAYOUT_ORDER_CODE&"', '"& replace(nvpstr, "'", "\'") &"')")
   
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("doDirectPayment",nvpstr)
	ack = UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	'If ack="SUCCESS" Then
	
    dim reskey, resitem
    reskey = resArray.Keys
    resitem = resArray.items
    dim resindex
    
     if ack <> UCase("SUCCESS") and not is_return_error_page then       
            For resindex = 0 To resArray.Count - 1 
                set srs = server.createobject("adodb.recordset")
                srs.open "select * from tb_order_paypal_error_info", conn, 1,3
                srs.addnew
                srs("errkey") =reskey(resindex)
                srs("erritem") = resitem(resindex) 
               ' response.write reskey(resindex) & ":" &  resitem(resindex)  &"<br/>"
                srs("order_code") = LAYOUT_ORDER_CODE   
                srs.update()
                srs.close : set srs = nothing
            next  
    end if
	
	if ack <> UCase("Failure") then 
		message="Thank you for your payment!"
		SESSION("ErrorMessage")	= Null
		
		conn.execute("Update tb_order_helper set order_pay_status_id='"& LAYOUT_PAYPAL_SUCCESS &"',Is_Modify=1 where order_code='"& LAYOUT_ORDER_CODE &"'")
'        conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate) values ( '"&resArray("TRANSACTIONID")&"', '"&resArray("AVSCODE")&"', '"&resArray("CVV2MATCH")&"','"& LAYOUT_ORDER_CODE &"', now())")
		
    	response.Redirect("shopping_cheout_order_ok.asp?TRANSACTIONID="&resArray("TRANSACTIONID")&"&AVS="&resArray("AVSCODE")&"&CVV2="&resArray("CVV2MATCH"))
		response.End()
	Else
		conn.execute("Update tb_order_helper set order_pay_status_id='"& LAYOUT_PAYPAL_FAILURE &"',Is_Modify=1 where order_code='"& LAYOUT_ORDER_CODE &"'")
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
		 response.End()
	End If
closeconn()
	
%>
<html>
	<head>
		<title>PayPal ASP SDK - DoDirectPayment API</title>
	</head>
	<body alink="#0000FF" vlink="#0000FF">
		<center>
			<font size="2" color="black" face="Verdana"><b>Do Direct Payment</b></font>
			<br>
			<br>
			<b>
				<%=message%>
			</b>
			<br>
			<br>
			<table width="400"">
				<tr>
					<td>
						Transaction ID:</td>
					<td><%=resArray("TRANSACTIONID")%></td>
				</tr>
				<tr>
					<td>
						Amount:</td>
					<td>USD <%=resArray("AMT")%></td>
				</tr>
				<tr>
					<td>
						AVS:</td>
					<td><%=resArray("AVSCODE")%></td>
				</tr>
				<tr>
					<td>
						CVV2:</td>
					<td><%=resArray("CVV2MATCH")%></td>
				</tr>
			</table>
		</center>
		<%
    If Err.Number <> 0 Then
	    SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"shopping_cheout2_doDirectPayment.asp")
	    Response.Redirect "APIError.asp"
	Else
	    SESSION("ErrorMessage")	= Null
	End If
    %>
		<br>
		<a class="home" id="CallsLink" href="http://www.lucomputers.com/"><font color="blue"><B>Home<B><font></a>
	</body>
</html>
