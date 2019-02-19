<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bottom.ascx.cs" Inherits="UC_bottom" %>
<div id="page-bottom">
    <div class="container">
        <div class="row mg-0">
            <div class="col-xm-0 col-md-3">
                <ul class="list-group">
                    <li class="text-left">
                        <a href="/bAboutUs.aspx">
                            <i class="iconfont">&#xe639;</i> About US
                        </a>
                    </li>
                    <li>
                        <a href="/bContactUs.aspx">
                            <i class="glyphicon glyphicon-earphone"></i>Contact Us
                        </a>
                    </li>
                </ul>
            </div>
            <div class="col-xm-0 col-md-3">
                <ul class="list-group">
                    <li>
                        <a href="/bGeneralFaq.aspx">
                            <i class="iconfont">&#xe620;</i> General FAQ
                        </a>
                    </li>
                    <li>
                        <a href="/bWarrantyFaq.aspx">
                            <i class="iconfont">&#xe620;</i> Warranty FAQ
                        </a>
                    </li>
                </ul>
            </div>
            <div class="col-xm-0 col-md-3">
                <ul class="list-group">
                    <li>
                        <a href="/bManufacturers.aspx">
                            <i class="iconfont">&#xe64a;</i> Manufacturers
                        </a>
                    </li>
                    <li>
                        <a href="/bContacts.aspx">
                            <i class="iconfont">&#xe64e;</i> Contacts
                        </a>
                    </li>
                </ul>
            </div>
            <div class="col-xm-0 col-md-3">
                <ul class="list-group">
                    <li>
                        <a href="/bCompanyPolicy.aspx">
                            <i class="iconfont">&#xe632;</i> Company Policy
                        </a>
                    </li>
                    <li>
                        <a href="/bPaymentMethods.aspx">
                            <i class="iconfont">&#xe6d6;</i> Payment Methods
                        </a>
                    </li>
                    <li>
                        <a href="/bPrivacySecurity.aspx">
                            <i class="iconfont">&#xe632;</i> Privacy Security
                        </a>
                    </li>
                </ul>
            </div>
        </div>
        <div class="share-component pd text-center"
            data-disabled="weibo,tencent,qzone,qq,douban,diandian"
            data-image="<%= LU.BLL.Config.ResHost %>images/logo1.png">
        </div>
        <hr style="border-top: 1px solid #555;" />
        <div style="text-align: center">
            <span id="siteseal"><%= GodaddySSL %></span>
        </div>
        <footer class="txtColorWhite text-center">
            <p>Copyrights &copy; 2004 - <%= System.DateTime.Now.Year %> - Lu Computers. All rights reserved.</p>
        </footer>
    </div>
    <div class='btnToTop'>
        <a href="#form100" class="item">
            <span class="glyphicon glyphicon-menu-up"></span>
        </a>
      <%--  <a class="item" data-toggle="modal" data-target=".modal-sm-home-question">
            <span class="glyphicon glyphicon-edit"></span>
        </a>--%>
    </div>
</div>
