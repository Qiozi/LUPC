<!--#include virtual="site/inc/inc_helper.asp"-->
<%
    
    
    Dim Id  :   Id  =   SQLescape(Request("id"))
    Dim ebay_system_number :    ebay_system_number = SQLescape(Request("id"))
    
    Dim system_price        :   system_price    =   null
    Dim compatibility_parts :   compatibility_parts =   null
    
    Set rs = conn.execute("select id,	category_id, ebay_system_name, ebay_system_price, ebay_system_current_number"   &_
                          " ,showit"    &_
                          " from tb_ebay_system"    &_
                          " where id='"& id &"' or ebay_system_current_number='"& id &"' ")
                          
    If not rs.eof Then
        if rs("showit") <> 1 then
            Response.write "<div style=""border:#E3E3E3 1px solid; ""><div style='line-height: 400px;'>No Match Data </div></div>"
            Response.End()
        end if
        id              	=   rs("id")
        ebay_system_number 	= 	rs("ebay_system_current_number")
        system_price    	=   rs("ebay_system_price")
    End if         
    Set crs = conn.execute("select es.id, luc_sku, product_ebay_name, epc.comment, is_case "&_
                                            " ,es.part_quantity"   &_
                                            " ,es.max_quantity"    &_
                                            " ,compatibility_parts"&_
                                            " ,case when p.other_product_sku>0 then p.other_product_sku else luc_sku end as img_sku" &_
                                            " ,epc.priority"    &_
                                            " ,epc.id comment_id"   &_
                                            " from tb_product p "&_
                                            " inner join tb_ebay_system_parts es on p.product_serial_no=es.luc_sku "&_
                                            " inner join tb_ebay_system_part_comment epc on epc.id=es.comment_id "&_
                                            " where es.system_sku='"& id &"' order by epc.priority asc ")
 %>
<div style="border:#E3E3E3 1px solid;padding: 0px 0px 30px 0px;">
        <table cellpadding="0" cellspacing="0" >
            <tr>
                    <td width="50%" valign="top">
                            
                            <%
                                if not crs.eof then
                                    Do while not crs.eof 
                                        if (crs("is_case") = 1 ) then
                                            WriteSystemBigImg(crs("luc_sku"))                                        
                                        end if
                                    crs.movenext
                                    loop
                                    crs.movefirst                                
                                end if 
                            %>
                    </td>
                    <td valign="top">
                           <table title="system_top_area" class="table_td_ccc" cellspacing="1" cellpadding="3">
                                <tr>
                                        <td style="text-align:left"><b>Now Low Price:</b></td>
                                        <td style="text-align:right" ><span title='ebay_system_sell'><%= ConvertDecimalUnit(Current_System, system_price)  %></span></td>
                                </tr>
                                <tr>
                                        <td style="text-align:left"><b>Quote Number:</b></td><td>Press here to obtain</td>
                                </tr>
                           </table> 
                           <p style="padding:10px auto;text-align:left; COLOR: #4C4C4C;">
                                The system you customize below will be fully assembled and tested before delivery.
                           </p>
                           <p style="padding:10px auto;text-align:left; COLOR: #4C4C4C;">
