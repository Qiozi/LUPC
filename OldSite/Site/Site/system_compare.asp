<!--#include virtual="site/inc/inc_page_top.asp"-->
<%
	dim ids,idss
	ids = SQLescape(request("ids"))
	'response.Write(ids)
	if left(ids,1) = "|" then ids = right(ids,len(ids)-1)
	if (right(ids,1) = "|") then ids = left(ids, len(ids)-1)
	IF INSTR(IDS, "|") >0 THEN 
		ids = replace(ids,"|", ",")
	End if
	'response.Write(ids)
%><style>
	#main tr td { background:#fff}
	#main thread tr td { background:#f2f2f2}

</style>
<table width="960"  border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td width="947" height="100" valign="top" bgcolor="#CDE3F2" style="border-left:#8E9AA8 1px solid; border-right:#8E9AA8 1px solid; border-bottom:#8E9AA8 1px solid;">
    	<table width="100%"  border="0" cellspacing="0" cellpadding="0"  align="left">
        <tr>
          
          <td width="947" valign="top">
          	<div style="cursor:pointer; padding-left:10px; background:#fff; line-height:20px" onclick="window.history.go(-1);"><span class="nav1"><a >back</a></span></div>
          	<table id="main" cellspacing="1" cellpadding="2" style="clear:both">
            
            	<%
					Dim rebate_save_price
					
					if len(ids)>0 then 
						
						set rs = conn.execute("select ebay_system_name from tb_ebay_system where id in ("& ids &")")
						if not rs.eof then
							response.Write("<thead><tr><td>&nbsp;</td>")
							do while not rs.eof
								
								response.Write("<td style='background:#f2f2f2'>"&rs(0)&"</td>")							
								
							rs.movenext						
							loop
							response.Write("</tr></thead>")
						end if
						rs.close 
						
						idss = split(ids,",")
						response.Write("<td>&nbsp;</td>")
						for i=lbound(idss) to ubound(idss)
							'if i <> 0 then 
								if trim(idss(i)) <> "0" then
								  response.Write("<td style='text-align:center; font-weight:bold; color: #ff9900'> <span style='color:#000'><a href=""/site/system_view.asp?cid="&session("sys_cate_id")&"&class=1&id="&idss(i)&""">[" & idss(i) &"]</a></span>&nbsp;&nbsp;&nbsp;&nbsp;")
								  dim tmp_system_price, tmp_system_save_price, tmp_system_price_first
								  dim price_and_save
								   
								  rebate_save_price = 0 'FindSystemRebatePrice(rs("system_templete_serial_no"))
								  price_and_save = GetSystemPriceAndSave(trim(idss(i)))
								  
								  tmp_system_save_price = splitConfigurePrice(price_and_save,1)
								  tmp_system_price_first = splitConfigurePrice(price_and_save,0)						  
								  tmp_system_price= tmp_system_price_first - tmp_system_save_price
								 
								  if tmp_system_save_price <> 0 or rebate_save_price <> 0 then 		
										response.write "<span class='Original_price' >" & formatcurrency(cdbl(tmp_system_price_first), 2) &"</span>&nbsp;&nbsp;"
								  end if
								  
								  if isnumeric(tmp_system_price) then 
									if rebate_save_price > 0 then 
										response.write rebate_comment2 & "&nbsp;" & formatcurrency(tmp_system_price - rebate_save_price, 2)
									else
										response.write formatcurrency(tmp_system_price, 2)
									end if
								  end if
								  response.Write(" <span class='price_unit'>"& CCUN &"</span></td>")
							'else
								
							end if
						next 
						
						set rs = conn.execute("select pg.part_group_id, part_group_name from tb_part_group pg inner join tb_product_category pc on pc.menu_child_serial_no=pg.product_category where pg.part_group_id in "&_
	" (select distinct part_group_id from tb_ebay_system_parts where system_sku in ("& ids &") ) "&_
	" order by menu_child_order,part_group_name asc ")
						if not rs.eof then
							dim x
							do while not rs.eof 
								x = x + 1
								response.Write("<tr ")
								if x mod 2 = 1 then response.Write( " style="" background:#E8E8FA; "" >") else response.Write( " > ")
								response.Write("<td style='text-align:right;padding-right:2px;color:#8080E6; font-weight: bold; '>"&rs (1)&"</td>")
									idss = split(ids,",")
									for i=lbound(idss) to ubound(idss)
										if trim(idss(i)) <> "0" then
											set srs = conn.execute("select product_short_name, p.product_serial_no, sp.part_quantity from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku where p.tag=1 and part_group_id='"& rs(0) &"' and system_sku='"& trim(idss(i)) &"' and p.product_serial_no not in("&LAYOUT_NONE_SELECTED_IDS&") ")
											if not srs.eof then
												response.Write "<td><a class=""hui-orange-s"" href=""/site/view_part.asp?id="& srs(1) &""" onClick=""javascript:js_callpage_cus(this.href, 'view_part', 620, 600);return false;"">"
												if srs("part_quantity") > 1 then response.write "<span style='color:blue'>"& srs("part_quantity") &"X </span>"
												Response.write  srs(0) 
												
												REsponse.write "</a></td>" 
											else
												response.Write("<td>&nbsp;</td>")
											end if
											srs.close : set srs = nothing
										end if
									next
								response.Write("</tr>")						
							rs.movenext
							loop
						end if 
						rs.close : set rs = nothing
					end if
				%>
              </table>
            </td>
         
        </tr>
    </table></td>
    <td valign="bottom" background="/soft_img/app/left_middle.gif" style="width: 13px"><img src="/soft_img/app/left_bt.gif" width="13" height="214"></td>
  </tr>
</table>
<%
session("is_compare") = true
closeconn()
%>
<script language="javascript">
function MergeCellsVertical()
{
	var tbl = document.getElementById("main");
	if (tbl.rows.length < 2) return;
	var i, j;
	var last = "";//tbl.rows(3).cells(0).innerHTML;
	var lastIndex = 0;	
	for (i = tbl.rows.length-1; i >1; i--)
	{		
		//alert(tbl.rows[i].cells[0].innerHTML);
		if (tbl.rows[i].cells[0].innerHTML == last && last != "")	// find new value.
		{		
				//alert(tbl.rows[i].cells[0].innerHTML);
				for (j = tbl.rows[0].cells.length-1; j >= 0; j--)
				{
					if (j == 0)
						tbl.rows[i].cells[j].innerHTML = tbl.rows[i+1].cells[j].innerHTML;
					else
						tbl.rows[i].cells[j].innerHTML += tbl.rows[i+1].cells[j].innerHTML;
					
				}
				if(document.all)
					tbl.rows[i+1].removeNode();		
				else
					tbl.rows[i+1].parentNode.removeChild(tbl.rows[i+1])	;
		}
		else
		{
			last = tbl.rows[i].cells[0].innerHTML;			
		}
	}
	//tbl.cellspacing = 0;
	//alert(tbl.rows.length);
}

MergeCellsVertical();


</script>
<!--#include virtual="/site/inc/inc_bottom.asp"-->
</body>
</html>

