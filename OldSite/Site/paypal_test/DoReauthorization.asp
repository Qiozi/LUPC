<html>
<head>
    <title>PayPal SDK - DoReauthorization</title>
    <link href="sdk.css" rel="stylesheet" type="text/css" />
</head>
<body>
<form action="DoReauthorizationReceipt.asp" method =get>
    <center>
    <font size="2" color="black" face="Verdana"><b>DoReauthorization</b></font>
    <table class="api">
        <tr>
            <td class="field">
                AuthorizationID:</td>
            <td>
                <input type="text" name="authorizationID" >
                (Required)</td>
        </tr>
        <tr>
            <td class="field">
                Amount:</td>
            <td>
                <input type="text" name="amount" size="5" maxlength="7" >
                <select name="currency">
                <option value="USD">USD</option>
                <option value="GBP">GBP</option>
                <option value="EUR">EUR</option>
                <option value="JPY">JPY</option>
                <option value="CAD">CAD</option>
                <option value="AUD">AUD</option>
                </select>
                (Required)</td>
        </tr>
        <tr>
            <td class="field">
            </td>
            <td>
                <input type="Submit" value="Submit" >
            </td>
        </tr>
    </table>
    </center>
    </form>
   <a class="home" id="CallsLink" href="Default.htm">Home</a>
</body>
</html>
