<!--#include virtual="site/inc/inc_page_top.asp"-->
<%
	
	CAll CurrentSystemDefault("site", CurrentIsEbay)
%>

<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top"  class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class='page_main_nav' id="page_main_nav"><%= FindNav(SQLescape(Request("cid")),2 , "part_product") %></div>
            	<div id="page_main_area">
                		<%
						dim keyword, menu_child_serial_no, parent_category, parent_name
						dim product_lists
						parent_name = ""
						parent_category = SQLescape(request("class"))
						menu_child_serial_no = SQLescape(Request("cid"))
                     
						%>
                        <table width="600" border="0" align="center" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid; ">
                        <tr>
                          <td style="border:#E3E3E3 1px solid;  padding-top:0px; height: 750px" valign="top">
                            <%
                            if isnumeric(menu_child_serial_no) then 
                                set rs = conn.execute("select * from tb_product_category where tag=1 and menu_child_name <> 'Warranty' and menu_pre_serial_no="& SQLquote(menu_child_serial_no)&" order by menu_child_order asc ")
                                if not rs.eof then
                                    do while not rs.eof 
                                        if rs("menu_is_exist_sub") = 0 then 
                                            response.write "<table width='100%' cellpadding='4' cellspacing='1'><tr><td width='6%' align='center' bgcolor='#eeeeee'><img src='/soft_img/app/arrow_3.gif'></td><td style='padding-left:5px;' bgcolor='#eeeeee'><a href="""& LAYOUT_HOST_URL &"product_list.asp?page_category="&rs("page_category")&"&class="&request("class")&"&cid="&rs("menu_child_serial_no")&""">" & rs("menu_child_name") & "</a></td></tr></table>"
                                        else
                                            response.write "<table width='100%' cellpadding='4' cellspacing='1'><tr><td width='6%' align='center' bgcolor='#eeeeee'><img src='/soft_img/app/arrow_9.gif'></td><td style='padding-left:5px;' bgcolor='#eeeeee'>" & GetHrefByProductCategory2(rs("menu_child_serial_no"), rs("menu_child_name")) &  "</td></tr></table>"
                                        end if
                                    rs.movenext
                                    loop
                                end if
                                rs.close : set rs = nothing
                                
                            end if
                            %>
                          </td>
                        </tr>
                      </table>
                </div>
            <!-- main end 	-->
        </td>
        <td id="page_main_right" valign="top" class='page_frame'>
        	<!-- right begin -->                   	
            	<div id="page_main_right_html"><!--#include virtual="/Site/inc/inc_right.asp"--></div>
            <!-- right end 	-->
        </td>
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>

<!--#include virtual="site/inc/inc_bottom.asp"-->
<script type="text/javascript">
$().ready(function(){
	//$('#page_main_area').load('/site/inc/inc_default.asp?'+rand(1000));
});
</script>