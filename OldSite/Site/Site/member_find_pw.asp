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
                    <span class="nav1">Member Find Password</span>
                </div>
            	<div id="page_main_area">
                	<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                          <tr>
                            <td valign="top" style="border:#E3E3E3 1px solid; ">
                            
                            <%
function GetSendCustomerPassword(username, password)
	GetSendCustomerPassword = "Hi "& username
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
	GetSendCustomerPassword = GetSendCustomerPassword & "Login name: "& username
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
	GetSendCustomerPassword = GetSendCustomerPassword & "Password  : "& password
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
	GetSendCustomerPassword = GetSendCustomerPassword & "<br><br>"
end function 

                                if request.Form("ok2") <> "ok" then 
                            %>
                            <!-- input email begin-->
                            <table width="95%" height="100"  border="0" align="center" cellpadding="2" cellspacing="0">
                              <form name="form2" method="post" action="member_find_pw.asp">
                                <tr>
                                  <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                  <td>&nbsp;</td>
                                  <td class="text_hui_11">Please fill in the following to get your new password:<br>
                                     If you still cannot solve the problem please call 1866.999.7828 or email <a href="mailto:support@lucomputers.com" class="orag-blue" style="font-size:11px;">support@lucomputers.com </a> for help. </span> </td>
                                </tr>
                                <tr>
                                  <td width="120"><img src="/soft_img/app/fangdajing.gif" width="103" height="103"></td>
                                  <td><table width="85%"  border="0" cellpadding="0" cellspacing="0">
                                      <tr>
                                        <td colspan="2" class="text_white"><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                            <tr>
                                              <td class="text_hui_11">User Name：</td>
                                              <td><input name="username" type="text" style="height:16px; width:150px;" size="20"></td>
                                            </tr>
                                            <tr>
                                              <td class="text_hui_11">Your Registered Email：</td>
                                              <td><input name="email" type="text" style="height:16px; width:150px;" value="" size="20">
                                                  <input type=hidden value="ok" name="ok2"></td>
                                            </tr>
                                        
                                        </table></td>
                                      </tr>
                                  </table></td>
                                </tr>
                                <tr>
                                  <td>&nbsp;</td>
                                  <td align="center"><table width="50%"  border="0" align="center" cellpadding="3" cellspacing="0">
                                      <tr align="right">
                                        <td>
                                          <table id="__01" width="170" height="24" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                              <td width="6"> <img src="/soft_img/app/customer_bottom_08.gif" width="6" height="24" alt=""></td>
                                              <td align="center" background="/soft_img/app/customer_bottom_03.gif"><a href="javascript:document.form2.reset()" class="white-hui-12"><strong>Reset</strong></a> </td>
                                              <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                            </tr>
                                        </table></td>
                                        <td>
                                          <table id="__01" width="170" height="24" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                              <td width="6"> <img src="/soft_img/app/customer_bottom_08.gif" width="6" height="24" alt=""></td>
                                              <td align="center" background="/soft_img/app/customer_bottom_03.gif" style="cursor:pointer" onclick="document.form2.submit();"><a href="#" class="white-hui-12"><strong>Submit</strong></a> </td>
                                              <td width="6"> <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                                            </tr>
                                        </table></td>
                                      </tr>
                                    </table></td>
                                </tr>
                              </form>
                            </table>
                            <!-- input email begin-->
                            
                        <%else%>
                            <!-- send email begin-->
                            <div style="height:100px; text-align:center">
                            <%
							Dim email_sql 
							email_sql = ""
							if len(SQLescape(request("email"))) >0 then 
								email_sql = email_sql & " customer_email1='"&SQLescape(request("email"))&"' or customer_login_name='"&SQLescape(request("email"))&"' " 
							end if
								
							if len(SQLescape(request("username"))) >0 then
								if len(email_sql) >0 then 
									email_sql = email_sql & "or customer_login_name='"& SQLescape(request("username"))&"'"
									
								else
									email_sql = " customer_login_name='"& SQLescape(request("username"))&"'"
								end if
							end if
							
                            set rs = conn.execute("select * from tb_customer where " & email_sql )
                            if not rs.eof then
                                        dim send_email, jmail
										send_email	=	""
                                        if IsEmail(rs("customer_email1")) then 
                                            send_email = rs("customer_email1")
                                        end if
                                        if IsEmail(rs("customer_login_name")) then 
                                            send_email = rs("customer_login_name")
                                        end if
										'response.Write(send_email)
										if send_email <> "" then 
											call SendEmail("Lu Computers - User Info", GetSendCustomerPassword(rs("customer_login_name"), rs("customer_password")) , send_email)
											'set jmail=server.CreateObject ("jmail.message")
											'jmail.Silent=true
											'jmail.charset="utf-8"
											'jmail.addrecipient   send_email '"xiaowu021@126.com"
											'jmail.subject = " Lu Computers - User Info "
											'jmail.appendtext  "This is a mail of HTML type"
											'jmail.appendhtml   GetSendCustomerPassword(rs("customer_login_name"), rs("customer_password"))
											'jmail.from = "sale@lucomputers.com"
											'jmail.fromname="LU COMPUTERS"
											'jmail.priority = 3
											'jmail.MailServerUserName ="sales@lucomputers.com"
											'jmail.MailServerPassWord = "5calls2day"
											'err=jmail.send("p3smtpout.secureserver.net")
											''err=jmail.send("smtp.lucomputers.com")
											''err=jmail.send("smtp.126.com")
											'jmail.close
											'set jmail=nothing
                                        End if
                                        'response.write send_email & "dd"
                                    'response.write cont
                                    'if (err) then 
                                        response.write "<br><br>You have successfully submitted request to retrieve your password.<br>Please check your email in a few minutes. "
                                   ' else
                                       ' response.write "error code: 9867."
                                   ' end if
                            else
                                response.Write("User don't found!")
                            end if
                            rs.close : set rs = nothing
                        %>				
                        
                        <%end if %>
                            </div><!-- send email end-->
                        </td>
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

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
</script>