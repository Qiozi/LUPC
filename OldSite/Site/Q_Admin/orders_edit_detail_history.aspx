<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_history.aspx.cs" Inherits="Q_Admin_orders_edit_detail_history" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:GridView ID="gv_order_product_history" runat="server" 
                AutoGenerateColumns="False" 
                onrowdatabound="gv_order_product_history_RowDataBound" Width="100%">
                <RowStyle Font-Size="8.5pt" />
                <Columns>
                    <asp:BoundField HeaderText="SKU" DataField="product_serial_no" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="name" DataField="product_name"/>
                    <asp:BoundField HeaderText="unit price$" DataField="order_product_sold" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="order_product_sum" HeaderText="Sum" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cmd" DataField="add_del" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Datetime" DataField="create_datetime" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle Font-Size="8.5pt" />
            </asp:GridView>
</asp:Content>

