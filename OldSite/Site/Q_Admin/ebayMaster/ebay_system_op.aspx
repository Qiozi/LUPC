<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ebay_system_op.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_system_op" Title="Ebay System" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    a.top_btn { float: right; display: block; padding: 2px; font-size: 12px}

</style>
    <script src="../JS/lib/jquery-1.3.2.min.js" type="text/javascript"></script>
	<script src="../js/lib/jquery.cookie.js" type="text/javascript"></script>
	<script src="../js/lib/jquery.treeview.js" type="text/javascript"></script>
	<script src="../js/lib/demo.js" type="text/javascript"></script>
    <script src="../js/lib/jquery.contextmenu.r2.js" type="text/javascript"></script>
    <script src="../js/winOpen.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="background: #f2f2f2; border-bottom:1px solid #cccccc; height: 40px;padding-left: 1em; vertical-align:middle; ">
    <table cellpadding="0" cellspacing="0" width="100%" border="0" height="40">
            <tr>
                    <td style="vertical-align:middle"><input type="text" value="" id="ebay_code" size="30" />
                    <input type="button" value="GO" onclick="$('#ifr_main_frame1').attr('src','ebay_system_edit.asp?ebay_system_sku='+ $('#ebay_code').val() +'&cmd=modify');"/></td>
                    <td valign="top">
                            <a class="top_btn" href="ebay_number_update.aspx" onclick="window.open(this.href,'ebay_update','left=260px,top=100px,width=400px,height=300px'); return false;">上传Ebay Number</a>
                            <a class="top_btn" href="ebay_system_comment_templete.asp" onclick=" js_callpage_name_custom(this.href,'ebay_comment', 650, 400);return false;" style="cursor:pointer;">修改系统模板</a>
                            <a class="top_btn" href="ebay_system_keyword.asp" onclick=" js_callpage_name_custom(this.href,'ebay_keyword', 650, 400);return false;" style="cursor:pointer;">修改关键字模板</a>
                    </td>
            </tr>
    </table>
</div>
<table cellpadding="0" cellspacing="0" width="100%">
        <tr>
                <td width="250" valign="top">           
                           <iframe id="ifr_left_frame1" name="ifr_left_frame2" src="/q_admin/ebayMaster/ebay_system_left_menu.asp" frameborder="0" style="width: 100%"></iframe>
                           <asp:Literal runat="server" ID="literal_treeview" Visible="false"></asp:Literal>
    	                   <%-- <ul id="browser" class="filetree">
    	                        <li><span class="folder">Folder 1</span>
    	                            <ul>
    	                                <li><span class="file">Item 1.1</span></li>
    	                            </ul>
    	                        </li>
    	                        <li><span class="folder">Folder 2</span>
                                <ul>
    	                                <li><span class="folder">Subfolder 2.1</span>
    	                                    <ul id="folder21">
    	                                        <li><span class="file">File 2.1.1</span></li>
    	                                        <li><span class="file">File 2.1.2</span></li>
    	                                    </ul>
    	                                </li>
    	                                <li><span class="file">File 2.2</span></li>
    	                            </ul>
    	                        </li>
    	                        <li class="closed"><span class="folder">Folder 3 (closed at start)</span>
    	                            <ul>
    	                                <li><span class="file">File 3.1</span></li>
    	                            </ul>
    	                        </li>
    	                        <li><span class="file">File 4</span></li>
    	                    </ul> --%>                
                
                </td>
                <td style="border-left: 1px solid #cccccc;">
                        <iframe id="ifr_main_frame1" name="ifr_main_frame1" src="" frameborder="0" style="width: 100%; height: 100px; border-bottom: 1px solid #ccc"></iframe>
                </td>
        </tr>
</table>



</div>


 <div class="contextMenu" id="myMenu1" style="display:none;">
      <ul>
        <li id="open"><img src="../../soft_img/tags/(02,40).png" /> Open</li>
        <li id="email"><img src="../../soft_img/tags/(02,40).png" /> Email</li>
        <li id="save"><img src="../../soft_img/tags/(02,40).png" /> Save</li>
        <li id="close"><img src="../../soft_img/tags/(02,40).png" /> Close</li>
      </ul>
 </div>

<script type="text/javascript">	
  $(document).ready(function() {   
        
        // window resize
     var _attr = parseInt( document.body.clientHeight);
    $('iframe').css("height", isNaN(_attr) || _attr <= 41 ? "100%": (_attr - 42) +"px");
});


var resizeTimer = null;
$(window).bind("resize", function(){
    if(resizeTimer) 
        clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function(){
            var _attr = parseInt( document.body.clientHeight);
           // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";
           $('iframe').css("height", isNaN(_attr) || _attr <= 41 ? "100%": (_attr - 42) +"px");
    }, 100);
});    
            
</script>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

