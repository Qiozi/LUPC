<%@ Page Title="My Order View - LU Computers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyOrderView.aspx.cs" Inherits="MyOrderView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <div style="background-color: White; margin: -1em 3em 3em 3em; padding: 10px;">
        <h3>LU COMPUTERS ORDER FORM</h3>
        <div class="row">
            <div class="col-sm-3"></div>
            <div class="col-sm-4"></div>
            <div class="col-sm-5">
                <%--  1875 Leslie Street, Unit 24 
                <br />
                Toronto, Ontario, M3B 2M5 
                <br />--%>
                Tel: (866)999-7828 (416)446-7743
            </div>
            <div class="col-md-1"></div>
        </div>
        <hr size='1' />
        <div class="row">

            <div class="col-sm-3">Order Number:</div>
            <div class="col-sm-3"><%= this.cookiesHelper.CurrOrderCode %></div>
            <div class="col-sm-2">Customer#:</div>
            <div class="col-sm-4"><%= CurrCustomer.customer_serial_no %></div>

        </div>
        <div class="row">

            <div class="col-sm-3">Date:</div>
            <div class="col-sm-3"><%= OrderDate %></div>
            <div class="col-sm-2">Payment:</div>
            <div class="col-sm-4"><%= PayMent %></div>

        </div>
        <div class="row">

            <div class="col-sm-3">Customer Name:</div>
            <div class="col-sm-3"><%= this.CustomerName %></div>
            <div class="col-sm-2"></div>
            <div class="col-sm-4"></div>

        </div>

        <hr size='1' />
        <asp:Literal runat="server" ID="ltShipCompany"></asp:Literal>
        <hr size="1" />
        <p><%= ShippingString%></p>
        <hr size="1" />
        <asp:Literal runat="server" ID="ltOrderProdList"></asp:Literal>
        <p>
            <asp:Literal runat="server" ID="ltOrderPriceArea"></asp:Literal>
        </p>

    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#page-bottom').remove();
            $('#top').remove();
            $('body').css({ "background": "white", "top": "1em" });
        });
    </script>
</asp:Content>
