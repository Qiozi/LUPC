<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_add_customer_search.aspx.cs" Inherits="Q_Admin_orders_add_customer_search" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table style="width: 100%;">
                  <tr>
                   
                           <td valign="top">
                                 <asp:Panel runat="server" ID="panel5"  SkinID="back_blue" Width="100%">
                                                <asp:panel runat="server" ID="panel6" SkinID="panel_title1" Width="100%">
                                                     &nbsp;&nbsp;&gt;&gt; Customer List</asp:panel> 
                                                    
                                 
                                   <table>
                                        <tr>
                                                <td>
                                                name:<asp:TextBox ID="txt_customer_search_keyword" runat="server" CssClass="input" 
                                                        BackColor="Yellow" TabIndex="60"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Panel runat="server" ID="panel23" SkinID="btn">
                                                            <asp:LinkButton runat="server" ID="lb_search_customer" Text="Search" 
                                                                OnClick="lb_search_customer_Click" TabIndex="61"></asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                                <td>
                                                    <asp:Panel runat="server" ID="panel8" SkinID="btn">
                                                            <asp:LinkButton runat="server" ID="lb_clear_search" Text="Clear Search" 
                                                                OnClick="lb_clear_search_Click" TabIndex="62"></asp:LinkButton>
                                                    </asp:Panel>
                                                </td>
                                        </tr>
                                   </table>
                                     Count:
                                     <asp:Label ID="lbl_customer_count" runat="server" Font-Bold="True">0</asp:Label><br />
                             
                                        
                               <asp:GridView ID="dg_customer" Backcolor="White" AlternatingItemStyle-BackColor="#efefef" 
                                                    runat="server" AllowPaging="True"  AutoGenerateColumns="False" PageSize="200" 
                                                    Width="100%" OnItemCommand="dg_customer_ItemCommand" 
                                                    OnItemDataBound="dg_customer_ItemDataBound" EnableModelValidation="True" 
                                                    onselectedindexchanged="dg_customer_SelectedIndexChanged">
                                   <RowStyle  CssClass="tdItem_5"/>
                                   <HeaderStyle CssClass="trTitle" />
                    
                                   <Columns>
                                    
                                       <asp:TemplateField>
                                           <itemtemplate>

</itemtemplate>
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="customer_serial_no" HeaderText="ID"></asp:BoundField>
                                       <asp:BoundField DataField="customer_first_name" HeaderText="Customer Name"></asp:BoundField>
                                       <asp:BoundField DataField="phone_d" HeaderText="Phone 1"></asp:BoundField>
                                       <asp:BoundField DataField="phone_n" HeaderText="Phone 2 "></asp:BoundField>
                                       <asp:BoundField DataField="state_code" HeaderText="state"></asp:BoundField>
                                       <asp:BoundField DataField="customer_card_zip_code" HeaderText="Zip Code"></asp:BoundField>
                                       <asp:BoundField DataField="customer_login_name" HeaderText="Login Name"></asp:BoundField>
                                       <asp:BoundField DataField="customer_password" HeaderText="Password"></asp:BoundField>
                                       <asp:CommandField ShowSelectButton="True" >
                                       <ControlStyle ForeColor="#0000CC" />
                                       </asp:CommandField>
                                   </Columns>
                                   
                               </asp:GridView> 
                            
                               </asp:Panel> </td> 
                    </tr>
            </table>
</asp:Content>

