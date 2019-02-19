<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyAllOrders.aspx.cs" Inherits="MyAllOrders" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        strong {
            color: #666;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <div class="container" style="background: #74C774; padding: 0px;">
        <div style="padding-bottom: 0px; background: #74C774; border: 0px; padding: 15px;">
            <ul class="list-inline" style="margin-left: 2em;">
                <li>
                    <div style="color: White; font-size: 20px;">
                        <span class="glyphicon glyphicon-home"></span>
                        <a href="default.aspx" style="color: White;">Home</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="color: White; font-size: 20px;">
                        <span class="glyphicon glyphicon-user"></span>&nbsp;My Account                                
                            <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="color: White; font-size: 20px;">All Orders</div>
                </li>
            </ul>

        </div>
        <div style="background: white; margin: -1em 3em 3em 3em; padding: 1em;" role="navigation">
            <!-- menu -->
            <ul class="nav nav-tabs">
                <li role="presentation"><a href="MyAccount.aspx">My Profile</a></li>
                <li role="presentation"><a href="MyPendingOrders.aspx">Pending Orders</a></li>
                <li role="presentation" class="active"><a href="MyAllOrders.aspx">All Order</a></li>
            </ul>
            <!-- menu -->

            <div style="min-height: 700px; margin-top: 1em;">
                <asp:Repeater runat="server" ID="rptList"
                    OnItemDataBound="rptList_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-bordered table-hover">
                            <tr>
                                <td><strong></strong></td>
                                <td><strong>Order#</strong></td>
                                <td><strong>Status</strong></td>
                                <td><strong>Date</strong></td>
                                <td><strong>Download</strong></td>
                                <td><strong>Amount</strong></td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td class="note text-center"><%# Container.ItemIndex+1 %></td>
                            <td class="note text-center">
                                <a class="btn" data-toggle="modal" onclick="viewOrder('<%# Eval("OrderCode") %>');" data-target="#myModalViewOrder"><%# Eval("OrderCode") %></a></td>
                            <td class="note"><%# Eval("StatusName") %></td>
                            <td class="note"><%# Eval("OrderDate") %></td>
                            <td class="note"><%# Eval("Download")%></td>
                            <td class="note text-right">$<%# Eval("Amount") %> <small><%# Eval("PriceUnit") %></small></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
                <asp:Literal runat="server" ID="ltNoRecord"></asp:Literal>
            </div>

        </div>
    </div>

    <div class="modal fade" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel" aria-hidden="true" id="myModalViewOrder">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title" id="myModalLabel">Order# <span id="spanCurrOrderCode"></span></h4>
                </div>
                <div class="modal-body">
                    <iframe style="height: 700px; width: 100%; border: 0px" name="iframeViewOrder" id="iframeViewOrder" frameborder="0"></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        function viewOrder(orderCode) {
            $('#spanCurrOrderCode').html(orderCode);
            $('#iframeViewOrder').attr("src", "MyOrderView.aspx?oc=" + orderCode);
        }
    </script>
</asp:Content>



