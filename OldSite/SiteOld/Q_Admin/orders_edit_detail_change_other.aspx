<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_change_other.aspx.cs" Inherits="Q_Admin_orders_edit_detail_change_other" %>

<%@ Register assembly="Anthem" namespace="Anthem" tagprefix="anthem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

            <p>
                    <hr size="1" style="clear:both">
                    <table>
                            <tr>
                                        <td valign="top">TAX EXAMP</td><td colspan="3"><asp:TextBox runat="server" 
                                            ID="txt_tax_examp" Columns="30" MaxLength="20"></asp:TextBox>
                                        <br />
                                        <anthem:CheckBox ID="CheckBox1" runat="server" Font-Bold="True" 
                                            Text="Check for tax free" Visible="False" />
                                        </td>
                            </tr>
                                                                            
                            <tr>
                                        <td>&nbsp;</td><td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                            </tr>
                                                                            
                            <tr>
                                        <td>SCHEDULED</td><td align="left" colspan="3">
                                                day<asp:DropDownList ID="ddl_pick_up_day" runat="server" >
                                                </asp:DropDownList>
                                                month
                                                <asp:DropDownList ID="ddl_pick_up_month" runat="server">
                                                </asp:DropDownList>
                                                hour
                                                <asp:DropDownList ID="ddl_pick_up_hour" runat="server">
                                                </asp:DropDownList></td>
                            </tr>
                                                                            
                            <tr>
                                        <td>&nbsp;</td><td>
                                                &nbsp;</td>
                                        <td>
                                                &nbsp;</td>
                                        <td>
                                                &nbsp;</td>
                            </tr>
                            <tr>
                                        <td valign="top" colspan="2">PAYMENT<br />
                                            <asp:DropDownList runat="server" ID="ddl_paymethod" size="10" Width="200px"></asp:DropDownList>
                                            <br />
                                        </td><td colspan="2">SHIP METHOD<br />
                            <anthem:DropDownList ID="ddl_ship_method" runat="server" Size="10"
                                                onselectedindexchanged="ddl_ship_method_SelectedIndexChanged" 
                                                Width="200px">
                            </anthem:DropDownList></td>
                            </tr> 
                            <tr>
                                        <td valign="top">&nbsp;</td><td> 
                                        &nbsp;</td>
                                        <td> 
                                            &nbsp;</td>
                                        <td> 
                                            &nbsp;</td>
                            </tr> 
                            <tr>
                                        <td valign="top" colspan="2">WEB STATUS<br />
                            <anthem:DropDownList ID="ddl_pre_status" runat="server" Size="10" Width="200px">
                            </anthem:DropDownList>
                                            <br />
                                        </td><td colspan="2"> 
                                            <br />
                                        </td>
                            </tr> 
                            <tr>
                                        <td valign="top">&nbsp;</td><td> 
                                        &nbsp;</td>
                                        <td> 
                                            &nbsp;</td>
                                        <td> 
                                            &nbsp;</td>
                            </tr> 
                    </table>
                    <hr size="1" style="clear:both">
                    <div style="text-align:center; clear:both;">
                                <asp:Button runat="server" ID="btn_save_other" Text="Save" 
                        OnClientClick="ParentLoadWait();" onclick="btn_save_other_Click" />
                    </div>
            </p>   
</asp:Content>

