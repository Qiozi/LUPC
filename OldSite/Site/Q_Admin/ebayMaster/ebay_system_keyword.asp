<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Ebay Keyword</title>
<script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
</head>
<style>
body{font-size:8pt;margin:0px;}
input{ font-size: 8pt;}
</style>
<body>
<!--#include virtual="site/inc/inc_helper.asp"-->
<form action="ebay_system_comment_templete_exec.asp" method="post" name="form1">
<%
	response.End()
	Dim 	is_case 	:	is_case 	= 	null
	Dim 	is_cpu 		:	is_cpu 	= 	null
	Dim 	is_lcd 		:	is_lcd 	= 	null
	Set rs = conn.execute("Select * from tb_product_category_keyword where is_ebay=1 order by priority asc")
	If not rs.eof then
			
			Response.write "<table cellspacing='0' cellpadding='2' width='100%'>"&vblf
'			Response.write "	<tr>"&vblf
'			Response.write "		<th></th>"&vblf
'			Response.write "		<th></th>"&vblf
'			
'			Response.write "	</tr>"&vblf
			Do while not rs.eof
				
			Response.write 	"	<tr>"&vblf
			Response.Write	"		<td width='120'>"&vblf
			Response.write 	"			<input type=hidden name='id'  value='"& rs("id") &"' />"&vblf
			Response.write 	"			<input type=text name='keyword'  maxlength='20' value='"& rs("keyword") &"' />"&vblf
			Response.write 	"		</td>"&vblf
			Response.Write	"		<td>"&vblf
			
			Set crs = conn.execute("select * from tb_product_category_keyword_sub where parent_id='"& rs("id")&"'")
			if not crs.eof then
					Do while not crs.eof 
						Response.Write	"			<input type='text' name='sub_keyword' value='"&crs("keyword")&"' />	<br/>"&vblf
					crs.movenext
					loop
			end if
			crs.close : set crs = nothing
			
			Response.write 	"		</td>"&vblf
			Response.Write 	"	</tr>"&vblf
			rs.movenext
			loop
			Response.write "</table>"&vblf
	End if
	rs.close : set rs = nothing
	

%>
	<hr size="1" />
    <center>
    	<input type="submit" value="submit"  />
    </center>
</form>
<i>* Category Group ,输入目录ID， 多个请用 <b>"|"</b> 进行分割。<br/> *  附加价格主要用于对所有的 LCD 价格进行添加</i>
<% closeConn() %>
<script type="text/javascript">
	$().ready(function(){
		
		$('tr').each(function(i){
			if(i%2 == 0)
			{
				$(this).find("td").css("background","#ffffff").css("font-size","8pt").css("padding", "5px");
	
			}
			else
			{
				$(this).find("td").css("background","#eaeaea").css("font-size","8pt").css("padding", "5px");
			}
		}).hover(function(){$(this).find("td, input").css("color","green");}, function(){$(this).find("td, input").css("color","#000000");});
	});
</script>
</body>
</html>
