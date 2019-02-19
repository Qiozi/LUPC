<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="CheckOutEmailTransfer.aspx.cs" Inherits="CheckOutEmailTransfer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="checkOutForm" name="checkOutForm" class="checkOutForm  form-horizontal" action="javascript:formSubmit();" method="post">
        <div class="container" style="background: #74C774; padding: 0px;">
            <div style="padding-bottom: 0px; background: #74C774; border: 0px solid red; padding: 15px 15px 0px 15px;">
                <ul class="list-inline" style="margin-left: 2em;">
                    <li>
                        <div>
                            <a href="/default.aspx" style="color: White; font-size: 20px;"><span class="glyphicon glyphicon-home"></span>Home</a>
                            <span class="glyphicon glyphicon-menu-right"></span>
                        </div>
                    </li>
                    <li>
                        <div>
                            <a href="ShoppingCart.aspx" style="color: White; font-size: 20px;"><span class="glyphicon glyphicon-shopping-cart"></span>Shopping Cart</a>
                            <span class="glyphicon glyphicon-menu-right"></span>
                        </div>
                    </li>
                    <li>
                        <div style="font-size: 20px; color: White;">PAYING BY EMAIL TRANSFER</div>
                    </li>
                </ul>
            </div>
            <div style="background-color: White; margin: 0 3em 3em 3em; padding: 10px;">
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
                    Email transfer(Interac Online) is available in Canada only. Email transfer is as same as paying with your debit card. It is safe and quick. Here are two steps towards an instant email transfer: 
            
                <ol style="padding: 2em 2em 2em 4em;">
                    <li>Log into your online banking site and select the Email Money Transfer option;
                    </li>
                    <li>Send an Email Money Transfer to our email address: <strong><a href="mailto:sales@lucomputers.com">sales@lucomputers.com</a></strong>
                    </li>
                    <li>Contact us by phone or email to inform us your answer to security question.
                    </li>
                </ol>
                    <p>
                        <a href="http://www.interac.ca/index.php/en/interac-online/interac-online-for-consumers" target="_blank">Learn more about Email Transfer (Interac Online)</a>
                    </p>
                </div>
                <hr size="1" />
                <fieldset style='width: 80%; margin: 0 auto;'>
                    <input type="hidden" name="cmd" value="saveEmailTransfer" />
                    <input type="hidden" name="shippingID" value="<%= ReqShippingID %>" />
                    <input type="hidden" name="stateID" value="<%= ReqStateID %>" />
                    <input type="hidden" name="purl" value="<%= Request.RawUrl %>" />
                    <h4 class="title1">SHIPPING ADDRESS: </h4>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">First Name:</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" name="firstName" maxlength="30" id="firstName" value="<%= FirstName %>" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Last Name:</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" name="lastName" maxlength="30" id="lastName" value="<%= LastName %>" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Address:</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" name="address" id="address" value="<%= Address %>" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Country:</label>
                        <div class="col-sm-6">
                            <input type="text" disabled="true" class="form-control" name="shippingcountry" id="shippingcountry" value="Canada" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">City:</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" name="shippingCity" id="shippingCity" value="<%= City %>" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">State (Province)</label>
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
                        <label class="col-sm-3 control-label">Zip(Post) Code:</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" name="shippingZipCode" id="shippingZipCode" value="<%= ZipCode %>" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <h4 class="title1">CONTACT INFORMATION:</h4>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Email Address:(option)</label>
                        <div class="col-sm-6">
                            <input type="email" class="form-control" id="email" name="email" value="<%= Email1 %>" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Business Phone:</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" id="businessPhone" value="<%= BusinessPhone %>" name="businessPhone" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">Home Phone: (option)</label>
                        <div class="col-sm-6">
                            <input type="text" class="form-control" id="homePhone" value="<%= HomePhone %>" name="homePhone" placeholder=""></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                    <div class="form-group">
                        <label class="col-sm-3 control-label">CUSTOMER COMMENT / NOTE:(option)</label>
                        <div class="col-sm-6">
                            <textarea class="form-control" cols="40" name="comment" id="commentNote" rows="3"><%= CustComment %></textarea></div>
                        <div class="col-sm-3 control-label" style="text-align: left;"></div>
                    </div>
                </fieldset>
                <div class="text-center" style="padding: 1em;">
                    <a class="btn btn-default btn-warning" href="ShoppingCart.aspx" style="width: 120px;">
                        <spsn class="glyphicon glyphicon-chevron-left"></spsn>
                        Black</a>
                    <a class="btn btn-default btn-success" id="btnNext" style="width: 120px;" onclick="$('#checkOutForm').submit();return false;">Next<spsn class="glyphicon glyphicon-chevron-right"></spsn></a>
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
                },
                success: function (label) {
                    // label.html("&nbsp;").addClass("right");
                }
            });

            $("input:reset").click(function () {
                validate.resetForm();
            });
        });
    </script>
</asp:Content>

