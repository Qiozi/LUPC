<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_configure_helper.aspx.cs" Inherits="Q_Admin_product_helper_configure_helper" Title="Configure Setting" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" />
    <table width="100%">
        <tr>
            <td style="width: 532px" valign="top">
                <anthem:RadioButtonList ID="radio_configure" runat="server" AutoCallBack="True" OnSelectedIndexChanged="radio_configure_SelectedIndexChanged"
        RepeatColumns="3">
    </anthem:RadioButtonList><anthem:DataGrid runat="server" ID="dg_configure_detail" AutoGenerateColumns="False" OnItemDataBound="dg_configure_detail_ItemDataBound" Width="400px" OnItemCommand="dg_configure_detail_ItemCommand" PreCallBackFunction="confirm_PreCallBack">
                    <HeaderStyle CssClass="trTitle" />
                    <ItemStyle CssClass="tdItem" /><Columns>
                        <asp:BoundColumn DataField="value" HeaderText="SKU">
                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                                font-underline="False" horizontalalign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="text" HeaderText="part category "></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="Delete" Text="delete">
                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                                font-underline="False" horizontalalign="Center" />
                        </asp:ButtonColumn>
                    </Columns>
                
                </anthem:DataGrid>
               <div>
                    <hr size="1" />
                   <br />
                    Computer Cases:&nbsp;&nbsp;&nbsp;&nbsp;<anthem:Label ID="lbl_computer_cases" runat="server"></anthem:Label>
                   <br />
                   Computer CPU: &nbsp; &nbsp; &nbsp;
                   <anthem:Label ID="lbl_computer_cpu" runat="server"></anthem:Label></div>
             </td>
            <td valign="top">
                <anthem:Button ID="btn_Save" runat="server" OnClick="btn_Save_Click" Text="Save" /><anthem:Button ID="btn_Computer_Cases" runat="server" Text="Computer Cases" OnClick="btn_Computer_Cases_Click" /><br /><anthem:Button ID="btn_save_cpu" runat="server" Text="Computer CPU" OnClick="btn_save_cpu_Click" />
                <anthem:DataGrid ID="dg_part_category" runat="server" AutoGenerateColumns="False" Width="400px">
                <HeaderStyle CssClass="trTitle" />
                <ItemStyle CssClass="tdItem" />
                    <Columns>
                        <asp:BoundColumn DataField="menu_child_serial_no" HeaderText="SKU">
                            <itemstyle font-bold="False" font-italic="False" font-overline="False" font-strikeout="False"
                                font-underline="False" horizontalalign="Center" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="menu_child_name" HeaderText="part category"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <itemtemplate>
<anthem:CheckBox id="_cb_category" runat="server" __designer:wfdid="w7"></anthem:CheckBox>
</itemtemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid>
            </td> 
        </tr>
    </table>
</asp:Content>

