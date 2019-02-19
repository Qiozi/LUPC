<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyPendingOrders.aspx.cs" Inherits="MyPendingOrders" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        strong {
            color: #666;
        }

        td {
            background: #ECF1F1;
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
                    <div style="color: White; font-size: 20px;">Pending Orders</div>
                </li>
            </ul>

        </div>
        <div style="background: white; margin: -1em 3em 3em 3em; padding: 1em;" role="navigation">
            <!-- menu -->
            <ul class="nav nav-tabs">
                <li role="presentation"><a href="MyAccount.aspx">My Profile</a></li>
                <li role="presentation" class="active"><a href="MyPendingOrders.aspx">Pending Orders</a></li>
                <li role="presentation"><a href="MyAllOrders.aspx">All Order</a></li>
            </ul>
            <!-- menu -->

            <div style="min-height: 700px; margin-top: 1em;">
                <asp:Repeater runat="server" ID="rptList"
                    OnItemDataBound="rptList_ItemDataBound">
                    <ItemTemplate>
                        <table class="table table-bordered">
                            <tr>
                                <td><strong>Order#</strong></td>
                                <td><strong>Date</strong></td>
                                <td><strong>Name</strong></td>
                                <td><strong>Amount</strong></td>
                            </tr>
                            <tr>
                                <td class="note"><%# Eval("OrderCode") %></td>
                                <td class="note"><%# Eval("OrderDate") %></td>
                                <td class="note"><%# Eval("Name") %></td>
                                <td class="note">$<%# Eval("Amount") %><small> <%# Eval("PriceUnit") %></small></td>
                            </tr>
                            <tr>
                                <td><strong>Shipping Date</strong></td>
                                <td><%# Eval("ShippingDate") %></td>
                                <td><strong>UPS Tracking Number</strong></td>
                                <td><%# Eval("UpsTrackingNumber") %></td>
                            </tr>
                            <tr>
                                <td><strong>STATUS:</strong></td>
                                <td class="note" colspan="3" style='padding-left: 3em;'><%# Eval("StatusName") %></td>
                            </tr>
                            <tr>
                                <td colspan="4">
                                    <asp:HiddenField runat="server" ID="hfOrderCode" Value='<%# Eval("OrderCode") %>' />
                                    <asp:Literal runat="server" ID="ltMsgList"></asp:Literal>


                                    <div class="input-group">
                                        <input type="text" class="form-control" oc='<%# Eval("OrderCode") %>' maxlength="200" aria-label="..." />
                                        <div class="input-group-btn">
                                            <a class="btn btn-default sendMsg"><span class="glyphicon glyphicon-envelope"></span>Send seller a message</a>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
                <asp:Literal runat="server" ID="ltNoRecord"></asp:Literal>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('.sendMsg').click(function () {
                var msg = $(this).parent().prev().val();
                var oc = $(this).parent().prev().attr("oc");
                $(this).parent().prev().val('');
                $(this).addClass("disabled");
                $.get("cmds/orders.aspx", { cmd: 'SaveMsg', cont: msg, orderCode: oc, t: Math.random() }, function () {
                    window.location.href = "MyPendingOrders.aspx";
                });
            });
        });


    </script>
</asp:Content>



