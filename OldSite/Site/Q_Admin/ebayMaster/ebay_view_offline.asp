<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
     <title>eBay Offline Info</title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
</head>
<body>
<%
    Dim luc_sku         
    Dim cost
    Dim screen_size
    Dim adjustment
    

    set rs = conn.execute("Select * from tb_ebay_selling es inner join tb_product p on p.product_serial_no=es.luc_sku and p.tag=0"&_
                            " inner join ( "&_
                            " select * from (Select distinct luc_sku is_ebay_online,"&_
                            " max(cast(replace(replace(left(timeleft,instr(TimeLeft, ""D"")), ""P"",""""),""D"","""") as signed)  - "&_
                            " (cast(date_format(now(), ""%j"") as signed) - "&_
                            " cast(date_format(regdate, ""%j"") as signed))) st  "&_
                            " from tb_ebay_selling where luc_sku>0 group by luc_sku order by regdate desc, st desc) t where st>=0) "&_
                            " luValid on luValid.is_ebay_online=es.luc_sku")
    if not rs.eof then
        Response.write "<table>"
        do while not rs.eof 
            Response.write "<tr>"
            Response.write "<tr>"
            Response.write "<td>Title</td><td>"& rs("Title") &"</td>"
            Response.write "</tr>"
            if(rs("sku")<>"")then response.write  "<td width='200'>Custom Label</td><td>"& rs("sku") &"</td>"
            Response.write "</tr>"
            Response.write "<tr>"
            Response.write "<td>Item ID</td><td>"& rs("itemID") &"</td>"
            Response.write "</tr>"
            Response.write "<tr>"
            Response.write "<td>Start Time</td><td>"& rs("ListingDetails_StartTime") &"</td>"
            Response.write "</tr>"
            Response.write "<tr>"
            Response.write "<td>Left Time</td><td>"& Replace(Replace(Replace(Replace(replace(rs("TimeLeft"), "D", " Days "), "H", " Hour "),"M", " M "), "P",""), "T","")  &"</td>"
            Response.write "</tr>"
            Response.write "<tr>"
            Response.write "<td> Buy It Now Price</td><td>"& rs("BuyItNowPrice") &"&nbsp;&nbsp;" & rs("BuyItNowPrice_currencyID") & "</td>"
            Response.write "</tr>"
            Response.write "<tr>"
            Response.write "<td> View Item Url</td><td><a href='"& rs("ListingDetails_ViewItemURL") & "' target='_blank'>"& rs("ListingDetails_ViewItemURL") &"</a></td>"
            Response.write "</tr>"
            Response.write "<tr>"
            Response.write "<td> Watch Regdate </td><td>"& rs("regdate") & "</td>"
            Response.write "</tr>"
        rs.movenext
        loop
        Response.write "</table>"
    else
        Response.write "NO Match."
    end if
    rs.close : set rs = nothing

 %>

<% closeconn() %>
<br /><hr /><br />
</body>
</html>
