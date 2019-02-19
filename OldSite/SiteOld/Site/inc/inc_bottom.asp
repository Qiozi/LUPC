

<table width="960"  border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="8"></td>
    <td height="8"></td>
  </tr>
  <tr>
    <td width="947"><table width="100%"  border="0" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#9ECBE9 1px solid; ">
      <tr>
        <td style="border:#E3E3E3 1px solid; "><table width="100%"  border="0" cellspacing="0" cellpadding="0">
          <tr>
            <td height="40"><div align="center" class="text_blue_11"><a href="<%= LAYOUT_HOST_URL %>about_us.asp" class="blue_orange_11">About Us</a> | <a href="<%= LAYOUT_HOST_URL %>Contact_us.asp" class="blue_orange_11">Contact Us</a> | <a href="<%= LAYOUT_HOST_URL %>general_faq.asp" class="blue_orange_11">General FAQ</a> | <a href="<%= LAYOUT_HOST_URL %>shipping.asp" class="blue_orange_11">Shipping FAQ</a> | <a href="<%= LAYOUT_HOST_URL %>warranty.asp" class="blue_orange_11">Warranty FAQ</a> | <a href="<%= LAYOUT_HOST_URL %>customer_service.asp" class="blue_orange_11">Support Center</a> | <a href="<%= LAYOUT_HOST_URL %>tech_support.asp" class="blue_orange_11">Support FAQ</a> | <span ><a href="<%= LAYOUT_HOST_URL %>manufacturers.asp" class="blue_orange_11">Manufacturers</a> | </span><a href="<%= LAYOUT_HOST_URL %>driver_links.asp" class="blue_orange_11">Support Links</a><br>
                <a href="<%= LAYOUT_HOST_URL %>contact.asp" class="blue_orange_11">Contacts</a> | <a href="<%= LAYOUT_HOST_URL %>company_policy.asp" class="blue_orange_11">Company Policy</a> | <a href="<%= LAYOUT_HOST_URL %>payment.asp" class="blue_orange_11">Payment Methods</a> | <a href="<%= LAYOUT_HOST_URL %>privacy_security.asp" class="blue_orange_11">Privacy Security</a> | <a href="<%= LAYOUT_HOST_URL %>" class="blue_orange_11">Home</a></div></td>
          </tr>
        </table></td>
      </tr>
    </table></td>
  <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="40" class="text_red_12"><div align="center"><% 
	Dim begin_timer_sc	:	begin_timer_sc	=	null
	if begin_timer_sc <> "" then response.Write((timer()-begin_timer_sc)*1000) : response.Write("MS<Br>")%>Copyrights &copy; 2004-2014. Lu Computers. All rights reserved. </div></td>
    <td>&nbsp;</td>
  </tr>
</table>

<div id="pre_top_float_area2" style="width:100px; position:absolute; left:11px;float:left;">
    <a id="page_top_username" href='/site/member_login.asp'>Log in</a>
    <a href='/site/p_rebate.asp'>Rebate</a>
    <a href='/site/p_sale.asp'>On Sale</a>
    <a href='/site/Shopping_Cart.asp'>My Cart</a>
    <a href='/site/member_login.asp'>My Account</a>
    <a href="/site/member_logout.asp" id='pre-logout' style='display:none;'>Log out</a>
</div>

<div id='page_loading' style="display:none;padding:3em;" title="Load"><img src="/soft_img/tags/loaderc.gif" /><span style='font-size:2em;'>&nbsp;&nbsp;Loading</span></div>
<!--script src="/jsized.snow.min.js" type="text/javascript"><script-->  
<script type="text/javascript">
/* google stat */
try{
_uacct = "UA-4447256-1";
urchinTracker();
}catch(e){}



//createSnow('/', 60);

<%'= Current_system & CurrentIsEbay%>


	if( <%= lcase(CurrentIsEbay) %>)
		$("a.at_ebay_in").css("border", "1px solid #174674").css("margin-left","24px").css("background","#8BA2B9");
	else
		$("a.at_ebay_in").css("border", "0px solid #174674").css("margin-left","24px");	
	

	<% 
	if not CurrentIsEbay then 
		Response.write "$('#page_top_10_area').load(""/site/top10_tpl.asp?r=""+rand(1000));"
		
	else
		Response.Write "$('#page_top_btn_list>a').each(function(){ if($(this).attr('class')=='blue_white_11'){$(this).remove();} });"
		Response.Write "$('#page_top_btn_list>b').remove();"
	End if
	%>
	
	$(function(){
        $('#page_top_username').load('/site/get_username.asp?'+rand(1000), function(){
        
            $('#page_top_username')
            .button({ icons: { primary: "ui-icon-person"} }).click(function () {


             })         
             .css({ "width": "120px" })
             .next().button({ icons: { primary: "ui-icon-arrowstop-1-e"} }).click(function () {
             
             })
             .css({ "width": "120px"})
             .next().button({ icons: { primary: "ui-icon-arrowstop-1-e"} }).click(function () {
             
             })
             .css({ "width": "120px"})
             .next().button({ icons: { primary: "ui-icon-arrowstop-1-e"} }).click(function () {
             
             })
             .css({ "width": "120px"})
             .next().button({ icons: { primary: "ui-icon-arrowstop-1-e"} }).click(function () {
             
             })
             .css({ "width": "120px"})
             .next().button({ icons: { primary: "ui-icon-arrowstop-1-w"} }).click(function () {
             
             })
             .css({ "width": "120px"});

        });
        //
        //
        

         movePreTopFloatArea();

         $('#pre-view-index-area').dialog({
             autoOpen: false,
             width: 800,
             height:600,
             show: {
                 effect: "blind",
                 duration: 1000
             }
         , hide: { effect: "explode", duration: 1000 }
         });
	});
	
</script>
<center><span id="siteseal"><script type="text/javascript" src="https://seal.godaddy.com/getSeal?sealID=m1rXgddaupGKJemjFI0QslvJUzAlm8xCPKQIS8YF5EQySbD7W50Rjths2lK"></script></span></center>