<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_price_compare.aspx.cs" Inherits="Q_Admin_product_helper_price_compare" Title="View Compare" %>

<%@ Register src="UC/MenuChildName.ascx" tagname="MenuChildName" tagprefix="uc1" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    th { border-bottom: 1px solid #ccc;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <table>
                <td width="160">
                    <uc2:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" />
                </td>
                <td width="50" style="text-align:left">
                    <asp:Button ID="btn_go" runat="server" Text="Go" onclick="btn_go_Click" /></td>
                <td>
                        
                          <asp:CheckBoxList runat="server" ID="cb_fields" RepeatColumns="11" 
                              RepeatDirection="Horizontal" AutoPostBack="True" 
                              onselectedindexchanged="cb_fields_SelectedIndexChanged"/> 
                                    
                </td>
        </table>
        <hr size="1" />
        <asp:Literal runat="server" ID="literal_view_compare"></asp:Literal>
    </div>
   
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>  
</asp:Content>

