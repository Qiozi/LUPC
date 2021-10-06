<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include virtual="q_admin/ebayMaster/ebay_inc.asp"-->
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<%= LAYOUT_CSS_FILES_BACK 	%>
<%= LAYOUT_SCRIPT_FILES 	%>
<%= LAYOUT_LINK_FILES 		%>
<title>part keywords</title>
<style>
.s_keyword{cursor:pointer;}
.s_keyword span:hover{ color:#ff6600;}
</style>
</head>

<body>
	<% 
		Dim cid				:	cid			=	SQLescape(Request("cid"))
		Dim id				:	id			=	SQLescape(Request("id"))
        Dim run             : run = SQLescape(request("run"))

		Dim mfp
		Dim product_long_name
		Dim img_sku
		Dim keyword
			
		dim current_postion, group_count, pre_postion, next_postion,pre_if, next_if
		group_count 	= 0
		current_postion = 1
		pre_postion  	= id 
		next_postion 	= id
		pre_if 			= 0
		next_if 		= 0		
		
		Set rs = conn.execute("Select manufacturer_part_number,product_name_long_en,product_name,keywords,menu_child_serial_no,other_product_sku"&_
							" ,case when other_product_sku >0 then other_product_sku else product_serial_no end as img_sku from tb_product "&_
								" where product_serial_no="&SQLquote(id))
		if not rs.eof then
			mfp 				= 	rs("manufacturer_part_number")
			product_long_name	=	rs("product_name_long_en")
			if product_long_name = "" then 
			    product_long_name = rs("product_name")
			End if
			keyword				= 	rs("keywords")
			'if cint(rs("other_product_sku")) >0 then 
				img_sku 	=	rs("img_sku")
			'else	
			'	img_sku 	=	id
			'End if
            if isnull(cid) or isempty(cid) then 
                cid = rs("menu_child_serial_no")
            end if
		End if
		rs.close : set rs = nothing		
		 
		set rs = conn.execute("select product_serial_no "&_
		 						" from tb_product "&_
								" where tag=1 and is_non=0 and split_line=0 "&_
								" and menu_child_serial_no ='"& cid &"'"&_
								" order by product_serial_no asc")
		if not rs.eof then
			
			do while not rs.eof 
				group_count = group_count + 1				
				if next_if = 1 then next_postion = rs(0)

				if cstr(id) = cstr(rs(0)) then 
					current_postion = group_count
					pre_if = 1
					next_if = 1
				else
					next_if = 0
				end if
				if pre_if = 0 then pre_postion = rs(0)
				
			rs.movenext
			loop
		end if
		rs.close : set rs = nothing				
	%>
    <form name="form1" id="form1" action="product_helper_part_keywords_exec.asp" method="post" target="iframe1">
    <table width="100%">
        <tr>
            <td width="250">	
            	<img src="https://www.lucomputers.com<%= HTTP_PART_GALLERY & img_sku%>_list_1.jpg" width="250" />
            </td>
            <td  valign="top">
                <a href="https://www.lucomputers.com/detail_part.aspx?sku=<%= id %>&cid=<%= cid %>" target="_blank"><h3><%= id %></h3></a>
                <p> <a href="https://www.lucomputers.com/detail_part.aspx?sku=<%= id %>&cid=<%= cid %>" target="_blank" id="part_name"><%= product_long_name %></a></p>
                <p style="color:green; border-top: 1px solid green;text-align:right">
                    MFP#:<%= mfp %>
                </p>
                
                	<input type="hidden" value="<%= id %>" name="id" />
                	KEYWORD: <input type="text" id="keyword" name="keyword" value="<%= TAGescape(keyword)%>" size="50" maxlength="100" readonly="readonly" />

                    	<input type="submit" name="" value="Save" />
                        <input type="button" name="Analyse" value="Analyse" onclick="analyse();" />
                        <hr size="1" />
                        <input type="button" name="" value="Clear" onclick="$('input[name=keyword]').val('');" />
                        <br />
                        <input type="radio" name="for" value="0" />None
                        <br />
                        <input type="radio" name="for" value="1" />forward after press save.
                        <br />
                        <input type="radio" name="for" value="2" />backward after press save.
                
        </tr>
    </table>

	
    <div style="height:40px;clear:both; line-height:40px;color:#006699; font-weight:bold; text-align:center; background:#f2f2f2; border:1px solid #ccc;">
                    	<a href="/q_admin/product_helper_part_keywords.asp?id=<%= pre_postion %>&CID=<%= Cid %>&run=<%= run %>" class="movebar_left">&nbsp;&nbsp;&nbsp;</a>&nbsp;&nbsp;&nbsp;
                        <span style="color:#ff6600"><%= current_postion %></span> <span > of <%= group_count %></span>
                        &nbsp;&nbsp;&nbsp;<a href="/q_admin/product_helper_part_keywords.asp?id=<%= next_postion %>&CID=<%= Cid %>" class="movebar_right">&nbsp;&nbsp;&nbsp;</a>
	</div>
<%

	
	
	Set rs = conn.execute("Select keyword, id"&_
						"  from tb_product_category_keyword "&_
						"  Where category_id="&SQLquote(cid)&" and showit=1 and keyword<>'' order by keyword asc ")
	if not rs.eof then
		Response.write "<table id='keywords_area' cellspacing='0'>"&vblf
		Do while not rs.eof 
			Response.write "	<tr>"&vblf 
			response.write "		<td nowrap=""nowrap"">"&vblf
			Response.Write 				"<b>"& rs("keyword")		&"</b>"	
			Response.write "		</td>"&vblf
			Response.write "		<td>"&vblf
				Set crs = conn.execute("Select id, keyword"&_
										" from tb_product_category_keyword_sub "&_
										" Where parent_id="& SQLquote(rs("id")) &" and showit=1")
				if not crs.eof then
					Do while not crs.eof 
					        Response.write  " <div title='keyword' >"
                            Response.write  " <div id='k_"& crs("ID") &"' onclick='kClick($(this));' title='kButton' onmouseover=""kOver($('#k_"& crs("ID") &"'));"" onmouseout=""kOut($('#k_"& crs("ID") &"'));"">K</div>"
							Response.Write	" <div class='s_keyword' onmouseover=""kOver($('#k_"& crs("ID") &"'));"" onmouseout=""kOut($('#k_"& crs("ID") &"'));"">"&vblf
							Response.write  " <input id=""c_"& crs("ID") &""" type='checkbox' value='"& TAGescape(crs("keyword")) &"' />"
							Response.write  "<span id=""txt_"& crs("ID") &""">"& crs("keyword") &"</span>"
							Response.write  " </div>"&vblf			
                            Response.write  " </div>"				
					crs.movenext
					loop				
				End if
				crs.close : set crs = nothing
			REsponse.write "		</td>"&vblf			
			Response.write "	</tr>"&vblf
		rs.movenext
		loop	
		Response.write "</table>"&vblf
		
	End if
	rs.close : set rs = nothing

%>

</form>
<%
    set rs  = conn.execute("select * from tb_part_keyword_analyse_word order by ParentID asc")
    if not rs.eof then
        response.Write "<script type=""text/javascript""> var analyseWords =new Array();"&vblf
        i=0
        do while not rs.eof 
            response.Write "analyseWords["& i & "] = new Array(["""& replace(rs("Analyse_word"), """","\""") &"""], ["&rs("ParentID")&"]);"&vblf
            
            i = i+1
        rs.movenext
        loop
        response.write "</script>"
    end if
    rs.close : set rs = nothing


