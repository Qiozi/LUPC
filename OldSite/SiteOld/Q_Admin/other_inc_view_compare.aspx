<%@ Page Language="C#" AutoEventWireup="true" CodeFile="other_inc_view_compare.aspx.cs" Inherits="Q_Admin_other_inc_view_compare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Shopbot</title>
    <script src="JS/Alert.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table cellpadding="0" cellspacing="0">
            <tr>
                    <td style="text-align:center" rowspan="3">
                        <asp:Literal ID="literal_lu_img" runat="server"></asp:Literal>
                        <br />
                        <asp:Literal ID="literal_pre_next_btn" runat="server"></asp:Literal>
                    </td>
                    <td colspan="2" style="text-align:left; font-weight:bold;"><asp:LinkButton runat="server" ID="lb_lu_sku"></asp:LinkButton></td>
            </tr>
             <tr>
                    <td colspan="2" style="text-align:left">
                            <div>
                                    <asp:LinkButton runat="server" ID="lb_product_name"></asp:LinkButton>
                            </div>
                            <div style="border-top:1px solid #cccccc; text-align:right">
                                    <b>MFP#: </b><asp:Label runat="server" ID="lbl_mfp" ForeColor="#006600"></asp:Label>
                            </div>
                    </td>
            </tr>
            <tr>
               
                    <td colspan="2">
                            <table align="left" style="width:350px; border: 1px solid #ccc;">
                                    <tr>
                                    <th style="text-align:left;">
                                                    Real Cost:</th>
                                            <th style="text-align:right;">
                                                    <asp:Label runat="server" ID="lbl_part_real_cost" ForeColor="Blue"></asp:Label>
                                                    <br />
                                                    <asp:Label runat="server" ID="lbl_part_real_cost_regdate" ForeColor="Blue"></asp:Label>
                                            </th>
                                      </tr>
                                    <tr>
                                    <th style="text-align:left;">
                                                    Cost
                                                    :</th>
                                            <th style="text-align:right;">
                                                    <asp:Label runat="server" ID="lbl_part_cost"></asp:Label>
                                            </th>
                                      </tr>
                                    <tr>
                                    <th style="text-align:left;">
                                                    Price:</th>
                                            <th style="text-align:right;">
                                                    <asp:Label runat="server" ID="lbl_part_price"></asp:Label>
                                            </th>
                                      </tr>
                                    <tr>
                                    <th style="text-align:left;">
                                                    discount:</th>
                                            <th style="text-align:right;">
                                                    <asp:Label runat="server" ID="lbl_product_current_discount"></asp:Label>
                                            </th>
                                      </tr>
                                    <th style="text-align:left;">
                                                    Sold
                                                                                        <th style="text-align:right;">
                                                    <asp:Label runat="server" ID="lbl_part_sold"></asp:Label>
                                            </th>
                                      </tr>
                                    <tr>
                                            <td style="text-align:left;">
                                                    special cash:</td>
                                            <td style="text-align:right;">
                                                  *除去折扣的现金价  <asp:TextBox runat="server" ID="txt_product_current_price"></asp:TextBox>
                                                   
                                                    </td>
                                    </tr>
                                    <tr>
                                            <td style="text-align:center;">
                                                    &nbsp;</td>
                                            <td style="text-align:center;">
                                    <asp:Button runat="server" ID="btn_save" Text="Save" onclick="btn_save_Click"/>
                                            </td>
                                    </tr>
                            </table>
                    </td>
            </tr>
           <tr>
                    <td colspan="3" ><hr size="1" /></td>
            </tr>
            <tr>
                    <td valign="top" style="background: #E8E8F6;" colspan="2">
                            <asp:ListView runat="server" ID="lv_other_inc" ItemPlaceholderID="itemPlaceholderID">
                                    <LayoutTemplate >
                                            <table>
                                            <tr runat="server" id="itemPlaceholderID"></tr>
                                            </table>
                                    </LayoutTemplate>
                                    
                                    <ItemTemplate>
                                            <tr>
                                                    <td><%# DataBinder.Eval(Container.DataItem, "other_inc_name") %></td>
                                                    <td style="width: 100px; text-align:right;"><%# DataBinder.Eval(Container.DataItem, "other_inc_price") %></td>
                                                    <td style="width: 60px; text-align:right;"><%# DataBinder.Eval(Container.DataItem, "other_inc_store_sum")%></td>
                                                    <td style="width: 180px; text-align:right;"><%# DataBinder.Eval(Container.DataItem, "regdate").ToString()%></td>
                                            </tr>
                                    </ItemTemplate>
                            </asp:ListView>        
                    </td>
                    <td valign="top" style="width: 500px; border-left: 1px solid #aabbcc ">                         
                             <asp:ListView runat="server" ID="lv_shopbot" ItemPlaceholderID="itemPlaceholderID">
                                    <LayoutTemplate >
                                            <table>
                                            <tr runat="server" id="itemPlaceholderID"></tr>
                                            </table>
                                    </LayoutTemplate>
                                    
                                    <ItemTemplate>
                                            <tr>
                                                    <td><%# DataBinder.Eval(Container.DataItem, "other_inc_name")%></td>
                                                    <td style="width: 100px; text-align:right;"><%# DataBinder.Eval(Container.DataItem, "price") %></td>
                                                     <td style="width: 160px; text-align:right;"><%# DataBinder.Eval(Container.DataItem, "regdate").ToString()%></td>
                                            </tr>
                                    </ItemTemplate>
                            </asp:ListView>       
                    </td>
            </tr>
    </table>
    </div><asp:Literal ID="Literal1" runat="server"></asp:Literal>
    </form>
    
</body>
</html>
