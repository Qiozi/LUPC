<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/Q_Admin/None.master" CodeFile="orders_edit_detail_customer_msg.aspx.cs" Inherits="Q_Admin_orders_edit_detail_customer_msg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    <asp:DataList 
            ID="dl_msg_list" runat="server" Width="100%" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px" CellPadding="4" 
        ForeColor="Black" GridLines="Vertical">
            <FooterStyle BackColor="#CCCC99" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#F7F7DE" />
            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>
            <div style="color:#2E2EFF;">
                                      
                            <div style="float:left; color:#000">
                            <%# DataBinder.Eval(Container.DataItem,"msg_author").ToString() == "Me" ?"Customer":DataBinder.Eval(Container.DataItem,"msg_author").ToString() %>
                            </div>
                            <div style="float: right; color:#000"> 
                            <%# DataBinder.Eval(Container.DataItem,"regdate") %>
                            </div>
                        <div  style="min-height: 40px; color: #2E2EFF; clear:both; vertical-align:middle; padding-left: 2em;margin-top: 5px;">
                            <%# System.Web.HttpUtility.HtmlDecode( DataBinder.Eval(Container.DataItem,"msg_content_text").ToString().Replace("\r\n", "<br/>")) %>
                        </div>
            </div>
        </ItemTemplate>
        </asp:DataList>

        <hr size="1" />
        
        <table align="center">
            <tr>
                <td colspan="2">
                 <asp:TextBox ID="txt_msg_from_seller" runat="server" 
                                    Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">  
                <asp:TextBox runat="server" ID="txt_mail" Columns="70"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="left">  <asp:Checkbox ID="checkbox_send_mail" runat="server" 
                                    
            Text="Send Mail" oncheckedchanged="checkbox_send_mail_CheckedChanged" 
                        AutoPostBack="True" />
                </td>
                <td align="right"> 
                    <asp:Button ID="btn_close" runat="server" 
                                    onclick="btn_close_Click" 
            Text="Close" />
                    <asp:Button ID="btn_submit_to_customer" runat="server" 
                                    onclick="btn_submit_to_customer_Click" 
            Text="Submit" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                <asp:Panel runat="server" ID="panel_mail_area">
                <br />
                *只发送最后一个提交的消息。
            </asp:Panel></td>
            </tr>
        </table>

<asp:Literal ID="Label1" runat="server"></asp:Literal>
    </div>
    <a name='bottom'></a>

</asp:Content>
