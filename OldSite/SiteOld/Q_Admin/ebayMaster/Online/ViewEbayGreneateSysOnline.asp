<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>view eBay generate sys.</title>
    <script type="text/javascript" src="/Q_admin/JS/winOpen.js"></script>
    <script src="/Q_admin/JS/helper.js" type="text/javascript"></script>
</head>
<body>
<%
    set rs = conn.execute("select BuyItNowPrice, ItemID, left(ListingDetails_StartTime,10) ListingDetails_StartTime, Title, sku from tb_ebay_selling where sku like 'New:%'")
    dim count
    count = 0
    response.write "<table>"

    if not rs.eof then
        do while not rs.eof 
            count = count + 1
            Response.write "<tr>"
            response.write "    <td width='200'>"& rs("sku") &"</td>"
            response.write "    <td width='200'><a href=""http://cgi.ebay.ca/ws/eBayISAPI.dll?ViewItem&Item="& rs("ItemID") &""" target=""_blank"">"& rs("ItemID") &"</a></td>"
            response.write "    <td width='100'>"& rs("ListingDetails_StartTime") &"</td>"
            response.write "    <td width='100'>"& rs("BuyItNowPrice") &"</td>"
            response.write "    <td>"& rs("Title") &"</td>"
            response.write "    <td>"
                'if datevalue(rs("ListingDetails_StartTime"))-datevalue(now()) < -15 then 
                    response.write "<a href='/q_admin/ebayMaster/online/EndItem.aspx?itemid="& rs("ItemID") &"' onclick=""{js_callpage_cus(this.href, 'ebay_part_end_"& rs("ItemID") &"', 780, 500);} return false;"" target=""_blank""> End </a></td>"
                'end if
            response.write "    <td>"& datevalue(rs("ListingDetails_StartTime"))-datevalue(now())
            response.write "</tr>"
        rs.movenext
        loop
    end if 
    response.Write "</table>"
    response.write count
    rs.close : set rs = nothing 
 %>
</body>
</html>
