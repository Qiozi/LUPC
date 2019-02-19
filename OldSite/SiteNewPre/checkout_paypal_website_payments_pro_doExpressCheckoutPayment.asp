<%@  language="VBSCRIPT" codepage="65001" %>
<!-- #include virtual ="/paypal/CallerService.asp" -->
<!--#include virtual="/inc/conn.asp"-->
<!--#include virtual="/inc/inc_escape.asp"-->
<!--#include virtual="/inc/inc_func_sys.asp"-->
<%
'-------------------------------------------------------------------------------------------
' PayPal Express Checkout Example Step 3
' ======================================================================
' Complete the final payment for PayPal.  This
' page would normally be run to complete the
' payment with PayPal, and display the result
' back to the buyer
'-------------------------------------------------------------------------------------------

'checkout_paypal_website_payments_pro_doExpressCheckoutPayment.asp?token=EC-78834042V7749511V&payerID=5XV2B5BVEYKLU&paymentAmount=19.56&currCodeType=USD&paymentType=Sale
	'On Error Resume Next
	Dim token
	Dim payerID
	Dim paymentAmount
	Dim currCodeType
	Dim paymentType
	Dim nvpstr
	Dim ack
	Dim resArray
	Dim message
    Dim AMT
	
'-------------------------------------------------------------------------------------------
' Gather the information to make the final call to
' finalize the PayPal payment.  The variable nvpstr
' holds the name value pairs
'-------------------------------------------------------------------------------------------
	
	token			= SESSION("token")
	currCodeType	= SESSION("currencyCodeType")
	paymentAmount	= SESSION("paymentAmount")
	paymentType		= SESSION("PaymentType")
	payerID			= SESSION("PayerID")
	
	nvpstr			=	"&" & Server.URLEncode("TOKEN") & "=" & Server.URLEncode(token)  & "&" &_
						Server.URLEncode("PAYERID")&"=" &Server.URLEncode(payerID) & "&" &_
						Server.URLEncode("PAYMENTACTION")&"=" & Server.URLEncode(paymentType) & "&" &_
						Server.URLEncode("AMT") &"=" & Server.URLEncode(paymentAmount)&"&"&_ 
						Server.URLEncode("CURRENCYCODE")& "=" &Server.URLEncode(currCodeType)
'-------------------------------------------------------------------------------------------
' Make the call to PayPal to finalize payment
' If an error occured, show the resulting errors
'-------------------------------------------------------------------------------------------
	Set resArray=hash_call("DoExpressCheckoutPayment",nvpstr)
	ack = UCase(resArray("ACK"))
'-------------------------------------------------------------------------------------------
' Display the API request and API response back to the browser using APIError.asp.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'-------------------------------------------------------------------------------------------	


	if ack <> UCase("SUCCESS") and not is_return_error_page then       
		For resindex = 0 To resArray.Count - 1 
			set srs = server.createobject("adodb.recordset")
			srs.open "select * from tb_order_paypal_error_info", conn, 1,3
			srs.addnew
			srs("errkey") =reskey(resindex)
			srs("erritem") = resitem(resindex) 
			srs("order_code") = session("orderCode")   
			srs.update()
			srs.close : set srs = nothing
		next  
	end if
		
	If ack=UCase("Success") Then
		message			= "Thank you for your payment!"
		SESSION("ErrorMessage")	= Null
		response.write resArray("TRANSACTIONID")
		'
    
        dim paypal_transaction_id, paypal_avs, paypal_cvv2
        paypal_transaction_id 	= resArray("TRANSACTIONID")
        paypal_avs 				= resArray("AVS")
        paypal_cvv2 			= resArray("CVV2")
        AMT                     = resArray("AMT")

        if paypal_transaction_id <> "" then 
           
            call  AddOrderPayRecord(session("orderCode"), AMT, 15)		
    
            conn.execute("Update tb_order_helper set order_pay_status_id='2',Is_Modify=1 where order_code='"& session("orderCode") &"'")
            conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate,Amt) values ( '"&paypal_transaction_id&"', '"&paypal_avs&"', '"&paypal_cvv2&"','"& session("orderCode") &"', now(),'"&AMT&"')")
            conn.execute("Delete from tb_cart_temp where cart_temp_code='"& session("orderCode") &"'")
            conn.execute("Delete from tb_cart_temp_price where order_code='"& session("orderCode") &"'")
        end if
      
        response.Redirect("ShoppingCartGoSubmit.aspx?ordercode="&session("orderCode")&"&paypal_transaction_id="&paypal_transaction_id)
        response.End()
	Else       
		 'Set SESSION("nvpErrorResArray") = resArray
		 Response.Redirect "APIError.asp"
		 response.End()
	End If	
	


'--------------------------------------------------------------------------------------------
' If there is no Errors Construct the HTML page with a table of variables Loop through the associative array 
' for both the request and response and display the results.
'--------------------------------------------------------------------------------------------

closeconn()
%>

<html>

<head>
    <title>PayPal ASP SDK - DoExpressCheckoutPayment API</title>
</head>
<body alink="#0000FF" vlink="#0000FF">
    <center>
				<font size="2" color="black" face="Verdana"><b>DoExpressCheckoutPayment</b></font>
				<br><br>
				<b><%=message%></b><br>
				<br>
				<table class="api">
					<tr>
						<td class="field">
							Transaction ID:</td>
						<td><%=resArray("TRANSACTIONID")%></td>
					</tr>
					<tr>
						<td class="field">
							Amount:</td>
						<td><%=resArray("CURRENCYCODE")%>&nbsp;<%=resArray("AMT")%></td>
					</tr>
				</table>
			</center>
    <% 
    If Err.Number <> 0 Then 
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"DoExpressCheckoutPayment.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null

	End If
    %>
    <br>
    <a class="home" href="https://lucomputers.com/"><font color="blue"><B>Home</B></font></a>
</body>
</html>
