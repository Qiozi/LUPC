<%
' **********************************************************************************
' * FILE HEADER:															
' **********************************************************************************
'	GetStrFromFile(FileName)
'	ReadAdvFile(id, post)
'	FindSystemRebatePrice()
'	FindSystemPriceAndSaveAndCost8(sys_tmp_code)
'	splitConfigurePrice(prices, t)
'	FindPartStoreStatus(id)
'	ResponseStockStatus(id)
'	FindPartStoreStatus2(id, ltd_stock)
'	readASIKeyfeature(lu_sku)
'	FindPartStoreStatus_system_setting( ltd_stock)
'	ConvertToEbayPrice(regular_price)
'	FindSpecialCashPriceComment()
'	
'   GetSystemConfigurePartGroupQuantity(part_group_id)
'   SetSystemConfigurePartGroupQuantity(templete_system_info )
'   FontChangeToRed(str)
'   isMemoryCategoryId(categoryid)
'   isHardDirveCategory(categoryid)
'   setOrderInoviceNo(order_code)
'   getPartVirtualCategoryName(lu_sku)
'   setViewCount(is_part)
'   AddOrderPayRecord(order_code, amt, pay_record_id)
' *																					
' **********************************************************************************
' * DESCRIPTION:																	
' **********************************************************************************



' ---------------------------------------------------------------------------------
function FindSystemRebatePrice(system_templete_serial_no)
' ---------------------------------------------------------------------------------
	dim rs
	FindSystemRebatePrice = 0
	set rs = conn.execute("select ifnull(sum(save_cost),0) save_cost from tb_system_product st inner join tb_product p on p.product_serial_no=st.product_serial_no "&_
"inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no "&_
"inner join (select sp.save_cost,sp.begin_datetime, sp.end_datetime,sp.product_serial_no from tb_sale_promotion sp  where sp.show_it=1 and sp.promotion_or_rebate=2 and (date_format(now(),'%Y%j') "&_
"between date_format(sp.begin_datetime,'%Y%j') and date_format(sp.end_datetime,'%Y%j')) ) R on r.product_serial_no=st.product_serial_no where system_templete_serial_no='"& system_templete_serial_no &"'  and showit=1 and p.tag=1 and pc.tag=1 ")
	if not rs.eof then 
		FindSystemRebatePrice = cdbl(rs(0))
	end if
	rs.close 
	set rs = nothing

end function




' ---------------------------------------------------------------------------------
Function GetStrFromFile(FileName)
' ---------------------------------------------------------------------------------
	dim out : out = ""
	GetStrFromFile = out
	if (not isEmpty(FileName)) and (not isNull(FileName)) and (Len(FileName) > 3) then 
		if Right(FileName, 5) = ".html" then
			FileName = Server.MapPath(FileName)

				dim fs : set fs=Server.CreateObject("Scripting.FileSystemObject")
				if fs.FileExists(FileName)=true then
					set fs = nothing
					'response.Write(FileName)
					GetStrFromFile = ReadTextFile(FileName, "utf-8")
				else
					set fs = nothing
				end if
				set fs = nothing
			'end if
		end if ' html file
	end if
	
	
End Function


' ---------------------------------------------------------------------------------
Function ReadTextFile(filePath,CharSet) 
' ---------------------------------------------------------------------------------
       dim stm 
       set stm=Server.CreateObject("adodb.stream")  
       stm.Type=1 'adTypeBinary，按二进制数据读入 
       stm.Mode=3 'adModeReadWrite ,这里只能用3用其他会出错 
       stm.Open  
       stm.LoadFromFile filePath 
       stm.Position=0 '把指针移回起点 
       stm.Type=2 '文本数据 
       stm.Charset=CharSet 
       ReadTextFile = stm.ReadText 
       stm.Close  
       set stm=nothing  
End Function  


'
'	send email
'

' ---------------------------------------------------------------------------------
sub SendEmail(title, body, recipient)
' ---------------------------------------------------------------------------------
		set jmail=server.CreateObject ("jmail.message")
		jmail.Silent=true
		jmail.charset="utf-8"
		jmail.addrecipient recipient
		jmail.subject = title
		jmail.appendtext "This is a mail of HTML type"
		jmail.appendhtml body
        jmail.ReplyTo = "sales@lucomputers.com"
		jmail.from = "sales@lucomputers.com"
		jmail.fromname="LU COMPUTERS"
		jmail.priority = 3
	
		jmail.MailServerUserName ="sales@lucomputers.com"
		jmail.MailServerPassWord = "5calls2day"
		err=jmail.send("mail.lucomputers.com")
		jmail.close
		set jmail=nothing

end sub
	
'
' Get category name 
'
function getMenuChildName(id)
	getMenuChildName = ""
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select menu_child_name from tb_product_category where menu_child_serial_no="&id)
		if not mcn.eof then
			getMenuChildName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if

end function
'
'	Get product category name or parent category name
' 
function GetProductCategoryName(id)
	dim grs
	GetProductCategoryName = ""
	if id<>"" and isnumeric(id) then
		set grs = conn.execute("select menu_child_name, menu_pre_serial_no from tb_product_category where  menu_child_serial_no="&id)
		if not rs.eof then
			if( GetPreSerialNO( grs("menu_pre_serial_no"))) = 0 then 
				GetProductCategoryName = grs("menu_child_name")
			else
				GetProductCategoryName = GetProductCategoryName(grs("menu_pre_serial_no"))
			end if
		
		end if
	
	end if
end function 

function GetPreSerialNO(id)
	dim grs
	GetPreSerialNO = ""
	if id<>"" and isnumeric(id) then
		set grs = conn.execute("select menu_pre_serial_no from tb_product_category where  menu_child_serial_no="&id)
		if not rs.eof then
			GetPreSerialNO = grs(0)
		
		end if
		grs.close : set grs = nothing
	end if
end function 

function getProudctName(id)
	getProudctName = ""
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select product_short_name from tb_product where product_serial_no="&id)
		if not mcn.eof then
			getProudctName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if

end function

function getSplitLine(classid)
	getSplitLine = ""
	if classid <> "" and isnumeric(classid)then
		set gsrs =  conn.execute("select split_line from tb_product p on tb_product_line on p.product_serial_no=pl.product_serial_no where menu_child_serial_no="&classid&" group by split_line")
		if not gsrs.eof then
			do while not gsrs.eof
				getSplitLine = getSplitLine & gsrs(0) & ","
			gsrs.movenext
			loop
		end if
		gsrs.close : set gsrs = nothing
	end if
	
	if instr(getSplitLine, ",") > 0 then 
		getSplitLine = left(getSplitLine, len(getSplitLine)-1)
	end if
end function 

function getSplitLineName(lineID)
	getSplitLineName = ""
	if lineID <> "" and isnumeric(lineID) then 
		set mcn = conn.execute("select split_line_name from tb_split_line where split_line="&lineID)
		if not mcn.eof then
			getSplitLineName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if
end function
'
'function getSplitLineName(line_list)
'	getSplitLineName = ""
'	if line_list <> "" then 
'		set llrs = conn.execute("select * from tb_split_line where split_line in ("&line_list&")")
'		if not llrs.eof then
'			do while not llrs.eof 
'		end if
'	end if
'end function 

