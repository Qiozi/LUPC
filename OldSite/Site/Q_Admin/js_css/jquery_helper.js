$(function () {

    $('table.page_main').css("width", "961px");
    $('td[title=page_frame]').css({ background: "#CDE3F2" });

    $('table.page_main').css("border-left", "1px solid #8E9AA8");
    $('#page_main_right_backgroundImg').css("background", "url('/soft_img/app/left_middle.gif')").css("background-position", "0px 0px").css("background-repeat", "repeat");
    if ($('#page_main_banner').length > 0)
        $('#page_main_banner').load('/site/inc/inc_banner.asp?' + rand(1000));
    $('#page_main_right_html').css("padding", "0px 3px 2px 3px").css("margin", "auto");
    //$('#page_main_right_html >ul').css("width","145px").css("margin", "auto").css("margin-top","3px");

    $('div.left_menu_child_category_area').css("display", "block");
    $('div.left_menu_child_category_area>a').css("cursor", "pointer");

    getViewCookies('ebay');

    bindHoverBTNTable();

    $('#top_search_btn_img').hover(function () {
        $(this).css({ "cursor": "pointer" }).click(function () {
            window.location.href = "/site/search.asp?keywords=" + $(this).prev().val(); ;
        });
    });



    $(window).scroll(function () {
        movePreTopFloatArea();
    });
    $(window).resize(function () {
        movePreTopFloatArea();
    });

    $("#page_loading").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

});

function movePreTopFloatArea() {
    var offsetTop = $(window).scrollTop() + 155 + "px";
    var offsetLeft = $(window).width() / 2 + 478 + "px";
    $("#pre_top_float_area2").animate({ top: offsetTop, left: offsetLeft }, { duration: 500, queue: false });
}

// globlal var.
var  js_current_system_code ;
var  js_currency_convert 	;
var  js_card_rate;
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
    

//
//
//  popImage(imageURL,imageTitle,pos,AutoClose,CopyRight)
//  OpenProductList(id, t)
//  changeProductImage(list_img, big_img)
//	setViewCookies(value, type)
//	getViewCookies(type)
//	validateNewsLetter(the)
//	IsEmail(argValue)
//	delCart(id, returnUrl)
//	js_callpage_cus(htmlurl, name, width, height)
//	showLoading()
//	closeLoading()
//	cartChangeOrderState(theSelect, theRadio, shipping_company_id, shipping_charge, is_only_charge)
//	ViewSubGroup2(elementID, image_id,the, ids, plane_count, group_id, incept_id, sys_tmp_detail,  is_show, part_max_quantity, part_quantity, lu_sku_selected)
//	ViewSubGroup(elementID, image_id,the, ids, plane_count, group_id, incept_id, sys_tmp_detail,  part_max_quantity, part_quantity, lu_sku_selected)
//	change_part_quantity(part_group_id, product_serial_no, radio_id,the)
//	js_callpage_name(htmlurl, name)
//	getSet(v)
//	writeCaseImg(case_sku)
//	onclickMenuParent(the, image_id)
//	TransferList(page_category, classs, id, the)
//	currencyConvert(price)
//	bindHoverBTNTable();
//	gotoBackCart(str)
//
//



function OpenProductList(id, t){
    switch(t)
    {
        case 'ebay':
            //$('#page_main_area').load("/ebay/inc/inc_product_list.asp?id="+ id);
			window.location.href='/ebay/list.asp?cid='+id;
        break
    }
}

