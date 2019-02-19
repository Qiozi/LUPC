<div style="background:url('/soft_img/app/title_bg_green.gif'); " title="right_search_area"  class="page_right_area">
	Search
</div>
<ul style="padding: 10px 5px 10px 5px ; border: 1px solid #A7AAAB; margin-top: 2px; background:#EBEEEE;">
    <li> 
    	<form name="form1_Search" method="get" action="<%= LAYOUT_HOST_URL%>search.asp?class=Search">
    	<ul>
        	<li style="float:left; padding-top: 2px; padding-right: 2px;">        
        		<input style="padding-left:4px; " name="keywords" type=text class="b" id="keywords" onFocus="if (keywords.value=='Search') {keywords.value='';}" onBlur="if (keywords.value==''){keywords.value='Search';}" value="Search" size=15>
            </li>
            <li style="float:left;">
                <input name="imageField3" type="image" src="/soft_img/app/go4.gif" border="0">
            </li>
        </ul>
        </form>
    </li>
    <li style="clear:both;COLOR: #4C4C4C; letter-spacing:0px; font-size:8pt">
   		Enter keywords, sku#, part#
    </li>
</ul>

<div style="text-align:center; padding:2px;margin-top:3px; margin-bottom:3px;background:#fff;border:1px solid #cc9900;">
	<div style="border:1px solid #ccc;padding:5px; background:#EBEEEE">
	<a href='http://www.lucomputers.com/site/product_list.asp?page_category=0&class=52&cid=288'><img src='http://www.lucomputers.com/ebay/banner/Banner_Gaming_batmanarkhamcity.jpg' border="0"/></a>
    </div>
</div>

<div style="text-align:center; padding:2px;margin-top:3px; margin-bottom:3px;background:#fff;border:1px solid #cc9900;">
	<div style="border:1px solid #ccc;padding:5px; background:#ffcc66;">
	<a href='/site/p_sale.asp'><img src='/soft_img/app/left_41.gif' border="0" width="130" /></a>
    </div>
</div>

<div class="view_hot_area" style="width:100%;"></div>
<span id='page_right_view_adv'></span>
<%
	Dim current_page_name 
	Dim right_content_all
	current_page_name	=	getCurrentPageName()
	if current_page_name <> "product_list.asp" and current_page_name <> "product_parts_detail.asp" and current_page_name<> "product_detail.asp" and current_page_name<> "list.asp" then 
		set rs = conn.execute("select right_id from tb_right where right_page='"& current_page_name &"'")
		if rs.eof then
			set rs = conn.execute("select right_id from tb_right where right_page='default'")
		end if
		
		if not rs.eof then 
		'Response.Write(rs(0))
			right_content_all = ReadAdvFile(rs(0), "right")
			
			response.Write "<script>"&vblf
			Response.write "	$('#page_right_view_adv').load('"& right_content_all &"?'+rand(1000));"&vblf
			Response.Write "</script>"&vblf
			'Response.write "	$('#page_right_view_adv').load('"& right_content_all &"');"&vblf
		
		end if
		rs.close : set rs = nothing
		
		response.Write "<script>"&vblf
		Response.write "	$('div.view_hot_area').remove();"&vblf
		Response.Write "</script>"&vblf
	else
		if CurrentIsEbay then 
			Response.write "<script>getViewHot('ebay', '"&request("cid")&"');</script>"
		else
			Response.write "<script>getViewHot('site', '"&request("cid")&"');</script>"
		end if
	end if

	if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" then 
		dim params_id
		params_id = ""
		if current_page_name = "product_list.asp" then 
			params_id = "&id="&request("cid")
		end if 
		if current_page_name = "product_parts_detail.asp" or current_page_name="product_detail.asp" then 
			params_id = "&id="&request("parent_id")
		end if%>
		<a href="/right_manage.aspx?page=<%=current_page_name & params_id%>" onclick="js_callpage_cus(this.href, 'right_manage', 800,600);return false;">Manage</a>
 	
<% end if %>