<%@ Control Language="C#" AutoEventWireup="true" CodeFile="bottom.ascx.cs" Inherits="UC_bottom" %>
<div style="background: #333333; min-height: 200px" id="page-bottom">
    <div class="container">
        <div class="row">
            <div class="col-xm-0 col-md-2">
            </div>
            <div class="col-xm-0 col-md-2" style="border-right: 1px dashed #999; height: 120px;">
                <ul>
                    <li><a href="/bAboutUs.aspx">
                        <h5>About US</h5>
                    </a></li>
                    <li><a href="/bContactUs.aspx">
                        <h5>Contact Us</h5>
                    </a></li>
                </ul>
            </div>
            <div class="col-xm-0 col-md-2" style="border-right: 1px dashed #999; height: 120px;">
                <ul>
                    <li><a href="/bGeneralFaq.aspx">
                        <h5>General FAQ</h5>
                    </a></li>
                    <li><a href="/bWarrantyFaq.aspx">
                        <h5>Warranty FAQ</h5>
                    </a></li>
                </ul>
            </div>
            <div class="col-xm-0 col-md-2" style="border-right: 1px dashed #999; height: 120px;">
                <ul>
                    <li><a href="/bManufacturers.aspx">
                        <h5>Manufacturers</h5>
                    </a></li>
                    <li><a href="/bContacts.aspx">
                        <h5>Contacts</h5>
                    </a></li>
                </ul>
            </div>
            <div class="col-xm-0 col-md-3">
                <ul>
                    <li><a href="/bCompanyPolicy.aspx">
                        <h5>Company Policy</h5>
                    </a></li>
                    <li><a href="/bPaymentMethods.aspx">
                        <h5>Payment Methods</h5>
                    </a></li>
                    <li><a href="/bPrivacySecurity.aspx">
                        <h5>Privacy Security</h5>
                    </a></li>
                </ul>
            </div>
            <div class="col-xm-12 col-md-1">
            </div>
        </div>
        <hr size="1" style="border-top: 1px solid #555;" />
        <center style="color: #666;">
            Copyrights © 2004-<%= DateTime.Now.Year.ToString() %>. Lu Computers. All rights
            reserved.</center>
       <%= GodaddySSL %>
    </div>
</div>
