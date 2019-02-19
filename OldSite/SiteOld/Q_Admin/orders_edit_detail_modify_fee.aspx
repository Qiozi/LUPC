<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_modify_fee.aspx.cs" Inherits="Q_Admin_orders_edit_detail_modify_fee" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <p >
           <hr size="1" style="clear:both">

        <table>
                <tr>
                            <td align="right">Special Discount</td><td>
                            <asp:TextBox runat="server" 
                                ID="txt_special_discount" Columns="30" MaxLength="30"></asp:TextBox></td>
                            <td><anthem:CheckBox ID="CheckBox_lock_input_discount" runat="server" 
                                    Text="Lock Input" /></td>
                            <td>&nbsp;</td>
                </tr>
                <tr>
                            <td align="right">Ship Charge</td><td>
                            <asp:TextBox runat="server" ID="txt_ship_charge" 
                                Columns="30" MaxLength="30"></asp:TextBox></td>
                            <td><anthem:CheckBox ID="CheckBox_lock_input_ship_charge" runat="server" 
                                    Text="Lock Input" /></td>
                            
                            <td>&nbsp;</td>
                            
                </tr>
                <tr>
                            <td align="right">WEEE</td><td>
                            <asp:TextBox runat="server" ID="txt_weee" 
                                Columns="30" MaxLength="20"></asp:TextBox></td>
                           
                            <td>&nbsp;</td>
                           
                            <td>&nbsp;</td>
                           
                </tr>
                <tr>
                            <td align="right" valign="top">Price Unit</td><td>
                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                                RepeatLayout="Flow">
                                <Items>
                                    <asp:ListItem>CAD</asp:ListItem>
                                    <asp:ListItem>USD</asp:ListItem>
                                </Items>
                            </asp:RadioButtonList>
                            </td>
                            
                            <td>
                                &nbsp;</td>
                            
                            <td>
                                &nbsp;</td>
                            
                </tr>
                <tr>
                            <td align="right" valign="top">Ship Method</td><td> 
                            <asp:DropDownList ID="ddl_ship_method" runat="server" Size="10">
                            </asp:DropDownList></td>
                            <td valign="top"> Tax Rate<br />
                                <asp:CheckBox ID="CheckBox_lock_tax_change" runat="server" 
                                   
                                    Text="Lock, tax no auto change" />
                            </td>
                            <td valign="top">  
                            <asp:DropDownList ID="ddl_tax" runat="server" Size="10">
                            </asp:DropDownList></td>
                </tr>
                
        </table>
        <hr size="1" style="clear:both">
        <div style="text-align:center; clear:both;">
                    <asp:Button runat="server" ID="bt_save" Text="Save" 
            OnClientClick="ParentLoadWait();" onclick="bt_save_credit_card_Click" />
        </div>
        
</p> 
</asp:Content>

