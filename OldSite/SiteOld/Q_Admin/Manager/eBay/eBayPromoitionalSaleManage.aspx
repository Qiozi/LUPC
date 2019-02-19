<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/Manager/MasterPage.master" AutoEventWireup="true" CodeFile="eBayPromoitionalSaleManage.aspx.cs" Inherits="Q_Admin_Manager_eBay_eBayPromoitionalSaleManage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div style="padding: 1em; background: #f2f2f2; border: 1px solid #ccc;">
        <input type="button" id="btnEbayOnsale" value="eBay onsale" />
        <input type="button" id="btnEbayOnsaleHaveSaleId" value="eBay Onsale have Sale Id" />
        <input type="button" id="btnEbayOnsaleLoadInfo" value="eBay loadinfo" />
        <span class="ebayTime">eBay Time:</span>
        <input type="hidden" id="token" value="<%= Token %>" />
    </div>
    <table>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>Title</td>
                        <td>
                            <asp:TextBox ID="txtSaleTitle" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Cost</td>
                        <td>

                            <asp:TextBox ID="txtSaleCost" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>Begin Date</td>
                        <td>

                            <asp:TextBox ID="txtBeginSale" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>End Date</td>
                        <td>

                            <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="text-center" colspan="2">
                            <asp:Button ID="btnCreateSaleId" runat="server" Text="Create" OnClick="btnCreateSaleId_Click" />
                            <asp:Label ID="lblCreateOnsaleNote" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                <table>
                    <tr>
                        <td>SKU</td>
                        <td>
                            <asp:TextBox ID="txtSku" runat="server"></asp:TextBox>
                        </td>

                    </tr>
                    <tr>
                        <td>Sale Id</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSaleIds"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td colspan="2" class="text-center">
                            <asp:Button ID="btnAddSKU" runat="server" Text="Add" OnClick="btnAddSKU_Click" />
                            <asp:Label ID="lblAddItemNote" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <hr size="1" />
    <table class="table">
        <thead>
            <tr>
                <th>Sku</th>
                <th>Part Name</th>
                <th>Title</th>
                <th>Item Id</th>
                <th>Sale Id</th>
                <th>Begin Date</th>
                <th>End Date</th>
                <th>Cost</th>
                <th>Price</th>
                <th>eBay Price</th>
                <th>Discount</th>
            </tr>
        </thead>
        <tbody id="eBayPromoitionalSaleList">
            <asp:Repeater runat="server" ID="rptList">
                <ItemTemplate>
                    <tr>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Sku") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "PartName") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Title") %>
                        </td>
                        <td>
                            <a href='http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=<%# DataBinder.Eval(Container.DataItem, "ItemId") %>' target="_blank">
                                <%# DataBinder.Eval(Container.DataItem, "ItemId") %></a>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "eBaySaleId") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "StartDate")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "EndDate") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Cost")%>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Price") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "eBayPrice") %>
                        </td>
                        <td>
                            <%# DataBinder.Eval(Container.DataItem, "Discount") %>
                        </td>
                    </tr>
                </ItemTemplate>
            </asp:Repeater>
        </tbody>
    </table>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footScript" runat="Server">
    <script>
        $().ready(function () {
            $.get('http://webapi.lucomputers.com/Api/GeteBayTime', { t: $('#token').val() }, function (data) {
                var msg = eval('(' + data + ')');
                if (msg.Success) {
                    $('.ebayTime').html('eBay Time: &nbsp;' + msg.Data);
                }
                else {
                    alert(msg.errMsg);
                }
            });

            $('#btnEbayOnsale').on('click', function () {

                if (confirm('are you sure?')) {
                    $.post("http://webapi.lucomputers.com/Api/SetPromotionalSale", { t: $('#token').val(), BeginDate: '', EndDate: '', Title: '', Discount: '', AutoRun: true }, function (data) {
                        var msg = eval('(' + data + ')');
                        if (!msg.Success) {
                            alert(msg.ErrMsg);
                        }
                        else {
                            $('#currEbaySaleId').val(msg.Data);
                            alert("Ok");
                        }
                    });
                }
                // $('#iframe2').attr('src', '../Manager/eBay/eBayPromoitionalSaleManage.aspx');
            });

            $('#btnEbayOnsaleHaveSaleId').on('click', function () {

                if (confirm('are you sure?')) {
                    $.post("http://webapi.lucomputers.com/Api/SetPromotionalSaleListings", { t: $('#token').val(), SaleId: $('#currEbaySaleId').val(), Sku: 0, ItemIds: '', IsSys: false, AutoRun: true, AutoRunPrice: 50 }, function (data) {
                        var msg = eval('(' + data + ')');
                        if (!msg.Success) {
                            alert(msg.ErrMsg);
                        }
                        else {

                            alert("Ok");
                        }
                    });
                }
            });

            $('#btnEbayOnsaleLoadInfo').on('click', function () {
                $.post("http://webapi.lucomputers.com/Api/GetPromotionalSaleDetails/Get?t=" + $('#token').val(), {}, function () {
                    alert('OK');
                });
            });
        });
    </script>
</asp:Content>

