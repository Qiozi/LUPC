<!-- #include file ="CallerService.asp" -->
<%

	Dim emailSubject
	Dim receiverType

	Dim receiveremail1
	Dim amount1
	Dim uniqueID1
	Dim note1

	Dim receiveremail2
	Dim amount2
	Dim uniqueID2
	Dim note2

	Dim receiveremail3
	Dim amount3
	Dim uniqueID3
	Dim note3

	Dim message

	emailSubject		= Request.QueryString ("emailSubject")
	receiverType		= Request.QueryString ("receiverType")

	receiveremail1		= Request.QueryString ("receiveremail1")
	amount1				= Request.QueryString ("amount1")
	uniqueID1			= Request.QueryString ("uniqueID1")
	note1				= Request.QueryString ("note1")

	receiveremail2		= Request.QueryString ("receiveremail2")
	amount2				= Request.QueryString ("amount2")
	uniqueID2			= Request.QueryString ("uniqueID2")
	note2				= Request.QueryString ("note2")

	receiveremail3		= Request.QueryString ("receiveremail3")
	amount3				= Request.QueryString ("amount3")
	uniqueID3			= Request.QueryString ("uniqueID3")
	note3				= Request.QueryString ("note3")

'-----------------------------------------------------------------------------
' Construct the request string that will be sent to PayPal.
' The variable $nvpstr contains all the variables and is a
' name value pair string with &as a delimiter
'-----------------------------------------------------------------------------



	nvpstr	=	"&EMAILSUBJECT=" &emailSubject & _
				"&RECEIVERTYPE="&receiverType &_

				"&L_EMAIL0="&receiveremail1 &_
				"&L_Amt0="&amount1 & _
				"&L_UNIQUEID0=" &uniqueID1 &_
				"&L_NOTE0=" &note1 &_

				"&L_EMAIL1="&receiveremail2 &_
				"&L_Amt1="&amount2 & _
				"&L_UNIQUEID1=" &uniqueID2 &_
				"&L_NOTE1=" &note2 &_

				"&L_EMAIL2="&receiveremail3 &_
				"&L_Amt2="&amount3 & _
				"&L_UNIQUEID2=" &uniqueID3&_
				"&L_NOTE=" &note3


'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("MassPay",nvpstr)
	ack = UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	If ack="SUCCESS" Then
		message= " MassPay has been completed successfully! "
	Else
	 Set SESSION("nvpErrorResArray") = resArray
	Response.Redirect "APIError.asp"
	End If

%>

<html>
<head>
<title>PayPal ASP SDK - MassPay API </title>


</head>

<body alink=#0000FF vlink=#0000FF>

    <center>
    <table class="api">
        <tr>
            <td align=center>
                <b>MassPay Receipt</b>
            </td>
        </tr>
      <tr>
            <td><%=message%></td>
        </tr>


    </table>
    </center>
    <%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"MassPayReceipt.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
<br>
<a class="home"  id="CallsLink" href="default.htm">Home</a>
</body>
</html>


