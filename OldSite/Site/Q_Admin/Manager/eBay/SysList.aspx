<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysList.aspx.cs" Inherits="Q_Admin_Manager_eBay_SysList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table>
                <asp:Repeater runat="server" ID="rptList">
                    <ItemTemplate>
                        <tr>
                            <td><%# DataBinder.Eval(Container.DataItem,"SysSku") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem,"CustomLabel") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem,"eBayItemId") %></td>
                            <td><a>Modify</a></td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <td><%# DataBinder.Eval(Container.DataItem,"eBayTitle") %></td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">
                                <td><%# DataBinder.Eval(Container.DataItem,"PartShortNameLists") %></td>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
        </div>
    </form>
</body>
</html>
