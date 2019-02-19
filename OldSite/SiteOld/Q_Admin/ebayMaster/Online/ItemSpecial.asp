<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml"><head>
    <title>Item Specifics </title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
</head>

<body>
<%

Dim XmlPath : XmlPath = "/soft_img/eBayXml/categoryAttribute/"
Dim xmlStoreFile    :   xmlStoreFile    =   SQLEscape(request("xmlStoreFile")) 


if(xmlStoreFile<>"")then
    xmlStoreFile    =   XmlPath &  xmlStoreFile &"_ebay.xml"    
else
    xmlStoreFile    =   XmlPath  &"1858_ebay.xml"  
end if
set xmldoc = server.createobject("microsoft.xmldom")
set xsldoc = server.createobject("microsoft.xmldom")

xmldoc.async = false
xsldoc.async = false

if not xmldoc.load(server.mappath(xmlStoreFile)) then 
    response.write "Failed to load the xml file"
    Response.end
    
end if

if not xsldoc.load(server.mappath(XmlPath & "syi_attributes.xsl")) then 
    response.write " Failed to load the xsl file"
    response.end
end if

Response.contenttype = "text/html"
response.write xmldoc.transformnode(xsldoc)

set xmldoc = nothing
set xsldoc = nothing


if(request("IsItemSpecificsEnabled") = "1")then
    Response.write "<Hr size='1'>"&vblf
    Response.Write "<table><tr>"&vblf
    Response.Write "<td>Item Specifics Label:</td><td><input type='text' id='item_specifics_label'></td>"
    Response.Write "</tr><tr><td>Item Specifics :</td><td> <input type='text' id='item_specifics'></td>"
    Response.Write "</tr></table>"

end if

closeconn()
 %>
 <hr size=1 />
 <button onclick="submit();">Get Item List</button><button onclick="window.history.go(-1);">Go Back</button>
 <script type="text/javascript">
 	$().ready(function(){
    	$("#APIForm").attr("action", "?");
    	ClearSelectedEvent();
	});
	function submit()
	{
	    var item_specifics_str = "";
	    if('1'=='<%= request("IsItemSpecificsEnabled") %>')
	        item_specifics_str = "&item_specifics_label="+ $('#item_specifics_label').val() +"&item_specifics="+ $('#item_specifics').val();
		//alert(item_specifics_str);
		$("#APIForm").attr("action", "/q_admin/ebayMaster/Online/itemSpecial_next.asp?cid=<%= request("cid")%>&system_sku=<%= request("system_sku") %>"+ item_specifics_str);
		$("#APIForm").submit();
	}
	
	function ClearSelectedEvent(){
	    $('select').each(function(){
	        $(this).attr("onChange", "selectedChange($(this));");
	        $(this).parent().append("<input type='text' name='"+ $(this).attr("name") + "_text' style='display: none;'/>");
	    });
	}
	
	function selectedChange(the){
	    var new_element_name = the.attr("name") + "_text";

	    if(the.val() == "-6")
	    {
	        $('input').each(function(){ if($(this).attr("name") == new_element_name) $(this).css("display", "") });
	    }
	    else
	    {
	       $('input').each(function(){ if($(this).attr("name") == new_element_name) $(this).css("display", "none") });
	    }
	}
 </script>
</body>
</html>
