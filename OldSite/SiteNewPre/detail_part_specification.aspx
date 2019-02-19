<%@ Page Language="C#" AutoEventWireup="true" CodeFile="detail_part_specification.aspx.cs" Inherits="detail_part_specification" %>
<!DOCTYPE html>
<html lang="en">
<head runat="server">
    <title>LU Computer</title>
    <script src="/js/jquery-1.9.1.js" type="text/javascript"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal runat="server" id="ltSpecinfication"></asp:Literal>
    </form>

    <script type="text/javascript">
        $(window.parent.document).find("#iframePartSpecification").load(function () {
            var main = $(window.parent.document).find("#iframePartSpecification");
            var thisheight = $(document).height() + 100;

            main.height(thisheight);
            main.css({ 'width': '100%', 'border': '0' });
        });
    </script>
</body>
</html>
