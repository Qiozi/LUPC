<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Untitled Page</title>
    <script type="text/javascript" src="../JS/lib/jquery-1.3.2.min.js"></script>
    <script type="text/javascript" src="../JS/lib/jquery.bgiframe.min.js"></script>
    <script type="text/javascript" src="../JS/lib/jquery.ajaxQueue.js"></script>
    <script type="text/javascript" src="../JS/lib/thickbox-compressed.js"></script>
    <script type="text/javascript" src="../JS/lib/jquery.autocomplete.js"></script>
    <script type="text/javascript" src="../xmlStore/part_name_data.js"></script>
    <script src="../JS/lib/jquery.history_remote.pack.js"   type="text/javascript"></script>
    <script src="../JS/lib/jquery.tabs.pack.js"  type="text/javascript"></script>
    
    <link rel="stylesheet" type="text/css" href="../../js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="../../js_css/b_lu.css" />
    <style type="text/css">
        input{font-size:7.5pt;}
        #Button1
        {
            width: 97px;
            height: 67px;
        }
        .bottom_menu li { float:left; list-style: none }
        .bottom_menu li a { display: block; float:left; width: 80px; padding: 2px; text-align:center}
        .bottom_menu li a:hover { display: block; float:left; width: 80px; padding: 2px; background:green;  text-align:center; color:White}
    </style>

</head>

<body>    
<div id="container-5">
            <ul>
                <li><a href="#fragment-13"><span>Edit</span></a></li>
                
                <li><a href="#fragment-14"><span>Custom Setting</span></a></li>
                <li><a href="#fragment-15"><span>ALL Ebay Code</span></a></li>
            </ul>
             <div id="fragment-13">
                
