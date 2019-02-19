<%
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
<!-- #include file ="CallerService.asp" -->

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
<link href="lu.css" rel="stylesheet" type="text/css" />

<script language="javascript" src="js/helper.js"></script>
<script language="javascript" src="js/pre_helper.js"></script>
<style type="text/css">
<!--
body {
	background-color: #ECF6FF;
}
-->
</style></head>
<body>

                <center>

<table>

	<tr>
            <td colspan="2" class="header">
               <%=message%><%= resArray("TRANSACTIONID") %>
 </td></tr>

    
        <tr>
            <td colspan="2" class="header" >
                <%=ResponseHeader%>
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
                <% =reskey(resindex) %><B><%=Sepration%></B>
            </td>
            <td>
                <% =resitem(resindex) %></td>
        </tr>
        <% next %>

</table>
</center>

</body>
</html>