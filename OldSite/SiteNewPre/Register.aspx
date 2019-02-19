<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div>
        <div class="container">
            <div class="well-lg">
                <div class="modal-dialog modal-md" style="background-color: White;">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true"></span></button>
                            <h4 class="modal-title" id="H1"><span class="glyphicon glyphicon-user"></span><label>&nbsp;Register</label></h4>
                        </div>
                        <div class="modal-body">

                            <h3>Information Privacy</h3>
                            <p>
                                Lu Computers understands our customer’s wishes for privacy, so we handle all the information you provide to us accordingly. Our conduct with your information will in no way involve a third party organization. 
                            </p>
                            <h3>Fast Register </h3>
                            <p>
                                Just enter your email address and a password so you can securely access all
your account information in the future such as order tracking & order history.
Your email address is also used for password retrieval.                    
                            </p>
                            <hr size='1' />
                            <form>
                                <div class="form-group">
                                    <label for="exampleInputEmail1" id="form-group-email">User Name</label>
                                    <input type="email" class="form-control" id="InputEmail1" placeholder="Enter email">
                                    <%--<p class="help-block">Example block-level help text here.</p>--%>
                                </div>
                                <div class="form-group" id="form-group-pwd1">
                                    <label for="exampleInputPassword1">Password</label>
                                    <input type="password" class="form-control" id="InputPassword1" placeholder="Password">
                                </div>
                                <div class="form-group" id="form-group-pwd2">
                                    <label for="exampleInputPassword1">Verify Password:</label>
                                    <input type="password" class="form-control" id="InputPassword2" placeholder="Password">
                                </div>
                                <p class="alert alert-danger" style="display: none;" role="alert"></p>

                                <div>
                                    <a class="btn btn-info" id="btnSignIn">Submit</a>
                                    <a class="btn btn-default" href="/">Cancel</a>
                                    <a href="findPwd.aspx">Forgot your password?</a>
                                </div>

                            </form>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $('#btnSignIn').on("click", function () {
                var username = $('#InputEmail1').val();
                var pwd1 = $('#InputPassword1').val();
                var pwd2 = $('#InputPassword2').val();

                $('.alert-danger').hide(500, function () {
                    $(this).html("");
                });

                $.ajax({
                    type: 'get',
                    url: 'cmds/user.aspx',
                    data: { cmd: 'register', username: username, pwd1: pwd1, pwd2: pwd2 },
                    error: function (g, s, t) {
                        $('.alert-danger').show(800, function () {
                            $(this).html(g);
                        });
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
                                    //$('#form-group-pwd1').addClass("has-error");

                                }
                                $('.alert-danger').show(800, function () {
                                    $(this).html("<span class='glyphicon glyphicon-remove'></span> " + r.msg);
                                });
                            }

                        }
                    }
                });
            });
        });

    </script>
</asp:Content>

