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
    
    cmd     =   SQLescape(request("cmd"))

    if cmd = "getAll" then
        set rs = conn.execute("Select * from tb_ebay_system_part_name_keyword order by keyword asc ")
        if not rs.eof then
            do while not rs.eof 
                Response.write "<div class='part_name' id='"& rs("id") &"' onmouseover=""this.bgColor='red';"">"
                Response.write "        <ul class='ul_parent'>"
                Response.write "            <li>    "
                Response.write                      rs("keyword") 
                Response.write "                    <span style='border:0px solid #ff9900;'>"&_
                               "                        <img src='/soft_img/tags/(02,41).png' onclick=""dekPartNameKeyword('"& rs("id") &"');"" style='cursor: pointer;'>"
                response.write "                    </span> "&_
                               "            </li>"&_
                               "      </ul>"&_
                               "</div>"
                            
            rs.movenext
            loop        
        end if
        rs.close :set rs = nothing 
    
    end if
 %>
 </div>
</body>
</html>
