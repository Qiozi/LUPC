<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="other_inc_bind_price.aspx.cs" Inherits="Q_Admin_other_inc_bind_price" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
    table td{font-size: 8pt;}
#table_part_list td {font-size: 8pt;}
</style>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<div style="border:1px solid #C7D5B9; padding: 5px;">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    </asp:UpdatePanel>                      
            <div style="background:#ccc; width: 1066px">
                <table>
                        <tr>
                                <td valign="top" colspan="4">
                                           <table><tr><td>Category:</td><td><uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" /></td></tr></table> 
                                </td>
                        </tr>
                        <tr>
                                <td valign="top">
                                            <table width="250" cellpadding="1" cellspacing="1" class="table_td_white">
                                    
                                       
                                        <tr>
                                            <td style="background:#f2f2f2;">
                                                Other Inc: </td>
                                            <td>
                                                <asp:ListBox ID="ddl_other_inc" runat="server" Rows="1"></asp:ListBox>
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td>
                                                LUC SKU:</td>
                                            <td>
                                                <asp:TextBox ID="txt_lu_sku" runat="server"></asp:TextBox>
                                                <br />
                                                * 如果输入了SKU，将是处理单个产品</td>
                                        </tr>

                                        <tr >
                                            <td style="height:19px;">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btn_Save_category" runat="server" 
                                                    onclick="btn_Save_category_Click" Text="与目录挂勾" />
                                            </td>
                                        </tr>
                                        
                                        </table>
                                </td>
                                <td valign="top">
                                          <table width="250" cellpadding="1" cellspacing="1" class="table_td_white">                                    
                                               
                                                <tr>
                                                    <td style="background:#f2f2f2;">                                 
                                                        Manufacturer:</td>
                                                    <td>
                                                        <asp:ListBox ID="lb_manufacture" runat="server" Rows="1"></asp:ListBox>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="background:#f2f2f2;">
                                                        Other Inc: </td>
                                                    <td>
                                                        <asp:ListBox ID="lb_vendor_manufacture" runat="server" Rows="1"></asp:ListBox>
                                                    </td>
                                                </tr>
                                                <tr style="display:none;">
                                                    <td>
                                                        LUC SKU:</td>
                                                    <td>
                                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                                        <br />
                                                        * 如果输入了SKU，将是处理单个产品</td>
                                                </tr>

                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Button ID="btn_relating_manufacture" runat="server" 
                                                            onclick="btn_relating_manufacture_Click" Text="与manufacture挂勾" />
                                                    </td>
                                                </tr>
                                                
                                                </table>
                                </td>
                                <td valign="top">
                                      <table width="250" cellpadding="1" cellspacing="1" class="table_td_white">                                    
                                       
                                        <tr>
                                            <td style="background:#f2f2f2;">
                                                Other Inc: </td>
                                            <td>
                                                <asp:ListBox ID="lb_vendor_real_price_cate" runat="server" Rows="1"></asp:ListBox>
                                            </td>
                                        </tr>

                                        <tr >
                                            <td style="height:19px;">
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>

                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btn_relating_real_price_cate" runat="server" 
                                                    onclick="btn_relating_real_price_cate_Click" Text="与采购价挂勾" 
                                                    Enabled="False" />
                                                &nbsp;</td>
                                        </tr>
                                        
                                        </table>
                                </td>
                                <td valign="top">
                                      <table width="250" cellpadding="1" cellspacing="1" class="table_td_white">                                    
                                       
                                        <tr>
                                            <td style="background:#f2f2f2;">                                 
                                                Manufacturer:</td>
                                            <td>
                                                <asp:ListBox ID="lb_manufacture_real_price" runat="server" Rows="1"></asp:ListBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="background:#f2f2f2;">
                                                Other Inc: </td>
                                            <td>
                                                <asp:ListBox ID="lb_vendor_real_price" runat="server" Rows="1"></asp:ListBox>
                                            </td>
                                        </tr>

                                        <tr>
                                            <td align="center" colspan="2">
                                                <asp:Button ID="btn_relating_real_price" runat="server" 
                                                    onclick="btn_relating_real_price_Click" Text="与采购价挂勾" Enabled="False" />
                                                &nbsp;</td>
                                        </tr>
                                        
                                        </table>
                                </td>
                        </tr>
                </table>   
             </div>

