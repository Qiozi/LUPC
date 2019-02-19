<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_match_sku_2.aspx.cs" ValidateRequest="false" Inherits="Q_Admin_product_helper_match_sku_2" %>
<%@ Register src="UC/MenuChildName.ascx" tagname="MenuChildName" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
    td { font-size: 8.5pt}
</style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<br />
<br />
<br />
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
                            <td>ETC<asp:HiddenField runat="server" ID="_hf_etc_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_etc_sku" Columns="30"></asp:TextBox></td><td></td>
                         </tr>
                        <tr>
                            <td>asi <asp:HiddenField runat="server" ID="_hf_asi_id" /> </td>
                            <td><asp:TextBox runat="server" ID="_txt_asi_sku" Columns="30"></asp:TextBox></td>
                            <td>Ncix<asp:HiddenField runat="server" ID="_hf_ncix_id" /></td><td><asp:TextBox runat="server" ID="_txt_ncix_sku" Columns="30"></asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>EPROM<asp:HiddenField runat="server" ID="_hf_eprom_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_eprom_sku" Columns="30"></asp:TextBox></td>
                            <td>NewEgg<asp:HiddenField runat="server" ID="_hf_newegg_id" /></td><td><asp:TextBox runat="server" ID="_txt_newegg_sku" Columns="30"></asp:TextBox></td><td></td>
                         </tr>
                        <tr>
                            <td>DAIWA<asp:HiddenField runat="server" ID="_hf_daiwa_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_daiwa_sku" Columns="30"></asp:TextBox></td><td>PcvOnline<asp:HiddenField runat="server" ID="_hf_pcvonline_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_pcvonline_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>MUTUAL<asp:HiddenField runat="server" ID="_hf_mutual_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_mutual_sku" Columns="30"> </asp:TextBox></td>
                            <td>DirectDial<asp:HiddenField runat="server" ID="_hf_directdial_id" /></td><td><asp:TextBox runat="server" ID="_txt_directdial_sku" Columns="30"></asp:TextBox></td><td></td>
                         </tr>
                        <tr>
                            <td>MMAX<asp:HiddenField runat="server" ID="_hf_mmax_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_mmax_sku" Columns="30"> </asp:TextBox></td>
                            <td>TigerDirect<asp:HiddenField runat="server" ID="_hf_tigerdirect_id" /></td><td><asp:TextBox runat="server" ID="_txt_tigerdirect_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>COMTRONIX<asp:HiddenField runat="server" ID="_hf_comtronix_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_comtronix_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>
                         </tr>
                        <tr>
                            <td>SINOTECH<asp:HiddenField runat="server" ID="_hf_sinotech_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_sinotech_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>
                            
                        </tr>
                        <tr>
                            <td>MINIMICRO<asp:HiddenField runat="server" ID="_hf_minimicro_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_minimicro_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>   
                        </tr>
                        <tr>                      
                            <td>ALC<asp:HiddenField runat="server" ID="_hf_alc_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_alc_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>
                            
                        </tr>
                        <tr>
                            <td>SAMTACH<asp:HiddenField runat="server" ID="_hf_samtach_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_samtach_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>
                         </tr>
                         <tr>
                            <td>OCZ<asp:HiddenField runat="server" ID="_hf_ocz_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_ocz_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>
                         </tr>
                      
                         <tr>
                            <td>CanadaComputer<asp:HiddenField runat="server" ID="_hf_CanadaComputer_id" /></td>
                            <td><asp:TextBox runat="server" ID="_txt_CanadaComputer_sku" Columns="30"> </asp:TextBox></td>
                            <td></td>
                             <td></td>
                            <td></td>
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

