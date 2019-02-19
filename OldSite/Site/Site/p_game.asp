<!--#include virtual="site/inc/inc_page_top.asp"-->
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" style="width:600px" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class='page_main_nav' id="page_main_nav">
                	<span class="nav1"><a href='<%= LAYOUT_HOST_URL %>'>home</a></span>
                    <span class="nav1">Game</span>
                </div>
            	<div id="page_main_area">
                	<%
						
					dim product_list_sku(8)
					product_list_sku(0) = 502861
					product_list_sku(1) = 503129'502855
					product_list_sku(2) = 503124'502878
					product_list_sku(3) = 503145 '502874
					product_list_sku(4) = 503116 
					product_list_sku(5) = 503115
					product_list_sku(6) = 502875'2964
					product_list_sku(7) = 503118 '502894
					product_list_sku(8) = 503092'2909
					
					
					function sel_sql(sku)
						sel_sql = "select p.product_serial_no , p.product_short_name  from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no = sp.luc_sku inner join tb_ebay_system st on st.id=sp.system_sku and st.showit=1 where  p.product_serial_no not in (select product_serial_no from tb_product where product_name like '%warranty%' or product_short_name like '%warranty%') and sp.system_sku="& sku &" and p.is_non=0 and p.tag=1 order by sp.id asc"
					end function
					function short_name_str(id, name)
						response.Write "• <a class=""text_hui_11"" style=""line-height:12px;"" href="""& LAYOUT_HOST_URL &"view_part.asp?id="& id &""" onClick=""return js_callpage_cus(this.href, 'view_system', 602, 600);"">" & name &"</a><br/>"'& "</li>"
					end function  
					%>
                	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td height="2"></td>
                              </tr>
                            </table>
                            <table width="600" height="387"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td style="padding-top:1px; padding-bottom:2px; " valign="top"><%'=PageMainHTMLText(current_page_name)%>
                                <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td height="5"><img src="/soft_img/app/gaming_banner.jpg" width="600" height="110"></td>
                              </tr>
                            </table>
                <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td height="5"></td>
                              </tr>
                            </table>
                <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td height="182" ><img src="/soft_img/app/game_6.jpg" width="600" height="200"></td>
                              </tr>
                              <tr>
                                <td background="/soft_img/app/game_7.jpg" ><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top">
                                            <%
                                            set rs = conn.execute(sel_sql(product_list_sku(0)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write("SKU:" & product_list_sku(0))
                                            %>
                                          </td>
                                        </tr>
                                      </table></td>
                                      <td valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(1)))
                                            if not rs.eof then 
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write( "SKU:" &product_list_sku(1))
                                            %></td>
                                        </tr>
                                      </table></td>
                                      <td valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(2)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                     short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write( "SKU:" &product_list_sku(2))
                                            %></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                            <tr>
                                              <td class="text_red_12b"> <script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(0)%>&is_sold=1&is_char=1&is_card_rate=1"></script></td>
                                              <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(0)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                            </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(1)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(1)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(2)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(2)%>"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                </table>               </td>
                              </tr>
                              <tr>
                                <td height="14" ><img src="/soft_img/app/game_9.jpg" width="600" height="14"></td>
                              </tr>
                            </table>
                <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td height="182" ><img src="/soft_img/app/game_11.jpg" width="600" height="200"></td>
                              </tr>
                              <tr>
                                <td background="/soft_img/app/game_7.jpg" ><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(3)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write( "SKU:" &product_list_sku(3))
                                            %></td>
                                        </tr>
                                      </table></td>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(4)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write( "SKU:" &product_list_sku(4))
                                            %></td>
                                        </tr>
                                      </table></td>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(5)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write("SKU:" & product_list_sku(5))
                                            %></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(3)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(3)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                      <td><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(4)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(4)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                      <td><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(5)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(5)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td colspan="3"><img src="/soft_img/app/game_9.jpg" width="600" height="14"></td>
                                      </tr>
                                </table></td>
                              </tr>
                            </table>
                <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td height="182" ><img src="/soft_img/app/game_10.jpg" width="600" height="200"></td>
                              </tr>
                              <tr>
                                <td background="/soft_img/app/game_7.jpg" ><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(6)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write( "SKU:" &product_list_sku(6))
                                            %></td>
                                        </tr>
                                      </table></td>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(7)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                    short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write("SKU:" & product_list_sku(7))
                                            %></td>
                                        </tr>
                                      </table></td>
                                      <td width="33%" valign="top"><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td align="left" class="text_hui_11" style="padding:4px; padding-bottom:4px; line-height:12px;" valign="top"><%
                                            set rs = conn.execute(sel_sql(product_list_sku(8)))
                                            if not rs.eof then 
                                              '  response.Write("<ul>")
                                                do while not rs.eof 
                                                     short_name_str rs(0), rs(1) 
                                                rs.movenext
                                                loop
                                                'response.Write("</ul>")
                                            end if
                                            rs.close : set rs = nothing
                                            'response.Write( "SKU:" & product_list_sku(8))
                                            %></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(6)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(6)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                      <td><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(7)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(7)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                      <td><table width="88%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td><img src="/soft_img/app/line.jpg" width="178" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td><table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                              <tr>
                                                <td class="text_red_12b"><script language="javascript" src="<%= LAYOUT_HOST_URL %>inc/inc_get_price.asp?category=3&product_id=<%=product_list_sku(8)%>&is_sold=1&is_char=1&is_card_rate=1"></script> </td>
                                                <td width="58"><a href="<%= LAYOUT_HOST_URL %>system_view.asp?class=Search&id=<%=product_list_sku(8)%>" target="_blank"><img src="/soft_img/app/select.jpg" width="58" height="18" border="0"></a></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td colspan="3"><img src="/soft_img/app/game_9.jpg" width="600" height="14"></td>
                                    </tr>
                                </table></td>
                              </tr>
                            </table>
                                </td>
                              </tr>
                            </table>
                            <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td height="5"></td>
                              </tr>
                          </table> 
                </div>
            <!-- main end 	-->
        </td>
        <td id="page_main_right" valign="top" class='page_frame'>
        	<!-- right begin -->                   	
            	<div id="page_main_right_html"><!--#include virtual="/Site/inc/inc_right.asp"--></div>
            <!-- right end 	-->
        </td>
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
</script>