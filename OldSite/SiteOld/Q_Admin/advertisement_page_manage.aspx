<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="advertisement_page_manage.aspx.cs" Inherits="Q_Admin_advertisement_page_manage" Title="Manage ADV Page" %>
<%@ Register Assembly="AspNetPager" Namespace="Wuqi.Webdiyer" TagPrefix="webdiyer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        

        summary：<asp:TextBox runat="server" ID="txt_summary" Columns="40" ></asp:TextBox><asp:Button runat="server" ID="btn_create_file" Text="New" 
            onclick="btn_create_file_Click"/>
        <hr size="1" />
        
        <asp:ListView runat="server" ID="lv_file_list" 
            ItemPlaceholderID="itemPlaceholderID" onitemcommand="lv_file_list_ItemCommand">
                <LayoutTemplate>
                    <div style="background:#f2f2f2;">
                        <table cellpadding="2" cellspacing="1" style="width: 100%;border-top:1px solid #ccc;width: 100%">
                                <tr>
                                        <th style="border-bottom:1px solid #ccc; background:#D1DAF6;"></th>
                                        <th style="border-bottom:1px solid #ccc; background:#D1DAF6;"></th>
                                        <th style="border-bottom:1px solid #ccc;background:#D1DAF6;">Summary</th>
                                        <th style="border-bottom:1px solid #ccc;background:#D1DAF6;">Title</th>
                                        <th style="border-bottom:1px solid #ccc;background:#D1DAF6;">File Name</th>
                                        <th style="border-bottom:1px solid #ccc;background:#D1DAF6;">Date Taken</th>
                                        <th style="border-bottom:1px solid #ccc;background:#D1DAF6;">Size</th>
                                </tr>
                                <tr runat="server" id="itemPlaceholderID">
                                
                                </tr>
                        </table>
                    </div>
                </LayoutTemplate>
                <ItemTemplate>
                        <tr  onmouseover='this.className="onmouseover";' onmouseout="this.className='onmouseout';">
                            <td width="40" style="text-align:center;"><asp:ImageButton ID="ImageButton2" runat="server"  ImageAlign="AbsMiddle" AlternateText="Edit" CommandName="EditFile" CommandArgument='<%# DataBinder. Eval(Container.DataItem, "file_name") %>' ImageUrl="/soft_img/tags/(02,40).png" /></td>
                            <td width="40" style="text-align:center;">
                                <asp:ImageButton ID="ImageButton1" runat="server"  ImageAlign="AbsMiddle" AlternateText="Delete" CommandName="DeleteFile" CommandArgument='<%# DataBinder. Eval(Container.DataItem, "file_name") %>' ImageUrl="/soft_img/tags/(02,41).png" OnClientClick="return confirm('are you sure');" />    
                            </td>
                            <td><%# DataBinder.Eval(Container.DataItem, "summary")%></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "title")%></td>
                            <td width="200"><a href="/adv_page/adv_index.asp?file_name=<%# DataBinder. Eval(Container.DataItem, "file_name") %>" target="_blank"><%# DataBinder. Eval(Container.DataItem, "file_name") %></a></td>
                            <td width="180"><%# DataBinder.Eval(Container.DataItem, "date_taken")%></td>
                            <td width="200" style="text-align:right;"><%# DataBinder.Eval(Container.DataItem, "size") %></td>
                        </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                        <tr  onmouseover='this.className="onmouseover";' onmouseout="this.className='onmouseout';">
                            <td width="40" style="text-align:center; background:#f2f2f2;"><asp:ImageButton ID="ImageButton2" runat="server"  ImageAlign="AbsMiddle" AlternateText="Edit" CommandName="EditFile" CommandArgument='<%# DataBinder. Eval(Container.DataItem, "file_name") %>' ImageUrl="/soft_img/tags/(02,40).png" /></td>
                            <td width="40" style="text-align:center; background:#f2f2f2;">
                                <asp:ImageButton ID="ImageButton1" runat="server"  ImageAlign="AbsMiddle" AlternateText="Delete" CommandName="DeleteFile" CommandArgument='<%# DataBinder. Eval(Container.DataItem, "file_name") %>' ImageUrl="/soft_img/tags/(02,41).png" OnClientClick="return confirm('are you sure');" />    
                            </td>
                            <td style="background:#f2f2f2;"><%# DataBinder.Eval(Container.DataItem, "summary")%></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "title")%></td>
                            <td width="200" style="background:#f2f2f2;"><a href="/adv_page/adv_index.asp?file_name=<%# DataBinder. Eval(Container.DataItem, "file_name") %>" target="_blank"><%# DataBinder. Eval(Container.DataItem, "file_name") %></a></td>
                            <td width="180" style="background:#f2f2f2;"><%# DataBinder.Eval(Container.DataItem, "date_taken")%></td>
                            <td width="200" style="text-align:right;background:#f2f2f2;"><%# DataBinder.Eval(Container.DataItem, "size") %></td>
                        </tr>            
                </AlternatingItemTemplate>
        </asp:ListView>
        <webdiyer:AspNetPager ID="AspNetPager1" runat="server" AlwaysShow="true"
            CustomInfoHTML="page: %CurrentPageIndex%, pagecount: %PageCount%, %PageSize%/page" 
            ShowCustomInfoSection="Left" onpagechanged="AspNetPager1_PageChanged" 
            PageSize="30" >
        </webdiyer:AspNetPager>
        <asp:Panel runat="server" ID="panel_edit_content" Visible="false" CssClass="order_list_assigned_to">
                <asp:Button runat="server" ID="btn_save" Text="save" onclick="btn_save_Click"/>
                <hr size="1" />
                Title:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox runat="server" ID="txt_title" Columns="40" MaxLength="100"></asp:TextBox><br />
                Summary:<asp:TextBox runat="server" ID="txt_summary_2" Columns="40" MaxLength="200"></asp:TextBox><br />                
                <asp:TextBox runat="server" ID="txt_content" TextMode="MultiLine" Rows="20" Columns = "80"></asp:TextBox>
        </asp:Panel>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

