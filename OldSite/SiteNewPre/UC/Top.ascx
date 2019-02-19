<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top.ascx.cs" Inherits="UC_Top" %>
<script type="text/javascript">
    function searchGo(key) {
        window.location.href = '/Search.aspx?cate=' + $('#SearchCateCurrType').val() + '&key=' + key;
        return false;
    }

    function searchCategoryBtnGroup(the) {
        the.parent().find('.btn-danger').eq(0).removeClass("btn-danger").addClass("btn-default");
        the.addClass("btn-danger");
        $('#SearchCateCurrType').val(the.attr('v'));
    }
</script>
<div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"
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
                        <button type="button" class="btn btn-default" v="2" data-tag="systemComputer" onclick="searchCategoryBtnGroup($(this));">
                            Desktop Computers</button>
                        <button type="button" class="btn btn-default" v="3" data-tag="notebook" onclick="searchCategoryBtnGroup($(this));">
                            Mobile Computers</button>
                        <button type="button" class="btn btn-danger" v="1" data-tag="part" onclick="searchCategoryBtnGroup($(this));">
                            Part & Peripherals</button>
                    </div>
                </div>
                <div class="input-group">
                    <!-- /btn-group -->
                    <input type="text" class="form-control" maxlength="18" placeholder="MFP#/Keyword/LUSku/eBay itemid/Quote Number"
                        onkeydown="if(event.keyCode==13){searchGo($(this).val());return false;}">
                    <span class="input-group-btn"><a class="btn btn-default" id="searchBtn" onclick="searchGo($(this).parent().prev().val());return false;">
                        Go!</a> </span>
                </div>
            </div>
        </div>
    </div>
</div>
<header class="navbar navbar-inverse bs-docs-nav navbar-fixed-top" id="top" role="banner">
  <div class="container">
    <div class="navbar-header">
      <button class="navbar-toggle collapsed" type="button" data-toggle="collapse" data-target=".bs-navbar-collapse">
        <span class="sr-only">Toggle navigation</span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
        <span class="icon-bar"></span>
      </button>
      <a href="/" class="navbar-brand" style=" margin-top: -15px;"><img src="/images/logo1.png"/></a>
    </div>
    <nav class="collapse navbar-collapse bs-navbar-collapse">
      <ul class="nav navbar-nav" id='page-top-country'>        

      </ul>
      <ul class="nav navbar-nav navbar-right">
        <li>
            <a class="btn " data-toggle="modal" data-target=".bs-example-modal-sm"><span class='glyphicon glyphicon-search'></span> Search</a>
        </li>
        <li>
            <asp:Literal runat="server" ID="ltLoginStr"></asp:Literal>
        </li>

      </ul>
    </nav>
  </div>
</header>
