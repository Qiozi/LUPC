<%
    if request.Cookies("AdminLoginID") = "" or isnull(request.Cookies("AdminLoginID")) or isempty(request.Cookies("AdminLoginID")) then 
        response.Write("error,don't login")
        response.End()
    end if
%>