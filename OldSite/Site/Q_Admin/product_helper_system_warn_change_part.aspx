<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_system_warn_change_part.aspx.cs" Inherits="Q_Admin_product_helper_system_warn_change_part" Title="Change System Part" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center">
        <div id="btn_list" style="width: 100%; background:#f2f2f2; border-bottom:1px solid #cccccc;">
            <asp:Button runat="server" ID="btn_save" Text="Save" Font-Bold="True" 
                onclick="btn_save_Click"/>
        </div>
        <br /><br /><br />
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <hr size="1" />
        <div style="text-align:left">
            <asp:RadioButtonList ID="RadioButtonList1" runat="server">
       
    </asp:RadioButtonList>
        </div>
    </div>
    
   
<script type="text/javascript">
$().ready(function(){
     $("#btn_list").floatdiv("lefttop");
});

</script>
</asp:Content>