function getSplitLineName(lineID)
	getSplitLineName = ""
	if lineID <> "" and isnumeric(lineID) then 
		set mcn = conn.execute("select split_line_name from tb_split_line where split_line="&lineID)
		if not mcn.eof then
			getSplitLineName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if
end function

function ReponseWriteSumImage(id)
	ReponseWriteSumImage = ""
	dim sum
	response.write "<div>"
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select system_templete_img_sum from tb_system_templete where system_templete_serial_no="&id)
		if not mcn.eof then
			sum = cint(mcn(0))
			for i=1 to sum
				response.write "<img src=""/images/"&i&".gif"">"
			next
		end if
		mcn.close : set mcn = nothing
	end if	
	response.write "</div>"
end function

function GetSystemName(id)
	GetSystemName = ""
	if id<>"" and isnumeric(id) then
		set grs = conn.execute("select system_templete_name from tb_system_templete st inner join tb_sp_tmp sp on sp.system_templete_serial_no=st.system_templete_serial_no where sp.sys_tmp_code='"&id&"'")
		if not grs.eof then
			GetSystemName = grs(0)
		end if
		grs.close : set grs = nothing
	end if
end function

'-----------------------------
'
'	system ???????
'
'------------------------------

'	Find system_templete Name
function GetSystemName2(id)
	GetSystemName2 = ""
	if id<> "" and isnumeric(id) then 
		set grs = conn.execute("select system_templete_name from tb_system_templete where system_templete_serial_no="&id)
		if not grs.eof then 
			GetSystemName2 = grs(0)
		end if	
		grs.close : set grs = nothing
	end if
end function
'
'	Find system temp name
'
function GetSystemName3(id)
	GetSystemName3 = ""
	if id<> "" then 
		set grs = conn.execute("select sys_tmp_product_name from tb_sp_tmp where sys_tmp_code='"&id&"'")
		if not grs.eof then 
			GetSystemName3 = grs(0)
		end if	
		grs.close : set grs = nothing
	end if
end function
'
'	get system name
'
function GetSystemName8(sys_prod_sku)
	dim rs,tmp_product_name
	tmp_product_name = "System"
	set rs = conn.execute("select sys_tmp_code,p.product_serial_no, p.product_short_name from tb_sp_tmp_detail std inner join tb_product p on p.product_serial_no=std.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no  where sys_tmp_code='"& sys_prod_sku&"' and (pc.menu_child_serial_no in (select computer_cpu_category from tb_computer_cpu) or pc.menu_pre_serial_no in (select computer_cpu_category from tb_computer_cpu))")
	if not rs.eof then 
		tmp_product_name = rs("product_short_name") & " System"
	end if
	rs.close : set rs = nothing
	GetSystemName8 = tmp_product_name
end function
'
'	find system price, save
'
'
function GetSystemPriceAndSave(system_templete_serial_no)
	dim rs
	GetSystemPriceAndSave = "0|0"
	
	if system_templete_serial_no <> "" then 
		set rs = conn.execute("select ifnull(sum(p.product_current_price * part_quantity),0), ifnull(sum(p.product_current_discount* part_quantity),0) from tb_system_product sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where system_templete_serial_no='"& system_templete_serial_no &"' and showit=1 and p.tag=1 and pc.tag=1 and sp.showit=1 and p.is_non=0")
		if not rs.eof then 
			GetSystemPriceAndSave = rs(0) & "|" & rs(1)			
		end if
		rs.close : set rs = nothing
	end if
end function
'
'
'
function GetSystemPriceAndSave8(sys_tmp_code)
	dim rs
	GetSystemPriceAndSave8 = "0|0|0"
	
	if sys_tmp_code <> "" then 
		set rs = conn.execute("select old_price, save_price,sys_tmp_price from tb_sp_tmp where sys_tmp_code='"&sys_tmp_code&"'")		
		if not rs.eof then 
			GetSystemPriceAndSave8 = rs(0) & "|" & rs(1)	& "|" & rs(2)			
		end if
		rs.close : set rs = nothing
	end if
end function


' ---------------------------------------------------------------------------------
function FindSystemPriceAndSaveAndCost8(sys_tmp_code)
' ---------------------------------------------------------------------------------
	FindSystemPriceAndSaveAndCost8 = "0|0|0"
	dim rs 
	if len(sys_tmp_code) = 8 then 
		set rs = conn.execute("select  ifnull(sum(product_current_price * part_quantity),0) , ifnull(sum(save_price * part_quantity),0), ifnull(sum(product_current_cost * part_quantity),0) from tb_sp_tmp_detail where sys_tmp_code='"&sys_tmp_code&"'")
		if not rs.eof then
			FindSystemPriceAndSaveAndCost8 = rs(0) & "|" & rs(1)	& "|" & rs(2)			
		end if
		rs.close : set rs = nothing
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



'
' find system price 
'
function GetSystemPrice(system_templete_serial_no, rate,is_remove_save)
	dim rs , tmp
	GetSystemPrice = 0
			set rs = conn.execute("select sum(p.product_current_price * part_quantity) from tb_system_product sp inner join tb_product p on p.product_serial_no = sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.tag=1 and pc.tag=1 and sp.showit=1 and system_templete_serial_no='"&system_templete_serial_no&"'")
			if not rs.eof then
				do while not rs.eof 
					if (is_remove_save) then 
						if not isnull(rs(0)) then 
							GetSystemPrice = GetSystemPrice + cdbl(rs(0)) - GetSystemSaveCost(system_templete_serial_no)
													
						end if
					else
						if not isnull(rs(0)) then 
							GetSystemPrice = GetSystemPrice + cdbl(rs(0)) 
						end if
					end if
					
				rs.movenext
				loop
			end if
			rs.close : set rs = nothing
	'response.Write(GetSystemPrice)
end function

' ???system ???

function GetSystemPrice8(sys_tmp_code, is_rate)
	dim rs , tmp
	GetSystemPrice8 = 0
	if (is_rate) and sys_tmp_code<> "" then 
		tmp = GetDataTableOnerRowCell("select sum(sp.product_current_price_rate * sp.part_quantity) from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no = sp.product_serial_no where  sys_tmp_code='"&sys_tmp_code&"'")
	else
		tmp = GetDataTableOnerRowCell("select sum(sp.product_current_price * sp.part_quantity) from tb_sp_tmp_detail sp inner join tb_product p on p.product_serial_no = sp.product_serial_no where sys_tmp_code='"&sys_tmp_code&"'")
	end if
	
	if tmp <> "null" then 
		GetSystemPrice8 = cdbl(tmp) '- GetSystemSaveCost8(sys_tmp_code)
		'response.Write(GetSystemSaveCost8(sys_tmp_code))
		'response.end
	end if
end function
'
'	get system product cost by SKu
'
function GetSystemCost(system_templete_serial_no)
	dim rs , tmp
	GetSystemCost = 0
	tmp = GetDataTableOnerRowCell("select sum(p.product_current_cost) from tb_system_product sp inner join tb_product p on p.product_serial_no = sp.product_serial_no where p.tag=1 and sp.showit=1 and system_templete_serial_no="&system_templete_serial_no)
	if tmp <> "null" then 
		GetSystemCost = tmp
	end if
end function

