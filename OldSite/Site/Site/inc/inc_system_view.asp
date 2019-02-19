<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    Dim id              	:       id                  =       SQLescape(request("ID"))
	Dim cid					:		cid					=		SQLescape(request("cid"))
	Dim sclass				:		sclass				=		SQLescape(sclass)
    Dim system_price    	:       system_price        =   null
    Dim System_title    	:       system_title        =   null
    Dim system_ebay_code	:       system_ebay_code    =   null
	Dim price_and_save		:		price_and_save		=	null
	Dim system_sell
	Dim System_special_cash	:		System_special_cash	=	null
	Dim sys_showit			:		sys_showit 			= 	false
    Dim eBayItemId
	if id="" or not isnumeric(id) then closeconn(): Response.End()

	' 
	dim current_postion, group_count, pre_postion, next_postion,pre_if, next_if
	group_count = 0
	current_postion = 1
	pre_postion  = id 
	next_postion = id
	pre_if = 0
	next_if = 0
	if len(id)>0 then 
					
		Set rs = conn.execute(" "&_
                                "	select es.id "&_
                                " 	 from tb_ebay_system es inner join tb_ebay_system_and_category sc on es.id=sc.systemSku"&_
                                " 	 where showit=1 and sc.ebaySysCategoryId = '"& cid &"' "&_
                                "   	 order by id asc ")


								
		if not rs.eof then
			do while not rs.eof 
				group_count = group_count + 1
				
				if next_if = 1 then next_postion = rs(0)
				if cstr(id) = cstr(rs(0)) then 
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
	
    Set rs = conn.execute("select es.id, ebay_system_name	"&_
							" ,eBaySysCategoryID, showit, ebayOnline.itemid"&_
                            " from tb_ebay_system es inner join tb_ebay_system_and_category sc on sc.systemSku = es.id "&_
                            " left join  tb_ebay_selling ebayOnline on ebayOnline.sys_sku=es.id"&_
                            " where es.id="&SQLquote(id)&" limit 0,1")
						
    if not rs.eof then
        System_title    =   rs("ebay_system_name")
		if cid 		= "" then cid 		= rs("eBaySysCategoryID")
		'if sclass 	= "" then sclass 	= cid
		if cint(rs("showit")) = 1 then sys_showit = true
        eBayItemId = rs("itemid")
    end if
    rs.close : set rs = nothing
	

	 
	  	price_and_save 	= GetSystemPriceAndSave(id)	  
	  	system_price	= splitConfigurePrice(price_and_save,0)	   
	  	system_save  	= splitConfigurePrice(price_and_save,1)	  
	  	system_sell		= system_price - system_save		
	  	System_special_cash = ChangeSpecialCashPrice(system_sell)
    