All components are brand new.
                           </p>
                           
                           <div id='ebay_customize_system_price_area2' style="margin-top:20px;"></div>
                    </td>
            </tr>                
        </table>
        
        <!--    parts area  Begin -->
        <div  style="background-image:url(/soft_img/app/customer_bottom_03.gif);line-height: 25px; width: 180px;color:White;margin-top: 5px;"><b>Major  Components</b></div>
        <form action="/ebay/BuyS.asp" method='post' id="form1" name="form1">
        <input type="hidden" name="is_view_system" value="0" />
        <input type="hidden" name="cmd" value="customize" />
        <input type="hidden" name="system_sku" value="<%= id %>" />
        <input type="hidden" name="number" value="<%= ebay_system_number %>" />
        <input type="hidden" name="ebay_system_sell" value="<%= ConvertDecimal(system_price) %>" />        
        <table width="100%" cellpadding="0">    
                <%
                if not crs.eof then
                    
                    Dim luc_sku_price       :       luc_sku_price=0
                    Dim luc_sku_cost        :       luc_sku_cost = 0
                    Do while not crs.eof 
                        Response.write "<tr>"       &vblf
                        Response.write "    <td colspan=""2"" class='customize_part_comment' style='background:#ffffff;border-bottom: 1px solid #ccc;'>"      &vblf
                        Response.write "        <ul width='100%'>"   &vblf
                        Response.write "            <li style='width: 90px;float:left;color:white; font-weight:bold;line-height: 16px; overflow:hidden;' class='comment'>"       &vblf
                        Response.write "                <span>"       &vblf                        
                        Response.write                      crs("comment")            
                        Response.write "                </span>"    &vblf
                        Response.write "            </li>"  &vblf
						Response.write "        <input type='hidden' name='product_name' id='product_name_"& crs("id") &"' value='"& crs("product_ebay_name") &"'/>"       &vblf
						Response.write "        <input type='hidden' name='cate_name' value='"& crs("comment")  &"'/>"       &vblf
                        Response.write "        <input type='hidden' name='comment_id' value='"& crs("comment_id") &"'/>"       &vblf
                        Response.write "        <input type='hidden' name='priority' value='"& crs("priority") &"'/> "               &vblf
                        Response.write "        <input type='hidden' name=""part_price"" style='color:red;' id='current_price_"& crs("id") &"' "     &vblf
                        Response.write "        />"    &vblf
                        Response.write "        <input type='hidden' name=""part_sku"" style='color:blue;' id='current_luc_sku_"& crs("id") &"' "    &vblf
                        Response.write "        value='"& crs("luc_sku") &"'"
                        Response.write "        />"    &vblf
                        Response.write "        <input type='hidden' name=""part_quantity"" style='color:blue;' id='current_luc_sku_quantity_"& crs("id") &"' "    &vblf
                        Response.write "        value='"& crs("part_quantity") &"'"
                        Response.write "        />"    &vblf
                        Response.write "            <li class='ebay_comment_part_title'  style='width: 462px;float:left; background:#ffffff;padding: 2px;'>"       &vblf 
                        Response.write "                <span id=""ebay_selected_part_quantity_"& crs("id") &""" "
                        if crs("part_quantity")<=1 then 
                                Response.write " style=""display:none;color: blue;"" "
                        else
                                Response.write " style="" color: blue; "" "
                        end if
                        Response.write "                >"& crs("part_quantity") &"X</span>"  &vblf
                        Response.write "                <span title='current_parts_title' id='current_parts_title_"& crs("id") &"'>"     &vblf
                        Response.write              crs("product_ebay_name")  
                        Response.write "                </span>"    &vblf
                        Response.write "            </li>"  &vblf   
                        Response.write "    </td>"       &vblf
                        Response.write "</tr>"
                        
                        
                        Response.write "<tr  class='customize_part_name'>"       &vblf
                        Response.write "    <td width=""50"" >"       &vblf
                        Response.write "        <div class='customize_part_logo'>"  &vblf   
                        Response.write "            <img src="""& GetImgMinURL(HTTP_PART_GALLERY , crs("img_sku")) &""" width=""50"" border=""0"" />"  &vblf
                        Response.write "        </div>" &vblf
                        Response.write "    </td>"       &vblf
                        Response.write "    <td>"       &vblf
                        Response.write "        <table width=""100%"" cellspacing=""0"" cellpadding=""0"">"    &vblf
                        Response.write "            <tr>"   &vblf
                        Response.write "                <td  style=""text-align:right;"" "
                        if crs("max_quantity") > 1 then
                            Response.write " width='70' "
                        else
                            Response.write " width='30' "
                        end if
                        Response.write ">"   &vblf
                        Response.write "                        <input type='radio' name='part_selected_"& crs("id")&"' "
						Response.write " id='part_selected_"& crs("id") &"_"&crs("luc_sku")&"'"
						REsponse.write " value='part_selected_"& crs("id") &"_"&crs("luc_sku")&"' "
						Response.write " checked='true' onclick='EbayCustomizeChangeParts("""& crs("id") &""", """& crs("luc_sku") &""");'  />"  &vblf
						
                        if crs("max_quantity") >1 then 
                            Response.write "            <select id='part_quantity_"& crs("id") &"_"& crs("luc_sku")&"' title='part_quantity'  onchange='EbayPartQuantityChange(this, """ &crs("id")& ""","""& crs("luc_sku") &""")'>"      &vblf
                            for x = crs("part_quantity") to crs("max_quantity")
                                Response.write "<option value='"& x &"' "
                                if(x = crs("part_quantity")) then Response.write " selected='true' "
                                Response.write " >"& x &"x</option>" &vblf  
                            next
                            Response.write "            </select>"      &vblf    
                        else
                            Response.write "            <select id='part_quantity_"& crs("id") &"_"& crs("luc_sku")&"' title='part_quantity' onchange='' style='display:none;'>"      &vblf
                            Response.write "<option value='1' selected='true'>1x</option>" &vblf        
                            Response.write "            </select>"      &vblf   
                        end if
                        Response.write "                </td>"  &vblf
                        Response.write "                <td id='parts_title_"& crs("id") &"_"& crs("luc_sku") &"' title='parts_title'>"   &vblf
                        Response.write                       crs("product_ebay_name") 
                        Response.write "                </td>"  &vblf 
                        Response.write "                <td width=""60"" title='parts_price' noWrap=""noWrap"">"   &vblf                       
                        
                        Response.write "<span id='current_price_"& crs("id") &"_"& crs("luc_sku") &"'  title='current_price_"& crs("id") &"' style='display:none;'>0</span>"
                        Response.write "<span title='current_price_"& crs("id") &"_"& crs("luc_sku") &"' >"& ConvertDecimalUnitSystemCustomize(Current_System,  0) &"</span>"
                        
                        Response.write "            </td>"  &vblf
                        Response.write "            </tr>"  &vblf
                        
                        if SQLescape(crs("compatibility_parts")) = "" then
                            '  无可选产品，取price
                            Set grs = conn.execute("select round(product_current_price-product_current_discount, 0) sell "&_
                                                   " ,round(product_current_cost,0) cost"&_
                                                   "  from tb_product "&_
                                                   "    where product_serial_no='"& crs("luc_sku") &"'")
                            if not grs.eof then                               
                                Response.write "<script>$('#current_price_"& crs("id") &"').val('"& grs("sell") &"');</script>"   &vblf   
                            end if
                            grs.close : set grs = nothing
                        else
                            compatibility_parts = SQLescape(crs("compatibility_parts"))
                            if instr(compatibility_parts, "|")>0 then
                                compatibility_parts = replace(compatibility_parts, "|", ",")
                            end if
                            
                            Set grs = conn.execute("select round(product_current_price-product_current_discount, 0) sell "&_
                                                   " ,round(product_current_cost,0) cost"&_
                                                   "  from tb_product "&_
                                                   "    where product_serial_no='"& crs("luc_sku") &"'")
                            if not grs.eof then
                                luc_sku_price   =   grs("sell")
                                luc_sku_cost    =   grs("cost")
                            end if                            
                            grs.close : set grs = nothing
                            
                            
                            
                            Set srs = conn.execute("Select product_serial_no, other_product_sku "&_
                                                   " , case when ifnull(product_ebay_name,'')='' and  ifnull(product_name_long_en,'')<>'' then product_name_long_en when ifnull(product_name_long_en,'')='' then product_name  else product_ebay_name end as  product_ebay_name"    &_
                                                   " ,round(ifnull(product_current_price-product_current_discount-"& luc_sku_price &", 0),0) diff_price"  &_
                                                   " ,round(ifnull(product_current_cost-"& luc_sku_cost &", 0),0) diff_cost"  &_
                                                   " from tb_product "&_
                                                   " where tag=1 and split_line=0 and product_serial_no in ("& compatibility_parts &")")
                            if not srs.eof then
                                    if cdbl(srs("diff_price")) >= cdbl(srs("diff_cost")) then
                                        Response.write "<script>$('#current_price_"& crs("id") &"').val('"& luc_sku_price &"');</script>"   &vblf
                                    else
                                        Response.write "<script>$('#current_price_"& crs("id") &"').val('"& luc_sku_cost &"');</script>"   &vblf
                                    end if
                                    do while not srs.eof
                                        Response.write "        <tr>"   &vblf
                                        Response.write "            <td style=""text-align:right;"">"   &vblf
                                        Response.write "                   <input type='radio' name='part_selected_"& crs("id")&"' id='part_selected_"& crs("id") &"_"&srs("product_serial_no")&"' value='part_selected_"& crs("id") &"_"&srs("product_serial_no")&"'  onclick='EbayCustomizeChangeParts("""& crs("id") &""", """& srs("product_serial_no") &""");'  />" &vblf
                                        if crs("max_quantity") >1 then 
                                            Response.write "            <select id='part_quantity_"& crs("id") &"_"& srs("product_serial_no")&"' title='part_quantity' onchange='EbayPartQuantityChange(this, """ &crs("id")& ""","""& srs("product_serial_no") &""")'>"      &vblf
                                            for x = crs("part_quantity") to crs("max_quantity")
                                                Response.write "<option value='"& x &"'>"& x &"x</option>" &vblf  
                                            next
                                            Response.write "            </select>"      &vblf    
                                        else
                                            Response.write "            <select id='part_quantity_"& crs("id") &"_"& srs("product_serial_no")&"' title='part_quantity' onchange='' style='display:none;'>"      &vblf
                                            Response.write "<option value='1'>1x</option>" &vblf        
                                            Response.write "            </select>"      &vblf   
                                        end if
                                        Response.write "            </td>"  &vblf
                                        Response.write "            <td id='parts_title_"& crs("id") &"_"& srs("product_serial_no") &"'  title='parts_title'>"   &vblf
                                        Response.write   srs("product_ebay_name") 
                                        Response.write "            </td>"  &vblf
                                        Response.write "            <td title='parts_price' noWrap=""noWrap"">"   &vblf
                                        if cdbl(srs("diff_price")) >= cdbl(srs("diff_cost")) then
                                            Response.write "<span id='current_price_"& crs("id") &"_"& srs("product_serial_no") &"'  title='current_price_"& crs("id") &"' style='display:none;'>"& ConvertDecimal(cdbl(srs("diff_price"))) &"</span>"
                                            Response.write "<span title='current_price_"& crs("id") &"_"& srs("product_serial_no") &"' >"& ConvertDecimalUnitSystemCustomize(Current_System,  crs("part_quantity")*cdbl(srs("diff_price"))) &"</span>"
                                        else
                                            Response.write "<span id='current_price_"& crs("id") &"_"& srs("product_serial_no") &"'  title='current_price_"& crs("id") &"' style='display:none;'>"& ConvertDecimal(cdbl(srs("diff_cost"))) &"</span>"
                                            Response.write "<span title='current_price_"& crs("id") &"_"& srs("product_serial_no") &"' >"& ConvertDecimalUnitSystemCustomize(Current_System, crs("part_quantity")* cdbl(srs("diff_cost"))) &"</span>"
                                        end if
                                        Response.write "            </td>"  &vblf
                                        Response.write "        </tr>"  &vblf
                                    srs.movenext
                                    loop
                        
                            end if
                            srs.close : set srs = nothing
                              
                        end if                      
                        Response.write "        </table>"   &vblf            
                        Response.write "    </td>"       &vblf
                        Response.write "</tr>" 
                        

                        
                    crs.movenext
                    loop
                                       
                end if
                crs.close : set crs = nothing
                %>          
                
        </table>
        </form>
        <!--    parts area  End     -->
        <hr size="1" style="color: #ccc"/>
        <div title="ebay_system_sell" style="text-align:right; clear:both; line-height: 30px;color:#ff9900; font-size:9pt;padding-right: 10px;">
           <b>Updated Price:&nbsp;&nbsp;&nbsp;&nbsp;</b><span title="ebay_system_sell"><%=  ConvertDecimalUnit(Current_System, system_price)  %></span>
        </div>
        <div id='ebay_customize_system_price_area1'>
            
            <table width="115" height="24" border="0" cellpadding="0" cellspacing="0"  style="float:right; margin-right: 10px;">
                  <tr>
                    <td width="28"><img src="/soft_img/app/buy_car.gif" width="28" height="24" alt="" /></td>
                    <td class="btn_middle"><a title="buy" class="btn_img"><strong>Buy It</strong></a> </td>
                    <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt="" /></td>
                  </tr>
            </table>
            <table width="155" height="24" border="0" cellpadding="0" cellspacing="0" style="float:right; margin-right: 10px;">
                <tr>
                  <td width="28"><img src="/soft_img/app/Review.gif" width="28" height="24" alt=""></td>
                  <td class="btn_middle"><div id="view_system_print"><a onClick="viewSystemSubmit(1);" class="btn_img"><strong>System Review</strong></a></div></td>
                  <td width="6"><img src="/soft_img/app/customer_bottom_04.gif" width="6" height="24" alt=""></td>
                </tr>
            </table>
        </div>
        
                            
        
        <br style="clear:both;"/><br />
        <p style='clear:both;font: 8pt "tahoma"; COLOR: #4C4C4C; letter-spacing:0px; border-top: 1px solid #cccccc;text-align:left; padding:5px'>Prices, system package content and availability subject to change without notice. 
        </p>
        <p style='FONT: 8pt "tahoma"; COLOR: #4C4C4C; letter-spacing:0px; text-align:left; padding-left:5px'>Please read our FAQ for answers to most commonly asked questions. Any textual or pictorial information pertaining to products serves as a guide only. Lu Computers will not be held responsible for any information errata.</p>


</div> 
<DIV id="float_price" style='width:172px;height: 200px; text-align:left;border: 0px solid #ccc;'>
              <table width="172" border="0" cellpadding="0" cellspacing="0"  style="background: url('/soft_img/app/fly_bg.gif') no-repeat;">
                  <tr>
                    <td height="260" valign="top"><table width="166" border="0" align="center" cellpadding="0" cellspacing="0">
                      <tr>                
                        <td height="30">&nbsp;</td>
                      </tr>
                      <tr>
                        <td style="border:#FDFBFA 1px solid; line-height:15px" height="30" bgcolor="#EFDACD">
                        	<table width="160" border="0" align="center" cellpadding="0" cellspacing="0">
                          <tr>
                            <td><span title="ebay_system_sell"><%=  ConvertDecimalUnit(Current_System, system_price)  %></span> Price</span></td>
                          </tr>  
                        </table></td>
                      </tr>
                      <tr>
                        <td height="5"></td>
                
                      </tr>
                      <tr>
                        <td>                         
                          <table width="162" border="0" align="center" cellpadding="1" cellspacing="1">
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a href="<%= LAYOUT_HOST_URL %>ask_question.asp?cate=sys&type=2&change=true" style="cursor:pointer" class="white-hui-11"  onClick="return js_callpage_cus(this.href, 'question', 602, 450);">Ask seller a question</a></td>
                            </tr>
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a class="btn_img" onClick="viewSystemSubmit(1);">View my customized system</a>
                              </td>
                            </tr>
                
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a class="btn_img" onClick="viewSystemSubmit(2);">Print this customized system</a></td>
                            </tr>
                            <tr>
                              <td style="border:#D69357 1px solid;" bgcolor="#CC792F">&nbsp;<a class="btn_img" onClick="viewSystemSubmit(3);">Email this customized system</a></td>
                            </tr>
                          </table></td>
                      </tr>
                
                      <tr>
                        <td height="1"></td>
                      </tr>
                      <tr>
                        <td>
                        <table width="160" border="0" align="center" cellpadding="0" cellspacing="0">
                 		  <tr>
                            <td height="1"></td>
                          </tr>
                          <tr>
                            <td style="padding-left:6px;" class="text_white_11"><a title="buy" class="btn_img">Check shipping cost & total</a></td>
                          </tr>
                          
                        </table></td>
                      </tr>
                      <tr>
                        <td height="35" align="center"><a title="buy"><img src="/soft_img/app/fly_add.gif" width="130" height="17" border="0"/></a></td>
                      </tr>
                
                      
                    </table></td>
                  </tr>
                </table>
</DIV>

 
<script type="text/javascript">
    
    
    function EbayCustomizeChangeParts(ebay_part_id, current_luc_sku){
        var els = document.getElementsByTagName("SPAN");
        var current_price_html 	= $('#current_price_'+ ebay_part_id).val()
        var current_price 		= parseFloat(current_price_html);
        var new_current_price 	= parseFloat($('#current_price_'+ebay_part_id+'_'+ current_luc_sku).html());
        // total (sell)
        var ebay_system_sell 	= parseFloat($('input[name=ebay_system_sell]').val());          
        
        
        var current_quantity 	= parseInt($("#current_luc_sku_quantity_"+ebay_part_id).val(), 10);
        var new_current_quantity = parseInt($("#part_quantity_"+ebay_part_id+"_"+ current_luc_sku).val(), 10);
        new_current_price = parseFloat((new_current_price+current_price).toFixed(2));
        
        var ebay_sell = parseFloat((ebay_system_sell + (new_current_price * new_current_quantity - current_quantity* current_price)).toFixed(2));
        $('input[name=ebay_system_sell]').val(ebay_sell);
        
        for(var i=0; i<els.length; i++)
        {
            if(els[i].id.indexOf('current_price_'+ebay_part_id+'_')!= -1)
            {
                var c_price = parseFloat($("#"+els[i].id).html());  
                var c_part_quantity = parseInt($("#"+els[i].id.replace("current_price_", "part_quantity_")).val(),10);
                
          
                c_price = parseFloat((c_price + current_price - new_current_price).toFixed(2)); 
                $("#"+els[i].id).html(
                    c_price
                 );
                 
                
                var cha_price = ((c_price+ new_current_price)*c_part_quantity- new_current_price * new_current_quantity).toFixed(2);
				//alert(cha_price);
                if (((c_price+ new_current_price)*c_part_quantity- new_current_price * new_current_quantity)<0)
                {
                    $('span[title='+els[i].id+']').html("<span style='color: green;' class='price2'>-$"+ cha_price + "</span><span class='price_unit'><%= Current_Currency_Unit(current_system) %></span>");
                }
                else
                    $('span[title='+els[i].id+']').html("<span title='price' class='price1'>+$"+ cha_price  + "</span><span class='price_unit'><%= Current_Currency_Unit(current_system) %></span>");
                
                //alert($('span[title='+els[i].id+']').html());
            }
        }
        if(new_current_quantity>1 ) {
		
            $("#ebay_selected_part_quantity_"+ebay_part_id).css("display", "").html(new_current_quantity +"X");
			
			//var new_part_name = $("#parts_title_"+ebay_part_id+"_"+ current_luc_sku).html();
//            $("#current_parts_title_"+ebay_part_id).html( new_part_name );
//			$("#product_name_"+ ebay_part_id).html( new_part_name );
        }
		//
		//	set name
		var new_part_name = $("#parts_title_"+ebay_part_id+"_"+ current_luc_sku).html();
		$("#current_parts_title_"+ebay_part_id).html( new_part_name );
		$("#product_name_"+ ebay_part_id).val( new_part_name );
		
        
        $("#current_luc_sku_quantity_"+ebay_part_id).val(new_current_quantity);
        $('#current_price_'+ ebay_part_id).val(
                        new_current_price
                        );
        $('#current_luc_sku_'+ ebay_part_id).val( current_luc_sku );
        $('span[title=ebay_system_sell]').html("<span class=\"price\">$"+ ebay_sell +"</span><span class=\"price_unit\"><%= Current_Currency_Unit(current_system) %></span>");
		
    }
    
    function EbayPartQuantityChange(the, ebay_part_id, current_luc_sku){

        if($('#part_selected_'+ebay_part_id+"_"+current_luc_sku).attr("checked")){
            EbayCustomizeChangeParts(ebay_part_id, current_luc_sku);
        }
        else{
            var current_price_html 	= $('#current_price_'+ ebay_part_id).val()
            var current_price 		= parseFloat(current_price_html);
            var current_quantity 	= parseInt($("#current_luc_sku_quantity_"+ebay_part_id).val(), 10);
            
            var new_current_quantity = parseInt($("#part_quantity_"+ebay_part_id+"_"+ current_luc_sku).val(), 10);
            
            var new_current_price 	= parseFloat($('#current_price_'+ebay_part_id+'_'+ current_luc_sku).html());
            
            var new_price 			= (new_current_price + current_price) * new_current_quantity -(current_price * current_quantity);
            if(new_price >=0)
            {
                $('span[title=current_price_'+ebay_part_id+'_'+ current_luc_sku+']').html("<span title='price' class='price1'>+$"+  new_price + "</span><span class='price_unit'><%= Current_Currency_Unit(current_system) %></span>");
                
            }
            else
            {
                $('span[title=current_price_'+ebay_part_id+'_'+ current_luc_sku+']').html("<span style='color: green;' class='price2'>-$"+ new_price + "</span><span class='price_unit'><%= Current_Currency_Unit(current_system) %></span>");
            }
        }
    }
    

    $().ready(function(){
		$("#float_price").floatdiv("customize");		
		$('#ebay_customize_system_price_area2').html($('#ebay_customize_system_price_area1').html()).css("text-align","right");
		$('#ebay_customize_system_price_area2>table').css('float','none').css("width", "150px").css("margin", "5px 10px auto auto");
		
		$("div.big_btn_imgs").css("text-align","left").css("padding", "5px").css("background","#ffffff");
		$("div.big_btn_imgs>img").css("margin", "2px");
		$("table[title=system_top_area]").css("width","100%");
		$("table[title=system_top_area]>td").css("text-align","left");
		$("td[title=parts_price]>span").css("font-size", "8pt");
		$("td[title=parts_price]").css("text-align", "right");
		$("li.ebay_comment_part_title").css("color", "#466B90");
		
		$("td[title=parts_title]").css("color","#7490AC");
		$("select[title=part_quantity]").css("font-size", "7.5pt").css("color","#5D7D9E");
		$('li.comment').css("background", "url(/soft_img/app/filter_title.gif)  no-repeat").css("padding-left", "5px").find("span").css("overflow", "hidden");
		
		$("a[title=buy]").css("cursor","pionter").click(function(){
			
				$("#form1").submit();
		});
	});
	
	
	function viewSystemSubmit(s)
	{
		$('input[name=is_view_system]').val(s);
		$('#form1').attr("target","iframe1");
		$('#form1').submit();
		$('input[name=is_view_system]').val("0");
		$('#form1').attr("target","");
	}
</script>