function popImage(imageURL,imageTitle,pos,AutoClose,CopyRight){
	//Safari workaround
	is_safari=(navigator.userAgent.toLowerCase().indexOf("safari")!=-1)?true:false;
	safari_version=Number(navigator.userAgent.substring(navigator.userAgent.indexOf("Safari/")+7));
	if (is_safari && safari_version<300){
		var f="top=0, left=0, width="+400+",height="+400+" ,scrollbars=yes";
		imgWin=window.open(imageURL,'im',f);
	}
	else{
		//Based on browser set correct resize mode
		if (parseInt(navigator.appVersion.charAt(0))>=4){
			if (navigator.appName=="Netscape" || is_safari){
				var f="width="+400+",height="+400;	
				var rs="var iWidth=window.innerWidth;iHeight=window.innerHeight";
				var adj="0";
			}
			if(document.all){
				var f="width=150,height=150";	
				var rs = "var iWidth=document.body.clientWidth;iHeight=document.body.clientHeight;";
				if(typeof window.opera != 'undefined'){ 
					 var adj="window.outerHeight-24";
				 }
				else{
					var adj="32";
				}	 
			} 
		}
		v=pos.substring(0,pos.indexOf("_"));
		h=pos.substring(pos.indexOf("_")+1);
		f+=",left="+ (screen.width - 400) / 2 +",top="+ (screen.height - 400) / 2;
		imgWin=window.open('about:blank','',f);
		if(!imgWin)return;
		imDoc=imgWin.document;
		with (imDoc){
			writeln('<html><head><title>LU Computers</title>');writeln('<sc'+'ript>');
			writeln('var sdstr=\"\";window.onerror=function(){return true;}');
			writeln('function resizeWin(){');
			//If the image size is bigger then screen resize window to the screen size
			writeln('sc=(navigator.userAgent.toLowerCase().indexOf("safari")!=-1)?15:"";');
			writeln('iW=document.images[0].width;iH=document.images[0].height;sW=screen.availWidth;sH=screen.availHeight;');
			writeln('if(iW>=sW || iH>=sH){window.resizeTo(sW,sH);window.moveTo(0,0)}else{');
			writeln(rs);
			writeln('var v=\"'+v+'\";var h=\"'+h+'\";');
			writeln('switch (v){case \"top\":vPos=0;break;case \"middle\":vPos=(sH-iH-'+adj+')/2;break;case \"bottom\":vPos=sH-'+adj+'-iH'+((is_safari)?'-20':'')+';break;default: vPos=0;}');
			
			writeln('switch (h){case \"left\":hPos=0;break;case \"center\":hPos=(sW-iW)/2;break;case \"right\":hPos=sW-iW'+((is_safari)?'-20':'')+';break;default: hPos=0;}');
			writeln('iWidth = document.images[0].width - iWidth;iHeight = document.images[0].height - iHeight;');
			writeln('window.resizeBy(iWidth+sc, iHeight+sc);');
			writeln('/*window.moveBy(hPos,vPos)*/}}');
			writeln('function doTitle(){document.title="'+unescape(imageTitle)+'";}'+'</sc'+'ript>');
			var cT=(!AutoClose)?"":"onBlur=\"sdstr=setTimeout('window.close(this)',1000)\"";
			//Prevent closing in IE
			var fL=(!AutoClose)?"":"onfocus=\"clearTimeout(sdstr)\"";
			var safari_fix=(is_safari)?" onload='resizeWin()'":"";
			if(!CopyRight){var cr1="";var cr2=""}else{var cr1=" galleryimg=\"no\" title=\"Copyright Protected\" onmousedown=\"window.close()\" ";var cr2="oncontextmenu=\"return false\" "}
			writeln('</head><body  leftmargin=\"0\" topmargin=\"0\" marginwidth=\"0\" marginheight=\"0\" bgcolor=\"FFFFFF\"  onload="'+ ((!is_safari)?'resizeWin();doTitle();':'')+'self.focus()" '+cT+' '+fL+' '+cr2+'>');
			writeln('<div '+fL+' align=\"center\" style=\"width:100%;height:100%;overflow:auto\"><img src="'+imageURL+'" '+cr1+safari_fix+' ></div></body></html>');
			close();
		}
	}
}


//------------------------------------------------------------------------------------
function changeProductImage(list_img, big_img)
//------------------------------------------------------------------------------------
{
    $("#case_big_image").val(big_img);
    $("#product_image_list_area").attr("src", list_img);
}




//------------------------------------------------------------------------------------
function setViewCookies(value, type){
//------------------------------------------------------------------------------------
	try{
		if(type=="ebay")
		{
			var v = $.cookie("ebay_view_g");
			if(v!=null)
			{
				if(v.length>1)
				{
					if(v.indexOf("\["+value+"\]") == -1)
					{
						$.cookie("ebay_view_g", v+";\["+value+"\]");//, {expires: 7, path:'/ebay/', domain:'localhost', secure: true}
					}
				}
			}
			else
				$.cookie("ebay_view_g", "\["+value+"\]");//, {expires: 7, path:'/ebay/', domain:'lucomputers.com', secure: true}
				
		}
		else
		{
			
		}
	}
	catch(e){}
}




//------------------------------------------------------------------------------------
// type : ebay, unEbay
function getViewCookies(type)
//------------------------------------------------------------------------------------
{
	try{
		var v = $.cookie("ebay_view_g");
		if(v !=null){
			//var vs = v.split(";");
			//for(var i = 0;i<vs.length; i++)
			//alert(vs[i]);
			//$("span.left_view_watch_history").html("/site/inc/inc_view_prod_watch_history.asp?cmd=ebay&ids="+v);
			$("span.left_view_watch_history").load("/site/inc/inc_view_prod_watch_history.asp?cmd=ebay&ids="+v);
		}
	}
	catch(e){}
}



//------------------------------------------------------------------------------------
// type : ebay, unEbay
function getViewHot(type, cid)
//------------------------------------------------------------------------------------
{//$("div.view_hot_area").load("/site/inc/inc_view_prod_hot.asp?cmd="+ type +"&cid="+ cid);
		
		
		this.g = function(){$("div.view_hot_area").load("/site/inc/inc_view_prod_hot.asp?cmd="+ type +"&cid="+ cid +"&"+rand(1000));}
		
		setTimeout(this.g, 3000);
}




//------------------------------------------------------------------------------------
function ViewPartListByCategoryKeyword(hidden_keyID_control_id, element_hidden, current_element, v, page_category, class_value, category_id)
//------------------------------------------------------------------------------------
{
        //LoadWait();
        if (current_element == null)
			$('input[name=sort_by]').val(v);
		else
        	document.getElementById(current_element).value = v;
		var sortby = $('input[name=sort_by]').val();
        var els = document.getElementsByName(element_hidden);
        var idels = document.getElementsByName(hidden_keyID_control_id);
        var vls = "";
        for(var i=0; i<els.length; i++)
        {
            //if(els[i] != "-1")
            vls += "," + els[i].value +"|"+ idels[i].value ;
        }
        if(vls.indexOf("+")!= -1)
            vls = vls.replace("+", "[qiozi]");
        var svalue = vls; //(vls.substr(1,vls.length -1));
        window.location.href= "/ebay/list.asp?id="+ category_id+"&sortby="+ sortby +"&category_query_keys="+svalue;
}



