<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_Promotional_shipping.aspx.cs" Inherits="Q_Admin_product_helper_Promotional_shipping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>            

                    <asp:TextBox runat="server" ID="txt_lu_sku"></asp:TextBox>
                    <asp:Button runat="server" ID="btn_set_store" Text="Set in " 
                        onclick="btn_set_store_Click" />
                    <asp:Button runat="server" ID="btn_save_all" Text="Save All" 
                        onclick="btn_save_all_Click" />
                    <asp:Button runat="server" ID="btn_remove_all" Text="Remove All" 
                        OnClientClick="return confirm('Sure?');" onclick="btn_remove_all_Click" />
                    <hr size="1" />
                    <asp:ListView runat="server" ID="lv_sale_promotion_shipping_charge" 
                        ItemPlaceholderID="itemPlaceholderID" 
                        onitemcommand="lv_sale_promotion_shipping_charge_ItemCommand">
                            <LayoutTemplate>
                                    <table class="table_small_font" cellpadding="0" cellspacing="0">
                                            <tr>
                                                 <th></th>
                                                    <th>LU SKU</th>
                                                    <th>Name</th>
                                                    <th>$CA</th>
                                                    <th>$US</th>
                                                    <th></th>
                                                    <th></th
                                            </tr>
                                            <tr runat="server" id="itemPlaceholderID"></tr>
                                    </table>
                            </LayoutTemplate>
                            <ItemTemplate>
                                            <tr>
                                                    <td style="background: #fff"><asp:HiddenField runat="server" ID="_hf_id" Value='<%# DataBinder.Eval(Container.DataItem, "prod_shipping_fee_id") %>' /></td>
                                                    <td style="background: #fff"><%# DataBinder.Eval(Container.DataItem, "prod_sku") %></td>
                                                    <td style="background: #fff"><%# DataBinder.Eval(Container.DataItem, "product_name") %></td>
                                                    <td style="background: #fff"><asp:TextBox runat="server" id="_txt_price_ca" text='<%# DataBinder.Eval(Container.DataItem, "shipping_fee_ca") %>'></asp:TextBox></td>
                                                    <td style="background: #fff"><asp:TextBox runat="server" id="_txt_price_us" text='<%# DataBinder.Eval(Container.DataItem, "shipping_fee_us") %>'></asp:TextBox></td>
                                                    <td style="background: #fff"><asp:Button runat="server" ID="_btn_remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "prod_shipping_fee_id") %>' CommandName="DeletePart" Text="remove" OnClientClick="if( confirm('Sure?')) return true; else return false;" /></td>
                                                    <td style="background: #fff"><asp:Button runat="server" ID="_btn_save" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "prod_shipping_fee_id") %>' CommandName="SavePart" Text="Save" UseSubmitBehavior="False" /></td>
                                             </tr>
                            </ItemTemplate>
                            <AlternatingItemTemplate>
                                            <tr>
                                                    <td style="background: #f2f2f2"><asp:HiddenField runat="server" ID="_hf_id" Value='<%# DataBinder.Eval(Container.DataItem, "prod_shipping_fee_id") %>' /></td>
                                                    <td style="background: #f2f2f2"><%# DataBinder.Eval(Container.DataItem, "prod_sku") %></td>
                                                    <td style="background: #f2f2f2"><%# DataBinder.Eval(Container.DataItem, "product_name") %></td>
                                                    <td style="background: #f2f2f2"><asp:TextBox runat="server" id="_txt_price_ca" text='<%# DataBinder.Eval(Container.DataItem, "shipping_fee_ca") %>'></asp:TextBox></td>
                                                    <td style="background: #f2f2f2"><asp:TextBox runat="server" id="_txt_price_us" text='<%# DataBinder.Eval(Container.DataItem, "shipping_fee_us") %>'></asp:TextBox></td>
                                                    <td style="background: #f2f2f2"><asp:Button runat="server" ID="_btn_remove" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "prod_shipping_fee_id") %>' CommandName="DeletePart" Text="remove" OnClientClick="if( confirm('Sure?')) return true; else return false;" /></td>
                                                    <td style="background: #f2f2f2"><asp:Button runat="server" ID="_btn_save" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "prod_shipping_fee_id") %>' CommandName="SavePart" Text="Save" UseSubmitBehavior="False" /></td>
                                             </tr>   
                            </AlternatingItemTemplate>
                            <EmptyDataTemplate>
                                    No Match Data.
                            </EmptyDataTemplate>
                    </asp:ListView>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

