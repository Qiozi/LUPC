<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>ebay system</title>
    <script src="/js_css/jquery-1.9.1.js"></script>
    <!--<script src="../../JS/lib/jquery-1.3.2.min.js"></script>-->
    <script type="text/javascript" src="../../JS/lib/jquery.bgiframe.min.js"></script>
    <script type="text/javascript" src="../../JS/lib/jquery.ajaxQueue.js"></script>
    <script type="text/javascript" src="../../JS/lib/thickbox-compressed.js"></script>
    <script type="text/javascript" src="../../JS/lib/jquery.autocomplete.js"></script>
    <script type="text/javascript" src="../../xmlStore/part_name_data.js"></script>
    <!-- <script src="/q_admin/JS/lib/jquery.history_remote.pack.js" type="text/javascript"></script>-->

    <script src="/q_admin/JS/WinOpen.js" type="text/javascript"></script>
    <script src="/q_admin/js/helper.js" type="text/javascript"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popup.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popupclass.js"></script>
    <link rel="stylesheet" type="text/css" href="/js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="/js_css/b_lu.css" />

    <style type="text/css">
        input {
            font-size: 7.5pt;
        }

        #Button1 {
            width: 97px;
            height: 67px;
        }

        .bottom_menu li {
            float: left;
            list-style: none;
        }

            .bottom_menu li a {
                display: block;
                float: left;
                width: 80px;
                padding: 2px;
                text-align: center;
            }

                .bottom_menu li a:hover {
                    display: block;
                    float: left;
                    width: 80px;
                    padding: 2px;
                    background: green;
                    text-align: center;
                    color: White;
                }
        /* table */

        #etcResultArea {
            border-collapse: collapse;
            border-spacing: 0;
            margin-right: auto;
            margin-left: auto;
        }

            #etcResultArea > th > td {
                border: 1px solid #b5d6e6;
                font-size: 12px;
                font-weight: normal;
                text-align: center;
                vertical-align: middle;
                height: 20px;
            }

            #etcResultArea th {
                background: #D1ECFF;
            }

        #etc_system_part_detail_list td {
            padding: 3px;
        }

        #partRelated h3 {
            font-weight: bold;
            color: green;
            padding: 5px;
        }

        #partRelated h4 {
            font-weight: bold;
            color: #3cbe3c;
            padding-left: 1.2rem;
        }

        #partRelated label {
            font-weight: bold;
            padding-left: 2rem;
        }
    </style>
    <script type="text/javascript">

        function editMotherboard(e) {
            var href = e.attr('href');
            var part_group_id = 0;
            e.parent().parent().find('select').each(function () {
                if ($(this).attr('name') == 'ebay_part_group')
                    part_group_id = $(this).val();
            });
            js_callpage_name_custom(href + '?part_group_id=' + part_group_id, 'sys_part_motherboard' + part_group_id, 1150, 700);

        }

        ////////
        function changeEbayPartDetail(the) {
            var luc_sku = null;
            the.parent().parent().find('input').each(function () {
                if ($(this).attr('name') == 'p_luc_sku') {
                    luc_sku = $(this).val();
                }
            });
            if (luc_sku == the.val())
                the.css('color', 'green');
            else
                the.css('color', 'black');
        }

        ///////
        function ModifyPriceToEbay(price, itemid) {

            if (itemid == '')
                return;
            var href = "/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=&Price=" + price + "&IsDesc=0&onlyprice=1&itemid=" + itemid + "&issystem=1";

            if (confirm('are you sure?\r\nPrice: ' + price + '\r\nitemid: ' + itemid)) {
                js_callpage_cus(href, 'ebay_price_' + itemid, 300, 200);
            }
            return false;
        }

        function ModifyTitleToEBay(itemid) {
            if (itemid == '')
                return;
            var href = "/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?itemid=" + itemid + "&issystem=1&LogoPrictureUrl=.jpg";

            if (confirm('are you sure?\r\nitemid: ' + itemid)) {
                js_callpage_cus(href, 'ebay_title_' + itemid, 300, 200);
            }
            return false;
        }

        function ModifyShippingFeeToEBay(itemid, systemSku) {
            if (itemid == '')
                return;
            var href = "/q_admin/ebayMaster/Online/ModifyOnlineShippingFee.aspx?itemid=" + itemid + "&sku=" + systemSku;
            // href="/q_admin/ebayMaster/Online/ModifyOnlineShippingFee.aspx?sku=215445&amp;itemid=251190772958" title="Modify Shipping Fee" onclick="if(confirm('Are you sure.')){js_callpage_cus(this.href, 'ebay_modify_shipping_215445', 1000, 400); $(this).css({'color':'white', 'background':'black'});}return false;"><b>S</b></a>
            if (confirm('are you sure?\r\nitemid: ' + itemid)) {
                js_callpage_cus(href, 'ebay_shipping_' + itemid, 300, 200);
            }
            return false;
        }

        function ModifyDescToEBay(price, itemid) {
            if (itemid == '') {
                alert("itemid is null.");
                return;
            }
            //"/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=680.52&amp;Profit=68.05&amp;eBayFee=40.63&amp;ShippingFee=40.00&amp;Price=805.99&amp;IsDesc=1&amp;onlyprice=0&amp;itemid=250698574192&amp;issystem=1
            var href = "/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?Cost=&Price=" + price + "&IsDesc=1&onlyprice=0&itemid=" + itemid + "&issystem=1";

            if (confirm('are you sure?\r\nitemid: ' + itemid)) {
                js_callpage_cus(href, 'ebay_' + itemid, 300, 200);
            }
            return false;
        }

        function closeSys(sysSku) {
            $.ajax({
                type: "get",
                url: "../ebay_system_cmd.asp",
                data: "cmd=closesys&sku=" + sysSku,
                error: function (r, t, s) {
                    alert(t);
                },
                success: function (msg) {
                    alert(msg);
                }
            });
        }

        function reInitStock(sysSku) {
            $.ajax({
                type: "get",
                url: "../ebay_system_cmd.asp",
                data: "cmd=reInitStock&sku=" + sysSku,
                error: function (r, t, s) {
                    alert(t);
                },
                success: function (msg) {
                    alert(msg);
                }
            });
        }
    </script>
</head>

