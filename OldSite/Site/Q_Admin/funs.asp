<%
'
'	GetSysLeftMenu
'	GetGroupLeftMenu
'	writePartOfSysSKU
'	writeGroupOfSysSKU
'   writeGroupOfeBaySysSKU(part_group_id)
'   GetStateIdByStateCount(state, country)
'
'
'
'


'------------------------------------------------------------------------------------
Function GetSysLeftMenu()
'------------------------------------------------------------------------------------
    Dim Rs, s, cRs, sRs
    set Rs = Conn.execute("select menu_child_serial_no, menu_child_name, menu_is_exist_sub from tb_product_category where menu_pre_serial_no=52 and tag=1 and is_view_menu=1 order by menu_child_order asc ")
    If Not Rs.eof Then
            s   = " <ul id=""browser"" class=""filetree"">"  &vblf
            Do While Not Rs.eof 
                        s   = s & "<li><span class=""folder"" id=""treeview_category_id_"& rs("menu_child_serial_no") &""">"& rs("menu_child_name")& "</span>" &vblf
                        'Response.Write "<ul>"
                        if(rs("menu_is_exist_sub") = 0)then
                            s   = s & "<ul style='display:none'>"
                            set sRs = conn.execute("select system_templete_serial_no,system_templete_category_serial_no "&_
                                                                    " from tb_system_templete where system_templete_category_serial_no ='"& Rs("menu_child_serial_no") &"' and tag=1 "&_
                                                                    " union all "&_
                                                                    " select system_templete_serial_no,system_templete_category_serial_no "&_
                                                                    " from tb_system_templete st inner join tb_product_virtual pv "&_
                                                                    " on pv.menu_child_serial_no ='"& Rs("menu_child_serial_no") &"' and st.tag=1 and pv.lu_sku = st.system_templete_serial_no" )
                            if not sRs.eof then
                                    do while not srs.eof 
                                            s =     s &     "<li><span class=""file"" id=""treeview_system_sku_"& sRs("system_templete_serial_no") &"""> <a onclick=""parent.$('#ifr_main_frame1').attr('src','/q_admin/product_system_custom.asp?menu_child_serial_no="&sRs("system_templete_category_serial_no")&"&cmd=modify&id="&sRs("system_templete_serial_no")&"');"")>"& sRs("system_templete_serial_no") &"</a></span></li>" &vblf
                                    srs.movenext
                                    loop
                            end if
                            srs.close : set sRs = nothing
                            'Response.Write "</ul></li>"
                        else
                            s   = s & "<ul>"
                            set cRs = conn.execute ( "select menu_child_serial_no, menu_child_name from tb_product_category where menu_pre_serial_no='"&rs("menu_child_serial_no")&"' and tag=1 order by menu_child_order asc  ")
                            if not cRs.eof then
                                    Do While not cRs.eof
                                            s   =      s & "<li><span class=""folder"" id=""treeview_category_id_"& cRs("menu_child_serial_no") &""" style='font-weight:600;'>"& crs("menu_child_name") &"</span>" & vblf
                                            s   =       s & "       <ul style='display:none'>"

                                                    set sRs = conn.execute("select system_templete_serial_no,system_templete_category_serial_no "&_
                                                                    " from tb_system_templete where system_templete_category_serial_no ='"& cRs("menu_child_serial_no") &"' and tag=1 "&_
                                                                    " union all "&_
                                                                    " select system_templete_serial_no,system_templete_category_serial_no "&_
                                                                    " from tb_system_templete st inner join tb_product_virtual pv "&_
                                                                    " on pv.menu_child_serial_no ='"& cRs("menu_child_serial_no") &"' and st.tag=1 and pv.lu_sku = st.system_templete_serial_no")
                                                    if not sRs.eof then
                                                            do while not srs.eof 
                                                                    s =     s &     "<li><span class=""file"" id=""treeview_system_sku_"& sRs("system_templete_serial_no") &"""> <a onclick=""parent.$('#ifr_main_frame1').attr('src','/q_admin/product_system_custom.asp?menu_child_serial_no="&srs("system_templete_category_serial_no")&"&cmd=modify&id="&srs("system_templete_serial_no")&"');"")>"& sRs("system_templete_serial_no") &"</a></span></li>" &vblf
                                                            srs.movenext
                                                            loop
                                                    end if
                                                    srs.close : set sRs = nothing
                                            s   =       s & "       </ul> "&vblf &_
                                                                "   </li>"
                                    cRs.movenext
                                    loop 
                            end if
                            cRs.close : set cRs = nothing
                        end if
                        s   =       s &"</ul></li>"
            Rs.movenext
            Loop
            s   =    s &                      "</ul>"
    End If

    GetSysLeftMenu   =   s
End Function



