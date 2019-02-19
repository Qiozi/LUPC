<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<%

Response.Buffer = True 
Response.ExpiresAbsolute = Now() - 1 
Response.Expires = 0 
Response.CacheControl = "no-cache" 
Response.AddHeader "Pragma", "No-Cache"


' 判断网页是否失效
if (session("Expires") = false) then 
	'Response.redirect("expired.asp")
	'response.end
end if

%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8">
<title>LU Computers</title>
<link href="/lu.css" rel="stylesheet" type="text/css" />
<script language="javascript" src="/js/pre_href.js"></script>
<script language="javascript" src="/js/helper.js"></script>
<script language="javascript" src="/js/pre_helper.js"></script>
<script language="javascript" src="/js/product_custom_helper.js"></script>
<script language="javascript" src="/js/popImage.js"></script>
<script language="javascript" src="/js/float.js"></script>
<script language="javascript">

function newWindow()
{
	var url = document.getElementById("product_big_image").value ;
	window.open('product_image_g.asp?file_name='+ url, '','width=500,height=500,top=200,left=300');
}


</script>
<script language="JavaScript" type="text/JavaScript">
<!--
function MM_reloadPage(init) {  //reloads the window if Nav4 resized
  if (init==true) with (navigator) {if ((appName=="Netscape")&&(parseInt(appVersion)==4)) {
    document.MM_pgW=innerWidth; document.MM_pgH=innerHeight; onresize=MM_reloadPage; }}
  else if (innerWidth!=document.MM_pgW || innerHeight!=document.MM_pgH) location.reload();
}
MM_reloadPage(true);
//-->
</script>
</head>
<style>
	.color2 { color: #ff9900;}
	plane table{float:left;}
	.title1 {border: 1px solid #ffffff;float:left; border-bottom: 0px solid red; background-image:url(/images/customer_bottom_03.gif); height:100%; width: 150px;text-weight: blod; color:white;text-align:center;}
	.title2 {border: 1px solid #ffffff;float:left;border-bottom: 0px solid red; background:#FAE6D0; height:100%; width: 150px;text-weight: blod; text-align:center;}
body {
	background-color: #ECF6FF;
}
</style>
<body onclick="RemovePostion('d',1);">
<!--#include virtual="/public_helper/helper.asp"-->
<!--#include virtual="/public_helper/sql.asp"-->
<!--#include virtual="/public_helper/public_helper.asp"-->
<!--#include virtual="/top.asp"-->
<!--#include virtual="/public_helper/custom_helper.asp"-->

<%

	' 加载没有显示明细。
	session("view_system") = false
	session("is_change") = false

	dim system_templete_serial_no, category, factory
	category = encode.IntRequest("category")
	system_templete_serial_no = encode.IntRequest("id")
	
	'------------------------------
	'
	' 产生SKU
	'
	'------------------------------
	'if session("sys_tmp_sku") =""  then 
'		session("sys_tmp_sku") = getCode.sys_prod()
'	end if
	if Session("sys_tmp_sku") =""  then 
		Session("sys_tmp_sku") = getCode.sys_prod()
	end if
	dim sys_tmp_sku
	'sys_tmp_sku = session("sys_tmp_sku")
	sys_tmp_sku = Session("sys_tmp_sku")
	
	if isnumeric(sys_tmp_sku) then 
	if len(clng(sys_tmp_sku)) <> 8 then 
		Session("sys_tmp_sku") = getCode.sys_prod()
		sys_tmp_sku = Session("sys_tmp_sku")
	end if
	end if
	'------------------------------
	'
	' 把system templete放进数据表
	'
	'------------------------------
	'if len(system_templete_serial_no) < 8 or system_templete_serial_no <> session("sys_tmp_sku") then 
	if len(system_templete_serial_no) < 8 or system_templete_serial_no <> Session("sys_tmp_sku")then 
		addToCustome system_templete_serial_no, sys_tmp_sku 
		conn.execute("update tb_sp_tmp set is_templete=1 where sys_tmp_code='"&sys_tmp_sku&"'")
		'response.write sys_tmp_sku
	end if

	'-------------------------------
	' 机箱
	'-------------------------------
	dim case_sku 
	case_sku = 0
	'if(len(system_templete_serial_no) <> 8) then 
		set casers = conn.execute("select max(product_serial_no) from (select product_category, sp.product_serial_no from tb_sp_tmp_detail sp inner join tb_part_group pgd on pgd.part_group_id=sp.part_group_id where sys_tmp_code="&sys_tmp_sku&") a1 , (  select pc.* from tb_product_category pc , tb_computer_case cc where pc.menu_child_serial_no=cc.computer_case_category or pc.menu_pre_serial_no=cc.computer_case_category) a2 where a1.product_category=a2.menu_child_serial_no")
	'else 
		
	'end if
	if not casers.eof then
		case_sku = casers(0)
	
	end if
	casers.close : set casers = nothing
	' 图
	product_image_url = "pro_img/COMPONENTS/"
	product_image_1 = product_image_url & case_sku & "_list_1.jpg"
	product_image_1_g= product_image_url & case_sku & "_g_1.jpg"
	
	
	'response.write case_sku
	
	if not isnumeric(system_templete_serial_no) and category <> "custems" then closeconn():response.End()
	
	'----------------------------------------------
	'	取得当前配置产品
	'----------------------------------------------
	dim current_prod_list, getSystemProductPrice
	current_prod_list = "0"
	
	sql = "select product_serial_no,product_current_price from tb_sp_tmp_detail where sys_tmp_code='"&sys_tmp_sku&"'  "
	
	getSystemProductPrice = 0
	set rs = conn.execute(sql)
	if not rs.eof then 
		
		do while not rs.eof 
			current_prod_list = current_prod_list &"," &rs(0)
			'getSystemProductPrice = getSystemProductPrice + cdbl(rs(1))
			
		rs.movenext
		loop
	end if
	rs.close :set rs = nothing
	
	dim old_save_cost, old_current_price, old_current_price_rate, Special_cash_price
	'old_save_cost = GetSystemSaveCost8(session("sys_tmp_sku"))
'	old_current_price = GetSystemPrice8(session("sys_tmp_sku"))
	old_save_cost = GetSystemSaveCost8(Session("sys_tmp_sku"))
	old_current_price = GetSystemPrice8(Session("sys_tmp_sku"), true)
	old_current_price_rate = old_current_price
	Special_cash_price = ChangeSpecialCashPriceByRate(old_current_price - old_save_cost)
	
	getSystemProductPrice = cdbl(old_current_price_rate) - old_save_cost
	'response.write sys_tmp_sku
'	response.Write(old_save_cost & "<br>")
'	response.Write(old_current_price & "<br>")
'	response.Write(old_current_price_rate & "<br>")
'	response.Write(getSystemProductPrice & "<br>")
%>
<%
' 首次弹出时间
dim open_datetime 
open_datetime =now()
%>
<input type="hidden" value="<%= open_datetime %>" id="open_datetime">
<table width="960"  border="0" align="center" cellpadding="0" cellspacing="0" id="right_Layer" style="position:relative;  ">
  <tr>
    <td width="947" height="100" valign="top" bgcolor="#CDE3F2" style="border-left:#8E9AA8 1px solid; border-right:#8E9AA8 1px solid; border-bottom:#8E9AA8 1px solid;"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
        <tr>
          <td width="165" valign="top" style="padding-left:3px; ">
		 
            <!--#include virtual="/templetes/left.asp"--> 
         	
          </td>
          <td width="606" valign="top">
		  <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
              <tr>
                <td style="height:100px"><!--include virtual="/adv.html"--><qiozi>adv</qiozi></td>
              </tr>
            </table>
            
              <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                <tr>
                  <td height="2"></td>
                </tr>
                <tr>
                  <td><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="22" style="border:#ffffff 1px solid; " bgcolor="#327AB8" class="text_white_12">                           &nbsp;<img src="/Images/arrow_8.gif" width="11" height="10">&nbsp;<a href="default.asp" class="white-red-11"><strong>Home</strong></a>&nbsp;<img src="/Images/arrow_8.gif" width="11" height="10">&nbsp;<a href="product_detail.asp?class=<%= request("class") %>&id=<%= request("id") %>" class="white-red-11"><strong>Product Detail</strong></a> <img src="/Images/arrow_8.gif" width="11" height="10">&nbsp; <strong>Customize
                        </strong></td>
                      </tr>
                  </table></td>
                </tr>
                <tr>
                  <td height="1"></td>
                </tr>
              </table>
              <table width="600" height="670" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                <tr>
                  <td style="border:#E3E3E3 1px solid; " valign="top"><table width="100%"  border="0" cellspacing="0" 

cellpadding="0">
                    <tr>
                      <td width="300" valign="top"><table width="90%" height="90%"  border="0" align="center" cellpadding="0" 

cellspacing="0">
                        <tr>
                          <td width="227">
						  <div id="case_img_big">
						  <input type="hidden" id="product_big_image" name="product_big_image" value="<%=product_image_1_g%>">
						  <a  onClick="javascript:popImage(document.getElementById('product_big_image').value,'Lu Computers','middle_center',true,true);return false;">						  
						  <img src="/<%= product_image_1%>" name="product_image_list_area"  border="0" id="product_image_list_area" style="cursor:pointer;"  onerror="imgerror(this);this.height='50px'" width="300" ></a>
						  </div>
						  </td>
                        </tr>
                      </table></td>
                      <td valign="top"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                          <tr>
                            <td >
								<%
								
								%>
							<span id="custem_system_price_area">
							<table width="100%"  border="0" cellspacing="2" cellpadding="2">
								 <% if cint(old_save_cost) <> 0 then %>
							   <tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Regular&nbsp;Price: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11"><strong><%=formatcurrency(cdbl(old_current_price_rate))%></strong></td>
								</tr>
								<tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Discount: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11" style="color:red;"><strong>-$<%=formatnumber(old_save_cost)%></strong></td>
								</tr>
							   <% end if %>
								<tr bgcolor="#f2f2f2" >
								  <td width="35%" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Now &nbsp;Low&nbsp;Price: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11"><strong><%=formatcurrency(cdbl(old_current_price_rate) - old_save_cost)%></strong></td>
								</tr>
							  <tr bgcolor="#f2f2f2" >
								<td class="text_hui_11"><strong>&nbsp;Special&nbsp;Cash&nbsp;Price: </strong></td>
								<td class="text_hui_11"><strong><%=formatcurrency(Special_cash_price)%></strong></td>
							  </tr>
							
								<tr bgcolor="#f2f2f2">
								  <td valign="top" bgcolor="#f2f2f2" class="text_hui_11"><strong>&nbsp;Quote Number: </strong></td>
								  <td align="left" bgcolor="#f2f2f2" class="text_hui_11">&nbsp;<a onClick="js_callpage_win('/system_quote.asp?1=1', 'system_quote', 450, 300, 300 , 300);" style="cursor:pointer;">Press here to obtain</a></td>
								</tr>
							  </table>
							</span></td>
                          </tr>
                          <tr>
                            <td height="20"><table width="100%" height="1"  border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td background="/images/line2.gif"><img src="/images/line2.gif" width="3" height="1"></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td >The system you customize below will be fully assembled and tested before delivery. <br>
                            All components are brand new.</td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                          </tr>
                          <tr>
                            <td height="30">&nbsp;</td>
                          </tr>
                      </table></td>
                    </tr>
                    <tr>
                      <td colspan="2"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                        <tr>
                          <td width="300"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                              <tr>
                                <td height="22">&nbsp;
								<div id="case_img_list">
								<%
								
							dim product_img_sum
							product_img_sum = 0
							set rs = conn.execute("select product_img_sum from tb_product where product_serial_no="&case_sku)
							if not rs.eof then
								product_img_sum = rs(0)
							end if
							rs.close : set rs = nothing
							
						  for i=1 to product_img_sum
						  	response.write "&nbsp;<img src=""/Images/"& i& ".gif"" width=""20"" height=""11"" style=""cursor:pointer;"" onclick=""changeProductImage('"&product_image_url & case_sku& "_list_"&i &".jpg', '"&product_image_url & case_sku& "_g_"&i &".jpg');"">"
						  next
						  %>
						  		</div>
						  	</td>
                              </tr>
                              <tr>
                                <td height="4" bgcolor="#F2F2F2"></td>
                              </tr>
                              <tr>
                                <td>&nbsp;</td>
                              </tr>
                          </table></td>
                          <td valign="bottom">&nbsp;</td>
                        </tr>
                      </table>
					  
					  <form action="" name="save" id="save" method="post">
					
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                     <tr>
                        <td height="25">
							<div class="title1" style="cursor:hand; padding-top:3px;" id="product_c_1" onClick="getSet(1);">	
								<strong>Major&nbsp;&nbsp;Components</strong>
							</div>
							<div class="title2" style="cursor:hand; padding-top:3px;" id="product_c_2" onClick="getSet(2);" >	
								<strong>Accessories</strong>
							</div>
							<div class="title2" style="cursor:hand; padding-top:3px;" id="product_c_3" onClick="getSet(3);" >	
								<strong>Additional&nbsp;&nbsp;Parts</strong>
							</div>		
							</td>
                      </tr>
					   
					  <tr>
                        <td height="26">						
						<%
							dim c_list, c_list_arr
							set parentrs = conn.execute("select menu_child_list from tb_system_configure_category")
							if not parentrs.eof then
								qn=0
								dim area_group_count
								area_group_count =0
								do while not parentrs.eof 
								
									qn=qn+1
									'response.write parentrs("menu_child_list") & "<br>"
									response.Write("<div id=""cust_plane_"&qn&""" style=""display:")
									if qn=1 then response.Write "" else response.Write "none;"
									response.Write( """>")
									c_list = parentrs("menu_child_list")
									if trim(c_list) <> "" then 
									
									set rs = conn.execute("select  count(pg.part_group_id) from tb_part_group pg inner join tb_product_category mc on pg.product_category=mc.menu_child_serial_no inner join  tb_sp_tmp_detail sp on  sp.part_group_id=pg.part_group_id inner join tb_product p on p.product_serial_no=sp.product_serial_no where sys_tmp_code="&sys_tmp_sku&" and product_category in ("&c_list&") and pg.showit=1  and p.tag=1 order by menu_child_order, sp.product_order asc")
									if not rs.eof then
										area_group_count = rs(0)
									end if
									rs.close
									set rs = conn.execute("select sp.sys_tmp_detail,pg.part_group_id,pg.part_group_name,mc.menu_child_serial_no,menu_child_name,menu_pre_serial_no, sp.product_order,p.product_current_price, p.product_name, p.product_serial_no, p.other_product_sku,p.is_non from tb_part_group pg inner join tb_product_category mc on pg.product_category=mc.menu_child_serial_no inner join  tb_sp_tmp_detail sp on  sp.part_group_id=pg.part_group_id inner join tb_product p on p.product_serial_no=sp.product_serial_no where sys_tmp_code="&sys_tmp_sku&" and product_category in ("&c_list&") and pg.showit=1  and p.tag=1 order by menu_child_order, sp.product_order asc")
									
										if not rs.eof then
											dim current_price, plane_count, part_group_id, area_count, sys_tmp_detail
											acce_n = 0 '输出CPU等产品
											plane_count = 0
											area_count = 0
											
											do while not rs.eof 
												sys_tmp_detail = rs("sys_tmp_detail")
												area_count = area_count + 1
												part_group_id = rs("part_group_id")
												plane_count = plane_count + 1
												current_price = cdbl(rs("product_current_price"))

%>
				
				<div id="product_plane_<%=qn%>" >
				
					<table  width="100%" height="22" border="0" cellpadding="0" cellspacing="0" bgcolor="#efefef" style="cursor:hand; border-top: 1px solid #cccccc; <%if area_count = area_group_count then response.Write("border-bottom: 1px solid #cccccc;")%>" id="plane_<%=qn%>_<%=area_count%>">
						<tr onClick="displayProductGroup('product_group_<%=rs("menu_child_serial_no")%>','product_check_<%=rs("menu_child_serial_no")%>','img_v_<%=rs("menu_child_serial_no")%>_<%=area_count%>','table_plane_group_<%=rs("menu_child_serial_no")%>_<%= plane_count%>', 'plane_<%=qn%>_<%=area_count%>', '<%if area_count = area_group_count then response.Write("1") else response.Write("0") %>');">
							<td width="16">&nbsp;
								<input type="hidden" value="<%=rs("product_serial_no")%>" id="current_<%= sys_tmp_detail %>">
								<input type="hidden" value="img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>" id="current_img_logo_<%= sys_tmp_detail %>">
								<input type="hidden" value="product_child_img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>" id="a_product_name_<%= sys_tmp_detail %>">
                                <script>
                                document.getElementById("a_product_name_<%= sys_tmp_detail %>").value = "product_child_img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>";
                                </script>
							</td>
							<td >
								<table cellpadding="0" cellspacing="0">
									<tr>
										<td style="width: 120px;"><strong><%=  rs ("part_group_name") %>:</strong></td>
										<td><span id="product_head_<%= rs("sys_tmp_detail")%>">
												<%=rs("product_name")%>
											</span>
												<span style="display:none;">
													(<%=rs("part_group_id")%>)	
												</span>		
																		
											</td>
									</tr>
								</table>							</td>
							<td width="24" style="padding-top:3px; " align="center"><a href="#position1"><img  style="cursor:hand" src="/images/products/cust_arrow_2.gif"  border="0" id="img_v_<%=rs("menu_child_serial_no")%>_<%=area_count%>"></a> </td>
							<td width="6">&nbsp;</td>
						</tr>
					</table>
														<%
								dim cc_sum , p_page, p_page_size, plane_count_sub, tmp_sql
								plane_count_sub = 0
								p_page_size = 130
								cc_sum = 0
									
								dim tmp_sql_1, tmp_sql_2
								
								tmp_sql_1 = "select * from (select p.product_serial_no,p.product_name,p.product_short_name,p.product_current_price,p.menu_child_serial_no, p.split_line,part.nominate, '1' as is_nominate, p.other_product_sku,p.is_non,p.product_order, pc.menu_child_order from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where p.product_serial_no<>"& rs("product_serial_no")&" and part.showit=1 and (part.nominate=1 or p.is_non=1) and p.tag=1 and part_group_id="&rs("part_group_id")&_
" union all "&_
"select p.product_serial_no,p.product_name,p.product_short_name,p.product_current_price,p.menu_child_serial_no, p.split_line,part.nominate, '0' as is_nominate, p.other_product_sku,p.is_non,p.product_order, pc.menu_child_order from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no inner join tb_product_category pc on p.menu_child_serial_no=pc.menu_child_serial_no where  p.product_serial_no<>"& rs("product_serial_no")&" and  p.tag=1 and part.showit=1 and  p.split_line=1 and part_group_id="&rs("part_group_id")&"  ) t order by split_line, menu_child_order, product_order asc  "
								' 无 split line 产品
								tmp_sql_2 = "select p.product_serial_no,p.product_name,p.product_short_name,p.product_current_price,p.menu_child_serial_no, p.split_line,part.nominate, '1' as is_nominate, p.other_product_sku,p.is_non from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no where p.product_serial_no<>"& rs("product_serial_no")&" and part.showit=1 and (part.nominate=1 or p.is_non=1) and p.tag=1 and part_group_id="&rs("part_group_id")&_
" union all "&_
"select p.product_serial_no,p.product_name,p.product_short_name,p.product_current_price,p.menu_child_serial_no, p.split_line,part.nominate, '0' as is_nominate, p.other_product_sku,p.is_non from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no where  p.product_serial_no<>"& rs("product_serial_no")&" and p.tag=1 and  part.showit=1  and  part_group_id="&rs("part_group_id")&" "
								set child = conn.execute(tmp_sql_1 )
								' 如果没有纪录，表明没有分割线，没有推荐产品， 则直接查询第二种方式
								if child.eof then 
									dim is_sql_2
									is_sql_2 = true
									set child = conn.execute(tmp_sql_2 )
								end if
								if not child.eof then
									if not is_sql_2 then 
								' 判断是否有split line 产品
										dim is_exist_line
										is_exist_line = false
										do while not child.eof 
											if cstr(child("split_line")) = "1" then 
												is_exist_line = true
											end if
										child.movenext
										loop
										child.movefirst
										
										if not is_exist_line or is_exist_line="" then 
											set child = conn.execute(tmp_sql_2 )
											
										end if
									end if
															
														%>
					<!--begin part group -->
					<div id="product_part_group_id_<%=part_group_id%>_<%=plane_count%>" style="border:1px solid #ffffff;">				
					<table width="100%"  border="0" cellpadding="2" cellspacing="0" bgcolor="#FFFFFF" id="table_plane_group_<%=rs("menu_child_serial_no")%>_<%= plane_count%>"  style="display:none">
						<tr>
							
							<td>
							<table cellpadding="0" cellspacing="0" border="0" width="100%">
							<tr>
								<td width="80">
								<span style="position:relative;">
									<span style="position:absolute; z-index:2000; top: 0px; left: 0px">
										<img src="/pro_img/COMPONENTS/<%=rs("product_serial_no")%>_t.jpg" width="50" height="50" id="img_product_<%= sys_tmp_detail%>_<%=rs("product_serial_no")%>" onerror="imgerror(this);" onClick="javascript:popImage('/<%=product_image_url & PartChoosePhotoSKU(rs("product_serial_no"),rs("other_product_sku")) %>_list_1.jpg','Lu Computers','middle_center',true,true);return false;" style=" cursor: pointer"> 
									</span>
								</span>
								</td>
								
								<td width="10" nowrap="nowrap" name="product_group_<%=rs("menu_child_serial_no")%>" id="product_group_<%=child("product_serial_no")%>"  valign="top" style="!important;padding-top:1px;">

							         <input 
										name="product_check_<%=sys_tmp_detail%>" 
										id="product_check_<%=child("product_serial_no")&"_"&plane_count&"_"&plane_count_sub%>" 
										checked="true"
										type="radio" 
										value="<%=child("product_serial_no")%>" 
										onclick="getProductName('img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>', 'product_child_img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>','product_head_<%= rs("sys_tmp_detail")%>', '<%=sys_tmp_detail%>', '<%=rs("product_serial_no")%>','<%= sys_tmp_sku %>');"></td>
									<td valign="top">
										<table width="100%">
											<tr>
												<td>
                                                	
													<a class="hui-red" 
													<% if (rs("is_non")) <> 1 then %>
                                                    href="/site/view_part.asp?id=<%= rs("product_serial_no")%>" onClick="js_callpage_name(this.href, 'custom_part_detail');return false;"  
                                                    <% end if %>
                                                    id="product_child_img_product_<%= sys_tmp_detail %>_<%=rs("product_serial_no")%>"
													style='color: #ff9900;'>
                                                    
                                                    <%
													product_title = rs("product_name")
													if product_title = "" then 
														product_title=rs("product_short_name")
													end if
													if rs("is_non") = 0 and instr(product_title, "onboard")<1 and instr(product_title, "warranty") <1 then %>
                                                    (Featured)&nbsp;
                                                    <%end if
													
													response.write product_title%>	
                                                    
													</a>	
                                                    
												</td>
												<td style="text-align:right;" valign="top">
													<% dim save_cost, current_price_rate
													save_cost = GetSavePrice(rs("product_serial_no"))
													current_price_rate = changePrice(rs("product_current_price"), card_rate)
													if cint(save_cost) = 0 then 
													%>
													<span id="product_child_price_<%=rs("product_serial_no")%>">
															$<%= formatnumber( current_price_rate , 2)%>
													</span>
													<% else%>
													<span id="product_child_price_<%=rs("product_serial_no")%>">
															$<%= formatnumber(current_price_rate - cdbl(save_cost), 2)%>
													</span>
													<span style="text-decoration:line-through;color: #cccccc;">
															$<%= formatnumber(current_price_rate,2) %>
													</span>
													<% end if %>
													
												</td>
											</tr>
										</table>
									</td>
									</tr>
						<%	
							dim is_sub_group,pre_is_sub_group, is_sub_group_view, view_sub_group_id, is_view_sub_group, image_postion_rate, plane_count_2
							is_sub_group = 0
							is_sub_group_view = 0
							is_view_sub_group = 0
							image_postion_rate = 0
							do while not child.eof  
							plane_count_2 = plane_count
							' 判断产品在列表中位置是第几个， 然后判断并处理图片的位置。
							image_postion_rate = image_postion_rate + 1
							
							' 取得此类的所有产品ID号，方便更改价格使用
							child_list_serial = child_list_serial & "," & child("product_serial_no")

							plane_count_sub = plane_count_sub + 1
							' 是否是分割线
							if(child("split_line") = 1) then
								is_sub_group = child("product_serial_no") 
								dim split_line_sub_product_ids, split_add
								split_line_sub_product_ids = "0"
								split_add = false
								set subrs = conn.execute("select p.product_serial_no,p.split_line from tb_part_group_detail part inner join tb_product p on p.product_serial_no = part.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where  p.product_serial_no<>"& rs("product_serial_no")&" and   part.showit=1 and p.is_non=0 and  p.tag=1  and part_group_id="&rs("part_group_id")&" order by pc.menu_child_order, p.product_order asc")
								if not subrs.eof then
									do while not subrs.eof 
										
										if split_add = true and (subrs("split_line") = 1) then 
											split_add = false
										end if
										if subrs("product_serial_no") = child("product_serial_no") then 
											split_add = true
										end if
										
										if split_add = true then
											split_line_sub_product_ids = split_line_sub_product_ids & "," & subrs("product_serial_no")
										end if
										
									subrs.movenext
									loop
								end if
								subrs.close : set subrs = nothing
							%>
							<tr>
							<td width="80" style="max-width:80px">&nbsp;								
							</td>
							<td colspan="2" style="text-align:left" width="520px">	
							<div style="text-align:left;color: green;background: #f2f2f2;" onClick="ViewSubGroup('sub_group_<%= plane_count %>_<%= rs("part_group_id") %>_<%=child("product_serial_no")%>', 'img_exp_<%= plane_count %>_<%=is_sub_group%>',this, '<%= split_line_sub_product_ids %>', '<%= plane_count %>', '<%= rs("part_group_id") %>', 'split_line_<%= plane_count %>_<%= rs("part_group_id") %>_<%=child("product_serial_no")%>', '<%= rs("sys_tmp_detail") %>', '<%= sys_tmp_sku %>');">
								<table style="width: 100%" align="left" border="0">
									<tr>
										<td style="width:10px">
											<img id="img_exp_<%= plane_count %>_<%=is_sub_group%>" src="/images/col.gif">
										</td>
										<td style="text-align:left;color: green;background: #f2f2f2;">
											<%= child("product_short_name")%>
										</td>
									</tr>
								</table>
							</div>					
							</td>
							</tr>
							<tr id="sub_group_<%= plane_count %>_<%= rs("part_group_id") %>_<%=child("product_serial_no")%>" style="display:none">
								<td colspan="3" id="split_line_<%= plane_count %>_<%= rs("part_group_id") %>_<%=child("product_serial_no")%>" style="text-align:center">Load......</td>
							</tr>
							<%
								' 如果有标题，就隐藏其子产品
								is_sub_group_view = 1
								
								
								' 展开选中产品的组
								if is_view_sub_group = 1 then 
									is_view_sub_group = 0
									response.write "<script>ViewSubGroup('sub_group_"& pre_is_sub_group &"_"&plane_count&"_','img_exp_"& plane_count_2 &"_"&pre_is_sub_group&"', this);</script>"
								end if
								
								pre_is_sub_group = is_sub_group
								plane_count_2 = plane_count
								' 存储组ID
								view_sub_group_id = child("product_serial_no")
							else
							%>
							<tr  onMouseOver="this.bgColor='#f2f2f2';" onMouseOut="this.bgColor='white';" id="sub_group_<%= is_sub_group%>_<%= plane_count %>_<%=image_postion_rate%>" <% 'if is_sub_group_view = 1 then response.write " style='display:none;' " %>>
							<td width="80">
							<%
							' 如果是当前的被选中，则输入图片们位置
							'if (cstr(rs("product_serial_no")) = cstr(child("product_serial_no"))) then 
							%><span style="position:relative;">
							<span style="position:absolute; z-index:2000; top: 0px; left: 0px"><img src="/pro_img/COMPONENTS/<%=PartChoosePhotoSKU(child("product_serial_no"),child("other_product_sku"))%>_t.jpg" width="50" height="50" id="img_product_<%=sys_tmp_detail%>_<%= child("product_serial_no") %>" onerror="imgerror(this);" style="display:none;" onClick="javascript:popImage('/<%=product_image_url & PartChoosePhotoSKU(child("product_serial_no"),child("other_product_sku")) %>_list_1.jpg','Lu Computers','middle_center',true,true);return false;" style=" cursor: pointer"> </span></span>
							<%	
							'else
							'	response.write "&nbsp;"
								
							'end if
							%>&nbsp;
							</td>
							<td width="10" nowrap="nowrap" name="product_group_<%=rs("menu_child_serial_no")%>" id="product_group_<%=child("product_serial_no")%>"  valign="top" style="!important;padding-top:1px;">

							         <input 
										name="product_check_<%=sys_tmp_detail%>" 
										id="product_check_<%=child("product_serial_no")&"_"&plane_count&"_"&plane_count_sub%>" 
										<%
											if (rs("product_serial_no") = child("product_serial_no")) then 
												response.Write( " checked=""true"" " ) 
												is_view_sub_group = 1
											end if
										%>
										type="radio" 
										value="<%=child("product_serial_no")%>" 
										onclick="getProductName('img_product_<%= sys_tmp_detail %>_<%=child("product_serial_no")%>', 'product_child_img_product_<%= sys_tmp_detail %>_<%=child("product_serial_no")%>','product_head_<%= rs("sys_tmp_detail")%>', '<%=sys_tmp_detail%>', '<%=child("product_serial_no")%>', '<%= sys_tmp_sku %>');"></td>
									
									<td valign="top">
										<table width="100%">
											<tr>
												<td>
                                                	
													<a class="hui-red" 
                                                    <% if (child("is_non")) <> 1 then %>
                                                    href="/site/view_part.asp?id=<%= child("product_serial_no")%>" onClick="js_callpage_name(this.href, 'custom_part_detail');return false;"  
                                                    <% end if %>
                                                    id="product_child_img_product_<%= sys_tmp_detail %>_<%=child("product_serial_no")%>" >
													
													<%
													product_title = child("product_name")
													if product_title = "" then 
														product_title=child("product_short_name")
													end if
													
													if child("nominate") = 1 and child("is_nominate") = "1" then
															 if child("is_non") = 0 and instr(lcase(product_title), "onboard")<1 and instr(lcase(product_title), "warranty") <1 then
															 response.write "<span style=''>(Featured)&nbsp;</span>"
												    		 end if
													end if
													response.Write(product_title)
													%>
													
													</a>
													
										
												</td>
												<td style="text-align:right;" valign="top">
													<% 'dim save_cost, current_price_rate
													save_cost = GetSavePrice(child("product_serial_no"))
													current_price_rate = changePrice(child("product_current_price"),card_rate)
													if cint(save_cost) = 0 then 
													%>
													<span id="product_child_price_<%=child("product_serial_no")%>">
															$<%= formatnumber( current_price_rate , 2)%>
													</span>
													<% else%>
													<span id="product_child_price_<%=child("product_serial_no")%>">
															$<%= formatnumber(current_price_rate - cdbl(save_cost), 2)%>
													</span>
													<span style="text-decoration:line-through;color: #cccccc;">
															$<%= formatnumber(current_price_rate,2) %>
													</span>
													<% end if %>
													
												</td>
											</tr>
										</table>
									</td>
															
								</tr>			
							<% 
							' 分割线结束
							end if
								if not end_page then
									'response.write "</div>"
								end if
								'is_sub_group_view = 0
								child.movenext:loop
							%>
							<tr><td>&nbsp;</td>
								<td colspan="2" style="text-align:right; height:30px; padding-right:8px;">
									<%
										'if is_view_sub_group = 1 then 
'											is_view_sub_group = 0
'											response.write "<script>ViewSubGroup('sub_group_"& pre_is_sub_group &"_"&plane_count&"_','img_exp_"& plane_count_2 &"_"&pre_is_sub_group&"', this);</script>"
'										end if
									%>
									<!-- Close -->
										<span align="right" onClick="displayProductGroup('product_group_<%=rs("menu_child_serial_no")%>','product_check_<%=rs("menu_child_serial_no")%>','img_v_<%=rs("menu_child_serial_no")%>_<%=area_count%>','table_plane_group_<%=rs("menu_child_serial_no")%>_<%= plane_count%>', 'plane_<%=qn%>_<%=area_count%>', '<%if area_count = ubound(split(c_list, ",")) then response.Write("1") else response.Write("0") %>');" style="cursor: pointer;"><img src="/Images/close_custome.gif"></span>
									    
									<!-- close end -->
								</td>
							</tr>
							
								</table>
							<%
								' 输出此类所有产品ID
								Response.write ("<input type=""hidden"" name=""child_list_ID_"&rs("menu_child_serial_no")&""" id=""child_list_ID_"&rs("menu_child_serial_no")&""" value="""&child_list_serial&""">")
								response.write ("</div>")

							'response.write p_page
							%>
							</td>
						</tr>
					</table>
					
					</div>
					<!--END part group -->
														<%end if
															child.close:set child = nothing
														
														
															%>
				</div>
														<%
															
											
																	'response.Write("<div>"&rs("product_name")&"</div>")
											rs.movenext
											loop
										end if
										rs.close :set rs = nothing									
									end if
									
												
									response.Write("</div>")

									
								parentrs.movenext
								loop
							end if
							parentrs.close : set parentrs= nothing
						%>
						
						</td>
                      </tr>
                      <tr>
                        <td></td>
                      </tr><%if session("MN")>2 then%><%end if%>
                      <!--tr>
                        <td height="25" valign="bottom"><a href="#" class="red-black">Continue selecting Major Components</a> | <a href="#" class="red-black">Continue selecting Accessories</a> | <a href="#" class="red-black">Continue selecting Additional Parts</a> </td>
                      </tr-->
                    </table>
					</form>
					
                      </td>
                    </tr>
                  </table>
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td height="40" align="right" class="text_red_12b">Updated Price : <span id="currentprice_3"><strong><%=formatcurrency(getSystemProductPrice)%></strong></span> </td>
                      </tr>
                      <tr>
                        <td height="25" align="right" class="text_small">
						
						<table width="70%"  border="0" cellspacing="0" cellpadding="0" align="right">
                          <tr align="center">
						  	 <td></td>
							 <td ><table id="__01" width="155" height="24" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="28"><img src="/images/Review.gif" width="28" height="24" alt=""></td>
                                  <td align="center" background="/images/customer_bottom_03.gif"><div id="view_system_print"><a href="/view_print_system_customer.asp?change=true&cmd=print&id=<%=sys_tmp_sku%>" onClick="return js_callpage_custome(this.href)"  class="white-hui-12"><strong>System Review</strong></a></div></td>
                                  <td width="6"><img src="/images/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                            <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="28"><img src="/images/reset.gif" width="28" height="24" alt=""></td>
                                  <td align="center" background="/images/customer_bottom_03.gif"><a href="<%=Request.ServerVariables("HTTP_url")%>" class="white-hui-12"></a><div  onClick="window.document.location.reload();" style="cursor:pointer; color:white" ><strong >Reset</strong></div> </td>
                                  <td width="6"><img src="/images/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                            <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="28"><img src="/images/arrow_right.gif" width="28" height="24" alt=""></td>
                                  <td align="center" background="/images/customer_bottom_03.gif"><a href="javascript:customerSubmit('<%= sys_tmp_sku%>');"  class="white-hui-12"><strong><span id="submit_button">Next</span></strong></a> </td>
                                  <td width="6"><img src="/images/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                </tr>
                            </table></td>
                          </tr>
                        </table></td>
                      </tr>
                      <tr>
                        <td height="25" align="right" class="text_small"><span class="text_hui_11"><a href="/view_print_system_customer.asp?change=true&cmd=print&id=<%=sys_tmp_sku%>" onClick="return js_callpage_custome(this.href)"  >To keep your configuration for future use, save and obtain System Number.</a></span></td>
                      </tr>
                    </table>
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td style="padding-bottom:5px; "><table width="100%"  border="0" cellpadding="3" cellspacing="0">
                          <tr>
                            <td width="70%">&nbsp;</td>
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td width="32"><img src="/images/products/print.gif" width="32" height="31" alt=""></td>
                                  <td background="/images/save_title_02.gif"><a href="/view_print_parts.asp?id=<%=request("id")%>" target="_blank" class="hui-red"> </a><a onClick="return js_callpage_custome(this.href)" href="/ask_question.asp?cate=<%=request("pro_class")%>&id=<%=sys_tmp_sku%>&type=2&change=true" target="_blank" class="hui-red">Ask  a Question</a></td>
                                  <td width="9"><img src="/images/save_title_03.gif" width="9" height="31" alt=""></td>
                                </tr>
                            </table></td>
                            
                            <td><table id="__01" width="100%" height="31" border="0" cellpadding="0" 

cellspacing="0" style="display:none">
                                <tr>
                                  <td width="32"><img src="/images/save_title_01.gif" width="32" height="31" alt=""></td>
                                  <%
								'session("sys")=pdrs("info")
'								session("sys")=replace(session("sys"),chr(10),"")
'								session("sys")=replace(session("sys"),chr(13),"")
'								session("sys")=replace(session("sys"),"<P>","")
'								session("sys")=replace(session("sys"),"</P>","")
'								session("sys")=replace(session("sys"),"<BR>","")
'								session("sys")=replace(session("sys"),"&nbsp;","")
'								session("sys")=replace(session("sys")," ","")
'								session("price")=pdrs("price")
								%>
                                  <td background="/images/save_title_02.gif">
							  	  </td>
                                  <td width="9"><img src="/images/save_title_03.gif" width="9" height="31" alt=""></td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                          </tr>
                        </table></td>
                      </tr>
                    </table>
                    <table style="border-top:#e3e3e3 1px solid; " width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td  style="padding:10px; "><span class="text_hui_11">Prices, system package content and availability subject to change without notice. </span>
                          <p class="text_hui_11">Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata.</p></td>
                      </tr>
                    </table></td>
                </tr>
            </table></td>
          <td valign="top" style="padding-left:1px; ">
            <DIV id="IconDiagram_Layer"  style="position:absolute; left: 0px; top: 0px; ">
              <table width="162" height="120"  border="0" cellpadding="0" cellspacing="0" background="/Images/fly_left_bg.gif">
                <tr>
                  <td><table width="93%"  border="0" align="center" cellpadding="2" cellspacing="0">
                      <tr>
                        <td class="text_red2_12">Updated Price<br>
                            <strong><span id="currentprice_1"><%=formatcurrency(getSystemProductPrice)%></span></strong></td>
                      </tr>
                      <tr>
                        <td><!--table width="100%"  border="0" cellspacing="0" cellpadding="0">
                        <tr>
                         
                          <td><a  onclick="return js_callpage2(this.href)" href="view_print.asp" class="hui-red" target="_blank"> <img src="/images/fly_print.gif" width="39" height="17" border="0"></a></td>
                          <td><a onClick="return js_callpage(this.href)" href="email_me.asp" target="_blank" class="hui-red"><img src="/images/fly_email.gif" width="39" height="17" border="0"></a></td>
                        </tr>
                      </table--></td>
                      </tr>
                  </table>
                    <table width="93%"  border="0" align="center" cellpadding="2" cellspacing="0">
                      <tr>
                        <td colspan="3" align="right"><img src="/images/fly_submit.gif" width="42" height="17"  style="cursor:pointer" border="0" onClick="window.location.replace('/system_to_temp.asp?t_s_id=<%= sys_tmp_sku%>');"></td>
                      </tr>
                      <tr>
                        <td colspan="3"><a onClick="js_callpage_win('/system_quote.asp?1=1', 'system_quote', 450, 300, 300 , 300);" style="cursor:pointer;"><span class="text_white_11">To Obtain Quote Number</span></a></td>
                      </tr>
                      <tr>
                        <td width="33%"><a href="/view_print_system_customer.asp?change=true&cmd=print&id=<%=sys_tmp_sku%>" onClick="return js_callpage_custome(this.href)" ><img src="/images/fly_view.gif" width="39" height="17" border="0"></a></td>
                      <td width="33%" align="center"><a href="/view_print_system_customer.asp?change=true&cmd=print&id=<%=sys_tmp_sku%>" onClick="return js_callpage_custome(this.href)" ><img src="/images/fly_print.gif" width="39" height="17" border="0"></a></td>
                        <td width="33%" align="right"><a href="/view_print_system_customer.asp?change=true&cmd=email&id=<%=request("id")%>" onClick="return js_callpage_custome(this.href)" ><img src="/images/fly_email.gif" width="39" height="17" border="0"></a></td>
                      </tr>
                    </table></td>
                </tr>
              </table>
            </DIV>
        
            <!--#include virtual="/right.asp"-->
           
            </td>

        </tr>
    </table></td>
    <td valign="bottom" background="/Images/left_middle.gif"><img src="/soft_img/app/left_bt.gif" width="13" height="214"></td>
  </tr>
</table>
<%
function getParentCategory(menu_child_serial_no)
	set getrs = conn.execute("select * from tb_product_category where menu_child_serial_no="&menu_child_serial_no)
	if not getrs.eof then
		'response.Write(menu_child_serial_no)
		if getrs("menu_pre_serial_no") = "2" then
			response.Write getrs("menu_child_name")
		else
			getParentCategory (getrs("menu_pre_serial_no"))
		end if
	end if
	getrs.close:set getrs= nothing
end function 
%>

<!--include virtual="/bottom.asp"-->
<qiozi>bottom</qiozi>
<iframe src="/blank.html" id="ifrmae1" width="0" height="0" frameborder="0"></iframe>
<iframe src="/blank.html" id="ifrmae2" width="0" height="0" frameborder="0"></iframe>
<script>
document.onload = __OnLoad_Diagram (); 
</script>
</body>
</html>
