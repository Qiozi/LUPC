<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<%

Dim sku
Dim ebay_comment
Dim showit
Dim ebay_name
Dim ebay_name_2
dim tpl_summary_id
Dim cmd
Dim ebay_note
Dim screenSize
Dim UPC

cmd             = SQLescape(request("cmd"))
sku             = SQLescape(request("sku"))
ebay_comment    = SQLescape(request("ebay_comment"))
showit          =   SQLescape(request("showit"))
ebay_name       =   SQLescape(request("ebay_name"))
ebay_name_2     =   SQLescape(request("ebay_name_2"))
tpl_summary_id  =   SQLescape(request("ebay_part_tpl_summary"))
ebay_note       =   SQLescape(request("ebay_note"))
screenSize      =   SQLescape(request("screenSize"))
UPC             =   SQLescape(request("UPC"))

set rs = conn.execute("select count(product_serial_no) from tb_product where product_ebay_name="& SQLquote(trim(ebay_name)) &" and product_serial_no != "& SQLquote(sku))
if not rs.eof then
    if(rs(0)>0)then 
        response.Write "<script>alert('Product name repetition, please enter again!!!!!!');</script>"
        closeconn()
        rs.close : set rs = nothing
        response.End()
    end if
end if
rs.close : set rs = nothing


conn.execute("Delete from tb_ebay_part_comment where part_sku="& SQLquote(sku))

if(showit="1") then
    conn.execute("insert into tb_ebay_part_comment (ebay_comment, part_sku, showit, tpl_summary_id, ebay_note,regdate) values ("& SQLquote(ebay_comment) &", "&SQLquote(sku)&", 1, "&SQLquote(tpl_summary_id)&", "& SQLquote(ebay_note) &", now()) ")
else
    conn.execute("insert into tb_ebay_part_comment (ebay_comment, part_sku,tpl_summary_id, ebay_note,regdate) values ("& SQLquote(ebay_comment) &", "&SQLquote(sku)&", "&SQLquote(tpl_summary_id)&", "& SQLquote(ebay_note) &", now()) ")
end if
conn.execute("Update tb_product set product_ebay_name="& SQLquote(trim(ebay_name)) &", screen_size='"&screenSize&"',is_modify=1,product_ebay_name_2="&SQLquote(ebay_name_2)&",UPC="&SQLquote(UPC)&" where product_serial_no="& SQLquote(sku))

closeconn %>
<script type="text/javascript">
    if ('<%= cmd %>' == 'next') {
        parent.window.location.href = '/q_admin/ebayMaster/online/get_categories.aspx?system_sku=<%=sku %>';
    }
    else
        alert('ok');
</script>
</body>
</html>
