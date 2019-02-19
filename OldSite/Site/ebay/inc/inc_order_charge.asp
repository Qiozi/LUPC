<!--#include virtual="site/inc/inc_helper.asp"-->

<%
	Dim order_code		:		order_code		=	SQLescape(request("order_code"))
	Dim state_id		:		state_id		=	SQLescape(request("state_id"))
	Dim shipping_charge	:		shipping_charge	=	SQLescape(request("shipping_charge"))
	Dim sub_total		:		sub_total		=	SQLescape(request("sub_total"))
	Dim gst_rate		:		gst_rate		=	null
	Dim pst_rate		:		gst_rate		=	null
	Dim gst				:		gst				=	null
	Dim pst				:		pst				=	null
	Dim taxable			:		taxable			=	null
	Dim is_view			:		is_view 		= false
	
	'Response.Write(Request("order_code") & request("state_id"))
	
	Set	rs = conn.execute("Select * from tb_state_shipping where state_serial_no="& SQLquote(state_id))
	If not rs.eof then
		
		'
		' update state id
		'
		conn.execute("Update tb_cart_temp Set state_shipping="& SQLquote(state_id) &" where cart_temp_code="& order_code)
		'
		' update shipping charge
		'
		'if shipping_charge = "" then shipping_charge = 0
'		Conn.execute("Update tb_cart_temp_price Set sub_total="& SQLquote(sub_total) &_
'						", Shipping_and_handling="& SQLquote(shipping_charge) &_
'						", taxable_total="& SQLescape(sub_total &"+"& shipping_charge)&" where order_code="& order_code)
'		
		
		'
		'
		' tax, grand total, taxable
		'
		if (sub_total <> "" and shipping_charge <> "" ) then 
			sub_total 		= 	cdbl(sub_total)
			shipping_charge	=	cdbl(shipping_charge)
			taxable 		= 	sub_total + shipping_charge
			
			gst_rate 		= 	rs("gst")
			pst_rate		=	rs("pst")
			
			gst				=	taxable * gst_rate / 100
			pst				=	taxable * pst_rate / 100
			
			grand_total		=	taxable + gst + pst
			
			Conn.execute("Update tb_cart_temp_price Set sub_total="& SQLquote(sub_total) &_
						", Shipping_and_handling="& SQLquote(shipping_charge) &_
						", taxable_total="& SQLescape(taxable)&_
						", gst		="& SQLescape(gst)&_
						", pst		="& SQLescape(pst)&_
						", gst_rate	="& SQLescape(gst_rate)&_
						", pst_rate	="& SQLescape(pst_rate)&_
						", grand_total="& SQLquote(grand_total) &_
						", grand_total_rate="&SQLquote(grand_total)&" where order_code="& order_code)
		End if
				
		Set crs = conn.execute("Select taxable_total, grand_total_rate grand_total, sub_total"&_
								", Shipping_and_handling"&_
								", gst"&_
								", gst_rate"&_
								", pst"&_
								", pst_rate"&_
								" from tb_cart_temp_price "&_
								" where order_code="& order_code)
		if not crs.eof then
				Response.write "							<li>"&vblf
                Response.write "								<ul title='row'>"&vblf
				Response.write "							     	<li class='comment'>Sub Total:</li>"&vblf
				Response.write "							        <li title='price'>&nbsp;<span class=""price"">"& formatcurrency(crs("sub_total"), 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							     </ul>"&vblf
				Response.write "							 </li>"&vblf
				Response.write "							 <li>"&vblf
				Response.write "								<ul>"&vblf
				Response.write "							  		<li class='comment'>Shipping, Handling & Insurance(Not Adjusted):</li>"&vblf
				Response.write "							    	<li title='price'>&nbsp;<span class=""price"">"& formatcurrency(crs("Shipping_and_handling"), 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							  </ul>"&vblf
				Response.write "							  </li>"&vblf
				if cint(crs("gst_rate")) <> 0 then
                Response.write "							  <li>"&vblf
				Response.write "								<ul>"&vblf
				Response.write "							    	<li class='comment'>GST("& crs("gst_rate") &"%):</li>"&vblf
				Response.write "							       	<li title='price'>&nbsp;<span class=""price"">"& formatcurrency(crs("gst"), 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							    </ul>"&vblf
				Response.write "							  </li>"&vblf
				end if
				if cint(crs("pst_rate")) <> 0 then
				Response.write "							  <li>"&vblf
				Response.write "								<ul>"&vblf
				Response.write "							    	<li class='comment'>PST("& crs("pst_rate") &"%):</li>"&vblf
				Response.write "							       	<li title='price'>&nbsp;<span class=""price"">"& formatcurrency(crs("pst"), 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							    </ul>"&vblf
				Response.write "							  </li>"&vblf     
				end if               
				Response.write "							  <li>"&vblf
				Response.write "								<ul>"&vblf
				Response.write "							    	<li class='comment'>Total:</li>"&vblf
				Response.write "							       	<li title='price'>&nbsp;<span class=""price"">"& formatcurrency(crs("grand_total"), 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							    </ul>"&vblf
				Response.write "							  </li>"&vblf
				is_view = true
		else
			Response.write "Error: paramas is error."
		end if
		crs.close : set crs = nothing
		
	end if
	rs.close : set rs = nothing
	
	CloseConn() 
	
	if not is_view then
				Response.write "							<li>"&vblf
                Response.write "								<ul title='row'>"&vblf
				Response.write "							     	<li class='comment'>Sub Total:</li>"&vblf
				Response.write "							        <li title='price'>&nbsp;<span class=""price"">"& formatcurrency(0, 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							     </ul>"&vblf
				Response.write "							 </li>"&vblf
				Response.write "							 <li>"&vblf
				Response.write "								<ul>"&vblf
				Response.write "							  		<li class='comment'>Shipping, Handling & Insurance(Not Adjusted):</li>"&vblf
				Response.write "							    	<li title='price'>&nbsp;<span class=""price"">"& formatcurrency(0, 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							  </ul>"&vblf
				Response.write "							  </li>"&vblf
				     
				Response.write "							  <li>"&vblf
				Response.write "								<ul>"&vblf
				Response.write "							    	<li class='comment'>Total:</li>"&vblf
				Response.write "							       	<li title='price'>&nbsp;<span class=""price"">"& formatcurrency(0, 2) &"</span><span class=""price_unit"">"& CCUN &"</span></li>"&vblf
				Response.write "							    </ul>"&vblf
				Response.write "							  </li>"&vblf
	end if
%>

<script type="text/javascript">
    $().ready(function(){      
		$('ul[title=row]').css("width", "100%");
		$("li.comment").css("border", "0px solid red").css("width", "420px").css("text-align", "right").css("float", "left");
		$("li[title=price]").css("border", "0px solid red").css("float", "left").css("width", "170px").css("text-align", "right");
		closeLoading();
    });
</script>
