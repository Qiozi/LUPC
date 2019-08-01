﻿<%@ Page Title="Check out Bank Transfer - LU Computers" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true"
    CodeFile="CheckOutBankTransfer.aspx.cs" Inherits="CheckOutBankTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="checkOutForm" class="checkOutForm  form-horizontal" name="checkOutForm" role="form" action="javascript:formSubmit();"
        method="post">
        <div class="container" style="background: #74C774; padding: 0px;">
            <div style="padding-bottom: 0px; background: #74C774; border: 0px solid red; padding: 15px 15px 0px 15px;">
                <ul class="list-inline" style="margin-left: 2em;">
                    <li>
                        <div>
                            <span class="glyphicon glyphicon-home"></span>
                            <a href="/default.aspx" style="color: White; font-size: 20px;">Home</a> <span class="glyphicon glyphicon-menu-right"></span>
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
                            PAYING BY BANK TRANSFER
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
                        Please submit your order and then wire transfer payment. We will email you the account
                    information that you can send to.
                    </p>
                    <p>
                        To avoid currency discrepancy, payment must be made in Canadian Dollars. Please
                    notify us after you have made your payment; please send us a copy of your payment
                    receipt.
                    </p>
                    <p>
                        Bank wire is only necessary if you do not have a TD Canada Trust account. If you
                    have a TD account you can simply make a direct transfer by calling the bank, or
                    transfer using online banking.
                    </p>
                    <p>
                        Wire transfer is not an instant payment. Your funds usually takes 1-3 days to reach
                    our account. Wire transferring from some foreign countries can take longer.
                    </p>
                    <p>
                        NOTE: All items on below form are required.
                    </p>
                </div>
                <hr size="1" />
                <fieldset style='width: 80%; margin: 0 auto;'>
                    <input type="hidden" name="cmd" value="saveBankTransfer" />
                    <input type="hidden" name="shippingID" value="<%= ReqShippingID %>" />
                    <input type="hidden" name="stateID" value="<%= ReqStateID %>" />
                    <input type="hidden" name="purl" value="<%= Request.RawUrl %>" />
                    <h4 class="title1">SHIPPING ADDRESS:                    </h4>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            First Name:</label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" name="firstName" id="firstName" value="<%= FirstName %>"
                                placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Last Name:</label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" name="lastName" id="lastName" value="<%= LastName %>"
                                placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Address:</label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" name="address" id="address" value="<%= Address %>"
                                placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Country:</label>
                        <div class="col-sm-6">
                            <input type="text"  disabled="true" class="form-control" name="shippingcountry" id="shippingcountry"
                                value="Canada" placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            City:</label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" name="shippingCity" id="shippingCity" value="<%= City %>"
                                placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            State (Province)</label>
                        <div class="col-sm-6">
                            <select name="stateProvince" id="stateProvince" class="form-control">
                                <optgroup label="Canada">
                                    <asp:Literal runat="server" ID="stateOption"></asp:Literal>
                                </optgroup>
                            </select>
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Zip(Post) Code:</label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" name="shippingZipCode" id="shippingZipCode"
                                value="<%= ZipCode %>" placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <h4 class="title1">CONTACT INFORMATION:</h4>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Email Address:(option)</label>
                        <div class="col-sm-6">
                            <input type="email" class="form-control" id="email" name="email" value="<%= Email1 %>"
                                placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Business Phone:</label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" id="businessPhone" value="<%= BusinessPhone %>"
                                name="businessPhone" placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            Home Phone:(option)
                        </label>
                        <div class="col-sm-6">
                            <input type="text"  class="form-control" id="homePhone" value="<%= HomePhone %>" name="homePhone"
                                placeholder="">
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">
                            CUSTOMER COMMENT / NOTE:(option)</label>
                        <div class="col-sm-6">
                            <textarea class="form-control" cols="40" name="comment" id="commentNote" rows="3"><%= CustComment %></textarea>
                        </div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                </fieldset>
                <div class="text-center" style="padding: 1em;">
                    <a class="btn btn-default btn-warning" href="ShoppingCart.aspx" style="width: 120px;">
                        <span class="glyphicon glyphicon-chevron-left"></span>
                        Black</a>
                    <a class="btn btn-default btn-success" id="btnNext" onclick="$('#checkOutForm').submit();return false;" style="width: 120px;">Next
                         <span class="glyphicon glyphicon-chevron-right"></span>
                    </a>
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
                url: "/cmds/UserSaveInfo.aspx",
                data: $('#checkOutForm').serialize(),
                error: function (g, s, t) {
                    util.alertError(s);
                },
                success: function (msg, s) {
                    if (msg == "OK")
                        window.location.href = "ShoppingCartGoView.aspx";
                    else
                        util.alertError(msg);
                }
            });
        }

        $(function () {
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
                    error.css({ color: 'red', 'font-weight': 'normal' });
                    element.parent().next().html(error);

                }
            });


            $("input:reset").click(function () {
                validate.resetForm();
            });

        });
    </script>
</asp:Content>