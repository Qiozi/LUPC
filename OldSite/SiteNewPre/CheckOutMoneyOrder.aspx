<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true"
    CodeFile="CheckOutMoneyOrder.aspx.cs" Inherits="CheckOutMoneyOrder" %>

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
                            <span class="glyphicon glyphicon-home"></span>
                            <a href="default.aspx" style="color: White; font-size: 20px;">Home</a> <span class="glyphicon glyphicon-menu-right"></span>
                        </div>
                    </li>
                    <li>
                        <div>
                            <span class="glyphicon glyphicon-shopping-cart"></span>
                            <a href="ShoppingCart.aspx" style="color: White; font-size: 20px;">Shopping Cart</a> <span class="glyphicon glyphicon-menu-right"></span>
                        </div>
                    </li>
                    <li>
                        <div style="font-size: 20px; color: White;">
                            PAYING BY MONEY ORDER
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
                    <p>
                        All items are priced in Canadian Dollars. Payment must be made in Canadian Dollars.
                    If you have to send us a US dollar Money order please contact us for a total amount
                    in USD according to daily exchange rate.
                    </p>
                    <p>
                        If you will send us money order from the US please make sure the money order is
                    negotiable in Canada. USPS Postal Money Orders must be made international.
                    </p>
                    <p>
                        Please make your money order payable to: <strong>LU COMPUTERS</strong> and mark
                    your <strong>ORDER NUMBER</strong> on the money order. Money order will be mailed
                    to:
                    </p>
                    <pre>
                LU Computers
                1875 Leslie Street
                Unit 24
                Toronto, Ont. M3B 2M5
                Canada
               </pre>
                </div>
                <hr size="1" />
                <p style="padding: 0em 2em 0 2em;">
                    NOTE: Most items on this form are required.
                </p>
                <fieldset style='width: 80%; margin: 0 auto;'>
                    <input type="hidden" name="cmd" value="saveMoneyOrder" />
                    <input type="hidden" name="shippingID" value="<%= ReqShippingID %>" />
                    <input type="hidden" name="stateID" value="<%= ReqStateID %>" />
                    <input type="hidden" name="purl" value="<%= Request.RawUrl %>" />

                    <h4>SHIPPING ADDRESS:
                    </h4>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            First Name:</label>
                        <input type="text" size="80" class="form-control" name="firstName" id="firstName" value="<%= FirstName %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Last Name:</label>
                        <input type="text" size="80" class="form-control" name="lastName" id="lastName" value="<%= LastName %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Address:</label>
                        <input type="text" size="80" class="form-control" size="80" name="address" id="address" value="<%= Address %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Country:</label>
                        <input type="text" size="80" disabled="true" class="form-control" name="shippingcountry" id="shippingcountry"
                            value="Canada" placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            City:</label>
                        <input type="text" size="80" class="form-control" name="shippingCity" id="shippingCity" value="<%= City %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            State (Province)</label>
                        <select name="stateProvince" id="stateProvince" class="form-control">
                            <optgroup label="Canada">
                                <asp:Literal runat="server" ID="stateOption"></asp:Literal>
                            </optgroup>
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Zip(Post) Code*:</label>
                        <input type="text" size="80" class="form-control" name="shippingZipCode" id="shippingZipCode"
                            value="<%= ZipCode %>" placeholder="">
                    </div>
                    <h4>CONTACT INFORMATION:</h4>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Email Address:</label>
                        <input type="email" class="form-control" id="email" name="email" value="<%= Email1 %>"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Business Phone:*</label>
                        <input type="text" size="80" class="form-control" id="businessPhone" value="<%= BusinessPhone %>"
                            name="businessPhone" placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            Home Phone:
                        </label>
                        <input type="text" size="80" class="form-control" id="homePhone" value="<%= HomePhone %>" name="homePhone"
                            placeholder="">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputName2">
                            CUSTOMER COMMENT / NOTE:</label>
                        <textarea class="form-control" cols="40" name="comment" id="commentNote" rows="3"><%= CustComment %></textarea>
                    </div>
                </fieldset>
                <div class="text-center" style="padding: 1em;">
                    <a class="btn btn-default btn-warning" href="ShoppingCart.aspx" style="width: 120px;">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        Black</a> <a class="btn btn-default btn-success" id="btnNext" onclick="$('#checkOutForm').submit();return false;" style="width: 120px;">Next
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
                    //  label.html("&nbsp;").addClass("right");
                }
            });


            $("input:reset").click(function () {
                validate.resetForm();
            });

        });
    </script>
</asp:Content>