//------------------------------------------------------------------------------------------
function delCart(id, returnUrl)
//------------------------------------------------------------------------------------------
{
	if(confirm('You are about to remove one item from your shopping cart, please press OK to confirm or Cancel to continue.'))
		window.location = "/site/Cart_Del.asp?Pro_id="+ id+"&returnUrl="+returnUrl;
}




//------------------------------------------------------------------------------------------
function validateNewsLetter(the)
//------------------------------------------------------------------------------------------
{
	
	if(IsEmail(the.news_email.value))
	{
		the.submit();
	}
	else
	{
		alert("Email format error!");
	}
	return false;
}


//------------------------------------------------------------------------------------------
function IsEmail(argValue)
//------------------------------------------------------------------------------------------
{
	var emailStr=argValue.toLowerCase();
	var checkTLD=1;
	var knownDomsPat=/^(com|net|org|edu|int|mil|gov|arpa|biz|aero|name|coop|info|pro|museum)$/;
	var specialChars="\\(\\)><@,;:\\\\\\\"\\.\\[\\]";
	var validChars="\[^\\s" + specialChars + "\]";
	var quotedUser="(\"[^\"]*\")";
	var ipDomainPat=/^\[(\d{1,3})\.(\d{1,3})\.(\d{1,3})\.(\d{1,3})\]$/;
	var atom=validChars + '+';
	var word="(" + atom + "|" + quotedUser + ")";
	var userPat=new RegExp("^" + word + "(\\." + word + ")*$");
	var domainPat=new RegExp("^" + atom + "(\\." + atom +")*$");
	var emailPat=/^(.+)@(.+)$/;
	var matchArray=emailStr.match(emailPat);
	if (matchArray==null)
	{
		return false;
	}
	var user=matchArray[1];
	var domain=matchArray[2];
	for (i=0; i<user.length; i++)
	{
		if (user.charCodeAt(i)>127)
		{
			return false;
		}
	}
	for (i=0; i<domain.length; i++)
	{
		if (domain.charCodeAt(i)>127)
		{
			return false;
		}
	}
	if (user.match(userPat)==null)
	{
		return false;
	}
	var IPArray=domain.match(ipDomainPat);
	if (IPArray!=null)
	{
		for (var i=1;i<=4;i++)
		{
			if (IPArray[i]>255)
			{
				return false;
			}
		}
		return true;
	} 
	var atomPat=new RegExp("^" + atom + "$");
	var domArr=domain.split(".");
	var len=domArr.length;
	for (i=0;i<len;i++)
	{
		if (domArr[i].search(atomPat)==-1)
		{
			return false;
		}
	}
	if (checkTLD && domArr[domArr.length-1].length!=2 && domArr[domArr.length-1].search(knownDomsPat)==-1)
	{
		return false;
	}
	if (len<2)
	{
		return false;
	}
	return true;
}



//------------------------------------------------------------------------------------------
function js_callpage_cus(htmlurl, name, width, height)
//------------------------------------------------------------------------------------------
{
	var wincus = window.open(htmlurl,name,'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,top=200,left=200,width='+ width +',height='+ height);
    wincus.focus();
	return false;
}



//
// ///////////// system  and  warray	(end)////////////////////////////////////////////////////////////////////////////////////////////////////
//
function displayProductGroup3(divGroup, inputGroup, imgid, table_plane_id, table_title_id, is_line, system_product_serial_no, sub_group_detail_area, is_show, part_max_quantity, part_quantity, lu_sku_selected)
{
		//showLoading();
	try{
	// 图片
		var imgsrc = document.getElementById(imgid).src;
		imgsrc = (imgsrc.substring(imgsrc.length-5,imgsrc.length));
		if (imgsrc == "2.gif")
			document.getElementById(imgid).src = '/soft_img/app/cust_arrow_1.gif';
		else
			document.getElementById(imgid).src = '/soft_img/app/cust_arrow_2.gif';
	
		// 显示细线
		if (document.getElementById(sub_group_detail_area).style.display =="")
			document.getElementById(sub_group_detail_area).style.display = "none";
		else
			document.getElementById(sub_group_detail_area).style.display = "";
		//
		// get sub group detail
		if( document.getElementById(sub_group_detail_area).innerHTML == "Loading..." || is_show)
		{
			//$('#'+sub_group_detail_area).html("/site/computer_system_get_list_2.asp?system_product_serial_no=" + system_product_serial_no + "&parent_current_configure=" + sub_group_detail_area + "&is_show_warrary3year=1&part_max_quantity=" + part_max_quantity + "&part_quantity=" + part_quantity + "&lu_sku_selected=" + lu_sku_selected);
		    $('#' + sub_group_detail_area).load("/site/computer_system_get_list_2.asp?system_product_serial_no=" + system_product_serial_no + "&parent_current_configure=" + sub_group_detail_area + "&is_show_warrary3year=1&part_max_quantity=" + part_max_quantity + "&part_quantity=" + part_quantity + "&lu_sku_selected=" + lu_sku_selected);
		}
	}
	catch(e)
	{
			
	}
}



