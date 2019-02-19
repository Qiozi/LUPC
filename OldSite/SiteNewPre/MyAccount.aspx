<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="MyAccount.aspx.cs" Inherits="MyAccount" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        strong {
            color: #666;
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
                        <a href="default.aspx" style="color: White;"> Home</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="color: White; font-size: 20px;">
                        <span class="glyphicon glyphicon-user"></span> My Account                                
                            <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="color: White; font-size: 20px;">My Profile</div>
                </li>
            </ul>

        </div>
        <div style="background: white; margin: -1em 3em 3em 3em; padding: 1em;" role="navigation">
            <!-- menu -->
            <ul class="nav nav-tabs">
                <li role="presentation" class="active"><a href="MyAccount.aspx">My Profile</a></li>
                <li role="presentation"><a href="MyPendingOrders.aspx">Pending Orders</a></li>
                <li role="presentation"><a href="MyAllOrders.aspx">All Order</a></li>
            </ul>
            <!-- menu -->


            <div id="infoViewArea" style="display: none;">
                <h3>ACCOUNT</h3>
                <table class="table table-hover">
                    <tr>
                        <td width="20%"><strong>Login Name</strong></td>
                        <td class="note" width="20%"><%= LoginName %></td>
                        <td width="20%"><strong>eBay ID</strong></td>
                        <td class="note"><%= eBaYID%></td>
                    </tr>
                    <tr>
                        <td><strong>First Name</strong></td>
                        <td class="note"><%= FirstName%></td>
                        <td><strong>Last Name</strong></td>
                        <td class="note"><%= LastName%></td>
                    </tr>
                    <tr>
                        <td><strong>Home Phone#</strong></td>
                        <td class="note"><%= HomePhone%></td>
                        <td><strong>Bussiness Phone#</strong></td>
                        <td class="note"><%= BussinessPhone%></td>

                    </tr>
                    <tr>
                        <td><strong>Mobile Phone</strong></td>
                        <td class="note" colspan="3"><%= MobilePhone%></td>
                    </tr>
                    <tr>
                        <td><strong>Country</strong></td>
                        <td class="note" colspan="3"><%= Country%></td>
                    </tr>
                    <tr>
                        <td><strong>Address</strong></td>
                        <td class="note" colspan="3"><%= Address%></td>
                    </tr>
                    <tr>
                        <td><strong>City</strong></td>
                        <td class="note"><%= City%></td>
                        <td><strong>State(Province)</strong></td>
                        <td class="note"><%= State%></td>
                    </tr>
                    <tr>
                        <td><strong>Zip(Post) Code</strong></td>
                        <td class="note"><%= Zip%></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Email1</strong></td>
                        <td class="note"><%= Email1%></td>
                        <td><strong>Email2</strong></td>
                        <td class="note"><%= Email2%></td>
                    </tr>
                </table>
                <h3>SHIPPING ADDRESS</h3>
                <table class="table table-hover">
                    <tr>
                        <td width="20%"><strong>First Name</strong></td>
                        <td class="note" width="20%"><%= ShippingFirstName%></td>
                        <td width="20%"><strong>Last Name</strong></td>
                        <td class="note"><%= ShippingLastName%></td>
                    </tr>
                    <tr>
                        <td><strong>Country</strong></td>
                        <td class="note" colspan="3"><%= ShippingCountry%></td>
                    </tr>
                    <tr>
                        <td><strong>Address</strong></td>
                        <td class="note" colspan="3"><%= ShippingAddress%></td>
                    </tr>
                    <tr>
                        <td><strong>City</strong></td>
                        <td class="note"><%= ShippingCity%></td>
                        <td><strong>State(Province)</strong></td>
                        <td class="note"><%= ShippingState%></td>
                    </tr>
                    <tr>
                        <td><strong>Zip(Post) Code</strong></td>
                        <td class="note"><%= ShippingZip%></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <h3>BUSINESS</h3>
                <table class="table table-hover">
                    <tr>
                        <td width="20%"><strong>Company Name</strong></td>
                        <td class="note" width="20%"><%= BusinessCompanyName%></td>
                        <td width="20%"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Telephone</strong></td>
                        <td class="note"><%= BusinessTelephone%></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Country</strong></td>
                        <td class="note" colspan="3"><%= BusinessCountry%></td>

                    </tr>
                    <tr>
                        <td><strong>Address</strong></td>
                        <td class="note" colspan="3"><%= BusinessAddress%></td>
                    </tr>
                    <tr>
                        <td><strong>City</strong></td>
                        <td class="note"><%= BusinessCity%></td>
                        <td><strong>State(Province)</strong></td>
                        <td class="note"><%= BusinessState%></td>
                    </tr>
                    <tr>
                        <td><strong>Zip(Post) Code</strong></td>
                        <td class="note"><%= BusinessZip%></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Tax Execption Number</strong></td>
                        <td class="note"><%= BusinessTaxExectionNumber%></td>
                        <td><strong>Website Address</strong></td>
                        <td class="note"><%= BusinessWebsiteAddress%></td>
                    </tr>
                </table>

                <p class="text-center ">
                    <a class="btn btn-default btn-warning" href="?cmd=edit"><span class="glyphicon glyphicon-wrench"></span> Revise</a>
                </p>
            </div>
            <div id="infoEditArea" style="display: none;">
                <input type="hidden" name="cmd" value="saveMyAccount" />
                <h3>ACCOUNT</h3>
                <table class="table table-hover">
                    <tr>
                        <td width="20%"><strong>Login Name</strong></td>
                        <td class="note" width="20%"><%= LoginName %></td>
                        <td width="20%"><strong>eBay ID</strong></td>
                        <td class="note"><%= eBaYID%></td>
                    </tr>
                    <tr>
                        <td><strong>First Name</strong></td>
                        <td class="note">
                            <input type="text" name="FirstName" maxlength="100" value="<%= FirstName%>" /></td>
                        <td><strong>Last Name</strong></td>
                        <td class="note">
                            <input type="text" maxlength="100" name="lastName" value="<%= LastName%>" /></td>
                    </tr>
                    <tr>
                        <td><strong>Home Phone#</strong></td>
                        <td class="note">
                            <input type="text" maxlength="30" name="homePhone" value="<%= HomePhone%>" /></td>
                        <td><strong>Bussiness Phone#</strong></td>
                        <td class="note">
                            <input type="text" maxlength="30" name="bussinessPhone" value="<%= BussinessPhone%>" /></td>

                    </tr>
                    <tr>
                        <td><strong>Mobile Phone</strong></td>
                        <td class="note" colspan="3">
                            <input type="text" maxlength="30" name="mobilePhone" value="<%= MobilePhone%>" /></td>
                    </tr>
                    <tr>
                        <td><strong>Country/State(Province)</strong></td>
                        <td colspan="3">
                            <span class="note">
                                <select class='countrySelect' name="countryState" id="countryState">
                                    <%= CountryState %>
                                </select>
                            </span>
                            <span style="display: none;">
                                <strong>Other Country Name</strong><span class="note"><input class="note" maxlength="50" type="text" name="CountryText" value="<%= CountryText%>" /></span>
                                <strong>State(Province)</strong><span class="note"><input type="text" maxlength="50" name="StateText" value="<%= StateText %>" /></span></span>

                        </td>
                    </tr>
                    <tr>
                        <td><strong>Address</strong></td>
                        <td class="note" colspan="3">
                            <input type="text" maxlength="150" size="80" name="Address" value="<%= Address%>" /></td>
                    </tr>
                    <tr>
                        <td><strong>City</strong></td>
                        <td class="note">
                            <input type="text" maxlength="30" name="city" value="<%= City%>" /></td>
                        <td><strong></strong></td>
                        <td class="note"></td>
                    </tr>
                    <tr>
                        <td><strong>Zip(Post) Code</strong></td>
                        <td class="note">
                            <input type="text" maxlength="10" name="zip" value="<%= Zip%>" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Email1</strong></td>
                        <td class="note">
                            <input type="text" name="email1" maxlength="100" value="<%= Email1%>" /></td>
                        <td><strong>Email2</strong></td>
                        <td class="note">
                            <input type="text" maxlength="100" name="email2" value="<%= Email2%>" /></td>
                    </tr>
                </table>
                <h3>SHIPPING ADDRESS</h3>
                <table class="table table-hover">
                    <tr>
                        <td width="20%"><strong>First Name</strong></td>
                        <td class="note" width="20%">
                            <input type="text" maxlength="100" name="shippingFirstName" value="<%= ShippingFirstName%>" /></td>
                        <td width="20%"><strong>Last Name</strong></td>
                        <td class="note">
                            <input type="text" maxlength="100" name="shippingLastName" value="<%= ShippingLastName%>" /></td>
                    </tr>
                    <tr>
                        <td><strong>Country/State(Province)</strong></td>
                        <td class="note" colspan="3">
                            <span class="note">
                                <select class='countrySelect' name="ShippingCountryState" id="ShippingCountryState">
                                    <%= ShippingCountryState %>
                                </select>
                            </span>
                            <span style="display: none;">
                                <strong>Other Country Name</strong><span class="note"><input class="note" maxlength="50" type="text" name="ShippingCountryText" value="<%= ShippingCountryText%>" /></span>
                                <strong>State(Province)</strong><span class="note"><input type="text" maxlength="50" name="ShippingStateText" value="<%= ShippingStateText %>" /></span></span>

                        </td>
                    </tr>
                    <tr>
                        <td><strong>Address</strong></td>
                        <td class="note" colspan="3">
                            <input type="text" size="80" maxlength="150" name="shippingAddress" value="<%= ShippingAddress%>" /></td>
                    </tr>
                    <tr>
                        <td><strong>City</strong></td>
                        <td class="note">
                            <input type="text" maxlength="50" name="shippingCity" value="<%= ShippingCity%>" /></td>
                        <td><strong></strong></td>
                        <td class="note"></td>
                    </tr>
                    <tr>
                        <td><strong>Zip(Post) Code</strong></td>
                        <td class="note">
                            <input type="text" maxlength="10" name="shippingZip" value="<%= ShippingZip%>" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                </table>
                <h3>BUSINESS</h3>
                <table class="table table-hover">
                    <tr>
                        <td width="20%"><strong>Company Name</strong></td>
                        <td class="note" width="20%">
                            <input type="text" maxlength="50" name="CompanyName" value="<%= BusinessCompanyName%>" /></td>
                        <td width="20%"></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Telephone</strong></td>
                        <td class="note">
                            <input type="text" maxlength="30" name="businessTelephone" value="<%= BusinessTelephone%>" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Country/State(Province)</strong></td>
                        <td colspan="3">
                            <span class="note">
                                <select class='countrySelect' name="BusinessCountryState" id="Select1">
                                    <%= CountryState %>
                                </select>
                            </span>
                            <span style="display: none;">
                                <strong>Other Country Name</strong><span class="note"><input class="note" type="text" name="BusinessCountryText" value="<%= CountryText%>" /></span>
                                <strong>State(Province)</strong><span class="note"><input type="text" name="BusinessStateText" value="<%= StateText %>" /></span></span>

                        </td>

                    </tr>
                    <tr>
                        <td><strong>Address</strong></td>
                        <td class="note" colspan="3">
                            <input type="text" maxlength="100" size="80" name="businessAddress" value="<%= BusinessAddress%>" /></td>
                    </tr>
                    <tr>
                        <td><strong>City</strong></td>
                        <td class="note">
                            <input type="text" maxlength="30" name="businessCity" value="<%= BusinessCity%>" /></td>
                        <td><strong></strong></td>
                        <td class="note"></td>
                    </tr>
                    <tr>
                        <td><strong>Zip(Post) Code</strong></td>
                        <td class="note">
                            <input type="text" maxlength="10" name="businessZip" value="<%= BusinessZip%>" /></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td><strong>Tax Execption Number</strong></td>
                        <td class="note">
                            <input type="text" maxlength="10" name="businessTaxExectionNumber" value="<%= BusinessTaxExectionNumber%>" /></td>
                        <td><strong>Website Address</strong></td>
                        <td class="note">
                            <input type="text" name="website" maxlength="120" value="<%= BusinessWebsiteAddress%>" /></td>
                    </tr>
                </table>
                <p class="text-center"><a class="btn btn-default btn-warning" id="submitButton" href="javascript:void(0);"><span class="glyphicon glyphicon-save"></span> Submit</a></p>

            </div>

        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            if ('<%= ReqCmd %>' != 'edit') {
                $('#infoViewArea').css({ 'display': '' });
            }
            else
                $('#infoEditArea').css({ 'display': '' });

            function showCountryInput(the) {
                if (the.val() == "0")
                    the.parent().next().css({ display: '' });
                else
                    the.parent().next().css({ display: 'none' });
            }

            $('.countrySelect').each(function () {
                var the = $(this);
                showCountryInput(the);
                the.change(function () {
                    showCountryInput($(this));
                });

            });

            $('#submitButton').click(function () {
                $('#__VIEWSTATE').remove();


                $.ajax({
                    type: "post",
                    url: "cmds/UserSaveInfo.aspx",
                    data: $('#form100').serialize(),
                    error: function (g, s, t) {
                        alert(s);
                    },
                    success: function (msg, s) {
                        if (msg == "OK")
                            window.location.href = "myAccount.aspx?cmd=view";
                        else
                            alert(msg);
                    }
                });

            });
        });


    </script>
</asp:Content>



