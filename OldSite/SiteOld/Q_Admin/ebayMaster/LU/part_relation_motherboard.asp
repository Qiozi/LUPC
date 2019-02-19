<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Relation Motherboard</title>
    <script type="text/javascript" src="../../JS/lib/jquery-1.3.2.min.js"></script>
    <script src="../../JS/WinOpen.js"  type="text/javascript"></script>
    <script src="/q_admin/js/helper.js" type="text/javascript"></script>
    <link rel="stylesheet" type="text/css" href="/js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="/js_css/b_lu.css" />
    <style>
        .selectStyle { width: 300px;}
    </style>
</head>
<body>
    <h3>Motherboards</h3>
    <%
        Dim Part_group_id       :   part_group_id   =   SQLescape(request("part_group_id"))
        Dim SKU                 :   SKU             =   SQLescape(request("sku"))

        if isempty(part_group_id) or isnull(part_group_id) then 
            
        else
            set rs = conn.execute("select p.product_serial_no, p.product_ebay_name "&_
                                    ", case when p.other_product_sku >0 then p.other_product_sku else p.product_serial_no end as img_sku"&_
                                    ", pr.video_sku, pr.audio_sku, pr.network_sku, p.menu_child_serial_no"&_
                                    " from tb_part_group_detail pc "&_
                                    " inner join tb_product p on pc.product_serial_no=p.product_serial_no "&_
                                    " left join tb_part_relation_motherboard_video_audio_port pr on pr.mb_sku=p.product_serial_no "&_
                                    " where (pc.part_group_id = '"& Part_group_id &"' or p.product_serial_no='"& sku &"') and pc.showit=1 and tag=1 order by p.product_serial_no desc ")
            if not rs.eof then
                Response.Write "<table cellspacing='0' id='list' width='100%'>" &vblf
                do while not rs.eof 
                    
                    Response.write "<tr>" &vblf
                    REsponse.write "    <td><img src='http://www.lucomputers.com/pro_img/COMPONENTS/"& rs("img_sku") &"_t.jpg'/><br>"& rs("product_serial_no") &"</td>"&vblf
                    Response.write "    <td> <a href='/site/product_parts_detail.asp?id="& rs("product_serial_no") &"&cid="& rs("menu_child_serial_no") &"' target='_blank'>"& rs("product_ebay_name") &"</a>"
                    Response.write "        <br/>"&vblf
                    Response.write "        <input type='hidden' name='sku' value='"& rs("product_serial_no") &"'> "
                    Response.write "    <select name='video' tag='"& rs("video_sku") &"' class='selectStyle'><option value='-1'>Select video</option></select><br>"&vblf
                    Response.write "    <select name='audio' tag='"& rs("audio_sku") &"' class='selectStyle'><option value='-1'>Select audio</option></select><br>"&vblf
                    Response.write "    <select name='network' tag='"& rs("network_sku") &"' class='selectStyle'><option value='-1'>Select network</option></select></td>"&vblf
                    Response.write "    <td style='width:50px;'><input type='button' value='Save' onclick='SaveInfo($(this));'></td>"&vblf
                    Response.write "    <td>&nbsp;</td>"&vblf
                    Response.write "</tr>" &vblf
                rs.movenext
                loop
                Response.Write "</table>" &vblf
            end if
            rs.close : set rs = nothing
        end if


        
    %>

    <% closeconn() %>

    <script type="text/javascript">
        $().ready(function () {

            $('#list tr').each(function (i) {
                if (i % 2 == 0) {
                    $(this).find('td').each(function () { $(this).css({ 'background': '#f2f2f2', 'padding': '5px' }); });
                }
                else {
                    $(this).find('td').each(function () { $(this).css({ 'background': '#ffffff', 'padding': '5px' }); });
                }
            });

            GetVideoOptions();      
        });

        function SetSelectedValue() {
            $('select').each(function (i) {
                var exist = false;
                $(this).find('option').each(function (n) {
                    if ($(this).val() == $(this).parent().attr('tag')) {
                        $(this).attr('selected', 'selected');
                        exist = true;
                    }
                });
                if (!exist) {
                    $(this).css("color", "red");
                }
            });
            
        }

        function SaveInfo(e) {
            var sku = 0;
            var video = 0;
            var audio = 0;
            var network = 0;
            e.parent().parent().find('input').each(function () {
                if ($(this).attr('name') == "sku")
                    sku = $(this).val();
            });

            e.parent().parent().find('select').each(function () {
                if ($(this).attr('name') == 'video') {
                    video = $(this).val();
                }
                if ($(this).attr('name') == "audio") {
                    audio = $(this).val();
                }
                if ($(this).attr('name') == "network") {
                    network = $(this).val();
                }
            });

            $.ajax({
                type: "Post",
                url: "/q_admin/ebayMaster/lu/part_relation_motherboard_exec.asp?cmd=saveRInfo&sku=" + sku + "&video=" + video + "&audio=" + audio + "&network=" + network,
                data: "",
                success: function (msg) {
                    e.parent().parent().find('select').each(function () { $(this).css('color', 'blue'); });
                },
                error: function (msg) { alert(msg); }
            });
        }

        function GetVideoOptions() {
            $.ajax({
                type: "Get",
                url: "/q_admin/ebayMaster/lu/part_relation_motherboard_get_options.aspx",
                data: "cmd=GetVideoOptions&r=" + rnd(),
                success: function (msg) {

                    var json = eval(msg);
                    $.each(json, function (idx, item) {
                        $("<option value=\"" + item["value"] + "\">" + item["name"] + "</option>").appendTo($("select[name=video]"));
                    });
                    GetAudioOptions();
                }
            , error: function (msg) { alert(msg); }
            });
        }

        function GetAudioOptions() {
            $.ajax({
                type: "Get",
                url: "/q_admin/ebayMaster/lu/part_relation_motherboard_get_options.aspx",
                data: "cmd=GetAudioOptions&r=" + rnd(),
                success: function (msg) {

                    var json = eval(msg);
                    $.each(json, function (idx, item) {
                        $("<option value=\"" + item["value"] + "\">" + item["name"] + "</option>").appendTo($("select[name=audio]"));
                    });

                    GetNetworkOptions();
                }
            , error: function (msg) { alert(msg); }
            });
        }

        function GetNetworkOptions() {
            $.ajax({
                type: "Get",
                url: "/q_admin/ebayMaster/lu/part_relation_motherboard_get_options.aspx",
                data: "cmd=GetNetworkOptions&r=" + rnd(),
                success: function (msg) {

                    var json = eval(msg);
                    $.each(json, function (idx, item) {
                        $("<option value=\"" + item["value"] + "\">" + item["name"] + "</option>").appendTo($("select[name=network]"));
                    });

                    SetSelectedValue();
                }
            , error: function (msg) { alert(msg); }
            });
            
        }
    </script>
</body>
</html>
