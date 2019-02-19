<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<!--#include virtual="/q_admin/funs.asp"-->
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Untitled Document</title>
    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.tools.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.tabs.slideshow-1.0.2.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.cookie.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.expose.1.0.5.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.core.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.draggable.js" type="text/javascript"></script>
    <link href="/js_css/b_ebay.css" rel="stylesheet" type="text/css" />

</head>
<style>
/*#demotip { 
    display:none; 
    background:transparent url(/soft_img/back/black_arrow.png); 
    font-size:12px; 
    height:90px; 
    width:190px; 
    padding:10px; 
    color:#fff;     
}*/

</style>
<body>

<%
    Dim part_group_id       :   part_group_id   =   SQLescape(request("part_group_id"))
    Dim CategoryID          :   CategoryID      =   SQLescape(request("category_id"))
    
    Dim whereSQL            :   whereSQL = ""
    
    if(isnull(CategoryID) or isempty(CategoryID) or CategoryID="") then
        CategoryID = 0
    end if
    
    if(CategoryID<>0)then
        whereSQL = " and menu_child_serial_no='"+ CategoryID +"'"
    end if
    
    set rs = conn.execute("select menu_child_name,menu_child_serial_no from tb_product_category where page_category=1 and menu_pre_serial_no=0 and tag=1 and is_virtual=0")
    if not rs.eof then 
        do while not rs.eof 
            Response.write "<h5>"& rs("menu_child_name") &"</h5>"
            
            set crs = conn.execute("select menu_child_name,menu_child_serial_no from tb_product_category where page_category=1 "& whereSQL &" and menu_pre_serial_no='"& rs("menu_child_serial_no") &"' and tag=1 and is_virtual=0 order by menu_child_order asc")
            if not crs.eof then
                do while not crs.eof 
                    Response.write "<div class='categroy_child'>"& crs("menu_child_name") 
					Response.write " <a href='' style='color:#aaa;' onclick=""newPartGroup('"& crs("menu_child_serial_no") &"');return false;"" title='create new part group'>(New)</a>"
					Response.write "</div>"
					
						set srs = conn.execute("Select part_group_id, product_category, part_group_name, part_group_comment,is_ebay,showit"&_
										" from tb_part_group where product_category='"& crs("menu_child_serial_no") &"'  order by part_group_id asc ")
						if not srs.eof then
							response.write "<table class='part_group_area' border='0'>"
							response.write "<tr>"		
							response.write "	<td >"
							do while not srs.eof 
							      
                                    if srs("showit") = 1 then 
                                        hideText = "Hide"
                                    else
                                        hideText = "Show"
                                    end if

							        response.Write  "<span id='a_"& srs("part_group_id") &"'>"
							       
									response.write  "<div class='part_group_name'"&vblf
									Response.write  " id='"& srs("part_group_id") &"' "&_
									 				" name='"&srs("part_group_name")&"' "&_
													" showit='"&srs("showit")&"' "&_
													" comment='"& srs("part_group_comment") &"' "&_
													" is_ebay='"& srs("is_ebay") &"'>"&_
													" <span name='duplicate' onclick=""duplicateGroup('"&srs("part_group_id")&"');"">Duplicate</span>"&_
													" | <span name='edit' onclick=""editGroup('"&srs("part_group_id")&"', $(this));"">Edit</span>"&_
													" | <span name='hide' onclick=""hideGroup('"&srs("part_group_id")&"', $(this), "& srs("part_group_id") &");"">"& hideText &"</span>"&_
													" <span> "& srs("part_group_comment")
									Response.write 	" </span><span style='color:red;'></span>"
									Response.Write  "<span style='color:green;'>"& writeGroupOfeBaySysSKU(srs("part_group_id")) &"</span>"
									Response.write  " </div>"	
									response.write  "<span class='e' id='"& srs("part_group_id") &"'> "& srs("is_ebay") & "</span>"	
									Response.Write  "</span>"
							srs.movenext
							loop
							response.write "	</td>"
							response.write "</tr>"
							response.Write "</table>"
						end if
						srs.close : set srs = nothing 
                crs.movenext
                loop
            end if
            crs.close : set crs = nothing
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing

 %>
<% closeconn() %>

<div id="demotip" style="padding:0.5em;display:none; width: 400px; min-height: 400px; height:auto; background:#ffffff;position:absolute;z-index:10000;border:1px solid #ccc;">
    <input type="hidden" id='current_part_group_id' />
    <button type="button" onClick="api1.close(); $('#demotip').css('display','none');">Close 1</button> 
    <hr size="1" />
	<table cellpadding="2" cellspacing="0" border="0">
    	<tr>
        	<td>Name:</td>
            <td colspan="2"><input type="text" id="group_name" size="35" /><input type="hidden" id="part_group_id"></td>
        </tr>
        <tr>
        	<td>Comm:</td>
            <td colspan="2"><input type="text" id="group_comment"  size="35"/></td>
        </tr>
        <tr>
            <td><input type="checkbox" id="showit" value="1"/>Show</td>
            <td><input type="checkbox" id="is_ebay" value="1"/>is ebay</td>
            <td><input type="button" id="btn" value="Save" size='5' onclick="submitSave();" /><span id="result"></span></td>
        </tr>
    </table>

</div> 

<div class="exposed" id="e1">
</div> 
<script type="text/javascript">

    var api1 = $("#e1").expose({ api: true, lazy: true, color: '#78c' }); 
    
    $().ready(function() {
        $('div.categroy_child').css("margin-left", "2em").css({ "font-weight": "bold" });
        $('table.part_group_area').css({ "margin-left": "4em", "width": "95%" }).find('td').css({ "border": "1px solid #ccc" });
        $('div.part_group_name').css({ "margin": "5px", "border": "1px solid #ff9900", "padding": "5px" });
        $('span[name=duplicate]').css({ 'color': 'blue', 'cursor':'pointer' });
        $('span[name=hide]').css({ 'color': 'blue', 'cursor': 'pointer' });
        $('span[name=edit]').css({ 'color': 'blue', 'cursor': 'pointer' });
        bindHoverEvent();

        changeSpanBgColor();

        $('#demotip').draggable();
    });
	$(function(){

});


