<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="q_admin/ebayMaster/ebay_inc.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<%= LAYOUT_CSS_FILES_BACK 	%>
<%= LAYOUT_SCRIPT_FILES 	%>
<%= LAYOUT_LINK_FILES 		%>
<title>part keywords</title>
</head>

<body>
<%
	Dim id				:	id			=	SQLescape(Request.Form("id"))
	Dim keywords		:	keywords	=	SQLescape(request.Form("keyword"))
	Dim for_v           :   for_v       =   SQLescape(request.form("for"))
	if len(id)>0 then
		Conn.execute("Update tb_product set keywords="& SQLquote(keywords) &", last_regdate=now(),is_modify=1 Where product_serial_no="& SQLquote(id))
		if(for_v = "0")then
			Response.write "<script >alert('it is save');</script>"
		End if
	End if

CloseConn()

Session("for") = for_v
%>

<script type="text/javascript">
    $().ready(function(){
        if( '<%=for_v %>' == '1')
        {
            parent.forward();
            //$('input[name=for]').get(2).attr('checked', 'true');
        }
        else if('<%= for_v %>' == '2')
        {
            //$('input[name=for]').get(2).attr('checked', 'true');
            parent.backward();
        }
    });
</script>
</body>
</html>