//------------------------------------------------------------------------------------------
function displayProductGroup2(divGroup, inputGroup, imgid, table_plane_id, table_title_id, is_line, system_product_serial_no, sub_group_detail_area, part_max_quantity, part_quantity, lu_sku_selected) 
//------------------------------------------------------------------------------------------
{

    displayProductGroup3(divGroup, inputGroup, imgid, table_plane_id, table_title_id, is_line, system_product_serial_no, sub_group_detail_area, false, part_max_quantity, part_quantity, lu_sku_selected)
}


//------------------------------------------------------------------------------------------
function ViewSubGroup(elementID, image_id,the, ids, plane_count, group_id, incept_id, sys_tmp_detail,  part_max_quantity, part_quantity, lu_sku_selected, split_name)
//------------------------------------------------------------------------------------------
{
	ViewSubGroup2(elementID, image_id,the, ids, plane_count, group_id, incept_id, sys_tmp_detail,  false, part_max_quantity, part_quantity, lu_sku_selected,split_name)
}
//------------------------------------------------------------------------------------------
function ViewSubGroup2(elementID, image_id,the, ids, plane_count, group_id, incept_id, sys_tmp_detail,  is_show, part_max_quantity, part_quantity, lu_sku_selected, split_name)
//------------------------------------------------------------------------------------------
{
	//showLoading();
	//try{
//		//alert(split_name);
//	var table_tr = document.getElementsByTagName("TR");
//	for(var i=0; i<table_tr.length; i++)
//	{
//		if(table_tr[i].id.indexOf(elementID)!= -1)
//		{
//			if(table_tr[i].style.display== 'none')
//			{
//				table_tr[i].style.display= '';	
//				if ($('#'+incept_id).html() == "Load......" || is_show || $('#'+incept_id).html() == "")
//					//$('#'+incept_id).html("/site/computer_system_get_sub_list.asp?sys_tmp_detail="+ sys_tmp_detail +"&incept_id="+ incept_id +"&plane_count="+ plane_count + "&group_id="+ group_id +"&ids="+ ids+"&part_max_quantity=" + part_max_quantity + "&part_quantity=" + part_quantity + "&lu_sku_selected=" + lu_sku_selected +"&split_name"+ split_name);
//					$('#'+incept_id).load("/site/computer_system_get_sub_list.asp"
//										  ,{"sys_tmp_detail": sys_tmp_detail
//										  		, "incept_id": incept_id 
//												, "plane_count": plane_count 
//												, "group_id": group_id 
//												, "ids": ids
//												, "part_max_quantity": part_max_quantity
//												, "part_quantity": part_quantity
//												, "lu_sku_selected": lu_sku_selected
//												, "split_name":split_name
//										  }
//										  ,function(){closeLoading();}
//										  );
//					
//					
//			}
//			else
//			{
//				table_tr[i].style.display= 'none';	
//				
//			}
//		}
//	}
//	var imageEL = document.getElementById(image_id);
//	if(imageEL.src.indexOf("/soft_img/app/col.gif") != -1)
//		imageEL.src = '/soft_img/app/exp.gif';
//	else
//		imageEL.src = '/soft_img/app/col.gif';
//	}
//	catch(e)
//	{
//		alert(e);
//	}
	
	try{
		//alert(split_name);
	//var table_tr = document.getElementsByTagName("TR");
	//for(var i=0; i<table_tr.length; i++)
	//{
		//if(table_tr[i].id.indexOf(elementID)!= -1)
		//{
			
			if($('#'+elementID).css("display")== 'none')
			{
				$('#'+elementID).css("display", "");	
				if ($('#'+incept_id).html() == "Load......" || is_show || $('#'+incept_id).html() == "")
					//$('#'+incept_id).html("/site/computer_system_get_sub_list.asp?sys_tmp_detail="+ sys_tmp_detail +"&incept_id="+ incept_id +"&plane_count="+ plane_count + "&group_id="+ group_id +"&ids="+ ids+"&part_max_quantity=" + part_max_quantity + "&part_quantity=" + part_quantity + "&lu_sku_selected=" + lu_sku_selected +"&split_name"+ split_name);
					$('#'+incept_id).load("/site/computer_system_get_sub_list.asp"
										  ,{"sys_tmp_detail": sys_tmp_detail
										  		, "incept_id": incept_id 
												, "plane_count": plane_count 
												, "group_id": group_id 
												, "ids": ids
												, "part_max_quantity": part_max_quantity
												, "part_quantity": part_quantity
												, "lu_sku_selected": lu_sku_selected
												, "split_name":split_name
										  }
										  ,function(){closeLoading();}
										  );
					
					
			}
			else
			{
				$('#'+elementID).css("display", "none");	
				
			}
			
		//}
	//}
	var imageEL = document.getElementById(image_id);
	if(imageEL.src.indexOf("/soft_img/app/col.gif") != -1)
		imageEL.src = '/soft_img/app/exp.gif';
	else
		imageEL.src = '/soft_img/app/col.gif';
	}
	catch(e)
	{
		alert(e);
	}
}



