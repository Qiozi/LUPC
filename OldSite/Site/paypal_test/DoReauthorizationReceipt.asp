<!-- #include file ="CallerService.asp" -->
<%


	Dim authorizationID
	Dim amount
	Dim currencyCode
	


	authorizationID		= Request.QueryString ("authorizationID")
	amount				= Request.QueryString ("amount")
	currencyCode		= Request.QueryString ("currency")
	
'-----------------------------------------------------------------------------
' Construct the request string that will be sent to PayPal.
' The variable $nvpstr contains all the variables and is a
' name value pair string with &as a delimiter
'-----------------------------------------------------------------------------
	nvpstr	=	"&AuthorizationID=" &authorizationID & _
				"&AMT="&amount &_
				"&CURRENCYCODE="&currencyCode 
				
	nvpstr	=	URLEncode(nvpstr)
	
	
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("DoReauthorization",nvpstr)
	ack =  UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	If ack="SUCCESS" Then
		Message = " ReAuthorization Succeeded!"
	Else
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
	End If

%>
<html>
	<head>
		<title>PayPal ASP - DoReAuthorization API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<center>
			<table>
				<tr>
					<td align="center">
						<b>Do ReAuthorization Receipt</b>
					</td>
				</tr>
				<tr>
					<td align="center">
						<b>
							<%=Message%>
						</b>
					</td>
				</tr>
				<tr>
					<td>Authorization ID:</td>
					<td><%= authorizationID %></td>
				</tr>
			</table>
		</center>
		<%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"DoReauthorizationReceipt.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
		<a class="home" id="CallsLink" href="Default.htm">Home</a>
	</body>
</html>
