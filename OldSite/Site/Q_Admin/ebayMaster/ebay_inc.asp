﻿<%
'
'
'   GetEbayCategoryLeftMenu()
'   IsExistEbayNumber(system_sku)
'   GetEbayNumberList(system_sku)
'   GetEbaySystemPriceSource(system_sku)
'   GetEbaySystemPriceAdajustment(system_sku)
'   GetEbaySystemPriceTotal(system_sku)
'   EbaySystemShippingAccount(systemPriceSource)
'
'
'
'
'
'


'------------------------------------------------------------------------------------
Function GetEbayCategoryLeftMenu()
'------------------------------------------------------------------------------------
    Dim Rs, s, cRs, sRs
    
    set rs = conn.execute("select es.id, es.cutom_label, es.ebay_system_current_number from tb_ebay_system es inner join tb_ebay_code_and_luc_sku els on els.sku = es.id  where es.is_online = 1 order by es.id desc limit 200")
    if not rs.eof then
        s = s & "<ul>"
        do while not rs.eof
        s =     s &     "<li><span class=""file"" id=""treeview_system_sku_"& rs("id") &"""> <a onclick=""parent.$('#ifr_main_frame1').attr('src','/q_admin/ebayMaster/lu/ebay_system_edit_2.asp?ebay_system_sku="& rs("id") &"&category_id=00000&cmd=modify&'+rand(100));"")>"& rs("ebay_system_current_number")& rs("cutom_label") &"</a></span></li>" &vblf
        rs.movenext
        loop
        s = s & "</ul>"
    end if 
    rs.close : set rs = nothing
    GetEbayCategoryLeftMenu   =   s
End Function


Function GetEbayNumberList(system_sku)
    Dim rs
    
    set rs = conn.execute("select c.ebay_code, c.regdate,c.BuyItNowPrice from tb_ebay_code_and_luc_sku c "&_
	                      "  where SKU='"& system_sku &"'")
    if not rs.eof then
            Response.write "<table>" &vblf
            do while not rs.eof 
                Response.write "<tr><td style='width: 120px;'>" & rs("ebay_code") & "</td>"&_
                                " <td style='text-align:right;width: 80px;'>"& rs("BuyItNowPrice") &"</td>"&_
                                " <td>&nbsp;&nbsp;"& rs("regdate") &"</td></tr>" &vblf
            rs.movenext
            loop
            Response.write "</table>" &vblf
    end if
    rs.close : set rs = nothing   
    
End Function

'------------------------------------------------------------------------------------
Function IsExistEbayNumber(system_sku)
'------------------------------------------------------------------------------------
    dim rs
        
    IsExistEbayNumber = false
    
    set rs = conn.execute("Select count(id) from tb_ebay_item_number where luc_sku='"& system_sku &"' ")
    if not rs.eof then
        if cstr(rs(0)) <> "0" then
            IsExistEbayNumber = true
        end if
    end if
    rs.close : set rs = nothing

End Function


''////////////////////////////////////////////////////////////////////////////
'
'
Function GetEbaySystemPriceSource(system_sku)
    Dim rs
    GetEbaySystemPriceSource = 0
    set rs = conn.execute("select ifnull(sum(part_ebay_price), 0) from tb_ebay_system_parts es "&_
                            " inner join tb_product p on p.product_serial_no=es.luc_sku"&_
                            " where p.split_line=0 and es.system_sku='"& system_sku &"'")
    if not rs.eof then
        GetEbaySystemPriceSource = rs(0)
    end if
    rs.close : set rs = nothing    
End Function


'***********************************************************
'
'
Function GetEbaySystemPriceAdajustment(system_sku)
    Dim rs
    GetEbaySystemPriceAdajustment = 0
    
    set rs = conn.execute("Select ifnull(adjustment,0) from tb_ebay_system where id='"& system_sku &"'")
    if not rs.eof then
        GetEbaySystemPriceAdajustment = rs(0) 
    end if 
	
	if GetEbaySystemPriceAdajustment="" then GetEbaySystemPriceAdajustment = 0
End Function

'***********************************************************
'
'
'Function GetEbaySystemPriceTotal(system_sku)
'    Dim rs, total, source, adajustment, shipping, total1, total2
'    source      =   GetEbaySystemPriceSource(system_sku)
'    adajustment =   GetEbaySystemPriceAdajustment(system_sku)
'    shipping    =   EbaySystemShippingAccount(source)
'    'response.Write source
'    total       =   cdbl(source) + cdbl(adajustment)
'    'a: cost
'    'e: price adjustment
'   
'    
'     
'     '1.1 1.08 1.065 profit
'     '1.022 with paypal fee
'    'response.Write total
'    if total > 0 and total <= 1000 then
'             total = total * 1.1 * 1.022
'    elseif total >= 1000.01  and total <= 1500 then
'             total = total * 1.08 * 1.022
'    else
'             total = total * 1.065 * 1.022
'    End if
'    '1.055 when sale price <=1000, ebay fee
'    '1.025 when sale price >1000, ebay fee
'    
'    'c1 = c * 1.055
'    'c2 = c * 1.025
'    total1 = total * 1.055
'    total2 = total * 1.025
'    'a = c + b * 1.022
'    'a2 = a * 1.025
'    source = total + shipping * 1.022
'    
'    'response.Write total1 &"|"& total2 &"|"& source
'   if total2 >= 1 and total2 <= 1000 And source > 1000 then
'            GetEbaySystemPriceTotal = source
'   elseif total2 >= 1 and total2 <= 1000 And source <= 1000 then
'            GetEbaySystemPriceTotal = total1
'   else
'            GetEbaySystemPriceTotal = total2
'   End if
'   if(GetEbaySystemPriceTotal<>"")then
'        GetEbaySystemPriceTotal = formatnumber(GetEbaySystemPriceTotal, 2)
'   end if
'End Function



'***********************************************************
'
'
Function EbaySystemShippingAccount(systemPriceSource)
   if( systemPriceSource <>"") then
        systemPriceSource =cdbl(systemPriceSource)
       if clng(systemPriceSource)<=700 then
                 EbaySystemShippingAccount = 40
       elseif systemPriceSource >=700.01 and systemPriceSource<= 1050   then       
                 EbaySystemShippingAccount = 45
       elseif systemPriceSource>= 1050.01 and  systemPriceSource <=1440 then   
                 EbaySystemShippingAccount = 55
       else
                 EbaySystemShippingAccount = 65
       End if    
   else
      EbaySystemShippingAccount = 0
   end if
End Function

 %>