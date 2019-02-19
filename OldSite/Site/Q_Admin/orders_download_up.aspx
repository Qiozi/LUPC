<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="orders_download_up.aspx.cs" Inherits="Q_Admin_orders_download_up" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <br />
    <br />
    
    <table align="center">
            <tr>
                    <td>
                        <asp:CheckBox ID="cb_all" runat="server" Text="ALL(contain cancel)" 
                            Visible="False" />
                    </td>
                    <td>begin</td><td>
                    <asp:TextBox runat="server" ID="txt_begin_date" Columns="30"></asp:TextBox></td><td>
                                    <ul class="ul_parent">
                                                    <li> <span class="displayBlockTitle"><img src="http://www.lucomputers.com/images/arrow_6.gif" /></span> 
                                                        <div>
                                                        <asp:Calendar ID="Calendar2" runat="server" onselectionchanged="Calendar2_SelectionChanged"
                                                         ondayrender="Calendar2_DayRender" TitleFormat="Month">
                                                        </asp:Calendar>
                                                        </div>
                                                    </li>
                                                </ul>
                      </td>
                    <td>end</td> <td><asp:TextBox runat="server" ID="txt_end_date"></asp:TextBox></td><td>
                                                <ul class="ul_parent">
                                                    <li> <span class="displayBlockTitle"><img src="http://www.lucomputers.com/images/arrow_6.gif" /></span> 
                                                        <div>
                                                        <asp:Calendar ID="Calendar1" runat="server" onselectionchanged="Calendar1_SelectionChanged"
                                                         ondayrender="Calendar1_DayRender">
                                                        </asp:Calendar>
                                                        </div>
                                                    </li>
                                                </ul></td>
                    <td><asp:Button runat="server" ID="btn_download" onclick="btn_download_Click" 
                            Text="Download" /></td>
            </tr>
            <tr>
                    <td>
                        &nbsp;</td>
                    <td colspan="5"><i style="color:#ff9900">*format: 2009-01-01<br />* 下载后，请把sheet$名称改为table, 然后另存为xls文件即可上传.<br />
                        </i></td><td>
                                                &nbsp;</td>
                    <td>&nbsp;</td>
            </tr>
            <tr>
                    <td colspan="8">
                       <hr size="1" /></td>
            </tr>
            <tr>
                    <td>&nbsp;</td>
                    <td>UP</td><td colspan="4">
                    <asp:FileUpload ID="FileUpload1" runat="server" />
                    </td>
                    <td>&nbsp;</td>
                    <td><asp:Button runat="server" ID="btn_upload" Text="Upload" 
                            onclick="btn_upload_Click" />
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </td>
            </tr>
            <tr>
                    <td colspan="8"><hr size="1" /></td>
            </tr>
            <tr>
                    <td colspan="8">&nbsp;</td>
            </tr>
            <tr>
                    <td colspan="8">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <b style="color:Blue">Pre Status A.D.</b></td>
                                <td>
                                    <b style="color:Blue">Back Status A.D.</b></td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <asp:GridView ID="gv_pre_status" runat="server" Width="300px">
                        </asp:GridView>
                                </td>
                                <td valign="top">
                                    <asp:GridView ID="gv_back_status" runat="server" Width="300px">
                        </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </td>
            </tr>
            <tr>
                    <td colspan="8"><b style="color:Blue"><%--Pay Method A.D.--%></b></td>
            </tr>
            <tr>
                    <td colspan="8">
                        <asp:GridView ID="gv_pre_method" runat="server" Width="300px" Visible="False">
                        </asp:GridView>
                    </td>
            </tr>
            <tr>
                    <td colspan="8">&nbsp;</td>
            </tr>
    </table>
    
</asp:Content>

