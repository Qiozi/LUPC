<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerMsg.ascx.cs" Inherits="Q_Admin_UC_CustomerMsg" %>
<asp:DataList 
        ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <table width="300">
            <tr>
                <td style="background:#f2f2f2;">
                    <asp:Label ID="_lbl_author" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"msg_author").ToString() == "Me" ?"Customer":DataBinder.Eval(Container.DataItem,"msg_author").ToString() %>'  width="100">
                    </asp:Label></td><td><asp:Label ID="_lbl_datetime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"regdate") %>'> &nbsp;&nbsp; </asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="min-height: 60px">
                    &nbsp;&nbsp;  <asp:Label ID="_lbl_msg" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"msg_content_text") %>'> &nbsp;&nbsp; </asp:Label>
                </td>
            </tr>
        </table>
        <hr size="1" />
    </ItemTemplate>
    </asp:DataList>