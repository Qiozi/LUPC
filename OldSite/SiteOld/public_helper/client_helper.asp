<%
class client_helper

	public function getIP
		getIP =  Request.ServerVariables("REMOTE_ADDR")
	end function 
	
	public function getPageName
		getPageName = request.ServerVariables("script_name")
	end function
	
	public function onlyPageName
		dim arrays
		onlyPageName = request.ServerVariables("script_name") 
		if instr(onlyPageName, "/") > 0 then 
			arrays = split(onlyPageName, "/")
			'response.Write instr(onlyPageName, "/") 

			onlyPageName = arrays(ubound(arrays))
		end if
	end function

	public function getUrlParams1
		dim url 
		URL=Request.ServerVariables("HTTP_url")
		URL=replace(URL,"&page="&page,"")
		URL=replace(URL,"&keywords="&request("keywords"),"")
		URL=replace(URL,"?Keywords="&request("keywords"),"")
		if instr(URL,"?")=0 then
			w="?"
		else
			w="&"
		end if
		getUrlParams1 =  url &w
	end function
	
	public function getUrl
		getUrl = "http://"& request.ServerVariables("SERVER_NAME")&request.ServerVariables("url")
		
	end function 
end class

%>