<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchStockStatus.ascx.cs" Inherits="Q_Admin_UC_SearchStockStatus" %>
   
        <table class="bottom_search" cellpadding="0" cellspacing="1" width="1250">
            <tr>
                <td width="150" bgcolor="#E8E8FA"><asp:TextBox ID="txt_lu_sku" runat="server" Columns="10"></asp:TextBox>
                    <asp:Button ID="btn_go"
            runat="server" Text="GO" onclick="btn_go_Click" /></td>
                <td width="200">
                    SKU:<asp:Label runat="server" ID="lbl_part_stock"></asp:Label>
                </td>
                <td>
                    <table cellspacing="1">
                        <tr>
                            <td bgcolor="#f2f2f2">LU:</td>
                            <td bgcolor="#F2F2F2" align="center">&nbsp;<asp:Label runat="server" ID="lbl_store_lu" 
                                    Text="&amp;nbsp;" EnableViewState="False"></asp:Label></td>
                            <td bgcolor="#D1D1EC">Supercom:</td>
                            <td bgcolor="#D1D1EC" align="center">
                                <asp:Label runat="server" ID="lbl_store_supercom">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#f2f2f2">asi:</td>
                            <td align="center"  bgcolor="#f2f2f2">
                                <asp:Label runat="server" ID="lbl_store_asi">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#D1D1EC">EPROM:</td>
                            <td align="center" bgcolor="#D1D1EC">
                                <asp:Label runat="server" ID="lbl_store_eprom" EnableViewState="False">&nbsp;</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="#f2f2f2"><asp:Label runat="server" 
                                    ID="lbl_last_regdate_lu" EnableViewState="False" Width="150px" 
                                    Text="&amp;nbsp;"></asp:Label></td>
                            <td colspan="2" bgcolor="#D1D1EC">
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_supercom" EnableViewState="False" Width="150px" 
                                    Text="&amp;nbsp;"></asp:Label></td>
                            <td colspan="2" bgcolor="#f2f2f2">
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_asi" EnableViewState="False" Width="150px" 
                                    Text="&amp;nbsp;"></asp:Label></td>
                            <td colspan="2" bgcolor="#D1D1EC">
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_eprom" EnableViewState="False" Width="150px" 
                                    Text="&amp;nbsp;"></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <table cellspacing="1" cellpadding="0" bgcolor="#F2F2F2" width="100%">
                        <tr>                    
                            <td bgcolor="#f2f2f2">DAIWA:</td>
                            <td align="center">
                                <asp:Label runat="server" ID="lbl_store_daiwa" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#D1D1EC">MUTUAL:</td>
                            <td align="center" bgcolor="#D1D1EC">
                                <asp:Label runat="server" ID="lbl_store_mutual" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#f2f2f2">MMAX:</td>
                            <td align="center">
                                <asp:Label runat="server" ID="lbl_store_mmax" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#D1D1EC">COMTRONIX:</td>
                            <td align="center" bgcolor="#D1D1EC">
                                &nbsp;<asp:Label runat="server" ID="lbl_store_comtronix" 
                                    EnableViewState="False">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#f2f2f2">SINOTECH:</td>
                            <td align="center">
                                &nbsp;<asp:Label runat="server" ID="lbl_store_sinotech" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#D1D1EC">MINIMICRO:</td>
                            <td align="center" bgcolor="#D1D1EC">
                                <asp:Label runat="server" ID="lbl_store_minimicro" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#f2f2f2">ALC:</td>
                            <td align="center">
                                <asp:Label runat="server" ID="lbl_store_alc" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                            <td bgcolor="#D1D1EC">SAMTACH:</td>
                            <td align="center" bgcolor="#D1D1EC">
                                <asp:Label runat="server" ID="lbl_store_samtach" EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="#f2f2f2" width="150">
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_daiwa" EnableViewState="False" 
                                    width="150">&nbsp;</asp:Label>
                           
                            <td colspan="2" bgcolor="#D1D1EC" width="150" >
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_mutual" EnableViewState="False" 
                                     width="150">&nbsp;</asp:Label>
                         
                            <td colspan="2" bgcolor="#f2f2f2">
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_mmax" EnableViewState="False" width="150">&nbsp;</asp:Label></td>
                   
                            <td colspan="2" bgcolor="#D1D1EC" width="150">
                                <asp:Label runat="server" 
                                    ID="lbl_last_regdate_comtuonix" EnableViewState="False">&nbsp;</asp:Label></td>
                          
                            <td colspan="2" bgcolor="#f2f2f2">
                                <asp:Label runat="server" ID="lbl_last_regdate_sinotech" 
                                    EnableViewState="False" width="150">&nbsp;</asp:Label>
                            </td>
                         
                            <td colspan="2" bgcolor="#D1D1EC">
                                <asp:Label runat="server" ID="lbl_last_regdate_minimicro" 
                                    EnableViewState="False" width="150">&nbsp;</asp:Label></td>
                       
                            <td colspan="2" bgcolor="#f2f2f2">
                                <asp:Label runat="server" ID="lbl_last_regdate_alc" EnableViewState="False" width="150">&nbsp;</asp:Label></td>
                          
                            <td colspan="2" bgcolor="#D1D1EC">
                                <asp:Label runat="server" ID="lbl_last_regdate_samtach" EnableViewState="False" width="150">&nbsp;</asp:Label></td>
                           
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
