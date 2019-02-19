<!--#include virtual="site/inc/inc_page_top.asp"-->

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" style="width:770px;" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'>On Sale</span>
                </div>
            	<div id="page_main_area">               
					
                        <table width="100%" height="750" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                          <tr>
                            <td style="border:#E3E3E3 1px solid;  padding-top:0px;height: 900px" valign="top">
                              <table width="100%"  border="0" cellpadding="2" cellspacing="1" bgcolor="#E7E7E7">
                                <tr bgcolor="#CDE3F2">
                                  <td width="5%" height="20" align="center"  style="padding-bottom:4px;">Sku#  <br></td>
                                <td align="center"  style="padding-bottom:4px;">Description </td>
                                  <td width="7%" align="center"  style="padding-bottom:4px;">Original </td>
                                  <td width="7%" align="center"  style="padding-bottom:4px;">Sale</td>
                                  <td width="7%" align="center"  style="padding-bottom:4px;">Save</td>
                                  <td width="10%" align="center"  style="padding-bottom:4px;">Start - End</td>
                                <td width="7%" align="center"  style="padding-bottom:4px;">Buy</td>
                                </tr>
                                <%
                                    dim part_category_id,tmp_category_id,temp_sql
                                    part_category_id = 0
                                    		
                                    set rs = conn.execute("select os.*,(os.save_price + ifnull(sp.save_cost,0) ) save_cost, p.menu_child_serial_no, pc.menu_pre_serial_no,p.product_short_name, p.product_name, (p.product_current_price + ifnull(sp.save_cost,0)) product_current_price"&_
                                    " , concat( date_format(os.begin_datetime, '%m/%d/'), '-', date_format(os.end_datetime, '%m/%d/%Y')) cdate"&_
									" ,os.begin_datetime begin_date, os.end_datetime end_date from tb_on_sale os "&_
									" inner join tb_product p on p.product_serial_no=os.product_serial_no "&_
									" inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no "&_
									" left join tb_sale_promotion sp on p.product_serial_no = sp.product_serial_no "&_
									" and date_format(now(),'%Y%m%d') >= date_format(sp.begin_datetime, '%Y%m%d') "&_
									" and date_format(now(),'%Y%m%d') <=date_format(sp.end_datetime, '%Y%m%d') "&_
									" where (date_format(now(),'%Y%j') between date_format(os.begin_datetime,'%Y%j') and date_format(os.end_datetime,'%Y%j')) and p.tag=1 and pc.tag=1  "& temp_sql &" order by pc.menu_child_order,p.producter_serial_no  asc ")
                                    
                                    if not rs.eof then
                                        do while not rs.eof 
                                        if cint(rs("menu_pre_serial_no")) > 10 then 
                                            tmp_category_id  = cint(rs("menu_pre_serial_no"))
                                        else
                                            tmp_category_id  = cint(rs("menu_child_serial_no"))
                                        end if
                                        
                                        if (tmp_category_id <> part_category_id) then 
                                            part_category_id = tmp_category_id
                                        %>
                                            <tr>
                                              <td height="20" colspan="7" bgcolor="#efefef"  style="padding-bottom:4px;"><strong name='cateName'><%= getMenuChildName(tmp_category_id)%></strong></td>
                                            </tr>
                                        <% end if %>
                                        <tr>
                                        <td height="20" bgcolor="#FFFFFF"  style="padding-bottom:4px;text-align:center"> <%= rs("product_serial_no") %> </td>
                                        <td bgcolor="#FFFFFF"  style="padding-bottom:4px;"><a href="product_parts_detail.asp?class=2&pro_class=3&id=<%= rs("product_serial_no")%>&parent_id=<%= rs("menu_child_serial_no")%>" class="hui-orange-12"><%= rs("product_short_name")%></a> </td>
                                          <td bgcolor="#FFFFFF"  style="padding-bottom:4px; text-align:right"><span style="text-decoration:line-through"><%= formatcurrency(ConvertDecimal(rs("product_current_price")),2)%></span></td>
                                          <td bgcolor="#FFFFFF"  style="padding-bottom:4px; text-align:right"><%= formatcurrency(ConvertDecimal(cdbl(rs("product_current_price"))-cdbl(rs("save_cost"))),2)%></td>
                                          <td bgcolor="#FFFFFF"  style="padding-bottom:4px; text-align:right"><%= ConvertDecimalUnit(Current_system,rs("save_cost"))%></td>
                                          <td bgcolor="#FFFFFF" width="10%" style="padding-bottom:4px; text-align:right" nowrap="true"><%'= rs("cdate")%>
                                          <%= month(rs("begin_datetime")) %>/<%= day(rs("begin_datetime")) %>-<%= datevalue(rs("end_datetime"))%></td>
                                        <td bgcolor="#FFFFFF"  style="padding-bottom:4px;"><a href="<%= LAYOUT_HOST_URL %>shopping_cart_pre.asp?pro_class=3&Pro_Id=<%= rs("product_serial_no")%>"><img src="/soft_img/app/buy.gif" width="34" height="13" border="0"></a></td>
                                        </tr>
                                        <%
                                                rs.movenext
                                                loop
                                            end if
                                            rs.close : set rs =nothing
                                '			prs.movenext
            '							loop
            '						end if
            '						prs.close : set prs = nothing	
                                %>
                              </table>
                              <br>
                              <table width="100%" height="50"  border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td align="right">
                                   
                                  </td>
                                </tr>
                            </table></td>
                          </tr>
                        </table>
                </div>
            <!-- main end 	-->
        </td>

        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
