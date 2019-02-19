<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	Dim sku		:		sku			=	SQLescape(request("sku"))
	
	if sku <> "" then 	WriteSystemBigImg(sku)  
   
%>