<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<html>
<head>
    <title></title>
     <script type="text/javascript" src="../../../js_css/jquery-1.9.1.js"></script>
</head>
<body>
<form action="ebay_system_item_specifics_exec.asp" target="iframe100" method="post">
<input type="hidden" name="system_sku" value="<%= SQLescape(request("system_sku")) %>"/>
<input type="hidden" name="cmd" value="saveItemSpecifics" />
<%
Dim system_sku
system_sku = SQLescape(request("system_sku"))
        
        set rs = conn.execute("Select * from tb_ebay_system_item_specifics where system_sku='"& system_sku &"'")
        if not rs.eof then
            do while not rs.eof 
                response.Write("<input type=""text"" name=""title"" value="""& rs("ItemSpecificsName") &""" size=""30"" style=""font-weight: bold""/>:")
                response.Write("<input type=""text"" name=""titleValue"" value="""& replace(rs("ItemSpecificsValue"),",","~") &""" size=""40""/>")
                response.Write("<br>")
            rs.movenext
            loop
        else
%>
<input type="text" name="title" value="Brand" size="30" style="font-weight: bold"/>:<input 
        type="text" name="titleValue" value="Custom~ Whitebox" size="40"/><br />
<input type="text" name="title" value="Memory" size="30" style="font-weight: bold"/>:<input 
        type="text" name="titleValue" value="Not Included" size="40"/><br />
<input type="text" name="title" value="Operating System" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Not Included" size="40"/><br />
<input type="text" name="title" value="Hard Drive Capacity" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Not Included" size="40"/><br />
<input type="text" name="title" value="Processor Type" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Not Included" size="40"/><br />
<input type="text" name="title" value="Processor Speed" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Not Included" size="40"/><br />
<input type="text" name="title" value="Primary Drive" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Not Included" size="40"/><br />
<input type="text" name="title" value="Graphics Processing Type" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Dedicated Graphics" size="40"/><br />
<input type="text" name="title" value="Processor Configuration" size="30" 
        style="font-weight: bold"/>:<input type="text" name="titleValue" 
        value="Quad Core" size="40"/><br />
<input type="text" name="title" value="" size="30" style="font-weight: bold"/>:<input 
        type="text" name="titleValue" value="" size="40"/>
<%
        end if
        rs.close : set rs = nothing 
 %>
 "~" 代表","
 <input type="submit" value="save" />
 <input type="button" id="btnModifySysSpecifics" value="Modify System Specifics" />
 <div id="resultComment">
 
 </div>
 </form>
<br />
        <hr size=1 />
        <input type=text id="eBayStoreCateId" value="179" readonly="readonly"/>
        <hr size="1" />
        <table>
        <tr>
                            <td>Store Category</td><td><select name="storeCategory" id="storeCategory"></select></td>
                        </tr>
                        <tr>
                            <td>Store Category2</td><td><select name="storeCategory2" id="storeCategory2"></select></td>
                        </tr>
        </table>
        <input type="button" id="issueSys" value="issue system onto eBay" />
 <iframe name='iframe100' id='iframe100' style='width:1110px; height:110px;' frameborder="0"></iframe>
         <script type="text/javascript">

             $().ready(function () {
                 $('#btn_save_and_next').bind('click', function () {
                     $('#form1').attr('action', 'ebay_part_comment_edit_exec.asp?cmd=next');
                     $('#form1').submit();
                 });

                 bindStoreCategory();

                 $('#issueSys').bind('click', function () {
                     $('#resultComment').html('<img src="/soft_img/tags/loading.gif">');

                     $.ajax({
                         type: "get",
                         url: "/q_admin/ebaymaster/online/AddItem2.aspx?cmd=addsys&system_sku=<%=system_sku %>&storeCategory1=" + $('#storeCategory').val() + "&storeCategory2=" + $('#storeCategory2').val() + "&eBayStoreCateId=" + $('#eBayStoreCateId').val(),
                         data: "",
                         success: function (msg) {
                             $('#resultComment').html(msg);
                         },
                         error: function (msg) { $('#resultComment').html(msg); }

                     });
                 });

                 $('#btnModifySysSpecifics').click(function () {
                     $.ajax({
                         type: "get",
                         url: "/q_admin/ebaymaster/online/AddItem2.aspx?cmd=modifyItemSpecifics&system_sku=<%=system_sku %>&storeCategory1=" + $('#storeCategory').val() + "&storeCategory2=" + $('#storeCategory2').val() + "&eBayStoreCateId=" + $('#eBayStoreCateId').val(),
                         data: "",
                         success: function (msg) {
                             alert(msg);
                         },
                         error: function (r, t, x) { alert(t); }

                     });
                 });
             });

             function bindStoreCategory() {
                 $.ajax({
                     type: "get",
                     url: "/q_admin/ebaymaster/ebay_system_cmd.aspx?cmd=GetEBayStoreCategoryString",
                     data: "",
                     success: function (msg) {
                         $(msg).appendTo($('select[name=storeCategory]'));


                         $('select[name=storeCategory]').each(function () {
                             var id = '<%= Session("storeCategory1") %>';
                             $(this).val(id);

                         });

                         $(msg).appendTo($('select[name=storeCategory2]'));


                         $('select[name=storeCategory2]').each(function () {
                             var id = '<%= Session("storeCategory2") %>';
                             $(this).val(id);

                         });

                     },
                     error: function (msg) { alert('error:' + msg); }
                 });
             }
     </script>
</body>
</html>
