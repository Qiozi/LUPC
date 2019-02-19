<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_view_sort.aspx.cs" Inherits="Q_Admin_product_helper_view_sort" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <table>
        <tr>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="2">
                            <asp:ListItem Value="1" Text="middle name"></asp:ListItem>
                            <asp:ListItem Value="2" Text="long name" Selected></asp:ListItem>
                    </asp:RadioButtonList>
                </td>
                <td><uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btn_go" runat="server" Text="Go" onclick="btn_go_Click" />&nbsp;<asp:Button 
                        ID="btn_save" runat="server" Text="Save" onclick="btn_save_Click" />
&nbsp;<asp:Button ID="btn_initial_proirity" runat="server" Text="Initial Proirity" 
                        onclick="btn_initial_proirity_Click" />
                </td>
        </tr>
   </table>
   <hr />
   
   <asp:ListView runat="server" ID="lv_part_list" ItemPlaceholderID="itemPlaceholderID">
            <LayoutTemplate>
                    <div style="background:#f2f2f2;">
                        <table cellpadding="0" cellspacing="0" id="table_part_list" style="border:1px solid #ccc;width: 100%">
                            <tr id="itemPlaceholderID" runat="server" ></tr>
                        </table>
                    </div>
            </LayoutTemplate>
            <ItemTemplate>
                    <tr>
                            <td style="width:60px; border-bottom: 1px solid #ccc;<%# DataBinder.Eval(Container.DataItem, "split_line").ToString()=="1"? "background: green;color: white;":"background: white;"%>"><asp:Label runat="server" ID="_lbl_sku" Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Label></td>
                            <td style="width:80px; border-bottom: 1px solid #ccc;<%# DataBinder.Eval(Container.DataItem, "split_line").ToString()=="1"? "background: green;color: white;":"background: white;"%>"><asp:TextBox runat="server" ID="_txt_priority" Columns="8" Text='<%# DataBinder.Eval(Container.DataItem,"product_order") %>'></asp:TextBox></td>
                            <td style=" border-bottom: 1px solid #ccc;<%# DataBinder.Eval(Container.DataItem, "split_line").ToString()=="1"? "background: green;color: white;":"background: white;"%>"><asp:Label runat="server" ID="_lbl_part_name" Text='<%# DataBinder.Eval(Container.DataItem, "product_name") %>'></asp:Label></td>
                            <td style="text-align:right; border-bottom: 1px solid #ccc;<%# DataBinder.Eval(Container.DataItem, "split_line").ToString()=="1"? "background: green;color: white;":"background: white;"%>"><%# DataBinder.Eval(Container.DataItem, "product_sell") %></td>
                            <td style="text-align:right;width: 50px; border-bottom: 1px solid #ccc;<%# DataBinder.Eval(Container.DataItem, "split_line").ToString()=="1"? "background: green;color: white;":"background: white;"%>"><a href="/q_admin/editPartDetail.aspx?id=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>" onclick="winOpen(this.href, 'modify_detail', 880, 800, 120, 200);return false;" title="Modify Detail">Edit</a></td>
                    </tr>
            </ItemTemplate>        
   </asp:ListView>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

