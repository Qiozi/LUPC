<%@ Page Language="C#" AutoEventWireup="true" CodeFile="demo_html_to_pdf.aspx.cs" Inherits="temp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    </div>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="html to pdf" />
    </form>
</body>
</html>
