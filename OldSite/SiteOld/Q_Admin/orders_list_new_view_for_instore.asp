<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!DOCTYPE html>
<html>
<head>
    <title>Order Part List</title>
    <script src="../js_css/jquery_lab/jquery-1.9.1.js"></script>
    <link href="../js_css/bootstrap/css/bootstrap.min.css" type="text/css" rel="stylesheet" />
    <script src="../js_css/bootstrap/js/bootstrap.min.js"></script>

    <script type="text/javascript" src="/q_admin/js/winOpen.js"></script>
    <style>
        #list td {
            font-size: 9pt;
        }

        #historyList h5 {
            background: #f2f2f2;
            padding: 3px;
        }

        #historyList div div {
            word-wrap: nowrap;
            border: 0px solid red;
            width: 200px;
        }

            #historyList div div a {
                width: 70px;
                font-size: 9pt;
                margin: 3px;
            }

        #form1 input {
            height: 30px;
            line-height: 30px;
        }
    </style>

</head>
<body>
    <%
    Dim webCodes
    Dim eBayCodes
    Dim codeArray
    Dim tmpSubOrders
    dim tmpOrderArray 
    Dim getId
    Dim bgColor 
    getId   =   SQLescape(request("getid"))
    codes = Trim(SQLescape(request("codes")))
    %>
    <table style="height: 500px; width: 100%;">
        <tr>
            <td style='width: 200px; border-right: 1px solid #ccc; display:none' valign="top">
                <h4>History (Recently 30)</h4>
                <div id='historyList'>
                    <%
                
                    set rs = conn.execute("select * from tb_part_getin order by id desc limit 0,30 ")
                    if not rs.eof then
                        do while not rs.eof 
                            if getId = cstr(rs("id")) then 
                                bgColor = "background:#ccc;"
                            else
                                bgColor=""
                            end if
                            response.Write "<div><h5 style='"+ bgColor +"'>"& rs("regdate") &" &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='orders_list_new_view_for_instore.asp?getid="& rs("id") &"'>edit</a></h5>"
                            response.Write "    <div>"
                            tmpOrderArray = split(rs("Orders"), "|")
                            for i = lbound(tmpOrderArray) to ubound(tmpOrderArray)
                            
                                if trim(tmpOrderArray(i)) <> "" then 
                                    response.Write "<a target='_blank' href='' onclick=""OpenOrderDetail('"& trim(tmpOrderArray(i)) &"'); return false;"">"& trim(tmpOrderArray(i)) &"</a>&nbsp;&nbsp;&nbsp;"
                                    if i mod 3 = 0 then response.Write ("<br>")
                               
                                end if
                            next
                            response.Write "    </div>"
                            response.Write "</div>"
                        rs.movenext
                        loop
                    end if
                    rs.close : set rs = nothing 

                     '  edit status
                    if getId <> "" then 
                        set rs = conn.execute("Select * from tb_part_getin where id='"+ getid +"'")
                        if not rs.eof then
                            codes = rs("Orders")
                        end if
                    end if
                    %>
                </div>

            </td>
            <td style="padding: 1em" valign="top">
                <%
                    if request("codes") <>"" then

                %>
                <div style="padding: 0.5em; background: #f2f2f2; border: 1px solid #ccc">

                    <button onclick="window.location.href='orders_download.aspx?codes=<%= request("codes") %>';">Download Excel</button>
                </div>
                <%
                    end if
                %>


                <form id="form1" action="orders_list_new_view_for_instore_exec.asp" method="post" target="iframe1">
                    <input type="hidden" name='orders' value='<%=codes %>' />
                    <input type="hidden" name='getid' value='<%= getId%>' />
                    <%
                   
                   
    
    if instr(codes, "|") >0 then 
        codes = replace(codes, "|",",")
    end if

    if(left(codes,1) = ",")then 
        codes = right(codes, len(codes)-1)
    end if
    codeArray = split(codes, ",")
    for i = lbound(codeArray) to ubound(codeArray)
        if len(trim(codeArray(i))) = 6 then 
            webCodes = webCodes & ", "& codeArray(i)
        else
            eBayCodes =eBayCodes & ", "& codeArray(i)
        end if        
    next
    
    if len(webCodes)>0 then webCodes = right(webCodes, len(webCodes)-1)
    if len(eBayCodes)>0 then eBayCodes = right(eBayCodes, len(eBayCodes)-1)

    REsponse.Write "<div> <b>Web Orders:</b> "+ webCodes +"</div>"
    REsponse.Write "<div> <b>eBay Orders:</b> "+ eBayCodes +"</div>"

    response.Write "<hr size=1>"
    
    if getid <> "" then 

        sql = " select partSku product_serial_no, partName product_name, qty c, wholesaler,cost, note, orders,id  from tb_part_getin_detail where getinid='"+ getid +"'"
    else
        sql = " select * from "&_
                        " ("&_
                        " select distinct op.product_serial_no, op.product_name, count(op.product_serial_no) c from tb_order_product op "&_
                        " where order_code  in ("& codes &") and length(op.product_serial_no)<>8 group by op.product_serial_no, op.product_name"&_
                        " union all"&_
                        " select distinct ops.product_serial_no,ops.product_name,count(ops.product_serial_no) c from tb_order_product op inner join tb_order_product_sys_detail ops "&_
                        " on op.product_serial_no = ops.sys_tmp_code "&_
                        " where order_code  in ("& codes &") and length(op.product_serial_no)=8 group by ops.product_serial_no, ops.product_name"&_
                        " ) t order by product_name asc "
    end if

    set rs = conn.execute(sql)


    if not rs.eof then
        Response.Write "<table width='100%' id='list' cellpadding='5'>"
        response.Write "    <tr bgColor='#ccc'><td> <b>Part Sku</b> </td><td> <b>Quantity</b> </td><td><b>MFP#</b></td><td><b>Part Name</b></td><td> <b>Orders</b></td></tr>"
        do while not rs.eof 
            if rs("product_serial_no") <> 16684 and 6775 <> rs("product_serial_no") and 15780 <> rs("product_serial_no") then

                if getid<> "" then 
                    tmpSubOrders= rs("Orders")
                    wholesaler  = rs("wholesaler")
                    cost        = rs("cost")
                    note        = rs("note")
                    detailId    = rs("id")
                else
                    tmpSubOrders =  getOrderCodesByPartSku(rs("product_serial_no"))
                end if

                response.Write "<tr>"
                response.Write "    <td width='80'><input type='hidden' name='detailId' value='"& detailId &"'><input type='hidden' name='sku' value='"& rs(0) &"'>"& rs(0) &"</td>"
                response.Write "    <td width='50' style='text-align:center;'><input type='hidden' name='qty' value='"& rs("c") &"'>"& rs("c") &"</td>"
                response.Write "    <td >"& getMFPByPartSKU(rs("product_serial_no")) &"</td>"
                response.Write "    <td >"& rs(1) &"</td>"
                response.Write "    <td><input type='hidden' name='subOrders' value='"& tmpSubOrders &"'>"& tmpSubOrders &"</td>"                   
                response.Write "</tr>"

                response.Write "<tr style='background:#f2f2f2;'>"
                response.Write "    <td colspan='4' style='padding-left:50px;'>"

                response.Write "    <span name='ltdName'></span>"

                response.Write "    <input type='text' name='wholesaler' placeholder='wholesaler ' style='width:100px;' value="""& wholesaler &""">"

                response.Write "    <input type='text' name='cost'  placeholder='Cost' style='width:80px;' value='"& cost &"'>"
                response.Write "    <input type='text' name='note' placeholder='Notepad' style='width:400px;' value="""& note &""">"
                response.Write "    </td><td>"
                response.Write "    <span name='ltdPriceList' tag='"&rs("product_serial_no")&"'></span>"
                response.Write "    </td>"                   
                response.Write "</tr>"

            end if
        rs.movenext
        loop
        Response.Write "</table>"
    end if
    rs.close : set rs = nothing
                    %>


                    <div style="text-align: center; padding: 1em;">
                        <input type="submit" value="Save" />
                    </div>
                </form>
            </td>
        </tr>
    </table>
    <iframe src="" name="iframe1" id="iframe1" frameborder="0" style='width: 0; height: 0; border: 0 solid red;'></iframe>
    <%
        
    function getOrderCodesByPartSku(partSKU)
        dim rs
        set rs = conn.execute("select distinct op.order_code, op.product_serial_no from tb_order_product op "&_
                                          "   where order_code  in ("& codes &") and length(op.product_serial_no)<>8 and op.product_serial_no='"&partSKU&"'"&_
                                           "  union all"&_
                                           "  select distinct op.order_code, ops.product_serial_no from tb_order_product op inner join tb_order_product_sys_detail ops "&_
                                           "  on op.product_serial_no = ops.sys_tmp_code "&_
                                           "  where order_code  in ("& codes &") and length(op.product_serial_no)=8  and ops.product_serial_no='"&partSKU&"'")
        if not rs.eof then
            do while not rs.eof 
                getOrderCodesByPartSku = getOrderCodesByPartSku & rs("order_code") &","
            rs.movenext
            loop
        end if
        rs.close : set rs = nothing

        if len(getOrderCodesByPartSku)>0 then
            if right(getOrderCodesByPartSku, 1) = "," then 
                getOrderCodesByPartSku =  left(getOrderCodesByPartSku, len(getOrderCodesByPartSku)-1)
            end if
        end if
    end function

    function getMFPByPartSKU(partSKU)
        dim  rs
        set rs = conn.execute("select manufacturer_part_number from tb_product where product_serial_no='"& partSKU &"'")
        if not rs.eof then
            getMFPByPartSKU = rs(0)
        end if
        rs.close : set rs = nothing
    end function
    %>

    <%closeconn() %>
</body>

<script>
    
    jQuery(document).ready(function () {
         getLtdName();
         getWholesalerPriceList();
     });

     function getLtdName() {

         $.getJSON("orders_cmd.aspx?cmd=getOtherLtdNames", function (s) {
             var str = "<select onchange='changeLtdName($(this));'>";
             for (var i = 0; i < s.item.length; i++)
                 str += "<option>" + s.item[i].text + "</option>";


             $('span[name=ltdName]').each(function () {
                 $(this).html(str);
             });
         });
     }

     function changeLtdName(the) {
         the.parent().next().val(the.val());
     }

     function getWholesalerPriceList() {
         $('span[name=ltdPriceList]').each(function () {
             var the = $(this);
             $.ajax({
                 type: "get",
                 url: "/q_admin/inc/get_part_shopbot_price.aspx?OnlyWholesaler=1&sku=" + the.attr("tag"),
                 success: function (msg) { the.html(msg); }
             });
         });     
     }
</script>
</html>
