<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>eBay System Part Stock <%= now() %></title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.9.1.js"></script>
    <style>
        body {
            font-size: 9pt;
        }
        .cateName{
            background-color:#b6ff00;
            font-weight:bold;
            display:block;
            
        }
        .groupName{ color: green;}
    </style>
    <script type="text/javascript">
        function copyLogoToTmp() {
            //title = 'diff'
            $('a[title=partSku]').each(function () {
                var the = $(this);
                var sku = the.html();
                the.css({ "color": "white", "background": "green" });
                $.ajax({
                    type: "get",
                    url: "../copyLogoToFolder.aspx?sku=" + sku,
                    data: "",
                    success: function (msg) {
                        the.html("OK");
                        the.css({ "color": "white", "background": "#000000" });
                    },
                    error: function (msg) {
                        the.html(sku);
                        the.css({ "color": "white", "background": "red" });
                    }
                });
            });
        }
    </script>
</head>
<body>
   <!-- <input type="button" onclick="copyLogoToTmp();" value="Copy logo to temp folder" />-->
    <input type="button" value="modify group" onclick="window.location.href = 'view_eBay_Sys_Part_group.asp'" />
  <!--  <input type="button" value="modify part ebay title" onclick="window.location.href = 'view_eBay_Part.asp'" />-->
    <%
    dim generateStock : generateStock = request("generateStock")
    dim cateName : cateName = ""
    set rs = conn.execute("Select distinct ifnull(p.product_ebay_name, '') product_ebay_name"&_
                        " ,p.product_current_cost"&_
                        " , p.curr_change_cost"&_
                        " ,  p.product_store_sum "&_
                        " , p.curr_change_regdate"&_
                        " , p.ebay_system_short_name"&_
                        " , p.product_serial_no"&_
                        " , p.manufacturer_part_number"&_
                        " , case when ifnull(stock.c,0) = 0 then p.product_store_sum else stock.c end  ltd_stock"&_
                        " , online.c"&_
                        " , ifnull(p.short_name_for_sys , '') short_name_for_sys"&_
                        " , p.product_short_name"&_
                        " , p.menu_child_serial_no"&_
                        " from (select distinct pg.part_group_id, pg.part_group_name from tb_part_group pg "&_
                        " inner join tb_ebay_system_parts esp on esp.part_group_id=pg.part_group_id and pg.showit=1 and pg.is_ebay=1"&_
                        " inner join tb_ebay_system es on es.id=esp.system_sku) pg "&_

                        " inner join tb_part_group_detail pgd on pgd.part_group_id=pg.part_group_id "&_
                        " inner join tb_product p on p.product_serial_no=pgd.product_serial_no and p.tag=1 and p.split_line=0 "&_

                        " left join (select distinct luc_sku , count(luc_sku) c from tb_ebay_system_parts where is_online=1 group by luc_sku ) online on online.luc_sku = p.product_serial_no"&_
                        " left join tb_ebay_system_part_zero_price z on z.luc_sku=pgd.product_serial_no "&_
                        " left join (select distinct ip.luc_sku, count(ip.luc_sku) c from tb_other_inc_part_info ip	where  other_inc_store_sum >0 and date_format(now(), '%y%j')-date_format(last_regdate, '%y%j') <15 group by ip.luc_sku) stock on stock.luc_sku=p.product_serial_no"&_
                        " where z.luc_sku is null and (p.ltd_stock>0 or p.product_store_sum>0) order by p.menu_child_serial_no asc, pg.part_group_name asc, p.product_ebay_name asc  ")
    if not rs.eof then
        Response.write "<table cellpadding='3' cellspacing=0>"
        do while not rs.eof 

            if cateName <> rs("menu_child_serial_no") then 
                cateName = rs("menu_child_serial_no")
                set subRs = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no='"& rs("menu_child_serial_no") &"'")
                response.Write(" <tr><td colspan=""20""><label class='cateName'>"& subRs("menu_child_name") &"</label></td></tr>")
                subRs.close : set subRs = nothing
            end if
            response.write "<tr class='partTitleModify'>"
            response.write "    <td><a href='/q_admin/editPartDetail.aspx?id="& rs("product_serial_no") &"' target='_blank' title='partSku' class='sku'>"& rs("product_serial_no") &"</a></td>"
            response.write "    <td title='own quantity'>"& rs("product_store_sum") &"<td>"
            response.write "    <td title='cost' style='font-align:right;'>"& rs("product_current_cost") &"<td>"
            response.write "    <td title='ltd quantity' class='changeqty' sku='"& rs("product_serial_no") &"'>"& rs("ltd_stock") &"<td>"
            response.write "    <td title='change cost'>"& rs("curr_change_cost") &"<td>"
            response.write "    <td title='change time'>"& rs("curr_change_regdate") &"<td>"            
            response.write "    <td title='valid count'>"& rs("c") &"<td>"
            response.write "    <td title='eBay name'><input type='text' size='80' name='prodname' value="""& replace(rs("product_ebay_name"), """", "&quot;") &"""/><td>"   
            response.write "    <td ><input type='text' size='30' name='prodnameforSys' value="""& replace(rs("short_name_for_sys"), """", "&quot;") &"""/><td>" 
            response.write "    <td ><input type='text' size='60' name='prodShortName' value="""& replace(rs("product_short_name"), """", "&quot;") &"""/><td>" 
            response.write "    <td ><img src='/q_admin/images/save_1.gif' onclick=""saveName($(this),'"& rs("product_serial_no") &"');"" style='border:0; cursor: pointer;'><td>"
            response.write "    </td>"
            response.write "</tr>"
            response.Write "<tr>"
            response.Write "    <td colspan='20' >"

                if generateStock = 1 then 
                set cateRs = conn.execute("Select * from tb_part_group where showit=1 and product_category = '"&rs("menu_child_serial_no")&"' and is_ebay=1")
                if not cateRs.eof then
                    do while not cateRs.eof 
                        response.Write "<label class='groupName'><input type='checkbox' value='"& cateRs("part_group_id") &"' /> "& cateRs("part_group_comment") &"</label>"
                    cateRs.movenext
                    loop
                end if
                cateRs.close : set cateRs = nothing
                end if

            response.Write "    </td>"
            response.Write "</tr>"
        rs.movenext
        loop
        response.write "</table>"
    end if
    rs.close : set rs = nothing


    closeconn %>
    <script type="text/javascript">

        $().ready(function () {
            $('td[title=ltd_stock]').each(function () {
                if (parseInt($(this).html()) < 1)
                    $(this).css("color", "red");
                else
                    $(this).css("color", "green");
            });
            $('tr').hover(
                function () {
                    $(this).find("td").css("border-bottom", "1px solid #999999").css("padding-bottom", "0px");
                }
              , function () {
                  $(this).find("td").css("border-bottom", "0px solid #000000").css("padding-bottom", "1px");
              });

            
                if ('1' != '<%=generateStock%>') {
                    $('tr').each(function (i) {
                        if (i % 2 == 1) {
                            $(this).find("td").css("background", "#ffffff").css("font-size", "8pt").css("padding-bottom", "1px");

                        } else {
                            $(this).find("td").css("background", "#f2f2f2").css("font-size", "8pt").css("padding-bottom", "1px");
                        }
                    });
                }
                else {
                    $('.partTitleModify').css({ background: '#ccc' })
                    .find('input').css({background:'#ccc', border:'0'});
                }
           

            var skus = new Array();
            var skuIndex = 0;
            $('.sku').each(function (i) {
                skus[skuIndex] = $(this).text();
                skuIndex++;
            });

            function changeForSys(index) {
                var sku = skus[index];
                if (sku > 0) {

                    if (index == 0) {
                        $.get("../ebay_cmd.aspx"
                            , { cmd: 'ClearPartForSys', sku: '', qty: qty }
                            , function () {
                                var the = $('td[sku=' + sku + ']');
                                the.parent().css({ color: 'red' });
                                var qty = the.text();

                                $.get("../ebay_cmd.aspx"
                                , { cmd: 'ChangeForSys', sku: sku, qty: qty }
                                , function () {
                                    the.parent().css({ color: 'green' });
                                    changeForSys(index + 1);
                                });
                            });
                    }
                    else {
                        var the = $('td[sku=' + sku + ']');
                        the.parent().css({ color: 'red' });
                        var qty = the.text();

                        $.get("../ebay_cmd.aspx"
                            , { cmd: 'ChangeForSys', sku: sku, qty: qty }
                            , function () {
                                the.parent().css({ color: 'green' });
                                changeForSys(index + 1);
                            });
                    }
                }
            }
            if ('0' != '<%= generateStock %>' && '1' != '<%= generateStock %>')
            {
                changeForSys(0);
            }
        });

        function saveName(the, sku) {
            the.parent().css({ background: 'white' });

            var prodName = the.parent().parent().find('input[name=prodname]').eq(0).val();
            var sNameForSys = the.parent().parent().find('input[name=prodnameforSys]').eq(0).val();
            var shortName = the.parent().parent().find('input[name=prodShortName]').eq(0).val();

            $.ajax({
                type: "get",
                url: "../ebay_cmd.aspx",
                data: { cmd: 'nameAndForSysName', pname: prodName, sname: sNameForSys, sku: sku, prodShortName: shortName },
                error: function (s, r, t) { alert(r); },
                success: function (msg, s) {
                    if ("OK" == msg)
                        the.parent().css({ background: 'green' });
                    else
                        alert(msg);
                }
            });
        }
    </script>
</body>
</html>
