<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ebay system Comment</title>
    <link href="/js_css/b_ebay.css" rel="stylesheet" type="text/css" />
    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
</head>

<body>
<%
	Dim system_sku 			: system_sku		=	SQLescape(request("system_sku"))
	
	Response.write "<h3>"& system_sku &"</h3><hr size=1>"&_
					"	<input type='button' value='Save' onclick='saveGroupComment();'>"&_
					"   <span id='result'></span>"&_
					"   <hr size=1>"
	
	Set rs = conn.execute("select es.comment, es.id, ifnull(ep.luc_sku, '') luc_sku, product_ebay_name, es.id comment_id "&_
							" from tb_ebay_system_part_comment es left join "&_
							" (select distinct comment_id, luc_sku,p.product_ebay_name from tb_ebay_system_parts e1 inner join tb_product p on p.product_serial_no=luc_sku  where system_sku='"& system_sku &"') ep on ep.comment_id=es.id "&_
							" where  es.showit=1 order by priority asc ")
							
	If not rs.eof then
		Response.write "<table cellpadding='2' cellspacing='0' width='99%'>"
		do while not rs.eof 
				Response.write "<tr>"
				Response.write "	<td><input type='checkbox' name='checkComment' sku='"& rs("luc_sku") &"' id='"& rs("comment_id") &"'></td>"
				Response.write "	<td><b>"& rs("comment") & "</b></td>"
				Response.write "	<td><span class='sku'>"& rs("luc_sku") & "</span></td>"
				Response.write "	<td>"& rs("product_ebay_name") &"</td>"
				Response.write "</tr>"
		rs.movenext
		loop
		Response.write "</table>"
	end if 
	rs.close : set rs = nothing
 
%>

<% closeconn %>
<script type="text/javascript">
$().ready(function(){
	$('span.sku').each(function(){
		if($(this).html() !="")
		{
			$('input[name=checkComment][sku='+ $(this).html() +']').attr('checked',true);
		}
	});
	$('td').css({'border-bottom':'1px solid #ccc'});
	$('tr').hover(function(){
			$(this).css('background', "#f2f2f2");
		}
		,function(){
			$(this).css('background', "#ffffff");
		}
	);
});

function saveGroupComment()
{
	$('#result').html("...");
	var comment_ids = "0";
	$('input[name=checkComment]').each(function(){
		var comment_id = $(this).attr("id");
		if(comment_id.length >0 && $(this).attr('checked'))
		{
			comment_ids += ","+ comment_id;
		
		}

	});

	$('#result').load('/Q_ADMIN/ebayMaster/lu/ebay_system_cmd_custom.asp'
					,{ "cmd":"saveGroupComment"
						,"comment_ids":comment_ids
						,"system_sku": '<%= system_sku %>'
					 }
					 , function () { }
					 );
}
</script>
</body>
</html>
