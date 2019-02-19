<%@  language="VBSCRIPT" codepage="65001" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <script src="js_css/jquery-1.9.1.js"></script>
    <title>Untitled Document</title>
    <script type="text/javascript">
        $(document).ready(function () {
            alert("D");
    $.getJSON('http://localhost:83/ProdList/Get','',function(){

    });
    });

    </script>
</head>

<body>

    <%
        
      response.End() %>
    <% response.Redirect("/site/help_desk.asp") %>
</body>
</html>
