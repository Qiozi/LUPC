<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sale_on_sale_settings.aspx.cs" Inherits="Q_Admin_sale_on_sale_settings_" Title="On Sale Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center"> 
        &nbsp;SKU:<asp:TextBox ID="txt_sku" runat="server"></asp:TextBox>
        <asp:Button ID="btn_add"
        runat="server" Text="ADD" onclick="btn_add_Click" />
    &nbsp;&nbsp;
        <asp:Button ID="btn_save" runat="server" Text="Save" onclick="btn_save_Click" />
        <asp:Label ID="lbl_note" runat="server" ForeColor="#CC3300"></asp:Label>
    </div>
        <hr size="1" /> 
          <div style="text-align:center; font-size: 9pt;">
                begin datetime 
                <asp:TextBox ID="txt_begin_datetime" runat="server"  onFocus="calendar()"></asp:TextBox>
                end datetime
                <asp:TextBox ID="txt_end_datetime" runat="server"  onFocus="calendar()"></asp:TextBox>
                <asp:Button ID="btn_Change_date"
        runat="server" Text="Change Datetime" onclick="btn_Change_date_Click" />
                <br />
                *注：当需要操作onsale时，请先用“change datetime&quot;, 然后再进行操作，因为如果产品onsale到期，先点击save时， 
                0$会被作为最后一次的价格进行保存，所以再点&quot;change datetime&quot;时，功能将失效。</div>        
         <hr size="1" />
    <asp:GridView ID="gv_promotion_list" runat="server" AutoGenerateColumns="False" 
        Width="90%" onrowdatabound="gv_promotion_list_RowDataBound" 
        onrowcommand="gv_promotion_list_RowCommand">
        <Columns>
            <asp:BoundField DataField="serial_no" HeaderText="ID" />
            <asp:BoundField DataField="product_serial_no" HeaderText="SKU" />
            <asp:BoundField DataField="menu_child_name" HeaderText="Category Name" />
            <asp:BoundField DataField="product_name" HeaderText="Middle Name" />
            <asp:TemplateField HeaderText="Begin Datetime">
                <ItemTemplate>
                    <asp:TextBox ID="_txt_begin_time" runat="server"  onFocus="calendar()" Text='<%# DataBinder.Eval(Container.DataItem, "begin_datetime", "{0:yyyy}") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Datetime">
                <ItemTemplate>
                    <asp:TextBox ID="txt_end_datetime" runat="server"  onFocus="calendar()" Text='<%# DataBinder.Eval(Container.DataItem, "end_datetime", "{0:yyyy}") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="product_current_cost" HeaderText="Cost$" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:BoundField DataField="product_current_price" HeaderText="Price$" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Sold Price$">
                <ItemTemplate>
                    <asp:TextBox ID="_txt_sold_price" runat="server" style="text-align:right" Text='<%# DataBinder.Eval(Container.DataItem, "sale_price") %>'></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="save_price" HeaderText="Discount$" >
                <ItemStyle HorizontalAlign="Right" />
            </asp:BoundField>
            <asp:ButtonField Text="Del" CommandName="Del" />
        </Columns>
    </asp:GridView>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

