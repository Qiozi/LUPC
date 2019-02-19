<!--#include virtual="site/inc/inc_page_top_no_doctype.asp"-->
<table class="page_main" cellpadding="0" cellspacing="0" id="page_main_center_area" style="position:relative;" align="center">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
        <td id="page_main_center" valign="top" class='page_frame'>
        	<!-- main begin -->
            	<table border="0"  cellpadding="0" cellspacing="0" style="display:none;">
                	<tr>
                    	<td><div id="page_main_banner"></div></td>
                        <td style='padding:0px 1px 5px 5px;'>
</td>
                    </tr>
                </table>
        	    <div class="page_main_nav">
                	<span class='nav1'><a href="/site/default.asp">Home</a></span>
                    <span class='nav1'><a href="<%= LAYOUT_HOST_URL %>system_view.asp?cid=<%= request("cid") %>&class=<%= request("class") %>&id=<%= request("id") %>">Product Detail</a></span>
                	<span class='nav1'>Customize</span>

                </div>
            	<div id="page_main_area">
                	<div style='line-height: 100; font-size:11pt; text-align:center; vertical-align:top'>Loading...</div>
                </div>
            <!-- main end 	-->
        </td>

        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>
<span id="sys_customize_float"></span>
<!--#include virtual="site/inc/inc_bottom.asp"-->
<!--/*<script type="text/javascript" src="../js/float.js"></script>*/-->

<script type="text/javascript">
    $().ready(function(){
        $('#page_main_area').css("border","1px solid #8FC2E2").css("background", "#ffffff").css("padding", "1px");
        $('#page_main_area').load("/site/inc/inc_computer_system_2.asp?system_code=<%= request("system_code") %>&id=<%= Request("id") %>&cid=<%= request("cid") %>&="+ rand(1000));
    });
	
	//$('#page_main_area').css(
</script>