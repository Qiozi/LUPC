
<%
	function CustomerSendMsg(order_code, content)
		
		dim rs
		if len(trim(content)) > 0 then 
			set rs = server.CreateObject("adodb.recordset")
			rs.open "select * from tb_chat_msg ",conn,1,3
			rs.addnew
			rs("msg_order_code") = order_code
			rs("msg_content_text") = content
			rs("msg_type") = 1
			rs("msg_author") = "Me"
			rs("regdate") = now()
			rs.update
			rs.close : set rs = nothing
		end if
		
		'conn.execute("insert into tb_chat_msg(msg_order_code,msg_content_text, msg_type,msg_author, regdate)	values ('"&order_code&"', '"&content&"', 1, 'Me', '"& now()&"')")
	end function
%>