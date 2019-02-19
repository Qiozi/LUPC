<!-- #include file ="CallerService.asp" -->
<!-- #include virtual="public_helper/helper.asp"-->
<!-- #include virtual="public_helper/public_helper.asp"-->
<script type="text/javascript">
    window.location.replace("/shopping_cheout_order_ok.asp?3");
</script>

<%

Response.Buffer = True
'-----------------------------------------------------------------------------
' DoDirectPaymentReceipt.asp

' Submits a credit card transaction to PayPal using a
' DoDirectPayment request.

' The code collects transaction parameters from the form
' displayed by DoDirectPayment.asp then constructs and sends
' the DoDirectPayment request string to the PayPal server.
' The paymentType variable becomes the PAYMENTACTION parameter
' of the request string.

' After the PayPal server returns the response, the code
' displays the API request and response in the browser.
' If the response from PayPal was a success, it displays the
' response parameters. If the response was an error, it
' displays the errors.

' Called by DoDirectPayment.asp.

' Calls CallerService.asp and APIError.asp.

'-----------------------------------------------------------------------------


	Dim firstName
	Dim lastName
	Dim creditCardType
	Dim creditCardNumber
	Dim expDateMonth
	Dim expDateYear
	Dim padDate
	Dim cvv2Number
	Dim address1
	Dim address2
	Dim city
	Dim state
	Dim zip
	Dim amount
	Dim currencyCode
	Dim paymentType
	Dim nvpstr
	Dim resArray
	Dim ack
	Dim message

    dim order_code
    order_code = current_tmp_order_code
    set rs = conn.execute("select oh.pay_method "&_
                                            " ,grand_total "&_
                                            " ,oh.pay_method "&_
                                            " ,cs.card_verification_number "&_
                                            " ,cs.customer_first_name "&_
                                            " ,cs.customer_last_name "&_
                                            " ,cs.customer_credit_card"&_
                                            " ,cs.customer_card_billing_shipping_address"&_
                                            " ,cs.customer_card_city "&_
                                            " ,cs.customer_card_zip_code "&_
                                            " ,ss.state_code "&_
                                            " ,cs.customer_expiry "&_ 
                                            " ,cs.customer_card_country "&_
                                            " ,cs.customer_card_type "&_
                                            "         from tb_order_helper oh inner join tb_customer_store cs"&_
                                            "         on cs.order_code=oh.order_code "&_
                                            "          left join tb_state_shipping ss on ss.state_serial_no=cs.customer_card_state "&_
                                            "         where oh.order_code='"& order_code &"'")
    if not rs.eof  then
        if instr(PAYPAL_PAYMENTS, "["& rs("pay_method") &"]") > 0 then
    
             if rs("grand_total")<>"" and rs("customer_credit_card")<>"" and rs("customer_expiry") <> "" and rs("customer_first_name") <> "" and rs("customer_last_name") <>"" and rs("customer_card_billing_shipping_address") <>"" and rs("state_code") <>"" and rs("customer_card_city") <> "" and rs("card_verification_number") <> ""  and rs("customer_card_country") <> "" then
                   
                    if rs("customer_card_type") = "VS" then
                                CREDITCARDTYPE = "Visa"
                    end if
                    if rs("customer_card_type") = "MC" then
                                CREDITCARDTYPE = "MasterCard"
                    end if
                    if cstr(rs("customer_card_country")) = "1" then
                        COUNTRYCODE = "CA"
                    end if
                    if cstr(rs("customer_card_country")) = "2" then
                        COUNTRYCODE = "US"    
                    end if
                    currencyCode = "CAD"
                        
	            firstName			    = rs("customer_first_name")
	            lastName			    = rs("customer_last_name")
	            creditCardType		= CREDITCARDTYPE
	            creditCardNumber	    = rs("customer_credit_card")
	            padDate				    = rs("customer_expiry")
	            cvv2Number			= rs("card_verification_number")
	            address1			        = rs("customer_card_billing_shipping_address")

	            city				        = rs("customer_card_city")
	            state				        = rs("state_code")
	            zip					        = rs("customer_card_zip_code")
	            amount				    = rs("grand_total")
	            paymentType			= "Sale"
