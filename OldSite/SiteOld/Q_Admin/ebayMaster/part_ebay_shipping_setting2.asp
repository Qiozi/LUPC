
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Part Price Change Settings</title>
    <script type="text/javascript" src="../../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popup.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popupclass.js"></script>
    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 256px;
        }
    </style>
</head>
<body>
<iframe src="" frameborder="0" id="iframe1" name="iframe1" style="border:0px solid red; width:0px; height:0px;"></iframe>
<a name='top'></a>

<%

    Dim CBPickup, CBPickupFree
    Dim UPSStandardCanad, UPSStandardCanadFree
    Dim UPSExpeditedCanada, UPSExpeditedCanadaFree
    Dim UPSStandardUnitedStates, UPSStandardUnitedStatesFree
    Dim UPS3DaySelectUnitedStates, UPS3DaySelectUnitedStatesFree
    Dim UPSWorldWideExpedited, UPSWorldWideExpeditedFree
    Dim UPSWorldWideExpress, UPSWorldWideExpressFree
    Dim CA_StandardShipping, CA_StandardShippingFree
    Dim CA_StandardInternational, CA_StandardInternationalFree
    Dim CA_ExpeditedInternational, CA_ExpeditedInternationalFree
    Dim CA_EconomyToUS, CA_EconomyToUSFree
    Dim CA_StandardToUS, CA_StandardToUSFree
    Dim CategoryID
    Dim eBayCategoryID
    Dim eBayStoreCategoryID

    Set rs = conn.execute("Select menu_child_serial_no , menu_child_name "&_
                        " from tb_product_category "&_
                        " where menu_pre_serial_no=0 and tag=1 order by menu_child_order asc")
    if not rs.eof then
        do while not rs.eof 
            Response.Write "<h4 style='clear:both;'>"& rs("menu_child_name") &"</h4>"          
            
            set crs = conn.execute("select menu_child_serial_no, menu_child_name "&_
                                " from tb_product_category "&_
                                " where menu_pre_serial_no='"& rs("menu_child_serial_no")&"' and tag=1 and page_category=1 "&_
                                " order by menu_child_order asc ")
            if not crs.eof then
                do while not crs.eof
                    response.Write "<a name='top_c_name' href='#"& crs("menu_child_serial_no") &"'>"& crs("menu_child_name") &"</a>"
                    
                crs.movenext
                loop
            end if
            crs.close : set crs = nothing
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing
 %>
 <hr size='1' style="clear:both;">
<%
    Set rs = conn.execute("Select menu_child_serial_no , menu_child_name, eBayCategoryID, ebayStoreCategoryID_1 "&_
                        " from tb_product_category "&_
                        " where menu_pre_serial_no=0 and tag=1  order by menu_child_order asc")
    if not rs.eof then
        do while not rs.eof 
            Response.Write "<h3>"& rs("menu_child_name") &"</h3>"          
            
            set crs = conn.execute("select menu_child_serial_no, menu_child_name, eBayCategoryID, ebayStoreCategoryID_1 "&_
                                " from tb_product_category "&_
                                " where menu_pre_serial_no='"& rs("menu_child_serial_no")&"' and tag=1 and page_category=1 "&_
                                " order by menu_child_order asc ")
            if not crs.eof then
                do while not crs.eof
                    Response.Write "<a name='"& crs("menu_child_serial_no")&"'></a>"
                    Response.Write "<ul>"
                    Response.Write "<li>"
                    Response.Write "<div name='cmd_area'>"
                    Response.Write "<h4 style='padding-left:0px;'>"
                    Response.Write "<input type='button' value='new' onclick=""newShipping('"& crs("menu_child_serial_no")&"');"">"
                    Response.Write  crs("menu_child_name") 
                    
                    Response.Write "</h4>"
                   
                    'Response.write "    <input type='button' value='Default' onclick=""defaultItem('"& crs("menu_child_serial_no") &"');"">"
                   ' Response.Write "    <input type='button' value='New' onclick="" newItem('"& crs("menu_child_serial_no") &"'); "">"
                    Response.Write " "
                    Response.Write "    <a style='float:right;' href='#top' >Top</a>"
                    Response.Write "</div>"
                       
                    eBayCategoryID = crs("eBayCategoryID")
                    eBayStoreCategoryID = crs("ebayStoreCategoryID_1")
                    CategoryID = crs("menu_child_serial_no")
                    CBPickup = ""
                    CBPickupFree = 0                    
                    shortCategoryName = ""

                    set csrs = conn.execute("select distinct categoryid, ShortCategoryName from tb_ebay_shipping_settings where categoryId like '"& crs("menu_child_serial_no") &"-%' order by categoryid asc")
                    if not csrs.eof then
                        
                        do while not csrs.eof       
  
                    'response.Write UPSWorldWideExpressFree
                    response.Write "<form action=""part_ebay_shipping_setting_exec2.asp"" target=""iframe1"" method=""post"" name='from"& crs("menu_child_serial_no") &"'>"
                   
                    %>
                        <table cellpadding="3" cellspacing="0"> 
                            <tr>
                                <td style="background:#DAE3D1;">CategoryID <input type="text" readonly="readonly" name="categoryid" value="<%= csrs("categoryid") %>" /></td>
                                <td style="background:#DAE3D1;">Short Category Name<input type="text" name="shortCategoryName" value="<%= csrs("ShortCategoryName") %>"/></td>
                                <td style="background:#DAE3D1;"><input type='submit' value='Save' ></td>
                            </tr>                            
                            <tr>
                                <td colspan="3">
                                <br />
                                    <div id="shippingArea_<%= csrs("categoryid") %>" title="<%= csrs("categoryid") %>" name="shippingServiceArea">
                                    </div>
                                </td>
                            </tr>                           
                        </table>
                     <%
                        response.Write " </form>"
                        set srs = conn.execute("select 	ID, shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName"&_	 
	                                           " from "&_
	                                           " tb_ebay_shipping_settings where CategoryID = '"& csrs("categoryid") &"' ")
                    
                        if not srs.eof then
                            do while not srs.eof 
                                response.Write("<script>")


                                response.Write("</script>")
                            srs.movenext
                            loop
                        end if


                        srs.close : set srs = nothing
                    csrs.movenext
                    loop
                    end if
                    csrs.close : set csrs = nothing

                    response.Write "    </li>"
                    Response.Write "</ul>"
                
                crs.movenext
                loop            
            end if
            crs.close : set crs = nothing
        
        rs.movenext
        loop
    
    end if
    rs.close :set rs = nothing


