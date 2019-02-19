<%

function getHTTPPage(url) 
	on error resume next 
	dim http 
	set http=Server.createobject("Microsoft.XMLHTTP") 
	Http.open "GET",url,false 
	Http.send() 
	if Http.readystate<>4 then
		exit function 
	end if 
	getHTTPPage=BytesToBstr(Http.responseBody)
	set http=nothing
	if err.number<>0 then err.Clear  
end function 

Function BytesToBstr(body)
 dim objstream
 set objstream = Server.CreateObject("adodb.stream")
 objstream.Type = 1
 objstream.Mode =3
 objstream.Open
 objstream.Write body
 objstream.Position = 0
 objstream.Type = 2
 objstream.Charset = "utf-8"
 BytesToBstr = objstream.ReadText 
 objstream.Close
 set objstream = nothing
End Function

function toUrl(filename)
	dim http_url 
	http_url = "http://"& request.ServerVariables("SERVER_NAME")&request.ServerVariables("url")
	
	dim arr 
	arr = split (http_url,"/")
	'response.write 
	http_url = replace(http_url, arr(ubound(arr)),"")
	toUrl = http_url & filename

end function

%>