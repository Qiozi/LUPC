
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Part Price Change Settings</title>
    <script type="text/javascript" src="/JS_css/jquery_lab/jquery-1.3.2.min.js"></script>
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
<iframe src="" frameborder="0" id="iframe1" name="iframe1" style="border:0px solid red; width:110px; height:110px;"></iframe>
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
                        " where menu_pre_serial_no=0 and tag=1 and page_category=1 order by menu_child_order asc")
    if not rs.eof then
        do while not rs.eof 
            Response.Write "<h4 style='clear:both;'>"& rs("menu_child_name") &"</h4>"          
            
            set crs = conn.execute("select menu_child_serial_no, menu_child_name "&_
                                " from tb_product_category "&_
                                " where menu_pre_serial_no='"& rs("menu_child_serial_no")&"' and tag=1 "&_
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
                        " where menu_pre_serial_no=0 and tag=1 and page_category=1 order by menu_child_order asc")
    if not rs.eof then
        do while not rs.eof 
            Response.Write "<h3>"& rs("menu_child_name") &"</h3>"          
            
            set crs = conn.execute("select menu_child_serial_no, menu_child_name, eBayCategoryID, ebayStoreCategoryID_1 "&_
                                " from tb_product_category "&_
                                " where menu_pre_serial_no='"& rs("menu_child_serial_no")&"' and tag=1 "&_
                                " order by menu_child_order asc ")
            if not crs.eof then
                do while not crs.eof
                    response.Write "<form action=""part_ebay_shipping_setting_exec.asp"" target=""iframe1"" method=""post"" name='from"& crs("menu_child_serial_no") &"'>"
                    Response.Write "<a name='"& crs("menu_child_serial_no")&"'></a>"
                    Response.Write "<ul>"
                    Response.Write "<li>"
                    Response.Write "<div name='cmd_area'>"
                    Response.Write "<h4 style='padding-left:0px;'>"
                    
                    Response.Write  crs("menu_child_name") 
                    Response.Write "</h4>"
                   
                    'Response.write "    <input type='button' value='Default' onclick=""defaultItem('"& crs("menu_child_serial_no") &"');"">"
                   ' Response.Write "    <input type='button' value='New' onclick="" newItem('"& crs("menu_child_serial_no") &"'); "">"
                    Response.Write "    <input type='submit' value='Save' >"
                    response.Write "    <input type='hidden' value='"& crs("menu_child_serial_no")&"' name='cid'>"
                    Response.Write "    <a style='float:right;' href='#top' >Top</a>"
                    Response.Write "</div>"
                       
                    eBayCategoryID = crs("eBayCategoryID")
                    eBayStoreCategoryID = crs("ebayStoreCategoryID_1")
                    CategoryID = crs("menu_child_serial_no")
                    CBPickup = ""
                    CBPickupFree = 0
                    UPSStandardCanad = ""
                    UPSStandardCanadFree = 0
                    UPSExpeditedCanada = ""
                    UPSExpeditedCanadaFree = 0
                    UPSStandardUnitedStates = ""
                    UPSStandardUnitedStatesFree = 0
                    UPS3DaySelectUnitedStates = ""
                    UPS3DaySelectUnitedStatesFree = 0
                    UPSWorldWideExpedited = ""
                    UPSWorldWideExpeditedFree = 0
                    UPSWorldWideExpress = ""
                    UPSWorldWideExpressFree = 0
                    CA_StandardShipping = ""
                    CA_StandardShippingFree = 0
                    shortCategoryName = ""
                    CA_ExpeditedInternational = ""
                    CA_ExpeditedInternationalFree = 0
                    CA_StandardInternational  = ""
                    CA_StandardInternationalFree  = 0
                    CA_EconomyToUS = ""
                    CA_EconomyToUSFree = 0
                    CA_StandardToUS=""
                    CA_StandardToUSFree = 0

                    set srs = conn.execute("select 	ID, shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName"&_	 
	                                       " from "&_
	                                       " tb_ebay_shipping_settings where CategoryID = '"& crs("menu_child_serial_no") &"' ")
                    
                    if not srs.eof then
                        do while not srs.eof 
                             shortCategoryName = srs("ShortCategoryName")
                            if( srs("shippingCompany") = "CA_Pickup") then
                                CBPickup = srs("shippingFee")
                                CBPickupFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_UPSStandardCanada") then
                                UPSStandardCanad = srs("shippingFee")
                                UPSStandardCanadFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_UPSExpeditedCanada") then
                                UPSExpeditedCanada = srs("shippingFee")
                                UPSExpeditedCanadaFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_UPSStandardUnitedStates") then
                                UPSStandardUnitedStates = srs("shippingFee")
                                UPSStandardUnitedStatesFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_UPS3DaySelectUnitedStates") then
                                UPS3DaySelectUnitedStates = srs("shippingFee")
                                UPS3DaySelectUnitedStatesFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_UPSWorldWideExpedited") then
                                UPSWorldWideExpedited = srs("shippingFee")
                                UPSWorldWideExpeditedFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_UPSWorldWideExpress") then
                                UPSWorldWideExpress = srs("shippingFee")
                                UPSWorldWideExpressFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_StandardShipping") then 
                                CA_StandardShipping = srs("shippingFee")
                                CA_StandardShippingFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_ExpeditedInternational") then 
                                CA_ExpeditedInternational = srs("shippingFee")
                                CA_ExpeditedInternationalFree = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_StandardInternational") then 
                                CA_StandardInternational  = srs("shippingFee")
                                CA_StandardInternationalFree  = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_EconomyToUS") then 
                                CA_EconomyToUS  = srs("shippingFee")
                                CA_EconomyToUSFree  = srs("IsFree")
                            end if
                            if( srs("shippingCompany") = "CA_StandardToUS") then 
                                CA_StandardToUS  = srs("shippingFee")
                                CA_StandardToUSFree  = srs("IsFree")
                            end if
                            
                        srs.movenext
                        loop
                    end if
                    srs.close : set srs = nothing
                    'response.Write UPSWorldWideExpressFree
                    %>
                        <table>
                            
                            <input type="hidden" name="categoryid" value="<%= CategoryID %>" />
                            <tr>
                                <td>Short Category Name</td>
                                <td><input type="text" name="shortCategoryName" value="<%= shortCategoryName %>"/></td>
                                <td>eBay Category<input type="text" name="ebayCategoryID" id="ebayCategoryID<%= CategoryID %>" value="<%= eBayCategoryID %>" readonly="readonly" />
                                <input type="button" value="change" onclick="ShowIframe('Change eBay CategoryId','/q_admin/ebaymaster/online/get_categories.aspx?IsChild=1&ParentElement=ebayCategoryID<%= CategoryID %>',700,450);"/></td>
                                <td>Store Category </td>
                                <td><select name="storeCategory"></select><input type="hidden" name="hiddenStoreCategory" id="hiddenStoreCategory" value="<%= eBayStoreCategoryID %>" /></td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td><td>
                                &nbsp;</td>
                                <td class="style1">
                                    &nbsp;</td>
                                <td>Economy Shipping to United States</td>
                                <td>
                                    <input id="Text11" type="text" name="CA_EconomyToUS" 
                                        value="<%= CA_EconomyToUS %>" /></td>
                                <td>
                                    <input id="Checkbox11" name="CBCA_EconomyToUS" type="checkbox"  
                                        <% if CA_EconomyToUSFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td><td>
                                &nbsp;</td>
                                <td class="style1">
                                    &nbsp;</td>
                                <td>Standard Shipping to United States</td>
                                <td>
                                    <input id="Text12" type="text" name="CA_StandardToUS" 
                                        value="<%= CA_StandardToUS %>" /></td>
                                <td>
                                    <input id="Checkbox12" name="CBCA_StandardToUS" type="checkbox"  
                                        <% if CA_StandardToUSFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                            </tr>
                            <tr>
                                <td>Pickup</td><td>
                                <input id="Text1" type="text" name="CA_Pickup"  value="<%= CBPickup %>" /></td>
                                <td class="style1">
                                    <input id="Checkbox2" name="CBCA_Pickup" type="checkbox" <% if CBPickupFree = 1 then response.write "checked='checked'" %> />Free shipping</td>
                                <td>UPS Standard UnitedStates</td>
                                <td>
                                    <input id="Text4" type="text" name="CA_UPSStandardUnitedStates" value="<%= UPSStandardUnitedStates %>" /></td>
                                <td>
                                    <input id="Checkbox5" name="CBCA_UPSStandardUnitedStates" type="checkbox"  <% if UPSStandardUnitedStatesFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                            </tr>
                            <tr>
                                <td>CA Economy Shipping&nbsp;</td><td>
                                <input id="Text8" type="text" name="CA_StandardShipping"  
                                    value="<%= CA_StandardShipping %>" /></td>
                                <td class="style1">
                                    <input id="Checkbox8" name="CBCA_StandardShipping" type="checkbox"  
                                        <% if CA_StandardShippingFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                                <td>UPS 3Day Select UnitedStates</td>
                                <td>
                                    <input id="Text5" type="text" name="CA_UPS3DaySelectUnitedStates"  value="<%= UPS3DaySelectUnitedStates %>"/></td>
                                <td>
                                    <input id="Checkbox1" name="CBCA_UPS3DaySelectUnitedStates" type="checkbox" 
                                        <% if UPS3DaySelectUnitedStatesFree = 1 then response.write "checked='checked'" %> 
                                        disabled="disabled"  />Free 
                                    shipping</td>
                            </tr>
                            <tr>
                                <td>UPS Standard Canada</td><td>
                                <input id="Text2" type="text" name="CA_UPSStandardCanada"  value="<%= UPSStandardCanad %>" /></td>
                                <td class="style1">
                                    <input id="Checkbox3" name="CBCA_UPSStandardCanada" type="checkbox"  <% if UPSStandardCanadFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                                <td>Standard Int&#39;l Flat Rate Shipping</td>
                                <td>
                                    <input id="Text9" type="text" name="CA_StandardInternational" 
                                        value="<%= CA_StandardInternational %>" /></td>
                                <td>
                                    <input id="Checkbox9" name="CBCA_StandardInternational" type="checkbox"  
                                        <% if CA_StandardInternationalFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                              
                            </tr>
                            <tr>
                                <td>UPS Expedited Canada</td><td>
                                <input id="Text3" type="text" name="CA_UPSExpeditedCanada"  value="<%= UPSExpeditedCanada %>"/></td>
                                <td class="style1">
                                    <input id="Checkbox4" name="CBCA_UPSExpeditedCanada" type="checkbox"  <% if UPSExpeditedCanadaFree = 1 then response.write "checked='checked'" %>/>Free 
                                    shipping</td>
                                <td>Expedited Int&#39;l Flat Rate Shipping</td>
                                <td>
                                    <input id="Text10" type="text" name="CA_ExpeditedInternational" 
                                        value="<%= CA_ExpeditedInternational %>" /></td>
                                <td>
                                    <input id="Checkbox10" name="CBCA_ExpeditedInternational" type="checkbox"  
                                        <% if CA_ExpeditedInternationalFree = 1 then response.write "checked='checked'" %> />Free 
                                    shipping</td>
                              
                            </tr>
                            <tr>
                                <td>&nbsp;</td><td>
                                &nbsp;</td>
                                <td class="style1">
                                    &nbsp;</td>
                                <td>UPS World WideExpedited</td>
                                <td>
                                    <input id="Text6" type="text" name="CA_UPSWorldWideExpedited" value="<%= UPSWorldWideExpedited %>" /></td>
                                <td>
                                    <input id="Checkbox6" name="CBCA_UPSWorldWideExpedited" type="checkbox" 
                                        <% if UPSWorldWideExpeditedFree = 1 then response.write "checked='checked'" %> 
                                        disabled="disabled"/>Free 
                                    shipping</td>
                              
                            </tr>
                            <tr>
                                <td>&nbsp;</td><td>
                                &nbsp;</td>
                                <td class="style1">
                                    &nbsp;</td>
                                <td>UPS World WideExpress</td>
                                <td>
                                    <input id="Text7" type="text" name="CA_UPSWorldWideExpress" value="<%= UPSWorldWideExpress %>" /></td>
                                <td>
                                    <input id="Checkbox7" name="CBCA_UPSWorldWideExpress" type="checkbox" 
                                        <% if UPSWorldWideExpressFree = 1 then response.write "checked='checked'" %> 
                                        disabled="disabled" />Free 
                                    shipping</td>
                            </tr>
                            <tr>
                                <td>&nbsp;</td><td>&nbsp;</td>
                                <td class="style1">&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                                <td>&nbsp;</td>
                            </tr>
                           
                        </table>
                     <%
                    
                    response.Write "    </li>"
                    Response.Write "</ul>"
                response.Write " </form>"
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
        

</script>
</body>
</html>
