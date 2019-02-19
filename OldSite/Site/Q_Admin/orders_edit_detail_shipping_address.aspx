<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_shipping_address.aspx.cs" Inherits="Q_Admin_orders_edit_detail_shipping_address" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <div>
           <hr size="1" style="clear:both">
            <asp:Button ID="btn_same_personal_address" runat="server" 
                Text="Same as account(Personal Information) address" 
                onclick="btn_same_personal_address_Click" />
            <asp:Button ID="btn_same_credit_card_address" runat="server" 
                Text="Same as account(Credit card) address" 
               onclick="btn_same_credit_card_address_Click" />          
            <hr size="1" style="clear:both">
            <hr size="1" style="clear:both">
            <table>
                    <tr>
                                <td>shipping first name</td><td><asp:TextBox runat="server" ID="txt_shipping_first_name" Columns="20" MaxLength="32"></asp:TextBox></td>
                    </tr>
                    <tr>
                                <td>shipping last name</td><td><asp:TextBox runat="server" ID="txt_shipping_last_name" Columns="20" MaxLength="32"></asp:TextBox></td>
                    </tr>
                    <tr>
                                <td>shipping address</td><td><asp:TextBox runat="server" ID="txt_shipping_address" Columns="20" MaxLength="150"></asp:TextBox></td>
                    </tr>
                    <tr>
                                <td>shipping city</td><td><asp:TextBox runat="server" ID="txt_shipping_city" Columns="20" MaxLength="32"></asp:TextBox></td>
                    </tr>
                    <tr>
                                <td>shipping zip code</td><td><asp:TextBox runat="server" ID="txt_shipping_zipcode" Columns="20" MaxLength="10"></asp:TextBox></td>
                    </tr>
                    <tr>
                                <td valign="top">shipping state</td><td valign="top"><asp:DropDownList runat="server" 
                                    ID="ddl_shipping_country" AutoPostBack="True" 
                                    onselectedindexchanged="ddl_shipping_country_SelectedIndexChanged" size="3"></asp:DropDownList>
                                    <asp:DropDownList runat="server" ID="ddl_shipping_state" size="3"></asp:DropDownList></td>
                    </tr>
                    <asp:Panel runat="server" ID="otherCountryArea">
                    <tr>
                                <td>Input Country</td><td><asp:TextBox runat="server" ID="txt_inputCountry" 
                                    Columns="30" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    <tr>
                                <td>Input State</td><td><asp:TextBox runat="server" ID="txt_inputState" 
                                    Columns="30" MaxLength="100"></asp:TextBox></td>
                    </tr>
                    </asp:Panel>                                                            
            </table>

            <hr size="1" style="clear:both">
             <div style="text-align:center; clear:both;">
                        <asp:Button runat="server" ID="btn_save_shipping_address" Text="Save" 
                OnClientClick="ParentLoadWait();" onclick="btn_save_shipping_address_Click" />
            </div>
    </div>   
</asp:Content>

