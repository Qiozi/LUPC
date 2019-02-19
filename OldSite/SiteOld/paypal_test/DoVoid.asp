<%
If Not Request.QueryString ("transactionID")= "" Then
authorization_id= Request.QueryString ("transactionID")

End If
%>
<html >
<head>
    <title>PayPal ASP SDK - DoVoid API</title>
    <link href="sdk.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form id ="DoVoid" method ="get" action="DoVoidReceipt.asp">
    <center>
    <font size="2" color="black" face="Verdana"><b>DoVoid</b></font>
    <table class="api">
        <tr>
            <td class="field">
                Authorization ID:</td>
            <td>
                <input type="text" name="authorization_id" value=<%=authorization_id%>>
                <b>(Required)</b></td>
        </tr>
        <tr>
            <td class="field">
                Note:</td>
            <td>
                <textarea name="note" cols="30" rows="4"></textarea></td>
        </tr>
        <tr>
            <td colspan="2">
                <center>
                <input type="Submit" value="Submit" /></center>
            </td>
        </tr>
    </table>
    </center>
</form>
   <a class="home" id="CallsLink" href="Default.htm">Home</a>
</body>
</html>
