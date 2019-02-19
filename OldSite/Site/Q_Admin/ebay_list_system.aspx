<%@ Page Language="C#" MasterPageFile="~/Q_Admin/ebay.master" AutoEventWireup="true" CodeFile="ebay_list_system.aspx.cs" Inherits="Q_Admin_ebay_list_system" Title="Untitled Page" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="btn_list">
        <a href="" onclick="ParentLoadWait();parent.window.ViewSystemDetail(-1);" class="a_btn">New</a>
        <asp:LinkButton runat="server" ID="lb_save" Text="Save" 
            onclientclick="ParentLoadWait();" onclick="lb_save_Click"></asp:LinkButton>    
    </div>
    <div style="clear:both"> 
        <fieldset class="fieldset1">
            <legend>Search</legend>
                 <asp:Panel ID="Panel1" runat="server" Height="30px" >
                    <asp:TextBox ID="txt_keyword" runat="server" Columns="40"></asp:TextBox>
                    <asp:Button ID="btn_search" runat="server" onclick="btn_search_Click" 
                        Text="Search"  OnClientClick="ParentLoadWait();"/>
                         <asp:Button ID="Button1" runat="server" 
                        Text="Clear Search"  OnClientClick="ParentLoadWait();" 
                         onclick="Button1_Click"/>
                        keyword: sku, ebay sku, ebay NO#
                </asp:Panel>
        </fieldset>       
        
    </div>
    <div style="clear:both">
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="true"
                                    CustomInfoHTML="%CurrentPageIndex%/%PageCount%,page size: %PageSize%" 
                                    ShowCustomInfoSection="Left" onpagechanged="AspNetPager1_PageChanged" 
                                    PageSize="4" CustomInfoTextAlign="Left" >
                                </webdiyer:AspNetPager>
    </div>
    
    <div style="clear:both">
        
        <table cellpadding="0" cellspacing="0" style="width: 99%" align="center"> 
            <tr>
                <td colspan="5"><hr size="1" /></td>
            </tr>
        <asp:ListView ID="ListView1" runat="server" ItemContainerID="itemPlaceholder" 
                onitemdatabound="ListView1_ItemDataBound"  >
                                
            <EmptyDataTemplate>
                data is empty
            </EmptyDataTemplate>
            <LayoutTemplate>
                <div  id="itemPlaceholder" runat="server"> </div>                             
            </LayoutTemplate>
                
            <ItemTemplate> 
                <tr>
                    <td style="text-align:center; width: 150px">
                        <img src="http://www.lucomputers.com/pro_img/COMPONENTS/<%# DataBinder.Eval(Container.DataItem, "img_sku") %>_system.jpg" />
                        <br />[<asp:Label runat="server" ID="_lbl_id" Text='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:Label>]
                    </td>
                    <td valign="top">
                        <div>
                            <%# DataBinder.Eval(Container.DataItem, "ebay_store_comment")%>
                        </div>
                        <div style="clear: both; background:#f2f2f2;margin-top:5px; border-bottom: 1px solid #ccc">
                            <table cellpadding="0" cellspacing="0" width="90%">
                                <tr>
                                    <td style=" color:#ccc">regdate:<i><%# DataBinder.Eval(Container.DataItem, "regdate").ToString()%></td></i> 
                                    <td style=" color:#ccc">last modify:<i><%# DataBinder.Eval(Container.DataItem, "last_regdate").ToString()%></td></i> 
                                </tr>
                            </table>
                        </div>
                        
                        <asp:Literal runat="server" ID="_literal_system_part"></asp:Literal>
                        
                    </td>
                    <td style="width: 200px; text-align:left">
                        E NO#:&nbsp;<asp:TextBox runat="server" ID="_txt_ebay_code" CssClass="input_unline" Text='<%# DataBinder.Eval(Container.DataItem, "ebay_code") %>'></asp:TextBox>
                        <br />E Sold$:<asp:TextBox runat="server" ID="_txt_ebay_price" CssClass="input_right_line" Text='<%# DataBinder.Eval(Container.DataItem, "price") %>'></asp:TextBox>
                        
                        <br />
                        Cost$:&nbsp;&nbsp;<asp:TextBox runat="server" ReadOnly="true" ID="_txt_cost" CssClass="input_right_line" Text='<%# DataBinder.Eval(Container.DataItem, "lu_cost") %>'></asp:TextBox>
                        <br />
                        Price$:&nbsp;&nbsp;<asp:TextBox runat="server" ReadOnly="true" ID="_txt_price" CssClass="input_right_line" Text='<%# DataBinder.Eval(Container.DataItem, "lu_price") %>'></asp:TextBox>
                    </td>
                    <td style="width:90px">
                        <asp:CheckBox runat="server" ID="_cb_is_templete" Text="templete" Checked='<%# DataBinder.Eval(Container.DataItem, "is_templete").ToString() == "1" %>'/>
                    
                    </td>
                    <td style="width: 70px">
                        <a href="" onclick="parent.window.ViewSystemDetail(<%# DataBinder.Eval(Container.DataItem, "id") %>);">detail</a>
                        <div>
                            <a href="/view_for_ebay.asp?cate=2&id=<%# DataBinder.Eval(Container.DataItem, "id") %>" onclick="js_callpage_name_custom(this.href, 'view_flash', 900, 800); return false;">View</a><br />
                            ebay view
                            <a href="/view_in_flash.asp?cate=2&id=<%# DataBinder.Eval(Container.DataItem, "id") %>" onclick="js_callpage_name_custom(this.href, 'view_flash', 760, 410); return false;">Flash View</a>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="5"><hr size="1" /></td>
                </tr>
            </ItemTemplate>
        </asp:ListView>
        </table>        
    </div>
</asp:Content>

