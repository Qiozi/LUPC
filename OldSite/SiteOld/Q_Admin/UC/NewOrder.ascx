<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewOrder.ascx.cs" Inherits="Q_Admin_UC_NewOrder" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
                <div style="height: 60px; line-height: 60px; text-align:center;">
	               <asp:Panel runat="server" ID="panel1"  SkinID="back_white" Width="50%">
	                <asp:panel runat="server" ID="panel2" SkinID="panel_title1" Width="100%">
                                                         Order</asp:panel> 
                                                         <asp:Panel runat="server" ID="pc12" SkinID="panel_cantion">
                                                      <table style="width:100%; text-align:center; " >
                                                            <tr>
                                                                <td>
                                                                    Order Number : <anthem:TextBox ID="txt_order_code" runat="server" CssClass="input"></anthem:TextBox> 
                                                                </td>
                                                                <td>
                                                                        <asp:Panel runat="server" ID="panel13" SkinID="btn">
                                                                                <anthem:LinkButton runat="server" ID="lb_Search" Text="Search" OnClick="lb_Search_Click" ></anthem:LinkButton>
                                                                        </asp:Panel>
                                                                </td>
                                                                <td>
                                                                        <asp:Panel runat="server" ID="panel3" SkinID="btn">
                                                                                <anthem:LinkButton runat="server" ID="lb_new_order" Text="New Order" OnClick="lb_new_order_Click"></anthem:LinkButton>
                                                                        </asp:Panel>
                                                                </td>
                                                            </tr>
                                                      </table> 
                                                         </asp:Panel>
                                                         </asp:Panel>               
                </div>

                                                     
                                                     