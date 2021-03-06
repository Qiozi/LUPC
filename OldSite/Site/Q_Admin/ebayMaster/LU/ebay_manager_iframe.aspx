﻿<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPageH5.master" AutoEventWireup="true" CodeFile="ebay_manager_iframe.aspx.cs" Inherits="Q_Admin_ebayMaster_LU_ebay_manager_iframe" Title="Ebay Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        html, body {
            height: 100%;
            margin: 0;
            padding: 0;
        }

            html > body {
                font-size: 16px;
                font-size: 68.75%;
            }
        /* Reset Base Font Size */

        body {
            font-family: Verdana, helvetica, arial, sans-serif;
            font-size: 68.75%;
            background: #fff;
            color: #333;
        }


        span.demo1 {
            background-color: yellow;
            margin-right: 20px;
            padding: 5px;
        }

        a.top_btn {
            float: right;
            display: block;
            padding: 2px;
            font-size: 12px;
        }

        .selectItemCase {
            padding: 0.3rem;
        }
    </style>
    <script src="/js_css/jquery_lab/jquery.tools.min.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/jquery.cookie.js" type="text/javascript"></script>
    <script src="/q_admin/js/winOpen.js" type="text/javascript"></script>
    <script src="/js_css/jquery_lab/tools.tabs.slideshow-1.0.2.js" type="text/javascript"></script>
    <link href="/js_css/tabs.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="background: #f2f2f2; border-bottom: 1px solid #cccccc; height: 40px; padding-left: 1em; vertical-align: middle;">
        <table cellpadding="0" cellspacing="0" width="100%" border="0" height="40">
            <tr>
                <td style="vertical-align: middle">
                    <input type="text" value="" id="ebay_code" size="30" />
                    <input type="button" value="GO" onclick="$('#ifr_main_frame1').attr('src', '/q_admin/eBayMaster/LU/ebay_system_edit_2.asp?ebay_system_sku=' + $('#ebay_code').val() + '&cmd=modify&viewLeft=true');" /></td>
                <td class="selectCases"></td>
                <td>
                    <input class="dupiSystem" type="button" value="Dupi" /><input class="dupiSystemClear" type="button" value="Clear" />
                </td>
                <td valign="top">
                    <span style="display: none;">
                        <a class="top_btn" href="ebay_number_update.aspx" onclick="window.open(this.href,'ebay_update','left=260px,top=100px,width=400px,height=300px'); return false;">上传Ebay Number</a>
                        <a class="top_btn" href="ebay_system_comment_templete.asp" onclick=" js_callpage_name_custom(this.href,'ebay_comment', 650, 400);return false;" style="cursor: pointer;">修改系统模板</a>
                        <a class="top_btn" href="ebay_system_keyword.asp" onclick=" js_callpage_name_custom(this.href,'ebay_keyword', 650, 400);return false;" style="cursor: pointer;">修改关键字模板</a>
                    </span>
                    <a class="top_btn" href="/q_admin/eBayMaster/lu/eBayTempletePageComment.aspx" onclick="js_callpage_name_custom(this.href, 'eBay_page_comment_edit', 750, 600);return false;">Edit Comment</a>

                    <a class="top_btn" href="/q_admin/eBayMaster/lu/eBay_Sys_Category_Edit.aspx" onclick="js_callpage_name_custom(this.href, 'eBay_cate_edit', 750, 600);return false;">Edit eBay Category =:=</a>

                    <a class="top_btn" href="/q_admin/eBayMaster/ebay_system_templete_edit.aspx" onclick="js_callpage_name_custom(this.href, 'eBay_tpl_edit', 750, 600);return false;">Edit eBay Templete =:=</a>
                </td>
            </tr>
        </table>
    </div>
    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td width="250" valign="top" id='table_left'>

                <!-- the tabs -->
                <ul class="tabs">
                    <li><a href="#">ebay Sys</a></li>
                    <li><a href="#">Group1</a></li>
                    <li><a href="#">Group2</a></li>
                </ul>
                <!-- the 'panes' -->
                <div class="panes">
                    <div>
                        <iframe id="Iframe1" name="ifr_left_frame2" src="/q_admin/ebayMaster/lu/ebay_left_menu.asp?pageType=1" frameborder="0" style="width: 100%"></iframe>
                    </div>
                    <div>
                        <iframe id="Iframe2" name="ifr_left_frame3" src="/q_admin/ebayMaster/lu/ebay_left_menu.asp?pageType=2" frameborder="0" style="width: 100%"></iframe>
                    </div>
                    <div>
                        <iframe id="Iframe3" name="ifr_left_frame4" src="/q_admin/ebayMaster/lu/ebay_left_menu.asp?pageType=3" frameborder="0" style="width: 100%"></iframe>
                    </div>
                </div>

                <asp:Literal runat="server" ID="literal_treeview" Visible="false"></asp:Literal>
            </td>
            <td style="background: #f2f2f2; width: 3px; border-left: 1px solid #ccc; cursor: pointer; font-size: 6pt;" onclick='hideShow();'>::</td>
            <td style="border-left: 1px solid #cccccc;" valign="top">
                <iframe id="ifr_main_frame1" name="ifr_main_frame1" src="" frameborder="0" style="width: 100%; height: 100px; border-bottom: 1px solid #ccc"></iframe>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content runat="server" ID="content3" ContentPlaceHolderID="ContentPlaceHolder3">
    <script type="text/javascript">
        function setSelectedCase(sku, sysSku) {
            if (sku === '') {
                $('.selectCases').html('')
            }
            else {
                $('.selectCases').append('<span class="selectItemCase" data-sku="' + sku + '">' + sku + '|' + sysSku + '</span>');
            }
        }
        $(function () {
            $("ul.tabs").tabs("div.panes > div");

            // window resize
            var _attr = parseInt(document.body.clientHeight);
            $('iframe').css("height", isNaN(_attr) || _attr <= 41 ? "100%" : (_attr - 66) + "px");
            $('#ifr_main_frame1').css("height", isNaN(_attr) || _attr <= 41 ? "100%" : (_attr - 42) + "px");

            $('.dupiSystem').bind('click', function () {
                $('.selectItemCase').each(function () {
                    var caseSku = $(this).html().split('|')[0];
                    var sysSku = $(this).html().split('|')[1];
                    window.open('/q_admin/eBayMaster/LU/ebay_system_edit_2.asp?ebay_system_sku=' + sysSku + '&cmd=modify&viewLeft=true&dupiCaseSku=' + caseSku)
                });
            });

            $('.dupiSystemClear').bind('click', function () {
                $('.selectCases').html('')
            });
        });

        var resizeTimer = null;
        $(window).bind("resize", function () {
            if (resizeTimer)
                clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () {
                var _attr = parseInt(document.body.clientHeight);
                // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";

                $('iframe').css("height", isNaN(_attr) || _attr <= 41 ? "100%" : (_attr - 66) + "px");
                $('#ifr_main_frame1').css("height", isNaN(_attr) || _attr <= 41 ? "100%" : (_attr - 42) + "px");
            }, 100);
        });

        function hideShow() {
            if ($('#table_left').css("display") != 'none')
                $('#table_left').css("display", 'none');
            else
                $('#table_left').css("display", '');
        }
    </script>
</asp:Content>
