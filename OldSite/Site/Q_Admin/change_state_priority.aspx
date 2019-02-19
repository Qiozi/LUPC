<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="change_state_priority.aspx.cs" Inherits="Q_Admin_change_state_priority" Title="LU Computer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center">
        c<asp:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click" />
        <hr size="1" />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="state_serial_no" HeaderText="ID" >
                    <ItemStyle CssClass="displayNone" />
                    <HeaderStyle CssClass="displayNone" />
                </asp:BoundField>
                <asp:BoundField DataField="state_name" HeaderText="State" >
                    <ItemStyle Width="200px" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Priority">
                    <ItemTemplate>
                        <asp:TextBox ID="_txt_priority" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "priority") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

