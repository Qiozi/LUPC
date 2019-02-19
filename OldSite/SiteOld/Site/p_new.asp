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
        	    <div class="page_main_nav">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'>On Sale</span>
                </div>
            	<div id="page_main_area">
                
					  <table width="600" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                              <tr>
                                <td style="border:#E3E3E3 1px solid;  padding-top:0px;height: 900px" valign="top" >
                                 <form action="p_new.asp" method="post">
                                 <table align="center"><tr><td>
                                 
                                  <select name="pc_id">
                                    <option value="-1">all</option>
                                    <%
                                        dim pc_id
                                        pc_id = SQLescape(request("pc_id"))
                                        set rs = conn.execute("select menu_child_serial_no, menu_child_name from tb_product_category where menu_pre_serial_no in (select menu_child_serial_no from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no=0 and tag=1) and tag=1  and menu_child_serial_no in (select distinct * from (select  menu_child_serial_no mc from tb_product where tag=1 and is_non=0 and split_line=0 and new=1 "&_
                " union all "&_
                " select pc.menu_pre_serial_no mc from tb_product_category pc inner join tb_product p on p.menu_child_serial_no=pc.menu_child_serial_no where p.tag=1 and pc.tag=1 and p.is_non=0 and p.split_line=0 and p.new=1) t) and tag=1 order by menu_child_order asc ")
                                        if not rs.eof then
                                            do while not rs.eof 
                                                response.Write("<option value='"&rs(0)&"' ")
                                                if pc_id = cstr(rs(0)) then 
                                                    response.Write(" selected=true ")
                                                end if
                                                response.Write(" >"&rs(1)&"</option>")
                                            rs.movenext
                                            loop
                                        end if
                                        rs.close : set rs = nothing
                                    %>
                                  </select></td><td><input name="imageField2" type="image" src="/soft_img/app/go5.jpg" width="39" height="20" border="0" >
                                  </td></tr></table>
                                  </form>
                                  </div>
                                  
                                  <% 
                                    dim temp_sql
                                    dim category_name
                                    category_name = ""
                                    if pc_id <> "-1" and pc_id <> "" then 
                                        temp_sql = " and (pc.menu_child_serial_no='"&pc_id&"' or pc.menu_pre_serial_no='"&pc_id&"') "
                                    end if
                                    set rs = conn.execute("select p.*, pc.is_noebook,pc.menu_child_name from tb_product p inner join tb_product_category pc on p.menu_child_serial_no = pc.menu_child_serial_no where p.tag=1 and pc.tag=1 and is_non=0 and split_line=0 and new=1 "&temp_sql&" order by menu_child_serial_no,product_order asc")
                                    
                                    if not rs.eof then
                                        do while not rs.eof 
                                            
                                  %>
                                  
                                  
                                  <table width="100%"  border="0" align="center" cellpadding="0" cellspacing="0" style="border-bottom: 1px solid #dddddd;">
                                        <% if category_name <> rs("menu_child_name") then
                                            category_name = rs("menu_child_name")
                                        %>
                                    <tr>
                                        <td colspan="3" style="background:#E7E7E7; height: 20px; padding-left: 5px"><a href="<%= LAYOUT_HOST_URL %>product_list.asp?page_category=1&class=4&id=<%= rs("menu_child_serial_no")%>" class="text_green_11" style="color:#006600"><%= category_name %></a></td>
                                    </tr>
                                        
                                        <% end if %>
                                    <tr>
                                      <td width="70"><a href="<%= LAYOUT_HOST_URL %>Product_parts_detail.asp?pro_class=2&id=<%= rs("product_serial_no")%>"><img src="<%=GetImgMinURL(HTTP_PART_GALLERY,  PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku"))) %>" width="50" height="50" hspace="10" border="0"></a></td>
                                      <td style="padding-right:10px; "><table width="100%"  border="0" cellpadding="0" cellspacing="0">
                                          <tr>
                                            <td valign="top"><a class="hui-orange-12" href="<%= LAYOUT_HOST_URL %>product_parts_detail.asp?class=2&pro_class=2&id=<%= rs("product_serial_no")%>&cid=<%= rs("menu_child_serial_no")%>"><%= rs("product_name") %></a> <br>
                                                <span class="text_hui2_11"> </span></td>
                                          </tr>
                                      </table></td>
                                      <td width="80" valign="bottom"><table width="94%"  border="0" cellspacing="0" cellpadding="3">
                                          <tr>
                                            <td align="right" class="text_orange_11"> <span class="price_big">
                                                  <%
                                                 dim single_save_price
                                                 single_save_price = GetSavePrice(rs("product_serial_no"))
                                                 'response.write sql_sale_promotion(rs("product_serial_no"))
                                                 ' card_rate = 1.022
                                                 'response.write single_save_price
                                                  if cint(single_save_price) <> 0 then
                                                        response.write  "<span style='text-decoration:line-through;color: #cccccc;'>"& ConvertDecimalUnit(Current_system, rs("product_current_price"))& "</span><br/>"
                                                        response.write  ConvertDecimalUnit(Current_system,cdbl(rs("product_current_price"))- cdbl(single_save_price))
                                                  else
                                                        'response.write  price_unit & formatcurrency(cdbl(rs("product_current_price")) * card_rate )
                                                        response.write  ConvertDecimalUnit(Current_system, rs("product_current_price"))
                                                  end if
                                                 
                                                  %></span>
                                                                  </td>
                                          </tr>
                                          <tr align="right">
                                          <%
                                          dim parent_category
                                          if rs("is_noebook") = 1 then 
                                            parent_category = 2
                                          else
                                            parent_category = 3
                                          end if
                                           %>
                                            <td> <a href="Shopping_Cart_pre.asp?pro_class=<%= parent_category %>&Pro_Id=<%= rs("product_serial_no")%>"><img src="/soft_img/app/buyNow_bt.gif" width="56" height="13" border="0"></a> </td>
                                          </tr>
                                      </table></td>
                                    </tr>
                                  </table>				  
                                  <%		
                                        rs.movenext
                                        loop
                                    end if
                                    rs.close : set rs = nothing
                                  
                                %>
                                  
                                 </td>
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