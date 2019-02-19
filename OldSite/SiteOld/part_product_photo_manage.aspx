<%@ Page Language="C#" AutoEventWireup="true" CodeFile="part_product_photo_manage.aspx.cs" Inherits="part_product_photo_manage" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <hr size="1" />
        <h2>
            <anthem:Label ID="lbl_current_part" runat="server" Font-Bold="True"></anthem:Label>
        </h2> 
        <hr size="1" />
        <anthem:TextBox ID="txt_sku" runat="server"></anthem:TextBox>
        <hr size="1" />
        <anthem:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" /></div>
    </form>
</body>
</html>
