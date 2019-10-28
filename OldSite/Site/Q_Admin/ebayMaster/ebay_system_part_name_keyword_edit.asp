<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/validate_admin.asp"-->
<!--#include virtual="site/inc/inc_helper.asp"-->
<!--#include file="ebay_inc.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Edit Ebay System Part Name Keyword</title>
    <script type="text/javascript" src="../JS/lib/jquery-1.3.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="../../js_css/jquery.css?a" />
    <link rel="stylesheet" type="text/css" href="../../js_css/b_lu.css" />
    <link href="../../App_Themes/default/admin.css" rel="stylesheet" type="text/css" />

    <style>
        .part_name { float:left; margin: 4px;border:1px solid #fff;}
        .part_name:hover { border:1px solid #ccc; background:#f2f2f2;}
    </style>
</head>

<body>
        
            Keyword<input type="text" name="keyword" />
            <input type="submit" name="Submit" onclick="SubmitKeyword();" />
            <div><i>*Sort items Alphabetically</i></div>
            <hr size=1 />
            

        <span id="keyword_list_area"></span>
        
<script type="text/javascript">
    $().ready(function(){
        LoadKeywordList();    
    
    });
    
    function LoadKeywordList()
    {
        $('#keyword_list_area').load('ebay_system_part_name_keyword_get.asp'
            ,{ "cmd":"getAll" }
            , function(){ }
            
            );
    }
    
    function SubmitKeyword()
    {
       
        $('#keyword_list_area').load('ebay_system_part_name_keyword_exec.asp'
            ,{ "cmd":"Add"
            , "keyword": $('input[name=keyword]').val() }
            
            , function(){ LoadKeywordList();$('input[name=keyword]').val('')}
            
            );
    }
    
    function dekPartNameKeyword(id)
    {
        if(true)
        {
            $('#keyword_list_area').load('ebay_system_part_name_keyword_exec.asp'
            ,{ "cmd":"del"
            , "keyword_id": id }
            
            , function(){ LoadKeywordList();}
            
            );
        }
    }
</script>
</body>
</html>