'------------------------------------------------------------------------------------
Function GetGroupLeftMenu(ct)
'------------------------------------------------------------------------------------
    Dim Rs, s, cRs, sRs
    set Rs = Conn.execute("select menu_child_serial_no, menu_child_name from tb_product_category where menu_child_serial_no in (2,214)")
    If Not Rs.eof Then
            s   = " <ul id=""browser"" class=""filetree"">"  &vblf
            Do While Not Rs.eof 
                        s   = s & "<li><span class=""folder"" id=""treeview_category_id_"& rs("menu_child_serial_no") &""">"& rs("menu_child_name")& "<ul>" &vblf
                        set cRs = conn.execute ( "select menu_child_serial_no, menu_child_name from tb_product_category where menu_pre_serial_no='"&rs("menu_child_serial_no")&"' and tag=1 order by menu_child_order asc  ")
                        if not cRs.eof then
                        
                            if ct = "editGroupDetail" then 
                                Do While not cRs.eof
                                        s   =      s & "<li class='group_menu_name'><span class=""folder"" id=""treeview_category_id_"& cRs("menu_child_serial_no") &""" style='font-weight:600;'>"& crs("menu_child_name") &"</span>" & vblf
                                        s   =       s & "       <ul style='display:none'>"

                                                set sRs = conn.execute("select part_group_id, case when part_group_comment<>'' then part_group_comment else part_group_name end as name,is_ebay from tb_part_group where product_category = '"& cRs("menu_child_serial_no") &"' and showit=1 ")
                                                if not sRs.eof then
                                                        do while not srs.eof 
                                                                s =     s &     "<li><span class=""file"" id=""treeview_system_sku_"& sRs("part_group_id") &"""> <a onclick=""parent.$('#ifr_main_frame1').attr('src','/q_admin/sys/sys_group_part_list.asp?part_group_id="&sRs("part_group_id")&"');"")>"& sRs("name") &"</a></span></li>" &vblf
                                                        srs.movenext
                                                        loop
                                                end if
                                                srs.close : set sRs = nothing
                                        s   =       s & "       </ul> "&vblf &_
                                                            "   </li>"
                                cRs.movenext
                                loop 
                            else
                                Do While not cRs.eof
                                        s   =       s & "<li class='group_menu_name'><span class=""file"" id=""treeview_category_id_"& cRs("menu_child_serial_no") &""" style='font-weight:600;'>"
                                        
                                        s   =       s & "<a onclick=""parent.$('#ifr_main_frame1').attr('src','/q_admin/ebayMaster/lu/part_group_edit.asp?category_id="&cRs("menu_child_serial_no")&"');"")>" & crs("menu_child_name") &"</a></span>" & vblf

                                        s   =       s & "   </li>"
                                cRs.movenext
                                loop 
                            end if
                            
                        end if
                        cRs.close : set cRs = nothing
                        s   =       s &"</ul></li>"
            Rs.movenext
            Loop
            s   =    s &                      "</ul>"
    End If

    GetGroupLeftMenu   =   s
End Function



function writePartOfSysSKU(luc_sku)
	writePartOfSysSKU = ""
	dim rs
	set rs = conn.execute("select distinct sp.system_sku from tb_ebay_system_parts sp inner join tb_ebay_system st on "&_
						" sp.system_sku=st.id where st.showit=1 "&_
						" and sp.luc_sku= '"& luc_sku &"'")
	if not rs.eof then
		do while not rs.eof 
			if writePartOfSysSKU = "" then
				writePartOfSysSKU = rs(0)
			else
				writePartOfSysSKU = writePartOfSysSKU &" | "& rs(0)
			end if
		rs.movenext
		loop
	end if
	rs.close : set rs = nothing

end function



function writeGroupOfSysSKU(part_group_id)

	writeGroupOfSysSKU = ""
	dim rs, i
	set rs = conn.execute("select sp.system_sku from tb_ebay_system_parts sp inner join tb_ebay_system st on "&_
						" sp.system_sku=st.id where st.showit=1 "&_
						" and sp.part_group_id= '"& part_group_id &"' limit 0,10")
	if not rs.eof then
        i = 0
		do while not rs.eof 
            i = i + 1
			if writeGroupOfSysSKU = "" then
				writeGroupOfSysSKU = rs(0)
			else
				writeGroupOfSysSKU = writeGroupOfSysSKU &" | "& rs(0)
			end if
		rs.movenext
		loop
	end if
	rs.close : set rs = nothing
end function



function writeGroupOfeBaySysSKU(part_group_id)
	writeGroupOfeBaySysSKU = ""
	dim rs, i
	set rs = conn.execute("select esp.system_sku from tb_ebay_system_parts esp inner join tb_ebay_system st on "&_
						" esp.system_sku=st.id where st.showit=1 "&_
						" and esp.part_group_id= '"& part_group_id &"' and st.is_online=1 limit 0, 10")
						

	if not rs.eof then
        i = 0
		do while not rs.eof 
            i = i + 1
			if writeGroupOfeBaySysSKU = "" then
				writeGroupOfeBaySysSKU = rs(0)
			else
				writeGroupOfeBaySysSKU = writeGroupOfeBaySysSKU &" | "& rs(0)
			end if
		rs.movenext
		loop
        if(i = 10) then
            response.Write("...")
        end if
	end if
	rs.close : set rs = nothing
End function

function GetStateIdByStateCount(state, country, countryCode)
    dim rs
    set rs = conn.execute("select * from tb_state_shipping where (state_name ='"& state &"' or state_code='"& state &"') and (country='"& country &"' or country='"& countryCode &"')")
    if not rs.eof then
        GetStateIdByStateCount = rs("state_serial_no")
    else
        if countryCode = "" then  countryCode = country
        conn.execute("insert into tb_state_shipping(state_name, state_shipping, system_category_serial_no, state_short_name, state_code, is_paypal, Country, isOtherCountry) values "&_
                    "('"& state &"', '150',  '3',  '"& state &"',  '"& state &"',  '1',  '"&countryCode&"', 1)")

        GetStateIdByStateCount = GetStateIdByStateCount(state, country, countryCode)     

    end if
    rs.close : set rs = nothing
end function
%>