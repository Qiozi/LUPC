<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="ebay_part_list.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_part_list" Title="Ebay Part List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="border-bottom: 1px solid #cccccc; background: #f2f2f2; width: 100%;" id='keywords_area'>
        <input type="hidden" name="current_page" value="1" />
        <table id="search_cmd_btn" cellpadding="3" cellspacing="0" width="100%" border="0">
            <tr>
                <td width="200">
                    <ul class="ul_parent_2">
                        <li>
                            <table cellpadding="0" cellspacing="0" style="border: 0px solid #ccc; width: 150px;">
                                <tr>
                                    <td>Category</td>
                                    <td>
                                        <input type="hidden" id="txt_id_ValueChanged" />
                                        <input id="txt_text" type="text" size="25" readonly="true" />
                                        <div style="left: auto; top: 16px; display: none" id="uc_dropDownList_category_selected">
                                            <iframe frameborder="0" src="/q_admin/asp/category_selected_not_sys.asp?div_id=uc_dropDownList_category_selected&id=txt_id_ValueChanged&textid=txt_text" style="width: 300px; height: 300px; border: 1px solid #ccc;"></iframe>
                                        </div>
                                    </td>
                                    <td>
                                        <img src="/q_admin/images/arrow_5.gif" alt="Press" title="Press" style="height: 19px; width: 15px; cursor: pointer" onclick="document.getElementById('uc_dropDownList_category_selected').style.display = '';" /></td>
                                </tr>
                            </table>
                        </li>
                    </ul>
                </td>
                <td style="width: 502px">Keyword:<input type="text" value='' name="keyword" id="keyword" />
                    <select name="sql_type">
                        <option value="1" selected="selected">模糊</option>
                        <option value="2">精确</option>
                    </select>
                    <select name="part_type">
                        <option value="Comment" selected="selected">Comment</option>
                        <option value="SKU">SKU</option>
                    </select>

                    <%-- <select name="part_status">
                    <option value="Show" selected="selected">Show</option>
                    <option value="Hidde">Hidde</option>
                    <option value="ALL">ALL</option>
                </select>--%>
                    <input type="checkbox" id="CheckBox_ALL" />ALL
                <input type="checkbox" id="cbNoNotebook" />not notebook
                </td>
                <td>
                    <input type="hidden" id="token" value='<%= Token %>' />

                    <input type="button" value="Find It"
                        onclick="LoadKeyword();" />

                                     
                    <input type="button" id="btnEbaySaleItemList" value="eBay Sale list"/>
                </td>
                <td>
                    <span id='changeQtyArea'>d</span>
                </td>
            </tr>
        </table>

    </div>

    <div id='hidden_form_area' style="display: none"></div>
    <iframe src="/site/blank.html" name="iframe2" id="iframe2" style="width: 0px; height: 0px;" frameborder="0"></iframe>

    <script type="text/javascript">
        $(document).ready(function () {

            // window resize
            var _attr = parseInt($(window).height());
            var _height = isNaN(_attr) || _attr <= 600 ? "100%" : (_attr - 45 * 2) + "px";

            $('#iframe2').css({ "height": _height, "width": "100%" });

        });


        var resizeTimer = null;
        $(window).bind("resize", function () {
            if (resizeTimer)
                clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () {
                var _attr = parseInt($(window).height()); //alert(_attr);
                // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";
                $('#iframe2').css("height", isNaN(_attr) || _attr <= 45 ? "100%" : (_attr - 45 * 2) + "px").css("width", "100%");
            }, 100);

        });

        function LoadKeyword() {
            var category_id = $('#txt_id_ValueChanged').val();
            var keyword = $('input[name=keyword]').val();
            var sql_type = $('select[name=sql_type]').val();
            var part_type = $('select[name=part_type]').val();
            var part_status = "ALL";
            var online = "true";
            var viewAll = $('#CheckBox_ALL').prop("checked");
            var cbNoNotebook = $('#cbNoNotebook').prop("checked");
            var token = $('#token').val();
            //alert(viewAll);
            //alert(part_status);
            $('#iframe2').attr("src", "/q_admin/ebayMaster/ebay_part_list_sub.asp?token=" + token + "&online=" + online + "&category_id=" + category_id + "&keyword=" + keyword + "&sql_type=" + sql_type + "&part_status=" + part_status + "&part_type=" + part_type + "&viewAllCate=" + viewAll + "&NoNotebook=" + cbNoNotebook);
        }


      

        $('#btnEbaySaleItemList').on('click', function () {
            $('#iframe2').attr('src', '../Manager/eBay/eBayPromoitionalSaleManage.aspx');
        });

    </script>
</asp:Content>

