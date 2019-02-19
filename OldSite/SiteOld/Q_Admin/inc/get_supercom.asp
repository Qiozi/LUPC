<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<%
dim strRequestXML
dim myUserName
dim myPassord
dim getResponseXML

myUserName = "TL054900"
myPassord = "prx914ru"

'Build the XML
strRequestXML= "<?xml version=""1.0"" encoding=""utf-8""?><PNARequest><Version>3.0</Version><Header><UserName>" & myUserName & "</UserName><Password>" & myPassord & "</Password></Header><Detail><PartNumber>"& request("mfp") &"</PartNumber></Detail></PNARequest>"

dim strUrl 
strUrl = "http://xml3.supercom.ca/XMLWebService3.asmx/XMLCall"

Set xmlHttp = Server.CreateObject("MSXML2.ServerXMLHTTP") 
xmlHttp.Open "POST", strUrl, False 

xmlHttp.setRequestHeader "content-type","application/x-www-form-urlencoded" 

'Assign the parameter str with our XML
strRequestXML = "str=" & strRequestXML

'Send via Post the HTTP Request
xmlHttp.Send(strRequestXML)

'output
getResponseXML = xmlHttp.responseText 

'clean-up
xmlHttp.abort()
set xmlHttp = Nothing

Dim price 			: 	price 		= 	0
Dim Available 		:	Available	=	"N/A"
Dim toronto			:	toronto		=	"N/A"

getResponseXML = lcase(getResponseXML)

if instr(getResponseXML, "<price>")>0 then
	price = mid( getResponseXML, instr(getResponseXML, "<price>")+ len("<price>"),  instr(getResponseXML, "</price>") - instr(getResponseXML, "<price>")-len("</price>") )
End if
if instr(getResponseXML, "<stock id=""toronto"">" )>0 then 

	toronto = mid( getResponseXML, instr(getResponseXML, "<stock id=""toronto"">"),  instr(getResponseXML, "</stock>") - instr(getResponseXML, "<stock id=""toronto"">"))
	
	if instr(toronto, "<available>")>0 then
		Available = mid( toronto, instr(toronto, "<available>") + len("<available>"),  instr(toronto, "</available>") - instr(toronto, "<available>")- len("<available>") )
	End if
end if
'response.write price & "|"& available

if price <> 0 and instr(toronto, "toronto" )>0 then 
%>
<td style='text-align:right'>
    <span style='color:#993300'>Supercom</span>
</td>
<td style="width:50px ; text-align:center">

    <% if isnumeric( available ) then
			if cint(available)>0 then
				response.write "<span style='color:green;'>"& available &"</span>"
			else
				response.write available
			end if
		else
			response.write available
		end if
	%>
</td>
<td style="width:60px;text-align:right">
    $<%= price %>
</td>
<td style="width: 150px;text-align:right">

   <span style='color:#CCC'> <%= now() %></span>
</td>
<% else %>
<td colspan="4"></td>
<% end if %>
</body>
</html>
