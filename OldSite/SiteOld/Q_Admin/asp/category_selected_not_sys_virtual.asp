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
    <ul><li>Mobile Computers<ul><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '338';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Netbooks';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Netbooks</a>
</li></ul></li><li>Server Systems<ul><li >Server Accessories<ul><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '368';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Hard Drives';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Hard Drives</a>
</li><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '369';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Memory Sticks';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Memory Sticks</a>
</li><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '371';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Server Cases';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Server Cases</a>
</li><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '375';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Server Boards';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Server Boards</a>
</li><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '376';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Server CPUs';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Server CPUs</a>
</li></ul><li class='validTitle'><a href='' onclick="parent.document.getElementById('<%= Request.QueryString("id") %>').value = '362';parent.document.getElementById('<%= Request.QueryString("textid") %>').value = 'Software for Servers';parent.document.getElementById('<%= Request.QueryString("div_id") %>').style.display='None';return false;">Software for Servers</a>
</li></ul></li><li>Parts & Peripherals<ul></ul></li><li>Software<ul></ul></li><li>CCTV Parts<ul></ul></li></ul>
</body>
</html>
