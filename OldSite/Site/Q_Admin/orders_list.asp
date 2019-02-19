<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Orders List</title>
    <script type="text/javascript" src="/q_admin/js/winOpen.js"></script>
    <script type="text/javascript" src="/q_admin/js/helper.js"></script>
    <script src="/js_css/jquery_lab/jquery-1.3.2.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.tools.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.expose.1.0.5.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.core.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/ui.draggable.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.pager.js" type="text/javascript"></script>
    <link href="/js_css/pager.css" rel="stylesheet" type="text/css" />
    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
    <link href="/App_Themes/default/admin.css" type="text/css" rel="stylesheet" />

    <style type="text/css">
        .tdOver{ background-color:#e0e0e0;}
        .tdOut { background-color:#ffffff;}
    </style>
</head>
<body>
    <div id="pager"></div>
    <div id="order_list" style="clear:both;padding-top:1px;">
        <div style="line-height:30px; text-align:center; padding:5em;">Loading...</div>
    </div>
    
<div id='editArea' style="display:none; width: 400px; min-height: 150px; height:auto; background:#ffffff;position:absolute;z-index:10000;border:1px solid #ccc;"> 
    <b id='expose_sku'></b>
   
    <div style="text-align:right"><input type='button' onclick="$('#editArea').css('display','none');" value="Close 1"></div>
    <hr size="1" />
    <table>
            <tr>
                    <td>Order ID:</td><td id='td_curr_order_id'></td>
            </tr>
            <tr>
                    <td>Order Code:</td><td id='td_curr_order_code'></td>
            </tr>
            <tr>
                    <td>BK STAT:</td><td><select name="bk_stat"></select></td>
            </tr>
            <tr>
                    <td>FRT STAT:</td><td><select name="frt_stat"></select></td>
            </tr>
            <tr style="display:none;">
                    <td>Note:</td><td><input type="hidden" id="current_id" /><input type="text" id="ipt_note"/></td>
            </tr>
    </table>
    <hr size='1' />
    <div style="text-align:right"><input type='button' value='Save' onclick="saveStatus($(this));" /><span style='color:red;' id='save_result'></span></div>
	
</div>
<script type="text/javascript">
var pagerCount = 100;
var currPager = 1;

$().ready(function(){
    
    LoadList();
    $('#editArea').draggable();
    
    $("#pager").pager({ pagenumber: 1, pagecount: 1, buttonClickCallback: PageClick });
});


//
// load list db.
//
function LoadListSplit(data)
{
//    var json = {"options":"[{\"text\":\"TESt1\",\"value\":\"9\"},{\"text\":\"TEST2\",\"value\":\"10\"},{\"text\":\"TEst3\",\"value\":\"13\"}]"} 
//    json = eval(json.options)
//    var json = $.toJSON(data);//eval("("+ data +")");
//    json = $.evalJSON(json.options);
//   

    //data = {"options": data};
   // data = eval(data.options);
    data = eval(data);
    var html = "<table id=\"tab_order_list\" cellspacing='0' cellpadding=\"3\" id='tab_order_list' width='100%'>";
    html += "<tr id='tr'>";
    html += "   <td></td><td></td><td> ID</td><td> ORDER# </td><td>Ship Date</td><td>DATE</td><td>AMNT$</td><td>BALANCE$</td>";
    html += "   <td></td><td>PAY</td><td>NAME</td><td>BK STATUS</td><td>Fre STATUS</td><td>PAID</td><td title='assigned to'>assi to</td><td>source</td>";
    html += "</tr>";
    for(var i=0; i<data.length; i++)
    {
        //alert(json[i].customer_serial_no+" " + json[i].customer_serial_no)
        html += "<tr id='o_"+ data[i].order_helper_serial_no +"'>";
        html += "   <td> "+ getE(data[i].order_code,data[i].order_source ) +" </td>";
        html += "   <td name='up_btn' id='"+data[i].order_helper_serial_no+"' tag='"+data[i].order_code+"'> "+ getUHref()+" </td>";
        html += "   <td>"+ getOrderDetail(data[i].order_code,data[i].order_source ,data[i].order_invoice, false, data[i].order_date) +"</td>";        
        html += "   <td>";
        html += "           "+ getOrderDetail(data[i].order_code,data[i].order_source ,data[i].order_invoice, true, data[i].order_date);
        html += "   </td>";
        html += "   <td>"+ data[i].shipping_date +"</td>";
        html += "   <td>"+ data[i].order_date +"</td>";
        html += "   <td name='td_group_total'>$"+ data[i].grand_total +"</td>";
        html += "   <td name='td_balance'>$"+ data[i].balance +"</td>";
        html += "   <td name='td_price_unit'>"+ data[i].price_unit +"</td>";
        html += "   <td nowrap='nowrap' style='width:180px; overflow:hidden;'>"+ data[i].pay_method +"</td>";
        html += "   <td nowrap='nowrap' style='width:180px; overflow:hidden;'>"+ getNameHref(data[i].customer_serial_no, data[i].name) +"</td>";
        html += "   <td name='bak_name' tag='"+ data[i].out_status +"'>"+ data[i].facture_state_name +"</td>";
        html += "   <td name='fre_name' tag='"+ data[i].pre_status_serial_no +"' style='background:"+data[i].pre_back_color+"'>"+ data[i].pre_status_name +"</td>";
        html += "   <td>"+ getPaidStatusString(data[i].order_pay_status_id, data[i].order_code) +"</td>";
        html += "   <td>"+ data[i].assigned_to_staff_name +"</td>";
        html += "   <td>"+ getOrderSourceName(data[i].order_source) +"</td>";
        html += "</tr>";
        if(data[i].out_note.length>0)
        {
            html += "<tr><td colspan='16' name='td_note'>"+ data[i].out_note +"</td>";
        }
    }
    if(data.length <1)
        html += "<tr><td colspan='16' name='td_note' style='padding:10em; text-align:center;'>No Match Data.</td>";
    else
    {
        getPageCount();
    }
    //$('#order_list').html(html);
    $('#order_list').html(html);
    
    //
    // initial Style
    initListStyle();
}

function LoadList()
{
    //alert(currPager);
    $.getJSON("/q_admin/orders_cmd.aspx?cmd=getOrderList&PageID="+ currPager +"&searchIndex=<%=request("searchIndex") %>&PayMethodID=<%=request("PayMethodID") %>&order_source=<%= request("order_source") %>&out_status=<%=request("out_status") %>&keyword=<%=request("keyword") %>&field_name=<%=request("field_name") %>&d=" + rnd()
        , function(msg){LoadListSplit(msg);});
}

function getPageCount(){
    pagerCount = 100;
    $("#pager").pager({ pagenumber: currPager, pagecount: pagerCount, buttonClickCallback: PageClick });
}

//
// pager Click Event.
//
PageClick = function(pageclickednumber) {
            currPager = pageclickednumber;
            $('#order_list').html("<div style=\"line-height:30px; text-align:center; padding:5em;\">Loading...</div>");
            LoadList();            
            $("#pager").pager({ pagenumber: pageclickednumber, pagecount: pagerCount, buttonClickCallback: PageClick });
            //$("#result").html("Clicked Page " + pageclickednumber);
}


//
// init list style.
//
function initListStyle()
{
    $('#tr').find('td').css({"font-weight":"bold"
                    , "text-align":"center"
                    , "background":"#DAB5A2"
                    });
    var bgc = "";       //  Row Background.
    var statusBgc = ""; //  pre status Background
    
    $('#tab_order_list tr').hover(
                    function(e){
                        bgc = $(this).find('td').css("background");
                        
                        $(this).find('td').each(function(){
                            if($(this).attr('name')== 'fre_name')
                                statusBgc = $(this).css("background");
                        });
                        $(this).find('td').css({"background":"#e0e0e0"});
                      
                    }
                    , function(e){
                        //alert($(e).html());
                        $(this).find('td').css({"background":bgc});
                        $(this).find('td').each(function(){
                            if($(this).attr('name')== 'fre_name')
                                 $(this).css("background",statusBgc);
                        });
                        
                        
                    }).each(function(i){
                        
                        if(i%2==0)
                        {
                            if($(this).attr('id') != 'tr')
                            {
                                $(this).find('td').each(function(){
                                    if($(this).attr('name') != 'fre_name')
                                        $(this).css({"background":"#f2f2f2"});
                                 });
                            }
                        }
                        $(this).find('td').css({"border-bottom":"1px solid #ccc"});
                    });
    $('td[name=td_group_total]').css({"text-align":"right"});
    $('td[name=td_balance]').css({"text-align":"right"});
    $('td[name=td_price_unit]').each(function(){
        if($(this).html()=="USD")
            $(this).css("color","blue");
    });
}

//
//
//
function getE(order_code, order_source)
{
    if(order_source != 3 && order_code.length>2)
    {
        return "<a href=\"orders_edit_detail_selected.aspx?menu_id=2&order_code=" + order_code + "\">E</a>"
        // return "<a href=\"orders_edit_detail.aspx?menu_id=2&order_code=" + order_code + "\">E</a>"
    }
    else
        return "E";
}

//
// get Order Detail href
//
function getOrderDetail(order_code, order_source, order_invoice, is_show_order_code, order_date)
{
    if(order_date.indexOf('2009')!=-1)
        return "";
        
    if(!is_show_order_code)
    {
        if(order_source != 3)
            return "<span onclick=\"OpenOrderDetail('" + order_code + "')\" style=\"cursor:pointer\">"+ order_invoice +"</span>";
        else
            return "<span onclick=\"winOpen('orders_ebay_view.aspx?sales_record_number=" + order_invoice + "', 'ebay_view', 720, 700, 120, 200);\" style=\"cursor:pointer\">"+ order_invoice +"</span>";
    }
    else
    {
        if(order_source != 3)
            return "<span onclick=\"OpenOrderDetail('" + order_code + "')\" style=\"cursor:pointer\">"+ order_code +"</span>";
        else
            return "<span onclick=\"winOpen('orders_ebay_view.aspx?sales_record_number=" + order_invoice + "', 'ebay_view', 720, 700, 120, 200);\" style=\"cursor:pointer\">"+ order_code +"</span>";
    }
}
//
// get Name Href String.
//
function getNameHref(customer_serial_no, name){
    return "<a href='#' onclick=\"winOpen('sales_customer_history.aspx?customer_id="+ customer_serial_no +"','order_history', 1000, 600, 300, 300);return false;\">"+name+"</a>";
}

//
//  get Paid PNG
//
function getPaidStatusString(order_pay_status_id, order_code)
{
    if(order_pay_status_id == 1)
        return "";
    if (order_pay_status_id == 2)
        return "<span style=\"width: 16px; height: 16px; background: url('/soft_img/tags/(15,47).png'); \">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
    if (order_pay_status_id == 3)
        return "<span style=\"width: 16px; height: 16px; background: url('/soft_img/tags/(31,36).png'); \">&nbsp;&nbsp;&nbsp;&nbsp;</span>";
    if (order_pay_status_id == 4)
        return "<span onclick=\"winOpen('order_paypal_error_info.aspx?order_code=" + order_code + "', 'paypal_error', 420, 400, 120, 200);\" style=\"cursor : pointer; width: 16px; height: 16px; background: url('/soft_img/tags/(14,45).png'); \">&nbsp;&nbsp;&nbsp;&nbsp;</span>";

    return "";
}
//
// get Order Source name.
//
function getOrderSourceName(order_source)
{
    if(order_source == 1)
        return "WebSite";
    if(order_source == 2)
        return "Input";
    if(order_source == 3)
        return "eBay";
    return "None";
}

//
// get U Href
//
function getUHref()
{
    return "<a href='#' onclick=\"clickUButton($(this));return false;\">U</a>";
}
//
// click U 
// 
function clickUButton(e)
{
    //$(this).offset().top
    var orderID = '';
    var orderCode = "";
    var orderFriID = "";    //  FRT STAT
    var orderBakID = "";    //  out status 
    
    e.parent().parent().find('td').each(function(){
        if($(this).attr('name') =='up_btn')
        {
            orderID = $(this).attr('id');
            orderCode = $(this).attr('tag');
        }
        if($(this).attr('name') =="fre_name")
        {
            orderFriID = $(this).attr('tag');
        }
        if($(this).attr('name') =="bak_name")
        {
            orderBakID = $(this).attr('tag');
        }
    });
    $('#td_curr_order_code').html(orderCode).css("font-weight","bold");
    $('#td_curr_order_id').html(orderID).css("font-weight","bold");
    $('select[name=bk_stat]').html("<option>Loading...</option>");
    $('select[name=frt_stat]').html("<option>Loading...</option>");

    getFriStat(orderFriID);
    getBKStat(orderBakID);
    $('#editArea').css({'top': "100px", 'left':'200px', 'display':''});
}

//
// get 
//
function getFriStat(currID)
{
    $.ajax({
        url:"orders_cmd.aspx"
        ,type:"get"
        ,data:{"cmd":"get_frt_stat", "currentID":currID}
        ,success:function(msg){
            if(msg.indexOf("error:")!= -1)
                alert(msg);
            else
                $('select[name=frt_stat]').html(msg);
        }
        ,error:function(msg){alert(msg);}
    });
}

//
// get 
//
function getBKStat(currID)
{
    $.ajax({
        url:"orders_cmd.aspx"
        ,type:"get"
        ,data:{"cmd":"get_bak_stat", "currentID":currID}
        ,success:function(msg){
            if(msg.indexOf("error:")!= -1)
                alert(msg);
            else
            {
                //alert(currID);
                $('select[name=bk_stat]').html(msg);
                //alert(msg);
            }
        }
        ,error:function(msg){alert(msg);}
    });
}
//
// Save Order Status
//
function saveStatus(e)
{
    var bk_stat = $('select[name=bk_stat]').val();
    var frt_stat = $('select[name=frt_stat]').val();
    var oid = $('#td_curr_order_id').html();
    var bk_name = "";
    var frt_name = "";
    var frt_bgC = "";
    $.ajax({
        url:"orders_cmd.aspx"
        ,type:"get"
        ,data:{"cmd":"save_order_status", "OrderID":oid, "bk_stat":bk_stat,"frt_stat":frt_stat }
        ,success:function(msg){
            if(msg.indexOf("error:")!= -1)
            {
                
                alert(msg);
            }
            else
            {
                frt_stat = msg.split("|")[0];
                $('select[name=frt_stat]').find('option').each(function(){
                    if(parseInt($(this).attr("value")) == parseInt(frt_stat))
                    {
                       frt_name = $(this).html();   
                       frt_bgC  = $(this).css('background');             
                    }
                });
                bk_stat = msg.split("|")[1];
                $('select[name=bk_stat]').find('option').each(function(){
                    if(parseInt($(this).attr("value")) == parseInt(bk_stat))
                    {
                       bk_name  = $(this).html();                       
                    }
                   // frt_stat  = frt_stat + '|'+ (parseInt($(this).attr("value")) +"|"+ parseInt(bk_stat)) + "\r\n";
                });
               /// alert(frt_stat);
                //alert(bk_stat);
                
                $('#o_'+ oid).find('td').each(function(){
                    if($(this).attr('name') == "bak_name")
                    {
                        $(this).html(bk_name);
                        $(this).attr('tag',bk_stat);
                    }
                    if($(this).attr('name') == "fre_name")
                    {
                        $(this).css('background',frt_bgC);
                        $(this).html(frt_name);
                        $(this).attr('tag',frt_stat);
                        
                        $('#editArea').css('display','none');
                    }
                    
                });
            }
        }
        ,error:function(msg){alert(msg);}
    });
}
</script>

</body>
</html>
