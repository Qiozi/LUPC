<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="CheckOutPickup.aspx.cs" Inherits="CheckOutPickup" %>

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

        .line-height {
            height: 8px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: #74C774; padding: 0px;">
        <div style="padding-bottom: 0px; background: #74C774; border: 0px; padding: 15px;">
            <ul class="list-inline" style="margin-left: 2em;">
                <li>
                    <div><span class="glyphicon glyphicon-home"></span>
                        <a href="default.aspx" style="color: White; font-size: 20px;">Home</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div><span class="glyphicon glyphicon-shopping-cart"></span>
                        <a href="ShoppingCart.aspx" style="color: White; font-size: 20px;">Shopping Cart</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="font-size: 20px; color: White;">LOCAL PICK UP</div>
                </li>
            </ul>

        </div>
        <div style="background-color: White; margin: -1em 3em 3em 3em; padding: 5px;">
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
                <form class="form-inline" id="checkOutForm" name="checkOutForm" role="form">
                    <input type="hidden" name="cmd" value="savePickup" />
                    <input type="hidden" name="shippingID" value="<%= ReqShippingID %>" />
                    <input type="hidden" name="stateID" value="<%= ReqStateID %>" />
                    <input type="hidden" name="purl" value="<%= Request.RawUrl %>" />

                    <fieldset style='width: 80%; margin: 0 auto;'>
                        <div id="legend" class="">
                            <legend class="">YOUR PICK UP DATE & TIME</legend>
                            <small>You may pick up your item the same day or next day for light systems, within 2-5 days for heavy systems. Please feel free to contact us to arrange your pick up. </small>
                        </div>


                        <div class="form-group has-error">
                            <label for="exampleInputName2"></label>
                            Date:<select class="form-control" name="pickupDate" id="selectDays">
                                <option value="0">Select date</option>
                            </select>
                            Time:
                                <select class="form-control" name="pickupTime" id="selectTime">
                                    <option value="13">13</option>
                                </select>

                        </div>

                        <h4>CREDIT CARD (for deposit purpose only):</h4>
                        <div class="form-group">
                            <label for="exampleInputName2">Card Type:</label>
                            <select class="form-control" name="cardType" id="selectCartType">
                                <option value="">Please select</option>
                                <option value="MC" <%= CardType=="MC"?" selected='true'":"" %>>Master Card</option>
                                <option value="VS" <%= CardType=="VS"?" selected='true'":"" %>>Visa</option>
                            </select>
                        </div>

                        <div class="form-group">
                            <label for="exampleInputName2">Card Number:</label>
                            <input type="text" size="80" class="form-control" name="cardNumber" id="cardNumber" value="<%= CardNumber %>" placeholder="(Do not enter space.) ">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">Card Expiry Date:</label>
                            Month:
                                <select class="form-control" name="cartExpiryMonth" id="selectCartExpiryMonth">
                                    <option value=""></option>
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
                                    <option value=""></option>
                                    <option value="2009" <%= CardExpiryYear=="2009"?" selected='true'":"" %>>2009</option>
                                    <option value="2010" <%= CardExpiryYear=="2010"?" selected='true'":"" %>>2010</option>
                                    <option value="2011" <%= CardExpiryYear=="2011"?" selected='true'":"" %>>2011</option>
                                    <option value="2012" <%= CardExpiryYear=="2012"?" selected='true'":"" %>>2012</option>
                                    <option value="2013" <%= CardExpiryYear=="2013"?" selected='true'":"" %>>2013</option>
                                    <option value="2014" <%= CardExpiryYear=="2014"?" selected='true'":"" %>>2014</option>
                                    <option value="2015" <%= CardExpiryYear=="2015"?" selected='true'":"" %>>2015</option>
                                    <option value="2016" <%= CardExpiryYear=="2016"?" selected='true'":"" %>>2016</option>
                                    <option value="2017" <%= CardExpiryYear=="2017"?" selected='true'":"" %>>2017</option>
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


                        <div class="form-group">
                            <label for="exampleInputName2">Card Issuer:</label>
                            <input type="text" size="80" class="form-control" id="cardIssuer" name="cardIssuer" placeholder="" value="<%= CardIssuer %>">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">Card Issuer's Telephone Number:</label>
                            <input type="text" size="80" class="form-control" name="cardTelephone" id="cardTelephone" value="<%=CardIssuerTelephone %>" placeholder="Format:555-77-8888">
                        </div>

                        <h4>CREDIT CARD HOLDER:</h4>

                        <div class="form-group">
                            <label for="exampleInputName2">First Name:</label>
                            <input type="text" size="80" class="form-control" name="firstName" id="firstName" value="<%= CardFirstName %>" placeholder="">
                        </div>



                        <div class="form-group">
                            <label for="exampleInputName2">Last Name:</label>
                            <input type="text" size="80" class="form-control" name="lastName" id="lastName" value="<%= CardLastName %>" placeholder="">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">Card Billing / Shipping Address:</label>
                            <textarea class="form-control" name="cardShippingAddress" id="cardShippingAddress" cols="40" rows="3"><%= CardBillingShippingAddress %></textarea>
                        </div>



                        <div class="form-group">
                            <label for="exampleInputName2">City:</label>
                            <input type="text" size="80" class="form-control" id="cardCity" name="cardCity" value="<%= CardCity %>" placeholder="">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">State (Province)</label>
                            <select name="stateProvince" id="stateProvince" class="form-control">
                                <optgroup label="Canada">
                                    <asp:Literal runat="server" ID="stateOption"></asp:Literal>
                                </optgroup>
                            </select>
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">Zip (Post) Code</label>
                            <input type="text" size="80" class="form-control" id="zipCode" name="zipCode" value="<%= ZipCode %>" placeholder="">
                        </div>


                        <h4>CONTACT INFORMATION:</h4>

                        <div class="form-group">
                            <label for="exampleInputName2">Email Address:</label>
                            <input type="email" class="form-control" id="email" name="email" value="<%= Email1 %>" placeholder="">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">Business Phone:*</label>
                            <input type="text" size="80" class="form-control" id="businessPhone" value="<%= BusinessPhone %>" name="businessPhone" placeholder="">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">Home Phone: </label>
                            <input type="text" size="80" class="form-control" id="homePhone" value="<%= HomePhone %>" name="homePhone" placeholder="">
                        </div>


                        <div class="form-group">
                            <label for="exampleInputName2">CUSTOMER COMMENT / NOTE:</label>
                            <textarea class="form-control" cols="40" name="comment" id="commentNote" rows="3"></textarea>
                        </div>

                    </fieldset>

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
                </form>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('form').addClass("form-inline");
            $('[data-toggle="tooltip"]').tooltip()

            for (var i = 11; i <= 19; i++) {
                $('#selectTime').append("<option value='" + i + "'>" + i + "</option>");
            }

            $.get("cmds/orders.aspx", { cmd: "getPickupTimeArea", t: Math.random() }, function (msg) {
                var cont = eval("(" + msg + ")");
                $.each(cont, function (index, item) {
                    $('#selectDays').append("<option value='" + item.Value + "'>" + item.Key + "</option>");
                });
            });

            $('#businessPhone').change(function () {
                if ($.trim($(this).val()) != "")
                    $(this).parent().removeClass("has-error");
            });

            $('#selectDays').change(function () {
                if ($(this).val() == 0) {

                }
                else {
                    $(this).parent().removeClass("has-error");
                }

            });

            var validate = $("#checkOutForm").validate({
                rules: {
                    businessPhone: {
                        required: true,
                        maxlength: 30,
                        minlength: 2
                    }
                },
                messages: {
                    user: {

                    }
                },
                errorPlacement: function (error, element) {
                    if (element.is(":radio"))
                        error.appendTo(element.parent());
                    else if (element.is(":checkbox"))
                        error.appendTo(element.parent());
                    else if (element.is("input[name=captcha]"))
                        error.appendTo(element.parent());
                    else
                        error.insertAfter(element);
                },
                success: function (label) {
                    //label.html("&nbsp;").addClass("right");
                }
            });

            $('#btnNext').click(function () {
                $('#__VIEWSTATE').remove();
                $('#noteBtn').css({ display: 'none' });

                if ($('#selectDays').val() == 0) {
                    $(this).attr("data-content", "Please select pick up date.");
                    $(this).popover('show');
                    return;
                }

                if ($('#businessPhone').val() == "") {
                    $(this).attr("data-content", "Please input Business Phone.");
                    $(this).popover('show');
                    $('#businessPhone').parent().addClass("has-error");
                    return;
                }
                $(this).attr("data-content", "Loading...");
                $(this).popover('show');

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

            });
        });
    </script>
</asp:Content>

