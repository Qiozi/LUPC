<%@ Page Title="上传进价" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sales_upload_part_import_price.aspx.cs" Inherits="Q_Admin_sales_upload_part_import_price" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<table style="width: 100%">
        <tr>
                <td style="width: 49%;" valign="top">
                    <h3>上传有订单号的产品进价列表</h3>
                    <asp:FileUpload ID="FileUpload1" runat="server" /><asp:Button runat="server" 
                        ID="btn_upload_order_code" Text="Upload" 
                        onclick="btn_upload_order_code_Click" />
                    <br />
                    
                    <asp:Label runat="server" ID="lbl_upload_last_time_order_code"></asp:Label>
                    
                </td>
            
                <td valign="top">
                    <h3>上传产品进价列表</h3>
                    <asp:FileUpload ID="FileUpload2" runat="server" />
                    <asp:Button runat="server" 
                        ID="btn_upload_no_order" Text="Upload" onclick="btn_upload_no_order_Click" 
                         />
                    <br />
                    
                    <asp:Label runat="server" ID="Label1"></asp:Label>
                </td>
        </tr>
        <tr>
                <td colspan="2"><hr size="1" /></td>
        </tr>
        <tr>
                <td colspan="2"><table>
                            <tr>
                                    <td>&nbsp;</td>
                                    <td>×注意：</td>
                                    <td colspan="5">1. 只能上传xls结尾的excel文件， sheet 名称改为 table, <span style="color: blue;">单元格式使用文本格式</span></td>
                                    <td rowspan="3">
                    <asp:Button runat="server" 
                        ID="btn_change_price" Text="更新刚上传的产品价格" onclick="btn_change_price_Click" Height="44px" Enabled="False" 
                         />
                                    </td>
                            </tr>
                            <tr>
                                    <td width="50"> &nbsp;</td>
                                    <td width="50"> &nbsp;</td>
                                    <td colspan="5"> 2. 第一行请用以下字母(Column name 格式)：</td>
                            </tr>
                            <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td width="50">&nbsp;</td>
                                    <td style="font-weight: bold;">inc</td><td width="100">上家公司名称</td>
                                    <td style="font-weight: bold;">quantity</td><td style="width: 316px">产品数量</td>
                            </tr>
                            <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td style="font-weight: bold;">name</td><td>产品名称</td>
                                    <td style="font-weight: bold;">cost</td><td style="width: 316px">进价</td>
                                    <td style="width: 316px">&nbsp;</td>
                            </tr>
                            <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td style="font-weight: bold;">sell</td><td>售价</td>
                                    <td style="font-weight: bold;">vendor_invoice</td><td style="width: 316px">上家订单发票号</td>
                                    <td style="width: 316px">&nbsp;</td>
                            </tr>
                            <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td style="font-weight: bold;">sku</td><td>产品SKU</td>
                                    <td style="font-weight: bold;">order_codes</td><td style="width: 316px"> 订单编号，如果多个请用逗号分割(",")</td>
                                    <td style="width: 316px"> &nbsp;</td>
                            </tr>
                            <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td style="font-weight: bold;">note</td><td>备注</td><td></td>
                                    <td style="width: 316px"></td>
                                    <td style="width: 316px">&nbsp;</td>
                            </tr>
                       </table>
                </td>
        </tr>
        <tr>
                <td colspan="2"><hr size="1" /><h3>今日上传</h3></td>
        </tr>
        <tr>
                <td colspan="2">
                    <asp:ListView runat="server" ID="lv_part_list" 
                        ItemPlaceholderID="itemPlaceholderID" 
                        onitemcommand="lv_part_list_ItemCommand" 
                        onitemdatabound="lv_part_list_ItemDataBound">
                            <LayoutTemplate>
                                    <div>
                                    <table class="table_bottom_line" style="width: 100%;" cellspacing="0" cellpadding="2">
                                            <tr>       
                                                    <th></th>           
                                                    <th>Order</th>                                  
                                                    <th>Inc.</th>
                                                    <th>Quantity</th>
                                                    <th>Name</th>
                                                    <th>Cost$</th>
                                                    <th>Sell$</th>
                                                    <th>差价(cost)</th>
                                                    <th>Current Cost$</th>
                                                    <th>Current Sell$</th>
                                                    <th>vendor invoice#</th>
                                                    <th>LUC SKU</th>
                                                    <th>Order Codes#</th>
                                                    <th>Note</th>
                                                    <th>Cmd</th>
                                            </tr>
                                            <tr runat="server" id="itemPlaceholderID">
                                            
                                            </tr>
                                    </table>
                                    </div>
                            </LayoutTemplate>
                            <ItemTemplate>
                                    <tr onmouseover="this.className='onmouseover';" onmouseout="this.className='onmouseout';">
                                            <td width="50" ><asp:HiddenField runat="server" ID="_hf_menu_child_serial_no" Value='<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>' /><asp:Literal runat="server" ID="_lb_shopbot" ></asp:Literal></td>
                                            <td style="color:Green; text-align:center;"><%# DataBinder.Eval(Container.DataItem, "is_order").ToString() == "1" ? "Y":""%></td>
                                            <td>&nbsp;<%# DataBinder.Eval(Container.DataItem, "other_inc_name")%></td>
                                            <td style=" text-align:center;">&nbsp;<%# DataBinder.Eval(Container.DataItem, "part_quantity")%></td>
                                            <td>&nbsp;<%# DataBinder.Eval(Container.DataItem, "part_name")%></td>
                                            <td style="text-align:right;">&nbsp;$<asp:Label runat="server" ID="_lbl_part_real_cost" Text='<%# DataBinder.Eval(Container.DataItem, "part_real_cost")%>'></asp:Label></td>
                                            <td style="text-align:right;">&nbsp;$<%# DataBinder.Eval(Container.DataItem, "part_sell")%></td>
                                            <td style="text-align:right;">&nbsp;<asp:Label runat="server" ID="_lbl_price_defference" Text='<%# DataBinder.Eval(Container.DataItem, "price_difference")%>'></asp:Label></td>
                                            <td style="text-align:right;">&nbsp;<asp:Label runat="server" ID="_lbl_current_cost" Text='<%# DataBinder.Eval(Container.DataItem, "current_cost")%>'></asp:Label></td>
                                            <td style="text-align:right;">&nbsp;<asp:Label runat="server" ID="_lbl_current_sell" Text='<%# DataBinder.Eval(Container.DataItem, "current_sell")%>'></asp:Label></td>
                                            <td>&nbsp;<%# DataBinder.Eval(Container.DataItem, "vendor_invoice")%></td>
                                            <td>&nbsp;<asp:Label runat="server" ID="_lbl_luc_sku" Text='<%# DataBinder.Eval(Container.DataItem, "luc_sku")%>'></asp:Label></td>
                                            <td>&nbsp;<%# DataBinder.Eval(Container.DataItem, "order_codes")%></td>
                                            <td>&nbsp;<%# DataBinder.Eval(Container.DataItem, "note")%></td>
                                            
                                            <td><asp:LinkButton runat="server" ID="_lb_delete" CommandName="DeleteRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id")%>'  OnClientClick="return confirm('Sure?');" Text="Del"></asp:LinkButton></td>
                                    </tr>
                            </ItemTemplate>
                    </asp:ListView>
                </td>
            
        </tr>
</table>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

