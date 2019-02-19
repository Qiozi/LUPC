<html>
	<head>
		<title>PayPal ASP SDK - TransactionSearch API</title>
		<link href="sdk.css" rel="stylesheet" type="text/css" />
	</head>
	<body>
		<center>
			<form action="TransactionResult.asp" method="Get">
				<span id="apiheader">TransactionSearch</span>
				<table class="api">
					<tr>
						<td class="field">
							StartDate:</td>
						<td>
							<input type="text" name="startDate" maxlength="10" size="10" value='<%=DATEADD("d",-1,date())%>' >
							(Required)
						</td>
					</tr>
					<tr>
						<td>
						</td>
						<td>
							MM/DD/YYYY
						</td>
					</tr>
					<tr>
						<td class="field">
							EndDate:</td>
						<td>
							<input type="text" name="endDate" maxlength="10" size="10" value='<%=date()%>' >
						</td>
					</tr>
					<tr>
						<td>
						</td>
						<td>
							MM/DD/YYYY
						</td>
					</tr>
					<tr>
						<td class="field">
							TransactionID:</td>
						<td>
							<input type="text" name="transactionID"></td>
					</tr>
					<tr>
						<td class="field">
						</td>
						<td>
							<br />
							<input type="Submit" value="Submit"></td>
					</tr>
				</table>
			</form>
		</center>
		<a class="home" id="CallsLink" href="Default.htm">Home</a>
	</body>
</html>
