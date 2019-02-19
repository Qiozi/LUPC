<!--#include virtual="site/inc/inc_page_top.asp"-->
<%
	if len(LAYOUT_CCID)>0 then 
		response.Redirect(LAYOUT_HOST_URL & "member_center_info.asp"): response.End()
	End if	
						%>
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" style="width:770px;" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'>Member Login</span>
                </div>
            	<div id="page_main_area">
                		<%
						
						%>
                		 <table width="100%" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                              <tr>
                                <td valign="top" style="border:#E3E3E3 1px solid; "><table width="80%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td><table id="__01" width="538" height="93" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td style="background:url(/soft_img/app/member_login_box_01.gif) no-repeat 0 100%;" height="6"> </td>
                                          <td valign="bottom" style="background:url(/soft_img/app/member_login_box_02.gif) no-repeat 0  100%;" height="6">&nbsp; </td>
                                          <td style="background:url(/soft_img/app/member_login_box_03.gif) no-repeat 0  100%;" height="6"></td>
                                        </tr>
                                        <tr>
                                          <td> <img src="/soft_img/app/member_login_box_04.gif" width="6" height="81" alt=""></td>
                                          <td bgcolor="#1584C9">
                                            <table width="85%"  border="0" align="center" cellpadding="0" cellspacing="0">
                                              <form name="form2" method="post" action="login.asp">
                                              <input type="hidden" name="url" value="<%= Request("url") %>" />
                                                <tr>
                                                  <td width="100"><img src="/soft_img/app/left.gif" width="67" height="68"></td>
                                                  <td colspan="2" class="text_white"><table width="100%"  border="0" cellspacing="0" cellpadding="2">
                                                    <tr>
                                                      <td style='color:#FFFFFF'>User Name:</td>
                                                      <td><input name="username" type="text" class="input2" id="username" size="20" style="width:150px;"></td>
                                                    </tr>
                                                    <tr>
                                                      <td style='color:#FFFFFF'>Password:</td>
                                                      <td><input name="password" type="password" class="input2" id="password" size="20" style="width:150px;"></td>
                                                    </tr>
                                                    <tr>
                                                      <td colspan="2"><input type="checkbox" name="remember" value="1" >
                                                        <span class="white_shadow_11">Remember my Login Information</span></td>
                                                      </tr>
                                                  </table></td>
                                                  <td width="100"><input name="imageField2" type="image" src="/soft_img/app/login_bottom.gif" width="67" height="68" border="0"></td>
                                                </tr>
                                              </form>
                                          </table></td>
                                          <td> <img src="/soft_img/app/member_login_box_06.gif" width="6" height="81" alt=""></td>
                                        </tr>
                                        <tr>
                                          <td> <img src="/soft_img/app/member_login_box_07.gif" width="6" height="6" alt=""></td>
                                          <td> <img src="/soft_img/app/member_login_box_08.gif" width="526" height="6" alt=""></td>
                                          <td> <img src="/soft_img/app/member_login_box_09.gif" width="6" height="6" alt=""></td>
                                        </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td><table id="__01" width="538" height="93" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          <td> <img src="/soft_img/app/member_login_box2_01.gif" width="6" height="6" alt=""></td>
                                          <td> <img src="/soft_img/app/member_login_box2_02.gif" width="526" height="6" alt=""></td>
                                          <td> <img src="/soft_img/app/member_login_box2_03.gif" width="6" height="6" alt=""></td>
                                        </tr>
                                        <tr>
                                          <td> <img src="/soft_img/app/member_login_box2_04.gif" width="6" height="81" alt=""></td>
                                          <td bgcolor="#F1F1F1"><table width="100%" height="80"  border="0" cellpadding="2" cellspacing="0">
                                              <tr>
                                                <td width="50%"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                      <td width="70" align="center"><img src="/soft_img/app/left2.jpg" width="60" height="54"></td>
                                                      <td> If you are new and like to register for Lu Computers, please click&nbsp;<a href="member_register_order.asp" class="orag-blue" title="New Client">here</a>.</td>
                                                    </tr>
                                                </table></td>
                                                <td width="4" bgcolor="#FFFFFF"  ></td>
                                                <td width="50%"><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                                                    <tr>
                                                      <td width="70" align="center"><img src="/soft_img/app/left3.jpg" width="52" height="54"></td>
                                                      <td> If you forgot your user name and pssword, please click <a href="member_find_pw.asp" class="orag-blue">here</a>.</td>
                                                    </tr>
                                                </table></td>
                                              </tr>
                                          </table></td>
                                          <td> <img src="/soft_img/app/member_login_box2_06.gif" width="6" height="81" alt=""></td>
                                        </tr>
                                        <tr>
                                          <td> <img src="/soft_img/app/member_login_box2_07.gif" width="6" height="6" alt=""></td>
                                          <td> <img src="/soft_img/app/member_login_box2_08.gif" width="526" height="6" alt=""></td>
                                          <td> <img src="/soft_img/app/member_login_box2_09.gif" width="6" height="6" alt=""></td>
                                        </tr>
                                    </table></td>
                                  </tr>
                                  <tr>
                                    <td>&nbsp;</td>
                                  </tr>
                                  <tr>
                                    <td class="text_small">Use of this system by unauthorized persons or in an unauthorized manner is strictly prohibited.</td>
                                  </tr>
                                </table></td>
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

        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp');
});
</script>