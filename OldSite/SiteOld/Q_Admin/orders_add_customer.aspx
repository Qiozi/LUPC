<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_add_customer.aspx.cs" Inherits="Q_Admin_orders_add_customer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
 .btn { padding:.5em;}
 #ordersArea a{ display:block; padding:0.5em; border:1px solid #ccc; float:left;margin:2px;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div style='padding:1em; background:#f2f2f2; border:1px solid #ccc;'>
        Keyword:<asp:TextBox runat="server" ID="txtKeyword"></asp:TextBox>
        <asp:Button runat="server" ID="btnGo" Text="GO" onclick="btnGo_Click"/>
        <asp:Label
            ID="lblGoNote" runat="server" ForeColor="Red"></asp:Label>
        <br /><i style="color:Blue;">&nbsp;customer number, login email</i>
    </div>
<table>    
    <tr>
        <td valign="top">
                <fieldset class="fieldset2">
                    <legend><asp:LinkButton runat="server" ID="lb_simple_info" Text="base info" 
                            onclick="lb_simple_info_Click"></asp:LinkButton> </legend>
                           
                            <asp:Panel runat="server" ID="panel_simple_info">
                                <table>
                                    <tr>
                                        <td>
                                            Password</td>
                                        <td>
                                            <asp:TextBox ID="txt_password" runat="server"  
                                                CssClass="input" TabIndex="1">1111</asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                            <td>login name</td><td>
                                            <asp:TextBox ID="txt_customer_login_name" runat="Server"  
                                                CssClass="input" OnTextChanged="txtcustomer_fullname_TextChanged" 
                                                TabIndex="2" AutoPostBack="True"></asp:TextBox>
                                            <br />
                                            username@domain.com</td>
                                            <td>
                                                <asp:Label ID="lbl_validate_login" runat="server"></asp:Label>
                                            </td>
                                            
                                    </tr>                                                                       
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            first_name
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtcustomer_first_name" runat="Server"  
                                                CssClass="input" OnTextChanged="txtcustomer_first_name_TextChanged" 
                                                TabIndex="3" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_validate_first_name" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            last_name</td>
                                        <td>
                                            <asp:TextBox ID="txtcustomer_last_name" runat="Server"  
                                                CssClass="input" OnTextChanged="txtcustomer_last_name_TextChanged" 
                                                TabIndex="4" AutoPostBack="True"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lbl_validate_customer" runat="server"></asp:Label>
                                        </td>
                                    </tr>    
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>                               
                                    <tr>
                                        <td>
                                            phoneN</td>
                                        <td>
                                            <asp:TextBox ID="txt_phone_n" runat="Server"  
                                                CssClass="input" TabIndex="5"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PhoneD</td>
                                        <td>
                                            <asp:TextBox ID="txt_phone_d" runat="Server"  
                                                CssClass="input" 
                                                TabIndex="6"></asp:TextBox>
                                        </td>
                                        <td>
                                            </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            PhoneC</td>
                                        <td>
                                            <asp:TextBox ID="txt_phone_c" runat="Server" 
                                                CssClass="input" TabIndex="7"></asp:TextBox></td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            email1</td>
                                        <td>
                                            <asp:TextBox ID="txtcustomer_email1" runat="Server" CssClass="input" 
                                                TabIndex="8"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            email2</td>
                                        <td>
                                            <asp:TextBox ID="txtcustomer_email2" runat="Server" CssClass="input" 
                                                TabIndex="9"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Address</td>
                                        <td>
                                            <asp:TextBox ID="txt_customer_address" runat="Server" CssClass="input" 
                                                TabIndex="10" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            City</td>
                                        <td>
                                            <asp:TextBox ID="txt_customer_city" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Country</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_customer_country" runat="Server"  
                                                AutoPostBack="True" CssClass="input" 
                                                OnSelectedIndexChanged="ddl_customer_country_SelectedIndexChanged" 
                                                TabIndex="12" Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    <asp:Panel ID="otherCountryArea1" runat="Server" Visible ="false">
                                        <tr>
                                            <td>
                                           Input Country</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherCountryName1" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Province</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherCountryProvince1" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panel_province1">
                                    <tr>
                                        <td>
                                            Province</td>
                                        <td>
                                            <asp:DropDownList ID="ddl_customer_state" runat="server" TabIndex="13" 
                                                Width="150px">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td>
                                            Post code</td>
                                        <td>
                                            <asp:TextBox ID="txt_customer_zip_code" runat="Server" CssClass="input" 
                                                Height="16px" TabIndex="14"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            
                            </asp:Panel>
                           
                </fieldset>
        </td>
        <td valign="top">
                <fieldset class="fieldset2">
                        
                        <legend> <asp:LinkButton runat="server" ID="lb_shipping_address" 
                                Text="Shipping Address" onclick="lb_shipping_address_Click"></asp:LinkButton> </legend>
                              
                        <asp:Panel runat="server" ID="panel_shipping_address" Visible="True">
                                <table>
                                            <tr>
                                                <td>&nbsp;</td>
                                                <td>
                                                           <asp:CheckBox ID="cb_set_same_base_info" runat="server" AutoPostBack="True" 
                                                               OnCheckedChanged="cb_set_same_base_info_CheckedChanged" TabIndex="15" 
                                                               Text="Same as base info" Visible="False" />
                                        
                                                           <asp:CheckBox ID="cb_set_same" runat="server" Text="Same as Credit Card" 
                                                                OnCheckedChanged="cb_set_same_CheckedChanged" 
                                                        TabIndex="34" AutoPostBack="True" Visible="False" />
                                                           <asp:RadioButtonList ID="RadioButtonList_shipping" runat="server" 
                                                               AutoPostBack="True" 
                                                               onselectedindexchanged="RadioButtonList_shipping_SelectedIndexChanged" 
                                                               TabIndex="15">
                                                               <asp:ListItem Value="1">Same as base info</asp:ListItem>
                                                               <asp:ListItem Value="2">Same as credit card</asp:ListItem>
                                                           </asp:RadioButtonList>
                                                           
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;First Name</td>
                                                <td>
                                                           <asp:TextBox ID="txt_shipping_first_name" runat="server" 
                                                         CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="16"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                       <td>
                                                           Last Name</td>
                                                <td>
                                                           <asp:TextBox ID="txt_shipping_last_name" runat="server" 
                                                         CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="17"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Address</td>
                                                <td>
                                                           <asp:TextBox ID="txt_customer_shipping_address" runat="server" 
                                                                CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="18" 
                                                               TextMode="MultiLine"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>City</td>
                                                <td>
                                                           <asp:TextBox ID="txt_customer_shipping_city" runat="server" 
                                                         CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="19"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td>Country</td>
                                                <td>
                                                    <asp:DropDownList runat="server" 
                                                                id="ddl_customer_shipping_country" CssClass="input"  
                                                                OnSelectedIndexChanged="ddl_shipping_country_SelectedIndexChanged" 
                                                                Width="150px" TabIndex="20" AutoPostBack="True">
                                                        </asp:DropDownList></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <asp:Panel ID="otherCountryArea2" runat="Server" Visible ="false">
                                        <tr>
                                            <td>
                                           Input Country</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherCountry2" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Province</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherProvince2" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel ID="panel_province2" runat="server">
                                            <tr>
                                                <td>Province</td>
                                                <td>
                                                    <asp:DropDownList
                                                                ID="ddl_customer_shipping_state" runat="server" TabIndex="21" 
                                                        Width="150px">
                                                           </asp:DropDownList></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    </asp:Panel>
                                            <tr>
                                                <td>Post code</td>
                                                <td>
                                                           <asp:TextBox ID="txt_customer_shipping_zip_code" runat="server" 
                                                               CssClass="input" TabIndex="22"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                            </table>
                             
                      </asp:Panel>
                </fieldset>
        </td>
        <td valign="top">
                <fieldset class="fieldset2">                                
                                <legend>
                                    <asp:LinkButton runat="server" ID="lb_shipping_and_credit_card" 
                                        Text="Credit Card" onclick="lb_shipping_and_credit_card_Click"></asp:LinkButton></legend>
                                
                                   
                                <asp:Panel runat="server" ID="panel_shipping_and_credit_card" Visible="True">
                                <table>
                                        <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
				                                            <asp:CheckBoxList ID="CheckBoxList_card" runat="server" 
                                                                onselectedindexchanged="CheckBoxList1_SelectedIndexChanged" Visible="False">
                                                                <asp:ListItem Value="1">Same as base info</asp:ListItem>
                                                                <asp:ListItem Value="2">Same as shiping address</asp:ListItem>
                                                            </asp:CheckBoxList>
                                                            <asp:RadioButtonList ID="RadioButtonList_card" runat="server" 
                                                                AutoPostBack="True" 
                                                                onselectedindexchanged="RadioButtonList_card_SelectedIndexChanged" 
                                                                TabIndex="23">
                                                                <asp:ListItem Value="1">Same as base info</asp:ListItem>
                                                                <asp:ListItem Value="2">Same as shipping address</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                            <td>
                                                First Name</td>
                                            <td>
                                                <asp:TextBox ID="txt_customer_card_first_name" runat="Server" CssClass="input" 
                                                    TabIndex="24"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                                <td>
                                                           Last Name</td>
                                                <td>
				                                            <asp:TextBox ID="txt_customer_card_last_name" runat="Server" CssClass="input" 
                                                                TabIndex="25"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                            <td>
                                                Address</td>
                                            <td>
                                                <asp:TextBox ID="txt_customer_card_billing_shipping_address" runat="Server" 
                                                    CssClass="input" TabIndex="26" TextMode="MultiLine"></asp:TextBox>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                                <td>
                                                    City</td>
                                                <td>
				                                            <asp:TextBox ID="txt_customer_card_city" runat="Server" CssClass="input" 
                                                                TabIndex="27"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                                <td>
                                                               Country</td>
                                                <td>
                                                               <asp:DropDownList
                                                                ID="ddl_customer_card_country" runat="Server" TabIndex="28" Width="150px" 
                                                                   AutoPostBack="True" CssClass="input" 
                                                                   OnSelectedIndexChanged="ddl_customer_card_country_SelectedIndexChanged">
                                                            </asp:DropDownList></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                     <asp:Panel ID="otherCountryArea3" runat="Server" Visible ="false">
                                        <tr>
                                            <td>
                                           Input Country</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherCountry3" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Province</td>
                                        <td>
                                            <asp:TextBox ID="txt_otherProvince3" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat="server" ID="panel_Province3">
                                        <tr>
                                                <td>
                                                           Province</td>
                                                <td>
				                                            <asp:DropDownList ID="ddl_customer_card_state" runat="server" TabIndex="29" 
                                                                Width="150px">
                                                            </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    </asp:Panel>
                                        <tr>
                                                <td>
                                                           Post code</td>
                                                <td>
				                                            <asp:TextBox ID="txt_customer_card_zip_code" runat="Server" CssClass="input" 
                                                                Height="16px" TabIndex="30"></asp:TextBox>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                                <td>
                                                           &nbsp;</td>
                                                <td>
				                                            &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                                <td>
                                                               &nbsp;</td>
                                                <td>
				                                            &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                                <td>
                                                           Type CC</td>
                                                <td>
				                                            <asp:DropDownList ID="ddl_customer_card_type" runat="Server" CssClass="input" 
                                                                TabIndex="31">
                                                                <Items>
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem>MC</asp:ListItem>
                                                                    <asp:ListItem Value="VS">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                </Items>
                                                            </asp:DropDownList>
                                                           </td>
                                                <td>
                                                           &nbsp;</td>
                                            </tr>
                                        <tr>
                                                <td>
                                                           CC Number</td>
                                                <td>
				                                            <asp:TextBox runat="Server" id="txt_customer_credit_card" CssClass="input" 
                                                                TabIndex="32" OnTextChanged="txtcustomer_credit_card_TextChanged"></asp:TextBox>
                                                           </td>
                                                <td>
                                                           <asp:Label ID="lbl_watch_card" runat="server" ForeColor="Red" Visible="False">**</asp:Label>
                                                </td>
                                            </tr>
                                        <tr>
                                                <td>
                                                           Expiry Date</td>
                                                <td>
				                                            <asp:TextBox runat="Server" id="txt_customer_expiry" CssClass="input" 
                                                                   TabIndex="33" Columns="6" maxlength="7"></asp:TextBox></td>
                                                <td>
                                                    (MMyy)</td>
                                            </tr>
                                        <tr>
                                                <td>
                                                          CC phone</td>
                                                <td>
				                                            <asp:TextBox runat="Server" id="txt_customer_card_phone" CssClass="input" 
                                                                   TabIndex="34"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        <tr>
                                            <td>
                                                Card Issuer</td>
                                            <td>
                                                <asp:TextBox ID="txt_customer_card_issuer" runat="Server" CssClass="input" 
                                                    TabIndex="35"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Verify Code</td>
                                            <td>
                                                <asp:TextBox ID="txt_verify_code" runat="Server" CssClass="input" MaxLength="6" 
                                                    TabIndex="35"></asp:TextBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                </table>
                                
                                 
                                </asp:Panel>
                      
                        </fieldset></td>
        <td valign="top"><fieldset class="fieldset2">
                            
                            <legend><asp:LinkButton runat="server" ID="lb_business" Text="Business" 
                                    onclick="lb_business_Click"></asp:LinkButton></legend>
                            
                            <asp:Panel runat="server" ID="panel_business" Visible="True">        
                            <table>
                                    <tr>
                                                <td>
                                                           company</td>
                                                <td>
				                                            <asp:TextBox runat="Server" id="txt_customer_company" CssClass="input" 
                                                                   TabIndex="36"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <tr>
                                                <td>
                                                           Telephone</td>
                                                <td>
                                                           <asp:TextBox ID="txt_customer_business_telephone" runat="Server" 
                                                               CssClass="input" TabIndex="37"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                   
                                    <tr>
                                                <td>
                                                               Address</td>
                                                <td>
                                                               <asp:TextBox ID="txt_business_address1" runat="server" CssClass="input" 
                                                                   TabIndex="38" TextMode="MultiLine" Rows="2"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <tr>
                                                <td>
                                                         City</td>
                                                <td>
                                                               <asp:TextBox ID="txt_business_city" runat="server" CssClass="input" 
                                                                   TabIndex="39"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                     <tr>
                                                <td>
                                                               Country</td>
                                                <td>
                                                           <asp:DropDownList runat="server" id="ddl_business_country" CssClass="input" 
                                                                
                                                               OnSelectedIndexChanged="ddl_business_country_SelectedIndexChanged" 
                                                               Width="150px" TabIndex="40" AutoPostBack="True">
                                                           </asp:DropDownList></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                <asp:Panel ID="otherCountryArea4" runat="Server" Visible ="false">
                                        <tr>
                                            <td>
                                           Input Country</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherCountry4" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                            Province</td>
                                        <td>
                                            <asp:TextBox ID="txt_OtherProvince4" runat="Server" CssClass="input" 
                                                TabIndex="11"></asp:TextBox>
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                        </tr>
                                    </asp:Panel>
                                    <asp:Panel runat ="server" ID="panel_Province4">
                                    <tr>
                                                <td>
                                                           Province</td>
                                                <td>
                                                           <asp:DropDownList
                                                                ID="ddl_business_state" runat="server" TabIndex="41" Width="150px">
                                                           </asp:DropDownList></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    </asp:Panel>
                                    <tr>
                                                <td>
                                                           Post Code</td>
                                                <td>
                                                               <asp:TextBox ID="txt_business_zip_code" runat="server" CssClass="input" 
                                                                   TabIndex="42"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <tr>
                                                <td>
                                                               Website</td>
                                                <td>
                                                               <asp:TextBox ID="txt_busniess_website" runat="server" CssClass="input" 
                                                                   TabIndex="43"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <tr>
                                                <td>
                                                               Tax Ex#</td>
                                                <td>
				                                            <asp:TextBox runat="server" id="txt_tax_execmtion" CssClass="input" 
                                                                MaxLength="9" TabIndex="44"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <tr>
                                                <td>
                                                           Fax#</td>
                                                <td>
				                                            <asp:TextBox runat="Server" id="txt_customer_fax" CssClass="input" 
                                                                TabIndex="45"></asp:TextBox></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                    <tr>
                                                <td>
                                                         &nbsp;</td>
                                                <td>
				                                            &nbsp;</td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                            </table></asp:Panel>

                            
                    </fieldset></td>
    </tr>

    <tr>
            <td valign="top" class="btn_list" style="text-align:right" colspan="4">
            <div id="ordersArea">
		     <asp:Literal runat="Server" ID="ltOrderHistory"></asp:Literal> &nbsp;
            </div>
                                                    </td>
    </tr>
    <tr>
            <td valign="top" class="btn_list" style="text-align:right" colspan="3">

                     <input type="button" Class="btn" value="Search Customer" onclick="ShowIframe('Search Customer','/q_admin/orders_add_customer_search.aspx',800,400)"/>
		            <asp:Button ID="btn_new_customer" runat="server"  CssClass="btn"
                        onclick="btn_new_customer_Click" Text="New Customer" 
                        onclientclick="ParentLoadWait();" TabIndex="46" />
                    <asp:Button ID="btn_new_sales_order" runat="server"   CssClass="btn"
                        onclick="btn_new_sales_order_Click" Text="New Sales Order" 
                        onclientclick="ParentLoadWait();" TabIndex="47" />
                    <asp:Button ID="btn_save_change" runat="server" onclick="btn_save_change_Click"   CssClass="btn"
                        Text="Save Change" onclientclick="ParentLoadWait();" TabIndex="48" />
                    <asp:Button ID="btn_cancel" runat="server" Enabled="False" ForeColor="#CCCCCC"   CssClass="btn"
                        Text="Cancel" TabIndex="49" />

                                                   
            </td>
            <td valign="top" class="btn_list" style="text-align:right">
		                                            &nbsp;</td>
    </tr>
</table>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
   
   <script type="text/javascript">
        $().ready(function(){
                $('input').bind('onblur',function(){
                    event.preventDefault();
                    alert("ye");
                });
        });
   </script>
</asp:Content>

