<html>
<head>
    <title></title>
</head>
<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->

<%

        dim category_id, cmd, system_sku
        system_sku 	= SQLescape(request("system_sku"))
        cmd 		= SQLescape(request("cmd"))

        dim itemtext, itemvalue, itemTextArray, itemValueArray
        itemtext 	= SQLescape(request("title"))
        itemvalue	= SQLescape(request("titleValue"))
            response.Write itemtext &"<br>"
            response.Write itemvalue
        if cmd = "saveItemSpecifics" then
            itemTextArray = split(itemtext, ",")
            itemValueArray = split(itemvalue, ",")

            if system_sku <> "" then 
                conn.execute("delete from tb_ebay_system_item_specifics where system_sku ='"&system_sku&"'")
                for i=lbound(itemValueArray) to ubound(itemValueArray)
                    if trim(itemValueArray(i)) <> "" then 
                        conn.execute("insert into tb_ebay_system_item_specifics "&_
                                    " (system_sku, ItemSpecificsName, ItemSpecificsValue "&_
                                    "	)"&_
                                    "	values "&_
                                    "	( '"&system_sku&"', '"&trim(itemTextArray(i))&"', '"&replace(trim(itemValueArray(i)), "~", ",")&"')")

                    end if
                next
            end if
        end if
 %>


<%       
        closeconn() %>
        <script type="text/javascript">
            alert("OK");
        </script>
</body>
</html>
