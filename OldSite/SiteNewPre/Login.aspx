<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .error {
            color: Red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <form class="form-horizontal" id="loginform" name="loginform" role="form" action="javascript:login();" method="post">
            <div class="container">
                <div style="background: white; width: 700px; margin: 1em auto">


                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true"></span></button>
                        <h4 class="modal-title" id="H1"><span class="glyphicon glyphicon-user"></span> Login</h4>
                    </div>
                    <div class="modal-body">
                        <div class="form-group" style="margin: .3em;">
                            <label>User Name</label>
                            <input type="text" class="form-control" id="user" name="user" placeholder="Example sales@lucomputers.com" />
                        </div>
                        <div class="form-group" style="margin: .3em;">
                            <label>Password</label>
                            <input type="password" class="form-control" name="pass" id="pass" placeholder="Password">
                        </div>
                        <div class="checkbox">
                            <label>
                                <input type="checkbox" id="cbRememberMe">
                                Remember me
                            </label>
                        </div>

                        <p class="alert alert-danger" style="display: none;" role="alert"></p>

                        <div>
                            <input type="submit" class="btn btn-info" id="btnSignIn" value="Sign in" />
                            <input type="reset" class="btn btn-default" id="Submit1" value="Reset" />
                            <a class="btn btn-default" href="/Register.aspx">Register</a>
                            <a class="btn btn-default" href="/">Cancel</a>
                            <a href="findPwd.aspx">Forgot your password?</a>
                        </div>

                        <p class="alert alert-success" role="alert" style="margin-top:1em;">
                            Use of this system by unauthorized persons or in an unauthorized manner is strictly prohibited.
                        </p>
                    </div>


                </div>
            </div>
        </form>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            var validate = $("#loginform").validate({
                rules: {
                    user: {
                        required: true,
                        maxlength: 50,
                        minlength: 3,
                    },
                    pass: {
                        required: true,
                        maxlength: 16,
                        minlength: 4
                    }
                },
                messages: {
                    
                },
                errorPlacement: function (error, element) {
                    if (element.is(":radio"))
                        error.appendTo(element.parent());
                    else if (element.is(":checkbox"))
                        error.appendTo(element.parent());
                    else if (element.is("input[name=captcha]"))
                        error.appendTo(element.parent());
                    else
                        error.insertAfter(element);
                },
                success: function (label) {
                    //label.html("&nbsp;").addClass("right");
                }
            });


            $("input:reset").click(function () {
                validate.resetForm();
            });


        });


        function login() {
            var username = $('#user').val();
            var pwd = $('#pass').val();
            var rememberMe = $('#cbRememberMe').is(":checked");

            $('.alert-danger').hide(500, function () {
                $(this).html("");
            });

            $.ajax({
                type: 'get',
                url: 'cmds/user.aspx',
                data: { cmd: 'login', username: username, pwd1: pwd, rememberMe: rememberMe },
                error: function (g, s, t) {
                    //alert(s);
                    $('.alert-danger').show(1000).html(s);
                },
                success: function (m, s) {
                    if (s == "success") {
                        var r = eval('(' + m + ')');
                        if (r.result == "OK") {
                            if ('<%= ReqU %>' != "") {
                            window.location.href = "<%= ReqU %>";
                        }
                        else
                            window.location.href = "myAccount.aspx";
                    }
                    else {
                        if (r.msg.indexOf('Password') > -1) {
                            $('#form-group-pwd1').addClass("has-error");
                        }
                        $('.alert-danger').show(800, function () {
                            $(this).html("<span class='glyphicon glyphicon-remove'></span> " + r.msg);
                        });
                    }

                }
            }
        });
    }


    $(document).keydown(function (event) {
        if (event.keyCode == 13)
            $('#loginform').submit();
    });
    </script>
</asp:Content>

