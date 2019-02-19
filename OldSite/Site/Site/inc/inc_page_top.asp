<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<% Option Explicit %>
<% 
'if instr(1, Request.ServerVariables("PATH_TRANSLATED"), "product_parts_detail.asp")>0 then 
'    response.redirect("http://ca.lucomputers.com/detail_part.aspx?sku=" & request("id"))
'    response.End()
'end if

'response.redirect("http://ca.lucomputers.com/")



%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
http://www.lucomputers.com/site/product_parts_detail.asp?id=30258&cid=350
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="site/inc/inc_layout.asp"-->
<%
        Call FindPageTopHtml()
        Response.Write  FindPageContTopHTML()
%>

<%'= CurrentIsEbay %>

