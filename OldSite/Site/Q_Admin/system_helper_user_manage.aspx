<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="system_helper_user_manage.aspx.cs" Inherits="Q_Admin_system_helper_user_manage" Title="User Manage" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" NavigationText="User Manage" />
    <anthem:DataGrid ID="dg_user_list" SelectedItemStyle-BackColor="#f2f2f2" runat="server" AutoGenerateColumns="False" Width="100%" OnCancelCommand="dg_user_list_CancelCommand" OnDeleteCommand="dg_user_list_DeleteCommand" OnEditCommand="dg_user_list_EditCommand" OnSelectedIndexChanged="dg_user_list_SelectedIndexChanged" OnUpdateCommand="dg_user_list_UpdateCommand">
        <Columns>
            <asp:BoundColumn DataField="staff_serial_no" HeaderText="id">
            
            </asp:BoundColumn>
            <asp:BoundColumn DataField="staff_login_name" HeaderText="Login Name"></asp:BoundColumn>
            <asp:BoundColumn DataField="staff_realname" HeaderText="Real Name"></asp:BoundColumn>
            <asp:EditCommandColumn CancelText="取消" EditText="编辑" UpdateText="更新"></asp:EditCommandColumn>
            <asp:ButtonColumn CommandName="Select" Text="选择"></asp:ButtonColumn>
            <asp:ButtonColumn CommandName="Delete" Text="删除"></asp:ButtonColumn>
        </Columns>
    </anthem:DataGrid>
    <hr size="1" />
        <table>
            <tr>
                <td style="width: 300px;"><anthem:Label runat="server" ID="lbl_current_staff" ForeColor="Blue"></anthem:Label></td>
                <td><anthem:Button runat="server" id="btn_save" OnClick="btn_save_Click" Text="Save" /></td>
            </tr>
        </table>
        
    <hr size="1" />
   <anthem:CheckBoxList runat="server" ID="cbl_models" RepeatColumns="6" ></anthem:CheckBoxList>
</asp:Content>

