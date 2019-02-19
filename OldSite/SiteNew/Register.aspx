<%@ Page Title="Sign Up - LU Computers" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="background-box1"></div>
    <div class="customPlane  lu-desc-page-two container">
        <input type="hidden" name="ReturnUrl" id="ReturnUrl" value="<%= ReturnUrl %>" />
        <div>
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true"></span></button>
                <h2 class="modal-title" id="H1">Sign Up</h2>
            </div>
            <div class="modal-body formCustom pd">
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
                        <label for="exampleInputPassword1">Confirm Password:</label>
                        <input type="password" class="form-control" id="InputPassword2" placeholder="Password">
                    </div>
                    <p class="alert alert-danger" style="display: none;" role="alert"></p>
                    <div>
                        <a class="btn btn-info width200" id="btnSignIn">Submit</a>
                        <a class="btn btn-default width200" href="/">Cancel</a>
                        <a href="/findPwd.aspx">Forgot your password?</a>
                    </div>
                </form>
                <div class="alert alert-warning mg-t mg-b-0" role="alert">
                    <h3>Information Privacy</h3>
                    <p>
                        Lu Computers understands our customer’s wishes for privacy, so we handle all the information you provide to us accordingly. Our conduct with your information will in no way involve a third party organization. 
                    </p>
                </div>
                <div class="alert mg-t alert-warning" role="alert">
                    <h3>Fast Register </h3>
                    <p>
                        Just enter your email address and a password so you can securely access all
your account information in the future such as order tracking & order history.
Your email address is also used for password retrieval.                    
                    </p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript" src="/Scripts/page/register.js"></script>
</asp:Content>

