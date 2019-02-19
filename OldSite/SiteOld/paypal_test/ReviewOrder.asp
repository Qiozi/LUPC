<!-- #include file ="CallerService.asp" -->
<%
'----------------------------------------------------------------------
' PayPal Express Checkout Example
' ==============================
' Collect a transaction parameters from a webform and
' process a transaction using a PayPal account.

' This script would normally be called after a user clicks on
' a button during the checkout process to checkout
' using PayPal's Express Checkout
'----------------------------------------------------------------------


'----------------------------------------------------------------------
' Define the PayPal URL.  This is the URL that the buyer is
' first sent to to authorize payment with their paypal account
' change the URL depending if you are testing on the sandbox
' or going to the live PayPal site
' For the sandbox, the URL is
' https://www.sandbox.paypal.com/webscr&cmd=_express-checkout&token=
' For the live site, the URL is
' https://www.paypal.com/webscr&cmd=_express-checkout&token=
'------------------------------------------------------------------------
	On Error Resume Next
	PAYPAL_URL	= PAYPAL_EC_URL
'-----------------------------------------------------------------------------
' An express checkout transaction starts with a token, that
' identifies to PayPal your transaction
' In this example, when the script sees a token, the script
' knows that the buyer has already authorized payment through
' paypal.  If no token was found, the action is to send the buyer
' to PayPal to first authorize payment
'--------------------------------------------------------------------------

	If  Request.QueryString("token")="" Then
'---------------------------------------------------------------------------
' The servername and serverport tells PayPal where the buyer
' should be directed back to after authorizing payment.
' In this case, its the local webserver that is running this script
' Using the servername and serverport, the return URL is the first
' portion of the URL that buyers will return to after authorizing payment
'----------------------------------------------------------------------------
		url = GetURL()
		paymentAmount=Request.Form("paymentAmount")
		currencyCodeType=Request.Form("currencyCodeType")
		paymentType=Request.Form("paymentType")
 
'---------------------------------------------------------------------------
' The returnURL is the location where buyers return when a
' payment has been succesfully authorized.
' The cancelURL is the location buyers are sent to when they hit the
' cancel button during authorization of payment during the PayPal flow
'---------------------------------------------------------------------------
		returnURL	= url & "ReviewOrder.asp?currencyCodeType=" &  currencyCodeType & _
					"&paymentAmount=" & paymentAmount & _ 
					"&paymentType=" &paymentType 
		cancelURL	= url & "SetExpressCheckout.asp?paymentType="&paymentType

'---------------------------------------------------------------------------
' Construct the parameter string that describes the PayPal payment
' the varialbes were set in the web form, and the resulting string
' is stored in nvpstr
'---------------------------------------------------------------------------
		nvpstr		= "&" & Server.URLEncode("AMT")&"=" & Server.URLEncode(paymentAmount) & _
					"&" &Server.URLEncode("PAYMENTACTION")&"=" & Server.URLEncode(paymentType) & _
					"&"&server.URLEncode("RETURNURL")&"=" & Server.URLEncode(returnURL) & _
					"&" &Server.URLEncode("CANCELURL")&"=" &Server.URLEncode(cancelURL) & _ 
					"&"&server.UrlEncode("CURRENCYCODE")&"=" & Server.URLEncode(currencyCodeType)

'--------------------------------------------------------------------------- 
' Make the call to PayPal to set the Express Checkout token
' If the API call succeded, then redirect the buyer to PayPal
' to begin to authorize payment.  If an error occured, show the
' resulting errors
'---------------------------------------------------------------------------
		Set resArray=hash_call("SetExpressCheckout",nvpstr)
		Set SESSION("nvpResArray")=resArray
		ack = UCase(resArray("ACK"))

		If ack="SUCCESS" Then
				' Redirect to paypal.com here
				token = resArray("TOKEN")
				payPalURL = PAYPAL_URL & "?cmd=_express-checkout&token=" & token
				ReDirectURL(payPalURL)
		Else  
				'Redirecting to APIError.asp to display errors. 
				message="<font color=red>PayPal API has returned an error!</font>"	          
				SESSION("msg")=message
				Response.Redirect "APIError.asp"
		End If

	Else 

'---------------------------------------------------------------------------
' At this point, the buyer has completed in authorizing payment
' at PayPal.  The script will now call PayPal with the details
' of the authorization, incuding any shipping information of the
' buyer.  Remember, the authorization is not a completed transaction
' at this state - the buyer still needs an additional step to finalize
' the transaction
'---------------------------------------------------------------------------
		SESSION("token") = Request.Querystring("TOKEN")
		SESSION("currencyCodeType") = Request.Querystring("currencyCodeType")
		SESSION("paymentAmount") = Request.Querystring("paymentAmount")
		SESSION("PaymentType")= Request.Querystring("PaymentType")
		SESSION("PayerID")= Request.Querystring("PayerID")
   

 '---------------------------------------------------------------------------
 'Build a second API request to PayPal, using the token as the
    'ID to get the details on the payment authorization
'---------------------------------------------------------------------------
		nvpstr="&TOKEN="&Request.Querystring("TOKEN")
		
'---------------------------------------------------------------------------
' Make the API call and store the results in an array.  If the
    'call was a success, show the authorization details, and provide
   ' an action to complete the payment.  If failed, show the error
'---------------------------------------------------------------------------
		Set resArray=hash_call("GetExpressCheckoutDetails",nvpstr)
		ack = UCase(resArray("ACK"))
		Set SESSION("nvpResArray")=resArray
		

		
	If UCase(ack)="SUCCESS" Then
		Response.Redirect "GetExpressCheckoutDetails.asp"
	Else  
		SESSION("msg")="<font color=red>Review Order.PayPal API has returned an error!</font>"
		Response.Redirect "APIError.asp"	          
	
	End If	
	
End If
    If Err.Number <> 0 Then 
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"RevievOrder.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If

%>