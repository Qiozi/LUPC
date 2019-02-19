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
<script type="text/javascript">
    function deleteKey(id, pid) {
        $('#iframe1').attr('src', 'product_helper_part_keywords_word_cmd.asp?cmd=delete&id=' + id  + "&pid="+ pid);
    }
    function formSubmit(e) {
       
    }
</script>
</head>

<body>
	<% 
        dim id
        id      =   SQLescape(request("id"))
        set rs = conn.execute("select keyword from tb_product_category_keyword_sub where id='"& id &"'")
        if not rs.eof then
            Response.write "<br><h2><b>"& rs(0) &"</b></h2><hr size=1>"
        end if
        rs.close : set rs = nothing
    %>
    <form action="product_helper_part_keywords_word_cmd.asp?cmd=insert" target="iframe1" method ="post">
        <input type=hidden name="pid" value="<%= id %>" />
        Keyword: <input type="text" name="key" value="" /><input type="submit" value="Submit" />
    </form>
    <hr size="1" />
    <%
        set rs = conn.execute("select ID, Analyse_word  from tb_part_keyword_analyse_word where ParentID='"& id &"'")
        Response.write "<table cellpadding='5' cellspacing='0'>"
        if not rs.eof then
            do while not rs.eof 
                Response.write "<tr title='row'>"&vblf
                Response.write "    <td width='300'>"&vblf
                Response.write rs("analyse_word")
                Response.Write "    </td>" &vblf
                Response.write "    <td> <a style=""cursor:pointer;"" onclick=""deleteKey('"& rs("id") &"', '"&id&"');"">Delete</a> <td>"&vblf
                Response.write "</tr>"&vblf
            rs.movenext
            loop
        else
            response.write "<tr><td>No Data.</td></tr>"
        end if
        Response.write "</table>"
        rs.close : set rs = nothing
     %>
<iframe id="iframe1" name="iframe1" src="" style="width: 330px; height: 330px; " frameborder="0"></iframe>

<% closeconn() %>

<script type="text/javascript">

    $().ready(function () {
        $('tr[title=row]').hover(
	 		function () {
	 		    $(this).find("td").css("border-bottom", "1px solid #999999").css("padding-bottom", "0px");
	 		}
	  	  , function () {
	  	      $(this).find("td").css("border-bottom", "0px solid #000000").css("padding-bottom", "1px");
	  	  }

	  ).each(function (i) {
	      if (i % 2 == 1) {
	          $(this).find("td").css("background", "#ffffff").css("font-size", "8pt").css("padding-bottom", "1px");

	      } else {
	          $(this).find("td").css("background", "#f2f2f2").css("font-size", "8pt").css("padding-bottom", "1px");
	      }
	  });
    });

 </script>
</body>
</html>
