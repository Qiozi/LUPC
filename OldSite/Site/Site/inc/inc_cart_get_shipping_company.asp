<!--#include virtual="site/inc/inc_helper.asp"-->
<div title="shiping_company_1"><%
	Dim order_code		:		order_code		=	SQLescape(request("order_code"))
	Dim state_code		:		state_code		=	SQLescape(request("state_code"))
	Dim shipping_company_id:		shipping_company_id=	SQLescape(request("shipping_company_id"))
	Dim system_count	:		system_count	=	0
	Dim current_shipping_charge:		current_shipping_charge = 0
	
	'Response.Write(Request("order_code") & request("state_code"))
	
	Set	rs = conn.execute("Select * from tb_state_shipping where state_code="& SQLquote(state_code))
	If not rs.eof then
		'
		' update state_code
		'
		'
		Conn.Execute("Update tb_cart_temp Set shipping_state_code = "&SQLquote(state_code))
		'
		' system count
		Set srs = conn.execute("select count(cart_temp_code) from tb_cart_temp where cart_temp_code="& SQLquote(order_code))		
		if not srs.eof then
				system_count	=	srs(0)
		end if
		srs.close : set srs = nothing
		
		'response.Write(system_count)
		
		'
		' update shipping company id
		'
		if isnumeric(shipping_company_id) then 
			conn.execute("Update tb_cart_temp set shipping_company="& SQLquote(shipping_company_id) )
		end if
		
		Set crs = conn.execute("Select shipping_company_id, shipping_company_name"&_
								" from tb_shipping_company "&_
								" where system_category="&SQLquote(rs("system_category_serial_no"))&" and is_sales_promotion=0 order by qty asc")
		if not crs.eof then
				Do while not crs.eof 
						
						
												
						Set srs = conn.execute("select sum(es.shipping_charge * ct.cart_temp_Quantity) shipping_charge, count(es.shipping_charge) c "&_
												" from tb_ebay_system_shipping  es"&_
												" inner join tb_sp_tmp st on st.system_templete_serial_no=es.ebay_system_sku"&_
												" inner join tb_cart_temp ct on ct.product_serial_no = st.sys_tmp_code and ct.cart_temp_code="& SQLquote(order_code) &_
												" where shipping_company_id="& SQLquote(crs("shipping_company_id")) )
						'Response.Write ("select sum(es.shipping_charge * ct.cart_temp_Quantity) shipping_charge, count(es.shipping_charge) c "&_
'												" from tb_ebay_system_shipping  es"&_
'												" inner join tb_sp_tmp st on st.system_templete_serial_no=es.ebay_system_sku"&_
'												" inner join tb_cart_temp ct on ct.product_serial_no = st.sys_tmp_code and ct.cart_temp_code="& SQLquote(order_code) &_
'												" where shipping_company_id="& SQLquote(crs("shipping_company_id")) &"<br>")
						If not srs.eof then 
							if system_count = srs("c") then 
								'response.write cstr(shipping_company_id) &"|"&cstr(crs("shipping_company_id")) 
								Response.write "<input type='radio' name='shipping_charge' value='"& ConvertDecimal(srs(0)) &"' title='"&crs("shipping_company_id")&"' "
								if cstr(shipping_company_id) = cstr(crs("shipping_company_id")) then 
									Response.write " checked='true' "
									current_shipping_charge = srs(0)
								end if
								Response.write " > "& crs("shipping_company_name") &"&nbsp;&nbsp;<span class='price1'>"& formatcurrency(ConvertDecimal(srs(0)),2) &"</span><span class='price_unit'>"& CCUN &"</span><br/>"&vblf
							else
								'Response.write system_count &"|"& srs("c") &"<br>"
							End if
						end if
						srs.close  : set srs = nothing
				crs.movenext
				loop
		end if
		crs.close : set crs = nothing
		
		
	else
		Response.Write("Please select Country/Province")
	end if
	rs.close : set rs = nothing
	
	CloseConn() 
%></div>
<script type="text/javascript">
$().ready(function(){
	$("input[type=radio]").click(function(){
				
				cartChangeOrderCharge('<%= order_code %>', $('#country').val(), $(this).attr('title'), $(this).val());
				
				});
	if($('div[title=shiping_company_1]').html() == "")
		$('div[title=shiping_company_1]').html('<b>Params is error.</b>');
	
	//closeLoading();
});
</script>