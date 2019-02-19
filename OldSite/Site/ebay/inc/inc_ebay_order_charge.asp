<!--#include virtual="site/inc/inc_helper.asp"-->
<%
	Dim order_code			:	order_code			=	SQLescape(request("order_code"))	
	Dim state_code			:	state_code			=	SQLescape(request("state_code"))
	Dim shipping_charge		:	shipping_charge		=	SQLescape(request("shipping_charge"))
	Dim shipping_company	:	shipping_company	=	SQLescape(request("shipping_company"))
	Dim sub_total			:	sub_total			=	0
	Dim shipping_handling	:	shipping_handling	=	0
	Dim total				:	total				=	0
	Dim pst_charge			:	pst_charge			=	0
	Dim gst_charge			:	gst_charge			=	0
	Dim taxable_total		:	taxable_total		=	0
	Dim tax_charge			:	tax_charge			=	0
	Dim gst_rate			:	gst_rate			=	0
	Dim pst_rate			:	pst_rate			=	0
	Dim hst_rate			:	hst_rate			=	0
	Dim hst_charge			:	hst_charge			=	0
	Dim price_unit			:	price_unit			=	null
	
	if isnumeric(shipping_company) then 
		Conn.Execute("UPdate tb_cart_temp Set shipping_company="& SQLquote(shipping_company) &" Where cart_temp_code="& SQLquote(order_code))
	End if
	'response.write "Update tb_cart_temp_price Set shipping_and_handling="& SQLquote(shipping_charge)&" Where order_code="&SQLquote(order_code)
	if shipping_charge = "undefined" then 
		shipping_charge = 0
	end if
	Conn.Execute("Update tb_cart_temp_price Set shipping_and_handling="& SQLquote(shipping_charge)&" Where order_code="&SQLquote(order_code))
	
	IF instr(LAYOUT_HST_STATE_IDS, state_code) >0 then 
		Conn.Execute("Update tb_cart_temp_price ct Set "&_
				" taxable_total=sub_total + "& SQLquote(shipping_charge)&_
				" ,ct.gst_rate 	=0"&_
				" ,ct.pst_rate	=0"&_
				" ,ct.pst 	= 0"&_
				" ,ct.gst 	= 0"&_
				" ,ct.hst_rate=(Select max(pst+gst) from tb_state_shipping Where state_code="& SQLquote(state_code) &")" &_
				" ,ct.hst	= round((ct.shipping_and_handling+sub_total) * (Select max(pst+gst) from tb_state_shipping Where state_code="& SQLquote(state_code) &")/100,2)"&_
				" Where order_code="& SQLquote(order_code))
	else
	
		Conn.Execute("Update tb_cart_temp_price ct Set "&_
				" taxable_total=sub_total + "& SQLquote(shipping_charge)&_
				" ,ct.gst_rate =(Select max(gst) from tb_state_shipping Where state_code="& SQLquote(state_code) &")"&_
				" ,ct.pst_rate	=(Select max(pst) from tb_state_shipping Where state_code="& SQLquote(state_code) &")"&_
				" ,ct.pst 	= round((ct.shipping_and_handling+sub_total) * (Select max(pst) from tb_state_shipping Where state_code="& SQLquote(state_code) &")/100,2)"&_
				" ,ct.gst 	= round((ct.shipping_and_handling+sub_total) * (Select max(gst) from tb_state_shipping Where state_code="& SQLquote(state_code) &")/100,2)"&_
				" ,ct.hst=0, ct.hst_rate=0"&_
				" Where order_code="& SQLquote(order_code))
	End if
	
	Conn.execute("UPdate tb_cart_temp_price ct Set grand_total_rate=ct.pst+ct.gst++ct.hst+ct.taxable_total"&_
					" ,grand_total=ct.pst+ct.gst++ct.hst+ct.taxable_total, sub_total_rate=sub_total Where order_code="&SQLquote(order_code))
	
	Set rs = COnn.execute("Select * from tb_cart_temp_price where order_code="& SQLquote(order_code))
	If not rs.eof then
		sub_total			=	rs("sub_total")
		shipping_handling	=	rs("shipping_and_handling")
		'total				=	rs("grand_total_rate")
		taxable_total		=	rs("taxable_total")
		pst_charge			= 	rs("pst")
		gst_charge			=	rs("gst")
		gst_rate			=	rs("gst_rate")
		pst_rate			=	rs("pst_rate")
		price_unit			=	rs("price_unit")
		total				=	rs("grand_total_rate")
		hst_charge			=	rs("hst")
		hst_rate			=	rs("hst_rate")
	End if
	rs.close : set rs =nothing

	
	Set rs = conn.execute("select Country from tb_state_shipping Where state_code="& SQLquote(state_code))
	if not rs.eof then 
		Response.cookies("shipping_country_code") 	= 	rs(0)
		Response.cookies("shipping_state_code") 	=	state_code
	End if
	

%>						
            <li>
                <ul title='row'>
                    <li class='comment'>Sub Total:</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( sub_total, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
            <li>
                <ul>
                    <li class='comment'>Shipping, Handling & Insurance(Not Adjusted):</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( shipping_handling, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
            <li>
                <ul>
                    <li class='comment'>Taxable Total:</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( taxable_total, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
            <% 
			IF instr(LAYOUT_HST_STATE_IDS, state_code) >0 then 
			%>
            <li>
                <ul>
                    <li class='comment'>HST(<%= hst_rate %>%):</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( hst_charge, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
            <%
			Else
			%>
            <li>
                <ul>
                    <li class='comment'>GST(<%= gst_rate %>%):</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( gst_charge, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
            <li>
                <ul>
                    <li class='comment'>PST(<%= pst_rate %>%):</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( pst_charge, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
            <% End if %>
            <li>
                <ul>
                    <li class='comment'>Total:</li>
                    <li title='price'>&nbsp;<span class="price"><%= formatcurrency( total, 2) %></span><span class="price_unit"><%= ( price_unit) %></span></li>
                </ul>
            </li>
                         
                                
                                
<script type="text/javascript">
$().ready(function(){
	
		$('#cart_charge_area_p').css("height", "180px").css("background", "#efefef").css("border-top", "1px solid #dddddd").css("border-bottom", "1px solid #dddddd").css("line-height", "20px").find('p').css("padding", "15px 5px 15px 5px");
		$('ul[title=row]').css("width", "100%");
		$("li.comment").css("border", "0px solid red").css("width", "440px").css("text-align", "right").css("float", "left");
		
		$("li[title=price]").css("border", "0px solid red").css("float", "left").css("width", "150px").css("text-align", "right");
});
</script>