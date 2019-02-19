<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
</head>
<style>
form { margin: 0px;}
</style>
<script>
function CurrentPage(page)
{
	window.location.href= "right_manage.asp?page="+ page +"&old_page=" + page;
}
function DefaultPage(page)
{
	window.location.href= "right_manage.asp?page=default&old_page="+page;
}
</script>
<body>
<!--#include file="public_helper/public_helper.asp"-->
<%
	dim page, old_page
	page = encode.StringRequest("page")
	
	if(page <> "default") then 
		old_page = page
	else
		old_page = encode.StringRequest("old_page")
	end if

%>
<input type="button" value="Current Page" onclick="CurrentPage('<%= old_page %>');" />
<input type="button" value="Default" onclick="DefaultPage('<%= old_page %>');" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input type="button" value="Save" onclick="document.getElementById('form1').submit();"/><br />
<form action="right_manage_exec.asp" method="post" name="form1" id="form1">
<input type="hidden" name="page" value="<%= page %>" />
<input type="hidden" name="old_page" value="<%= old_page %>" />
<hr size="1" />
<%= page %>
<hr size=1 />
<%
	dim content
	content = ""
	set rs = conn.execute("select right_content from tb_right where right_page='"& page &"'")
	if not rs.eof then 
		content = rs(0)
	end if
	rs.close : set rs = nothing
%>
<textarea rows="8" cols="50" name="right_body" id="right_body" class="areatext" style="display:none;"><%= content %></textarea>
			<iframe id="edit1" src="luadmin/ewebeditor/ewebeditor.asp?id=right_body&style=standard" frameborder="0" scrolling="no" width="99%" height="500"></iframe>
			</form>
<hr size="1" />
<input type="button" onclick="window.close();" value="Close Window" />
<%
	closeconn()
%>
</body>
</html>
