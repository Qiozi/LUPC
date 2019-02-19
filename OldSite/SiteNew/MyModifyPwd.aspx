<%@ Page Title="" Language="C#" MasterPageFile="~/SiteNoForm.master" AutoEventWireup="true" CodeFile="MyModifyPwd.aspx.cs" Inherits="MyModifyPwd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: #74C774; padding: 0px;">
        <div style="padding-bottom: 0px; background: #74C774; border: 0px; padding: 15px;">
            <ul class="list-inline" style="margin-left: 2em;">
                <li>
                    <div style="color: White; font-size: 20px;">
                        <span class="glyphicon glyphicon-home"></span>
                        <a href="/default.aspx" style="color: White;">Home</a>
                        <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="color: White; font-size: 20px;">
                        <span class="glyphicon glyphicon-user"></span>My Account                                
                            <span class="glyphicon glyphicon-menu-right"></span>
                    </div>
                </li>
                <li>
                    <div style="color: White; font-size: 20px;">My Profile</div>
                </li>
            </ul>

        </div>
        <div style="background: white; margin: -1em 3em 3em 3em; padding: 1em;" role="navigation">
            <!-- menu -->
            <ul class="nav nav-tabs">
                <li role="presentation"><a href="MyAccount.aspx">My Profile</a></li>
                <li role="presentation" class="active"><a href="MyModifyPwd.aspx">Modify Password</a></li>
                <li role="presentation"><a href="MyPendingOrders.aspx">Pending Orders</a></li>
                <li role="presentation"><a href="MyAllOrders.aspx">All Order</a></li>
            </ul>
            <!-- menu -->

            <div id="infoViewArea" class="pd">
                <h3>Modify Password</h3>
                <form id="form1" name="form1" method="post" style="padding:2rem;">
                    <div class="form-group">
                        <label for="exampleInputEmail1">Old Password</label>
                        <input type="password" class="form-control" id="oldPassword" placeholder="Email">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputPassword1">New Password</label>
                        <input type="password" class="form-control" id="newPassword" placeholder="Password">
                    </div>
                    <div class="form-group">
                        <label for="exampleInputFile">Confirm Password</label>
                       <input type="password" class="form-control" id="confirmPassword" placeholder="Password">
                        <p class="help-block">Example block-level help text here.</p>
                    </div>
                   
                    <button type="button" class="btn btn-default btnSubmit">Submit</button>
                </form>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="/Scripts/page/myModifyPassoword.js"></script>
</asp:Content>

