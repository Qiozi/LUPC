<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>

<body>
<%

set xmldoc = server.createobject("microsoft.xmldom")
set xsldoc = server.createobject("microsoft.xmldom")

xmldoc.async = false
xsldoc.async = false

if not xmldoc.load(server.mappath("test.xml")) then 
    response.write "Failed to load the xml file"
    Response.end
    
end if

if not xsldoc.load(server.mappath("syi_attributes.xsl")) then 
    response.write " Failed to load the xsl file"
    response.end
end if

Response.contenttype = "text/html"
response.write xmldoc.transformnode(xsldoc)

set xmldoc = nothing
set xsldoc = nothing
 %>
</body>
</html>
