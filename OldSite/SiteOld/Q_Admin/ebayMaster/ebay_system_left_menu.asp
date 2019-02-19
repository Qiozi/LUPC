<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ebay left menu</title>
    <script src="../JS/lib/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../js/lib/jquery.cookie.js" type="text/javascript"></script>
	<script src="../js/lib/jquery.treeview.js" type="text/javascript"></script>
	<script src="../js/lib/demo.js" type="text/javascript"></script>
    <script src="../js/lib/jquery.contextmenu.r2.js" type="text/javascript"></script>
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
    <link rel="Stylesheet" type="text/css" href="../../js_css/jquery.css" />
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
<h4>Ebay System</h4>
<%
   
    Response.write GetEbayCategoryLeftMenu()

closeconn() %>
 <div class="contextMenu" id="create_New_system" style="display:none;">
      <ul>
        <li id="TreeViewCmdNew"><img src="../../soft_img/tags/(02,40).png" /> New</li>
        <li id="TreeViewCmdEditText"><img src="../../soft_img/tags/(02,40).png" /> Edit Text</li>
        <li id="TreeViewCmdShowALL"><img src="../../soft_img/tags/(02,40).png" />Show All</li>
        <li id="TreeViewCmdHiddenALL"><img src="../../soft_img/tags/(02,40).png" />Hidden All</li>
        
      </ul>
 </div>
<script type="text/javascript">
$().ready(function(){
    $('span.folder').contextMenu('myMenu1', {
          bindings: {
            'open': function(t) {
              alert('Trigger was '+t.id+'\nAction was Open');
            },
            'email': function(t) {
              alert('Trigger was '+t.id+'\nAction was Email');
            },
            'save': function(t) {
              alert('Trigger was '+t.id+'\nAction was Save');
            },
            'delete': function(t) {
              alert('Trigger was '+t.id+'\nAction was Delete');
            }   
            }
        });
        
         $('span.folder').contextMenu('create_New_system', {
          bindings: {
            'TreeViewCmdNew': function(t) {
              var category_id = t.id.replace("treeview_category_id_", "");
              parent.$('#ifr_main_frame1').attr("src","ebay_system_edit_2.asp?category_id="+ category_id +"&cmd=create");
            },
            'TreeViewCmdEditText': function(t) {
               var category_id = t.id.replace("treeview_category_id_", "");
               parent.$('#ifr_main_frame1').attr("src","ebay_system_edit_2.asp?category_id="+ category_id +"&cmd=create");
            },
            'TreeViewCmdShowALL': function(t) {
              var category_id = t.id.replace("treeview_category_id_", "");
              parent.$('#ifr_main_frame1').attr("src","ebay_system_cmd.asp?category_id="+ category_id +"&cmd=ShowALL");
            },
            'TreeViewCmdHiddenALL': function(t) {
              var category_id = t.id.replace("treeview_category_id_", "");
              parent.$('#ifr_main_frame1').attr("src","ebay_system_cmd.asp?category_id="+ category_id +"&cmd=HiddenALL");
            }
            }
        });
 });
</script>
</body>
</html>
