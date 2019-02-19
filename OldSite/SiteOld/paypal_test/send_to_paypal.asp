<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>


<body>
<%
'
'function getHTTPPage(url) 
'	on error resume next 
'	dim http 
'	set http=Server.createobject("Microsoft.XMLHTTP") 
'	Http.open "GET",url,false 
'	Http.send() 
'	if Http.readystate<>4 then
'		exit function 
'	end if 
'	getHTTPPage=BytesToBstr(Http.responseBody)
'	set http=nothing
'	if err.number<>0 then err.Clear  
'end function 
'
'Function BytesToBstr(body)
' dim objstream
' set objstream = Server.CreateObject("adodb.stream")
' objstream.Type = 1
' objstream.Mode =3
' objstream.Open
' objstream.Write body
' objstream.Position = 0
' objstream.Type = 2
' objstream.Charset = "utf-8"
' BytesToBstr = objstream.ReadText 
' objstream.Close
' set objstream = nothing
'End Function

Dim Item_name, Item_number, Payment_status, Payment_amount
Dim Txn_id, Receiver_email, Payer_email
Dim objHttp, str

' read post from PayPal system and add 'cmd'
str = Request.Form & "&cmd=_notify-validate"

' post back to PayPal system to validate
set objHttp = Server.CreateObject("Msxml2.ServerXMLHTTP")
' set objHttp = Server.CreateObject("Msxml2.ServerXMLHTTP.4.0")
' set objHttp = Server.CreateObject("Microsoft.XMLHTTP")
'objHttp.open "POST", "https://www.paypal.com/cgi-bin/webscr", false
objHttp.open "POST", "https://api-3t.sandbox.paypal.com/nvp/", false
objHttp.setRequestHeader "Content-type", "application/x-www-form-urlencoded"
objHttp.Send str

' assign posted variables to local variables
Item_name = Request.Form("item_name")
Item_number = Request.Form("item_number")
Payment_status = Request.Form("payment_status")
Payment_amount = Request.Form("mc_gross")
Payment_currency = Request.Form("mc_currency")
Txn_id = Request.Form("txn_id")
Receiver_email = Request.Form("receiver_email")
Payer_email = Request.Form("payer_email")

' Check notification validation
if (objHttp.status <> 200 ) then
' HTTP error handling
	response.Write("http error")
elseif (objHttp.responseText = "VERIFIED") then
' check that Payment_status=Completed
' check that Txn_id has not been previously processed
' check that Receiver_email is your Primary PayPal email
' check that Payment_amount/Payment_currency are correct
' process payment
response.write "verified"
elseif (objHttp.responseText = "INVALID") then
' log for manual investigation
	response.Write("Invalid")
else
' error
response.Write("error")
end if
set objHttp = nothing

'response.Write(getHTTPPage("https://api.sandbox.paypal.com/nvp?user=qiozi4_1206899598_biz_api1.163.com&pwd=1206899607&signature=Ab.--knS-exvZZP3DONGdTh.Z32eAmLcNYNggqRzRox4Ft.eAAPdAar-&AMT=1&method=SetExpressCheckout&ReturnURL=https://www.lucomputers.com/paypal_test/paypal_return_ok.asp&CancelURL=https://www.lucomputers.com/paypal_test/paypal_return_cancel.asp") & "test") 
%>
<!--<form action="https://api.sandbox.paypal.com/nvp?AMT=1&method=SetExpressCheckout&ReturnURL=https://www.lucomputers.com/paypal_test/paypal_return_ok.asp&CancelURL=https://www.lucomputers.com/paypal_test/paypal_return_cancel.asp" method="post" name="form1" id="form1">
       <input type=hidden name=USER value="qiozi4_1206899598_biz_api1.163.com">
            <input type=hidden name=PWD value="1206899607">
            <input type=hidden name=SIGNATURE value="Ab.--knS-exvZZP3DONGdTh.Z32eAmLcNYNggqRzRox4Ft.eAAPdAar-"> 

        <input type="text" name="AMT" value="1" />
		<input type="text" name="ReturnURL" value="https://www.lucomputers.com/paypal_test/paypal_return_ok.asp" />
		<input type="text" name="CancelURL" value="https://www.lucomputers.com/paypal_test/paypal_return_cancel.asp" />
        <input type="text" name="method" value="SetExpressCheckout" />
		
        <input type="submit" value="Submit To Paypal" />
	</form>-->
    
<!--   <form action="https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=EC-99F320300J3785721" method="post" name="form1" id="form1">
         
        <input type="submit" value="Submit To Paypal" />
	</form>-->
    <%



'/////////////////////////////////////////////////////////////////////////////////////////////////////



' THIS CODE WILL NOT WORK IF YOU DID NOT GRANT THE IUSR_MACHINENAME AND IWAM_MACHINENAME ACCOUNT ACCESS PERMISSION TO THE PAYPAL API SSL CERT, BECAUSE THOSE ARE THE ACCOUNTS THAT EXECUTE ASP3 CODE (NOT THE ASPNET ACCOUNT). WORKS LIKE A CHARM 



' IF YOU READ THE DIRECTIONS GOOD LUCK



'/////////////////////////////////////////////////////////////////////////////////////////////////////
 
