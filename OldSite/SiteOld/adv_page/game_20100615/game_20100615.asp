<!--#include virtual="site/inc/inc_page_top.asp"-->
    <style>
        .btn_middle{  background: url(/soft_img/app/customer_bottom_03.gif); text-align:center }

    </style>

<%
    Dim game_sku1       :   game_sku1 = 503115
    Dim game_sku2       :   game_sku2 = 503121
    Dim game_sku3       :   game_sku3 = 503120
    Dim game_sku4       :   game_sku4 = 503119
	Dim game_sku5		: 	game_sku5 = 503093
	Dim game_sku6		:	game_sku6 = 503095
    
%>
<div name='area' style="background:url('gamePage_01.jpg'); width:960px; height:195px;">&nbsp;</div>
<div name='area' style="background:url('gamePage_02.jpg'); width:960px; height:138px;">&nbsp;</div>
<div name='area' style="background:url('gamePage_03.jpg'); width:960px; height:288px;">&nbsp;</div>
<div name='area' style="background:url('gamePage_04.jpg'); width:960px; height:262px; vertical-align:bottom;">
    <%
      dim price_and_save, tmp_system_save_price, tmp_system_price_first, tmp_system_price
      price_and_save = GetSystemPriceAndSave(game_sku1)
							  
      tmp_system_save_price = splitConfigurePrice(price_and_save,1)
      tmp_system_price_first = splitConfigurePrice(price_and_save,0)
      tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
							  
	%>
    <table name='list_area' border="0" width="600">
        <tr>
            <td width="200"><b name="price"><i>$<%=tmp_system_price %></i></b></td><td>(<%=game_sku1 %>)</td>
            <td><img src='/soft_img/app/select.jpg' onclick="window.location.href='/site/computer_system.asp?id=<%= game_sku1 %>';" style="cursor:pointer; border:0px;" /></td>
        </tr>
        <tr>
        	<td colspan="2" >
            	<% sysListWriteToPage(game_sku1) %>
            </td>
            <td  valign="bottom"  width="100">&nbsp;
            	
            </td>
        </tr>
    </table>
</div>
<div name='area' style="background:url('gamePage_05.jpg'); width:960px; height:255px; vertical-align:bottom;">
    <%
    ' dim price_and_save, tmp_system_save_price, tmp_system_price_first, tmp_system_price
      price_and_save = GetSystemPriceAndSave(game_sku2)
							  
      tmp_system_save_price = splitConfigurePrice(price_and_save,1)
      tmp_system_price_first = splitConfigurePrice(price_and_save,0)
      tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
							  
	%>
    <table name='list_area' border="0" width="600">
        <tr>
            <td width="200"><b name="price"><i>$<%=tmp_system_price %></i></b></td><td>(<%=game_sku2 %>)</td>
            <td><img src='/soft_img/app/select.jpg' onclick="window.location.href='/site/computer_system.asp?id=<%= game_sku2 %>';" style="cursor:pointer; border:0px;" /></td>
        </tr>
        <tr>
        	<td colspan="2" >
            	<% sysListWriteToPage(game_sku2) %>
            </td>
            <td  valign="bottom" width="100">&nbsp;
            	
            </td>
        </tr>
    </table>
</div>
<div name='area' style="background:url('gamePage_06.jpg'); width:960px; height:251px; vertical-align:bottom;">
    <%
    ' dim price_and_save, tmp_system_save_price, tmp_system_price_first, tmp_system_price
      price_and_save = GetSystemPriceAndSave(game_sku3)
							  
      tmp_system_save_price = splitConfigurePrice(price_and_save,1)
      tmp_system_price_first = splitConfigurePrice(price_and_save,0)
      tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
							  
	%>
    <table name='list_area' border="0" width="600">
        <tr>
            <td width="200"><b name="price"><i>$<%=tmp_system_price %></i></b></td><td>(<%=game_sku3 %>)</td>
            <td><img src='/soft_img/app/select.jpg' onclick="window.location.href='/site/computer_system.asp?id=<%= game_sku3 %>';" style="cursor:pointer; border:0px;" /></td>
        </tr>
        <tr>
        	<td colspan="2" >
            	<% sysListWriteToPage(game_sku3) %>
            </td>
            <td  valign="bottom" width="100">&nbsp;
            	
            </td>
        </tr>
    </table>
</div>
<div name='area' style="background:url('gamePage_07.jpg'); width:960px; height:275px; vertical-align:bottom;">
    <%
    ' dim price_and_save, tmp_system_save_price, tmp_system_price_first, tmp_system_price
      price_and_save = GetSystemPriceAndSave(game_sku4)
							  
      tmp_system_save_price = splitConfigurePrice(price_and_save,1)
      tmp_system_price_first = splitConfigurePrice(price_and_save,0)
      tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
							  
	%>
    <table name='list_area' border="0" width="600">
        <tr>
            <td width="200"><b name="price"><i>$<%=tmp_system_price %></i></b></td><td>(<%=game_sku4 %>)</td>
            <td><img src='/soft_img/app/select.jpg' onclick="window.location.href='/site/computer_system.asp?id=<%= game_sku4 %>';" style="cursor:pointer; border:0px;" /></td>
        </tr>
        <tr>
        	<td colspan="2">
            	<% sysListWriteToPage(game_sku4) %>
            </td>
            <td  valign="bottom" width="100">&nbsp;
            	
            </td>
        </tr>
    </table>
