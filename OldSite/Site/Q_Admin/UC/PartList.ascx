<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PartList.ascx.cs" Inherits="Q_Admin_UC_PartList" %>
 <table cellspacing="0" cellpadding="0" style="width: 100%">   
                    <asp:ListView ID="ListView1" runat="server" itemContainerID="itemPlaceholder" 
                            onitemdatabound="ListView1_ItemDataBound" 
                            onitemcommand="ListView1_ItemCommand"  >
                    
                    <EmptyDataTemplate>
                        data is empty
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                        <div style="width: 1000px" id="itemPlaceholder" runat="server"> </div>                             
                    </LayoutTemplate>
                    <ItemTemplate>
                        <asp:Panel runat="server" ID="_panel_part_commont">
                            <tr>
                                <td rowspan="2">
                                     <asp:Button runat="server" ID="_btn_up" Text="Up" CommandName="UP" 
                                    onclientclick="ParentLoadWait();" Font-Size="8pt" Width="20" />
                                </td>
                                <td rowspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td><asp:Literal ID="_Literal_order_part_sum" runat="server"></asp:Literal></td>
                                            <td> <img src='http://www.lucomputers.com/pro_img/COMPONENTS/<%# DataBinder.Eval(Container.DataItem, "img_sku") %>_t.jpg' alt='<%# DataBinder.Eval(Container.DataItem, "img_sku") %>' />
                                    <br /><asp:CheckBox runat="server" id="_checkBox_showit" Checked="true" Text="Showit"/></td>
                                        </tr>
                                    </table>
                                    
                                   
                                </td>
                                <td>                                
                                </td>
                                <td style="text-align:left">
                                    <b>
                                    <asp:HiddenField runat="server" ID="_hiddenField_split_line" Value='<%# DataBinder.Eval(Container.DataItem, "split_line") %>' />
                                    <asp:Label runat="server" ID="_lbl_lu_sku" Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Label></b>
                                    <span style="color:#cccccc"> -- <asp:Label runat="server" ID="_lbl_lu_manufacture" Text='<%# DataBinder.Eval(Container.DataItem, "manufacturer_part_number") %>'></asp:Label></span><br />
                                    <ul>
                                        <li>
                                            <a target="_blank" href="/product_parts_detail.asp?pro_class=<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>&parent_id=<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>&id=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>"><%# DataBinder.Eval(Container.DataItem, "product_name") %></a>
                                           
                                        </li>
                                    </ul><div style="background:#f2f2f2;padding:2px; color:#cccccc; font-size:8pt;">
                                         Create Datetime: <%# DataBinder.Eval(Container.DataItem, "regdate") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                        Last Modify Datetime: <%# DataBinder.Eval(Container.DataItem, "last_regdate") %>
                                    </div><asp:CheckBoxList runat="server" ID="_check_box_list_virtual"  RepeatLayout="Flow" RepeatColumns="6"></asp:CheckBoxList>
                                    
                                   <asp:Literal runat="server" ID="_literal_on_sale_rebate"></asp:Literal>
                                </td>
                                <td style="text-align:right"><div style="line-height:22px"><asp:Literal runat="server" ID="_lbl_stock" Text='<%# DataBinder.Eval(Container.DataItem, "ltd_stock") %>'></asp:Literal></div>
                                Sum:<asp:TextBox runat="server" ID="_txt_part_sum" Columns="6"
                                 Text='<%# DataBinder.Eval(Container.DataItem, "product_store_sum") %>' CssClass="input_right_line"></asp:TextBox>
                                </td>
                                <td style="text-align:right">
                                    Cost:<asp:TextBox runat="server" ID="_txt_lu_cost" Columns="10"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_cost") %>' CssClass="input_right_line"></asp:TextBox>
                                    <br />
                                    Price:<asp:TextBox runat="server" ID="_txt_lu_price" Columns="10"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_price") %>' CssClass="input_right_line"></asp:TextBox>
                                    <div>
                                        Sold: <span style="color:#ff9900"><input type="text" readonly="readonly" size="10" value='<%# DataBinder.Eval(Container.DataItem, "product_current_sold") %>' class="input_right_line" /></span>
                                    </div> 
                                </td>
                                <td style="text-align:right; font-size:8pt">
                                    <a href="#top">Top</a><br />
                                    <asp:LinkButton runat="server" CommandName="Modify" Text="Save" ID="_lb_modify"  OnClientClick="ParentLoadWait();"></asp:LinkButton><br /> 
                                    <a href="/product_part_desc.aspx?product_id=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>" onclick="winOpen(this.href, 'right_manage', 620, 600, 120, 200);return false;" title="Modify Detail">Edit</a><br /> 
                                    <a href="part_and_group.aspx?partid=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>" onclick="winOpen(this.href, 'right_manage', 620, 600, 120, 200);return false;" title="Modify Detail">Edit Group</a>
                                    </td>
                            </tr>
                            <tr>
                                <td colspan="7" style="border-top: 1px dotted #D5582e; line-height: 20px; ">
                                    
                                    <asp:Literal runat="server" ID="_literal_ravil_vendor"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="background:#999; line-height:1px" colspan="7">&nbsp;</td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="_panel_part_title" >
                            <tr>
                                <td style="background:#E7E7E7;">
                                <asp:Button runat="server" ID="_btn_up_title" Text="Up" CommandName="UP" 
                                    onclientclick="ParentLoadWait();" Font-Size="8pt" Width="20" />
                                </td>
                                <td style="background:#E7E7E7; color:green; line-height:20px; text-align:center; font-weight:bold" colspan="6">
                                <asp:Label runat="server" ID="_lbl_lu_sku_title" ></asp:Label>&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "product_short_name") %>
                                <br />
                                <asp:CheckBoxList runat="server" ID="_check_box_list_virtual_title"  RepeatLayout="Flow" RepeatColumns="6"></asp:CheckBoxList>                                
                                </td>
                            </tr>
                        </asp:Panel>
                    </ItemTemplate>
                    
                </asp:ListView>  
                </table>