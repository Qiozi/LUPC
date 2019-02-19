<%@ Page Title="ADV Banner" Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="advertisement_banner.aspx.cs" Inherits="Q_Admin_advertisement_banner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table class="table_shell" cellspacing="0" cellpadding="0">
<tr>
                <td align="center" bgcolor="#F2F2F2">
                    <asp:Button runat="server" ID="btn_save_banner_type" 
                        Text="Save(flash or image)" onclick="btn_save_banner_type_Click" 
                        Height="57px" /></td><td></td>
        </tr>
        <tr>
                <td bgcolor="#CCCCCC"><asp:RadioButton ID="RadioButton1" runat="server" GroupName="banner" 
                        Text="Select" /></td>
                
                <td valign="top" style="height: 600px">
                            <table cellspacing="0" cellpadding="0">
                                    <tr>
                                            <th bgcolor="#AAAAAA">Banner</th>
                                    </tr>
                                    <tr>
                                        
                                            <td bgcolor="#F2F2F2">
                                                    <asp:Image ID="Image_banner_1" runat="server" /><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_url1" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_image1" Columns="50"></asp:TextBox>
                                                    <asp:RadioButton ID="RadioButton_banner_flash_image1" GroupName="flash_image" 
                                                        runat="server" Text="选中，可给此项赋值 " ForeColor="#ff3300" />
                                            </td>
                                    </tr>
                                    <tr>
                                        
                                            <td bgcolor="#F2F2F2">
                                                    <asp:Image ID="Image_banner_2" runat="server" />
                                                    
                                                    <br />
                                                    <asp:TextBox runat="server" ID="txt_banner_url2" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_image2" Columns="50"></asp:TextBox>
                                                    <asp:RadioButton ID="RadioButton_banner_flash_image2" GroupName="flash_image" 
                                                        runat="server" Text="选中，可给此项赋值 " ForeColor="#FF3300" />
                                            </td>
                                    </tr>
                                    <tr>
                                        
                                            <td bgcolor="#F2F2F2">
                                                    
                                                    <asp:Image ID="Image_banner_3" runat="server" /><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_url3" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_image3" Columns="50"></asp:TextBox>
                                                    <asp:RadioButton ID="RadioButton_banner_flash_image3" GroupName="flash_image" 
                                                        runat="server" Text="选中，可给此项赋值 " ForeColor="#ff3300" />
                                            </td>
                                    </tr>
                                    <tr>
                                        
                                            <td bgcolor="#F2F2F2">
                                                    
                                                    <asp:Image ID="Image_banner_4" runat="server" /><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_url4" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_image4" Columns="50"></asp:TextBox>
                                                    <asp:RadioButton ID="RadioButton_banner_flash_image4" GroupName="flash_image" 
                                                        runat="server" Text="选中，可给此项赋值 " ForeColor="#ff3300" />
                                            </td>
                                    </tr>
                                    <tr>
                                        
                                            <td bgcolor="#F2F2F2">
                                                    
                                                    <asp:Image ID="Image_banner_5" runat="server" /><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_url5" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_image5" Columns="50"></asp:TextBox>
                                                    <asp:RadioButton ID="RadioButton_banner_flash_image5" GroupName="flash_image" 
                                                        runat="server" Text="选中，可给此项赋值 " ForeColor="#ff3300" />
                                            </td>
                                    </tr>
                                    <tr>
                                        
                                            <td bgcolor="#F2F2F2">
                                                    
                                                    <asp:Image ID="Image_banner_6" runat="server" /><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_url6" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" ID="txt_banner_image6" Columns="50"></asp:TextBox>
                                                    <asp:RadioButton ID="RadioButton_banner_flash_image6" GroupName="flash_image" 
                                                        runat="server" Text="选中，可给此项赋值 " ForeColor="#ff3300" />
                                            </td>
                                    </tr>
                                    <tr>
                                             <td bgcolor="#F2F2F2">&nbsp;</td>
                                    </tr>
                            </table>
                </td>
            
           
            
                <td rowspan="3" valign="top" style="padding-left:10px;">
                        <table cellpadding="0" cellspacing="0" >
                                <tr>
                                    <td style="padding: 5px;">
                                    <table style="border: 1px solid #ff9900; background:#f2f2f2; width: 100%;">
                                            <tr>
                                                    <th style="text-align:left;padding-left: 10px">上传图片</th>
                                            </tr>
                                            <tr>
                                                    <td style="padding:10px;"><asp:FileUpload ID="FileUpload1" runat="server" />
                                                        <asp:Button ID="Button_upload" runat="server"
                                            Text="Upload" onclick="Button_upload_Click" /></td>
                                            </tr>
                                    </table>
                                    </td>
                                </tr>

                                <tr>
                                        <td>
                                                <asp:ListView runat="server" ID="lv_banner_image_list" 
                                                    ItemPlaceholderID="itemPlaceHolderID" 
                                                    onitemcommand="lv_banner_image_list_ItemCommand">
                                                        <LayoutTemplate>
                                                                <table cellpadding="0" cellspacing="2">
                                                                        <tr runat="server" id="itemPlaceHolderID"></tr>
                                                                </table>
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                                <tr>
                                                                        <td><asp:Button runat="server" ID="_btn_image_banner" Text="把此图传给左边的选中项"  CommandName="SetIn"
                                                                         CommandArgument='<%# DataBinder.Eval(Container.DataItem, "image_filename") %>'/></td>
                                                                         <td><asp:Image runat="server" ID="_img_banner" Width="300" ImageUrl='<%# DataBinder.Eval(Container.DataItem, "image_filename") %>' /></td>
                                                                </tr>
                                                        </ItemTemplate>
                                                </asp:ListView>
                                        </td>
                                </tr>
                        </table>
                </td>
            
        </tr>
         <tr>
                <td bgcolor="#F2F2F2" valign="top"><asp:RadioButton ID="RadioButton2" runat="server" GroupName="banner" 
                        Text="Select" /></td><td valign="top">
                                                    <asp:Image ID="Image_banner_7" runat="server" />
                    <br />
                    <asp:TextBox runat="server" ID="txt_banner_url7" Columns="50"></asp:TextBox><br />
                                                    <asp:TextBox runat="server" 
                        ID="txt_banner_image7" Columns="50"></asp:TextBox><asp:RadioButton ID="RadioButton_banner_flash_image7" 
                                                        GroupName="flash_image" runat="server" ForeColor="#ff3300" 
                                                        Text="选中，可给此项赋值 " 
                                                        oncheckedchanged="RadioButton_banner_flash_image7_CheckedChanged" />
                                            </td>            
        </tr>
        
</table>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
       
        
</asp:Content>

