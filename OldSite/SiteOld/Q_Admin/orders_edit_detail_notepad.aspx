<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_notepad.aspx.cs" Inherits="Q_Admin_orders_edit_detail_notepad" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:DataList 
            ID="dl_msg_list" runat="server" Width="100%" BackColor="White" 
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px" CellPadding="4" 
        ForeColor="Black" GridLines="Vertical" 
        ondeletecommand="dl_msg_list_DeleteCommand">
        <FooterStyle BackColor="#CCCC99" />
        <AlternatingItemStyle BackColor="White" />
        <ItemStyle BackColor="#F7F7DE" />
        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>
            <div style="color:#2E2EFF;">
                <div style="float:left; color:#000">
                    <%# DataBinder.Eval(Container.DataItem,"Author").ToString() %>
                </div>
                <div style="float: right; color:#000">
                    <%# ViewDateFormat.View(DateTime.Parse(DataBinder.Eval(Container.DataItem, "regdate").ToString()))%>
                </div>
                <div  style="min-height: 40px; color: #2E2EFF; clear:both; vertical-align:middle; padding-left: 2em;margin-top: 5px;">
                    <%# System.Web.HttpUtility.HtmlDecode( DataBinder.Eval(Container.DataItem,"msg").ToString().Replace("\r\n", "<br/>")) %>
                    <asp:ImageButton runat="server" CommandName="Delete" onclick="ImageButton1_Click" OnClientClick="return confirm('Are you sure?');" ID="_imgButtonDelete" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"ID").ToString() %>' ImageUrl="../soft_img/tags/(02,41).png" />
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>
    <hr size="1" />
    
            <asp:TextBox ID="TextBox1" runat="server" Columns="80" Rows="5" 
        TextMode="MultiLine"></asp:TextBox>
            <br />
            <div style="text-align:center;">
            <asp:Button ID="Button_save"
                runat="server" Text="Submit" onclick="Button_save_Click" />
             
                </div>
</asp:Content>