//----------------------------------------------------
function change_part_quantity(part_group_id, product_serial_no, radio_id,the, system_product_serial_no) {
//----------------------------------------------------
    var price_single = $("#product_child_single_price_" + system_product_serial_no + "_" + product_serial_no).html();
    var save_single = $("#product_child_single_save_" + system_product_serial_no + "_" + product_serial_no).html();
    var part_quantity = the.selectedIndex + 1;


    var price_element = document.getElementById("product_child_price_price_" + system_product_serial_no + "_" + product_serial_no);
    if (price_element) {
        var price = price_element.innerHTML;
		//alert($("#product_child_single_price_" + system_product_serial_no + "_" + product_serial_no).html());
        price_element.innerHTML = parseFloat(part_quantity * parseFloat(price_single).toFixed(2)).toFixed(2);
    }

    price_element = document.getElementById("product_child_price_real_price_" + system_product_serial_no + "_" + product_serial_no);
    if (price_element) {
        var price = price_element.innerHTML;
        price_element.innerHTML = (part_quantity * (Number(parseFloat(price_single)) - Number(parseFloat(save_single)))).toFixed(2);
    }

    price_element = document.getElementById("product_child_price_discount_" + system_product_serial_no + "_" + product_serial_no);
    if (price_element) {
        var price = price_element.innerHTML;
        price_element.innerHTML = (part_quantity * Number(parseFloat(price_single))).toFixed(2);
    }
    
    if ($('#'+radio_id).attr("checked")) {
       // document.getElementById("iframe1").src = "/site/computer_system_change_quantity.asp?part_group_id=" + part_group_id + "&product_serial_no=" + product_serial_no + "&part_quantity=" + part_quantity;
		$('#current_part_quantity_'+ system_product_serial_no).val(part_quantity);
		//if (part_quantity>1 )
		{
			$('#current_part_quantity_view_'+ system_product_serial_no).html(part_quantity + "X").css("display", "");
		}
		//alert($('#current_part_quantity_view_'+ system_product_serial_no).html());		
		customizeSystemStatPrice();
    }	
	
}


//----------------------------------------------------
function getSet(v)
//----------------------------------------------------
{
	if(v == 1)
	{
		$("#product_c_1").attr("class", "title1");
		$("#product_c_2").attr("class", "title2");
		$("#product_c_3").attr("class", "title2");
		
		$("#cust_plane_1").css("display", "");
		$("#cust_plane_2").css("display", "none");
		$("#cust_plane_3").css("display", "none");
		$("#submit_button").html("Next");
	}
	if(v == 2)
	{
		$("#product_c_1").attr("class", "title2");
		$("#product_c_2").attr("class",  "title1");
		$("#product_c_3").attr("class",  "title2");
		
		$("#cust_plane_1").css("display", "none");
		$("#cust_plane_2").css("display", "");
		$("#cust_plane_3").css("display", "none");
		$("#submit_button").html("Next");
	}
	if(v == 3)
	{
		$("#product_c_1").attr("class",  "title2");
		$("#product_c_2").attr("class",  "title2");
		$("#product_c_3").attr("class",  "title1");
		
		$("#cust_plane_1").css("display","none");
		$("#cust_plane_2").css("display","none");
		$("#cust_plane_3").css("display", "");
		$("#submit_button").html("Add &nbsp;to&nbsp; Cart");
	}
	
}



//------------------------------------------------------------------------------------------
function customerSubmit()
//------------------------------------------------------------------------------------------
{
	customerSubmit(0);
}



//------------------------------------------------------------------------------------------
function customerSubmit(s)
//------------------------------------------------------------------------------------------
{
	if(document.getElementById("product_c_1").className == "title1")
		return getSet(2);
	if(document.getElementById("product_c_2").className == "title1")
	{
		document.getElementById("submit_button").innerHTML = "Add &nbsp;to&nbsp; Cart";
		return getSet(3);
	}
	if(document.getElementById("product_c_3").className == "title1")
		buy();
}



//------------------------------------------------------------------------------------------
function js_callpage_name(htmlurl, name)
//------------------------------------------------------------------------------------------
{
	var nw2 = window.open(htmlurl,name,'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,top=2,width=618,height=650');
    nw2.focus();
	return false;
}