<%
        dim category_id, cmd, ebay_system_sku
		Dim ebay_system_code	:	ebay_system_code= SQLescape(request("ebay_system_code"))
		Dim id 	: 	id = null
        Dim is_exist_ebay_number
       
        
        ebay_system_sku            		=      SQLescape(request("ebay_system_sku"))
        cmd                             =      SQLescape(request("cmd"))
        category_id                     =      SQLescape(request("category_id"))
		
		'
		Set rs = conn.execute("select id, ebay_system_current_number, logo_filenames from tb_ebay_system where id="&SQLquote(ebay_system_sku)&" or ebay_system_current_number="&SQLquote(ebay_system_sku))
		if not rs.eof then
			ebay_system_sku		=	RS("ID")
			ebay_system_code	=	RS("ebay_system_current_number")
			response.write "<div class='title'><h3> SKU: "& ebay_system_sku &"&nbsp;&nbsp; <span id='system_comment_span'></span> <input type='button' value='Dupi' onclick='OpenDuplicateSys("""& ebay_system_sku &""")'>"
			if len(ebay_system_code) >0 then response.write "<br>Current Ebay#: &nbsp;"& ebay_system_code
			Response.write "</h3></div>"
		end if
		rs.close : set rs = nothing

        dim el_sku_ids, el_part_name_ids, el_comment_ids
        el_comment_ids                   =          "0"
        el_sku_ids                       =          "0"
        el_part_name_ids                 =          "0"
        
        response.write "<form action='/q_admin/ebayMaster/ebay_system_edit_exec.asp' method='post' target='iframe1' onsubmit='return ValidPost();'>"
        response.write "<input type='hidden' name='category_id' value='"& category_id &"'/>"
        response.write "<input type='hidden' name='cmd' value='"& cmd &"' />"
        response.write "<input type='hidden' name='ebay_system_sku' value= '"& ebay_system_sku &"'/>"
        
        set rs = conn.execute("select id, comment from tb_ebay_system_part_comment where showit=1 order by priority asc ")
        response.write "<table>"
        if not rs.eof then
                is_exist_ebay_number = IsExistEbayNumber(ebay_system_sku)
                do while not rs.eof 
                        el_sku_ids               = el_sku_ids & ",sku_"&rs(0)
                        el_part_name_ids         = el_part_name_ids & ",name_" & rs(0) 
                        el_comment_ids           = el_comment_ids & "," & rs(0)
                        response.write "<tr>"
                        response.write "       <td>"& rs(1) &"<input type='hidden' name='comment_id' value='"& rs(0) &"'/></td>"
                        
                        if cmd = "modify" then
                                set srs = conn.execute("select es.id, luc_sku , product_ebay_name, es.part_quantity, es.max_quantity"&_
								",p.product_current_price-p.product_current_discount sell, p.product_current_cost"&_
								" from tb_ebay_system_parts es inner join tb_product p on p.product_serial_no=es.luc_sku where comment_id='"& rs(0) &"' and system_sku='"& ebay_system_sku &"'")
                                if not srs.eof then
										Response.write " 	   <input type='hidden' name='p_id' value='"&srs("id")&"' >"
                                        response.write "       <td><input type='text' maxlength='6' name='luc_sku' id='sku_"&rs(0)&"' size='8' value='"& srs("luc_sku") &"' "
                                        'if is_exist_ebay_number then Response.write " disabled='true' "
                                        response.write "            ></td>"
                                        response.write "       <td><input type='text' name='luc_part_name' id='name_"& rs(0) &"' size='100' value='"& srs("product_ebay_name") &"'></td>"                                
										response.write "       <td>qty:<input type='text' name='luc_part_part_quantity' id='part_quantity_"& rs(0) &"' size='3'  value='"&srs("part_quantity")&"'></td>"
										response.write "       <td>max:<input type='text' name='luc_part_max_quantity' id='max_quantity_"& rs(0) &"' size='3' value='"&srs("max_quantity")&"'>"
										
										' write price
										
										response.write "		<input type='text' name='cost' value='"& srs("product_current_cost") &"' style='text-align:right;'>"
										Response.write "		<input type='text' name='sell' value='"& srs("sell") &"' style='text-align:right;'>"
										response.write "</td>"
                                else
										Response.write " 	   <input type='hidden' name='p_id'  >"
                                        response.write "       <td><input type='text' maxlength='6' name='luc_sku' id='sku_"&rs(0)&"' size='8' "
                                       ' if is_exist_ebay_number then Response.write " disabled='true' "
                                        response.write "        ></td>"
                                        response.write "       <td><input type='text' name='luc_part_name' id='name_"& rs(0) &"' size='100'></td>"
										response.write "       <td>qty:<input type='text' name='luc_part_part_quantity' id='part_quantity_"& rs(0) &"' size='3' value=1></td>"
										response.write "       <td>max:<input type='text' name='luc_part_max_quantity' id='max_quantity_"& rs(0) &"' size='3' value=1>"
										
										response.write "		<input type='text' name='cost' value='' >"
										Response.write "		<input type='text' name='sell' value='' >"
										
										response.write "</td>"
                                end if
                                srs.close : set srs = nothing
                        else
                        		Response.write " 	   <input type='hidden' name='p_id'  >"
                                response.write "       <td><input type='text' maxlength='6' name='luc_sku' id='sku_"&rs(0)&"' size='8' "                                
                                response.write "        ></td>"
                                response.write "       <td><input type='text' name='luc_part_name' id='name_"& rs(0) &"' size='100'></td>"
                        		response.write "       <td>qty:<input type='text' name='luc_part_part_quantity' id='part_quantity_"& rs(0) &"' size='3' value=1></td>"
								response.write "       <td>max:<input type='text' name='luc_part_max_quantity' id='max_quantity_"& rs(0) &"' size='3' value=1></td>"
                        end if
                        
                        response.write "</tr>"
                rs.movenext
                loop
               
        else
            response.write "<tr><td> No Match Data </td></tr>" 
        end if
        response.write "<table>"
        rs.close : set rs = nothing
        
        response.write "<div style='text-align:center;width: 800px;'><input type='submit' value='Save'><input type='button' value='Issue' onclick=""IssueSystem('"& ebay_system_sku &"');""></div>"
        response.write "</form>"
        
        
        dim el_sku_ids_gs, el_part_name_ids_gs, comment_ids_gs
        el_comment_ids_gs = split(el_comment_ids, ",")
        el_sku_ids_gs = split( el_sku_ids, ",")
        el_part_name_ids_gs = split (el_part_name_ids, ",")
        
        for i=lbound(el_sku_ids_gs) to ubound(el_sku_ids_gs)
                if(cstr(el_sku_ids_gs(i)) <> "0") then
                        response.write  "<script> $().ready(function() {"
                        
                        response.write  "      $(""#"& el_sku_ids_gs(i) &""").autocomplete(PART_JSON_"& el_comment_ids_gs(i) &", { "
                        response.write  "      minChars: 0,         "
                        response.write  "      width: 510,         "
                        response.write  "      matchContains: true,         "
                        response.write  "      autoFill: false,         "
                        response.write  "      formatItem: function(row, i, max) {         "
                        response.write  "       return i + ""/"" + max + "": \"""" + row.sku + ""\"" ["" + row.name + ""]"";         "
                        response.write  "      },         "
                        response.write  "      formatMatch: function(row, i, max) {         "
                        response.write  "      return row.sku + "" "" + row.name;         "
                        response.write  "      },         "
                        response.write  "      formatResult: function(row) {         "
                        response.write  "       return row.sku+"""& LAYOUT_SPLIT_CHAR & """+row.name;         "
                        response.write  "       }         "
                        response.write  "      });        "   
                        
                        response.write  "      $(""#"& el_sku_ids_gs(i) & """).blur(function() {	          "    
	                    response.write  "              var v = $(""#"& el_sku_ids_gs(i) & """).val();	    if(v.indexOf("""& LAYOUT_SPLIT_CHAR & """)!=-1){  $(""#"& el_sku_ids_gs(i) &""").val(v.split("""& LAYOUT_SPLIT_CHAR & """)[0]); $(""#"& el_part_name_ids_gs(i) &""").val(v.split("""& LAYOUT_SPLIT_CHAR & """)[1]);}          "
	                    response.write  "      });             "
	                    
	                    response.write  "      }); </script>"
                end if
        next 
        
    if cmd = "modify" then
            dim system_name, system_price, system_cost, system_sell
            Dim large_pic_name
			Dim sys_keywords
			Dim logo_filenames
			Dim is_exist
            set rs = conn.execute("select id, ebay_system_name, ebay_system_price, keywords, logo_filenames"&_
                                ", system_title1, system_title2, system_title3,large_pic_name from tb_ebay_system where id='"&ebay_system_sku & "'")
            if not rs.eof then
                    system_name     =   rs("ebay_system_name")
                    system_sell     =   rs("ebay_system_price")

					sys_keywords    =   rs("keywords")
					logo_filenames	=	rs("logo_filenames")
					
					system_title1   =   rs("system_title1")
					system_title2   =   rs("system_title2")
					system_title3   =   rs("system_title3")
					large_pic_name  =   rs("large_pic_name")
            end if
            rs.close : set rs = nothing 
            
            set rs = conn.execute("select "&_
                                                " sum(product_current_price-product_current_discount*es.part_quantity) price "&_
                                                "  , sum(product_current_cost*es.part_quantity) cost "&_
                                                "  from tb_product p inner join  tb_ebay_system_parts es on es.luc_sku=p.product_serial_no where es.system_sku='"&ebay_system_sku &"'")
            if not rs.eof then
                    system_price =  rs(0)
                    system_cost  =  rs(1)
                    
            end if
            rs.close : set rs = nothing
 %>

                <hr size="1" />
                <br />
                <div style="border-top: 1px dotted #cccccc; margin-top:5px; padding-top:5px;"></div>
                 <form action="ebay_system_edit_update.asp" method="post" target="iframe1">
                        <input type="hidden" name="ebay_system_sku" value="<%= ebay_system_sku %>" />         
                 <table>
                        <tr>
                                <td>System Comment: </td><td colspan="2"><input type="text" size="70"  name="system_name" value="<%= system_name %>"/></td>
                        </tr>
                        <tr>
                                <td>System Title1</td><td colspan="2"><input type="text" size="200"  name="system_title1" value="<%= system_title1 %>"/></td>
                        </tr>
                        <tr>
                                <td>System Title2</td><td colspan="2"><input type="text" size="200"  name="system_title2" value="<%= system_title2 %>"/></td>
                        </tr>
                        <tr>
                                <td>system_title3</td><td colspan="2"><input type="text" size="200"  name="system_title3" value="<%= system_title3 %>"/></td>
                        </tr>
                        <tr>
                                <td>Large Picture Name</td><td colspan="2"><input type="text" size="200"  name="large_pic_name" value="<%= large_pic_name %>"/></td>
                        </tr>
                        <tr>
                                <td style="width: 120px">System Cost$</td><td><input type="text" name="cost" 
                                    style="text-align:right; color: #cccccc;" disabled="disabled"  
                                    value="<%= system_cost %>"/></td>
                                <td rowspan="3" style="text-align:right">
                                    <input id="Button1" type="submit" value="Save"  /></td>
                        </tr>
                        <tr>
                                <td>System Price$</td><td><input type="text" name="price" 
                                    style="text-align:right; color: #cccccc;" disabled="disabled" 
                                    value="<%= system_price %>" /></td>
                        </tr>
                        <tr>
                                <td>System Sell$</td><td><input type="text" name="sell" style="text-align:right;" value="<%= system_sell %>" /></td>
                        </tr>
                </table>
                </form>
                 
                 
                <hr size="1" />
                <p style="">
                    <div class='title'><h3>Keyword </h3></div>
                    <div style="padding: 0 0 5px 10px;border-bottom: 1px dotted #cccccc; margin-bottom: 5px;"><input type='text' name="sys_keywords" value="<%= sys_keywords %>" size="80" /><input type='button' value="Submit" onclick="$('span[title=keyword_result]').load('ebay_save_system_keyword.asp?id=<%= ebay_system_sku %>&keyword='+ $('input[name=sys_keywords]').val());" />
                    </div>
                    <span title='keyword_result'></span>
                    	<%
							Set rs = conn.execute("Select * from tb_product_category_keyword where is_ebay=1 order by priority asc ")
							If not rs.eof then
								Response.write "<table border='0' title='keyword_area'>"&vblf
								
								Do while not rs.eof 
										Response.write "	<tr>"&vblf
										Response.write "		<td title='keyword_comment'>"&vblf
										Response.write "			<span>"&  rs("keyword") &"</span>"&vblf
										Response.write "		</td>"&vblf										
										Response.write "		<td>"&vblf
												set crs = conn.execute("select * from tb_product_category_keyword_sub where parent_id='"& rs("id") &"'")
                                    			if not crs.eof then 
														Do while not crs.eof
															if instr(sys_keywords, "["& crs("keyword") &"]") >0 then 
																is_exist = " checked='true' "
															else
																is_exist = ""
															end if
															Response.Write "		<div ><input type='checkbox' name='key' onclick='SetKeywordValue(this);' "& is_exist&" value='"& crs("keyword") &"'>"& crs("keyword") &"</div>"&vblf
														crs.movenext
														loop
												end if
												crs.close : set crs  = nothing
										Response.write "		</td>"&vblf
										Response.write "	</tr>"&vblf												
								rs.movenext
								loop	
								Response.Write "</table>"&vblf						
							end if
							rs.close : set rs = nothing
						%>
                </p>
                <hr size="1" />
                <p >                    
                    <div class='title'><h3>Shipping & LOGO</h3></div>
                    
                    <table style="width:98%;">
                        <tr>
                            <td style="width:40%; border-right: 1px dotted #cccccc;" valign="top">
                                    <form action="ebay_system_shipping_exec.asp" method="post">
                    	                <center style="border-bottom: 1px dotted #cccccc;margin-bottom:5px;padding-bottom:5px;"><input type='submit' value="submit" /></center>
                                        <input type="hidden" value="<%= cmd %>" name="cmd" />
                                        <input type="hidden" value="<%= ebay_system_sku %>" name="ebay_system_sku" />
                    	                <%
							                Set rs = conn.execute("Select ifnull(es.shipping_charge, 0) shipping_charge,sc.shipping_company_name, sc.shipping_company_id from tb_shipping_company  sc "&_
													                " left join tb_ebay_system_shipping es on es.shipping_company_id=sc.shipping_company_id and es.ebay_system_sku="& SQLquote(ebay_system_sku)&""&_
													                " where is_sales_promotion=0 order by qty asc")
							                If not rs.eof then
								                Response.Write "<table title='shipping_charge' style='width:100%'>"&vblf
								                Do while not rs.eof
									                Response.write "	<tr>"&vblf
									                Response.Write "		<td width='150'>"&vblf
									                Response.write "			<input type='hidden' name='shipping_id' value='"& rs("shipping_company_id") &"'/>"&vblf
									                Response.write rs("shipping_company_name")	&vblf
									                Response.write "		</td>"&vblf
									                Response.Write "		<td>"&vblf
									                Response.write "			<input type='text' name='shipping_charge' value='"& rs("shipping_charge")&"' class='price'/>"&vblf
									                Response.write "		</td>"&vblf
									                Response.write "	</tr>"&vblf
								                rs.movenext
								                loop
								                Response.write "</table>"&vblf
							                End if
							                rs.close : set rs = nothing
						                %>
                                    </form>                            
                            </td>
                            <td valign="top">
                                <form action="ebay_system_logo_exec.asp" method="post" target="iframe1">
                                    <center style="border-bottom: 1px dotted #cccccc;margin-bottom:5px;padding-bottom:5px;">
                                        <span id='main_logo_view'></span>
                                        <input type="text" name='logo' size="80" value='<%= logo_filenames %>' onchange="viewSelectedLogo();"/>
                                        <input type="submit" name='submit' value="save" />
                                        <input type="button" name='clear' value="clear" onclick="$('input[name=logo]').val('');$('#main_logo_view').html('')" />
                                        <input type="hidden" value="<%= ebay_system_sku %>" name="ebay_system_sku" />
                                    </center>
                                    <span  id='main_logo_area'></span>
                                    
                                </form>
                            </td>
                        </tr>
                    </table>

                </p>
               
                <ul class="bottom_menu" style="border-top: 1px solid #cccccc;padding-top:5px;margin-top:5px;">
                        <li></li>
                        <li style="padding:5px ; border:1px solid #cccccc; background:#f2f2f2;"><a href="ebay_system_temp_page_view.aspx?system_sku=<%= ebay_system_sku %>" target="_blank">展示模板</a></li>
                        
                </ul>
            </div>
            <div id="fragment-14">
                       <%
                            set rs = conn.execute("select       ep.id  "&_
                                                           "            , ep.comment_id   "&_
                                                           "            , ep.compatibility_parts"&_
                                                           "            , es.comment      "&_
                                                           "        from tb_ebay_system_parts ep inner join tb_ebay_system_part_comment es on es.id=ep.comment_id  "&_
                                                           "            where es.showit=1 and ep.system_sku=" & SQLquote(ebay_system_sku) &" order by priority asc  ")
                            if not rs.eof then
                                    response.write "       <i>请用逗号隔开，例： 1543,1544,1555   <br> 请勿用超过30个产品</i>  "
                                    response.write "       <form action='ebay_system_edit_save_compatibility_parts.asp' method='post'  target='iframe1'>   "
                                    response.write "               <table>                          "
                                    do while not rs.eof 
                                                response.write "               <tr>                  "
                                                response.write "                           <td>      "
                                                response.write "                                       <input type='hidden' name='id' value='"& rs("id") &"'     >  "
                                                response.write                                         rs("comment")
                                                response.write "                           </td>     "
                                                response.write "                           <td>      "
                                                response.write "                                       <input type='text' name='compatibility_parts' value='"& rs("compatibility_parts") &"' size='80' maxlength='200'>                 "
                                                response.write "                           </td>    "
                                                response.write "               </tr>                 "                                             
                                                
                                                
                                                
                                    rs.movenext
                                    loop          
                                     response.write "   </table>                        "     
                                     response.write "      <input type='submit' value='Save' />       "
                                     response.write "   </form>                         "                     
                            end if
                       
                      %> 
            </div>
            <div id="fragment-15">
                <% call GetEbayNumberList(system_sku) %>
            </div>
   </div>
<%
        response.write "<script>$(function() { $('#container-5').tabs({ fxSlide: true, fxFade: true, fxSpeed: 'normal' });});</script>"
else
        response.write "<script>$(function() { $('#container-5').tabs({ fxSlide: true, fxFade: true, fxSpeed: 'normal' });});$('#container').enableTab(2); $('#container').enableTab(3);</script>"
end if
     
     
        closeconn() %>
<iframe id="iframe1" name="iframe1" src="" style="width: 100px; height: 110px; " frameborder="0"></iframe>
<script type="text/javascript">
$(function() { $('#container-5').tabs({ fxSlide: true, fxFade: true, fxSpeed: 'normal' });});
$().ready(function(){
		$('td[title=keyword_comment]').css('text-align', 'right').css("font-weight", "bold").css("width", "120px").css("padding-right","10px");
		$('table[title=keyword_area]').find("div").css("float", "left").css("width", "120px");
		
		$('ul[title=shipping_charge]').css("width", "100%").find("li").css("float", "left");
		$('hr').css("border","0");
		$('div.title').css("padding", "5px").css("background", "#f2f2f2").css("border", "1px solid #cccccc").css("margin", "1em 0 1em 0");
		//$("ul[title=shipping_charge] li:first-child").css("width", "150px");
		
		$('#main_logo_area').load('/q_admin/inc/get_sys_logo_list.aspx', function(){
		    $('#main_logo_area div').hover(function(){
		        $(this).css("padding", "0").css("border", "1px solid #ccc");
		         
		    }
		    , function(){
		         $(this).css("border", "0px solid #ccc").css("padding", "1px");
		    }).css("cursor", "pointer").click(
		        function(){
		            var fv = $(this).find('img').attr("title");
		            var v = $('input[name=logo]').val();
		            if(v.length >4)
		            {
		                if(v.indexOf(fv)== -1)
		                {
		                    v = v +"|"+fv;
		                }
		            }
		            else
		                v =fv;
		           $('input[name=logo]').val(v);
		           
		           // view img
                    viewLogo();
		          
		        }
		    );
		});
		
		// view system comment to Label
		$('#system_comment_span').html( $('input[name=system_name]').val());
		
		// view logo img
		viewLogo();
});

function viewLogo()
{
   var v = $('input[name=logo]').val();
   var vh = "";
   if(v.length>4)
   {
       if(v.indexOf("|")==-1)
       {
            vh = vh + "<img src='/pro_img/logo/"+ v +"'>";
       }
       else
       {
            var s = v.split("|");
            
            for(var x=0; x<s.length; x++)
            {   
                vh = vh + "<img src='/pro_img/logo/"+ s[x] +"'>";
            }
       }
   }
   $('#main_logo_view').html(vh);
}

function SetKeywordValue(the)
{
	var n_v = '['+ the.value +']';
	if(the.checked)
	{
		var v = $('input[name=sys_keywords]').val();
		$('input[name=sys_keywords]').val(v+n_v);
		
	}
	else
	{
		var v = $('input[name=sys_keywords]').val();
		if(v.indexOf('['+ the.value +']')!=-1)
		{
			$('input[name=sys_keywords]').val(v.replace(n_v, ''));
		}
	}
	
}


function IssueSystem(sys_sku)
{
	$('#iframe1').attr('src', '/q_admin/ebayMaster/ebay_system_edit_issue.asp?sys_sku='+ sys_sku)
}


function OpenDuplicateSys(sys_sku)
{
    if(confirm('are you sure'))
    $('#iframe1').attr("src", '/q_admin/ebayMaster/ebay_system_Duplicate_sys.asp?sys_sku='+ sys_sku +'&category_id=<%=category_id %>')
}


function ValidPost()
{
    $('input[name=luc_part_name]').each(function(){
        var v = $(this).val();
        if(v.indexOf(',') != -1)
        {
            v = v.replace(/,/gi, '[qiozi-comma]');
        }
        $(this).val(v);
    });
    return true;
}
</script>

</body>
</html>
