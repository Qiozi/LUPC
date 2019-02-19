//
//
//
//  function SetIframeSize(iframeName)
//  SetIframeSize2(iframeName, removeHeight)
//  MergeCellsVertical(tbl, cellIndex)
//  SendEmailToEbayUser(the, order_code)
//  showLoading()
//  closeLoading()
//
//
//
//
//
//
//
//
//
//
//
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


function StatShowYearReport(el)
{
    if(document.getElementById(el).style.display == "")
        document.getElementById(el).style.display = "none";
    else
        document.getElementById(el).style.display = "";
    
}

function statShowYearDetail(el,ELoading, year, month)
{
    try{
    if(document.getElementById(el).style.display == "")
        document.getElementById(el).style.display = "none";
    else
        document.getElementById(el).style.display = "";
   
        if(document.getElementById(ELoading).innerHTML == "Loading...")
        {
            document.getElementById("iframe1").src="sale_stat_month_report_2_sub.aspx?ParentElement="+ ELoading+"&year="+year + "&month="+ month;
        
        }     
    }
    catch(e){ alert(e);}
}

function setPartExportValue(id,checked)
{
    var c = 1;
    if (!checked )
        c = 0;
   document.getElementById("iframe1").src="sale_stat_month_report_2_sub.aspx?id="+ id+"&OrderHelperChecked="+c ;

}

function exportToFile(year,month)
{    
    try{
    if(!document.all)
    document.getElementById("iframe2").src="sale_stat_month_report_2_sub.aspx?year="+year + "&month="+ month; 
    else
        window.open("sale_stat_month_report_2_sub.aspx?year="+year + "&month="+ month,"down","_blank");
    }
        catch(e){ alert(e);}
}

function ViewElement(id)
{
    if(document.getElementById(id).style.display=='')
        document.getElementById(id).style.display  =   'none';
    else
        document.getElementById(id).style.display  =   '';
    
}

//-----------------------------------------------------------------------------------
function SetIframeSize(iframeName)
//-----------------------------------------------------------------------------------
    {
        var iframe = document.getElementById(iframeName);
        try{
            var bHeight = iframe.contentWindow.document.body.scrollHeight;
            var dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
            var bWidth = iframe.contentWindow.document.body.scrollWidth;
            var dWidth = iframe.contentWindow.document.documentElement.scrollWidth;
            var height = Math.max(bHeight, dHeight);
            var width = Math.max(bWidth, dWidth);
            //alert(width);
            iframe.height = height + 40;
            if(width < 800)
                width = '100%';
            iframe.width = width;
        }
        catch(e){}    
}



//-----------------------------------------------------------------------------------
function SetIframeSize2(iframeName, removeHeight)
//-----------------------------------------------------------------------------------
    {
        var iframe = document.getElementById(iframeName);
        iframe.height= 1000;
        //alert(window.document.body.offsetHeight);
        try{
            var bHeight = window.document.body.clientHeight;
            var dHeight = window.document.body.clientHeight;
            var bWidth = iframe.contentWindow.document.body.clientWidth;
            var dWidth = iframe.contentWindow.document.documentElement.clientWidth;
            var height = Math.max(bHeight, dHeight);
            var width = Math.max(bWidth, dWidth);
           
            iframe.height = height- removeHeight;//window.screen.availHeight;//height -removeHeight;
            //alert(iframe.height);
            if(width < 800)
                width = '100%';
            iframe.width = width;
        }
        catch(e){alert(e);}    
}



//-----------------------------------------------------------------------------------
function MergeCellsVertical(tbl, cellIndex) {
//-----------------------------------------------------------------------------------
    if (tbl.rows.length < 2) return;
    var i, j;
    var last = tbl.rows[0].cells[cellIndex].innerHTML;
    var lastIndex = 0;
    for (i = 1; i < tbl.rows.length; i++) {
        if (tbl.rows[i].cells[cellIndex].innerHTML != last)	// 发现新的值
        {
            if (i > lastIndex + 1) {
                for (j = lastIndex + 1; j < i; j++) {
                    tbl.rows[j].cells[cellIndex].innerHTML = "";
                    tbl.rows[j].removeChild(tbl.rows[j].cells[cellIndex]);
                }
                tbl.rows[lastIndex].cells[cellIndex].rowSpan = i - lastIndex;
            }
            last = tbl.rows[i].cells[cellIndex].innerHTML;
            lastIndex = i;
        }
    }
    // 最后一行要特别处理
    if (lastIndex != tbl.rows.length - 1) {
        for (j = lastIndex + 1; j < tbl.rows.length; j++) {
            tbl.rows[j].cells[cellIndex].innerHTML = "";
            tbl.rows[j].removeChild(tbl.rows[j].cells[cellIndex]);
        }
        tbl.rows[lastIndex].cells[cellIndex].rowSpan = tbl.rows.length - lastIndex + 1;
    }
}


//-----------------------------------------------------------------------------------
function SendEmailToEbayUser(the, order_code)
//-----------------------------------------------------------------------------------
{
    if(confirm('are you sure')){
        the.className = "send_email_tag1";
        window.open("/q_admin/orders_ebay_send_email.aspx?order_code="+ order_code, "ebay_send_email", "scrollbars=yes, left=100,top=100, height=200,width=300");
    }  
}


//------------------------------------------------------------------------------------------
function showLoading()
//------------------------------------------------------------------------------------------
{
	$('#page_loading').css("display", "");
}



//------------------------------------------------------------------------------------------
function closeLoading(){$('#page_loading').css("display", "none");}
//------------------------------------------------------------------------------------------

