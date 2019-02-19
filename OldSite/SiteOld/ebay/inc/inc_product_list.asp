<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="ebay/inc/inc_product_list_layout.asp"-->
<%
	Dim PS			:		PS			=	5
	
	' 	fliter
	call GetEbayKeywordArea(SQLescape(Request("id")) , SQLescape(request("category_query_keys")))
	
	'	sort by 
	call WriteSortBy(SQLescape(Request("sortby")) , SQLescape(request("id")))
	
	

	
	Response.Write("<div class='ebay_system_list_area' style='border:0px solid red;'>"&vblf)
	
	'	syste list
	call GetEbaySystemList(SQLescape(Request("id")), SQLescape(request("page")) , SQLescape(request("category_query_keys")), SQLescape(Request("sortby")), PS, SQLescape(request("number")))
	
	
	Response.write "</div>"&vblf 

%>

<script type="text/javascript">
    $('td[title=system_price_line]').css("padding", "10px").css("text-align", "right").css("border-top", "1px dotted #ccc");
    $('td[title=system_price_line]>img').css("margin", "6px").css("cursor", "pointer");
    $('td[title=system_parts]').css("padding", "5px");
	$('div[title=list_floor_area]').css("height", "60px");
	$('table[title=page]').css("float","right").css("margin", "20px 10px 10px 10px").find('td').css("background", "#ffffff");
	$('#main_list_head_area').html($('div[title=list_floor_area]').html());
	$('#main_list_head_area>table').css("margin", "0px");
	$('div.sort_by_area >ul').css("width", "100%");
	$('div.sort_by_area').find('li').css("float", "left").css("border", "0px solid red").css("line-height", "20px");
</script>