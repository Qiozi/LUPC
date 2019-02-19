<!-- begin left menu -->
<% 
	

	dim menu_parent_name ,menu_parent_ID, menu_child_name, menu_child_id
	Dim left_title_count	:    	left_title_count = 0
	Dim default_view		:		default_view 	=	null
	Dim mcrs				:		mcrs 			=	null
	Dim mccount				:		mccount			=	null
	Dim mcount				:		mcount			=	null
	Dim n
	Dim ChangeColorCategoryIDs :	ChangeColorCategoryIDs = ""
    Dim ReqCid              :       ReqCid          = SQLescape(Request("cid"))
    Dim CidParentID
    Dim leftMenuString      :       leftMenuString  = ""
    Dim IsReadDBForLeftMenu :       IsReadDBForLeftMenu = true
    Dim objFSO, f
    '
    '
    ' get parent id
    CidParentID = 0
    set rs = conn.execute("Select menu_pre_serial_no from tb_product_category where menu_child_serial_no = '"& ReqCid &"' and is_view_menu=1")
    if not rs.eof then
        CidParentID = rs(0)
    end if
    rs.close : set rs = nothing

    set objFSO = CreateObject("Scripting.FileSystemObject")
    if objFSO.FileExists(Server.MapPath("/site/leftMenu.htm")) then 

        if datevalue(objFSO.GetFile(Server.MapPath("/site/leftMenu.htm")).DateLastModified) = dateValue(now()) then 
            set f=objFSO.OpenTextFile(Server.MapPath("/site/leftMenu.htm"), 1, false)
            leftMenuString = f.readall
            IsReadDBForLeftMenu = false
            f.close : set f = nothing
        else
            IsReadDBForLeftMenu = true
        end if
       
    else
        IsReadDBForLeftMenu = true
    end if

    set objFSO = nothing

	if IsReadDBForLeftMenu then 
		set rs =server.CreateObject("adodb.recordset")
		set rs1 =server.CreateObject("adodb.recordset")		  
		rs.open "select menu_child_name,menu_child_serial_no,menu_is_exist_sub,page_category from tb_product_category where menu_pre_serial_no=0  and menu_parent_serial_no=1 and tag=1 and is_view_menu=1  order by menu_child_order asc",conn,1,1
	'			  a=0
			n = 0 
			do until rs.eof
				n = n + 1
				left_title_count = rs("menu_child_serial_no")
				menu_parent_name = rs("menu_child_name")
				menu_parent_ID = rs("menu_child_serial_no")
        					
					if rs("page_category") = 0 then default_view =left_title_count
						'
						'
						'	first level menu
						'
						leftMenuString = leftMenuString & ("<div class=""page_left_menu_parent"" ")
								
						if n <> 1 then	leftMenuString = leftMenuString & (" style=""margin-top:3px""")
								
						leftMenuString = leftMenuString &  ("onClick=""onclickMenuParent('menu_left_parent_"& left_title_count& "', 'menu_left_item_"& left_title_count &"');"" ><span id=""menu_left_item_"& left_title_count &""" class=""page_left_menu_parent_sub2"">"& trim(menu_parent_name) &"</span>")   
						leftMenuString = leftMenuString & ("</div>")
						leftMenuString = leftMenuString & ("<div style=""margin-top: 1px; border: 1px solid #8FC2E2; background:#ffffff; padding:2px; display:none"" id=""menu_left_parent_"& left_title_count &"""> ")
						leftMenuString = leftMenuString & ("<ul style=""padding: 2px; border:1px solid #CCCCCC"">")
        											
        											
						rs1.open "select menu_child_name,menu_child_serial_no,menu_is_exist_sub,page_category from tb_product_category  where tag=1 and is_view_menu=1 and menu_parent_serial_no=1 and menu_pre_serial_no="&cint(menu_parent_ID)&" order by menu_child_order asc",conn,1,1    '						
							do until rs1.eof
								menu_child_name = trim(rs1("menu_child_name"))
								menu_child_id = rs1("menu_child_serial_no")
        	                    if(menu_child_id<>378) then 
								                    leftMenuString = leftMenuString & ("<li class=""page_left_menu_sub_li"" style='border: 0px solid red;'>")
										
								                    ' onsale tag image
        						                    if(rs1("page_category") = 0) then 
        							                    'Response.write "<span><img src='http://www.lucomputers.com/soft_img/app/Sale.gif' "&_
									                    '				" style='position:absolute; margin-left: 110px; float:right; z-index:"& rs1("menu_child_serial_no") &"'></span>"
								                    end if
										
								                    if cstr(rs1("menu_is_exist_sub")) = "0" then 
									                    '
									                    '
									                    '	have href
									                    '
									                    leftMenuString = leftMenuString & ("<a href="""& LAYOUT_HOST_URL &"Product_list.asp?page_category="&  rs1("page_category") &"&class="& left_title_count &"&cid="& menu_child_id &""" ")
									                    leftMenuString = leftMenuString & (" onclick=""TransferList('"&  rs1("page_category") &"', '"& left_title_count &"', '"& menu_child_id &"', this); return false;""> " &vblf)
									                    leftMenuString = leftMenuString & (" <span  id=""item_id_"&  menu_child_id &""" ")
									                    if instr(ChangeColorCategoryIDs, "["& menu_child_id & "]")>0  then leftMenuString = leftMenuString &  "style=""Color:#ff9900;"" "
											
									                    leftMenuString = leftMenuString &  (">"& menu_child_name &"</span> ")
        		
									                    if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and rs1("page_category")=1 then
										                    leftMenuString = leftMenuString & ("<a href=""/part_showit_manage.aspx?categoryID="&  menu_child_id  &""" onclick=""js_callpage_cus(this.href, 'right_manage', 1000,800);return false;"">M</a>")                                    
									                    end if
        							 		
								                    else
									                    '
									                    '
									                    '	haven't href
									                    '
									                    leftMenuString = leftMenuString & ("<div style=""cursor:pointer;"" onClick=""onclickMenuChild('menu_left_child_"& menu_child_id &"');"">" &vblf)
									                    leftMenuString = leftMenuString & ("<a href=""#"" onClick=""return false;""" &vblf)
									                    if instr(ChangeColorCategoryIDs, "["& menu_child_id &"]")>0  then leftMenuString = leftMenuString & "style=""Color:#ff9900;"""
									                    leftMenuString = leftMenuString &  (" >"& menu_child_name &"</a>" &vblf)
									                    leftMenuString = leftMenuString & ("</div>" &vblf)
        									
									                    'if cstr(menu_parent_ID) <> "3" then 
										                    set mcrs = server.CreateObject("adodb.recordset")
										                    mcrs.open "select menu_child_serial_no,menu_child_name,page_category from tb_product_category  where tag=1 and  is_view_menu=1  and menu_pre_serial_no="&menu_child_id &" order by menu_child_order asc", conn,1,1
        										
        										
										                    'mccount = mcrs.recordcount
										                    if not mcrs.eof then
											                    set mccount = conn.execute("select count(tag) from tb_product_category  where tag=1 and  is_view_menu=1  and menu_pre_serial_no="&menu_child_id &" order by menu_child_order asc")
											                    mcount = mccount(0) 
											                    set mccount = nothing
                                                    
                                                                    if CidParentID = menu_child_id then 
                                                                        leftMenuString = leftMenuString & ("<div name='menu_left_child' id='menu_left_child_"&menu_child_id&"'>" &vblf)
                                                         
                                                                    else
                                                                        leftMenuString = leftMenuString & ("<div style='display:none;' name='menu_left_child' id='menu_left_child_"&menu_child_id&"'>" &vblf)
                                                                    end if
											        
											                    dim mcrs_name 
											                    dim iii
											                    iii = 0
											                    leftMenuString = leftMenuString &  ""
											                    do while not mcrs.eof
											                    iii = iii+1
												                    mcrs_name  = trim(mcrs("menu_child_name"))
        												
												                    if iii = mcount then 
													                    leftMenuString = leftMenuString &  "<div class='page_left_menu_end_btn1'>" &vblf
												                    else 
													                    leftMenuString = leftMenuString &  "<div class='page_left_menu_end_btn2'>" &vblf
												                    end if
        											   
												                    leftMenuString = leftMenuString & ("<a href=""/site/Product_list.asp?page_category="& mcrs("page_category")&"&class="&left_title_count&"&cid="&mcrs("menu_child_serial_no")&""" onclick=""TransferList("& mcrs("page_category")&", "&left_title_count&", "&mcrs("menu_child_serial_no")&", this ); return false;"" class=""hui_orange_11"" id=""item_id_"&mcrs("menu_child_serial_no")&""">")
														
												                    if len(mcrs_name) >26 then 
													                    leftMenuString = leftMenuString & (left(mcrs_name,26))
												                    else
													                    leftMenuString = leftMenuString &  mcrs_name
												                    end if
												                    leftMenuString = leftMenuString &  "</a>" &vblf
												                    ' add manage button
												                    if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" and mcrs("page_category")=1 then 
													                    leftMenuString = leftMenuString &  "&nbsp;<a href=""/part_showit_manage.aspx?categoryID="& mcrs("menu_child_serial_no") &""" onclick=""js_callpage_cus(this.href, 'right_manage', 1000,800);return false;"">M</a>"										
												                    end if
        												
												                    leftMenuString = leftMenuString &  "</div> "
											                    mcrs.movenext : loop 
											                    leftMenuString = leftMenuString &  "</div>"
										                    end if
										                    mcrs.close :set mcrs  = nothing
									                    'end if
								                    end if
										
								                    leftMenuString = leftMenuString & ("</a>")
										
								                    leftMenuString = leftMenuString & ("</li>")

                                    end if
									    rs1.movenext
								    loop
								    rs1.close
        		
					    leftMenuString = leftMenuString & (" </ul>")
					    leftMenuString = leftMenuString & ("</div>")                 
        	
			rs.movenext
			loop
			rs.close : set rs = nothing		

             Set objFSO = CreateObject("Scripting.FileSystemObject")
             set f = objFSO.OpenTextFile(Server.MapPath("/site/leftMenu.htm"), 2, true) 

             f.write(leftMenuString)
             
             f.close : set f = nothing
             set objFSO = nothing
        end if



            response.Write leftMenuString 	
			
%>

<script type="text/javascript">
	var showMenuId= '<% if SQLescape(request("class")) = "" then response.write  default_view else response.write SQLescape(request("class"))%>';
	onclickMenuParent('menu_left_parent_'+showMenuId , 'menu_left_item_' +showMenuId);
	
	onclickMenuParent('menu_left_parent_214' , 'menu_left_item_214');
	onclickMenuParent('menu_left_parent_2', 'menu_left_item_2');
	$('#menu_left_parent_214').css("display","");
	$('#menu_left_item_214').attr("class", "page_left_menu_parent_sub1");
	$('#menu_left_parent_2').css("display","");
	$('#menu_left_item_2').attr("class", "page_left_menu_parent_sub1");
	$('#menu_left_parent_52').css("display","");
	$('#menu_left_item_52').attr("class", "page_left_menu_parent_sub1");

	$('#item_id_<%= request("cid") %>').css("color", "red");
	if ($('#item_id_<%= request("cid") %>').parent().parent().attr("id") != undefined) {
	    if ($('#item_id_<%= request("cid") %>').parent().parent().attr("id").indexOf("child") > -1) {
	        $('#item_id_<%= request("cid") %>').parent().parent().css("display", "");
	    } 
	}
</script>
<!-- end left menu -->
<!-- top 10 begin -->
<span id="page_top_10_area"></span>
<!-- top 10 end-->
<!-- view begin -->
<span class="left_view_watch_history"></span>
<!-- view end 	-->