<%@ Page Language="C#" AutoEventWireup="true" CodeFile="part_and_group.aspx.cs" Inherits="Q_Admin_part_and_group" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <table >
            
            
            <tr>
                <td valign="top" align="center" width="100"><asp:Label ID="lbl_sku" 
                        runat="server" Font-Bold="True" Font-Size="11pt" ForeColor="#006600"></asp:Label></td>
                <td valign="top">
                    <asp:Label ID="lbl_short_name" runat="server" Font-Bold="True" 
                        Font-Size="9pt"></asp:Label><br />
                    <asp:Label ID="lbl_middle_name" runat="server" Font-Size="8pt"></asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" />
        <div>
            <asp:CheckBoxList ID="cbl_group_list_ebay" runat="server" RepeatColumns="3" 
                ForeColor="Green">
        </asp:CheckBoxList>

        </div>
        <hr size="1" />
        <div style="text-align:left">
        eBay Cost:<asp:TextBox runat="server" ID="txt_ebay_cost" Text=""></asp:TextBox>
        eBay Price:<asp:TextBox runat="server" ID="txt_ebay_price" Text=""></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click" 
                 /><asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
                <div style="color:#ff9900">绿色字样的 part group 是eBay System 专用。</div>
        </div> 
        
        <div>
            
        </div>
        <hr size="1" />
        <div>
            <asp:CheckBoxList ID="cbl_group_list" runat="server" RepeatColumns="3">
        </asp:CheckBoxList>

        </div>
        <hr size="1" />
        <div style="text-align:center">
        <asp:Button ID="btn_submit" runat="server" Text="Save" 
                onclick="btn_submit_Click" Visible="False" /><asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
        </div> 
        
        <div>
            
        </div>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Button1" />
            <asp:AsyncPostBackTrigger ControlID="btn_submit" />
            <asp:AsyncPostBackTrigger ControlID="cbl_group_list" />
            <asp:AsyncPostBackTrigger ControlID="cbl_group_list_ebay" />
        </Triggers>
        </asp:UpdatePanel>
    </div>
    
    </form>
</body>
</html>