%>
<div>
    <h3 title='prod_detail_title' class="prod_detail_title"><%=ucase(System_title) %></h3>
    
    <table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
            <td width="50%" valign="top">
                    
                    <%
                        set crs  =conn.execute("select es.luc_sku from tb_ebay_system_part_comment ec inner join  tb_ebay_system_parts es on ec.id=es.comment_id where is_case = 1 and system_sku="&SQLquote(id)&" ")
                        if not crs.eof then  
							WriteSystemBigImg(crs(0))  
                        end if 
                        crs.close : set crs = nothing
						
                    %>
                    <div style="height:40px;clear:both; line-height:40px;color:#006699; font-weight:bold; text-align:center">
                    	<a href="/site/system_view.asp?class=<%= sclass %>&id=<%= pre_postion %>&cid=<%= cid %>" class="movebar_left">&nbsp;&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;
                        <span style="color:#ff6600"><%= current_postion %></span> <span > of <%= group_count %></span>
                        &nbsp;&nbsp;&nbsp;<a href="/site/system_view.asp?class=<%= sclass %>&id=<%= next_postion %>&cid=<%= cid %>" class="movebar_right">&nbsp;&nbsp;&nbsp;</a>
                    </div>
            </td>
            <td valign="top">
                   <table title="system_top_area" class="table_td_ccc" cellspacing="1" cellpadding="3" width="100%" border="0">
                   		<% if system_save <> 0 then %>
                            <tr>
                                    <td style="text-align:left"><b>Regular &nbsp;Price:</b></td>
                                    <td style="text-align:right;" ><span class="price_dis"><b><%= formatcurrency(system_price, 2)  %></b></span><span class="price_unit"><b><%= CCUN %></b></span></span></td>
                            </tr>
                            <tr>
                                    <td style="text-align:left;font-weight:bold"><b>Discount:</b></td><td style="text-align:right"><span class="price"><%= formatcurrency( system_save, 2 ) %></span><span class="price_unit"><b><%= CCUN %></b></span></td>
                            </tr>                       
                        <tr>
                                <td style="text-align:left"><b>Now Low Price:</b></td>
                                <td style="text-align:right" ><span class="price"><%= formatcurrency( system_sell, 2)  %><span class="price_unit"><%= CCUN %></span></span></td>
                        </tr>
                        <% else %>
                        <tr>
                                <td style="text-align:left"><b>Regular Price:</b></td>
                                <td style="text-align:right" ><span class="price"><%= formatcurrency( system_sell, 2)  %><span class="price_unit"><%= CCUN %></span></span></td>
                        </tr>
                        <% end if %>

                        <tr>
                                <td style="text-align:left"><b>*Special&nbsp;Cash&nbsp;Price:</b></td>
                                <td style="text-align:right" ><span class="price"><%= formatcurrency( System_special_cash, 2)  %><span class="price_unit"><%= CCUN %></span></span></td>
                        </tr>
                         
                        <tr>
                                <td style="text-align:left"><b>LUC SKU Number:</b></td><td><%= id %></td>
                        </tr>
                        <tr>
                                <td style="text-align:left"><b>eBay Item Number:</b></td><td><a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&rd=1&item=<%= eBayItemId %>' target='_blank'><%= eBayItemId %></a></td>
                        </tr>
                   </table> 
                   <br />
                   <p style="text-align:left; COLOR: #4C4C4C;width:298px;">
                        <% response.Write(LAYOUT_FindSpecialCashPriceComment) %>
                   </p>
                   <p style="color:#4C4C4C;text-align:left;width:298px;">All components are brand new and include full manufacturers warranty unless otherwise stated. <br />
                                System is assembled and fully tested.<br />
                   </p>
                   <div style="border-bottom:1px dotted #cccccc">&nbsp;</div>
                   <table width="100%"  border="0" cellspacing="0" cellpadding="0" style="margin:10px 0px 10px 0px;">
                          <tr align="center">
                            <td>&nbsp;</td>
                            <td  style="padding-right: 16px;"><table id="Table1" width="115" height="24" border="0" cellpadding="0" cellspacing="0" align="right" class="btn_table" onclick="window.location.href='/site/computer_system.asp?class=<%= sclass %>&id=<%= id %>&cid=<%= cid %>';"
                            <% if not sys_showit then response.Write(" style='display:none;' ")%>>
                              <tr>
                                <td width="28"><img src="/soft_img/app/customer_bottom_01.gif" width="28" height="24" alt="" /></td>
                                <td class="btn_middle"><a href='/site/computer_system.asp?class=<%= sclass %>&id=<%= id %>&cid=<%= cid %>' class="btn_img"><strong>Customize It</strong></a> </td>
                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr align="center">
                            <td>&nbsp;</td>
                            <td  style="padding-right: 16px;padding-top: 5px"><table id="Table2" width="115" height="24" border="0" cellpadding="0" cellspacing="0" align="right" class="btn_table" 
                            <% if sys_showit then %>
                            onclick="window.location.href='<%= LAYOUT_HOST_URL %>computer_system_to_cart.asp?system_sku=<%= id%>&cmd=uncustomize';"
                            <% end if %>
                            
                            >
                              <tr>
                                <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
                                <td class="btn_middle">
                                
                                	<%if not sys_showit then %>
                                    	Discontinued
                                    <% else %>
									<a href="<%= LAYOUT_HOST_URL %>computer_system_to_cart.asp?system_sku=<%= id%>&cmd=uncustomize" class="btn_img"><strong>Buy It</strong></a>
                                    <% end if %> </td>
                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr align="center">
                                <td colspan="2" style="padding-right: 16px;padding-top: 5px; display:none"><table id="__3" width="185" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
                                  <tr>
                                    <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
                                    <td class="btn_middle"><a href="<%= LAYOUT_HOST_URL %>computer_system_to_cart.asp?cate=<%= cid %>&cmd=arrange&system_sku=<%= id%>" class="btn_img"><strong>Arrange a local pick up</strong></a> </td>
                                    <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                                  </tr>
                                                                </table></td>
                              </tr>
                  </table>
                  <!-- -->
                 
                 	<table width="298"  border="0" align="right" cellpadding="1" cellspacing="0" style="margin-top:15px;">
                          <tr>
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/email.gif" width="32" height="31" alt=""></td>
                                <td  class="btn_middle2">
                                <span id='ebay_sys_email_area'></span>
                                <a href="<%= LAYOUT_HOST_URL %>view_configure_system.asp?cmd=email&system_code=<%=ID%>" onClick="return js_callpage_cus(this.href, 'send_email', 620, 550);" class="hui_red">Email to Me</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                              </tr>
                            </table></td>
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/quest.gif" width="32" height="31" border="0" /></td>
                                <td  class="btn_middle2"><a onclick="return js_callpage_cus(this.href, 'ask_question', 600, 500)" href="<%= LAYOUT_HOST_URL %>ask_question.asp?cate=<%=cid%>&type=2&id=<%=ID%>"  class="hui_red">Ask a Question</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt="" /></td>
                              </tr>
                            </table>
                            </td>
                          </tr>
                          <tr>
                            <td width="45%"><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/print.gif" width="32" height="31" alt="" /></td>
                                <td  class="btn_middle2"><a onclick="return js_callpage_cus(this.href, 'print_sys', 620, 700)" href="<%= LAYOUT_HOST_URL %>view_configure_system.asp?cmd=print&system_code=<%=ID%>" class="hui_red">Print this page</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt="" /></td>
                              </tr>
                            </table>
                            </td>
                            <td>&nbsp;</td>
                          </tr>
                      </table>
            </td>
    </tr>                
    
    </table>
    
    
    <div style="border-top:1px solid #ccc;border-bottom: 1px solid #ccc" title="system_part_list_area">
        <%=WriteSystemDetailList(id, false) %>
    </div>
    
    <p class="comment">
    For more options on this configuration please select the &quot;Customize&quot; button.<br>
