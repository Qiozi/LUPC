<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="other_inc_add_part.aspx.cs" Inherits="Q_Admin_other_inc_add_part" Title="add part" %>
<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad"  tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> <table>
                <tr>
                        <td>
                                &nbsp;</td>
                        <td>
                            <asp:Button ID="btn_save" runat="server" Text="Save" onclick="btn_save_Click" />
                        </td>
                </tr>
                <tr>
                        <td colspan="2">
                               <hr size="1" /></td>
                </tr>
                <tr>
                        <td>
                                Category:
                        </td>
                        <td>
                            <uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" CFT="no_system" />
                        </td>
                </tr>
                <tr>
                        <td>Short Name:</td>
                        <td><asp:TextBox runat="server" ID="txt_short_name" Columns="80" TabIndex="1"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>Middle Name:</td>
                        <td><asp:TextBox runat="server" ID="txt_middle_name" Columns="80" TabIndex="2"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>long Name:</td>
                        <td><asp:TextBox runat="server" ID="txt_long_name" Columns="80" TabIndex="3"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>cost:</td>
                        <td><asp:TextBox runat="server" ID="txt_cost" TabIndex="4"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>special cash price:</td>
                        <td><asp:TextBox runat="server" ID="txt_special_cash_price" TabIndex="5"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>MFP:</td>
                        <td><asp:TextBox runat="server" ID="txt_manufactory_name" TabIndex="6"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>MFP #:</td>
                        <td><asp:TextBox runat="server" ID="txt_mfp_code" TabIndex="7"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>Priority</td>
                        <td><asp:TextBox runat="server" ID="txt_priority" TabIndex="8"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>Size</td>
                        <td>
                            <asp:DropDownList ID="ddl_product_size" runat="server">
                            </asp:DropDownList>
                        </td>
                </tr>
                <tr>
                        <td>other inc SKU*</td>
                        <td><asp:TextBox runat="server" ID="txt_other_inc_sku" TabIndex="8"></asp:TextBox></td>
                </tr>
                <tr>
                        <td>Other Inc</td>
                        <td>
                            <asp:DropDownList ID="ddl_other_inc" runat="server">
                            </asp:DropDownList>
                            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                        </td>
                </tr>
        </table>
</asp:Content>

