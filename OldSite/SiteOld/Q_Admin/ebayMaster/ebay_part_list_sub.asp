<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ebay part list</title>
    <script src="/js_css/jquery-1.9.1.js"></script>
   <!-- <script type="text/javascript" src="../JS/lib/jquery-1.3.2.min.js"></script>-->
    <link rel="stylesheet" type="text/css" href="/js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="/js_css/b_lu.css" />

    <script src="../JS/WinOpen.js" type="text/javascript"></script>
    <script type="text/javascript">
        var itemids = new Array();
        var modifyShippingItems = new Array();
        var modifyPriceEndItems = new Array();

        function modifyDesc(mfpName) {
            itemids = new Array();
            var n = 0;
            if (mfpName == "" || mfpName == null) {
                $('a').each(function () {
                    var the = $(this);
                    if (the.attr("tag") != undefined) {
                        if (the.attr("tag") == "desc") {
                            itemids[n] = $(this).attr("title");
                            n += 1;

                        }
                    }
                });
            }
            else {
                $('a').each(function () {
                    if ($(this).attr('title') == mfpName) {
                        itemids[n] = $(this).attr("tag");
                        n += 1;
                    }
                });
            }
            if (itemids.length > 0) {
                //alert(itemids.length);
                modifyDescToEbay(0)
            }
            else
                alert("NO");
        }

        function modifyDescToEbay(index) {

            if (index >= itemids.length)
                return;
            var the = $('a[title=' + itemids[index] + ']');
            var href = the.attr('href');
            the.css('color', 'red');
            $.ajax({
                type: "get",
                url: href,
                data: "",
                success: function (msg) {
                    the.css('color', 'green');
                    modifyDescToEbay(index + 1);
                },
                error: function (msg) {
                    the.html(msg);
                    the.css('color', 'back');
                    modifyDescToEbay(index + 1);
                }
            });
        }

        function modifyShipping() {
            var n = 0;
            $('.ModifyShippingFee').each(function () {
                modifyShippingItems[n] = $(this).attr('href');
                n += 1;
            });
            alert(modifyShippingItems.length);
            if (modifyShippingItems.length > 0)
                modifyShippingToEbay(0);
        }

        function modifyShippingToEbay(index) {
            if (index >= modifyShippingItems.length)
                return;
            $('#changeQtyArea', parent.document).html(index + "/" + modifyShippingItems.length);

            var the =  $('.ModifyShippingFee').eq(index);
            var href = the.attr('href');
            the.css('color', 'red');
            var itemid = the.attr("tag");

            //            $.ajax({
            //                type: "get",
            //                url: "/q_admin/ebaymaster/online/modifyonlineprice.aspx?cmd=modifyItemSpecific&itemid=" + itemid,
            //                data: "",
            //                success: function (msg) {
            //                    
            //                },
            //                error: function (msg) {
            //                    the.html(msg);                  
            //                }
            //            });

            $.ajax({
                type: "get",
                url: href,
                data: "",
                success: function (msg) {
                    the.css('color', 'green');
                    modifyShippingToEbay(index + 1);
                },
                error: function (msg) {
                    the.html(msg);
                    the.css('color', 'back');
                    modifyShippingToEbay(index + 1);
                }
            });
        }



        function GetEbayPrice(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {
            $('#modifyEbayPrice' + luc_sku).html("<a href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + Cost + "&Profit=" + Profit + "&eBayFee=" + eBayFee + "&ShippingFee=" + ShippingFee + "&Price=" + price + "&IsDesc=0&onlyprice=1&itemid=" + itemid + "&issystem=0' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200); $(this).css({'color':'white', 'background':'black'});}return false;\"> Price </a>");
        }
        function GetEbayDesc(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {
            $('#modifyEbayDesc' + luc_sku).html("<a title='" + itemid + "' href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=" + Cost + "&Profit=" + Profit + "&eBayFee=" + eBayFee + "&ShippingFee=" + ShippingFee + "&Price=" + price + "&IsDesc=1&onlyprice=0&itemid=" + itemid + "&issystem=0' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200);  $(this).css({'color':'white', 'background':'black'});}return false;\" tag='desc'> Desc </a>");
        }
        function GetEbayDescPrice(price, itemid, luc_sku, Cost, Profit, eBayFee, ShippingFee) {

            $('#modifyEbayDescPrice' + luc_sku).html("<a href='/q_admin/ebayMaster/ebay_system_cmd.aspx?cmd=ModifyPriceToWeb&sell=" + price + "&luc_sku=" + luc_sku + "' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_" + luc_sku + "', 300, 200);  $(this).css({'color':'white', 'background':'black'});}return false;\"> Price To Web </a>");
            //$('#modifyEbayDescPrice'+ luc_sku).html("<a href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost="+Cost+"&Profit="+Profit+"&eBayFee="+eBayFee+"&ShippingFee="+ShippingFee+"&Price="+ price +"&IsDesc=0&onlyprice=0&itemid="+ itemid +"&issystem=0' target='_blank' onclick=\"if(confirm('are you sure?')){js_callpage_cus(this.href, 'ebay_"+luc_sku+"', 300, 200);  $(this).css({'color':'white', 'background':'black'});}return false;\"> Desc&Price </a>");
        }
        function GetNewBuyItNow(the) {

            var luc_sku = the.attr("luc_sku");
            var cost = the.attr("cost");
            var screen = the.attr("screen");
            var adjustment = the.attr("adjustment");
            var itemid = the.attr('itemid');
            $.ajax({
                type: "get",
                url: "/q_admin/ebayMaster/ebay_notebook_get_ebayPrice.aspx",
                data: "Cost=" + cost + "&Screen=" + screen + "&Adjustment=" + adjustment + "&LUC_Sku=" + luc_sku,
                success: function (msg) {
                    //                var oldPrice = parseFloat($('#oldPrice'+ luc_sku).html()).toFixed(2);
                    //                var newPrice = parseFloat(msg).toFixed(2);
                    //                var diff = (newPrice - oldPrice).toFixed(2);
                    //                
                    //                $('#BuyItNowPriceNew'+ luc_sku).html(msg);
                    //                $('#diff'+ luc_sku).html(diff);
                    //                if(diff>0)
                    //                    $('#diff'+ luc_sku).css('color','red');
                    //                else
                    //                    $('#diff'+ luc_sku).css('color','green');
                    var json = eval(msg);
                    $.each(json, function (idx, item) {
                        var oldPrice = parseFloat($('#oldPrice' + luc_sku).html()).toFixed(2);
                        var newPrice = parseFloat(item.ebayPrice);
                        var diff = (newPrice - oldPrice).toFixed(2);

                        $('#BuyItNowPriceNew' + luc_sku).html(newPrice);
                        $('#diff' + luc_sku).html(diff);
                        if (diff > 0)
                            $('#diff' + luc_sku).css('color', 'red');
                        else
                            $('#diff' + luc_sku).css('color', 'green');

                        GetEbayPrice(newPrice, itemid, luc_sku, cost, item.profit, item.ebay_fee, item.shipping_fee);
                        GetEbayDesc(newPrice, itemid, luc_sku, cost, item.profit, item.ebay_fee, item.shipping_fee);
                        GetEbayDescPrice(newPrice, itemid, luc_sku, cost, item.profit, item.ebay_fee, item.shipping_fee);

                        var autoPayHref = $('#a' + itemid).attr('href');
                        $('#a' + itemid).attr('href', autoPayHref + "&price=" + newPrice);
                    });

                },
                error: function (msg) { $('#BuyItNowPriceNew' + luc_sku).html(msg); }
            });
        }



        function modifyALLPriceToEBay() {
            modifyPriceEndItems = new Array();
            var index = 0;
            $('td[title=diff]').each(function () {
                var the = $(this);

                if (the.parent().next().next().attr("name") != "ebay_note") {
                    if (parseFloat(the.html()) != 0) {

                        var IsEnd = parseFloat(the.prev().prev().prev().prev().html()) < 1 || isNaN(parseFloat(the.prev().prev().prev().prev().html()))

                        if (parseFloat(the.prev().prev().prev().prev().html()) > 2
                            || (parseFloat(the.html()) > 0 && !IsEnd)) {
                            var a = the.next().next().next().children(0); //.attr("href");
                            //modifyALLPriceToEBaySub(a);

                            modifyPriceEndItems[index] = a;//.attr("id");
                            index += 1;
                        }
                        if (IsEnd) {
                            var a = the.next().next().next().next().next().next().children(0);
                            //modifyALLPriceToEBaySub(a);
                            modifyPriceEndItems[index] = a;//.attr("id");
                            index += 1;
                        }
                    }
                    else {
                        var IsEnd = parseFloat(the.prev().prev().prev().prev().html()) < 1 || isNaN(parseFloat(the.prev().prev().prev().prev().html()))
                        if (IsEnd) {
                            var a = the.next().next().next().next().next().next().children(0);
                            //modifyALLPriceToEBaySub(a);
                            modifyPriceEndItems[index] = a;//.attr("id");
                            index += 1;
                        }
                    }
                }
            });

            runModifyAllPrice(0);
        }

        function runModifyAllPrice(index) {
            modifyALLPriceToEBaySub(index);
        }

        function modifyALLPriceToEBaySub(index) {
            if (index > modifyPriceEndItems.length)
                return;

            var a = modifyPriceEndItems[index];

            $('#changeQtyArea', parent.document).html(index + "/" + modifyPriceEndItems.length);



            a.css({ "color": "white", "background": "green" });
            $.ajax({
                type: "get",
                url: a.attr("href"),
                data: "",
                success: function (msg) {
                    a.css({ "color": "white", "background": "#000000" });
                    modifyALLPriceToEBaySub(index + 1);
                },
                error: function (msg) {
                    a.html(msg);
                    a.css({ "color": "white", "background": "red" });
                    modifyALLPriceToEBaySub(index + 1);
                }
            });
        }

        function copyLogoToTmp() {
            //title = 'diff'
            $('td[title=diff]').each(function () {
                var the = $(this);
                var sku = $(this).attr("id").replace("diff", "");
                the.css({ "color": "white", "background": "green" });
                $.ajax({
                    type: "get",
                    url: "copyLogoToFolder.aspx?sku=" + sku,
                    data: "",
                    success: function (msg) {
                        the.css({ "color": "white", "background": "#000000" });
                    },
                    error: function (msg) {
                        the.html(sku);
                        the.css({ "color": "white", "background": "#000000" });
                    }
                });
            });

        }

        function uploadLogoToEbay(index) {
            //title = 'diff'
            $('a[title=Modify Logo]').each(function (i) {

                if (index > i)
                    return;

                if (index == i) {
                    var the = $(this);
                    the.css({ "color": "white", "background": "green" });

                    $.ajax({
                        type: "get",
                        url: the.attr("href"),
                        data: "",
                        success: function (msg) {
                            the.css({ "color": "white", "background": "#000000" });

                            uploadLogoToEbay(index + 1);
                        },
                        error: function (msg) {
                            the.html(sku);
                            the.css({ "color": "white", "background": "red" });
                            uploadLogoToEbay(index + 1);
                        }
                    });

                }
            });

        }

        function uploadAutoPayToEbay(index) {
            $('a[title=modify auto pay]').each(function (i) {
                if (index > i)
                    return;
                if (index == i) {
                    var the = $(this);
                    the.css({ "color": "white", "background": "green" });

                    $.ajax({
                        type: "get",
                        url: the.attr("href"),
                        data: "",
                        success: function (msg) {
                            the.css({ "color": "white", "background": "#000000" });

                            uploadAutoPayToEbay(index + 1);
                        },
                        error: function (msg) {
                            the.html(sku);
                            the.css({ "color": "white", "background": "red" });
                            uploadAutoPayToEbay(index + 1);
                        }
                    });
                }
            });
        }

    </script>
</head>
<body style="padding: 20px; font-size: 10pt; line-height: 20px;">
    <input type="button" onclick="uploadAutoPayToEbay(0);" value="upload Auto Pay to eBay" />
    <input type="button" onclick="uploadLogoToEbay(0);" value="upload logo to eBay" />
    <input type="button" onclick="copyLogoToTmp();" value="Copy logo to temp folder" />
    <input type="button" onclick="modifyShipping();" value="Modify Shipping On eBay.ca" />
    <input type="button" onclick="modifyDesc();" value="Modify Desc On eBay.ca" />
    <input type="button" onclick="modifyALLPriceToEBay();" value="Modify ALL price to eBay.ca" />
    <span id="warn_comment"></span>
    <%
 
        Dim keyword, sql_type, part_type, part_status, category_id, online
		Dim sql_keyword	:	sql_keyword	=	""
		Dim sql_online  :   sql_online  =   ""
        Dim sql_cate    :   sql_cate    =   ""
		Dim colorString 
        Dim bgColorString 
        Dim viewAllCate 
        Dim NoNotebook
        Dim WarnQty     
        Dim token
       
        WarnQty = 0
        keyword         =   SQLescape(request("keyword"))
        sql_type        =   SQLescape(request("sql_type"))
        part_type       =   SQLescape(request("part_type"))
        part_status     =   SQLescape(request("part_status"))
        category_id     =   SQLescape(request("category_id"))
        online          =   SQLescape(request("online"))
        viewAllCate     =   SQLescape(request("viewAllCate"))
        NoNotebook      =   SQLescape(request("NoNotebook"))
        token           =   SQLescape(request("token"))
		
		' 模糊
		
		if category_id <> "" then 
			if sql_type = "1" then 
					if part_type = "Comment" then 
						sql_keyword =	sql_keyword & " and e.ebay_comment like '%"& keyword &"%'"
					else
						sql_keyword =	sql_keyword & " and e.part_sku like '%"& keyword &"%'"
					end if
					
			else
				if part_type = "Comment" then 
						sql_keyword =	sql_keyword & " and e.ebay_comment = '"& keyword &"'"
					else
						sql_keyword =	sql_keyword & " and e.part_sku = '"& keyword &"'"
					end if
			end if
			
			if part_status = "Show" then
				sql_keyword	=	sql_keyword & " and e.showit=1 "
			end if
			if part_status = "Hidde" then
				sql_keyword	=	sql_keyword & " and e.showit=0 "
			end if
			if online = "true" then 
			    sql_online  =   " inner join (Select luc_sku, itemid,QuantityAvailable, BuyItNowPrice from tb_ebay_selling ) p_online on p_online.luc_sku=p.product_serial_no "
			else
			    sql_online  =   " inner join (Select luc_sku, itemid,QuantityAvailable, BuyItNowPrice from tb_ebay_selling ) p_online on p_online.luc_sku=p.product_serial_no "
			end if
			
			if(viewAllCate = "true")then
                Set prs = conn.execute("select distinct producter_serial_no, menu_child_serial_no  from tb_product where menu_child_serial_no<>'' order by menu_child_serial_no desc  ")
            elseif (NoNotebook = "true") then
                set prs = conn.execute("Select '0' producter_serial_no,'0' menu_child_serial_no ")
            else 
			    Set prs = conn.execute("select distinct producter_serial_no, menu_child_serial_no from tb_product where menu_child_serial_no='"& category_id &"'")
            end if

			if not prs.eof then
                   
                
				do while not prs.eof
				
										
					' 模糊
					category_id = prs("menu_child_serial_no")

                    if (NoNotebook = "true") then
                        sql_cate = " and p.menu_child_serial_no<>350 and  p.menu_child_serial_no<>216 and p.menu_child_serial_no<>378"
                    else 
                        sql_cate = " and p.menu_child_serial_no='"& category_id &"' and p.producter_serial_no='"& prs(0) &"'"
                    end if
                        
					Set rs = conn.execute("Select e.ebay_comment, e.part_sku, p.product_ebay_name "&_
					                        " , case when other_product_sku>0 then other_product_sku else p.product_serial_no end as img_sku "&_
					                        " , p_online.itemid, p_online.BuyItNowPrice "&_
					                        " , p.product_current_cost, p.screen_size, p.adjustment "&_
					                        " , p.tag , p.ltd_stock"&_
                                            " , p.screen_size "&_
                                            " , e.ebay_note "&_
                                            " , p.UPC"&_
                                            " , p_online.quantityAvailable"&_
                                            " , p.curr_change_ltd, p.curr_change_cost, p.curr_change_quantity, date_format(p.curr_change_regdate, '%Y-%m-%d') curr_change_regdate"&_
                                            " , ifnull(pas.ShippingCategoryId, '') ShippingCategoryId "&_
					                        " from tb_ebay_part_comment e inner join tb_product p on e.part_sku=p.product_serial_no "&_	
                                            " left join tb_part_and_shipping pas on pas.sku=e.part_sku "&_                                           
					                        " "& sql_online &_				                
											" where 1=1  "& sql_cate &" "& sql_keyword &" order by ebay_comment asc ")              
					
					if not rs.eof then
                        
                        set crs = conn.execute("Select menu_child_name from tb_product_category where menu_child_serial_no='"& prs("menu_child_serial_no") &"'")
                        if not crs.eof then
                            response.Write "<div style='padding:5px; text-align:center;color:blue; font-weight: bold;border:1px solid #ccc; background:#f2f2f2;'>"& crs(0) &"</div>"
                        end if
                        crs.close : set crs = nothing
                        Response.write "<h4><b>"& prs(0) &"</b> <span onclick=""modifyDesc('"&prs(0)&"');"">Modify Desc To eBay</span></h4>"
						response.write "<table cellpadding='3' cellspacing='0' width='100%'>" &vblf
						do while not rs.eof 
						    if (cstr(rs("tag"))<>"1")then
						        colorString = " color: red;"
						    else
						        colorString = ""
						    end if
						    if cstr(rs("ltd_stock")) = "0" then 
						        ltd_stock = ""
						    else
						        ltd_stock = rs("ltd_stock")
						    end if

                            if trim(rs("ebay_note")) <> "" then 
                                bgColorString = " background: #FFBE5D; "                                
                            else
                                bgColorString = "  title='part' "
                            end if

                            if rs("ShippingCategoryId") = "" then 
                                shippingString = "<b style='color:red;'>N/A</b>"
                            else
                                shippingString = "<b>S</b>"
                            end if

                            if rs("quantityAvailable") <3 then 
                                quantityAvailable = "<span style='color:red;'><b>"& rs("quantityAvailable") &"</b></span>"
                                WarnQty = WarnQty + 1
                            else
                                quantityAvailable = rs("quantityAvailable")
                            end if

                            if len(rs("product_ebay_name"))>0 then
                                product_ebay_name = replace(rs("product_ebay_name"), "<br>","")
                            else
                                product_ebay_name = rs("product_ebay_name")
                            end if

							response.write "<tr "& bgColorString &">"&_
                                            "       <td width='15'><a href='/q_admin/ebayMaster/Online/ModifyOnlineAutoPay.aspx?issystem=0&sku="& rs("part_sku") &"&itemid="&rs("itemid")&"' id='a"&rs("itemid")&"' tag='"&rs("itemid")&"' title='modify auto pay' target='_blank'>A</a>"&_
                                            "       <td width='15'><a href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?issystem=0&IsDesc=0&onlyprice=0&LogoPrictureUrl=.jpg&sku="& rs("part_sku") &"&itemid="& rs("itemid") &"' tag='"&rs("itemid")&"' title='Modify Logo' onclick=""js_callpage_cus(this.href, 'ebay_logo_"& rs("part_sku")& "', 400, 400); $(this).css({'color':'white', 'background':'black'});return false;"">L</a></td>"&_
                                            "       <td width='15'><a href='/q_admin/ebayMaster/Online/ModifyOnlineShippingFee.aspx?sku="& rs("part_sku") &"&itemid="& rs("itemid") &"' tag='"&rs("itemid")&"' title='Modify Shipping Fee' class='ModifyShippingFee' onclick=""js_callpage_cus(this.href, 'ebay_modify_shipping_"& rs("part_sku")& "', 1000, 400); $(this).css({'color':'white', 'background':'black'});return false;"">"& shippingString & "</a></td>"&_
											"		<td ><span style='color:blue; '>"& rs("ebay_comment") &"</span></td>"&_
											"		<td width='80' style='text-align:center;"& colorString &"'  ><a href='/q_admin/editPartDetail.aspx?id="& rs("part_sku") &"' onclick=""js_callpage_cus(this.href, 'ebay_part_edit', 780, 500); return false;"" target='_blank' style='"& colorString &"' title='"&prs(0)&"' tag='"&rs("itemid")&"'>["& rs("part_sku") &"]</a></td>"&_
											"		<td >"& product_ebay_name &"</td>"&_
											"		<td><a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="& rs("itemid") &"' target=""_blank"">"& rs("itemid") &"</a></td>"&_
											"       <td title='stock' style='width:50px;text-align:center;'>"& ltd_stock &"</td>"&_
											"       <td title='buyItNowPrice' id='oldPrice"&rs("part_sku")&"' style='width:50px;text-align:right;'>"& rs("BuyItNowPrice") &"</td>"&_
											"       <td title='cost' style='width:50px;text-align:right;'>"& rs("product_current_cost") &"</td>"&_
											"       <td title='NewBuyItNowPrice' id='BuyItNowPriceNew"&rs("part_sku")&"' luc_sku='"& rs("part_sku") &"'"&_
											"            cost='"& rs("product_current_cost") &"'"&_
											"            screen='"& rs("screen_size") &"' "&_
											"            itemid='"& rs("itemid") &"' "&_
											"            adjustment='"& rs("adjustment") &"' style='width:50px;text-align:right;'><img src='/soft_img/tags/loading.gif'></td>"&_
											"       <td title='diff' id='diff"&rs("part_sku")&"' style='width:50px;text-align:right;'></td>"&_
											"		<td><a href=""/q_admin/ebayMaster/ebay_part_temp_page_view.aspx?sku="& rs("part_sku") &""" target='_blank' style='margin-left: 3em;' onclick=""js_callpage_cus(this.href, 'ebay_part_comment_edit', 1050, 700);return false;""> Page </a></td>"&_
											"       <td>&nbsp;&nbsp;<a href=""/q_admin/ebayMaster/ebay_part_comment_edit.asp?sku="& rs("part_sku") &"""  onclick=""js_callpage_cus(this.href, 'ebay_part_comment_edit', 780, 500); $(this).css({'color':'white', 'background':'black'}); return false;"" title='Edit Ebay'>Summary</a></td>"&_
											"       <td id='modifyEbayPrice"&rs("part_sku")&"' style='width:40px;text-align:center;"& bgColorString &"'></td>"&_
											"       <td id='modifyEbayDesc"&rs("part_sku")&"' style='width:40px;text-align:center;"& bgColorString &"'> E Desc </td>"&_
											"       <td id='modifyEbayDescPrice"&rs("part_sku")&"' style='width:80px;text-align:center;"& bgColorString &"'> E Price & Desc </td>"&_
											"       <td style='width:40px; text-align:center;'><a id='enditema_"&rs("part_sku")&"' href=""/q_admin/ebayMaster/online/EndItem.aspx?itemid="&rs("itemid")&""" onclick=""if(confirm('are you sure')){js_callpage_cus(this.href, 'ebay_part_end_"& rs("itemid") &"', 780, 500);} return false;"" target='_blank'> End </a></td>"&_
                                            "       <td style='color:#333333;'> "& rs("curr_change_ltd") &"|"& rs("curr_change_cost") &"|"& rs("curr_change_quantity") &"|"& rs("curr_change_regdate") &"</td>"&_
                                            "       <td >"& rs("UPC") &"</td>"&_
                                            "       <td >"& quantityAvailable & "</td>"&_
                                            "       <td class='modifyStock' data-token='"+ token +"' data-sku='"& rs("part_sku") &"'>Modify Stock</td>"&_
                                            "</tr>" &_
											"<script> GetNewBuyItNow($('#BuyItNowPriceNew"&rs("part_sku")&"')); </script>"
                            if trim(rs("ebay_note")) <> "" then 
                                Response.Write "<tr name='ebay_note'><td colspan='19'  style='color:green; "& bgColorString &"'>" & rs("ebay_note") & "</td></tr>" &vblf
                            end if
						rs.movenext
						loop
						response.write "</table>"
					end if
					rs.close : set rs = nothing
				prs.movenext
				loop
			
			end if
			prs.close : set prs = nothing
		else
			response.write "Please select Category."
    	end if
    	
    	
    	function GetModifyEbayPrice(price, itemID)
    	    GetModifyEbayPrice = "<a href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Price="& price &"&IsDesc=0&onlyprice=1&itemid="& itemID &"&issystem=0' target='_blank'>"&_
    	                         " eBay Price"&_
    	                         " </a>"
    	
    	end function
    closeconn() %>
    <script type="text/javascript">

        $().ready(function () {


            $('tr[title=part]').hover(
               function () {
                   $(this).find("td").css("border-bottom", "1px solid #999999").css("padding-bottom", "0px");
               }
             , function () {
                 $(this).find("td").css("border-bottom", "0px solid #000000").css("padding-bottom", "1px");
             }

         ).each(function (i) {
             if (i % 2 == 1) {
                 $(this).find("td").css("background", "#ffffff").css("font-size", "8pt").css("padding-bottom", "1px");

             } else {
                 $(this).find("td").css("background", "#f2f2f2").css("font-size", "8pt").css("padding-bottom", "1px");
             }
         });

            $('td[title=stock]').each(function () {
                if ($(this).html() == "" || $(this).html().indexOf("-") > 0)
                    $(this).css("background", "yellow");
            });

            $('#warn_comment').html("stock warning <%= WarnQty %>");

            $('.modifyStock').on('click', function () {
                var sku = $(this).attr("data-sku");
                var token = $(this).attr('data-token');
                $.getJSON("http://webapi.lucomputers.com/api/SeteBayItemQty/Get?t=" + token + "&sku=" + sku, {}, function () {
                    $(this).text("OK");
                });
            });
        });

    </script>
</body>
</html>
