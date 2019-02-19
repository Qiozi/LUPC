<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html >
<head>
    <title>LUC Paypal Do Direct Payment</title>
</head>
<body>
<%
    Dim Order_code  :   Order_code = SQLescape(request("order_code"))
    Dim balance     :   balance = SQLescape(request("balance"))
    Dim USD_CAD     :   USD_CAD =   SQLescape(request("USD_CAD"))
    if isnull(USD_CAD) or isempty(USD_CAD) then USD_CAD = "CAD"

    Dim First_name  
    Dim Last_Name 
    Dim Card_type 
    Dim Card_number 
    Dim Expiration_Date 
    Dim expiry_month
    Dim expiry_year
    Dim Card_verification_number 
    Dim address1 
    Dim address2
    Dim City
    Dim State 
    Dim ZipCode	
    Dim Country	

    set rs = conn.execute("Select * from tb_customer_store where order_code='"& Order_code &"'")
    if not rs.eof then

     First_name     = rs("customer_card_first_name")
     Last_Name      = rs("customer_card_last_name")
     Card_type      = rs("customer_card_type")

     if(trim(rs("customer_credit_card"))) <> "" then
        Card_number    = replace(rs("customer_credit_card"), " ","")
     end if
     Expiration_Date = rs("customer_expiry")
     Card_verification_number = rs("card_verification_number")
     address1       = rs("customer_card_billing_shipping_address")
     City           = rs("customer_card_city")
     State          = rs("customer_card_state_code")
     ZipCode	    = rs("customer_card_zip_code")
     Country	    = rs("customer_card_country_code")
    end if
    rs.close : set rs = nothing

    if len(Expiration_Date) =6 then
        expiry_month = left(Expiration_Date, 2)
        expiry_year  = right(Expiration_Date, 4)
    end if

    set rs = conn.execute("select price_unit from tb_order_helper  where order_code='"& Order_code &"'")
    if not rs.eof then
        USD_CAD = rs(0)
    end if
    rs.close : set rs = nothing
 %>
