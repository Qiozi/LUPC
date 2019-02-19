<% 
'----------------------------------------------------------------------
'PayPal Express Checkout Example Step 2
'================================================
'Display the resulting authorization details from
'PayPal on a PayPal payment, and complete the
'payment authorization.  This script would be called
'when the buyer returns from PayPal and has authorized
'the payment
'----------------------------------------------------------------------
	On Error Resume Next
	Dim resArray
	Dim reqArray
	Dim message
	
	
	Set resArray	= SESSION("nvpResArray")
'----------------------------------------------------------------------
'Collect the necessary information to complete the
'authorization for the PayPal payment
'----------------------------------------------------------------------
	token			= SESSION("token")
	currCodeType	= SESSION("currencyCodeType")
	paymentAmount	= SESSION("paymentAmount")
	paymentType		= SESSION("PaymentType")
	payer_id		= SESSION("PayerID")

'----------------------------------------------------------------------
 'Set the final URL to complete the authorization.  The
 'link will be displayed at the bottom the the browser for this
 'example.  The link would normally be displayed at the end of your checkout
 'and would finalize payment when clicked.
 '----------------------------------------------------------------------
	final_url		="DoExpressCheckoutPayment.asp?token=" & token &_
					"&payerID="& payer_id &_ 
					"&paymentAmount="& paymentAmount &_ 
					"&currCodeType="& currCodeType &_ 
					"&paymentType=" &paymentType

	message			= "Get Express checkout Details!"
'----------------------------------------------------------------------
'Display the API request and API response back to the browser using Diaplay.asp.
'If the response from PayPal was a success, display the response parameters
'If the response was an error, display the errors received
'----------------------------------------------------------------------

%>
<html>
	<head>
		<title>PayPal ASP - ExpressCheckout API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
		<!-- #include file ="CallerService.asp" -->
	</head>
	<body>
		<center>
			<form action='<%=final_url%>'>
				<table class="api">
					<tr>
						<td class="field">
							Order Total:</td>
						<td>USD&nbsp<%=paymentAmount%></td>
					</tr>
					<tr>
						<td class="field">
							Street 1:</td>
						<td><%=resArray("SHIPTOSTREET")%></td>
					</tr>
					<tr>
						<td class="field">
							Street 2:</td>
						<td>
						</td>
					</tr>
					<tr>
						<td class="field">
							City:</td>
						<td><%=resArray("SHIPTOCITY")%></td>
					</tr>
					<tr>
						<td class="field">
							State:</td>
						<td><%=resArray("SHIPTOSTATE")%></td>
					</tr>
					<tr>
						<td class="field">
							Postal code:</td>
						<td><%=resArray("SHIPTOZIP")%></td>
					</tr>
					<tr>
						<td class="field">
							Country:</td>
						<td><%=resArray("SHIPTOCOUNTRYNAME")%></td>
					</tr>
					<tr>
						<td colspan="2" class="header">
						<center>
							<input type="submit" value="Pay" />
						</center>
						</td>
					</tr>
				</table>
			</form>
		</center>
		<% 
    If Err.Number <> 0 Then 
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"GetExpressCheckoutDetails.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
	</body>
</html>
