<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ebay_system_updata_excel.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_system_updata_excel" Title="Ebay sys Updata Excel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Ebay System</h2>
    <div style="text-align:center; line-height: 30px; padding:1em; border-bottom: 1px solid #ccc;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button_updata" runat="server" Text="Updata" 
            onclick="Button_updata_Click" />
    </div>
    <p>
        excel 格式如图:<br />
        <img src='http://www.lucomputers.com/soft_img/example/example_ebay_sys_updata.jpg' />
       
    </p>
    <a name='part'></a>
    <h2>Ebay Part</h2>
     <div style="text-align:center; line-height: 30px; padding:1em; border-bottom: 1px solid #ccc;">
        <asp:FileUpload ID="FileUpload2" runat="server" />
        <asp:Button ID="Button1" runat="server" Text="Updata" onclick="Button1_Click" 
             />
    </div>
    <p>
        excel 格式如图:<br />
        <img src='http://www.lucomputers.com/soft_img/example/example_ebay_part_updata.jpg' />
        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </p>
</asp:Content>

