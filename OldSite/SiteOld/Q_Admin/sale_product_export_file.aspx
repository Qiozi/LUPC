<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sale_product_export_file.aspx.cs" Inherits="Q_Admin_sale_product_export_file" Title="Part Export File" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register src="UC/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <uc1:Navigation ID="Navigation1" runat="server" 
        NavigationText="Export Product Categories" />
    <anthem:Button ID="btn_save_setting" runat="server" 
        onclick="btn_save_setting_Click" Text="save" />
    <anthem:Button ID="btn_create_file" runat="server" 
        onclick="btn_create_file_Click" Text="Create File" />
    <br />
    <table cellspacing="0" width="1000" >
        <tr>
            <td colspan="2">
                <anthem:Label ID="Label1" runat="server">/</anthem:Label>
                <anthem:TextBox ID="txt_path" runat="server" Columns="50" Text="export"></anthem:TextBox>
            </td>
            <td align="center" style="border-left: 1px solid #cccccc">
                输入文件名，只能计算xml文件里的產品數量：<asp:TextBox ID="txt_xml_name" runat="server" 
                    Columns="50"></asp:TextBox>
    <asp:Button ID="btn_account" runat="server" 
        onclick="btn_account_Click" Text="Run" />
            </td>
        </tr>
        <tr>
            <td>
                <anthem:RadioButtonList ID="radio_file_type" runat="server" 
                    onselectedindexchanged="radio_file_type_SelectedIndexChanged" 
                    AutoCallBack="True">
                    <Items>
                        <asp:ListItem Value="1">Print Excel file: </asp:ListItem>
                        <asp:ListItem Value="2">Create CSV file: </asp:ListItem>
                        <asp:ListItem Value="3">Create XML file: </asp:ListItem>
                    </Items>
                </anthem:RadioButtonList>
            </td>
            <td>
                Delitimer:<anthem:DropDownList ID="ddl_csv_type" runat="server">
                    <asp:ListItem Value="1">tab char</asp:ListItem>
                    <asp:ListItem Value="3">&lt;tab&gt;</asp:ListItem>
                    <asp:ListItem Value="2">comm</asp:ListItem>
                </anthem:DropDownList>
            </td>
            <td valign="top" align="center" style="border-left: 1px solid #cccccc">
                產品數量：<asp:Label ID="lbl_sum" runat="server" Text="0"></asp:Label>
            </td>
        </tr>
        <tr>
            <td >File Name:</td><td><anthem:TextBox ID="txt_file_name" runat="server" 
                    Columns="20" Text="lucomputers"></anthem:TextBox>.<anthem:TextBox 
                    ID="txt_file_ext" runat="server" Columns="4"></anthem:TextBox></td>
            <td style="border-left: 1px solid #cccccc">&nbsp;</td>
        </tr>
    </table>
    <hr size="1" />
    <anthem:CheckBox ID="cb_export_all" runat="server" Text="Checked All Export" 
        AutoCallBack="True" oncheckedchanged="cb_export_all_CheckedChanged" />
    <hr size="1" />
    <table>
        <tr>
            <td style="width: 70%">
                <anthem:DataGrid ID="dg_product_category" runat="server" AlternatingItemStyle-BackColor="#f2f2f2" 
                    AutoGenerateColumns="False" 
                    onitemcommand="dg_product_category_ItemCommand">
                    <Columns>
                        <asp:BoundColumn DataField="id" HeaderText="CategoryID"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="export">
                            <ItemTemplate>
                                <anthem:CheckBox ID="cb_checked" runat="server" Checked='<%#DataBinder.Eval(Container.DataItem,"export").ToString()=="1" ? true:false %>'/>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="short_name" HeaderText="name1"></asp:BoundColumn>
                        <asp:BoundColumn DataField="name" HeaderText="name2">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ready" HeaderText="ready"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="Manage" HeaderText="Mange" Text="Manage ">
                        </asp:ButtonColumn>
                    </Columns>

            <AlternatingItemStyle BackColor="#F2F2F2"></AlternatingItemStyle>
                </anthem:DataGrid>            
            </td>
            <td valign="top">
                
                <anthem:DataGrid runat="server" id="rpt_all_filename" 
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

