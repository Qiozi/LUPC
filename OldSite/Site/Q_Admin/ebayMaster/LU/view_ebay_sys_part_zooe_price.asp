<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="/q_admin/ebayMaster/ebay_inc.asp"-->
<!--#include virtual="/q_admin/funs.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>view eBay System Part -- zero price</title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
    <style>
        td, i{ font-size:8pt;}
        table{ margin-left: 50px;}
    </style>
</head>
<body>
<%
    Set rs = conn.execute("select 	pg.part_group_id, pg.product_category, pg.part_group_name, "&_
                        " pg.showit, 	pg.part_group_comment	 from tb_part_group pg"&_
                        " inner join ( select distinct part_group_id from tb_ebay_system_parts where is_online=1 and part_group_id>0 group by part_group_id) esp on esp.part_group_id=pg.part_group_id  "&_
                        " where pg.is_ebay=1 and pg.showit=1 order by part_group_name asc ")
    if not rs.eof then 
        do while not rs.eof
            
                    
                        set srs = conn.execute("select p.product_serial_no , case when length(p.product_ebay_name)>0 then p.product_ebay_name else p.product_name end as product_name "&_
                                        " "&_
                                        " from tb_part_group_detail pg inner join tb_product p "&_
	                                    "  on p.product_serial_no = pg.product_serial_no"&_
	                                    "  left join tb_ebay_system_part_zero_price zp on zp.luc_sku=p.product_serial_no"&_
	                                    " where p.product_serial_no<>16684 and p.product_serial_no <>1049 and zp.luc_sku is null and pg.part_group_ID='"&rs("part_group_id")&"' and P.product_current_price=0 and p.tag=1 and p.split_line=0")
                        if not srs.eof then
                                
                                    Response.write "<b>"& rs("part_group_name") &"</b><div><i>" &rs("part_group_comment")& "<a href=""/q_admin/eBayMaster/lu/ebay_system_edit_part_of_group.asp?part_group_id="&rs("part_group_id")&""" onclick=""return parent.js_callpage_cus(this.href, 'view_ebay_part_edit', 1000, 800);"">Edit</a> </i></div>"
                                    response.write "<table>"
                                    do while not srs.eof 
                                        ' 输出引用到的 system sku
     
                                         'Response.write "</td></tr>"
                                        if instr(srs("product_name"), "Integrated")<1 and instr(srs("product_name"), "on board")<1 then 
                                            Response.write "<tr><td width='80'>"& srs("product_serial_no") & "</td><td>"& srs("product_name") &"</td> <td><a href='' onclick='hidePart("""& srs("product_serial_no") &""");return false;'>hide</td></tr>"&vbclf
                                        end if
                                    
                                    srs.movenext
                                    loop
                                    response.write "</table>"
                                 
                        end if
                        srs.close : set srs =nothing

        rs.movenext
        loop
    end if
    rs.close : set rs = nothing
%>

<% closeconn() %>
<script type="text/javascript">
    parent.window.document.getElementById('e_loading').innerHTML="";
    
    function hideZeroPrice(sku){
        if(confirm("Sure???")){
            
        }
    }
    
    function hidePart(luc_sku){
        $.ajax({
            type:"POST",
            url:"/q_admin/ebayMaster/lu/ebay_system_cmd_custom.asp",
            data: "cmd=hideEbaySysPart&luc_sku="+luc_sku,            
            success:function(msg)
            {
                if(msg.indexOf("OK")>-1)
                    alert("OK");
                else
                    alert(msg);
            },
            Error:function(errorMsg){alert("Error:" + errorMsg);}
        });
    }
</script>
</body>
</html>