'-----------------------------------------------------------------------------
' Construct the request string that will be sent to PayPal.
' The variable $nvpstr contains all the variables and is a
' name value pair string with &as a delimiter
'-----------------------------------------------------------------------------
	nvpstr	=	"&PAYMENTACTION=Sale"& _
				"&AMT="&amount &_
				"&CREDITCARDTYPE="&creditCardType &_
				"&ACCT="&creditCardNumber & _
				"&EXPDATE=" & padDate &_
				"&CVV2=" & cvv2Number &_
				"&FIRSTNAME=" & firstName &_
				"&LASTNAME=" & lastName &_
				"&STREET=" & address1 &_
				"&CITY=" & city &_
				"&STATE=" & state &_
				"&ZIP=" &zip &_
				"&COUNTRYCODE="& COUNTRYCODE &_
				"&CURRENCYCODE=" & currencyCode

	nvpstr	=	URLEncode(nvpstr)
'--------------------------------------------------------------------------
' Make the API call to PayPal,using API signature.
' The API response is stored in an associative array called gv_resArray
'-----------------------------------------------------------------------------
	Set resArray	= hash_call("doDirectPayment",nvpstr)
	ack = UCase(resArray("ACK"))
'----------------------------------------------------------------------------------
' Display the API request and API response back to the browser.
' If the response from PayPal was a success, display the response parameters
' If the response was an error, display the errors received
'----------------------------------------------------------------------------------
                            dim reskey, resitem
                            reskey = resArray.Keys
                            resitem = resArray.items
                            dim resindex
                            if ack <> UCase("SUCCESS") and not is_return_error_page then       
                                    For resindex = 0 To resArray.Count - 1 
                                        set srs = server.createobject("adodb.recordset")
                                        srs.open "select * from tb_order_paypal_error_info", conn, 1,3
                                        srs.addnew
                                        srs("errkey") =reskey(resindex)
                                        srs("erritem") = resitem(resindex) 
                                       ' response.write reskey(resindex) & ":" &  resitem(resindex)  &"<br/>"
                                        srs("order_code") = order_code   
                                        srs.update()
                                        srs.close : set srs = nothing
                                    next  
                            end if
                            
                            
	                        If ack<>"FAILURE" Then
		                            message="Thank you for your payment!"
		                            SESSION("ErrorMessage")	= Null	   
    		                                             
                                    dim paypal_transaction_id, paypal_avs, paypal_cvv2
                                    paypal_transaction_id = resArray("TRANSACTIONID")
                                    paypal_avs =  resArray("AVSCODE")
                                    paypal_cvv2 = resArray("CVV2MATCH")

                                    if paypal_transaction_id <> "" then 
                                        call AddOrderPayRecord(order_code, amount, HELPER_PAY_RECORD_METHOD_PAYPAL)
                                        conn.execute("Update tb_order_helper set order_pay_status_id='"& HELPER_PAYPAL_SUCCESS &"',is_modify=1 where order_code='"& order_code &"'")
                                        conn.execute("insert into tb_order_paypal_record ( transaction, avs, cvv2, order_code, regdate) values ( '"&paypal_transaction_id&"', '"&paypal_avs&"', '"&paypal_cvv2&"','"& current_tmp_order_code &"', now())")
                                    end if

                                    response.Redirect("/shopping_cheout_order_ok.asp?1")
                            else
                                    conn.execute("Update tb_order_helper set order_pay_status_id='"& HELPER_PAYPAL_FAILURE &"',is_modify=1 where order_code='"& order_code &"'")
	                        End If
	                    else
                response.write "ERROR"
            end if    
    else
        response.write "ERROR2"
    end if
else
    response.write "error3"
end if
rs.close : set rs = nothing

closeconn()
response.redirect("/shopping_cheout_order_ok.asp?2")

%>
