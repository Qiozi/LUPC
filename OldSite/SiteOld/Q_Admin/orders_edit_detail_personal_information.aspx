<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_personal_information.aspx.cs" Inherits="Q_Admin_orders_edit_detail_personal_information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
       <div style="padding: 1em;">
 <ul>
                <li><b style="font-size: 11pt;">Personal Information</b> 
                 
                                
                                <hr size="1" style="clear:both">
                                <table>
                                        <tr>
                                                    <td>Company</td><td><asp:TextBox runat="server" ID="txt_customer_company" 
                                                        Columns="30" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>first name</td><td><asp:TextBox runat="server" ID="txt_first_name" 
                                                        Columns="30" MaxLength="32"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>last name</td><td><asp:TextBox runat="server" ID="txt_last_name" 
                                                        Columns="30" MaxLength="32"></asp:TextBox></td>
                                        </tr>                                                                                                
                                        <tr>
                                                    <td>business phone</td><td><asp:TextBox runat="server" ID="txt_phone_d" Columns="30" 
                                                        MaxLength="20"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>home phone</td><td><asp:TextBox runat="server" ID="txt_phone_n" Columns="30" 
                                                        MaxLength="20"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>mobile phone</td><td><asp:TextBox runat="server" ID="txt_phone_c" Columns="30" 
                                                        MaxLength="20"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>email1</td><td><asp:TextBox runat="server" ID="txt_email1" Columns="30" 
                                                        MaxLength="100"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>email2</td><td><asp:TextBox runat="server" ID="txt_email2" Columns="30" 
                                                        MaxLength="100"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>city</td><td><asp:TextBox runat="server" ID="txt_customer_city" 
                                                        Columns="30" MaxLength="50"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>address</td><td><asp:TextBox runat="server" ID="txt_customer_address" 
                                                        Columns="30" MaxLength="150"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td>zip code</td><td><asp:TextBox runat="server" ID="txt_customer_zip_code" 
                                                        Columns="30" MaxLength="10"></asp:TextBox></td>
                                        </tr>
                                        <tr>
                                                    <td valign="top">state</td><td valign="top">
                                                    <asp:DropDownList runat="server" 
                                                        ID="ddl_customer_country" AutoPostBack="True" 
                                                        size="3" 
                                                        onselectedindexchanged="ddl_customer_country_SelectedIndexChanged" ></asp:DropDownList>
                                                        <asp:DropDownList runat="server" ID="ddl_customer_state" size="3"></asp:DropDownList>
                                                        
                                                   
                                                        </td>
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
                                                                                           
                </li>                                                                       
        </ul> </div> 
         <hr size="1" style="clear:both">
        <center>
            <asp:Button runat="server" ID="btn_save_personal_info" Text="Save" 
                                                                                      
                                    onclick="btn_save_personal_info_Click"/>
        </center>   
</asp:Content>

