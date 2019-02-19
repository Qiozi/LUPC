<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	dim pro_class,id, product_image_1,  product_image_1_g, is_noebook
    Dim prodType
    Dim img_url
	Dim cid
    dim description 
	CID = SQLescape(request("cid"))
	id 	= SQLescape(request("ID"))
	save_cost = 0

		

	dim product_name, product_price, product_sku, product_Manufacturer, product_img_sum, product_category, part_image_sku, is_display_stock
	dim stock_status_id
	product_category = ""	    
	product_summary = ""
	dim is_Discontinued
	is_Discontinued = false
	if isnumeric(id) then
		set rs = conn.execute("select p.product_name,p.product_current_price,p.product_serial_no,p.Manufacturer_part_number,p.supplier_sku,p.producter_serial_no,p.product_img_sum,p.menu_child_serial_no,p.product_name_long_en, p.other_product_sku,pc.is_noebook,is_display_stock, "&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum+ltd_stock <0 then 5 end as ltd_stock "&_
", p.prodType, p.img_url, p.product_current_discount "&_
"from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.product_serial_no="&id&" and ( p.tag=1 "& sql_not_issue & ")")
		if not rs.eof then
			product_name = rs("product_name")
			product_price = (rs("product_current_price"))
			product_sku = rs("product_serial_no")
			product_Manufacturer = rs("Manufacturer_part_number")
			part_number = rs("supplier_sku")
			producter_serial_no = rs("producter_serial_no")
			product_img_sum  = rs("product_img_sum")
			product_category = rs("menu_child_serial_no")
			is_noebook = rs("is_noebook")
			product_name_long_en = rs("product_name_long_en")
			part_image_sku = rs("other_product_sku")
			is_display_stock = rs("is_display_stock")
			stock_status_id = rs("ltd_stock")
            prodType = rs("prodType")
            img_url = rs("img_url")
            save_cost = cdbl(rs("product_current_discount"))
		else
			is_Discontinued = true
		end if
		rs.close : set rs = nothing
		
        if(prodType <> "New") then
            set rs = conn.execute("select part_comment from tb_part_comment where part_sku = '"& id &"'")
            if not rs.eof then
                description = rs(0)
                if instr(description, "<body") >0 then

                description = mid(description, instr(description, "<body>")+6, len(description)-instr(description, "<body>"))
                description = replace(replace(description, "</body>", ""), "</html>","")
                description = replace(description, " width=", "")
                end if
            end if
            rs.close : set rs = nothing
        end if
	end if

dim is_virtual, virtual_keywords, virtual_keyword_sql
dim vir_key_sql, vir_key_sqls
virtual_keywords = ""
if product_category <> "" then	

	set rs = conn.execute("select is_virtual,keywords, keywords_cates from tb_product_category where menu_child_serial_no='"& SQLescape(Cid) &"'")
	if not rs.eof then
		if rs(0) = 1 then 
			is_virtual = true
			virtual_keywords = rs(1)
			
			if instr(virtual_keywords, "|")<1 or virtual_keywords = "" or isnull(virtual_keywords) then 
				    vir_key_sql = "and p.product_name_long_en like '%"& virtual_keywords &"%' "
			else
				    vir_key_sqls = split(virtual_keywords, "|")
				    for i=lbound(vir_key_sqls) to ubound(vir_key_sqls)
				        vir_key_sql =  vir_key_sql & "and p.product_name_long_en like '%"& vir_key_sqls(i) &"%' "
				    next
			end if
				
			if virtual_keywords <> "" then
                virtual_keyword_sql = vir_key_sql & " and p.menu_child_serial_no in ("& rs("keywords_cates") &")"
            else
                virtual_keyword_sql = " and  pv.menu_child_serial_no='"& SQLescape(Cid) &"'"
            end if
		else
			is_virtual = false
		end if
	end if
	rs.close: set rs = nothing
