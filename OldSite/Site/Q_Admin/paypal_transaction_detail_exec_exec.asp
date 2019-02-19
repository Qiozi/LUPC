<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="q_admin/funs.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
</head>
<body>
<%
    Dim EMAIL
    Dim PAYERSTATUS
    Dim SHIPTOCOUNTRYCODE
    Dim SHIPTONAME
    Dim SHIPTOSTREET
    Dim SHIPTOSTREET2
    Dim SHIPTOCITY
    Dim SHIPTOSTATE
    Dim SHIPTOCOUNTRYNAME
    Dim SHIPTOZIP
    DiM FIRSTNAME
    Dim LASTNAME
    Dim TRANSACTIONID
    Dim SHIPTOPHONENUM
    Dim NewCustomerCode
    Dim stateID
    dim stateIDCode
    Dim AMT
    Dim ORDERTIME
    Dim shiptoFirstName 
    Dim shipToLastName

    NewCustomerCode = ""
    EMAIL           = SQLescape(request("EMAIL"))
    PAYERSTATUS     = SQLescape(request("PAYERSTATUS"))
    SHIPTOCOUNTRYCODE = SQLescape(request("SHIPTOCOUNTRYCODE"))
    SHIPTONAME      = SQLescape(request("SHIPTONAME"))
    SHIPTOSTREET    = SQLescape(request("SHIPTOSTREET"))
    SHIPTOSTREET2   = SQLescape(request("SHIPTOSTREET2"))
    SHIPTOCITY      = SQLescape(request("SHIPTOCITY"))
    SHIPTOSTATE     = SQLescape(request("SHIPTOSTATE"))
    SHIPTOCOUNTRYNAME= SQLescape(request("SHIPTOCOUNTRYNAME"))
    SHIPTOZIP       = SQLescape(request("SHIPTOZIP"))
    FIRSTNAME       = SQLescape(request("FIRSTNAME"))
    LASTNAME        = SQLescape(request("LASTNAME"))
    TRANSACTIONID   = SQLescape(request("TRANSACTIONID"))
    SHIPTOPHONENUM  = SQLescape(request("SHIPTOPHONENUM"))
    AMT             = SQLescape(request("AMT"))
    ORDERTIME       = SQLescape(request("ORDERTIME"))

    if instr(SHIPTONAME, " ")>0 then 
        shiptoFirstName = left(SHIPTONAME, instr(SHIPTONAME, " "))
        shipToLastName  = right(SHIPTONAME, len(SHIPTONAME) - instr(SHIPTONAME, " "))
    else
        shiptoFirstName = SHIPTONAME
    end if

    if instr(ORDERTIME, "T")>0 then
        ORDERTIME  = replace(replace(ORDERTIME, "T", " "), "Z","")
    end if


    stateID = GetStateIdByStateCount(SHIPTOSTATE, SHIPTOCOUNTRYCODE, SHIPTOCOUNTRYNAME)

    set rs = conn.execute("Select state_code from tb_state_shipping where state_serial_no = '"& stateID &"'")
    if not rs.eof then
        stateIDCode = rs("state_code")
    end if
    rs.close 

    conn.execute("insert into tb_order_paypal_record "&_
	                                " (transaction, regdate) "&_
                                    " values"&_
                                    " ('"&TRANSACTIONID&"','"&ORDERTIME&"')")

    conn.execute("insert into tb_order_pay_record "&_
	            "( pay_regdate, pay_cash, regdate, balance)"&_
	            "values"&_
	            "( '"+ ORDERTIME +"', '"+ AMT +"', now(), 999999)")


    set rs = conn.execute("Select * from tb_customer where customer_login_name='" & EMAIL & "' or customer_email1='" & EMAIL & "'")
    response.Write("Select * from tb_customer where customer_login_name='" & EMAIL & "' or customer_email1='" & EMAIL & "'")
    if not rs.eof then
          NewCustomerCode = rs("customer_serial_no")
          
    else
        set codeRs = conn.execute("select * from tb_store_customer_code limit 0,1")
        if not codeRs.eof then
            NewCustomerCode = codeRs("customercode")


            conn.execute("Delete from tb_store_customer_code where customercode='"& NewCustomerCode &"'")
            
            conn.execute("Insert into tb_customer(customer_serial_no"&_
                        ", customer_login_name"&_
                        ", phone_d"&_
                        ", customer_email1"&_
                        ", customer_first_name"&_
                        ", customer_last_name"&_
                        ", state_code"&_
                        ", state_serial_no"&_
                        ", pay_method"&_
                        ", shipping_state_code"&_
                        ", shipping_country_code"&_
                        ", customer_shipping_first_name"&_
                        ", customer_shipping_last_name"&_
                        ", customer_shipping_state"&_
                        ", customer_shipping_city"&_
                        ", customer_shipping_address"&_
                        ", customer_shipping_zip_code"&_
                        ", customer_note"&_
                        ", zip_code"&_
                        ", create_datetime "&_
                        ", customer_country_code "&_
                        ", customer_card_country_code "&_
                        ", customer_card_state_code "&_
                        ", customer_card_state "&_
                        " "&_
                        " ) values ("&_
                        " '"& NewCustomerCode &"' "&_
                        ", '"& EMAIL &"'"&_
                        ", '"& SHIPTOPHONENUM &"'"&_
                        ", '"& EMAIL &"'"&_
                        ", '"& FIRSTNAME &"'"&_
                        ", '"& LASTNAME &"'"&_
                        ", '"& stateIDCode &"'"&_
                        ", '"& stateID &"'"&_
                        ", '15'"&_
                        ", '"& stateIDCode &"'"&_
                        ", '"& SHIPTOCOUNTRYCODE &"'"&_
                        ", '"& shiptoFirstName &"'"&_
                        ", '"& shipToLastName &"'"&_
                        ", '"& stateID &"'"&_ 
                        ", '"& SHIPTOCITY &"'"&_
                        ", '"& SHIPTOSTREET & " " & SHIPTOSTREET2 &"'"&_
                        ", '"& SHIPTOZIP &"'"&_
                        ", '"& NOTE &"'"&_
                        ", '"& SHIPTOZIP &"'"&_
                        ", now()"&_
                        ", '"& SHIPTOCOUNTRYCODE &"'"&_
                        ", '"& SHIPTOCOUNTRYCODE &"'"&_
                        ", '"& stateIDCode &"'"&_
                        ", '"& stateID &"')")

                       

        end if
        codeRs.close : set codeRs = nothing
    end if
    rs.close 

    


    closeconn()
 %>
 <script type="text/javascript">
     function newOrder(customerID) {
         if (!confirm("Are you create new order?"))
             return;
         $.ajax({
             url: "/q_admin/orders_cmd.aspx"
        , data: "cmd=createNewByCustomerID&customerID=" + customerID + "&TRANSACTIONID=<%= TRANSACTIONID %>"
        , type: "get"
        , success: function (msg) {
            //alert(msg.substr(0,6));
            if (msg.indexOf("OK") > 0)
                window.location.href = '/q_admin/orders_edit_detail_selected.aspx?order_code=' + msg.substr(0, 6);
            ///q_admin/orders_edit_detail.aspx?order_code=" + OrderCode.ToString() + "&order_source=" + this.ddl_order_source.SelectedValue.ToString() +
            else
                alert(msg);
        }
        , error: function (msg) { alert(msg); }
         });
     }
     if ("" != "<%= NewCustomerCode %>")
         newOrder('<%= NewCustomerCode %>')
     else
         alert("error, new customer create faild.");
</script>
</body>
</html>
