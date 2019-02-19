<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
</head>
<body>
<%

    Dim sku
    Dim ebayCategoryID
    Dim ebayCategoryName
    Dim storeCategory1
    Dim storeCategory2

    sku = request("sku")
    ebayCategoryID = request("ebayCategoryID")
    ebayCategoryName = request("ebayCategoryName")
    storeCategory1 = request("storeCategory1")
    storeCategory2 = request("storeCategory2")

    Session("ebayCategoryID") = ebayCategoryID
    Session("ebayCategoryName") = ebayCategoryName
    Session("storeCategory1") = storeCategory1
    Session("storeCategory2") = storeCategory2

    set rs = conn.execute("select * from tb_ebay_category_part_category where eBayCategoryID='"& ebayCategoryID &"'")
    if not rs.eof then
        
    else
        set crs = conn.execute("Select menu_child_serial_no from tb_product where product_serial_no='"& sku &"'")
        if not crs.eof then
        conn.execute("insert into tb_ebay_category_part_category(CategoryID, eBayCategoryID, eBayCategoryName) values ('"&crs("menu_child_serial_no")&"', '"&ebayCategoryID&"', '"&ebayCategoryName&"')")
        end if
        crs.close : set crs = nothing

    end if
    rs.close : set rs = nothing
    closeconn()
    response.Redirect("ebay_part_to_ebay.aspx?sku=" + sku + "&ebayCategoryID=" + ebayCategoryID + "&ebayCategoryName=" + ebayCategoryName + "&storeCategory1=" + storeCategory1 + "&storeCategory2=" + storeCategory2)


 %>
</body>
</html>
