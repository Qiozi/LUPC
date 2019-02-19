<%@ Page Title="Search - LU Computers" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <link href="/Content/search.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: white; margin-top: .2em; min-height: 700px;">
        <ol class="breadcrumb">
            <li><a href="/default.aspx">Home</a></li>
            <li class="active">Search</li>
        </ol>
        <div class="well">
            <p>
                Search Terms: <strong class="note">"<%= ReqKey %>"</strong> in <strong><%= CateTypeName %></strong><br />
                Search Result: count&nbsp; <%= SearchQty %>
            </p>
            <%= SearchNote %>
        </div>
        <div id="prodListArea">
            <%= ResultString %>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("img.lazy").lazyload();
            $('.addToCart').on('click', function (response) {
                var sku = $(this).data('sku');
                util.addToCart(sku);
            });
        });
    </script>
</asp:Content>

