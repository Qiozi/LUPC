<!--#include virtual="site/inc/inc_helper.asp"-->

<%

	Dim			ids 		:		ids 		= 	SQLescape(request("ids"))
	Dim 		ids_g		:		ids_g		=	null
	Dim 		cmd 		:		cmd 		= 	SQLescape(request("cmd"))
	Dim 		cid			:		cid			= 	SQLescape(request("cid"))
	Dim 		is_sys_category:is_system_category= false

	if cmd = "ebay" then
		
		
		Set rs = conn.execute("select case when p.other_product_sku >0 then p.other_product_sku else esp.luc_sku end as img_sku, ebay_system_current_number"&_
								" ,ebay_system_name "&_
								" ,ebay_system_price "&_
								" from tb_ebay_system_parts esp"&_
								" inner join tb_ebay_system_part_comment espc on espc.id=esp.comment_id"&_
								" inner join tb_ebay_system es on es.id=esp.system_sku "&_
								" inner join tb_product p on p.product_serial_no = esp.luc_sku"&_
								" where es.showit=1 and  espc.is_case = 1 order by es.view_count desc limit 0,5")
		if not rs.eof then
				
				Response.write "	<div title='banner_hot'><h3> HOT </h3></div>"	&vblf
				Response.write "<ul class='view_hot_inSide'>"	&vblf
				Do while not rs.eof 
						Response.write 	"		<li>"	&vblf
						Response.write 	"			<div>"	&vblf
						
						Response.write 	"			<a href=""/ebay/system_view.asp?id="& rs("ebay_system_current_number") &"""><img src='"& GetImgCaseMiddle(HTTP_PART_GALLERY, rs("img_sku")) &"'  /></a>"	&vblf
						Response.write 	"			</div>"	&vblf
						Response.write  "		</li>"	&vblf
						Response.write 	"		<li>"	&vblf
						
						Response.write 	"			<a href=""/ebay/system_view.asp?id="& rs("ebay_system_current_number") &""">"&	rs("ebay_system_name")
						Response.write 	"<br><b>Price</b>: "&	ConvertDecimalUnit(Current_system, rs("ebay_system_price"))
						Response.write 	"</a>" &vblf
						Response.write 	"		</li>"	&vblf
						
				rs.movenext
				loop
				Response.write "</ul>"	&vblf
				
				Response.write "<script > $('div[title=banner_hot]').css(""background"", ""url('/soft_img/app/title_bg_yellow.gif')"").css(""color"", ""#ffffff"").css(""padding"",""5px"").css(""text-align"", ""center"").css(""border"",""1px solid white"").css(""margin"",""5px 0px 0px 0px"");$('div[title=banner_hot]>h3').css(""font-weight"", ""bold"");</script>"
		end if
		rs.close : set rs = nothing		
	
	else
	
		'
		'	是否是系统
		'
		Set rs = conn.execute("Select page_category from tb_product_category Where Menu_child_serial_no="& SQLquote(cid))
		If not rs.eof then
			if rs(0) = 0 then
				is_sys_category = true
			end if			
		End if
		rs.close 
		
		if is_sys_category then 										
				Set rs = conn.execute("Select st.* from tb_ebay_system st inner join tb_ebay_system_and_category ec on ec.SystemSku=st.id "&_
									"  where st.showit=1 and ec.eBaySysCategoryID="&SQLquote(cid) &" order by view_count desc limit 0,3 ")
				if not rs.eof then
						
						Response.write "	<div title='banner_hot'><h3> HOT </h3></div>"	&vblf
						Response.write "<ul class='view_hot_inSide'>"	&vblf
						Do while not rs.eof 
								Response.write 	"		<li>"	&vblf
								Response.write 	"			<div>"	&vblf
								
								Response.write 	"			<a href=""/site/system_view.asp?cid="& cid &"&id="& rs("id") &"""><img src='"& GetSystemPhotoByID2(rs("id")) &"'  /></a>"	&vblf
								Response.write 	"			</div>"	&vblf
								Response.write  "		</li>"	&vblf
								Response.write 	"		<li>"	&vblf
								
								Response.write 	"			<a href=""/site/system_view.asp?cid="& cid &"&id="& rs("id") &""">"&	rs("ebay_system_name")
								Response.write  "<br ><span class='text_blue_13'>["&rs("id")&"]</span>"&vblf
								Response.write 	"<br><b></b> "&	ConvertDecimalUnit(Current_system, rs("tmp_sell"))
								Response.write 	"</a>" &vblf
								Response.write 	"		</li>"	&vblf
								
						rs.movenext
						loop
						Response.write "</ul>"	&vblf
						
						Response.write "<script > $('div[title=banner_hot]').css(""background"", ""url('/soft_img/app/title_bg_yellow.gif')"").css(""color"", ""#ffffff"").css(""padding"",""5px"").css(""text-align"", ""center"").css(""border"",""1px solid white"").css(""margin"",""3px 0px 0px 0px"");$('div[title=banner_hot]>h3').css(""font-weight"", ""bold"");"
                        response.write "$('ul.view_hot_inSide div').css('width','138px');"
						Response.write "</script>"
				end if
				rs.close : set rs = nothing	
		else
		
				Set rs = conn.execute("Select product_serial_no, product_short_name,other_product_sku "&_
									" ,product_current_price-product_current_discount sell"&_
									" ,product_current_price "&_
									" ,pc.menu_pre_serial_no"&_
									" ,vt.c "&_
									" from tb_product  p"&_
									" inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no"&_
									" left join (select distinct luc_sku ,  sum(view_count) c from tb_part_cate_view_count where date_format(regdate,'%Y%j') >=date_format(date_sub(current_date, interval 3 day),'%Y%j') group by luc_sku) vt on vt.luc_sku=p.product_serial_no "&_
									"  where p.tag=1 "&_			
									"  and (p.product_store_sum >0 or p.ltd_stock >0) "&_				
									"  and p.menu_child_serial_no="&SQLquote(cid) &" order by vt.c desc limit 0,8 ")
				if not rs.eof then
						
						Response.write "	<div title='banner_hot'><h3> HOT </h3></div>"	&vblf
						Response.write "<ul class='view_hot_inSide'>"	&vblf
						Do while not rs.eof 
								Response.write 	"		<li>"	&vblf
								Response.write 	"			<div>"	&vblf
								
								Response.write 	"			<a href=""/site/product_parts_detail.asp?class="&rs("menu_pre_serial_no")&"&cid="& cid &"&id="& rs("product_serial_no") &"""><img width='50' src='"&  GetImgMinURL(HTTP_PART_GALLERY , PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku"))) &"'  /></a>"	&vblf
								
								'Response.write  "		</li>"	&vblf
								'Response.write 	"		<li>"	&vblf
								Response.write  " <br style='clear:both'>"
								Response.write 	"			<a href=""/site/product_parts_detail.asp?class="&rs("menu_pre_serial_no")&"&cid="& cid &"&id="& rs("product_serial_no") &""">"&	rs("product_short_name")
								Response.write 	"<br/>SKU: &nbsp;["& rs("product_serial_no") &"]"&vblf
								if cdbl(SQLescape(rs("product_current_price"))) = cdbl(SQLescape(rs("sell"))) then 
								Response.write 	"<br>"&	ConvertDecimalUnit(Current_system, rs("sell"))
								else
								Response.write 	"<br><span class='price_dis'>"& formatcurrency(	ConvertDecimal(rs("product_current_price")),2) &"</span>"
								Response.write 	"<br>"&	ConvertDecimalUnit(Current_system, rs("sell"))
								end if
								Response.write 	"</a>" &vblf
								Response.write 	"			</div>"	&vblf
								Response.write 	"		</li>"	&vblf
								
						rs.movenext
						loop
						Response.write "</ul>"	&vblf
						
						Response.write "<script > $('div[title=banner_hot]').css(""background"", ""url('/soft_img/app/title_bg_yellow.gif')"").css(""color"", ""#ffffff"").css(""padding"",""5px"").css(""text-align"", ""center"").css(""border"",""1px solid white"").css(""margin"",""3px 0px 0px 0px"");$('div[title=banner_hot]>h3').css(""font-weight"", ""bold"");"
                        response.write "$('ul.view_hot_inSide div').css('width','142px');"
                        response.write "$('ul.view_hot_inSide>li').css('padding','2px');"
                        response.write "</script>"
				end if
				rs.close : set rs = nothing	
		End if
	end if
	

closeconn()
%>