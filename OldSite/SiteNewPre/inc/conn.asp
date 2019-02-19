
<%

Session.Timeout = 60
dim connString
dim sql, rs, conn, i, crs, srs,rs1

Dim page				:	page					=	null
Dim sortby				:	sortby					=	null
Dim classs				:	classs					=	null
Dim page_category		:	page_category			=	null

'Response.buffer = true
'On Error Resume Next

connString = Application("DBCONN")
set conn=server.CreateObject("adodb.connection")
conn.open "Driver={MySQL ODBC 3.51 Driver};SERVER=localhost;DATABASE=nicklu2;UID=root;PWD=1234qwer;OPTION="& 1 + 2 + 8 + 32 + 2048 + 16384 & ";"

'==========================================
dim tmpReadIP, tmpReadQty, tmpReadDeny

tmpReadIP =  Request.ServerVariables("REMOTE_ADDR")
tmpReadDeny = false


set rs = conn.execute("Select count(id) from tb_ip_deny where ip ='"&tmpReadIP&"'")
if not rs.eof then
    if rs(0) = 1 then
        tmpReadDeny = true
    end if
end if
rs.close 

if not tmpReadDeny then 
    set rs = conn.execute("select count(*) from (select distinct  date_format(regdate, '%y%m%d%h%i') from tb_read_log where ip='"& tmpReadIP &"' and TIMESTAMPDIFF(hour, regdate , now())<2) t")
    if not rs.eof then
        if (rs(0)>40) then 
            tmpReadDeny = true
        end if
    end if
    rs.close 
end if

if tmpReadDeny then 
    conn.execute("insert into tb_ip_deny (IP, regdate)	values	('"& tmpReadIP &"',now())")
    response.Redirect("/p.html")
    closeconn()
end if

conn.execute("insert into tb_read_log(regdate, ip) values (now(),'"& tmpReadIP &"')")


'=========================================================

sub closeConn()
	conn.close
	set conn = nothing
	'If Error.Number <> 0 then
	'	Response.Clear()
	'	Response.write ERR.Number '"<html><head><title></title></head><body><FONT FACE=""ARIAL"">An error occurred in the execution of this ASP page<BR>"&_ 
  						'" Please report the following information to the support desk<P> "&_
'         				" <B>Page Error Object</B><BR>  "&_
'         				" Number£º "& Err.Number &"<BR>  "&_
'         				" description£º "& Err.Description &"<BR>   "&_
'         				" file£º "& Err.Source &"<BR>  "&_
'         				" line£º "& Err.Line &"<BR>  "&_
'         				" </FONT>  </body></html> "

	'end if
end sub
'---------------------------------------------------

'---------------------------------------------------
Application("system_name") = "LU Computer web site manage"


%>