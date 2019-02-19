<%@ Page Language="C#"  AutoEventWireup="true" ValidateRequest="false" CodeFile="right_manage.aspx.cs" Inherits="right_manage" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>adv manage</title>
    <script type="text/javascript" src="Q_Admin/JS/Loading.js"></script>
</head>
<body style="text-align:center;">
    <form id="form1" runat="server">
    <div>
        <br />
        <br />
        &nbsp;<anthem:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" PostCallBackFunction="Anthem_PostCallBack" PreCallBackFunction="Anthem_PreCallBack" />
        <input type="button" value="Close Window" onclick="window.close();" /><asp:Button 
            ID="btn_generate_all_file" runat="server" Text="生成所有左、中、右广告文件" 
            onclick="btn_generate_all_file_Click" />
&nbsp;<hr size="1" />
            <anthem:CheckBox runat="server" ID="cb_top" Text="show top 10" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<span onclick="window.open('manage_top.aspx', 'manageTop')" style="color:Blue; cursor: pointer">Change</span>
        <hr size="1" />
    </div>
        <div style="text-align:left;">
        
            <table width="100%">
                <tr>
                    <td>
                        <anthem:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoCallBack="True">
        </anthem:RadioButtonList>
                    </td>
                    <td valign="middle" style="font-weight:bold; text-align:center;">
                        <anthem:Label runat="server" ID="lbl_part_product_category"></anthem:Label>
                    </td>
                </tr>
            </table></div>
        
        <hr size="1" />
         <table align="center">
            <tr>
                <td><anthem:TextBox runat="server"  ID="txt_left_content" Columns="20" Rows="25" TextMode="MultiLine"></anthem:TextBox></td>
                <td><anthem:TextBox runat="server"  ID="txt_main_content" Columns="40" Rows="25" TextMode="MultiLine"></anthem:TextBox></td>
                <td><anthem:TextBox runat="server"  ID="txt_content" Columns="20" Rows="25" TextMode="MultiLine"></anthem:TextBox></td>
            </tr>
         </table>

    </form>
</body>
</html>
