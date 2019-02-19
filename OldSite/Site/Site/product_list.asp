<!--#include virtual="site/inc/inc_page_top.asp"-->
<table class="page_main" cellpadding="0" cellspacing="0">
	<tr>
    	<td  id="page_main_left" valign="top" style="padding-bottom:5px; padding-left: 2px" class='page_frame'>
        	<!-- left begin -->
            	<!--#include virtual="site/inc/inc_left_menu.asp"-->
            <!-- left end 	-->
    	</td>
    	
        <td id="page_main_center" valign="top" class='page_frame'>
        	<!-- main begin -->
        	    <div id="page_main_banner"></div>
        	    <div class='page_main_nav'><%= FindNav(  SQLescape(request("cid")),2 , "part_product") %></div>
            	<div id="page_main_area"> 
                <table height="750" width="100%" border="1" cellpadding="0" cellspacing="2" bgcolor="#FFFFFF" style="border:#8FC2E2 1px solid;" align="center">
                    <tr>
                      <td style="border:#E3E3E3 1px solid;  padding-top:0px;" valign="top">
                            <div id="page_main_filter_area"></div>
                            <div id="page_main_prod_list_area">
                            	<div style="line-height:100px;text-align:center;">Loading...</div>
                            </div>
                      </td>
                    </tr>
                  </table>              
             	</div>
            <!-- main end 	-->
        </td>
       
        <td valign="bottom" id="page_main_right_backgroundImg" style="border-left: 1px solid #8E9AA8"><img src="/soft_img/app/left_bt.gif" width="14" height="214"></td>
    </tr>
</table>
<span id='search_keywords' style="display:none"><%= request("keywords") %></span>
<!--#include virtual="site/inc/inc_bottom.asp"-->
<%
	if SQLescape(trim(request("page"))) = "" then 
		page=1
	else
		page = SQLescape(request("page"))
	end if
	if SQLescape(trim(request("sortby"))) = "" then 
		sortby=2
	else
		sortby= SQLescape(request("sortby"))
	end if
	
	if SQLescape(trim(request("class"))) = "" then 
		classs=1
	else
		classs= SQLescape(request("class"))
	end if
	if SQLescape(trim(request("page_category"))) = "" then 
		page_category=0
	else
		page_category= SQLescape(request("page_category"))
	end if

	'
	'	query ebay number.
	'
	Dim ebay_number 	:		ebay_number 	=	SQLescape(request("number"))
	if ebay_number <> "" then
	
	
	end if
	

	Dim search_keywords, scarchKeyword2, searchCid, viewStock
    viewStock = 0
    
    search_keywords = Session("search_keywords")

    if instr(search_keywords, "=")>0 then 
        searchCid = left(search_keywords, instr(search_keywords, "=")-1)
        if searchCid <> request("cid") then 
            Session("search_keywords") = ""
            search_keywords = ""
        else
            search_keywords = right(search_keywords, len(search_keywords) - instr(search_keywords, "="))
            if len(search_keywords)>0 then
                if right(search_keywords, 1) = "=" then 
                    search_keywords = ""
                    Session("search_keywords") = ""
                end if
            end if
        end if
    end if
        
    'response.Write search_keywords 

    if(search_keywords="") then
        search_keywords = SQLescape(request("keywords"))
	end if
    scarchKeyword2 = search_keywords


    if len(search_keywords) >0 then 
		if instr(search_keywords, "|")>0 then 
			search_keywords = replace(search_keywords, "|", "[qiozi]")
		end if
	end if
     ' response.Write "<br>"&  search_keywords 

      if instr(search_keywords, "[qiozi]") > 0 then 
        scarchKeyword2 = replace(search_keywords, "[qiozi]", "|")
      end if

      set rs = conn.execute("select ifnull(sum(is_display_stock),0) from tb_product_category where menu_child_serial_no='"& SQLescape(request("cid")) &"'")
      if not rs.eof then
        viewStock = rs(0)
      end if
      rs.close : set rs = nothing