CloseConn()
%>
<script type="text/javascript">
    $().ready(function () {
        $('div[title=keyword]').css("float", "left").css("width", '150px');
        $('div[title=keyword] div').css("float", "left");
        $('div[title=kButton]').css({ "display": "none", "width": "10px" });
        $('#keywords_area td').css("border-bottom", "1px solid #ccc");


        $('input[type=radio]:eq(<%=Session("for") %>)').attr('checked', true);


        $('div.s_keyword').find("span").click(function () {
            var el = $(this).parent().find('input').eq(0);
            
            if (!el.prop('checked'))
                el.prop('checked', true);
            else
                el.prop('checked', false);
            //el.attr('checked', !el.prop('checked'));
            SetKeywordValue(el);
        });

        $('div >input').click(function () {
            SetKeywordValue(this);
        });

        setTimeout(function () {
            if ('<%= run %>' == "1" && $('#keyword').val().length <15) {
                analyse();

                setTimeout(function () {
                    $('#form1').submit();
                }, 1000)
            }

        }, 2000);
    });

function SetKeywordValue(the)
{
	var n_v = '['+ $(the).val() +']';
	if ($(the).prop('checked'))
	{
		var v = $('input[name=keyword]').val();
		$('input[name=keyword]').val(v+n_v);
		
	}
	else
	{
		var v = $('input[name=keyword]').val();
		if(v.indexOf('['+ $(the).val() +']')!=-1)
		{
			$('input[name=keyword]').val(v.replace(n_v, ''));
		}
	}
	$('input[type=submit]').focus();
}

function forward()
{
    window.location.href= $('a.movebar_left').attr('href');
}

function backward()
{
    window.location.href= $('a.movebar_right').attr('href');
}

function kOver(e) {

    e.css("display", "");
}

function kOut(e) {

    e.css("display", "none");
}

function kClick(e) {
    //alert(e.attr("id").replace("k_", ""));
    return js_callpage_cus('/q_admin/product_helper_part_keywords_word.asp?id=' + e.attr("id").replace("k_", ""), 'part_keyword_word', 520, 500);
   
}


function analyse() {
    var str = $('#part_name').html();

//   var a = new Array();
//   a[0] = new Array(["8GB"],[917]);
//   a[1] = new Array(["17HD"], [782]);
//   a[2] = new Array(["1.5"], [985]);

   for (var i = 0; i < analyseWords.length; i++)
   {
       var s = analyseWords[i];
       if (str.indexOf(s[0]) > -1) {
           //alert(s[1]);
           $('#c_' + s[1]).attr("checked", true);

           var txt = $('#txt_' + s[1]).html();

           if (txt != undefined) {
               var keyword = $('#keyword').val();
               if (keyword.indexOf('[' + txt + ']') > -1) {
                   //alert("exist");
               }
               else
                   $('#keyword').val(keyword + "[" + txt + "]");
           } 
       }
   }
}
</script>
<iframe id="iframe1" name="iframe1" src="" style="width: 330px; height: 330px; " frameborder="0"></iframe>
</body>
</html>
