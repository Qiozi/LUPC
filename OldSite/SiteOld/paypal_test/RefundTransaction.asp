<%
If Not Request.QueryString ("transactionID")= "" Then
authorizationID	= Request.QueryString ("transactionID")
amount= Request.QueryString ("amount")
End If
%>
<html>
	<head>
		<title>PayPal ASP - RefundTransaction API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body alink="#0000FF" vlink="#0000FF">
		<br>
		<center>
			<font size="2" color="black" face="Verdana"><b>RefundTransaction</b></font>
			<br>
			<br>
			<FORM id="Refund" method="get" action="RefundReceipt.asp">
				<table width="500" ID="Table1">
					<tr>
						<td align="right">Transaction ID:</td>
						<td align="left"><input type="text" name="transactionID" value='<%=authorizationID%>' ID="transactionID"></td>
						<td><b>(Required)</b></td>
					</tr>
					<tr>
						<td align="right">Refund Type:</td>
						<td align="left">
							<select name="refundType" ID="refundType">
								<option value="Full">Full</option>
								<option value="Partial">Partial</option>
							</select>
						</td>
					</tr>
					<tr>
						<td align="right">Amount:</td>
						<td align="left">
							<input type="text" name="amount" value='<%=amount%>' ID="amount">
							<select name="currency" ID="Select2">
								<option value="USD">USD</option>
								<option value="GBP">GBP</option>
								<option value="JPY">JPY</option>
								<option value="EUR">EUR</option>
								<option value="CAD">CAD</option>
								<option value="AUD">AUD</option>
							</select>
						</td>
					</tr>
					<tr>
						<td />
						<td><b>(Required if Partial Refund)</b></td>
					</tr>
					<tr>
						<td align="right">Memo:</td>
						<td align="left"><textarea name="memo" cols="30" rows="4" ID="Textarea1"></textarea></td>
					</tr>
					<tr>
						<td align="right"></td>
						<td align="left"><br>
							<input type="Submit" value="Submit" ID="Submit1" NAME="Submit1"></td>
					</tr>
				</table>
			</FORM>
		</center>
		<br>
		<a id="CallsLink" href="Default.htm">Home</a>
	</body>
</html>