You have the option to add, remove, or change most components in this configuration to assure that all of your technology needs are fulfilled. To speak to a live consultant regarding your needs please call 1866-999-7828.<br>
You can submit your order online or call 1866-999-7828 &nbsp;416-446-7323(local). Local pick up is welcomed.
    </p>
    <hr class="border1" />
    <p class="comment">Prices, system package content and availability subject to change without notice.
    <br /><br />
    Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata. </p>
    <hr class="border1" />

    <!--#include virtual="site/inc/inc_error_submit.asp"-->
</div>


<%
Function WriteSystemDetailList(id, is_ebay)
    Dim     rs      :   rs  =   null
    Dim     s       :   s   =   s
    
    If not is_ebay then
            Set rs = conn.execute("select p.product_name"&_
							" ,pg.part_group_name"&_
							" , p.product_serial_no"&_
							" , p.product_current_price"&_
							" , part_quantity"&_
							"  from tb_ebay_system_parts sp "&_
							" 	inner join tb_product p on sp.luc_sku=p.product_serial_no "&_
							" 	inner join tb_part_group pg on sp.part_group_id=pg.part_group_id "&_
							"  where sp.system_sku="&id&" and p.tag=1 and (p.is_non=0 or p.product_name like '%onboard%' or  p.product_name like '%default basic fan%') order by sp.id asc ")
            if not rs.eof then
                    s   =   "<table title=""system_part_list"" cellspacing=""0"" id=""system_part_list"">"   &vblf
                    do while not rs.eof
                        s   =   s&  "   <tr title='part'>"   &vblf
                        s   =   s&  "       <td><b>"& rs("part_group_name") &"</b></td>"   &vblf
                        s   =   s&  "       <td><a  href=""/Site/view_part.asp?id="& rs("product_serial_no") &""" onClick=""javascript:js_callpage_cus(this.href, 'view_part', 610, 616);return false;"">"& rs("product_name")  &"</a></td>"    &vblf
                        s   =   s&  "       <td>x"& rs("part_quantity") &"</td>"    &vblf
                        s   =   s&  "   </tr>"  &vblf
                    rs.movenext
                    loop
                    s   =   s& "</table>"  &vblf
            end if
            rs.close : set rs = nothing
    end if
    WriteSystemDetailList = s
    
End Function


CLoseConn()%>		

<script type="text/javascript">
$().ready(function(){

    $('div[title=system_part_list_area]').css("padding","10px 5px 10px 5px");
    $('table[title=system_part_list]').css("text-align","left").css("width","100%").css("line-height","20px").css("color","#4c4c4c");
    $('tr[title=part]').each(function(i){
        if(i%2 == 1)
        {
            $(this).find("td").css("background","#ffffff").css("font-size","8pt");

        }else
        {
            $(this).find("td").css("background","#f2f2f2").css("font-size","8pt");
        }
    });
	
	bindHoverBTNTable();
});

</script>