//------------------------------------------------------------------------------------------
function getProductName2(press_image_logo, press_title_id, title_post_id, sys_tmp_detail, product_serial_no, sys_tmp_sku)
//------------------------------------------------------------------------------------------
{
	try{
                var current_selected_control;
                var current_selected_control_value
            try{
                     current_selected_control = document.getElementById("part_max_quantity_"+sys_tmp_detail+"_"+product_serial_no);
                     current_selected_control_value = current_selected_control.options[current_selected_control.selectedIndex].value;
                }
                catch(e){
                 
                     current_selected_control_value = 1;
                
                }
	// 改变标题文件本
	var title = document.getElementById(press_title_id).innerHTML;
	
	if (title.length > 5)
	{
		$('#'+ title_post_id).html( title.replace("(Featured)&nbsp;", ""));
		
	}
	else
	{
		document.getElementById(title_post_id).innerHTML = title;
	}
	
	if(current_selected_control_value>=1)
		$('#current_part_quantity_view_'+ sys_tmp_detail).html(current_selected_control_value + "X");
	if(current_selected_control_value>1)
		$('#current_part_quantity_view_'+ sys_tmp_detail).html(current_selected_control_value + "X").css("display", "");
	//	alert(current_selected_control_value)
	//	else
	//	$('#current_part_quantity_view_'+ sys_tmp_detail).html(current_selected_control + "X").css("display", "");
		
	$('#current_part_'+ sys_tmp_detail).val(product_serial_no);
	$('#current_part_price_'+ sys_tmp_detail).val($.trim($('#product_child_single_price_'+ sys_tmp_detail +'_'+ product_serial_no).html()));
	$('#current_part_discount_'+ sys_tmp_detail).val($.trim($('#product_child_single_save_'+ sys_tmp_detail +'_'+ product_serial_no).html()));
	$('#current_part_name_'+ sys_tmp_detail).val($.trim($('#'+ title_post_id).text().replace(/,/gi, "\[Q\]")));

	$('#current_part_cost_'+ sys_tmp_detail).val($.trim($('#product_child_single_cost_'+ sys_tmp_detail +'_'+ product_serial_no).html()));
	$('#current_cate_name_'+ sys_tmp_detail).attr('title', product_serial_no);
	
	eval(document.getElementById(document.getElementById("a_product_name_"+ sys_tmp_detail).value)).style.color = '#4C4C4C';
	

	document.getElementById(press_title_id).style.color = "#EF5412";
	eval(document.getElementById("a_product_name_"+ sys_tmp_detail )).value =press_title_id;
	
		// change img t
		try{
			eval(document.getElementById(document.getElementById("current_img_logo_"+ sys_tmp_detail).value)).style.display = 'none';
			document.getElementById(press_image_logo).style.display = '';
			eval(document.getElementById("current_img_logo_"+ sys_tmp_detail)).value = press_image_logo;
		}
		catch(e){}
		
		try{
			
			var current_selected_control_id = "part_max_quantity_"+sys_tmp_detail+"_"+product_serial_no;
            
			var selected_control_id = "part_max_quantity_"+ sys_tmp_detail +"_"+ product_serial_no;
			
			$('#' + current_selected_control_id).parent().parent().find('select').each(function(e){
																								$('#'+ selected_control_id).find('option').each(function(){ 
																																						 if($(this).attr("selected"))
																																						 {
	var quantity = $(this).attr('value');	
	$('#current_part_quantity_'+ sys_tmp_detail).val(quantity);
	}
																																						 });
																								
																								});
				
			
		}
		catch(e){alert(e);}
		
		customizeSystemStatPrice();
		
		// change Case img
		if($('#current_cate_name_'+ sys_tmp_detail).val().toLowerCase() == "case")
		{
			viewCaseImg();	
		}
		//alert(js_current_system_code);
	}
	catch(e)
	{
		//alert(e);
	}	
}



//-----------------------------------------------------------------------------------
function currencyConvert(price)
//-----------------------------------------------------------------------------------
{
	//alert(js_current_system_code)
	if(js_current_system_code=="2")
	{
		
		return parseFloat(price * js_currency_convert).toFixed(2);		
	}
	else
		return parseFloat(price).toFixed(2);
	
}

function customizeSystemStatPrice()
{
		var total = 0;
		var discount = 0;
		var quantity = 1;
		
		
		$("input[name=current_part_price]").each(function(){
					//total += parseFloat($(this).val());		
					var sys_prod_id = $(this).attr('id').replace("current_part_price_", "");					
					quantity = parseInt($('#current_part_quantity_'+ sys_prod_id).val());
					total += parseFloat($(this).val()) * quantity;	
					//alert(quantity);
										});
		$("input[name=current_part_discount]").each(function(){
					
					var sys_prod_id = $(this).attr('id').replace("current_part_discount_", "");
					quantity = parseInt($('#current_part_quantity_'+ sys_prod_id).val());
					if(parseFloat($(this).val()) != 0)
						discount += parseFloat($(this).val()) * quantity;
						//alert($('#current_part_quantity_'+ sys_prod_id).val());
					
										});
//		alert(total);
//		alert(discount);
		
		var regular_price = total;
		var now_low_price = (total - discount).toFixed(2);
		
		//alert(discount);
		$('span[name=regular_price]').html(total.toFixed(2));
		$('span[name=now_low_price]').html(now_low_price);
		$('span[name=discount_price]').html(discount.toFixed(2));
		$('input[name=system_discount]').val(discount.toFixed(2));
		if(discount ==0)
		{
			$('tr.no_save').css("display", "none");	
			$('#page_main_sys_regular_text1').html('<b>&nbsp;Now &nbsp;Low&nbsp;Price:</b>');
			$('#page_main_sys_regular_text2').html('<b>&nbsp;Regular&nbsp;Price: </b>');
		}
		else
		{
			$('tr.no_save').css("display", "");
			$('#page_main_sys_regular_text1').html('<b>&nbsp;Regular&nbsp;Price: </b>');
			$('#page_main_sys_regular_text2').html('<b>&nbsp;Now &nbsp;Low&nbsp;Price:</b>');
		}
		var special_cash = parseFloat(now_low_price / 1.022).toFixed(2);
		
		$('span[name=special_cash_price]').html(special_cash);
		$('input[name=system_sell]').val(now_low_price);
}

