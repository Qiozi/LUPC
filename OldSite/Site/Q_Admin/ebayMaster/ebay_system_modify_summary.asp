<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Modify Summary</title>
    <script src="../../js_css/jquery-1.9.1.js" type="text/javascript"></script>

    <link href="../../App_Themes/default/admin.css" rel="stylesheet" type="text/css" />
    <style>
        body { font-size: 8.5pt;}
        #cpuListArea { width:500px;}
        #cpuListArea div{ float:left; width: 150px;}
    </style>
</head>
<body>
<%
    Dim system_sku      :   system_sku          =  SQLescape(request("system_sku")) 
    Dim summary_value   :   summary_value       = null
    Dim summary_group   :   summary_group       =   ""

    set rs = conn.execute("Select main_comment_ids from tb_ebay_system where id="& system_sku )
    if not rs.eof then
        summary_value = rs(0)
    end if
    rs.close : set rs = nothing

 %> 
 <b><%= system_sku %></b>
 <hr size="1" />
 <table><tr><td valign="top">
 <select size="20" name="summary_name" onchange="change_summary_index();">
    <% 
       set rs = conn.execute("select 	id, sub_id, comm_name, comment	from 	tb_ebay_system_main_comment order by comm_name asc")
       if not rs.eof then
            do while not rs.eof
                response.write "<option value='"& rs("id") &"'>"
                response.write "::::::" & rs("comm_name") & "_" & rs("sub_id")
                Response.write "</option>"
                summary_group = summary_group & "<span style='display:none;' id='summary_"& rs("id") &"'>"& rs("comment") &"</span>"
            rs.movenext
            loop
       end if
       rs.close : set rs = nothing
    %>
 
 </select>
 <hr size=1 />
 <div>
    <input type="button" value="Save" onclick="saveCPUList();" />
    <input type="button" value="Clear" onclick="clearSelectedCPU();"/>
    <span id="cpuSaveNote"></span>
 </div>
 <div id="cpuListArea"></div>

 </td><td>
 <input type="button" value=">>" onclick="to_right();" />
 </td><td valign="top">
 <div id="summary_note" style="border:1px solid green; color:Green;padding:3px; display:none; height:150px;" ></div>
  <hr size="1" />
 <input type="text" name="summary" value="<%= summary_value %>" size="30"/>
 <input type="button" value="Clear" onclick="ShowResultClear();" />
 <input type="button" value="Save" onclick="SaveResult();"/>
 <span id="cmd_result" style="font-size:8pt; color:Red;"></span>
 <div><b style="color:#ccc;">Summary:</b></div>
 <div id="summary_result" style="color:#ccc;"></div>
 </td></tr></table>
 <% closeconn() 
 
 response.write summary_group
 %>
 
<script type="text/javascript">
    function change_summary_index()
    {
        $('#summary_note').css("display","");
        var summary_id = $('select[name=summary_name]').val();
        $('#summary_note').html($('#summary_' + summary_id).html());

        getCpus();
    }
    
    function to_right()
    {
        var e = $('input[name=summary]');
        var summary_id = $('select[name=summary_name]').val();
        if(e.val().length ==0)
        {
            e.val(summary_id);
        }
        else
        {
            if(!summaryIDIsExist())
                e.val(e.val()  +"|"+ summary_id);
        }
        ShowResultText();
    }
    
    function summaryIDIsExist()
    {
        var e = $('input[name=summary]');
        var summary_id = $('select[name=summary_name]').val();
        if(e.val().length !=0)
        {
            var vs = e.val().split('|');
            
            for(var i=0; i<vs.length; i++)
            {
                if(summary_id == vs[i])
                return true;
            }
        }
        return false;
    }
    
    function ShowResultText()
    {
        var e = $('input[name=summary]');
        var str = "";
        if(e.val().length !=0)
        {
            var vs = e.val().split('|');
            
            for(var i=0; i<vs.length; i++)
            {
               str += $('#summary_'+vs[i]).html() + "<br/><br/>";
            }
            
        }
        $('#summary_result').html(str);
    }
    
    function ShowResultClear()
    {
        $('input[name=summary]').val("");
        $('#summary_result').html("");
    }
    
    function SaveResult()
    {
        $("#cmd_result").html("...");
        $("#cmd_result").load("ebay_system_modify_summary_exec.asp",{'system_sku':"<%= system_sku %>", 'summary_value':$('input[name=summary]').val()} , function(){});
    }

    function getCpus() {
        $('#cpuListArea').html("........");
        var summary_id = $('select[name=summary_name]').val();
        $.ajax({
            type: "get",
            url: 'ebay_cmd.aspx',
            data: { cmd: "getCPUSList", commentid: summary_id },
            error: function (r, s, t) {
                alert(s);
            },
            success: function (msg, d) {
                $('#cpuListArea').html(msg);
            }
        });
    }

    function saveCPUList() {
        var sku = "0";
        var summary_id = $('select[name=summary_name]').val();

        $('#cpuListArea').find("input").each(function () {
            var the = $(this);
            if (the.is(":checked"))
                sku += ','+the.attr("value");

        });

        $.ajax({
            type: "get",
            url: 'ebay_cmd.aspx',
            data: { cmd: "saveCPUList", commentid: summary_id, skus: sku },
            error: function (r, s, t) {
                alert(s);
            },
            success: function (msg, d) {
                alert(msg);
            }
        });
    }

    function clearSelectedCPU() {
        $('#cpuListArea').find("input").each(function () {
            $(this).prop("checked", false);
        });
    }

    ShowResultText();
</script>
</body>
</html>
