<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_customer_list.aspx.cs" Inherits="Q_Admin_orders_customer_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div style="text-align:center">
    <table align="center">
            <tr>
                    <td>Customer#</td>
                    <td><asp:TextBox ID="txt_customer" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_cutomerID"   Text="Search" 
                             onclientclick="ParentLoadWait();" onclick="btn_cutomerID_Click"/></td> 
                    <td>email</td>
                    <td> <asp:TextBox ID="txt_email" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_email"   Text="Search" 
                            onclick="btn_search_email_Click" onclientclick="ParentLoadWait();"/></td>
            </tr>
            <tr>
                    <td>first name</td>
                    <td> <asp:TextBox ID="txt_first_name" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_first_name"   Text="Search" 
                            onclick="btn_search_first_name_Click" onclientclick="ParentLoadWait();"/></td> 
                    <td>phone</td>
                    <td> <asp:TextBox ID="txt_phone" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_phone"  Text="Search" 
                            onclick="btn_search_phone_Click" onclientclick="ParentLoadWait();" /></td>
            </tr>
            <tr id="h">
                    <td>last name</td>
                    <td> <asp:TextBox ID="txt_last_name" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_last_name" Text="Search" 
                            onclick="btn_search_last_name_Click" onclientclick="ParentLoadWait();" /></td>
                    <td>login name</td>
                    <td> <asp:TextBox ID="txt_login_name" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_login_name" Text="Search" 
                            onclick="btn_search_login_name_Click" onclientclick="ParentLoadWait();" /></td>
            </tr>
            <tr>
                    <td>shipping first name</td>
                    <td> <asp:TextBox ID="txt_shipping_first_name" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_shipping_first_name"   Text="Search" 
                            onclick="btn_search_shipping_first_name_Click" 
                            onclientclick="ParentLoadWait();"/></td>
                            <td>&nbsp;</td>
                    <td> &nbsp;</td>
                    <td>&nbsp;</td>
            </tr>
            <tr>
                    <td>shipping last name</td>
                    <td> <asp:TextBox ID="txt_shipping_last_name" runat="server"></asp:TextBox></td>
                    <td><asp:Button runat="server" ID="btn_search_shipping_last_name"   Text="Search" 
                            onclick="btn_search_shipping_last_name_Click" 
                            onclientclick="ParentLoadWait();"/></td>
                    <td></td><td align="right" colspan="2"><asp:Button ID="btn_clear_search" 
                        runat="server" Text="Clear Search" 
        onclick="btn_clear_search_Click" onclientclick="ParentLoadWait();" /></td>
            </tr>

    </table>
</div>
<hr size="1" />
                                        
                                        
                               <asp:GridView ID="gv_customer" Backcolor="White" AlternatingItemStyle-BackColor="#efefef" 
                                                    runat="server" 
        AllowPaging="True"  AutoGenerateColumns="False" PageSize="26" 
                                                    Width="100%" OnItemCommand="dg_customer_ItemCommand" 
                                                    
       
        onrowdatabound="gv_customer_RowDataBound" 
        onrowcommand="gv_customer_RowCommand">
                                   <RowStyle  CssClass="tdItem_5"/>
                                   <HeaderStyle CssClass="trTitle" />
                    
                                   <Columns>
                                    
                                       <asp:TemplateField>
                                           <itemtemplate>
<asp:CheckBox id="_cb_customer" CommandName="Checked" runat="server" 
                                                ></asp:CheckBox>
</itemtemplate>
                                       </asp:TemplateField>
                                       <asp:BoundField DataField="customer_serial_no" HeaderText="Customer ID"></asp:BoundField>
                                       <asp:BoundField DataField="customer_first_name" HeaderText="Customer Name"></asp:BoundField>
                                       <asp:BoundField DataField="phone_d" HeaderText="Phone 1"></asp:BoundField>
                                       <asp:BoundField DataField="phone_n" HeaderText="Phone 2 "></asp:BoundField>
                                       <asp:BoundField DataField="customer_card_state" HeaderText="state"></asp:BoundField>
                                       <asp:BoundField DataField="customer_card_zip_code" HeaderText="Zip Code"></asp:BoundField>
                                       <asp:BoundField DataField="customer_login_name" HeaderText="Login Name"></asp:BoundField>
                                       <asp:BoundField DataField="customer_password" HeaderText="Password"></asp:BoundField>
                                       <asp:TemplateField HeaderText="Order Count">
                                                <ItemTemplate>
                                                        <asp:Literal runat="server" ID="_literal_order_code"></asp:Literal>     
                                                                                               
                                                </ItemTemplate>
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Cmd">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="_lb_new_order" CommandName="NewOrder" OnClientClick='return confirm("Are you sure create Order?");' CommandArgument='<%# DataBinder.Eval(Container.DataItem,"customer_serial_no")  %>' Text="New Order"></asp:LinkButton>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                   </Columns>
                                   
                               </asp:GridView> 
                               </asp:Content>

