<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_factory_search.aspx.cs" Inherits="Q_Admin_product_helper_factory_search" Title="Part Factory Search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center">
        <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="3" 
            RepeatLayout="Flow">
            <asp:ListItem Selected="True" Value="1">SKU</asp:ListItem>
            <asp:ListItem Value="2">Factory#</asp:ListItem>
            <asp:ListItem Value="3">Supplier#</asp:ListItem>
        </asp:RadioButtonList>
    <asp:TextBox ID="TextBox1" runat="server" Columns="50"></asp:TextBox>
    <asp:Button ID="Button1"
        runat="server" Text="Search" onclick="Button1_Click" />
        <hr size="1" />
        </div>
  <ul class="ul_table_heard">

    <asp:Repeater runat="server" ID="rpt_product_list"             >
        <HeaderTemplate>
            <li style="width: 100%; clear: left;">
                <ul class="ul_row" style="margin:0px;">
                    <li style="width: 40px;background-color:#DAB5A2;">SKU</li>
                    <li style="width:160px;background-color:#DAB5A2; text-align:left">factory#</li>
                    <li style="width:150px;background-color:#DAB5A2;text-align:left" >supplier#</li>
                    <li style="width:480px; text-align:left;background-color:#DAB5A2;">middle name</li>
                    <li style="width: 100px;text-align:right;background-color:#DAB5A2;">cost$</li>
                    <li style="width:100px;background-color:#DAB5A2;text-align:right">price$</li>
                    <li style="width:100px; background-color:#DAB5A2;">Sale</li>                    
                </ul>
            </li>
        </HeaderTemplate>
        <ItemTemplate>
            <li style="width: 100%; clear: left; border-bottom: 1px solid #cccccc;">
                <ul class="ul_row" style="line-height:15px">
                    <li style="width: 40px"><%# DataBinder.Eval(Container.DataItem, "product_serial_no")%></li>
                    <li style="width:160px;text-align:left">&nbsp; <%# DataBinder.Eval(Container.DataItem, "manufacturer_part_number")%></li>
                    <li style="width:150px;text-align:left" >&nbsp;<%# DataBinder.Eval(Container.DataItem, "supplier_sku").ToString()%></li>
                    <li style="width:480px;text-align:left">&nbsp;<%# DataBinder.Eval(Container.DataItem, "product_name")%></li>
                    <li style="width:100px; text-align:right">&nbsp;<%# DataBinder.Eval(Container.DataItem, "product_current_cost")%></li>
                    <li style="width:100px;text-align:right"><%# DataBinder.Eval(Container.DataItem, "product_current_price")%></li>
                    <li style="width:100px">&nbsp;<%# DataBinder.Eval(Container.DataItem, "tag").ToString() == "1" ? "Y":"<span style='color:red'>N</span>"%></li>                    
                </ul>
            </li>
        </ItemTemplate>
    </asp:Repeater>
    </ul>
    
</asp:Content>

