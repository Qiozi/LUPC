<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Change Part Title</title>
</head>

<body>
<form action="#" method="post">
<input type="hidden" name="cmd" value="update" />
old:<input type='text' name="keyword1" value="" /><br />
new:<input type="text" name="keyword2" value="" /><br />
<input type='submit' value="submit" />
</form>
<%
dim cmd 			: 	cmd 		= SQLescape(request("cmd"))
dim keyword1		:	keyword1	= SQLescape(request("keyword1"))
dim keyword2		:	keyword2	= SQLescape(request("keyword2"))

if len(keyword1)<20 and len(keyword2)<20 and cmd = "update" and len(keyword1)>0 then
	
	conn.execute("update tb_product set "&_
	  " product_short_name= replace(product_short_name, """& keyword1 &""","""& keyword2 &""")"&_
	  " , product_ebay_name= replace(product_ebay_name, """& keyword1 &""","""& keyword2 &""")"&_
	  " , product_name= replace(product_name, """& keyword1 &""","""& keyword2 &""") "&_
	  " , product_name_long_en= replace(product_name_long_en, """& keyword1 &""","""& keyword2 &""") "&_
	  " , product_name_long_en= replace(product_name_long_en, """& keyword1 &""","""& keyword2 &""")")

else

	response.Write("long")
end if
response.Write( now())
closeconn()
%>
</body>
</html>
