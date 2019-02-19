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
                    <span class="nav1">Privacy Security</span>
                </div>
            	<div id="page_main_area">                
					<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                        <tr>
                          <td valign="top" style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellspacing="0" cellpadding="0">
                            <tr bgcolor="#CDE3F2">
                              <td height="22" colspan="3" class="text_blue_11"><strong > &nbsp;&nbsp;Welcome to LU Computer's Help Desk </strong></td>
                            </tr>
                            <tr>
                              <td colspan="3" style="padding:8px; "><span class="text_hui_11">For your convenience, we have created a centralized location for you to easily find the answer to your questions! Please refer to the links below to look up questions and answers about specific topics.
                                </span>                    <p class="text_hui_11">If you are not able to find answer to your question, or prefer to talk to someone directly, please feel free to contact us! We will be glad to help. If it helps other customers, we will post it in the Help Desk for future customers.</p></td>
                            </tr>
                            <tr>
                              <td width="49%" height="22" bgcolor="#CDE3F2">&nbsp;&nbsp;<span class="text_blue_11"><strong>Company</strong></span><span class="text_white_12"><strong><br>
                              </strong></span></td>
                            <td>&nbsp;</td>
                              <td width="49%" height="22" bgcolor="#CDE3F2"> &nbsp;&nbsp;<span class="text_blue_11"><strong>Frequently Asked Questions</strong></span> </td>
                            </tr>
                            <tr>
                              <td valign="top" style="padding:8px; "> <a href="<%= LAYOUT_HOST_URL %>about_us.asp" class="hui-orange-s">About Us</a><br>
                                <a href="Contact_us.asp" class="hui-orange-s">Contact Us</a></td>
                            <td>&nbsp;</td>
                              <td style="padding:8px; "><a href="<%= LAYOUT_HOST_URL %>General_faq.asp" class="hui-orange-s">General FAQ</a><br>
                                <a href="<%= LAYOUT_HOST_URL %>Shipping.asp" class="hui-orange-s">Shipping FAQ</a><br>
                                <a href="<%= LAYOUT_HOST_URL %>Warranty.asp" class="hui-orange-s">Warranty FAQ</a></td>
                            </tr>
                            <tr>
                              <td width="49%" height="22" bgcolor="#CDE3F2">&nbsp;&nbsp;<span class="text_blue_11"><strong>Support</strong></span><span class="text_white_12"><strong><br>
                              </strong></span></td>
                              <td>&nbsp;</td>
                              <td width="49%" height="22" bgcolor="#CDE3F2">&nbsp;&nbsp;<span class="text_blue_11"><strong>Terms &amp; Policies </strong></span></td>
                            </tr>
                            <tr>
                              <td  style="padding:8px; "> <p> <a href="customer_service.asp" class="hui-orange-s">Support Center</a><br>
                                  <a href="<%= LAYOUT_HOST_URL %>tech_support.asp" class="hui-orange-s">Support FAQ</a><br>
                                  <a href="<%= LAYOUT_HOST_URL %>Manufacturers.asp" class="hui-orange-s">Manufacturers</a><br>
                                  <a href="<%= LAYOUT_HOST_URL %>driver_links.asp" class="hui-orange-s">Support Links</a><br>
                                  <a href="<%= LAYOUT_HOST_URL %>contact.asp" class="hui-orange-s">Contacts</a><br>
                                </p>                    </td>
                            <td>&nbsp;</td>
                              <td valign="top"  style="padding:8px; "> <p> <a href="Company_Policy.asp" class="hui-orange-s">Company Policy</a><br>
                                  <a href="<%= LAYOUT_HOST_URL %>payment.asp" class="hui-orange-s">Payment Methods</a><br>
                                  <a href="<%= LAYOUT_HOST_URL %>privacy_security.asp" class="hui-orange-s">Privacy Security</a><br>
                                </p>                    </td>
                            </tr>
                            <tr bgcolor="#CC9933">
                              <td height="22" colspan="3" bgcolor="#CDE3F2">&nbsp;&nbsp; <span class="text_blue_11"><strong>Feedback </strong></span></td>
                            </tr>
                            <tr>
                              <td colspan="3" class="text_hui_11"  style="padding:8px; ">We would love to hear from you. Please let us know how we are doing. This will allow us to create an even better shopping experience for you. feedback@lucomputers.com </td>
                            </tr>
                          </table></td>
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