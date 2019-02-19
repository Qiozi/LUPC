<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_list_timer.aspx.cs" Inherits="Q_Admin_orders_list_timer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div><i>每隔10分钟刷新一次</i></div>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                        <table>
                                <tr>
                                        <td style="text-align:center"><asp:Label runat="server" ID="lbl_assinged_to_sum" Font-Bold="true" ForeColor="Red" Text="0"></asp:Label></td>
                                </tr>
                                <tr>
                                        <td>您需要处理的订单数量<asp:Timer ID="Timer1" runat="server" Interval="600000" 
                                                ontick="Timer1_Tick">
                                            </asp:Timer>
                                        </td>
                                </tr>
                        </table>
                </ContentTemplate>
    </asp:UpdatePanel>
       
</asp:Content>