function changeSpanBgColor() {
    $('span.e').each(function() {
        if ($(this).html() == 1) {
            var p_g_id = $(this).attr("id");
            $('div[id=' + p_g_id + ']').css("background", "#f2f2f2");
            //$(this).html(p_g_id);
            //alert(p_g_id);
        }
        $(this).css({ "display": "none" });
    });
}

function bindHoverEvent() {

//    $("div.part_group_name").tooltip('#demotip').hover(function() {
//        $('#group_name').val($(this).attr("name"));
//        $('#part_group_id').val($(this).attr("id"));
//        $('#group_comment').val($(this).attr("comment"));
//        $('#showit').attr('checked', $(this).attr("showit") == "1");
//        $('#is_ebay').attr('checked', $(this).attr("is_ebay") == "1");
//        $('#Save').val("Save");
//        $('#current_part_group_id').val('a_' + $(this).attr("id"));
//    }
//		 , function() {

//		 });

}

function editGroup(gid,e) {
  		//
		// click Event
    
    $('#demotip').css({ 'top': e.offset().top, 'left': '200px', 'display': '' });
    api1.load();
    
    // set value
    $('#group_name').val(e.parent().attr("name"));
    $('#part_group_id').val(e.parent().attr("id"));
    $('#group_comment').val(e.parent().attr("comment"));
    $('#showit').attr('checked', e.parent().attr("showit") == "1");
    $('#is_ebay').attr('checked', e.parent().attr("is_ebay") == "1");
    $('#Save').val("Save");
    $('#current_part_group_id').val('a_' + e.parent().attr("id"));

}
	function submitSave()
	{
	    $("#result").html("...");
	    var a_id = $('#current_part_group_id').val();

	    $.ajax({
	        url: '/q_admin/ebayMaster/lu/ebay_system_cmd_custom.asp',
	        type: 'GET',
	        data: { "cmd": "editGroupComment"
							, "part_group_id": $("#part_group_id").val()
							, "group_name": $('#group_name').val().replace(/&/gi,'[and]')
							, "group_comment": $('#group_comment').val().replace(/&/gi, '[and]')
							, "showit": ($('#showit').attr("checked") ? "1" : "0")
							, "is_ebay": ($('#is_ebay').attr("checked") ? "1" : "0")
	        },
	        timeout: 1000,
	        error: function() {
	            alert('Error loading document');
	        },
	        success: function(xml) {
	            // do something with xml

	            if (xml.indexOf('<\/script>') != -1) {
	                //alert(xml.split('<\/script>')[1]);
	                //alert($('#' + a_id).html());
	                $('#' + a_id).html(xml.split('<\/script>')[1]);
	                $("#result").html("end");
	                // alert($('#' + a_id).html());
	                $('div.part_group_name').css({ "margin": "5px", "border": "1px solid #ff9900", "padding": "5px" });
	                bindHoverEvent();
	                changeSpanBgColor(); 
	            }
	            else
	                alert('Warning..');
	        }


	    });
//	    $("#" + a_id).load("/q_admin/ebayMaster/lu/ebay_system_cmd_custom.asp"
//						, { "cmd": "editGroupComment"
//							,"part_group_id":$("#part_group_id").val()
//							,"group_name": $('#group_name').val()
//							,"group_comment": $('#group_comment').val()
//							,"showit":($('#showit').attr("checked") ? "1": "0")
//							,"is_ebay":($('#is_ebay').attr("checked") ? "1": "0")}
//						, function() { $('#result').html('end'); }
//						);
	   
	}
	function newPartGroup(categoryID)
	{
		$("#result").load("/q_admin/ebayMaster/lu/ebay_system_cmd_custom.asp"
						, { "cmd": "newPartGroup"
							,"categoryID":categoryID
							,"group_name": 'New Part Group'
							,"group_comment": 'New Part Group'
							,"showit":1
							,"is_ebay":0}
						, function(){window.location.reload();}
						);
}

function hideGroup(gid, the, groupID) {

    var cmd = "ShowPartGroup";
    if (the.parent().attr("showit") == "1")
        cmd = "HidePartGroup";

    $.ajax({
        url: '/q_admin/ebayMaster/lu/ebay_system_cmd_custom.asp',
        type: 'GET',
        data: { "cmd": cmd
							, "part_group_id": groupID
        },
        timeout: 1000,
        error: function (msg) {
            alert(msg);
        },
        success: function (xml) {
            // do something with xml

            if (xml.indexOf('OK') != -1) {
                alert("OK");
                the.parent().attr("showit") = "0";
                the.html("Show");
            }
            else
                alert('Warning..');
        }


    });

}

function duplicateGroup(groupID) {
    var ds = prompt('Please input New Group Comment:', '');
    if (ds != null) {
       // alert(ds);
        $.ajax({
            url: '/q_admin/ebayMaster/lu/ebay_system_cmd_custom.asp',
            type: 'GET',
            data: { "cmd": "duplicateGroup"
							, "part_group_id": groupID
							, "group_comment": ds.replace(/&/gi, '[and]')
            },
            timeout: 1000,
            error: function() {
                alert('Error loading document');
            },
            success: function(xml) {
                // do something with xml

                if (xml.indexOf('<\/span>') != -1) {
                    window.location.reload();
                }
                else
                    alert('Warning..');
            }


        });
    }
}
</script>
</body>
</html>
