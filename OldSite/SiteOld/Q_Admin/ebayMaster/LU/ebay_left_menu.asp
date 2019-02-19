<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="q_admin/ebayMaster/ebay_inc.asp"-->
<!--#include virtual="q_admin/funs.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ebay left menu</title>

    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.tools.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.tabs.slideshow-1.0.2.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.treeview.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.cookie.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/treeview.demo.js" type="text/javascript"></script>
<script type="text/javascript">
//			rand 		


rnd.today = new Date();
rnd.seed = rnd.today.getTime();

function rnd(){
	rnd.seed = (rnd.seed * 9301+49297)%233280;
	return rnd.seed/(233280.0);
}
//
//
// rand(1000)
function rand(number){ return Math.ceil(rnd()* number)};
    
	</script>

    <link href="/js_css/tabs.css" rel="stylesheet" type="text/css" />
    <link href="/js_css/treeview.css" rel="stylesheet" type="text/css" />
    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
    <style>
	html, body {height:100%; margin: 0; padding: 0; }
	
	html>body {
	    font-size: 16px;
	    font-size: 68.75%;
	} /* Reset Base Font Size */
	
	body {
	    font-family: Verdana, helvetica, arial, sans-serif;
	    font-size: 68.75%;
	    background: #fff;
	    color: #333;
	}

	
	span.demo1 {
  background-color: yellow;
  margin-right: 20px;
  padding: 5px;
}


</style>
</head>
<body>
<a href="ebay_left_menu.asp?pageType=1" style="border:1px solid #ccc;padding: 2px;color:Blue;" >reload</a>
<input type="checkbox" id='showall' value="1" onclick="showAll($(this));" />Show ALL
<div class="panes">
    <%
        'response.Write (request("pageType"))
        dim showALL
        showALL = Request("showall")
        if(cstr(request("pageType")) = "1")then Response.write GetEbayCategoryLeftMenu()        
        if(request("pageType") = "2") then Response.write GetGroupLeftMenu("editGroupDetail")
        if(request("pageType") = "3") then Response.write GetGroupLeftMenu("")
        
     %>

   
</div>

<%
   
   

closeconn() %>

<script type="text/javascript">

    $().ready(function () {
        $('li.group_menu_name span:first-child').css({ 'cursor': 'pointer' })
		.hover(function () { $(this).css("color", "red"); }, function () { $(this).css("color", "black"); })
		.bind("click", function () { viewGroupName($(this)); });


        $('.file a').bind("click", function () {
            $('.file a').each(function () {
                $(this).css({ "background": "white", "color": "#666666" });
            });
            $(this).css({ "background": "black", "color": "white" });
        });

        if ('<%=showALL %>' == "1") {
            $('#showall').attr('checked', true);
        }
    });

    function viewGroupName(e) {
        //alert(e.parent());
        if (e.parent().find('ul').css('display') == 'none')
            e.parent().find('ul').css('display', '');
        else
            e.parent().find('ul').css('display', 'none');
    }

    function showAll(the) {
        if(the.attr('checked'))
            window.location.href = "ebay_left_menu.asp?pageType=1&showall=1"
        else
            window.location.href = "ebay_left_menu.asp?pageType=1&showall=0"
    }
</script>
<%
    if request("cid")<>"" then
        Response.Write "<script>$('#treeview_category_id_"& request("cid") &" ~ ul').css('display','');</script>" 
        
    end if
%>
</body>
</html>
