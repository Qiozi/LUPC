<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sale_promotion_History.aspx.cs" Inherits="Q_Admin_sale_promotion_History" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Promotion History</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h3>
            
        <anthem:Label ID="lbl_product" runat="server"></anthem:Label>
        </h3>
        <div style="text-align:center"><anthem:Button ID="btn_save" runat="server" 
                onclick="btn_save_Click" Text="Save" /></div>
        <anthem:RadioButtonList ID="radio_rebate" runat="server" AutoCallBack="True" 
            onselectedindexchanged="radio_rebate_SelectedIndexChanged" RepeatColumns="2">
            <Items>
                <asp:ListItem Selected="True" Value="2">rebate</asp:ListItem>
            </Items>
        </anthem:RadioButtonList>
        <hr size="1" />
        
        <anthem:DataGrid runat="server" ID="dg_product_history" AutoGenerateColumns="False" Width="100%">
            <HeaderStyle CssClass="trTitle" />
            <Columns>
                <asp:BoundColumn DataField="sale_promotion_serial_no" HeaderText="ID">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="begin_datetime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="begin datetime">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="end_datetime" DataFormatString="{0:yyyy-MM-dd}" HeaderText="end_datetime">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="price" HeaderText="price"></asp:BoundColumn>
                <asp:BoundColumn DataField="cost" HeaderText="cost"></asp:BoundColumn>
                <asp:BoundColumn DataField="save_cost" HeaderText="save_price"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Showit">
                    <ItemTemplate>
                        <anthem:CheckBox ID="_cb_showit" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"show_it").ToString()=="1" ? true:false %>' />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </anthem:DataGrid>
    </div>
    </form>
</body>
</html>