'
'
'username = "qiozi4_1206899598_biz_api1.163.com"
'password = "1206899607"
'
'Dim objXMLDOC
'Dim objXMLDOM
'Dim objXSLDOM
'Dim SoapStr
'
'SoapStr = "<?xml version='1.0' encoding='UTF-8'?>"
'SoapStr = SoapStr & "<SOAP-ENV:Envelope "
'SoapStr = SoapStr & "xmlnssd='http://www.w3.org/1999/XMLSchema '"
'SoapStr = SoapStr & "xmlnssi='http://www.w3.org/1999/XMLSchema-instance' "
'SoapStr = SoapStr & "xmlns:SOAP-ENC='http://schemas.xmlsoap.org/soap/encoding/' "
'SoapStr = SoapStr & "xmlns:SOAP-ENV='http://schemas.xmlsoap.org/soap/envelope/' "
'SoapStr = SoapStr & "SOAP-ENV:encodingStyle='http://schemas.xmlsoap.org/soap/encoding/'>"
'SoapStr = SoapStr & "<SOAP-ENV:Header>"
'SoapStr = SoapStr & "<RequesterCredentials "
'SoapStr = SoapStr & "xmlns='urn:ebay:api:PayPalAPI' "
'SoapStr = SoapStr & "SOAP-ENV:mustUnderstand='1'>"
'SoapStr = SoapStr & "<Credentials xmlns='urn:ebay:apis:eBLBaseComponents'>"
'SoapStr = SoapStr & "<Username>"&username &"</Username>"
'SoapStr = SoapStr & "<Password>"&password &"</Password>"
'SoapStr = SoapStr & "<Subject></Subject>"
'SoapStr = SoapStr & "</Credentials>"
'SoapStr = SoapStr & "</RequesterCredentials>"
'SoapStr = SoapStr & "</SOAP-ENV:Header>"
'SoapStr = SoapStr & "<SOAP-ENV:Body>"
'
'
'' Actions Below Here
'SoapStr = SoapStr & "<DoDirectPaymentReq xmlns='urn:ebay:api:PayPalAPI'>"
'SoapStr = SoapStr & "<DoDirectPaymentRequest xsi:type='nsoDirectPaymentRequestType'>"
'SoapStr = SoapStr & "<Version xmlns='urn:ebay:apis:eBLBaseComponents' xsi:type='xsd:string'>1.0</Version>"
'SoapStr = SoapStr & "<DoDirectPaymentRequestDetails xmlns='urn:ebay:apis:eBLBaseComponents'>"
'SoapStr = SoapStr & "<PaymentAction>Sale</PaymentAction>"
'SoapStr = SoapStr & "<PaymentDetails>"
'SoapStr = SoapStr & "<OrderTotal currencyID='USD'>60</OrderTotal>"
'SoapStr = SoapStr & "</PaymentDetails>"
'SoapStr = SoapStr & "<CreditCard>"
'SoapStr = SoapStr & "<CreditCardType>Visa</CreditCardType>"
'SoapStr = SoapStr & "<CreditCardNumber>4250614840518960</CreditCardNumber>"
'SoapStr = SoapStr & "<ExpMonth>05</ExpMonth>"
'SoapStr = SoapStr & "<ExpYear>2008</ExpYear>"
'SoapStr = SoapStr & "<CardOwner>"
'SoapStr = SoapStr & "<PayerName>"
'SoapStr = SoapStr & "<FirstName>Joe</FirstName>"
'SoapStr = SoapStr & "<LastName>Smith</LastName>"
'SoapStr = SoapStr & "</PayerName>"
'SoapStr = SoapStr & "<Address>"
'SoapStr = SoapStr & "<Street1>1 Main St</Street1>"
'SoapStr = SoapStr & "<CityName>San Jose</CityName>"
'SoapStr = SoapStr & "<StateOrProvince>CA</StateOrProvince>"
'SoapStr = SoapStr & "<Country>US</Country>"
'SoapStr = SoapStr & "<PostalCode>95131</PostalCode>"
'SoapStr = SoapStr & "</Address>"
'SoapStr = SoapStr & "</CardOwner>"
'SoapStr = SoapStr & "</CreditCard>"
'SoapStr = SoapStr & "<IPAddress>123.123.123.123</IPAddress>"
'SoapStr = SoapStr & "</DoDirectPaymentRequestDetails>"
'SoapStr = SoapStr & "</DoDirectPaymentRequest>"
'SoapStr = SoapStr & "</DoDirectPaymentReq>"
'SoapStr = SoapStr & "</SOAP-ENV:Body>"
'SoapStr = SoapStr & "</SOAP-ENV:Envelope>"
'
'Set objXMLDOC = Server.CreateObject("Msxml2.ServerXMLHTTP")
'Set objXMLDOM = Server.CreateObject("Msxml2.DomDocument")
'objXMLDOC.setOption 3, "LOCAL_MACHINE\MY\" &username
'objXMLDOC.SetOption 2, 13056
'objXMLDOC.setTimeouts 10 * 100, 10 * 100, 10 * 100, 10 * 100
'
'objXMLDOC.open "POST", "https://api.paypal.com/2.0/", False
'objXMLDOC.setRequestHeader "Content-Type", "text/xml"
'objXMLDOC.send (SoapStr)
'
'objXMLDOM.async=false
'objXMLDOM.loadXML objXMLDOC.responseText
'
'Response.Write objXMLDOC.responseText
'
'Set objXMLDOC = Nothing
'Set objXMLDOM = Nothing
%>

</body>
</html>