end if


	'response.write product_price
	if (part_image_sku = ""  or isnull(part_image_sku)  or part_image_sku = 0) then 
		part_image_sku = id
	end if
	product_image_1 = HTTP_PART_GALLERY & part_image_sku & "_list_1.jpg"
	product_image_1_g= HTTP_PART_GALLERY & part_image_sku & "_g_1.jpg"
	
	if isnumeric(id) and cint(save_cost)>0 then 
		'save_cost = 0
		'response.write "select * from tb_sale_pomotion where '"& cdate(now())&"' between begin_datetime and end_datetime product_serial_no="&id
		set rs = conn.execute(FindOnSaleSingle(id))
		
		if not rs.eof then
			sale_promot_datetime = rs("begin_datetime")
			sale_pronot_endtime = rs("end_datetime")
			
		end if
		rs.close : set rs = nothing
	end if
	
	' 取得当前类的列表， 并判断当前第几个
	dim current_postion, group_count, pre_postion, next_postion,pre_if, next_if
	group_count = 0
	current_postion = 1
	pre_postion  = id 
	next_postion = id
	pre_if = 0
	next_if = 0
	if isnumeric(id) then 
	    if not is_virtual then
		    set rs = conn.execute("select product_serial_no from tb_product where tag=1 and is_non=0 and split_line=0 and menu_child_serial_no in (select  menu_child_serial_no from tb_product where product_serial_no='"& id &"') order by product_order asc")
		else
		    if trim(virtual_keywords) <> "" then
		            set rs = conn.execute("select product_serial_no from tb_product p where tag=1 and is_non=0 and split_line=0 "& virtual_keyword_sql & " order by product_current_price-product_current_discount, product_order asc")
		    else
		         '   response.write "select product_serial_no from tb_product_virtual pv inner join tb_product p on p.product_serial_no=pv.lu_sku where p.tag=1 and is_non=0 and split_line=0 "& virtual_keyword_sql & " order by product_current_price-product_current_discount, product_order asc"
		            set rs = conn.execute("select product_serial_no from tb_product_virtual pv inner join tb_product p on p.product_serial_no=pv.lu_sku where p.tag=1 and is_non=0 and split_line=0 "& virtual_keyword_sql & " order by product_order asc")
		    end if		
		end if
		
		if not rs.eof then
			do while not rs.eof 
				group_count = group_count + 1
				
				if next_if = 1 then next_postion = rs(0)
				if cint(id) = cint(rs(0)) then 
					current_postion = group_count
					pre_if = 1
					next_if = 1
				else
					next_if = 0
				end if
				if pre_if = 0 then pre_postion = rs(0)
				
			rs.movenext
			loop
		end if
		rs.close : set rs = nothing
	end if
%>

<table width="100%" height="670" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid;text-align:left ">
              <tr>
                <td valign="top" style="border:#E3E3E3 1px solid;">
                	<h3 title='prod_detail_title' class="prod_detail_title"><%=ucase(product_name) %>
                    <% if prodType <> "New" then response.Write "&nbsp;" & prodType %>
                    </h3>
    
                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                    <tr>
                      <td width="45%" valign="top"><table border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td width="300" align="center" style=" min-height:300px; height:300px">
 <span id="case_img_big_2">
								  <%
                                 
                                    'WriteSystemBigImg(crs(0))  
                                
                                  %>
						  </span>
     						<br style='clear:both;'/>
                          <label id="flash_view_button" style="clear:both; border:0px solid red"></label>
                          <script language="javascript" type="text/javascript" src="/site/inc/get_view_flash_button.asp?id=<%=id%>"></script>
                          <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width='160' style="text-align:center;">
                            <%'= getProducter(id)%>
							<a href="/site/product_list.asp?page_category=1&class=<%= request("class")%>&pro_class=<%=CID%>&id=<%=CID%>" class="blue_orange_11">More From <%= getMenuChildName(product_category) %></a>
                          </td>
                          <td height="22" style="text-align:left">
                          
                          	 <div style="height:40px;clear:both; line-height:40px;color:#006699; font-weight:bold">
                    	<a href="/site/product_parts_detail.asp?class=<%= request("class") %>&id=<%= pre_postion %>&CID=<%= Cid %>" class="movebar_left">&nbsp;&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;
                        <span style="color:#ff6600"><%= current_postion %></span> <span > of <%= group_count %></span>
                        &nbsp;&nbsp;&nbsp;<a href="product_parts_detail.asp?class=<%= request("class") %>&id=<%= next_postion %>&CID=<%= Cid %>" class="movebar_right">&nbsp;&nbsp;&nbsp;</a>
                    </div>
                    </td></tr></table>
							</td>
                          </tr>
                      </table></td>
                      <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td ><table width="100%"  border="0" cellspacing="2" cellpadding="2">
                              <tr bgcolor="#f2f2f2">
                                <td width="150" class="text_hui_11"><strong>Manufacturer&nbsp;Part#: </strong></td>
                                <td class="text_hui_11"><strong>
                                  <% if  product_Manufacturer = "0" then response.write "" else response.write product_Manufacturer%>
                                </strong></td>
                              </tr>
                                <%
								
								dim filter_back, filter_first, Special_cash_price
								filter_first = cdbl(product_price)
								filter_back = filter_first
								Special_cash_price = ChangeSpecialCashPrice(filter_first- cdbl(save_cost))
								 
								%>
								<tr bgcolor="#f2f2f2">
                                  <td class="text_hui_11"><strong>Manufacturer: </strong></td>
								  <td class="text_hui_11"><strong>
                                    <%
									'if producter_serial_no <> "" then 
