<%@ Page Title="Find password - LU Computers" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="findPwd.aspx.cs" Inherits="findPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="background-box1"></div>
    <div class="customPlane lu-desc-page-two container">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true"></span></button>
            <h2 class="modal-title" id="H1"><span class="glyphicon glyphicon-user"></span>&nbsp;Find My Password!</h2>
        </div>
        <div class="modal-body pd-t">
            <div class="form-group">
                <label>User Name</label>
                <input type="email" class="form-control" id="user" name="user" placeholder="Example sales@lucomputers.com" />
            </div>
            <div>
                <input type="button" class="btn btn-info" id="btnSubmit" value="Submit" />
                <input type="reset" class="btn btn-default" id="Submit1" value="Reset" />
                <a class="btn btn-default" href="/Register.aspx">Sign Up</a>
                <a href="Login.aspx">Sign In</a>
            </div>
            <div id="MsgInfo" class="mg-t"></div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="Scripts/page/findPwd.js"></script>
</asp:Content>

