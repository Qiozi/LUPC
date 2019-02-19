<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="product_part_desc.aspx.cs" Inherits="product_part_desc" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Edit Part(one)</title>
        <script type="text/javascript" src="q_admin/JS/Loading.js"></script>
</head>
<body style="text-align:center;">
    <form id="form1" runat="server">
    <div>
        
    </div>
        <anthem:Panel ID="Panel1" runat="server"  PostCallBackFunction="Anthem_PreCallBack" PreCallBackFunction="Anthem_PostCallBack" Height="510px" Width="100%" >
            <br /><anthem:Button ID="btn_save" runat="server" Text="Save" 
                OnClick="btn_save_Click" Enabled="False" />
            &nbsp;
            <anthem:Button ID="btn_generate_all_file" runat="server" 
                onclick="btn_generate_all_file_Click" 
                Text="Generate All Part Comment File" Visible="False" Enabled="False" />
            <hr size="1" />
            <anthem:Label ID="lbl_part_name" runat="server" Font-Bold="True"></anthem:Label><hr size="1" />
            <table cellspacing="0" width="100%" >
                <tr>
                    <td align="right">SKU:</td><td><anthem:Label runat="server" ID="lbl_sku"></anthem:Label></td>
                </tr>
                <tr>
                    <td align="right">Short Name:</td><td><anthem:TextBox runat="server" 
                        ID="txt_short_name" Columns="50" MaxLength="200"></anthem:TextBox></td>
                </tr>
                <tr>
                    <td align="right">
                        Middle Name:</td>
                    <td>
                        <anthem:TextBox ID="txt_middle_name" runat="server" Columns="50" MaxLength="200"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Long Name:</td>
                    <td>
                        <anthem:TextBox ID="txt_long_name" runat="server" Columns="50" MaxLength="200"></anthem:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td align="right">
                        Manufacturer:</td>
                    <td>
                        <anthem:TextBox ID="txt_manufacturer" runat="server" Columns="50" MaxLength="50"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        manufacturer_url:</td>
                    <td>
                        <anthem:TextBox ID="txt_manufacturer_url" runat="server" Columns="50" 
                            MaxLength="200"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Supplier SKU:</td>
                    <td>
                        <anthem:TextBox ID="txt_supplier_sku" runat="server" Columns="50" MaxLength="50"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Priority:</td>
                    <td>
                        <anthem:TextBox ID="txt_priority" runat="server" Columns="50" MaxLength="8"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Img Sum:</td>
                    <td>
                        <anthem:TextBox ID="txt_img_sum" runat="server" Columns="50" MaxLength="2"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Keywords:</td>
                    <td>
                        <anthem:TextBox ID="txt_keywords" runat="server" Columns="50" MaxLength="200"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Other Product SKU(img):</td>
                    <td>
                        <anthem:TextBox ID="txt_other_product_sku" runat="server" Columns="50" 
                            MaxLength="6"></anthem:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="2">
                        <anthem:CheckBox ID="CheckBox_showit" runat="server" Text="Showit" />
                        &nbsp;
                        <anthem:CheckBox ID="CheckBox_hot" runat="server" Text="Hot" />
                        &nbsp;
                        <anthem:CheckBox ID="CheckBox_new" runat="server" Text="New" />
                        &nbsp;
                        <anthem:CheckBox ID="CheckBox_split_line" runat="server" Text="Split Line" />
                        &nbsp;
                        <anthem:CheckBox ID="CheckBox_is_non" runat="server" Text="Is None" />
                        &nbsp;
                        <anthem:CheckBox ID="CheckBox__export" runat="server" Text="Export" />
                        &nbsp;
                    </td>
                </tr>
            </table>
            <hr size="1"/>
            
            <anthem:TextBox ID="TextBox1" runat="server" Columns="70" Rows="20" TextMode="MultiLine"></anthem:TextBox></anthem:Panel>
    </form>
</body>
</html>