</div>
<div name='area' style="background:url('bg-1.jpg') no-repeat; background-color:#000000; width:960px; height:318px; vertical-align:bottom; ">
    <%
    ' dim price_and_save, tmp_system_save_price, tmp_system_price_first, tmp_system_price
      price_and_save = GetSystemPriceAndSave(game_sku5)
							  
      tmp_system_save_price = splitConfigurePrice(price_and_save,1)
      tmp_system_price_first = splitConfigurePrice(price_and_save,0)
      tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
							  
	%>
    <table name='list_area' border="0" width="600">
        <tr>
            <td width="200"><b name="price"><i>$<%=tmp_system_price %></i></b></td><td>(<%=game_sku5 %>)</td>
            <td><img src='/soft_img/app/select.jpg' onclick="window.location.href='/site/computer_system.asp?id=<%= game_sku5 %>';" style="cursor:pointer; border:0px;" /></td>
        </tr>
        <tr>
        	<td colspan="2">
            	<% sysListWriteToPage(game_sku5) %>
            </td>
            <td  valign="bottom" width="100">&nbsp;
            	
            </td>
        </tr>
    </table>
</div>
<div name='area' style=" background:url('bg-2.jpg') 63px 20% no-repeat; background-color:#000000; width:960px; height:318px; vertical-align:bottom; ">
    <%
    ' dim price_and_save, tmp_system_save_price, tmp_system_price_first, tmp_system_price
      price_and_save = GetSystemPriceAndSave(game_sku6)
							  
      tmp_system_save_price = splitConfigurePrice(price_and_save,1)
      tmp_system_price_first = splitConfigurePrice(price_and_save,0)
      tmp_system_price = cdbl(tmp_system_price_first) - cdbl(tmp_system_save_price)
							  
	%>
    <table name='list_area' border="0" width="600">
        <tr>
            <td width="200"><b name="price"><i>$<%=tmp_system_price %></i></b></td><td>(<%=game_sku6 %>)</td>
            <td><img src='/soft_img/app/select.jpg' onclick="window.location.href='/site/computer_system.asp?id=<%= game_sku6 %>';" style="cursor:pointer; border:0px;" /></td>
        </tr>
        <tr>
        	<td colspan="2">
            	<% sysListWriteToPage(game_sku6) %>
            </td>
            <td  valign="bottom" width="100">&nbsp;
            	
            </td>
        </tr>
    </table>
</div>
<%

function sysListWriteToPage(sku)
    Dim rs
    
    Set rs = conn.execute("select case when length(p.product_short_name) >5 then p.product_short_name"&_
                    " else p.product_name end as product_short_name "&_
					" ,pg.part_group_name"&_
					" , p.product_serial_no"&_
					" , p.product_current_price"&_
					" , part_quantity"&_
					"  from tb_system_product sp "&_
					" 	inner join tb_product p on sp.product_serial_no=p.product_serial_no "&_
					" 	inner join tb_part_group pg on sp.part_group_id=pg.part_group_id "&_
					" 	inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no "&_
					"  where sp.system_templete_serial_no="&sku&" and sp.showit=1 and p.tag=1 and (p.is_non=0 or  p.product_name like '%default basic fan%') order by  sp.product_order asc ")
    if not rs.eof then
        Response.Write "<ul name='list'>"
            do while not rs.eof 
                Response.Write "<li style=""font-family:Myriad Pro; font-size:12pt;"">"
				if cint(rs("part_quantity"))>1 then
					Response.Write rs("part_quantity") &"&nbsp;x&nbsp;" & rs("product_short_name")
				else
                	Response.Write "    " &  rs("product_short_name")
				end if
                Response.Write "</li>"
            rs.movenext
            loop
        Response.Write "</ul>"
    end if
    rs.close : set rs = nothing
end function 


closeconn()
 %>
<script type="text/javascript">
    $().ready(function() {
        $('body').css({ "margin": "0px", "background": "#464646" });
        $('div[name=area]').css({ "margin": "0px auto", "overflow": "hidden" });
        $('table[name=img_area]').css({ "margin": "0px auto", "overflow": "hidden" });
        $('table[name=list_area]').css({ "margin-left": "300px", "color": "#ffffff" });
        $('ul').css({ "margin": "0px", "padding": "0px", "margin-top":'5px' });
		$('ul[name=list]').find('li').css({ 'color':'#fff'});
        $('b[name=price]').css({ 'font-size': '15pt' });
        $('ul[name=list]').css({ 'font-size': '11pt' });
    });
</script>
<script type="text/javascript">
try{
_uacct = "UA-4447256-1";
urchinTracker();
}catch(e){}
</script>
</body>
</html>
