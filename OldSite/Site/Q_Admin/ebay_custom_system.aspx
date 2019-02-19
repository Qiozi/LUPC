<%@ Page Language="C#" MasterPageFile="~/Q_Admin/ebay.master" AutoEventWireup="true" CodeFile="ebay_custom_system.aspx.cs" Inherits="Q_Admin_ebay_custom_system" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<hr size="1" />
    <asp:Button ID="btn_add_part" runat="server" Text="+" 
        onclick="btn_add_part_Click" /><asp:Button ID="btn_save" runat="server" 
    Text="Save" onclick="btn_save_Click" />
<hr size="1" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate> 
    <asp:GridView runat="server" ID="gv_custom_sys" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="id"  />
            <asp:BoundField  />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:DropDownList ID="_ddl_part_group" runat="server" 
                        DataSource ="<%# PartGroup %>" DataTextField="part_group_comment" DataValueField="part_group_id"
                     AutoPostBack="True" 
                        onselectedindexchanged="_ddl_part_group_SelectedIndexChanged">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:DropDownList ID="_ddl_part" runat="server">
                    </asp:DropDownList>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>

    </asp:GridView>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

