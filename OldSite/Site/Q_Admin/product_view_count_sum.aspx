<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_view_count_sum.aspx.cs" Inherits="Q_Admin_product_view_count_sum" Title="Untitled Page" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
            <tr>
                    <td>
                        <uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" />
                    </td>
                    <td>
                        <asp:Button ID="btn_part" runat="server" Text="Go" Enabled="False" />
                    </td>
            </tr>
    </table>
    <hr size="1" />
    <table>
            <tr>
                    <td width="400"><asp:Label runat="server" ID="lbl_lv_1_title" Text="ALL Category"></asp:Label></td>
                    <td><asp:Label runat="server" ID="lbl_lv_2_title" Text="Top 100"></asp:Label></td>
            </tr>
            <tr>
                                <td valign="top">
                                
                              <asp:ListView runat="server" ID="lv_cate_view_count_list" 
                                            ItemPlaceholderID="itemPlaceHolderID" >
                                                <LayoutTemplate>
                                                        <div style="background:#bbbbbb;">
                                                        <table cellspacing="1" cellpadding="2" style="border:1px solid #ccc;width:100%" id="table_part_list" class="table_td_white" >
                                                                <tr>
                                                                 <th>Name </th>
                                                                 <th>Sum</th>                                                            
                                                                </tr>
                                                               
                                                                <tr runat="server" id="itemPlaceHolderID"></tr>
                                                        </table></div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                                <tr>
                                                                        <td><%# DataBinder.Eval(Container.DataItem, "name") %></td>
                                                                        <td style="text-align:right;"><%# DataBinder.Eval(Container.DataItem, "view_count")%></td>                                                                        
                                                                </tr>
                                                </ItemTemplate>
                                 </asp:ListView>
                                   </td> 
                                 <td valign="top">
                                    <asp:ListView runat="server" ID="lv_part_view_count_list" 
                                            ItemPlaceholderID="itemPlaceHolderID" >
                                                <LayoutTemplate>
                                                        <div style="background:#bbbbbb;">
                                                        <table cellspacing="1" cellpadding="2" style="border:1px solid #ccc;width: 100%" id="table_part_list" class="table_td_white">
                                                                <tr>
                                                                 <th>Name </th>
                                                                 <th>Sum</th>                                                                
                                                                </tr>
                                                               
                                                                <tr runat="server" id="itemPlaceHolderID"></tr>
                                                        </table></div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                                <tr>
                                                                        <td><%# DataBinder.Eval(Container.DataItem, "name") %></td>
                                                                        <td style="text-align:right;"><%# DataBinder.Eval(Container.DataItem, "view_count")%></td>                                                                        
                                                                </tr>
                                                </ItemTemplate>
                                 </asp:ListView>                               
                                 </td>
            </tr>
    </table>
</asp:Content>

