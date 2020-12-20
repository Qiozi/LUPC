<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sales_index.aspx.cs" Inherits="Q_Admin_sales_index" Title="Manage Index" %>

<%@ Register Src="UC/AlertMessage.ascx" TagName="AlertMessage" TagPrefix="uc1" %>
<%@ Register Src="UC/OnSaleInfo.ascx" TagName="OnSaleInfo" TagPrefix="uc2" %>
<%@ Register Src="UC/CustomerMsgList.ascx" TagName="CustomerMsgList" TagPrefix="uc3" %>
<%@ Register Src="UC/IncStat.ascx" TagName="IncStat" TagPrefix="uc4" %>
<%--<%@ Register Assembly="OpenFlashChart" Namespace="OpenFlashChart" TagPrefix="cc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-top: 5em; background: white;">

        <br />
        <%--<input type="button" value="变化首页--当有修改价格时，请变化首页，因首页是静态文件" 
            style="height: 58px" onclick="winOpen('netcmd/ChangeHomePageAndPrice.aspx?homepage=1', 300, 300, 300, 300);" />--%>

        <table width="1260">
            <tr>
                <td style="width: 450px">
                    <table width="100%" style="border: 1px solid #D1DAF6;">
                        <tr>
                            <td colspan="2" style="background: #D1DAF6; text-align: left; padding: 5px">当前在eBay Sys 中零价格产品</td>
                        </tr>
                        <tr>
                            <td>
                                <div id='e_loading'>
                                    <img src="/soft_img/tags/loading.gif" /></div>
                                <iframe src="/q_admin/eBayMaster/lu/view_ebay_sys_part_zooe_price.asp" frameborder="0" height="150" width="100%"></iframe>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <uc1:AlertMessage ID="AlertMessage1" runat="server" Visible="True" />
                    <br />
                    <uc2:OnSaleInfo ID="OnSaleInfo1" runat="server" />

                    <br />

                    Current Currency Converter:<asp:Label runat="server" ID="lbl_currency_converter" Font-Bold="True" Font-Size="Large"></asp:Label>
                    <a href="change_currency_converter.aspx" class="blue" onclick="return js_callpage_cus(this.href, 'change_currency', 500, 400);">Change</a>
                    <asp:Repeater runat="server" ID="rpt_currency_converter">
                        <HeaderTemplate>
                            <div style="background: #cccccc;">
                                <table style="width: 100%" cellspacing="1" id="currency_converter">
                                    <tr>
                                        <th>ID</th>
                                        <th>Canadian Dollar</th>
                                        <th>U.S. Dollar</th>
                                        <th></th>
                                        <th></th>
                                    </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "id") %>
                                </td>
                                <td style="text-align: right">
                                    <%# DataBinder.Eval(Container.DataItem, "currency_cad") %>
                                </td>
                                <td style="text-align: right">
                                    <%# DataBinder.Eval(Container.DataItem, "currency_usd") %>
                                </td>
                                <td style="text-align: right">
                                    <%# DataBinder.Eval(Container.DataItem, "regdate") %>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "is_auto") %>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                                </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
                <td valign="top" style="border: 1px solid #ccc; width: 820px">
                    <h2 style="background: #f2f2f2; padding: 3px;">profit  & price change history</h2>
                    <p>
                        SKU/eBay Itemid:<input type="text" id="searchKey" /><input type="button" value="Profit" onclick="queryProfit();" /><input type="button" value="Price change history" onclick="    queryHistory();" />
                    </p>
                    <div id="queryResultArea">
                    </div>
                </td>
            </tr>
        </table>
        <hr size="1" style="color: #A2B5DA" />



        <uc4:IncStat ID="IncStat1" runat="server" Visible="false" />
        <%--<hr size="1" style="color:#A2B5DA" />--%>


        <div style="margin-top: 1em; margin-left: 1em;">
            <div>
                <asp:Label runat="server" ID="lbl_30day_total" Font-Bold="True"
                    Font-Size="12pt"></asp:Label>
              <%--  <cc1:OpenFlashChartControl ID="OpenFlashChartControl1" runat="server"
                    EnableCache="false" LoadingMsg="test loading..." Height="350px"
                    DataFile="/q_admin/OpenFlashChartData.aspx?cmd=GetStat30Days"
                    Width="1240px">
                </cc1:OpenFlashChartControl>--%>
            </div>
        </div>

        <div style="margin-top: 1em; margin-left: 1em;">
            <div>
                <asp:Label runat="server" ID="lbl_current_month_total" Font-Bold="True"
                    Font-Size="12pt"></asp:Label>
             <%--   <cc1:OpenFlashChartControl ID="OpenFlashChartControl2" LoadingMsg=" loading..."
                    runat="server" DataFile="/q_admin/OpenFlashChartData.aspx?cmd=GetStat360Days"
                    Width="1240px">
                </cc1:OpenFlashChartControl>--%>
            </div>
        </div>



    </div>
    <script type="text/javascript">
        $().ready(function () {
            $('#currency_converter th').css("background", "#f2f2f2");
            $('#currency_converter td').css("background", "#ffffff");
        });

        function queryProfit() {
            $('#queryResultArea').html("<img src='/soft_img/tags/loaderc.gif'>");

            $.get("/q_admin/inc/get_ebay_price_info.aspx"
            , { sku: $('#searchKey').val(), showtitle: '1', showebaycode: '1' }
            , function (msg) {
                $('#queryResultArea').html("<hr size='1'>" + msg.replace("value='refresh'", "value='refresh' style='display:none;'"));
            });
        }

        function queryHistory() {
            $('#queryResultArea').html("No model");
        }
    </script>
</asp:Content>


