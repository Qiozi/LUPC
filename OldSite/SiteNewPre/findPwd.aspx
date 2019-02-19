<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="findPwd.aspx.cs" Inherits="findPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container">
        <div style="background: white; width: 700px; height: 500px; margin: 1em auto">


            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true"></span></button>
                <h4 class="modal-title" id="H1"><span class="glyphicon glyphicon-user"></span>&nbsp;Find My Password!</h4>
            </div>
            <div class="modal-body">
                <div class="form-group" style="margin: 2em;">
                    <label>User Name</label>
                    <input type="text" class="form-control" id="user" name="user" placeholder="Example sales@lucomputers.com" />
                </div>

                <div style="margin: 2em;">
                    <input type="button" class="btn btn-info" id="btnSubmit" value="Submit" />
                    <input type="reset" class="btn btn-default" id="Submit1" value="Reset" />
                    <a class="btn btn-default" href="/Register.aspx">Register</a>
                    <a class="btn btn-default" href="/">Cancel</a>
                    <a href="Login.aspx">Login</a>
                </div>
                <div id="MsgInfo"></div>
            </div>


        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSubmit').on('click', function () {
                var user = $('#user').val();
                if (user.length > 3) {
                    $.get("/cmds/user.aspx?cmd=findpwd&username=" + $('#user').val(), {}, function (data) {
                        if (data.indexOf("success") > -1) {
                            $('#MsgInfo').html("<div class=\"alert alert-success\" role=\"alert\">" + data + "</div>");
                        }
                        else {
                            $('#MsgInfo').html("<div class=\"alert alert-danger\" role=\"alert\">" + data + "</div>");
                        }
                    });
                }
            })
        })
    </script>

</asp:Content>

