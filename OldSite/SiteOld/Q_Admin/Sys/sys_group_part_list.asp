<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="q_admin/funs.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>group part list</title>

    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.tools.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.tabs.slideshow-1.0.2.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.cookie.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.expose.1.0.5.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.core.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.draggable.js" type="text/javascript"></script>
    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
</head>

<body>

<%
	Dim part_group_id 			:		part_group_id 		=	SQLescape(request("part_group_id"))
	Dim part_group_comment		
	Dim CategoryID				:		CategoryID			=	0
	Dim part_count              :       part_count          =   0
	
	
	Set rs = conn.execute("Select case when part_group_comment<> part_group_comment then part_group_comment else part_group_name end as name"&_
						" , product_category "&_
						" from tb_part_group where part_group_id='"& part_group_id &"'")
	if not rs.eof then
		part_group_comment 	= rs(0)
		CategoryID			=	rs("product_category")
	end if
	rs.close : set rs = nothing
%>
	<table width="100%">
    	<tr>
        	<td><h2><%= part_group_comment %></h2></td>
            <td style="text-align:right;">
            	<textarea rows="5" cols="20"></textarea>
            </td>
            <td><input type="button" value="Submit to the Group" onclick='submitSkuToTheGroup("<%= part_group_id %>");'/>
            	<span id='submitSkuToTheGroup_result' style="color:red"></span>
                <br />
                <i>* 输入Part SKU，用“,”号分隔</i>
            </td>
        </tr>
    </table>
    <%  
        set rs = conn.execute("select count(p.product_serial_no) from tb_product p inner join tb_part_group_detail g "&_
						" on p.product_serial_no=g.product_serial_no and g.part_group_id='"& part_group_id&"' and p.tag=1"&_
						" order by p.split_name asc ")
	    if not rs.eof then
	        part_count = rs(0)
	    end if
	    rs.close : set rs = nothing
        set rs = conn.execute("select is_ebay from tb_part_group where part_group_id='"& part_group_id & "'")
        if not rs.eof then
            if rs("is_ebay") = 1 then 
                if part_count>20 then 
                    Response.write "<div style='color:red;'>"
                else
                    Response.write "<div style='color:blue;'>"
                end if
                Response.write "    此为 eBay System Group, 请不要加入超过20个零件, 当前零件数量为 <b>"  & part_count &"</b>"
                Response.write "</div>"
            end if
        end if
        rs.close : set rs = nothing
    
     %>
    <div id="div_warn_note"></div>
    <hr size='1' />
    
<%
	set rs = conn.execute("select distinct split_name from tb_product p inner join tb_part_group_detail g "&_
						" on p.product_serial_no=g.product_serial_no and g.part_group_id='"& part_group_id&"' and p.tag=1"&_
						" order by p.split_name asc ")
	if not rs.eof then
		Do while not rs.eof 
			Response.write "<div class='part_group_name'><span>" & rs(0) & "</span></div>"
			Response.write "<div class='part_group_detail_area' style='display:none' title='" & rs(0) & "' id=''>Waitting...</div>"
		rs.movenext
		loop
	end if
	rs.close : set rs = nothing

%>
<span id='result'></span>

<div class="exposed" id="e1">
</div> 
<script type="text/javascript">
	var api1 = $("#e1").expose({api:true, lazy:true, color: '#78c'}); 
	
	$().ready(function(){
		$('h2').css("font-weight","bold");
		$('div.part_group_name span:first-child').css({'color':'green', 'font-weight':'bold', 'padding-left':'10px', 'cursor':'pointer'})
		.hover(function(){$(this).css('color', '#cccccc');}, function(){$(this).css('color','green');})
		.bind('click', function(){ viewPartDetail($(this).html(), '<%= part_group_id %>') });

		$('div.part_group_detail_area').css({'padding':'15px','text-align':'left'});
		
		//$('#editArea').fadeIn(3000);
		$('#editArea').draggable();
	});
	
	function viewPartDetail(split_name, part_group_id)
	{
		$('div').each(function(){
			if(split_name == $(this).attr('title'))
			{
				if($(this).css('display') == 'none')
				{
					$(this).css('display','');
					$(this).load('/q_admin/sys/sys_cmd.asp'
						, {'part_group_id':part_group_id, 'split_name': split_name, 'cmd':'viewGroupDetail'}
						, function(){afterViewGroupDetail($(this));});
					//$('#result').html('/q_admin/sys/sys_cmd.asp?cmd=viewGroupDetail&part_group_id=<%= part_group_id %>&split_name=' + split_name);
				}
				else
					$(this).css('display','none');
			}
		});
	}
	
	function afterViewGroupDetail(e)
	{
		e.find('tr').each(function(i){
			if(i%2 == 1)
			{
				$(this).find("td").css("background","#ffffff").css("font-size","8pt");
	
			}else
			{
				$(this).find("td").css("background","#f2f2f2").css("font-size","8pt");
			}
			
		});
		
		e.find('span').each(function(){
			if('part__name' == $(this).attr('class'))
			{
				$(this).css({'cursor':'pointer'});//.click(function(){ viewEditOnGroup($(this)); });	
			}
		});
		
		//
		// click Event
		e.find('span').each(function(){ $(this).click(function(e){
			$('#editArea').css({'top': $(this).offset().top, 'left':'200px', 'display':''});
			$('#expose_sku').html("SKU: "+ $(this).attr('id'));
			
			$('#edit_part_group_area').load('/q_admin/sys/sys_cmd.asp'
						, {'luc_sku':$(this).attr('id'), 'CategoryID': '<%= CategoryID %>', 'cmd':'viewPartOfGroupDetail'}
						, function(){api1.load();});
			
			
			
		})});
	}
	
	function SavePartOfGroup()
	{
		$('#save_result').html("Saving...");
		var groupIDS = "";
		var showit = 1;
		var sku = $('#expose_sku').html().replace("SKU: ","");
		$('input[name=chk_part_group]').each(function(){
			if($(this).attr('checked'))
			{
				if(groupIDS=="")
					groupIDS = $(this).attr('value');
				else
					groupIDS += ',' + $(this).attr("value");
			}
		});
		
		if(!$('#part_showit').attr('checked'))
			showit=0;


$('#save_result').load('/q_admin/sys/sys_cmd.asp'
						, { 'luc_sku': sku, 'part_showit': showit, 'cmd': 'savePartInfo', 'groupIDS': groupIDS }
						, function() { window.document.reload(); api1.load(); });
			
	}
	
	function submitSkuToTheGroup(part_group_id)
	{
		$('#submitSkuToTheGroup_result').html("Saving...");
		var skus = $('textarea').val().replace('，', ',');//.replace(/\n/,",");
		
		//alert(skus);
		$('#submitSkuToTheGroup_result').load('/q_admin/sys/sys_cmd.asp'
						, {'luc_skus':skus, 'part_group_id': part_group_id, 'cmd':'savePartsToGroup' }
						, function(){});
	}
</script>
<div id='editArea' style="display:none; width: 400px; min-height: 400px; height:auto; background:#ffffff;position:absolute;z-index:10000;border:1px solid #ccc;"> 
    <b id='expose_sku'></b>
    <br />    
    <button type="button" onClick="api1.close(); $('#editArea').css('display','none');">Close 1</button> 
    <hr size="1" />
    <div id='edit_part_group_area'></div>
    <hr size='1' />
	<input type='button' value='Save' onclick="SavePartOfGroup();" /><span style='color:red;' id='save_result'></span>
</div>
<% 

closeconn() %>
</body>
</html>