<body>
    <iframe id="iframe1" name="iframe1" src="" style="width: 0px; height: 0px;" frameborder="0"></iframe>

    <div id="fragment-13">
        <table>
            <tr>
                <td valign="top">

                    <%

        dim category_id, cmd, ebay_system_sku
		Dim ebay_system_code	:	ebay_system_code= SQLescape(request("ebay_system_code"))
		Dim id 	: 	id = null
        Dim is_exist_ebay_number
        Dim is_from_ebay
        Dim ItemID
        Dim caseImages
        Dim initPartStatus              : initPartStatus = "none"

        ebay_system_sku            		=      SQLescape(request("ebay_system_sku"))
        cmd                             =      SQLescape(request("cmd"))
        category_id                     =      SQLescape(request("category_id"))
		if (len(trim(ebay_system_sku))=12) then 
		    set rs = conn.execute("select sku from tb_ebay_code_and_luc_sku where ebay_code='"& trim(ebay_system_sku) &"'")
		    if not rs.eof then
		        ebay_system_sku = rs(0)
		    end if
		    rs.close : set rs = nothing
		end if	

        set rs = conn.execute("select status from tb_timer where cmd = '18'")
        if not rs.eof then
            if (rs(0) = 1) then initPartStatus = "reading"
            if (rs(0) = 2) then initPartStatus = "runing"
            if (rs(0) = 3) then initPartStatus = "End"
        end if
        rs.close()
		
		' SKU
		Set rs = conn.execute("select id, category_id, ebay_system_current_number,is_from_ebay, logo_filenames, keywords,eBayCategoryName from tb_ebay_system where id="&SQLquote(ebay_system_sku)&" or ebay_system_current_number="&SQLquote(ebay_system_sku))
		if not rs.eof then
		    if category_id = "" then category_id = rs("category_id")    ' Search Fun have not category id
			ebay_system_sku		=	RS("ID")
			ebay_system_code	=	RS("ebay_system_current_number")
            is_from_ebay    =   rs("is_from_ebay")

			response.write "<div class='title'><h3> "
            if(is_from_ebay = 1) then 
                Response.write "<img src='http://www.lucomputers.com/soft_img/app/ebay_logo.jpg'>"
            end if
            response.write " SKU: "& ebay_system_sku &"&nbsp;&nbsp; <span id='system_comment_span'></span> "	
            response.Write "("& rs("eBayCategoryName")&")"					
			if len(ebay_system_code) >0 then response.write "<br>Current Ebay#: &nbsp;"& ebay_system_code
			Response.write "<input type='button' value='Dupi' onclick='OpenDuplicateSys("""& ebay_system_sku &""")'>"
			Response.write "<input type='button' value='Edit Group' onclick=""editSysPartGroup('"& ebay_system_sku &"');"" >"
			Response.write "<input type='button' value='View Page' onclick=""viewSysPage("& ebay_system_sku &");"">"
			Response.Write "<input type='button' value='Edit All Part' onclick=""editAllPartPrice();"">"
            Response.write "<input type='button' value='Close this system.' onclick=""closeSys('"& ebay_system_sku &"');"">"
            Response.write "<input type='button' value='ReInit Stock("& initPartStatus &")' onclick=""reInitStock('"& ebay_system_sku &"');"">"
            Response.write "<input type='button' value='reset group parts data.' class='createPartGroupTempList'>" 
			Response.write "</h3></div>"

            Response.Write "<div style='padding:3px; border:1px solid #ccc;background:#f2f2f2;margin-bottom:10px;'>"
            Response.Write "   <span id='sysKeyword'>"& rs("keywords") &"</span><input type='button' value='Edit Keyword' onclick=""editKeywords('"& ebay_system_sku &"');"">"
            Response.Write "</div>"
		end if
		rs.close 

        
         Response.Write "<div style='padding:3px; border:1px solid #ccc;background:#f2f2f2;margin-bottom:10px;'>"
         set rs = conn.execute("Select menu_child_name from tb_product_category pc inner join tb_ebay_system_and_category ec on ec.eBaySysCategoryId=pc.menu_child_serial_no where ec.SystemSku='"&ebay_system_sku&"'")
         if not rs.eof then
         response.Write " <span id='sysCategory'>"&vblf
         do while not rs.eof 
                Response.Write "  "& rs("menu_child_name") &",&nbsp;&nbsp;"        
         rs.movenext
         loop
         response.Write " </span>"
         end if
         rs.close 
         Response.Write "<input type='button' value='Edit Category' onclick=""editCategorys('"& ebay_system_sku &"');""></div>"
		
		'ebay code
        set rs = conn.execute("Select ItemID, BuyItNowPrice, quantity - quantityavailable sellQty from tb_ebay_selling where sys_sku='"& ebay_system_sku &"'")
        if  rs.eof then 
        
          set rs = conn.execute("select ebay_code itemid,BuyItNowPrice, '0' sellQty  from tb_ebay_code_and_luc_sku where sku='"& trim(ebay_system_sku) &"' order by id desc limit 1")
        end if
        if not rs.eof then
            Response.write "<div style='padding:3px; border:1px solid #ccc;background:#B9E3C7;margin-bottom:10px;'>Current eBay ItemID: "
            do while not rs.eof 
                ItemID = rs("itemid")
                Response.write "&nbsp;&nbsp;&nbsp;&nbsp;<a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&rd=1&item=" & rs("itemid") &"' target='_blank'>" & rs("itemid") &"</a>"
                Response.write "&nbsp;&nbsp;$"& rs("BuyItNowPrice")
                 Response.write "&nbsp;&nbsp;Sell Qty: "& rs("sellQty")
                             Response.write "<input type='button' value='Change eBay Store Category' onclick=""window.open('../change_ebay_category.aspx?sku="& ebay_system_sku &"&itemid="&rs("itemid")&"', 'cate_"& ebay_system_sku &"', 'width=500,height=300');"">"
            rs.movenext
            loop
            Response.write "</div>"
        end if
        rs.close : set rs = nothing
        
        dim el_sku_ids, el_part_name_ids, el_comment_ids
        el_comment_ids                   =          "0"
        el_sku_ids                       =          "0"
        el_part_name_ids                 =          "0"
        
        response.write "<form action='/q_admin/ebayMaster/lu/ebay_system_edit_exec_2.asp' method='post'  onsubmit='return ValidPost();'>"
        response.write "<input type='hidden' name='IsParent' value='"& request("IsParent") &"'/>"
        response.write "<input type='hidden' name='category_id' value='"& category_id &"'/>"
        response.write "<input type='hidden' name='cmd' value='"& cmd &"' />"
        response.write "<input type='hidden' name='ebay_system_sku' value= '"& ebay_system_sku &"'/>"
        
		response.write "<table border=0 cellpadding='3'>"
		if cmd = "modify" then
			set rs = conn.execute("select c.id, c.comment,category_ids, ps.luc_sku,  "&_
								 " p.product_ebay_name, ps.part_quantity, ps.max_quantity"&_
								 " ,p.product_current_price-p.product_current_discount sell, p.product_current_cost"&_
								 " ,ifnull(ps.part_group_id, -1) part_group_id "&_
								 " ,ps.is_label_of_flash"&_
                                 " ,ps.is_belong_price"&_
                                 " ,c.is_mb"&_
                                 " ,c.is_cpu"&_
								 " from tb_ebay_system_part_comment c inner join tb_ebay_system_parts ps on ps.comment_id=c.id and ps.ebayshowit=1 "&_
								 " left join tb_product p on p.product_serial_no=ps.luc_sku "&_
								 " where c.showit=1 and ps.system_sku = '"&ebay_system_sku&"' order by c.priority asc ")

			if not rs.eof then
                is_exist_ebay_number = IsExistEbayNumber(ebay_system_sku)
				i = 0
                do while not rs.eof 
						i = i +1 
                        el_sku_ids               = el_sku_ids & ",sku_"&rs(0)
                        el_part_name_ids         = el_part_name_ids & ",name_" & rs(0) 
                        el_comment_ids           = el_comment_ids & "," & rs(0)
                        response.write "<tr>"
						response.write "	<td width='25'>"& i &"</td>"
                        Response.write "    <td title='Inside Total'>"
                        Response.write "        <input type='checkbox' name='cb_inside_total' value='"&rs(0)&"' "
                        if(rs("is_belong_price") = 1) then Response.Write " checked ='checked' "
                        Response.Write "        >&nbsp;</td>"
						Response.write "       <td title='Label Of Flash'><input type='checkbox' name='cb_label_of_flash' value='"&rs(0)&"' "
						if(rs("is_label_of_flash")=1) then response.write " checked='checked' "
						Response.write "        >&nbsp;</td>"
                        response.write "       <td>"& rs(1) &"<input type='hidden' name='comment_id' value='"& rs(0) &"'/></td>"
                        Response.Write "        <td width='20' style='text-align:center;'>"
                        if rs("part_group_id")<>-1 and rs("part_group_id")<>"-1" and rs("part_group_id")<>"" then 
                            Response.Write "        <a href='/q_admin/sys/sys_group_part_list.asp' onclick=""editPartOfGroup($(this));return false;"">E</a>"&vblf
                            Response.Write "        <input type='hidden' name='part_group_id_f' value='"& rs("part_group_id") &"'>"&vblf
                        end if
                        Response.Write "        </td>"
                                Response.write "       <input type='hidden' name='p_luc_sku' value='"& rs("luc_sku") &"'>"
								Response.write " 	   <input type='hidden' name='p_id' value='"&rs("id")&"' >"
								response.write "       <td><Select  name='ebay_part_group' id='ebay_part_group_"&rs(0)&"' onChange='return changeEbayPartGroup($(this));'>"
							   ' if is_exist_ebay_number then Response.write " disabled='true' "
								response.write GetEbayPartGroup(rs("category_ids"), rs("part_group_id") )
								response.write "		</select>"
								'response.write  srs("part_group_id")
								response.write "        </td>"
								
								Response.Write "        <td width='20' style='text-align:center;'>"
								if rs("part_group_id")<>-1 and rs("part_group_id")<>"-1" and rs("part_group_id")<>"" then 
									Response.Write "        <a href='ebay_system_edit_part_of_group.asp' onclick=""editPartOfGroup($(this));return false;"">E</a>"
								
                                else
                                    response.Write "<td></td>" 
                                end if
								Response.Write "        </td>"
								
                                response.write "       <td class='ebay_part_group_detail_box' id='td_ebay_part_group_detail_"&rs(0)&"' data-id='"&rs(0)&"' data-group-id='"&rs("part_group_id")&"' data-luc-sku='"& rs("luc_sku") &"'>"
								Response.write "		<Select name='ebay_part_group_detail' id='ebay_part_group_detail_"&rs(0)&"' "&_
								                           " onChange='return changeEbayPartDetail($(this));' >"
								
								Response.write "		</select>"
                                response.write "       </td>"
                                if rs("is_mb") = 1 then 
                                    Response.write "    <td><a href='part_relation_motherboard.asp' onclick=""editMotherboard($(this));return false;"">E</a></td>"&vblf
                                else
                                    response.Write "<td></td>" 
                                end if

                                if rs("is_cpu") = 1 then 
                                    Response.write "    <td><a href='ebay_system_edit_part_of_group.asp' onclick=""editPartOfGroup($(this));return false;"">E</a></td>"&vblf
                                else
                                    response.Write "<td></td>"   
                                end if

                        		response.write "       <td style='display:none;'>qty:<input type='text' name='luc_part_part_quantity' id='part_quantity_"& rs(0) &"' ReadOnly='true' size='3' value=1></td>"
								response.write "       <td style='display:none;'>max:<input type='text' name='luc_part_max_quantity' id='max_quantity_"& rs(0) &"' ReadOnly='true' size='3' value=1></td>"
								response.Write "       <td>"& rs("luc_sku") &"</td>"

                                if rs(1) = "Case" then 
                                    caseImages = "https://www.lucomputers.com/pro_img/components/" & rs("luc_sku") & "_list_1.jpg"
                                end if
						response.write "</tr>"
						rs.movenext
						loop
				end if
				rs.close : set rs = nothing
        end if
        response.write "<table>"
'        rs.close : set rs = nothing
'        end if
		Response.Write "<i><b>Group Quantity:10,15,16,17,18,19,20,21</b></i><br/>"
        response.write "<div style='text-align:center;width: 800px;'><input type='submit' value='Save'></div>"
        response.write "</form>"
        
        
        dim el_sku_ids_gs, el_part_name_ids_gs, comment_ids_gs
        el_comment_ids_gs = split(el_comment_ids, ",")
        el_sku_ids_gs = split( el_sku_ids, ",")
        el_part_name_ids_gs = split (el_part_name_ids, ",")

        
    if cmd = "modify" then
            dim cutom_label, system_price, system_cost, system_sell
            Dim large_pic_name
			Dim sys_keywords
			Dim logo_filenames
			Dim is_exist
			Dim adjustment
			Dim ebay_system_price
			Dim is_disable_flash_customize
			Dim is_shrink
            Dim caseType 
			Dim batch
			'system_sell     =   GetEbaySystemPriceTotal(ebay_system_sku)
			
			
            set rs = conn.execute("select id,adjustment, cutom_label, ebay_system_price, keywords, logo_filenames"&_
                                ", is_disable_flash_customize"&_
								", is_include_shipping "&_
								", is_shrink"&_
                                ", is_barebone"&_
                                ", CaseType"&_
                                ", batch "&_
                                ", system_title1, system_title2, system_title3,large_pic_name from tb_ebay_system where id='"&ebay_system_sku & "'")
            if not rs.eof then
                    cutom_label     =   rs("cutom_label")
                    ebay_system_price=  rs("ebay_system_price")

					sys_keywords    =   rs("keywords")
					logo_filenames	=	rs("logo_filenames")
					
					system_title1   =   rs("system_title1")
					system_title2   =   rs("system_title2")
					system_title3   =   rs("system_title3")
					large_pic_name  =   rs("large_pic_name")
					adjustment      =   rs("adjustment")
					is_disable_flash_customize = rs("is_disable_flash_customize")
					is_include_shipping = rs("is_include_shipping")
					is_shrink		=	rs("is_shrink")
                    is_barebone     =   rs("is_barebone")
                    caseType        =   rs("CaseType")
                    batch           =   rs("batch")
                    if(batch =0)then
                            batch = ebay_system_sku
                    end if

            end if
            rs.close : set rs = nothing 
            
                    %>
                    <br />
                    <div id="partRelated">
                        <div>
                            <h3>Memory</h3>
                            <div>
                                <% set prs = conn.execute("Select distinct note from tb_ebay_system_memory_group")
                                if not prs.eof then
                                    do while not prs.eof 
                                %>
                                <h4><%= prs("note") %></h4>
                                <%
                                        set rs = conn.execute("SELECT e.*, p.short_name_for_sys, p.ltd_stock FROM tb_ebay_system_memory_group e inner join tb_product p on p.product_serial_no=e.luc_sku;")
                                        if not rs.eof then
                                            do while not rs.eof
                                %>
                                <label>
                                    <input type="checkbox" value="<%= rs("luc_sku") %>" class="memoryItem" />
                                    [<%= rs("luc_sku") %>](<%= rs("ltd_stock") %>) <%= rs("short_name_for_sys") %>
                                </label>
                                <%
                                            rs.movenext
                                            loop
                                        end if 
                                        rs.close
                                prs.movenext
                                loop
                                end if
                                prs.close
                                %>
                            </div>
                        </div>
                        <div>
                            <h3>Video Card</h3>
                            <div>
                                <% set prs = conn.execute("Select distinct note from tb_ebay_system_vc_group")
                                if not prs.eof then
                                    do while not prs.eof 
                                %>
                                <h4><%= prs("note") %></h4>
                                <%
                                        set rs = conn.execute("SELECT e.*, p.short_name_for_sys, p.ltd_stock FROM tb_ebay_system_vc_group e inner join tb_product p on p.product_serial_no=e.luc_sku;")
                                        if not rs.eof then
                                            do while not rs.eof
                                %>
                                <label>
                                    <input type="checkbox" value="<%= rs("luc_sku") %>" class="vcItem" />
                                    [<%= rs("luc_sku") %>](<%= rs("ltd_stock") %>) <%= rs("short_name_for_sys") %>
                                </label>
                                <%
                                            rs.movenext
                                            loop
                                        end if 
                                        rs.close
                                prs.movenext
                                loop
                                end if
                                prs.close
                                %>
                            </div>
                        </div>
                        <div>
                            <h3>ssd</h3>
                            <div>
                                <% set prs = conn.execute("Select distinct note from tb_ebay_system_ssd_group")
                                if not prs.eof then
                                    do while not prs.eof 
                                %>
                                <h4><%= prs("note") %></h4>
                                <%
                                        set rs = conn.execute("SELECT e.*, p.short_name_for_sys, p.ltd_stock FROM tb_ebay_system_ssd_group e inner join tb_product p on p.product_serial_no=e.luc_sku;")
                                        if not rs.eof then
                                            do while not rs.eof
                                %>
                                <label>
                                    <input type="checkbox" value="<%= rs("luc_sku") %>" class="ssdItem" />
                                    [<%= rs("luc_sku") %>](<%= rs("ltd_stock") %>) <%= rs("short_name_for_sys") %>
                                </label>
                                <%
                                            rs.movenext
                                            loop
                                        end if 
                                        rs.close
                                prs.movenext
                                loop
                                end if
                                prs.close
                                %>
                            </div>
                        </div>
                        <div>
                            <h3>ssd M.2</h3>
                            <div>
                                <% set prs = conn.execute("Select distinct note from tb_ebay_system_ssdm2_group")
                                if not prs.eof then
                                    do while not prs.eof 
                                %>
                                <h4><%= prs("note") %></h4>
                                <%
                                        set rs = conn.execute("SELECT e.*, p.short_name_for_sys, p.ltd_stock FROM tb_ebay_system_ssdm2_group e inner join tb_product p on p.product_serial_no=e.luc_sku;")
                                        if not rs.eof then
                                            do while not rs.eof
                                %>
                                <label>
                                    <input type="checkbox" value="<%= rs("luc_sku") %>" class="ssdM2Item" />
                                    [<%= rs("luc_sku") %>](<%= rs("ltd_stock") %>) <%= rs("short_name_for_sys") %>
                                </label>
                                <%
                                            rs.movenext
                                            loop
                                        end if 
                                        rs.close
                                prs.movenext
                                loop
                                end if
                                prs.close
                                %>
                            </div>
                        </div>
                        <div>
                            <h3>hdd</h3>
                            <div>
                                <% set prs = conn.execute("Select distinct note from tb_ebay_system_hdd_group")
                                if not prs.eof then
                                    do while not prs.eof 
                                %>
                                <h4><%= prs("note") %></h4>
                                <%
                                        set rs = conn.execute("SELECT e.*, p.short_name_for_sys, p.ltd_stock FROM tb_ebay_system_hdd_group e inner join tb_product p on p.product_serial_no=e.luc_sku;")
                                        if not rs.eof then
                                            do while not rs.eof
                                %>
                                <label>
                                    <input type="checkbox" value="<%= rs("luc_sku") %>" class="hddItem" />
                                    [<%= rs("luc_sku") %>](<%= rs("ltd_stock") %>) <%= rs("short_name_for_sys") %>
                                </label>
                                <%
                                            rs.movenext
                                            loop
                                        end if 
                                        rs.close
                                prs.movenext
                                loop
                                end if
                                prs.close
                                %>
                            </div>
                        </div>

                        <div style="text-align: center; padding: 1rem;">
                            <input class="btnSubmitDuplicateSystem" type="button" value="Submit" />
                        </div>
                    </div>
                    <br />

                    <form action="/q_admin/ebayMaster/ebay_system_edit_update.asp" method="post" target="iframe1">
                        <input type='hidden' name='IsParent' value='<%= request("IsParent")%>' />
                        <input type="hidden" name="ebay_system_sku" value="<%= ebay_system_sku %>" />
                        <table>
                            <tr>
                                <td rowspan="5" style="text-align: right" valign="bottom">
                                    <table>
                                        <tr>
                                            <td>Flash Show Price$</td>
                                            <td colspan="7" align="left">&nbsp;&nbsp;<span id='flash_price' style='color: blue; font-weight: bold'>flash price</span></td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                            <td colspan="7" align="left">
                                                <input type='checkbox' name='disableFlash' <% if is_disable_flash_customize=1 then response.write " checked='checked' " %> value='1' />
                                                <b>&nbsp;Disable Flash Customize
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type='checkbox' id='is_include_shipping'
                                                            <% if(is_shrink=0) then Response.Write " disabled='disabled' "%>
                                                            name='is_include_shipping' <% if is_include_shipping=1 then response.write " checked='checked' " %> value='1' />
                                                    <b>&nbsp;eBay Price include Shipping Fee</b></b></td>
                                        </tr>
                                        <tr>
                                            <td>Flash Type</td>
                                            <td colspan="7" align="left">
                                                <input type="radio" name='flashType' <% if is_shrink=0 and is_barebone=0 then response.write " checked='checked' " %> value='1' />
                                                <b>&nbsp;Old, All &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="radio" name="flashType" value="2" <% if is_shrink=1 then response.write " checked='checked' " %> />
                                                    Is Child
                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                        <input type="radio" name="flashType" value="3" <% if is_barebone=1 then response.write " checked='checked' " %> />
                                                    Barebone
                                                </b>
                                                <select name="CaseType">
                                                    <%
                                                            set sdrs = conn.execute("select distinct CaseType from tb_ebay_system_case_group")
                                                            if not sdrs.eof then
                                                                do while not sdrs.eof 
                                                                    response.Write("<option ")
                                                                    if caseType = sdrs("CaseType") then response.Write " selected='selected' "
                                                                    Response.Write (">"& sdrs("CaseType") &"</option>")
                                                                sdrs.movenext
                                                                loop
                                                            end if
                                                            sdrs.close:set sdrs = nothing
                                                    %>
                                                </select>
                                                <input type="text" name="batch" value="<%= batch %>" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Custom Label: </td>
                                            <td colspan="6" align="left">
                                                <input type="text" size="70" name="system_name" value="<%= cutom_label %>" /></td>
                                            <td rowspan="12" valign="bottom">
                                                <input id="btn_modify_ebay_title_logo" disabled="disabled" type="button"
                                                    value="Modify eBay Title and Logo" onclick="" /><br />
                                                <br />
                                                <input id="btn_modify_sold_to_ebay" disabled="disabled" type="button" value="Modify eBay Price" onclick="" />
                                                <br />
                                                <br />
                                                <input id="btn_modify_desc_to_ebay" disabled="disabled" type="button" onclick="" value="Modify eBay Desc" />
                                                <br />
                                                <br />
                                                <input id="btn_modify_shipping_fee" disabled="disabled" type="button" onclick="" value="Modify eBay Shipping Fee" />
                                                <br />
                                                <br />
                                                <input id="btn_edit_summary" type="button" value="Edit Summary" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Auto Title:</td>
                                            <td colspan="6" align="left">
                                                <input type="text" size="100" id="system_title_auto"
                                                    value="..." maxlength="80" /></td>
                                        </tr>
                                        <tr>
                                            <td>eBay Title:</td>
                                            <td colspan="6" align="left">
                                                <input type="text" size="100" name="system_title1"
                                                    value="<%= system_title1 %>" maxlength="80" /></td>
                                        </tr>
                                        <tr>
                                            <td>Large Picture Name</td>
                                            <td colspan="6" align="left">
                                                <input type="text" size="70" name="large_pic_name" value="<%= large_pic_name %>" /></td>
                                        </tr>
                                        <tr>
                                            <td>Logo Picture Name</td>
                                            <td colspan="6" align="left">
                                                <input type="text" size="70" name="logo_filenames" value="<%= logo_filenames %>" /></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 120px">System Cost(web)$</td>
                                            <td>
                                                <input type="text" id='cost' name="cost"
                                                    style="text-align: right; color: #cccccc;" disabled="disabled"
                                                    value="..." /></td>
                                            <td>Selected Cost$</td>
                                            <td>
                                                <input type="text" id='selected_cost' name="selected_cost"
                                                    style="text-align: right; color: #cccccc;" disabled="disabled"
                                                    value="<%= selected_cost %>" /></td>
                                            <td>No Selected Cost$</td>
                                            <td>
                                                <input type="text" id='no_selected_cost' name="no_selected_cost"
                                                    style="text-align: right; color: #cccccc;" disabled="disabled"
                                                    value="<%= no_selected_cost %>" /></td>
                                            <td rowspan="8" style="text-align: right" valign="bottom">
                                                <input id="Button1" type="submit" value="Save" /></td>
                                        </tr>
                                        <tr>
                                            <td>System Price(web)$</td>
                                            <td>
                                                <input type="text" name="price" id='sys_web_price'
                                                    style="text-align: right; color: #cccccc;" disabled="disabled"
                                                    value="..." /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>adjustment$</td>
                                            <td>
                                                <input type="text" id='adjustment' name="adjustment"
                                                    style="text-align: right;"
                                                    value="..." /></td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>System price(ebay)$</td>
                                            <td>
                                                <input type="text" id='all_ebay_price' name="all_ebay_price" disabled="disabled" style="text-align: right;" value="..." /></td>
                                            <td>eBay Price$</td>
                                            <td>
                                                <input type="text" id='selected_ebay_price' name="selected_ebay_price" disabled="disabled" style="text-align: right;" value="..." /></td>
                                            <td>eBay Price$</td>
                                            <td>
                                                <input type="text" id='no_selected_ebay_price' name="no_selected_ebay_price" disabled="disabled" style="text-align: right;" value="..." /></td>
                                        </tr>
                                        <tr>
                                            <td>Profit$</td>
                                            <td>
                                                <input type="text" name="ebay_profit_1" id="ebay_profit_1" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>Profit$</td>
                                            <td>
                                                <input type="text" name="ebay_profit_2" id="ebay_profit_2" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>Profit$</td>
                                            <td>
                                                <input type="text" name="ebay_profit_3" id="ebay_profit_3" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                        </tr>
                                        <tr>
                                            <td>eBay Fee$</td>
                                            <td>
                                                <input type="text" name="ebay_fee_1" id="ebay_fee_1" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>eBay Fee$</td>
                                            <td>
                                                <input type="text" name="ebay_fee_2" id="ebay_fee_2" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>eBay Fee$</td>
                                            <td>
                                                <input type="text" name="ebay_fee_3" id="ebay_fee_3" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                        </tr>
                                        <tr>
                                            <td>Shipping Fee$</td>
                                            <td>
                                                <input type="text" name="shipping_fee_1" id="shipping_fee_1" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>Shipping Fee$</td>
                                            <td>
                                                <input type="text" name="shipping_fee_2" id="shipping_fee_2" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>Shipping Fee$</td>
                                            <td>
                                                <input type="text" name="shipping_fee_3" id="shipping_fee_3" disabled="disabled"
                                                    style="text-align: right;" value="..." /></td>
                                        </tr>
                                        <tr>
                                            <td>eBay Sell$</td>
                                            <td>
                                                <input type="text" name="ebay_system_price_real_1" id="ebay_system_price_real_1"
                                                    style="text-align: right;" value="..." /></td>
                                            <td>eBay Sell$</td>
                                            <td>
                                                <input type="text" name="ebay_system_price_real_2" id="ebay_system_price_real_2"
                                                    style="text-align: right;" value="<%= ebay_system_price_2 %>" /></td>
                                            <td>eBay Sell$</td>
                                            <td>
                                                <input type="text" name="ebay_system_price_real_3" id="ebay_system_price_real_3" disabled="disabled"
                                                    style="text-align: right;" value="<%= ebay_system_price_3 %>" /></td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="padding: 1rem">
                                    <%
                                       '     set rs = conn.execute(" select esc.*,es.*, p.product_name,p.product_short_name,p.product_current_cost, es.id from tb_ebay_system_case_group esc "&_ 
                                       '                             " inner join tb_product p on esc.caseSku=p.product_serial_no "&_
		                               '                             " left join "&_
		                               '                             " (select id ,casetype, (select max(luc_sku) from tb_ebay_system_parts where comment_id=9 and system_sku=ee.id) caseSku2 from tb_ebay_system ee where batch='"&batch&"') es on es.casetype= esc.casetype and esc.casesku = es.casesku2 where esc.casetype='"&caseType&"'")
                                       '     if not rs.eof then
                                       '         response.Write "<table cellpadding='10'>"
                                       '         do while not rs.eof 
                                       '             response.Write("<tr>")
                                       '             response.Write("    <td><img style='width:30px;' onclick='parent.setSelectedCase("""&rs("caseSku")&""", """&ebay_system_sku&""")' src='https://www.lucomputers.com/pro_img/components/"& rs("caseSku") &"_t.jpg' ></td>")
                                       '             response.Write("    <td>"& rs("id") &"</td>")
                                       '             response.Write("    <td>"& rs("caseSku") &"</td>")
                                       '             response.Write("    <td>"& rs("caseSku2") &"</td>")
                                       '             response.write("    <td>"& rs("product_short_name") &"("&rs("product_current_cost")&")</td>")
                                       '             response.Write("</tr>")
                                       '         rs.movenext
                                       '         loop
                                       '         response.Write "</table>"
                                       '     end if
                                       '     rs.close : set rs  = nothing
                                            
                                    %>
                                </td>
                            </tr>
                        </table>
                    </form>
                </td>
            </tr>
        </table>
        <div style="float: right; position: absolute; right: 10px; top: 10px;">
            <img src="<%=caseImages %>" width="300" /><br />
            <iframe src="ebay_system_item_specifics.asp?system_sku=<%=ebay_system_sku %>" style="width: 500px; height: 500px;"></iframe>
        </div>

    </div>

    <%
end if
     
	 
	 
	 function GetEbayPartGroup(categoryIDS, part_group_id)
	 	dim rs,  i, str
		dim categoryID 
		str = ""
		if instr(categoryIDS, "|")>0 then 
			dim cates
			cates = split(categoryIDS, "|")
			for i=lbound(cates) to ubound(cates)
				if cates(i) <> "378" then 
					categoryID = cates(i)
				end if
			next
		else
			categoryID = categoryIDS 
		end if

		'Response.write "Select part_group_id, part_group_name from tb_part_group where is_ebay=1 and showit=1"
		set rs = conn.execute("Select part_group_id, part_group_comment from tb_part_group where product_category='"& categoryID &"' and is_ebay=1  and showit=1 order by part_group_comment asc")
		if not rs.eof then
			str = str & "<option value='-1'>None</option>" &vblf
			do while not rs.eof 
				str = str & "<option value='"& rs("part_group_id") &"' "
				if cstr(part_group_id) = cstr(rs("part_group_id")) then 
					str = str &  " selected='selected' "
				end if
				str = str & ">"& rs("part_group_comment") &"</option>" &vblf
				response.write rs("part_group_id")
			rs.movenext
			loop
		end if
		rs.close :set rs = nothing
		
		GetEbayPartGroup = str
	 end function
     
        closeconn() %>

    <script type="text/javascript">

        $().ready(function () {
            $('td[title=keyword_comment]').css('text-align', 'right').css("font-weight", "bold").css("width", "120px").css("padding-right", "10px");
            $('table[title=keyword_area]').find("div").css("float", "left").css("width", "120px");

            $('ul[title=shipping_charge]').css("width", "100%").find("li").css("float", "left");
            $('hr').css("border", "0");
            $('div.title').css("padding", "5px").css("background", "#f2f2f2").css("border", "1px solid #cccccc").css("margin", "1em 0 1em 0");
            //$("ul[title=shipping_charge] li:first-child").css("width", "150px");

            $('#main_logo_area').load('/q_admin/inc/get_sys_logo_list.aspx', function () {
                $('#main_logo_area div').hover(function () {
                    $(this).css("padding", "0").css("border", "1px solid #ccc");

                }
                    , function () {
                        $(this).css("border", "0px solid #ccc").css("padding", "1px");
                    }).css("cursor", "pointer").click(
                        function () {
                            var fv = $(this).find('img').attr("title");
                            var v = $('input[name=logo]').val();
                            if (v.length > 4) {
                                if (v.indexOf(fv) == -1) {
                                    v = v + "|" + fv;
                                }
                            }
                            else
                                v = fv;
                            $('input[name=logo]').val(v);

                            // view img
                            viewLogo();

                        }
                    );
            });

            // view system comment to Label
            $('#system_comment_span').html($('input[name=system_name]').val());

            // view logo img
            viewLogo();


            // ebay setting
            if ("<%= ebay_system_sku %>".length == 6) {
                $('#ifr_ebay_setting').attr("src", "/q_admin/ebayMaster/lu/ebay_system_item_specifics.asp?system_sku=<%= ebay_system_sku %>");
            }
            //alert($("#ifr_main_frame1", window.parent.document).css("height"));
            var _attr = parseInt($("#ifr_main_frame1", window.parent.document).css("height"));
            $('#ifr_ebay_setting').css({ "width": "100%" }).css("height", isNaN(_attr) || _attr <= 52 ? "100%" : (_attr - 52) + "px");

            // btn_edit_summary
            if ("<%= ebay_system_sku %>".length == 6) {
                $('#btn_edit_summary').css({ 'height': '67px', 'width': '97px' }).bind('click', function () {
                    js_callpage_name_custom('/q_admin/ebayMaster/ebay_system_modify_summary.asp?system_sku=<%= ebay_system_sku %>', 'sys_part_preview', 600, 600);
                });
            }
            else
                $('#btn_edit_summary').css({ 'display': 'none' });


            // price
            //GetSysEbayPriceBySysCost();
            GetEbaySysPrices();


            $('#system_title_auto').load('../ebay_cmd.aspx?cmd=getSysAutoTitle&sku=<%= ebay_system_sku %>', '', function (msg) {
                $('#system_title_auto').val(msg);
            });

            //OpenDuplicateSys
            if ('<%=Request("dupiCaseSku")%>' !== '') {
                OpenDuplicateSys('<%= ebay_system_sku%>', '<%=Request("dupiCaseSku")%>');
            }

            function getElementSkus(obj) {
                var result = [];
                obj.each(function () {
                    if ($(this).context.checked) {
                        result.push($(this).val());
                    }
                });
                console.log(result);
                return result;
            }

            /**
             * duplicate system 
             * @param {number} sys_sku
             * @param {number} memorySku
             * @param {number} vcSku
             * @param {number} ssdSku
             * @param {number} ssdM2Sku
             * @param {number} hddSku
             */
            function openDuplicateSysMemory(sys_sku, memorySku, vcSku, ssdSku, ssdM2Sku, hddSku) {
                if (sys_sku) {
                    var src = '/q_admin/ebayMaster/ebay_system_Duplicate_sys.asp?isCurrPage=1&hddSku=' + hddSku + '&ssdM2Sku=' + ssdM2Sku + '&ssdSku=' + ssdSku + '&vcSku=' + vcSku + '&memorySku=' + memorySku + '&sys_sku=' + sys_sku + '&category_id=<%=category_id %>';
                    console.log(src);
                    window.open(src);
                }
            }

            $('.btnSubmitDuplicateSystem').on('click', function () {
                var vcElements = $('.vcItem');
                var memoryElements = $('.memoryItem');
                var ssdElements = $('.ssdItem');
                var ssdM2Elements = $('.ssdM2Item');
                var hddElements = $('.hddItem');

                var vcArray = getElementSkus(vcElements),
                    memoryArray = getElementSkus(memoryElements),
                    ssdArray = getElementSkus(ssdElements),
                    ssdM2Array = getElementSkus(ssdM2Elements),
                    hddArray = getElementSkus(hddElements);

                var sysSku = "<%= ebay_system_sku %>";

                for (var i = 0; i < memoryArray.length; i++) {
                    openDuplicateSysMemory(sysSku, memoryArray[i], '', '', '', '');

                    for (var j = 0; j < ssdArray.length; j++) {
                        openDuplicateSysMemory(sysSku, memoryArray[i], '', ssdArray[j], '', '');

                        for (var x = 0; x < vcArray.length; x++) {
                            openDuplicateSysMemory(sysSku, memoryArray[i], vcArray[x], ssdArray[j], '', '');
                        }
                    }
                    for (var j = 0; j < ssdM2Array.length; j++) {
                        openDuplicateSysMemory(sysSku, memoryArray[i], '', '', ssdM2Array[j], '');

                        for (var x = 0; x < vcArray.length; x++) {
                            openDuplicateSysMemory(sysSku, memoryArray[i], vcArray[x], '', ssdM2Array[j], '');
                        }
                    }
                    for (var j = 0; j < hddArray.length; j++) {
                        openDuplicateSysMemory(sysSku, memoryArray[i], '', '', '', hddArray[j]);
                    }
                    for (var j = 0; j < vcArray.length; j++) {

                        openDuplicateSysMemory(sysSku, memoryArray[i], vcArray[j], '', '', '');
                    }
                }

                for (var i = 0; i < vcArray.length; i++) {
                    openDuplicateSysMemory(sysSku, '', vcArray[i], '', '', '');

                    for (var j = 0; j < ssdM2Array.length; j++) {
                        openDuplicateSysMemory(sysSku, '', vcArray[i], '', ssdM2Array[j], '');
                    }
                    for (var j = 0; j < hddArray.length; j++) {
                        openDuplicateSysMemory(sysSku, '', vcArray[i], '', '', hddArray[j]);
                    }
                    for (var j = 0; j < ssdArray.length; j++) {
                        openDuplicateSysMemory(sysSku, '', vcArray[i], ssdArray[j], '', '');
                    }
                }

                for (var i = 0; i < ssdArray.length; i++) {
                    openDuplicateSysMemory(sysSku, '', '', ssdArray[i], '', '');
                }
                for (var i = 0; i < ssdM2Array.length; i++) {
                    openDuplicateSysMemory(sysSku, '', '', '', ssdM2Array[i], '');
                }
                for (var i = 0; i < hddArray.length; i++) {
                    openDuplicateSysMemory(sysSku, '', '', '', '', hddArray[i]);
                }

            });

            //
            $('.ebay_part_group_detail_box').each(function () {
                console.log($(this));
                var _this = $(this);
                $.get("ebay_system_cmd_custom.asp", { cmd: "PartGroupDetail", part_group_id: _this.data('group-id'), part_group_detail: _this.data('luc-sku') }, function (response) {
                    _this.html(response)
                    _this.find('select').each(function () {
                        $(this).attr('onChange', 'return changeEbayPartDetail($(this));');
                        changeEbayPartDetail($(this));
                    });
                });
            });

            function createPartGroupTempList() {
                $.get("ebay_system_cmd_custom.asp", { cmd: "CreatePartGroupDetail" }, function () {
                    alert('ok')
                });
            }

            $('.createPartGroupTempList').on('click', function () {
                createPartGroupTempList();
            })
        });


        function viewLogo() {
            //   var v = $('input[name=logo]').val();
            //   var vh = "";
            //   if(v.length>4)
            //   {
            //       if(v.indexOf("|")==-1)
            //       {
            //            vh = vh + "<img src='/pro_img/logo/"+ v +"'>";
            //       }
            //       else
            //       {
            //            var s = v.split("|");
            //            
            //            for(var x=0; x<s.length; x++)
            //            {   
            //                vh = vh + "<img src='/pro_img/logo/"+ s[x] +"'>";
            //            }
            //       }
            //   }
            //   $('#main_logo_view').html(vh);
        }

        function SetKeywordValue(the) {
            var n_v = '[' + the.value + ']';
            if (the.checked) {
                var v = $('input[name=sys_keywords]').val();
                $('input[name=sys_keywords]').val(v + n_v);

            }
            else {
                var v = $('input[name=sys_keywords]').val();
                if (v.indexOf('[' + the.value + ']') != -1) {
                    $('input[name=sys_keywords]').val(v.replace(n_v, ''));
                }
            }

        }


        function IssueSystem(sys_sku) {
            $('#iframe1').attr('src', '/q_admin/ebayMaster/ebay_system_edit_issue.asp?sys_sku=' + sys_sku)
        }


        function OpenDuplicateSys(sys_sku, caseSku) {
            if (caseSku || confirm('are you sure')) {
                if (caseSku === undefined || caseSku === '' || caseSku === null) {
                    $('#iframe1').attr("src", '/q_admin/ebayMaster/ebay_system_Duplicate_sys.asp?sys_sku=' + sys_sku + '&category_id=<%=category_id %>');
                }
                else {
                    $('#iframe1').attr("src", '/q_admin/ebayMaster/ebay_system_Duplicate_sys.asp?caseSku=' + caseSku + '&sys_sku=' + sys_sku + '&category_id=<%=category_id %>');
                }
            }
        }


        function ValidPost() {
            var rowCount = 0;
            //    $('input[name=luc_part_name]').each(function(){
            //        var v = $(this).val();
            //        if(v.indexOf(',') != -1)
            //        {
            //            v = v.replace(/,/gi, '[qiozi-comma]');
            //            
            //        }
            //        $(this).val(v);

            //    });
            $('select[name=ebay_part_group]').each(function () { rowCount += 1; });
            if ("[10][15][16][17][18][19][20][21]".indexOf("[" + rowCount + "]") > -1)
                return true;
            else {
                alert("Group Quantity is error.");
                return false;
            }
        }

        function changeEbayPartGroup(the) {
            //alert(the.val());

            var part_group_id = parseInt(the.attr("id").replace("ebay_part_group_", ""));


            $("#td_ebay_part_group_detail_" + part_group_id).load(
                "ebay_system_cmd_custom.asp",
                {
                    "cmd": "PartGroupDetail", "part_group_id": the.val(), "part_group_detail": null
                },
                function () {
                    $(this).find('select').each(function () {
                        $(this).attr('onChange', 'return changeEbayPartDetail($(this));');
                        changeEbayPartDetail($(this));
                    });
                });

            return false;
        }

        function editSysPartGroup(system_sku) {
            return js_callpage_name_custom("/q_admin/ebayMaster/lu/ebay_system_edit_comment.asp?system_sku=" + system_sku, 'ebay_comm', 750, 800);
        }

        function editPartOfGroup(e) {
            var href = e.attr('href');
            var part_group_id = 0;
            e.parent().parent().find('select').each(function () {
                if ($(this).attr('name') == 'ebay_part_group')
                    part_group_id = $(this).val();
            });
            js_callpage_name_custom(href + '?part_group_id=' + part_group_id, 'sys_part_preview' + part_group_id, 1150, 700);

        }

        function viewSysPage(system_sku) {
            js_callpage_name_custom('/q_admin/ebayMaster/ebay_system_temp_page_view.aspx?new=1&system_sku=' + system_sku, 'sys_preview', 1050, 700);


        }

        function editAllPartPrice() {
            var ids = '';
            $('input[name=part_group_id_f]').each(function () {
                if (ids == '')
                    ids = $(this).val();
                else
                    ids += ',' + $(this).val();
            });
            js_callpage_name_custom("ebay_system_edit_part_of_group.asp" + '?part_group_id=' + ids, 'sys_part_preview', 1050, 700);

        }

        function GetSysEbayPriceBySysCost() {
            var cost = $('#cost').val();
            var Adjustment = $('#adjustment').val();

            $.ajax({
                type: "Get",
                url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                data: "cmd=GetSysEbayPriceBySysCost&Cost=" + cost + "&Adjustment=" + Adjustment,
                success: function (msg) {
                    $('#Sell').val(msg);
                }
                , error: function (msg) { alert(msg); }
            });
        }

        function GetEbaySysPrices() {
            if ('<%= ebay_system_sku %>' != '') {
                var sysSKU = '<%= ebay_system_sku %>';
                var includeShipping = $('input[name=flashType][checked]').val() == 3 ? 1 : 0;// $('#is_include_shipping').attr('checked')?1:0;

                $.ajax({
                    type: "Get",
                    url: "/q_admin/ebaymaster/ebay_system_cmd.aspx",
                    data: "cmd=GetEbaySysPrices&systemsku=" + sysSKU + "&includeShipping=" + includeShipping + "&is_shrink=<% if is_shrink = 1 or is_barebone=1 then response.write 1 else response.write 0%>&" + rnd(),
                    success: function (msg) {

                        var json = eval(msg);
                        $.each(json, function (idx, item) {

                            $('#flash_price').html(item.all_ebay_price_real + " - " + item.selected_ebay_price_real);

                            $('#cost').val(item.all_cost);
                            $('#sys_web_price').val(item.all_web_price);
                            $('#adjustment').val(item.adjustment);
                            $('#all_ebay_price').val(item.all_ebay_price);
                            $('#ebay_profit_1').val(item.all_profits);
                            $('#ebay_fee_1').val(item.all_ebay_fee);
                            $('#shipping_fee_1').val(item.all_shipping_fee);
                            $('#ebay_system_price_real_1').val(item.all_ebay_price_real);

                            $('#selected_cost').val(item.selected_cost);
                            $('#selected_ebay_price').val(item.selected_ebay_price);
                            $('#ebay_profit_2').val(item.selected_profits);
                            $('#ebay_fee_2').val(item.selected_ebay_fee);
                            $('#shipping_fee_2').val(item.selected_shipping_fee);
                            $('#ebay_system_price_real_2').val(item.selected_ebay_price_real);

                            $('#no_selected_cost').val(item.no_selected_cost);
                            $('#no_selected_ebay_price').val(item.no_selected_ebay_price);
                            $('#ebay_profit_3').val(item.no_selected_profits);
                            $('#ebay_fee_3').val(item.no_selected_ebay_fee);
                            $('#shipping_fee_3').val(item.no_selected_shipping_fee);
                            $('#ebay_system_price_real_3').val(item.no_selected_ebay_price_real);

                            $('#btn_modify_sold_to_ebay').bind('click', function () {
                                return ModifyPriceToEbay(item.all_ebay_price, '<%= itemid %>');
                            }).removeAttr("disabled");

                            $('#btn_modify_desc_to_ebay').bind('click', function () {
                                return ModifyDescToEBay(item.all_ebay_price, '<%= itemid %>');
                            }).removeAttr("disabled");

                            $('#btn_modify_ebay_title_logo').bind('click', function () {
                                return ModifyTitleToEBay('<%= itemid %>');
                            }).removeAttr("disabled");

                            $('#btn_modify_shipping_fee').bind('click', function () {
                                return ModifyShippingFeeToEBay('<%= itemid %>', sysSKU);
                            }).removeAttr("disabled");


                        });
                    }
                    , error: function (msg) { alert(msg); }
                });
            }
        }

        function editKeywords(sysSku) {

            ShowIframe('Edit Keyword', '/q_admin/product_system_custom_change_keywords.asp?sys_sku=' + sysSku, 900, 550);
        }

        function editCategorys(sysSku) {

            ShowIframe('Edit Category', '/q_admin/product_system_custom_change_categorys.asp?sys_sku=' + sysSku, 700, 550);
        }

        parent.setSelectedCase("", "");
    </script>
    <%
    if(request("viewLeft") = "true")then
        Response.Write "<script  type=""text/javascript"">"
        Response.Write "    parent.$('#ifr_left_frame1').attr('src','/q_admin/eBayMaster/lu/ebay_left_menu.asp?cid="& category_id &"');"
        Response.Write "</script>"
    else
       ' response.Write "NO"
    end if
    %>
</body>
</html>
