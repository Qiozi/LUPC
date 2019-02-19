<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

	Dim ids 			:		ids 			= 	SQLescape(request("id"))		
	Dim prioritys		:		prioritys		=	SQLescape(request("priority"))	
	dim comments		:		comments		= 	SQLescape(request("comment"))	
	Dim	category_ids	:		category_ids	=	SQLescape(request("category_ids"))	
	Dim max_quantitys	:		max_quantitys	=	SQLescape(request("max_quantity"))		
	Dim append_charges	:		append_charges	=	SQLescape(request("append_charge"))	
	Dim is_case			:		is_case			=	SQLescape(request("is_case"))	
	Dim is_cpu			:		is_cpu			=	SQLescape(request("is_cpu"))	
	Dim is_lcd			:		is_lcd 			=	SQLescape(request("is_lcd"))
	
	
	Dim is_lcd_g		:		is_lcd_g 		=	null
	Dim is_cpu_g		:		is_cpu_g		=	null
	Dim is_case_g		:		is_case_g		=	null
	DIM append_charges_g:		append_charges_g=	null
	Dim max_quantitys_g	:		max_quantitys_g	=	null
	Dim category_ids_g	:		category_ids_g 	= 	null
	Dim comments_g		:		comments_g		= 	null
	Dim priority_g		:		priority_g 		= 	null
	Dim ids_g			:		ids_g 			= 	null
	
	if len(ids)>0 then
		ids_g 		= 	split(ids, ",")
		priority_g			= 	split(prioritys, ",")
		comments_g			= 	split(comments, ",")
		category_ids_g		= 	split(category_ids, ",")
		max_quantitys_g		= 	split(max_quantitys, ",")
		append_charges_g	= 	split(append_charges, ",")		
		is_case_g			= 	split(is_case, ",")
		is_cpu_g			= 	split(is_cpu, ",")
		is_lcd_g			= 	split(is_lcd, ",")	
'		REsponse.write ubound(ids_g)&"<br>"
'		REsponse.write ubound(priority_g)&"<br>"
'		REsponse.write ubound(comments_g)&"<br>"
'		REsponse.write ubound(category_ids_g)&"<br>"
'		REsponse.write ubound(max_quantitys_g)&"<br>"
'		REsponse.write ubound(append_charges_g)&"<br>"
'		REsponse.write (is_case)&"<br>"
'		REsponse.write ubound(is_cpu_g)&"<br>"
'		REsponse.write (is_lcd)&"<br>"

		for i = lbound(ids_g) to ubound(ids_g)
		
			is_case = 0
			is_cpu 	= 0
			is_lcd	= 0
			for j = lbound(is_case_g) to ubound(is_case_g)
				if trim(is_case_g(j)) = trim(ids_g(i)) then
					is_case = 1				
				end if
			next
			for j = lbound(is_cpu_g) to ubound(is_cpu_g)
				if trim(is_cpu_g(j)) = trim(ids_g(i)) then
					is_cpu = 1				
				end if
			next
			for j = lbound(is_lcd_g) to ubound(is_lcd_g)
				if trim(is_lcd_g(j)) = trim(ids_g(i)) then
					is_lcd = 1				
				end if
			next
						
			conn.execute("Update tb_ebay_system_part_comment Set "&_
						" priority = "&	SQLquote(trim(priority_g(i))) &_
						" ,comment = "&	SQLquote(trim(comments_g(i))) &_
						" ,category_ids = "&	SQLquote(trim(category_ids_g(i))) &_
						" ,max_quantity = "&	SQLquote(trim(max_quantitys_g(i))) &_
						" ,append_charge = "&	SQLquote(trim(append_charges_g(i))) &_
						" ,is_case = "&	SQLquote(is_case) &_
						" ,is_cpu = "&	SQLquote(is_cpu) &_
						" ,is_lcd = "&	SQLquote(is_lcd) &_
						" where id = "& SQLquote(trim(ids_g(i))))
		next
		Response.write "<script>alert('it is ok');window.location.href='ebay_system_comment_templete.asp';</script>"
	else
		Response.write "Params is Error."
	end if


CloseConn()
%>
<body>
</body>
</html>
