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
        	    <div class='page_main_nav' id="page_main_nav">
                	<span class="nav1"><a href='<%= LAYOUT_HOST_URL %>'>home</a></span>
                    <span class="nav1">Member Register</span>
                </div>
            	<div id="page_main_area">
                	<%
						Dim prs
						Dim rsIP
					%>
                	<table width="600" height="900"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                          <tr>
                            <td valign="top" style="border:#E3E3E3 1px solid; "><table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                              <tr>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                              </tr>
                              <tr>
                                <td style="padding-right:10px; "><div align="justify"><span class="text_hui_12 style2">Information Privacy</span><span class="text_small"><br>
                                </span><span class="text_hui_11">Lu Computers understands our customer’s wishes for privacy, so we handle all the information you provide to us accordingly. Our conduct with your information will in no way involve a third party organization. <br>
            </span><a href="Privacy_Security.asp" target="_blank" class="orag-blue" style="font-size:11px;">(Learn more about our Privacy and Security policies)</a><br>
                                </div></td>
                                <td rowspan="2" valign="top"><img src="/soft_img/app/right2.jpg" width="178" height="131"></td>
                              </tr>
                              <tr>
                                <td style="padding-right:10px; "><span class="text_hui_12"><strong><br>
                                  Fast Register</strong></span> <br>
                                  <span class="text_hui_11">Just enter your email address and a password so you can securely access all </span></td>
                              </tr>
                            </table>
                              <table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td class="text_hui_11">                        your account information in the future such as order tracking &amp; order history. <br>
                                  Your email address is also used for password retrieval.  </td>
                                </tr>
                                <tr>
                                  <td><span class="text_small"><br>
                                    </span><span class="text_orange_11"><a href="member_find_pw.asp" target="_blank" class="orag-blue" style="font-size:11px;">Already have an account but forgot your password?</a><br>
                  </span> <strong><br>
                  </strong><span class="text_hui_11"><strong>NOTE</strong> All items are required.</span></td>
                                </tr>
                              </table>
                              <table width="95%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                  <td height="20" align="right"><table width="100%"  border="0" cellpadding="0" cellspacing="1" bgcolor="#D0DAE1">
                                      <tr>
                                        <td height="3" bgcolor="#FFFFFF"></td>
                                      </tr>
                                  </table></td>
                                </tr>
                                <tr>
                                  <td>&nbsp;</td>
                                </tr>
                                <tr>
                                  <td><!--include file="public_helper/md5.asp"-->
                                <% if request("cmd") = "" or isnull(request("cmd")) then %>
                               <form method="post" action="member_register_order.asp" name="fm1" id="fm1">
                                    <input type="hidden" name="cmd" value="validate">
                                      <table width="95%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                        <tr>
                                          <td width="25%" class="text_hui_11">Email:</td>
                                          <td><input name="Email" id="email" type="text" style="width:200px; " tabindex="1" class="input" size="22" ></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Password:</td>
                                          <td><input name="user_pwd" id="user_pwd" type="password" style="width:200px; " tabindex="2" class="input" size="22" >                                &nbsp;&nbsp;</td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11"> Verify Password:</td>
                                          <td><input name="confirmpwd" id="confirmpwd" type="password" style="width:200px; " tabindex="3" class="input" size="22" >&nbsp;&nbsp;</td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">First Name:</td>
                                          <td><input name="FN" id="fn" type="text" class="input" style="width:200px; " tabindex="4" size="22" ></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Last Name:</td>
                                          <td><input name="LN" id="ln" type="text" class="input" style="width:200px; " tabindex="5" size="22" ></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Country:</td>
                                          <td><select name="user_Country" id="user_country" class="b"  tabindex="7"  style="width:200px; " onChange="cardChangeCountry( '', 'user_State');">                                            
                                            <option value="CA" selected>Canada</option>
                                            <option value="US">United States</option>
                                          </select></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Province/State:</td>
                                          <td>
                                          <div id="div_user_state"></div></td>
                                        </tr>
                                        <tr>
                                          <td colspan="2" class="text_hui_11">Where did you hear about us? </td>
                                        </tr>
                                        <tr>
                                          <td>&nbsp;</td>
                                          <td><div id=state>
                                            <input name="where_about_us" id="where_about_us" type="text" class="input" style="width:200px; " tabindex="8" size="22" maxlength="80">
                                          </div> </td>
                                        </tr>
                                        <tr>
                                          <td colspan="2"><table width="50%"  border="0" align="center" cellpadding="3" cellspacing="0">
                                            <tr align="right">
                                              <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                  <td class="btn_middle"><a style="color:White;" onClick="return toSubmit();"><strong>Reset</strong></a></td>
                                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                </tr>
                                              </table></td>
                                              <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                  <td class="btn_middle"><a style="color:White;" class="white-hui-12" onClick="checkForm();"><strong>Submit</strong></a></td>
                                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                </tr>
                                              </table></td>
                                            </tr>
                                          </table></td>
                                        </tr>
                                      </table>
                                    </form>
                                    <% end if %>
                                    <!-- validate begin-->
                                    <% if request("cmd") = "validate" then %>
                                    <form method="post" action="member_register_order.asp" name="fm2" id="fm2">
                                    <input type="hidden" name="cmd" value="update">
                                    <h2 style="text-align:center;padding:1em; font-size:1.5em;">Please verify and confirm</h2>
                                      <table width="95%"  border="0" align="center" cellpadding="2" cellspacing="0">
                                        <tr>
                                          <td width="24%" class="text_hui_11">Email:</td>
                                          <td><input name="Email" id="u_email" type="hidden" style="width:200px; " tabindex="1" class="input" size="22"  value="<%= request("Email") %>"><%= request("Email") %></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Password:</td>
                                          <td>
                                          <input name="confirmpwd" id="u_confirmpwd" type="hidden" style="width:200px; " tabindex="3" class="input" size="22"  value="<%= request("confirmpwd") %>" >
                                          <input name="user_pwd" id="u_user_pwd" type="hidden" style="width:200px; " tabindex="2" class="input" size="22" value="<%= request("user_pwd") %>" >                                <%= request("user_pwd") %></td>
                                        </tr>
                                        
                                        <tr>
                                          <td class="text_hui_11">First Name:</td>
                                          <td><input name="FN" id="u_fn" type="hidden" class="input" style="width:200px; " tabindex="4" size="22"  value="<%= request("FN") %>"><%= request("FN") %></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Last Name:</td>
                                          <td><input name="LN" id='u_ln' type="hidden" class="input" style="width:200px; " tabindex="5" size="22"  value="<%= request("LN") %>"><%= request("LN") %></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Country:</td>
                                          <td>
                                          <input name="user_Country" id="u_user_country" type="hidden" class="input" style="width:200px; " tabindex="4" size="22"  value="<%=request("user_Country")%>">
                                          <% if request("user_Country")= "CA" then response.Write("Canada") else response.Write("United States") %></td>
                                        </tr>
                                        <tr>
                                          <td class="text_hui_11">Province/State:</td>
                                          <td>

                                         <input name="user_State" id="u_user_state" type="hidden" class="input" style="width:200px; " tabindex="4" size="22"  value="<%=  request("user_State")%>">
                                          <%  response.Write( (request("user_State")))%>
                                          </td>
                                        </tr>
                                        <tr>
                                          <td colspan="2" class="text_hui_11">Where did you hear about us? </td>
                                        </tr>
                                        <tr>
                                          <td>&nbsp;</td>
                                          <td><div id=state>
                                            <input name="where_about_us" type="hidden" class="input" style="width:200px; " tabindex="8" size="22" maxlength="80" value="<%=  request("where_about_us")%>"><%=  request("where_about_us")%>
                                          </div> </td>
                                        </tr>
                                        <tr>
                                          <td colspan="2"><table width="50%"  border="0" align="center" cellpadding="3" cellspacing="0">
                                            <tr align="right">
                                              <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                  <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="javascript:window.history.go(-1);" class="white-hui-12" onClick="window.history.go(-1);"><strong>Back</strong></a></td>
                                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                </tr>
                                              </table></td>
                                              <td><table id="__01" width="115" height="24" border="0" cellpadding="0" cellspacing="0">
                                                <tr>
                                                  <td width="6"><img src="/soft_img/app/3232.gif" width="6" height="24"></td>
                                                  <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="#" class="white-hui-12" onClick="$('#fm2').submit();return false;"><strong>Confirm</strong></a></td>
                                                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                                </tr>
                                              </table></td>
                                            </tr>
                                          </table></td>
                                        </tr>
                                      </table>
                                    </form>
                                    <% end if %>
                                    <!-- validate end-->
                                    </td>
                                </tr>
                                <tr>
                                  <td>&nbsp;</td>
                                </tr>
                                <tr>
                                  <td>&nbsp;</td>
                                </tr>
                              </table>
                            </td>
                          </tr>
                        </table>
                        <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td height="5"></td>
                          </tr>
                        </table>
                        <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td height="5"></td>
                          </tr>
                        </table>
                        <table width="600"  border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td height="5"></td>
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
<%
    response.Write(request("cmd"))
 if request("cmd")= "update" then 
	if SQLescape(request.form("Email")) <> "" and SQLescape(request.form("user_pwd")) <> "" then 
		'
		' validate password
		'
		if SQLescape(request.form("user_pwd")) <> SQLescape(request.form("confirmpwd")) then 
			response.Write("<script> alert('The Re-enter password is not correct.');window.history.go(-1);</script>")
			
		else	
				'
				' is customer exist.
				'
				set prs = conn.execute("Select * from tb_customer where customer_login_name='" & SQLescape(request.form("Email")) &"' or customer_email1='" & SQLescape(request.form("Email")) &"'")
				if not prs.eof then
					response.Write("<script> alert('The email address you entered has already been registered, please use \'forgot password\' and retrive, or use a different email address to register a new account');window.history.go(-1);</script>")
					
					'response.end
				else		
						set rs=server.CreateObject("adodb.recordset")
						rs.open "select * from tb_customer",conn,1,3
						rs.addnew
						rs("customer_login_name")=SQLescape(request.form("Email"))
						rs("customer_password")=SQLescape(request.form("user_pwd"))
						rs("create_datetime")=now()
						
						if SQLescape(request.form("user_Country")) <> "" then 
							rs("customer_country_code")=SQLescape(request.form("user_Country"))
							rs("customer_business_country_code")=SQLescape(request.form("user_Country"))
							rs("customer_card_country_code")=SQLescape(request.form("user_Country"))
							rs("shipping_country_code")=SQLescape(request.form("user_Country"))
						end if
						
						if SQLescape(request.form("user_State")) <> "" then 
							rs("shipping_state_code")=SQLescape(request.form("user_State"))
							rs("customer_card_state_code")=SQLescape(request.form("user_State"))
							rs("state_code")=SQLescape(request.form("user_State"))
							rs("customer_business_state_code")=SQLescape(request.form("user_State"))
						end if
						
						if SQLescape(request.form("FN")) <> "" then 
							rs("customer_first_name")=SQLescape(request.form("FN"))
							rs("customer_shipping_first_name")=SQLescape(request.form("FN"))
						end if
						
						if SQLescape(request.form("LN")) <> "" then 
							rs("customer_last_name")=SQLescape(request.form("LN"))
							rs("customer_shipping_last_name")=SQLescape(request.form("LN"))
						end if
						

						
						if SQLescape(request.form("Email")) <> "" then 
							rs("customer_email1")=SQLescape(request.form("Email"))
							rs("customer_email2")=SQLescape(request.form("Email"))
						end if
						'rs("customer_credit_card")=""
						'rs("customer_expiry")=""
						'rs("customer_Company")=""
						'rs("EBay_ID")=""
						'rs("customer_note") = ""
						rs("system_category_serial_no") = current_system
						if SQLescape(request.form("where_about_us")) <> "" then rs("customer_rumor") = SQLescape(request.form("where_about_us"))
						'rs("customer_note") = ""
						
						rs("customer_serial_no") = GetNewCustomerCode()
						rs.update
						rs.close()
						set rs = conn.execute("Select * from tb_customer where customer_login_name='" & SQLescape(request.form("Email")) &"'")
						if not rs.eof then
						
							'rs.movelast
							'response.Write(rs("customer_serial_no"))
							Session("user_id") = rs("customer_serial_no")
							response.Cookies("customer_serial_no") = rs("customer_serial_no")
							session("customer_first_name") = ucase(rs("customer_first_name") )
							session("customer_last_name") = ucase(rs("customer_last_name"))
							session("user") = rs("customer_login_name")
							response.Cookies("customer_serial_no") = rs("customer_serial_no")					
							response.Cookies("user") = rs("customer_login_name")				
							response.Cookies("customer_first_name") = ucase(rs("customer_first_name") )			
							response.Cookies("customer_last_name") = ucase(rs("customer_last_name"))
						
							Set rsIP = Server.CreateObject("ADODB.Recordset")
							rsIP.open "tb_login_log",conn,1,3
							rsIP.addnew
							rsIP("login_name")= rs("customer_serial_no")
							rsIP("remote_address")=LAYOUT_HOST_IP
							rsIP("login_datetime")=now
							rsip("login_log_category") = "c"
							rsip("http_user_agent") = ""
							rsIP.update
							rsIP.close
							set rsIP=nothing
						end if
						rs.close
						set rs = nothing
						closeconn()
	
						session("user") = request("user_name")
						session("Email") = request("Email")
						session("isAdmin")=false		
						
						
						if IsExistOrderCode() then 
							response.redirect "Shopping_Cart.asp"
						else
							response.redirect "Member_center_info.asp"
						end if
				end if
				prs.close : set prs = nothing
		end if
	end if
