<!------------------------------------------------------------------------------------------
GetTransactionDetails.html
==========================
This is the main page for GetTransactionDetails sample.
This page displays a text box where the user enters a
transaction ID and a Submit button that calls
TransactionDetails.asp.

Called by Default.htm.

Calls TransactionDetails.asp.
------------------------------------------------------------------------------------------->
<!--#include virtual="site/inc/validate_admin.asp"-->
<html>
	<head>
		<title>PayPal NVP Web Samples Using ASP - GetTransactionDetails</title>
		<link rel="stylesheet" type="text/css" />
	</head>
	<body>
		<center>
			<form action="paypal_transaction_detail_exec.asp" method="Get">
			<span id="apiheader">GetTransactionDetails</span>
			<br><br>
				<table class="api">
					<tr>
						<td class="field">
							<b>Transaction ID:</b
						></td>
						<td><input name="transactionID" value="">(Required)</td>
					</tr>
					<tr>
						<td colspan="2">
							<center>
								<input type="submit" value="Submit" name="Submit"></center>
						</td>
					</tr>
				</table>
			</form>
		</center>
		<br>
		<
	</body>
</html>
