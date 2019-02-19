<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Cmd</title>
</head>
<body>
     <%
     
     
        dim category_id, cmd
        Dim maxs, mins, rates, percents
        
       
        cmd                             =      SQLescape(request("cmd"))
        category_id                     =      SQLescape(request("category_id"))
        
        if(cmd = "new") then
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 0, 999999, 105, 1)")
            Response.Write "OK"
        end if
        
        if(cmd = "save")then
            maxs = split(SQLescape(request("maxs")), ",")
            mins = split(SQLescape(request("mins")), ",")
            rates= split(SQLescape(request("rates")),",")
            percents=split(SQLescape(request("percents")), ",")
            
            conn.execute("delete from tb_part_price_change_setting where category_id='"& category_id &"'")
            
            for i = lbound(maxs) to ubound(maxs)
                if len(trim(maxs(i)))>0 and len(trim(mins(i)))>0 then
                    conn.execute("insert into tb_part_price_change_setting(category_id, cost_min, cost_max, rate, is_percent)"&_
                                " values ('"&category_id&"','"&mins(i)&"','"&maxs(i)&"','"&rates(i)&"','"&percents(i)&"')")
                                
                end if
            next    
            conn.execute("delete from tb_part_price_change_setting where cost_min <1 and cost_max<1")
            response.Write "OK"    
        end if
        
        if (cmd = "default") then
            conn.execute("delete from tb_part_price_change_setting where category_id='"& category_id &"'")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 0, 50, 5, 0)")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 50, 100, 8, 0)")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 100, 150, 9, 0)")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 150, 200, 10, 0)")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 200, 300, 15, 0)")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 300, 500, 108, 1)")
            conn.execute("insert into tb_part_price_change_setting"&_
                        " (category_id, cost_min, cost_max, rate, is_percent )"&_
                        " " &_
                        " values "&_
                        " ('"& category_id &"', 500, 999999, 107, 1)")
            Response.Write "OK"
        
        end if
        
        closeconn() %>
        
</body>
</html>