//------------------------------------------------------------------------------------------
function getProductName(press_image_logo, press_title_id, title_post_id, sys_tmp_detail, product_serial_no, sys_tmp_sku)
//------------------------------------------------------------------------------------------
{
	getProductName2(press_image_logo, press_title_id, title_post_id, sys_tmp_detail, product_serial_no, sys_tmp_sku)
}



//------------------------------------------------------------------------------------------
function showLoading()
//------------------------------------------------------------------------------------------
{
    $('#page_loading').dialog("open");
}



//------------------------------------------------------------------------------------------
function closeLoading() {
    $('#page_loading').dialog("close"); 
}
//------------------------------------------------------------------------------------------




//------------------------------------------------------------------------------------------
function writeCaseImg(case_sku)
//------------------------------------------------------------------------------------------
{
	if (case_sku.length >0)
	{
		$('#case_img_big_2').load("/site/inc/inc_write_system_case_img.asp?sku="+ case_sku);
	}
}



//------------------------------------------------------------------------------------------
function onclickMenuParent(the, image_id)
//------------------------------------------------------------------------------------------
{
	//var els = document.getElementsByTagName("TABLE");
	var n = 1;
	
	if ($('#'+ the).css("display")!="none")
	{
		$('#'+ the).css("display","none");
		$('#'+image_id).attr("class", "page_left_menu_parent_sub2");

	}
	else
	{
		$('#'+ the).css("display","");
		$('#'+image_id).attr("class", "page_left_menu_parent_sub1");
	}
	//alert(the + '|' + image_id);
}




//------------------------------------------------------------------------------------------
function TransferList(page_category, classs, id, the)
//------------------------------------------------------------------------------------------
{
	try
	{
	    var httpHead = "";
	    if (location.href.toLowerCase().indexOf("https://www.lucomputers.com/") != -1)
	        httpHead = "http://www.lucomputers.com";
	    
		showLoading();
		the.style.color = "#EF5412";
		
//		// 288 system page
//		if (id == 288 || id == "288")
//		{
//			window.location.href= httpHead + "/site/product_list_"+ id +".asp?page_category="+ page_category +"&class="+ classs +"&id="+ id;	
//			return;
//		}
//		if(document.URL.toLowerCase().indexOf('id=288')!= -1)
//		{
//			
//			window.location.href= httpHead + "/site/product_list.asp?page_category="+ page_category +"&class="+ classs +"&id="+ id;	
//			return;
//		}
//		
//		// 74
//		
//		if (id == 74 || id == "74")
//		{
//		
//			window.location.href= httpHead + "/site/product_list_"+ id +".asp?page_category="+ page_category +"&class="+ classs +"&id="+ id;	
//			return;
//		}
//		if(document.URL.toLowerCase().indexOf('id=74')!= -1)
//		{
//			window.location.href= httpHead +  "/site/product_list.asp?page_category="+ page_category +"&class="+ classs +"&id="+ id;	
//			return;
//		}

		
		if(document.URL.toLowerCase().indexOf('/product_list.asp')== -1)
		{
			window.location.href= httpHead + "/site/product_list.asp?page_category="+ page_category +"&class="+ classs +"&cid="+ id;	
		}
		else
		{
			//document.getElementById("ifr_product_sub_list").src = httpHead + "/lists/product_list_sub.asp?page_category="+ page_category +"&class="+ classs +"&id="+ id;	
			window.location.href=httpHead + "/site/product_list.asp?page_category="+ page_category +"&class="+ classs +"&cid="+ id;	
			//alert(page_category);
		}
		//var el = document.getElementById("loading");
//		document.body.removeChild(el);
	}
	catch(e)
	{
		//alert(e);	
	}
}



//------------------------------------------------------------------------------------------
function onclickMenuChild(the)
//------------------------------------------------------------------------------------------
{
	var b = document.getElementById(the).style.display;
	var els = document.getElementsByTagName("UL");
	for(var i=0; i<els.length; i++)
	{
		if(els[i].name == "menu_left_child")
		els[i].style.display = "none";	
	}
	//alert(the);
	if(b != "none")
		document.getElementById(the).style.display="none";	
	else
		document.getElementById(the).style.display="";	
}



//------------------------------------------------------------------------------------------
function bindHoverBTNTable()
//------------------------------------------------------------------------------------------
{
	$('table.btn_table').hover(
						   function(){
							   $(this).find('a').css("color", "#cccccc");
							   }
							,function(){
								$(this).find('a').css("color", "#ffffff");
							}
						);
}


//------------------------------------------------------------------------------------------
function gotoBackCart(str)
//------------------------------------------------------------------------------------------
{
	if(str == "ebay")
		window.location.href='/ebay/cart.asp';
	if(str == "site")
		window.location.href='/site/Shopping_Cart.asp';
}


//
function imgerror(the)
{
	the.src = "/pro_img/noexist.gif";
}



