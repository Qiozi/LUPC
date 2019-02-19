<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu2.ascx.cs" Inherits="SiteApp.UC.TopMenu2" %>

 <nav class="navbar navbar-default navbar-fixed-top" role="navigation">
      <div class="container-fluid">
        <!-- Brand and toggle get grouped for better mobile display -->
        <div class="navbar-header">
          <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#bs-example-navbar-collapse-1">
            <span class="sr-only">Toggle navigation</span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
            <span class="icon-bar"></span>
          </button>
          <span class="navbar-brand" href="default.aspx">LU Computers <span class="badge cartBadge" id='logoBadge'></span></span>
        </div>

        <!-- Collect the nav links, forms, and other content for toggling -->
        <div class="collapse navbar-collapse" id="bs-example-navbar-collapse-1">

          <ul class="nav navbar-nav">            
            <li><a href="MyAccount.aspx"><span class="glyphicon glyphicon-user"></span>&nbsp;My Account</a></li>
            <li><a href="ShoppingCart.aspx"><span class="glyphicon glyphicon-shopping-cart"></span>&nbsp;My Cart <span class="badge cartBadge"></span></a></li>           
          </ul>

          <ul class="nav navbar-nav navbar-right">
             <li class="dropdown">
              <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-list-alt"></span> Desktop Computers <span class="caret"></span></a>
              <ul class="dropdown-menu" role="menu">
                <%= ProdSystem %>
              </ul>
            </li>
            <li class="dropdown">
              <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-list-alt"></span> Mobile Computers <span class="caret"></span></a>
              <ul class="dropdown-menu" role="menu">
                <%= ProdMobile %>
              </ul>
            </li>
             <li class="dropdown">
              <a href="#" class="dropdown-toggle" data-toggle="dropdown"><span class="glyphicon glyphicon-list-alt"></span> Parts & Peripherals <span class="caret"></span></a>
              <ul class="dropdown-menu" role="menu">
                <%= ProdPart %>
              </ul>
            </li>
          </ul>
        </div><!-- /.navbar-collapse -->
      </div><!-- /.container-fluid -->
      
                <div class="input-group input-group-sm" style='padding:10px;'>
                  <input type="text" placeholder='Search MFP#/SKU#' class="form-control">
                  <span class="input-group-btn">
                  <button class="btn btn-default" type="button"><span class="glyphicon glyphicon-search"></span> Go!</button>
                  </span>
                </div><!-- /input-group -->
            
    </nav>
