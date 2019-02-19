<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
<%
    call ValidateUserSessionInfo(LAYOUT_CCID ,"closeConn") 

    Dim Cmd
    Dim msg
    Dim maxID
    dim limitSQL
    dim resultString 
    maxID = SQLescape(request("maxid"))
    if isnull(maxID) or isempty(maxID) or maxID = "" then maxID = 0
    response.Write(maxID)
    msg = SQLescape(request("msg"))
    cmd = SQLescape(request("cmd"))
    resultString = ""

    if cmd = "save" then
   
        response.Write("Insert into tb_chat_msg_online(username, firstname, lastname, IsReadByUser, IsReadByServer, ChatText, Regdate) values ('"&request.Cookies("customer_serial_no")&"', '"&request.Cookies("customer_first_name")&"', '"&request.Cookies("customer_last_name")&"', 1, 0, '"& msg &"', now())")
        conn.execute("Insert into tb_chat_msg_online(username, firstname, lastname, IsReadByUser, IsReadByServer, ChatText, Regdate)"&_
                " values ('"&request.Cookies("customer_serial_no")&"', '"&request.Cookies("customer_first_name")&"', '"&request.Cookies("customer_last_name")&"', 1, 0, '"& msg &"', now())")
 
    end if

    if cmd = "getmsg" then
        response.Clear()
            if( cint(maxID)<1) then 
                limitSQL = " limit 10 "
            else
                limitSQL = ""
            end if

            set rs = server.CreateObject("adodb.recordset")
           
            if cint(maxID) >0 then 
                rs.open "select * from tb_chat_msg_online where username='"&request.Cookies("customer_serial_no")&"' and id >"& maxID &" order by id desc "& limitSQL ,conn,1,1
            else
                rs.open "select * from tb_chat_msg_online where username='"&request.Cookies("customer_serial_no")&"' order by id desc "& limitSQL , conn,1,1
            end if
                
            if not rs.eof then
                rs.MoveLast  
                do while not rs.eof and not rs.bof

                    if rs("ID")>cint(maxID) then maxID = rs("ID")

                   
                    if rs("ServerName") <> "" then 
                         resultString = resultString & "<div style='clear: bold; '>"
                        resultString = resultString & "<table cellpadding=""0"" cellspacing=""0"" class='msgServer'>" &vblf
                        resultString = resultString & "     <tr>"&vblf
                        resultString = resultString & "         <td rowspan=""2"" class='faceImageServer'><img src='images/user3.png' class=""chargeFaceImg""></td>"&vblf
                        resultString = resultString & "         <td  style='font-size:8pt;'><span class='clientName'>"&rs("ServerName") & "</span>&nbsp;&nbsp;&nbsp;&nbsp;"&rs("regdate")&"</td>"&vblf
                        resultString = resultString & "     </tr>"&vblf
                        resultString = resultString & "     <tr>"&vblf
                        resultString = resultString & "         <td class='msgContent'>"& rs("ChatText") &"</td>"
                        resultString = resultString & "     </tr>"&vblf
                        resultString = resultString & "</table>"&vblf
                        resultString = resultString & "</div>"
                    else
                        resultString = resultString & "<div style='clear:both;'>"
                        resultString = resultString & "<table cellpadding=""0"" cellspacing=""0"" class='msgClient' align=""right"">" &vblf
                        resultString = resultString & "     <tr>"&vblf
                        resultString = resultString & "         <td style='font-size:8pt;'><span class='clientName'>"&rs("firstname") & " " & rs("lastname") & "</span>&nbsp;&nbsp;&nbsp;&nbsp;"&rs("regdate")&"</td>"&vblf
                        resultString = resultString & "         <td rowspan=""2"" class='faceImageClient'><img src='images/user1.png' class=""chargeFaceImg""></td>"&vblf
                        resultString = resultString & "     </tr>"&vblf
                        resultString = resultString & "     <tr>"&vblf

                        resultString = resultString & "         <td class='msgContent'>"& rs("ChatText") &"</td>"
                        resultString = resultString & "     </tr>"&vblf
                        resultString = resultString & "</table>"&vblf
                        resultString = resultString & "</div>"
                    end if
                    
                rs.MovePrevious 
                loop
                resultString = resultString & right("0000000000"& maxID, 10)
            end if
            response.Write(resultString)

        closeconn()
        response.End()
    end if
 %>
<%closeconn() %>
</body>
</html>
