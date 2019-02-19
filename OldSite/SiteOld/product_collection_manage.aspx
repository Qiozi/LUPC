<%@ Page Language="C#" AutoEventWireup="true" CodeFile="product_collection_manage.aspx.cs" Inherits="product_collection_manage" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
    <script src="Q_Admin/JS/Loading.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
        <h1>&nbsp;<anthem:Label runat="server" ID="lbl_note"></anthem:Label></h1>
        <table >
            <tr>
                <td style="vertical-align: top; height: 70px">product group</td>
                <td style="vertical-align: top; height: 70px">
                    <anthem:TextBox ID="txt_product_group" runat="server" Columns="80"></anthem:TextBox><br />
                    example: 2906|2880|2879</td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 70px">
                    Product Topic</td>
                <td style="vertical-align: top; height: 70px">
                    <anthem:TextBox ID="txt_product_topic" runat="server" Columns="80"></anthem:TextBox><br />
                    example: Intel powered work stations</td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 70px">
                    Navigation</td>
                <td style="vertical-align: top; height: 70px">
                    <anthem:TextBox ID="txt_navigation" runat="server" Columns="80"></anthem:TextBox><br />
                    example: Business</td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 70px">
                    Navigation Return</td>
                <td style="vertical-align: top; height: 70px">
                    <anthem:TextBox ID="txt_navigation_return_path" runat="server" Columns="80"></anthem:TextBox><br />
                    example: Product_list_business.asp</td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 70px">
                    Product Category</td>
                <td style="vertical-align: top; height: 70px">
                    <anthem:DropDownList ID="ddl_product_category" runat="server">
                        <asp:ListItem Value="1">Part &amp; Noebook</asp:ListItem>
                        <asp:ListItem Value="3">System</asp:ListItem>
                    </anthem:DropDownList></td>
            </tr>
            <tr>
                <td style="vertical-align: top; height: 70px">
                </td>
                <td style="vertical-align: top; height: 70px">
                    <anthem:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" PostCallBackFunction="Anthem_PostCallBack" PreCallBackFunction="Anthem_PreCallBack" /></td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
