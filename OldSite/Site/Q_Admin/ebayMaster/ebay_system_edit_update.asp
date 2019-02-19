<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
</head>
<body>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%

    dim ebay_system_sku
    dim system_sell
    dim ebay_system_name 
    Dim system_title1 
    Dim system_title2 
    Dim system_title3
    Dim large_pic_name
    Dim adjustment
    Dim ebay_sell
    Dim ebay_system_price
    Dim logo_filenames
    Dim disableFlash
    Dim selected_ebay_sell
	Dim is_include_shipping
    Dim is_barebone
    Dim flashType
	Dim IsParent
    Dim caseType
    Dim batch


    IsParent                =       SQLescape(request("IsParent"))
	is_include_shipping		=		SQLescape(request("is_include_shipping"))
    if(is_include_shipping<>"1") then is_include_shipping = 0
    flashType          =       SQLescape(request("flashType"))
    if(flashType = "3") then 
        is_barebone =1 
        is_shrink = 0
    elseif(flashType="2") then 
        is_shrink = 1
        is_barebone =0 
    else
        is_shrink = 0
        is_barebone =0 
    end if

    if(is_barebone <>"1") then is_barebone = 0	
	selected_ebay_sell 		= 		SQLescape(request("ebay_system_price_real_2"))
	if(selected_ebay_sell="NA") then selected_ebay_sell = 0
    disableFlash            =       SQLescape(request("disableFlash"))
    if(disableFlash<>"1") then disableFlash = 0
    ebay_sell               =       0   ' 用于价格返回到界面
    ebay_system_sku         =       SQLescape(request("ebay_system_sku"))
    ebay_system_name      	=       SQLescape(request("system_name"))
    
    system_title1           =       SQLescape(request("system_title1"))
    system_title2           =       SQLescape(request("system_title2"))
    system_title3           =       SQLescape(request("system_title3"))
    large_pic_name          =       SQLescape(request("large_pic_name"))
    adjustment              =       SQLescape(request("adjustment"))
    ebay_system_price       =       SQLescape(request("ebay_system_price_real_1"))
	if(ebay_system_price="NA") then ebay_system_price = 0
    logo_filenames          =       SQLescape(request("logo_filenames"))
    caseType                =       SQLescape(request("caseType"))
    batch                   =       SQLescape(request("batch"))
    
    if instr(large_pic_name, ".") = 0 then
        large_pic_name = large_pic_name '& ".jpg"
    
    end if
    
  '  response.write ebay_system_name
'    conn.execute("    Update tb_ebay_system set "&_
'                        "       ebay_system_price="& SQLquote( system_sell) &_
'                        "       ,ebay_system_name= "& SQLquote( ebay_system_name ) &_
'                        "       ,system_title1= "& SQLquote( system_title1 ) &_
'                        "       ,system_title2= "& SQLquote( system_title2 ) &_
'                        "       ,system_title3= "& SQLquote( system_title3 ) &_
'                        "       ,large_pic_name=    "& SQLquote( large_pic_name ) &_
                
'                        "       where id="& SQLquote( ebay_system_sku ) &"" )
    response.write ebay_system_name
    conn.execute("    Update tb_ebay_system set "&_
                        "       cutom_label= "& SQLquote( ebay_system_name ) &_
                        "       ,large_pic_name=    "& SQLquote( large_pic_name ) &_
                        "       ,adjustment="& SQLquote(adjustment) &_
                        "       ,ebay_system_price="& SQLquote(ebay_system_price) &_
                        "       ,logo_filenames="& SQLquote(logo_filenames) &_
                        "       ,is_disable_flash_customize=" & disableFlash &_
						"		,is_include_shipping="& is_include_shipping&_
						"		,selected_ebay_sell='"& selected_ebay_sell &"'"&_
                        "       ,is_barebone='"& is_barebone &"'"&_
                        "       ,is_shrink='"& is_shrink &"'"&_
                        "       ,system_title1='"& system_title1 &"'"&_
                        "       ,ebay_system_name='"& system_title1 &"'"&_
                        "       ,CaseType='"& caseType &"'"&_
                        "       ,batch = '"& batch &"'"&_
                        "       where id="& SQLquote( ebay_system_sku ) &"" )
 

                'ebay_sell = GetEbaySystemPriceTotal(ebay_system_sku)

  '  rs.close : set rs = nothing
closeconn() %>
<script type="text/javascript">
    alert("It is OK");
    // parent.window.document.getElementById('ebay_sell').value = '<%= ebay_sell %>';
    if ('<%= IsParent %>' == 'false')
        parent.location.href = "/q_admin/ebayMaster/lu/ebay_system_edit_2.asp?IsParent=false&ebay_system_sku=<%=  ebay_system_sku %>&category_id=<%= category_id %>&cmd=modify";
    else
        parent.parent.$("#ifr_main_frame1").attr("src", "/q_admin/ebayMaster/lu/ebay_system_edit_2.asp?ebay_system_sku=<%=  ebay_system_sku %>&category_id=<%= category_id %>&cmd=modify");
</script>
</body>
</html>
