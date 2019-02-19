<%
' !!! NEVER EVER INSERT A STRING INTO A HTML, JAVASCRIPT OR EVEN SQL CODE WITHOUT ESCAPING IT PROPERLY!!!

' HTMLescape : if the variable is to be showed on the page
'	<td>< %=HTMLescape( variable )% ></td>

' TAGescape : if its used to set the value of a field
'	<input type="text" name="Foo" value="< %=TAGescape( variable )% >">

' JSescape : if it's used as a parameter to a JavaScript function
'	<a href="JavaScript:foo( '< %=JSescape(variable)% >' );">

' URLescape : if it's to become part of a query string
'	<a href="Foo.asp?name=< %=URLescape(variable)% >"

' SQLescape : if it's to become part of an SQL query
'	sql = "select * from Table where name = '" & SQLescape( name) & "'"

' SQLquote : if it's to become part of an SQL query, adds the quotes aroung the value, works correct with NULLs
'	sql = "select * from Table where name = " & SQLquote( name)

' TrimCommaEscape
'   "2,2,3"    =      TrimCommaEscape(",2,2,3") 

'   VEmptyNull(s)   ' validate data is empty or null
'

Function HTMLescape (s)
	if isEmpty(s) or isNull(s) then
		HTMLescape = ""
	else
		HTMLescape = Server.HTMLEncode(s)
	end if
End Function

Function TAGescape (s)
	if isEmpty(s) or isNull(s) then
		TAGescape = ""
	else
		TAGescape = Replace(Replace(Server.HTMLEncode(s),"""", "&dblquote;" ), "'", "&#39;")
	end if
End Function

Function URLescape (s)
	if isEmpty(s) or isNull(s) then
		URLescape = ""
	else
		URLescape = Replace(Replace(Server.URLEncode(s),"'","%27"), """", "%22" )
	end if
End Function

Function JSescape (s)
	if isEmpty(s) or isNull(s) then
		JSescape = ""
	else
		JSescape = TAGescape( Replace( Replace( Replace( s, "\", "\\"), """", "\""" ) , "'", "\'" ))
	end if
End Function

Function SQLescape (s)
	if isEmpty(s) or isNull(s) then
		SQLescape = ""
	else
		SQLescape = Replace( s, "'", "''")
	end if
End Function

Function SQLquote (s)
	if isEmpty(s) or isNull(s) then
		SQLquote = "NULL"
	else
		SQLquote = "'" & Replace( s, "'", "''") & "'"
	end if
End Function

Function TrimCommaEscape(s)
        TrimCommaEscape = ""
        if isEmpty(s) or isNull(s) then
                TrimCommaEscape = ""
        else
                s = trim(s)
                if len(s)>0 then
                        if  left(s, 1) = "," then
                                TrimCommaEscape = right(s, len(s)-1)                        
                        end if
                        if  right(s, 1) = "," then
                                TrimCommaEscape = left(s, len(s)-1)       
                        end if 
                        if  TrimCommaEscape = "" then
                            TrimCommaEscape = s
                        end if      
                end if
        end if
end Function



Function CommaEncode(s)
	if isEmpty(s) or isNull(s) then
        CommaEncode = ""
	else
		if instr(s, ",")>0 then
			CommaEncode = replace(s, ",", JS_COMMA_EXCODE)
		else
			CommaEncode = s
		end if
		
	end if
End Function



Function CommaDecode(s)
	if isEmpty(s) or isNull(s) then
        CommaDecode = ""
	else
		if instr(s, JS_COMMA_EXCODE)>0 then
			CommaDecode = replace(s, JS_COMMA_EXCODE, ",")
		else
			CommaDecode = s
		end if		
	end if
End Function

Function VEmptyNull(s)

    if isEmpty(s) or isNull(s) then
		VEmptyNull = ""
    else
        VEmptyNull = s
    end if
End Function
%>