<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sale_stat_month_report_2.aspx.cs" Inherits="Q_Admin_sale_stat_month_report_2" Title="Stat Month Report" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Repeater runat="server" ID="repeater_part_list" 
        onitemdatabound="repeater_part_list_ItemDataBound">
        <HeaderTemplate>
        <table>
            <tr>
                <td style="background:#000">
                    <table border="0" class="table_report" width="700" cellspacing="1">
                        <thead>
                            <tr style="background: #999">
                                <td>Year</td>
                                <td>GST</td>
                                <td>PST</td>
                                <td>PST</td>
                                <td>taxable</td>
                                <td>Grand Total</td>
                            </tr>
                        </thead>
        </HeaderTemplate>
        <ItemTemplate>
            <tr style="background: #cccccc">
                <td style="cursor: pointer" onclick="StatShowYearReport('table_stat_year_<%# DataBinder.Eval(Container.DataItem, "order_year")%>');" class="tdChangeBgColor">
                    <asp:Literal runat="server" ID="_literal_year" Text='<%# DataBinder.Eval(Container.DataItem, "order_year")%>'></asp:Literal>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "gst", "{0:###,###.00}") %>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "pst", "{0:###,###.00}")%>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "hst", "{0:###,###.00}")%>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "taxable_total", "{0:###,###.00}")%>
                </td>
                <td>
                    <%# DataBinder.Eval(Container.DataItem, "grand_total", "{0:###,###.00}")%>
                </td>
            </tr>
            <tr style="display:none; background:#ccc" id="table_stat_year_<%# DataBinder.Eval(Container.DataItem, "order_year")%>">
                <td></td>
                <td colspan="5">
                    <asp:Literal runat="server" ID="_literal_month_string"></asp:Literal>
                </td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
                    </table>
                </td>
            </tr>
        </table>
        </FooterTemplate>
    </asp:Repeater>
    <iframe id="iframe2" name="iframe2" src="" style="width:0px; height: 0px;background: green" frameborder="0"></iframe>
  
    <iframe id="iframe1" name="iframe1" src="" style="width:0px; height: 0px;background: green" frameborder="0"></iframe>
</asp:Content>

