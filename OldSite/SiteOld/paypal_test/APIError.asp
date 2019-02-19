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


<html>
<head>
<title>PayPal ASP API Response</title>
<link href="sdk.css" rel="stylesheet" type="text/css"/>
   <!-- #include file ="CallerService.asp" -->
</head>

<body alink=#0000FF vlink=#0000FF>

<center>

<table width="700">

	<tr>
            <td colspan="2" class="header">
               <%=message%>
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
       
    
</tr>

</table>
</center>
<br>
<a class="home"  href="default.htm"><font color=blue><B>Home<B><font></a>
</body>
</html>
