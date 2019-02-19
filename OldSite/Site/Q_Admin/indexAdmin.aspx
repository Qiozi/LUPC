<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="indexAdmin.aspx.cs" Inherits="Q_Admin_indexAdmin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    $(function () {
//        $("#tabs").tabs();

//        $('#paramLaptopArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=350", function () { });

//        $('#paramCPUArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=22", function () { });

//        $('#paramMBArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=31", function () { });

//        $('#paramHDDArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=25", function () { });

//        $('#paramVCArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=41", function () { });

//        $('#paramCaseArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=21", function () { });

//        $('#paramLCDArea').load("product_part_cmd.aspx", "cmd=gettopsalelist&cateid=28", function () { });
    });
</script>
    <asp:Repeater runat="server" id="rptSysList">
        <HeaderTemplate>
        <h2>System</h2>
        </HeaderTemplate>
        <ItemTemplate>
            <div style="padding:0.5em;">
                <asp:HiddenField runat="server" ID="hfid" Value='<%#Eval("id") %>' />
                <label>Title</label><asp:TextBox runat="server" ID="txtTitle" Text='<%# Eval("title") %>' />
                <label>System SKU</label><asp:TextBox runat="server" ID="txtSku" Text='<%# Eval("sku") %>' />
                <label>LCD Image Filename</label><asp:TextBox runat="server" ID="txtLCDImgFilename" Text='<%# Eval("LCDImage") %>' />
                Img X<asp:TextBox runat="server" ID="txtImgX" Text='<%# Eval("lcd_p_X") %>'  />
                Img Y<asp:TextBox runat="server" ID="txtImgY" Text='<%# Eval("lcd_p_Y") %>' />
                Case X<asp:TextBox runat="server" ID="txtCaseX" Text='<%# Eval("case_p_X") %>' />
                Case Y<asp:TextBox runat="server" ID="txtCaseY" Text='<%# Eval("case_p_Y") %>' />
            </div>
            
        </ItemTemplate>
        <FooterTemplate>
            
        </FooterTemplate>
    </asp:Repeater>
    <div style="text-align:center;">
        <asp:Button runat="server" ID="btnSave" Text="Save" onclick="btnSave_Click" />
        <asp:Label runat="server" ID="lblSysNote" Text=""></asp:Label>
    </div>
    <hr size=1 />
    <h2>Part</h2>
    <table>
        <tr>
            <td><iframe src="indexAdminLeftManu.asp" style="width:250px; height:500px; border:1px solid #ccc;"></iframe></td>
            <td><iframe src="indexAdminPartEdit.aspx?cid=350" id="iframeCenter" name="iframeCenter" style="width:980px; height:500px; border:1px solid #ccc;"></iframe></td>
        </tr>
    </table>
   
</asp:Content>

