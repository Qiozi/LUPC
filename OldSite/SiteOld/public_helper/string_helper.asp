<%
' //  //  //  //  //  //  //  //  //  //  //  
'		过滤单引号
'		by : 	Qiozi@msn.com
'//  //  //   //   //   ///   ///  //   //  //

class string_helper
	public function StringRequest(str)
		dim ReRequest
		ReRequest = ""
		if(str <> "")then
			ReRequest = Replace(trim(Request(str)), "'", "''")
		end if
		StringRequest = ReRequest
	end function

	public function IntRequest(str)
		IntRequest = 0
		if(instr(str,"'") > 0)then
			IntRequest = clng(Replace(trim(Request(str)), "'", ""))
		else
			IntRequest = trim(trim(Request(str)))
		end if
	end function
	
	public function StringRequest2(str)
		dim ReRequest
		ReRequest = Request(str)
		if(ReRequest <> "")then
			ReRequest=replace(ReRequest,"'","''")
			ReRequest=replace(ReRequest,"select","")
			ReRequest=replace(ReRequest,"insert","")
			ReRequest=replace(ReRequest,"delete","")
			ReRequest=replace(ReRequest,"update","")
			ReRequest=replace(ReRequest,"drop","")
			ReRequest=replace(ReRequest,"--","")
		end if
		StringRequest2 = ReRequest
	end function 
end class
%>

