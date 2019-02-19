<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master"  ValidateRequest="false" AutoEventWireup="true" CodeFile="product_edit_parts.aspx.cs" Inherits="Q_Admin_product_edit_parts" Title="Shopbot" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
                <asp:ListView runat="server" ID="lv_category_btn" 
                    ItemPlaceholderID="itemPlaceholderID" 
                    onitemcommand="lv_category_btn_ItemCommand">
                        <LayoutTemplate >
                                <table>
                                        <tr><td><div runat="server" id="itemPlaceholderID"></div></td></tr>
                                </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                                        <asp:button runat="server" ID="_btn_category" Text='<%# "第 "+ DataBinder.Eval(Container.DataItem, "position").ToString() +" 位 "+"("+DataBinder.Eval(Container.DataItem, "c").ToString()+")" %>' 
                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "position") %>' CommandName="ViewParts" />
                        </ItemTemplate>
                </asp:ListView>
                <hr size="1" />
                
                <asp:ListView runat="server" ID="lv_part_list" 
                    ItemPlaceholderID="itemPlaceholderID" >
                        <LayoutTemplate >
                            <div style="background:#bbbbbb;width: 1066px;">
                                <table cellspacing="1" cellpadding="2" style="border:1px solid #ccc;width: 100%" class="table_td_white" >
                                        <tr>
                                                <th width="60"></th>
                                                <th width="60">SKU</th>
                                                <th>name</th>
                                                <th width="100">Sell$</th>
                                                <th width="100">Cost$</th>
                                                <th width="100">shopbot sell$</th>
                                                <th width="100">位置</th>
                                        </tr>
                                        <tr runat="server" id="itemPlaceholderID"></tr>
                                </table>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                                    <tr  onmouseover='this.className="onmouseover";' onmouseout="this.className='onmouseout';">
                                            <td><a href="/q_admin/other_inc_view_compare.aspx?id=<%# DataBinder.Eval(Container.DataItem, "lu_Sku") %>&position=<%# DataBinder.Eval(Container.DataItem, "position") %>" onclick="winOpen(this.href, 'right_manage', 880, 800, 120, 200);return false;" title="Modify Detail">Shopbot</a></td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "lu_sku")%></td>
                                            <td><a href='/Product_parts_detail.asp?pro_class=<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>&id=<%# DataBinder.Eval(Container.DataItem, "lu_sku") %>&parent_id=<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>' target="_blank"><%#DataBinder.Eval(Container.DataItem, "product_name") %></a></td>
                                            <td style="text-align:right"><asp:Label runat="server" ID="_lbl_sell" Text='<%# DataBinder.Eval(Container.DataItem, "sell") %>'></asp:Label></td>
                                            <td style="text-align:right"><asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "cost") %>'></asp:Label></td>
                                            <td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "price") %></td>
                                            <td >&nbsp;&nbsp; <asp:Label runat="server" ID="Label2" Text='<%# DataBinder.Eval(Container.DataItem, "position") %>'></asp:Label> of <%#DataBinder.Eval(Container.DataItem, "c") %></td>
                                    </tr>                                       
                        </ItemTemplate>
                </asp:ListView>
</asp:Content>

