<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ebay_number_update.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_number_update" Title="Update Ebay Turbo List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div style="margin:auto; padding: auto;height: 50px; padding-top: 20px;">
    <asp:FileUpload ID="FileUpload1" runat="server" />
    <asp:Button ID="btn_upload" runat="server"
        Text="Upload" onclick="btn_upload_Click" />
        <asp:Button ID="Button_match" runat="server" onclick="Button_match_Click" 
        Text="Match Import" />
        <br /><i>* sheet$ 改为: table; <br />* item id 如果已成功上传，将无法改变， 如果有误，请联系小吴</i>
    <hr />
    <asp:Literal runat="server" ID="literal_comment"></asp:Literal>
</div>
</asp:Content>

