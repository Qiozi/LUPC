<%
' **********************************************************************************
' * FILE HEADER:															
' **********************************************************************************
'	IsExistOrderCode()
'	GetCookiesOrderCode()
'	SetCookiesOrderCode(v)
'	
'	
'	
'	
'	
'	
'	
'	
'	
'	

' *																					
' **********************************************************************************
' * DESCRIPTION:																	
' **********************************************************************************


' ---------------------------------------------------------------------------------
function IsExistOrderCode()
' ---------------------------------------------------------------------------------
	IsExistOrderCode = false
	if len(request.Cookies("tmp_order_code")) = 6 then 
		IsExistOrderCode = true
	end if
end function



' ---------------------------------------------------------------------------------
function GetCookiesOrderCode()
' ---------------------------------------------------------------------------------
	GetCookiesOrderCode = ""
	if IsExistOrderCode() then 
		GetCookiesOrderCode = request.Cookies("tmp_order_code")
	end if

end function



' ---------------------------------------------------------------------------------
function SetCookiesOrderCode(v)
' ---------------------------------------------------------------------------------
	response.Cookies("tmp_order_code") = v
end function


%>