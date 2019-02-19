<%
'
'
'
'	GetCountryName(code)
'   Current_Currency_Unit(current_system)
'	ConvertDecimal(ca_price)
'   ConvertDecimalUnit(current_system, ca_price)
'	getCurrencyConvert()
'   Function ConvertDecimalUnitSystemCustomize(current_system, ca_price)
'   GetNewSystemCode()
'   IsLoginWeb(page_index)
'   GetNewOrderCode()
'	GetPartShortName(sku)	
'
'	GetSystemPriceAndSave(system_templete_serial_no)
'	UpdateSystemPriceByCategoryID(category_id)
'	GetSystemPriceAndSave8NoDynamic(sys_tmp_code)
'	splitConfigurePrice(prices, t)
'	GetSavePrice(sku)
'	GetPartCurrentSell(sku)
'	GetPartCurrentPrice(sku)
'	ChangeSpecialCashPrice(card_price)
'	GetConfigureProductIds()
'	FindPartStoreStatus_system_setting( ltd_stock)
'	PartChoosePhotoSKU(current_part_sku, other_part_sku)
'	ExportSelectControl(product_serial_no, max_quantity, part_quantity, lu_sku_selected, part_group_id, radio_id, sys_tmp_sku)
'	GetSystemConfigurePrice()
'
'	CopyConfigureSystemToCart(system_templete_serial_no, sys_tmp_sku,tmp_order_code, is_copy_to_cart )
'	sysAddToSpTmp(system_sku, sys_tmp_sku, tmp_order_code, is_to_cart)
'	GetSystemNameFromSpTmp(id)
'	FindSpecialCashPriceComment()
'	GetShippingCompany(tmp_code)
'	GetStateShipping(tmp_code)
'	GetOrderCountryCodeAndStateIDAndShipingCompanyID(order_code)
'	setViewCount(is_part, ip, id)
'	GetSystemPhotoByID2(id)
'	FindOnSaleSingle(product_id)
'	sql_sale_promotion_rebate_sign(product_id )
'	sql_sale_promotion_rebate_all()
'	setViewCount(is_part, ip, id)
'	GetPartCommentFile(sku)
'
'	GetHotImg(str)
'	GetRebateImg(str)
'	GetNewImg(str)
'	GetSaleImg(str)
'
'	FindPartStoreStatus2(id, ltd_stock)
'	getMenuChildName(id)
'	ResponseStockStatus(id)
'	GetPartOnSaleSavePrice(sku)
'
'	CustomerSendMsg(order_code, content)
'	GetPayMethodNew(id)
'	CopyCartToOrder(order_code, customerID, cmd)
'	copyToSPDetail(system_sku)
'	ConvertDateHour(currentdate)
'
'	AddOrderPayRecord(order_code, amt, pay_record_id)
'	setOrderInoviceNo(order_code)
'	GetEbayNumberForSpTmp(sys_code)
'
'	GetCurrentOrder()
'	ReadAdvFile(id, post)
'	CurrentSystemDefault(str, is_current_bay)
'
'	getCurrentPageName()
'	ValidateOrder_Code(str)
'	ValidateUserSessionInfo(user_id, is_close_conn)
'	GetSysOldSKUbyUncomtusize(sku8)
'	FindSystemPriceAndSaveAndCost8Action(sys_tmp_code)
'
'
'	GetSystemName2(id)
'
'	GetNewCustomerCode()
' 	validHttpReferer()
'
'	SaveNewCountryState(country, stateName)
'
'   ChangeTempOrderSystemPrice(tmp_order_code)

Function GetCountryName(code)
	if code = "CA" or code=CSCA then
		GetCountryName = "Canada"
	elseif code = "US" or code=CSUS then
		GetCountryName = "USA"
	else
		GetCountryName = "N/A"
	end if

End function



'   ??CAD
'--------------------------------------------------------------------------
Function Current_Currency_Unit(current_system)
'--------------------------------------------------------------------------
    Current_Currency_Unit = "CAD"
    
    if(current_system)= "1" then
        Current_Currency_Unit = "CAD"
    end if
    if(current_system)= "2" then
        Current_Currency_Unit = "USD"
    end if
End Function




'--------------------------------------------------------------------------
Function ConvertDecimal(ca_price)
'--------------------------------------------------------------------------
	Dim CURRENCY_CONVERTER : CURRENCY_CONVERTER = getCurrencyConvert()

	if isnull(ca_price) or isempty(ca_price) or ca_price = "" or (Current_System = CSUS and CURRENCY_CONVERTER = 1) then 
		ConvertDecimal = "params is error"
	end if
	'response.write CURRENCY_CONVERTER &"d"
	if Current_System = CSCA then 
		ConvertDecimal = formatnumber(ca_price, 2,,,0) 
	else
		ConvertDecimal = formatnumber(cdbl(ca_price) * CURRENCY_CONVERTER, 2,,,0)
	end if
	'response.write getCurrencyConvert()
End Function



'--------------------------------------------------------------------------
function appendUnit(ca_price, is_format)
'--------------------------------------------------------------------------
	if not is_format then 
		appendUnit = "<span class='price1'>"& ca_price &"</span><span class='price_unit'>"& CCUN &"</span>"
	else
		appendUnit = "<span class='price1'>"& formatcurrency(ca_price, 2) &"</span><span class='price_unit'>"& CCUN &"</span>"
	end if
End function



'--------------------------------------------------------------------------
Function getCurrencyConvert()
'--------------------------------------------------------------------------
	Dim rs
	getCurrencyConvert = Cdbl( Session("CURRENCY_CONVERTER") )
	
	if SQLescape(getCurrencyConvert) = "" then 
		set rs = conn.execute("select 	  currency_usd "&_
								"	from "&_
								"	tb_currency_convert where is_current=1  ")
		if not rs.eof then
			getCurrencyConvert				= cdbl(rs(0))
			Session("CURRENCY_CONVERTER") 	= rs(0)
		End if
		rs.close : set rs = nothing
	End if
	'Response.write getCurrencyConvert
End Function


'--------------------------------------------------------------------------
Function ConvertDecimalUnit(current_system, ca_price)
'--------------------------------------------------------------------------
    
    if  not isNull(ca_price) and not isEmpty(ca_price) then
	
        if(current_system)= "1" then
            ConvertDecimalUnit = "<span class='price'>" &   formatcurrency(ca_price, 2) & "</span><span class='price_unit'>CAD</span>"
        end if
        if(current_system)= "2" then   
            ConvertDecimalUnit = "<span class='price'>" &   formatcurrency(ConvertDecimal(ca_price ), 2) & "</span><span class='price_unit'>USD</span>"
        end if
    else
        ConvertDecimalUnit = "ERROR"
    end if
End Function



'--------------------------------------------------------------------------
Function ConvertDecimalUnitSystemCustomize(current_system, ca_price)
'--------------------------------------------------------------------------
    Dim CURRENCY_CONVERTER : CURRENCY_CONVERTER = getCurrencyConvert()
    Dim sign        :   sign = "+"
    if SQLescape(ca_price) <> ""  then
        if (cdbl(ca_price) >=0) then
            sign = "+"
        else
            sign = "-"
        end if
    
        if(current_system)= "1" then
            if instr(formatnumber(ca_price, 2), "-")>0 then            
                ConvertDecimalUnitSystemCustomize = "<span  class='price2'>" & sign & "$"&  replace(formatnumber(ca_price, 2),"-", "") & "</span><span class='price_unit'>CAD</span>"
            else
                ConvertDecimalUnitSystemCustomize = "<span class='price1'>" & sign & "$"&  formatnumber(ca_price, 2) & "</span><span class='price_unit'>CAD</span>"
            end if
        end if
        if(current_system)= "2" then
            ca_price = ConvertDecimal(ca_price)
            'response.write CURRENCY_CONVERTER
			
             if instr(formatnumber(ca_price, 2), "-")>0 then            
                ConvertDecimalUnitSystemCustomize = "<span  class='price2'>" & sign & "$"&  replace(formatnumber(ca_price, 2),"-", "") & "</span><span class='price_unit'>USD</span>"
            else
                ConvertDecimalUnitSystemCustomize = "<span class='price1'>" & sign & "$"&  formatnumber(ca_price, 2) & "</span><span class='price_unit'>USD</span>"
            end if
        end if
		
    else
        ConvertDecimalUnitSystemCustomize = "ERROR"
    end if
End Function



'--------------------------------------------------------------------------
Function GetNewSystemCode()
'--------------------------------------------------------------------------
'    Dim newCode         
'    Dim rs              :       rs          =       null
'	RANDOMIZE
'	GetNewSystemCode     =       Int((99999999-20000000+1)*rnd + 20000000)
'
'	if Len(GetNewSystemCode) <> 8 then 
'		GetNewSystemCode = GetNewSystemCode()
'	end if
'
'    set rs = conn.execute("select system_code from tb_system_code_store where system_code='"&GetNewSystemCode&"'")
'	if not rs.eof then
'		GetNewSystemCode = GetNewSystemCode()
'	end if
'	rs.close : set rs = nothing
	dim rs
	GetNewSystemCode = ""
	set rs = conn.execute("select * from tb_store_sys_code limit 0,1")
	if not rs.eof then
		GetNewSystemCode	=	rs("SysCode")
	end if
	rs.close : set rs =nothing
	
	conn.execute("delete from tb_store_sys_code where SysCode='"& GetNewSystemCode &"'")
End Function
   

'--------------------------------------------------------------------------
Function GetNewOrderCode()
'--------------------------------------------------------------------------
'
'	RANDOMIZE
'    Dim newCode         :       newCode     =       Int((999999-200000+1)*rnd + 200000)
'    Dim rs              :       rs          =       null
'	GetNewOrderCode = newCode
'
'    if Len(GetNewOrderCode) <> 6 then
'		 GetNewOrderCode = GetNewSystemCode()
'	End if
'    set rs = conn.execute("select order_code from tb_order_code where order_code='"&newCode&"'")
'	if not rs.eof then
'		GetNewOrderCode = GetNewOrderCode()
'	else
'		GetNewOrderCode = newCode
'		conn.execute("insert into tb_order_code (order_code, regdate, is_order)	values	( '"&newCode&"', now(), 0)")
'	end if
'	rs.close : set rs = nothing
	dim rs
	GetNewOrderCode = ""


    set rs = conn.execute("select count(id ) c from tb_order_code where is_order=0 and ip='"&LAYOUT_HOST_IP&"' and date_format(regdate,'%y%m%d')=date_format(now(), '%y%m%d')")
    if not rs.eof then
        if(rs(0)>30) then
            conn.execute("insert into tb_ip_deny (IP, regdate)	values	('"& LAYOUT_HOST_IP &"',now())")

        end if
    end if
    rs.close
	
		set rs = conn.execute("select * from tb_store_order_code limit 0,1")
		if not rs.eof then
			GetNewOrderCode	=	rs("OrderCode")
			conn.execute("insert into tb_order_code (order_code, regdate, is_order, ip)	values	( '"&GetNewOrderCode&"', now(), 0, '"& LAYOUT_HOST_IP &"')")
		end if
		rs.close : set rs =nothing
	
	
	conn.execute("delete from tb_store_order_code where OrderCode='"& GetNewOrderCode &"'")
	
