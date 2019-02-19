
<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	dim system_id
	system_id = SQLescape(request("system_id"))
	
	set rs = conn.execute(sel_sql(system_id))
	if not rs.eof then 
		do while not rs.eof 
			response.write ("document.write('"&short_name_str(rs(0), rs(1))&"');")
		rs.movenext
		loop
	end if
	rs.close : set rs = nothing
	closeconn()
	
	function sel_sql(sku)
        'sel_sql = "select p.product_serial_no,p.product_short_name,p.product_name from tb_system_product sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no = p.menu_child_serial_no where"&_
'
'		" ("&_
'		" p.menu_child_serial_no in"&_
'		" ("&_
'		" select computers_memory_category 'category' from tb_computers_memory "&_
'		" union all "&_
'		" select computer_cpu_category from tb_computer_cpu "&_
'		" union all"&_
'		" select computer_video_card_category from tb_computer_video_card"&_
'		" union all"&_
'		" select computers_hard_drive_category from tb_computers_hard_drive"&_
'		" )"&_
'		" or pc.menu_pre_serial_no in "&_
'		" ("&_
'		" select computers_memory_category 'category' from tb_computers_memory"&_
'		" union all"&_
'		" select computer_cpu_category from tb_computer_cpu"&_
'		" union all"&_
'		" select computer_video_card_category from tb_computer_video_card"&_
'		" union all"&_
'		" select computers_hard_drive_category from tb_computers_hard_drive"&_
'		" )  "&_
'		" )"&_
'		" and "&_
'		" sp.system_templete_serial_no="&sku&" and sp.showit=1 and p.is_non=0 order by sp.product_order asc "
		sel_sql = "select p.product_serial_no , p.product_short_name  from tb_ebay_system st inner join tb_ebay_system_parts sp on sp.system_sku=st.id inner join tb_product p on p.product_serial_no = sp.luc_sku where  p.product_serial_no not in (select product_serial_no from tb_product where product_name like '%warranty%' or product_short_name like '%warranty%') and sp.system_sku="& sku &" and p.is_non=0 and  st.showit=1 order by sp.id asc"
    end function
	
    function short_name_str(id, name)
        short_name_str = "• <a class=\""text_hui_11\"" style=\""line-height:12px;\"" href=\"""& LAYOUT_HOST_URL&"view_part.asp?id="& id &"\"" onClick=\""return js_callpage_cus(this.href,\'view_system\',602, 600);\"">" & name &"</a><br/>"'& "</li>"
    end function
%>