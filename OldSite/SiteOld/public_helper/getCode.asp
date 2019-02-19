<%
	
class getCodeClass
	public function order()
		dim code, order_code
		set code = new code_helperClass
			order_code = code.getOrderNewCode()
		set code = nothing

		set rsclass = conn.execute("select order_code from tb_order_code where order_code='"&order_code&"'")
		if not rsclass.eof then
			order = order()				
		else
			order = order_code
		end if
		conn.execute("insert into tb_order_code (order_code, regdate, is_order)	values	( '"&order&"', now(), 0)")
		rsclass.close : set rsclass = nothing				
	end function
	
	public function Tests(s)
		response.Write(s&"test<br>")
	end function
	
	public function sys_prod()
		dim code, order_code, code2
		set code = new code_helperClass
			order_code = code.getRND(8)
		set code = nothing
			
		'set rsclass = conn.execute("select sys_tmp_code from tb_sp_tmp_detail where sys_tmp_code='"&order_code&"'")
		set rsclass = conn.execute("select system_code from tb_system_code_store where system_code='"&order_code&"'")
		if not rsclass.eof then
			sys_prod = sys_prod()
		else
			sys_prod = order_code
		end if
		rsclass.close : set rsclass = nothing
		
	end function
end class	
%>
