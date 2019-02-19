<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="change_currency_converter.aspx.cs" Inherits="Q_Admin_change_currency_converter" Title="Change Currency Converter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="line-height:30px;text-align:center; padding-top: 5px;">
        <asp:TextBox runat="server" ID="txt_currency"></asp:TextBox>
        <asp:Button runat="server" ID="btn_submit" Text="submit" 
            onclick="btn_submit_Click" />    
    </div>

    <hr size="1" />
        <center>            
            <asp:Label runat="server" ID="lbl_currency_converter"></asp:Label>
        </center>
    <hr size="1" />
    <asp:Repeater runat="server" ID="rpt_currency_converter" 
        onitemcommand="rpt_currency_converter_ItemCommand">
            <HeaderTemplate>
                <div style="width:100%; background:#cccccc;">
                    <table style="width:100%" cellspacing="1" id="currency_converter">
                        <tr>
                                <th>ID</th>
                                <th>Canadian Dollar</th>
                                <th>U.S. Dollar</th>
                                <th></th>  
                                <th></th> 
                                <th></th>   
                        </tr>
            </HeaderTemplate>
            <ItemTemplate>
                        <tr>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "id") %>
                                </td>
                                <td style="text-align:right">
                                    <%# DataBinder.Eval(Container.DataItem, "currency_cad") %>
                                </td>
                                <td style="text-align:right">
                                    <%# DataBinder.Eval(Container.DataItem, "currency_usd") %>
                                </td>
                                <td style="text-align:right">
                                    <%# DataBinder.Eval(Container.DataItem, "regdate") %>
                                </td>
                                <td>
                                    <%# DataBinder.Eval(Container.DataItem, "is_auto") %>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="btn_set" Text="Set" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' CommandName="Set" />
                                </td>
                        </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
                </div>
            </FooterTemplate>
    </asp:Repeater>
    
<asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <script type="text/javascript">
    $().ready(function(){
        $('#currency_converter th').css("background", "#f2f2f2");
        $('#currency_converter td').css("background", "#ffffff");
    });
</script>
</asp:Content>

