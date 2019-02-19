<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="eBayTempletePageComment.aspx.cs" Inherits="Q_Admin_ebayMaster_LU_eBayTempletePageComment" Title="Edit eBay Sys Comment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
    table tr td{background: #fff;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>Summary:</td>
                <td><asp:TextBox runat="server" ID="txt_comm_name" Columns="50"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Comment:</td>
                <td><asp:TextBox runat="server" ID="txt_comment" Columns="50" Rows="5" 
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Clear" />
                    <asp:Button runat="server" ID="btn_submit" Text="Save" 
                        onclick="btn_submit_Click" /><asp:Label ID="Label_note" runat="server" 
                        ForeColor="Blue"></asp:Label></td>
            </tr>
        </table>
        
        <hr size="1" />
        
    </div>
    <asp:DataList ID="DataList1" runat="server" 
        onitemcommand="DataList1_ItemCommand"  >
        <HeaderTemplate>
            <div style='background:#ccc;width:700px;'>
            <table cellpadding='0' cellspacing='1' id='table1' width='100%'>
                <tr>
                    <td>ID</td>
                    <td>Comment Name</td>
                    <td>CMD</td>
                </tr>            
        </HeaderTemplate>
        <ItemTemplate>
                <tr>                    
                    <td style="text-align:center;width:50px;">
                        <%# DataBinder.Eval(Container.DataItem, "ID") %>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "comm_name") %>
                    </td>
                    <td style="text-align:center;">
                        <asp:Button CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CommandName="EditComment" Text="Edit" runat="server" ID="btn_edit"/>
                        <asp:Button CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID") %>' CommandName="Delete" Text="Delete" runat="server" ID="btn_delete" />
                    </td>
                </tr>            
        </ItemTemplate> 

        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>        
        
    </asp:DataList>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
