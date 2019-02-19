<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Cmd</title>
</head>
<body>
     <%
     
     
        dim category_id, cmd, ebay_system_sku, isShowWarnSys
        dim sku
        dim sysBgColor 
        sysBgColor                      =       ""
        cmd                             =      SQLescape(request("cmd"))
        category_id                     =      SQLescape(request("category_id"))
        sku                             =      SQLescape(request("sku"))
        isShowWarnSys                   =      SQLescape(request("isShowWarnSys"))
        
        if(cmd = "ShowALL") then
            conn.execute("Update tb_ebay_system set is_issue = 1 where category_id = "& category_id)
            response.Write "<script >        alert(""It is OK"");</script>"
        end if
        
        if (cmd = "HiddenALL") then
            conn.execute("Update tb_ebay_system set is_issue = 0 where category_id = "& category_id)
            response.Write "<script >        alert(""It is OK"");</script>"
        end if

        if (cmd="closesys") then 
            conn.execute("Update tb_ebay_system set is_issue = 0, showit=0 where id = '"& sku & "'")
            response.clear()
            response.Write "It is OK"
            response.End()
        end if

        if (cmd = "reInitStock") then
            conn.execute("Update tb_timer set Status = 1, regdate=now() where Cmd = '18'")
            response.clear()
            response.Write "It is OK"
            response.End()
        end if

        if(cmd = "getEbaySysListByCid") then 
            dim whereSql
            whereSql = ""
            if (isShowWarnSys = "1") then 
                whereSql =  " inner join tb_ebay_system_warned esw on esw.systemSku=es.id "
            end if
            if (isShowWarnSys = "2") then 
                whereSql =  " inner join (select id from tb_ebay_system where date_format(now(),'%Y%M%d')=date_format(regdate,'%Y%M%d')) today on today.id=es.id "
            end if
            set sRs = conn.execute("select es.id, e.BuyItNowPrice,es.category_id"&_
                                            " ,es.cutom_label, es.ebay_system_price"&_
                                            " ,e.ItemID, es.adjustment, es.ebay_system_name"&_
                                            " ,es.is_barebone, es.is_shrink "&_
                                            " ,es.large_pic_name "&_
                                            " ,es.logo_filenames "&_
                                            " ,es.sub_part_quantity "&_
                                            " ,e.QuantityAvailable"&_
                                            " ,oeq.quantity sellqty"&_
                                            " ,es.is_disable_flash_customize"&_                                           
                                            " , e.sku"&_
                                            " ,ifnull(ps.isys, 0) isIndexSys"&_
                                            ", e.Title"&_
                                            " from tb_ebay_system es "&_
                                            " inner join tb_ebay_selling e on e.sys_sku=es.id "&_ 
                                            " left join tb_order_ebay_quantity oeq on oeq.itemid=e.ItemID"&_    
                                            whereSql &_
                                            " left join tb_pre_index_page_setting ps on ps.sku=es.id and ps.isys=1 "&_                                                           
                                            " where (es.is_from_ebay=0 or (es.is_from_ebay=1 and es.is_shrink=0)) and es.category_id='"& category_id &"' and es.showit=1 "&_
                                            " order by e.sku asc")
                                                                       
            if not sRs.eof then
                   
                    s = s & "<li><table>"
                    s = s & "       <tr>"	&vblf
                    s = s & "           <td width=""80""></td>"	&vblf
                    s = s & "           <td width='100' style='text-align:center; ' nowarp>System SKU</td>"	&vblf
                    s = s & "           <td width='90' style='text-align:center;"& sysBgColor & "'>Custom Label</td>"	&vblf
                    s = s & "           <td width='60' style='text-align:center;"& sysBgColor & "'>Flash Price</td>"	&vblf
                    s = s & "           <td width='60' style='text-align:center;"& sysBgColor & "'>BuyItNowPrice</td>"	&vblf
                    s = s & "           <td width='60' style='text-align:center;"& sysBgColor & "'>Diff</td>"	&vblf
                    s = s & "           <td width='100' style='text-align:center;"& sysBgColor & "'>ItemID</td>"	&vblf
                    s = s & "           <td width='60' style='text-align:center;"& sysBgColor & "'>Sys Cost</td>"	&vblf
                    s = s & "           <td width='60' style='text-align:center;"& sysBgColor & "'>Adjustment</td>"	&vblf
                    s = s & "           <td width='80' style='text-align:center;"& sysBgColor & "'><span style='color:blue'>New</span> BuyItNow</td>"	&vblf
                    s = s & "           <td width='60' style='text-align:center;"& sysBgColor & "'>Diff</td>"	&vblf
                    s = s & "           <td width='105' style='text-align:center;"& sysBgColor & "'>Modify Price</td>"	&vblf
                    s = s & "           <td width='70' style='text-align:center;"& sysBgColor & "'>Modify Desc</td>"	&vblf
                    s = s & "           <td width='80' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='80' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='30' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='30' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='30' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='40' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='100' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "           <td width='100' style='text-align:center;"& sysBgColor & "'></td>"	&vblf
                    s = s & "       </tr>"	&vblf
                    do while not srs.eof 
                        if(sRs("isIndexSys") = 1 ) then 
                            sysBgColor = "background: #ccc; font-weight:bold; "
                        else
                            sysBgColor = ""
                        end if
                        dim cutom_label 
                        if(trim(srs("sku")) = "")then
                            cutom_label = trim(sRs("cutom_label"))
                        else
                            cutom_label = srs("sku")
                        end if
                        cutom_lable = ""
                        if cint(srs("QuantityAvailable"))<4 then 
                            QuantityAvailable = "<b style='color:red;'>"&srs("QuantityAvailable")&"</b>"
                        else
                            QuantityAvailable = srs("QuantityAvailable")
                        end if

                        if srs("is_shrink") = 1 or srs("is_barebone") = 1 then 
                            is_shrink = 1 
                        else
                            is_shrink = 0
                        end if
                        
                            s =     s & "<tr onmouseover=""this.bgColor='#f2f2f2';"" onmouseout=""this.bgColor='#ffffff';"" name='itemdata' adjustment='"&srs("adjustment")&"' BuyItNowPrice='"&sRs("BuyItNowPrice")&"' rid='"&sRs("ID")&"' itemid='"&sRS("ItemID")&"' is_shrink='"&is_shrink&"'>"	&vblf      
                            s =     s & "   <td  nowrap=""nowrap""> "&vblf
                            s =     s & "   <a href='/q_admin/ebayMaster/Online/ModifyOnlineAutoPay.aspx?issystem=1&sku="& srs("id") &"&itemid="&sRS("itemid")&"' id='a"&sRS("itemid")&"' tag='"&sRS("itemid")&"' title='modify auto pay' target='_blank'>A</a>&nbsp;"&vblf
                            s =     s &    QuantityAvailable 
                            s =     s & "   <a href='/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?itemid=" + sRS("ItemID") + "&issystem=1&LogoPrictureUrl=.jpg' id='modifyLogo"& sRs("id") &"' title='modify logo' target='_blank'>MP</span>"&vblf
                            s =     s & "   <span sku='"& sRs("id") &"' itemid='" + sRS("ItemID") + "' name='prictureHref'><a href='http://www.lucomputers.com/ebay/"& srs("logo_filenames") &".jpg' target='_blank'>p</a></span>"
                            s =     s & " <span style='color:blue;' id=""barebone_"& srs("id") &""">"
                            if srs("is_barebone") = 1 then 
                                s =     s & "B"
                            end if
                            s =     s & "</span>"&vblf
                            s =     s & "   </td> "&vblf                                                               
                            s =     s & "   <td nowrap=""nowrap""> "&vblf
                            s =     s & "     <span class=""file"" ><a href='/q_admin/ebayMaster/ebay_system_temp_page_view.aspx?new=1&system_sku="&sRs("id")&"' onclick=""return js_callpage_cus(this.href, 'view_page', 1050, 750);"">"& sRs("id") &" ("& sRs("sub_part_quantity") &")</a></td>" &vblf
                            s =     s & "   <td width='250' nowrap=""nowrap""><span >"& cutom_label &"</span></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right;"& sysBgColor & "'>"& srs("ebay_system_price") &"</td>"   &vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right;"& sysBgColor & "'><span name='buyItNowPrice'> $" & sRs("BuyItNowPrice") &" </span></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right; color: blue;'>"
								if cstr(srs("ebay_system_price")) <> cstr(sRs("BuyItNowPrice")) then 
									s =     s & formatnumber(cdbl(srs("ebay_system_price")) - cdbl(sRs("BuyItNowPrice")),2) 
								end if
							s =     s & " </td>"   &vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:center;"& sysBgColor & "'><a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="&sRs("ItemID")&"' target='_blank'>"& srs("ItemID") & "</a></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right;"& sysBgColor & "'><span name='cost' id='cost_"&sRs("id")&"'>...</span></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right;"& sysBgColor & "'><span name='adjustment'>"& srs("adjustment") &"</span></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right;"& sysBgColor & "'><span id='newEbayPrice"&sRs("id")&"' buyItNowPrice='" & sRs("BuyItNowPrice") &"' cost='0' adjustment='"& srs("adjustment") &"'>...</span></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:right;"& sysBgColor & "' id='newDiff"&sRs("id")&"'></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:center;"& sysBgColor & "'  id='modifyEbayPrice"& sRs("id") &"'>[eBay Price]</a></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap"" style='text-align:center;"& sysBgColor & "'  id='modifyEbayDesc"& sRs("id") &"'>[eBay desc]</a></td>"	&vblf
							's =     s & "   <script> "&vblf
                            's =     s & "       InfoArray["& JsArrayIndex &"][0] = '"& srs("adjustment") &"'; "&vblf
                            's =     s & "       InfoArray["& JsArrayIndex &"][1] = '"& sRs("BuyItNowPrice") &"'; "&vblf
                            's =     s & "       InfoArray["& JsArrayIndex &"][2] = '"& sRs("ID") &"'; " &vblf 
                            's =     s & "       InfoArray["& JsArrayIndex &"][3] = '"& sRS("ItemID") &"'; "&vblf 
                            's =     s & "       InfoArray["& JsArrayIndex &"][4] = '"& is_shrink & "'; "&vblf
                            's =     s & "   </script> "&vblf
							
                            s =     s & "   <td nowrap=""nowrap""><a href='ebay_system_modify_summary.asp?system_sku="&sRs("id") &"' onclick="" $(this).css({'color':'white', 'background':'black'}); return js_callpage_cus(this.href, 'change_currency', 900, 550);"">[summary]</a></td>"	&vblf
							s =     s & "   <td nowrap=""nowrap""><a href='lu/ebay_system_edit_2.asp?IsParent=false&ebay_system_sku="&sRs("id") &"&category_id="&sRs("category_id") &"&cmd=modify' onclick="" $(this).css({'color':'white', 'background':'black'});return js_callpage_cus(this.href, 'change_currency', 950, 650);"">[detail]</a></td>"	&vblf
																
                            s =     s & "   <td  nowrap=""nowrap""><a id=""modifyShipping_"&srs("id")&""" href='/q_admin/ebayMaster/Online/ModifyOnlineShippingFee.aspx?sku="& srs("id") &"&itemid="& srs("ItemID") &"' title='Modify Shipping Fee' onclick=""if(confirm('Are you sure.')){js_callpage_cus(this.href, 'ebay_modify_shipping_"& srs("id")& "', 1000, 400); $(this).css({'color':'white', 'background':'black'});}return false;""><b>S</b></a></td>"&vblf
							s =     s & "   <td  nowrap=""nowrap"" id='warn"& sRs("id") &"'></td>"	&vblf
                            s =     s & "   <td nowrap=""nowrap""><a href=""/q_admin/ebayMaster/online/EndItem.aspx?itemid="&srs("ItemID")&""" onclick=""if(confirm('are you sure')){js_callpage_cus(this.href, 'ebay_part_end_"& srs("itemid") &"', 780, 500);} return false;"" target='_blank'> End </a></td>" &vblf
                            s =     s & "   <td nowrap=""nowrap""><span id='generateXmlFile"&sRs("id")&"' sku='"&sRs("id")&"'></span></td>" &vblf
                            s =     s & "   <td nowrap=""nowrap"">"& srs("sellqty") &"</td>"&vblf                        
                                s =     s & "   <td style='color:green;'></td>"&vblf     
                                s =     s & "   <td></td>"&vblf                       
                            if srs("is_disable_flash_customize") = 1 then
                                s =     s & "   <td nowrap=""nowrap"" style='color:red;'>"& srs("is_disable_flash_customize") &"</td>"&vblf
                            else 
                                s =     s & "   <td nowrap=""nowrap""r></td>"&vblf
                            end if
                            s =     s & "<td nowrap=""nowrap"">"& srs("Title") &"</td>"&vblf
                            s =     s & "</tr>"	&vblf
																
                            JsArrayIndex = JsArrayIndex + 1 
                    srs.movenext
                    loop
                    s =     s & "</table></li>" &vblf
            end if
            srs.close : set sRs = nothing
            response.Clear()
            response.write s
            response.End()
        end if
        
        closeconn() %>
        

</body>
</html>
