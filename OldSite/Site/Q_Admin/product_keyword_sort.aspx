<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_keyword_sort.aspx.cs" Inherits="Q_Admin_product_keyword_sort" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    #table1 tr td { font-size: 8.5pt; border-bottom: 1px dotted #ccc;}
      #table1 tr:hover { background:#ccc; font-size: 8.5pt; border-bottom: 1px dotted #ccc;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3>
    <asp:Label runat="server" ID="lbl_system_title"></asp:Label>
</h3>
<hr size="1" />
    <asp:Button ID="Button1" runat="server" Text="New priority" 
        onclick="Button1_Click" />
<hr size="1" />
<table cellpadding="0" cellspacing="0" style="width: 99%" align="center" id="table1"> 
          
        <asp:ListView ID="ListView1" runat="server" ItemContainerID="itemPlaceholder" 
                onitemdatabound="ListView1_ItemDataBound" 
            onitemcommand="ListView1_ItemCommand"  >
                                
            <EmptyDataTemplate>
                data is empty
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div  id="itemPlaceholder" runat="server"> </div>                             
            </LayoutTemplate>                
            <ItemTemplate> 
                <tr>
                    <td>
                    <asp:HiddenField runat="server" ID="_hf_sp_id" Value='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
                    <asp:Button runat="server" ID="btn_up" Text="Up" CommandName="UP" OnClientClick="ParentLoadWait();" /></td>
                    <td style="width: 60px; text-align:center"><asp:Literal runat="server" ID="_literal_priority" Text='<%# DataBinder.Eval(Container.DataItem, "priority") %>'></asp:Literal></td>
                    
                    <td><asp:Literal runat="server" ID="_literal_part_group_name" Text='<%# DataBinder.Eval(Container.DataItem, "keyword") %>'></asp:Literal></td>
                </tr>
           </ItemTemplate>
       </asp:ListView>
</table>
</asp:Content>

