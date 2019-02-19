<!--#include virtual="/inc/conn.asp"-->
<!-- #include virtual ="/Paypal/CallerService.asp" -->
<table width="100%" height="670" border="0" align="center" cellpadding="1" cellspacing="2"
    bgcolor="#FFFFFF" style="border: #8FC2E2 1px solid;">
    <tr>
        <td valign="top" style="border: #E3E3E3 1px solid;">
            <center>
                <table>
                    <tr>
                        <td colspan="2" class="header">
                            <%=message%>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="header">
                            <%=ResponseHeader%>
                        </td>
                    </tr>
                    <!--displying all Response parameters -->
                    <% 
                            'reskey = resArray.Keys
                            'resitem = resArray.items
							'response.write reskey.count
                            'For resindex = 0 To resArray.Count - 1 
							'response.Write("Select * from tb_order_paypal_error_info where order_code='"& (request.Cookies("ordercode")) &"'")
							Set rs = conn.execute("Select * from tb_order_paypal_error_info where order_code='"& (request.Cookies("ordercode")) &"'")
							if not rs.eof then
								do while not rs.eof 
                    %>
                    <tr>
                        <td class="field">
                            <%' =reskey(resindex) %><b><%'=Sepration%><%= rs("errKey")%></b>
                        </td>
                        <td>
                            <%' =resitem(resindex) %><%= rs("errItem")%>
                        </td>
                    </tr>
                    <% 
								rs.movenext
								loop
						end if 
						rs.close : set rs = nothing
                        
                        set rs = conn.execute("select * from tb_order_paypal_error_info where erritem like '%Please enter a valid credit card%' and order_code ='"& request.Cookies("ordercode") &"'")
                        if not rs.eof then
                            Response.Write "<tr><td colspan=2 style='font-size:14pt;'><br><p><b>Credit card charge declined.  Please contact the credit card issurer.  Please call for any website technical issues.  Thank you.</b></p><br></td></tr>"
                        end if
                        rs.close : set rs = nothing
                        
                        
                    %>
                </table>
                <table id="__01" width="130" height="24" border="0" cellpadding="0" cellspacing="0"
                    class="btn_table" onclick="gotoBackCart('<%= LAYOUT_CURRENT_ORDER_TYPE %>');">
                    <tr>
                        <td width="28">
                            <img src="/soft_img/app/arrow_left.gif" width="28" height="24" alt="">
                        </td>
                        <td align="center" background="/soft_img/app/customer_bottom_03.gif" class="btn_style">
                            <strong><a style="cursor: pointer; color: #FFFFFF;" href='https://lucomputers.com/' title="to home">
                                Back</a> </strong>
                        </td>
                        <td width="6">
                            <img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="">
                        </td>
                    </tr>
                </table>
            </center>
        </td>
    </tr>
</table>
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td height="5">
        </td>
    </tr>
</table>
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td height="5">
        </td>
    </tr>
</table>
<table width="600" border="0" align="center" cellpadding="0" cellspacing="0">
    <tr>
        <td height="5">
        </td>
    </tr>
</table>
</div>
<!-- main end 	-->
