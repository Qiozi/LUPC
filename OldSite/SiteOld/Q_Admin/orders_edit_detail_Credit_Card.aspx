<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_Credit_Card.aspx.cs" Inherits="Q_Admin_orders_edit_detail_Credit_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <p >
           <hr size="1" style="clear:both">
            <asp:Button ID="btn_same_personal_address" runat="server" 
                Text="Same as account(Personal Information) address" 
                onclick="btn_same_personal_address_Click" />
            <asp:Button ID="btn_same_credit_card_address" runat="server" 
                Text="Same as account(Shipping) address" 
                onclick="btn_same_shipping_address_Click" />          
            <hr size="1" style="clear:both">

        <table>
                <tr>
                            <td>card first name</td><td><asp:TextBox runat="server" 
                                ID="txt_card_first_name" Columns="30" MaxLength="30"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>card last name</td><td><asp:TextBox runat="server" ID="txt_card_last_name" 
                                Columns="30" MaxLength="30"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>card number</td><td><asp:TextBox runat="server" ID="txt_card_number" 
                                Columns="30" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>verification_number</td><td><asp:TextBox runat="server" 
                                ID="txt_verification_number" Columns="30" MaxLength="3"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>card expiry</td><td><asp:TextBox runat="server" ID="txt_card_expiry" 
                                Columns="30" MaxLength="6"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>card issuer</td><td><asp:TextBox runat="server" ID="txt_card_isssuer" 
                                Columns="30" MaxLength="32"></asp:TextBox></td>
                </tr>                                                                                             
                <tr>
                            <td>card phone</td><td><asp:TextBox runat="server" ID="txt_card_phone" 
                                Columns="30" MaxLength="20"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>city</td><td><asp:TextBox runat="server" ID="txt_card_city" Columns="30" 
                                MaxLength="50"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>address</td><td><asp:TextBox runat="server" 
                                ID="txt_card_billing_shipping_address" Columns="30" MaxLength="150"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>zip code</td><td><asp:TextBox runat="server" ID="txt_card_zip_code" 
                                Columns="30" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                            <td valign="top">state</td><td valign="top">
                            <asp:DropDownList runat="server" 
                                ID="ddl_card_country" AutoPostBack="True" 
                                size="3" onselectedindexchanged="ddl_card_country_SelectedIndexChanged"></asp:DropDownList>
                                <asp:DropDownList runat="server" ID="ddl_card_state" size="3"></asp:DropDownList></td>
                </tr>
                 <asp:Panel runat="server" ID="otherCountryArea">
                <tr>
                            <td>Input Country</td><td><asp:TextBox runat="server" ID="txt_inputCountry" 
                                Columns="30" MaxLength="10"></asp:TextBox></td>
                </tr>
                <tr>
                            <td>Input State</td><td><asp:TextBox runat="server" ID="txt_inputState" 
                                Columns="30" MaxLength="10"></asp:TextBox></td>
                </tr>
                </asp:Panel>
        </table>
        <hr size="1" style="clear:both">
        <div style="text-align:center; clear:both;">
                    <asp:Button runat="server" ID="bt_save_credit_card" Text="Save" 
            OnClientClick="ParentLoadWait();" onclick="bt_save_credit_card_Click" />
        </div>
        
</p> 
</asp:Content>

