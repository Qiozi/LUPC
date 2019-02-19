<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>eBay Online Info</title>
    <script type="text/javascript" src="/js_css/jquery_lab/jquery-1.3.2.min.js"></script>
</head>
<body>
<%
    Dim luc_sku         :       luc_sku     =   SQLescape(request("luc_sku"))
    Dim cost
    Dim screen_size
    Dim adjustment
    set rs = conn.execute("Select product_current_cost,screen_size,adjustment from tb_product where product_serial_no='" & luc_sku & "'")
    if not rs.eof then
        cost            = rs("product_current_cost")
        screen_size     = rs("screen_size")
        adjustment      = rs("adjustment")
    end if
    rs.close: set rs = nothing
    
    Response.write "<h3>"& luc_sku &"</h3>"
    set rs = conn.execute("Select * from tb_ebay_selling where luc_sku='"+ luc_sku +"' or sys_sku='"+ luc_sku +"'")
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
<table>
    <tr>
        <td>new eBay Price: </td>
        <td><span id='newPrice' style="color:blue"><img src="/soft_img/tags/loading.gif" /></span></td>
    </tr>
    <tr>
        <td>cost:</td>
        <td><input type="text" id='cost' value="<%= cost %>"/></td>
    </tr>
    <tr>
        <td>size:</td>
        <td><input type="text" id='Screen' value="<%= screen_size %>"/></td>
    </tr>
     <tr>
        <td>Adjustment:</td>
        <td><input type="text" id='Adjustment' value="<%= adjustment %>"/></td>
    </tr>
    <tr>
        <td></td>
        <td><input type="button" id='submit' value='account' onclick="Acount();"/></td>
    </tr>
</table>
<script type="text/javascript">

function Acount(){
    $('#newPrice').html("<img src=\"/soft_img/tags/loading.gif\" />");
    $.ajax({
        type:   "get",
        url:    "/q_admin/ebayMaster/ebay_notebook_get_ebayPrice.aspx",
        data:   "Cost="+ $('#cost').val()+"&Screen="+ $('#Screen').val() + "&Adjustment="+ $('#Adjustment').val() +"&LUC_Sku=<%=luc_sku %>" ,
        success: function(msg){
            $("#newPrice").html(msg);
        },
        error: function(msg){alert(msg);}
    });
}
Acount();
</script>
</body>
</html>