End Function



'--------------------------------------------------------------------------
Function IsLoginWeb(page_index)
'--------------------------------------------------------------------------
    If  LAYOUT_CCID="" then
        Response.redirect("/site/member_login.asp?url="& page_index)
		response.End()
    end if
	'Response.write request.Cookies("customer_serial_no")
	'response.End()
End Function



'--------------------------------------------------------------------------
function ValidateUserSessionInfo(user_id, is_close_conn)
'--------------------------------------------------------------------------
	if SQLescape(user_id) = "" then  
		if(is_close_conn = "closeConn") then 
			closeConn()
		end if
		response.redirect(LAYOUT_HOST_URL & "session_lost.asp") 
		response.End()
	end if
end function



'--------------------------------------------------------------------------
function ValidateOrder_Code(str)
'--------------------------------------------------------------------------
	if SQLescape(str)<>"" then  
		'if(is_close_conn = "closeConn") then 
			
		'end if		
		if (str = "site" and LAYOUT_ORDER_CODE = "") or (str = "ebay" and LAYOUT_EBAY_ORDER_CODE = "") then
			closeConn()
			response.redirect(LAYOUT_HOST_URL & "session_lost.asp") 
			response.End()
		End if
	else
		if LAYOUT_CURRENT_ORDER_TYPE <> "" then 
			if (LAYOUT_CURRENT_ORDER_TYPE = "ebay" and LAYOUT_EBAY_ORDER_CODE = "") or (LAYOUT_CURRENT_ORDER_TYPE = "site" and LAYOUT_ORDER_CODE = "") then 
				closeConn()
				response.redirect(LAYOUT_HOST_URL & "session_lost.asp") 
				response.End()
			End if
			
		End if 
	end if
end function




'--------------------------------------------------------------------------
Function GetPartShortName(sku)
'--------------------------------------------------------------------------
	Dim rs 
	GetPartShortName = null
	
	Set rs = conn.execute("select product_short_name from tb_product where product_serial_no="& SQLquote(sku) )
	if not rs.eof then
		GetPartShortName = rs(0)
	end if
	rs.close : set rs = nothing
	
End Function
'
'	find system price, save
'
'
'--------------------------------------------------------------------------
function GetSystemPriceAndSave(system_templete_serial_no)
'--------------------------------------------------------------------------
	dim rs
	GetSystemPriceAndSave = "0|0|0"
	'Response.write getCurrencyConvert()
	if system_templete_serial_no <> "" then 
		if len(system_templete_serial_no) = LAYOUT_SYSTEM_CODE_LENGTH then
            if Current_system = CSUS then 

			    set rs = conn.execute("select ifnull(sum(round(p.product_current_price * "& getCurrencyConvert() &", 2)* part_quantity),0), ifnull(sum(round(p.product_current_discount * "& getCurrencyConvert() &" , 2)* part_quantity),0), ifnull(sum(round(p.product_current_cost * "& getCurrencyConvert() &" , 2)* part_quantity),0) from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sys_tmp_code='"& system_templete_serial_no &"' ")
		
            else
                set rs = conn.execute("select ifnull(sum(round(p.product_current_price * 1, 2)* part_quantity),0), ifnull(sum(round(p.product_current_discount * 1 , 2)* part_quantity),0), ifnull(sum(round(p.product_current_cost * 1 , 2)* part_quantity),0) from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sys_tmp_code='"& system_templete_serial_no &"' ")
		
            end if
        
        else
			if Current_system = CSUS then 
				set rs = conn.execute("select ifnull(sum(round(p.product_current_price * "& getCurrencyConvert() &", 2) * part_quantity),0), ifnull(sum(round(p.product_current_discount * "& getCurrencyConvert() &", 2) * part_quantity),0), ifnull(sum(round(p.product_current_cost * "& getCurrencyConvert() &", 2) * part_quantity),0) from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.system_sku='"& system_templete_serial_no &"' and p.tag=1 and pc.tag=1 and p.is_non=0")
			else
				set rs = conn.execute("select ifnull(sum(round(p.product_current_price * 1, 2) * part_quantity),0), ifnull(sum(round(p.product_current_discount * 1, 2) * part_quantity),0) ,ifnull(sum(round(p.product_current_cost * 1, 2) * part_quantity),0) from tb_ebay_system_parts sp inner join tb_product p on p.product_serial_no=sp.luc_sku inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.system_sku='"& system_templete_serial_no &"' and p.tag=1 and pc.tag=1 and p.is_non=0")
			End if
		end if
		if not rs.eof then 
			GetSystemPriceAndSave = rs(0) & "|" & rs(1)	& "|" & rs(2)		
		end if
		rs.close : set rs = nothing
	end if
end function




function UpdateSystemPriceByCategoryID(category_id)
    
	if Current_System = CSUS then
			Conn.execute("update tb_ebay_system  st, tb_ebay_system_and_category ec Set "&_
		"tmp_sell=	(select ifnull(sum(round((p.product_current_price-p.product_current_discount)* "&getCurrencyConvert() &" , 2) * part_quantity),0) "&_
		"		from tb_ebay_system_parts sp  "&_
		"		inner join tb_product p on p.product_serial_no=sp.luc_sku  "&_
		"		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no  "&_
		"		where sp.system_sku=st.id and p.tag=1 and pc.tag=1 and p.is_non=0) "&_
		",tmp_discount=	(select ifnull(sum(round( p.product_current_discount * "&getCurrencyConvert() &", 2)* part_quantity),0) "&_
		"		from tb_ebay_system_parts sp  "&_
		"		inner join tb_product p on p.product_serial_no=sp.luc_sku  "&_
		"		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no  "&_
		"		where sp.system_sku=st.id and p.tag=1 and pc.tag=1 and p.is_non=0) "&_
		"where st.id=ec.systemSku and ec.eBaySysCategoryId="& SQLquote(category_id) &" and st.showit=1 ")
	else
			Conn.execute("update tb_ebay_system st, tb_ebay_system_and_category ec Set "&_
		"tmp_sell=	(select ifnull(sum((p.product_current_price-p.product_current_discount) * part_quantity),0) "&_
		"		from tb_ebay_system_parts sp  "&_
		"		inner join tb_product p on p.product_serial_no=sp.luc_sku  "&_
		"		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no  "&_
		"		where sp.system_sku=st.id and p.tag=1 and pc.tag=1 and p.is_non=0) "&_
		",tmp_discount=	(select ifnull(sum(p.product_current_discount* part_quantity),0) "&_
		"		from tb_ebay_system_parts sp  "&_
		"		inner join tb_product p on p.product_serial_no=sp.luc_sku  "&_
		"		inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no  "&_
		"		where sp.system_sku=st.id and p.tag=1 and pc.tag=1 and p.is_non=0) "&_
		"where st.id=ec.systemSku and ec.eBaySysCategoryId="& SQLquote(category_id) &" and st.showit=1 ")
        
	End if
		Conn.execute("Update tb_product_category Set update_price_date=now() where menu_child_serial_no="& SQLquote(category_id))
		
End function



'--------------------------------------------------------------------------
'function GetSystemPriceAndSave8NoDynamic(sys_tmp_code)
'--------------------------------------------------------------------------
	'dim rs
	'GetSystemPriceAndSave8NoDynamic = "0|0|0"
	
	'if sys_tmp_code <> "" then 
	'	set rs = conn.execute("select old_price, save_price,sys_tmp_price from tb_sp_tmp where sys_tmp_code='"&sys_tmp_code&"'")		
	'	if not rs.eof then 
	'		GetSystemPriceAndSave8NoDynamic = rs(0) & "|" & rs(1)	& "|" & rs(2)			
	'	end if
	'	rs.close : set rs = nothing
	'end if
    ''GetSystemPriceAndSave8NoDynamic = GetSystemPriceAndSave(sys_tmp_code)
'end function



' ---------------------------------------------------------------------------------
function splitConfigurePrice(prices, t)
' ---------------------------------------------------------------------------------
	dim ss
	ss = split(prices, "|")
	splitConfigurePrice = ss(t)	
end function




' ---------------------------------------------------------------------------------
function GetSavePrice(sku)
' ---------------------------------------------------------------------------------
	dim rs 
	GetSavePrice = 0
	set rs = conn.execute("select product_current_discount from tb_product where product_serial_no="& SQLquote(sku) &"")
	if not rs.eof then
		GetSavePrice = GetSavePrice + cdbl(rs(0))
	end if
	rs.close :set rs = nothing
	
	GetSavePrice = cdbl(GetSavePrice)
	
end function


'
'	get part product price$
'
' ---------------------------------------------------------------------------------
function GetPartCurrentPrice(sku)
' ---------------------------------------------------------------------------------
	dim rs
	GetPartCurrentPrice = 0
	if len(sku)>0 then 
		set rs = conn.execute("select product_current_price from tb_product where product_serial_no="& SQLquote(sku) &" and tag=1")
		if not rs.eof then 
			GetPartCurrentPrice = rs(0)
		end if
		rs.close : set rs = nothing
	end if
end function




'
'	get part product price$
'
' ---------------------------------------------------------------------------------
function GetPartCurrentSell(sku)
' ---------------------------------------------------------------------------------
	dim rs
	GetPartCurrentSell = 0
	if len(sku)>0 then 
		set rs = conn.execute("select product_current_price-product_current_discount sell from tb_product where product_serial_no="& SQLquote(sku) &" and tag=1")
		if not rs.eof then 
			GetPartCurrentSell =rs(0)
		end if
		rs.close : set rs = nothing
	end if
end function



'
'	Special Cash Price
'
' ---------------------------------------------------------------------------------
function ChangeSpecialCashPrice(card_price)
' ---------------------------------------------------------------------------------
	ChangeSpecialCashPrice = 0
	if isnumeric(source_price) then
		ChangeSpecialCashPrice = formatnumber(card_price / LAYOUT_CARD_RATE, 2)
	end if
