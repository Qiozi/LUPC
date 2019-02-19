<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReadPdf.aspx.cs" Inherits="Q_Admin_ReadPdf" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js_css/jquery-ui-1.10.2.custom.min.js"></script>
    <link rel="stylesheet" href="../js_css/ui-lightness/jquery-ui-1.10.2.custom.min.css" />
    <script type="text/javascript">
        $(function () {
            $("#beginDate").datepicker({
                inline: true
            });
            $("#endDate").datepicker({
                inline: true
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
            RepeatDirection="Horizontal">
            <asp:ListItem>ASUS</asp:ListItem>
            <asp:ListItem>MSI</asp:ListItem>
            <asp:ListItem>Gigabyte</asp:ListItem>
            <asp:ListItem>Cooler Master</asp:ListItem>
            <asp:ListItem>TT</asp:ListItem>
            <asp:ListItem>Anter</asp:ListItem>
        </asp:RadioButtonList> 
        <br />
        
            <asp:FileUpload ID="FileUpload1" runat="server" />
           <asp:Button ID="btnUpload" runat="server" Text="Upload" 
               onclick="btnUpload_Click" /> <br />
               <asp:TextBox ID="beginDate" runat="server"></asp:TextBox>
               <asp:TextBox ID="endDate" runat="server"></asp:TextBox>
           <asp:TextBox ID="TextBox1" runat="server" Width="197px"></asp:TextBox>
            <asp:Button ID="ButtonRead" runat="server" Text="Read" 
                onclick="ButtonRead_Click" />
            <asp:Button ID="ButtonSave" runat="server" Text="Save" 
               onclick="ButtonSave_Click"  />
           <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </center>
            <hr size="1" />
            <asp:GridView ID="GridView1"
                runat="server">
            </asp:GridView>
        </div>
    </form>
</body>
</html>
