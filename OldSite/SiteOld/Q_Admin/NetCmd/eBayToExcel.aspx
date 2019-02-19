<%@ Page Language="C#" AutoEventWireup="true" CodeFile="eBayToExcel.aspx.cs" Inherits="Q_Admin_NetCmd_eBayToExcel" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:100px; height:200px; text-align:center;">
     <asp:Button ID="Button1" runat="server" onclick="Button1_Click" 
        Text="Delete excel file on Service." />
    <asp:Button ID="Button_generate" runat="server" onclick="Button_generate_Click" 
        Text="Create and download" />
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
   
    </form>
</body>
</html>