end if
%>
<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
	cardChangeCountry( 'ON', 'user_state');
});

<!--
function cardChangeCountry( selected_state_code, element_id)
{
	var country_code = $('#user_country').val();
	$('#div_user_state').load('/site/inc/inc_get_state.asp?is_have_event=true&state_selected_code='+ selected_state_code+'&element_state_id='+ element_id +'&country_code='+ country_code);
}
function checkForm()
{
    var err = "Please fill up all missing items.";
	if (!IsEmail($('#email').val()))
	{
		alert ("Email Incorrect!");
		$('#email').focus();
		return false;		
	}
	if ($('#user_pwd').val()!=$('#confirmpwd').val())
	{
		alert ("The two passwords are different!");
		$('#user_pwd').val("");
		$('#confirmpwd').val("");
		$('#user_pwd').focus();
		return false;
	}
	if ($.trim($('#user_pwd').val()) == "")
	{
		alert("Password is empty")
		$('#user_pwd').focus();
		return false;
	}
	if ($.trim($('#fn').val()) == "")
	{
		alert(err)
		$('#fn').focus();
		return false;
	}
	if ($.trim($('#ln').val()) == "")
	{
		alert(err)
		$('#ln').focus();
		return false;
	}
	if($('#user_state').val() == "-1")
	{
		alert(err)
		$('#user_state').focus();
		return false;
	}

	document.getElementById('fm1').submit();
}

function toSubmit()
{
	$('#email').val('');
	$('#user_pwd').val('');
	$('#confirmpwd').val('');
	$('#fn').val('');
	$('#ln').val('');
	$('#where_about_us').val('');
	$('#user_country').val('CA');
	cardChangeCountry( 'ON', 'user_state');
	$('#where_about_us').val('');
}
//-->
</script>