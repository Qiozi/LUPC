<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="sales_order_system_list.aspx.cs" Inherits="Q_Admin_sales_order_system_list" Title="System Detail In Order" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center">
        <asp:TextBox runat="server" id="txt_keyword"></asp:TextBox>
        <asp:Button runat="server" ID="btn_search" Text="Search" 
            onclientclick="ParentLoadWait();" onclick="btn_search_Click"/>
    &nbsp;<asp:Button runat="server" ID="btn_clear" Text="Clear" 
            onclientclick="ParentLoadWait();" onclick="btn_clear_Click"/>
    </div> 
    <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <hr size="1" />
                <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="true"
                        CustomInfoHTML="第%CurrentPageIndex%页，共%PageCount%页，每页%PageSize%条" 
                        ShowCustomInfoSection="Left" onpagechanged="AspNetPager1_PageChanged" 
                        PageSize="5" CustomInfoTextAlign="Left" >
                    </webdiyer:AspNetPager>
               <hr size="1" />
   
                <table cellpadding="5" cellspacing="0">
                       
                <asp:ListView ID="ListView1" runat="server" ItemContainerID="itemPlaceholder" 
                    onitemcommand="ListView1_ItemCommand" 
                    onitemediting="ListView1_ItemEditing" onitemcanceling="ListView1_ItemCanceling" 
                    onitemupdating="ListView1_ItemUpdating" onitemdatabound="ListView1_ItemDataBound" 
                    >
                    
                    <EmptyDataTemplate>
                        data is empty
                    </EmptyDataTemplate>
                    <LayoutTemplate>

                        <div id="itemPlaceholder" runat="server"/>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="border: 1px solid #ccc; background:#ccc">
                                <div onclick="OpenOrderDetail('<%# DataBinder.Eval(Container.DataItem, "order_code") %>')" style="cursor:pointer"><asp:Label runat="server" ID="_label_order_code" Text='<%# DataBinder.Eval(Container.DataItem, "order_code") %>'></asp:Label></div>
                            </td>
                            <td style="border: 1px solid #ccc; background:#ccc">
                                <asp:Label runat="server" ID="_label_system_templete_serial_no" Text='<%# DataBinder.Eval(Container.DataItem, "system_templete_serial_no") %>' />
                            </td>
                            <td style="font-weight:bold; border: 1px solid #ccc; background:#ccc">
                                <%# DataBinder.Eval(Container.DataItem, "product_name") %>                    
                            </td>
                            <td style="border: 1px solid #ccc; background:#ccc"><%# DataBinder.Eval(Container.DataItem, "create_datetime").ToString()%></td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" background:#fff">
                                
                            </td>
                            <td style=" background:#f2f2f2">
                                <asp:Literal runat="server" ID="_literal_detail"></asp:Literal></td>
                            <td style=" background:#f2f2f2"></td>
                        </tr>
                        <tr>
                            <td colspan="2" style=" background:#fff">
                                
                            </td>
                            <td style="border: 1px solid #ff9900; background:#F0E2D8" colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            Estimated shipping date: <asp:TextBox runat="server" ID="_txt_end_datetime" ReadOnly="true" CssClass="input"></asp:TextBox>
                                        </td>
                                        <td>
                                            <ul class="ul_parent">
                                                <li>Set in
                                                    <div>
                                                    <asp:Calendar ID="Calendar1" runat="server" onselectionchanged="Calendar2_SelectionChanged"
                                                     ondayrender="Calendar2_DayRender">
                                                    </asp:Calendar>
                                                    </div>
                                                </li>
                                            </ul>  
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="_btn_save_finish_date" CommandName="SaveFinishDate" Text="Save" onclientclick="ParentLoadWait();"/>
                                        </td>
                                    </tr>
                                </table>
                                
                                                  
                            </td>
                            
                        </tr>
                        <tr>
                            <td colspan="2" style=" background:#fff">
                                
                            </td>
                            <td style=" background:#fdfdfd">
                                <div>
                                    <asp:Literal runat="server" ID="_literal_note"></asp:Literal>
                                </div>
                                Note: <asp:TextBox runat="server" ID="_txt_note" CssClass="input" Columns="100" MaxLength="100"></asp:TextBox>
                                <asp:Button runat="server" ID="btn_save_note" CommandName="SaveNote" Text="Save" onclientclick="ParentLoadWait();"/>
                            </td>
                            <td style=" background:#fdfdfd"></td>
                        </tr>
                    </ItemTemplate>
                    
                </asp:ListView>
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Content>

