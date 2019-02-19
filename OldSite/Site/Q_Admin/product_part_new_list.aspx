<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="product_part_new_list.aspx.cs" Inherits="Q_Admin_product_part_new_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
                <asp:Button runat="server" ID="btn_issue_all_ok" Text="Issue And Save!" 
                    onclick="btn_issue_all_ok_Click" 
                    onclientclick="return confirm('Sure!!!');" />
                <asp:Button runat="server" ID="btn_View_OK" Text="Show OK" 
                    onclick="btn_View_OK_Click" onclientclick="ParentLoadWait();" />
                <asp:Button runat="server" ID="btn_view_all" Text = "Show All" 
                    onclick="btn_view_all_Click" onclientclick="ParentLoadWait();" 
                    Visible="False" />
    <hr size="1" />
                <asp:ListView runat="server" ID="lv_category_btn" 
                    ItemPlaceholderID="itemPlaceholderID" 
                    onitemcommand="lv_category_btn_ItemCommand">
                        <LayoutTemplate >
                                <table>
                                        <tr><td><div runat="server" id="itemPlaceholderID"></div></td></tr>
                                </table>
                        </LayoutTemplate>
                        <ItemTemplate>
                                        <asp:button runat="server" ID="_btn_category" Text='<%# DataBinder.Eval(Container.DataItem, "menu_child_name").ToString() +"("+DataBinder.Eval(Container.DataItem, "c").ToString()+")" %>' 
                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>' CommandName="ViewParts" />
                        </ItemTemplate>
                </asp:ListView>
    <hr size="1" />
    </div>
                    <asp:ListView ID="ListView1" runat="server" itemContainerID="itemPlaceholder" 
                            onitemdatabound="ListView1_ItemDataBound" 
                            onitemcommand="ListView1_ItemCommand"  >
                    
                    <EmptyDataTemplate>
                        data is empty
                    </EmptyDataTemplate>
                    <LayoutTemplate>
                     <table cellspacing="0" cellpadding="0" style="width: 100%">  
                            <tr style="width: 1000px" id="itemPlaceholder" runat="server"></tr>
                     </table>
                                       
                    </LayoutTemplate>
                    <ItemTemplate>
                        <asp:Panel runat="server" ID="_panel_part_commont">
                            <tr>
                                <td rowspan="2">
                                    
                                </td>
                                <td rowspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td><asp:Literal ID="_Literal_order_part_sum" runat="server"></asp:Literal></td>
                                            <td> <img src='http://www.lucomputers.com/pro_img/COMPONENTS/<%# DataBinder.Eval(Container.DataItem, "img_sku") %>_t.jpg' alt='<%# DataBinder.Eval(Container.DataItem, "img_sku") %>' />
                                    <br /><asp:CheckBox runat="server" id="_checkBox_showit" Checked='True' Text="issue"/></td>
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
                                            <a target="_blank" href="/site/product_parts_detail.asp?pro_class=<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>&cid=<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no") %>&id=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>"><%# DataBinder.Eval(Container.DataItem, "product_name") %></a>
                                           
                                        </li>
                                    </ul><div style="background:#f2f2f2;padding:2px; color:#cccccc; font-size:8pt;">
                                         Create Datetime: <%# DataBinder.Eval(Container.DataItem, "regdate") %>&nbsp;&nbsp;&nbsp;&nbsp;
                                        Last Modify Datetime: <%# DataBinder.Eval(Container.DataItem, "last_regdate") %>
                                    </div><asp:CheckBoxList runat="server" ID="_check_box_list_virtual"  RepeatLayout="Flow" RepeatColumns="6"></asp:CheckBoxList>
                                    
                                   <asp:Literal runat="server" ID="_literal_on_sale_rebate"></asp:Literal>
                                </td>
                                <td style="text-align:right"><div style="line-height:22px"><asp:Literal runat="server" ID="_lbl_stock" Text='<%# DataBinder.Eval(Container.DataItem, "ltd_stock") %>'></asp:Literal></div>
                                
                                     <ul class="ul_parent">
                                                    <li >
                                                            Sum:<asp:TextBox runat="server" ID="_txt_part_sum" Columns="6"
                                 Text='<%# DataBinder.Eval(Container.DataItem, "product_store_sum") %>' CssClass="input_right_line"></asp:TextBox>
                                                                <div class="float_area">
                                                                  <table cellspacing="2" align="left">
                                                                    <tr>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_0" Text="0" CssClass="a_btn" CommandArgument="0" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_1" Text="1" CssClass="a_btn" CommandArgument="1" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_2" Text="2" CssClass="a_btn" CommandArgument="2" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_3" Text="3" CssClass="a_btn" CommandArgument="3" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_4" Text="4" CssClass="a_btn" CommandArgument="4" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_5" Text="5" CssClass="a_btn" CommandArgument="5" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_6" Text="6" CssClass="a_btn" CommandArgument="6" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_7" Text="7" CssClass="a_btn" CommandArgument="7" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_8" Text="8" CssClass="a_btn" CommandArgument="8" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_9" Text="9" CssClass="a_btn" CommandArgument="9" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_10" Text="10" CssClass="a_btn" CommandArgument="10" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    </tr>
                                                                  </table>
                                                            </div>
                                                    </li>
                                           </ul>
                                </td>
                                <td style="text-align:right">
                                    Cost:<asp:TextBox runat="server" ID="_txt_lu_cost" Columns="10"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_cost") %>' CssClass="input_right_line"></asp:TextBox>
                                    <br />
                                    Price:<asp:TextBox runat="server" ID="_txt_lu_price" Columns="10" ReadOnly="true"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_price") %>' CssClass="input_right_line"></asp:TextBox>
                                    <div>
                                        Sold: <span style="color:#ff9900"><asp:TextBox runat="server" ID="_txt_lu_sold" Columns="10"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_sold") %>' CssClass="input_right_line" ReadOnly="true"></asp:TextBox></span>
                                    </div> 
                                    Cash: <asp:TextBox runat="server" ID="_txt_special_cash_price" Columns="10"  Text='<%# DataBinder.Eval(Container.DataItem, "product_current_special_cash_price") %>'
                                     CssClass="input_right_line"></asp:TextBox>
                                </td>
                                <td style="text-align:right; font-size:8pt">
                                    <a href="#top">Top</a><br />
                                    <asp:LinkButton runat="server" CommandName="Issue" Text="Issue" ID="_lb_issue"  OnClientClick="ParentLoadWait();"></asp:LinkButton><br /> 
                                    <asp:LinkButton runat="server" CommandName="Modify" Text="Save" ID="_lb_modify"  OnClientClick="ParentLoadWait();"></asp:LinkButton><br /> 
                                    <a href="/q_admin/editPartDetail.aspx?id=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>" onclick="winOpen(this.href, 'right_manage', 880, 800, 120, 200);return false;" title="Modify Detail">Edit</a>
                                    |<a href="/q_admin/editPartDetail.aspx?cmd=1&id=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>" onclick="winOpen(this.href, 'right_manage', 880, 800, 120, 200);return false;" title="Modify Detail">Edit Comm</a><br /> 
                                    <a href="part_and_group.aspx?partid=<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>" onclick="winOpen(this.href, 'right_manage', 620, 600, 120, 200);return false;" title="Modify Detail">Edit Group</a>
                                    <br />
                                    <span class='part_delete_btn' tag='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></span>
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
               
<script type="text/javascript">
    $().ready(function(){
        //
        // del btn
        //
        $('span.part_delete_btn').each(function(){
            var sku = $(this).attr('tag');
            $(this).load('/q_admin/inc/del_part.aspx?cmd=viewdel&sku='+sku+'&r='+ rand(1000));
        });
    });
</script>
</asp:Content>