end function




'
' get configure product list
'
'
' ---------------------------------------------------------------------------------
'function GetConfigureProductIds()
'' ---------------------------------------------------------------------------------
'	dim ids, pids
'	GetConfigureProductIds = "0"
'	pids = "0"
'	ids = session("templete_system_info")
'	if ubound(ids,1) > 0 then 
'		for i = lbound(ids,1) to ubound(ids,1)
'			if ids(i, 1) <> "" then 
'				pids = pids & "," & ids(i,1)
'			end if
'		next
'	end if
'	GetConfigureProductIds = pids
'end function



' ---------------------------------------------------------------------------------
function FindPartStoreStatus_system_setting( ltd_stock)
' ---------------------------------------------------------------------------------
		if ltd_stock =1 then 
			FindPartStoreStatus_system_setting = ""
		elseif ltd_stock = 2 then 
			FindPartStoreStatus_system_setting = ""
		elseif ltd_stock =3 then 
			FindPartStoreStatus_system_setting = ""
		elseif ltd_stock = 4 then 
			FindPartStoreStatus_system_setting = ""'"<span style='color:red; font-size:8.5pt'> (Out of Stock)</span>"
		else
			FindPartStoreStatus_system_setting = ""'"<span style='color:red; font-size:8.5pt'> (Back Order)</span>"
		end if

end function



'
'	choose Photo SKu 
'
' ---------------------------------------------------------------------------------
function PartChoosePhotoSKU(current_part_sku, other_part_sku)
' ---------------------------------------------------------------------------------
	if other_part_sku = 0 or other_part_sku = "" or isnull(other_part_sku) or other_part_sku = "0" then 
		PartChoosePhotoSKU = current_part_sku
	else
		PartChoosePhotoSKU = other_part_sku
	end if
end function



' ---------------------------------------------------------------------------------
function ExportSelectControl(product_serial_no, max_quantity, part_quantity, lu_sku_selected, part_group_id, radio_id, sys_tmp_sku)
' ---------------------------------------------------------------------------------
    dim str, i
    str = ""

    if max_quantity  > 1 then 
        str = "<td><select id=""part_max_quantity_"& sys_tmp_sku &"_"&product_serial_no&""" name=""part_max_quantity"" onchange=""change_part_quantity('"& part_group_id &"', '"&product_serial_no&"', '"&radio_id&"',this, '"& sys_tmp_sku &"');"">"
        for i=1 to max_quantity 
            str = str & "<option value="""& i &""" "
            if cint(part_quantity) = i and cstr(lu_sku_selected) = cstr(product_serial_no) then str = str & " selected=""true"""
            str = str & ">"& i &"</option>"
        next
        str = str & "</select></td>"
    end if

    ExportSelectControl = str
end function



'
'	configure price
'
'
' ---------------------------------------------------------------------------------
function GetSystemConfigurePrice()
' ---------------------------------------------------------------------------------
	dim ids, pids, rs
	GetSystemConfigurePrice = "0|0|0"
	pids = "0"
	ids = session("templete_system_info")
	dim price, save_price, cost, part_quantity
	price = 0
	save_price = 0
	cost = 0
	if ubound(ids,1) > 0 then 
		for i = lbound(ids) to ubound(ids)
			if ids(i, 1) <> "" then 
				pids =  ids(i,1)
				part_quantity = ids(i, 4)
				
				if cint(pids) > 0 then 
				
					set rs = conn.execute("select product_current_price ,product_current_discount,product_current_cost from tb_product p inner join tb_product_category pc"&_
					                      " on p.menu_child_serial_no = pc.menu_child_serial_no where product_serial_no ='"& pids &"' and p.tag=1 and split_line=0 and is_non=0 and pc.tag=1")
					if not rs.eof then 
						price = price + ( cdbl(rs(0)) * cdbl(part_quantity))
						save_price = save_price +( cdbl(rs(1))  * cdbl(part_quantity))
						cost = cost + (cdbl(rs(2)) * cdbl(part_quantity))
					'response.Write( part_quantity)
					end if
					rs.close : set rs = nothing
				end if
			end if
		next	

		GetSystemConfigurePrice = price&"|"&save_price & "|"& cost

	end if
end function




'	 ' ' ' ' ' ' ' ' ' ' ' ' '  ' ' ' ' ' ' ' '  ' ' ' ' ' ' ' ' '' 
'	
'	copy configure to cart
'	
'	
' ---------------------------------------------------------------------------------
	function CopyConfigureSystemToCart(system_templete_serial_no, sys_tmp_sku,tmp_order_code, is_copy_to_cart , ip)
' ---------------------------------------------------------------------------------
		dim rs, price_sum, cost_sum, save_sum, cpu_category_id, system_name
		system_name = "System "
		cpu_category_id = 22
		price_sum = 0
		cost_sum = 0
		save_sum = 0
		dim templete_system_infos, system_product_serial_no, product_serial_no, product_order, part_group_id
		templete_system_infos =	session("templete_system_info")	
		
		if system_templete_serial_no = "" or isnull(templete_system_infos ) then 		
			response.Write("Sorry, your session has expired, please resubmit your information.")
			response.End()
		end if
'		
'		record system code history
'		
'
'		get CPU category id
'
		set rs = conn.execute("select computer_cpu_category from tb_computer_cpu")
		if not rs.eof then 
			cpu_category_id = rs(0)
		end if
		rs.close : set rs = nothing
				
		if sys_tmp_sku = "" then response.Write("Server is error. "):response.End()
		
		if session("current_custom_system_code")  = "" or isnull(session("current_custom_system_code")) then 
			conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip) values ( '"&system_templete_serial_no&"', '"&sys_tmp_sku&"', now(), '"&ip&"')")
		else
			conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip, old_system_code) values ( '"&system_templete_serial_no&"', '"&sys_tmp_sku&"', now(), '"&ip&"', '"&session("current_custom_system_code")&"')")
		end if

'		
'		copy detail
'			
		'dim begin, endtimer
		'begin = timer
		dim part_quantity, part_max_quantity
		part_max_quantity = 0
		part_quantity = 0
		for i=lbound(templete_system_infos,1) to ubound(templete_system_infos,1)
		
			system_product_serial_no =  templete_system_infos(i,0)
			product_serial_no = templete_system_infos(i,1)
			part_group_id = templete_system_infos(i,2)
			product_order = templete_system_infos(i,3)
			part_quantity = templete_system_infos(i,4)
			part_max_quantity = templete_system_infos(i,5)
			'response.Write(product_serial_no & "<br>")
			if system_product_serial_no <> "" and product_serial_no <> "" then 
				
				if (product_serial_no <> "") and isnumeric(product_serial_no) then 
					set rs = conn.execute("select  p.product_current_price, p.product_current_cost, p.product_current_discount, (product_current_price - product_current_discount) product_current_sold, pc.menu_pre_serial_no,p.menu_child_serial_no, p.product_short_name, p.product_name from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where product_serial_no='"&product_serial_no&"' and p.tag=1 and (pc.tag=1 or p.manufacturer_part_number like '%Overclocked%')")
					if not rs.eof then 
					'Response.write sys_tmp_sku & "<br>"
					conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost,product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity, product_name,cate_name) values "&_
					" ( '"&sys_tmp_sku&"', '"&product_serial_no&"', '"& convertDecimal(rs("product_current_price")) &"','"& ConvertDecimal(rs("product_current_cost")) &"', '"& product_order &"','"& system_templete_serial_no &"','"& system_product_serial_no &"', '"& part_group_id &"', '"& ConvertDecimal(rs("product_current_discount")) &"','"& ConvertDecimal(rs("product_current_price")) &"','"& ConvertDecimal(rs("product_current_price")) &"', '"& ConvertDecimal(rs("product_current_sold")) &"', '"&part_quantity&"', '"&part_max_quantity&"' , "& SQLquote(rs("product_name"))&", '')")	
					
					price_sum = price_sum + ConvertDecimal(cdbl(rs("product_current_price")) * cdbl(part_quantity))
					cost_sum = cost_sum + ConvertDecimal(cdbl(rs("product_current_cost")) * cdbl(part_quantity))
					save_sum = save_sum + ConvertDecimal(cdbl(rs("product_current_discount")) * cdbl(part_quantity))
					
						if cstr(cpu_category_id) = cstr(rs("menu_child_serial_no")) or cstr(cpu_category_id) = cstr(rs("menu_pre_serial_no")) then 
							system_name = rs("product_short_name") & " System"
						end if
					end if
	
					rs.close : set rs = nothing
				end if
			end if
			
			Conn.Execute("Update tb_sp_tmp_detail sp, tb_part_group pg "&_
						" Set cate_name=pg.part_group_name "&_
						" where pg.part_group_id=sp.part_group_id and sp.sys_tmp_code="& SQLquote(sys_tmp_sku))
			
		next
		'endtimer = timer
		'response.Write(endtimer-begin )
		'response.Write("<br>")
		'begin = timer

		'response.Write(system_name)
		dim  current_price, current_price_rate, now_low_price, price_and_save	, save_price, cost	
		
		current_price_rate = price_sum
		save_price = save_sum
		current_price = cdbl(current_price_rate) - cdbl(save_price)
		cost = cost_sum
		
		conn.execute("insert into tb_sp_tmp(sys_tmp_code, sys_tmp_price, create_datetime, tag, ip,  system_templete_serial_no, email, is_noebook, sys_tmp_cost, sys_tmp_product_name, save_price, old_price, syst_tmp_price_rate, is_customize) values "&_
		"( '"& sys_tmp_sku &"', '"& current_price &"', now(), 1, '"& ip&"', '"& system_templete_serial_no &"', '"&session("email")&"', 0, '"& cost & "', '"& system_name &"', '"& save_price & "', '"& current_price_rate &"', '"& current_price_rate &"' ,1)")
		'endtimer = timer
		'response.Write(endtimer-begin )
		'response.Write("<br>")
