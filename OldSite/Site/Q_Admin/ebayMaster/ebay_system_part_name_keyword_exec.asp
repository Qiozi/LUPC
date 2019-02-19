<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html>
<head>
    <title>Untitled Page</title>
</head>
<body>
<div id="list">
<%
    Dim cmd
    Dim keyword_id
    Dim keyword
    cmd     =   SQLescape(request("cmd"))
    keyword =   SQLescape(request("keyword"))
    keyword_id =   SQLescape(request("keyword_id"))
    
    if cmd = "Add" then
        conn.execute("Insert into tb_ebay_system_part_name_keyword(keyword) values ('"& keyword &"')")
        response.write (" Add Keyword is success.")
    
    end if
    
    if cmd = "del" then
        conn.execute("Delete from tb_ebay_system_part_name_keyword where id='"& keyword_id &"'")
        response.write (" Deleted is OK")
    end if
 %>
 </div>
</body>
</html>
