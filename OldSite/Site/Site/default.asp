
<!--#include virtual="site/inc/inc_page_top.asp"-->
<% response.Redirect("newdefault.asp")

	
	CAll CurrentSystemDefault("site", CurrentIsEbay)
%>

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top"  class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div style="display:none;" class='page_main_nav' id="page_main_nav">Ebay</div>
            	<div id="page_main_area"></div>
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
	$('#page_main_area').load('/site/inc/inc_default.asp?'+rand(1000));
});
</script>