<form action="paypal_card_do_direct_payment_exec.asp" method="POST" onsubmit=" return confirm('Are you sure!!');">
			<input type=hidden name=paymentType value='sale' >

				<font size="2" color="black" face="Verdana"><b>DoDirectPayment</b></font>
				<table style="margin:auto;">
					<tr>
						<td >
							Order Code:</td>
						<td>
							<input type="text" size="30" maxlength="6" name="order_code" value="<%= Order_code %>"></td>
					</tr>
					<tr>
						<td >
							Ebay Code:</td>
						<td>
							<input type="text" size="30" maxlength="30" name="code" value=""></td>
					</tr>
					<tr>
						<td >
							First Name:</td>
						<td>
							<input type="text" size="30" maxlength="32" name="firstName" value="<%= First_name %>"></td>
					</tr>
					<tr>
						<td >
							Last Name:</td>
						<td>
							<input type="text" size="30" maxlength="32" name="lastName" value="<%= Last_Name%>"></td>
					</tr>
					<tr>
						<td >
							Card Type:</td>
						<td>
							<select name="creditCardType">
								<option></option>
								<option value="Visa" <% if Card_type = "Visa" then Response.write " selectec='selected' "  %>>Visa</option>
								<option value="MasterCard"<% if Card_type = "MasterCard" then Response.write " selectec='selected' "  %>>MasterCard</option>
								<option value="Discover"<% if Card_type = "Discover" then Response.write " selectec='selected' "  %>>Discover</option>
								<option value="Amex"<% if Card_type = "Amex" then Response.write " selectec='selected' "  %>>American Express</option>
							</select>
						</td>
					</tr>
					<tr>
						<td >
							Card Number:</td>
						<td>
							<input type="text" size="19" maxlength="19" name="creditCardNumber" value="<%= Card_number %>"></td>
					</tr>
					<tr>
						<td >
							Expiration Date:</td>
						<td>
							<select name="expDateMonth">
                                <option></option>
								<option value="01" <% if expiry_month = "01" then Response.write " selected='selected' " %>>01</option>
								<option value="02" <% if expiry_month = "02" then Response.write " selected='selected' " %>>02</option>
								<option value="03" <% if expiry_month = "03" then Response.write " selected='selected' " %>>03</option>
								<option value="04" <% if expiry_month = "04" then Response.write " selected='selected' " %>>04</option>
								<option value="05" <% if expiry_month = "05" then Response.write " selected='selected' " %>>05</option>
								<option value="06" <% if expiry_month = "06" then Response.write " selected='selected' " %>>06</option>
								<option value="07" <% if expiry_month = "07" then Response.write " selected='selected' " %>>07</option>
								<option value="08" <% if expiry_month = "08" then Response.write " selected='selected' " %>>08</option>
								<option value="09" <% if expiry_month = "09" then Response.write " selected='selected' " %>>09</option>
								<option value="10" <% if expiry_month = "10" then Response.write " selected='selected' " %>>10</option>
								<option value="11" <% if expiry_month = "11" then Response.write " selected='selected' " %>>11</option>
								<option value="12" <% if expiry_month = "12" then Response.write " selected='selected' " %>>12</option>
							</select>
							<select name="expDateYear">			
                                <option></option>
								<option value="2011" <% if expiry_year = "2011" then Response.write " selected='selected'" %>>2011</option>
								<option value="2012" <% if expiry_year = "2012" then Response.write " selected='selected'" %>>2012</option>
								<option value="2013" <% if expiry_year = "2013" then Response.write " selected='selected'" %>>2013</option>
								<option value="2014" <% if expiry_year = "2014" then Response.write " selected='selected'" %>>2014</option>
								<option value="2015" <% if expiry_year = "2015" then Response.write " selected='selected'" %>>2015</option>
								<option value="2016" <% if expiry_year = "2016" then Response.write " selected='selected'" %>>2016</option>
								<option value="2017" <% if expiry_year = "2017" then Response.write " selected='selected'" %>>2017</option>
								<option value="2018" <% if expiry_year = "2018" then Response.write " selected='selected'" %>>2018</option>

                                <option value="2019" <% if expiry_year = "2019" then Response.write " selected='selected'" %>>2019</option>
                                <option value="2020" <% if expiry_year = "2020" then Response.write " selected='selected'" %>>2020</option>
                                <option value="2021" <% if expiry_year = "2021" then Response.write " selected='selected'" %>>2021</option>
                                <option value="2022" <% if expiry_year = "2022" then Response.write " selected='selected'" %>>2022</option>
                                <option value="2023" <% if expiry_year = "2023" then Response.write " selected='selected'" %>>2023</option>
                                <option value="2024" <% if expiry_year = "2024" then Response.write " selected='selected'" %>>2024</option>
                                <option value="2025" <% if expiry_year = "2025" then Response.write " selected='selected'" %>>2025</option>
							</select>
						</td>
					</tr>
					<tr>
						<td >
							Card Verification Number:</td>
						<td>
							<input type="text" size="4" maxlength="4" name="cvv2Number" value="<%= Card_verification_number %>" 
                                style="height: 22px"></td>
					</tr>
					<tr>
						<td align="right"><br>
							<b>Billing Address:</b></td>
						</td>
					</tr>
					<tr>
						<td >
							Address 1:
						</td>
						<td>
							<input type="text" size="25" maxlength="100" name="address1" value="<%= address1 %>"></td>
					</tr>
					<tr>
						<td >
							Address 2:
						</td>
						<td>
							<input type="text" size="25" maxlength="100" name="address2">(optional)</td>
					</tr>
					<tr>
						<td >
							City:
						</td>
						<td>
							<input type="text" size="25" maxlength="40" name="city" value="<%= City %>"></td>
					</tr>
					<tr>
						<td >
							State:
						</td>
						<td>
							<select name="state">
							    <optgroup label="CA">
							    <%
							    set rs = conn.execute("select state_code,state_name from tb_state_shipping where is_paypal=1 and system_category_serial_no=1 order by state_code asc")
							    if not rs.eof then
							        do while not rs.eof 
							                response.write "<option value='"& rs(0)&"' "
                                            if State = rs(0) then Response.Write " Selected='Selected' "
                                            Response.write ">"& rs(0) &"--"& rs(1) &"</option>"
							        rs.movenext
							        loop
							    end if
							    rs.close : set rs = nothing
							     %>
							     </optgroup>
								<optgroup label="US">
								<%
							    set rs = conn.execute("select state_code,state_name from tb_state_shipping where is_paypal=1 and system_category_serial_no=2 order by state_code asc")
							    if not rs.eof then
							        do while not rs.eof 
							                response.write "<option value='"& rs(0)&"' "
                                            if State = rs(0) then Response.Write " Selected='Selected' "
                                            Response.write " >"& rs(0) &"--"& rs(1) &"</option>"
							        rs.movenext
							        loop
							    end if
							    rs.close : set rs = nothing
							     %>
								</optgroup >
							</select>
						</td>
					</tr>
					<tr>
						<td >
							ZIP Code:
						</td>
						<td>
							<input type="text" size="10" maxlength="10" name="zip" value="<%= ZipCode %>">(5 or 9 
							digits)
						</td>
					</tr>
					<tr>
						<td >
							Country:
						</td>
						<td>
							<select name="Country">
								<option value="US" <% if Country = "US" then Response.write " selected='Selected' " %>>US</option>								
								<option value="CA" <% if Country = "CA" then Response.write " selected='Selected' " %>>CA</option>
							</select>
						</td>
					</tr>
					<tr>
						<td >
							Amount:</td>
						<td>
							<input type="text" size="4" maxlength="7" name="amount" value="<%= balance %>"> 
							<select name="currency">
								<option value="USD" <% if USD_CAD = "USD" then response.write " Selected='selected'" %>>USD</option>
								<!--<option value="GBP">GBP</option>
								<option value="EUR">EUR</option>
								<option value="JPY">JPY</option>-->
								<option value="CAD" <% if USD_CAD = "CAD" then response.write " Selected='selected'" %>>CAD</option>
								<!--<option value="AUD">AUD</option>-->
							</select>
						</td>
					</tr>
					<tr>
						<td >
						</td>
						<td>
							<input type="Submit" value="Submit"></td>
					</tr>
				</table>

			
		</form>
		<% closeconn() %>
</body>
</html>
