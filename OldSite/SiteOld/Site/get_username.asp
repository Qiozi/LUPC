<!--#include virtual="site/inc/inc_helper.asp"-->

<%
response.Clear()
 if len(LAYOUT_CCID) = 0 then %>
Log in
<% else %>
Hi <% 
    dim login_customer_first_name
    login_customer_first_name = request.Cookies("customer_first_name")    
    if len(login_customer_first_name)>= 18 then
        login_customer_first_name = left(login_customer_first_name, 18)
        response.write  ucase(left(login_customer_first_name, 1)) & lcase(right(login_customer_first_name, len(login_customer_first_name) -1))
    elseif  len(login_customer_first_name)>= 1 then
        response.write  ucase(left(login_customer_first_name, 1)) & lcase(right(login_customer_first_name, len(login_customer_first_name) -1))
    end if
    response.Write("<script>$('#pre-logout').css({'display':''});</script>")
 end if

CloseConn()

%>