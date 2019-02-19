<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_ebay_item.aspx.cs" Inherits="Q_Admin_orders_edit_detail_ebay_item2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
    
    <p style="text-align:center;"><asp:Button runat="server" ID="btn_add" Text="添加进入订单" 
            onclick="btn_add_Click" /></p>
    <asp:CheckBox runat="server" Visible="false" ID="cb_sys" Text="添加为一个系统" />
    <hr size='1' />
        <asp:DataList ID="DataList1" runat="server">
            <HeaderTemplate>
                <table>
                    
            </HeaderTemplate>
            <ItemTemplate>
                        <tr>

                            <td><%#DataBinder.Eval(Container.DataItem, "luc_sku")%></td>
                            <td><b><%#DataBinder.Eval(Container.DataItem, "commentName")%></b></td>
                            <td><%#DataBinder.Eval(Container.DataItem, "product_ebay_name")%></td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:DataList>
    </div>
</asp:Content>

