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
                    <span class="nav1">Customer Service</span>
                </div>
            	<div id="page_main_area">
                
					<table width="600" height="670"  border="0" align="center" cellpadding="1" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                      <tr>
                        <td valign="top" style="border:#E3E3E3 1px solid; "><table width="97%"  border="0" align="center" cellpadding="2" cellspacing="0">
                          <tr>
                            <td width="36" class="text_blue_13" ><img src="/soft_img/app/question.gif" width="30" height="31"></td>
                            <td class="text_blue_13" ><table width="100%"  border="0" cellpadding="2" cellspacing="0" bgcolor="#CDE3F2">
                                <tr>
                                  <td style="padding-left:10px; font-weight: bold;" class="text_blue_11">Currently under rebuild!</td>
                                </tr>
                            </table></td>
                          </tr>
                          <tr>
                            <td height="25" valign="top" class="text_small" >&nbsp;</td>
                            <td valign="top" class="text_hui_11" style="padding-bottom:10px; padding-left:10px;"> Our support center is under-going some major changes and will re-open soon to better serve you</td>
                          </tr>
                          <tr>
                            <td height="25" valign="top" class="text_small" >&nbsp;</td>
                            <td valign="top" class="text_small" style="padding-bottom:10px; padding-left:10px;">&nbsp;</td>
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