function GetSystemCost2(tmp_code)
	dim rs , tmp
	GetSystemCost2 = 0
	tmp = GetDataTableOnerRowCell("select sum(product_current_cost) from tb_sp_tmp_detail where sys_tmp_code='"& tmp_code&"'")
	if tmp <> "null" then 
		GetSystemCost2 = tmp
	end if
end function
'
'	get system product save price$
'
function GetSystemSaveCost(system_templete_serial_no)
	dim rs 
	GetSystemSaveCost = 0
	set rs = conn.execute("select p.product_serial_no,  part_quantity from tb_system_product sp inner join tb_product p on p.product_serial_no = sp.product_serial_no where p.tag=1 and sp.showit=1 and system_templete_serial_no="&system_templete_serial_no)
	'set rs = conn.execute("select s1+s2 from (select sps1.save_cost s1 from tb_sale_promotion sps1 inner join  tb_system_product sp1 on sps1.product_serial_no=sp1.product_serial_no inner join tb_product p on p.product_serial_no = sp1.product_serial_no where p.tag=1 and sp1.showit=1 and system_templete_serial_no='"&system_templete_serial_no&"' and sps1.show_it=1 and sps1.promotion_or_rebate='1' and '"&GetDate()&"' between sps1.begin_datetime and sps1.end_datetime order by sps1.sale_promotion_serial_no desc limit 0,1) a, (select sps2.save_cost s2 from tb_sale_promotion sps2  inner join tb_system_product sp2 on sps2.product_serial_no=sp2.product_serial_no inner join tb_product p on p.product_serial_no = sp2.product_serial_no where p.tag=1 and sp2.showit=1 and system_templete_serial_no='"&system_templete_serial_no&"' and sps2.show_it=1 and sps2.promotion_or_rebate='2' and '"&GetDate()&"' between sps2.begin_datetime and sps2.end_datetime order by sps2.sale_promotion_serial_no desc limit 0,1) b")
	'response.write "select s1+s2 from (select sps1.save_cost s1 from tb_sale_promotion sps1 inner join  tb_system_product sp1 on sps1.product_serial_no=sp1.product_serial_no inner join tb_product p on p.product_serial_no = sp1.product_serial_no where p.tag=1 and sp1.showit=1 and system_templete_serial_no='"&system_templete_serial_no&"' and sps1.show_it=1 and sps1.promotion_or_rebate='1' and '"&GetDate()&"' between sps1.begin_datetime and sps1.end_datetime order by sps1.sale_promotion_serial_no desc limit 0,1) a, (select sps2.save_cost s2 from tb_sale_promotion sps2  inner join tb_system_product sp2 on sps2.product_serial_no=sp2.product_serial_no inner join tb_product p on p.product_serial_no = sp2.product_serial_no where p.tag=1 and sp2.showit=1 and system_templete_serial_no='"&system_templete_serial_no&"' and sps2.show_it=1 and sps2.promotion_or_rebate='2' and '"&GetDate()&"' between sps2.begin_datetime and sps2.end_datetime order by sps2.sale_promotion_serial_no desc limit 0,1) b"
	'response.End()
	if not rs.eof then
		do while not rs.eof 
		    
			GetSystemSaveCost = GetSystemSaveCost + cdbl(GetSavePrice(rs(0))) * cint(rs("part_quantity"))
		rs.movenext
		loop
		'GetSystemSaveCost = rs(0)
	end if
	rs.close : set rs = nothing	
	GetSystemSaveCost = cdbl(GetSystemSaveCost)
end function

' find system save price
function GetSystemSaveCost8(sys_tmp_code)
	dim rs 
	GetSystemSaveCost8 = 0
	set rs = conn.execute("select ifnull(sum(save_price* part_quantity) , 0) from tb_sp_tmp_detail sp where sys_tmp_code='"&sys_tmp_code&"'")
	if not rs.eof then
		if not isnull(rs(0)) then  
			GetSystemSaveCost8 = cdbl(rs(0))
		end if
	end if
	rs.close : set rs = nothing	
end function



function GetSystemSavePrice8Current(sys_tmp_code)
	dim rs
	GetSystemSavePrice8Current = 0
	set rs = conn.execute("select product_serial_no from tb_sp_tmp_detail sp where sys_tmp_code='"&sys_tmp_code&"'")
	if not rs.eof then
		do while not rs.eof 
			GetSystemSavePrice8Current = GetSystemSavePrice8Current + cdbl(GetSavePrice(rs(0)))
		rs.movenext
		loop
	end if
	rs.close : set rs = nothing
end function

function GetSystemPrice8Current(sys_tmp_code)
	dim rs
	GetSystemPrice8Current = 0
	set rs = conn.execute("select sum(p.product_current_price * sp.part_quantity) from tb_product p inner join tb_sp_tmp_detail sp on sp.product_serial_no=p.product_serial_no where sp.sys_tmp_code='"&sys_tmp_code&"'")
	if not rs.eof then 
		GetSystemPrice8Current = rs(0)
	end if
	rs.close : set rs = nothing	
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

'
'
'   quantity > 1  , is exist group
'
'
' ---------------------------------------------------------------------------------
function GetSystemConfigurePartGroupQuantity(part_group_id)
' ---------------------------------------------------------------------------------
    GetSystemConfigurePartGroupQuantity = 1
    
    dim part_infos : part_infos = session("templete_system_info")
    dim pgi
    
    if not isnull(part_infos) then 
        if ubound(part_infos) > 0 then 
		    for i = lbound(part_infos) to ubound(part_infos)
			    if part_infos(i, 2) <> "" then 
				    pgi =  part_infos(i,2)			
				    if not isnull(part_group_id) then 	
				        if cint(pgi) > 0 and cint(pgi) = cint(part_group_id) then 
				            
					        GetSystemConfigurePartGroupQuantity = cint(part_infos(i, 4)) + 1
				        end if
				    end if
			    end if
		    next
	    end if
	end if
	'response.Write(GetSystemConfigurePartGroupQuantity)
end function

'
'
'   set quantity to Session
'
'
' ---------------------------------------------------------------------------------
function SetSystemConfigurePartGroupQuantity(templete_system_info )
' ---------------------------------------------------------------------------------
    dim part_infos : part_infos = templete_system_info
    dim new_stores(100, 6) 
    dim pgi, quantity, is_exist
    is_exist = false
    dim i,j,x
     x = 0
    SetSystemConfigurePartGroupQuantity = null
    if true then 
        if ubound(part_infos) > 0 then 
            response.Write ubound(part_infos)
		    for i = lbound(part_infos) to ubound(part_infos)
		        is_exist = false
		        if not isnull(new_stores) then 		           
		            for j = 0 to x   		                
			                if part_infos(i, 2) = new_stores(j, 2) then 			            
			                    is_exist = true			                   
			                    new_stores(j, 4) = cint(new_stores(j, 4)) + 1
			                    exit for			                     			                    
			                end if
			        next 
			    end if
			    'response.Write part_infos(i, 2) 
			    if not is_exist then 	
			        
			        new_stores(x, 0) = part_infos(i,0)
			        new_stores(x, 1) = part_infos(i,1)
			        new_stores(x, 2) = part_infos(i,2)
			        new_stores(x, 3) = part_infos(i,3)
			        new_stores(x, 4) = part_infos(i,4)
			        new_stores(x, 5) = part_infos(i,5)
			        'response.Write new_stores(ubound(new_stores), 1)	 
			        x = x + 1   
			    end if				           
		    next
	    end if
	end if
	'response.Write new_stores(0, 4) 	 & "Yes"
	SetSystemConfigurePartGroupQuantity = new_stores
