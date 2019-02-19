function winOpen(pathfile, winName, width, height, top, left)
{
    var nw = window.open(pathfile, winName, "scrollbars=yes, left=" + left + ",top=" + top + ", height=" + height + ",width=" + width );
    nw.focus();
    return nw;
}

function OpenOrderDetail(code)
{   
    var order_detail_new =  window.open('sales_order_detail.aspx?order_code='+code,'order_detail','width=850,height=800,menubar=1,toolbar=0,status=0,scrollbars=1,resizable=0');
    order_detail_new.focus();
}

function js_callpage_name(htmlurl, name)
{
	var nw2 = window.open(htmlurl,name,'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,top=2,width=618,height=650');
    nw2.focus();
	return false;
}
function js_callpage_name_custom(htmlurl, name, width, height)
{
	var nw22 = window.open(htmlurl,name,'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,top=2,width='+ width+',height='+height);
    nw22.focus();
	return false;
}


//------------------------------------------------------------------------------------------
function js_callpage_cus(htmlurl, name, width, height)
//------------------------------------------------------------------------------------------
{
	var wincus = window.open(htmlurl,name,'toolbar=no,location=no,directories=no,status=no,menubar=no,scrollbars=yes,resizable=yes,top=200,left=200,width='+ width +',height='+ height);
    wincus.focus();
	return false;
}
