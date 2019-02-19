<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="Sale_on_sale_settings_download_up.aspx.cs" Inherits="Q_Admin_Sale_on_sale_settings_download_up" %>


<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
<br /><br /><table>
        <tr>
                <td>on sale
                    <asp:TextBox ID="txt_begin_date" runat="server"></asp:TextBox>
                    </td>
                
                <td><ul class="ul_parent">
                                                    <li> <span class="displayBlockTitle"><img src="http://www.lucomputers.com/images/arrow_6.gif" /></span> 
                                                        <div>
                                                        <asp:Calendar ID="Calendar2" runat="server" onselectionchanged="Calendar2_SelectionChanged"
                                                         ondayrender="Calendar2_DayRender">
                                                        </asp:Calendar>
                                                        </div>
                                                    </li>
                                                </ul></td>
                
                <td>
                    TO</td>
                
                <td>
                    <asp:TextBox ID="txt_end_date" runat="server"></asp:TextBox>
                </td>
                
                <td>
                    <ul class="ul_parent">
                                                    <li> <span class="displayBlockTitle"><img src="http://www.lucomputers.com/images/arrow_6.gif" /></span> 
                                                        <div>
                                                        <asp:Calendar ID="Calendar1" runat="server" onselectionchanged="Calendar1_SelectionChanged"
                                                         ondayrender="Calendar1_DayRender">
                                                        </asp:Calendar>
                                                        </div>
                                                    </li>
                                                </ul></td>
                <td>
                    <uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" />
                </td>
                <td><asp:Button runat="server" ID="btn_download" Text="Download" 
                        onclick="btn_download_Click" /></td>
        </tr>
        <tr>
                <td colspan="7"><hr size="1" /></td>
                
        </tr>
        <tr>
                <td colspan="6"><asp:FileUpload runat="server" ID="fileupload1" Width="490px" /></td>
                <td><asp:Button runat="server" ID="btn_upload" Text="Upload" 
                        onclick="btn_upload_Click" />
                    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                </td>
                
        </tr>
</table>
<hr size="1" />

<asp:GridView ID="GridView1" runat="server">
</asp:GridView>
</asp:Content>

