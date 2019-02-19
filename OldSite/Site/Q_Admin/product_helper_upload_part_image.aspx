<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_upload_part_image.aspx.cs" Inherits="Q_Admin_product_helper_upload_part_image" Title="Upload Part Image" %>

<%@ Register Src="UC/CategoryDropDownLoad.ascx" TagName="CategoryDropDownLoad" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center; font-size: 9pt;">
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Upload" />
        <br />
        <asp:Button ID="btn_generate_photo_for_ebay" runat="server"
            OnClick="btn_generate_photo_for_ebay_Click" OnClientClick="ParentLoadWait();"
            Text="Generate Photo For Ebay." Visible="False" />
        <br />
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click"
            Text="temp reupload" Visible="False" />
        <hr size="1" />
        * 1. 每次只能上传一个产品的图片， 可以是多张。 第一张的图片名称格式为：sku.jpg&nbsp; 例：2002.jpg<br />
        2. 如果旧图片已存在， 那新上传的将会覆盖旧产品图片。<br />
        3. 图片大小尽量是 418x418
        <hr size="1" />
        如果没有第一张图片，请输入产品SKU：<asp:TextBox runat="server" ID="txt_sku"></asp:TextBox>
        <hr size="1" />
        <asp:GridView ID="GridView_Upload" runat="server" AutoGenerateColumns="False" align="center">
            <Columns>
                <asp:BoundField DataField="id" />
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:FileUpload ID="_FileUpload1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <hr size="1" />
        <table align="center" width="700">
            <tr>
                <td>300px Image 坐标</td>
                <td>X:
                    <asp:TextBox runat="server" ID="txt_300_x">20</asp:TextBox>
                    <br />
                    Y:
                    <asp:TextBox runat="server" ID="txt_300_y">250</asp:TextBox>
                </td>
                <td>
                    <uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server"
                        Visible="False" />
                    <asp:Button ID="btn_tmp" runat="server" OnClick="btn_tmp_Click" Text="临时按钮，转换已存在的图片" Visible="False" />
                    <br />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr size="1" />
                </td>
            </tr>

            <tr>
                <td>418px&nbsp; Image 坐标</td>
                <td>X:
                    <asp:TextBox runat="server" ID="txt_418_x">20</asp:TextBox>
                    <br />
                    Y:
                    <asp:TextBox runat="server" ID="txt_418_y">340</asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btn_tmp_single" runat="server" OnClick="btn_tmp_single_Click" Text="临时按钮，转换单个已存在的图片"
                        Visible="False" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr size="1" />
                </td>
            </tr>
            <tr>
                <td colspan="3">数值越小，越透明<br />
                    <asp:RadioButtonList ID="RadioButtonList_transparency_v" runat="server"
                        RepeatColumns="10">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem Selected="True">2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <hr size="1" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/soft_img/shuyin/1.gif" />
                </td>
                <td>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/soft_img/shuyin/2.jpg" />
                </td>
                <td>
                    <asp:Image ID="Image3" runat="server" ImageUrl="~/soft_img/shuyin/3.gif" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="RadioButton_image1" GroupName="image_name" runat="server" /></td>

                <td>
                    <asp:RadioButton ID="RadioButton_image2" GroupName="image_name" runat="server" Checked="True" /></td>
                <td>
                    <asp:RadioButton ID="RadioButton_image3" GroupName="image_name" runat="server" /></td>
            </tr>
        </table>
        <hr size="1" />
        <asp:Label runat="server" ID="lbl_sku" Font-Bold="True"></asp:Label>
        <hr size="1" />
    </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

</asp:Content>

