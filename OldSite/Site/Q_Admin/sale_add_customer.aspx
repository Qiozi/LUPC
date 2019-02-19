<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="sale_add_customer.aspx.cs" Inherits="Q_Admin_sale_add_customer" Title="Add Customer and Order" %>
<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Src="UC/NewOrder.ascx" TagName="NewOrder" TagPrefix="uc3" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content2" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

 <uc1:Navigation ID="Navigation1" runat="server" />
<table style="width: 100%;">
                    <tr>
                            <td valign="top">
                                        <asp:Panel runat="server" ID="panel3"  SkinID="back_blue" Width="100%">
                                                <asp:panel runat="server" ID="panel4" SkinID="panel_title1" Width="100%">
                                                     &nbsp;&nbsp;&gt;&gt; Add Customer</asp:panel> 
                                                   <table >
                                                   <tr>
                                                       <td style="width: 128px">
                                                           login name</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_customer_login_name" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_fullname_TextChanged" TabIndex="1"></anthem:TextBox>
                                                           <anthem:Label ID="lbl_validate_login" runat="server"></anthem:Label></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           <anthem:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="Green">Credit Card</anthem:Label></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           <anthem:Label ID="Label3" runat="server" Font-Bold="True" ForeColor="Green">Business</anthem:Label></td>
                                                   </tr>
                                                       <tr>
                                                           <td style="width: 128px">
                                                               Password</td>
                                                           <td style="width: 285px">
                                                               <anthem:TextBox ID="txt_password" runat="server" AutoCallBack="True" CssClass="input" TabIndex="2"></anthem:TextBox></td>
                                                           <td>
                                                               Type CC</td>
                                                           <td>
				                                            <anthem:DropDownList runat="Server" id="ddl_customer_card_type" CssClass="input" TabIndex="16">
                                                                <Items>
                                                                    <asp:ListItem Value="-1">Select</asp:ListItem>
                                                                    <asp:ListItem>MC</asp:ListItem>
                                                                    <asp:ListItem Value="VS">
                                                                    </asp:ListItem>
                                                                    <asp:ListItem>AM</asp:ListItem>
                                                                </Items>
                                                            </anthem:DropDownList></td>
                                                           <td>
                                                           company</td>
                                                           <td>
				                                            <anthem:TextBox runat="Server" id="txt_customer_company" CssClass="input" TabIndex="21"></anthem:TextBox></td>
                                                       </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           first_name
                                                       </td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txtcustomer_first_name" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="3"></anthem:TextBox>
                                                           <anthem:Label ID="lbl_validate_first_name" runat="server"></anthem:Label></td>
                                                       <td>
                                                           CC Number</td>
                                                       <td>
				                                            <anthem:TextBox runat="Server" id="txt_customer_credit_card" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_credit_card_TextChanged" TabIndex="17"></anthem:TextBox>
                                                           <anthem:Label ID="lbl_watch_card" runat="server" ForeColor="Red" Visible="False">**</anthem:Label></td>
                                                       <td>
                                                           Telephone</td>
                                                       <td>
                                                           <anthem:TextBox ID="txt_customer_business_telephone" runat="Server" CssClass="input" TabIndex="22"></anthem:TextBox></td>
                                                   </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           last_name
                                                       </td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txtcustomer_last_name" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_last_name_TextChanged" TabIndex="4"></anthem:TextBox>
                                                           <anthem:Label ID="lbl_validate_customer" runat="server"></anthem:Label></td>
                                                       <td>
                                                           Expiry Date</td>
                                                       <td>
				                                            <anthem:TextBox runat="Server" id="txt_customer_expiry" CssClass="input" Columns="6" maxlength="7" TabIndex="18"></anthem:TextBox>
                                                           (MMyy)</td>
                                                       <td>
                                                               Country</td>
                                                       <td>
                                                           <anthem:DropDownList runat="server" id="ddl_customer_country" CssClass="input" AutoCallBack="True" OnSelectedIndexChanged="ddl_customer_country_SelectedIndexChanged1" Width="150px" TabIndex="23">
                                                           </anthem:DropDownList></td>
                                                   </tr>
                                                       <tr>
                                                           <td style="width: 128px">
                                                           EBay ID</td>
                                                           <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_EBay_ID" CssClass="input" TabIndex="5"></anthem:TextBox></td>
                                                           <td>
                                                           CC phone</td>
                                                           <td>
				                                            <anthem:TextBox runat="Server" id="txt_customer_card_phone" CssClass="input" TabIndex="19"></anthem:TextBox></td>
                                                           <td>
                                                               Address</td>
                                                           <td>
                                                               <anthem:TextBox ID="txt_customer_address1" runat="server" CssClass="input" TabIndex="24"></anthem:TextBox></td>
                                                       </tr>
                                                       <tr>
                                                           <td style="width: 128px">&nbsp;
                                                           </td>
                                                           <td style="width: 285px">
                                                           </td>
                                                           <td>
                                                          Card Issuer</td>
                                                           <td>
				                                            <anthem:TextBox runat="Server" id="txt_customer_card_issuer" CssClass="input" TabIndex="20"></anthem:TextBox></td>
                                                           <td>
                                                               City</td>
                                                           <td>
                                                               <anthem:TextBox ID="txt_customer_city" runat="server" CssClass="input" TabIndex="25"></anthem:TextBox></td>
                                                       </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           PhoneD</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_phone_d" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_home_tel_TextChanged" TabIndex="6"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           <anthem:Label ID="Label2" runat="server" Font-Bold="True" ForeColor="Green">Shipping Address</anthem:Label><br />
                                                           <anthem:CheckBox ID="cb_set_same" runat="server" Text="Same as Left" AutoCallBack="True" OnCheckedChanged="cb_set_same_CheckedChanged" TabIndex="34" /></td>
                                                       <td>
                                                           Province</td>
                                                       <td>
                                                           <anthem:DropDownList
                                                                ID="ddl_state_serial_no" runat="server" TabIndex="26">
                                                           </anthem:DropDownList></td>
                                                   </tr>
                                                   <tr>
                                                       <td style="width: 128px; ">
                                                           phoneN</td>
                                                       <td style="width: 285px;">
				                                            <anthem:TextBox runat="Server" id="txt_phone_n" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_work_tel_TextChanged" TabIndex="7"></anthem:TextBox></td>
                                                       <td style="">
                                                           First Name</td>
                                                       <td style="">
                                                           <anthem:TextBox ID="txt_shipping_first_name" runat="server" AutoCallBack="True" CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="35"></anthem:TextBox></td>
                                                       <td style="">
                                                           Post Code</td>
                                                       <td style="">
                                                               <anthem:TextBox ID="txt_zip_code" runat="server" CssClass="input" TabIndex="27"></anthem:TextBox></td>
                                                   </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           PhoneC</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_phone_c" CssClass="input" AutoCallBack="True" OnTextChanged="txtcustomer_cell_tel_TextChanged" TabIndex="8"></anthem:TextBox></td>
                                                       <td>
                                                           Last Name</td>
                                                       <td>
                                                           <anthem:TextBox ID="txt_shipping_last_name" runat="server" AutoCallBack="True" CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="36"></anthem:TextBox></td>
                                                       <td>
                                                               Website</td>
                                                       <td>
                                                               <anthem:TextBox ID="txt_busniess_website" runat="server" CssClass="input" TabIndex="28"></anthem:TextBox></td>
                                                   </tr>
                                                    
                                                   <tr>
                                                       <td style="width: 128px">
                                                           </td>
                                                       <td style="width: 285px">
				                                            </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           Address</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_customer_card_billing_shipping_address" CssClass="input" TabIndex="9" TextMode="MultiLine"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           <anthem:TextBox ID="txt_customer_shipping_address" runat="server" AutoCallBack="True" CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="37" TextMode="MultiLine"></anthem:TextBox></td>
                                                       <td>
                                                               Tax Ex#</td>
                                                       <td>
				                                            <anthem:TextBox runat="server" id="txt_tax_execmtion" CssClass="input" MaxLength="9" TabIndex="29"></anthem:TextBox></td>
                                                   </tr>
                                                  
                                                   <tr>
                                                       <td style="width: 128px">
                                                           City
                                                       </td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_customer_card_city" CssClass="input" TabIndex="10"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           <anthem:TextBox ID="txt_customer_shipping_city" runat="server" AutoCallBack="True" CssClass="input"
                                                               OnTextChanged="txtcustomer_first_name_TextChanged" TabIndex="38"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                   </tr>
                                                    <tr>
                                                       <td style="width: 128px">
                                                           country</td>
                                                       <td style="width: 285px">
				                                            <anthem:DropDownList runat="Server" id="ddl_customer_card_country" CssClass="input" AutoCallBack="True" OnSelectedIndexChanged="ddl_customer_country_SelectedIndexChanged" Width="150px" TabIndex="11"></anthem:DropDownList></td>
                                                        <td>
                                                        </td>
                                                        <td><anthem:DropDownList runat="server" id="ddl_customer_shipping_country" CssClass="input" AutoCallBack="True" OnSelectedIndexChanged="ddl_shipping_country_SelectedIndexChanged" Width="150px" TabIndex="39">
                                                        </anthem:DropDownList></td>
                                                        <td>
                                                        </td>
                                                        <td>
                                                        </td>
                                                   
                                                   </tr>   <tr>
                                                           <td style="width: 128px">
                                                               Province</td>
                                                           <td style="width: 285px">
                                                               <anthem:DropDownList
                                                                ID="ddl_customer_card_state" runat="server" TabIndex="12">
                                                            </anthem:DropDownList></td>
                                                       <td>
                                                       </td>
                                                           <td><anthem:DropDownList
                                                                ID="ddl_customer_shipping_state" runat="server" TabIndex="40">
                                                           </anthem:DropDownList></td>
                                                       <td>
                                                           Fax#</td>
                                                       <td>
				                                            <anthem:TextBox runat="Server" id="txt_customer_fax" CssClass="input" TabIndex="30"></anthem:TextBox></td>
                                                       </tr>
                                                   
                                                   <tr>
                                                       <td style="width: 128px">
                                                           Post code</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_customer_card_zip_code" CssClass="input" TabIndex="13"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           <anthem:TextBox ID="txt_customer_shipping_zip_code" runat="server" CssClass="input" TabIndex="41"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                   </tr>
                                                       <tr>
                                                           <td style="width: 128px">
                                                               &nbsp;
                                                           </td>
                                                           <td style="width: 285px">
                                                           </td>
                                                           <td>
                                                           </td>
                                                           <td>
                                                           </td>
                                                           <td>
                                                           </td>
                                                           <td>
                                                           </td>
                                                       </tr>
                                                  
                                                   <tr>
                                                       <td style="width: 128px">
                                                           email1</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txtcustomer_email1" CssClass="input" TabIndex="14"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                           </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                   </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           email2</td>
                                                       <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txtcustomer_email2" CssClass="input" TabIndex="15"></anthem:TextBox></td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                   </tr>
                                                       <tr>
                                                           <td style="width: 128px">
                                                               Notes</td>
                                                           <td style="width: 285px">
				                                            <anthem:TextBox runat="Server" id="txt_customer_note" CssClass="input" TextMode="MultiLine" TabIndex="33"></anthem:TextBox></td>
                                                           <td>
                                                               Rumor</td>
                                                           <td>
                                                               <anthem:TextBox ID="txt_customer_rumor" runat="server" CssClass="input" TabIndex="32"></anthem:TextBox></td>
                                                           <td>
                                                               <anthem:CheckBox ID="cb_email_tag" runat="server" Checked="True" TabIndex="31" /></td>
                                                           <td>
                                                           </td>
                                                       </tr>
                                                   <tr>
                                                       <td style="width: 128px">
                                                           </td>
                                                       <td style="width: 285px">
				                                            </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                       <td>
                                                       </td>
                                                   </tr>
		                                            <tr><td colspan="2" align="center">
		                                            <table>
		                                                    <tr>
		                                                            <td style="height: 66px">
		                                                            <asp:Panel runat="Server" ID="panel1" SkinID="btn">
		                                            <anthem:LinkButton CssClass="btn" runat="server" id="btn_save" text="Save" OnClick="btn_save_Click" Width="80px" TabIndex="42" />
		                                            </asp:Panel>
		                                                            </td>
		                                                            <td style="height: 66px">
		                                                            <asp:Panel runat="Server" ID="panel2" SkinID="btn">
		                                            <anthem:LinkButton CssClass="btn" runat="server" id="lb_reset_customer" text="New"  Width="80px" OnClick="lb_reset_customer_Click" TabIndex="43" />
		                                            </asp:Panel>
		                                                            </td>
		                                                            <td style="height: 66px">
		                                                            <asp:Panel runat="Server" ID="panel7" SkinID="btn">
		                                            <anthem:LinkButton CssClass="btn" runat="server" id="lb_new_Order" text="New Order" Width="80px" OnClick="lb_new_Order_Click" TabIndex="44" />
		                                            </asp:Panel>
		                                                            </td>
		                                                            
		                                                    </tr>
		                                            </table>
                                                        <td align="center" colspan="1">
                                                        </td>
                                                        <td align="center" colspan="1">
                                                        </td>
                                                        <td align="center" colspan="1">
                                                        </td>
                                                        <td align="center" colspan="1">
                                                        </td>
                                            		
                                               </table> 
                                     </asp:Panel> 
                            </td>
                        
                        </tr><tr>
                   
                           <td valign="top">
                                 <asp:Panel runat="server" ID="panel5"  SkinID="back_blue" Width="100%">
                                                <asp:panel runat="server" ID="panel6" SkinID="panel_title1" Width="100%">
                                                     &nbsp;&nbsp;&gt;&gt; Customer List</asp:panel> 
                                                    
                                 
                                   <table>
                                        <tr>
                                                <td>
                                                name:<anthem:TextBox ID="txt_customer_search_keyword" runat="server" CssClass="input" BackColor="Yellow"></anthem:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Panel runat="server" ID="panel23" SkinID="btn">
                                                            <anthem:LinkButton runat="server" ID="lb_search_customer" Text="Search" OnClick="lb_search_customer_Click"></anthem:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Panel runat="server" ID="panel8" SkinID="btn">
                                                            <anthem:LinkButton runat="server" ID="lb_clear_search" Text="Clear Search" OnClick="lb_clear_search_Click"></anthem:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                        </tr>
                                   </table>
                                     Count:
                                     <anthem:Label ID="lbl_customer_count" runat="server" Font-Bold="True">0</anthem:Label><br />
                               <anthem:DataGrid ID="dg_customer" Backcolor="White" AlternatingItemStyle-BackColor="#efefef" runat="server" AllowPaging="True"  AutoGenerateColumns="False" PageSize="26" Width="100%" OnItemCommand="dg_customer_ItemCommand" OnPageIndexChanged="dg_customer_PageIndexChanged" OnItemDataBound="dg_customer_ItemDataBound">
                                   <ItemStyle  CssClass="tdItem_5"/>
                                   <HeaderStyle CssClass="trTitle" />
                                   <Columns>
                                       <asp:TemplateColumn>
                                           <itemtemplate>
