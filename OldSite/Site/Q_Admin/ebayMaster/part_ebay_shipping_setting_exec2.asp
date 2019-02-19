<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Cmd</title>
</head>
<body>
     <%
       
        Dim categoryid
        Dim shortCategoryName
        Dim shippingServiceDetail, shippingServiceDetailArray
        Dim shippingCost, shippingCostArray
        Dim checkedFree, checkedFreeArray

        response.Write(SQLescape(request("categoryid")) & "<br>")
        response.Write(SQLescape(request("shortCategoryName")) & "<br>")
        response.Write(SQLescape(request("shippingServiceDetail")) & "<br>")
        response.Write(SQLescape(request("shippingCost")) & "<br>")
        response.Write(SQLescape(request("checkedFree")) & "<br>")

        categoryid = SQLescape(request("categoryid"))
        shortCategoryName = SQLescape(request("shortCategoryName"))
        shippingServiceDetail = SQLescape(request("shippingServiceDetail"))
        shippingCost = SQLescape(request("shippingCost"))
        checkedFree = SQLescape(request("checkedFree"))

        shippingServiceDetailArray  = split( shippingServiceDetail, ",")
        shippingCostArray           = split( shippingCost, ",")
        

        conn.execute("delete from tb_ebay_shipping_settings where CategoryID='"+ categoryid +"'")
        for i=lbound(shippingServiceDetailArray) to ubound(shippingServiceDetailArray)
            
            if(trim(shippingServiceDetailArray(i)) <> "-1") then

                if instr(checkedFree, trim(shippingServiceDetailArray(i))) > 0 then 
                    checkedFreeArray = 1
                else
                    checkedFreeArray = 0
                end if

                if(trim(shippingCostArray(i)) <> "") then 
                    shippingCost = trim(shippingCostArray(i))
                else
                    shippingCost = 0
                end if

                conn.execute("insert into tb_ebay_shipping_settings(shippingFee, shippingCompany, IsFree, CategoryID, ShortCategoryName, regdate) values ('"& shippingCost &"', '"& trim(shippingServiceDetailArray(i)) &"','"& checkedFreeArray &"','"& categoryid &"','"& shortCategoryName &"',now())")
            end if
        next

        closeconn() %>
        <script >
            alert("Save is ok.");
        </script>
</body>
</html>
