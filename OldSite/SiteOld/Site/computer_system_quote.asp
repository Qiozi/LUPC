<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<TITLE>LU Computers</TITLE>
<style>
.helpText		{ background : transparent; color : #5186BE; font-size : 12px; font-weight: bold; } 
.helpTextWhiteSm	{ background : transparent; color : #FFFFFF; font-size :  9px; font-weight: bold; text-decoration : none; }
.helpTextWhite	{ background : transparent; color : #FFFFFF; font-size : 12px; font-weight: bold; text-decoration : none; }
.helpTextWhiteLg	{ background : transparent; color : #FFFFFF; font-size : 18px; font-weight: bold; }
</style>
</head>

</HEAD>
<BODY>
<!--include virtual="site/inc/inc_helper.asp"-->

<table width="100%" cellspacing="1" cellpadding="2"  border="0" bgcolor="#417CB6" align="center">
<tr><td align="left"><div style="margin-top : 2px; margin-bottom : 2px; margin-left : 3px;"><span class="helpTextWhiteLg">Save Quote</span></div></td></tr>
<tr>
<td width="100%" style="background:#ffffff">
&nbsp;<P align="center">This Configuration is already saved.<P align=center> It can be accessed by Quote Number: <b style="font-size:16pt;"><%

response.write request("quote")
%></b>&nbsp;&nbsp;<p align=center><a href="javascript: this.close()">CLOSE THIS WINDOW</a><P>&nbsp;

</td>
</tr>
</table>
</BODY>
</HTML>
