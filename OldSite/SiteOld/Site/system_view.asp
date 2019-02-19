<!--#include virtual="site/inc/inc_page_top.asp"-->
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px"  class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
        <td id="page_main_center" valign="top" class="page_frame_middle">
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav" id="page_main_nav"><%= FindNav( SQLescape(Request("cid")) ,1, "sys_product") %></div>
            	<div id="page_main_area"></div>
            <!-- main end 	-->
        </td>

        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<iframe src="blank.html" style="width:0px; height:0px;" frameborder="0" id="iframe1" name="iframe1"></iframe>
<script type="text/javascript">
    $().ready(function(){
        $('#page_main_area').css("border","1px solid #8FC2E2").css("background", "#ffffff").css("padding", "1px");
        $('#page_main_area').load("/site/inc/inc_system_view.asp?class=<%= request("class")%>&id=<%= Request("id") %>&cid=<%= request("cid") %>");
    });
</script>