<!--#include virtual="site/inc/inc_page_top_no.asp"-->


<%
	dim pro_class,id, product_image_1,  product_image_1_g
	pro_class = SQLescape(request("pro_class"))
	id = SQLescape(request("id"))
    save_cost = 0
	
	dim product_name, product_price, product_sku, product_Manufacturer, product_img_sum, specal_price
	
	if isnumeric(id) then
		set rs = conn.execute("select p.*, pc.is_noebook from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.product_serial_no="&SQLquote(id)&" and p.tag=1 ")
		if not rs.eof then
			product_name = rs("product_name")
			product_price = formatcurrency(rs("product_current_price"))
			is_noebook = rs("is_noebook")
			product_sku = rs("product_serial_no")
			product_Manufacturer = rs("Manufacturer_part_number")
			part_number = rs("supplier_sku")
			product_ship_price = rs("product_ship_price")
			producter_serial_no = rs("producter_serial_no")
			'product_summary = rs("product_desc")
			product_img_sum  = rs("product_img_sum")
			part_image_sku = rs("other_product_sku")
            save_cost = rs("product_current_discount")
		end if
		rs.close : set rs = nothing
		
	end if
	
	if (part_image_sku = ""  or isnull(part_image_sku)  or part_image_sku = 0) then 
		part_image_sku = id
	end if
	product_image_1 = HTTP_PART_GALLERY & part_image_sku & "_list_1.jpg"
	product_image_1_g= HTTP_PART_GALLERY & part_image_sku & "_g_1.jpg"
	'response.write product_price
	
	if isnumeric(id) and cint(save_cost)>0 then 
		'save_cost = 0
		'response.write "select * from tb_sale_pomotion where '"& cdate(now())&"' between begin_datetime and end_datetime product_serial_no="&id
		set rs = conn.execute("select * from tb_sale_promotion where '"& cdate(now())&"' between begin_datetime and end_datetime and product_serial_no="&id)
		
		if not rs.eof then
			sale_promot_datetime = rs("begin_datetime")
			sale_pronot_endtime = rs("begin_datetime")
			
		end if
		rs.close : set rs = nothing
	end if

	
	
	dim sale_promot_datetime, sale_pronot_endtime, save_cost
	if isnumeric(id)  and cint(save_cost)>0 then 
		'save_cost = 0
		'response.write "select * from tb_sale_pomotion where '"& cdate(now())&"' between begin_datetime and end_datetime product_serial_no="&id
		set rs = conn.execute(FindOnSaleSingle(id))
		
		if not rs.eof then
			sale_promot_datetime = rs("begin_datetime")
			sale_pronot_endtime = rs("end_datetime")
			'save_cost = cdbl(rs("save_cost"))
		end if
		rs.close : set rs = nothing
	end if
	'response.write save_cost & "DD"
    save_cost = cdbl(save_cost)
	specal_price = ChangeSpecialCashPrice(product_price - save_cost)
