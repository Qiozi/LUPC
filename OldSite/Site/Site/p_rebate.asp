<!--#include virtual="site/inc/inc_page_top.asp"-->

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" class='page_frame'>
        	<!-- main begin -->
        	  <div style=" background:#327AB8; ">
        	    <div class="page_main_nav" style="width:100%;">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'>Rebate Center </span>
                </div></div>
            	<div id="page_main_area">
                		<%
							Dim category_name_temp
						%>
					   <table width="100%" height="750" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                              <tr>
                                <td style="border:#E3E3E3 1px solid;  padding-top:0px;" valign="top">
                                  <table width="100%"  border="0" cellspacing="0" cellpadding="12">
                                    <tr>
                                      <td width="100%" class="text_hui_11"><span class="text_hui_11">Here you will find a list of rebates that are currently provided by the manufacturers.
                                        </span>
                                        <p class="text_hui_11">These rebates are valid for purchases made at LU Computers only. Please read all rebates' terms and conditions carefully. It is the responsibility of the end user to meet rebate requirements set forth by the manufacturer.</p>
                                        <p class="text_hui_11">Please make sure to check for the following:</p>
                                        1) Make sure you have purchased the correct product / part number.<br>
                  2) Read all terms &amp; conditions of the rebate prior to making purchase. <br>
                  3) Keep a copy of all rebate form &amp; proof of purchase for your records.<br>
                                          4) Download, fill out, mail it in.<br>
                                      5) Please make sure you reside in the country (USA/Canada) where the rebate is valid. 
                                        <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                          <tr>
                                            <td height="8"></td>
                                          </tr>
                                        </table>
                                        <table width="100%"  border="0" cellspacing="0" cellpadding="4">
                                          <tr>
                                            <td valign="top"><a href="http://www.adobe.com/products/acrobat/readstep2.html" target="_blank"><img src="/soft_img/app/acrobat.gif" width="88" height="31" border="0"></a></td>
                                            <td class="text_hui_11">Note: you must have Adobe Acrobat reader installed on your computer in order to download a mail in rebate. If you do not have Acrobat Reader installed click here to download a free version of this software.</td>
                                          </tr>
                                        </table></td>
                                    </tr>
                                  </table>
                                  <div style=" border-bottom: 1px solid green; ">
                                      <div style="background: #ccc url('/soft_img/app/title_bg_green.gif');line-height: 20px; color: #fff; width: 180px; font-weight:bold; ">
                                        &nbsp;&nbsp;Shortcut
                                        <span style="display:none"><a name="top"></a></span>
                                      </div>
                                  </div>
                                  <div style="padding:1em; ">
                                    <%
                                    set rs =  conn.execute(sql_rebate_all(2))
                                    if not rs.eof then 	
                                        do while not rs.eof 
                                            category_name_temp = ucase(rs(0))
                                            
                                            if category_name_temp<>"" then 
                                            
                                            response.write "<div style='float:left;width: 145px;text-align:center'><a href=""#"& replace(category_name_temp, " ", "_") &""">"& replace(category_name_temp, " ", "_") &"</a></div>"
                                            end if
                                        rs.movenext
                                        loop						
                                    end if 
                                    rs.close : set rs = nothing
                                    
                                    %>
                                  </div>
                                  <table style="clear:both"><tr><td>&nbsp;</td></tr></table>
                                  <hr size="1" style="width:100%; clear:both">
                                  <table width="100%"  border="0" cellpadding="4" cellspacing="1" bgcolor="#e7e7e7" NOFLOW NOWRAP>
                                    <tr bgcolor="#CDE3F2">
                                      <td align="center" nowrap="true"> <font color="#4c4c4c"><span style="font-size: 9pt; font-weight: 700">SKU</span></font></td>
                                      <td height="14"> <font color="#4c4c4c"><span style="font-size: 9pt; font-weight: 700">Description</span></font></td>
                                      <td width="10%" align="center"><strong>Maf. Part#</strong></td>
                                      <td width="10%" height="14" align="center"> <b>Rebate<span style="font-size: 9pt"><font color="#FFFFFF"> </font> </span> </b> </td>
                                      <td height="14" align="center" nowrap="true" > <font color="#4c4c4c"><span style="font-size: 9pt; font-weight: 700"> Start</span></font></td>
                                      <td height="14" align="center" nowrap="true" > <font color="#4c4c4c"><span style="font-size: 9pt; font-weight: 700">End</span></font></td>
                                    </tr>
                                    
                                    <%
                                    
                                    function sql_rebate_all(p)
                                        if p = 1 then 
                                            sql_rebate_all =  "select distinct p.product_serial_no from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.show_it=1 and pc.tag=1 and pdf_filename <> '' and p.tag=1 and (TO_DAYS(now()) between TO_DAYS(sp.begin_datetime) and TO_DAYS(sp.end_datetime)+30) and promotion_or_rebate=2  and sp.show_it=1 order by p.producter_serial_no,sp.sale_promotion_serial_no asc"
                                        end if
                                        
                                        if p = 2 then 
                                            sql_rebate_all =  "select distinct p.producter_serial_no from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.show_it=1 and pc.tag=1 and pdf_filename <> '' and p.tag=1 and (TO_DAYS(now()) between TO_DAYS(sp.begin_datetime) and TO_DAYS(sp.end_datetime)+30) and promotion_or_rebate=2 and sp.show_it = 1 order by p.producter_serial_no asc"
                                        end if
                                    end function
                                        dim factory
                                        factory = ""
                                        'set prs =  conn.execute(sql_rebate_all(1))
                                        'if not prs.eof then
                                            'do while not prs.eof
                                                set rs = conn.execute("select sp.*, p.producter_serial_no, p.product_short_name,p.manufacturer_part_number from tb_sale_promotion sp inner join tb_product p on p.product_serial_no = sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.show_it=1 and pc.tag=1 and pdf_filename <> '' and p.tag=1 and (TO_DAYS(now()) between TO_DAYS(sp.begin_datetime) and TO_DAYS(sp.end_datetime)+30) and promotion_or_rebate=2  and sp.show_it=1 order by sp.end_datetime desc,p.product_short_name asc ")
                                                if not rs.eof then
                                                    dim td_bgcolor
                                                    do while not rs.eof 
                                                    
                                                    if ucase(factory) <> ucase(trim(rs("producter_serial_no"))) then
													 
                                                    	factory = ucase(trim(rs("producter_serial_no")))
                                            %>
                                            <tr bgcolor="#efefef">
                                              <td>&nbsp;</td>
                                              <td height="14"><strong> <a name="<%= replace(factory, " ", "_")%>" id="<%= replace(ucase(factory), " ", "_")%>"></a><span style="font-size: 9pt"><%= replace(ucase(factory), " ", "_")%></span></strong></td>
                                              <td width="10%" align="center">&nbsp;</td>
                                              <td height="14"  align="center">&nbsp; </td>
                                              <td height="14"  align="center">&nbsp; </td>
                                              <td height="14"  align="center">&nbsp; <a href="#page_top"><img src="/soft_img/app/top.gif" style="border: 0px none ; cursor: pointer;" alt=""></a></td>
                                            </tr>
                                            <%
                                                    end if
                                                    
                                                    
                                                    td_bgcolor = ""
													
													if not isnull(rs("end_datetime")) and not isempty(rs("end_datetime")) and rs("end_datetime") <> "" then 
														if cdate(rs("end_datetime"))  < date() then  td_bgcolor = "color: #888888;"
													end if
													
                                            %>
                                            <tr bgcolor="#FFFFFF" >
                                              <td><a name="<%= rs("product_serial_no")%>"></a><span style="font-size: 9pt; <%= td_bgcolor %>"><%= rs("product_serial_no")%></span></td>
                                              <td height="14"><strong style="font-weight: 400"> <span style="text-decoration: none; font-size: 9pt"> <a href="/pro_img/rebate_pdf/<%= rs("pdf_filename") %>"> <span style="text-decoration: none; <%= td_bgcolor %>"><%= rs("product_short_name")%></span></a> <span style="text-decoration: none; <%= td_bgcolor %>"><%= rs("comment")%></span></span><span style="font-size: 9pt">&nbsp;</span></strong></td>
                                              <td width="10%" align="left"> <span style="font-size: 9pt; <%= td_bgcolor %>"><%= rs("manufacturer_part_number")%></span></td>
                                              <td  height="14" align="right"> <span style="font-size: 9pt; <%= td_bgcolor %>"><%= formatcurrency(rs("save_cost"))%></span><span class="price_unit">CAD</span></td>
                                              <td  height="14" align="center" nowrap="true"> <span style="font-size: 9pt;<%= td_bgcolor %>"><%= ConvertDate(rs("begin_datetime"))%></span></td>
                                              <td  height="14" align="center" nowrap="true"> <span style="font-size: 9pt;<%= td_bgcolor %>"><%= ConvertDate(rs("end_datetime"))%></span></td>
                                            </tr>
                                            <%
                                                    rs.movenext
                                                    loop
                                                end if
                                                rs.close : set rs =nothing
                                            'prs.movenext
                                            'loop
                                        'end if
                                        'prs.close : set prs = nothing
                                    %>
                                  </table>                  
                                  <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                      <td>&nbsp;</td>
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
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
</script>