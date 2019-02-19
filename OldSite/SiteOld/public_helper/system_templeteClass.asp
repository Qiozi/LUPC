<%
'//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	
'
'	author: 	qiozi@msn.com
'	date:		19/10/2006 22:57:05
'	descation:	system_templeteClass
'
'
'//	//	//	//	//	//	//	//	//	//	//	//	//	//	//	//

class system_templeteClass
	private m_conn_string
	private m_cmd 
	private m_exec_result 
	private m_system_templete_serial_no
	private m_system_templete_name
	private m_system_templete_price
	private m_system_templete_sale_sum
	private m_regdate
	private m_last_regdate
	private m_tag
	private m_system_templete_category_serial_no
	
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
	
	property let system_templete_serial_no(s)
		m_system_templete_serial_no = s
	end property
	
	property get system_templete_serial_no()
		system_templete_serial_no = m_system_templete_serial_no
	end property
	property let system_templete_name(s)
		m_system_templete_name = s
	end property
	
	property get system_templete_name()
		system_templete_name = m_system_templete_name
	end property
	property let system_templete_price(s)
		m_system_templete_price = s
	end property
	
	property get system_templete_price()
		system_templete_price = m_system_templete_price
	end property
	property let system_templete_sale_sum(s)
		m_system_templete_sale_sum = s
	end property
	
	property get system_templete_sale_sum()
		system_templete_sale_sum = m_system_templete_sale_sum
	end property
	property let regdate(s)
		m_regdate = s
	end property
	
	property get regdate()
		regdate = m_regdate
	end property

	

	property let last_regdate(s)
		m_last_regdate = s
	end property
	
	property get last_regdate()
		last_regdate = m_last_regdate
	end property
	property let tag(s)
		m_tag = s
	end property
	
	property get tag()
		tag = m_tag
	end property
	
	
	property let system_templete_category_serial_no(s)
		m_system_templete_category_serial_no = s
	end property
	
	property get system_templete_category_serial_no()
		system_templete_category_serial_no = m_system_templete_category_serial_no
	end property
	
	public function execute()
		dim rs
		if isnumeric(m_system_templete_serial_no) then
			set rs = conn.execute("select * from tb_system_templete where system_templete_serial_no="&cint(m_system_templete_serial_no))
			if not rs.eof then
					m_system_templete_serial_no = rs("system_templete_serial_no")
					m_system_templete_name = rs("system_templete_name")
					m_system_templete_price = rs("system_templete_price")
					m_system_templete_sale_sum = rs("system_templete_sale_sum")
					m_regdate = rs("regdate")
					m_last_regdate = rs("last_regdate")
					m_tag = rs("tag")
					m_system_templete_category_serial_no = rs("system_templete_category_serial_no")
			end if	
			rs.close : set rs = nothing
		end if
	end function 
	
	public function executeInsertUpdate()
		dim sql,rs
			if m_cmd = "update" and isnumeric(m_system_templete_serial_no) then
				sql = "select * from tb_system_templete where system_templete_serial_no="&cint(m_system_templete_serial_no)
			else
				sql = "select * from tb_system_templete"
			end if
			if sql <> "" then
			set rs = server.CreateObject("adodb.recordset")
			rs.open sql,conn,1,3
			if m_cmd <> "update" then
				rs.addnew
			end if
			if m_cmd = "update" then 
				if rs.eof then rs.close:set rs = nothing: closeConn(): closeClass():Response.End()
			end if
				if 	m_system_templete_name <> "qiozi_null" then
					rs("system_templete_name") = m_system_templete_name
				end if
				if 	m_system_templete_price <> "qiozi_null" then
					rs("system_templete_price") = m_system_templete_price
				end if
				if 	m_system_templete_sale_sum <> "qiozi_null" then
					rs("system_templete_sale_sum") = m_system_templete_sale_sum
				end if
				if 	m_regdate <> "qiozi_null" then
					rs("regdate") = m_regdate
				end if

				if 	m_last_regdate <> "qiozi_null" then
					rs("last_regdate") = m_last_regdate
				end if
				if 	m_tag <> "qiozi_null" then
					rs("tag") = m_tag
				end if

				
				if 	m_system_templete_category_serial_no <> "qiozi_null" then
					rs("system_templete_category_serial_no") = m_system_templete_category_serial_no
				end if
				rs.update
				if(m_cmd <> "update") then
					rs.movelast
					m_system_templete_serial_no = rs("system_templete_serial_no")
				end if
			rs.close : set rs = nothing
		end if

	end function
	
	public function getSystemProductList()
		getSystemProductList = "0"
		if m_system_templete_serial_no <> "" and isnumeric(m_system_templete_serial_no) then
			
			set rsclass = conn.execute("select * from tb_system_product where system_templete_serial_no="&cint(m_system_templete_serial_no))
			if not rsclass.eof then
				do while not rsclass.eof
				getSystemProductList = getSystemProductList&",["&rsclass("product_serial_no")&"]"
				rsclass.movenext
				loop
			end if
			rsclass.close:set rsclass = nothing
		end if
	end function
end class


%>