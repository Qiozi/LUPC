<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Untitled Page</title>
</head>
<body>
<!--#include virtual="/site/inc/inc_helper.asp"-->
<%
    dim error_type, content, email_address,category_id, luc_sku
	dim run_time
    category_id		= request.form("parent_id")
	if category_id = "" then  category_id = 0
    luc_sku 		= request.form("id")    
    run_time 		= request.form("run_time")
    error_type 		= request.form("page_error_type")
    content 		= request.form("content")
    email_address 	= request.form("email_address")
    


    if not IsEmail(email_address) then
        response.write "<script>alert('The address is not in the correct e-mail format.');</script>"
        closeconn()
        response.end
    end if

    if cstr(error_type) = "1" then
                error_type = "The information above is incorrect or conflicting."
    end if
    
    if cstr(error_type) = "2" then
                error_type = "This page has misspellings and/or bad grammar."
    end if
    
    if cstr(error_type) = "3" then
                error_type = "This page did not load currectly on my browser or generated an error."
    end if
    
    if cstr(error_type) = "4" then
                error_type = "The rebate information is incorrect."
    end if
    
    dim category_name, luc_name
    if category_id <> "" then
        set rs = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no='"& category_id &"'")
        if not rs.eof then
            category_name = rs(0)
        end if
        rs.close : set rs = nothing    
    end if
    if luc_sku <> "" then
        set rs = conn.execute("select case when product_name_long_en='' then  product_name else product_name_long_en end as product_name from tb_product where product_serial_no='"&luc_sku&"'")
        if not rs.eof then
            luc_name = rs(0)
        end if
        rs.close : set rs = nothing
    end if

    if(cstr(run_time) = cstr(session("Page_error_info_exec_datetime"))) then
        set rs = server.createobject("adodb.recordset")
        rs.open "select * from tb_page_error_info", conn,1,3
        rs.addnew
			rs("error_type") = error_type
			rs("email_address") = email_address
			rs("content") = content
			rs("category_id") =category_id
			'response.write luc_sku
			rs("luc_sku")  = luc_sku
			
        rs.update
        rs.close : set rs = nothing
        session("Page_error_info_exec_datetime")  = ""
        
        'call LUWebSendEmail("sales@lucomputers.com", "809840415@qq.com", "sales@lucomputers.com", "LU COMPUTERS",  "sales@lucomputers.com", "", "LU WEB(report any errors on the page)", true, "Customer: "&email_address&" <br/> category id ("&category_id&"): "& category_name &"<br/> luc sku("&luc_sku&"): "&luc_name&"<br/> error type:" & error_type & "<br/>" &content)
		Dim aq_title			:	aq_title			=	"LU WEB(report any errors on the page:) SKU: " & luc_sku
		Dim body_html			:	body_html			=	"<b style='color:#ff9900;'>Customer: (" & email_address & ")</b><br><b>CategoryID:</b> "&category_id&"<br/><b>SKU:</b> "&luc_sku&"<br/><b>name:</b>"& luc_name &"<br/><b>error type:</b>" &error_type &"<br/><span style='font-size:8pt'>" & server.HTMLEncode( content)&"</span>"

		call SendEmail(aq_title, body_html , "sales@lucomputers.com")
        
        response.write ("<script >alert('Your message has bee sent successfully.  Thank you..');parent.document.getElementById('error_submit_email_address').value = '';</script>")
    else
      
    end if
    closeconn()
%>

</body>
</html>
