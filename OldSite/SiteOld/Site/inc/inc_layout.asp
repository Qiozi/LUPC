

<%
'
'
'   FindPageTopHtml()
'   FindPageContTopHTML()
'   GetImgMinURL(http, img_sku)
'   GetImgListUrl(http, img_sku, i)
'   GetImgBigUrl(http, img_sku, i)
'	GetImgCaseMiddle(http, img_sku)
'   WriteSystemBigImg(img_sku)
'	WriteSystemDetailList(id, is_ebay)
'
'	WritePartStoreSumAtSysList(sum)
'
'
'
'
'
'
'
'



'-------------------------------------------------------------------------------------------
Function FindPageTopHtml()
'-------------------------------------------------------------------------------------------
    dim rs 

    Response.write "<html xmlns=""http://www.w3.org/1999/xhtml"">" & Vblf
    Response.write "    <head>" &vblf
    Response.write      LAYOUT_CONTENT_TYPE &vblf
    if instr(Request.ServerVariables("URL"), "product_parts_detail.asp")>0 then 
        Response.write "    <title>LU Computers | "
        set rs = conn.execute("select pc.menu_child_name,p.manufacturer_part_number, p.product_short_name from tb_product p inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where p.product_serial_no='"& SQLescape(request("id")) &"'")     
        if not rs.eof then
            response.Write  rs("menu_child_name") & " | "& rs("manufacturer_part_number") & " | " & rs("product_short_name") & "</title>"&vblf  
            Response.write "    <meta itemprop=""brand"" content="""& rs("menu_child_name")&"""/>"
        else
            response.Write "Laptop, Tablet, CPU, Motherboard, Video Card, Memory, Hard Drive</title>"&vblf  
        end if
        rs.close : set rs = nothing
    else
        Response.write "    <title>LU Computers | Laptop, Tablet, CPU, Motherboard, Video Card, Memory, Hard Drive</title>"&vblf  
    end if
    

    Response.write "    <META NAME=""ROBOTS"" CONTENT=""ALL"">"&vblf  
    REsponse.Write "    <META NAME=""GOOGLEBOT"" CONTENT=""ALL"">"&vblf  
    
    Response.write      LAYOUT_DESCRIPTION &vblf  
    Response.write      LAYOUT_KEYWORDS &vblf
    Response.write      LAYOUT_CSS_FILES &vblf
    Response.write      LAYOUT_SCRIPT_FILES &vblf
    Response.write "    </head>"    &vblf
    Response.write "    <body>  "&vblf

End Function



'-------------------------------------------------------------------------------------------
Function FindPageContTopHTML()
'-------------------------------------------------------------------------------------------
        FindPageContTopHTML = "<a name=""page_top""></a><a name=""top""></a>"
        FindPageContTopHTML = FindPageContTopHTML & "<div class=""div_center"">"& Vblf  
        FindPageContTopHTML = FindPageContTopHTML & "<div id=""page_top_flash_area"">" 
        FindPageContTopHTML = FindPageContTopHTML & "</div>"

        
        FindPageContTopHTML = FindPageContTopHTML & "<div class=""page_top_main_item"" style=""border:0px solid red;"">" & Vblf
        FindPageContTopHTML = FindPageContTopHTML & "<div style=""float:left; text-align:center; border:0px solid red; width:600px;"" id=""page_top_btn_list"">" & Vblf
		FindPageContTopHTML = FindPageContTopHTML & "<a href="""& LAYOUT_HOST_URL &"sys_transition_ebay.asp""></a>" & Vblf
		'FindPageContTopHTML = FindPageContTopHTML & "<a href=""/sys_transition.asp"" class=""at_ebay_in"" id='page_top_country_selected'></a>" & Vblf
       ' FindPageContTopHTML = FindPageContTopHTML & "<a href="""& LAYOUT_HOST_URL &"p_game.asp"" class=""blue_white_11"">Gaming</a>" & Vblf
        'FindPageContTopHTML = FindPageContTopHTML & "<b>|</b> <a href="""& LAYOUT_HOST_URL &"p_home.asp"" class=""blue_white_11"">Home &amp; Home Office</a>" & Vblf
        FindPageContTopHTML = FindPageContTopHTML & "<a href="""& LAYOUT_HOST_URL &"p_business.asp"" class=""blue_white_11"">Business</a>" & Vblf
        'FindPageContTopHTML = FindPageContTopHTML & "<b>|</b> <a href="""& LAYOUT_HOST_URL &"p_media.asp"" class=""blue_white_11"">Media Center</a>" & Vblf
        'FindPageContTopHTML = FindPageContTopHTML & "<b>|</b> <a href="""& LAYOUT_HOST_URL &"p_new.asp"" class=""blue_white_11"">New Products</a>" & Vblf
        FindPageContTopHTML = FindPageContTopHTML & "<b>|</b> <a href="""& LAYOUT_HOST_URL &"p_sale.asp"" class=""blue_white_11"">On Sale</a>" & Vblf
        FindPageContTopHTML = FindPageContTopHTML & "<b>|</b> <a href="""& LAYOUT_HOST_URL &"p_rebate.asp"" class=""blue_white_11"">Rebate Center</a>" & Vblf
        FindPageContTopHTML = FindPageContTopHTML & "</div>" & Vblf
        FindPageContTopHTML = FindPageContTopHTML & "<div class=""topSearchArea""><input type=""text"" value='' size='26' placeholder='Enter keywords, sku#, part# '><img src='/soft_img/app/top-search-btn.png'  id='top_search_btn_img' onmouseout=""this.src='/soft_img/app/top-search-btn.png';"" onmouseover=""this.src = '/soft_img/app/top-search-btn2.png'""></div>"&vblf
        'FindPageContTopHTML = FindPageContTopHTML & "<div style=""float:left;width:122px;height:15px;font-size: 8pt; border: 0px solid red;text-align:right;""><span id='page_top_username'></span></div>" &vblf
        FindPageContTopHTML = FindPageContTopHTML & "<div style=""float:left;width:38px; text-align:right; border: 0px solid red; ""><img src=""/soft_img/app/nav_right2.jpg"" width=""38"" height=""30"" id='page_right_sollt'></div>"&vblf
        FindPageContTopHTML = FindPageContTopHTML & "</div>" & Vblf
        
        FindPageContTopHTML = FindPageContTopHTML & "</div>" & Vblf
        
        if Current_system = CSCA Then 
			FindPageContTopHTML = FindPageContTopHTML & "<script type=""text/javascript"">" & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "    $('#page_top_flash_area').flash({" & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "        src: '/flash/top-2_CA.swf'," & Vblf
			'FindPageContTopHTML = FindPageContTopHTML & "        src: '/flash/top_chres.swf'," & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "       width:  960," & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "       height: 128});        " & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "</script>" & Vblf
		
		Else 
			FindPageContTopHTML = FindPageContTopHTML & "<script type=""text/javascript"">" & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "    $('#page_top_flash_area').flash({" & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "        src: '/flash/top-2_US.swf'," & Vblf
			'FindPageContTopHTML = FindPageContTopHTML & "        src: '/flash/top_chres.swf'," & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "       width:  960," & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "       height: 128});        " & Vblf
			FindPageContTopHTML = FindPageContTopHTML & "</script>" & Vblf

		End if
