<%@ CodePage=65001 %>

<HTML>
	<HEAD>
		<title>PayPal ASP API Response</title><%

Response.Charset = "UTF-8"
'--------------------------------------------------------------------------------------------
' API Request and Response/Error Output
' =====================================
' This page will be called after getting Response from the server
' or any Error occured during comminication for all APIs,to display Request,Response or Errors.
'--------------------------------------------------------------------------------------------
	Dim resArray
	Dim message
	Dim ResponseHeader
	Dim Sepration
	On Error Resume Next
	message		 =  SESSION("msg")
	Sepration		=":"
	Set resArray = SESSION("nvpErrorResArray")
	
	ResponseHeader="Error Response Details"
	
	
	If Not  SESSION("ErrorMessage")Then
	message = SESSION("ErrorMessage")
	ResponseHeader=""
	Sepration		=""
	End If
	
	
	If Err.Number <> 0 Then
	
	SESSION("nvpReqArray") = Null
	
	Response.flush
	End If
'--------------------------------------------------------------------------------------------
' If there is no Errors Construct the HTML page with a table of variables Loop through the associative array 
' for both the request and response and display the results.
'--------------------------------------------------------------------------------------------
%>
		<link href="sdk.css" rel="stylesheet" type="text/css">
		<!-- #include file ="../paypal/CallerService.asp" -->
	</HEAD>
	<body alink="#0000ff" vlink="#0000ff">
		<center>
			<table width="700">
				<tr>
					<td colspan="2" class="header" height="16">
						<%=message%>
					</td>
				</tr>
				<tr>
					<td colspan="2" class="header">
						<center>
							<%=ResponseHeader%>
						</center>
					</td>
				</tr>
				<!--displying all Response parameters -->
				<% 
		    reskey = resArray.Keys
		    resitem = resArray.items
			For resindex = 0 To resArray.Count - 1 
     %>
				<tr>
					<td class="field">
						<% =reskey(resindex) %>
						<B>
							<%=Sepration%>
						</B>
					</td>
					<td>
						<% =resitem(resindex) %>
					</td>
				</tr>
				<% next %>
				</TR>
			</table>
		</center>
		<br>
<%
DIM strPage
strPage = Request.QueryString("RecurringPage")
%>

<%
IF strPage = "RecurringPayments.asp" THEN
%>
<a class="home" href="RecurringPayments.asp"><font color="blue"><B>Home<B><font></a> </FONT></B></B></FONT>
<%
ELSE

END IF
%>
		
	</body>
</HTML>
