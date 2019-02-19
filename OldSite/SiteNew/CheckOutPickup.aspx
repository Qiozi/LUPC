<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="CheckOutPickup.aspx.cs" Inherits="CheckOutPickup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: #74C774; padding: 0px;">
        <div style="padding-bottom: 0px; background: #74C774; border: 0px; padding: 15px;">
            <ul class="list-inline" style="margin-left: 2em;">
                <li>
                    <div>
                        <span class="glyphicon glyphicon-home"></span>
                        <a href="/default.aspx" style="color: White; font-size: 20px;">Home</a>
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
                    <div style="font-size: 20px; color: White;">LOCAL PICK UP</div>
                </li>
            </ul>
        </div>
        <div style="background-color: White; margin: -1em 3rem 3rem 3rem; padding: 1rem">
            <p class="note">
                We welcome our local Toronto customers to pick up your order from our store location. No shipping or handling fee will be applied.
            </p>
            <p class="note">
                Credit card is required to confirm your order for processing. At the time of pick up, you may pay with cash, debit or credit card. If paying by credit card at the time of pickup, your card must be presented to be swiped, and the card holder must present to sign.
            </p>
            <p class="note">
                Please note that credit card payments at the time of pick up do not qualify for our special cash discount.
            </p>
            <p class="note">
                Please note that we do not accept checks, money orders, cashier's checks or PayPal for pick up.
            </p>
            <hr size="1" />
            <div>
                <form id="checkOutForm" class="checkOutForm form-horizontal" name="checkOutForm">
                    <input type="hidden" name="cmd" value="savePickup" />
                    <input type="hidden" name="shippingID" value="<%= ReqShippingID %>" />
                    <input type="hidden" name="stateID" value="<%= ReqStateID %>" />
                    <input type="hidden" name="purl" value="<%= Request.RawUrl %>" />
                    <input type="hidden" name="Payment" value="<%= ReqPayment %>" />
                    <div style="padding: 3rem 5rem 5rem 5rem;">
                        <div id="legend" class="">
                            <legend class="">YOUR PICK UP DATE & TIME</legend>
                            <small>You may pick up your item the same day or next day for light systems, within 2-5 days for heavy systems. Please feel free to contact us to arrange your pick up. </small>
                        </div>
                        <div class="form-group" style="padding: 2rem;">
                            <label class="col-sm-3 control-label">
                                Pick up Date 
                            </label>
                            <div class="col-sm-6 control-label" style="text-align:left;">
                                Date:
                            <input type="text" name="pickupDate" id="pickupDate" readonly />
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>
                        <div class="<%= showCreditCard %>">
                            <h4 class="title1">CREDIT CARD (for deposit purpose only):</h4>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Card Type:*</label>
                                <div class="col-sm-6">
                                    <select class="form-control" name="cardType" id="selectCartType">
                                        <option value="MC" <%= CardType=="MC"?" selected='true'":"" %>>Master Card</option>
                                        <option value="VS" <%= CardType=="VS"?" selected='true'":"" %>>Visa</option>
                                    </select>
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Card Number:*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" name="cardNumber" id="cardNumber" value="<%= CardNumber %>" placeholder="(Do not enter space.) ">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Card Expiry Date:*</label>
                                <div class="col-sm-6">
                                    Month:
                                <select class="form-control" name="cartExpiryMonth" id="selectCartExpiryMonth">
                                
                                    <option value="01" <%= CardExpiryMonth=="01"?" selected='true'":"" %>>01</option>
                                    <option value="02" <%= CardExpiryMonth=="02"?" selected='true'":"" %>>02</option>
                                    <option value="03" <%= CardExpiryMonth=="03"?" selected='true'":"" %>>03</option>
                                    <option value="04" <%= CardExpiryMonth=="04"?" selected='true'":"" %>>04</option>
                                    <option value="05" <%= CardExpiryMonth=="05"?" selected='true'":"" %>>05</option>
                                    <option value="06" <%= CardExpiryMonth=="06"?" selected='true'":"" %>>06</option>
                                    <option value="07" <%= CardExpiryMonth=="07"?" selected='true'":"" %>>07</option>
                                    <option value="08" <%= CardExpiryMonth=="08"?" selected='true'":"" %>>08</option>
                                    <option value="09" <%= CardExpiryMonth=="09"?" selected='true'":"" %>>09</option>
                                    <option value="10" <%= CardExpiryMonth=="10"?" selected='true'":"" %>>10</option>
                                    <option value="11" <%= CardExpiryMonth=="11"?" selected='true'":"" %>>11</option>
                                    <option value="12" <%= CardExpiryMonth=="12"?" selected='true'":"" %>>12</option>
                                </select>
                                    Year:
                                <select class="form-control" name="cartExpiryYear" id="selectCartExpiryYear">                                  
                               
                                    <option value="2018" <%= CardExpiryYear=="2018"?" selected='true'":"" %>>2018</option>
                                    <option value="2019" <%= CardExpiryYear=="2019"?" selected='true'":"" %>>2019</option>
                                    <option value="2020" <%= CardExpiryYear=="2020"?" selected='true'":"" %>>2020</option>
                                    <option value="2021" <%= CardExpiryYear=="2021"?" selected='true'":"" %>>2021</option>
                                    <option value="2022" <%= CardExpiryYear=="2022"?" selected='true'":"" %>>2022</option>
                                    <option value="2023" <%= CardExpiryYear=="2023"?" selected='true'":"" %>>2023</option>
                                    <option value="2024" <%= CardExpiryYear=="2024"?" selected='true'":"" %>>2024</option>
                                    <option value="2025" <%= CardExpiryYear=="2025"?" selected='true'":"" %>>2025</option>
                                    <option value="2026" <%= CardExpiryYear=="2026"?" selected='true'":"" %>>2026</option>
                                    <option value="2027" <%= CardExpiryYear=="2027"?" selected='true'":"" %>>2027</option>
                                    <option value="2028" <%= CardExpiryYear=="2028"?" selected='true'":"" %>>2028</option>
                                    <option value="2029" <%= CardExpiryYear=="2029"?" selected='true'":"" %>>2029</option>
                                </select>
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">
                                    Card Verification Number:*
                                </label>
                                <div class="col-sm-6">
                                    <input type="text" class="form-control" size="80" id="cardVerificationNumber" value="<%= CardVerificationNumber %>"
                                        name="cardVerificationNumber" placeholder="">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Card Issuer:*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" id="cardIssuer" name="cardIssuer" placeholder="" value="<%= CardIssuer %>">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Card Issuer's Telephone Number:*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" name="cardTelephone" id="cardTelephone" value="<%=CardIssuerTelephone %>" placeholder="Format:555-77-8888">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <h4 class="title1">CREDIT CARD HOLDER:</h4>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">First Name:*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" name="firstName" id="firstName" value="<%= CardFirstName %>" placeholder="">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Last Name:*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" name="lastName" id="lastName" value="<%= CardLastName %>" placeholder="">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Card Billing / Shipping Address:*</label>
                                <div class="col-sm-6">
                                    <textarea class="form-control" name="cardShippingAddress" id="cardShippingAddress" cols="40" rows="3"><%= CardBillingShippingAddress %></textarea>
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">City:*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" id="cardCity" name="cardCity" value="<%= CardCity %>" placeholder="">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">State (Province)*</label>
                                <div class="col-sm-6">
                                    <select name="stateProvince" id="stateProvince" class="form-control">
                                        <optgroup label="Canada">
                                            <asp:Literal runat="server" ID="stateOption"></asp:Literal>
                                        </optgroup>
                                    </select>
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                            <div class="form-group">
                                <label class="col-sm-3 control-label">Zip (Post) Code*</label>
                                <div class="col-sm-6">
                                    <input type="text" size="80" class="form-control" id="zipCode" name="zipCode" value="<%= ZipCode %>" placeholder="">
                                </div>
                                <div class="col-sm-3  control-label"></div>
                            </div>
                        </div>
                        <h4 class="title1">CONTACT INFORMATION:</h4>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">First Name:</label>
                            <div class="col-sm-6">
                                <input type="text" size="80" class="form-control" name="firstName2" id="firstName2" value="<%= FirstName %>" placeholder="">
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Last Name:</label>
                            <div class="col-sm-6">
                                <input type="text" size="80" class="form-control" name="lastName2" id="lastName2" value="<%= LastName %>" placeholder="">
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>

                        <div class="form-group">
                            <label class="col-sm-3 control-label">Email Address:</label>
                            <div class="col-sm-6">
                                <input type="email" class="form-control" id="email" name="email" value="<%= Email1 %>" placeholder="">
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Business Phone:*</label>
                            <div class="col-sm-6">
                                <input type="text" size="80" class="form-control" id="businessPhone" value="<%= BusinessPhone %>" name="businessPhone" placeholder="">
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">Home Phone: </label>
                            <div class="col-sm-6">
                                <input type="text" size="80" class="form-control" id="homePhone" value="<%= HomePhone %>" name="homePhone" placeholder="">
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">CUSTOMER COMMENT / NOTE:</label>
                            <div class="col-sm-6">
                                <textarea class="form-control" cols="40" name="comment" id="commentNote" rows="3"></textarea>
                            </div>
                            <div class="col-sm-3  control-label"></div>
                        </div>
                        <div class="text-center" style="padding: 1em;">
                            <a class="btn btn-default btn-warning" href="ShoppingCart.aspx" style="width: 120px;">
                                <span class="glyphicon glyphicon-chevron-left"></span>
                                &nbsp;Black
                            </a>
                            <a tabindex="0" class="btn btn-default btn-success" id="btnNext" style="width: 120px;"
                                role="button" data-toggle="popover" data-trigger="focus" title="Info" data-content="">Next&nbsp;
                            <span class="glyphicon glyphicon-chevron-right"></span>
                            </a>
                        </div>
                        <a class="btn btn-default disabled btn-success" id="noteBtn" style="display: none;"></a>
                    </div>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            $('[data-toggle="tooltip"]').tooltip()

            $('#businessPhone').change(function () {
                if ($.trim($(this).val()) != "")
                    $(this).parent().removeClass("has-error");
            });

            var validate = $("#checkOutForm").validate({
                rules: {
                    businessPhone: {
                        required: true,
                        maxlength: 30,
                        minlength: 5
                    },
                    pickupDate: {
                        required: true
                    },
                    firstName2: {
                        required: true
                    },
                    lastName2: {
                        required: true
                    },
                    cardVerificationNumber: {
                        required: true
                    },
                    cardNumber: {
                        required: true
                    },
                    cardIssuer: {
                        required: true
                    }
                },
                messages: {
                    user: {

                    },
                    businessPhone: {

                    },
                    pickupDate: {

                    },
                    firstName2: {

                    },
                    lastName2: {

                    }
                },
                errorPlacement: function (error, element) {
                    error.css({ color: 'red', 'font-weight': 'normal' });
                    element.parent().next().css({ 'text-align': 'left' }).html(error);
                },
                submitHandler: function () {
                    $.ajax({
                        type: "post",
                        url: "/cmds/UserSaveInfo.aspx",
                        data: $('#checkOutForm').serialize(),
                        error: function (g, s, t) {
                            util.alertError(s);
                        },
                        success: function (msg, s) {
                            if (msg == "OK")
                                window.location.href = "/ShoppingCartGoView.aspx";
                            else
                                util.alertError(msg);
                        }
                    });
                }
            });
            $('#btnNext').click(function () {
                $('#checkOutForm').submit();
            });
            $("#pickupDate").datetimepicker({ format: 'mm/dd/yyyy hh:ii', minuteStep: 20 });
        });
    </script>
</asp:Content>