////////////////////////////////////////////////////////////float /////////////////////////////////////////////////
//-----------------------------------------------------------------------------------------------
// Diagram Functions
//-----------------------------------------------------------------------------------------------
function SetDiagramPosition ()
{			
	var xPos;
	var yStart;
	var yPos;
	var yInc;
	
	if (pGI)
	{	
		if (IsNetScapeBrowser ())
		{
			xPos = window.pageXOffset + (window.innerWidth/2 + 960 /2);

			var yHeight = document.getElementById ("IconDiagram_Layer").clientHeight;
			if (yHeight == 0)
				yHeight = 128;
			yPos = window.pageYOffset + window.innerHeight - yHeight - 40;
			
			if (pGI.AlienwareRequest > 0)
				yStart = yPos;		
			else
			{			
				yStart = document.getElementById("IconDiagram_Layer").style.top;			
				yInc = Math.log(Math.pow(yPos - yStart, 3));
				
				if (yStart + yInc >= yPos)
					yStart = yPos;
				else if (yStart < yPos)	
					yStart += yInc;
				else
					yStart = yPos;		
			}
			
			document.getElementById("IconDiagram_Layer").style.top = yStart;
			//document.getElementById("IconDiagram_Layer").style.left = xPos;
			if (window.document.body.clientWidth <=1280)
				document.getElementById("IconDiagram_Layer").style.left = xPos - 80;
			else
				document.getElementById("IconDiagram_Layer").style.left = xPos;
			//document.getElementById("SystemDiagram_Layer").style.top  = yStart - 245;
			//document.getElementById("SystemDiagram_Layer").style.left = xPos - 295;
			
		}
		else
		{
			xPos = document.body.scrollLeft + (window.document.body.clientWidth/2 + 960 /2);			
			yPos = document.body.scrollTop + document.body.clientHeight -  400;
			
			if (pGI.AlienwareRequest > 0)
				yStart = yPos;		
			else
			{
				yStart = IconDiagram_Layer.style.pixelTop;		
				yInc = Math.log(Math.pow(yPos - yStart, 2));		
				if (yStart + yInc >= yPos)
					yStart = yPos;
				else if (yStart < yPos)	
					yStart += yInc;
				else
					yStart = yPos;		
			}
			
			//document.getElementById('page_main_right').innerHTML = document.body.scrollTop;
			IconDiagram_Layer.style.pixelTop  = yStart ;
			//alert(page_main_center_area.style.left);
			if (window.document.body.clientWidth <=1280)
				IconDiagram_Layer.style.pixelLeft = xPos - 80 ;
			else
				IconDiagram_Layer.style.pixelLeft = xPos ;
			//alert(page_main_center_area.style.pixelLeft);
			//IconDiagram_Layer.style.pixelLeft = page_main_center_area.style.pixelLeft+ 960 - 182;;
			
			//SystemDiagram_Layer.style.pixelTop  = yStart - 245;
			//SystemDiagram_Layer.style.pixelLeft = xPos - 295;	
		}
	}
	
	var nTimeOut = 50;
	if (pGI && pGI.AlienwareRequest > 0)
		nTimeOut = 2000;
	setTimeout('SetDiagramPosition()', nTimeOut);
}


function IsNetScapeBrowser ()
{
	return !document.all;
}

var DiagramVisible = false;
function __OnToggle_Diagram ()
{		
	DiagramVisible = !GetLayerVisibility ('SystemDiagram_Layer');
	if (DiagramVisible)							
		CheckRequirementsAndProvisions ();
	
	SetLayerVisibility ('SystemDiagram_Layer', DiagramVisible);
}

function __OnLoad_Diagram ()
{		
	SetLayerVisibility ('IconDiagram_Layer', true);
	SetDiagramPosition ();
}

function SetLayerVisibility (sLayerId, bVisible)
{
	var sStatement;
	var sVisibility = (bVisible) ? "visible" : "hidden";
	if (IsNetScapeBrowser ())
		sStatement = "document.getElementById ('" + sLayerId + "').style.visibility = '" + sVisibility + "';";		
	else
		sStatement = sLayerId + ".style.visibility = '" + sVisibility + "';";	
		
	try {
		eval (sStatement);
	} catch (e) {}	
	
}

function GetLayerVisibility (sLayerId)
{
	var sStatement;
	var sResult = '';
	
	if (IsNetScapeBrowser ())
		sStatement = "sResult = document.getElementById ('" + sLayerId + "').style.visibility;";		
	else
		sStatement = "sResult = " + sLayerId + ".style.visibility;";	
		
	try {
		eval (sStatement);
	} catch (e) {}			
	
	return sResult == 'visible';
}
var pGI =new TGI (0,1,2,0,1,15,0,0,'$XXX,YYY','$XXX,YYY.ZZ');

function TGI (TaxPercentage, ShipRate, OrderProcessingTime, PotentialShippingDelay, FinancingEnabled, FinancingMinimumMonthlyPayment, AlienwareRequest, Total2Enabled, CurrencyFormat0Decimal, CurrencyFormat2Decimal)
{
	this.TaxPercentage = TaxPercentage;
	this.ShipRate = ShipRate;	
	this.OrderProcessingTime = OrderProcessingTime;
	this.PotentialShippingDelay = PotentialShippingDelay;	
	this.FinancingEnabled = FinancingEnabled;
	this.FinancingMinimumMonthlyPayment = FinancingMinimumMonthlyPayment;

	this.AlienwareRequest = AlienwareRequest;
	this.Total2Enabled = Total2Enabled;	
	this.CurrencyFormat0Decimal = CurrencyFormat0Decimal;
	this.CurrencyFormat2Decimal = CurrencyFormat2Decimal;
}// JavaScript Document
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

