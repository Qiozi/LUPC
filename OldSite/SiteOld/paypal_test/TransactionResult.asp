<!-- #include file ="CallerService.asp" -->
<%

	Dim transaction_id
	Dim PayPalFromDate
	Dim PayPalToDate
	Dim note
	
	PayPalFromDate=Request.QueryString ("startDate")
	PayPalToDate=Request.QueryString ("endDate")
	Yearno =Year(PayPalFromDate)
	monthno=Month(PayPalFromDate)
	dayno=Day(PayPalFromDate)
	PayPalFromDate			= yearno &"-"& monthno &"-"& dayno & "T00:00:00Z"
	Yearno =Year(PayPalToDate)
	monthno=Month(PayPalToDate)
	dayno=Day(PayPalToDate)
	PayPalToDate		= yearno &"-"& monthno &"-"& dayno & "T23:00:00Z"
	
	nvpstr=	"&STARTDATE=" &PayPalFromDate & _ 
				"&TRXTYPE=Q"& _  
				"&ENDDATE="&PayPalToDate 

If Not Request.QueryString ("transactionID")= "" Then
transaction_id		= Request.QueryString ("transactionID")
nvpstr	=	"&STARTDATE=" &PayPalFromDate & _ 
				"&TRXTYPE=Q"& _  
				"&ENDDATE="&PayPalToDate  & _
			"&TRANSACTIONID="&transaction_id
End If
	
'-----------------------------------------------------------------------------
' Make the API call to PayPal,using API Signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("TransactionSearch",nvpstr)
	ack = UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
	If (ack="SUCCESS" or ack=UCase("SuccessWithWarning")) Then
		Message = "The transaction has been search!"
	Else
		 Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
	End If

%>
<html>
	<head>
		<title>PayPal ASP SDK: Transaction Search Results</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body alink="#0000FF" vlink="#0000FF">
		<center>
			<table class="api">
				<tr>
					<td colspan="6" align="center">
						<% 
                CntOfTrxn=0
               
		   
			For resindex = 0 To resArray.Count - 1 
			TrxnID="L_TRANSACTIONID"&resindex
			  If Not resArray(TrxnID) = ""Then
                CntOfTrxn= CntOfTrxn +1
                
                End if
                Next
                %>
						<b>Transaction Search Results 1 -<%=CntOfTrxn%></b>
					</td>
				</tr>
				<tr align="left">
					<td>
					</td>
					<td>
						ID</td>
					<td>
						Time</td>
					<td>
						Status</td>
					<td>
						Payer Name</td>
					<td>
						Gross Amount</td>
				</tr>
				<% 
		   IndexOfTrxn=0
		   reskey = resArray.Keys
		   resitem = resArray.items
		if (CntOfTrxn)=0 then
				 Response.Write "Your search did not match any transactions!"
		end if
			For resindex = 0 To CntOfTrxn - 1 %>
			
				<tr align="left">
					<td>
						<% 
				TrxnID="L_TRANSACTIONID"&resindex
			  If Not resArray(TrxnID) = ""Then
                IndexOfTrxn= IndexOfTrxn +1
                Response.Write(IndexOfTrxn)
                else
                IndexOfTrxn = null
                End if	



				 %>
					</td>
					<td>
						<% 
                If Not resArray(TrxnID) = ""Then
                 trans="<A id=TransactionDetailsLink0 href=TransactionDetails.asp?transactionID="&resArray(TrxnID)&_
                ">"&resArray(TrxnID)&"</A>"
                Response.Write(trans)
                else
                trans = null
                End if	
               
              
                %>
					</td>
					<td nowrap="true"><% 
				  

Timestamp="L_TIMESTAMP"&resindex

		If Not resArray(Timestamp) = ""Then
              	Timestamp=replace(resArray(Timestamp),"T"," ")
	Timestamp=replace(Timestamp,"Z"," ")
Response.Write(Timestamp)
                else
                Timestamp = null
                End if	


				 %></td>
					<td nowrap="true"><%
				  

Status="L_STATUS"&resindex
			If Not resArray(Status) = ""Then
              Response.Write(resArray(Status))
                else
                Status = null
                End if	 


				 %></td>
					<td><%
				  

Name="L_NAME"&resindex
				 
If Not resArray(Name) = ""Then
              Response.Write(resArray(Name))
                else
                Name = null
                End if	

				 %></td>
					<td nowrap="true"><%
				  

Amt="L_AMT"&resindex
				 
If Not resArray(Amt) = ""Then
               Response.Write("USD "&resArray(Amt))
                else
                Amt = null
                End if	

				 %></td>
				</tr>
				<% 
        
        next %>
				<%
    If Err.Number <> 0 Then
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"TransactionResult.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
    %>
				<tr>
					<td>
						
					</td>
				</tr>
			</table>
		</center>
		<a class="home" id="CallsLink" href="default.htm">Home</a>
	</body>
</html>
