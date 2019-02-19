<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!DOCTYPE html>
<html>
<head>
    <title>Order Part List</title>
    <link href="../js_css/bootstrap/css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <script src="../js_css/bootstrap/js/bootstrap.min.js"></script>
        <script type="text/javascript" src="/q_admin/js/winOpen.js"></script>
    <style>
        #list td{font-size:9pt;}
    </style>
   
</head>
<body>
   <%
        Dim wholesaler, wholesalerArray
        Dim cost, costArray
        Dim note, noteArray
        Dim c:c="[comma]"
        Dim getID 
        Dim orders, ordersArray
        Dim newId
        Dim sku, skuArray
        Dim qty, qtyArray
        Dim subOrders, subOrdersArray
        Dim mfp, partName
        Dim detailId, detailIdArray

        orders      = SQLescape(request("orders"))
        getId       = SQLescape(request("getId"))
        wholesaler  = SQLescape(request("wholesaler"))
        cost        = SQLescape(request("cost"))
        note        = SQLescape(request("note"))
        sku         = SQLescape(request("sku"))
        qty         = SQLescape(request("qty"))
        subOrders   = SQLescape(request("subOrders"))
        detailId    = SQLescape(request("detailId"))

        wholesalerArray = split(wholesaler, ",")
        costArray       = split(cost, ",")
        noteArray       = split(note, ",")
        skuArray        = split(sku, ",")
        qtyArray        = split(qty, ",")
        subOrders       = split(subOrders, ",")
        detailIdArray   = split(detailId, ",")
        ordersArray          = split(orders, "|")

        if getid<>"" then
             for i=lbound(detailIdArray) to ubound(detailIdArray)
                if trim(detailIdArray(i)) <> "" then 

                    if (  trim(costArray(i)) = "") then  costArray(i) = 0
                    if instr(trim(noteArray(i)), "''") > 0 then noteArray(i) = replace(trim(noteArray(i)), "''", "\'")
                    'response.Write trim(noteArray(i))

                    conn.execute("update tb_part_getin_detail set wholesaler='"&trim(wholesalerArray(i))&"'"&_
                            ", cost='"&trim(costArray(i))&"'"&_
                            ", note='"&trim(noteArray(i))&"'"&_
                            " where id='"+ trim(detailIdArray(i)) +"'")

                    response.Write "update tb_part_getin_detail set wholesaler='"&trim(wholesalerArray(i))&"'"&_
                            ", cost='"&trim(costArray(i))&"'"&_
                            ", note='"&trim(noteArray(i))&"'"&_
                            " where id='"+ trim(detailIdArray(i)) +"'"
                end if
             next
        else
            
            conn.execute("insert into tb_part_getin(Regdate, Orders) values ( now(), '"+ orders +"')")
            set rs = conn.execute("select id from tb_part_getin order by id desc limit 0,1")
            if not rs.eof then
                newId = rs(0)
                for i=lbound(skuArray) to ubound(skuArray)
                    if trim(skuArray(i)) <> "" then 
                        response.Write ("select manufacturer_part_number mfp,product_name_long_en partName  from tb_product where product_serial_no='"& trim(skuArray(i)) &"'")
                        
                        set crs = conn.execute("select manufacturer_part_number mfp,product_name_long_en partName  from tb_product where product_serial_no='"& trim(skuArray(i)) &"'")
                        if not crs.eof then
                            mfp = crs("mfp")
                            partName = replace(crs("partName"), "'", "\'")
                        end if
                        crs.close: set crs = nothing
                       
                        if(  trim(costArray(i)) = "") then  costArray(i) = 0
                        if instr(trim(noteArray(i)), "''") > 0 then noteArray(i) = replace(trim(noteArray(i)), "''", "\'")
                        
                        conn.execute("insert into tb_part_getin_detail "&_
	                                    " (GetinId, PartSKU, Qty, MFP, PartName, Orders, wholesaler, cost, note, Regdate)"&_
	                                    " values "&_
	                                    " ('"&newId&"', '"& trim(skuArray(i)) &"', '"& trim(qtyArray(i)) &"', '"& mfp &"', '"& partName &"', '"& trim(subOrders(i)) &"', '"& trim(wholesalerArray(i)) &"', '"& trim(costArray(i)) &"', '"& trim(noteArray(i)) &"', now())")

                    end if
                   
                next

                for i=lbound(ordersArray) to ubound(ordersArray)
                
                    if trim(ordersArray(i)) <> "" then
                    conn.execute("insert into tb_part_getin_order "&_
	                                        " (GetinId, OrderCode) "&_
	                                        " values "&_
	                                        " ('"&newId&"', '"&trim(ordersArray(i))&"')")
                    end if

                next
               
            end if
        end if
   %>
<%closeconn() %>

<script>
    alert('is ok.');
</script>
</body>

</html>
