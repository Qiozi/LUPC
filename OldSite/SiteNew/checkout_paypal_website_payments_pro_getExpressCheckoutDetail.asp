<!--#include virtual="/inc/conn.asp"-->
<!--#include virtual="/inc/inc_escape.asp"-->
<!--#include virtual="/inc/inc_func_sys.asp"-->
<% 
'----------------------------------------------------------------------
'PayPal Express Checkout Example Step 2
'================================================
'Display the resulting authorization details from
'PayPal on a PayPal payment, and complete the
'payment authorization.  This script would be called
'when the buyer returns from PayPal and has authorized
'the payment
'----------------------------------------------------------------------
	'On Error Resume Next
	Dim resArray
	Dim reqArray
	Dim message
	Dim customerID
	Dim token
	Dim currCodeType
	Dim paymentAmount
	Dim paymentType
	Dim payer_id
	Dim final_url
	Dim rate_pay_methods
	Dim pay_pal_value
	Dim first_last
	Dim first_name
	Dim last_name
	Dim email
	Dim is_exist_user
	'Dim GetOrderRateByCart
	
	Set resArray	= SESSION("nvpResArray")
        response.Write(resArray("SHIPTOSTREET"))
'----------------------------------------------------------------------
'Collect the necessary information to complete the
'authorization for the PayPal payment
'----------------------------------------------------------------------
	token			= SESSION("token")
	currCodeType	= SESSION("currencyCodeType")
	paymentAmount	= SESSION("paymentAmount")
	paymentType		= SESSION("PaymentType")
	payer_id		= SESSION("PayerID")
	email			= resArray("EMAIL")
    orderCode       = Session("orderCode")   
	'response.write payer_id &"|"& email

'----------------------------------------------------------------------
 'Set the final URL to complete the authorization.  The
 'link will be displayed at the bottom the the browser for this
 'example.  The link would normally be displayed at the end of your checkout
 'and would finalize payment when clicked.
 '----------------------------------------------------------------------
	final_url		="checkout_paypal_website_payments_pro_doExpressCheckoutPayment.asp?token=" & token &_
					"&payerID="& payer_id &_ 
					"&paymentAmount="& paymentAmount &_ 
					"&currCodeType="& currCodeType &_ 
					"&paymentType=" &paymentType

	message			= "Get Express checkout Details!"
'----------------------------------------------------------------------
'Display the API request and API response back to the browser using Diaplay.asp.
'If the response from PayPal was a success, display the response parameters
'If the response was an error, display the errors received
'----------------------------------------------------------------------



'
'
'	save message to LUWeb
'
'
'customerID = LAYOUT_CCID
'if customerID = "" then SessionLost("closeConn")

dim state_id, state_code
set rs = conn.execute("select state_serial_no, state_code from tb_state_shipping where state_code='"&resArray("SHIPTOSTATE")&"' or state_name='"&resArray("SHIPTOSTATE")&"'")
if not rs.eof then
	state_id = rs(0)
	state_code = rs(1)
end if
rs.close : set rs = nothing


'
' if user is exist. then update info. else create
'
Set rs = conn.execute("Select customer_serial_no from tb_customer_store  where order_code='" & orderCode &"' ")
if not rs.eof then
	is_exist_user = true
    LAYOUT_CCID =  rs(0)
else
	is_exist_user = false
End if
rs.close : set rs = nothing


		set rs = server.createobject("adodb.recordset")
		
		if LAYOUT_CCID = "" then 
			if is_exist_user then 
			   rs.open "select * from tb_customer where customer_login_name="& SQLquote(resArray("EMAIL")), conn, 1,3		
			else             
			        rs.open "select * from tb_customer ", conn, 1,3
                    rs.addnew
                    rs("Customer_serial_no") = GetNewCustomerCode()
				    rs("create_datetime")=now()          			
			End if
		else
			rs.open "select * from tb_customer where customer_serial_no="& SQLquote(LAYOUT_CCID), conn, 1,3	
		end if
		
		rs("customer_shipping_address") = resArray("SHIPTOSTREET") & resArray("SHIPTOSTREET2")
		rs("customer_shipping_city") = resArray("SHIPTOCITY")
		rs("phone_d") = resArray("PHONENUM")
		rs("customer_shipping_state") = state_id
		rs("shipping_state_code") = state_code
		rs("customer_shipping_zip_code") = resArray("SHIPTOZIP")
       ' response.Write(resArray("FIRSTNAME"))
		rs("customer_first_name") = resArray("FIRSTNAME")
		rs("customer_last_name")  = resArray("LASTNAME")
'		if(email <> "" ) then 
'			rs("customer_email1") = email
'		end if
		
		rs("customer_email1") = resArray("EMAIL")
		
		first_last = resArray("SHIPTONAME")
		if instr(first_last, " ")>0 then 
			rs("customer_shipping_first_name") = mid(first_last, 1, instr(first_last, " "))
			rs("customer_shipping_last_name") = replace(first_last, rs("customer_shipping_first_name"), "")
		else
			rs("customer_shipping_first_name") = resArray("SHIPTONAME")
		end if
		if resArray("SHIPTOCOUNTRYNAME") = "Canada" or resArray("SHIPTOCOUNTRYNAME") = "CA" then 
			rs("customer_shipping_country") = 1
			rs("shipping_country_code") = "CA"
		end if
		if resArray("SHIPTOCOUNTRYNAME") = "United States" or resArray("SHIPTOCOUNTRYNAME") = "US" then 
			rs("customer_shipping_country") = 2
			rs("shipping_country_code") = "US"
		end if
		first_name = rs("customer_shipping_first_name")
		last_name = rs("customer_shipping_last_name")
		
		if not is_exist_user then 
			rs("customer_country_code") = rs("shipping_country_code")
			rs("state_code")			= rs("shipping_state_code")	
			rs("customer_login_name")	= resArray("EMAIL")
			rs("EBay_ID")				= resArray("EMAIL")
			rs("customer_address1")		= rs("customer_shipping_address")
			rs("customer_city")			= rs("customer_shipping_city")
			rs("customer_country_code") = rs("shipping_country_code")
			rs("zip_code")				= rs("customer_shipping_zip_code")

		End if

		rs.update()
		rs.close : set rs = Nothing

       
%>
<!-- #include virtual ="/paypal/CallerService.asp" -->
<script type="text/javascript">
    //$().ready(function () {
    window.location.href = "<%= final_url %>";
    //});
</script>
<% 

    If Err.Number <> 0 Then 
	SESSION("ErrorMessage")	= ErrorFormatter(Err.Description,Err.Number,Err.Source,"checkout_paypal_website_payments_pro_getExpressCheckoutDetail.asp")
	Response.Redirect "APIError.asp"
	Else
	SESSION("ErrorMessage")	= Null
	End If
	
	closeconn()

    response.Redirect( final_url)
%>
