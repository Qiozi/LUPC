<!--#include virtual="site/inc/inc_helper.asp"-->


<%

	Dim			ids 		:		ids 		= 	SQLescape(request("ids"))
	Dim 		ids_g		:		ids_g		=	null
	Dim 		cmd 		:		cmd 		= 	SQLescape(request("cmd"))
	

	if len(ids)>0 and cmd = "ebay" then
		if(instr(ids, ";")>0) then
			ids_g = replace(ids, ";", ",")
		else
			ids_g = ids
		end if
		
		ids_g = replace(replace(ids_g, "]", ""), "[", "")
		
		Set rs = conn.execute("select case when p.other_product_sku >0 then p.other_product_sku else esp.luc_sku end as img_sku, ebay_system_current_number"&_
								" ,ebay_system_name "&_
								" ,ebay_system_price"&_
								" from tb_ebay_system_parts esp"&_
								" inner join tb_ebay_system_part_comment espc on espc.id=esp.comment_id"&_
								" inner join tb_ebay_system es on es.id=esp.system_sku "&_
								" inner join tb_product p on p.product_serial_no = esp.luc_sku"&_
								" where es.showit=1 and es.id in ("& ids_g &") and espc.is_case = 1")
		if not rs.eof then
				Response.write "<table  border=""0"" cellpadding=""0"" cellspacing=""0""  height=""28"" style=""margin-top:5px;"">"&vblf
				Response.write "	<tbody>"&vblf
				Response.write "		<tr>"&vblf
				Response.write "			<td> <img src=""/soft_img/app/title2_01.gif"" alt="""" width=""19"" height=""28""></td>"&vblf
				Response.write "			<td style=""background: url(/soft_img/app/title2_02.gif); text-align:center"" width=""123"" class='history_title'><h3>HISTORY</h3></td>"&vblf
				Response.write "			<td> <img src=""/soft_img/app/title2_03.gif"" alt="""" width=""23"" height=""28""></td>"&vblf
				Response.write "		</tr>"&vblf
				Response.write "	</tbody>"&vblf
				Response.write "</table>"&vblf
				response.write "<div>"&vblf
				Response.write "<ul class='view_history_inSide'>"	&vblf
				'Response.write "	<li><h3> History </h3></li>"	&vblf
				Do while not rs.eof 
						Response.write 	"		<li style='text-align:center;'>"	&vblf
						Response.write 	"			<a href=""/ebay/system_view.asp?id="& rs("ebay_system_current_number") &""">"&vblf
						Response.write  "				<img src='"& GetImgMinURL(HTTP_PART_GALLERY, rs("img_sku")) &"' width='50' />"&vblf
						Response.write 	"			</a>"&vblf
						REsponse.write  "			<br/>["& rs("ebay_system_current_number") &"]<br/>"
						Response.write  " 				<span class='price1'>"&formatcurrency(ConvertDecimal(rs("ebay_system_price")),2)&"</span>"
						Response.Write  "				<span class='price_unit'>"& CCUN &"</span><br/>"	&vblf
						Response.write 	"			<a href=""/ebay/system_view.asp?id="& rs("ebay_system_current_number") &""">"&	rs("ebay_system_name") &"</a>" &vblf
						Response.write 	"		</li>"	&vblf
				rs.movenext
				loop
				Response.write "</ul>"	&vblf
				Response.write "</div>"&vblf
				Response.write "<script > $('li[title=history_title]').css(""background"", ""url('/soft_img/app/title_bg_yellow.gif')"").css(""color"", ""#ffffff"").css(""padding-left"",""10px"").css(""font-weight"", ""bold"");$('li[title=history_title]>h3').css(""font-weight"", ""bold"");</script>"
		end if
		rs.close : set rs = nothing		
	end if
	

closeconn()
%>