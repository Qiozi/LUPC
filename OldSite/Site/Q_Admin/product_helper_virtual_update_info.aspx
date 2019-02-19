<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_virtual_update_info.aspx.cs" Inherits="Q_Admin_product_helper_virtual_update_info" Title="Untitled Page" %>

<%@ Register src="UC/MenuChildName.ascx" tagname="MenuChildName" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager runat="server">
    
            </asp:ScriptManager>
    
        
        
    <uc1:MenuChildName ID="MenuChildName1" runat="server" />
     <hr size="1" />
    <div style="text-align:center; color: #ff9900; font-size: 9pt;">
        <asp:Button runat="server" ID="btn_downLoad" onclick="btn_downLoad_Click" 
            Text="Download" />
        <hr size="1" />
        <asp:FileUpload runat="server" ID="file_upload" />
        <asp:Button runat="server" ID="btn_upload" onclick="btn_upload_Click"  onclientclick="ParentLoadWait();"
            Text="Upload" />    
        <br />
        上传前， 旧数据将会被删除。<br />
        数据更新，只支持：Lu_sku, priority, showit
        <hr size="1" />
         <asp:UpdatePanel runat="server">
        <ContentTemplate>   
        <asp:Button runat="server" ID="btn_initial_priority"   onclientclick="ParentLoadWait();"
            Text="Initial all parts Priority Value" onclick="btn_initial_priority_Click" /> 
        <hr size="1" />
        <table style="width:100%" cellpadding="0" cellspacing="0">
        <asp:ListView ID="ListView1" runat="server" ItemContainerID="itemPlaceholder" 
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
                                <td rowspan="2"  style="text-align:center"><asp:Label runat="server" ID="_lbl_lu_priority" Text='<%# DataBinder.Eval(Container.DataItem, "priority") %>'></asp:Label></td>
                                <td rowspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                         
                                            <td>
                                            <asp:Button ID="_btn_up" CommandName="Up" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>' runat="server" Text="Up" onclientclick="ParentLoadWait();"/>
                                            <br />
                                            <asp:Button ID="_btn_down" CommandName="Download" Visible="false" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>' runat="server" Text="Down" onclientclick="ParentLoadWait();"/>
                                            </td>
                                            <td>
                                                <img src='http://www.lucomputers.com/pro_img/COMPONENTS/<%# DataBinder.Eval(Container.DataItem, "img_sku") %>_t.jpg' alt='<%# DataBinder.Eval(Container.DataItem, "img_sku") %>' />
                                            </td>
                                        </tr>
                                    </table>
                                       
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
                                    </ul>
                                </td>
                               
                                <td style="text-align:right">
                                
                                    Cost:<asp:TextBox runat="server" ID="_txt_lu_cost" Columns="10" ReadOnly="true"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_cost") %>' CssClass="input_right_line"></asp:TextBox>
                                    <br />
                                    Price:<asp:TextBox runat="server" ID="_txt_lu_price" Columns="10" ReadOnly="true"
                                     Text='<%# DataBinder.Eval(Container.DataItem, "product_current_price") %>' CssClass="input_right_line"></asp:TextBox>
                                    <div>
                                        Sold: <span style="color:#ff9900"><input type="text" readonly="readonly" size="10" value='<%# DataBinder.Eval(Container.DataItem, "product_current_sold") %>' class="input_right_line" /></span>
                                    </div> 
                                </td>
                                <td style="text-align:right; font-size:8pt">
                                     <asp:Button ID="_btn_delete" CommandName="DeletePart" runat="server" Text="Delete" onclientclick="if(confirm('Sure?')){ ParentLoadWait();return true;}else { return false;}"/>                                       
                               </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="border-top: 1px dotted #D5582e; line-height: 20px; ">                                    
                                </td>
                            </tr>
                            <tr>
                                <td style="background:#999; line-height:1px" colspan="5">&nbsp;</td>
                            </tr>
                        </asp:Panel>
                        <asp:Panel runat="server" ID="_panel_part_title" >
                            <tr>
                                <td style="text-align:center">
                                <asp:Label runat="server" ID="_lbl_lu_priority_title" Text='<%# DataBinder.Eval(Container.DataItem, "priority") %>'></asp:Label>
                                </td>
                                <td style="background:#E7E7E7; ">
                                    <asp:Button ID="_btn_up_title" CommandName="Up" runat="server" Text="Up" onclientclick="ParentLoadWait();"/>
                                        <br />
                                    <asp:Button ID="_btn_down_title" CommandName="Download" Visible="false" runat="server" Text="Down" onclientclick="ParentLoadWait();"/>
                                </td>
                                <td style="background:#E7E7E7; color:green; line-height:20px; text-align:center; font-weight:bold" colspan="2">
                                <asp:Label runat="server" ID="_lbl_lu_sku_title" Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Label>&nbsp;
                                <%# DataBinder.Eval(Container.DataItem, "product_name") %>
                                <br />
                                <asp:CheckBoxList runat="server" ID="_check_box_list_virtual_title"  RepeatLayout="Flow" RepeatColumns="6"></asp:CheckBoxList>                                
                                </td>
                                <td style="background:#E7E7E7; text-align:right"><asp:Button ID="_btn_delete_title" CommandName="DeletePart" runat="server" Text="Delete" onclientclick="if(confirm('Sure?')){ ParentLoadWait();return true;}else { return false;}"/></td>
                            </tr>
                            <tr>
                                <td style="background:#999; line-height:1px" colspan="5">&nbsp;</td>
                            </tr>
                        </asp:Panel>
                    </ItemTemplate>
                    
                </asp:ListView>  </table>
                </ContentTemplate>
    </asp:UpdatePanel>
    </div>
   

    
</asp:Content>