end function


' ---------------------------------------------------------------------------------
function splitConfigurePrice(prices, t)
' ---------------------------------------------------------------------------------
	dim ss
	ss = split(prices, "|")
	splitConfigurePrice = ss(t)	
end function
'
' get configure product list
'
'
function GetConfigureProductIds()
	dim ids, pids
	GetConfigureProductIds = "0"
	pids = "0"
	ids = session("templete_system_info")
	if ubound(ids,1) > 0 then 
		for i = lbound(ids,1) to ubound(ids,1)
			if ids(i, 1) <> "" then 
				pids = pids & "," & ids(i,1)
			end if
		next
	end if
	GetConfigureProductIds = pids
end function
'--------------------------------
'
'sql_sale_promotion
'	
'
'--------------------------------
function GetDataTableOnerRowCell(sql)
	GetDateTableOnerRowCell = "null"
	dim rs
	set rs = conn.execute(sql)
	if not rs.eof then 
		GetDataTableOnerRowCell = rs(0)
	end if
	rs.close :set rs = nothing
end function

function GetPayMethodNew(id)
	dim rs 
	GetPayMethodNew = 0
	if id<> "" and isnumeric(id) then
		set rs = conn.execute("select pay_method_name from tb_pay_method_new where pay_method_serial_no="&id )
		if not rs.eof then
			do while not rs.eof 
				GetPayMethodNew = rs(0)
			rs.movenext
			loop
		end if
		
		rs.close : set rs = nothing	
	end if

end function


' 
'	export state html
'
function export_state(element_name, area_name_ca, area_name_us, eventMethod, tabIndex)
%>
	<div id="<%=area_name_ca%>" style="display: none;">
	 <select name="<%=element_name%>" tabindex="<%= tabIndex %>" class="b"  style="width:150px; " 
	 <%if eventMethod <> "" then
	 	 response.write " onchange="""&eventMethod&" "" "
	   end if
	 %>
	 >
	 <option value="-1">Please Select</option>
	<%
		
		set rs = conn.execute("select state_serial_no,state_name from tb_state_shipping where system_category_serial_no=1")
		if not rs.eof then
			do while not rs.eof 
				response.Write("<option value='"&rs("state_serial_no")&"'>"&rs("state_name")&"</option>")
			rs.movenext
			loop
		end if
		rs.close :set rs  = nothing
	%>
	</select>
	</div>
	<div id="<%=area_name_us%>" style="display: none;">
	 <select name="<%=element_name%>" tabindex="<%= tabIndex %>"   class="b" style="width:150px; " >
	 	<option value="-1">Please Select</option>
	<%
		set rs = conn.execute("select state_serial_no,state_name from tb_state_shipping where system_category_serial_no=2")
		if not rs.eof then
			do while not rs.eof 
				response.Write("<option value='"&rs("state_serial_no")&"'>"&rs("state_name")&"</option>")
			rs.movenext
			loop
		end if
		rs.close :set rs  = nothing
	%></select>
	</div>
<%
end function

'
'	export state html  to paypal
'
'
function export_state_paypal(element_name, area_name_ca, area_name_us, eventMethod, tabIndex)
%>
	<div id="<%=area_name_ca%>" style="display: none;">
	 <select name="<%=element_name%>" tabindex="<%= tabIndex %>" class="b"  style="width:150px; " 
	 <%if eventMethod <> "" then
	 	 response.write " onchange="""&eventMethod&" "" "
	   end if
	 %>
	 >
	 <option value="-1">Please Select</option>
	<%
		
		set rs = conn.execute("select state_serial_no,state_name from tb_state_shipping where system_category_serial_no=1 and is_paypal=1")
		if not rs.eof then
			do while not rs.eof 
				response.Write("<option value='"&rs("state_serial_no")&"'>"&rs("state_name")&"</option>")
			rs.movenext
			loop
		end if
		rs.close :set rs  = nothing
	%>
	</select>
	</div>
	<div id="<%=area_name_us%>" style="display: none;">
	 <select name="<%=element_name%>" tabindex="<%= tabIndex %>"   class="b" style="width:150px; " >
	 	<option value="-1">Please Select</option>
	<%
		set rs = conn.execute("select state_serial_no,state_name from tb_state_shipping where system_category_serial_no=2 and is_paypal=1")
		if not rs.eof then
			do while not rs.eof 
				response.Write("<option value='"&rs("state_serial_no")&"'>"&rs("state_name")&"</option>")
			rs.movenext
			loop
		end if
		rs.close :set rs  = nothing
	%></select>
	</div>
<%
end function
'
'	Get state name by ID
'
function GetStateName(id)
	dim mcn
	GetStateName = ""
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select state_name from tb_state_shipping where state_serial_no="&id)
		if not mcn.eof then
			GetStateName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if

end function
'
'	???????
'
function GetStateShortName(id)
	dim mcn
	GetStateShortName = ""
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select state_short_name from tb_state_shipping where state_serial_no="&id)
		if not mcn.eof then
			GetStateShortName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if

end function
'
'	get country name by ID
'
function GetCountryName(id)
	dim mcn
	GetCountryName = ""
	if id <> "" and isnumeric(id) then 
		set mcn = conn.execute("select name from tb_country where id="&id)
		if not mcn.eof then
			GetCountryName = mcn(0)
		end if
		mcn.close : set mcn = nothing
	end if

end function
'
' session ?????????
'
function SessionLost(str)
	if(str = "closeConn") then 
		closeConn()
	end if
	response.redirect("session_lost.asp") 
	response.End()
end function

'
' ??????μ????s
'

'
'	?????????????
'
function getTotal(order_code)
	dim crs 
	getTotal = 0
	set crs = conn.execute("select sum(order_product_sold) from tb_order_product WHERE order_code='"& order_code& "'")
	if not crs.eof then 
		getTotal = rs(0)
	end if
	crs.close : set crs = nothing
	
end function 

'
'	??????????
'
function GetSystemPhotoByID(id)

	dim case_sku , product_image_url, product_image_1, product_image_1_g, casers
	case_sku = 0

	set casers = conn.execute("select max(product_serial_no) from (select p.menu_child_serial_no as product_category, sp.product_serial_no from tb_system_product sp inner join tb_product p on p.product_serial_no=sp.product_serial_no where system_templete_serial_no="&id&") a1 , (select pc.* from tb_product_category pc , tb_computer_case cc where pc.menu_child_serial_no=cc.computer_case_category or pc.menu_pre_serial_no=cc.computer_case_category) a2 where a1.product_category=a2.menu_child_serial_no")
	
	if not casers.eof then
		case_sku = casers(0)
	
	end if
	casers.close : set casers = nothing
	' ?
	product_image_url = "/pro_img/COMPONENTS/"
	GetSystemPhotoByID = product_image_url & case_sku & "_list_1.jpg"