' 
' 		copy to cart
' 
' 
		dim customer_serial_no	
		if "" <> request.Cookies("customer_serial_no") then 
			customer_serial_no	=request.Cookies("customer_serial_no")
		else
			customer_serial_no = 0
		end if
		
		if is_copy_to_cart then 
			conn.execute("insert into tb_cart_temp(cart_temp_code, product_serial_no, menu_child_serial_no, create_datetime, ip, customer, cart_temp_Quantity, customer_serial_no, shipping_company, state_shipping, is_noebook, price, price_rate, product_name, cost, price_unit, current_system) "&_
			" select '"& tmp_order_code &"', '"& sys_tmp_sku &"', '"& system_templete_serial_no &"', now(), '"&ip&"', '"& customer_serial_no &"', 1, '"&customer_serial_no&"', '"& GetShippingCompany(tmp_order_code) &"','"& GetStateShipping(tmp_order_code)&"', 0, sys_tmp_price, sys_tmp_price, sys_tmp_product_name, sys_tmp_cost, "& SQLquote(CCUN) &","& SQLquote(Current_System) &" from tb_sp_tmp where sys_tmp_code='"&sys_tmp_sku&"'")
		end if
	
end function
	
' ---------------------------------------------------------------------------------
function ChangeTempOrderSystemPrice(tmp_order_code)
    dim rs, price_save_cost,save_cost,price
    set rs = conn.execute("Select product_serial_no from tb_cart_temp where cart_temp_code ='"&tmp_order_code&"' ")
    if not rs.eof then
        do while not rs.eof 
            if len(trim(rs("product_serial_no"))) = LAYOUT_SYSTEM_CODE_LENGTH then 
                    
                    conn.execute("update tb_sp_tmp_detail sd, tb_product p set "&_
" sd.product_current_price = p.product_current_price "&_
", sd.product_current_cost = p.product_current_price "&_
", sd.save_price = p.product_current_price "&_
", sd.old_price = p.product_current_price "&_
", sd.product_current_price_rate = p.product_current_price "&_
", sd.product_current_sold = p.product_current_price-p.product_current_discount where sd.product_serial_no = p.product_serial_no and sd.sys_tmp_code='"&rs("product_serial_no")&"'")
                    
                    price_save_cost = GetSystemPriceAndSave(rs("product_serial_no"))
					save_cost 		= splitConfigurePrice(price_save_cost, 1)
					price 			= splitConfigurePrice(price_save_cost, 0)
					'cost = splitConfigurePrice(price_save_cost, 2 )
					
					conn.execute("Update tb_sp_tmp set sys_tmp_price='"&cdbl(formatnumber(price-save_cost, 2))&"', save_price='"&cdbl(formatnumber(save_cost,2))&"', old_price='"&cdbl(formatnumber(price, 2))&"', syst_tmp_price_rate='"&cdbl(formatnumber(price, 2))&"', price_unit="& SQLquote(CCUN)&" where sys_tmp_code='"&rs("product_serial_no")&"';")
                  
                    conn.execute("update tb_cart_temp set price='"&cdbl(formatnumber(price-save_cost, 2))&"',price_rate='"&cdbl(formatnumber(price-save_cost, 2))&"',cost='0',price_unit="&SQLquote(CCUN)&" where cart_temp_code ='"&tmp_order_code&"' and product_serial_no='"& rs("product_serial_no") &"'")	
					
            end if
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing

end function

' ---------------------------------------------------------------------------------



' ---------------------------------------------------------------------------------
function sysAddToSpTmp(system_sku, sys_tmp_sku, tmp_order_code, is_to_cart)
' ---------------------------------------------------------------------------------
	dim crs, child, price, cost, save_cost, one_price, one_cost, one_save_cost, current_price, old_price_sum, single_sold
	dim ont_price_rate
	ont_price_rate = 0
	price = 0
	cost = 0
	
	save_cost =0
	
	
	
	if len(system_sku)>0 then 
		'8
		
		if len(system_sku) <> LAYOUT_SYSTEM_CODE_LENGTH then 		
			set rs = conn.execute("select id, st.ebay_system_name as sys_tmp_product_name ,'0' as is_noebook from tb_ebay_system st where st.id="& SQLquote(system_sku))
		else
			set rs = conn.execute("select *,0 as is_noebook from tb_sp_tmp  where sys_tmp_code="& SQLquote(system_sku))
		end if
		
		if not rs.eof then 

					'ddddd= timer()
					'
					'
					' copy system_code to Store
					'
					if len(system_sku) <> LAYOUT_SYSTEM_CODE_LENGTH then 
						
						conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip) values ( '"&system_sku&"', '"&sys_tmp_sku&"', now(), '"&LAYOUT_HOST_IP&"')")
					else
						conn.execute("insert into tb_system_code_store ( system_templete_serial_no, system_code, create_datetime, ip, old_system_code) values ( '"&system_sku&"', '"&sys_tmp_sku&"', now(), '"&LAYOUT_HOST_IP&"', '"&system_sku&"')")
					end if					
					'
					'
					'
					'	copy system detial 
					'
					if len(system_sku) <> LAYOUT_SYSTEM_CODE_LENGTH then 
						conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost, "&_
" product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, re_sys_tmp_detail, "&_
" old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity, product_name, cate_name) "&_
" select '"&sys_tmp_sku&"', p.product_serial_no,p.product_current_price,p.product_current_cost , "&_
" sp.id ,system_sku, sp.id, sp.part_group_id , p.product_current_discount,'-1', "&_
" p.product_current_price,p.product_current_price, p.product_current_price-p.product_current_discount, part_quantity, max_quantity "&_
" ,p.product_name"&_
" ,pg.part_group_name"&_
" from tb_ebay_system_parts sp "&_
" inner join tb_part_group pg on pg.part_group_id=sp.part_group_id "&_
" inner join tb_product p on sp.luc_sku=p.product_serial_no where p.tag=1 and system_sku='"&system_sku&"'")
					else
					
						if Current_System = CSCA then 
							conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost,"&_
" product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, re_sys_tmp_detail, "&_
" old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity, product_name, cate_name) "&_
" select '"&sys_tmp_sku&"',  p.product_serial_no,p.product_current_price,p.product_current_cost ,sp.product_order ,sp.sys_tmp_code , sp.system_product_serial_no, "&_
" sp.part_group_id,p.product_current_discount,sp.sys_tmp_detail,p.product_current_price,p.product_current_price,p.product_current_price-p.product_current_discount, part_quantity, part_max_quantity"&_
", sp.product_name"&_
", sp.cate_name"&_
" from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no "&_
" where p.tag=1 and sys_tmp_code='"&system_sku&"'")
						else
							conn.execute("insert into tb_sp_tmp_detail(sys_tmp_code,product_serial_no,product_current_price,product_current_cost,"&_
" product_order,system_templete_serial_no, system_product_serial_no, part_group_id, save_price, re_sys_tmp_detail, "&_
" old_price, product_current_price_rate,product_current_sold, part_quantity, part_max_quantity, product_name) "&_
" select '"&sys_tmp_sku&"'"&_
" ,p.product_serial_no"&_
" ,round(p.product_current_price* "& getCurrencyConvert() &", 2)"&_
" ,round(p.product_current_cost*"& getCurrencyConvert() &", 2) "&_
" ,sp.product_order "&_
" ,sp.sys_tmp_code "&_
" ,sp.system_product_serial_no "&_
" ,sp.part_group_id"&_
" ,round(p.product_current_discount*"&getCurrencyConvert()&", 2)"&_
" ,sp.sys_tmp_detail"&_
" ,round(p.product_current_price*"&getCurrencyConvert()&", 2)"&_
" ,round(p.product_current_price*"&getCurrencyConvert()&", 2)"&_
" ,round(p.product_current_price-p.product_current_discount*"&getCurrencyConvert()&", 2)"&_
" ,part_quantity"&_
" ,part_max_quantity"&_
" ,sp.product_name"&_
" from tb_sp_tmp_detail sp inner join tb_product p on sp.product_serial_no=p.product_serial_no "&_
" where p.tag=1 and sys_tmp_code='"&system_sku&"'")
						end if
					end if
					
					dim price_save_cost
					price_save_cost = GetSystemPriceAndSave(system_sku)
					save_cost 		= splitConfigurePrice(price_save_cost, 1 )
					price 			= splitConfigurePrice(price_save_cost, 0 )
					'cost = splitConfigurePrice(price_save_cost, 2 )
					
					Dim is_customize
					if len(system_sku) <> LAYOUT_SYSTEM_CODE_LENGTH then 
						is_customize	=	0
					else
						is_customize	=	1
					End if
					conn.execute("insert into tb_sp_tmp(is_customize,sys_tmp_code,sys_tmp_price,create_datetime,tag,ip,system_templete_serial_no,email,"&_
					"is_noebook,sys_tmp_product_name,save_price,old_price,syst_tmp_price_rate, price_unit) values "&_
					"( "& SQLquote(is_customize) &", '"&sys_tmp_sku&"','"&cdbl(formatnumber(price-save_cost, 2))&"',now(),'1','"&LAYOUT_HOST_IP&"','"&system_sku&"','"&session("email")&"','"&rs("is_noebook")&"','"&rs("sys_tmp_product_name")&"','"&cdbl(formatnumber(save_cost,2))&"','"&cdbl(formatnumber(price, 2))&"','"&cdbl(formatnumber(price, 2))&"', "& SQLquote(CCUN)&")")					

					if is_to_cart then 
					if LAYOUT_CCID = "" then LAYOUT_CCID = -1
					
						conn.execute("insert into tb_cart_temp("&_
									" cart_temp_code"&_
									" ,product_serial_no"&_
									" , menu_child_serial_no"&_
									" , create_datetime"&_
									" , ip"&_
									" , cart_temp_Quantity"&_
									" , customer_serial_no"&_
									" , shipping_company"&_
									" , state_shipping"&_
									" , is_noebook"&_
									" , price"&_
									", price_rate"&_
									", product_name"&_
									", cost"&_
									", price_unit"&_
									", current_system) "&_
									" select '"& tmp_order_code &"'"&_
									", '"& sys_tmp_sku &"'"&_
									", '"& system_sku &"'"&_
									", now()"&_
									", '"&LAYOUT_HOST_IP&"'"&_
									", 1"&_
									", '"&LAYOUT_CCID&"'"&_
									", '"& GetShippingCompany(tmp_order_code) &"'"&_
									",'"& GetStateShipping(tmp_order_code)&"'"&_
									", 0"&_
									", sys_tmp_price"&_
									", sys_tmp_price"&_
									", sys_tmp_product_name"&_
									", sys_tmp_cost"&_
									", "& SQLquote(CCUN) &""&_
									","& SQLquote(Current_System) &""&_
									" from tb_sp_tmp where sys_tmp_code='"&sys_tmp_sku&"'")

					End if					
		end if
		rs.close:set rs = nothing
	end if
end function
		
	
	
