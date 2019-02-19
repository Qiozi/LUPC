<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_system_move_or_copy.aspx.cs" Inherits="Q_Admin_product_system_move_or_copy" Title="Untitled Page" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div>
<br />
        <table>
                <tr>
                        <td><asp:Label runat="server" ID="lbl_sku" Font-Bold="true"></asp:Label></td>
                        <td><asp:Label runat="server" ID="lbl_cmd"></asp:Label></td>
                        <td><uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" 
                            CFT="system" /></td>
                        <td><asp:Button runat="server" ID="btn_cmd" Text="Move" 
            onclick="btn_cmd_Click" /></td>
                </tr>
            
        </table>           
                * Move: 把指定的SKU<span style="color: Red;">转移到非虚</span>拟目录<br />
        * Copy: 把指定的SKU<span style="color: Red;">添加到虚</span>拟目录     
</div><asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

