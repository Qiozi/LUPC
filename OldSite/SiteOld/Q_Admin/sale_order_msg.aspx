<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="sale_order_msg.aspx.cs" Inherits="Q_Admin_sale_order_msg" Title="Untitled Page" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><anthem:DataList 
        ID="DataList1" runat="server" Width="100%">
    <ItemTemplate>
        <table width="100%">
            <tr>
                <td style="background:#f2f2f2;">
                    <anthem:Label ID="_lbl_author" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"msg_author").ToString() == "Me" ?"Customer":DataBinder.Eval(Container.DataItem,"msg_author").ToString() %>'  width="100">
                    </anthem:Label></td><td><anthem:Label ID="_lbl_datetime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"regdate") %>'> &nbsp;&nbsp; </anthem:Label></td>
            </tr>
            <tr>
                <td colspan="2" style="min-height: 60px">
                    &nbsp;&nbsp;  <anthem:Label ID="_lbl_msg" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"msg_content_text") %>'> &nbsp;&nbsp; </anthem:Label>
                </td>
            </tr>
        </table>
        <hr size="1" />
    </ItemTemplate>
    </anthem:DataList>
&nbsp;
</asp:Content>

