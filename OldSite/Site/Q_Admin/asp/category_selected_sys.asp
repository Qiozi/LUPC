<!DOCTYPE html>
<html>
<head>
    <title>Category Select</title>
<style>
.validTitle{color: #8B8BD1;}
body { font-size: 8.5pt;}
ul, li { margin-top: 2px; margin-bottom: 2px; padding:0px; padding-left:5px; list-style:none;}
a { display: block; padding: 2px;}
a:hover { display:block;padding: 2px; background: blue; cursor:pointer; color: white;}
</style>
</head>

<body>
    <ul><li><label>Desktop Computers</label><ul><li class='validTitle'><a onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '377';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Desktop All-in-One';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Desktop All-in-One</a>
</li></ul></li></ul>
</body>
</html>