End Function



'-------------------------------------------------------------------------------------------
Function GetImgMinURL(http, img_sku)
'-------------------------------------------------------------------------------------------
    GetImgMinURL = http & cstr(img_sku) &"_t.jpg"
End Function



'-------------------------------------------------------------------------------------------
Function GetImgListUrl(http, img_sku, i)
'-------------------------------------------------------------------------------------------
    GetImgListUrl   =   http & img_sku  &"_list_"&i&".jpg"
End Function



'-------------------------------------------------------------------------------------------
Function GetImgBigUrl(http, img_sku, i)
'-------------------------------------------------------------------------------------------
    GetImgBigUrl    =   http & img_sku  &"_g_"&i&".jpg"
End Function



'-------------------------------------------------------------------------------------------
Function GetImgCaseMiddle(http, img_sku)
'-------------------------------------------------------------------------------------------
    GetImgCaseMiddle = http & img_sku &"_system.jpg"
End Function




'-------------------------------------------------------------------------------------------
Function WriteSystemBigImg(img_sku)
'-------------------------------------------------------------------------------------------
    Dim rs  :   rs = null
    Dim img_qty :   img_qty =   1
    Dim other_sku   :   other_sku = 0
    Set rs = conn.execute("select product_img_sum, other_product_sku, img_url from tb_product where product_serial_no='"& img_sku &"'")
    if not rs.eof then  
            if SQLescape(rs("product_img_sum")) <> "" then
                    img_qty = clng(rs("product_img_sum"))
            end if
            
            if SQLescape(rs("other_product_sku")) <> "" then
                other_sku = clng(rs("other_product_sku"))
            end if
            
            if(other_sku = 0)then
                other_sku = img_sku
            end if
            
            Response.write "<div  >"   &vblf

           
            Response.write "    <a  onClick=""javascript:popImage($('#case_big_image').val(),'Lu Computers','middle_center',true,true);return false;"">"	&vblf			
            if len(rs("img_url")) < 6 or isnull(rs("img_url")) then 		  
			Response.write ("		  <img src="""&HTTP_PART_GALLERY & other_sku & "_list_1.jpg"" width='280' name=""product_image_list_area""  border=""0"" id=""product_image_list_area"" />")     &vblf
             Response.write "    <input type='hidden' id='case_big_image' value='"&GetImgBigUrl( HTTP_PART_GALLERY, other_sku , 1)&"'>"  &vblf
            else
            Response.write ("		  <img src="""& rs("img_url") &""" width='280' name=""product_image_list_area""  border=""0"" id=""product_image_list_area"" />")     &vblf
             Response.write "    <input type='hidden' id='case_big_image' value='"&rs("img_url")&"'>"  &vblf
            end if
            Response.write "    </a>"       &vblf

            Response.write "</div>"     &vblf
            Response.write "<div  class=""big_btn_imgs"" style='margin-top:10px;'>"  &vblf
            Response.write "    <ul>"   &vblf
            for i=1 to img_qty
                    
                    'test
                    'other_sku=4136
	                'response.write "<img src=""/soft_img/app/"& i& ".gif"" width=""20"" height=""11"" style=""cursor:pointer;"" onclick=""changeProductImage('"&HTTP_PART_GALLERY & other_sku & "_list_"&i &".jpg', '"&product_image_url & case_sku& "_g_"&i &".jpg');"">"
                    Response.write "        <li class='img_t_list'>"   &vblf
                    Response.write "            <img src="""&HTTP_PART_GALLERY & other_sku & "_list_"&i&".jpg"" width=""40"" border=""0"" onmouseover=""changeProductImage('"&HTTP_PART_GALLERY & other_sku & "_list_"&i &".jpg', '"&HTTP_PART_GALLERY & other_sku & "_g_"&i &".jpg');"" onclick=""changeProductImage('"&HTTP_PART_GALLERY & other_sku & "_list_"&i &".jpg', '"&HTTP_PART_GALLERY & other_sku & "_g_"&i &".jpg');"">"      &vblf
                    Response.write "        </li>"  &vblf
            next
			Response.write "    </ul>"   &vblf
            Response.write "</div>"     &vblf
            
    end if
    rs.close : set rs = nothing

End Function



'-------------------------------------------------------------------------------------------
Function WriteSystemDetailList(id, is_ebay)
'-------------------------------------------------------------------------------------------
    Dim     rs      :   rs  =   null
    Dim     s       :   s   =   s
    
    If is_ebay then
            Set rs = conn.execute("select es.part_quantity,ec.comment, p.product_ebay_name, p.product_serial_no"&_
                                  " from tb_ebay_system_parts  es"&_
                                  " inner join tb_ebay_system_part_comment ec on ec.id=es.comment_id "&_
                                  " inner join tb_product p on p.product_serial_no=es.luc_sku"&_
                                  " where system_sku='"& id &"'"&_
                                  " order by ec.priority asc")
            if not rs.eof then
                    s   =   "<table title=""system_part_list"" cellspacing=""0"" id=""system_part_list"">"   &vblf
                    do while not rs.eof
                        s   =   s&  "   <tr title='part'>"   &vblf
                        s   =   s&  "       <td><b>"& rs("comment") &"</td>"   &vblf
                        s   =   s&  "       <td>"& rs("product_ebay_name")  &"</td>"    &vblf
                        s   =   s&  "       <td>x"& rs("part_quantity") &"</td>"    &vblf
                        s   =   s&  "   </tr>"  &vblf
                    rs.movenext
                    loop
                    s   =   s& "</table>"  &vblf
            end if
            rs.close : set rs = nothing
	else
			Set rs = conn.execute("select p.product_name"&_
							" ,pg.part_group_name"&_
							" , p.product_serial_no"&_
							" , p.product_current_price"&_
							" , part_quantity"&_
							"  from tb_ebay_system_parts sp "&_
							" 	inner join tb_product p on sp.luc_sku=p.product_serial_no "&_
							" 	inner join tb_part_group pg on sp.part_group_id=pg.part_group_id "&_
							" 	inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no "&_
							"  where sp.system_sku="&id&" and p.tag=1 and (p.is_non=0 or p.product_name like '%onboard%' or  p.product_name like '%default basic fan%') order by  sp.id asc ")
            if not rs.eof then
                    s   =   "<table title=""system_part_list"" cellspacing=""0"" id=""system_part_list"">"   &vblf
                    do while not rs.eof
                        s   =   s&  "   <tr title='part'>"   &vblf
                        s   =   s&  "       <td><b>"& rs("part_group_name") &"</td>"   &vblf
                        s   =   s&  "       <td>"& rs("product_name")  &"</td>"    &vblf
                        s   =   s&  "       <td>x"& rs("part_quantity") &"</td>"    &vblf
                        s   =   s&  "   </tr>"  &vblf
                    rs.movenext
                    loop
                    s   =   s& "</table>"  &vblf
            end if
            rs.close : set rs = nothing
    end if
    WriteSystemDetailList = s
    
End Function



'
'	change https to http
'
function redirectHTTP(current_page_name)
	dim httpPages
	httpPages = "[default.asp][product_list.asp][product_parts_detail.asp][product_detail.asp][system_view.asp]"
	if instr(httpPages, "["& lcase(current_page_name) &"]") > 0  then 
		if lcase(Request.ServerVariables("HTTPS")) = "on" then 		
			response.Redirect(GetCurrentUrl())
			response.End()
		end if
	end if
end function 

Function GetCurrentUrl()     
 
   	Dim strTemp     
   	If LCase(Request.ServerVariables("HTTPS")) = "on" Then     
		strTemp = "http://"    
		strTemp = strTemp & Request.ServerVariables("SERVER_NAME")     
		'If Request.ServerVariables("SERVER_PORT") <> 80 Then strTemp = strTemp & ":" & Request.ServerVariables("SERVER_PORT")     
		strTemp = strTemp & Request.ServerVariables("URL")     
		If Trim(Request.QueryString) <> "" Then strTemp = strTemp & "?" & Trim(Request.QueryString)   
    End If 
   	GetCurrentUrl = strTemp     
End Function 



function WritePartStoreSumAtSysList(sum)
	
	if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" then 
		if VEmptyNull(sum) = "" then 
			sum = 0
		end if
		
		if cint(sum)>0 then
			WritePartStoreSumAtSysList = "<span style='color:blue'>["& sum &"]</span>"
		else
			WritePartStoreSumAtSysList = "<span style='color:red'>["& sum &"]</span>"
		end if
	else
		WritePartStoreSumAtSysList = ""
	end if
	
end function
 %>