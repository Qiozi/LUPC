<%@ Page Title="Shopping Cart - LU Computers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ShoppingCart.aspx.cs" Inherits="ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="/Content/cart.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div class="panel panel-default">
            <div class="panel-heading"><span class="glyphicon glyphicon-home"></span><a href='default.aspx'>&nbsp;Home <span class="glyphicon glyphicon-menu-right"></span></a>Shopping Cart Contents</div>
            <div class="panel-body">
                <div id="prodListArea">Loading...</div>
                <hr size="1" />
                <h3 class="text-center">How would you like to receive your order?</h3>
                <p class="well" style="width: 400px; margin: 0 auto 10px;">
                    <a class="btn btn-default btn-block " onclick="pickUp();return false;" id="btn-pickup">I will pick up my order and pay at your store.</a>
                    <a class="btn btn-default btn-block" onclick="shipMy();return false;" id="btn-shipmy">Please ship my items to me.</a>
                </p>
                <p id="country-stat-area"></p>
                <div id="payment-area-parent" style="display: none;">
                    <p class="text-center"><span class="glyphicon glyphicon-chevron-down"></span></p>
                    <div class="row">
                        <div class="col-md-1"></div>
                        <div class="col-md-5">
                            <div id="shippingCompayListArea" class="well" style="width: 400px; margin: 0 auto;"></div>
                        </div>
                        <div class="col-md-5">
                            <div id="paymentListArea" class="well" style="width: 400px; margin: 0 auto;"></div>
                        </div>
                        <div class="col-md-1"></div>
                    </div>
                    <p class="text-center"><span class="glyphicon glyphicon-chevron-down"></span></p>
                </div>
                <p id="price-area"></p>
                <p class="well text-center">
                    <a class="btn btn-default" href="/default.aspx"><span class="glyphicon glyphicon-home"></span>&nbsp;Continue Shopping</a>
                    <span class="nextBtnArea">
                        <a class="btn btn-default disabled btnCheckout" id="btnCheckout">Check Out&nbsp;<span class="glyphicon glyphicon-arrow-right"></span></a>
                    </span>
                    <span class="nextBtnAreaPaypal hidden">
                        <a class="btn btn-default " id="btnCheckoutPaypal">
                            <img src='https://www.paypalobjects.com/webstatic/en_US/btn/btn_buynow_pp_142x27.png' border='0' align='top' alt='Buy Now with PayPal' />
                        </a>
                    </span>
                </p>
                <div class="note">
                    <h4 class="note">You can find out the total amount BEFORE you check out.</h4>
                    <p class="note">
                        Please select your destination state / province and shipping method below.
    Orders are processed and shipped Monday through Friday. In-stock items and special orders (when available) are usually shipped immediately. You will be notified for any items if not shipped right away. Computer systems are usually shipped in 1-7 business days. But fast shipping is not guaranteed. LU Computers is a fast shipper; we take every effort to ship your item as soon as possible.
                    </p>
                    <h4 class="note">Shopping with LU Computers is safe and secure!</h4>
                    <p class="note">
                        To protect your transaction, we use GeoTrust's service and 128-bit Secure Sockets Layer (SSL) technology, thereby offering the highest level of encryption or security possible. This means you can rest assured that communications between your browser and this site's web servers are private and secure, and your personal information is also stored securely in our server.
    LU Computers reserves the right to change above shipping fees if the actual shipping costs are significantly greater than above estimate.
                    </p>
                </div>
            </div>
        </div>
    </div>
    <div id="backupBtnList1" style="display: none;">
        <p class="text-center"><span class=" glyphicon glyphicon-chevron-down"></span></p>
        <div class="well" style="width: 400px; margin: 0 auto 10px;">

            <div class="btn-group">
                <a class="btn btn-default btn-block payBtn" onclick="payment(22, $(this));return false;">Local Pick up and <b>Cash</b> Paymenth</a>
                <a class="btn btn-default btn-block payBtn" onclick="payment(23, $(this));return false;">Local Pick up and <b>Debit Card</b> Paymenth</a>
                <a class="btn btn-default btn-block payBtn" onclick="payment(24, $(this));return false;">Local Pick up and <b>Credit Card</b> Paymenth</a>
            </div>
        </div>
    </div>
    <div id="backupBtnList2" style="display: none;">
        <p class="text-center"><span class="glyphicon glyphicon-chevron-down"></span></p>
        <div class="panel panel-default" style="width: 900px; margin: 0 auto 10px;">
            <div class="panel-heading">Please select ship to location.  <span id="currStateLocation" style="color: Green;"></span></div>
            <div class="panel-body">
                <ul class="nav nav-tabs">
                    <li role="presentation" class="active"><a href="#tabStateCanada" data-toggle="tab">Canada</a></li>
                    <li role="presentation"><a href="#tabStateUS" data-toggle="tab">United States</a></li>
                    <%--  <li role="presentation"><a href="#tabStateOther" data-toggle="tab">Other</a></li>--%>
                </ul>
                <div class="tab-content" style="border-left: 1px solid #ccc; border-right: 1px solid #ccc; border-bottom: 1px solid #ccc; padding: 1em;">
                    <div class="tab-pane active" id='tabStateCanada'></div>
                    <div class="tab-pane " id='tabStateUS'></div>
                    <div class="tab-pane " id='tabStateOther'>
                    </div>
                </div>
                <div style="display: none;" id="tabSelectedState"></div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
   <%= WebExtensions.CombresLink("siteJsCart") %>
</asp:Content>