<anthem:CheckBox id="_cb_customer" CommandName="Checked" runat="server" OnCheckedChanged="_cb_customer_CheckedChanged" AutoCallBack="True"></anthem:CheckBox>
</itemtemplate>
                                       </asp:TemplateColumn>
                                       <asp:BoundColumn DataField="customer_serial_no" HeaderText="ID"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="customer_first_name" HeaderText="Customer Name"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="phone_d" HeaderText="Phone 1"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="phone_n" HeaderText="Phone 2 "></asp:BoundColumn>
                                       <asp:BoundColumn DataField="customer_card_state" HeaderText="state"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="customer_card_zip_code" HeaderText="Zip Code"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="customer_login_name" HeaderText="Login Name"></asp:BoundColumn>
                                       <asp:BoundColumn DataField="customer_password" HeaderText="Password"></asp:BoundColumn>
                                   </Columns>
                                   <PagerStyle Mode="NumericPages" />
                                   <AlternatingItemStyle BackColor="#EFEFEF" />
                               </anthem:DataGrid> </asp:Panel> </td> 
                    </tr>
            </table>
               <anthem:Panel runat="server" ID="panel_order" Visible="false">
                <div style="text-align:center;">
                            <asp:Panel runat="server" ID="panel9" SkinID="btn">
                                 <anthem:LinkButton runat="server" ID="lb_order" Text="Order" OnClick="lb_order_Click"></anthem:LinkButton>
                            </asp:Panel> 
                </div>
   </anthem:Panel>
</asp:Content>

