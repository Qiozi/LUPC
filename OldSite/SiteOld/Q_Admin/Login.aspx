<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Q_Admin_Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LU Computers</title>
    <script type="text/javascript" src="js/winopen.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="400" align="center" style="margin-top: 5em; border: 1px solid #cccccc;">
                <tr>
                    <td colspan="2" style="padding-left: 5px; font-weight: bold; background: #f2f2f2; height: 25px">LU User Login</td>
                </tr>
                <tr>
                    <td>username: </td>
                    <td>
                        <asp:TextBox ID="txt_username" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>password:</td>
                    <td>
                        <asp:TextBox ID="txt_password" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="btn_login" runat="server" Text="Login" OnClick="btn_login_Click"></asp:Button>
                        <asp:Button ID="btn_reset" runat="server" Text="Reset"
                            UseSubmitBehavior="False"></asp:Button>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="padding-left: 5px">
                        <asp:Label ID="lbl_error" runat="server" ForeColor="Red"></asp:Label><asp:Literal
                            ID="Literal1" runat="server"></asp:Literal></td>
                </tr>
            </table>
        </div>
        <iframe src="../ChangeOnSalePriceToProduct.aspx?QioziCommand=update" frameborder="0" style="width: 0px; height: 0px"></iframe>
    </form>

</body>
</html>
