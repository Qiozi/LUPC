<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true"
    CodeFile="CheckOutCreditCard.aspx.cs" Inherits="CheckOutCreditCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .form-group {
            display: block;
            margin-bottom: 15px;
        }

            .form-group > label:first-child {
                width: 200px;
                font-weight: 200;
            }

        h4 {
            margin-left: -1em;
        }

        .error {
            color: Red;
            font-weight: normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form class="form-inline" id="checkOutForm" name="checkOutForm" role="form" action="javascript:formSubmit();"
        method="post">
        <div class="container" style="background: #74C774; padding: 0px;">
            <div style="padding-bottom: 0px; background: #74C774; border: 0px solid red; padding: 15px 15px 0px 15px;">
                <ul class="list-inline" style="margin-left: 2em;">
                    <li>
                        <div>
                            <a href="default.aspx" style="color: White; font-size: 20px;"><span class="glyphicon glyphicon-home"></span>Home</a> <span class="glyphicon glyphicon-menu-right"></span>
                        </div>
                    </li>
                    <li>
                        <div>
                            <a href="ShoppingCart.aspx" style="color: White; font-size: 20px;"><span class="glyphicon glyphicon-shopping-cart"></span>Shopping Cart</a> <span class="glyphicon glyphicon-menu-right"></span>
                        </div>
                    </li>
                    <li>
                        <div style="font-size: 20px; color: White;">
                            PAYING BY CREDIT CARD
                        </div>
                    </li>
                </ul>
            </div>
            <div style="background-color: White; margin: 0 3em 3em 3em; padding: 5px;">
                <div class="row">
                    <div class="col-md-8 note">
                    </div>
                    <div class="col-md-4 text-right">
                        <strong>Please call us for any questions:</strong><br />
                        Toll Free: 1 (866) 999-7828<br />
                        Toronto Local: (416) 446-7828
                    </div>
                </div>
                <div class="note" style="padding: 2em 2em 0 2em;">
                    <strong>PAYING BY CREDIT CARD</strong> We accept VISA and Master Card only. We accept
                US and Canadian bank issuing credit cards only.
                </div>
                <hr size="1" />
                <p style="padding: 0em 2em 0 2em;">
                    NOTE: Most items on this form are required.
                </p>
                <fieldset style='width: 80%; margin: 0 auto;'>
                    <input type="hidden" name="cmd" value="saveCreditCard" />
                    <input type="hidden" name="shippingID" value="<%= ReqShippingID %>" />
                    <input type="hidden" name="stateID" value="<%= ReqStateID %>" />
                    <input type="hidden" name="purl" value="<%= Request.RawUrl %>" />
                    <h4>SHIPPING ADDRESS:
                    </h4>
                    <div class="form-group">
                        <label>
                            First Name:</label>
                        <input type="text" class="form-control" size="80" name="firstName" id="firstName"
                            value="<%= FirstName %>" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Last Name:</label>
                        <input type="text" class="form-control" size="80" name="lastName" id="lastName" value="<%= LastName %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Address:</label>
                        <input type="text" class="form-control" size="80" name="address" id="address" value="<%= Address %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Country:</label>
                        <input type="text" disabled="true" size="80" class="form-control" name="shippingcountry"
                            id="shippingcountry" value="Canada" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            City:</label>
                        <input type="text" class="form-control" size="80" name="shippingCity" id="shippingCity"
                            value="<%= City %>" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            State (Province)</label>
                        <select name="stateProvince" id="stateProvince" class="form-control">
                            <optgroup label="Canada">
                                <asp:Literal runat="server" ID="stateOption"></asp:Literal>
                                <optgroup>
                                </optgroup>
                        </select>
                    </div>
                    <div class="form-group">
                        <label>
                            Zip(Post) Code:</label>
                        <input type="text" class="form-control" size="80" name="shippingZipCode" id="shippingZipCode"
                            value="<%= ZipCode %>" placeholder="">
                    </div>
                    <h4>CREDIT CARD BILLING INFORMATION:</h4>
                    <p>
                        Address and telephone number must be exactly same as appeared on your credit card
                    monthly statement. Please check for accuracy; incorrect information causes delay
                    in processing of your orders.
                    </p>
                    <div class="form-group">
                        <label>
                            First Name:</label>
                        <input type="text" class="form-control" size="80" id="cardFirstName" name="cardFirstName"
                            value="<%= CardFirstName %>" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Last Name:</label>
                        <input type="text" class="form-control" size="80" id="cardLastName" name="cardLastName"
                            value="<%= CardLastName %>" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Card Billing Address:</label>
                        <input type="text" class="form-control" size="80" id="cardBillingAddress" name="cardBillingAddress"
                            value="<%= CardBillingAddress %>" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Country:</label>
                        <input type="text" disabled="true" size="80" class="form-control" name="cardCountry"
                            id="cardCountry" value="Canada" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            City:</label>
                        <input type="text" class="form-control" size="80" name="cardCity" id="cardCity" value="<%= CardCity %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            State (Province)</label>
                        <select name="cardState" id="cardState" class="form-control">
                            <optgroup label="Canada">
                                <asp:Literal runat="server" ID="ltState"></asp:Literal>
                                <optgroup>
                                </optgroup>
                        </select>
                    </div>
                    <div class="form-group">
                        <label>
                            Zip(Post) Code:</label>
                        <input type="text" class="form-control" size="80" name="cardZipcode" id="Text3" value="<%= CardZipcode %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Telephone:</label>
                        <input type="text" class="form-control" size="80" id="cardTelephone" value="<%= CardTelephone %>"
                            name="cardTelephone" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Card Type:
                        </label>
                        <select name="CardType" class="form-control" id="CardType" style="width: 150px;"
                            tabindex="19" onchange="StoreSession('s_card_type', this.options[this.selectedIndex].value);">
                            <option value="Visa" selected>Visa</option>
                            <option value="MasterCard">MasterCard</option>
                            <option value="Discover">Discover</option>
                            <option value="Amex">American Express</option>
                        </select>
                    </div>
                    <div class="form-group">
                        <label>
                            Card Number:
                        </label>
                        <input type="text" class="form-control" size="80" id="cardNumber" value="<%= CardNumber %>"
                            name="cardNumber" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Card Expiry Date:
                        </label>
                        <%--<input type="text" class="form-control" size="80" id="cardExpiryDate" value="<%= CardExpiryDate %>"
                        name="cardExpiryDate" placeholder="">--%>
                    Month:
                    <select name="card_expiry_month" id="card_expiry_month" class="form-control" style="width: 100px;">
                        <option value="-1"></option>
                        <option value="01">01</option>
                        <option value="02">02</option>
                        <option value="03">03</option>
                        <option value="04">04</option>
                        <option value="05">05</option>
                        <option value="06">06</option>
                        <option value="07">07</option>
                        <option value="08">08</option>
                        <option value="09">09</option>
                        <option value="10">10</option>
                        <option value="11">11</option>
                        <option value="12">12</option>
                    </select>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year:
                    <select name="card_expiry_year" id="card_expiry_year" class="form-control" style="width: 100px;">
                        <option value="-1"></option>
                        <option value="2009">2009</option>
                        <option value="2010">2010</option>
                        <option value="2011">2011</option>
                        <option value="2012">2012</option>
                        <option value="2013">2013</option>
                        <option value="2014">2014</option>
                        <option value="2015">2015</option>
                        <option value="2016">2016</option>
                        <option value="2017">2017</option>
                        <option value="2018">2018</option>
                        <option value="2019">2019</option>
                        <option value="2020">2020</option>
                        <option value="2021">2021</option>
                        <option value="2022">2022</option>
                        <option value="2023">2023</option>
                        <option value="2024">2024</option>
                        <option value="2025">2025</option>
                    </select>
                    </div>
                    <div class="form-group">
                        <label>
                            Card Verification Number:
                        </label>
                        <input type="text" class="form-control" size="80" id="cardVerificationNumber" value="<%= CardVerificationNumber %>"
                            name="cardVerificationNumber" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Card Issuing Bank:
                        </label>
                        <input type="text" class="form-control" size="80" id="cardIssuingBank" value="<%= CardIssuingBank %>"
                            name="cardIssuingBank" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Card Issuing Bank's Telephone Number:
                        </label>
                        <input type="text" class="form-control" size="80" id="cardIssuingTelephone" value="<%= CardIssuingTelephone %>"
                            name="cardIssuingTelephone" placeholder="">
                    </div>
                    <div class="form-group">
                        <label>
                            Your special instructions and notes:</label>
                        <textarea class="form-control" cols="40" size="80" name="notes" id="notes" rows="3"><%= Notes%></textarea>
                    </div>
                </fieldset>
                <div class="text-center" style="padding: 1em;">
                    <a class="btn btn-default btn-warning" href="ShoppingCart.aspx" style="width: 120px;">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        Black</a> 
                    <a class="btn btn-default btn-success" id="btnNext" onclick="$('#checkOutForm').submit();return false;" style="width: 120px;">Next
                        <span class="glyphicon glyphicon-chevron-right"></span></a>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        function formSubmit() {
            $('#__VIEWSTATE').remove();
            $.ajax({
                type: "post",
                url: "cmds/UserSaveInfo.aspx",
                data: $('#checkOutForm').serialize(),
                error: function (g, s, t) {
                    alert(s);
                },
                success: function (msg, s) {
                    if (msg == "OK")
                        window.location.href = "ShoppingCartGoView.aspx";
                    else
                        alert(msg);
                }
            });
        }

        $(document).ready(function () {
            $('form').addClass("form-inline");


            var validate = $("#checkOutForm").validate({
                rules: {
                    firstName: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    },
                    lastName: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    },
                    address: {
                        required: true,
                        maxlength: 80,
                        minlength: 2
                    },
                    shippingCity: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    },
                    shippingZipCode: {
                        required: true,
                        maxlength: 10,
                        minlength: 2
                    },
                    cardFirstName: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    },
                    cardLastName: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    },
                    cardBillingAddress: {
                        required: true,
                        maxlength: 150,
                        minlength: 2
                    },
                    cardCity: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    },
                    cardZipcode: {
                        required: true,
                        maxlength: 10,
                        minlength: 2
                    },
                    cardTelephone: {
                        required: true,
                        maxlength: 20,
                        minlength: 2
                    },
                    cardNumber: {
                        required: true,
                        maxlength: 20,
                        minlength: 2
                    },
                    cardExpiryDate: {
                        required: true,
                        maxlength: 6,
                        minlength: 4
                    },
                    cardVerificationNumber: {
                        required: true,
                        maxlength: 4,
                        minlength: 2
                    },
                    cardIssuingBank: {
                        required: true,
                        maxlength: 50,
                        minlength: 2
                    },
                    cardIssuingTelephone: {
                        required: true,
                        maxlength: 50,
                        minlength: 2
                    }
                },
                messages: {
                    user: {

                    }
                },
                errorPlacement: function (error, element) {
                    if (element.is(":radio")) {
                        error.appendTo(element.parent());
                    }
                    else if (element.is(":checkbox")) {
                        error.appendTo(element.parent());
                    }
                    else if (element.is("input[name=captcha]")) {
                        error.appendTo(element.parent());
                    } else {
                        error.insertAfter(element);
                    }
                },
                success: function (label) {
                    // label.addClass("hide");
                }
            });


            $("input:reset").click(function () {
                validate.resetForm();
            });

        });
    </script>
</asp:Content>
