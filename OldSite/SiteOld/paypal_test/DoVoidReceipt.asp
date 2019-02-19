<!-- #include file ="CallerService.asp" -->
<%

	Dim authorization_id
	Dim note


	authorization_id			= Request.QueryString ("authorization_id")
	note					= Request.QueryString ("note")

'-----------------------------------------------------------------------------
' Construct the request string that will be sent to PayPal.
' The variable $nvpstr contains all the variables and is a
' name value pair string with &as a delimiter
'-----------------------------------------------------------------------------
	nvpstr	=	"&AUTHORIZATIONID=" &authorization_id & _
				"&NOTE="&note 
				
	nvpstr	=	URLEncode(nvpstr)
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("DOVoid",nvpstr)
	ack = UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	If ack="SUCCESS" Then
		message="Thank you for your payment!"
	Else
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
	End If

%>
<html>
<head>
<title>PayPal ASP SDK - DoVoid API</title>
<link href="sdk.css" rel="stylesheet" type="text/css"/>

</head>

<body alink=#0000FF vlink=#0000FF>

    <center>
    <font size=2 color=black face=Verdana><b>Do Void</b></font>
		<br><br>
		<b>Authorization voided!</b><br><br>
    <table class="api">
        <tr>
            <td class="field">
                Authorization ID:</td>
            <td><%=left(resArray("AUTHORIZATIONID"),17)%></td>
        </tr>
    </table>
    </center>
    
    <%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"DoVoidReceipt.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
<br>
<a class="home"  id="CallsLink" href="default.htm">Home</a>
</body>
</html>
