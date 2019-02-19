<%
'----------------
' 	取得打折产品信息
'-----------------
function sql_sale_promotion(product_id)
 	sql_sale_promotion = FindOnSaleSingle(product_id)
end function 

'----------------
' 	所有打折产品信息
'-----------------
function sql_sale_promotion_all()    
   sql_sale_promotion_all = FindOnSaleAll()
end function 


'----------------
' 	取得产家促销产品信息
'-----------------
function sql_sale_rebate(product_id)
	sql_sale_rebate = sql_sale_promotion_rebate_sign(2,product_id)
end function 

'----------------
' 	所有产家促销产品信息
'-----------------
function sql_sale_rebate_all()
    
	sql_sale_rebate_all = sql_sale_promotion_rebate_all(2)
   
end function 


function sql_sale_promotion_rebate_sign(s,product_id )
	sql_sale_promotion_rebate_sign = "select p.producter_serial_no, pc.menu_child_serial_no, pc.menu_pre_serial_no,p.product_short_name, p.product_name, sp.*, p.product_current_price from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where sp.product_serial_no="&product_id&" and sp.show_it=1 and sp.promotion_or_rebate="& s & " and (date_format(now(),'%Y%j') between date_format(sp.begin_datetime,'%Y%j') and date_format(sp.end_datetime,'%Y%j')) order by sp.sale_promotion_serial_no desc limit 0,1"
end function

function sql_sale_promotion_rebate_all(s)
	sql_sale_promotion_rebate_all =  "select distinct p.product_serial_no from tb_sale_promotion sp inner join tb_product p on p.product_serial_no=sp.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where pc.tag=1 and p.tag=1 and sp.show_it=1 and (date_format(now(),'%Y%j') between date_format(sp.begin_datetime,'%Y%j') and date_format(sp.end_datetime,'%Y%j')) and promotion_or_rebate="& s 
	
	if s = 1 then 
		sql_sale_promotion_rebate_all = sql_sale_promotion_rebate_all & " order by pc.menu_child_order , p.product_order asc" 
	else
		sql_sale_promotion_rebate_all = sql_sale_promotion_rebate_all & " order by p.producter_serial_no,p.product_order asc"
	end if
end function


function GetDate()
	GetDate = cstr(year(date()))&"-" & right("0"& cstr(month(date())), 2) &"-" & right("0" & cstr(day(date())),2)
end function 

function GetSavePrice(product_id)
	dim rs 
	GetSavePrice = 0
	set rs = conn.execute("select product_current_discount from tb_product where product_serial_no='"&product_id&"'")
	if not rs.eof then
		GetSavePrice = GetSavePrice + cdbl(rs(0))
	end if
	rs.close :set rs = nothing
	
	GetSavePrice = cdbl(GetSavePrice)
	
end function

function FindOnSaleAll()
	FindOnSaleAll = "select os.*, p.menu_child_serial_no, pc.menu_pre_serial_no,p.product_short_name, p.product_name, p.product_current_price from tb_on_sale os inner join tb_product p on p.product_serial_no=os.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where (date_format(now(),'%Y%j') between date_format(os.begin_datetime,'%Y%j') and date_format(os.end_datetime,'%Y%j')) and p.tag=1 and pc.tag=1 order by pc.menu_child_order , p.product_order asc"

end function

function FindOnSaleSingle(product_id)
	FindOnSaleSingle = "select os.*,os.save_price save_cost, p.menu_child_serial_no, pc.menu_pre_serial_no,p.product_short_name, p.product_name, p.product_current_price from tb_on_sale os inner join tb_product p on p.product_serial_no=os.product_serial_no inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where (date_format(now(),'%Y%j') between date_format(os.begin_datetime,'%Y%j') and date_format(os.end_datetime,'%Y%j')) and p.tag=1 and pc.tag=1 and p.product_serial_no='"&product_id&"'"

end function

%>