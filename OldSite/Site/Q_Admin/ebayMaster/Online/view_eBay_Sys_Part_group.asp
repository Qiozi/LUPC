<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>eBay System Part Stock <%= now() %></title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery.float.js"></script>

    <style>
        body {
            font-size: 9pt;
        }

        .cateName {
            background-color: #b6ff00;
            font-weight: bold;
            display: block;
            padding: 5px;
            text-align: center;
        }

        .groupName {
            color: green;
            font-weight: bold;
        }

            .groupName:hover {
                color: red;
            }

        .ebayName {
            padding: 5px;
        }

        #cateArea {
            padding: 5px;
            margin: 5px;
            position: absolute;
            top: 7px;
            right: 5px;
        }

        .catetitle {
            border-bottom: 1px solid #ccc;
            display: block;
            padding: 5px;
            background: #f2f2f2;
        }

            .catetitle:hover {
                color: green;
            }
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
    <input type="button" value="modify sys part title" onclick="window.location.href = 'view_eBay_Sys_Part.asp?generateStock=0'" />
  <!--  <input type="button" value="modify part ebay title" onclick="window.location.href = 'view_eBay_Part.asp'" />-->
    <%
    dim generateStock : generateStock = request("generateStock")
    dim cateName : cateName = ""
    dim lucSkuPart
    dim cateAreaString 
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
                        " where z.luc_sku is null order by p.menu_child_serial_no asc, pg.part_group_name asc, p.product_ebay_name asc  ")
    if not rs.eof then
        Response.write "<table cellpadding='3' cellspacing=0>"
        do while not rs.eof 

            if cateName <> rs("menu_child_serial_no") then 
                cateName = rs("menu_child_serial_no")
                set subRs = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no='"& rs("menu_child_serial_no") &"'")
                response.Write(" <tr><td colspan=""3"" id='"& rs("menu_child_serial_no") &"'><label class='cateName'>"& subRs("menu_child_name") &"</label></td></tr>")
                cateAreaString = cateAreaString & " <a class='catetitle' href='#"&rs("menu_child_serial_no")&"' data-cate-id='"& rs("menu_child_serial_no") &"'>"& subRs("menu_child_name") &"</a>"
                subRs.close : set subRs = nothing
            end if
            response.write "<tr class='partTitleModify'>"
            response.write "    <td><a href='/q_admin/editPartDetail.aspx?id="& rs("product_serial_no") &"' target='_blank' title='partSku' class='sku'>"& rs("product_serial_no") &"</a></td>"
         
            response.write "    <td class='ebayName' title='eBay name'>"& replace(rs("product_ebay_name"), """", "&quot;") &"<td>" 
            response.write "    </td>"
            response.write "</tr>"
            response.Write "<tr>"
            response.Write "    <td colspan='3' style='padding:1em;'>"

          
                set cateRs = conn.execute("Select * from tb_part_group where showit=1 and product_category = '"&rs("menu_child_serial_no")&"' and is_ebay=1 order by part_group_comment asc")
                if not cateRs.eof then
                    do while not cateRs.eof 
                        lucSkuPart = 0
                        set pRs = conn.execute("Select * from tb_part_group_detail where part_group_id='"& cateRs("part_group_id") &"' and product_serial_no='"& rs("product_serial_no") &"'")
                        if not pRs.eof then
                             lucSkuPart = 1   
                        end if
                        pRs.close : set pRs  = nothing
                        response.Write "<label class='groupName'><input class='checkboxGroup' type='checkbox' "
                        if lucSkuPart = 1 then 
                            response.Write " checked "
                        end if
                        response.Write " value='"& cateRs("part_group_id") &"' data-lusku='"&rs("product_serial_no")&"' /> "& cateRs("part_group_comment") &"</label>"
                        cateRs.movenext
                    loop
                end if
                cateRs.close : set cateRs = nothing
             

            response.Write "    </td>"
            response.Write "</tr>"
        rs.movenext
        loop
        response.write "</table>"
    end if
    rs.close : set rs = nothing


    closeconn %>
    <div id="cateArea"><%= cateAreaString %></div>
    <script type="text/javascript">

        $().ready(function () {
            function saveGroupInfo(gid, sku, isdel) {

                $.ajax({
                    type: "get",
                    url: "../ebay_cmd.aspx",
                    data: { cmd: 'changePartInGroup', isdel: isdel, gid: gid, sku: sku },
                    error: function (s, r, t) { alert(r); },
                    success: function (msg, s) {
                        if ("OK" == msg)
                            alert(msg);
                        else
                            alert(msg);
                    }
                });
            }
            $('.partTitleModify').css({ background: '#ccc' })
                    .find('input').css({ background: '#ccc', border: '0' });

            $(window).scroll(function () {
                var offsetTop = $(window).scrollTop() + 7 + "px";
                $("#cateArea").animate({ top: offsetTop }, { duration: 500, queue: false });
            });

            $('.checkboxGroup').on('click', function () {
                var sku = $(this).attr('data-lusku');
                var gid = $(this).val();
                saveGroupInfo(gid, sku, $(this).is(':checked') ? 0 : 1);
            });
        });


    </script>
</body>
</html>
