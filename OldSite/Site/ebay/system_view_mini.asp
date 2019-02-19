<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<html>
<head>
<%

'Response.Buffer = True 
'Response.ExpiresAbsolute = Now() - 1 
'Response.Expires = 0 
'Response.CacheControl = "no-cache" 
'Response.AddHeader "Pragma", "No-Cache"

dim begin_timer , end_timer
begin_timer = timer()
%>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
<script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
<script type="text/javascript" src="/js_css/jquery_helper.js"></script>
<link href="/js_css/pre_lu.css" rel="stylesheet" type="text/css" />
<style type="text/css">
<!--
a:link {color: #4C4C4C; TEXT-DECORATION: none;}
a:hover {
	color: #000000;
	text-decoration: underline;
}


@media   Print           
  {
  .noPrint   {   
  DISPLAY:   none   
  }  
  }
  
a:active {color: #4C4C4C; text-decoration: none;}

/*		ul row 		*/
.ulRows { width: 100%; }
.ulRows li { float: left; height:26px; border:0px solid blue; }


.text_small {FONT: 11px/14Px; COLOR: #4C4C4C; letter-spacing:0px}


/*		top menus 		*/
.menuover {}
.menuout {}

/* 		remove 		*/
.text_white {FONT: 12px/14Px "tahoma","tahoma"; COLOR: #ffffff; letter-spacing:0px}

td {
	font-family: "tahoma";
	letter-spacing:0px;
	font-size: 12px;
	color: #4C4C4C;
	text-decoration: none;
	line-height: 18px;
}
.datestyle {color: #4C4C4C;font-size: 9px}

.b {  font-family: "tahoma"; font-size: 11px; COLOR: #515A5B; background-color: #ffffff; border: #8A9B9C; border-style: 
solid; border-top-width: 1px; border-right-width: 1px; border-bottom-width: 1px; border-left-width: 1px}

.text_blue_11 {FONT: 11px/16px "tahoma"; COLOR: #006699; letter-spacing:0px}	

.text_white_12 {FONT: 12px/16Px "tahoma","tahoma"; COLOR: #ffffff; letter-spacing:0px}

.text_hui_11 {FONT: 11px/16Px "tahoma"; COLOR: #4C4C4C; letter-spacing:0px}


A.hui-orange-s:link {FONT: 11px/16Px "tahoma";  color: #4C4C4C; letter-spacing:0px; text-decoration: none}
A.hui-orange-s:visited {FONT: 11px/16Px "tahoma";  color: #4C4C4C; letter-spacing:0px; text-decoration: none}
A.hui-orange-s:hover {FONT: 11px/16Px "tahoma";  color: #ff6600; letter-spacing:0px; text-decoration: none}

A.orange-hui-s:link {FONT: 11px/16Px "tahoma";  color: #ff6600; letter-spacing:0px; text-decoration: none}
A.orange-hui-s:visited {FONT: 11px/16Px "tahoma";  color: #ff6600; letter-spacing:0px; text-decoration: none}
A.orange-hui-s:hover {FONT: 11px/16Px "tahoma";  color: #4C4C4C; letter-spacing:0px; text-decoration: none}

A.white-hui-12:link {FONT: 12px/14Px "tahoma";  color: #ffffff; letter-spacing:0px; text-decoration: none}
A.white-hui-12:visited {FONT: 12px/14Px "tahoma";  color: #ffffff; letter-spacing:0px; text-decoration: none}
A.white-hui-12:hover {FONT: 12px/14Px "tahoma";  color: #dddddd; letter-spacing:0px; text-decoration: none}


Body {
      SCROLLBAR-FACE-COLOR: #ffffff; 
      SCROLLBAR-HIGHLIGHT-COLOR: #999999; 
      SCROLLBAR-SHADOW-COLOR: #999999; 
      SCROLLBAR-3DLIGHT-COLOR: #ffffff; 
      SCROLLBAR-ARROW-COLOR: #999999; 
      SCROLLBAR-TRACK-COLOR: #ffffff; 
      SCROLLBAR-DARKSHADOW-COLOR: #ffffff 
	  
	  background-color: #ECF6FF;
	  margin-left: 0px;
	  margin-top: 0px;
	  margin-right: 0px;
	  margin-bottom: 0px;
}
-->
</style>

</head>
<script>
	function toSubmit(the)
	{		
		if(IsEmail($('#textfield').val()))
		{ 
			$('#form1').submit();
		} 
		else 
		{
			alert('The addresse is not in the correct e-mail format');			
		}
		return false;
	}

</script>
<body  onkeydown="if(event.keyCode=='13' && 'email' == '<%=request("cmd")%>') toSubmit(document.getElementById('form1'));">
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

	dim system_regdate
	Dim ebay_number
	Dim case_sku
	system_regdate =  now()
		
	Dim no_href
	no_href = SQLescape(request("no_href"))
	
	dim id, new_system_code, system_code
	system_code = SQLescape(request("system_code"))
	
	'response.write  len(system_code)
	if len(system_code) <> LAYOUT_SYSTEM_CODE_LENGTH and len(system_code)<> LAYOUT_EBAY_CODE_LENGTH then response.write "Sorry The Product Doesn't Exist!" : closeconn(): response.End()
	
	dim title,serial_no,price
	title = ""
	price = 0
	serial_no=0
	system_templete_serial_no = session("system_templete_serial_no")

	'title =  GetSystemNameFromSpTmp(id)
	system_regdate = now()

	' 给显示系统值，方便Quote处理
	session("view_system") = true
	
	dim save_cost, current_price, specal_price , current_price_rate, price_and_save
	
	if len(system_code) = LAYOUT_SYSTEM_CODE_LENGTH then 
		 Set rs = conn.execute("select sys_tmp_code, sys_tmp_price, create_datetime, "&_
						 "	tag,"                           &_
						 "  ip, "                           &_
						 "	system_templete_serial_no, "    &_
						 "	is_noebook "                    &_
						 "	,ebay_number"&_
						 "	,sys_tmp_product_name"&_
						"	from tb_sp_tmp "&_
						"	where sys_tmp_code="& SQLquote(system_code))
		if not rs.eof then
			current_price 	= rs("sys_tmp_price")
			title	  		= rs("sys_tmp_product_name")
			system_regdate	= datevalue(rs("create_datetime"))
		End if
		rs.close : set rs = nothing			
	else
		    Set rs = conn.execute("select id, ebay_system_name, ebay_system_price, ebay_system_current_number	"&_
                            " from tb_ebay_system "&_
                            " where (id='"& system_code &"' or ebay_system_current_number='"& system_code &"') and showit=1")						
			if not rs.eof then
				ID 				=	rs("id")
				current_price   =   rs("ebay_system_price")
				title    		=   rs("ebay_system_name")
				system_regdate	=   date()
			end if
			rs.close : set rs = nothing
	End if		
	
	
'	if isnumeric(current_price) then 
'		current_price = cdbl(current_price) * card_rate
'	end if
%>
<table width="600" height="100%"  border="0" align=center cellpadding="0" cellspacing="0" bgcolor="#CDE3F2">
  <tr>
    <td width="100%" valign="top" bgcolor="#FFFFFF">     
	 <table width="100%"  border="0" cellspacing="0" cellpadding="0">
        <TR>
          <TD height="94" style="background: url(/soft_img/app/small_top.jpg)" class="text_small"><table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
              <td width="67%">&nbsp;</td>
			  <form action="mailto:sales@lucomputers.com" id="frm100" name="frm100">
              <td width="33%" class="text_blue_11">
                1875 Leslie Street, Unit 24<br>
                Toronto, Ontario  M3B 2M5<br>
                Tel: (Toll free) 1866.999.7828 <br>&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;(Local)416.446.7743<br>
                <a onClick="document.getElementById('frm100').submit();" style="cursor:pointer">sales@lucomputers.com</a></td>
				</form>
            </tr>
          </table></TD>
        </TR>
      </table>
      <table width="100%" height="25"  border="0" cellpadding="0" cellspacing="0">
        <tr>
          <td height="27" align="center" bgcolor="#327AB8" class="text_white_12"><strong><%=ucase(title)%></strong></td>
        </tr>
      </table>
      <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
          <td >
            <table width=100% cellspacing="0" cellpadding="0" border="0" bgcolor="#ffffff" align=center>
              <tr>
                <td>
				
				<table width="100%"  border="0" cellpadding="3" cellspacing="1" bgcolor="#327AB8">
                      <tr>
                        <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#E3E3E3">
                            <tr>
                              <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                  <td width="45%">
								  <div id="case_img_big_2">	
								  </div>
								  </td>
                                  <td valign="top"><table width="100%"  border="0" cellspacing="2" cellpadding="2">                                    
                                      <tr bgcolor="#f2f2f2" >
                                          <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Now &nbsp;Low&nbsp;Price: </strong></td>
                                          <td align="left" bgcolor="#f2f2f2" class="price"><span class="price"><%=formatcurrency(current_price,2)%></span><span class="price_unit"><%= CCUN%></span></td>
                                        </tr>                                    
    
                                        <tr bgcolor="#f2f2f2">
                                          <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Quote Number: </strong></td>
                                          <td align="left" bgcolor="#f2f2f2" >&nbsp;<%= system_code %></td>
                                        </tr>
                                         <tr bgcolor="#f2f2f2">
                                          <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Date: </strong></td>
                                          <td align="left" bgcolor="#f2f2f2">&nbsp;<%=ConvertDate(system_regdate)%></td>
                                        </tr>
                                        
                                      </table>
								  		<div style="font-size:8pt;"><% response.Write(FindSpecialCashPriceComment()) %>
											<div style="height:8px; line-height:8px;font-size: 1pt;">&nbsp;</div>
                                            <p>Every unique computer takes 1-7 days to be preassembled and tested before installed into the computer chassis. System includes meticulous hand assembly and precision cable routing. We tune system performance to its best and complete driver updates. All manufacturer documentations and disks are included.</p>
                                            <p>
                                     <% 
									 if no_href <> "yes" then 
										 if request("cmd") = "print" then %>
												<table class="noPrint">
													<tr>
														<td>
															  <table id="__01" width="115" class="noPrint" height="24" border="0" cellpadding="0" cellspacing="0">
																<tr>
																  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
																  <td class="btn_middle"><a href="#" onClick="javascript:print()" class="white-hui-12"><strong>Print</strong></a></td>
																  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
																</tr>
															  </table>
													   </td>                                               
													   <td align="center"  style="padding:10px;" >                                          				
																<table id="__01" width="115" class="noPrint" height="24" border="0" cellpadding="0" cellspacing="0">
																<tr>
																  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
																  <td class="btn_middle"><a href="#" onClick="javascript:window.close()" class="white-hui-12"><strong>Close Window</strong></a></td>
																  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
																</tr>
															  </table>	
													   </td>
													</tr>
												</table>        
														   
										  <% elseif request("cmd") = "email" then %>
												<div id="email_input">
												  <table width="100%" border="0" align="center" cellpadding="2" cellspacing="0"  class="noPrint">
													<form action="<%= LAYOUT_HOST_URL %>email_to_me_system_ok.asp" method="post" name="form1" id='form1' onSubmit="return toSubmit(this);" target="ifr1">
														<input type="hidden" name="email_type" value="12">
														<input type="hidden" name="email_to_me" value="yes">
														<input type="hidden" name="id" value="<%=system_code%>">
													  <tr>
														<td colspan="2">Your email address 1: <input name="textfield" id="textfield" type="text" class="b" size="30" ></td>
														</tr>
													  <tr>
														<td colspan="2">Your email address 2: <input name="textfield2"  id="textfield2" type="text" class="b" size="30">
													  </td>
														</tr>
													  </form> 
													  <tr>
														<td width="50%" align="center">                                          <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
															<tr>
															  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
															  <td class="btn_middle"><a onClick="window.close()" class="btn_img"><strong>Cancel</strong></a> </td>
															  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
															</tr>
														</table></td>
														<td width="50%" align="center">                                          <table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
															<tr>
															  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
															  <td class="btn_middle"><a class="btn_img" onClick="toSubmit();return false;"><strong>Send</strong></a></td>
															  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
															</tr>
														</table></td>
													  </tr>
													</table>
											</div>
									 <% else %>
											<table  class="noPrint">
												<tr>
													<td>
													<table id="__2" width="90" height="24" border="0" cellpadding="0" cellspacing="0" align="right">
														  <tr>
															<td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
															<td class="btn_middle"><a class="btn_img"  onClick="opener.document.getElementById('form1').submit(); window.close();return false;"><strong>Buy It</strong></a> </td>
															<td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
														  </tr>
													  </table>
													</td>
												  
												</tr>
											</table>
									 <% end if
								 End  if
								 %>
                                    </p>
								  </div>

								  </td>
                                </tr>
                                
                              </table>
                                <table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                  <tr>
                                    <td colspan="3" >
									<!-- begin -->
									
									<TABLE cellSpacing="0" cellPadding="3" width="100%">
                                      <TBODY>
									  
							<%
							if len(system_code) = LAYOUT_SYSTEM_CODE_LENGTH then 
								set rs  = conn.execute("select sp.product_name,sp.cate_name, p.product_serial_no ,"&_
								"p.product_current_price-product_current_discount sell,"&_
"case when product_store_sum >2 then 2 "&_
"when ltd_stock >2 then 2  "&_
"when product_store_sum + ltd_stock >2 then 2  "&_
"when product_store_sum  <=2 and product_store_sum >0 then 3 "&_
"when product_store_sum +ltd_stock <=2 and product_store_sum +ltd_stock >0 then 3 "&_
"when ltd_stock <=2 and ltd_stock >0 then 3 "&_
"when product_store_sum +ltd_stock =0 then 4 "&_
"when product_store_sum +ltd_stock <0 then 5 end as ltd_stock , part_quantity from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no  inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.sys_tmp_code="&SQLquote(system_code)&" and (p.is_non=0 or p.product_name like '%onboard%') order by   sp.product_order asc ")
							else
								Set rs = conn.execute("select es.id, luc_sku, product_ebay_name product_name, epc.comment cate_name, is_case "&_
                                            " ,es.part_quantity"   &_
                                            " ,es.max_quantity"    &_
                                            " ,case when p.other_product_sku>0 then p.other_product_sku else luc_sku end as img_sku" &_
                                            " from tb_product p "&_
                                            " inner join tb_ebay_system_parts es on p.product_serial_no=es.luc_sku "&_
                                            " inner join tb_ebay_system_part_comment epc on epc.id=es.comment_id "&_
                                            " where es.system_sku="& SQLquote(id) &"")
								if not rs.eof then
									Do while not rs.eof 
										if (rs("is_case") = 1 ) then
											case_sku =  rs("luc_sku")                          
										end if
									rs.movenext
									loop
									rs.movefirst                                
								end if 
					  		end if
						if not rs.eof then		
							rscount = 0
							do while not rs.eof 
							rscount = rscount + 1
							
							%>
							<tr bgcolor="<%if rscount mod 2 = 1 then response.write "#efefef" else response.write "white"%>">
                            <td align="left" class="text_hui_11"><strong><%= rs("cate_name")%> </strong></td>
                            
                            <td align="left" class="text_hui_11">
							<%=rs("product_name")%>
							</td>
							<td style="width: 15px;" class="text_hui_11">
                            
                            x <%
                                        response.Write rs("part_quantity")
                            %>
                            </td>
                          </tr>
							<%rs.movenext
							
							loop
							end if
							rs.close : set rs = nothing
							%>
                                      </TBODY>
                                    </TABLE>
										<!--end -->
									</td>
                                  </tr>
                                  <tr>
                                    <td colspan="3" align="left"  style="padding-left:10px; padding-right:10px; padding-bottom:10px; padding-top:10px;" ><p class="text_hui_11">                                      <strong>Congratulations!</strong> <br>
                                      You have successfully configured a new system. The quote number displays on top of this page. If you are not to submit your order now please keep this quote by emailing to yourself. </p>
                                      <p><span class="text_hui_11">You can continue submitting your order now or place your order later: <br><br>
                                      1) Visit www.lucomputers.com to use our fully automated and secure online ordering system, Enter this quote number to the box at the upper right corner of the web page. Submit your order when system configuration is loaded.<br>
                                      <br>
                                      2) Call us by phone (Toll free 1866.999.7828 or 416.446.7743) during business hours and please refer to this quote number. Do not hesitate to ask any questions and further changes to the configuration.<br>
                                      <br>
                                      3) Visit us in our store at 1875 Leslie Street, Unit 24, Toronto.<br><br>
                                      Shipping and applicable taxes are not included in quotation. USA customers are tax and duty free, Ontario residents 13%, HST provinces 13%, rest of Canada 5%. If pickup is your preferred method please load this quote and click button ARRANGE A LOCAL PICK UP.<br>
                                      <br>
                                      Business Hours: Mon-Fri 10AM - 7PM EST Sat 11AM - 4PM EST</span><br>
                                      </p></td>
                                  </tr>
                                  
                                </table>
                              </td>
                            </tr>
                        </table>                        </td>
                      </tr>
                  </table>                </td>
              </tr>
              <tr height=4>
                <td align="center" valign="middle"><TABLE border=0 cellpadding=0 cellspacing=0 style="width:100%px; background: #CCCCCC;">
                    <TR>
                      <TD></TD>
                    </TR>
                </TABLE></td>
              </tr>
          </table></td>
        </tr>
    </table></td>
  </tr>
</table>

<div style="display:none;">
<%

		
		
	'------------------------------
	' 机箱?
	'------------------------------
	dim  product_image_url, product_image_1, product_image_1_g, casers
	if len(system_code) = LAYOUT_SYSTEM_CODE_LENGTH then 
		set casers = conn.execute("select max(product_serial_no) from (select p.menu_child_serial_no as product_category, sp.product_serial_no from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no where sys_tmp_code="&SQLquote(system_code)&") a1 , (  select pc.* from tb_product_category pc , tb_computer_case cc where pc.menu_child_serial_no=cc.computer_case_category or pc.menu_pre_serial_no=cc.computer_case_category) a2 where a1.product_category=a2.menu_child_serial_no")
		
		if not casers.eof then
			case_sku = casers(0)
		
		end if
		casers.close : set casers = nothing
	End if

%>

</div>
<script>
	$().ready(function(){	
		writeCaseImg('<%= case_sku %>')	;
	});
</script>	
<%
	closeconn()
%>	
<iframe src="/site/blank.html" name="ifr1" id="ifr1" style="width:0px; height:0px;" frameborder="0"></iframe>
</body>
</html>
