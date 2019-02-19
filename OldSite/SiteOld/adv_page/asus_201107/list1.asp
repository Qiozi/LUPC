<!--#include virtual="site/inc/inc_page_top.asp"-->
    <style>
        .btn_middle{  background: url(/soft_img/app/customer_bottom_03.gif); text-align:center }

    </style>

<table style="width:965px; margin: auto auto;">
	<tr>
    	<td style="background:#ffffff;">
        	<%
				dim part_name, long_name
				set rs = conn.execute("Select *, case when other_product_sku>0 then other_product_sku else product_serial_no end as img_sku from tb_product where menu_child_serial_no=350 and tag=1  and product_name_long_en like '% S3-951 %'")
				if not rs.eof then
					do while not rs.eof 
						part_name = rs("product_name")
						long_name = rs("product_name_long_en")
						if instr(lcase(part_name), "eee")=0 and instr(lcase(part_name), "epc")=0 then 
							Response.write "<div name='part' onmouseover='onOver($(this));'  onMouseOut='onOut($(this));'>"
							Response.write "	<a href='http://www.lucomputers.com/site/product_parts_detail.asp?class=2&pro_class=90&id="& rs("product_serial_no") &"&cid=350' target='_blank'>"
							Response.write "	<ul><li>SKU:<b>"& rs("product_serial_no") &"</b></li>"
							response.write "		<li style='font-size:10pt;'>"& long_name & "</li>"
							response.write "		<li><img src='http://www.lucomputers.com/pro_img/COMPONENTS/"& rs("img_sku") &"_list_1.jpg' width='150'></li>"
							response.write "		<li style='text-align:right;'>$"& rs("product_current_price") &"</li>"
							response.write "	</ul>"
							Response.write "	</a>"
							Response.write "</div>"
						end if
					rs.movenext
					loop
				end if
				rs.close : set rs  = nothing
			%>
        
        </td>
    </tr>
</table>
<%closeconn()%>
<script type="text/javascript">
	$().ready(function(){
		$('div[name=part]').css({"border-bottom":"1px solid #f2f2f2","padding":"4px", "float":"left", "width":"225px", "height":"250px"});
		$('a').bind("onmouseover", "onOver");
	});
	function onOver(e){
		e.css("border-bottom", "1px solid #ff9900");
	}
	function onOut(e){
		e.css("border-bottom", "1px solid #f2f2f2");
	}
try{
_uacct = "UA-4447256-1";
urchinTracker();
}catch(e){}
</script>
</body>
</html>
