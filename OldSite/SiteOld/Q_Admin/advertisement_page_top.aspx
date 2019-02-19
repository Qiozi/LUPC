<%@ Page Title="ADV Page Top Flash" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="advertisement_page_top.aspx.cs" Inherits="Q_Admin_advertisement_page_top" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" src="/js/flashobject.js"></script>
<br />
<br />
<br />
<br />
<br />
<br /><br />
<br />
<br />
<br />
<br />
<br />
<table>
                    <tr>
                            <td>
                            <div id="flash_top_1"></div>
                            <script  type="text/javascript">
                                var FocusFlash = new FlashObject("/flash/top.swf", "top_av", 960, 128, "7", "#FFFFFF", false, "High");
                                FocusFlash.write("flash_top_1");
                              
                            </script>
                            </td>
                    </tr>
                    <tr>
                            <td><asp:RadioButton ID="RadioButton_page_top_flash_1" runat="server" 
                                    GroupName="page_top_flash" Text="Select" /></td>
                        
                    </tr>
                    <tr>
                            <td>
                            <div id="flash_top_2"></div>
                            
                            <script type="text/javascript">
                                var FocusFlash = new FlashObject("/flash/top_chres.swf", "top_av2", 960, 128, "7", "#FFFFFF", false, "High");
                                FocusFlash.write("flash_top_2");
                            </script>
                            </td>
                    </tr>
                    <tr>
                            <td><asp:RadioButton ID="RadioButton_page_top_flash_2" runat="server" 
                                    GroupName="page_top_flash" Text="Select" /></td>
                    </tr>
                    <tr>
                            <td><asp:Button runat="server" ID="btn_save_page_top_flash" Text="Save" 
                                    onclick="btn_save_page_top_flash_Click" /></td>
                    </tr>
        </table>
</asp:Content>

