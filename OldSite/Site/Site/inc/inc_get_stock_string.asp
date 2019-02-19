<!--#include virtual="/site/inc/inc_func_sys.asp"-->
<!--#include virtual="/site/inc/inc_escape.asp"-->
<%
Dim lu_sku
Dim stock
lu_sku  =  SQLescape(request("lu_sku"))
stock   =  SQLescape(request("stock"))

response.Write FindPartStoreStatus3(stock) 
%>