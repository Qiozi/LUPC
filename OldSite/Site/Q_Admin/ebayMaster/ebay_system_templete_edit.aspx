<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="ebay_system_templete_edit.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_system_templete_edit" Title="eBay Sys Templete Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <style>
        table tr td {
            background: #fff;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" title="e">
        <div>
            <table>
                <tr>
                    <td>comment</td>
                    <td>
                        <asp:TextBox ID="txt_comment" runat="server" Columns="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Cont 1</td>
                    <td>
                        <asp:TextBox ID="txt_templete_content" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Cont 2</td>
                    <td>
                        <asp:TextBox ID="txt_templete_content2" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>top 1</td>
                    <td>
                        <asp:TextBox ID="txt_templete_summ_1" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>top 2</td>
                    <td>
                        <asp:TextBox ID="txt_templete_summ_2" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Button ID="btn_submit" runat="server" Text="Submit"
                            OnClick="btn_submit_Click" />
                        <asp:Label ID="Label_note" runat="server" ForeColor="Blue"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <hr size="1" />

        <asp:DataList ID="DataList1" runat="server"
            OnItemCommand="DataList1_ItemCommand">
            <HeaderTemplate>
                <div style='background: #ccc; width: 500px;'>
                    <table cellpadding='0' cellspacing='1' id='table1' width='100%'>
                        <tr>
                            <td>ID</td>
                            <td>Comment</td>
                            <td>CMD</td>
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr>
                    <td style="text-align: center; width: 50px;">
                        <%# DataBinder.Eval(Container.DataItem, "ID") %>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "templete_comment") %>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "category_Names") %>
                    </td>
                    <td style="text-align: center;">
                        <asp:Button CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CommandName="EditComment" Text="Edit" runat="server" ID="btn_edit" />
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr>
                    <td style="text-align: center; width: 50px; background: #f2f2f2">
                        <%# DataBinder.Eval(Container.DataItem, "ID") %>
                    </td>
                    <td style="background: #f2f2f2">
                        <%# DataBinder.Eval(Container.DataItem, "templete_comment") %>
                    </td>
                    <td>
                        <%# DataBinder.Eval(Container.DataItem, "category_Names") %>
                    </td>
                    <td style="text-align: center; background: #f2f2f2">
                        <asp:Button CommandArgument='<%# DataBinder.Eval(Container.DataItem, "ID") %>' CommandName="EditComment" Text="Edit" runat="server" ID="btn_edit" />
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </div>
            </FooterTemplate>

        </asp:DataList>
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
</body>
</html>
