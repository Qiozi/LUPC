<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" ValidateRequest="false" AutoEventWireup="true" CodeFile="product_helper_match_sku.aspx.cs" Inherits="Q_Admin_product_helper_match_sku" Title="Untitled Page" %>

<%@ Register src="UC/MenuChildName.ascx" tagname="MenuChildName" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MenuChildName ID="MenuChildName1" runat="server" />
    <hr size="1" />
    
    <div style="text-align:center">
        <div id="IconDiagram_Layer"  style="position:absolute; left: 0px; top: 0px; background-color: #5D7D9E; border: 1px solid #cccccc;width: 100%; height:30px;font-size:9pt; color:#ffffff">
	            <a name="top"></a><asp:Button runat="server" ID="btn_save" Text="Save" onclick="btn_save_Click" onclientclick="ParentLoadWait();"/>
        </div>
    </div>
    <hr size="1" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
        
    <asp:ListView ID="ListView_part" runat="server" 
        ItemContainerID="itemPlaceholder" 
        onitemdatabound="ListView_part_ItemDataBound" 
        onitemcommand="ListView_part_ItemCommand">
        <LayoutTemplate><div id="itemPlaceholder" runat="server"/></LayoutTemplate>
        <ItemTemplate>
        
            <table style="background: #f2f2f2" cellpadding="0" cellspacing="0">
                <tr style="background: #cccccc">
                    <td style="width:82px; text-align:center"><asp:Label runat="server" ID="_lbl_lu_sku" Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Label></td>
                    <td style="width:120px"><asp:TextBox runat="server" ID="_txt_lu_manufacture" Text='<%# DataBinder.Eval(Container.DataItem, "manufacturer_part_number") %>' Columns="30"></asp:TextBox></td>
                    <td style="text-align:left">&nbsp;<asp:Label runat="server" ID="_lbl_lu_part_name" Text='<%# DataBinder.Eval(Container.DataItem , "product_name") %>'></asp:Label></td>
                    <td><a href="#top" style="color:#666666">Top</a></td>
                </tr>
                <tr>
                    <td colspan="3">    
                    <table cellpadding="0" cellspacing="0">
                        <tr>
                            <td>supercom <asp:HiddenField runat="server" ID="_hf_supercom_id" />  </td>
                            <td><asp:TextBox runat="server" ID="_txt_supercom_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_supercom_name" Columns="60"></asp:TextBox></td>
                            <td><asp:ImageButton runat="server" ID="imageBtn_match" CommandName="supercom" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_supercom_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                         </tr>
                        <tr>
                            <td>asi <asp:HiddenField runat="server" ID="_hf_asi_id" /> </td>
                            <td><asp:TextBox runat="server" ID="_txt_asi_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_asi_name" Columns="60"></asp:TextBox></td>
                            <td><asp:ImageButton runat="server" ID="ImageButton1" CommandName="asi" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_asi_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>EPROM<asp:HiddenField runat="server" ID="_hf_eprom_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_eprom_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_eprom_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton2" CommandName="eprom" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_eprom_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                         </tr>
                        <tr>
                            <td>DAIWA<asp:HiddenField runat="server" ID="_hf_daiwa_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_daiwa_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_daiwa_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton3" CommandName="daiwa" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_txt_daiwa_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>MUTUAL<asp:HiddenField runat="server" ID="_hf_mutual_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_mutual_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_mutual_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton4" CommandName="mutual" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif"  /></td>
                            <td><asp:Label runat="server" id="_lbl_mutual_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                         </tr>
                        <tr>
                            <td>MMAX<asp:HiddenField runat="server" ID="_hf_mmax_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_mmax_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_mmax_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton5" CommandName="mmax" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_mmax_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>COMTRONIX<asp:HiddenField runat="server" ID="_hf_comtronix_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_comtronix_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_comtronix_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton6" CommandName="comtronix" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_comtronix_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                         </tr>
                        <tr>
                            <td>SINOTECH<asp:HiddenField runat="server" ID="_hf_sinotech_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_sinotech_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_sinotech_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton7" CommandName="sinotech" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_sinotech_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                            
                        </tr>
                        <tr>
                            <td>MINIMICRO<asp:HiddenField runat="server" ID="_hf_minimicro_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_minimicro_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_minimicro_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton8" CommandName="minimicro" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_minimicro_last_datetime" ForeColor="#FF6600"></asp:Label></td>   
                        </tr>
                        <tr>                      
                            <td>ALC<asp:HiddenField runat="server" ID="_hf_alc_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_alc_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_alc_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton9" CommandName="alc" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_alc_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                            
                        </tr>
                        <tr>
                            <td>SAMTACH<asp:HiddenField runat="server" ID="_hf_samtach_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_samtach_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_samtach_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton10" CommandName="samtach" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_samtach_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                         </tr>
                         <tr>
                            <td>OCZ<asp:HiddenField runat="server" ID="_hf_ocz_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_ocz_sku" Columns="30"> </asp:TextBox></td>
                            <td><asp:TextBox runat="server" ID="_txt_ocz_name" Columns="60"></asp:TextBox></td>
                             <td><asp:ImageButton runat="server" ID="ImageButton11" CommandName="OCZ" onclientclick="ParentLoadWait();" ImageUrl="~/images/down.gif" /></td>
                            <td><asp:Label runat="server" id="_lbl_ocz_last_datetime" ForeColor="#FF6600"></asp:Label></td>
                         </tr>
                        
                    </table>

    </td>
                </tr>
            </table>
        </ItemTemplate>
    </asp:ListView>
 </ContentTemplate>
    </asp:UpdatePanel>
<script type="text/javascript">
document.onload = __OnLoad_float(); 
</script>
</asp:Content>

