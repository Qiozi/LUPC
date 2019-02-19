<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPageNotDocType.master" AutoEventWireup="true" CodeFile="product_edit_frame.aspx.cs" Inherits="Q_Admin_product_edit_frame" Title="Edit Part Info" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="background: #f2f2f2; clear:both;">
    <table style="width:100%; " cellspacing="0" cellpadding="0" id="table_main" border="0">
        <tr>

            <td valign="top" style="background:#fff;">
                <iframe src="product_helper_part_view_info_3.aspx" frameborder="0" id="iframe1" name="iframe1" style="width: 100%; height:99%;"></iframe>
            </td>
        </tr>
    </table>

<script type="text/javascript">
    $(document).ready(function () {

        // window resize
        var _attr = parseInt(document.body.clientHeight);
        $('iframe').css("height", isNaN(_attr) || _attr <= 50 ? "100%" : (_attr - 50) + "px");
        
    });


var resizeTimer = null;
$(window).bind("resize", function () {
    if (resizeTimer)
        clearTimeout(resizeTimer);
    resizeTimer = setTimeout(function () {
        var _attr = parseInt(document.body.clientHeight); //alert(_attr);
        // $("#ifr_main_frame1").style.height = isNaN(_attr) || _attr <= 200 ? "100%": (_attr - 200) +"px";
        $('iframe').css("height", isNaN(_attr) || _attr <= 50 ? "100%" : (_attr - 50) + "px");
    }, 100);

});    
            
</script>
</div>
</asp:Content>

