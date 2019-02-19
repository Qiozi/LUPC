<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top.ascx.cs" Inherits="UC_Top" %>
<header class="navbar navbar-inverse bs-docs-nav navbar-fixed-top" id="top" role="banner">
    <div class="container">
        <div class="navbar-header">
            <button class="navbar-toggle collapsed" type="button" data-toggle="collapse" data-target=".bs-navbar-collapse">
                <span class="sr-only">Toggle navigation</span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
                <span class="icon-bar"></span>
            </button>
            <a href="/" class="navbar-brand" style="margin-top: -15px; margin-left: 10px;">
                <img src="<%= WebLogo %>" /></a>
        </div>
        <nav class="collapse navbar-collapse bs-navbar-collapse">
            <ul class="nav navbar-nav" id='page-top-country'>
                <li class="pull-left">
                    <a title='Canada' href="/ReturnHome.aspx?cid=1">
                        <img class="<%= HideUS %>" src="<%= LU.BLL.ImageHelper.Get("/images/ca24-2.png") %>" />
                        <img class="<%= HideCA %>" src="<%= LU.BLL.ImageHelper.Get("/images/ca24.png") %>" />
                    </a>
                </li>
                <li class="pull-left">
                    <a title='USA' href="/ReturnHome.aspx?cid=2">
                        <img class="<%= HideCA %>" src="<%= LU.BLL.ImageHelper.Get("/images/usa24-2.png") %>" />
                        <img class="<%= HideUS %>" src="<%= LU.BLL.ImageHelper.Get("/images/usa24.png") %>" />
                    </a>
                </li>
                <li class="pull-left">
                    <a href="/bContactUs.aspx">
                        <span class="glyphicon glyphicon-phone-alt"></span>1866.999.7828&nbsp;&nbsp;416.446.7743
                    </a>
                </li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <li class="pull-left">
                    <a class="btn " data-toggle="modal" data-target=".bs-example-modal-sm"><span class='glyphicon glyphicon-search'></span>Search</a>
                </li>
                <li class="pull-left">
                    <a class="btn " href="/ShoppingCart.aspx">
                        <span class=" glyphicon glyphicon-shopping-cart"></span>My Cart <%= badge %>                                                            
                    </a>
                </li>
                <asp:PlaceHolder runat="server" ID="placeMyAccountLogout">
                    <li>
                        <a class="btn " href="/MyAccount.aspx">
                            <span class="glyphicon glyphicon-user"></span>Hi!  <%= CustName %> My Account                                                       
                        </a>
                    </li>
                    <li>
                        <a class="btn " href="/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>Logout </a>
                    </li>
                </asp:PlaceHolder>
                <asp:PlaceHolder runat="server" ID="placeMyAccountLogin">
                    <li>
                        <a class="btn " href="/Login.aspx"><span class="glyphicon glyphicon-user"></span>Sign In</a>
                    </li>
                    <li>
                        <a class="btn " href="/Register.aspx">Sign Up</a>
                    </li>
                </asp:PlaceHolder>
            </ul>
        </nav>
    </div>
</header>
<%--<div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-md">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="exampleModalLabel">
                    <span class='glyphicon glyphicon-search'></span>Search</h4>
            </div>
            <div class="modal-body">
                <div style='margin-bottom: 1em;'>
                    <div class="input-group-btn">
                        <input type="hidden" id="SearchCateCurrType" value="<%= DefaultCateId %>" />

                        <button type="button"
                            class="btn btn-default"
                            v="2"
                            data-tag="systemComputer"
                            onclick="searchCategoryBtnGroup($(this),$('#SearchCateCurrType'));">
                            Desktop Computers</button>

                        <button type="button"
                            class="btn btn-default"
                            v="3"
                            data-tag="notebook"
                            onclick="searchCategoryBtnGroup($(this),$('#SearchCateCurrType'));">
                            Mobile Computers</button>

                        <button type="button"
                            class="btn btn-danger"
                            v="1"
                            data-tag="part"
                            onclick="searchCategoryBtnGroup($(this),$('#SearchCateCurrType'));">
                            Part & Peripherals</button>

                    </div>
                </div>
                <div class="input-group">
                    <!-- /btn-group -->
                    <input type="text" class="form-control" maxlength="18" placeholder="MF Part#/Keyword/LU SKU#/LU Quote#/eBay item#"
                        onkeydown="if(event.keyCode==13){util.searchGo($(this).val());return false;}">
                    <span class="input-group-btn">
                        <a class="btn btn-default" id="searchBtn" onclick="util.searchGo($(this).parent().prev().val());return false;">Go!</a>
                    </span>
                </div>
            </div>
        </div>
    </div>
</div>--%>
