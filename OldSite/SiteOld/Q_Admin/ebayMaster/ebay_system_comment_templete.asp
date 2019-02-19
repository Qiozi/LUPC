<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ebay system comment templete</title>
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
	Dim 	is_case 	:	is_case 	= 	null
	Dim 	is_cpu 		:	is_cpu 	= 	null
	Dim 	is_lcd 		:	is_lcd 	= 	null
	Set rs = conn.execute("Select * from tb_ebay_system_part_comment order by priority asc")
	If not rs.eof then
			
			Response.write "<table cellspacing='0' cellpadding='2'>"&vblf
			Response.write "	<tr>"&vblf
			Response.write "		<th>排序</th>"&vblf
			Response.write "		<th>目录描述</th>"&vblf
			Response.write "		<th>Category Group</th>"&vblf
			Response.write "		<th>最多数量</th>"&vblf
			Response.write "		<th>附加价格</th>"&vblf
			Response.write "		<th></th>"&vblf
			Response.write "	</tr>"&vblf
			Do while not rs.eof
				if rs("is_case") = 1 then is_case = " checked='true' " else is_case = ""
				if rs("is_cpu")  = 1 then is_cpu = " checked='true' " else is_cpu = ""
				if rs("is_lcd")  = 1 then is_lcd = " checked='true' " else is_lcd = ""
			Response.write 	"	<tr>"&vblf
			Response.Write	"		<td>"&vblf
			Response.write 	"			<input type=hidden name='id'  value='"& rs("id") &"' />"&vblf
			Response.write 	"			<input type=text name='priority' size='2' maxlength='2' value='"& rs("priority") &"' />"&vblf
			Response.write 	"		</td>"&vblf
			Response.Write	"		<td>"&vblf
			REsponse.write 	"			<input type='text' name='comment' maxlength='20' value='"& rs("comment") &"' />"&vblf
			Response.write 	"		</td>"&vblf
			Response.Write	"		<td>"&vblf
			Response.write 	"			<input type='text' name='category_ids' maxlength='30' size='30' value='"& replace(rs("category_ids"), ",", "|") &"' />"&vblf
			Response.write 	"		</td>"&vblf
			Response.Write	"		<td>"&vblf
			Response.write 	"			<input type=text name='max_quantity' size='2' maxlength='2' value='"& rs("max_quantity") &"' />"&vblf
			Response.write 	"		</td>"&vblf
			Response.Write	"		<td>"&vblf
			Response.write 	"			<input type=text name='append_charge' size='5' maxlength='5' value='"& rs("append_charge") &"' style='text-align:right;' />"&vblf
			Response.write 	"		</td>"&vblf
			Response.Write	"		<td>"&vblf
			Response.write 	"			<input type='checkbox' name='is_case' value='"& rs("id") &"' "& is_case &">is Case"&vblf
			Response.write 	"			<input type='checkbox' name='is_cpu' value='"& rs("id") &"' "& is_cpu &">is CPU"&vblf
			Response.write 	"			<input type='checkbox' name='is_lcd' value='"& rs("id") &"' "& is_lcd &">is LCD"&vblf
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
				$(this).find("td").css("background","#ffffff").css("font-size","8pt");
	
			}
			else
			{
				$(this).find("td").css("background","#eaeaea").css("font-size","8pt");
			}
		}).hover(function(){$(this).find("td, input").css("color","green");}, function(){$(this).find("td, input").css("color","#000000");});
	});
</script>
</body>
</html>
