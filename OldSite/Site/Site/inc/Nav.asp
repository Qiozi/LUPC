<%
' 
' 处理导航菜单
'
dim nav_images 

nav_images = ""'"&nbsp;&nbsp;<img src=""/soft_img/app/arrow_8.gif"" width=""11"" height=""10"">&nbsp;&nbsp;"

'
'	id: 查询的ID， 可以是产品ID， 也可以是类别ID
' 	is_product: 决定ID是产品ID，还是类别ID
'	category: 决定是产导航类型
'
function FindNav(id, is_product, category)
	FindNav = getHome()
	select case category
		case "part_product"
			FindNav = FindNav & GetPartProduct(id, is_product, -1)
		case "sys_product"
			FindNav = FindNav & GetSysProduct(id)
		
	end select	
end function

function FindPartNav(category_id)
	if len(SQLescape(category_id)) = 0 then 
	FindPartNav = ""
	End if
    FindPartNav = getHome()
    FindPartNav = FindPartNav & GetPartProduct(0, 0, category_id)
end function

' 首页连接
function getHome()
	getHome = nav_images & "<span class='nav1'><a href="""& LAYOUT_HOST_URL &""">Home</a></span>"
end function 


function GetSysProduct(id)
	GetSysProduct = ""
	dim rs
	Dim tmp_sql
	Dim chrs
	if id <>""then
		
		
		'if (category = 1) then 
			tmp_sql = "select pc.menu_child_serial_no, pc.page_category,pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc inner join tb_ebay_system_and_category st on pc.menu_child_serial_no =st.eBaySysCategoryID  where eBaySysCategoryID="& SQLquote(id)
		'elseif (category = 2) then 
'			tmp_sql = "select pc.menu_child_serial_no,pc.page_category, pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc where pc.menu_child_serial_no="&id 
'		end if
		
		set rs = conn.execute(tmp_sql)
		'response.write "select pc.menu_child_serial_no, pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc inner join tb_product p on p.menu_child_serial_no=pc.menu_child_serial_no where product_serial_no="&id
		if not rs.eof then 
			if (rs("menu_pre_serial_no")) <> 0 then 
				set crs = conn.execute("select menu_child_name,menu_child_serial_no,menu_pre_serial_no from tb_product_category where menu_child_serial_no="& rs("menu_pre_serial_no"))
				'response.write "select menu_child_name,menu_child_serial_no,menu_pre_serial_no from tb_product_category where menu_child_serial_no="& rs("menu_pre_serial_no")
				if not crs.eof then
					if crs("menu_pre_serial_no") <> 0 then 
						set chrs  = conn.execute("select  menu_child_name,menu_child_serial_no,menu_pre_serial_no from tb_product_category where menu_child_serial_no="& crs("menu_pre_serial_no"))
						'response.write "select  menu_child_name,menu_child_serial_no,menu_pre_serial_no from tb_product_category where menu_child_serial_no="& crs("menu_pre_serial_no")
						if not chrs.eof then 
							GetSysProduct = GetSysProduct & nav_images &  GetHrefByProductCategory(chrs("menu_child_serial_no"), getMenuChildName(chrs("menu_child_serial_no"))) & "</a>"
						end if
						chrs.close : set chrs = nothing
					
						
					end if
					GetSysProduct = GetSysProduct & nav_images & GetHrefByProductCategory(crs("menu_child_serial_no") , getMenuChildName(crs("menu_child_serial_no")))
					
				end if
				crs.close : set crs = nothing
			
			end if
			
			GetSysProduct = GetSysProduct & nav_images & "<span class='nav1'><a href="""&LAYOUT_HOST_URL&"product_list.asp?page_category="&rs("page_category")&"&class="&request("class")&"&cid="&rs("menu_child_serial_no")&"""> " & rs("menu_child_name") & "</a></span>"
		end if
		rs.close : set rs = nothing
	end if
	GetSysProduct = GetSysProduct & nav_images & "<span class=""nav1"">Item:&nbsp;"& id & "</span>"
end function


' part 产品连接
' category : 1 根据产品ID号查询， 
'			 2 根据类别ID号查询
function GetPartProduct(id, category, category_id)
	dim rs, crs, chrs, tmp_sql
	GetPartProduct = ""
	if id <>""then
		
		
		if (category = 1) then 
			tmp_sql = "select pc.menu_child_serial_no, pc.page_category,pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc inner join tb_product p on p.menu_child_serial_no=pc.menu_child_serial_no where product_serial_no='"& id &"'"
		elseif (category = 2) then 
			tmp_sql = "select pc.menu_child_serial_no,pc.page_category, pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc   where pc.menu_child_serial_no='"&id&"'" 
		elseif (len(category_id)>0) then
		    tmp_sql = "select pc.menu_child_serial_no,pc.page_category, pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc   where pc.menu_child_serial_no='"&category_id &"'"
		else
			tmp_sql = "select pc.menu_child_serial_no,pc.page_category, pc.menu_pre_serial_no,pc.menu_child_name, pc.menu_parent_serial_no from tb_product_category pc   where pc.menu_child_serial_no='"&category_id &"'"
		end if
		
		set rs = conn.execute(tmp_sql)

		if not rs.eof then 
			if (rs("menu_pre_serial_no")) <> 0 then 
				set crs = conn.execute("select menu_child_name,menu_child_serial_no,menu_pre_serial_no from tb_product_category where menu_child_serial_no="& SQLquote(rs("menu_pre_serial_no")))
				if not crs.eof then
					if crs("menu_pre_serial_no") <> 0 then 
						set chrs  = conn.execute("select  menu_child_name,menu_child_serial_no,menu_pre_serial_no from tb_product_category where menu_child_serial_no="& SQLquote(crs("menu_pre_serial_no")))
						if not chrs.eof then 
							GetPartProduct = GetPartProduct & nav_images &  GetHrefByProductCategory(chrs("menu_child_serial_no"), getMenuChildName(chrs("menu_child_serial_no"))) & "</a>"
						end if
						chrs.close : set chrs = nothing				
					end if
					GetPartProduct = GetPartProduct & nav_images & GetHrefByProductCategory(crs("menu_child_serial_no") , getMenuChildName(crs("menu_child_serial_no")))
				end if
				crs.close : set crs = nothing
			end if
			GetPartProduct = GetPartProduct & nav_images & "<span class='nav1'><a href=""/site/product_list.asp?page_category="&rs("page_category")&"&class="&request("class")&"&cid="&rs("menu_child_serial_no")&"""> " & rs("menu_child_name") & "</a></span>"
		end if
		rs.close : set rs = nothing
	end if
end function
'
' 一级类别连接路径
function GetHrefByProductCategory(ID, CateName)
	GetHrefByProductCategory = "<span class='nav1'><a href='/site/product_category.asp?class="& request("class") &"&cid="& ID &"'> " & CateName & " </a></span>"
end function 

function GetHrefByProductCategory2(ID, CateName)
	GetHrefByProductCategory2 = "<span class='nav1'><a href='/site/product_category.asp?class="& request("class") &"&cid="& ID &"' style='color:#333333;'> " & CateName & " </a></span>"
end function 
%>