end function
function GetSystemPhotoByID2(id)

	dim case_sku , product_image_url, product_image_1, product_image_1_g, casers
	case_sku = 0

	set casers = conn.execute("select max(product_serial_no) from (select p.menu_child_serial_no as product_category, sp.product_serial_no from tb_system_product sp inner join tb_product p on p.product_serial_no=sp.product_serial_no where system_templete_serial_no="&id&") a1 , (select pc.* from tb_product_category pc , tb_computer_case cc where pc.menu_child_serial_no=cc.computer_case_category or pc.menu_pre_serial_no=cc.computer_case_category) a2 where a1.product_category=a2.menu_child_serial_no")
	
	if not casers.eof then
		case_sku = casers(0)
	
	end if
	casers.close : set casers = nothing
	' ?
	product_image_url = tureurl &  "pro_img/COMPONENTS/"
	GetSystemPhotoByID2 = product_image_url & case_sku & "_system.jpg"
end function
'
'	????????????
'
function GetDiscount(order_code)
	dim crs 
	GetDiscount = 0
	set crs = conn.execute("select sum(save_price) from tb_sp_tmp where sys_tmp_code='"& order_code& "'")
	if not crs.eof then 
		GetDiscount = rs(0)
	end if
	crs.close : set crs = nothing
end function 

function NewsLetter(email, tag)
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
'
' ?????????????
'
function getProducter(id)
	if id<>"" and id <> 0 then 
		set grs = conn.execute("select producter_url from tb_product where product_serial_no='"&id&"'")
		if not grs.eof then
			if grs("producter_url") <> "0" and grs("producter_url") <> "" then 
				'response.write "<a href="""&grs("producter_url")&"""  class=""blue_orange_11"" target=""_blank"">More Information From Manufacturer</a><br/>"
			end if
		end if
		grs.close : set grs = nothing
	end if
end function 
	'
