<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>order edit cmd</title>
</head>
<body>
<%
    dim cmd         :   cmd             =   SQLRequest("cmd")
    Dim order_code  :   order_code      =   SQLRequest("order_code")
    
    if cmd = "getPersonalInfomation" then
        Set rs = conn.execute("Select * from tb_customer_store where order_code='"& order_code &"'")
        if not rs.eof then
            
            if(rs("customer_company") <> "")                                    then Response.write rs("customer_company") & "<br/>"
            if(rs("customer_last_name")<>"" or rs("customer_first_name")<>""    then Response.write rs("customer_first_name") & "&nbsp;"& rs("customer_last_name") & "<br/>"
            if(rs("phone_d")<>"")                                               then Response.write rs("phone_d") & "<br/>"
            if(rs("phone_n")<>"")                                               then Response.write rs("phone_n") & "<br/>"
            if(rs("phone_c")<>"")                                               then Response.write rs("phone_c") & "<br/>"
            if(rs("customer_email1")<>"")                                       then Response.write rs("customer_email1") & "<br/>"
            if(rs("customer_email2")<>"")                                       then Response.write rs("customer_email2") & "<br/>"
            if(rs("customer_address1")<>"")                                     then Response.write rs("customer_address1") & "<br/>"
            if(rs("customer_city")<>"")                                         then Response.write rs("customer_city") & "&nbsp;" & rs("state_code") & "&nbsp;" & rs("zip_code") & "<br/>"
            
            if(rs("customer_shipping_first_name")<>"")                          then Response.write rs("customer_shipping_first_name") & "&nbsp;"& rs("customer_shipping_last_name") &"<br/>"
            if(rs("customer_shipping_address")<>"")                             then Response.write rs("customer_shipping_address") & "<br/>"
            if(rs("customer_shipping_city")<>"")                                then Response.write rs("customer_shipping_city") & "&nbsp;" & rs("customer_shipping_zip_code") & "&nbsp;" & rs("shipping_state_code") &"<br/>"
        
            if(rs("customer_card_first_name")<>"")                              then Response.write rs("customer_card_first_name") & "&nbsp;" & rs("customer_card_last_name") & "<br/>"
            if(rs("customer_credit_card")<>"")                                  then Response.write rs("customer_credit_card") & "<br/>"
            if(rs("card_verification_number")<>"")                              then Response.write rs("card_verification_number") & "<br/>"
            if(rs("customer_expiry") <> "")                                     then Response.write rs("customer_expiry") & "<br/>"
            if(rs("customer_card_issuer")<>"")                                  then Response.write rs("customer_card_issuer") & "<br/>"
            if(rs("customer_card_phone")<>"")                                   then Response.write rs("customer_card_phone") & "<br/>"
            if(rs("customer_card_billing_shipping_address")<>"")                then Response.write rs("customer_card_billing_shipping_address") & "<br/>"
            if(rs("customer_card_city")<>"")                                    then Response.write rs("customer_card_city") & "&nbsp;" & rs("customer_card_state") & "&nbsp;" & rs("customer_card_zip_code") & "<br/>"
            
        else
        
        end if
        rs.close : set rs = nothing
    
    end if
    
    closeconn()
 %>
</body>
</html>
