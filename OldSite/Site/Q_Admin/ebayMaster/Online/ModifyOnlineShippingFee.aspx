<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ModifyOnlineShippingFee.aspx.cs" Inherits="Q_Admin_ebayMaster_Online_ModifyOnlineShippingFee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h4>Shipping Options</h4>
    <asp:Literal ID="Literal_sku" runat="server"></asp:Literal>
        <br /><br />
        <table>
            <tr>
                <td valign="top"> 
                    Domestic Shipping<br />
                    <asp:DropDownList runat="server" ID="ddl_domestic_shipping" Enabled="false">
                        
                        <asp:ListItem Text="Calculated-Cost varies by buyer location"></asp:ListItem>
                        <asp:ListItem Text="Flat-Same cost to all buyers" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Local pickup only"></asp:ListItem>
                        
                    </asp:DropDownList><br />
                    Domestic Services <br />
                    <asp:DropDownList runat="server" ID="ddl_domestic_services_1"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_domestic_service_1_cost"></asp:TextBox>
                    <asp:CheckBox runat="Server" ID="cb_domestic_free_shipping" 
                        Text="Free Shipping" 
                        Checked="true" />
                    <br />
                    <asp:DropDownList runat="server" ID="ddl_domestic_services_2"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_domestic_service_2_cost"></asp:TextBox>
                    <br />
                    <asp:DropDownList runat="server" ID="ddl_domestic_services_3"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_domestic_service_3_cost"></asp:TextBox>

                              
                </td>
                <td valign="top"> 
                    International Shipping<br />
                    <asp:DropDownList runat="server" ID="DropDownList1" Enabled="false">
                        
                        <asp:ListItem Text="Calculated-Cost varies by buyer location"></asp:ListItem>
                        <asp:ListItem Text="Flat-Same cost to all buyers" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Local pickup only"></asp:ListItem>
                        
                    </asp:DropDownList><br />
                    International Services <br />
                    <asp:DropDownList runat="server" ID="ddl_International_services_1"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_international_service_1_cost"></asp:TextBox>
                    <asp:CheckBox runat="Server" ID="cb_international_free_shipping" 
                        Text="Free Shipping" Enabled="False" />
                    <br />
                    <asp:DropDownList runat="server" ID="ddl_International_services_2"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_international_service_2_cost"></asp:TextBox>
                    <br />
                    <asp:DropDownList runat="server" ID="ddl_International_services_3"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_international_service_3_cost"></asp:TextBox>
                    <br />
                    <asp:DropDownList runat="server" ID="ddl_International_services_4"></asp:DropDownList>
                    cost $<asp:TextBox runat="server" ID="txt_international_service_4_cost"></asp:TextBox>
                </td>
               
            </tr>
        </table>
        <div style="text-align:center;"><asp:Button ID="Button1" runat="server" 
                Text="Update" onclick="Button1_Click" /></div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    
</asp:Content>

