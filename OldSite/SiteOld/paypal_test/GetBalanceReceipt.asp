<!-- #include file ="CallerService.asp" -->
<%

''nvpstr=""
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("GetBalance",nvpstr)
	ack = UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	If ack="SUCCESS" Then
		
	Else
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
	End If

%>
<html>
	<head>
		<title>PayPal ASP SDK - Get Balance API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body alink="#0000FF" vlink="#0000FF">
		<center>
			<font size="2" color="black" face="Verdana"><b>Get Balance</b></font>
			<table class="api">
				<tr>
					<td>
						Amount :</td>
					<td><%=resArray("L_AMT0")%></td>
				</tr>
				<tr>
					<td>
						Currency ID:</td>
					<td><%=resArray("L_CURRENCYCODE0")%></td>
				</tr>
			</table>
		</center>
		<%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"GetBalanceReceipt.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
		<br>
		<a class="home" id="CallsLink" href="default.htm">Home</a>
	</body>
</html>
