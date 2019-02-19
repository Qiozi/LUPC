<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="sale_order_list_invalid.aspx.cs" Inherits="Q_Admin_sale_order_list_invalid" Title="Order List Invalid" %>

<%@ Register src="UC/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:Navigation ID="Navigation1" runat="server" 
        NavigationText="Invalid Order" />
Order Code:<asp:TextBox ID="txt_order_code" runat="server"></asp:TextBox>
First Name:<asp:TextBox ID="txt_first_name" runat="server"></asp:TextBox>
Last Name:<asp:TextBox ID="txt_last_name" runat="server"></asp:TextBox>
    <asp:Button ID="btn_search" runat="server" Text="Search" 
        onclick="btn_search_Click" />
    <asp:Button ID="btn_clear" runat="server" Text="Clear" 
        onclick="btn_clear_Click" />
 
    
<hr />
    top:50<br />
    <asp:GridView ID="GridView_order_list" runat="server" 
         AlternatingRowStyle-BackColor="#f2f2f2"
        AutoGenerateColumns="False" onrowcommand="GridView_order_list_RowCommand" 
        onrowdatabound="GridView_order_list_RowDataBound" Width="800px">
        <Columns>
            <asp:BoundField DataField="order_helper_serial_no" HeaderText="ID" >
                <ItemStyle Height="18px" HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="order_code" HeaderText="Order#" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="order_date" HeaderText="Date" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:BoundField DataField="name" HeaderText="Shipping Name" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:ButtonField Text="change to valid" CommandName="ChangeValid" >
                <ItemStyle HorizontalAlign="Center" />
            </asp:ButtonField>
        </Columns>
    </asp:GridView>       
</asp:Content>

