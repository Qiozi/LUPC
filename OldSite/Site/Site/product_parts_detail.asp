<!--#include virtual="site/inc/inc_page_top.asp"-->
<%if (isempty(request("id"))) then response.end
    
    response.Redirect("https://www.lucomputers.com/detail_part.aspx?sku=" & request("id"))
    %>
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" class='page_frame_middle'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div Class="page_main_nav" id="page_main_nav"><%= FindPartNav(SQLescape(Request("cid")) ) %></div>
            	<div id="page_main_area">
                    
                </div>
            <!-- main end 	-->
        </td>      
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>
<%
call setViewCount(true, LAYOUT_HOST_IP, SQLescape(Request("id")))
CloseConn()

%>
<!--#include virtual="site/inc/inc_bottom.asp"-->
<iframe src="blank.html" style="width:0px; height:0px;" frameborder="0" id="iframe1" name="iframe1"></iframe>

<script type="text/javascript">
$().ready(function(){

	$('#page_main_area').load('/site/inc/inc_view_part_detail.asp?class=<%= request("class") %>&id=<%= request("id") %>&cid=<%= request("cid") %>&'+rand(1000));
});
</script>