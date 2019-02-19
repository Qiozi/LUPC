<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="indexAdminPartEdit.aspx.cs" Inherits="Q_Admin_indexAdminPartEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
.listtable
{
    border-collapse: collapse;
    border-spacing: 0;
    margin-right: auto;
    margin-left: auto;
    width: 800px;
 }
.listtable tr td
 {
    border: 1px solid #b5d6e6;
    font-size: 12px;
    font-weight: normal;
    text-align: center;
    vertical-align: middle;
    height: 20px;
 }
</style>
<script type="text/javascript">
    $(document).ready(function () {
        $('#paramLaptopArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=<%= Request["cid"].ToString() %>", function () {
        
            $('input').each(function(){
                var the = $(this);
                if(the.attr("name").indexOf('txtSKU')>-1){
                    if(the.val()!="")
                    {
                        var v = the.val();
                        $('td[name=sku]').each(function(){
                            if($(this).html() == v){
                                $(this).css({"color":"blue"});
                            }
                        });
                    }
                }
            })
         });
    });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <p>
                <table>
                    <tr>
                        <td valign="top">
                            <asp:Repeater runat="server" ID="rptLaptop">
                                <ItemTemplate>
                                    <asp:HiddenField runat="server" ID="_hfid" Value='<%# Eval("id") %>' />
                                    SKU
                                    <div><asp:TextBox runat="server" ID='_txtSKU' Text='<%#Eval("sku") %>'></asp:TextBox></div>
                                    title
                                    <div><asp:TextBox MaxLength="100" Columns="100" runat="server" ID='_txtTitle' Text='<%# Eval("title") %>'></asp:TextBox></div>
                                </ItemTemplate>
                            </asp:Repeater>
                            <asp:Button runat="server" ID="btnLaptop" Text="Save" 
                    onclick="btnLaptop_Click" />
                        </td>
                        <td valign="top">
                            <div id='paramLaptopArea'><img src="../soft_img/tags/loaderc.gif" /></div>
                        </td>
                    </tr>
                </table>
                
                
                    <asp:Label runat=server ID="lblLaptopNote"></asp:Label>
            </p>
</asp:Content>

