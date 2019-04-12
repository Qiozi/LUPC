<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Edit Part Comment</title>
    <script type="text/javascript" src="../JS/lib/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popup.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popupclass.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="../../js_css/b_lu.css" />
    <script type="text/javascript" src="../JS/WinOpen.js"></script>
</head>
<body>
    <table>
        <tr>
            <td style="padding: 1rem;">
                <%
    Dim sku : sku = SQLescape(request("sku"))
    response.Write "<a href='https://www.lucomputers.com/detail_part.aspx?sku="& sku &"' target='_blank'>to web page</a>"
    Dim short_name
    Dim middle_name
    Dim long_name
    Dim cost 
    Dim sell
    Dim mfp_name
    Dim mfp
    Dim ebay_comment
    Dim ebay_name
    dim tpl_summary_id
    Dim showit
    Dim eBayItemId
    Dim ebay_note
    Dim canIssue
    Dim CategoryID
    Dim IssueComment
    Dim SettingComment
    Dim n
    Dim product_ebay_name_2
    Dim screenSize
    Dim eBayShippingCategory
    Dim ImgNameSku
    Dim weight
    dim IsNotebook
    Dim UPC
    canIssue = false
    SettingComment = "<a href='part_ebay_shipping_setting2.asp' target='_blank'>Setting</a> Shipping Fee."
    Set rs = conn.execute("Select p.*, pc.is_noebook from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no = p.menu_child_serial_no where product_serial_no=" & SQLquote(sku) &"")
    
    if not rs.eof then 
        short_name      =       rs("product_short_name")
        middle_name     =       rs("product_name")
        long_name       =       rs("product_name_long_en")
        cost            =       rs("product_current_cost")
        sell            =       cdbl(rs("product_current_price"))-cdbl(rs("product_current_discount"))
        mfp_name        =       rs("producter_serial_no")
        mfp             =       rs("manufacturer_part_number")
        ebay_name       =       rs("product_ebay_name")
        CategoryID      =       rs("menu_child_serial_no")
        product_ebay_name_2=       rs("product_ebay_name_2")
        screenSize      =       rs("screen_size")
        if rs("other_product_sku") <>"" and rs("other_product_sku") <> "0" then 
            ImgNameSku = rs("other_product_sku")
        else
            ImgNameSku = rs("product_serial_no")
        end if
        weight          =       rs("weight")
        IsNotebook      =       rs("is_noebook")
        UPC             =       rs("UPC")
    end if
    rs.close : set rs = nothing
    
    ' read 
    Set rs = conn.execute("Select * from tb_ebay_part_comment where part_sku='"& sku &"'")
    if not rs.eof then
        ebay_comment    =   rs("ebay_comment")
        tpl_summary_id  =   rs("tpl_summary_id")
        showit          =   rs("showit")
        ebay_note       =   rs("ebay_note")
    end if
    rs.close : set rs = nothing
    
    
    
    Response.write "<h3>"& sku & "</h3>"
    response.write "<span><b>" & short_name &"</b></span><br>"
    response.write "<span style='color:#cccccc;'>"& middle_name&"</span><br>"
    response.write "<span style='color:#cccccc;'><i>"& long_name &"</i></span><br>"
    response.write "<span style='color:green;'>MFP#: "& mfp & "</span><br>"
    response.write "<hr size='1'>"
    
    ' edit comment
    Response.write "<p style='padding:10px;'>"
    Response.write "<form action='ebay_part_comment_edit_exec.asp' method='post' target='iframe100' id='form1'>"
    Response.write "<input type='hidden' name='sku' value='"& sku &"'>"
    response.write "<table><tr><td></td><td><label>"&vblf
    
    Response.write "<input type='checkbox' name='showit' value='1'"
    if showit = 1 then response.write " checked='checked' "
    response.write " >Showit</label>"
    Response.write "</td></tr>"&vblf
    Response.write "<tr><td>"&vblf
    Response.write "Ebay Name:"&vblf
    Response.write "</td><td>"
    Response.write "<input type='text' name='ebay_name' value='"& ebay_name &"' size='100' onblur=""validateTitle($(this));"" onkeyUp=""changeTxt($(this),$('#ENArea1'));""><span id='ENArea1'>" & len(ebay_name) &"</span><span id='validateTitleIsUse'>...</span>"
    Response.write "</td></tr>"&vblf
    Response.write "<tr><td>"&vblf
    Response.write "Ebay Short Name:"&vblf
    Response.write "</td><td>"
    Response.write "<input type='text' name='ebay_name_2' value='"& product_ebay_name_2 &"' size='100' maxlength=80 onkeyUp=""changeTxt($(this),$('#ENArea2'));""><span id='ENArea2'>" & len(product_ebay_name_2) &"</span>"& " <input type='text' size='5' name='screenSize' value='"& screenSize &"'>&nbsp;&nbsp;only notebook"
    Response.write "</td></tr>"&vblf
    Response.write "<tr><td>"&vblf
    
    Response.write "Comment: "&vblf
    Response.write "</td><td>"&vblf
    Response.write "<input type='text' name='ebay_comment' value='"& ebay_comment &"' size='100'>"&vblf
    response.write "</td></tr>"&vblf
    Response.write "<tr><td>"&vblf
    Response.write "Note: "&vblf
    Response.write "</td><td>"&vblf
    Response.Write "<input type='text' name='ebay_note' maxlength='100' value="""& ebay_note &""" size='100'>"&vblf
    Response.write "</td></tr>"&vblf
    Response.write "<tr><td>"&vblf
    Response.write "UPC: "&vblf
    Response.write "</td><td>"&vblf
    Response.Write "<input type='text' name='UPC' maxlength='12' value="""& UPC &""" size='100'>"&vblf
    Response.write "</td></tr>"&vblf

    Response.write "<tr><td>"&vblf    
    response.write "Templete &nbsp;Summany"&vblf
    Response.write "</td><td>"&vblf
    Response.write "<select name='ebay_part_tpl_summary'>"&vblf    
    set rs = conn.execute("Select id, comment from tb_ebay_templete_sub_summary ")
    if not rs.eof then  
        Response.write "<option value='0'>select</option>"&vblf
        do while not rs.eof 
            Response.write "<option value='"& rs("id") &"' "
            if tpl_summary_id = rs("id") then Response.write " selected='selected' "
            Response.write ">"& rs("comment") &"</option>"&vblf
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing
    Response.write "</select>"&vblf
    Response.write "</td></tr>"&vblf
    Response.write "<tr><td></td><td></td></tr></table>"&vblf
    Response.Write "<hr size='0' style='border:1px solid #f2f2f2;'>"

    Response.Write "<div style='text-align:center;padding:1em;'>"
    REsponse.write "<input type='submit' value='Save'>"
    Response.Write "<input type='button' value='Save And Next' id='btn_save_and_next' />"
    Response.Write "</div>"
    response.write "</form>"
    Response.write "</p>"
                %>
                <hr size="1" />
                <h4 style="color: Blue;">current Price : $<%= sell %><br />
                    weight:
        <%= weight %></h4>
                <%
        set rs = conn.execute("select ShippingCategoryId from tb_part_and_shipping where sku='"&sku&"' limit 1")
        if not rs.eof then
            eBayShippingCategory = rs(0)
        end if
        rs.close
       
        set rs = conn.execute("select distinct categoryId , min(shippingFee) shippingFee,ShortCategoryName from tb_ebay_shipping_settings where categoryid like '"&CategoryID&"-%' and shippingFee>0 group by categoryid order by shippingFee asc")
        if not rs.eof then 
            do while not rs.eof 
                response.Write("<label><input type='radio' name='ship' value='"& rs("categoryId") &"' onclick='cateChange();' ")
                if eBayShippingCategory = rs("categoryId") then response.Write (" checked='checked' ")
                response.Write(" >"& rs("categoryId") & "-- "& rs("ShortCategoryName") &"($"& rs("shippingFee") & ")</label><br/>")
               ' if eBayShippingCategory = rs("categoryId") then response.Write ("1") else response.Write("0") end if
            rs.movenext
            loop
        else
            rs.close : set rs = nothing
            closeconn()
            response.Write("no setting shipping")
            response.Write SettingComment
            response.end
        end if
        rs.close : set rs = nothing
                %>
            </td>
            <td style="padding: 1rem;">
                <table>
                    <tr>
                        <td>
                            <br />
                            <h3>
                                <label>
                                    <input type="checkbox" name="issueToEbay" style="display:none;" />Issue to eBay</label>
                                <table cellspacing="10">
                                    <tr>
                                        <td colspan="2">eBay Category
                            <br />
                                            <input type="text" name="ebayCategoryID" id="ebayCategoryID<%= CategoryID %>" value="<%= Session("ebayCategoryID") %>"
                                                readonly="readonly" />
                                            <input type="text" size="53" name="ebayCategoryName" id="ebayCategoryID<%= CategoryID %>Name"
                                                value="<%= Session("ebayCategoryName") %>" />
                                            <input type="button" value="change" onclick="ShowIframe('Change eBay CategoryId', '/q_admin/ebaymaster/online/get_categories.aspx?IsChild=1&ParentElement=ebayCategoryID<%= CategoryID %>', 700, 450);" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <% 
                                    set rs = conn.execute("select * from tb_ebay_category_part_category where CategoryID='"& CategoryID &"'")
                                    if not rs.eof then
                                        do while not rs.eof 
                                            response.Write "<label><input type='radio' name='ec' onclick='setEBayCategory("""& rs("eBayCategoryID") &""", """& rs("eBayCategoryName") &""");'>" & rs("eBayCategoryName") &"</label><br>"
                                        rs.movenext
                                        loop
                                    end if
                                    rs.close : set rs = nothing
                                            %>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Store Category
                            <br />
                                            <select name="storeCategory" id="storeCategory">
                                            </select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">Store Category2
                            <br />
                                            <select name="storeCategory2" id="storeCategory2">
                                            </select>
                                        </td>
                                    </tr>
                                </table>
                                <%
                    set rs = conn.execute("Select itemid from tb_ebay_selling where luc_sku='"& sku &"' limit 0,1")
                    if not rs.eof then
                        eBayItemId = rs(0)
                        response.Write "Current Itemid : "& eBayItemId &" &nbsp; <button class='btnModifyTitleToEbay' data-itemid='"&eBayItemId&"' data-sku='"&sku&"'> Modify Title On eBay.</button>"
                    end if
                    rs.close : set rs = nothing

                    set rs = conn.execute("Select ShortCategoryName from tb_ebay_shipping_settings where categoryid='"& eBayShippingCategory &"'")
                    if not rs.eof then 
                         IssueComment = "Custom Label: "&  rs(0) & " "& mfp_name &" "& Sku 
                         if( len( rs(0) & " "& mfp_name &" "& Sku )>30) then 
                            IssueComment = IssueComment &"<span style='color:red;'>("& rs(0) & " "& mfp_name &" "& Sku &")</span>"                         
                         end if
                         canIssue = true
                    end if
                    rs.close
                                %>
                            </h3>
                            <br />
                            <%= IssueComment %>
                            <br />
                            <input type="button" name="issueSubmit" value="Post to eBay" <% if not canIssue then  response.write "disabled=""disabled""" %>
                                onclick="issueToEbay();" /><span id='partToEbayLoading'></span>
                            <br />
                            <%= SettingComment %>
                            <br />
                        </td>
                        <td>
                            <img src="https://www.lucomputers.com/pro_img/components/<%=ImgNameSku %>_g_1.jpg" width="300" />
                        </td>
                    </tr>
                </table>
                <hr size="1" />
                <% 
    set rs = conn.execute("select ebay_code,regdate from tb_ebay_code_and_luc_sku where sku='"& sku &"' order by id desc limit 1")
    if not rs.eof then
        Response.write "<p class='pd'>"
        n =1
        do while not rs.eof 
            
            Response.write "<a href=""http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="& rs("ebay_code") &""" target=""_blank""> "&rs("ebay_code")&"</a> &nbsp;"& rs("regdate") 
            if n = 1 then response.Write "&nbsp;&nbsp;&nbsp;<a href='/q_admin/ebayMaster/online/modifyOnlineShippingFee.aspx?sku="+ sku +"&itemid=" + rs("ebay_code") + "' target='_blank' >modify shipping</a>"
            response.Write "<br>"
            n = n + 1
        rs.movenext
        loop
        Response.write "</p>"
    end if
    rs.close : set rs = nothing 
closeconn()
                %>
            </td>
        </tr>
    </table>

    <hr size="1" />
    <div style="display: flex">
        <div style="flex-grow: 1">
            <iframe id="iframeSpecific" name="iframeSpecific" src="/q_admin/manager/product/editSpecifics.aspx" style="width: 98%; height: 90%; border: 1px solid #ccc;"></iframe>
        </div>
        <div style="flex-grow: 1">
            <iframe id="iframeSpecific2" name="iframeSpecific2" src="/q_admin/manager/product/Specifics.aspx?sku=<%=sku %>" style="width: 98%; height: 90%; border: 1px solid #ccc;"></iframe>
        </div>
    </div>

    <iframe name='iframe100' id='iframe100' style='width: 110px; height: 110px;' frameborder="0"></iframe>
    <script type="text/javascript">
        //function
        $().ready(function () {
            $('#btn_save_and_next').bind('click', function () {
                //$('#form1').attr('action', 'ebay_part_comment_edit_exec.asp?cmd=next');
                $('#form1').submit();
            });

            $('.btnModifyTitleToEbay').bind('click', function () {
                modifyTitleOnEbay($(this).attr('data-itemid'));
            });

            bindStoreCategory();
        });

        function modifyTitleOnEbay(itemid) {
            if (itemid == '')
                return;
            var href = "/q_admin/ebayMaster/Online/ModifyOnlinePrice.aspx?itemid=" + itemid + "&issystem=0&LogoPrictureUrl=.jpg";

            if (confirm('are you sure?\r\nitemid: ' + itemid)) {
                js_callpage_cus(href, 'ebay_title_' + itemid, 300, 200);
            }
            return false;
        }

        function validateTitle(the) {
            $.get('ebay_cmd.aspx', { cmd: 'validateItemTitle', sku: '<%= sku %>', title: the.val() }, function (msg) { $('#validateTitleIsUse').html(msg); }, "text");
        }

        function issueToEbay() {
            var sku = '<%= sku %>';
            var ebayCategoryID = $('#ebayCategoryID<%= CategoryID %>').val();;
            var ebayCategoryName = $('#ebayCategoryID<%= CategoryID %>Name').val();;
            var storeCategory1 = $('#storeCategory').val();;
            var storeCategory2 = $('#storeCategory2').val();

            if (ebayCategoryName.indexOf("&") > -1)
                ebayCategoryName = ebayCategoryName.replace("&", "#");

            $('#partToEbayLoading').html("load....");
            $.ajax({
                type: "Get",
                url: "/q_admin/ebayMaster/online/ebay_part_to_ebay_transit.asp",
                data: "sku=" + sku + "&ebayCategoryID=" + ebayCategoryID + "&ebayCategoryName=" + ebayCategoryName + "&storeCategory1=" + storeCategory1 + "&storeCategory2=" + storeCategory2,
                success: function (msg) {
                    $('#partToEbayLoading').html("eBay ItemId: <a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item=" + msg + "' target='_blank'>" + msg + "</a> &nbsp;&nbsp;&nbsp;<a href='online/modifyOnlineShippingFee.aspx?sku=" + sku + "&itemid=" + msg + "' target='_blank' >modify shipping</a> ");

                    if ("<%= IsNotebook %>" == "1") {

                        js_callpage_cus("/q_admin/ebayMaster/online/modifyOnlineShippingFee.aspx?sku=" + sku + "&itemid=" + msg, "modifyShipping" + sku, 400, 400);
                    }
                }
                , error: function (msg) {
                    alert(msg);
                }
            });
        }

        function bindStoreCategory() {
            $.ajax({
                type: "get",
                url: "/q_admin/ebaymaster/ebay_system_cmd.aspx?cmd=GetEBayStoreCategoryString",
                data: "",
                success: function (msg) {
                    $(msg).appendTo($('select[name=storeCategory]'));


                    $('select[name=storeCategory]').each(function () {
                        var id = '<%= Session("storeCategory1") %>';
                        $(this).val(id);

                    });

                    $(msg).appendTo($('select[name=storeCategory2]'));


                    $('select[name=storeCategory2]').each(function () {
                        var id = '<%= Session("storeCategory2") %>';
                        $(this).val(id);

                    });

                },
                error: function (msg) { alert('error:' + msg); }
            });
        }

        function changeTxt(the, note) {

            note.html(the.val().length)
        }

        function setEBayCategory(id, text) {
            $('#ebayCategoryID<%= CategoryID %>').val(id);
            $('#ebayCategoryID<%= CategoryID %>Name').val(text);
            $(window.frames["iframeSpecific"].document).find('.btnReloadCategorySpecifices').data('cid', id);
            $('#iframeSpecific').attr('src', '/q_admin/manager/product/editSpecifics.aspx?sku=<%= sku %>&cid=' + id);
            //console.log($(window.frames["iframeSpecific"].document).find('.btnReloadCategorySpecifices').data('cid'));
        }

        function cateChange() {

            //alert($('input[name=ship]:checked').val());
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "WM.aspx/SavePartShipping",
                data: "{shipCate:'" + $('input[name=ship]:checked').val() + "',sku:'<%= sku %>'}",
                dataType: "json",
                success: function (result) {
                    if ("ERROR" == result.d.Result) {
                        alert(result.d.Result + ": " + result.d.Message);
                    }
                    else {
                        alert(result.d.Result);
                        window.location.href = "";
                    }
                }
            });

        }
    </script>
</body>
</html>
