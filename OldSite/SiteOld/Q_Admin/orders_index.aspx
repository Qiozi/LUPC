<%@ Page Title="Orders" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="orders_index.aspx.cs" Inherits="Q_Admin_orders_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <iframe src="/q_admin/orders_index.asp" id="iframe1" name="iframe1" frameborder="0" style="width: 100%; height: 100px; border-top: 1px solid #ccc"></iframe>
    <script type="text/javascript">
        //document.getElementById("iframe1").style.height = document.body.clientHeight - 140;
    </script>
    <asp:Literal ID="Literal_run_script" runat="server"></asp:Literal>

    <script type="text/javascript">

        $().ready(function () {

            // window resize
            var _attr = parseInt(document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight);

            $('#iframe1').css("height", isNaN(_attr) || _attr <= 50 ? "100%" : (_attr - 50) + "px");
        });
        var resizeTimer = null;
        $(window).bind("resize", function () {
            if (resizeTimer)
                clearTimeout(resizeTimer);
            resizeTimer = setTimeout(function () {
                var _attr = parseInt(document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight);

                // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";
                $('#iframe1').css("height", isNaN(_attr) || _attr <= 50 ? "100%" : (_attr - 50) + "px");

            }, 100);
        });
    </script>
</asp:Content>

