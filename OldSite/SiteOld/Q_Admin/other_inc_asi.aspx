<%@ Page Title="Asi" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="other_inc_asi.aspx.cs" Inherits="Q_Admin_other_inc_asi" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
#table1 td{ background: #ffffff;}
</style>
                                <table>
                                    
                                        <tr>
                                                <td>
                                                    <uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" CFT="ASI" />
                                                    
                                                    </td>
                                                <td>and<asp:DropDownList runat="server" ID="ddl_vendor"></asp:DropDownList>
                                                    
                                                    </td>
                                                <td><asp:Button runat="server" ID="btn_search" Text="Search" 
                                                        onclick="btn_search_Click" onclientclick="ParentLoadWait();" /></td>
                                                <td rowspan="3">
                                                    &nbsp;</td>
                                                <td rowspan="3">
                                                    &nbsp;</td>
                                        </tr>
                                        <tr>
                                                <td style="text-align:right">&nbsp;</td>
                                                <td style="text-align:right"><asp:TextBox runat="server" ID="txt_asi_sku"></asp:TextBox></td>
                                                <td><asp:Button runat="server" ID="btn_search_asi_sku" 
                                                        Text="Search ASI SKu" onclick="btn_search_asi_sku_Click" 
                                                        onclientclick="ParentLoadWait();" /></td>
                                        </tr>
                                       <tr>
                                                <td style="text-align:right">&nbsp;</td>
                                                <td style="text-align:right"><asp:TextBox runat="server" ID="txt_manufacturer_part_number"></asp:TextBox></td>
                                                <td><asp:Button runat="server" ID="btn_search_mfp" Text="Search MFP#" 
                                                        onclick="btn_search_mfp_Click" onclientclick="ParentLoadWait();" /></td>
                                        </tr>
                                       
                                </table>
                                <hr size="1" />
                                <asp:ListView runat="server" ID="lv_asi" 
                                    ItemPlaceholderID = "itemPlaceholderID" onitemdatabound="lv_asi_ItemDataBound">
                                        <LayoutTemplate >
                                                <div style="background: #ccc;">
                                                <table id="table1" cellspacing="1">
                                                        <tr runat="server" id="itemPlaceholderID"></tr>
                                                </table>
                                                </div>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                                <tr>
                                                        <td>
                                                                <asp:HiddenField runat="server" ID="_hf_id" Value='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>' /><asp:CheckBox runat="server" ID="_cb_checked" />
                                                        </td>
                                                        <td>        <asp:LinkButton runat="server" ID="_lb_edit" Text ="ADD" Visible="false" CommandName="EditPart"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id").ToString() %>'></asp:LinkButton>
                                                                    <asp:Literal runat="server" ID="_literal_winopen"></asp:Literal>
                                                                    <ul class="ul_parent">
                                                                            <li>
                                                                                    <asp:Label runat="server" ID="_lbl_lu_sku" Text='<%# DataBinder.Eval(Container.DataItem, "lu_sku").ToString() %>' ForeColor="Red"></asp:Label>
                                                                                    <div style="color:Blue;">
                                                                                        <%# DataBinder.Eval(Container.DataItem, "priority").ToString() %>
                                                                                    </div>
                                                                            </li>
                                                                    </ul>
                                                        </td>
                                                        <td>
                                                                 <%# DataBinder.Eval(Container.DataItem, "itmeid").ToString() %>
                                                        </td>
                                                        <td>
                                                                 <%# DataBinder.Eval(Container.DataItem, "name").ToString() %>
                                                        </td>
                                                        <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "description").ToString() %>
                                                        </td>
                                                        <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "vendor").ToString() %>
                                                        </td>
                                                        <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "status").ToString() %>
                                                        </td>
                                                        <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "price").ToString() %>
                                                        </td>
                                                        <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "quantity").ToString()%>
                                                        </td>
                                                        <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "mainCatName").ToString()%>
                                                        </td>
                                                </tr>
                                        </ItemTemplate>
                                </asp:ListView>
</asp:Content>

