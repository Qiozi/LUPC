<%@ Page Title="Sign In - LU Computers" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="background-box1"></div>
    <div class="customPlane lu-desc-page-two container">
        <form class="" id="loginform" name="loginform" role="form" action="javascript:login();" method="post">
            <input type="hidden" name="ReturnUrl" id="ReturnUrl" value="<%= ReturnUrl %>" />
            <div style="background: white;">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true"></span></button>
                    <h2 class="modal-title" id="H1"><%--<span class="glyphicon glyphicon-user"></span>--%>Sign In</h2>
                </div>
                <div class="modal-body formCustom pd-0">
                    <asp:Panel runat="server" ID="panelSiginFirst" Visible="false">
                        <div class="alert alert-warning">
                            <h3>* Please sigin first.</h3>
                        </div>
                    </asp:Panel>
                    <div class="form-group col-sm-12">
                        <label>User Name</label>
                        <input type="email" class="form-control" id="user" name="user" placeholder="Example sales@lucomputers.com" autocomplete="off" />
                    </div>
                    <div class="form-group col-sm-12">
                        <label>Password</label>
                        <input type="password" class="form-control" name="pass" id="pass" placeholder="Password" autocomplete="off">
                    </div>
                    <div class="form-group col-sm-12 error">

                    </div>
                    <div class="form-group formButtonArea col-sm-12" style="margin-top: 0;">
                        <input type="submit" class="btn btn-info width200" id="btnSignIn" value="Sign in" />
                        <input type="reset" class="btn btn-default width200" id="Submit1" value="Reset" />
                        <a class="btn btn-default width200" href="/Register.aspx?u=<%= ReturnUrl %>">Sign Up</a>
                        <p class="pd-t">
                            <a href="/findPwd.aspx">Forgot your password?</a>
                        </p>
                    </div>

                </div>
            </div>
        </form>
        <div class="alert text-success fontBold font2rem" role="alert">
            Use of this system by unauthorized persons or in an unauthorized manner is strictly prohibited.                          
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <%= WebExtensions.CombresLink("siteJsLogin") %>
</asp:Content>

