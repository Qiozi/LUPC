<%@ Page  Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="eBay_Sys_Category_Edit.aspx.cs" Inherits="Q_Admin_ebayMaster_LU_eBay_Sys_Category_Edit" Title="eBay Sys Category Edit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        table tr td{background: #fff;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
        <table>
            <tr>
                <td>Cate Name:</td>
                <td><asp:TextBox runat="server" ID="txt_cate_name" Columns="20"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Priority:</td>
                <td><asp:TextBox runat="server" ID="txt_priority" Columns="20" 
                       ></asp:TextBox></td>
            </tr>
            <tr>
                <td>Showit:</td>
                <td><asp:CheckBox runat="server" ID="cb_showit" 
                   ></asp:CheckBox></td>
            </tr>
            <tr>
                <td>Templete:</td>
                <td>
                    <asp:DropDownList ID="ddl_templete" runat="server">
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button runat="server" ID="btn_submit" Text="Submit" 
                        onclick="btn_submit_Click" /><asp:Label ID="Label_note" runat="server" 
                        ForeColor="Blue"></asp:Label></td>
            </tr>
        </table>
        
        <hr size="1" />
        
    </div>
    <asp:DataList ID="DataList1" runat="server" 
        onitemcommand="DataList1_ItemCommand"  >
        <HeaderTemplate>
            <div style='background:#ccc;width:600px;'>
            <table cellpadding='0' cellspacing='1' id='table1' width='100%'>
                <tr>
                    <td>Category ID</td>
                    <td>Category Name</td>
                    <td>Priority</td>
                    <td>Showit</td>
                    <td>Templte ID</td>
                    <td> CMD </td>
                </tr>            
        </HeaderTemplate>
        <ItemTemplate>
                <tr>                    
                    <td style="text-align:center;width:70px;">
                        <%# DataBinder.Eval(Container.DataItem, "category_id") %>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lbl_cate_name" Text='<%# DataBinder.Eval(Container.DataItem, "category_name") %>'></asp:Label> 
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lbl_priority" Text='<%# DataBinder.Eval(Container.DataItem, "priority") %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lbl_showit" Text='<%# DataBinder.Eval(Container.DataItem, "showit").ToString() %>'></asp:Label>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lbl_tpl_id" Text='<%# DataBinder.Eval(Container.DataItem, "tpl_id") %>'></asp:Label>
                    </td>
                    <td style="text-align:center;">
                        <asp:Button CommandArgument='<%# DataBinder.Eval(Container.DataItem, "category_ID") %>' CommandName="EditComment" Text="Edit" runat="server" ID="btn_edit"/>
                    </td>
                </tr>            
        </ItemTemplate> 
        <AlternatingItemTemplate>
                <tr>                    
                    <td style="text-align:center;width:50px; background:#f2f2f2;">
                        <%# DataBinder.Eval(Container.DataItem, "category_id") %>
                    </td>
                    <td style="background:#f2f2f2;">
                        <asp:Label runat="server" ID="lbl_cate_name" Text='<%# DataBinder.Eval(Container.DataItem, "category_name") %>'></asp:Label> 
                    </td>
                    <td style="background:#f2f2f2;">
                        <asp:Label runat="server" ID="lbl_priority" Text='<%# DataBinder.Eval(Container.DataItem, "priority") %>'></asp:Label>
                    </td>
                    <td style="background:#f2f2f2;">
                        <asp:Label runat="server" ID="lbl_showit" Text='<%# DataBinder.Eval(Container.DataItem, "showit").ToString() %>'></asp:Label>
                    </td>
                    <td style="background:#f2f2f2;">
                        <asp:Label runat="server" ID="lbl_tpl_id" Text='<%# DataBinder.Eval(Container.DataItem, "tpl_id") %>'></asp:Label>
                    </td>
                    <td style="text-align:center;background:#f2f2f2;">
                        <asp:Button CommandArgument='<%# DataBinder.Eval(Container.DataItem, "category_ID") %>' CommandName="EditComment" Text="Edit" runat="server" ID="btn_edit"/>
                    </td>
                </tr>   
        </AlternatingItemTemplate>
        <FooterTemplate>
            </table>
            </div>
        </FooterTemplate>        
        
    </asp:DataList>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

