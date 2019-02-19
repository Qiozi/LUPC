<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeFile="part_showit_manage.aspx.cs" Inherits="part_showit_manage" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>edit parts(list)</title>
    <script type="text/javascript" src="Q_Admin/JS/Loading.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h2>
            <anthem:Label ID="lbl_menu_child_name" runat="server"></anthem:Label>
        </h2>
        <hr size="1" />
            <anthem:CheckBox runat="server" id="cb_showit_all" Text="Show All" />&nbsp;
            <anthem:Button ID="btn_change" runat="server" Text="Change" OnClick="btn_change_Click" PostCallBackFunction="Anthem_PostCallBack" PreCallBackFunction="Anthem_PreCallBack" />&nbsp;
            <anthem:CheckBox runat="server" id="cb_all_checked" 
            Text="Checked All Showit" OnCheckedChanged="cb_all_checked_CheckedChanged" 
            AutoCallBack="True" />
            <anthem:CheckBox runat="server" id="cb_all_export_checked" 
            Text="Checked All Export" 
            OnCheckedChanged="cb_all_export_checked_CheckedChanged" AutoCallBack="True" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <anthem:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" PostCallBackFunction="Anthem_PostCallBack" PreCallBackFunction="Anthem_PreCallBack" />
       
         &nbsp;
        <anthem:CheckBox ID="CheckBox_display_stock_status" AutoCallBack="true" 
            Text="Display Stock" runat="server" 
            oncheckedchanged="CheckBox_display_stock_status_CheckedChanged" />
            
            
            
         <hr size="1" />
            <anthem:DataGrid runat="server" ID="dg_part" AutoGenerateColumns="False" 
            OnItemDataBound="dg_part_ItemDataBound" onitemcommand="dg_part_ItemCommand">
                <Columns>
                    <asp:BoundColumn DataField="product_serial_no" HeaderText="SKU"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Showit">
                        <itemtemplate>
<anthem:CheckBox id="_cb_showit" runat="server" __designer:wfdid="w1"  Checked='<%#DataBinder.Eval(Container.DataItem,"tag").ToString()=="1" ? true:false %>' ></anthem:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="New">
                        <itemtemplate>
<anthem:CheckBox id="_cb_new" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"new_product").ToString()=="1" ? true:false %>' ></anthem:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Export">
                        <itemtemplate>
<anthem:CheckBox id="_cb_export" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"export") %>' ></anthem:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="green line">
                        <itemtemplate>
<anthem:CheckBox id="_cb_split_line" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"split_line").ToString()=="1" ? true:false %>' ></anthem:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="hot">
                        <ItemTemplate>
                            <anthem:CheckBox ID="_cb_hot" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"hot").ToString()=="1" ? true:false %>' />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="product_current_cost" HeaderText="Cost$">
                        <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                            font-underline="False" horizontalalign="Right" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Price$">
                        <itemtemplate>
<anthem:TextBox id="_txt_price" runat="server" __designer:wfdid="w2" Columns="8"  Text='<%#DataBinder.Eval(Container.DataItem,"product_current_price") %>'></anthem:TextBox>
</itemtemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="sold price$">
                        <ItemTemplate>
                        <anthem:Label runat="server" id="_lbl_sold_price">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </anthem:Label>
                        </ItemTemplate>
                        
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Factory Part#">
                        <itemtemplate>
<anthem:TextBox id="_txt_manufacturer_part_number" runat="server" Columns="18"  Text='<%#DataBinder.Eval(Container.DataItem,"manufacturer_part_number") %>'></anthem:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="supplierSku">
                        <itemtemplate>
<anthem:TextBox id="_txt_supplier_sku" runat="server" Columns="10"  Text='<%#DataBinder.Eval(Container.DataItem,"supplier_sku") %>'></anthem:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Short Name">
                        <itemtemplate>
<anthem:TextBox id="_txt_short_name" runat="server" __designer:wfdid="w4" Columns="40"  Text='<%#DataBinder.Eval(Container.DataItem,"product_short_name") %>'></anthem:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Middle Name">
                        <itemtemplate>
<anthem:TextBox id="_txt_middle_name" runat="server" __designer:wfdid="w5" Columns="70"  Text='<%#DataBinder.Eval(Container.DataItem,"product_name") %>'></anthem:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Priority">
                        <itemtemplate>
<anthem:TextBox id="_txt_priority" runat="server" Columns="8"  Text='<%#DataBinder.Eval(Container.DataItem,"product_order") %>'></anthem:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                   
                    <asp:ButtonColumn CommandName="ChangeGroup" HeaderText="ChangeGroup" 
                        Text="Change"></asp:ButtonColumn>
                </Columns>
            </anthem:DataGrid>
    </div>
    </form>
</body>
</html>
