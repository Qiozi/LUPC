<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="PriceView.aspx.cs" Inherits="Q_Admin_PriceView" Title="Compare Price" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Repeater runat="server" ID="rpt_cate_list" 
        onitemdatabound="rpt_cate_list_ItemDataBound">
        <HeaderTemplate>
            <table>
                <tbody>
        </HeaderTemplate>
        <ItemTemplate>
                    <tr>
                        <td>
                            <asp:HiddenField runat="server" ID="hf_p_cid" Value='<%# DataBinder.Eval(Container.DataItem, "menu_child_serial_no")%>' />
                            <b><%# DataBinder.Eval(Container.DataItem, "menu_child_name")%></b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBoxList runat="server" ID="cbl_child_cate" RepeatColumns="8"></asp:CheckBoxList>
                        </td>
                    </tr>
        </ItemTemplate>
        <FooterTemplate>
                </tbody>
            </table>
        </FooterTemplate>
    </asp:Repeater>
    <hr size='1' />
    <table>
        <tr>
            <td></td>
            <td><asp:Button ID="btn_go" runat="server" Text="Go" onclick="btn_go_Click" />
                <asp:Button ID="btn_download" runat="server" Text="Download" 
                    onclick="btn_download_Click" />
            </td>
            <td>
                <asp:Label ID="Label1" runat="server" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
    </table>
    
    
        <hr size='1' />
    <asp:GridView ID="GridView1" runat="server" ondatabound="GridView1_DataBound">
    </asp:GridView>
        
</asp:Content>


