<%@ Page Title="View Order - LU Computers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCartGoView.aspx.cs" Inherits="ShoppingCartGoView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        #tableProdList td {
            padding: 5px;
        }

        #tableProdList th {
            padding: 5px;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: #74C774; padding: 0px;">
        <div style="padding-bottom: 0px; background: #74C774; border: 0px; padding: 15px;">
            <ul class="list-inline" style="margin-left: 2em;">
                <li>
                    <div>
                        <span class="glyphicon glyphicon-home"></span>
                        <a href="/default.aspx" style="color: White; font-size: 20px;">&nbsp;Home</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div>
                        <span class="glyphicon glyphicon-shopping-cart"></span>
                        <a href="ShoppingCart.aspx" style="color: White; font-size: 20px;">Shopping Cart</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="font-size: 20px; color: White;">ORDER FORM</div>
                </li>
            </ul>
        </div>


        <div style="background-color: White; margin: -1em 3em 3em 3em; padding: 10px;">
            <h3>LU COMPUTERS ORDER FORM</h3>
            <div class="row">
                <div class="col-md-3"></div>
                <div class="col-md-5"></div>
                <div class="col-md-3">
   <%--                 1875 Leslie Street, Unit 24 
                    <br />
                    Toronto, Ontario, M3B 2M5 
                    <br />--%>
                    Tel: (866)999-7828 (416)446-7743
                </div>
                <div class="col-md-1"></div>
            </div>
            <hr size='1' />
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2">Order Number:</div>
                <div class="col-md-3"><%= this.cookiesHelper.CurrOrderCode %></div>
                <div class="col-md-2">Customer Number:</div>
                <div class="col-md-3"><%= this.CurrCustomer.customer_serial_no %></div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2">Date:</div>
                <div class="col-md-3"><%= OrderDate %></div>
                <div class="col-md-2">Payment:</div>
                <div class="col-md-3"><%= PayMent %></div>
                <div class="col-md-1"></div>
            </div>
            <div class="row">
                <div class="col-md-1"></div>
                <div class="col-md-2">Customer Name:</div>
                <div class="col-md-3"><%= this.CustomerName %></div>
                <div class="col-md-2"></div>
                <div class="col-md-3"></div>
                <div class="col-md-1"></div>
            </div>
            <hr size='1' />
            <asp:Literal runat="server" ID="ltShipCompany"></asp:Literal>
            <hr size="1" />
            <asp:Literal runat="server" ID="ltOrderProdList"></asp:Literal>
            <p>
                <asp:Literal runat="server" ID="ltOrderPriceArea"></asp:Literal>
            </p>
            <div class="text-center" style="padding: 1em;">
                <a class="btn btn-default btn-warning" href="ShoppingCart.aspx">
                    <spsn class="glyphicon glyphicon-chevron-left"></spsn>
                    Black To Cart</a>
                <span id="myContainer">
                <a class="btn btn-default btn-success" runat="server" href="ShoppingCartGoOrder.aspx" id="btnNext" style="width: 120px">Next
                    <spsn class="glyphicon glyphicon-chevron-right"></spsn>
                </a></span>
            </div>
            <div class="note">
                <p>
                    Sales are subject to LU Computers' sales terms and policies.
                </p>
                <p>
                    No credit for any items that can be replaced. Any returned products must be complete and unused. All returns must be in their original packing material and re-saleable condition. Credit will not be issued unless the conditions are met. Returns must be reported within 14 days and are subject to a 15% and $30 whichever higher restocking charge is. Software and consumable items cannot be returned for credit or replacement.
                </p>
                <p>
                    Warranty claimed items must be shipped /carried in at customer's cost. Returned shipment without a LU issued RMA (Return Merchandise Authorization) number will be rejected. Warranty does not cover services completed by an unauthorized third party
                </p>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <asp:Literal runat="server" ID="litJSFile"></asp:Literal>
    <script type="text/javascript">
        $(function () {
            $('#tableProdList').find('tr').each(function (i) {
                if (i % 2 == 0) {
                    $(this).css({ background: '#f2f2f2' });
                }

            });
        });
    </script>

</asp:Content>