'
'	Find system temp name
'
function GetSystemNameFromSpTmp(id)
	Dim grs
	GetSystemNameFromSpTmp = ""
	'response.Write(id &"DDD")
	if id<> "" then 
		'Response.write "select sys_tmp_product_name from tb_sp_tmp where sys_tmp_code='"&id&"'"
		set grs = conn.execute("select sys_tmp_product_name from tb_sp_tmp where sys_tmp_code='"&id&"'")
		if not grs.eof then 
			GetSystemNameFromSpTmp = grs(0)
		end if	
		grs.close : set grs = nothing
	end if
end function



' ---------------------------------------------------------------------------------
function FindSpecialCashPriceComment()
' ---------------------------------------------------------------------------------
	FindSpecialCashPriceComment = "SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash,Interact,bank transfer,money order, etc.  Cash price does not waive sales taxes if applicable."
end function




' ---------------------------------------------------------------------------------
function GetShippingCompany(tmp_code)
' ---------------------------------------------------------------------------------
	if tmp_code <> "" then 
		set grs = conn.execute("select max(shipping_company) from tb_cart_temp where cart_temp_code="&SQLquote(tmp_code))
		if not grs.eof then 
			GetShippingCompany = grs(0)
		else
			GetShippingCompany = -1
		end if
		grs.close : set grs = nothing
	else
		GetShippingCompany = -1
	end if
	if isnull(GetShippingCompany) then GetShippingCompany = -1
end function


' ---------------------------------------------------------------------------------
function GetOrderCountryCodeAndStateIDAndShipingCompanyID(order_code)
' ---------------------------------------------------------------------------------
	GetOrderCountryCodeAndStateIDAndShipingCompanyID = "0|0|0"
	Dim rs
	if SQLescape(order_code) <> "" then 
		set rs = conn.execute("select max(shipping_company) shipping_company, max(shipping_state_code) shipping_state_code, max(shipping_country_code)  shipping_country_code from tb_cart_temp where cart_temp_code="&SQLquote(order_code))
		if not rs.eof then
				GetOrderCountryCodeAndStateIDAndShipingCompanyID = rs("shipping_country_code") &"|"& rs("shipping_state_code") &"|"& rs("shipping_company")
		end if
		rs.close : set rs = nothing	
	end if
End function


' ---------------------------------------------------------------------------------
function GetStateShipping(tmp_code)
' ---------------------------------------------------------------------------------
	if tmp_code <> "" then
		set grs = conn.execute("select max(state_shipping) from tb_cart_temp where cart_temp_code="&SQLquote(tmp_code))
		if not grs.eof then 
			GetStateShipping = grs(0)
		else
			GetStateShipping = -1
		end if
		grs.close : set grs = nothing
	else
		GetStateShipping = -1
	end if
	if isnull(GetStateShipping) then GetStateShipping = -1
end function



' ---------------------------------------------------------------------------------
function GetSystemPhotoByID2(id)
' ---------------------------------------------------------------------------------

	dim case_sku , product_image_url, product_image_1, product_image_1_g, casers
	case_sku = 0

	set casers = conn.execute("select es.luc_sku from tb_ebay_system_part_comment ec inner join  tb_ebay_system_parts es on ec.id=es.comment_id where is_case = 1 and system_sku="&id&" ")
	
	if not casers.eof then
		case_sku = casers(0)
	
	end if
	casers.close : set casers = nothing
	' ?

	GetSystemPhotoByID2 = GetImgCaseMiddle(HTTP_PART_GALLERY, case_sku )
end function




' ---------------------------------------------------------------------------------
function setViewCount(is_part, ip, id)
' ---------------------------------------------------------------------------------
    dim rs
    if(is_part)then    
        conn.execute("update tb_product set view_count=view_count+1 where product_serial_no='"& id &"' and 0=(select count(track_id) from tb_track where track_ip='"& ip &"')")
        set rs = conn.execute("select id from tb_part_cate_view_count where luc_sku = '"& id &"' and date_format(now(),'%Y%b%d')=date_format(regdate,'%Y%b%d')")
        if not rs.eof then
           ' response.write ("update tb_part_cate_view_count set  view_count=view_count+1 where id='"& rs(0) &"' and 0=(select count(*) from tb_track where track_ip='"& ip &"'")
            conn.execute("update tb_part_cate_view_count set  view_count=view_count+1 where id='"& rs(0) &"' and 0=(select count(track_id) from tb_track where track_ip='"& ip &"')")
        else
            conn.execute("Insert into tb_part_cate_view_count(luc_sku, view_count, regdate) values ('"& id &"', 1, now())")
        end if
        rs.close : set rs = nothing
    else   
        conn.execute("update tb_product_category set view_count=view_count+1 where menu_child_serial_no='"& id &"' and 0=(select count(track_id) from tb_track where track_ip='"& ip &"')")
        
        set rs = conn.execute("select id from tb_part_cate_view_count where category_id = '"& id &"' and date_format(now(),'%Y%b%d')=date_format(regdate,'%Y%b%d')")
        if not rs.eof then
            conn.execute("update tb_part_cate_view_count set  view_count=view_count+1 where id='"& rs(0) &"' and 0=(select count(track_id) from tb_track where track_ip='"& ip &"')")
        else
            conn.execute("Insert into tb_part_cate_view_count(category_id, view_count, regdate) values ('"& id &"', 1, now())")
        end if
        rs.close : set rs = nothing
    end if
end function



' ---------------------------------------------------------------------------------
function FindOnSaleSingle(product_id)
' ---------------------------------------------------------------------------------
	FindOnSaleSingle = "select os.*,os.save_price save_cost, p.menu_child_serial_no, pc.menu_pre_serial_no,p.product_short_name, p.product_name, p.product_current_price from tb_on_sale os inner join tb_product p on p.product_serial_no=os.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where (date_format(now(),'%Y%j') between date_format(os.begin_datetime,'%Y%j') and date_format(os.end_datetime,'%Y%j')) and p.tag=1 and pc.tag=1 and p.product_serial_no='"&product_id&"'"
end function



' ---------------------------------------------------------------------------------
function sql_sale_promotion_rebate_sign(product_id )
' ---------------------------------------------------------------------------------
	sql_sale_promotion_rebate_sign = "select p.producter_serial_no, pc.menu_child_serial_no, pc.menu_pre_serial_no,p.product_short_name, p.product_name, sp.*, p.product_current_price from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.product_serial_no="& SQLquote(product_id)&" and sp.show_it=1 and sp.promotion_or_rebate=2 and (date_format(now(),'%Y%j') between date_format(sp.begin_datetime,'%Y%j') and date_format(sp.end_datetime,'%Y%j')) order by sp.sale_promotion_serial_no desc limit 0,1"
end function



' ---------------------------------------------------------------------------------
function sql_sale_promotion_rebate_all()
' ---------------------------------------------------------------------------------
	sql_sale_promotion_rebate_all =  "select distinct p.product_serial_no from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where pc.tag=1 and p.tag=1 and sp.show_it=1 and (date_format(now(),'%Y%j') between date_format(sp.begin_datetime,'%Y%j') and date_format(sp.end_datetime,'%Y%j')) and promotion_or_rebate=2"
	
		sql_sale_promotion_rebate_all = sql_sale_promotion_rebate_all & " order by p.producter_serial_no,p.product_order asc"

end function



' ---------------------------------------------------------------------------------
function GetPartCommentFile(sku)
' ---------------------------------------------------------------------------------
	GetPartCommentFile = "/part_comment/" & sku & "_comment.html"

end function




'
'	send email
'

