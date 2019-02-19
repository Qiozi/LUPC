<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="upload_part_info.aspx.cs" Inherits="Q_Admin_inc_upload_part_info" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
.part_group_area{float:left; width: 250px; font-size:7pt;}

.part_group_area span:hover{ background:#f2f2f2; color: #ff6600; cursor:pointer}

.long_name{ color:#cccccc; font-size:7pt;}
</style>
<script type="text/javascript">
function loadStyle()
{

}

function pressGroupName(e)
{
    var inp  = $(e).parent().find('input');
    inp.attr('checked', !inp.attr('checked'));
}

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="height:30px;">
&nbsp;
</div>
    <asp:RadioButtonList runat="server" ID="radio_cmd" RepeatColumns="2" 
        RepeatLayout="Flow" >
        <asp:ListItem Text="Show part that it isn't exist group" Selected="True" Value="1"></asp:ListItem>
        <asp:ListItem Text="Show New Part" Value="2"></asp:ListItem>
    </asp:RadioButtonList>
    <asp:FileUpload ID="FileUpload_edit_part" runat="server" />
            <asp:Button ID="btn_upload" runat="server" Text="Part Upload" onclick="btn_upload_Click" />
            <i style="color:#ccc">The new products is certificated by the MFP#.</i>
            <br />
            <asp:Literal
                ID="literal_part_list" runat="server"></asp:Literal>
<hr size="1" />
<div id="btn_cmd_list" style="width: 100%;line-height:30px;background: #f2f2f2;  border-top:1px solid #cccccc;border-bottom:1px solid #cccccc;text-align:center;">
    <input type="button" value="Save" onclick="save_part_group();" />
</div>
<asp:Literal ID="Literal1" runat="server"></asp:Literal>

<div id='view_status' style="display:none; min-width:200px;padding: 2px ; border:1px solid #cccccc; background:#f2f2f2; text-align:center; vertical-align:top;">
    <div style="text-align:right; cursor:pointer;border-bottom: 1px dotted #666666;"><a onclick="$('#view_status').css('display', 'none');">close</a></div>
    <div id='save_result' style="line-height:25px;"></div>
    <div id='saved_quantity' style="line-height:25px;"></div>
</div>
 <script type="text/javascript">
    $().ready(function(){
        $("#btn_cmd_list").floatdiv("lefttop");
        $('#view_status').floatdiv("middle");
    });
    
    function save_part_group()
    {
        var quantity = 0;
        $("input[type=checkbox][checked]").each(
            function(){quantity+=1;}
         );
        
        $('#view_status').css("display", "");
        partEach(0, quantity);
        
    }
    
    function partEach(n, quantity)
    {
        $("input[type=checkbox][checked]").each(
            function(i){
                if (n == i)
                    save_part_group_value($(this).attr('tag'), $(this).val(),i+1, quantity)
            }
        );
    }
    
    
    
    function save_part_group_value(sku, group_id, post, quantity)
    {
         $("#saved_quantity").html(post + ' of ' +quantity);
        $('#save_result').html('/q_admin/inc/save_part_group_value.aspx?sku='+ sku +'&part_group_id='+ group_id);
        $('#save_result').load('/q_admin/inc/save_part_group_value.aspx?sku='+ sku +'&part_group_id='+ group_id
        , function(){
            partEach(post, quantity);
            //$("#saved_quantity").html(post + ' of ' +quantity);
        }
        );
        
    }
    </script>
</asp:Content>

