<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="SiteApp.login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<div class="jumbotron">
    <div class="panel panel-default panel-info" style="margin:10px;">
        <div class="panel-heading"><span class="glyphicon glyphicon-user"></span> Login</div>
          <div class="panel-body">
        
                <p class="input-group">
                  <span class="input-group-addon">@</span>
                  <input type="text" class="form-control" placeholder="Username">
                </p>
                <p class="input-group">
                  <span class="input-group-addon "><span class="glyphicon glyphicon-star"></span></span>
                  <input type="password" class="form-control" placeholder="Password">
                </p>
                <p class="input-group">
                  <div class="text-right">
                        <input type="submit" class="form-control active" value="GO !">
                  </div>
                </p>
          </div>
    </div>
</div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphScript" runat="server">
</asp:Content>
