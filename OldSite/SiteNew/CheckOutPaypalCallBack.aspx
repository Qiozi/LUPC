<%@ Page Language="C#" Title="Paypal Shipping Information" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="CheckOutPaypalCallBack.aspx.cs" Inherits="CheckOutPaypalCallBack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <form id="checkOutForm" name="checkOutForm" class="checkOutForm" action="javascript:formSubmit();" method="post">
        <div class="container" style="background: #74C774; padding: 0px;">
            <div style="background-color: White; margin: 0 3em 3em 3em; padding: 10px;">
                <div class="row">
                    <div class="col-md-8 note">
                        <h1>Shipping Information </h1>
                    </div>
                </div>
                <fieldset style='width: 80%; margin: 0 auto;'>
                    <h4 class="title1">Billing &amp; Delivery Address </h4>
                    <div style="padding: 3rem;">
                        <table class="table">
                            <tr>
                                <td class="text-right">Name:</td>
                                <td><%=Session["Address_Name"]%></td>
                            </tr>
                            <tr>
                                <td class="text-right">Address:</td>
                                <td><%=Session["Address_Street"]%></td>
                            </tr>

                            <tr>
                                <td class="text-right">City:</td>
                                <td><%=Session["Address_CityName"]%></td>
                            </tr>
                            <tr>
                                <td class="text-right">State (Province)</td>
                                <td><%=Session["Address_StateOrProvince"]%></td>
                            </tr>
                            <tr>
                                <td class="text-right">Country:</td>
                                <td><%=Session["Address_CountryName"]%> </td>
                            </tr>
                            <tr>
                                <td class="text-right">Zip(Post) Code:</td>
                                <td><%=Session["Address_PostalCode"]%></td>
                            </tr>
                            <tr>
                                <td class="text-right">Total:</td>
                                <td>$<%=Session["Order_Total"]%> </td>
                            </tr>
                        </table>
                    </div>
                </fieldset>
                <div class="text-center" style="padding: 1em;">
                    <a id="btnNext" style="width: 120px;cursor:pointer;" onclick="window.location.href='/DoExpressCheckoutPayment.aspx';">
                        <img src='https://www.paypalobjects.com/webstatic/en_US/btn/btn_pay_pp_142x27.png' />
                    </a>
                </div>
                <asp:Literal runat="server" ID="ltOrderProdList"></asp:Literal>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">

</asp:Content>

