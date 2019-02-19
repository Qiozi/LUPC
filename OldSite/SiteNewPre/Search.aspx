<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Search.aspx.cs" Inherits="Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        .price_list {
            list-style-type: none;
        }

            .price_list li {
                font-size: 14px;
            }

        #prodListArea h4.sysTitle {
            padding: 15px;
            background-color: #8BD18B;
            color: White;
        }

        #prodListArea a {
            color: #666;
        }

            #prodListArea a:hover {
                text-decoration: none;
            }

        #prodListArea li:hover {
            background-color: #f2f2f2;
            color: #333;
        }

            #prodListArea li:hover a {
                color: #333;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: white; margin-top: .2em; min-height: 700px;">
        <ol class="breadcrumb">
            <li><a href="default.aspx">Home</a></li>
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
        $(document).ready(function () {
            $("img.lazy").lazyload();
        });
    </script>
</asp:Content>

