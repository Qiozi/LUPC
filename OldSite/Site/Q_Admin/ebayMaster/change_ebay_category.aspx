<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="change_ebay_category.aspx.cs" Inherits="Q_Admin_ebayMaster_change_ebay_category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        p {
            padding: 1em;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <p>
        SKU:
        <asp:Label ID="LabelSKU" runat="server" Text="Label" Font-Bold="True"></asp:Label>
    </p>
    <p>
        eBay Itemid:
        <asp:Label ID="LabelItemid" runat="server" Text="Label"
            Font-Bold="True"></asp:Label>
    </p>

    <p>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
        <asp:DropDownList ID="DropDownList2" runat="server"></asp:DropDownList>
    <p>

        <asp:Button ID="ButtonSubmit" runat="server" Text="Submit"
            OnClick="ButtonSubmit_Click" />
    </p>
</asp:Content>

