<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="system_helper_track.aspx.cs" Inherits="Q_Admin_system_helper_track" Title="Track" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><uc1:Navigation ID="Navigation1" runat="server" NavigationText="Track" />
    最新一百个动作
    <hr size="1" />
    <anthem:DataGrid ID="dg_track_list"  runat="server" AutoGenerateColumns="False" AlternatingItemStyle-BackColor="#f2f2f2" AddCallBacks="False" EnableCallBack="False" EnableViewState="False" >
        <HeaderStyle HorizontalAlign="Center" BackColor="#F2F2F2" Font-Bold="True" />
        
        <Columns>
            <asp:BoundColumn DataField="track_regdate" HeaderText="Datetime"></asp:BoundColumn>
            <asp:BoundColumn DataField="staff_realname" HeaderText="Employee"></asp:BoundColumn>
            <asp:BoundColumn DataField="track_comment" HeaderText="Comment"></asp:BoundColumn>
            <asp:BoundColumn DataField="track_ip" HeaderText="IP"></asp:BoundColumn>
        </Columns>
        <AlternatingItemStyle BackColor="#F2F2F2" />
    </anthem:DataGrid>
    
    
</asp:Content>

