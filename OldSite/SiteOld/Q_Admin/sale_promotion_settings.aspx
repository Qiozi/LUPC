<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sale_promotion_settings.aspx.cs" Inherits="Q_Admin_sale_promotion_settings" Title="Rebate edit" ResponseEncoding="gb2312" EnableEventValidation="false" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" />
    <script type="text/javascript" src="JS/change.js"></script>
    <br />
    <table id="product_parent_category">
        <tr>
            <td valign="middle" align="center">  
            <anthem:Repeater runat="server" ID="rptParentCategory" OnItemCommand="rptParentCategory_ItemCommand">
                <ItemTemplate>
                                        
                    <anthem:LinkButton id="_lbTitle" CssClass="Title1" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"menu_child_name") %>'></anthem:LinkButton>
           
                </ItemTemplate>
            </anthem:Repeater>    
            </td>
        </tr>
        
    </table>
    <hr size="1" />
    <table>
        <tr>
            <td>
                <anthem:DropDownList ID="ddlChildCategory" runat="server" AutoCallBack="True" OnSelectedIndexChanged="ddlChildCategory_SelectedIndexChanged">
    </anthem:DropDownList>
                <anthem:DropDownList ID="ddlChildSubCategory" runat="server" OnSelectedIndexChanged="ddlChildSubCategory_SelectedIndexChanged">
    </anthem:DropDownList>
            </td>
            <td>
                <anthem:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2" 
                    AutoCallBack="True" 
                    onselectedindexchanged="RadioButtonList1_SelectedIndexChanged">
                    <Items>
                        <asp:ListItem Value="2" Selected="True">Rebate</asp:ListItem>
                    </Items>
                </anthem:RadioButtonList></td>
            <td>
                <asp:Panel runat="server" ID="panel1" SkinID="btn" Width="100px">
                            <anthem:LinkButton runat="server" ID="lb_go" Text="Go" OnClick="lb_go_Click"></anthem:LinkButton>
                    </asp:Panel>
            </td>
            <td>
                <anthem:CheckBox ID="cb_all" runat="server" Text="all" Visible="False" /></td>
                
                <td>
                <asp:Panel runat="server" ID="panel2" SkinID="btn" Width="100px">
                            <anthem:LinkButton runat="server" ID="lb_save" Text="Save" OnClick="lb_save_Click"></anthem:LinkButton>
                    </asp:Panel>
            </td>
        </tr>
    </table>
    <hr size="1" />
    <table>
        <tr>
            <td valign="top" style="width: 80%"><anthem:DataGrid ID="dg_product_save" runat="server" AlternatingItemStyle-BackColor="#f0f0f0" AutoGenerateColumns="False" Width="100%" OnItemDataBound="dg_product_save_ItemDataBound" OnItemCommand="dg_product_save_ItemCommand">
        <HeaderStyle CssClass ="trTitle" />
        <Columns>
            <asp:BoundColumn DataField="product_serial_no" HeaderText="SKU"></asp:BoundColumn>
            <asp:ButtonColumn CommandName="ViewHistory" DataTextField="product_name" HeaderText="product name">
            </asp:ButtonColumn>
           <asp:BoundColumn DataField="manufacturer_part_number" HeaderText="Factory Part#"></asp:BoundColumn>
            <asp:BoundColumn DataField="product_current_cost" HeaderText="$cost" DataFormatString="{0:###.##}" >
                <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                    font-underline="False" horizontalalign="Right" />
            </asp:BoundColumn>
            
            <asp:BoundColumn DataField="product_current_price" HeaderText="$price" DataFormatString="{0:###.##}" >
                <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                    font-underline="False" horizontalalign="Right" />
            </asp:BoundColumn>
            <asp:TemplateColumn HeaderText="begin datetime">
                <itemtemplate>
<anthem:TextBox id="_txt_begin_datetime" runat="server"  onFocus="calendar()" __designer:wfdid="w6" CssClass="input" Columns="12" onchange="styleChange(this);" ></anthem:TextBox>
</itemtemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="end datetime">
                <itemtemplate>
<anthem:TextBox id="_txt_end_datetime" runat="server"  onFocus="calendar()" __designer:wfdid="w7" CssClass="input" Columns="12" onchange="styleChange(this);" ></anthem:TextBox>
</itemtemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="$Selling Price">
                <itemtemplate>
<anthem:TextBox id="_txt_save_cost" runat="server"  __designer:wfdid="w8" CssClass="input" Columns="12"  onchange="styleChange(this);" ></anthem:TextBox>
</itemtemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="$Discount"></asp:TemplateColumn>
           <asp:TemplateColumn HeaderText="comment">
                <itemtemplate>
<anthem:TextBox id="_txt_comment"  runat="server" CssClass="input" Columns="20"  onchange="styleChange(this);" ></anthem:TextBox>
</itemtemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="PDF Filename">
                <ItemTemplate>
                    <anthem:TextBox ID="_txt_pdf_filename" runat="server" __designer:wfdid="w10" 
                        Columns="20" CssClass="input" onchange="styleChange(this);" 
                        >
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </anthem:TextBox>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>

<AlternatingItemStyle BackColor="#F0F0F0"></AlternatingItemStyle>
    </anthem:DataGrid></td>
            <td valign="top">
                pro_img/rebate_pdf/文件夹下所有pdf文件
                
               
                <anthem:DataGrid runat="server" id="rpt_all_pdf_filename" 
                    AutoGenerateColumns="False"  Width="400px" CellPadding="4" 
                    ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <Columns>
                        <asp:BoundColumn DataField="filename" HeaderText="File name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="modify_datetime"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#999999" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
                </anthem:DataGrid>
                
            </td>
        </tr>
    </table>
</asp:Content>

