<%@ Page Title="Change price record" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="other_inc_bind_price_change.aspx.cs" Inherits="Q_Admin_other_inc_bind_price_change" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style>
.top1{border-top:1px solid #ccc; font-weight:bold;}
.bottom1{ border-bottom: 1px solid #ccc;}
.onmouseover { background:#f2f2f2;}
.onmouseout { background: #ffffff;}
</style>
    <asp:DropDownList runat="server" ID="ddl_record_mark" AutoPostBack="True" 
        onselectedindexchanged="ddl_record_mark_SelectedIndexChanged"></asp:DropDownList>
    <hr size="1" />
            <asp:Label runat="server" ID="lbl_mark"> 最后一次</asp:Label>
    <hr size="1" />
<asp:ListView runat="server" ID="lv_change_list" 
        ItemPlaceholderID="itemPlacehodlerID" 
        onitemdatabound="lv_change_list_ItemDataBound">
    
        <LayoutTemplate >
            <div style="background:#f2f2f2;">
                <table cellpadding="0" cellspacing="0" id="table_part_list" style="border:1px solid #ccc;width: 100%">
                        <tr><th style="border-bottom: 1px solid #ccc;"></th>
                                <th style="border-bottom: 1px solid #ccc;"></th>
                                  <th style="border-bottom: 1px solid #ccc;">lu SKU</th>
                                   <th style="border-bottom: 1px solid #ccc;">name</th>
                                   <th style="border-bottom: 1px solid #ccc;">differences</th>
                                    <th style="border-bottom: 1px solid #ccc;">O cost</th>
                                     <th style="border-bottom: 1px solid #ccc;">N cost</th>
                                      <th style="border-bottom: 1px solid #ccc;">O price</th>
                                       <th style="border-bottom: 1px solid #ccc;">N price</th>
                        </tr>
                        <tr runat="server" id="itemPlacehodlerID"></tr>
                </table>
               </div>
        </LayoutTemplate>
        <ItemTemplate>
                <tr onmouseover='this.className="onmouseover";' onmouseout="this.className='onmouseout';">
                        <td style="border-bottom: 1px solid #ccc; text-align: center; font-weight:bold;border-right: 1px solid #ccc;"><%# DataBinder.Eval(Container.DataItem, "menu_child_name") %></td>
                        <td style="width: 80px; border-bottom: 1px solid #ccc; text-align: center"><a href="/q_admin/other_inc_view_compare.aspx?id=<%# DataBinder.Eval(Container.DataItem, "lu_Sku") %>&date=<%# DataBinder.Eval(Container.DataItem, "mark") %>" onclick="winOpen(this.href, 'right_manage', 880, 800, 120, 200);return false;" title="Modify Detail">Shopbot</a>
                                    </td>
                       
                        <td style="color:<%# DataBinder.Eval(Container.DataItem, "color")  %>; border-bottom: 1px solid #ccc;">
                            <a href="/Product_parts_detail.asp?pro_class=71&id=<%# DataBinder.Eval(Container.DataItem, "lu_sku") %>&parent_id=<%# DataBinder.Eval(Container.DataItem, "category_id") %>" target="_blank"><%# DataBinder.Eval(Container.DataItem, "lu_sku") %></a></td>
                        
                        <td style="color:<%# DataBinder.Eval(Container.DataItem, "color")  %>; border-bottom: 1px solid #ccc;"><%# DataBinder.Eval(Container.DataItem, "product_name") %></td>
                        <td style="text-align:right; border-bottom: 1px solid #ccc;"><asp:Label runat="server" ID="_lbl_differences"></asp:Label></td>
                        <td style="color: #ccc; text-align:right; border-bottom: 1px solid #ccc;"><asp:Label runat="server" ID="_lbl_old_cost" Text='<%# DataBinder.Eval(Container.DataItem, "old_cost") %>' ></asp:Label></td>                        
                        <td style="text-align:right; border-bottom: 1px solid #ccc;"><asp:Label runat="server" ID="_lbl_new_cost" Text='<%# DataBinder.Eval(Container.DataItem, "new_cost") %>' ></asp:Label></td>
                        <td style="color: #ccc; text-align:right; border-bottom: 1px solid #ccc;"><%# DataBinder.Eval(Container.DataItem, "old_price") %></td>
                        <td style="text-align:right;  border-bottom: 1px solid #ccc;"><%# DataBinder.Eval(Container.DataItem, "new_price") %></td>
                </tr>
            
        </ItemTemplate>
</asp:ListView>

<script type ="text/javascript">
//MergeCellsVertical(document.getElementById("table_part_list"), 0);
</script>
<asp:Literal ID="literal_run_script" runat="server"></asp:Literal>
</asp:Content>

