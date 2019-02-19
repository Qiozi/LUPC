<%@ Page Language="c#" AutoEventWireup="false" CodeFile="APIError.aspx.cs" Inherits="APIError" %>

<html>
<head>
    <title>PayPal - Error Page</title>
    <link href="/Content/bootstrap.css" rel="stylesheet" />
</head>
<body>
    <div class="container-fluid">
        <div class="well">
            <h2 class="text-center">
                <img src="/images/logo1.png" /><br />
                Checkout with PayPal </h2>
        </div>
        <div class="row-fluid">
            <form id="ErrorForm" runat="server">
                <h4 class="text-center">Sorry! An unexpected error occured.</h4>
                <table class="text-center" style="border-collapse: collapse" align="center" cellspacing="5" width="85%">
                    <tr>
                        <td>
                            <div class="row">
                                <b><u>API Error Message:</u></b>
                                <asp:Repeater runat="server" ID="rptList">
                                    <ItemTemplate>
                                        <div class="col-md-12"><%# DataBinder.Eval(Container.DataItem, "errKey") %></div>
                                        <div class="col-md-12"><%# DataBinder.Eval(Container.DataItem, "errItem") %></div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <br />
                            <a href="/Default.aspx">Back</a>
                        </td>
                    </tr>
                </table>
            </form>
        </div>
    </div>
</body>
</html>
