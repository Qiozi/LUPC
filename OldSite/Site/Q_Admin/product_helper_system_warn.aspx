<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_system_warn.aspx.cs" Inherits="Q_Admin_product_helper_system_warn" Title="System Warn" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"><uc1:Navigation ID="Navigation1" runat="server" />
  
<hr size="1" />
    <asp:Repeater ID="rpt_system" runat="server">
        <HeaderTemplate>
        
        </HeaderTemplate>
        
        <ItemTemplate>
            <br style='clear:both;' />
            
            <div style="">
            
                <table cellpadding="0" cellspacing="0" style="width:100%;height: 30px;border-top:1px solid #ccc;">
                    <tr style="background:#f2f2f2; line-height:25px;">
                        <td style="width: 60px;">[<%# DataBinder.Eval(Container.DataItem, "system_templete_serial_no")%>]</td>
                        <td style="width: 600px; font-weight: bold;">
                            <input type="hidden" name="sku" value="<%# DataBinder.Eval(Container.DataItem, "system_templete_serial_no")%>" />
                            <input type='text' name='part_name' value="<%# DataBinder.Eval(Container.DataItem, "system_templete_name")%>" size="150" />
                        </td>
                        <td>
                            <input type="button" value="Save" name="save" onclick="onSave($(this));" />
                            <input type="button" value="Close" name="Close"  onclick="onClose($(this));"  />
                        </td>
                    </tr>
                </table>
                         
            </div>
            <div style='min-height: 60px; border: 1px dotted #ff9900;'>
                <%# DataBinder.Eval(Container.DataItem, "sub_item") %>
            </div>  
        </ItemTemplate>
    </asp:Repeater>
    
<script type="text/javascript">
    function onSave(e) {
        var system_sku = '';
        var system_name = '';

        e.parent().parent().find("input").each(function() {
            if ($(this).attr("name") == "sku")
                system_sku = $(this).val();
            if ($(this).attr("name") == "part_name")
                system_name = $(this).val();
                
        });

        $.ajax({
            url: "/q_admin/Sys/Sys_cmd.asp"
            , type: "get"
            , data: { "cmd": "modifySystemName", "system_sku": system_sku, "system_name": system_name }
            , success: function(msg) {
                if (msg.indexOf('OK') != -1)
                    alert("OK");
                else
                    alert(msg);
            }
            , error: function(msg) { alert(msg); }
        });
    }

    function onClose(e) {
        if (confirm('Are you sure!')) {
        
            var system_sku = '';

            e.parent().parent().find("input").each(function() {
                if ($(this).attr("name") == "sku")
                    system_sku = $(this).val();

            });

            $.ajax({
                url: "/q_admin/Sys/Sys_cmd.asp"
            , type: "get"
            , data: { "cmd": "hideSystemBySku", "system_sku": system_sku }
            , success: function(msg) {
                if (msg.indexOf('OK') != -1)
                    window.location.reload();
                else
                    alert(msg);
            }
            , error: function(msg) { alert(msg); }
            });
        } 
    }
</script>
</asp:Content>

