<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>ETC Ebay System Href</title>
	<script type="text/javascript" src="../../js_css/jquery_lab/jquery-1.3.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="../../js_css/b_lu.css" />
    <script src="/Q_admin/JS/helper.js" type="text/javascript"></script>
    <script type="text/javascript" src="../js/winOpen.js"></script>
    <style>
		.div_top div{ float:left; background:#ccc; line-height: 20px;}
	</style>
	<script type="text/javascript">

	    function GetEbayPrice(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {
	        $('#modifyEbayPrice' + luc_sku).html("<a id='modifyEbayPriceHref" + luc_sku + "' href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + Cost + "&Profit=" + Profit + "&eBayFee=" + eBayFee + "&ShippingFee=" + ShippingFee + "&Price=" + price + "&IsDesc=0&onlyprice=1&itemid=" + itemid + "&issystem=1' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200); $(this).css({'color':'white', 'background':'black'});}return false;\">eBay Price </a>");
	    }
	    function GetEbayDesc(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {
	        $('#modifyEbayDesc' + luc_sku).html("<a id='modifyEbayDescHref" + luc_sku + "' href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + Cost + "&Profit=" + Profit + "&eBayFee=" + eBayFee + "&ShippingFee=" + ShippingFee + "&Price=" + price + "&IsDesc=1&onlyprice=0&itemid=" + itemid + "&issystem=1' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200);  $(this).css({'color':'white', 'background':'black'});}return false;\">eBay Desc </a>");
	    }
    
    function WarnShow(w,the){
        if(w=='True'){
            the.html("<img src='/soft_img/tags/(38,41).png' title='part disabled'>");
        }
//        else
//            the.html("n");
    }

    function Sleep(obj, iMinSecond) {
        if (window.eventList == null)
            window.eventList = new Array();
        var ind = -1;
        for (var i = 0; i < window.eventList.length; i++) {
            if (window.eventList[i] == null) {
                window.eventList[i] = obj;
                ind = i;
                break;
            }
        }
        if (ind == -1) {
            ind = window.eventList.length;
            window.eventList[ind] = obj;
        }
        setTimeout("GoOn(" + ind + ")", iMinSecond);
    }

    function GoOn(ind) {
        var obj = window.eventList[ind];
        window.eventList[ind] = null;
        if (obj.NextStep) obj.NextStep();
        else obj();
    }

    // array 
    var InfoArray = new Array();
    for (var i = 0; i < 500; i++) {
        InfoArray[i] = new Array();
    }

    //    function Amount(cost, adjustment, buyItNowPrice, the, the2, itemid, luc_sku, is_shrink) {
// InfoArray["& JsArrayIndex &"][0] = '"& srs("adjustment") &"'; "&vblf
//s =     s & "       InfoArray["& JsArrayIndex &"][1] = '"& sRs("BuyItNowPrice") &"'; "&vblf
//s =     s & "       InfoArray["& JsArrayIndex &"][2] = '"& sRs("ID") &"'; " &vblf 
//s =     s & "       InfoArray["& JsArrayIndex &"][3] = '"& sRS("ItemID") &"'; "&vblf 
//s =     s & "       InfoArray["& JsArrayIndex &"][4] = '"& is_shrink & "'; "&vblf

    function Warn(index) {
       
        var luc_sku = InfoArray[index][2];
      
        var the = $('#newEbayPrice' + luc_sku);
        var the2 = $('#newDiff' + luc_sku);

        the.html("<img src='/soft_img/tags/loading.gif'/>");

        Sleep(this, 50);
        this.NextStep = function () {
            // alert(luc_sku);
            $.ajax({
                type: "Get",
                url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                data: "cmd=GetEBaySysWarn&systemsku=" + luc_sku + "&" + rnd(),
                success: function (msg) {
                    //                   the.html(msg);
                    //                   var diff = (parseFloat(msg) - parseFloat(buyItNowPrice)).toFixed(2);
                    //                   
                    //                   if(diff>0)
                    //                        the2.css('color','green');
                    //                   else 
                    //                        the2.css('color','red');
                    //                   the2.html(diff);

                    var json = eval(msg);
                    $.each(json, function (idx, item) {                       
                        WarnShow(item.warn, $('#warn' + luc_sku));                       
                    });

                    Warn(index + 1);
                }
                , error: function (msg) { Warn(index + 1); the2.html(msg); }
            });
            the.html(".");
        }
    }
    function Amount(index) {
    
        var adjustment      = InfoArray[index][0];
        var buyItNowPrice   = InfoArray[index][1];
        var luc_sku = InfoArray[index][2];
        var itemid = InfoArray[index][3];
        var is_shrink = InfoArray[index][4];
        var the = $('#newEbayPrice' + luc_sku);
        var the2 = $('#newDiff' + luc_sku);

        the.html("<img src='/soft_img/tags/loading.gif'/>");

          Sleep(this,50);
          this.NextStep = function () {
              // alert(luc_sku);
              $.ajax({
                  type: "Get",
                  url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                  data: "cmd=GetSysEbayPriceBySysCost&is_shrink=" + is_shrink + "&Adjustment=" + adjustment + "&systemsku=" + luc_sku + "&" + rnd(),
                  success: function (msg) {
                      //                   the.html(msg);
                      //                   var diff = (parseFloat(msg) - parseFloat(buyItNowPrice)).toFixed(2);
                      //                   
                      //                   if(diff>0)
                      //                        the2.css('color','green');
                      //                   else 
                      //                        the2.css('color','red');
                      //                   the2.html(diff);

                      var json = eval(msg);
                      $.each(json, function (idx, item) {
                          the2.html(msg);
                          var newPrice = parseFloat(item.ebayPrice);
                          var diff = (newPrice - buyItNowPrice).toFixed(2);
                          the.html(newPrice);
                          if (diff > 0)
                              the2.css('color', 'red');
                          else
                              the2.css('color', 'green');
                          the2.html(diff);
                          WarnShow(item.warn, $('#warn' + luc_sku));
                          $('#cost_' + luc_sku).html(item.cost);
                          GetEbayPrice(newPrice, itemid, luc_sku, item.cost, item.profit, item.ebay_fee, item.shipping_fee)
                          GetEbayDesc(newPrice, itemid, luc_sku, item.cost, item.profit, item.ebay_fee, item.shipping_fee)
                      });

                      Amount(index + 1);
                  }
                , error: function (msg) { Amount(index + 1); the2.html(msg); }
              });
          }
      }

      function ChangePrice(index) {

          var luc_sku = InfoArray[index][2];
          var the2 = $('#newDiff' + luc_sku);
          var diff = parseInt(the2.html());
          if (diff < 10 && diff >-5) {
              ChangePrice(index + 1);
              return;
          }
          var href = $('#modifyEbayPriceHref' + luc_sku).attr("href");
          $.ajax({
              type: "Get",
              url: href,
              success: function (msg) {

                  ChangePrice(index + 1);
                  the2.css({ 'color': 'white', 'background': 'black' })
              }
                , error: function (msg) { ChangePrice(index + 1); the2.html(msg); }
          });

      }

      function GenerateXmlFile(index) {
          {

              var adjustment = InfoArray[index][0];
              var buyItNowPrice = InfoArray[index][1];
              var luc_sku = InfoArray[index][2];
              var itemid = InfoArray[index][3];
              var is_shrink = InfoArray[index][4];
            /*
              if (parseInt(luc_sku) < 202000) {
                  GenerateXmlFile(index + 1);
                  return;
              } */
              var the = $('#generateXmlFile' + luc_sku);
              the.html("<img src='/soft_img/tags/loading.gif'/>");

              $.ajax({
                  type: "Get",
                  url: "/q_admin/ebaymaster/Online/get_system_configuration.aspx",
                  data: "cmd=GenerateXmlFile&Version=3&system_sku=" + luc_sku + "&" + rnd(),
                  success: function (msg) {
                      the.html("OK");
                      GenerateXmlFile(index + 1);
                  }
                , error: function (msg) { GenerateXmlFile(index + 1); the.html(msg); }
              });

          }
      }
	</script>
</head>

<body>

<br style="clear:both;" />
<%

	Dim cmdSort	:	cmdSort = SQLescape(request("sort"))
	dim sqlSort : 	sqlSort = ""
	Dim SubSku  :   SubSku  = SQLescape(request("SubSku"))
    Dim is_shrink
    Dim JsArrayIndex : JsArrayIndex = 0
	
	if(cmdSort = "sku") then
		sqlSort = " order by id desc "
	else
		sqlSort = " order by cutom_label, id asc"
	end if
	
    set Rs = Conn.execute("select category_id, category_name from tb_product_category_new where parent_category_id=0 and showit=1")
    If Not Rs.eof Then
            s   = " <ul id=""browser"" class=""filetree"">"  &vblf
            Do While Not Rs.eof 
                        s   = s & "<li><span class=""folder"" id=""treeview_category_id_"& rs("category_id") &""">"& rs("category_name")& "<ul>" &vblf
                        set cRs = conn.execute ( "select category_id, category_name from tb_product_category_new where parent_category_id='"& rs("category_id") &"' and showit=1 order by category_name asc")
                        if not cRs.eof then
                                Do While not cRs.eof
                                        s   =      s & "<li><span class=""folder"" id=""treeview_category_id_"& cRs("category_id") &""" style='font-weight:600;'>"& crs("category_name") &"</span>" & vblf
                                        s   =       s & "       <ul>"

                                                set sRs = conn.execute("select es.id, e.BuyItNowPrice,es.category_id"&_
                                                                        " ,es.cutom_label, es.ebay_system_price"&_
                                                                        " ,e.ItemID, es.adjustment, es.ebay_system_name"&_
                                                                        " ,es.is_barebone, es.is_shrink "&_
                                                                        " ,es.large_pic_name "&_
                                                                        " ,es.logo_filenames "&_
                                                                        " ,es.sub_part_quantity "&_
                                                                        " ,etc.ItemID etc_item_id, etc.ItemTitle etc_item_title, etc.ItemPrice etc_item_price"&_
                                                                        " from tb_ebay_system es "&_
                                                                        " left join tb_ebay_selling e on e.sys_sku=es.id "&_   
                                                                        " inner join tb_ebay_etc_items etc on etc.LUC_eBay_Sys_Sku = es.id "&_                                                                     
                                                                        " where (es.is_from_ebay=0 or (es.is_from_ebay=1 and es.is_shrink=0)) and es.category_id='"& cRs("category_id") &"' "&_
                                                                        " " & sqlSort)
                                                                      
                                                if not sRs.eof then
                                                        s = s & "<li><table width='99%'>"
                                                        s = s & "       <tr>"	&vblf
                                                        s = s & "           <td width='30'></td>"	&vblf
                                                        s = s & "           <td width='100' style='text-align:center;'>System SKU</td>"	&vblf
                                                        s = s & "           <td width='90' style='text-align:center;'>ItemID</td>"	&vblf
                                                        s = s & "           <td width='90' style='text-align:center;'>BuyItNowPrice</td>"	&vblf
                                                        s = s & "           <td width='100' style='text-align:center;'>ETC ItemID</td>"	&vblf
                                                        s = s & "           <td width='90' style='text-align:center;'>ETC Price</td>"	&vblf
                                                        s = s & "           <td style='text-align:center;'>ETC Title</td>"	&vblf
                                                         s = s & "          <td></td>"	&vblf
                                                        s = s & "       </tr>"	&vblf
                                                        do while not srs.eof 
                                                            if srs("is_shrink") = 1 or srs("is_barebone") = 1 then 
                                                                is_shrink = 1 
                                                            else
                                                                is_shrink = 0
                                                            end if
                                                                s =     s & "<tr onmouseover=""this.bgColor='#f2f2f2';"" onmouseout=""this.bgColor='#ffffff';"">"	&vblf      
                                                                s =     s & "   <td> "&vblf
                                                                s =     s & " <a href='http://www.lucomputers.com/ebay/"& srs("logo_filenames") &".jpg' target='_blank'>p</a>"
                                                                if srs("is_barebone") = 1 then 
                                                                    s =     s & "     <span style='color:blue;'> B </span>"&vblf
                                                                end if
                                                                s =     s & "   </td> "&vblf                                                               
                                                                s =     s & "   <td> "&vblf
                                                                s =     s & "     <span class=""file"" ><a href='/q_admin/ebayMaster/lu/ebay_system_edit_2.asp?IsParent=false&ebay_system_sku="&sRs("id") &"&category_id="&sRs("category_id") &"&cmd=modify' onclick=""return js_callpage_cus(this.href, 'view_page', 1050, 750);"">"& sRs("id") &" ("& sRs("sub_part_quantity") &")</a></td>" &vblf
                                                                s =     s & "   <td><a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="&sRs("ItemID")&"' target='_blank'>"& srs("ItemID") & "</a></td>"	&vblf
																s =     s & "   <td style='text-align:right;'>"& srs("ebay_system_price") &"</td>"   &vblf
																s =     s & "   <td style='text-align:center;'><a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="&sRs("etc_item_id")&"' target='_blank'>"& srs("etc_item_id") & "</a></td>"	&vblf
																s =     s & "   <td style='text-align:right;padding-right:10px;'>"
																if cdbl(srs("etc_item_price")) < cdbl(srs("ebay_system_price")) then 
                                                                    s =     s & "<span style='color:red;'>"& srs("etc_item_price") &"</span>"&vblf
                                                                else
                                                                    s =     s & "<span >"& srs("etc_item_price") &"</span>"&vblf
                                                                end if
																s =     s & " </td>"   &vblf	
                                                                s =     s & " <td>"& srs("etc_item_title") &"</td>"&vblf
                                                                s =     s & " <td class='noteText'></td>"&vblf											
                                                                s =     s & "</tr>"	&vblf
																
                                                                JsArrayIndex = JsArrayIndex + 1 
                                                        srs.movenext
                                                        loop
                                                        s =     s & "</table></li>" &vblf
                                                end if
                                                srs.close : set sRs = nothing
                                        s   =       s & "       </ul> "&vblf &_
                                                            "   </li>"
                                cRs.movenext
                                loop 
                        end if
                        cRs.close : set cRs = nothing
                        s   =       s &"</ul></li>"
            Rs.movenext
            Loop
            s   =    s &                      "</ul>"
    End If


	response.write s
    ' Response.write "<script>Amount(0);</script>"
CloseConn()
%>
<script type="text/javascript">
	function SubmitRadio()
	{
		$('input[type=radio]').each(function(){
			if($(this).attr("checked"))
				window.location.href="ebay_system_href_list.asp?sort=" + $(this).val();
		});
	}
	
	function setRadio(str)
	{
		$('input[name=sort]').each(function(){
			if($(this).val() == str)
				$(this).attr("checked", "checked");
			
		});
	}

	setRadio('<%= cmdSort %>');
</script>
</body>
</html>
