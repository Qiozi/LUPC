<!--#include virtual="site/inc/inc_page_top_no.asp"-->
<script>
function check(the)
{
//		var el = the.addr.value;
//		if(IsEmail2(el))
//		{
//			the.submit();
//		}
//		else
//		{
//			alert('input email');
//		}
the.submit();
}
</script>
<%

	'call SendEmail("test", "it from (test)<br/>SKU: "&request("id")&" <br/>" & aq_body, "sales@lucomputers.com,terryeah@gmail.com")
	
	dim category, id
	category = SQLescape(request("cate"))
	product_type = SQLescape(request("type"))
	id = SQLescape(request("id"))
'
'	system need copyed
'	
	if category = "sys" then 
		dim new_system_code
		new_system_code = GetNewSystemCode()
		
		if(new_system_code = "" )then
			new_system_code = GetNewSystemCode()
		end if
		
		call CopyConfigureSystemToCart(session("system_templete_serial_no"), new_system_code,tmp_order_code, false, LAYOUT_HOST_IP )
		id = new_system_code
	end if
	
	dim addr, title, body,sys_name, addrs, is_email
	aq_email = Trim(request.form("addr"))
	addrs = split(aq_email, ",")
	'
	' validate email format
	'
	is_email = true 
	for i= lbound(addrs) to ubound(addrs)
		if not IsEmail(addrs(i)) then 
			is_email = false
		end if
	next
	
	'if instr(ucase(Request.ServerVariables("HTTP_REFERER")), ucase("ask_question.asp"))> 0 and instr(ucase(Request.ServerVariables("HTTP_REFERER")), ucase("http://www.lucomputers.com"))> 0  then 
	if instr(LAYOUT_HOST_IP, "94.102.60")=0 and instr(LAYOUT_HOST_IP, "216.58.44.83") = 0 then 
	        if (is_email) then 
		        aq_title = SQLescape(request("title"))
		        aq_body = SQLescape(request("body"))
		        aq_product_title = SQLescape(request("sys_name"))
        		
		        if (aq_email <> "" )  and aq_email<> "111-222-1933email@address.tst"  then 
			        set rs = server.CreateObject("adodb.recordset")
			        rs.open "select * from tb_ask_question" ,conn,1,3
			        rs.addnew
			        rs("aq_email") =  aq_email
			        rs("aq_title") =  aq_title
			        rs("aq_body") =  aq_body
			        rs("aq_product_title") =  aq_product_title
			        'rs("menu_child_serial_no") =  category
			        rs("product_serial_no") =  id
			        rs("create_datetime") =  now()
			        rs("product_category") = request("product_category")
			        rs("ip") = LAYOUT_HOST_IP
			        rs.update
			        rs.close :set rs = nothing
			        response.write "<script> alert('You have successfully sent the seller a question!');window.close();</script>"
        			
			        call SendEmail(aq_title, "<b style='color:#ff9900;'>it from (" & aq_email & ")</b><br><b>CategoryID: "&category&"<br/>SKU: "&request("id")&" </b><br/><span style='font-size:8pt'>" & server.HTMLEncode( aq_body)&"</span>", "809840415@qq.com")
        			        				
			        response.End()
		        end if
	        else
		        response.Write("<script> alert('Email format error, please re-enter.');window.history.go(-1);</script>")
		        response.End()
	        end if
	
    end if
	
	dim product_name
	product_name = ""
	
	' part product , noebook product
	if product_type = "1"  or product_type = "3" then 
		product_name = getProudctName(id)		
	ELSE
		'SYSTEM 
		product_name = GetSystemNameFromSpTmp(id)
	end if
	
	if new_system_code <> 0 then 
		id = new_system_code
	end if
	
%>

<table width="600" height="100%"  border="0" align=center cellpadding="0" cellspacing="0" bgcolor="#CDE3F2">
  <tr>
    <td width="100%" valign="top" bgcolor="#FFFFFF"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
      <TR>
        <TD height="94" background="/soft_img/app/small_top.jpg" class="text_small"><table width="100%" border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td width="75%">&nbsp;</td>
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
            <td height="27" align="center" bgcolor="#327AB8" class="text_white_12"><strong><%= ucase(product_name)%></strong></td>
          </tr>
        </table>
      <table cellspacing="0" cellpadding="0" width="100%" border="0">
          <tr>
            <td ><table width=100% cellspacing="0" cellpadding="0" border="0" bgcolor="#ffffff" align=center>
                <tr>
                  <td><table width="100%"  border="0" cellpadding="3" cellspacing="1" bgcolor="#327AB8">
                      <tr>
                        <td bgcolor="#FFFFFF"><table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#E3E3E3">
                          <tr>
                            <td align="center" bgcolor="#FFFFFF"><form action="ask_question.asp" method="post" name=fm id=fm >
                                <input type="hidden" name="cate" value="<%=category%>">
                                <input type="hidden" name="id" value="<%= id %>">
								<input type="hidden" name="type" value="<%= product_type %>">
                                <table width="90%"  border="0" align="center" cellpadding="3" cellspacing="1">
                                  <tr>
                                    <td align="left"><span style="font-weight:bold">Local Phone: 416.446.7743</span><br><br>Your Email: (required) </td>
                                  </tr>
                                  <tr>
                                    <td align="left"><input name="addr" type="TEXT" class="input" style="width=300px" value="<%=session("Email")%>" size=52 maxlength=60>
                                    Please use comma , to separate multi email addresses.</td>
                                  </tr>
                                  <tr>
                                    <td align="left">Subject :
									<%
									if len(id) = 8  then 
										response.write "(Quote Number："& id & ")"'12344477
									else
										response.write "(SKU:"& id &")"
									end if
									%> </td>
                                  </tr>
                                  <tr>
                                    <td align="left"><input name="title" type=text class="input" value="Ask a question" size="52" maxlength="100" onFocus="if(this.value=='Ask a question') this.value = '';"></td>
                                  </tr>
                                  <tr>
                                    <td align="left">Message Body - Remaining characters: </td>
                                  </tr>
                                  <tr>
                                    <td align="left"><textarea name=body cols=52 rows=6 wrap=physical class="input" style="width=300px" ></textarea></td>
                                  </tr>
                                  <tr>
                                    <td><table width="100%"  border="0" align="left" cellpadding="3" cellspacing="0">
                                        <tr>
                                          <td align="right"><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                <td align="center" style="background: url(/soft_img/app/customer_bottom_03.gif)"><a href="#" onClick="window.close()" class="btn_img"><strong>Cancel</strong></a> </td>
                                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                              </tr>
                                          </table></td>
                                          <td align="left"><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                              <tr>
                                                <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                <td align="center" style="background: url(/soft_img/app/customer_bottom_03.gif)"><a href="#" onClick="return $('#fm').submit();" class="btn_img"><strong>Submit</strong></a></td>
                                                <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                              </tr>
                                          </table></td>
                                        </tr>
                                    </table></td>
                                  </tr>
                                </table>
                            </form></td>
                          </tr>
                        </table></td>
                      </tr>
                  </table></td>
                </tr>

            </table></td>
          </tr>
      </table></td>
  </tr>
</table>
<%
	closeconn()
	%>
</body>
</html>
