<%@ Page Title="Rebate - LU Computers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Rebate.aspx.cs" Inherits="Rebate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background-color: White;">
        <div class="panel panel-default">
            <div class="panel-heading" style="padding-bottom: 0px;">
                <ul class="list-inline">
                    <li>
                        <div>
                            <a href="/Default.aspx"><span class="glyphicon glyphicon-home"></span> Home <span
                                class="glyphicon glyphicon-menu-right"></span></a>
                        </div>
                    </li>
                    <li>Rebate </li>
                </ul>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-2">
                <div data-spy="affix" data-offset-top="70" data-offset-bottom="200" style="width:180px;">
                    <asp:Repeater runat="server" ID="rptList">
                        <HeaderTemplate>
                            <ul class="list-group">
                                <li class="list-group-item list-group-item-success"> Shortcut</li>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <li class="list-group-item"><a href="?#<%# Eval("Brand") %>">
                                <%# Eval("Brand") %></a> </li>
                        </ItemTemplate>
                        <FooterTemplate>
                            </ul>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="col-lg-10">
                <div class="panel panel-default">
                    <!-- Default panel contents -->
                    <div class="panel-heading">
                        Rebate Center</div>
                    <div class="panel-body note">
                        <p>
                            Here you will find a list of rebates that are currently provided by the manufacturers.
                        </p>
                        <p>
                            These rebates are valid for purchases made at LU Computers only. Please read all
                            rebates' terms and conditions carefully. It is the responsibility of the end user
                            to meet rebate requirements set forth by the manufacturer.
                        </p>
                        <p>
                            Please make sure to check for the following:
                        </p>
                        <p>
                            <ol>
                                <li>Make sure you have purchased the correct product / part number.</li>
                                <li>Read all terms & conditions of the rebate prior to making purchase. </li>
                                <li>Keep a copy of all rebate form & proof of purchase for your records.</li>
                                <li>Download, fill out, mail it in.</li>
                                <li>Please make sure you reside in the country (USA/Canada) where the rebate is valid.</li>
                            </ol>
                        </p>
                        <p>
                            Note: you must have Adobe Acrobat reader installed on your computer in order to
                            download a mail in rebate. If you do not have Acrobat Reader installed click here
                            to download a free version of this software.
                        </p>
                    </div>
                    <asp:Repeater runat="server" ID="rptList2" OnItemDataBound="rptList2_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table ">
                                <thead>
                                    <tr>
                                        <th>
                                            SKU
                                        </th>
                                        <th>
                                            Description
                                        </th>
                                        <th>
                                            Maf.Part#
                                        </th>
                                        <th>
                                            Rebate
                                        </th>
                                        <th>
                                            Start
                                        </th>
                                        <th>
                                            End
                                        </th>
                                    </tr>
                                </thead>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr class="success">
                                <td colspan="6">
                                    <h5>
                                        <a id="<%# Eval("Brand") %>"></a>
                                        <asp:Literal runat="server" ID="ltBrand" Text='<%# Eval("Brand") %>'></asp:Literal></h5>
                                </td>
                            </tr>
                            </div>
                            <asp:Repeater runat="server" ID="rptSubList">
                                <ItemTemplate>
                                    <tr <%# Eval("BgColor") %>>
                                        <td title="SKU">
                                            <%# Eval("SKU") %>
                                        </td>
                                        <td>
                                            <%# Eval("ProdName") %>
                                        </td>
                                        <td>
                                            <%# Eval("MFP") %>
                                        </td>
                                        <td class="text-right" title="Save">
                                            $<%# Eval("Price") %><small>CAD</small>
                                        </td>
                                        <td>
                                            <%# Eval("BeginDate", "{0:d/M/yyyy}") %>
                                        </td>
                                        <td>
                                            <%# Eval("EndDate", "{0:d/M/yy}")%>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server"></asp:Content>