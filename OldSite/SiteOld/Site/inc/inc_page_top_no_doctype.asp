<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<% Option Explicit %>
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="site/inc/inc_layout.asp"-->
<div style="text-align:center">
<%
        Call FindPageTopHtml()
        Response.Write  FindPageContTopHTML()
%>
</div>
<script type="text/javascript">
if (!IsNetScapeBrowser ())
{
	$('#page_right_sollt').css('width', '37px').parent().css("width", "37px");
}
</script>
<%'= CurrentIsEbay %>

