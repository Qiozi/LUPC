<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%


    dim ids, ids_gs, comp_parts, comp_parts_gs
    
    ids            	=      SQLescape(request("id"))
    comp_parts     	=      SQLescape(request("compatibility_parts"))
    
    ids_gs         	=  split (ids, ",")
    comp_parts_gs	=  split (comp_parts, ",")

    for i = lbound(ids_gs) to ubound(ids_gs)
            conn.execute("Update tb_ebay_system_parts Set compatibility_parts="& SQLquote(trim(comp_parts_gs(i))) &" where id="& SQLquote(TrimCommaEscape(ids_gs(i))) &"")
               ' Response.write SQLquote(TrimCommaEscape(ids_gs(i)))
    next













closeconn()
 %>
 <script type="text/javascript">
        alert('It is ok');
 </script>