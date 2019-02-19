<%@ Page Title="LU Chat" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ChatServer.aspx.cs" Inherits="Chat_ChatServer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style> 
 th, td
 {
    border: 1px solid #b5d6e6;
    font-size: 12px;
    vertical-align: top;
    height: 20px;
    
 }
 th
 {
     background:#D1ECFF;
     font-weight:normal;
   
 }
 #cust_table tr:hover { background: #f2f2f2;}
 #displayMsg{ height:500px; min-height:500px}
</style>
<script type="text/javascript">

    $(document).ready(function () {


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

        function getCustomMsg(id) {
            $.ajax({
                type: "get",
                url: "csCmd.aspx",
                data: "cmd=getMsg&cid=" + $('#currCustomerID').val(),
                dataType: "json",
                contentType: "application/json",
                error: function (r, s) {
                    alert(s);
                },
                success: function (msg, s) {
                    if (s == "success") {
                        $.each(msg, function (index, item) {
                            var str = item.Name;
                            if (item.IsServerMsg == "True") {

                            }
                       
                            $('#displayMsg').append(str);
                        });
                    }
                    else
                        alert(msg);
                }
            });
        }

        function getCustomInfo(id) {

        }

        function selectedCustomer(id) {
            $('#currCustomerID').val(id);
            getCustomMsg(id);
            getCustomInfo(id);
        }

        function getCustomerList() {

            $.ajax({
                type: "get",
                url: "csCmd.aspx",
                data: "cmd=getCustomerList",
                dataType: "json",
                contentType: "application/json",
                error: function (r, s) {
                    alert(s);
                },
                success: function (msg, s) {
                    if (s == "success") {
                        $.each(msg, function (index, item) {
                            var row = $('#cust_row').clone(true);
                            row.find('#cust_code').text(item.Code).attr("id", "a" + index);
                            row.find('#cust_name').text(item.Name).attr("id", "b" + index);
                            row.find('#cust_date').text(item.Date).attr("id", "c" + index);
                            row.find('#cust_msg').html(item.Msg).attr("id", "d" + index);
                            if (item.Msg != "0")
                                row.css("font-weight", "700");
                            row.css({ "display": "", "cursor": "pointer" }).click(function () { selectedCustomer(item.Code); }).appendTo($('#cust_table'));
                        });
                    }
                    else
                        alert(msg);
                }
            });
        }

        $('#btnSend').click(function () {
            sendMsg();
        });

        getCustomerList();
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    <h1>LU Computers</h1>
</div>
<input type="hidden" id="maxid" value="0" />
<input type="hidden" id="currCustomerID" value="0" />
<table width="99%" cellspacing="3">
    <tr>
        <td width="30%" valign="top" style="border:1px solid #ccc;">
            <h3 style="background:#f2f2f2;padding:5px;">Customer List</h3>
            <div>
                <input type="text" placehold="d" id="custom_keyword" />
                <input type="button" value="Go" />
                <table width="100%" id="cust_table" cellspacing="0">
                    <tr>
                        <th>Code</th>
                        <th>Name</th>
                        <th>Date</th>
                        <th>msg count</th>
                    </tr>
                    <tr id="cust_row" style="display:none">
                        <td id="cust_code"></td>
                        <td id="cust_name"></td>
                        <td id="cust_date"></td>
                        <td id="cust_msg"></td>
                    </tr>
                </table>
            </div>
            <div id="customerList">
                
            </div>
        </td>
        <td width="40%" valign="top" style="border:1px solid #ccc;">
            <h3 style="background:#f2f2f2;padding:5px;">Chat message</h3>

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
       
        <td valign="top" style="border:1px solid #ccc;">
            <h3 style="background:#f2f2f2;padding:5px;">Customer info.</h3>
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
</asp:Content>

