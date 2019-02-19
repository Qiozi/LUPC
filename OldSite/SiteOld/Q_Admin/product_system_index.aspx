<%@ Page Title="System Product Manage" Language="C#" MasterPageFile="~/Q_Admin/MasterPageNotDocType.master" AutoEventWireup="true" CodeFile="product_system_index.aspx.cs" Inherits="Q_Admin_product_system_index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width:100%; height: 100%" cellspacing="2" cellpadding="0" id="table_main">
        <tr>
            <td valign="top">
                <iframe src="product_system_list.aspx" frameborder="0" id="iframe1" name="iframe1" style="width: 100%; height: 100%"></iframe>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        document.getElementById("table_main").style.height = document.body.clientHeight - 65;
    </script>
</asp:Content>

