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
<style>
.s_keyword{cursor:pointer;}
.s_keyword span:hover{ color:#ff6600;}
</style>

</head>

<body>
	
<% 
    dim id
    dim key
    dim cmd
    dim pid

    pid     =       SQLescape(request("pid"))
    cmd     =       SQLescape(request("cmd"))
    key     =       SQLescape(request("key"))
    id      =       SQLescape(request("id"))

    if cmd = "delete" then
        conn.execute("delete from tb_part_keyword_analyse_word where id='"&id&"'")
        Response.Clear()
        response.Write "<script>parent.location.href=""product_helper_part_keywords_word.asp?id="&pid&""";</script>"
        response.End()
    end if

    if cmd = "insert" then
    Response.write ("insert into tb_part_keyword_analyse_word 	(ParentID, Analyse_word) "&_
                     " values "&_
                     " ( '"& pid &"', '"& key &"')")
        conn.execute("insert into tb_part_keyword_analyse_word 	(ParentID, Analyse_word) "&_
                     " values "&_
                     " ( '"& pid &"', '"& key &"')")
        Response.Clear()
        response.Write "<script>parent.location.href=""product_helper_part_keywords_word.asp?id="&pid&""";</script>"
        response.End()
    end if

closeconn() %>
</body>
</html>
