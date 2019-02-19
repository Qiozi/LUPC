<%
If Not Request.QueryString ("transactionID")= "" Then
authorization_id	= Request.QueryString ("transactionID")
amount= Request.QueryString ("amount")
End If
%>
<html>
	<head>
		<title>PayPal ASP - DoCapture API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<form id="docapture" method="get" action="DoCaptureReceipt.asp">
		<body>
			<center>
			<font size=2 color=black face=Verdana><b>DoCapture</b></font>
			<br>
			<br>
				<table>
					<tr>
						<td class="field">
							Authorization ID:</td>
						<td>
							<input type="text" name="authorization_id" value='<%=authorization_id%>' > <b>(Required)</b></td>
					</tr>
					<tr>
						<td class="field">
							Complete Code Type:</td>
						<td>
							<select name="CompleteCodeType">
								<option value="Complete">Complete</option>
								<option value="NotComplete">NotComplete</option>
							</select>
						</td>
					</tr>
					<tr>
						<td class="field">
							Amount:</td>
						<td>
							<input type="text" name="amount" size="5" maxlength="7"  value='<%=amount%>' >
							<select name="currency">
								<option value="USD">USD</option>
								<option value="GBP">GBP</option>
								<option value="EUR">EUR</option>
								<option value="JPY">JPY</option>
								<option value="CAD">CAD</option>
								<option value="AUD">AUD</option>
							</select>
							<b>(Required)</b></td>
					</tr>
					<tr>
						<td class="field">
							Invoice ID:</td>
						<td>
							<input type="text" name="invoice_id"></td>
					</tr>
					<tr>
						<td class="field">
							Note:</td>
						<td>
							<textarea name="note" cols="30" rows="4"></textarea></td>
					</tr>
					<tr>
						<td class="field">
						</td>
						<td>
							<input type="Submit" value="Submit">
						</td>
					</tr>
				</table>
			</center>
			<a class="home" id="CallsLink" href="default.htm">Home</a>
		</body>
	</form>
</html>
