<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="sale_order_list_timer.aspx.cs" Inherits="Q_Admin_sale_order_list_timer" %>

<%@ Register src="UC/AlertMessage.ascx" tagname="AlertMessage" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    .table_td_white tr td{ background: #f2f2f2;}
    body { background:#f2f2f2;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

每10分钟刷新一次
<table class="table_td_white" cellpadding="2" cellspacing="1">
        <tr>
                <td valign="top">
                    你当前有  <span style="color: Red; font-size: 12pt">0</span>  个订单需要处理<br />
                    有 <span style="color: Red; font-size: 12pt">0</span>  个客户提交的系统要求发布到Ebay
                </td>
                <td>
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right" width="200" style=""><asp:Label ID="Label4" runat="server" Text="未完成订单数量"></asp:Label></td>
                                <td align="center" style="" width="60">
                                    <asp:LinkButton ID="lbl_order_sum" runat="server" Text="0" 
                                        onclick="lbl_order_sum_Click" ></asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td align="right" width="200" style="">订单(Submited)</td>
                                <td align="center" style="">
                                    <asp:LinkButton ID="lbl_order_submited_sum" runat="server" Text="0" 
                                        onclick="lbl_order_submited_sum_Click"></asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td align="right" style=""><asp:Label ID="Label5" runat="server">未配置完整的系统数量</asp:Label></td>
                                <td align="center" style=""><asp:Label ID="lbl_system_sum" runat="server"  Text="0"></asp:Label></td>
                            </tr>
                            <tr>
                                <td align="right" style=""><asp:Label ID="Label6" runat="server">未回复的问题数量</asp:Label></td>
                                <td align="center" style=""><asp:Label ID="lbl_question_sum" runat="server" Text="0"></asp:Label></td>
                            </tr>
                        </table>
                </td>
                <td >
                 
                    
                 
                </td>
        </tr>
</table>
<asp:Timer ID="Timer1" runat="server">
                    </asp:Timer>
</asp:Content>

