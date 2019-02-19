<!-- #include file ="CallerService.asp" -->
<%

	Dim transactionID
	Dim refundType
	Dim currencyCode
	Dim note
	Dim amount

	transactionID		= Request.QueryString ("transactionID")
	refundType			= Request.QueryString ("refundType")
	currencyCode		= Request.QueryString ("currency")
	note				= Request.QueryString ("memo")
	amount				= Request.QueryString ("amount")
'-----------------------------------------------------------------------------
' Construct the request string that will be sent to PayPal.
' The variable $nvpstr contains all the variables and is a
' name value pair string with &as a delimiter
'-----------------------------------------------------------------------------

	If refundType="Partial" Then	
	nvpstr	=	"&REFUNDTYPE="&refundType &_
				"&NOTE="&note &_
				"&CURRENCYCODE="&currencyCode &_
				"&AMT=" &amount &_
				"&TRANSACTIONID=" &transactionID 
	Else
		nvpstr	=	"&TRANSACTIONID=" &transactionID & _
				"&REFUNDTYPE="&refundType &_
				"&NOTE="&note &_
				"&CURRENCYCODE="&currencyCode 
	End If
				
	nvpstr	=	URLEncode(nvpstr)
	
	
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("RefundTransaction",nvpstr)
	ack = UCase(resArray("ACK"))
	amt = UCase(resArray("GROSSREFUNDAMT"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	If ack="SUCCESS" Then
		Message ="Transaction refunded!!"
	Else
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
	End If

%>
<html>
	<head>
		<title>PayPal ASP - Refund Receipt</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<center>
			<font size="2" color="black" face="Verdana"><b>Refund Transaction</b></font>
			<br>
			<br>
			<b>
				<%=Message%>
			</b>
			<br>
			<br>
			<table width="400">
				<tr>
					<td>Transaction ID:</td>
					<td><%=transactionID%></td>
				</tr>
				<tr>
					<td>Gross Refund Amount:</td>
					<td>USD
						<%=amt%>
					</td>
				</tr>
			</table>
		</center>
		<%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"RefundReceipt.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
		<a class="home" id="CallsLink" href="Default.htm">Home</a>
	</body>
</html>
