<%
'//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	
'
'	author: 	qiozi@msn.com
'	date:		15/11/2006 23:00:02
'	descation:	menu_childClass
'
'
'//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	//

class menu_childClass
	private m_conn_string
	private m_cmd 
	private m_exec_result 
	private m_menu_child_serial_no
	private m_menu_child_name
	private m_menu_child_href
	private m_menu_is_exist_sub
	private m_menu_parent_serial_no
	private m_menu_pre_serial_no
	private m_tag
	private m_menu_child_order
	
	property let conn_string(c)
		m_conn_string = c
	end property
	
	property get conn_string()
		conn_string = m_conn_string
	end property
	
	property let cmd(c)
		m_cmd = c
	end property
	
	property get cmd()
		cmd = m_cmd
	end property
	
	property get exec_result()
		exec_result = m_exec_result
	end property		
	
	property let menu_child_serial_no(s)
		m_menu_child_serial_no = s
	end property
	
	property get menu_child_serial_no()
		menu_child_serial_no = m_menu_child_serial_no
	end property
	property let menu_child_name(s)
		m_menu_child_name = s
	end property
	
	property get menu_child_name()
		menu_child_name = m_menu_child_name
	end property
	property let menu_child_href(s)
		m_menu_child_href = s
	end property
	
	property get menu_child_href()
		menu_child_href = m_menu_child_href
	end property
	property let menu_is_exist_sub(s)
		m_menu_is_exist_sub = s
	end property
	
	property get menu_is_exist_sub()
		menu_is_exist_sub = m_menu_is_exist_sub
	end property
	property let menu_parent_serial_no(s)
		m_menu_parent_serial_no = s
	end property
	
	property get menu_parent_serial_no()
		menu_parent_serial_no = m_menu_parent_serial_no
	end property
	property let menu_pre_serial_no(s)
		m_menu_pre_serial_no = s
	end property
	
	property get menu_pre_serial_no()
		menu_pre_serial_no = m_menu_pre_serial_no
	end property
	property let tag(s)
		m_tag = s
	end property
	
	property get tag()
		tag = m_tag
	end property
	property let menu_child_order(s)
		m_menu_child_order = s
	end property
	
	property get menu_child_order()
		menu_child_order = m_menu_child_order
	end property
	
	public function execute()
		dim rs
		if isnumeric(m_menu_child_serial_no) then
			set rsclass = conn.execute("select * from tb_menu_child where menu_child_serial_no="&cint(m_menu_child_serial_no))
			if not rsclass.eof then
					m_menu_child_serial_no = rsclass("menu_child_serial_no")
					m_menu_child_name = rsclass("menu_child_name")
					m_menu_child_href = rsclass("menu_child_href")
					m_menu_is_exist_sub = rsclass("menu_is_exist_sub")
					m_menu_parent_serial_no = rsclass("menu_parent_serial_no")
					m_menu_pre_serial_no = rsclass("menu_pre_serial_no")
					m_tag = rsclass("tag")
					m_menu_child_order = rsclass("menu_child_order")
			end if	
			rsclass.close : set rsclass = nothing
		end if
	end function 
	
	public function executeInsertUpdate()
		dim sql,rsclass

		
			if m_cmd = "update" and isnumeric(m_menu_child_serial_no) then
				sql = "select * from tb_menu_child where menu_child_serial_no="&cint(m_menu_child_serial_no)
			else
				sql = "select * from tb_menu_child"
			end if
			if sql <> "" then
			set rsclass = server.CreateObject("adodb.recordset")
			rsclass.open sql,conn,1,3
			if m_cmd <> "update" then
				rsclass.addnew
			end if
				if 	m_menu_child_name <> "qiozi_null" then
					rsclass("menu_child_name") = m_menu_child_name
				end if
				if 	m_menu_child_href <> "qiozi_null" then
					rsclass("menu_child_href") = m_menu_child_href
				end if
				if 	m_menu_is_exist_sub <> "qiozi_null" then
					rsclass("menu_is_exist_sub") = m_menu_is_exist_sub
				end if
				if 	m_menu_parent_serial_no <> "qiozi_null" then
					rsclass("menu_parent_serial_no") = m_menu_parent_serial_no
				end if
				if 	m_menu_pre_serial_no <> "qiozi_null" then
					rsclass("menu_pre_serial_no") = m_menu_pre_serial_no
				end if
				if 	m_tag <> "qiozi_null" then
					rsclass("tag") = m_tag
				end if
				if 	m_menu_child_order <> "qiozi_null" then
					rsclass("menu_child_order") = m_menu_child_order
				end if
				rsclass.update
				rsclass.movelast
				m_menu_child_serial_no = rsclass("menu_child_serial_no")
			rsclass.close : set rsclass = nothing
		end if

	end function
	
	public function del()
		if m_menu_child_serial_no <> "" then
			conn.execute("delete from tb_menu_child where menu_child_serial_no="&cint(m_menu_child_serial_no))
		end if
	end function
	
	public function deleteList()
		if m_menu_child_serial_no <> "" then
			conn.execute("delete from tb_menu_child where menu_child_serial_no in ("& m_menu_child_serial_no &")")
		end if
	end function 
	
	public function getMenuChildName()
		dim rs
		getMenuChildName = ""
		if m_menu_child_serial_no <> "" and isnumeric(m_menu_child_serial_no) then
			set rs = conn.execute("select menu_child_name from tb_menu_child where menu_child_serial_no="& m_menu_child_serial_no)
			if not rs.eof then
				getMenuChildName = rs(0)
			end if
			rs.close :set rs = nothing
		end if
	end function 
end class
%>