<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>

<%
dim id 
id=request("id")
set fso=Server.CreateObject ("Scripting.FileSystemObject")
fpath=server.MapPath ("/flash_view/"&id&".swf")
if fso.FileExists(fpath) then
%>
$('#flash_view_button').html('<div><img src=\"/soft_img/app/flash_view.gif\" style=\"cursor:pointer\" border=\"0\" onClick=\"js_callpage_cus(\'<%="/site/view_flash.asp?filename=/flash_view/"&id&".swf"%>\', \'view_flash\', 465, 465, 300, 400)"></div>');
<%
'response.write "/view_flash.asp?filename="&id&".swf"
end if

%>