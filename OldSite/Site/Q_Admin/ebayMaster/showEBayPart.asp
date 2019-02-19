
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Part eBay online</title>
    <script type="text/javascript" src="../../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popup.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/popupclass.js"></script>
    <link href="/js_css/b_lu.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 256px;
        }
        td{font-size: 9pt;}
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".getWidthHeight").each(function () {
                var the = $(this);
                var imgsku = the.attr("imgsku");

                $.get("ebay_cmd.aspx", { cmd: "getImgInfo", sku: imgsku }, function (msg) {
                    if (msg.indexOf("*") > -1) {
                        var width = msg.split('*')[0];
                        var height = msg.split('*')[1];
                        if (parseInt(width) >= 500) {
                            the.parent().css({ "color": "green" });
                        }
                        else {

                            the.parent().css({ "color": "#FF7474" });
                        }
                    }
                    else {
                        the.parent().css({ "color": "blue" });
                    }
                    the.html(msg)
                });
            });
        });
    </script>
</head>
<body>
    <input type="button" value="show image width * height" />
<%
    dim cateid : cateid=0
    set rs = conn.execute("select itemid,luc_sku, p.product_ebay_name,p.menu_child_serial_no, "&_
                        " case when p.other_product_sku >0 then p.other_product_sku else p.product_serial_no end as imgSKU "&_
                        " from tb_ebay_selling e left join tb_product p on p.product_serial_no=e.luc_sku "&_
                        " where e.luc_sku>0 order by p.menu_child_serial_no asc, p.product_ebay_name asc ")

    set ers = conn.execute("select 'sys part' itemid, luc_sku, product_ebay_name,imgSKU,menu_child_serial_no from ("&_
                            " select distinct ep.luc_sku,p.product_ebay_name,p.menu_child_serial_no,"&_
                            " case when p.other_product_sku >0 then p.other_product_sku else p.product_serial_no end as imgSKU "&_
                            " from tb_ebay_selling es left join tb_ebay_system_parts ep on ep.system_sku=es.sys_sku "&_
                            " inner join tb_product p on p.product_serial_no = ep.luc_sku "&_
                            " where sys_sku >0 and ep.luc_sku<>16684 and ep.luc_sku<>16680 and ep.luc_sku<>16773) t order by menu_child_serial_no asc, product_ebay_name asc ")
    if not rs.eof then
        response.Write "<table>"
        
        do while not rs.eof 
            if( cateid <> rs("menu_child_serial_no")) then 
                cateid = rs("menu_child_serial_no")
                set srs = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no="& rs("menu_child_serial_no") )
                if not srs.eof then
                    response.Write "<tr><td colspan='5' style=""background:#f2f2f2;""><h2>"& srs(0) &"</h2></td></tr>"
                end if
                srs.close : set srs = nothing
            end if
            Response.Write "<tr>"
            Response.Write ("<td width=""50"">"& rs("luc_sku") &"</td>")
            Response.Write ("<td width=""100""><a href='http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="& rs("itemid") &"' target=""_blank"">"& rs("itemid") &"</a></td>")
            Response.Write ("<td>"& rs("product_ebay_name") &"</td>")
            Response.Write ("<td><a href='/pro_img/COMPONENTS/"& rs("imgSKU") &"_g_1.jpg' target='_blank'>"& rs("imgSKU") &"_g_1.jpg</a></td>")
            Response.Write ("<td class='getWidthHeight' imgsku='"&rs("imgSKU")&"'>...</td>")
            Response.Write "</tr>"
        rs.movenext
        loop
        response.Write "<tr><td colspan='5' style=""background:#f2f2f2;""><h2>system parts</h2></td></tr>"
        do while not ers.eof 
            if( cateid <> ers("menu_child_serial_no")) then 
                cateid = ers("menu_child_serial_no")
                set srs = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no="& ers("menu_child_serial_no") )
                if not srs.eof then
                    response.Write "<tr><td colspan='5' style=""background:#f2f2f2;""><h2>"& srs(0) &"</h2></td></tr>"
                end if
                srs.close : set srs = nothing
            end if
            Response.Write "<tr>"
            Response.Write ("<td width=""50"">"& ers("luc_sku") &"</td>")
            Response.Write ("<td width=""100"">"& ers("itemid") &"</td>")
            Response.Write ("<td>"& ers("product_ebay_name") &"</td>")
            Response.Write ("<td><a href='/pro_img/COMPONENTS/"& ers("imgSKU") &"_g_1.jpg' target='_blank'>"& ers("imgSKU") &"_g_1.jpg</a></td>")
            Response.Write ("<td class='getWidthHeight' imgsku='"&ers("imgSKU")&"'>...</td>")
            Response.Write "</tr>"
        ers.movenext
        loop
        Response.Write "</table>"
    else
        Response.Write "no data from lucomputer.com"
    end if
    rs.close : set rs = nothing
    ers.close : set ers = nothing
 %>
<% closeconn() %>
</body>
</html>