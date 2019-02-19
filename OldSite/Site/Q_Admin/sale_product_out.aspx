<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="sale_product_out.aspx.cs" Inherits="Q_Admin_sale_product_out" Title="Untitled Page"  EnableEventValidation="false" ResponseEncoding="gb2312"%>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" />
   <table>
    <tr>
        <td>Order Number:<anthem:TextBox ID="txt_keyword" runat="server" CssClass="input"></anthem:TextBox></td>
        <td>
        <asp:Panel runat="server" ID="panel1" SkinID="btn" Width="150px">
                            <anthem:LinkButton runat="server" ID="lb_search" Text="Search" OnClick="lb_search_Click" ></anthem:LinkButton>
                    </asp:Panel>
        </td>
    </tr>
       
   </table>
   
   <hr size="1" />
   
   <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
                <td style="width: 49%">
                     out product
                </td>
            <td style="background: #f2f2f2; width: 7px">
            </td>
               <td>
                    store
               </td> 
        </tr>
         <tr>
                <td valign="top">
                    <h3>
                    <table style="width: 100%">
                            <tr>
                                <td>
                                    Count: <anthem:Label runat="server" ID="lbl_product_count"></anthem:Label>
                                </td>
                                <td>
                                    Sub&nbsp; Total : <anthem:Label runat="server" ID="lbl_product_price"></anthem:Label>
                                </td>
                            </tr>
                    </table></h3> 
                     <anthem:DataGrid runat="server" ID="dg_product" Width="100%" OnItemCommand="dg_product_ItemCommand" OnItemDataBound="dg_product_ItemDataBound">
                     <HeaderStyle CssClass="trTitle" />
                         <Columns>
                             <asp:ButtonColumn CommandName="SelectSN" Text="获取产品SN"></asp:ButtonColumn>
                         </Columns>
                     </anthem:DataGrid>
                </td>
             <td style="background: #f2f2f2; width: 7px" valign="top">
             </td>
               <td valign="top">
                   <anthem:TextBox ID="txt_out_sn" runat="server" AutoCallBack="True" Columns="30" CssClass="input"
                       OnTextChanged="TextBox1_TextChanged" Rows="20" TextMode="MultiLine"></anthem:TextBox><br />
                       <h3>
                    <table style="width: 100%">
                            <tr>
                                <td>
                                    Count: <anthem:Label runat="server" ID="lbl_store_count"></anthem:Label>
                                </td>
                                <td>
                                    Sub Total : <anthem:Label runat="server" ID="lbl_store_price"></anthem:Label>
                                </td>
                                <td>
                                        <asp:Panel runat="server" ID="panel2" SkinID="btn" Width="150px">
                            <anthem:LinkButton runat="server" ID="lb_save" Text="Save" OnClick="lb_save_Click" PreCallBackFunction="confirm2_PreCallBack"></anthem:LinkButton>
                    </asp:Panel>
                                </td>
                            </tr>
                    </table></h3>
                   <anthem:DataGrid runat="server" ID="dg_store" AutoGenerateColumns="False" Width="100%"><HeaderStyle CssClass="trTitle" />
                       <Columns>
                           <asp:BoundColumn DataField="SN" HeaderText="SN"></asp:BoundColumn>
                           <asp:BoundColumn DataField="in_date" HeaderText="in date" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                           <asp:BoundColumn DataField="factory_date" HeaderText="产家保修日期" DataFormatString="{0:dd/MM/yyyy}"></asp:BoundColumn>
                           <asp:BoundColumn DataField="sku" HeaderText="SKU"></asp:BoundColumn>
                           <asp:BoundColumn DataField="product_name" HeaderText="product  name"></asp:BoundColumn>
                             <asp:BoundColumn DataField="current_price" HeaderText="current_price" DataFormatString="{0:$##,###.##}"></asp:BoundColumn>
                       </Columns>
                   </anthem:DataGrid></td> 
        </tr>
   </table> 
</asp:Content>

