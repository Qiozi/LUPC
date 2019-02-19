<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sale_msg_from_seller.aspx.cs" Inherits="Q_Admin_sale_msg_from_seller" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center">
        <anthem:textbox id="TextBox1" runat="server" columns="50" rows="15" textmode="MultiLine"></anthem:textbox>
    <table align="center">
        <tr>
            <td >
                <asp:Panel runat="Server" ID="panel1" SkinID="btn">
                    <anthem:LinkButton CssClass="btn" runat="server" id="lb_save" text="Save" Width="80px" OnClick="lb_save_Click" />
                </asp:Panel>
            </td>
            <td >
                <asp:Panel runat="Server" ID="panel2" SkinID="btn"  Width="180px" >
                    <a class="btn"  id="lb_clore" Width="180px" OnClick="window.close();return false;" >Clore Window</a>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
