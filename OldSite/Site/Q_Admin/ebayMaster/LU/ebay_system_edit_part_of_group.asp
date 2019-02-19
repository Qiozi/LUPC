<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>edit Ebay System Group Part</title>
    <link rel="stylesheet" type="text/css" href="/js_css/b_lu.css" />

    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
</head>
<body>
<%
	Dim part_group_id		:	part_group_id	=	SQLescape(request("part_group_id"))
	Dim wResult 				: 	wResult = ""
	Dim part_group_id_array
	
	part_group_id_array = split (part_group_id, ",")
	
	Dim runed               :   runed = ""
	
	for i=lbound(part_group_id_array) to ubound(part_group_id_array)
	
	    
	    part_group_id = part_group_id_array(i)
	    
	    if instr(runed, "["& part_group_id &"]") = 0  then 
	        runed = runed & "[" & part_group_id & "]"
	    
	        if(len(trim(part_group_id))>0) then 
        	    
    	        Response.Write "<br/><div style='background:#DAECDA'>"
    	        Response.Write "<table cellpadding='3' cellspacing='0' width='100%'>"
    	        Response.Write "    <tr>"
    	        Response.Write "        <td style='width:200px; height:40px;'>"
	            Response.write "            Part Group ID:&nbsp;"& part_group_id
	            Response.write "            <br><b style='font-size:10pt;'>"& GetGroupComment(part_group_id) &"</b>"
	            Response.Write "            <div id='group_area_"& part_group_id &"'>"
	            Response.Write "            <a onclick=""alert('功能未完成');"">Add To Group</a> <b>|</b> <a style='cursor: pointer;' onclick=""savePriority('"& part_group_id &"');"">Save Priority</a>"
	            Response.Write "            </div> "
	            Response.Write "        </td>"
	            Response.Write "    </tr>"
	            Response.Write "</table>"
	            Response.Write "</div>"
	            Response.write "<hr size=1>"
            	
	            set rs = conn.execute("select p.product_serial_no"&_
							            ", case when length(p.product_ebay_name)>5  then p.product_ebay_name "&_
							            " 		when length(p.product_name_long_en)>5 then p.product_name_long_en "&_
							            " 		else p.product_name end as name "&_
							            " , p.product_current_cost,p.part_ebay_price "&_
							            " , p.product_store_sum, p.ltd_stock, p.product_current_price"&_
							            " , p.part_ebay_cost"&_
							            " , pg.priority"&_
							            " , p.part_ebay_describe"&_
                                        " , p.ebay_system_short_name"&_
							            " , case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end as img_sku"&_
 							            " from tb_part_group_detail pg inner join tb_product p "&_
							            " on p.product_serial_no=pg.product_serial_no"&_
							            " where pg.part_group_id='"& part_group_id &"' and pg.showit=1 and p.tag=1 and p.split_line=0"&_
							            " order by name asc " )
	            if not rs.eof then
		            wResult = "<table cellpadding='3' border='0' cellspacing='0' width='100%' style='line-height:22px;' name='part_list' id='part_list_"& part_group_id &"'>"
		           
		            do while not rs.eof 
			            wResult	= wResult & "<tr name='part_row'>"
			            wResult	= wResult & "<td><img src='http://www.lucomputers.com/pro_img/COMPONENTS/"& rs("img_sku") &"_t.jpg'></td>"
			            wResult	= wResult & "<td>"
			            wResult	= wResult & "   <table width='100%' cellspacing='0' border='0' name='part_list' id='part_list_"& part_group_id &"'><tr>"
			            wResult	= wResult & "	<td style='width:5px;'>"
			            wResult	= wResult & "	<input type='hidden' name='part_group_id' value='"&part_group_id&"'>"&_
								            "   <input type='hidden' name='part_sku' value='"&rs("product_serial_no") &"'>"&_
								            "   <input type='radio' name='part' style='display:none;'>"
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td title='sku' style='width:35px;'>"
			            wResult	= wResult & 		rs(0) 
			            wResult	= wResult & "	</td>"
			            wResult = wResult & "   <td title='priority' style='width: 40px;'>"
			            wResult = wResult & "       <input type='text' style='border:0px;' size='4' name='priority' value='"& rs("priority") &"'> "
			            wResult = wResult & "   </td>"
			            wResult	= wResult & "	<td style='width:30px; text-align:right;' title='lu_quantity'>"
			            wResult	= wResult & 		rs("product_store_sum") 
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td style='width:30px; text-align:right;' title='other_ltd_quantity'>"
			            wResult	= wResult & 		rs("ltd_stock") 
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td style='text-align: right;width:40px;' title='cost'>$"
			            wResult	= wResult & 		rs("product_current_cost") 
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td style='text-align: right;width:40px;' title='price'>$"
			            wResult	= wResult & 		rs("product_current_price") 
			            wResult	= wResult & "	</td>"
			            wResult = wResult & "   <td title='ebay_cost' style='width: 60px;'> "
			            wResult = wResult & "       <input type='text' name='ebay_cost' size='8' style='text-align:right;' value='"& rs("part_ebay_cost") &"'>"
			            wResult = wResult & "   </td>"
			            wResult	= wResult & "	<td title='ebay_price' style='width:50px;'>"
			            wResult	= wResult & "		<input type='text' name='ebay_price' size='8' style='text-align:right;' value='"& rs("part_ebay_price") &"'>"
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td title='part_name' style='width: 550px;'>"
			            'wResult	= wResult & 		rs("name") 
			            wResult = wResult & "   <input type='text' name='part_name' value=""" & replace(rs("name"), """", "&quot;") & """ size='120' onkeydown='if(event.keyCode==13)partSave($(this));'> "
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td title='part_cmd' style='width:40px;'>"
			            wResult	= wResult & "		<a onclick='delPartOfGroupList($(this));' style='color:green; cursor:pointer;' title='remove'>Remove</a>"
			            wResult	= wResult & "	</td>"
			            wResult	= wResult & "	<td title='btn' style='width:80px;'>"
			            wResult	= wResult & "		<input type='button' value='Save' ><span name='result'></span>"
			            wResult	= wResult & "	</td>"
			            wResult = wResult & "</tr>"
			            
			            wResult = wResult & "<tr><td><td>"
			            wResult = wResult & "   <td colspan='11' style='padding-left:1px;'>"
			            wResult = wResult & "       <input type='text' name='ebay_describe' id='ebay_describe_"& rs("product_serial_no") &"' style='width:358px;' value='"& rs("part_ebay_describe") &"'>"
                        wResult = wResult & "      || <input type='text' name='ebay_system_short_name' id='ebay_system_short_name_"& rs("product_serial_no") &"' style='width:200px;' value='"&rs("ebay_system_short_name")&"'> Computer Upgrade (from #2*****) " 
			            wResult = wResult & "   </td>"
			            wResult = wResult & "</tr>"
			            
			            wResult = wResult & "</table></td></tr>"
			            
            			
		            rs.movenext
		            loop
		            wResult	= wResult & "</table>"
	            end if
	            rs.close : set rs =nothing
 	            Response.write wResult
 	        end if
 	    end if
 	Next
%>

<% closeconn() %>

<script type="text/javascript">
	$().ready(function(){	
	
		$('td[title=btn]').each(function(){
			//$(this).css({"text-align":'center','width':'100px'}).find('input').each(function(){$(this).css({ 'display':'none'});});
		});
		
		linitPage();
	});

	//
	//
	//
	function linitPage()
	{

	    $('tr[name=part_row]').each(function(i) {

	        if (i % 2 ==1) {
	            $(this).find('td').css({ "background": "#ffffff", "font-size": "8pt", 'padding-top': '1px', 'padding-bottom': '1px' });

	        } else {
	            $(this).find('td').css({ "background": "#f2f2f2", "font-size": "8pt", 'padding-top': '1px', 'padding-bottom': '1px' });
	        }
	     

	    }).hover(
				function() {
				    /* $(this).find('input').each(function() {
				        if ($(this).attr('value') == 'Save') {
				            $(this).css('display', '');
				        }
				    });
				   
				    $(this).find("td").css({ 'padding-top': '0px', 'padding-bottom': '0px', 'border-top': '1px solid green', 'border-bottom': '1px solid green' });

				    //
				    // part name
				    $(this).find('td').each(function() {
				        if ($(this).attr('title') == 'part_name') {
				            var str = $(this).html() ;
				            $(this).html("<input type='text' name='part_name' value=\"" + str + "\" size='" + 80 + "' onkeydown='if(event.keyCode==13)partSave($(this));'>");

				        }

				        if ($(this).attr('title') == 'part_cmd') {
				            //
				            // remove button
				            $(this).html('<a onclick=\'delPartOfGroupList($(this));\' style=\'color:green; cursor:pointer;\' title=\'remove\'>Remove</a>').css('display', '');
				        }
				    });

				    //
				    // span
				    $(this).find("span").each(function() {
				        if ($(this).attr('name') == "result")
				            $(this).html('');
				    });
				    */


				}
			, function() {
			/*
			    $(this).find('input').each(function() {
			        if ($(this).attr('value') == 'Save')
			            $(this).css('display', 'none');
			    });
			    $(this).find("td").css({ 'padding-top': '1px', 'padding-bottom': '1px', 'border-top': '0px solid green', 'border-bottom': '0px solid green' });

			    //
			    // part name
			    $(this).find('td').each(function() {
			        if ($(this).attr('title') == 'part_name') {
			            var str = "";
			            $(this).find('input').each(function() {
			                str = $(this).val();
			            });
			            $(this).html(str);
			        }


			        if ($(this).attr('title') == 'part_cmd') {
			            //
			            // remove button
			            $(this).find('a').each(function() { $(this).css('display', 'none'); });
			        }

			    });*/
			});
		
			//
			// bind Button 
			$('input[value=Save]').each(function(){$(this).bind('click', function(){ partSave($(this));})});
			$('input[name=ebay_price]').each(function(){$(this).bind('keydown', function(e){
				if(e.keyCode == 13)
					partSave($(this));
				
				})
			});
			
	}
	//
	//
	//
	function partSave(e)
	{
		var ep = e.parent().parent();
		
		var price = "";
		var part_name = "";
		var luc_sku = "";
		var ebay_cost = "";
		var priority = "";
		var ebay_describe = "";
		var ebay_system_short_name = "";

		ep.find('td').each(function(){
			if($(this).attr("title")=="sku")
				luc_sku = $(this).html();
			
		});

		ep.find('input').each(function() {
		    if ($(this).attr('name') == 'ebay_price') {
		        price = $(this).val();
		    }
		    if ($(this).attr('name') == 'part_name') {
		        part_name = $(this).val();
		    }
		    if ($(this).attr('name') == 'ebay_cost') {
		        ebay_cost = $(this).val();
		    }
		    if ($(this).attr('name') == 'priority') {
		        priority = $(this).val();
		    }
           
		});

		ebay_describe = $('#ebay_describe_' + luc_sku).val();
		ebay_system_short_name = $('#ebay_system_short_name_' + luc_sku).val();
		$.ajax({
		    type: "get"
				, url: "ebay_system_cmd_custom.asp"
				, data: {"cmd":"savePartOfGroupList"
				    ,"luc_sku":luc_sku
				    ,"part_name":part_name
				    ,"part_ebay_price":price
				    ,"ebay_cost":ebay_cost
				    ,"priority":priority
				    , "ebay_describe": ebay_describe
                    , "ebay_system_short_name": ebay_system_short_name
				}
				, success: function(msg) {
				    if (msg.indexOf('OK.') != -1) {
				        //e.parent().parent().html('').css('display','none');
				        alert("OK");
				    }
				}
				, error: function(msg) {
				    alert(msg);
				}
		});	
		//alert( +"|"+ part_name);
	}

	//
	//
	//
	function delPartOfGroupList(e) {
	    if (confirm('Remove this part from Group')) {

	        var part_group_id = 0;
	        var part_sku = 0;
	        e.parent().parent().find("input").each(function() {
	            if ($(this).attr('name') == 'part_sku')
	                part_sku = $(this).val();
	            if ($(this).attr('name') == 'part_group_id')
	                part_group_id = $(this).val();
	        });
	        //alert(part_sku+part_group_id);
	        $.ajax({
	            type: "POST"
				, url: "ebay_system_cmd_custom.asp"
				, data: "cmd=delPartOfGroupList&luc_sku=" + part_sku + "&part_group_id=" + part_group_id
				, success: function(msg) {
				    if (msg.indexOf('OK.') != -1)
				        e.parent().parent().html('').css('display', 'none');

				}
	        });
	    }
	}

//
//
//
	function savePriority(part_group_id) {
	    var skus = '';
	    var prioritys = "";
	    $('#part_list_' + part_group_id).find("input").each(function() {
	        if ($(this).attr('name') == 'part_sku') {
	            if (skus == '')
	                skus += $(this).val();
	            else
	                skus += "," + $(this).val();
	        }

	        if ($(this).attr('name') == 'priority') {
	            if (prioritys == '')
	                prioritys += $(this).val();
	            else
	                prioritys += "," + $(this).val();

	        }

	    });

	    $.ajax({ url: "ebay_system_cmd_custom.asp"
	          , type: 'get'
	          , data: 'cmd=savePartOfGroupPriority&skus=' + skus + '&prioritys=' + prioritys + '&part_group_id=' + part_group_id
	          , success: function(msg) {
	              alert('OK');
	              window.location.href = window.document.URL.toString();
	          }
	          , error: function(msg) { alert(msg); }
	    });
    //alert(skus + "|" + prioritys);
}
</script>
</body>
</html>