</div> <hr size="1"  style="color: White;"/>
<table>
        <tr>
                <td>
                        
                                        <asp:ListView runat="server" ID="lv_other_inc_bind_info" 
                                            ItemPlaceholderID="itemPlaceHolderID" 
                                            onitemcommand="lv_other_inc_bind_info_ItemCommand" 
                                            onitemdatabound="lv_other_inc_bind_info_ItemDataBound">
                                                <LayoutTemplate>
                                                        <div style="background:#bbbbbb;width: 1066px;">
                                                        * 当Manufacture为空，是对整个目录的定义
                                                        <table cellspacing="1" cellpadding="2" style="border:1px solid #ccc;width: 100%" id="table_part_list" >
                                                                <tr>
                                                                 <th>Category </th>
                                                                 <th>ID</th>
                                                                   <th>type</th>
                                                                   <th>Manufacture</th>
                                                                   <th>Other INC.</th>
                                                                   <th>Relating</th>
                                                                   <th></th>
                                                                </tr>
                                                               
                                                                <tr runat="server" id="itemPlaceHolderID"></tr>
                                                        </table></div>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                        <tr>                                        
                                                                <td style=" background: #fff;"><%# DataBinder.Eval(Container.DataItem, "category_name") %></td>
                                                                <td style="width: 50px; background:#f2f2f2; text-align:center; background: #fff;"><asp:Label runat="server" ID="_lbl_id" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:Label></td>
                                                                <td style="width: 150px; background: #fff;"><asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "bind_type_name") %>'></asp:Label> </td>
                                                                <td style="width: 250px; background: #fff;"><asp:Label runat="server" ID="_lbl_manufacture" Text='<%# DataBinder.Eval(Container.DataItem, "manufacture") %>'></asp:Label></td>
                                                                <td style="width: 150px; background: #fff;"><asp:Label runat="server" ID="_lbl_other_inc_name" Text='<%# DataBinder.Eval(Container.DataItem, "other_inc_id") %>'></asp:Label> </td>
                                                                 <td style=" background: #fff;"><asp:Label runat="server" ID="_lbl_relating" Text='<%# DataBinder.Eval(Container.DataItem, "relating ")%>'></asp:Label></td>
                                                                <td style="background: #fff;"> 
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' OnClientClick="return confirm('Sure!!!');" CommandName="DeleteRecord" Text="Delete"></asp:LinkButton>
                                                                &nbsp;|&nbsp;
                                                                <asp:LinkButton ID="lb_relating" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' OnClientClick="return confirm('Sure!!!');" CommandName="Relating" Text="挂勾|脱勾"></asp:LinkButton>
                                                                </td>
                                                        </tr>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                        <tr>                                        
                                                                <td style=" background: #fff;"><%# DataBinder.Eval(Container.DataItem, "category_name") %></td>
                                                                <td style="width: 50px; background:#f2f2f2; text-align:center; "><asp:Label runat="server" ID="_lbl_id" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:Label></td>
                                                                <td style="width: 150px; background: #f2f2f2;"><asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "bind_type_name") %>'></asp:Label> </td>
                                                                <td style="width: 250px; background: #f2f2f2;"><asp:Label runat="server" ID="_lbl_manufacture" Text='<%# DataBinder.Eval(Container.DataItem, "manufacture") %>'></asp:Label></td>
                                                                <td style="width: 150px; background: #f2f2f2;"><asp:Label runat="server" ID="_lbl_other_inc_name" Text='<%# DataBinder.Eval(Container.DataItem, "other_inc_id") %>'></asp:Label> </td>
                                                                 <td style=" background: #f2f2f2;"><asp:Label runat="server" ID="_lbl_relating" Text='<%# DataBinder.Eval(Container.DataItem, "relating ")%>'></asp:Label></td>
                                                                <td style="background: #f2f2f2;"> 
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' OnClientClick="return confirm('Sure!!!');" CommandName="DeleteRecord" Text="Delete"></asp:LinkButton>
                                                                &nbsp;|&nbsp;
                                                                <asp:LinkButton ID="lb_relating" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' OnClientClick="return confirm('Sure!!!');" CommandName="Relating" Text="挂勾|脱勾"></asp:LinkButton>
                                                                </td>
                                                        </tr>
                                                </AlternatingItemTemplate>
                                        </asp:ListView>
                           <asp:UpdatePanel runat="server" ID="updatePanel2">
                        </asp:UpdatePanel>                   
                </td>
                <td>
                    
                </td>
        </tr>
</table>
<script >
    MergeCellsVertical(document.getElementById('table_part_list'), 0);
</script>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>