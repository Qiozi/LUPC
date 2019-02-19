<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_ebay_import.aspx.cs" Inherits="Q_Admin_orders_ebay_import" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
caption { text-align:left;font-size:9pt;}
i { color:Blue;}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div style="padding: 1em;">
            上传ebay 文件<asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="Button1" runat="server"
                Text="Upload" onclick="Button1_Click" />
            <br />
            <i>*上传前请把空纪录 与 最后两条纪录（非订单数据）删除；sheet$ 名称改为 table</i>
            <br /><asp:Literal ID="Literal2" runat="server"></asp:Literal>
        </div>
        <hr size="1" />
    <asp:GridView ID="GridView1" runat="server" Caption="无法导入的ebay订单列表" CaptionAlign="Top" 
      >
    </asp:GridView>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

