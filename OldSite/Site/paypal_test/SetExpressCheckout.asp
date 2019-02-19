<!-- #include file ="Constants.asp" -->
<%
'-------------------------------------------------------------------------------------------
' SetExpressCheckout.asp
'=======================
' This is the main web page for the Express Checkout sample.
' The page allows the user to enter amount and currency type.
' It also accept input variable paymentType which becomes the
' value of the PAYMENTACTION parameter.

' When the user clicks the Submit button, ReviewOrder.asp is called.

' Called by Default.htm.

' Calls ReviewOrder.asp.
'-------------------------------------------------------------------------------------------
On Error Resume Next
%>
<html>
	<head>
		<title>PayPal NVP Web Samples Using ASP - SetExpressCheckout</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<center>
			<form action="ReviewOrder.asp" method="POST">
				<input type="hidden" name="paymentType" value='<%=Request.QueryString("paymentType")%>'>
				<span id="apiheader">SetExpressCheckout</span>
				<table class="api">
					<tr>
						<td colspan="2">
							<center>
								You must be logged into <a href=<%=ECURLLOGIN%> target="_blank">Developer 
									Central</a>
							</center>
						</td>
					</tr>
					<tr>
						<td class="field">
							Amount:</td>
						<td>
							<input type="text" name="paymentAmount" size="5" maxlength="7" value="1.00" />
							<select name="currencyCodeType">
								<option value="USD">USD</option>
								<option value="GBP">GBP</option>
								<option value="EUR">EUR</option>
								<option value="JPY">JPY</option>
								<option value="CAD">CAD</option>
								<option value="AUD">AUD</option>
							</select>
							(Required)</td>
					</tr>
					<tr>
						<td>
							<input type="image" name="submit" src="https://www.paypal.com/en_US/i/btn/btn_xpressCheckout.gif" />
						</td>
						<td>
							Save time. Pay securely without sharing your financial information.
						</td>
					</tr>
				</table>
		</center>
		<%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"SetExpressCheckout.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
		<br>
		<a class="home" id="CallsLink" href="Default.htm">Home</a>
	</body>
</html>
