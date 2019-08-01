<html>
	<head>
		<title>PayPal SDK - MassPay</title>
		<link href="sdk.css" rel="stylesheet" type="text/css">
	</head>
	<body>
		<form action="MassPayReceipt.asp" method="get">
			<center>
				<font size="2" color="black" face="Verdana"><b>MassPay</b></font>
				<table class="api">
					<TR>
						<TD class="field" width="52">EmailSubject</TD>
						<TD class="field"><INPUT id="EmailSubject" type="text" value="You have money!" name="emailSubject" runat="server"></TD>
						<TD class="field"></TD>
						<TD class="field"></TD>
						<TD class="field"></TD>
					</TR>
					<TR>
						<TD class="field" width="52">ReceiverType</TD>
						<TD class="field"><INPUT id="ReceiverType" type="text" value="EmailAddress" name="receiverType" runat="server"></TD>
						<TD class="field"></TD>
						<TD class="field"></TD>
						<TD class="field"></TD>
					</TR>
					<TR>
						<TD class="field" nowrap>Currency Type</TD>
						<TD>
							<select name="currency">
								<option value="USD" selected>USD</option>
								<option value="GBP">GBP</option>
								<option value="EUR">EUR</option>
								<option value="JPY">JPY</option>
								<option value="CAD">CAD</option>
								<option value="AUD">AUD</option>
							</select>
						</TD>
					</TR>
					<TR>
						<TD class="field" height="14" colSpan="5">
							<P align="center">Mass Pay Item Details</P>
						</TD>
					</TR>
					<tr>
						<td class="field" width="52">
							Payee</td>
						<td class="field">
							ReceiverEmail (Required):</td>
						<td class="field">
							Amount(Required):</td>
						<td class="field">
							UniqueID
						</td>
						<td class="field">
							Note
						</td>
					</tr>
					<tr>
						<td width="52">
							<P align="right">1</P>
						</td>
						<td>
							<input type="text" name="receiveremail1" value="user@paypal.com">
						</td>
						<td>
							<input type="text" name="amount1" size="5" maxlength="7" value="1.00">
						</td>
						<td>
							<input type="text" name="uniqueID1"></td>
						<td>
							<input type="text" name="note1">
						</td>
					</tr>
					<tr>
						<td width="52">
							<P align="right">2</P>
						</td>
						<td>
							<input type="text" name="receiveremail2" value="user@paypal.com">
						</td>
						<td>
							<input type="text" name="amount2" size="5" maxlength="7" value="1.00">
						</td>
						<td>
							<input type="text" name="uniqueID2"></td>
						<td>
							<input type="text" name="note2">
						</td>
					</tr>
					<tr>
						<td width="52">
							<P align="right">3</P>
						</td>
						<td>
							<input type="text" name="receiveremail3" value="user@paypal.com">
						</td>
						<td>
							<input type="text" name="amount3" size="5" maxlength="7" value="1.00">
						</td>
						<td>
							<input type="text" name="uniqueID3"></td>
						<td>
							<input type="text" name="note3">
						</td>
					</tr>
					<tr>
						<td class="field" width="52">
						</td>
						<td align="center">
							<input type="submit" value="Submit"></td>
					</tr>
				</table>
			</center>
		</form>
		<a class="home" id="CallsLink" href="Default.htm">Home</a>
	</body>
</html>