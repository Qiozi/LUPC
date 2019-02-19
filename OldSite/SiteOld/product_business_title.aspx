<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true"
    CodeFile="product_business_title.aspx.cs" Inherits="product_business_title" Title="modify business title" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center">
        <asp:Button ID="Button1" runat="server" Text="Save" OnClick="Button1_Click" />
    </div>
    <hr size="1" />
    <table>
        <tr>
            <td>
                title
            </td>
            <td>
                <asp:TextBox runat="server" ID="txt_title" Columns="50" MaxLength="50"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                img url
            </td>
            <td>
                <asp:TextBox runat="server" ID="txt_url" Columns="50" MaxLength="200"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                content
            </td>
            <td>
                <asp:TextBox runat="server" ID="txt_content" Columns="50" MaxLength="1000" Rows="5"
                    TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>
