<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<%
	Dim attr		:	attr	=	""
	for each i in request.form
		Response.write i & "=" & request.Form(i) & "<br>"
		if(instr(i, "attr")>0) and instr(i, "_text")<1 then
			if attr = "" then 
			    if(request.form(i)= "-6") then
			        attr = "t"& getID1(i) &"|"& request.Form(i&"_text")
			    else
				    attr = getID1(i) &"|"& request.Form(i)
				end if
			else
                if( getID1(i) <> "10244" ) then 
			        if(request.form(i)= "-6") then
			            attr = attr & "[Q]" & "t"& getID1(i) &"|"& request.Form(i&"_text")
			        else
				        attr = attr & "[Q]" & getID1(i) &"|"& request.Form(i)
				    end if
                end if
			end if
		end if
	next
	
	response.write attr
	'attr4595_10244
	function getID1(str)
		dim array1, array2
		if(str<>"" and not isnull(str))then
			array1 = split(str, "_")
			if left(array1(1),1) = "t" then 
			    getID1 = "t"&array1(2)
			else
			    getID1 = array1(1)
			end if
		end if
	end function	
closeconn()

%>
/q_admin/ebayMaster/Online/addItem.aspx?system_sku=<%= request("system_sku") %>&=attribates=<%= attr %>&cid=<%= request("cid") %>&vcsid=<%= request("vcsid") %>
<script type="text/javascript">
	window.location.href= "/q_admin/ebayMaster/Online/addItem.aspx?system_sku=<%= request("system_sku") %>&attribates=<%= attr %>&cid=<%= request("cid") %>&vcsid=<%= request("vcsid") %>&item_specifics_label=<%= request("item_specifics_label") %>&item_specifics=<%= request("item_specifics") %>";
</script>
</body>
</html>
