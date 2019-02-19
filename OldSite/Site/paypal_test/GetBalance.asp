<!-- #include file ="CallerService.asp" -->
<HTML>
	<HEAD>
		<title>PayPal ASP SDK - Get Balance API</title>
		<%

nvpstr=""
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------

%>
		<link href="sdk.css" rel="stylesheet" type="text/css">
			<form action="GetBalanceReceipt.asp" method="post" ID="GetBalance">
	</HEAD>
	<body alink="#0000ff" vlink="#0000ff">
		<center>
			<font size="2" color="black" face="Verdana"><b>Get Balance</b></font>
			<table class="api">
				<tr>
					<td align="center"><INPUT id="GetBalance" type="submit" value="Get Balance" name="GetBalance">&nbsp;
					</td>
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
		<a class="home" id="CallsLink" href="default.htm">Home</a> </FORM>
	</body>
</HTML>
