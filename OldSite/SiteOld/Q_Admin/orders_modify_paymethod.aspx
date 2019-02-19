<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_modify_paymethod.aspx.cs" Inherits="Q_Admin_orders_modify_paymethod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<style>
label { font-size:8.5pt; color:#5D5DBE;}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   <p style="text-align:center">

       <asp:RadioButtonList ID="RadioButtonList_paymethod" runat="server" 
           RepeatDirection="Vertical" Width="300px" align="center">
       </asp:RadioButtonList>
    </p>
     <p style="text-align:center">

       <asp:RadioButtonList ID="RadioButtonList_country" runat="server" 
           RepeatDirection="Vertical" Width="300px" align="center" ForeColor="Red" 
             RepeatColumns="2">
           <asp:ListItem Value="CAD" Text="CAD" Selected="True"></asp:ListItem>
           <asp:ListItem Value="USD" Text="USD"></asp:ListItem>
       </asp:RadioButtonList>
    </p>
     <p style="text-align:center; border-top:1px dotted #ccc"><asp:Button ID="btn_submit" runat="server" 
             Text="Submit" onclick="btn_submit_Click" /></p>
    
</asp:Content>

