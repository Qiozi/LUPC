<!--#include virtual="site/inc/inc_page_top.asp"-->
<style>
.pre-index-sys--cate-title  
{
    display: block; 
    background-color:#CDE3F2;
    font-size:12px;
    font-weight:bold;
    color:#666;
    line-height: 35px;                                
    text-align:center;
    float:left;
    border-bottom: 3px solid #CDE3F2;  
}

.width200{ width: 200px;}
.width120{ width: 120px;}
.width80{ width:80px;}
.pre-index-sys--cate-title-selected{ display: block; float:left;
                                  background-color:White;
                                  font-size:12px;
                                  font-weight:bold;
                                  color:#666;
                                  line-height: 35px;                                                                  
                                  text-align:center;                                  
                                  background-repeat: no-repeat;
                                  background-position:bottom center;
                                  border-bottom: 3px solid white;/*#f12a2a;  */
                                  }
                                  
                                  
.pre-index-part-logo-detail 
{
    width: 238px;
    height: 238px;    
    margin:5px;
    background:#fff;
}
.pre-index-part-logo-detail img{ display:none}

.pre-index-part-logo-detail:hover
{
    width: 238px;
    height: 238px;  
    margin:5px;  
}

.pre-index-part-logo-detail:hover img
{
    display:inline;
    
}

.button {
	display: inline-block;
	zoom: 1; /* zoom and *display = ie7 hack for display:inline-block */
	*display: inline;
	vertical-align: baseline;
	margin: 0 2px;
	margin-left: 10px;
	outline: none;
	cursor: pointer;
	text-align: center;
	text-decoration: none;
	font: 14px/100% Arial, Helvetica, sans-serif;
	padding: 3px 2em 3px;
	text-shadow: 0 1px 1px rgba(0,0,0,.3);
	-webkit-border-radius: .5em; 
	-moz-border-radius: .5em;
	border-radius: .5em;
	-webkit-box-shadow: 0 1px 2px rgba(0,0,0,.2);
	-moz-box-shadow: 0 1px 2px rgba(0,0,0,.2);
	box-shadow: 0 1px 2px rgba(0,0,0,.2);
}
.button:hover {
	text-decoration: none;
	
}
.button:active {
	position: relative;
	top: 1px;
}
/* red */
.red {
	color: #faddde;
	border: solid 1px #980c10;
	background: #d81b21;
	background: -webkit-gradient(linear, left top, left bottom, from(#ed1c24), to(#aa1317));
	background: -moz-linear-gradient(top,  #ed1c24,  #aa1317);
	filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#ed1c24', endColorstr='#aa1317');
}
.red:hover {
	background: #b61318;
	background: -webkit-gradient(linear, left top, left bottom, from(#c9151b), to(#a11115));
	background: -moz-linear-gradient(top,  #c9151b,  #a11115);
	filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#c9151b', endColorstr='#a11115');
}
.red:active {
	color: #de898c;
	background: -webkit-gradient(linear, left top, left bottom, from(#aa1317), to(#ed1c24));
	background: -moz-linear-gradient(top,  #aa1317,  #ed1c24);
	filter:  progid:DXImageTransform.Microsoft.gradient(startColorstr='#aa1317', endColorstr='#ed1c24');
}


.clearBoth{ clear:both;}
.overflowHidden{ overflow:hidden;}

.show{display:block;}
.font9pt { font-size: 9pt;}
.textCenter { text-align: center;}
.textRight { text-align: right;}
.textLeft{text-align:left;}
.pretop{ background:url(images/index-top-back.jpg); height: 100px;}
.pre-index-border-white 
{
    border:1px solid white;
    height:100% 
}
.pre-index-border-white:hover 
{
    border:1px solid #cccccc;  
    background:#f2f2f2; 
    height:100% 
}

.pre-index-border-white:hover .pre-index-part-detail-price-area
{     
    background:#f2f2f2; 
}

.pre-index-border-white:hover .pre-index-part-detail-logo
{   
    background:#fff; 
    width:236px;
}

.ul-table >li{float:left;}
.nodisplay { display:none ;}
</style>
<%
	
	CAll CurrentSystemDefault("site", CurrentIsEbay)
%>

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top"  class='page_frame' style="border-bottom:1px solid #999;">
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div style="display:none;" class='page_main_nav' id="page_main_nav">Ebay</div>
            	<div id="page_main_area"></div>
            <!-- main end 	-->
        </td>
        
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	$('#page_main_area').load('/site/inc/inc_default2.asp?'+rand(1000));
});
</script>