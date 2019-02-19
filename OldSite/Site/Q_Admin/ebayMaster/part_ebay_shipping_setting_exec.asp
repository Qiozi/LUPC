<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Cmd</title>
</head>
<body>
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
        Dim ShortCategoryName 
        Dim eBayCategoryID
        Dim eBayStoreCategoryID
       

        eBayCategoryID                  = SQLescape(request("ebayCategoryID"))
        eBayStoreCategoryID             = SQLescape(request("storeCategory"))
        ShortCategoryName               = SQLescape(request("shortCategoryName"))

        CBPickup                        = SQLescape(request("CA_Pickup"))
        CBPickupFree                    = SQLescape(request("CBCA_Pickup"))
        if CBPickupFree <> "" then CBPickupFree = 1 else CBPickupFree = 0 end if

        UPSStandardCanad                = SQLescape(request("CA_UPSStandardCanada"))
        UPSStandardCanadFree            = SQLescape(request("CBCA_UPSStandardCanada"))
        if UPSStandardCanadFree <> "" then UPSStandardCanadFree = 1 else UPSStandardCanadFree = 0 end if

        UPSExpeditedCanada              = SQLescape(request("CA_UPSExpeditedCanada"))
        UPSExpeditedCanadaFree          = SQLescape(request("CBCA_UPSExpeditedCanada"))
        if UPSExpeditedCanadaFree <> "" then UPSExpeditedCanadaFree = 1 else UPSExpeditedCanadaFree = 0 end if

        UPSStandardUnitedStates         = SQLescape(request("CA_UPSStandardUnitedStates"))
        UPSStandardUnitedStatesFree     = SQLescape(request("CBCA_UPSStandardUnitedStates"))
        if UPSStandardUnitedStatesFree <> "" then UPSStandardUnitedStatesFree = 1 else UPSStandardUnitedStatesFree = 0 end if
        UPS3DaySelectUnitedStates       = SQLescape(request("CA_UPS3DaySelectUnitedStates"))
        UPS3DaySelectUnitedStatesFree   = SQLescape(request("CBCA_UPS3DaySelectUnitedStates"))
        if UPS3DaySelectUnitedStatesFree <> "" then UPS3DaySelectUnitedStatesFree = 1 else UPS3DaySelectUnitedStatesFree = 0 end if
        UPSWorldWideExpedited           = SQLescape(request("CA_UPSWorldWideExpedited"))
        UPSWorldWideExpeditedFree       = SQLescape(request("CBCA_UPSWorldWideExpedited"))
        if UPSWorldWideExpeditedFree <> "" then UPSWorldWideExpeditedFree = 1 else UPSWorldWideExpeditedFree = 0 end if

        UPSWorldWideExpress             = SQLescape(request("CA_UPSWorldWideExpress"))
        UPSWorldWideExpressFree         = SQLescape(request("CBCA_UPSWorldWideExpress"))
        if UPSWorldWideExpressFree <> "" then UPSWorldWideExpressFree = 1 else UPSWorldWideExpressFree = 0 end if

        CA_StandardShipping             = SQLescape(request("CA_StandardShipping"))
        CA_StandardShippingFree         = SQLescape(request("CBCA_StandardShipping"))
        if CA_StandardShippingFree <> "" then CA_StandardShippingFree = 1 else CA_StandardShippingFree = 0 end if

        
        CA_StandardInternational             = SQLescape(request("CA_StandardInternational"))
        CA_StandardInternationalFree         = SQLescape(request("CBCA_StandardInternational"))
        if CA_StandardInternationalFree <> "" then CA_StandardInternationalFree = 1 else CA_StandardInternationalFree = 0 end if

        CA_ExpeditedInternational             = SQLescape(request("CA_ExpeditedInternational"))
        CA_ExpeditedInternationalFree         = SQLescape(request("CBCA_ExpeditedInternational"))
        if CA_ExpeditedInternationalFree <> "" then CA_ExpeditedInternationalFree = 1 else CA_ExpeditedInternationalFree = 0 end if

        CA_EconomyToUS             = SQLescape(request("CA_EconomyToUS"))
        CA_EconomyToUSFree         = SQLescape(request("CBCA_EconomyToUS"))
        if CA_EconomyToUSFree <> "" then CA_EconomyToUSFree = 1 else CA_EconomyToUSFree = 0 end if
        
        CA_StandardToUS             = SQLescape(request("CA_StandardToUS"))
        CA_StandardToUSFree         = SQLescape(request("CBCA_StandardToUS"))
        if CA_StandardToUSFree <> "" then CA_StandardToUSFree = 1 else CA_StandardToUSFree = 0 end if
        
        CategoryID                      = SQLescape(request("categoryid"))

        conn.execute("Update tb_product_category set eBayCategoryID='"& eBayCategoryID &"', ebayStoreCategoryID_1='"& eBayStoreCategoryID &"' where menu_child_serial_no='"&CategoryID&"'")

        conn.execute("delete from tb_ebay_shipping_settings where categoryid='"& categoryID &"'")

        if CBPickup <> "" or CBPickupFree = 1 then
            if CBPickupFree = 1 and CBPickup = "" then CBPickup = 0
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName) values ('"& CBPickup & "', 'CA_Pickup', '"& CBPickupFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if CA_StandardShipping <> "" then
              conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& CA_StandardShipping & "', 'CA_StandardShipping', '"& CA_StandardShippingFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if


        if UPSStandardCanad <> "" or UPSStandardCanadFree = 1 then
            if UPSStandardCanadFree = 1 and UPSStandardCanad = "" then UPSStandardCanad = 0
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& UPSStandardCanad & "', 'CA_UPSStandardCanada', '"& UPSStandardCanadFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if UPSExpeditedCanada <> "" or UPSExpeditedCanadaFree = 1 then
            if UPSExpeditedCanadaFree = 1 and UPSExpeditedCanada = "" then UPSExpeditedCanada = 0
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& UPSExpeditedCanada & "', 'CA_UPSExpeditedCanada', '"& UPSExpeditedCanadaFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if CA_EconomyToUS <> "" or CA_EconomyToUSFree = 1 then
            if CA_EconomyToUSFree = 1 and CA_EconomyToUS = "" then CA_EconomyToUS = 0
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& CA_EconomyToUS & "', 'CA_EconomyToUS', '"& CA_EconomyToUSFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        
        if CA_StandardToUS <> ""  or CA_StandardToUSFree = 1 then
            if CA_StandardToUSFree = 1 and CA_StandardToUS = "" then CA_StandardToUS = 0
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& CA_StandardToUS & "', 'CA_StandardToUS', '"& CA_StandardToUSFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if UPSStandardUnitedStates <> ""  or UPSStandardUnitedStatesFree = 1 then
            if UPSStandardUnitedStatesFree = 1 and UPSStandardUnitedStates = "" then UPSStandardUnitedStates = 0
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& UPSStandardUnitedStates & "', 'CA_UPSStandardUnitedStates', '"& UPSStandardUnitedStatesFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if UPS3DaySelectUnitedStates <> ""  then
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& UPS3DaySelectUnitedStates & "', 'CA_UPS3DaySelectUnitedStates', '"& UPS3DaySelectUnitedStatesFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if CA_StandardInternational <> "" then
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& CA_StandardInternational & "', 'CA_StandardInternational', '"& CA_StandardInternationalFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if CA_ExpeditedInternational <> "" then
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& CA_ExpeditedInternational & "', 'CA_ExpeditedInternational', '"& CA_ExpeditedInternationalFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if


        if UPSWorldWideExpedited <> "" then
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& UPSWorldWideExpedited & "', 'CA_UPSWorldWideExpedited', '"& UPSWorldWideExpeditedFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if

        if UPSWorldWideExpress <> "" then
            conn.execute("Insert into tb_ebay_shipping_settings( shippingFee, shippingCompany, IsFree, CategoryID, regdate, ShortCategoryName ) values ('"& UPSWorldWideExpress & "', 'CA_UPSWorldWideExpress', '"& UPSWorldWideExpressFree &"', '"&CategoryID&"', now(), '"& ShortCategoryName &"')")
        end if



        closeconn() %>
        <script >
            alert("Save is ok.");
        </script>
</body>
</html>
