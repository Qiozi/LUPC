<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="detail_sys_print.aspx.cs" Inherits="detail_sys_print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .badge {
            background: white;
            color: #333;
            font-weight: normal;
        }

            .badge.price {
                color: #FF7D5D;
                font-weight: normal;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div style="background: white;">
        <div class="" style="background: #ccc; padding: 1em; margin-top: -50px;">
            <a class="navbar-brand" style="margin-top: -29px;">
                <img src="../images/logo1.png" /></a>
            <span class="glyphicon glyphicon-phone-alt"></span>1866.999.7828&nbsp;&nbsp;416.446.7743  
        </div>
        <div class="container" style="background: white;">
            <div style="height: 5px;"></div>
            <div class="row" id='sysLogoArea'>
                <div class="col-sm-6">
                    <img src="<%= CaseImgUrl %>" class="img-thumbnail" width='350' alt="...">
                </div>
                <div class="col-sm-6">
                    <ul class="list-group">
                        <li class="list-group-item">
                            <b>Price:</b><span class="badge price">$<%= RegularPrice %> <small><%= PriceUnit %></small></span>
                        </li>
                        <li class="list-group-item">
                            <b>Special Cash Price:</b><span class="badge price">$<%= SpecialCashPrice%> <small><%= PriceUnit %></small></span>
                        </li>
                        <li class="list-group-item">
                            <b>Quote Number:</b><span class="badge"><%= QuoteNumber%> </span>
                        </li>
                        <li class="list-group-item">
                            <b>Date:</b><span class="badge"><%= SysDate%> </span>
                        </li>
                    </ul>
                    <p><small>SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash,Interact,bank transfer,money order, etc. Cash price does not waive sales taxes if applicable.</small></p>
                    <p>
                        <small>Every unique computer takes 1-7 days to be preassembled and tested before installed into the computer chassis. System includes meticulous hand assembly and precision cable routing. We tune system performance to its best and complete driver updates. All manufacturer documentations and disks are included.
                        </small>
                    </p>

                </div>
            </div>
            <div style="height: 5px;"></div>
            <%= SysListString%>
            <hr size='1' />
            <div id='sysComment' class="note" style="color: #996633;">
                <h4>Congratulations! </h4>
                <p>
                    You have successfully configured a new system. The quote number displays on top of this page. If you are not to submit your order now please keep this quote by emailing to yourself.
                </p>
                <p>
                    You can continue submitting your order now or place your order later:
                </p>
                <p>
                    1) Visit www.lucomputers.com to use our fully automated and secure online ordering system, Enter this quote number to the box at the upper right corner of the web page. Submit your order when system configuration is loaded.
                </p>
                <p>
                    2) Call us by phone (Toll free 1866.999.7828 or 416.446.7743) during business hours and please refer to this quote number. Do not hesitate to ask any questions and further changes to the configuration.
                </p>
                <p>
                    3) Visit us in our store at 1875 Leslie Street, Unit 24, Toronto.
                </p>
                <p>
                    Shipping and applicable taxes are not included in quotation. USA customers are tax and duty free, Ontario residents 13%, HST provinces 13%, rest of Canada 5%. If pickup is your preferred method please load this quote and click button ARRANGE A LOCAL PICK UP.
                </p>
                <p>Business Hours: Mon-Fri 10AM - 7PM EST Sat 11AM - 4PM EST</p>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script language="javascript">
        $(document).ready(function () {
            $('#top').remove();
            $('#page-bottom').remove();
            $('.modal-content').remove();
        });
    </script>
</asp:Content>