%>
<table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="100" valign="top" bgcolor="#CDE3F2" style="border-left:#8E9AA8 1px solid; border-right:#8E9AA8 1px solid; border-bottom:#8E9AA8 1px solid;"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td valign="top">
          <div class="page_main_nav" id="page_main_nav"><span class='nav1'>Home</span><span class='nav1'>Parts Detail</span>                    </div>
            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
              <tr>
                <td valign="top" style="border:#E3E3E3 1px solid;">
                               <h3 title='prod_detail_title' class="prod_detail_title"><%=ucase(product_name) %></h3>
                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="45%" valign="top"><table width="100"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td width="300" align="center">
						
						
						  <span id="case_img_big_2">
								  <%
                                 
                                    'WriteSystemBigImg(crs(0))  
                                
                                  %>
						  </span>
						  </td>
                          </tr>
                      </table>
                      </td>
                      <td valign="top">
                      	<table width="100%"  border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td valign="top"><table width="100%"  border="0" cellspacing="2" cellpadding="2">
                                <%
								dim product_price_rate
								product_price_rate = product_price
								if cdbl(save_cost) <> 0 then 
								
								
								%>
								
								<%end if%>
								
								
								<% 'if product_Manufacturer <> "" then %>
                                <tr bgcolor="#f2f2f2">
                                  <td width="150"><strong>Manufacturer&nbsp;Part#: <br>
                                  </strong><span style='color:#666666; font-size:7pt;'>Subject to change without notice</span></td>
                                  <td><strong>
                                    <% if  product_Manufacturer = "0" then response.write "" else response.write product_Manufacturer%>
                                  </strong></td>
                                </tr>
								<%' end if%>
								<%' if part_number <> "" then %>
                                <tr bgcolor="#f2f2f2">
                                  <td><strong>Manufacturer: </strong></td>
                                  <td><strong>
                                    <%
									'if producter_serial_no <> "" then 
									'	response.write GetProducterName(producter_serial_no)
									'end if
									response.Write(producter_serial_no)
									%>
                                  </strong></td>
                                </tr>
								<%'end if%>
                                <tr bgcolor="#f2f2f2">
                                  <td valign="top"><strong>LUC&nbsp;SKU number: </strong></td>
                                  <td><strong><%= product_sku %></strong></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td height="20">
                            <br>
                            <table width="75%" border="0" cellpadding="1" cellspacing="0">
                              <%   if cdbl(save_cost) <> 0 then  %>
                              <tr bgcolor="#f2f2f2" >
                                <td width="58%" bgcolor="#FFFFFF"><strong>Regular Price</strong></td>
                                <td align="right" bgcolor="#FFFFFF"><strong><span class="Original_price"><%= ConvertDecimalUnit(CURRENT_SYSTEM, product_price_rate)%></span></strong></td>
                              </tr>
                              <tr bgcolor="#f2f2f2" >
                                <td bgcolor="#FFFFFF" style="border-bottom:#333333 1px solid;"><strong>Instant Discount</strong></td>
                                <td align="right" bgcolor="#FFFFFF" style="color:blue; border-bottom:#333333 1px solid;"><strong>-<%=  formatnumber(save_cost,2)%></strong><span class="price_unit"><%= CCUN %></span></td>
                              </tr>
                              <%end if%>
                              <tr bgcolor="#f2f2f2" >
                                <td bgcolor="#FFFFFF"><strong>Now&nbsp;Low&nbsp;Price</strong></td>
                                <td align="right" bgcolor="#FFFFFF"><strong><%= ConvertDecimalUnit(CURRENT_SYSTEM,product_price_rate - cdbl(save_cost))%></strong></td>
                              </tr>
                              <tr bgcolor="#f2f2f2" >
                                <td bgcolor="#FFFFFF"><strong>Special&nbsp;Cash&nbsp;Price</strong></td>
                                <td align="right" bgcolor="#FFFFFF"><strong><%=  ConvertDecimalUnit(CURRENT_SYSTEM,cdbl(specal_price))%></strong></td>
                              </tr>
                              <tr bgcolor="#f2f2f2" >
                                <td height="10" bgcolor="#FFFFFF"></td>
                                <td height="10" align="right" bgcolor="#FFFFFF"></td>
                              </tr>
                              
                              <script type="text/javascript" src="/get_rebate_desc.asp?id=<%= id %>"></script>
                            </table>
                              <br></td>
                          </tr>
                          <tr>
                            <td  >
							<%
								if save_cost <> 0 and save_cost <> "" then 
									response.Write("")
									response.write " Discount valid from <strong>"& Convertdate(sale_promot_datetime) & "-" & Convertdate(sale_pronot_endtime) 
									response.Write("</strong><br>")
								end if	
							%>	<script type="text/javascript" src="get_rebate_desc.asp?id=<%= id %>&is_text=true"></script>
                            <% response.Write(FindSpecialCashPriceComment()) %></td>
                          </tr>
                          <tr>
                            <td>
							 
                             <%
								dim product_type
								if cstr(is_noebook) = "1" then 
									product_type= 3
								else
									product_type= 1
								end if
							%>								
							CANADA & USA SHIPPING FROM: <iframe src="/AccountCharge3.aspx?ShippingState=<%=LAYOUT_ONTARIO_ID %>&product_id=<%= id %>&shipping_company=<%= LAYOUT_SHIPPING_COMPANY_LOW_PRICE_ID%>&IsNoebook=<%=is_noebook %>&product_type=<%= product_type %>" style="width: 100px; height: 18px;border-top: 1px solid white;" frameborder="0"  scrolling="no"></iframe><br/></td>
                          </tr>
                          
                      <td valign="bottom">
                      	<hr class="border_white" />
                      <table width="100%"  border="0" align="right" cellpadding="2" cellspacing="0" class="no_print">
                        <tr>
                          <td width="45%"><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td width="32"> <img src="/soft_img/app/print.gif" width="32" height="31" alt=""></td>
                              <td style="background:url(/soft_img/app/save_title_02.gif)"><a onclick='window.print();'>Print this page</a></td>
                              <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                            </tr>
                          </table>
                          </td>
                          <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                            <tr>
                              <td width="32"> <img src="/soft_img/app/quest.gif" alt="" width="32" height="31" border="0"></td>
                              <td  style="background:url(/soft_img/app/save_title_02.gif)"><a href="view_print_parts.asp?id=<%=request("id")%>" target="_blank" class="hui-red"> </a><a href="/site/ask_question.asp?cate=<%=request("pro_class")%>&id=<%=request("id")%>">Ask a Question</a></td>
                              <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                            </tr>
                          </table>
						</td>
                    </tr>
                  </table>
                      </table></td>
                    </tr>
                  </table>
                  
                 </td>
              </tr>
              <tr>
                <td valign="top" style="border:#E3E3E3 1px solid;"><table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                  <tr>
                    <td> <%response.Write(GetStrFromFile (GetPartCommentFile(id)))%></td>
                  </tr>
                </table></td>
              </tr>
              <tr>
                <td valign="top" style="border:#E3E3E3 1px solid;"><table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td height="4"></td>
                    </tr>
                    <tr>
                      <td><span>Prices, system package content and availability subject to change without notice. </span>
                          <p>Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata.</p></td>
                    </tr>
                  </table>
                  <table width="100%"  border="0" cellspacing="0" cellpadding="2" class="noPrint">
                    <tr>
                      <td >
                        <!-- begin -->
                        <!--end --></td>
                    </tr>
                    <tr>
                      <td align="right"  style="padding:10px;" ><a href="#" onClick="window.close()" class="blue_orange_11"><strong>Close Window</strong></a>&nbsp;&nbsp;&nbsp;</td>
                    </tr>
                  </table></td>
              </tr>
          </table></td>
        </tr>
    </table></td>
  </tr>
</table>
<%
call  setViewCount(true,  LAYOUT_HOST_IP, id)
 %>
 <script type="text/javascript">
$().ready(function(){	
	writeCaseImg('<%= id %>');
});

</script>
</body>
</html>