' ---------------------------------------------------------------------------------
Function SendEmail(title, body, recipient)
' ---------------------------------------------------------------------------------
		Dim return
		Dim serverUserName :serverUserName = "xiaowu021@126.com"
		Dim pwd				:	pwd			=	"1234qwer"
		Dim ServerName		:	ServerName	=	"smtp.126.com"
		
		set jmail=server.CreateObject ("JMAIL.SMTPMail")
		
		IF LAYOUT_RELEASE THEN 
			serverUserName	=	"sales@lucomputers.com"
			pwd				=	"5calls2day"
			ServerName		=	"p3smtpout.secureserver.net"
		End If
		'''jmail.Logging 	= true
		'jmail.Silent		=	false
		'jmail.charset		=	"utf-8"
		'response.write recipient
		'jmail.AddRecipient(recipient)
		'jmail.AddRecipientBCC("809840415@qq.com")
		'jmail.subject 		=title
		''jmail.appendtext 	"This is a mail of HTML type"
		'jmail.body =	body
		
        'jmail.Sender 		= 	"sales@lucomputers.com"
		'jmail.ServerAddress 			= 	serverUserName
		'jmail.SenderName		= 	"LU COMPUTERS"
	
		'jmail.subject = title '& " TEST "
		''jmail.MailServerUserName =serverUserName
		''jmail.MailServerPassWord = pwd
		'jmail.execute()
		'jmail.close
		if not LAYOUT_RELEASE then 
		'	response.write (jmail.ErrorSource &"<br/>")
		'	response.write (jmail.log &"<br/>")
		'	response.write returndd
		End if
		Set jmail = Server.CreateObject("JMAIL.SMTPMail") 
			jmail.silent = true 
			jmail.logging = true 
			jmail.Charset = "utf-8" 
			jmail.ContentType = "text/html" 
			jmail.ServerAddress = "p3smtpout.secureserver.net"
			jmail.AddRecipient recipient
			jmail.SenderName = "LU COMPUTERS" 
			jmail.Sender = "sales@lucomputers.com" 
			jmail.Priority = 1 
			jmail.Subject = title
			jmail.Body = body
			jmail.AddRecipientBCC "809840415@qq.com"
			'jmail.AddRecipientCC Email ????????
			jmail.Execute() 
			jmail.Close
		set jmail=nothing
		
end Function




'
' 

' ---------------------------------------------------------------------------------
function GetHotImg(str)
' ---------------------------------------------------------------------------------
	if (str = 1) then 
		response.write "&nbsp;<img src='/soft_img/app/hot.gif' border='0'/>"
	end if
end function 
'
'
'
' 
'
' ---------------------------------------------------------------------------------
function GetRebateImg(str)
' ---------------------------------------------------------------------------------
	'if (str = 1) then 
		response.write "&nbsp;<img src='/soft_img/app/rebate.gif' border='0' onclick='window.location.href=""/site/p_rebate.asp#"& str &"""'   style=""cursor:pointer""/>"
	'end if
end function 
'
'
'
' 
'
' ---------------------------------------------------------------------------------
function GetNewImg(str)
' ---------------------------------------------------------------------------------
	if (str = 1) then 
		response.write "&nbsp;<img src='/soft_img/app/new.gif' border='0'/>"
	end if
end function 
'
'
'
' 
'
' ---------------------------------------------------------------------------------
function GetSaleImg(str)
' ---------------------------------------------------------------------------------
	if (str = 1) then 
		response.write "&nbsp;<img src='/soft_img/app/Sale.gif' border='0'/>"
	end if
end function 




' ---------------------------------------------------------------------------------
function FindPartStoreStatus2(id, ltd_stock)
' ---------------------------------------------------------------------------------
dim rs, sum, is_noebook
	is_noebook = false
	set rs = conn.execute("select ifnull(is_display_stock, 0) from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where product_serial_no="&SQLquote(id))
	if not rs.eof then
		if rs(0) = 1 then 
			is_display = true
		end if
	end if
	rs.close :set rs = nothing
	
	FindPartStoreStatus2 = ""

	if is_display then 

		if ltd_stock =1 then 
			FindPartStoreStatus2 = "<span style='color:green; font-size:8.5pt'>In Stock</span>"
		elseif ltd_stock = 2 then 
			FindPartStoreStatus2 = "<span style='color:green; font-size:8.5pt'>Stock Available</span>"
		elseif ltd_stock =3 then 
			FindPartStoreStatus2 = "<span style='color:green; font-size:8.5pt'>Stock Low(Call)</span>"
		elseif ltd_stock = 4 then 
			FindPartStoreStatus2 = "<span style='color:#B9C7B9; font-size:8.5pt'>Out of Stock</span>"
		else
			FindPartStoreStatus2 ="<span style='color:#B9C7B9; font-size:8.5pt'>Back Order</span>"
		end if
	end if
end function

' ---------------------------------------------------------------------------------
function FindPartStoreStatus3(ltd_stock)
' ---------------------------------------------------------------------------------
    dim rs, sum, is_noebook
	
	FindPartStoreStatus3 = ""

	if ltd_stock =1 then 
		FindPartStoreStatus3 = "<span style='color:green; font-size:8.5pt'>In Stock</span>"
	elseif ltd_stock = 2 then 
		FindPartStoreStatus3 = "<span style='color:green; font-size:8.5pt'>Stock Available</span>"
	elseif ltd_stock =3 then 
		FindPartStoreStatus3 = "<span style='color:green; font-size:8.5pt'>Stock Low(Call)</span>"
	elseif ltd_stock = 4 then 
		FindPartStoreStatus3 = "<span style='color:#B9C7B9; font-size:8.5pt'>Out of Stock</span>"
	else
		FindPartStoreStatus3 ="<span style='color:#B9C7B9; font-size:8.5pt'>Back Order</span>"
	end if

end function
	
'
' Get category name 
'
function getMenuChildName(id)
	getMenuChildName = ""
	Dim mcn
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no="&SQLquote(id))
		if not mcn.eof then
			getMenuChildName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if

end function



' ---------------------------------------------------------------------------------
function ResponseStockStatus(id)
' ---------------------------------------------------------------------------------


    ResponseStockStatus = ""
    if id = 1 then 
        ResponseStockStatus = "This item can be shipped/picked up within 24 hours.  Shipping is available: Mon-Fri only."
    elseif id = 2 then 
        ResponseStockStatus = "This item is available within 24-48 hours.  If you place an order before 12 AM we may ship your order the same day (Mon-Fri only)."
    elseif id = 3 then 
        ResponseStockStatus = "If this item is still stock available we will ship within 24-48 hours. Please call to verify."
    elseif id = 4 then 
        ResponseStockStatus = "This product is active, but it takes longer than 2 days to be available for shipping or pick up."
    elseif id = 5 then 
        ResponseStockStatus = "This is a new product or product on back order.  You may place your order now.  Price will be updated when its available."
    end if
end function



' ---------------------------------------------------------------------------------
function GetPartOnSaleSavePrice(sku)
' ---------------------------------------------------------------------------------
	Dim rs
	GetPartOnSaleSavePrice	=	0
	set rs = conn.execute(FindOnSaleSingle(sku))
	
	if not rs.eof then
		GetPartOnSaleSavePrice = cdbl(rs("save_cost"))
	end if
	rs.close : set rs = nothing

End function



' ---------------------------------------------------------------------------------
function GetStateIdByCode(code)
' ---------------------------------------------------------------------------------
	Dim rs
	GetStateIdByCode=-1
	if SQLescape(code)<> "" then
		Set rs = conn.execute("select state_serial_no from tb_state_shipping where state_code="& SQLquote(code))
		if not rs.eof then 
			GetStateIdByCode = rs(0)
		end if
		rs.close : set rs = nothing
	end if

End function



' ---------------------------------------------------------------------------------
function CustomerSendMsg(order_code, content)
' ---------------------------------------------------------------------------------
	dim rs
	if len(trim(content)) > 0 then 
		set rs = server.CreateObject("adodb.recordset")
		rs.open "select * from tb_chat_msg ",conn,1,3
		rs.addnew
		rs("msg_order_code") = order_code
		rs("msg_content_text") = content
		rs("msg_type") = 1
		rs("msg_author") = "Me"
		rs("regdate") = now()
		rs.update
		rs.close : set rs = nothing
	end if

end function



' ---------------------------------------------------------------------------------
function GetPayMethodNew(id)
' ---------------------------------------------------------------------------------
	dim rs 
	GetPayMethodNew = ""
	if len(SQLescape(id))>0 then
		set rs = conn.execute("select pay_method_name from tb_pay_method_new where pay_method_serial_no="& SQLquote(id) )
		if not rs.eof then
			do while not rs.eof 
				GetPayMethodNew = rs(0)
			rs.movenext
			loop
		end if		
		rs.close : set rs = nothing	
	end if
end function



' ---------------------------------------------------------------------------------
Function CopyCartToOrder(order_code, customerID, cmd)
' ---------------------------------------------------------------------------------

	Dim rs
	Dim prs
	
	'
	' use paypal paymethod. if it is ok,then order end.
	'
	set rs = conn.execute("select is_ok from tb_order_helper where order_code='"&order_code&"'")
	if not rs.eof then
		if rs(0) = 1 then
			response.Redirect("shopping_cheOut_order_ok.asp")			
			response.end
		end if
		
	end if
	rs.close 
	
	'
	' save shipping state country
	'
	Conn.execute("Update tb_customer Set shipping_state_code="& SQLquote(LAYOUT_SHIPPING_STATE_CODE) &",shipping_country_code="& SQLquote(LAYOUT_SHIPPING_COUNTRY_CODE)&" WHere customer_serial_no="& SQLquote(customerID))
	
	dim order_sub_total , order_cost, state_shipping, shipping_company, cus_note, copy_pay_method
	
	order_sub_total = 0
	order_cost = 0
	
	
	dim first_name, last_name ,pay_method,shipping_phone,shipping_address,billing_address,billing_phone, email
	
	if Order_Code <> "" then 

		conn.execute("delete from tb_customer_store where order_code='"&order_code&"'")
		Conn.Execute("insert into tb_customer_store "&_
						" (zip_code, customer_serial_no, customer_login_name, customer_business_telephone,  "&_
						" phone_d,  "&_
						" phone_n,  "&_
						" customer_address1,  "&_
						" customer_city,  "&_
						" customer_country,  "&_
						" customer_email1,  "&_
						" customer_email2,  "&_
						" customer_credit_card,  "&_
						" customer_expiry,  "&_
						" customer_company,  "&_
						" customer_fax,  "&_
						" customer_note,  "&_
						" customer_password,  "&_
						" state_serial_no,  "&_
						" tag,  "&_
						" system_category_serial_no,  "&_
						" customer_first_name,  "&_
						" customer_last_name,  "&_
						" customer_rumor,  "&_
						" customer_card_type,  "&_
						" customer_card_phone,  "&_
						" EBay_ID,  "&_
						" news_latter_subscribe,  "&_
						" create_datetime,  "&_
						" customer_card_issuer,  "&_
						" customer_card_billing_shipping_address,  "&_
						" customer_card_city,  "&_
						" customer_card_state,  "&_
						" customer_card_zip_code,  "&_
						" pay_method,  "&_
						" customer_shipping_city,  "&_
						" customer_shipping_state,  "&_
						" customer_shipping_address,  "&_
						" shipping_state_code,  "&_
						" shipping_country_code,  "&_
						" customer_card_country,  "&_
						" my_purchase_order,  "&_
						" customer_shipping_first_name,  "&_
						" customer_shipping_last_name,  "&_
						" customer_shipping_country,  "&_
						" customer_shipping_zip_code,  "&_
						" tax_execmtion,  "&_
						" busniess_website,  "&_
						" phone_c,  "&_
						" is_old,  "&_
						" card_verification_number,  "&_
						" source, "&_
						" order_code, "&_
						" store_create_datetime "&_
						" ,customer_card_first_name"&_
						" ,customer_card_last_name"&_
						" ,customer_card_state_code"&_
						" ,customer_card_country_code"&_
						" ,customer_business_country_code"&_
						" ,customer_business_state_code"&_
						" ,customer_business_city"&_
						" ,customer_business_zip_code"&_
						" ,customer_business_address"&_
						" ,customer_country_code"&_
                        " ,is_all_tax_execmtion"&_
						" ) "&_

						" Select zip_code, customer_serial_no, customer_login_name, customer_business_telephone,  "&_
						" phone_d,  "&_
						" phone_n,  "&_
						" customer_address1,  "&_
						" customer_city,  "&_
						" customer_country,  "&_
						" customer_email1,  "&_
						" customer_email2,  "&_
						" customer_credit_card,  "&_
						" customer_expiry,  "&_
						" customer_company,  "&_
						" customer_fax,  "&_
						" customer_note,  "&_
						" customer_password,  "&_
						" state_serial_no,  "&_
						" tag,  "&_
						" system_category_serial_no,  "&_
						" customer_first_name,  "&_
						" customer_last_name,  "&_
						" customer_rumor,  "&_
						" customer_card_type,  "&_
						" customer_card_phone,  "&_
						" EBay_ID,  "&_
						" news_latter_subscribe,  "&_
						" create_datetime,  "&_
						" customer_card_issuer,  "&_
						" customer_card_billing_shipping_address,  "&_
						" customer_card_city,  "&_
						" customer_card_state,  "&_
						" customer_card_zip_code,  "&_
						" pay_method,  "&_
						" customer_shipping_city,  "&_
						" customer_shipping_state,  "&_
						" customer_shipping_address,  "&_
						" shipping_state_code,  "&_
						" shipping_country_code,  "&_
						" customer_card_country,  "&_
						" my_purchase_order,  "&_
						" customer_shipping_first_name,  "&_
						" customer_shipping_last_name,  "&_
						" customer_shipping_country,  "&_
						" customer_shipping_zip_code,  "&_
						" tax_execmtion,  "&_
						" busniess_website,  "&_
						" phone_c,  "&_
						" is_old,  "&_
						" card_verification_number,  "&_
						" source, "&_
						" "& order_code &"," &_
						"  now()"&_
						" ,customer_card_first_name"&_
						" ,customer_card_last_name"&_
						" ,customer_card_state_code"&_
						" ,customer_card_country_code"&_
						" ,customer_business_country_code"&_
						" ,customer_business_state_code"&_
						" ,customer_business_city"&_
						" ,customer_business_zip_code"&_
						" ,customer_business_address"&_
						" ,customer_country_code"&_
                        " ,is_all_tax_execmtion"&_
						" from tb_customer where customer_serial_no="& SQLquote(customerID))
		
		

		dim shipping_company_v, save_price_total
		dim prick_up_datetime1_var, prick_up_datetime2_var
		save_price_total = 0
		shipping_company_v=-1
		order_cost = 0
	
		Dim current_system, price_unit
		
		set rs = conn.execute("select shipping_company "&_
								" ,date_format(pick_datetime_1, '%b-%d-%Y') pick_datetime_1 "&_
								" ,date_format(pick_datetime_2, '%b-%d-%Y') pick_datetime_2 "&_
								" ,state_shipping"&_
								" ,shipping_company"&_
								" ,cost"&_
								" ,product_name"&_
								" ,old_price"&_
								" ,price"&_
								" ,price_rate"&_
								" ,product_serial_no"&_
								" ,cart_temp_Quantity"&_
								" ,save_price"&_
								" ,menu_child_serial_no"&_
								" ,price_rate"&_
								" ,is_noebook"&_
								" ,pay_method"&_
								" ,price_unit"&_
								" ,current_system"&_
                                " ,prodType "&_
								" from tb_cart_temp where cart_temp_code="& SQLquote(Order_Code))
		if not rs.eof then
			 
			conn.execute("delete  from tb_order_product where order_code='"&Order_Code&"'")
			copy_pay_method = 	rs("pay_method")
			current_system 	=	rs("current_system")
			price_unit		=	rs("price_unit")
			do while not rs.eof 	
				shipping_company_v 		= rs("shipping_company")
               ' response.Write rs("pick_datetime_1")
				prick_up_datetime1_var 	= rs("pick_datetime_1")
				prick_up_datetime2_var 	= rs("pick_datetime_2")

				set crs = server.createobject("adodb.recordset")
				crs.open "select * from tb_order_product ", conn,1,3
				crs.addnew 
					state_shipping 		= rs("state_shipping")
					shipping_company 	= rs("shipping_company")
				
					if len(trim(rs("product_serial_no"))) = 8 then 
							crs("order_product_cost") 	= rs("cost")
							crs("product_name") 		= rs("product_name") 
							crs("product_type") 		= 2
							crs("old_price")			= rs("old_price")
							
							'
							' copy part detail to store
							'
							Conn.Execute("Delete from tb_order_product_sys_detail Where sys_tmp_code="& SQLquote(rs("product_serial_no")))
							Conn.Execute("insert into tb_order_product_sys_detail "&_
										" ( sys_tmp_code, product_serial_no, product_name, "&_
										" cate_name, "&_
										" part_quantity, "&_
										" part_max_quantity, "&_
										" product_current_price, "&_
										" product_current_cost, "&_
										" product_order, "&_
										" system_templete_serial_no, "&_
										" system_product_serial_no, "&_
										" part_group_id, "&_
										" save_price, "&_
										" old_price, "&_
										" re_sys_tmp_detail, "&_
										" product_current_price_rate, "&_
										" product_current_sold, "&_
										" is_lock, "&_
										" ebay_number"&_
										" )"&_
	
										" select sys_tmp_code, product_serial_no, product_name, "&_
										" cate_name, "&_
										" part_quantity, "&_
										" part_max_quantity, "&_
										" product_current_price, "&_
										" product_current_cost, "&_
										" product_order, "&_
										" system_templete_serial_no, "&_
										" system_product_serial_no, "&_
										" part_group_id, "&_
										" save_price, "&_
										" old_price, "&_
										" re_sys_tmp_detail, "&_
										" product_current_price_rate, "&_
										" product_current_sold, "&_
										" is_lock, "&_
										" ebay_number from tb_sp_tmp_detail Where sys_tmp_code="& SQLquote(rs("product_serial_no")))
					else

						set prs = conn.execute("select product_current_price,product_current_cost,product_name, prodType from tb_product where product_serial_no="& rs("product_serial_no"))
						if not prs.eof then
                           
							crs("order_product_cost") 	= prs("product_current_cost")
							crs("product_name") 		= prs("product_name") & " (" & rs("prodType") &")"
                           
							if rs("is_noebook") = 1 then
								crs("product_type") 	= 3
							else
								crs("product_type") 	= 1
							end if
						end if
						prs.close : set prs = nothing					
					end if
					crs("order_product_price") 		= rs("price")
					crs("order_product_sold") 		= rs("price_rate")
					crs("prodType")                 = rs("prodType")
					crs("order_code") 				= order_code
					crs("product_serial_no") 		= rs("product_serial_no")
					crs("order_product_sum") 		= rs("cart_temp_Quantity")
					crs("save_price") 				= rs("save_price")
					if rs("save_price")<> "" then 
						save_price_total = save_price_total + cdbl(rs("save_price"))
					end if
					crs("tag") = 1
					
					crs("sku") = 0
					crs("menu_child_serial_no") 	= rs("menu_child_serial_no")
					crs("menu_pre_serial_no") 		= rs("menu_child_serial_no")
					order_sub_total = order_sub_total + cdbl(crs("order_product_price"))
					if isnumeric( crs("order_product_cost")) then 
						order_cost = order_cost + crs("order_product_cost")
					end if
					crs("product_current_price_rate") = rs("price_rate")
				crs.update				
				crs.close : set crs = nothing
			rs.movenext
			loop
		end if
		rs.close : set rs  = nothing
	end if
	

	
    

	conn.execute ("delete from tb_order_helper where order_code='"&order_code&"' and is_ok=0")
	if cmd = "update" then
		sql = "select * from tb_order_helper where order_code='"&order_code&"'"
	else
		sql = "select * from tb_order_helper"
	end if
	
	set rs = server.CreateObject("adodb.recordset")
	rs.open "select * from tb_order_helper", conn,1,3
	if cmd <> "update" then
	    rs.addnew
	    rs("order_code") = order_code
		
	end if
	
		if session("call_me") = "1" then 
			rs("call_me") = 1	
		else
			rs("call_me") = 0
		end if
		rs("price_unit") 			= price_unit
		rs("current_system") 		= current_system
		rs("order_pay_status_id") 	= LAYOUT_PAYPAL_NO_PAED
		rs("order_source") 			= 1
		rs("customer_serial_no") 	= LAYOUT_CCID
		rs("is_ok") 				= 0
		rs("discount") 				= save_price_total
		rs("tag") 					= 0
		rs("ready_date") 			= Date()
		rs("pay_method") 			= copy_pay_method
		rs("system_category_serial_no") = CURRENT_SYSTEM
		rs("create_datetime")		= now()
		rs("order_date") 			= date()
		rs("out_status") 			= LAYOUT_ORDER_BACK_STATUS'order_back_status
		rs("pre_status_serial_no") 	= LAYOUT_ORDER_PRE_STATUS'order_pre_status
		rs("cost") 					= order_cost
		rs("shipping_company") 		= shipping_company_v
		
		'rs("prick_up_datetime1") 	= prick_up_datetime1_var
		'rs("prick_up_datetime2") 	= prick_up_datetime2_var

		set crs = conn.execute("select * from tb_cart_temp_price where order_code='"&order_code&"'")

		if not crs.eof then
		    
			rs("cost") 				=  crs("cost")
			rs("sub_total") 		=  crs("sub_total")
			rs("shipping_charge") 	=  crs("shipping_and_handling")
			rs("tax_charge") 		=  crs("sales_tax")
			rs("tax_rate") 			=  crs("sales_tax_rate")
			rs("total") 			=  crs("grand_total")
			rs("gst") 				=  crs("gst")
			rs("pst")				=  crs("pst")
			rs("hst")				=  crs("hst")
			rs("gst_rate")			=  crs("gst_rate")
			rs("pst_rate")			=  crs("pst_rate")
			rs("hst_rate")			=  crs("hst_rate")
			rs("sub_total_rate")	=  crs("sub_total_rate")
			rs("total_rate")		=  crs("grand_total_rate")
			if  instr( LAYOUT_RATE_PAY_METHODS , "["&copy_pay_method&"]") > 0  then 
				rs("grand_total") 	=  crs("grand_total_rate")
			else
				rs("grand_total") 	=  crs("grand_total")
			end if

			
			
			rs("sur_charge_rate") 	=  crs("sur_charge_rate")
			
			rs("sur_charge") 		=  crs("sur_charge")
			if instr(LAYOUT_RATE_PAY_METHODS, copy_pay_method) > 0 then 
				rs("input_order_discount") = 0
			else
				rs("input_order_discount") = crs("sur_charge")
			end if
			
			rs("taxable_total") = crs("taxable_total")
		end if
		crs.close : set crs = nothing

	rs.update

	rs.close :set rs = nothing
	
End function



' ---------------------------------------------------------------------------------
'function copyToSPDetail(system_sku)
'' ---------------------------------------------------------------------------------
'	Dim sub_rs, ssrs
'	if len(system_sku) = 8 then 
'		set sub_rs = conn.execute("select * from tb_sp_tmp_detail where sys_tmp_code='"& system_sku&"'")
'		if not sub_rs.eof then
'			conn.execute("delete from tb_sp_tmp where sys_tmp_code='"&system_sku&"'")
'			do while sub_rs.eof 
'				
'				set ssrs = server.createobject("adodb.recordset")
'				ssrs.open "select * from tb_sp_tmp", conn,1,3
'				ssrs.addnew
'					ssrs("sys_tmp_code") 			= sub_rs("sys_tmp_code")
'					ssrs("product_serial_no") 		= sub_rs("product_serial_no")
'					ssrs("product_current_price") 	= sub_rs("product_current_price")
'					ssrs("product_current_cost") 	= sub_rs("product_current_cost")
'					ssrs("product_order") 			= sub_rs("product_order")
'					ssrs("system_templete_serial_no") = sub_rs("system_templete_serial_no")
'					ssrs("part_group_id") 			= sub_rs("part_group_id")
'					ssrs("system_product_serial_no") = sub_rs("system_product_serial_no")
'				ssrs.update
'				ssrs.close : set ssrs = nothing
'			sub_rs.movenext
'			loop
'		end if
'		sub_rs.close : set sub_rs = nothing
'	
'	end if
'end function



' ---------------------------------------------------------------------------------
function ConvertDateHour(currentdate)
' ---------------------------------------------------------------------------------
	ConvertDateHour = ""
	if currentdate <> "" then 
		ConvertDateHour = ConvertDate(currentdate) & ", "& hour(cdate(currentdate)) & ":00"
	end if
end function



' ---------------------------------------------------------------------------------
function AddOrderPayRecord(order_code, amt, pay_record_id)
' ---------------------------------------------------------------------------------

 conn.execute("insert into tb_order_pay_record "&_
	                   " ( order_code, pay_record_id, pay_regdate, pay_cash, regdate, balance) "&_
	                   " values "&_
	                   " ( '"& order_code &"', '"& pay_record_id &"', now(), '"& amt &"', now(), 0 )")

end function



' ---------------------------------------------------------------------------------
function setOrderInoviceNo(order_code)
' ---------------------------------------------------------------------------------
    dim invoice_no
    dim rs, rs2
    set rs = conn.execute("select order_invoice from tb_order_helper where order_code='"& order_code &"'")
    if not rs.eof then
        invoice_no = rs(0)
        if len(invoice_no) <> 7 then 
            dim in_no
            set rs2 = conn.execute("select invoice_code from tb_order_invoice where is_lock =0 limit 0,1")
            if not rs2.eof then
                
                conn.execute("update tb_order_helper set order_invoice='"& rs2(0) &"',Is_Modify=1 where order_code='"& order_code &"'")
                conn.execute(" Update tb_order_invoice set is_lock=1, order_code='"& order_code &"' where invoice_code='"& rs2(0) &"'")
                
            end if
            rs2.close : set rs2 = nothing            
        
        end if
    end if
    rs.close : set rs = nothing

end function



' ---------------------------------------------------------------------------------
function GetEbayNumberForSpTmp(sys_code)
' ---------------------------------------------------------------------------------
	GetEbayNumberForSpTmp	=	""
	Dim rs
	Set rs = conn.execute("Select sys_tmp_code, ebay_number, system_templete_serial_no, is_customize from tb_sp_tmp Where sys_tmp_code="& SQLquote(sys_code))
	If not rs.eof then
		if cstr(SQLescape(rs("is_customize"))) <> "1" then 
			GetEbayNumberForSpTmp	=	rs(1)
		else
			GetEbayNumberForSpTmp	=	rs(0)
		end if
		
	End if 
	rs.close : set rs = nothing
End function


function GetCurrentOrder()
	
	if CurrentIsEbay then 
		GetCurrentOrder =  SQLescape(request.Cookies("ebay_order_code"))
	else
		GetCurrentOrder =  SQLescape(request.Cookies("tmp_order_code"))
	End if
	'Response.write SQLescape(request.Cookies("ebay_order_code")) &"|"&SQLescape(request.Cookies("tmp_order_code")) &"<br/>"& GetCurrentOrder &"|"&CurrentIsEbay
End function



Function CurrentSystemDefault(str, is_current_bay)
	if str = "site" then 
		if is_current_bay then 
			Response.Redirect("/ebay/")
		End if
	End if
	if str = "ebay" then
		if not is_current_bay then 
			Response.Redirect("/site/")
		End if
	End if
End Function




' ---------------------------------------------------------------------------------
function ReadAdvFile(id, post)
' ---------------------------------------------------------------------------------
	dim filename
	ReadAdvFile = LAYOUT_ADV_COMMENT & id & "_"& post&"_comment.html"	
	'ReadAdvFile = GetStrFromFile("/adv_comment/"&filename)
end function



' ---------------------------------------------------------------------------------
function getCurrentPageName()
' ---------------------------------------------------------------------------------
	dim arrays
	getCurrentPageName = request.ServerVariables("script_name") 
	if instr(getCurrentPageName, "/") > 0 then 
		arrays = split(getCurrentPageName, "/")
		'response.Write instr(onlyPageName, "/") 

		getCurrentPageName = arrays(ubound(arrays))
	End if
End function



' ---------------------------------------------------------------------------------
function GetSysOldSKUbyUncomtusize(sku8)
' ---------------------------------------------------------------------------------
	Dim rs
	if len(sku8) = LAYOUT_SYSTEM_CODE_LENGTH then 
			set rs = conn.execute("Select system_templete_serial_no, is_customize from tb_sp_tmp WHere sys_tmp_code="& SQLquote(sku8))
			If not rs.eof then
				if rs("is_customize") = 0 then 
					GetSysOldSKUbyUncomtusize = rs(0)
				else
					GetSysOldSKUbyUncomtusize	=	sku8
				End if
			else
				GetSysOldSKUbyUncomtusize = sku8
			ENd if
			rs.close : set rs = nothing
	else
		GetSysOldSKUbyUncomtusize=sku8
	End if

End function




function getHTTPPage(url) 
	on error resume next 
	dim http 
	set http=Server.createobject("Microsoft.XMLHTTP") 
	Http.open "GET",url,false 
	Http.send() 
	if Http.readystate<>4 then
		exit function 
	end if 
	getHTTPPage=BytesToBstr(Http.responseBody)
	set http=nothing
	if err.number<>0 then err.Clear  
end function 

Function BytesToBstr(body)
 dim objstream
 set objstream = Server.CreateObject("adodb.stream")
 objstream.Type = 1
 objstream.Mode =3
 objstream.Open
 objstream.Write body
 objstream.Position = 0
 objstream.Type = 2
 objstream.Charset = "utf-8"
 BytesToBstr = objstream.ReadText 
 objstream.Close
 set objstream = nothing
End Function

function toUrl(filename)
	dim http_url 
	http_url = "http://"& request.ServerVariables("SERVER_NAME")&":"& request.ServerVariables("server_port") &request.ServerVariables("url")
	
	dim arr 
	arr = split (http_url,"/")
	'response.write 
	http_url = replace(http_url, arr(ubound(arr)),"")
	toUrl = http_url & filename

end function



' ---------------------------------------------------------------------------------
function NewsLetter(email, tag)
' ---------------------------------------------------------------------------------
	dim crs
	if cint(tag) = 1 then 
		set crs = conn.execute("select count(email) from tb_news_letter where email='"& email &"'")
		if cint(crs(0)) = 0 then 
			conn.execute("insert into tb_news_letter( email,regdate,tag	) values	( '"& email &"', '"& now() &"', 1	)")
		else
			conn.execute("update tb_news_letter set tag=1 where  email='"&email&"'")
		end if
				
	else
		set crs = conn.execute("select count(email) from tb_news_letter where email='"& email &"'")
		if cint(crs(0)) > 0 then 
			
			conn.execute("update tb_news_letter set tag=0 where  email='"&email&"'")
		end if
	end if
	
end function 



' ---------------------------------------------------------------------------------
function FindSystemPriceAndSaveAndCost8Action(sys_tmp_code)
' ---------------------------------------------------------------------------------
	FindSystemPriceAndSaveAndCost8Action = "0|0|0"
	dim rs 
	if len(sys_tmp_code) = 8 then 
		set rs = conn.execute("select  ifnull(sum(p.product_current_price* sp.part_quantity),0) , ifnull(sum(p.product_current_discount* sp.part_quantity),0), ifnull(sum(p.product_current_cost* sp.part_quantity),0) from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no=sp.product_serial_no where sys_tmp_code='"&sys_tmp_code&"'")
		if not rs.eof then
			FindSystemPriceAndSaveAndCost8Action = rs(0) & "|" & rs(1)	& "|" & rs(2)			
		end if
		rs.close : set rs = nothing
	end if
	

end function



' ---------------------------------------------------------------------------------
function GetGroupComment(part_group_id)
' --------------------------------------------------------------------------------- 
	GetGroupComment = ""
	dim rs
	set rs = conn.execute("Select ifnull(part_group_comment, part_group_name) part_group_comment from tb_part_group "&_
							" where part_group_id='"& part_group_id &"'")
	if not rs.eof then
			GetGroupComment = rs(0)
	end if
	rs.close : set rs = nothing

end function

'	Find system_templete Name
' ---------------------------------------------------------------------------------
function GetSystemName2(id)
' ---------------------------------------------------------------------------------
	Dim grs
	GetSystemName2 = ""
	if id<> "" and isnumeric(id) then 
		set grs = conn.execute("select ebay_system_name from tb_ebay_system where id='"&id&"'")
		if not grs.eof then 
			GetSystemName2 = grs(0)
		end if	
		grs.close : set grs = nothing
	end if
end function


'
'
' get new customer code.
function GetNewCustomerCode()
	dim rs
	GetNewCustomerCode = ""
	set rs = conn.execute("select * from tb_store_customer_code limit 0,1")
	if not rs.eof then
		GetNewCustomerCode	=	rs("CustomerCode")
	end if
	rs.close : set rs =nothing
	
	conn.execute("delete from tb_store_customer_code where CustomerCode='"& GetNewCustomerCode &"'")
	
end function

'
'
' only www.lucomputers.com
'
function validHttpReferer()

	if instr(LCase(Request.ServerVariables("HTTP_REFERER")), "www.lucomputers.com") = 0 then 
		response.Write("not from www.lucomputers.com..")
		response.End()
	end if

	validHttpReferer = ""
end function

'
'
' Save client custom Country, State.
'
function SaveNewCountryState(country, stateName)
	Dim rs, srs
	
	SaveNewCountryState = -1
	set rs = conn.execute("select state_serial_no from tb_state_shipping where state_name = '"&stateName&"' and country = '"&country&"'")
	if not rs.eof then
		SaveNewCountryState = rs("state_serial_no")
	else
		'if(rs(0) <1)then			
			conn.execute("insert into tb_state_shipping ( state_name, state_short_name, state_code, Country, IsOtherCountry) values ('"& stateName &"', '"& stateName &"','"& stateName &"','"& country &"', '1')")
			set srs = conn.execute("select last_insert_id();")
			if not srs.eof then
				SaveNewCountryState = srs(0)
			end if
			srs.close : set srs = nothing
		'end if 
	end if
	rs.close : set rs = nothing
end function
 %>