%>
<script type="text/javascript">
$().ready(function(){
	
		if(<%= len(search_keywords) %> > 0 )
		{
            $('#page_main_prod_list_area').load("/site/inc/inc_list_sys.asp"
                , {
                    "keywords":"<%= search_keywords %>"
                    ,"class":"<%= classs %>"
                    , "page_category":"<%= page_category %>"
                    , "cid":"<%= request("cid") %>"
                    , "page":"<%= page %>"
                 }		
				, function(){
				$('#page_main_filter_area').load('/site/inc/inc_get_prod_filter_area.asp?cid=<%= request("cid") %>'
			    	, function(){					
			        	MatchKeywords('<%= SQLescape(search_keywords) %>');
			    				});
                  getStock();
					
				});				
		} 
		else
		{			
			$('#page_main_prod_list_area').load('/site/inc/inc_list_sys.asp?class=<%= classs %>&page_category=<%= page_category %>&cid=<%= request("cid") %>&page=<%= page %>'
			, function(){
                 $('#page_main_filter_area').load('/site/inc/inc_get_prod_filter_area.asp?cid=<%= request("cid") %>');
                 	
                    getStock();
              });		
		}

});

function getStock(){
    if(<%= viewStock %>>0){
        $('.stockstatus').each(function(){
            var the = $(this);
            $.ajax({
                type:"get",
                url:"inc/inc_get_stock_string.asp",
                data:"stock="+the.attr("stock")+"&lu_sku="+the.attr("sku"),
                error:function(){},
                success:function(m,s){
                        if(s=="success")
                        the.html(m);
                             
                }                                                  
            });
        });
    }
}


function press_category_keyword(v, store_elm_id)
{
	showLoading();
	
	$('#'+ store_elm_id).val(v);
	var vs = '';
	var u = "";
	var keywords = "";
	$('input[name=category_query_keyword]').each(function(i){vs+= '[qiozi]' + $(this).val(); });
	if(vs.length>6)
		keywords	=	vs.substr(7, vs.length-6);
	$('#'+ store_elm_id+' ~ a').each(function(i){ 
        
		if($.trim($(this).html().replace(/,/ig, '')) == v.replace(/,/ig, '')) 
		{
			$(this).attr("class", "selected") ;
		}
		else 
		{
			$(this).attr("class", "unselected") ;
		}
	});

	MatchKeywords($('#search_keywords').html());

	keywords += "[qiozi]" + $('#search_keywords').html().replace('|', '[qiozi]');
	keywords = keywords.replace("|","[qiozi]");
	//alert(keywords);
	//alert($('#page_main_prod_page_v').val());
    
	$('#page_main_prod_list_area').load('/site/inc/inc_list_sys.asp',
        {
			'page':$('#page_main_prod_page_v').val()
			,'keywords':keywords 
			,'searchKeywords':  $('#search_keywords').html()
			,'sort_by':$("#sort_by").val()
			,'class':<%= classs %>
			,'cid':<%= request("cid") %>
			,'price_area':$('#category_query_keyword_price').val()
			,'stock':$('#category_query_keyword_stock').val()
		}
		, function(){ closeLoading();}
	);

}


function MatchKeywords(keywords){

    keywords = keywords.replace(/\[qiozi\]/g, "|");
    var d="";
	var ks = keywords.split('|');
	var sK = $('#search_keywords').html();
	var sKs = sK.split('|');
	var newSearchKey = "";
			
	for(var j=0;j<sKs.length; j++)
	{
		var exist = false;
		$('a[name=part_key]').each(function(){
		
			if($(this).html().replace(/,/g,'').toLowerCase() == sKs[j].toLowerCase())
			exist =true;
		});
		if(exist == false)
			newSearchKey += "|"+ sKs[j];
	}
			
    $('a[name=part_key]').each(function(){				
		for(var i=0; i<ks.length; i++)
		{
            if($(this).html().replace(/,/g,'').toLowerCase() == ks[i].toLowerCase())
            {               
                // un selected Class
                $(this).parent().find('a').each(
                    function(){ 
                        if($(this).attr('class') == 'selected')
                            $(this).attr('class', 'unselected'); 
                    }
                );
                
                // store Keyword.
                $(this).parent().find('input').each(
                    function(){
                        if($(this).attr('name') == 'category_query_keyword')
                        {   
                            $(this).val(ks[i]);                        
                        }
                    }
                );
                
                // set selected Class
                $(this).attr('class',"selected");                
            }
		}
    });
	$('#search_keywords').html(newSearchKey.substring(1));
}

</script>