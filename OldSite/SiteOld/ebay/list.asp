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
        	    <div class="page_main_nav" id="page_main_nav">
                	<span class="nav1"><a href="/ebay/">Home</a></span>
                	<span class="nav1">Ebay</span>
                </div>
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
<%
	if SQLescape(trim(request("page"))) = "" then 
		page=1
	else
		page = SQLescape(request("page"))
	end if
	if SQLescape(trim(request("sortby"))) = "" then 
		sortby=2
	else
		sortby= SQLescape(request("sortby"))
	end if

	'
	'	query ebay number.
	'
	Dim ebay_number 	:		ebay_number 	=	SQLescape(request("number"))
	if ebay_number <> "" then
	
	
	end if
%>

<script type="text/javascript">
    $().ready(function(){
        $('#page_main_area').css("border","1px solid #8FC2E2").css("background", "#ffffff").css("padding", "1px");	
		
		//$	('#page_main_area').html("/ebay/inc/inc_product_list.asp?page=<%= page %>&sortby=<%= sortby %>&id=<%= Request("id") %>&category_query_keys=<%= URLescape(request("category_query_keys"))%>");
		<% if ebay_number="" then %>	
        	$('#page_main_area').load("/ebay/inc/inc_product_list.asp?page=<%= page %>&sortby=<%= sortby %>&id=<%= Request("id") %>&category_query_keys=<%= URLescape(request("category_query_keys"))%>");
		<% else %>
			$('#page_main_area').load("/ebay/inc/inc_product_list.asp?page=<%= page %>&sortby=<%= sortby %>&number=<%= ebay_number %>&category_query_keys=<%= URLescape(request("category_query_keys"))%>");
		<% end if %>
    });
</script>