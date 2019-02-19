<%@ Page Language="C#" AutoEventWireup="true" CodeFile="system_alert.aspx.cs" Inherits="Q_Admin_system_alert" %>

<%@ Register src="UC/AlertMessage.ascx" tagname="AlertMessage" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    <uc1:AlertMessage ID="AlertMessage1" runat="server" />
    </form>
</body>
</html>
