<!--#include virtual="site/inc/inc_helper.asp"-->
<%
response.Clear()
dim id, is_text
dim filter_back,filter_first, Special_cash_price

id =SQLescape(request("id"))
is_text = SQLescape(Request("is_text"))
        
if len(id)>0 then 
'dim Special_cash_price, now_low_price, save_price
	save_price = GetSavePrice(id)

	    set rs = conn.execute("select product_current_price,product_current_discount from tb_product where product_serial_no='"& id &"'")
        if not rs.eof then
            product_price  = cdbl(rs("product_current_price"))
        	save_cost = cdbl(rs("product_current_discount"))
        end if
        rs.close 

								filter_first = cdbl(product_price)
								filter_back = filter_first
								Special_cash_price = ChangeSpecialCashPrice(filter_first- cdbl(save_cost))
         if is_text <> "true" then 
                          if cdbl(save_cost) <> 0 then  %>
                                <tr bgcolor="#f2f2f2" >
                                  <td width="58%" bgcolor="#FFFFFF"><strong>Regular Price</strong></td>
                                  <td align="right" bgcolor="#FFFFFF"><strong><%= ConvertDecimalUnit(CURRENT_SYSTEM, filter_back)%></strong></td>
                                </tr>
                                <tr bgcolor="#f2f2f2" >
                                  <td bgcolor="#FFFFFF" style="border-bottom:#333333 1px solid;"><strong>Instant Discount</strong></td>
                                  <td align="right" bgcolor="#FFFFFF" style="color:#ff6600; border-bottom:#333333 1px solid;"><strong>-<%=  ConvertDecimalUnit(CURRENT_SYSTEM, save_cost)%></strong></td>
                                </tr>
                                <tr bgcolor="#f2f2f2" >
                                  <td bgcolor="#FFFFFF"><strong>Now&nbsp;Low&nbsp;Price</strong></td>
                                  <td align="right" bgcolor="#FFFFFF"><strong><%= ConvertDecimalUnit(CURRENT_SYSTEM, filter_first-cdbl(save_cost))%></strong></td>
                                </tr>
                                <% else %>
                                <tr bgcolor="#f2f2f2" >
                                  <td bgcolor="#FFFFFF"><strong>Regular&nbsp;Price</strong></td>
                                  <td align="right" bgcolor="#FFFFFF"><strong><%= ConvertDecimalUnit(CURRENT_SYSTEM, filter_first-cdbl(save_cost))%></strong></td>
                                </tr>
                                <%end if%>
                                
                                <tr bgcolor="#f2f2f2" >
                                  <td bgcolor="#FFFFFF"><strong>Special&nbsp;Cash&nbsp;Price</strong></td>
                                  <td align="right" bgcolor="#FFFFFF"><strong><%=  ConvertDecimalUnit(CURRENT_SYSTEM, Special_cash_price)%></strong></td>
                                </tr>
                                <tr bgcolor="#f2f2f2" >
                                  <td height="10" bgcolor="#FFFFFF" class="text_hui_11"></td>
                                  <td height="10" align="right" bgcolor="#FFFFFF" class="text_hui_11"></td>
                                </tr>
<%
        end if

		set rs = conn.execute(sql_sale_promotion_rebate_sign(id))
	    if not rs.eof then
	    if save_price <> "" then 
		    now_low_price = cdbl(rs("product_current_price")) - cdbl(save_price)
	    else
		    now_low_price = cdbl(rs("product_current_price"))
	    end if
	    Special_cash_price = ChangeSpecialCashPrice(now_low_price )
        
		if is_text = "true" then 
		%>
		Manufacturer MIR Ending Date: <strong><%= ConvertDate(rs("end_datetime")) %></strong><br/><br/>
		<%
		else
		%>
<!--		//<a class="blue_orange_11" style="font-size:9pt;color:#FF6600" href="pro_img/rebate_pdf/<%= rs("pdf_filename")%>" target="_blank"><span  class="text_orange_13">
		//<%= formatcurrency(now_low_price - cdbl(rs("save_cost")),2) %> After Rebate: <%= formatcurrency(rs("save_cost"),2)%> Mail-In-Rebate <br>
		//<%= formatcurrency(Special_cash_price - cdbl(rs("save_cost")),2) %> After Rebate: <%= formatcurrency(rs("save_cost"),2)%> Mail-In-Rebate(Cash Price)")
		
		//<br>Ending Date: <%= ConvertDate(rs("end_datetime")) %></span></a><br/>-->
								<tr bgcolor="#f2f2f2" >
								<td bgcolor="#FFFFFF"><strong>Mail-In-Rebate</strong></td>
								  <td align="right" bgcolor="#FFFFFF"><span style="color:blue;"><strong>-<%= formatcurrency(ConvertDecimal( rs("save_cost")),2)%><span class="price_unit"><%= CCUN %></span></strong></span></td>
								</tr>
								  <tr bgcolor="#f2f2f2" >
								 <td bgcolor="#FFFFFF"><strong>After Rebate</strong></td>
								 <td align="right" bgcolor="#FFFFFF" ><strong><%=  ConvertDecimalUnit(Current_system,now_low_price - cdbl(rs("save_cost"))) %></strong></td>
								 </tr>
								  <tr bgcolor="#f2f2f2" >
								  <td bgcolor="#FFFFFF"><strong>After Rebate(Cash)</strong></td>
								   <td align="right" bgcolor="#FFFFFF"><strong><%=  ConvertDecimalUnit(Current_system,Special_cash_price - cdbl(rs("save_cost"))) %> </strong></td>
								  </tr>
		<%
		end if
	end if
	rs.close : set rs = nothing
end if
closeconn()
%>
