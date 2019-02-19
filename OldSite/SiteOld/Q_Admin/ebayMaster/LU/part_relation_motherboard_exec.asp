<html>
<head>
    <title></title>
</head>
<body>

<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<%
        

        dim sku, cmd, video, audio, network, CPU_Fan
        sku 	            = SQLescape(request("sku"))
        cmd 		        = SQLescape(request("cmd"))
        video 				= SQLescape(request("video"))
        audio 				= SQLescape(request("audio"))
        network 			= SQLescape(request("network"))
        CPU_Fan 			= SQLescape(request("CPU_Fan"))
        
        if(cmd = "saveRInfo") then 
            conn.execute("delete from tb_part_relation_motherboard_video_audio_port where mb_sku='"& sku &"'")
            conn.execute("insert into tb_part_relation_motherboard_video_audio_port "&_
	                    " (mb_sku, video_sku, audio_sku, port_sku, network_sku ) "&_
                        "	values "&_
                        "	('"& sku &"', '"& video &"', '"& audio &"', 0, '"& network &"') ")
            Response.write "OK"
        end if
       
        closeconn()
%>

</body>
</html>
