<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!--#include virtual="site/inc/inc_helper.asp"-->
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>LU Computers | Chat</title>
    <style>
        #displayMsg{ display: inline-block; 
                     width:95%; 
                     height: 600px; 
                     background:white;
                     scrollbar-arrow-color:yellow;  
                     scrollbar-base-color:lightsalmon;
                     overflow-y: scroll; 
                     padding:1em;
                     scrollbar-face-color: #D4D4D4; /*滚动条滑块颜色*/ 
scrollbar-hightlight-color: #ffffff; /*滚动条3D界面的亮边颜色*/ 
scrollbar-shadow-color: #919192; /*滚动条3D界面的暗边颜色*/ 
scrollbar-3dlight-color: #ffffff; /*滚动条亮边框颜色*/ 
scrollbar-arrow-color: #919192; /*箭头颜色*/ 
scrollbar-track-color: #ffffff; /*滚动条底色*/ 
scrollbar-darkshadow-color: #ffffff; /*滚动条暗边框颜色*/ 
                     }
                     
        #sendContent{ border:0px; }
        
        .sendBtnArea{ text-align:right;}
        
        .msgClient{ background:#f2f2f2; border:0px solid #ccc; margin-bottom:1em; clear:both;}
        .msgServer { background:#FFECE8; border:0px solid #ccc; margin-bottom:1em; clear:both;}
        .clientName { font-weight:bold; font-size:8pt;}
        .faceImageServer { background:white; border:0px; padding-right: 1em;}
        .faceImageClient { background:white; border:0px; padding-left: 1em;}
        .chargeFaceImg{ border-radius: 50px;}
        .msgContent{font-size:10pt; overflow:hidden;white-space:normal}
    </style>
    <link href="../js_css/ui-lightness/jquery-ui-1.10.2.custom.min.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js_css/jquery-ui-1.10.2.custom.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {


            function getMsg() {
                var now = new Date();

                var username = '<%= "D"  %>';
                var txt = $('#displayMsg').html();

                $.ajax({
                    type: "get",
                    url: "ccCmd.asp",
                    data: "cmd=getmsg&maxid=" + $('#maxid').val(),
                    error: function (r, s, t) {
                    },
                    success: function (msg, s) {
                        if (s == "success") {
                            if (msg.length > 10) {
                                $('#displayMsg').html(txt + "<br/>" + msg.substr(0, msg.length - 10));

                                $('#maxid').val(msg.substr(msg.length - 10, 10));
                                $('#displayMsg').scrollTop($('#displayMsg').height());
                            }
                        }
                    }
                });

                setTimeout(function () { getMsg() }, 2000);
            }
            setTimeout(function () { getMsg() }, 2000);

            function sendMsg() {

                if ($('#sendContent').html().length > 300) {

                    $('#contentLength').html("Have left " + (300 - $(this).val().length)).css({ "color": "blue" });
                    return;
                }
                $.ajax({
                    type: "get",
                    url: "cccmd.asp",
                    data: "cmd=save&msg=" + $('#sendContent').val(),
                    error: function (r, s, t) { },
                    success: function (msg, s) {
                        if (s == "success") {
                            $('#sendContent').val('');
                            getMsg();
                        }
                    }
                });

            }

            $('#btnSend').click(function () {
                sendMsg();
            });

            $('#sendContent').keydown(function () {
                $('#contentLength').html("Have left " + (300 - $(this).val().length));
            });
        });
    </script>
</head>
<body style="background:#f2f2f2;">
<div>
    <h1>LU Computers</h1>
</div>
<input type="hidden" id="maxid" value="0" />
<table width="99%">
    <tr>
        <td width="60%" valign="top">
            <div id="displayMsg">
                
            </div>
            <p>
                <textarea id="sendContent" rows="5" cols="80" style="width:100%;"></textarea>
                <br /><i id="contentLength">Max length 300</i>
            </p>
            <p class="sendBtnArea">
                <input type="button" value="Close" />
                <input type="button" value="Send" id="btnSend" />
            </p>
        </td>
        <td width="5">&nbsp;</td>
        <td>
           <table>
                <tr>
                    <td>&nbsp;</td>
                    <td rowspan="2"></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                </tr>
           </table>
        </td>
    </tr>
</table>
<%closeconn() %>
</body>
</html>
