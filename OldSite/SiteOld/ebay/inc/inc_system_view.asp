<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    Dim id              :       id                  =       SQLescape(request("id"))
    Dim system_price    :       system_price        =   null
    Dim System_title    :       system_title        =   null
    Dim system_ebay_code:       system_ebay_code    =   null

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
		Set rs = conn.execute("Select id "&_
		                        " from tb_ebay_system "&_
		                        " where showit=1 and category_id in (select category_id from tb_ebay_system where id='"& id &"' or ebay_system_current_number='"& id &"') and showit=1")
						
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
	
    Set rs = conn.execute("select id, ebay_system_name, ebay_system_price, ebay_system_current_number	"&_
                            " from tb_ebay_system "&_
                            " where (id='"& id &"' or ebay_system_current_number='"& id &"') and showit=1")
						
    if not rs.eof then
		ID 				=	rs("id")
        system_price    =   rs("ebay_system_price")
        System_title    =   rs("ebay_system_name")
        system_ebay_code=   rs("ebay_system_current_number")
    end if
    rs.close : set rs = nothing
    

    Set crs = conn.execute("select es.id, luc_sku, product_ebay_name, epc.comment, is_case "&_
                                            " ,es.part_quantity"   &_
                                            " ,es.max_quantity"    &_
                                            " ,compatibility_parts"&_
                                            " ,case when p.other_product_sku>0 then p.other_product_sku else luc_sku end as img_sku" &_
                                            " from tb_product p "&_
                                            " inner join tb_ebay_system_parts es on p.product_serial_no=es.luc_sku "&_
                                            " inner join tb_ebay_system_part_comment epc on epc.id=es.comment_id "&_
                                            " where es.system_sku='"& id &"'")
	
%>
<div>
    <h3 title='prod_detail_title' class="prod_detail_title"><%=ucase(System_title) %></h3>
    
    <table cellpadding="0" cellspacing="0"  class="table_td_width">
    <tr>
            <td width="50%" valign="top">
                    
                    <%
                        if not crs.eof then
                            Do while not crs.eof 
                                if (crs("is_case") = 1 ) then
                                    WriteSystemBigImg(crs("luc_sku"))                                        
                                end if
                            crs.movenext
                            loop
                            crs.movefirst                                
                        end if 
                    %>
            </td>
            <td valign="top">
                   <table title="system_top_area" class="table_td_ccc" cellspacing="1" cellpadding="3" width="100%">
                        <tr>
                                <td style="text-align:left"><b>Now Low Price:</b></td>
                                <td style="text-align:right" ><span title='ebay_system_sell'><%= ConvertDecimalUnit(Current_System, system_price)  %></span></td>
                        </tr>
                        <tr>
                                <td style="text-align:left"><b>LUC SKU Number:</b></td><td><%= system_ebay_code %></td>
                        </tr>
                   </table> 
                   <hr style="border:0; border-bottom:1px dotted #ccc;"  />
                   <p style="padding:10px auto;text-align:left; COLOR: #4C4C4C;">
                        <% response.Write(LAYOUT_FindSpecialCashPriceComment) %>
                   </p>
                   <p style="color:#4C4C4C;text-align:left">All components are brand new and include full manufacturers warranty unless otherwise stated. <br />
                                System is assembled and fully tested.<br />
                   </p>
                   <hr style="border:0; border-bottom:1px dotted #ccc;"  />
                   <table width="100%"  border="0" cellspacing="0" cellpadding="0" style="margin:10px 0px 10px 0px;">
                          <tr align="center">
                            <td>&nbsp;</td>
                            <td  style="padding-right: 16px;"><table id="Table1" width="115" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
                              <tr>
                                <td width="28"><img src="/soft_img/app/customer_bottom_01.gif" width="28" height="24" alt="" /></td>
                                <td align="center"  style="background: url(/soft_img/app/customer_bottom_03.gif);"><a href="/ebay/ebay_system_customize.asp?id=<%= system_ebay_code %>" class="btn_img"><strong>Customize It</strong></a> </td>
                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                              </tr>
                            </table></td>
                          </tr>
                          <tr align="center">
                            <td>&nbsp;</td>
                            <td  style="padding-right: 16px;padding-top: 5px"><table id="Table2" width="115" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
                              <tr>
                                <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
                                <td align="center"  style="background: url(/soft_img/app/customer_bottom_03.gif);"><a href="/ebay/BuyS.asp?number=<%= system_ebay_code%>" class="btn_img"><strong>Buy It</strong></a> </td>
                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                              </tr>
                            </table></td>
                          </tr>
                          
                  </table>
                  <!-- -->
                  <hr style="border:0; border-bottom:1px dotted #ccc;"  />
                 	<table width="100%"  border="0" align="right" cellpadding="1" cellspacing="0" style="margin-top:15px">
                          <tr>
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/email.gif" width="32" height="31" alt=""></td>
                                <td style="background: url(/soft_img/app/save_title_02.gif)">
                                <span id='ebay_sys_email_area'></span>
                                <!--<a class="hui_red" onclick="$('#ebay_sys_email_area').load('/ebay/inc/inc_email.asp');">Email to Me</a>-->
                                <a href="/ebay/system_view_mini.asp?cmd=email&system_code=<%=request("id")%>" onClick="return js_callpage_cus(this.href, 'send_email', 620, 600);" class="hui_red">Email to Me</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt=""></td>
                              </tr>
                            </table></td>
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/quest.gif" width="32" height="31" border="0" /></td>
                                <td style="background: url(/soft_img/app/save_title_02.gif)"><a onclick="return js_callpage_cus(this.href, 'ask_question', 600, 500)" href="<%= LAYOUT_HOST_URL %>ask_question.asp?cate=<%=request("class")%>&amp;type=2&amp;id=<%=request("id")%>"  class="hui_red">Ask a Question</a></td>
                                <td width="9"> <img src="/soft_img/app/save_title_03.gif" width="9" height="31" alt="" /></td>
                              </tr>
                            </table>
                            </td>
                          </tr>
                          <tr>
                            <td width="45%"><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                              <tr>
                                <td width="32"> <img src="/soft_img/app/print.gif" width="32" height="31" alt="" /></td>
                                <td style="background: url(/soft_img/app/save_title_02.gif)"><a onclick="return js_callpage_cus(this.href, 'print_sys', 620, 700)" href="/ebay/system_view_mini.asp?cmd=print&system_code=<%=request("id")%>" class="hui_red">Print this page</a></td>
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
        <%=WriteSystemDetailList(id, true) %>
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
});


setViewCookies('<%= id %>', 'ebay');
getViewCookies("ebay");
</script>