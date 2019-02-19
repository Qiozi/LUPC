<html>
<head>
    <title>Category Select</title>
</head>
<style>
.validTitle{color: #8B8BD1;}
body { font-size: 8.5pt;}
ul, li { margin-top: 0px; margin-bottom: 0px;}
a { display: block; padding: 2px;}
a:hover { display:block;padding: 2px; background: blue; color: white;}
</style>
<body>
    <ul><li>Desktop Computers<ul><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '377';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Desktop Computers';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Desktop Computers</a>
</li></ul></li></ul>
</body>
</html>
