<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="/public_helper/public_helper.asp"-->
<!--#include virtual="/public_helper/sql.asp"-->
<!--#include virtual="/public_helper/helper.asp"-->
<!-- begin left menu -->
<% 
	dim menu_sub_img_1, menu_sub_img_2
'	menu_sub_img_1 = "<img src=""/images/jiant4.gif"" border=""0"" alt="">"
'	menu_sub_img_2 = "<img src=""/images/jiant3.gif"" border=""0"" alt="">"
	menu_sub_img_1 =  "/images/jiant4.gif"
	menu_sub_img_2 =  "/images/jiant3.gif"


	dim page_category_request, is_exist_sub, default_view
	

	page_category_request = encode.IntRequest("page_category")
	default_view = 1
	
		set rs =server.CreateObject("adodb.recordset")
		set rs1 =server.CreateObject("adodb.recordset")		  
		rs.open "select menu_child_name,menu_child_serial_no,menu_is_exist_sub,page_category from tb_product_category where menu_pre_serial_no=0  and menu_parent_serial_no=1 and tag=1 order by menu_child_order asc",conn,1,1
	'			  a=0
			dim menu_parent_name ,menu_parent_ID, menu_child_name, menu_child_id
				left_title_count = 0
			do until rs.eof
				left_title_count = left_title_count + 1
				menu_parent_name = rs("menu_child_name")
				menu_parent_ID = rs("menu_child_serial_no")
					
					if left_title_count = 1 then default_view =left_title_count
						'
						'
						'	first level menu
						'
						response.Write("<div class=""page_left_menu_parent"" ")
						if left_title_count <> 1 then response.Write(" style=""margin-top:5px""")
						response.Write("onClick=""onclickMenuParent('menu_left_parent_"& left_title_count& "', 'menu_left_item_"& left_title_count &"');"" ><span id=""menu_left_item_"& left_title_count &""" class=""page_left_menu_parent_sub2"">"& trim(menu_parent_name) &"</span>")   
						response.Write("</div>")
						response.Write("<div style=""margin-top: 1px; border: 1px solid #8FC2E2; background:#ffffff; padding:2px; display:none"" id=""menu_left_parent_"& left_title_count &"""> ")
						response.Write("<ul style=""padding: 2px; border:1px solid #CCCCCC"">")
											
											
						rs1.open "select menu_child_name,menu_child_serial_no,menu_is_exist_sub,page_category from tb_product_category  where tag=1 and menu_parent_serial_no=1 and menu_pre_serial_no="&cint(menu_parent_ID)&" order by menu_child_order asc",conn,1,1    '						
							do until rs1.eof
								menu_child_name = trim(rs1("menu_child_name"))
								menu_child_id = rs1("menu_child_serial_no")
	 
								response.Write("<li class=""page_left_menu_sub_li"">")
						   
								if cstr(rs1("menu_is_exist_sub")) = "0" then 
									'
									'
									'	have href
									'
									response.Write("<a href="""& HOST_URL &"Product_list.asp?page_category="&  rs1("page_category") &"&class="& left_title_count &"&id="& menu_child_id &""" ")
									response.Write(" onclick=""TransferList('"&  rs1("page_category") &"', '"& left_title_count &"', '"& menu_child_id &"', this); return false;""> ")
									response.Write(" <span  id=""item_id_"&  menu_child_id &""">"& menu_child_name &"</span> ")
		
									if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and rs1("page_category")=1 then
										response.Write("<a href="""& HOST_URL &"part_showit_manage.aspx?categoryID="&  menu_child_id  &""" onclick=""js_callpage_cus(this.href, 'right_manage', 1000,800);return false;"">M</a>")                                    
									end if
							 
								else
									'
									'
									'	haven't href
									'
									response.Write("<div style=""cursor:pointer;"" onClick=""onclickMenuChild('menu_left_child_"& menu_child_id &"');"">")
									response.Write("<a href=""#"" onClick=""return false;"" >"& menu_child_name &"</a>")
									response.Write("</div>")
									
									'if cstr(menu_parent_ID) <> "3" then 
										set mcrs = server.CreateObject("adodb.recordset")
										mcrs.open "select menu_child_serial_no,menu_child_name,page_category from tb_product_category  where tag=1 and menu_pre_serial_no="&menu_child_id &" order by menu_child_order asc", conn,1,1
										
										
										'mccount = mcrs.recordcount
										if not mcrs.eof then
											set mccount = conn.execute("select count(menu_child_serial_no) from tb_product_category  where tag=1 and menu_pre_serial_no="&menu_child_id &" order by menu_child_order asc")
											mcount = mccount(0) 
											set mccount = nothing
											response.Write("<div style='display:none;' name='menu_left_child' id='menu_left_child_"&menu_child_id&"'>")
											dim mcrs_name 
											dim iii
											iii = 0
											response.Write ""
											do while not mcrs.eof
											iii = iii+1
												mcrs_name  = trim(mcrs("menu_child_name"))
												
												if iii = mcount then 
													response.write "<div style=""padding-left:12px; background: url('"&menu_sub_img_2&"') no-repeat 2px 0;padding-top:2px; padding-bottom: 1px"">"
												else 
													response.write "<div style=""padding-left:12px; background: url('"&menu_sub_img_1&"') no-repeat 2px 50%;padding-top:2px; padding-bottom: 1px"">"
												end if
											   
												response.Write("<a href="""& HOST_URL &"lists/Product_list.asp?page_category="& mcrs("page_category")&"&class="&left_title_count&"&id="&mcrs("menu_child_serial_no")&""" onclick=""TransferList("& mcrs("page_category")&", "&left_title_count&", "&mcrs("menu_child_serial_no")&", this ); return false;"" class=""hui_orange_11"" id=""item_id_"&mcrs("menu_child_serial_no")&""">")
												if len(mcrs_name) >26 then 
													response.Write(left(mcrs_name,26))
												else
													response.Write mcrs_name
												end if
												response.Write "</a>"
												' add manage button
												if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and mcrs("page_category")=1 then 
													Response.Write "&nbsp;<a href="""& HOST_URL &"part_showit_manage.aspx?categoryID="& mcrs("menu_child_serial_no") &""" onclick=""js_callpage_cus(this.href, 'right_manage', 1000,800);return false;"">M</a>"										
												end if
												
												response.write "</div> "
											mcrs.movenext : loop 
											response.Write "</div>"
										end if
										mcrs.close :set mcrs  = nothing
									'end if
								end if
								
								response.Write("</a>")
								response.Write("</li>")
									rs1.movenext
								  loop
								 rs1.close
		
					  response.Write(" </ul>")
					 response.Write("</div>")                 
	
			rs.movenext
			loop
			rs.close : set rs = nothing

%>
<!-- end left menu -->