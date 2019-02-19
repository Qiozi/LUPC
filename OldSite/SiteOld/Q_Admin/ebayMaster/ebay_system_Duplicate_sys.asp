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
    dim system_sku
    dim exec_sql
    dim new_id
    Dim category_id
    
'	set prs = conn.execute("SELECT ID, CATEGORY_ID FROM tb_ebay_system WHERE ID IN (200359,200364,200367,200368,200369,200372,200373,200374,200375,200376,200393,200394,200400,200401,200402,200403,200404,200405,200406,200407,200408,200409,200410,200411,200412,200413,200414,200425,200426,200427,200428,200429,200430,200431,200433,200434,200436,200450,200451,200487,200488,200489,200490,200491,200492,200493,200494,200495,200496,200497,200503,200508,200568,200569,200570,200572,200573,200574,200588,200589,200590,200591,200592,200593,200594,200595)")
'	if not prs.eof then
'	do while not prs.eof
	
    system_sku =  SQLescape(request("sys_sku"))
    category_id = SQLescape(request("category_id"))

    exec_sql = "insert into tb_ebay_system "&_
	            " ( category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, "&_
		        " showit, "&_
	            " view_count, "&_
	            " logo_filenames, "&_
	            " keywords, "&_
	            " system_title1, "&_
	            " system_title2, "&_
	            " system_title3, "&_
	            " cutom_label, "&_
	            " main_comment_ids, "&_
	            " large_pic_name, "&_
	            " adjustment, "&_
	            " is_issue "&_
	            " )"&_
	            " select category_id, ebay_system_name, ebay_system_price, ebay_system_current_number, "&_
	            " showit, "&_
	            " view_count, "&_
	            " logo_filenames, "&_
	            " keywords, "&_
	            " system_title1, "&_
	            " system_title2, "&_
	            " system_title3, "&_
	            " cutom_label, "&_
	            " main_comment_ids, "&_
	            " large_pic_name, "&_
	            " adjustment, "&_
	            " is_issue from tb_ebay_system where id="& system_sku 
	            response.write exec_sql
    conn.execute(exec_sql)
    
    new_id = 0
    set rs = conn.execute("select max(id) from tb_ebay_system")
    if not rs.eof then
        new_id = rs(0)
    end if
    rs.close : set rs = nothing
    
    exec_sql = "insert into tb_ebay_system_parts "&_
	            " ( system_sku, luc_sku, comment_id, price, cost, part_quantity, "&_
	            " max_quantity, "&_
	            " compatibility_parts, "&_
	            " comment_name, "&_
	            " part_group_id"&_
                " ,is_label_of_flash"&_
                " ,is_belong_price"&_
                " ,eBayShowit "&_
                ", regdate "&_
	            " )"&_
	            " select '"& new_id &"' , luc_sku, comment_id, price, cost, part_quantity, "&_
	            " max_quantity, "&_
	            " compatibility_parts, "&_
	            " comment_name, "&_
	            " part_group_id"&_
                " ,is_label_of_flash"&_
                " ,is_belong_price"&_
                " ,eBayShowit "&_
                " ,now() "&_
	            " from tb_ebay_system_parts where system_sku="& system_sku 
	            
	 conn.execute(exec_sql)
    
'	prs.movenext
'	loop
'	end if
'	prs.close : set prs = nothing

Closeconn() %>

<script type="text/javascript">
    alert("it is ok");
    parent.parent.$("#ifr_left_frame1").attr('src', '/q_admin/ebayMaster/lu/ebay_left_menu.asp?cid=<%= category_id %>');
    parent.parent.$("#ifr_main_frame1").attr("src", "/q_admin/ebayMaster/lu/ebay_system_edit_2.asp?ebay_system_sku=<%=  new_id %>&category_id=<%= category_id %>&cmd=modify");
</script>
</body>
</html>
