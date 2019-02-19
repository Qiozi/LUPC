<!--#include virtual="site/inc/inc_helper.asp"-->

<%
Dim country_code			:	country_code			=	SQLescape(request("country_code"))
Dim state_selected_code		:	state_selected_code		=	SQLescape(request("state_selected_code"))
Dim element_state_id		:	element_state_id		=	SQLescape(request("element_state_id"))
Dim order_code				:	order_code				=	SQLescape(request("order_code"))
Dim shipping_company		:	shipping_company		=	SQLescape(request("shipping_company"))
Dim is_have_event			:	is_have_event			=	SQLescape(request("is_have_event"))

if lcase(country_code) = "ca"  then country_code = "Canada"
if lcase(country_code) = "us" then country_code = "United States"
  
	REsponse.Write "<select name="""& element_state_id &""" id='"& element_state_id &"'"
	
	if is_have_event <> "true" then
		REsponse.write " onchange=""cartChangeState(this, '"& country_code &"', '"& shipping_company &"', '"& order_code &"');"">"&vblf		
	else
		Response.write " >"&vblf
	end if
	
	Response.write "	<option value='-1'>--Select--</option>"	&vblf
        set rs=server.CreateObject("adodb.recordset")
        
		rs.open "select state_name,state_serial_no,state_code,state_shipping from tb_state_shipping where IsOtherCountry=0 and country="&SQLquote(country_code),conn,1,1
		if not rs.eof then
		do while not rs.eof

				Response.write " 	<option value='"& rs("state_code") &"' "	
				
				if cstr(state_selected_code) = cstr(rs("state_code")) then 
					response.write " selected='true' "
				end if
				Response.write 	">"
				response.write rs("state_code")
				Response.write "--"
				Response.write  rs("state_name")
				Response.write "	</option>"	&vblf
		rs.movenext
		loop
		end if
		rs.close : set rs = nothing	
	Response.write "</select>" &vblf
CloseConn()
%>

<script type="text/javascript">
	$().ready(function(){
		
		
	});
	
</script>