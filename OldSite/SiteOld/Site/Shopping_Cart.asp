<%
    response.redirect("http://ca.lucomputers.com/ShoppingCart.aspx") 
%>
<!--#include virtual="site/inc/inc_page_top.asp"-->
<%  response.Cookies("CurrentOrder") = "site" %>
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" style="text-align:center"  class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class="page_main_nav"><span class='nav1'><a href="/site/default.asp">Home</a></span><span class='nav1'>My Shopping Cart </span> </div>
            	<div id="page_main_area"></div>
            <!-- main end 	-->
        </td>
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">

$().ready(function(){
	$('#page_main_area').load('/site/inc/inc_shopping_cart.asp?r='+rand(1000));
});
</script>