'										response.write GetProducterName(producter_serial_no)
'									end if
									response.write producter_serial_no
					%>
                                  </strong></td>
							    </tr>
								<tr bgcolor="#f2f2f2">
                                  <td valign="top" class="text_hui_11"><strong>LUC&nbsp;SKU number: </strong></td>
								  <td class="text_hui_11"><strong><%= product_sku %></strong></td>
							    </tr>
							    <% if is_display_stock = 1 then %>
								<tr bgcolor="#f2f2f2">
                                  <td valign="top" class="text_hui_11"><strong>Stock &nbsp;Status: </strong></td>
								  <td class="text_hui_11"><strong>
								  <%=  FindPartStoreStatus2(product_sku, stock_status_id)  %></strong></td>
							    </tr>
							    <% end if %>
								<tr bgcolor="#f2f2f2" >
								  <td height="10" bgcolor="#FFFFFF" class="text_hui_11"></td>
								  <td height="10" bgcolor="#FFFFFF" class="text_hui_11"></td>
							    </tr>
											
                            </table>
                              <table width="75%" border="0" cellpadding="1" cellspacing="0" id="partPriceArea">
                            	<tr>
                                    <td>Loading...</td>
                                </tr>
                                
                              </table></td>
                          </tr>
                          <tr bgcolor="#f2f2f2" >
                            <td height="10" bgcolor="#FFFFFF" class="text_hui_11"></td>
                          </tr>
                          <tr>
                            <td class="text_hui_12"  >
							<%
								if save_cost <> 0 and save_cost <> "" then 
									response.Write("")
									response.write " Discount valid from <strong>"& Convertdate(datevalue(sale_promot_datetime)) & " - " & Convertdate(datevalue(sale_pronot_endtime)) 
									response.Write("</strong><br>")
								end if
							%>
                            <span id="getRebateDescText"></span>
                            
                            <% if cint(stock_status_id) >0 then response.write ResponseStockStatus(stock_status_id)& "<br/><br/>"
							
								response.Write(FindSpecialCashPriceComment())
								
								dim product_type
								if cstr(is_noebook) = "1" then 
									product_type= 3
								else
									product_type= 1
								end if
							%>
							<div>

                           		
							CANADA & USA SHIPPING FROM: <iframe src="/AccountCharge3.aspx?ShippingState=<%=LAYOUT_ONTARIO_ID %>&product_id=<%= id %>&shipping_company=<%= LAYOUT_SHIPPING_COMPANY_LOW_PRICE_ID%>&IsNoebook=<%=is_noebook %>&product_type=<%= product_type %>" style="width: 95px; height: 18px;border-top: 1px solid white;" frameborder="0"  scrolling="no"></iframe><br/>
							 </div>							</td>
                          </tr>
    
                          <tr>
                            <td height="30" align="right" style="padding-right:15px; padding-top: 4px">
                            <hr class="border_white">
                            <table width="100%" border="0" >
                       	    <tr>
                                	<td width="120" align="left">
									
                                    </td>
                                
                                	<td align="right">
                                    
                                  <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0" class="btn_table"
                                  	onclick="window.location.href='<%= LAYOUT_HOST_URL %>Shopping_Cart_pre.asp?CID=<%=CID%>&Pro_Id=<%=id%>';">
                                            <tr>
                                              <td width="28"> <img src="/soft_img/app/buy_car.gif" width="28" height="24" alt=""></td>
                                              <td align="center" class="btn_middle">
                                              <%	if is_Discontinued then %>
                                              Discontinued
                                              <%	else 	%>
                                               <a class="btn_img" href="<%= LAYOUT_HOST_URL %>Shopping_Cart_pre.asp?CID=<%=CID%>&Pro_Id=<%=id%>" ><strong>Buy It</strong></a> 
                                              <%	end if 	%>
                                               </td>
                                              <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                           <div style="height:50px;">&nbsp;</div>
                            <table width="100%"  border="0" align="right" cellpadding="2" cellspacing="0">
                        <tr>
                          <td width="45%"><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0" onclick="return js_callpage_cus('view_part.asp?id=<%=ID%>', 'print', 602, 600)">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/print.gif" alt="" width="32" height="31" border="0"></td>
                                <td class='btn_middle2' style="text-align:left" nowrap="nowrap"><a href="view_part.asp?id=<%=ID%>" target="_blank" onclick="return js_callpage_cus(this.href, 'print', 602, 600)" class="hui-red">Print this page</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                              </tr>
                          </table></td>
                          <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0" 
                           onclick="return js_callpage_cus('ask_question.asp?product_category=1&cate=<%= CID%>&id=<%=ID%>', 'question' , 602, 450)">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/quest.gif" width="32" height="31" alt=""></td>
                                <td class="btn_middle2" style="text-align:left" nowrap="nowrap"><a onClick="return js_callpage_cus(this.href, 'question' , 602, 450)" href="ask_question.asp?product_category=1&cate=<%= CID%>&id=<%=ID%>" target="_blank" class="hui-red">Ask  a Question</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                              </tr>
                          </table></td>
                        </tr>
                      </table>
                            </td>
                          </tr>
                          <tr>
                            <td align="right" style="padding-right:15px; ">&nbsp;
                            <span style="color:#FFFFFF"><%
							if part_number <> "" then 
								response.Write("supplier_sku:" & part_number)
							else
								response.Write("http://www.lucomputers.com")
							end if
							%></span>
                            </td>
                          </tr>
                      </table></td>
                    </tr>
                  </table>
          
				  	<div style="border-top: 1px solid #efefef;text-align:left;"> 
					<%
					  if product_name_long_en <> "" then 
					  	%>					
                            <p class="text_red_12b" style="color:#999999; font-weight:bold;padding-left: 15px;" >MAJOR SPECS FOR THIS ITEM (SKU:<%= id %>): </p>
                            <p class="text_red_12b" style="font-weight:bold;padding-left: 15px;margin-top:0px; margin-bottom:0px;" ><%=product_name_long_en %> </p>
                            <p class="text_red_11" style="padding-left: 15px;" >(Please check this short specs if any options found on the page.) <br/><br/></p>
					<%end if %></div>					
                  	<div style="border-top: 1px solid #efefef; border-bottom: 1px solid #efefef; padding-left: 15px; padding-bottom:70px;" id='page_main_part_detail'>				
					 <%=description%>                     
					</div> 
                  <table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                    <tr>
                      <td> <br />
 <% if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" then %>
 	<a href="product_part_desc.aspx?product_id=<%=ID %>" onclick="js_callpage_name(this.href, 'right_manage');return false;">Manage</a>
 <% end if %>
 <div style="text-align:right"><a href="#page_top"><img src="http://www.lucomputers.com/images/top.gif" style="cursor:pointer; border:0px" alt=""></a></div>
 </td>
                    </tr>
                    <tr>
                      <td><span class="text_hui_11">Prices, system package content and availability subject to change without notice.
                      </span>
                      <p class="text_hui_11">Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata.</p></td>
                    </tr>
                    <tr>
                      <td style="border-top: 1px solid #cccccc;">
                            <!--#include virtual="site/inc/inc_error_submit.asp"-->               
                      
                      </td>
                    </tr>
                  </table></td>
              </tr>
            </table>
            

<script type="text/javascript">
    $().ready(function () {
        writeCaseImg('<%= id %>');
        bindHoverBTNTable();
        if ($.trim($('#page_main_part_detail').html()) == "") {
            showLoading();
            $('#page_main_part_detail').load("/part_comment/<%= id %>_comment.html"
		, function () {
		    closeLoading();
		    if ($('#page_main_part_detail').html().indexOf("logo_flash_bg.png") != -1)
		        $('#page_main_part_detail').html("");

		});
        }

        $.ajax({
            type: "get",
            url: "/site/inc/get_rebate_desc.asp",
            data: "is_text=false&id=<%=id %>",
            error: function (r, t, s) {

            },
            success: function (data, status) {
                $('#partPriceArea').html(data);
            }
        });
        $.ajax({
            type: "get",
            url: "/site/inc/get_rebate_desc.asp",
            data: "is_text=true&id=<%=id %>",
            error: function (r, t, s) {

            },
            success: function (data, status) {
                $('#getRebateDescText').html(data);
            }
        });
        
        
    });

</script>

