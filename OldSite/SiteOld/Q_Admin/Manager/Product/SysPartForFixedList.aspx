<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/Manager/MasterPage.master" AutoEventWireup="true" CodeFile="SysPartForFixedList.aspx.cs" Inherits="Q_Admin_Manager_Product_SysPartForFixedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:TextBox runat="Server" ID="txtSku"></asp:TextBox>
    <asp:Button runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" />
    <hr size="1" />
    <table class="table">
        <thead>
            <tr>
                <th>Sku</th>
                <th>Name</th>
                <th>Price</th>
                <th>Stock1</th>
                <th>Stock2</th>
                <th>CMD</th>
            </tr>
        </thead>
        <tbody>
        <asp:Repeater runat="server" ID="rptList" OnItemCommand="rptList_ItemCommand">
            <ItemTemplate>
                <tr>
                    <td><%# DataBinder.Eval(Container.DataItem, "Sku") %></td>
                     <td><%# DataBinder.Eval(Container.DataItem, "Name") %></td>
                     <td><%# DataBinder.Eval(Container.DataItem, "Price") %></td>
                     <td><%# DataBinder.Eval(Container.DataItem, "Stock1") %></td>
                     <td><%# DataBinder.Eval(Container.DataItem, "Stock2") %></td>
                    <td><asp:Button CommandName="Cancel" OnClientClick="return confirm('are you sure?');" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Sku") %>' Text="Cancel" runat="server" ID="btnCancel" /></td>
                </tr>
            </ItemTemplate>
        </asp:Repeater>
        </tbody>
    </table>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footScript" runat="Server">
</asp:Content>