' ?????????????
'
function GetProducterName(id)
	GetProducterName = ""
	dim grs
	if id<>"" and id <> 0 then 
		set grs = conn.execute("select producter_name, producter_web_address from tb_producter where producter_serial_no='"&id&"'")
		'response.write "select producter_name, producter_web_address from tb_producter where producter_serial_no="&id
		if not grs.eof then
			if grs("producter_web_address") <> "0" and grs("producter_web_address") <> "" then 
				GetProducterName = "<a href="""&grs("producter_web_address")&"""    target=""_blank"">"& grs(0)&"</a>"
			else
				GetProducterName = grs(0)
			end if
			
		end if
		grs.close : set grs = nothing
	end if
end function 

function FileIsExist(filename)
	'dim fso
'	set fso = server.createobject("Script.fileSystemObject")
'		FileIsExist =  fso.FileExists(Server.MapPath(filename))
'	set fso = nothing
end function

'
'
' ???????????????????????
'
function AddSpaceInKeyword(keyword)
	dim tmp_str, i, is_sum, is_add_ok
	is_sum = false
	is_add_ok = false
	tmp_str = ""
	keyword = trim(keyword)
	if (keyword <> "") then 
		for i=1 to len(keyword) 
			
			tmp_str = mid(keyword, i,1)
			if isnumeric(tmp_str) then
				is_sum = true
			else
				is_sum = false
			end if
			if is_sum then 
				AddSpaceInKeyword = AddSpaceInKeyword & tmp_str
			else
				if (is_add_ok) then 
					AddSpaceInKeyword = AddSpaceInKeyword & tmp_str
					
				else
					AddSpaceInKeyword = AddSpaceInKeyword & " " & tmp_str
					is_add_ok = true
				end if
			end if
		next
	end if
end function 
'
'
'
' 
'
function GetHotImg(str)
	if (str = 1) then 
		response.write "&nbsp;<img src='/images/hot.gif' border='0'/>"
	end if
end function 
'
'
'
' 
'
function GetRebateImg(str)
	if (str = 1) then 
		response.write "&nbsp;<img src='/images/rebate.gif' border='0' onclick='window.location.href=""/product_list_rebate.asp""'   style=""cursor:pointer""/>"
	end if
end function 
'
'
'
' 
'
function GetNewImg(str)
	if (str = 1) then 
		response.write "&nbsp;<img src='/images/new.gif' border='0'/>"
	end if
end function 
'
'
'
' 
'
function GetSaleImg(str)
	if (str = 1) then 
		response.write "&nbsp;<img src='/images/Sale.gif' border='0'/>"
	end if
end function 

 '----------------------------------------------------
' ?????????????
'----------------------------------------------------

function PageLeftHTMLText()
	dim rs
	PageLeftHTMLText = ""
	set rs = conn.execute("select * from tb_right where right_page='"& current_page_name &"' order by right_id desc")
	if not rs.eof then 
		if (trim(rs("left_content")) <> "" ) then 
			PageLeftHTMLText = trim(rs("left_content"))
			response.write rs("left_content")
		end if
		
	end if
	rs.close : set rs = nothing
end function

'----------------------------------------------------
' ?????????????
'----------------------------------------------------
function PageRightHTMLText()
	dim rs
	PageRightHTMLText = ""
	set rs = conn.execute("select * from tb_right where right_page='"& current_page_name &"'")
	if not rs.eof then 
		if (trim(rs("right_content")) <> "" ) then 
			PageRightHTMLText = rs("right_content")
			
		end if
	end if
	rs.close : set rs = nothing
end function

'----------------------------------------------------

' ???????м?????
'----------------------------------------------------
function PageMainHTMLText(cpn)
	dim rs
	PageMainHTMLText = ""
	set rs = conn.execute("select * from tb_right where right_page='"& cpn &"' order by right_id desc limit 0,1")
	if not rs.eof then 
		
			PageMainHTMLText = rs("main_content")
	
	end if
	rs.close : set rs = nothing
end function
'----------------------------------------------------

' ???????????????
'----------------------------------------------------
function ConvertDateToString(str)
    ConvertDateToString = year(str) & right("0" & month(str), 2) & right("0" & day(str), 2)
end function

'
'	取得SYSTEM 明细界面字符串
'
function GetSystemProductHref(category,id)
	GetSystemProductHref = "product_detail.asp?parent_id="&category&"&class=1&id="& id
end function 


'
' 	Get tax Rate of the order.
'
function GetOrderRateByCart(order_code)
	dim rs
	GetOrderRateByCart = "error"
	set rs = conn.execute("select gst_rate+ pst_rate + hst_rate rate from tb_cart_temp_price where order_code='"& order_code &"'")
	if  not rs.eof then 
		GetOrderRateByCart = rs(0)
	end if
	rs.close : set rs = nothing
end function

'
'	change Price$
'	rate: 1.022, 1
function ChangePrice(source_price, rate)
'	if isnumeric(source_price) then
'		
''		if ChangePrice <> 0 then 
''			if ChangePrice < 100 then 
''				ChangePrice = formatcurrency(ChangePrice,0) + 0.09
''			else
''				ChangePrice = formatcurrency(ChangePrice,0) + 0.99
''			end if
''		end if
'
'		ChangePrice = source_price * rate
'		if ChangePrice <> 0 then 
'			if ChangePrice < 100 then 
'				ChangePrice = formatcurrency(ChangePrice,0) - 0.01
'			else
'				ChangePrice = formatcurrency(ChangePrice,0) - 0.01
'			end if
'		end if
'		
'	end if
	if source_price <> "" and source_price <> "undefined" then 
		ChangePrice = cdbl(source_price)
	else
		source_price = 0
	end if

end function
'
'	Special Cash Price
'
function ChangeSpecialCashPrice(source_price, card_rate)
	ChangeSpecialCashPrice = 0
	if isnumeric(source_price) then
		ChangeSpecialCashPrice = formatnumber(source_price / card_rate)
	end if
end function

function ChangeSpecialCashPrice2(source_price, card_rate)
	ChangeSpecialCashPrice2 = 0
	if isnumeric(source_price) then
		ChangeSpecialCashPrice2 = (source_price / card_rate)
	end if
end function

function ChangeSpecialCashPriceByRate2(source_price)
	ChangeSpecialCashPriceByRate2 = 0
	if isnumeric(source_price) then
		ChangeSpecialCashPriceByRate2 = ChangeSpecialCashPrice2(source_price , card_rate)
	end if
end function

function ChangeSpecialCashPriceByRate(source_price)
	ChangeSpecialCashPriceByRate = 0
	if isnumeric(source_price) then
		ChangeSpecialCashPriceByRate = ChangeSpecialCashPrice(source_price , card_rate)
	end if
end function

function removeSavePrice(price, save_price)
	removeSavePrice = price
	price = cdbl(price)
	if price <> ""  and save_price <> "" then 
		removeSavePrice = cdbl(price) - cdbl(save_price)
	end if
	
	

end function
'
'	get part product price$
'
function GetPartProductCurrentPrice(sku)
	dim rs
	GetPartProductCurrentPrice = 0
	if isnumeric(sku) then 
		set rs = conn.execute("select product_current_price from tb_product where product_serial_no='"& sku &"' and tag=1")
		if not rs.eof then 
			GetPartProductCurrentPrice = rs(0)
		end if
		rs.close : set rs = nothing
	end if
end function
'
'	choose Photo SKu 
'
function PartChoosePhotoSKU(current_part_sku, other_part_sku)
	if other_part_sku = 0 or other_part_sku = "" or isnull(other_part_sku) or other_part_sku = "0" then 
		PartChoosePhotoSKU = current_part_sku
	else
		PartChoosePhotoSKU = other_part_sku
	end if
end function
'
'	Convert Date 
'
function ConvertDate(str)
	ConvertDate = ""
	if str <> "" then
		str =cdate(str)
		dim d, m , y
		d = day(str)
		m = month(str)
		y = year(str)
	
					
		select case m
			case 1
				ConvertDate = "Jan"
			case 2
				ConvertDate = "Feb"
			case 3
				ConvertDate = "Mar"
			case 4
				ConvertDate = "Apr"
			case 5
				ConvertDate = "May"
			case 6
				ConvertDate = "Jun"
			case 7
				ConvertDate = "Jul"
			case 8
				ConvertDate = "Aug"
			case 9
				ConvertDate = "Sept"
			case 10
				ConvertDate = "Oct"
			case 11
				ConvertDate = "Nov"
			case 12
				ConvertDate = "Dec"
			case else
				ConvertDate = "December"
		end select 
		
		ConvertDate = ConvertDate & " " & d & ", " & y
	end if
end function

function ConvertDateHour(currentdate)
	ConvertDateHour = ""
	if currentdate <> "" then 
		ConvertDateHour = ConvertDate(currentdate) & ", "& hour(cdate(currentdate)) & ":00"
	end if
end function


'****************************** 
'函数：IsValidEmail(email) 
'参数：email，待验证的邮件地址 
'作者：阿里西西 
'日期：2007/7/12 
'描述：邮件地址验证 
'示例：<%=IsValidEmail(alixixi@msn.com)
'****************************** 
function IsValidEmail(email) 
 dim names, name, i, c 
 IsValidEmail = true 
 if instr(email, "@") <0 or email = "" or isnull(email) then 
  	IsValidEmail = false
 else
	 names = Split(email, "@") 
	 if UBound(names) <> 1 then 
		IsValidEmail = false 
		exit function 
	 end if 
	 for each name in names 
	  if Len(name) <= 0 then 
	   IsValidEmail = false 
		  exit function 
	  end if 
	  for i = 1 to Len(name) 
		  c = Lcase(Mid(name, i, 1)) 
	   if InStr("abcdefghijklmnopqrstuvwxyz_-.", c) <= 0 and not IsNumeric(c) then 
			 IsValidEmail = false 
			 exit function 
		   end if 
		next 
		if Left(name, 1) = "." or Right(name, 1) = "." then 
		   IsValidEmail = false 
		   exit function 
		end if 
	 next 
	 if InStr(names(1), ".") <= 0 then 
	  IsValidEmail = false 
		exit function 
	 end if 
	 i = Len(names(1)) - InStrRev(names(1), ".") 
	 if i <> 2 and i <> 3 then 
		IsValidEmail = false 
		exit function 
	 end if 
	 if InStr(email, "..") > 0 then 
		IsValidEmail = false 
	 end if 
 end if

end function  
'
'	get product category ID by product serialno
'
function GetProductCategoryByPartID(sku)
	dim rs
	GetProductCategoryByPartID = -1
	set rs = conn.execute("select menu_child_serial_no from tb_product where product_serial_no='"&sku&"' and tag=1")
	if not rs.eof then
		GetProductCategoryByPartID = rs(0)
	end if
	rs.close : set rs = nothing
end function
'
'	customer is old client
'
function CustomerIsExistOrder(customerID)
	dim rs
	CustomerIsExistOrder = false
	if (customerID <> "" ) then 
		set rs = conn.execute("select count(*) c from tb_order_helper where customer_serial_no='"& customerID &"'")
		if not rs.eof then
			if rs(0) > 0 then 
				CustomerIsExistOrder = true
			end if 
		end if
		rs.close : set rs = nothing
	end if
end function

function ExportScriptIfCustomerIsNotExist(customerID)

	if not CustomerIsExistOrder(customerID) then 
		response.Write("<script language='javascript'>ChangeArea();</script>")
	end if
end function
'
' find custome adv
'
function GetRightContent(current_page_name,params_part_product_category, have_page_value , page_area_column)
	dim rs, pre_id, p_sql
	GetRightContent = ""
	if params_part_product_category <> "" then 
		if have_page_value then
			p_sql = "right_page='"& current_page_name &"' and "
		end if
		
		if (current_page_name = "product_list.asp" or current_page_name = "product_parts_detail.asp" or current_page_name="product_detail.asp") and params_part_product_category <> "" then 
			
			set rs = conn.execute("select  exist_top, right_id from tb_right where "& p_sql &" part_product_category='"& params_part_product_category&"'")
			'response.Write("select "& page_area_column& ", exist_top from tb_right where "& p_sql &" part_product_category="& params_part_product_category)
			if not rs.eof then 
			
				if(instr(page_area_column, "left")>0) then 
					GetRightContent = ReadAdvFile(rs("right_id"), "left") 
				elseif instr(page_area_column, "right")>0 then 
					GetRightContent = ReadAdvFile(rs("right_id"), "right") 
				elseif instr(page_area_column, "main")>0 then
					GetRightContent = ReadAdvFile(rs("right_id"), "main") 
				else
					GetRightContent = "&nbsp;"
				
				end if
				
				
				if (rs("exist_top")) = 1 then viewTop10()
				if GetRightContent =  ""  and (current_page_name = "product_parts_detail.asp" or current_page_name="product_detail.asp") then 
					GetRightContent = GetRightContent("product_list.asp", params_part_product_category, false, page_area_column)
				end if
				
				pre_id = FindPreMenuChildSerialNO(params_part_product_category)			
				
				if GetRightContent =  ""  and cstr(params_part_product_category) <> 0 and params_part_product_category <> "" and pre_id <> 0 then 
					'response.Write("select right_content from tb_right where "& p_sql &" part_product_category="& params_part_product_category & "<br>") 
					GetRightContent = GetRightContent(current_page_name, pre_id, false,page_area_column)
					
				end if
			else
				if GetRightContent =  ""  and (current_page_name = "product_parts_detail.asp" or current_page_name="product_detail.asp") then 
					GetRightContent = GetRightContent("product_list.asp", params_part_product_category, false,page_area_column)
				end if
				
				pre_id = FindPreMenuChildSerialNO(params_part_product_category)
				if GetRightContent =  ""  and cstr(params_part_product_category) <> 0 and params_part_product_category <> "" and pre_id <>0 then 
					GetRightContent = GetRightContent(current_page_name, pre_id, false,page_area_column)
				end if
			end if
		end if			
	end if
end function

' ---------------------------------------------------------------------------------
function GetPartCommentFile(sku)
' ---------------------------------------------------------------------------------
	GetPartCommentFile = "part_comment/" & sku & "_comment.html"

end function

' ---------------------------------------------------------------------------------
function ReadAdvFile(id, post)
' ---------------------------------------------------------------------------------
	dim filename
	filename = id & "_"& post&"_comment.html"	
	ReadAdvFile = GetStrFromFile("/adv_comment/"&filename)
end function
 
 
' ---------------------------------------------------------------------------------
function ViewNotSpecification(id)
' ---------------------------------------------------------------------------------
	ViewNotSpecification = ""
	dim rs
	set rs = conn.execute("select part_sku, (case when part_comment='' then 0 else 1 end) is_have_desc  from tb_part_comment where part_sku='"&id&"' ")
	if not rs.eof then
		if cstr(rs("is_have_desc")) = "0" then 
			ViewNotSpecification = "<span style="" color: red "">Specification is null</span>"
		end if		
	else
		ViewNotSpecification = "<span style="" color: red "">Specification is null</span>"		
	end if
	rs.close : set rs = nothing	

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
function FindPartStoreStatus(id)
' ---------------------------------------------------------------------------------
	dim rs, sum, is_noebook
	is_noebook = false
	set rs = conn.execute("select ifnull(is_display_stock, 0) from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where product_serial_no='"&id&"'")
	if not rs.eof then
		if rs(0) = 1 then 
			is_display = true
		end if
	end if
	rs.close :set rs = nothing
	
	FindPartStoreStatus = -1

	if is_display then 
	


		FindPartStoreStatus = 0
		set rs = conn.execute("select ifnull(product_store_sum, 0) store from tb_product where product_serial_no='"& id &"'")
		if not rs.eof then
			sum = cint(rs(0))		
		end if 
		rs.close : set rs = nothing

		'if sum >0 then 
'			FindPartStoreStatus = 1
'			response.write  "<span style='color:green; font-size:8.5pt'>In Stock</span>"
'		else
			set rs = conn.execute("select ifnull(sum(product_store_sum), 0) store from tb_product_store_sum where lu_sku='"& id &"'")
			if not rs.eof then 
				sum = cint(rs(0))
				if sum > 2 then 
					FindPartStoreStatus = 2
					response.write "<span style='color:green; font-size:8.5pt'>Stock Available</span>"
				elseif sum <= 2 and sum >0 then 
					FindPartStoreStatus = 3
					response.write "<span style='color:green; font-size:8.5pt'>Stock Available(Low,please Call)</span>"
				elseif sum = 0 then 
					FindPartStoreStatus = 4
					response.write "<span style='color:#B9C7B9; font-size:8.5pt'>Out of Stock</span>"
				else
					FindPartStoreStatus = 5
					response.write "<span style='color:#B9C7B9; font-size:8.5pt'>Back Order</span>"
				end if

			end if
			rs.close : set rs = nothing
		'end if

	end if
end function


' ---------------------------------------------------------------------------------
function FindPartStoreStatus2(id, ltd_stock)
' ---------------------------------------------------------------------------------
dim rs, sum, is_noebook
	is_noebook = false
	set rs = conn.execute("select ifnull(is_display_stock, 0) from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where product_serial_no='"&id&"'")
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



' ---------------------------------------------------------------------------------
function readASIKeyfeature(lu_sku)
' ---------------------------------------------------------------------------------
'	if lu_sku <> "" then 
'		dim rs 
'		set rs = conn.execute("select marketinginfo, key_features from tb_asi_key_feature asi inner join tb_product_store_sum pss "&_
'		" on pss.product_serial_no=asi.asi_sku and pss.product_store_category=3 and lu_sku='"&lu_sku&"'")
'		'response.Write("select marketinginfo, key_features from tb_asi_key_feature asi inner join tb_product_store_sum pss "&_
'		'" on pss.product_serial_no=asi.asi_sku and pss.product_store_category=3 and lu_sku='"&lu_sku&"'")
'		if not rs.eof then 
''			response.Write "<table cellpadding=""3"" cellspacing=""3"" width=""560"" style=""font-family: Verdana; font-size: 8.5"" height=""173"">"
''			response.Write "<!-- MSTableType=""layout"" -->"
''			response.Write "<tr>"
''			response.Write "<td valign=""top"" height=""167"">"
''			response.Write "<table align=""center"" border=""0"" width=""549"">"
''			response.Write "<tr>"
''			response.Write   "<td style=""padding-left: 8px; padding-right:28px""><font face=""Verdana"" style=""font-size: 8.5pt"">"
''			response.Write  rs(0)
''			response.write  "</font></td>"
''		
''			response.Write "<tr>"
''			response.Write   "<td><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""101%"">"			
''			response.Write      "<tr>"
''			response.Write    "<td height=""21"" width=""18""><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""21"">"
''			response.Write          "<tr>"
''			response.Write           "<td colspan=""3"" height=""10""></td>"
''			response.Write         " </tr>"
''			response.Write          "<tr>"
''			response.Write          " <td width=""5""></td>"
''			response.Write         " <td bgcolor=""#bbe87d"" height=""8"" width=""8""></td>"			
''			response.Write       " <td width=""8""></td>"
''			response.Write      " </tr>"
''			response.Write    "  <tr>"
''			response.Write       " <td colspan=""3"" height=""10""></td>"
''			response.Write      " </tr>"
''			response.Write   "</table></td>"
''			response.Write  " <td class=""title-02"" width=""530""><strong><font color=""#F25413"" size=""2"">"
''			response.Write 	"Key Features</font></strong>"			
''			response.Write " </table></td>"
''			response.Write " </tr>			  "
''			response.Write   "<td style=""padding-left: 8px""><font face=""Verdana"" style=""font-size: 8.5pt"">"
''			response.Write rs(1)
''			response.write "</font></tr>"
'''			response.Write "<tr>"
'''			response.Write   "<td><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""101%"">"
'''			response.Write    "<tr>"
'''			response.Write     "<td height=""21"" width=""18""><table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""21"">"
'''			response.Write  " <tr>"
'''			response.Write       "<td colspan=""3"" height=""10""></td>"			
'''			response.Write      "</tr>"
'''			response.Write     " <tr>"
'''			response.Write      "  <td width=""5"">¡¡</td>"
'''			response.Write       "  <td bgcolor=""#bbe87d"" height=""8"" width=""8"">¡¡</td>"
'''			response.Write       "  <td width=""8"">¡¡</td>"
'''			response.Write       " </tr>"
'''			response.Write       " <tr>"			
'''			response.Write     " <td colspan=""3"" height=""10""></td>"
'''			response.Write   " </tr>"
'''			response.Write  "</table></td>"
'''			response.Write   " <td class=""title-02"" width=""530""><strong><font color=""#F25413"" size=""2"">"
'''			response.Write 	"Details</font></strong>"
'''			response.Write  "</table></td>"
'''			response.Write  " </tr>			  "
'''			response.Write  " <td style=""padding-left: 8px""><pre><font face=""Verdana"" style=""font-size: 8.5pt"">Hyper-Threading Technology: No<br />Warranty: 1 Year Parts; 3 Years Labor</tr>"			
''			response.Write 		"</table>"
''			response.Write " </td>"
''			response.Write "</tr>"
''			response.Write "</table>"
'		
'		else
'			response.write ""		
'		end if
'		rs.close : set rs = nothing
'		
'	end if
end function



' ---------------------------------------------------------------------------------
function ConvertToEbayPrice(regular_price)
' ---------------------------------------------------------------------------------
'ebay：1-50  6%，  51-1000 3.75%， 1000+ 1%
	dim price, result 
	result = 0
	price = 0
	if 	regular_price <> "" and not isnull(regular_price) and not isempty(regular_price) and isnumeric(regular_price) then 
		price = cdbl(regular_price)
		if price <= 50 and price >0 then 
			result = price + price * 0.06
		elseif price > 50 and price <= 1000 then 
			result = price + 50 * 0.06 + (price - 50 ) * 0.0375
		elseif price > 1000 then
			result = price + 50 * 0.06 + (price - 50 ) * 0.0375 + (price - 50 - 1000 ) * 0.01
		end if
	end if
	
	ConvertToEbayPrice = result
end function


' ---------------------------------------------------------------------------------
function GetSystemEbayPrice(system_templete_serial_no)
' ---------------------------------------------------------------------------------
	dim rs
	GetSystemEbayPrice = null
	set rs = conn.execute("select ifnull(ebay_price, 0) from tb_ebay_store_page where is_system=1 and length(ebay_code) >10 and not isnull(ebay_publish_date) and lu_sku='"& system_templete_serial_no &"'")
	if not rs.eof then 
		GetSystemEbayPrice = rs(0)
	end if
	rs.close : set rs = nothing
end function


' ---------------------------------------------------------------------------------
function FindSpecialCashPriceComment()
' ---------------------------------------------------------------------------------
	FindSpecialCashPriceComment = "SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash, Interac, bank transfer, money order, etc.  Cash price does not waive sales taxes if applicable."
end function



' ---------------------------------------------------------------------------------
function FontChangeToRed(str)
' ---------------------------------------------------------------------------------
    FontChangeToRed = "<span style="" color: red;"">" & str & "</span>"
end function 



' ---------------------------------------------------------------------------------
function isMemoryCategoryId(categoryid)
' ---------------------------------------------------------------------------------
    isMemoryCategoryId = instr(Application("memory_category_all_id"), categoryid) > 0
end function 



' ---------------------------------------------------------------------------------
function isHardDirveCategory(categoryid)
' ---------------------------------------------------------------------------------
    isHardDirveCategory = instr(Application("hard_diver_all_id"), categoryid) > 0
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
                
                conn.execute("update tb_order_helper set order_invoice='"& rs2(0) &"' where order_code='"& order_code &"'")
                conn.execute(" Update tb_order_invoice set is_lock=1, order_code='"& order_code &"' where invoice_code='"& rs2(0) &"'")
                
            end if
            rs2.close : set rs2 = nothing            
        
        end if
    end if
    rs.close : set rs = nothing

end function


' ---------------------------------------------------------------------------------
function getPartVirtualCategoryName(lu_sku)
' ---------------------------------------------------------------------------------
    dim rs, str
    str = ""
    set rs = conn.execute("select pc.menu_child_name from tb_product_category pc inner join tb_product_virtual pv on pv.menu_child_serial_no=pc.menu_child_serial_no where lu_sku='"& lu_sku &"'")
    if not rs.eof then 
        do while not rs.eof
            str = str & "|" & rs(0)
        rs.movenext
        loop
    end if
    rs.close : set rs = nothing
    getPartVirtualCategoryName = str
end function



' ---------------------------------------------------------------------------------
function setViewCount(is_part, ip, id)
' ---------------------------------------------------------------------------------
    dim rs
    if(is_part)then    
        conn.execute("update tb_product set view_count=view_count+1 where product_serial_no='"& id &"' and 0=(select count(*) from tb_track where track_ip='"& ip &"')")
        set rs = conn.execute("select id from tb_part_cate_view_count where luc_sku = '"& id &"' and date_format(now(),'%Y%b%d')=date_format(regdate,'%Y%b%d')")
        if not rs.eof then
           ' response.write ("update tb_part_cate_view_count set  view_count=view_count+1 where id='"& rs(0) &"' and 0=(select count(*) from tb_track where track_ip='"& ip &"'")
            conn.execute("update tb_part_cate_view_count set  view_count=view_count+1 where id='"& rs(0) &"' and 0=(select count(*) from tb_track where track_ip='"& ip &"')")
        else
            conn.execute("Insert into tb_part_cate_view_count(luc_sku, view_count, regdate) values ('"& id &"', 1, now())")
        end if
        rs.close : set rs = nothing
    else   
        conn.execute("update tb_product_category set view_count=view_count+1 where menu_child_serial_no='"& id &"' and 0=(select count(*) from tb_track where track_ip='"& ip &"')")
        
        set rs = conn.execute("select id from tb_part_cate_view_count where category_id = '"& id &"' and date_format(now(),'%Y%b%d')=date_format(regdate,'%Y%b%d')")
        if not rs.eof then
            conn.execute("update tb_part_cate_view_count set  view_count=view_count+1 where id='"& rs(0) &"' and 0=(select count(*) from tb_track where track_ip='"& ip &"')")
        else
            conn.execute("Insert into tb_part_cate_view_count(category_id, view_count, regdate) values ('"& id &"', 1, now())")
        end if
        rs.close : set rs = nothing
    end if
end function


' ---------------------------------------------------------------------------------
function AddOrderPayRecord(order_code, amt, pay_record_id)
' ---------------------------------------------------------------------------------

    conn.execute("insert into tb_order_pay_record "&_
	                   " ( order_code, pay_record_id, pay_regdate, pay_cash, regdate, balance) "&_
	                   " values "&_
	                   " ( '"& order_code &"', '"& pay_record_id &"', now(), '"& amt &"', now(), 0)")

end function

%>