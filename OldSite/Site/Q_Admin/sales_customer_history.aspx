<%@ Page Language="C#" MasterPageFile="~/Q_Admin/none.master" AutoEventWireup="true" CodeFile="sales_customer_history.aspx.cs" Inherits="Q_Admin_sales_customer_history" Title="Customer History" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>
        <anthem:Label runat="server" ID="lbl_name"></anthem:Label>
    </h3>
    <div style="font-weight:bold;">
        <span >
            RecordCount: &nbsp;&nbsp;<anthem:Label runat="server" ID="lbl_record_count" ForeColor="Blue"></anthem:Label>&nbsp;&nbsp;
        </span>
        <span >
            Total:&nbsp;&nbsp;<anthem:Label runat="server" ID="lbl_total" ForeColor="Blue"></anthem:Label>&nbsp;&nbsp;
        </span>
    </div>
    <asp:datagrid id="dg_order_list" runat="server" allowpaging="True" alternatingitemstyle-backcolor="#f2f2f2"
        autogeneratecolumns="False" onitemcommand="dg_order_list_ItemCommand" onitemdatabound="dg_order_list_ItemDataBound"
        onpageindexchanged="dg_order_list_PageIndexChanged" pagesize="25" selecteditemstyle-forecolor="#ff9900"
        width="100%">
        <HeaderStyle CssClass="trTitle"  /> 
        <Columns>
            <asp:BoundColumn DataField="order_helper_serial_no" HeaderText="ID">

            </asp:BoundColumn>
            <asp:TemplateColumn>
                <ItemTemplate>
                <div onclick="OpenOrderDetail('<%# DataBinder.Eval(Container.DataItem, "order_code") %>')" style="cursor:pointer">View</div> 
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:BoundColumn DataField="order_code" HeaderText="Order#">
                <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                    font-underline="False" horizontalalign="Center" ></itemstyle>
            </asp:BoundColumn>            
           
            <asp:BoundColumn datafield="GRAND_total" HeaderText="AMNT" DataFormatString="{0:$###,###.##}">
                <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                    font-underline="False" horizontalalign="Right" ></itemstyle>
            </asp:BoundColumn>
            <asp:BoundColumn  DataField="order_date" HeaderText="DATE"></asp:BoundColumn>
            <asp:BoundColumn DataField="out_status" HeaderText="FR STAT"></asp:BoundColumn>
            <asp:BoundColumn DataField="pre_status_serial_no" HeaderText="BK STAT"></asp:BoundColumn>
            <asp:BoundColumn DataField="phone_d" HeaderText="telephone"></asp:BoundColumn>
            <asp:BoundColumn DataField="customer_shipping_state" HeaderText="state"></asp:BoundColumn>
            <asp:BoundColumn DataField="customer_email1" HeaderText="email"></asp:BoundColumn>
        </Columns>
        <PagerStyle Mode="NumericPages"  VerticalAlign="Middle"  Font-Bold="True" Font-Size="X-Large"  ForeColor="#FF9900" />
        <AlternatingItemStyle BackColor="#EFEFEF"  />
        <SelectedItemStyle ForeColor="#FF9900"  />
    </asp:datagrid>
    
</asp:Content>

