<!DOCTYPE html>
<html>
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta charset="utf-8" />
    <script src="/Scripts/jquery-2.0.0.min.js"></script>
</head>
<body>
    <iframe id="iframe" name="iframe" src="default.aspx"></iframe>
    <script>
        $(function(){
       
            var content = $(window.frames['iframe'].document).find("#page-web-home").length;
            console.log(content);
            console.log("d");
   })
    </script>
</body>
</html>