closeconn() %>

<script type="text/javascript">

    $().ready(function () {
        $('a[name=top_c_name]').css({ 'display': 'block', 'float': 'left', 'padding': '3px', 'margin-left': '5px' });
        $('h3').css({ 'font-size': '12pt' });
        $('h4').css({ 'color': 'green', 'font-weight': 'bold', 'font-size': '10pt' });
        $('div[name=cmd_area]').css({ 'padding': '3px 3px 3px 2em', 'background': '#f2f2f2', 'border': '1px solid #ccc' });
        $('input[type=text]').css({ 'text-align': 'right' });

        bindStoreCategory();

        getShippingDetailList();
        getShippingDetailValue();
       

        
    });


    function bindStoreCategory() {
        $.ajax({
            type: "get",
            url: "/q_admin/ebaymaster/ebay_system_cmd.aspx?cmd=GetEBayStoreCategoryString",
            data: "",
            success: function (msg) {
                $(msg).appendTo($('select[name=storeCategory]'));


                $('select[name=storeCategory]').each(function () {
                    var id = $(this).next().val();
                    $(this).val(id);

                });
            },
            error: function (msg) { alert('error:' + msg); }
        });
    }

    function getShippingDetailList() {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "WM.aspx/GetShippingServiceDetail",
            data: "",
            dataType: "json",
            success: function (result) {
                if ("ERROR" == result.d.Result) {
                    alert(result.d.Result + ": " + result.d.Message);
                }
                else {
                    var str = "<select name='shippingServiceDetail'>";
                    str += "<option value='-1'>Selected</option>";
                    $.each(result.d.Records, function (k, v) {
                        str += "<option value='" + v.Key + "'>" + v.Value + "</option>";
                    });
                    str += "</select>";

                    str += "<input type=\"text\" name=\"shippingCost\"  value=\"\" />";
                    str += " <input name=\"checkedFree\" type=\"checkbox\"/>Free shipping<br/>";
                    $('div[name=shippingServiceArea]').append(str);
                    $('div[name=shippingServiceArea]').append(str);
                    //$('div[name=shippingServiceArea]').append("");
                    $('div[name=shippingServiceArea]').append(str);
                    $('div[name=shippingServiceArea]').append(str);
                    //$('div[name=shippingServiceArea]').append("<br/>");
                    $('div[name=shippingServiceArea]').append(str);
                    $('div[name=shippingServiceArea]').append(str);
                    //$('div[name=shippingServiceArea]').append("<br/>");
                    $('div[name=shippingServiceArea]').append(str);
                    $('div[name=shippingServiceArea]').append(str);


                    $('select[name=shippingServiceDetail]').css({ 'margin-left': '15px' }).change(function () {
                        $(this).next().next().attr('value',$(this).val());
                    });
                }
            }
        });
    }

    function getShippingDetailValue() {
        $('div[name=shippingServiceArea]').each(function () {
            var the = $(this);
            //alert(the.attr('title'));
            $.ajax({
                type: "POST",
                contentType: "application/json",
                url: "WM.aspx/GetShippingServiceValue",
                data: "{categoryid:'" + the.attr('title') + "'}",
                dataType: "json",
                success: function (result) {
                    if ("ERROR" == result.d.Result) {
                        alert(result.d.Result + ": " + result.d.Message);
                    }
                    else {
                        for (var i = 0; i < result.d.Records.length; i++) {
                            setShippingDetailValue(the, i, result.d.Records[i]);

                        }
                       
                    }
                }

            });
        });
    }

    function setShippingDetailValue(the, index, result) {
        the.find("select").each(function (i) {
            if (i == index) {
                var cur = $(this);
                if (result.shippingCompany.length > 5) {
                    cur.val(result.shippingCompany);
                    cur.next().val(result.shippingFee);
                    if (result.IsFree) {
                        cur.next().next().attr("checked", true);
                    }
                    cur.next().next().attr("value", result.shippingCompany)
                }
            }
        });
    }

    function newShipping(cid) {
        $.ajax({
            type: "POST",
            contentType: "application/json",
            url: "WM.aspx/NewShipping",
            data: "{cid:'" + cid + "'}",
            dataType: "json",
            success: function (result) {
                if ("ERROR" == result.d.Result) {
                    alert(result.d.Result + ": " + result.d.Message);
                }
                else {
                    window.location.href = "?";
                }
            }
        });
    }
</script>
</body>
</html>
