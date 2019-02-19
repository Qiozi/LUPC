<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
    <!--#include virtual="/inc/conn.asp"-->
    <!--#include virtual="/inc/inc_escape.asp"-->
<!-- #include virtual ="/Paypal/CallerService.asp" -->
<%

	Response.Buffer = True
	
	Dim amount
	Dim state
    Dim orderCode 
    Dim cid

	cid = SQLescape(request("cid"))		
    orderCode = SQLescape(request("orderCode"))
	Session("ordercode") = orderCode
    state = SQLescape(request("cardCtate"))

	amount = ""
	
	
	set rs = conn.execute("select taxable_total, grand_total, price_unit from tb_order_helper where order_code='"&orderCode&"'")
		'response.write ("select taxable_total, grand_total, price_unit from tb_order_helper where order_code='"&orderCode&"'")
	
	if not rs.eof then
		amount = rs(1)
	end if
	rs.close : set rs = nothing
	
	'set rs = conn.execute("select state_code from tb_state_shipping where state_serial_no='"& request.form("card_state") &"'")
	'if not rs.eof then 
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

    set rs  = conn.execute("Select * from tb_customer_store where customer_serial_no='"& cid &"' and order_code='"&orderCode&"'")
    if not rs.eof then
	firstName			= rs("customer_card_first_name") 'Request.Form("firstName")
	lastName			= rs("customer_card_last_name") 'Request.Form("lastName")
	
	creditCardType		= rs("customer_card_type")
	
	creditCardNumber	= replace(rs("customer_credit_card"), " ", "") 'Request.Form("creditCardNumber")
	padDate				= rs("customer_expiry") 
	cvv2Number			= rs("card_verification_number") 'Request.Form("cvv2Number")
    
    
    COUNTRYCODE         = rs("customer_card_country_code")
    if (rs("customer_card_country_code") = "Canada") then
        COUNTRYCODE = "CA"
    end if 
    if (rs("customer_card_country_code") = "United States") then
        COUNTRYCODE = "US"
    end if 

	if len(rs("customer_card_billing_shipping_address")) > 100 then
		address1			= left(rs("customer_card_billing_shipping_address"), 100) 'Request.Form("address1")
		address2			= right(rs("customer_card_billing_shipping_address"), len(rs("customer_card_billing_shipping_address")) - 100)
	else
		address1			= rs("customer_card_billing_shipping_address")
		address2			= ""'request.form("") 'Request.Form("address2")
	end if
	
	city				= rs("customer_card_city") 'Request.Form("city")
	state				= rs("customer_card_state_code") 'Request.Form("state")
	zip					= rs("customer_card_zip_code") 'Request.Form("zip")
	
	end if
    rs.close 


	Set rs = conn.execute("select price_unit from tb_order_helper where order_code='"& Session("ordercode") &"'")
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
   
    conn.execute("insert into tb_order_paypal_send_info(regdate, OrderCode, content) values (now(), '"&Session("ordercode")&"', '"& replace(nvpstr, "'", "\'") &"')")
    response.Write(nvpstr)
    'response.End()
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
                srs("order_code") = Session("ordercode")   
                srs.update()
                srs.close : set srs = nothing
            next  
    end if
	
	if ack <> UCase("Failure") then 
		message="Thank you for your payment!"
		SESSION("ErrorMessage")	= Null
		
		conn.execute("Update tb_order_helper set order_pay_status_id='2',Is_Modify=1 where order_code='"& Session("ordercode") &"'")
'        conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate) values ( '"&resArray("TRANSACTIONID")&"', '"&resArray("AVSCODE")&"', '"&resArray("CVV2MATCH")&"','"& LAYOUT_ORDER_CODE &"', now())")
		orderCode = Session("ordercode") 
        Session("ordercode") =""
    	response.Redirect("CheckOutCreditCardToPaypalEnd.aspx?orderCode="&orderCode&"&TRANSACTIONID="&resArray("TRANSACTIONID")&"&AVS="&resArray("AVSCODE")&"&CVV2="&resArray("CVV2MATCH"))
		response.End()
	Else
		 conn.execute("Update tb_order_helper set order_pay_status_id='4',Is_Modify=1 where order_code='"& Session("ordercode") &"'")
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
		<a class="home" id="CallsLink" href="https://lucomputers.com/"><font color="blue"><B>Home</B></font></a>
	</body>
</html>
