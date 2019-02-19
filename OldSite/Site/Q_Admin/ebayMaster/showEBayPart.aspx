<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="showEBayPart.aspx.cs" Inherits="Q_Admin_ebayMaster_showEBayPart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    span {font-size:9pt;}
</style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".showpart").each(function () {
                var the = $(this);
                var imgsku = the.attr("imgsku");

                $.get("ebay_cmd.aspx", { cmd: "getImgInfo", sku: imgsku }, function (msg) {
                    if (msg.indexOf("*") > -1) {
                        var width = msg.split('*')[0];
                        var height = msg.split('*')[1];
                        if (parseInt(width) >= 500) {
                            the.css({ "color": "green" });
                            the.next().css({ "color": "green" });
                        }
                        else {

                            the.css({ "color": "#FF7474" });
                            the.next().css({ "color": "#FF7474" });
                        }
                    }
                    else {
                        the.css({ "color": "blue" });
                    }
                    the.next().html(msg)
                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:Literal runat="server" id="ltFilename"></asp:Literal>
</asp:Content>

