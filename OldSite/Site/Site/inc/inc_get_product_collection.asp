<%@LANGUAGE="VBSCRIPT" CODEPAGE="65001"%>
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
 response.Clear();
	dim cid, export_str
	cid = SQLescape(request("cid"))
	
	'
	' for Business Page Title
	'
	Dim id
	id  = SQLescape(request("id"))
	
	if cid <> "" then 
		set rs = conn.execute("select 	product_collection_id, product_collections, product_collection_topic, product_collection_redirect_path_type, 	product_collection_title, product_collection_title_path, regdate from tb_product_collection where product_collection_id='"& cid &"'")
		 if not rs.eof then 
		 	if  trim(rs("product_collections")) <> "" then 
		 		export_str = "<a href="""& GetRedirectPath(rs("product_collection_redirect_path_type")) & rs("product_collections") &"&page_title="& rs("product_collection_topic") &"&return_path="& rs("product_collection_title") &"&page_return_path="& rs("product_collection_title_path") &""">"&_
			rs("product_collection_topic") & "</a> "
			else
				export_str = rs("product_collection_topic") 
			end if
		 end if
		 rs.close : set rs = nothing

		if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" then
			export_str = export_str & "&nbsp;<a href=""/product_collection_manage.aspx?cid="& cid &""" onclick=""js_callpage_cus(this.href, \'right_manage\', 800,600);return false;"">M</a>"
		end if
		Response.write  "document.write('"& export_str &"');"
		'response.write "document.write('"& session("user")&"');"
	end if
	
	if len(id)>0 then
		if session("user") = LAYOUT_MANAGER_NAME  and  LAYOUT_MANAGER_NAME <> "" then
			export_str = export_str & "&nbsp;<a href=""/product_business_title.aspx?id="& id &""" onclick=""js_callpage_cus(this.href, \'right_manage\', 600,400);return false;"">M</a>"
		end if
		Response.write  "document.write('"& export_str &"');"	
	End if
	
	
	function GetRedirectPath(t)
		GetRedirectPath = ""
		if (t = 3) then 
			GetRedirectPath = "system_list.asp?system_ids="
		elseif (t = 1) then 
			GetRedirectPath = "part_list.asp?system_ids="
		end if
	end function 
	closeconn()
%>