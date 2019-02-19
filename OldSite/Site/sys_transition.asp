<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
</head>

<body>
<!--#include virtual="/site/inc/inc_layout.asp"-->
<!--#include virtual="/site/inc/inc_layout_params.asp"-->
<!--#include virtual="/site/inc/inc_func_sys.asp"-->
<!--#include virtual="/site/inc/inc_escape.asp"-->
<!--#include virtual="/site/inc/system_transition_inc.asp"-->
	<%
		Dim country_code 	: 	country_code 	=	SQLescape(request("country"))
		
		if "US" =  ucase(country_code) then			
			Session("system_transition") = CSUS
        	Response.cookies("system_transition") = CSUS
			Response.cookies("system_transition").expires = dateadd("d",365,now())
		else
			Session("system_transition") = CSCA
        	Response.cookies("system_transition") = CSCA
			Response.cookies("system_transition").expires = dateadd("d",365,now())
		end if
		
		Dim returnURl 		:		returnURl = request("returnURl")
		

		if returnURL = "" then
			if CurrentIsEbay then 
				Response.Redirect("/ebay/") 
			else
				Response.Redirect(LAYOUT_HOST_URL) 
			end if
		else
			Response.Redirect(returnURl)
		end if
	%>
</body>
</html>