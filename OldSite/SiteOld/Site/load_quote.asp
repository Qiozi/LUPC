<!--#include virtual="site/inc/inc_page_top.asp"-->
<%

Dim is_exist_product
Dim source_price
Dim rscount
Dim case_sku
dim id 
id = trim(request("quote"))
if id="" or len(id) <> 8 then
	id = -1
end if

%>
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
                    <span class="nav1"><%= id %></span>
                </div>
            	<div id="page_main_area">              
                		<%
	
						
					
						dim is_old, system_name, regdate, sys_tmp_price
						
						is_old = false
						system_name = ""
						regdate = ""
						is_exist_product = 0
						' get system is Old DB
						set rs = conn.execute("select sys_tmp_product_name, create_datetime, is_old from tb_sp_tmp where sys_tmp_code='"& id &"'")
						if not rs.eof then 
							if cstr(rs("is_old")) = "1" then 
								is_old = true
							end if
							system_name = rs("sys_tmp_product_name")
							regdate = ConvertDate(DateValue(rs("create_datetime")))
					
							is_exist_product = 1
						end if
						rs.close : set rs = nothing
						
						dim save_cost, current_price, specal_price , current_price_rate
						if id <> -1 then 
							dim price_save_cost 
							price_save_cost = FindSystemPriceAndSaveAndCost8Action(id)
							'response.Write price_save_cost
							save_cost = cdbl(splitConfigurePrice(price_save_cost, 1))
							current_price_rate= splitConfigurePrice(price_save_cost, 0)
							current_price = splitConfigurePrice(price_save_cost, 0)
							if(cdbl(save_cost) <> 0 ) then
								current_price = cdbl(splitConfigurePrice(price_save_cost, 0))-cdbl(save_cost)
							end if
							specal_price = ChangeSpecialCashPrice(current_price)
						end if
						
				 
					%>  
						<table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                                <tr>
                                <% if is_exist_product = 1 then %>
                                  <td style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellpadding="2" cellspacing="0" bgcolor="#E7E7E7">
                                    <tr>
                                      <td height="18" class="text_green_11" style="padding-bottom:4px;">&nbsp;&nbsp;<strong><%= ucase(system_name) %> (<%=id%>)</strong></td>
                                    </tr>
                                  </table>
                                 <table width="100%"  border="0" cellspacing="0" 
                
                cellpadding="0">
                                    <tr>
                                      <td width="300" valign="top">
                                      					 <div id="case_img_big">
                          <span id="case_img_big_2">
                          <%
						 
							'WriteSystemBigImg(crs(0))  
                        
						  %>
						  </span>                          
						  </div>  
                                      </td>
                                      <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td>

                                            <table width="100%"  border="0" cellspacing="2" cellpadding="0">
                                          <% if save_cost <> 0 then %>
                                                   <tr bgcolor="#f2f2f2" >
                                                      <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Regular&nbsp;Price: </strong></td>
                                                      <td align="left" bgcolor="#f2f2f2" class="price_dis" style="text-align:right"><strong><%=ConvertDecimalUnit(Current_system, current_price_rate)%></strong></td>
                                                    </tr>
                                                    <tr bgcolor="#f2f2f2" >
                                                      <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Discount: </strong></td>
                                                      <td align="left" bgcolor="#f2f2f2" class="text_hui_11"  style="text-align:right"><strong>-$<%=formatnumber(save_cost)%></strong></td>
                                                    </tr>
                                                   <% end if %>
                                                    <tr bgcolor="#f2f2f2" >
                                                      <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Now&nbsp;Low&nbsp;Price: </strong></td>
                                                      <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="text-align:right"><strong><%=ConvertDecimalUnit(Current_system, current_price)%></strong></td>
                                                    </tr>
                                                  <tr bgcolor="#f2f2f2" >
                                                    <td class="text_hui_11"><strong>&nbsp;Special&nbsp;Cash&nbsp;Price: </strong></td>
                                                    <td class="text_hui_11" style="text-align:right"><strong><%=ConvertDecimalUnit(Current_system, cdbl(specal_price))%></strong></td>
                                                  </tr>
                
                                                    <tr bgcolor="#f2f2f2">
                                                      <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Quote Number: </strong></td>
                                                      <td align="left" bgcolor="#f2f2f2" class="text_hui_11"><%=id%></td>
                                                    </tr>
                                                    <tr bgcolor="#f2f2f2">
                                                      <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Date:</strong></td>
                                                      <td align="left" bgcolor="#f2f2f2" class="text_hui_11"><%= ConvertDate(regdate) %></td>
                                                    </tr>
                                                    
                                                    
                                                  </table>
                                          

                                                  </td>
                                          </tr>
                                          <tr>
                                            <td height="20"><table width="100%" height="1"  border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td background="/soft_img/app/line2.gif"><img src="/soft_img/app/line2.gif" width="3" height="1"></td>
                                                </tr>
                                            </table>
                                            
                                             <% response.Write(FindSpecialCashPriceComment()) %><br/>
                                              All components are brand new and include full manufacturers warranty unless otherwise stated. 
                                              System is assembled and fully tested. <br/><br/></td>
                                          </tr>
                                         
                                          <tr>
                                            <td>                            
                                            
                                            </td>
                                          </tr>
                                          
                                          <tr>
                                            <td height="30">
                                            <div  id="table_customer_id_btn_area">
                                            <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                              <tr align="center">
                                                <td>
                                                    <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="window.location.href='load_quote_custom.asp?cmd=custom&class=<%= 1%>&id=<%=id%>&category=custems';">
                                                      <tr>
                                                        <td width="28"><img src="/soft_img/app/customer_bottom_01.gif" width="28" height="24" alt="" /></td>
                                                        <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="load_quote_custom.asp?cmd=custom&class=<%= 1%>&id=<%=id%>&category=custems" class="white-hui-12"><strong>Customize It</strong></a> </td>
                                                        <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                      </tr>
                                                    </table>
                                                    <% 
                                                    dim custome_it_btn
                                                    custome_it_btn = "<table id=""__01"" width=""115"" height=""24"" border=""0"" cellpadding=""0"" cellspacing=""0"">                                      <tr>                                        <td width=""28""> <img src=""/soft_img/app/customer_bottom_01.gif"" width=""28"" height=""24"" alt=""""></td>                                        <td align=""center"" background=""/soft_img/app/customer_bottom_03.gif""><a href=""load_quote_custom.asp?cmd=custom&class=1&id="&id&"&category=invalid"" class=""white-hui-12""><strong>Customize It</strong></a> </td>                                        <td width=""6""> <img src=""/soft_img/app/customer_bottom_04.gif"" width=""6"" height=""24"" alt=""""></td>                                      </tr>                                    </table>"
                                                    %>
                                                </td>
                                                <td>
                                                    <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table" onclick="window.location.href='load_quote_custom.asp?cmd=buy&id=<%= id%>';">
                                                      <tr>
                                                        <td width="28"> <img src="/soft_img/app/buy_car.gif" width="28" height="24" alt=""></td>
                                                        <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="load_quote_custom.asp?cmd=buy&id=<%= id%>" class="white-hui-12"><strong>Buy It</strong></a> </td>
                                                        <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                      </tr>
                                                    </table>    
                                                </td>
                                              </tr>
                                           </table>
                                           </div>
                                           <div id="is_not_sale" style="color:red; font-size:10pt; font-weight:bold" ></div>               
                                              <!--  <script type="text/javascript" language="javascript">
                                                    alert(document.getElementById('is_not_sale').innerHTML); 
                                                    document.getElementById('is_not_sale').innerHTML='We are sorry.  One or more components may be already removed from this system.  The price has excluded the removed components.  Please reconfigure your system.';
                                                    document.getElementById('table_customer_id_btn_area').innerHTML='';
                                                </script> -->                
                                                </td>
                                          </tr>
                                      </table></td>
                                    </tr>
                                    <tr>
                                      <td colspan="2"><div id="case_img_list">
                                        </div></td>
                                    </tr>
                                  </table>
                                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td height="5"></td>
                                      </tr>
                                      <tr>
                                        <td style="border-top:#e3e3e3 1px solid; padding:10px; " >
                                        <table width="100%" cellpadding="3" cellspacing="0">
                                        
                                            <%
                                            dim tmp_sql, is_not_sale
                                            is_not_sale = false
                                            tmp_sql = ""
                                            if is_old then 
                                                tmp_sql = "select unit_name as product_name, system_catname as part_group_name, unit_id as product_serial_no,priority as product_order, 1 part_quantity from user_system_unit where system_num='"& id &"' order by priority asc"
                                            else
                                                tmp_sql = " select p.product_name,sp.cate_name as part_group_name, p.product_serial_no,sp.product_order, p.tag,sp.part_quantity  from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.sys_tmp_code="&SQLquote(id)&" and (p.is_non=0 or p.product_name like '%onboard%') order by sp.product_order asc"
                                            end if 
                                                
                                                set rs  = conn.execute( tmp_sql)
                                        if not rs.eof then
                                            rscount = 0
                                            do while not rs.eof 
                                            rscount = rscount + 1
                                            	if lcase(rs("part_group_name")) = "case" then 
													case_sku	=	rs("product_serial_no")
												End if
                                            %>
                                            <tr bgcolor="<%if rscount mod 2 = 1 then response.write "#efefef" else response.write "white"%>">
                                            <td align="left" class="text_hui_11"><strong><%= rs("part_group_name")%> </strong></td>
                                            <td align="left" class="text_hui_11">
                                            <%' if is_old then 
                                                '	response.Write rs("product_name")
                                                'else
                                                if rs("tag") = 1 then 
                                            %>							
                                            
                                            <a class="hui-orange-s" onclick="return js_callpage_cus('/site/view_part.asp?id=<%= rs("product_serial_no")%>', 'view_part',620, 600);">
                                            <% 
                                                end if
                                                response.Write rs("product_name")
                                                
                                                if rs("tag") = 1 then 
                                            %></a>
                                            <%  
                                                else
                                                    is_not_sale = true
                                                    response.Write( "<span style='color:red'>(Invalid)</span>")
                                                end if
                                                
                                                if is_not_sale then 
                                                    response.Write("<script type=""text/javascript"" language=""javascript"">document.getElementById('is_not_sale').innerHTML='<p>We are sorry.  One or more components may be already removed from this system.  The price has excluded the removed components.  Please reconfigure your system.</p>"& custome_it_btn &"';document.getElementById('table_customer_id_btn_area').innerHTML=''</script>")
                                                    is_not_sale = false
                                                end if
                                            %>
                                            </td>
                                            <td width="20">x <%= rs("part_quantity") %></td>
                                          </tr>
                                            <%rs.movenext
                                            
                                            loop
                                            end if
                                            rs.close : set rs = nothing
                                            
                                            %>
                                        </table>
                                        </td>
                                      </tr>
                                    </table>
                                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                     
                                      <tr>
                                        <td height="170" class="text_hui_11" style="padding:10px; ">&nbsp;</td>
                                      </tr>
                                    </table>
                                    <table style="border-top:#e3e3e3 1px solid; " width="100%"  border="0" cellspacing="0" cellpadding="0">
                                      <tr>
                                        <td  style="padding:10px; "><span class="text_hui_11">Prices, system package content and availability subject to change without notice. </span>
                                          <p class="text_hui_11">Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata.</p></td>
                                      </tr>
                                    </table></td>
                                    <%
                                    else
                                    %> 
                                        <td style="height: 800px;padding: 2em;" valign="top">
                                            You search Quote ： <%= id %> <br/>
                                            Sorry, item not found! 
                                            <!--Requested Configuration is invalid or was not found!-->
                                        </td>
                                    
                                    <% end if%>
                                </tr>
                            </table>
                        
                              </div>
                     

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
	writeCaseImg('<%= case_sku %>');
	bindHoverBTNTable();
});
</script>