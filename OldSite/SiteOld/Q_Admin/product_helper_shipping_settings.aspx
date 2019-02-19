<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_shipping_settings.aspx.cs" Inherits="Q_Admin_product_helper_shipping_settings" Title="shipping/part_size setting"  EnableEventValidation="false" ResponseEncoding="gb2312"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" />
   <table style="width: 100%;" cellpadding="0" cellspacing="0" border="0">
        <tr>
                <td style="width: 30%; height: 337px;" valign="top">
                             <asp:Panel runat="server" ID="panel3"  SkinID="back_blue" Width="100%">
                                            <asp:panel runat="server" ID="panel4" SkinID="panel_title1" Width="100%">
                                                 &nbsp;&nbsp;&gt;&gt; Shipping Company</asp:panel> 
                                           <asp:panel runat="server" ID="panel1" Width="100%">
                                               <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel runat="server" ID="panel23" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_New_shipping_company" Text="NEW" OnClick="lb_New_shipping_company_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                        </td>
                                                         <td>
                                                            <asp:Panel runat="server" ID="panel2" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_Save_shipping_company" Text="Save" OnClick="lb_Save_shipping_company_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                         </td>
                                                    </tr>
                                               </table>
                                               <anthem:DataGrid ID="dg_shipping_company" runat="server" Width="100%" AutoGenerateColumns="False">
                                                   <Columns>
                                                       <asp:BoundColumn DataField="shipping_company_id" HeaderText="ID"></asp:BoundColumn>
                                                       <asp:TemplateColumn HeaderText="Shipping company name">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_shipping_company_name" runat="server" __designer:wfdid="w6" CssClass="input"  text='<%#DataBinder.Eval(Container.DataItem, "shipping_company_name") %>'></anthem:TextBox>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="priority">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_qty" runat="server" __designer:wfdid="w7" CssClass="input" Columns="6" text='<%#DataBinder.Eval(Container.DataItem, "qty") %>'></anthem:TextBox>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                   </Columns>
                                               </anthem:DataGrid>
                                           </asp:panel> 

                            </asp:Panel> 
                </td>
               <td style="width: 30%; height: 337px;" valign="top">
                <asp:Panel runat="server" ID="panel5"  SkinID="back_blue" Width="100%">
                                            <asp:panel runat="server" ID="panel6" SkinID="panel_title1" Width="100%">
                                                 &nbsp;&nbsp;&gt;&gt; product size</asp:panel> 
                                           <asp:panel runat="server" ID="panel7" Width="100%">
                                               <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel runat="server" ID="panel8" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_new_product_size" Text="NEW" OnClick="lb_new_product_size_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                        </td>
                                                         <td>
                                                            <asp:Panel runat="server" ID="panel9" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_save_product" Text="Save" OnClick="lb_save_product_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                         </td>
                                                    </tr>
                                               </table>
                                               <anthem:DataGrid ID="dg_product_size" runat="server" Width="100%" AutoGenerateColumns="False">
                                                   <Columns>
                                                       <asp:BoundColumn DataField="product_size_id" HeaderText="ID"></asp:BoundColumn>
                                                       <asp:TemplateColumn HeaderText="product size name">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_product_size_name" runat="server" text='<%#DataBinder.Eval(Container.DataItem, "product_size_name") %>' CssClass="input" __designer:wfdid="w6"></anthem:TextBox> 
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="begin price">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_begin_price" runat="server" text='<%#DataBinder.Eval(Container.DataItem, "begin_price") %>' CssClass="input" __designer:wfdid="w7" Columns="8"></anthem:TextBox> 
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="end price">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_end_price" runat="server" text='<%#DataBinder.Eval(Container.DataItem, "end_price") %>' CssClass="input" __designer:wfdid="w8" Columns="8"></anthem:TextBox> 
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                      
                                                   </Columns>
                                               </anthem:DataGrid>
                                           </asp:panel> 

                            </asp:Panel>
               </td>
            <td rowspan="3" style="width: 5px" valign="top">
                &nbsp;</td>
            <td rowspan="3" style="width: 39%" valign="top">
                             <asp:Panel runat="server" ID="panel15"  SkinID="back_blue" Width="100%">
                                            <asp:panel runat="server" ID="panel16" SkinID="panel_title1" Width="100%">
                                                 &nbsp;&nbsp;&gt;&gt; state shipping</asp:panel> 
                                           <asp:panel runat="server" ID="panel17" Width="100%">
                                               <table>
                                                    <tr>
                                                        <td>
                                                            <anthem:RadioButtonList ID="radioCountry" runat="server" AutoCallBack="True" OnSelectedIndexChanged="radioCountry_SelectedIndexChanged" RepeatColumns="2">
                                                                <Items>
                                                                    <asp:ListItem Value="1">Canada</asp:ListItem>
                                                                    <asp:ListItem Value="2">United States</asp:ListItem>
                                                                </Items>
                                                               
                                                            </anthem:RadioButtonList>
                                                        </td>
                                                        <td>
                                                            <asp:Panel runat="server" ID="panel18" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_new_state_shipping" Text="NEW" OnClick="lb_new_state_shipping_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                        </td>
                                                         <td>
                                                            <asp:Panel runat="server" ID="panel19" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_save_state_shipping" Text="Save" OnClick="lb_save_state_shipping_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                         </td>
                                                    </tr>
                                               </table>
                                               <anthem:DataGrid ID="dg_state_shipping" runat="server" Width="100%" AutoGenerateColumns="False">
                                                   <Columns>
                                                       <asp:BoundColumn DataField="state_serial_no" HeaderText="ID"></asp:BoundColumn>
                                                       <asp:TemplateColumn HeaderText="state_name">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_state_name" runat="server" __designer:wfdid="w28" CssClass="input" text='<%#DataBinder.Eval(Container.DataItem, "state_name") %>'></anthem:TextBox> 
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="short name">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_short_name" runat="server" __designer:wfdid="w19" CssClass="input" Columns="5" text='<%#DataBinder.Eval(Container.DataItem, "state_short_name") %>'></anthem:TextBox>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="gst">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_gst" runat="server" __designer:wfdid="w21" CssClass="input" Columns="2" MaxLength="2" text='<%#DataBinder.Eval(Container.DataItem, "gst") %>'></anthem:TextBox>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="pst">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_pst" runat="server" __designer:wfdid="w22" CssClass="input" Columns="2" MaxLength="2" text='<%#DataBinder.Eval(Container.DataItem, "pst") %>'></anthem:TextBox>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="shipping%">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_state_shipping" runat="server" __designer:wfdid="w29" CssClass="input" Columns="2" MaxLength="3" text='<%#DataBinder.Eval(Container.DataItem, "state_shipping") %>'></anthem:TextBox>% 
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                      
                                                   </Columns>
                                               </anthem:DataGrid>
                                           </asp:panel> 

                            </asp:Panel>
            </td>
        </tr>
       <tr>
           <td colspan="2" style="height: 5px" valign="top">
           </td>
       </tr>
       <tr>
           <td colspan="2" style="height: 337px" valign="top">
                            <asp:Panel runat="server" ID="panel10"  SkinID="back_blue" Width="100%">
                                            <asp:panel runat="server" ID="panel11" SkinID="panel_title1" Width="100%">
                                                 &nbsp;&nbsp;&gt;&gt; account settings</asp:panel> 
                                           <asp:panel runat="server" ID="panel12" Width="100%">
                                               <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Panel runat="server" ID="panel13" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_new_account" Text="NEW" OnClick="lb_new_account_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                        </td>
                                                         <td>
                                                            <asp:Panel runat="server" ID="panel14" SkinID="btn">
                                                                    <anthem:LinkButton runat="server" ID="lb_save_account" Text="Save" OnClick="lb_save_account_Click"></anthem:LinkButton>
                                                            </asp:Panel>
                                                         </td>
                                                    </tr>
                                               </table>
                                               <anthem:DataGrid ID="dg_account" runat="server" Width="100%" AutoGenerateColumns="False" OnItemDataBound="dg_account_ItemDataBound" AllowPaging="True" OnPageIndexChanged="dg_account_PageIndexChanged">
                                                   <Columns>
                                                       <asp:BoundColumn DataField="account_id" HeaderText="ID"></asp:BoundColumn>
                                                       <asp:TemplateColumn HeaderText="charge">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_charge" runat="server" __designer:wfdid="w25" CssClass="input" Columns="7" MaxLength="7" text='<%#DataBinder.Eval(Container.DataItem, "charge") %>'></anthem:TextBox> 
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="shipping company">
                                                           <itemtemplate>
<anthem:DropDownList id="_ddl_shipping_company" runat="server" __designer:wfdid="w11"></anthem:DropDownList>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="product size">
                                                           <itemtemplate>
<anthem:DropDownList id="ddl_product_size" runat="server" __designer:wfdid="w12"></anthem:DropDownList>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="category">
                                                           <itemtemplate>
<anthem:DropDownList id="_ddl_product_category" runat="server" __designer:wfdid="w13"></anthem:DropDownList>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="qty">
                                                           <itemtemplate>
<anthem:TextBox id="_txt_account_qty" runat="server" __designer:wfdid="w16" CssClass="input" Columns="3" MaxLength="3" text='<%#DataBinder.Eval(Container.DataItem, "qty") %>'></anthem:TextBox>
</itemtemplate>
                                                       </asp:TemplateColumn>
                                                      
                                                   </Columns>
                                                   <PagerStyle Mode="NumericPages" />
                                               </anthem:DataGrid>
                                           </asp:panel> 

                            </asp:Panel>
           </td>
       </tr>
   </table> 
</asp:Content>

