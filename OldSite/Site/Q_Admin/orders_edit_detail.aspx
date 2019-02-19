<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail.aspx.cs" Inherits="Q_Admin_orders_edit_detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
    fieldset { font-size: 8.5pt;}
     fieldset table tr td { font-size: 8.5pt;}
     #table_part_list td { font-size: 8.5pt;}
     #table_part_list th { font-size: 9pt;}
     #div_sys_list tr td:hover { background:#ccc}
     #table_price td{ font-size: 8.5pt; }

</style>
<script type="text/javascript">
    function OpenWinPaypalCreditCardDirect(order_code) {
        var balance = 0;
        $('span').each(function (i) {
            if ($(this).attr('id').indexOf('lbl_pay_balance') > -1)
                balance = $(this).html();
        });
        winOpen('/q_admin/paypal_card_do_direct_payment.asp?order_code=' + order_code + '&USD_CAD=USD_CAD&balance=' + balance, 'order_paypal_credit_card_diret_' + order_code, 'width=1000;height=900;top=100;left=100;');
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <input type="hidden" name="htmlOrderCode" id="htmlOrderCode" value="<%= OrderCodeRequest %>" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<input  type="button" value="打开百度" onclick="ShowIframe('百度','http://www.baidu.com',800,450)"><br>  
<input  type="button" value="HTML字符串" onclick="ShowHtmlString('字符串','<B>Hello,PopWin',400,200)"><br>  
<input  type="button" value="信息提示框" onclick="ShowAlert('提示框','<B>Hello,PopWin',200,100)"><br>  
<input  type="button" value="是否确认框" onclick="ShowConfirm('确定','是否删除','Button4','',340,80)"> --%> 
<h3><asp:Label runat="server" ID="lbl_order_code_title"></asp:Label></h3>
<table  cellpadding="0" cellspacing="0">
 <tr>
        <td bgcolor="White" valign="top">
                <table cellpadding="0" cellspacing="0" width="100%" >
                            <tr>
                                        <td valign="top">
                                                <fieldset style="min-height: 100px" class="fieldset2">
                                                                    <legend>Personal Information</legend>
                                                    <asp:UpdatePanel runat="server" ID="updatePanel2">
                                                            <ContentTemplate>
                                                                    &nbsp;<asp:Label ID="lblcustomer_company" runat="Server"></asp:Label>&nbsp;<br />
                                                                    &nbsp;<asp:Label runat="Server" id="lblcustomer_first_name"></asp:Label>&nbsp;<asp:Label runat="Server" id="lblcustomer_last_name"></asp:Label>&nbsp;<br />
                                                                    <br />
                                                                    &nbsp;<asp:Label ID="lbl_phone_d" runat="Server"></asp:Label>&nbsp;<br />
                                                                    &nbsp;<asp:Label runat="Server" id="lbl_phone_n"></asp:Label>&nbsp;<br />
                                                                    &nbsp;<asp:Label runat="Server" id="lbl_phone_c"></asp:Label>&nbsp;<br />
                                                                    &nbsp;<asp:Label runat="Server" id="lbl_customer_email1"></asp:Label><br />
                                                                    &nbsp;<asp:Label runat="Server" id="lbl_customer_email2"></asp:Label>
                                                                    
                                                                    <br />
                                                                    <br />
                                                                    &nbsp;<asp:Label ID="lbl_customer_address" runat="Server"></asp:Label>&nbsp;<br />
                                                                    &nbsp;<asp:Label ID="lbl_customer_city" runat="Server"></asp:Label>&nbsp;<asp:Label ID="lbl_customer_state_code" runat="Server"></asp:Label>&nbsp;<asp:Label ID="lbl_customer_zip_code" runat="Server"></asp:Label>&nbsp;
                                                                   
                                                                    
                                                                <br /><br />
                                                                <input  type="button" value="Modify Name" onclick="ShowIframe('编辑用户信息','/q_admin/orders_edit_detail_personal_information.aspx?OrderCode='+ $('#htmlOrderCode').val(),500,450)">
                                                               
                                                          </ContentTemplate>
                                                      </asp:UpdatePanel>
                                                                </fieldset>
                                             </td>
                                        <td valign="top">
                                        
                                        <asp:UpdatePanel runat="server" ID="updatePanel_shipping_address">
                                                <ContentTemplate>
                                                <fieldset style="min-height: 60px" class="fieldset2">
                                                                    <legend>SHIPPING ADDRESS</legend>
                                                         
                                                                    
                                                                    
                                                                &nbsp;<asp:Label ID="lbl_shipping_first_name" runat="server"></asp:Label>&nbsp;<asp:Label ID="lbl_shipping_last_name" runat="server"></asp:Label>
                                                                <br />
                                                                &nbsp;<asp:Label ID="lbl_shipping_address1" runat="Server"></asp:Label>
                                                                &nbsp;<br />
                                                    &nbsp;<asp:Label ID="lbl_shipping_city" runat="server"></asp:Label>&nbsp;<asp:Label ID="lbl_shipping_state" runat="server"></asp:Label>&nbsp;<asp:Label ID="lbl_shipping_zip_code" runat="server"></asp:Label>
                                                                 <br />
                                                                 <br />
                                                                <input  type="button" value="Edit Shipping Address" onclick="ShowIframe('Edit Shipping Address','/q_admin/orders_edit_detail_shipping_address.aspx?OrderCode='+ $('#htmlOrderCode').val(),500,350)">
                                                               
                                                            </fieldset>
                                                            
                                                            
                                                             <fieldset style="min-height: 60px" class="fieldset2">
                                                                    <legend>CREDIT CARD</legend>
                                                                    &nbsp;<asp:Label ID="lbl_card_name" runat="server"></asp:Label>&nbsp;<br />
                                                                &nbsp;<asp:Label ID="lbl_card_number" runat="server"></asp:Label>&nbsp;<br />
                                                                 &nbsp;<asp:Label ID="lbl_verification_number" runat="server"></asp:Label>&nbsp;<br />
                                                    &nbsp;<asp:Label ID="lbl_card_expiry" runat="server"> </asp:Label><br />
                                                    &nbsp;<asp:Label ID="lbl_card_issuer" runat="server"></asp:Label>&nbsp;<br />
                                                    &nbsp;<asp:Label ID="lbl_customer_card_phone" runat="server"></asp:Label>
                                                                    <br />
                                                                    <br />&nbsp;<asp:Label ID="lbl_card_billing_shipping_address" runat="server"></asp:Label>
                                                                    <br />
                                                                    &nbsp;<asp:Label ID="lbl_card_city" runat="server"></asp:Label>&nbsp;
                                                                    <asp:Label ID="lbl_card_state" runat="server"></asp:Label>&nbsp;&nbsp;<asp:Label ID="lbl_card_zip_code" runat="server"></asp:Label>
                                                                     <br />
                                                                    <br />
                                                                    <input  type="button" value="Edit Credit Card" onclick="ShowIframe('Edit Credit Card Info...','/q_admin/orders_edit_detail_Credit_Card.aspx?OrderCode='+ $('#htmlOrderCode').val(),500,450)">
                                                               
                                                                   
                                                                </fieldset>
                                                             </ContentTemplate>
                                                         </asp:UpdatePanel>
                                        </td>
                            </tr>
                </table>
            
            
            
            <fieldset class="fieldset2">
                <legend>Other</legend>
                <asp:UpdatePanel runat="server" ID="updatePanel_other">
                        <ContentTemplate>  
                                <div style="background: #D1DAEC">
                                <table cellpadding="0" cellspacing="1" style="width: 100%" class="table_td_white">
                                    <tr>
                                        <td class="title" style="width: 120px;background: #f2f2f2; font-weight: bold;">
                                            &nbsp;TAX EXAMP:</td>
                                        <td>
                                            <asp:Label ID="lbl_tax_exemption" runat="server"></asp:Label>
                                            &nbsp;
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                        <td class="title" style="font-weight: bold; background: #f2f2f2">
                                            &nbsp;PAYMENT:</td>
                                        <td>
                                        <asp:Label ID="lbl_pay_method" runat="server"></asp:Label>
                                            &nbsp;<asp:Label ID="lbl_pay_method_text" runat="server"></asp:Label></td>
                                    </tr>
                                    
                                    <tr>
                                        <td class="title" style="font-weight: bold; background: #f2f2f2">
                                            &nbsp;SCHEDULED:</td>
                                        <td>
                                            
                                            <asp:Label ID="lbl_pick_up_time_1" runat="server" Font-Bold="True">NONE</asp:Label>
                                            <asp:Label ID="lbl_pick_up_time_chart" runat="server" Font-Bold="True">,</asp:Label>
                                            <asp:Label ID="lbl_pick_up_time_2" runat="server" Font-Bold="True">NONE</asp:Label>&nbsp;
                                        </td>
                                    </tr>

                                    <tr>
                                        <td>&nbsp;</td>
                                        <td><input  type="button" value="Edit Other info" onclick="ShowIframe('Edit Other','/q_admin/orders_edit_detail_change_other.aspx?OrderCode='+ $('#htmlOrderCode').val(),500,300)"></td>
                                    </tr>                                        
                                    </table>
                                    </div>
                     </ContentTemplate>
                </asp:UpdatePanel>
            </fieldset>
            <asp:UpdatePanel runat="server" ID="updatePanel_base_info">
                    <ContentTemplate>
            <div style="text-align:center ;">
                    <asp:Button runat="server" ID="btn_prev" Text="Prev" onclick="btn_prev_Click" />
                    <asp:Button runat="server" ID="btn_save" Text="Save" onclick="btn_save_Click" />
                    <input type="button" ID="btn_view_order" value="View Order"  onclick='winOpen("sales_order_detail.aspx?order_code=" + <%= Request.QueryString["order_code"].ToString() %>, "order_detail", 1000,900, 100, 100);' />
                    
                    <input  type="button" value="Email Order Confirm" onclick='ShowIframe("Email Order Confirm","email_simple_invoice.aspx?order_code=<%= Request.QueryString["order_code"].ToString() %>&email=<%= SendEmail %>",800,600)'>
                    <input  type="button" value="Print Invoice" onclick='ShowIframe("Print Invoice","sale_print_order.aspx?order_code=<%= Request.QueryString["order_code"].ToString() %>",800,600)'>
                                                           
                    <%--<input type="button" ID="btn_print_invoice" value="Print Invoice"   onclick='window.open("sale_print_order.aspx?order_code=" + <%= Request.QueryString["order_code"].ToString() %>, "print_invoice");'/>
                     --%>
                     <asp:Button runat="server" ID="btn_accept_down_invoice" Text="允许客户下载INVOICE" 
                        onclick="btn_accept_down_invoice_Click"  /></div>
            
            <!-- order price info ---begin ---->
            
            <div style="clear:both; background:#FAEBE6; height: 160px">
                    <hr size="1"  style=" color: #4D7094;" />
                       <table cellpadding="1" cellspacing="0" style="width: 100%; " align="right" id="table_price">
                                <tr>
                                        <td colspan="5">
                                            Order Number:&nbsp;&nbsp;<asp:Label runat="server" ID="lbl_order_code" 
                                                Font-Bold="True"></asp:Label>
                                                &nbsp;&nbsp;&nbsp;&nbsp;
                                                <asp:Literal runat="server" ID="literal_invoice_code"></asp:Literal>
                                        </td>
                                </tr>
                                <tr>
                                            <td></td><td></td>
                                            <td style="width: 150px;">Sub Total</td>
                                            <td align="right" style="width: 120px;"><asp:Label runat="server" ID="lbl_sub_total" ForeColor="#663300"></asp:Label></td>
                                            <td style="width:100px;">&nbsp;</td>
                                            <td rowspan="9" valign="top">
                                                <table cellpadding="0" cellspacing="0">
                                                        <tr>
                                                                <td>balance:</td><td>$<asp:Label runat="server" ID="lbl_pay_balance" ForeColor="Blue"></asp:Label></td>
                                                        </tr>
                                                        <tr>
                                                                <td width="80">Pay Method:</td><td><asp:DropDownList runat="server" ID="ddl_pay_pay_method"></asp:DropDownList></td>
                                                        </tr>
                                                        <tr>
                                                                <td>payment:</td><td><asp:TextBox runat="server" ID="txt_pay_cash" Columns="10"></asp:TextBox></td>
                                                        </tr>
                                                </table>
                                                <table cellpadding="0" cellspacing="0" border="0" width="99%">
                                                    <tr>
                                                        <td width="80">
                                                            date:
                                                        </td>
                                                        <td width="60">
                                                            <asp:TextBox ID="txt_pay_date" runat="server" Columns="10" ReadOnly="true"></asp:TextBox>
                                                        </td>                                                        
                                                        <td>
                                                            <ul class="ul_parent">
                                                                <li>
                                                                    <span class="displayBlockTitle">
                                                                    <img src="http://www.lucomputers.com/images/arrow_6.gif" /></span>
                                                                    <div>
                                                                        <asp:Calendar ID="Calendar3" runat="server" ondayrender="Calendar3_DayRender" 
                                                                            onselectionchanged="Calendar3_SelectionChanged"></asp:Calendar>
                                                                    </div>
                                                                </li>
                                                            </ul>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <asp:Button ID="btn_save_pay_cash" runat="server" Text="Save" OnClick="btn_pay_cash_Click" />
                                                <hr size="1" />                                                
                                               <asp:ListView runat="server" ID="lv_pay_record" ItemPlaceholderID="itemPlaceholderID">
                                                    <LayoutTemplate>                                                                                            
                                                            <table cellpadding="0" cellspacing="0">                                                                   
                                                                    <tr runat="server" id="itemPlaceholderID"></tr>
                                                            </table>                                                                                                           
                                                    </LayoutTemplate>
                                                    <ItemTemplate>
                                                            <tr>
                                                                    <td style="color: #666666;text-align: right">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "pay_regdate")%></td>
                                                                    <td width="80" style="text-align: right; color: #663300">$<%# DataBinder.Eval(Container.DataItem, "pay_cash")%></td> 
                                                                    <td style="color: #663300" width="120">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "pay_record_name") %></td>                                                                                                                                                                                                      
                                                            </tr>
                                                    </ItemTemplate>
                                            </asp:ListView>
                                            <input type="button" value="Paypal Credit Card Direct" onclick="OpenWinPaypalCreditCardDirect('<%= Request.QueryString["order_code"].ToString() %>');" />
                                            </td>
                                </tr>
                                <tr>
                                            <td>&nbsp;</td><td class="style1">&nbsp;</td>
                                            <td>Special Cash Discount</td>
                                            <td align="right" style="color: Red">-<asp:Label runat="server" ID="lbl_special_cash_discount" 
                                                    ForeColor="red"></asp:Label></td>
                                            <td>&nbsp;<asp:Label runat="server" 
                                                    ID="lbl_special_cash_discount_recommend" ForeColor="Red"></asp:Label></td>
                                </tr>
                                 <tr>
                                            <td>&nbsp;</td><td class="style1">&nbsp;</td>
                                            <td>Ship Charge</td>
                                            <td align="right"><asp:Label runat="server" ID="lbl_ship_charge" 
                                                    ForeColor="#663300"></asp:Label></td>
                                            <td>&nbsp;<asp:Label runat="server" ID="lbl_ship_charge_recommend" 
                                                    ForeColor="Red"></asp:Label></td>
                                </tr>
                                <tr>
                                            <td>&nbsp;</td><td rowspan="2" align="left" class="style1">
                                            &nbsp;</td>
                                            <td>Taxable Total</td>
                                            <td align="right"><asp:Label runat="server" ID="lbl_taxable_total" 
                                                    ForeColor="#663300"></asp:Label></td>
                                            <td>&nbsp;</td>
                                </tr>
                                <tr>
                                            <td>&nbsp;</td>
                                            <td><asp:Label runat="server" ID="lbl_sale_tax_rate_text"></asp:Label></td>
                                            <td align="right"><asp:Label runat="server" ID="lbl_sale_tax" ForeColor="#663300"></asp:Label></td>
                                            <td>&nbsp;</td>
                                </tr>
                               
                                <tr>
                                            <td>
                                                    &nbsp;</td><td class="style1">&nbsp;</td>
                                            <td>
                                                <ul class="ul_parent">
                                                    <li class="displayBlockTitle"><span style="color:Black;">WEEE</span>C
                                                        <div style="border: 1px solid #ff9900; padding: 10px; width: 400px; background: #ffffff; text-align: center">
                                                            <asp:Button runat="server" ID="btn_save_weee" Text="Save WEEE" onclick="btn_save_weee_Click" 
                                                        OnClientClick="ParentLoadWait();" />
                                                            <hr size="1" />
                                                            <asp:TextBox runat="server" ID="txt_weee_charge" ></asp:TextBox>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lbl_weee" runat="server" ForeColor="#663300">0</asp:Label>
                                            </td><td>&nbsp;</td>
                                </tr>
                                
                                
                                
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style1">
                                        &nbsp;</td>
                                    <td>
                                        Grand Total</td>
                                    <td align="right">
                                        <asp:Label ID="lbl_grand_total" runat="server" Font-Bold="True" 
                                            Font-Size="10pt" ForeColor="#993300">0</asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lbl_price_unit" runat="server" CssClass="price_unit" 
                                            ForeColor="#0033CC"></asp:Label>
                                    </td>
                                </tr>
                                
                                
                                
                                <tr>
                                    <td>
                                        <ul class="ul_parent">
                                            <li class="displayBlockTitle">Change
                                                <div style="border: 1px solid #ff9900; padding: 10px; width: 400px; background: #ffffff; text-align: center">
                                                    <asp:Button ID="btn_save_ship_method_charge_discount" runat="server" 
                                                        onclick="btn_save_ship_method_charge_discount_Click" 
                                                        OnClientClick="ParentLoadWait();" Text="Save" />
                                                    <hr size="1" />
                                                    <table>
                                                        
                                                        <tr>
                                                            <td>
                                                                discount:
                                                            </td>
                                                            <td style="text-align:right">
                                                                $<asp:TextBox ID="txt_order_discount" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lb_remove_order_discount" runat="server" CssClass="a_btn" 
                                                                    onclick="lb_remove_order_discount_Click" Text="移除自定义输入锁定"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                ship charge:
                                                            </td>
                                                            <td style="text-align:right">
                                                                $<asp:TextBox ID="txt_order_ship_charge" runat="server"></asp:TextBox>
                                                            </td>
                                                            <td>
                                                                <asp:LinkButton ID="lb_remove_order_ship_charge" runat="server" 
                                                                    CssClass="a_btn" onclick="lb_remove_order_ship_charge_Click" Text="移除自定义输入锁定"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Ship method:
                                                            </td>
                                                            <td style="text-align:right">
                                                                <asp:DropDownList ID="ddl_ship_method" runat="server" size="9">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                            </li>
                                        </ul>
                                    </td>
                                    <td class="style1">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="2">
                                        <asp:Label ID="lbl_ship_method" runat="server"></asp:Label>
                                    </td>
                                  
                                </tr>
                                
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td class="style1">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="2">
                                        &nbsp;</td>
                                </tr>
                                
                                </table>
                       <hr size="1"  style=" color: #4D7094;" />
            </div>
            </ContentTemplate>
                    <Triggers>
                         
                    </Triggers>
            </asp:UpdatePanel>
            <!-- order Price info ---end ---->
            
            <!-- part list ----begin ---->
            <asp:UpdatePanel runat="server" ID="updatePanel_part_list">
                <ContentTemplate>
                 
                    <div style="text-align:right;border: 1px solid #ff9900;">
                        part#:<asp:TextBox runat="server" ID="txt_part_sku" Columns="20" Height="20px"></asp:TextBox>
                        <asp:Button runat="server" ID="btn_add_part" Text="Add" 
                            onclick="btn_add_part_Click" UseSubmitBehavior="False" />
                        <br />
                        
                        Part Name: <asp:TextBox runat="server" ID="txt_part_name" Columns="50"></asp:TextBox>
                        Sell$:<asp:TextBox runat="server" ID="txt_part_sell" Columns="8"></asp:TextBox>
                        <asp:Button runat="server" ID="btn_add_part_by_name" Text="Add" 
                            onclick="btn_add_part_by_name_Click" UseSubmitBehavior="False" />
                    </div>
             
            <asp:ListView runat="server" ID="lv_part_list" 
                    ItemPlaceholderID="itemPlaceHolderid" onitemcommand="lv_part_list_ItemCommand">
                    <LayoutTemplate>
                          <div style="background:#ccc">
                            <table cellspacing="1" class="table_td_white" style="width: 100%" id="table_part_list">
                                    <tr>
                                            <th>
                                                    SKU
                                            </th>
                                            <th>Name</th>
                                            <th>Cost</th>
                                            
                                            <th>Price</th><th>Sold</th>
                                            <th>Sum</th>
                                            <th>SubTotal</th>
                                    </tr>
                                    <tr id="itemPlaceHolderid" runat="server">
                                    
                                    </tr>
                            </table>
                          </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                            <tr>
                                    <td>
                                            <asp:HiddenField runat="server" ID="_hf_serial_no" Value='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                            <asp:Literal runat="server" ID="_literal_system_code"  Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Literal> </td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "product_name")%></td>
                                    <td style="padding-right:2px; text-align:right; " ><asp:Literal runat="server" ID="_literal_order_product_cost" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_cost")%>'></asp:Literal></td>
                                    <td style="padding-right:2px; text-align:right"><asp:Literal runat="server" ID="_literal_order_product_price" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_price")%>'></asp:Literal></td>
                                    <td><asp:TextBox id="_txt_sold_part" runat="server" CssClass="input" __designer:wfdid="w8" Columns="6" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_sold")%>'></asp:TextBox></td>
                                    <td>
                                            <ul class="ul_parent">
                                                    <li >
                                                        <asp:TextBox id="_txt_sum_part" runat="server" CssClass="input" __designer:wfdid="w9" 
                                                        Columns="2" Text='<%# DataBinder.Eval(Container.DataItem,"order_product_sum") %>'></asp:TextBox>
                                                            <div class="float_area">
                                                                  <table cellspacing="2" align="left">
                                                                    <tr>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_1" Text="1" CssClass="a_btn" CommandArgument="1" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_2" Text="2" CssClass="a_btn" CommandArgument="2" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_3" Text="3" CssClass="a_btn" CommandArgument="3" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_4" Text="4" CssClass="a_btn" CommandArgument="4" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_5" Text="5" CssClass="a_btn" CommandArgument="5" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_6" Text="6" CssClass="a_btn" CommandArgument="6" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_7" Text="7" CssClass="a_btn" CommandArgument="7" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_8" Text="8" CssClass="a_btn" CommandArgument="8" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_9" Text="9" CssClass="a_btn" CommandArgument="9" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_10" Text="10" CssClass="a_btn" CommandArgument="10" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    </tr>
                                                                  </table>
                                                            </div>
                                                    </li>
                                           </ul>
                                   </td>
                                   <td style="padding-right:2px; text-align:right">
                                            <asp:Literal runat="server" ID="literal_sub_total" Text='<%# DataBinder.Eval(Container.DataItem,"subtotal_2") %>'></asp:Literal>
                                   </td>
                                   <td>
                                            <ul class="ul_parent">
                                                    <li style="display:block ; width:30px; height: 20px" class="displayBlockTitle">Cmd
                                                            <div class="float_area">
                                                                   <asp:ImageButton runat="server" ID="imgBtn_delete" ImageAlign="Middle"  ImageUrl="~/Q_Admin/Images/exit.gif"
                                                                     CommandName="DeletePart" OnClientClick="if(!confirm('Sure?')) return false; else {ParentLoadWait(); return true;} "  
                                                                     CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                                                    <asp:ImageButton runat="server" ID="imgBtn_save" ImageAlign="Middle"  ImageUrl="~/Q_Admin/Images/save_1.gif"
                                                                     CommandName="SavePart" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                                            </div>
                                                    </li>
                                            </ul>
                                   </td>
                            </tr>
                    </ItemTemplate>
            </asp:ListView>
            
               </ContentTemplate>
                <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_save" />
                        <asp:AsyncPostBackTrigger ControlID="btn_add_part" />
                </Triggers>
            </asp:UpdatePanel>
            <!-- part list ---- end ----->
            
            
            <!-- system list ----begin ----->
            
             <asp:UpdatePanel runat="server" ID="updatePanel1">
                <ContentTemplate>
                 
                    <div style="text-align:right">
                        system:<asp:TextBox runat="server" ID="txt_sys_sku" Columns="20"></asp:TextBox>
                        <asp:Button runat="server" ID="btn_add_sys" Text="Add" 
                            onclick="btn_add_sys_Click" onclientclick="ParentLoadWait();" />
                        <asp:Button ID="btn_duplicate_sys" runat="server" 
                            onclick="btn_duplicate_sys_Click" onclientclick="ParentLoadWait();" 
                            Text="Duplicate Sys To New" />
                    </div>
            <div id="div_sys_list">
            <asp:ListView runat="server" ID="lv_sys_list" ItemPlaceholderID="itemPlaceHolderid" 
                    onitemdatabound="lv_sys_list_ItemDataBound" 
                    onitemcommand="lv_sys_list_ItemCommand">
                    <LayoutTemplate>
                          <div style="background:#ccc">
                            <table cellspacing="1" class="table_td_white" style="width: 100%" id="table_part_list">
                                    <tr>
                                            <th></th>
                                            <th>
                                                    SKU
                                            </th>
                                            <th>Name</th>
                                            <th>Cost</th>
                                            
                                            <th>Price</th><th>Sold</th>
                                            <th>Sum</th>
                                            <th>SubTotal</th>
                                    </tr>
                                    <tr id="itemPlaceHolderid" runat="server">
                                    
                                    </tr>
                            </table>
                          </div>
                    </LayoutTemplate>
                    <ItemTemplate>
                            <tr>
                                    <td style="width:15px"><asp:Button ID="_btn_view_detail" runat="server"  
                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>' 
                                    CommandName="ViewSystemDetail" 
                                    Text="+"/></td>
                                    <td> 
                                            <asp:HiddenField runat="server" ID="_hf_serial_no" Value='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                            <asp:Literal runat="server" ID="_literal_system_code"  Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Literal> </td>
                                    <td><%# DataBinder.Eval(Container.DataItem, "product_name")%></td>
                                    <td style="padding-right:2px; text-align:right; " >
                                            <asp:Literal runat="server" ID="_literal_order_product_cost" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_cost")%>'></asp:Literal>
                                            </td>
                                    <td style="padding-right:2px; text-align:right">
                                            <asp:Literal runat="server" ID="_literal_order_product_price" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_price")%>'></asp:Literal>
                                    </td>
                                    <td><asp:TextBox id="_txt_sold_part" runat="server" CssClass="input" __designer:wfdid="w8" Columns="6" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_sold")%>'></asp:TextBox></td>
                                    <td>
                                            <ul class="ul_parent">
                                                    <li >
                                                        <asp:TextBox id="_txt_sum_part" runat="server" CssClass="input" __designer:wfdid="w9" 
                                                        Columns="2" Text='<%# DataBinder.Eval(Container.DataItem,"order_product_sum") %>'></asp:TextBox>
                                                            <div class="float_area">
                                                                  <table cellspacing="2" align="left">
                                                                    <tr>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_1" Text="1" CssClass="a_btn" CommandArgument="1" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_2" Text="2" CssClass="a_btn" CommandArgument="2" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_3" Text="3" CssClass="a_btn" CommandArgument="3" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_4" Text="4" CssClass="a_btn" CommandArgument="4" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_5" Text="5" CssClass="a_btn" CommandArgument="5" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_6" Text="6" CssClass="a_btn" CommandArgument="6" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_7" Text="7" CssClass="a_btn" CommandArgument="7" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_8" Text="8" CssClass="a_btn" CommandArgument="8" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_9" Text="9" CssClass="a_btn" CommandArgument="9" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                            <td><asp:LinkButton runat="server" ID="_lb_10" Text="10" CssClass="a_btn" CommandArgument="10" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    </tr>
                                                                  </table>
                                                            </div>
                                                    </li>
                                           </ul>
                                   </td>
                                   <td style="padding-right:2px; text-align:right">
                                            <asp:Literal runat="server" ID="literal_sub_total" Text='<%# DataBinder.Eval(Container.DataItem,"subtotal_2") %>'></asp:Literal>
                                   </td>
                                   <td>
                                            <ul class="ul_parent">
                                                    <li style="display:block ; width:30px; height: 20px" class="displayBlockTitle">Cmd
                                                            <div class="float_area">
                                                                   <asp:ImageButton runat="server" ID="imgBtn_delete" ImageAlign="Middle"  ImageUrl="~/Q_Admin/Images/exit.gif" 
                                                                   CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>'
                                                                     CommandName="DeleteSys" OnClientClick="if(!confirm('Sure?')) return false; else {ParentLoadWait(); return true;} " />
                                                                    <asp:ImageButton runat="server" ID="imgBtn_save" ImageAlign="Middle"  ImageUrl="~/Q_Admin/Images/save_1.gif"
                                                                     CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>'
                                                                     CommandName="SaveSys" />
                                                            </div>
                                                    </li>
                                            </ul>
                                   </td>
                            </tr>
                            <asp:Panel runat="server" ID="_panel_sys_detail" Visible="false">
                            <tr>
                                <td colspan="9" style="background-color:#cccccc">
                                        <div style="text-align:right">
                                                    luc sku<asp:TextBox runat="server" ID="_txt_part_sku"></asp:TextBox>
                                                    qty<asp:TextBox runat="server" ID="_txt_part_quantity" MaxLength="1" Text="1" Columns="1" Width="15"></asp:TextBox>
                                                    <asp:Button runat="server" ID="_btn_add_part_to_sys" OnClientClick="ParentLoadWait();" CommandName="AddPartToSys" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "product_serial_no")%>' Text="Add"/>
                                        </div>
                                        <table cellspacing="1" align="right" width="95%">       
                                              
                                        <asp:Repeater runat="server" ID="_rpt_sys_detail" OnItemCommand="rpt_ItemCommand">
                                                <ItemTemplate>
                                                        <tr>
                                                                <td style="text-align:center">
                                                                        <%# DataBinder.Eval(Container.DataItem, "product_serial_no")%>
                                                                </td>
                                                                <td><%# DataBinder.Eval(Container.DataItem, "product_name")%></td>
                                                                <td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "product_current_sold")%></td>
                                                                <td style="color: green;text-align:center"><%# DataBinder.Eval(Container.DataItem, "part_quantity")%></td>
                                                                <td style="text-align:right"><%# decimal.Parse(DataBinder.Eval(Container.DataItem, "product_current_sold").ToString()) * decimal.Parse( DataBinder.Eval(Container.DataItem, "part_quantity").ToString())%></td>
                                                                <td>
                                                                    <asp:LinkButton runat="server" ID="_lb_sys_detail_remove" Text="Remove" CommandName="DelSysDetail" 
                                                                 CommandArgument='<%# DataBinder.Eval(Container.DataItem, "sys_tmp_detail")+"|"+ DataBinder.Eval(Container.DataItem, "sys_tmp_code")+"|"+ DataBinder.Eval(Container.DataItem, "product_serial_no")   %>'
                                                                  OnClientClick="if(!confirm('Sure?')) return false; else {ParentLoadWait(); return true;} "
                                                                 ></asp:LinkButton>
                                                                </td>
                                                        </tr>
                                                </ItemTemplate>
                                        </asp:Repeater>
                                        </table>                                       
                                </td>
                            </tr>
                            </asp:Panel>
                    </ItemTemplate>
            </asp:ListView>
            </div>	
                    </ContentTemplate>

            </asp:UpdatePanel>
            <!-- system list ---- end  ----->
        </td>
        <td  valign="top" >  
            <div style="border: 1px solid #ff9900; background:#FFECB9;width:400px; margin:auto; margin-left:5px; padding:5px;height: 80px">
            <asp:UpdatePanel runat="server" ID="updatePanel_order_status">
                    <ContentTemplate>
                    

                            当前工作人员：<asp:Label runat="server" ID="lbl_staff"></asp:Label>
                            <br />
                            前台状态：<asp:Label runat="server" ID="lbl_pre_status"></asp:Label>
                            <br />
                            后台状态：<asp:Label runat="server" ID="lbl_out_status"></asp:Label>
                            <br /><br />
                            <ul class="ul_parent">
                                    <li class="displayBlockTitle">Change
                                            <div style="border: 1px solid #ff9900; background:##E8ECC3; width: 400px;height: 400px">
                                                    <table cellpadding="0" cellspacing="0" width="230">
                                                            <tr>
                                                                    <td>
                                                                        <span style="text-align:center; clear:both;">
                                                                              <asp:Button runat="server" ID="btn_save_status" Text="Save" OnClick="btn_save_status_click" />
                                                                        </span>
                                                                    </td>
                                                                    <td style="text-align:center;width: 20px;">
                                                                        <span style="display:block;width: 30px; height: 10px; border:0px solid red;" onclick='winOpen("order_invoice_op.aspx?order_code=" + <%= Request.QueryString["order_code"].ToString() %>, "invoice_op", 400,200, 100, 100);'> </span>
                                                                    </td>
                                                            </tr>
                                                    </table> 
                                                    <hr size="1" style="clear:both">
                                                    <table>
                                                            <tr>
                                                                     <td>前台状态：</td><td><asp:DropDownList runat="server" ID="ddl_pre_status" ></asp:DropDownList></td>
                                                            </tr>
                                                            <tr>
                                                                     <td>后台状态：</td><td><asp:DropDownList runat="server" ID="ddl_back_status"></asp:DropDownList></td>
                                                            </tr>
                                                            
                                                            
                                                    </table>
                                            </div>                                                                        
                                    </li>                                                                       
                            </ul><br /><br />
                  </ContentTemplate>
            </asp:UpdatePanel>
            </div>               
            <fieldset class="fieldset2" style="width:400px">
                            <legend>To Customer</legend>
                            <div style=" vertical-align:middle;"><asp:TextBox ID="txt_msg_from_seller" runat="server" 
                                    Columns="30" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                <asp:Button ID="btn_submit_to_customer" runat="server" 
                                    onclick="btn_submit_to_customer_Click" Text="Submit Note" />
                            </div>
                            
                                <asp:DataList 
                                    ID="dl_msg_list" runat="server" Width="100%" BackColor="White" 
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                                ForeColor="Black" GridLines="Vertical">
                                    <FooterStyle BackColor="#CCCC99" />
                                    <AlternatingItemStyle BackColor="White" />
                                    <ItemStyle BackColor="#F7F7DE" />
                                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <ItemTemplate>
                                    <div style="color:#8BA2D1; width: 300px;">
                                      
                                                 <div style="float:left; color:#000">
                                                    <%# DataBinder.Eval(Container.DataItem,"msg_author").ToString() == "Me" ?"Customer":DataBinder.Eval(Container.DataItem,"msg_author").ToString() %>
                                                 </div>
                                                 <div style="float: right; color:#000"> 
                                                    <%# DataBinder.Eval(Container.DataItem,"regdate") %>
                                                 </div>
                                                <div  style="min-height: 40px; color: #8BA2D1; clear:both; vertical-align:middle; padding-left: 2em;margin-top: 5px;">
                                                   <%# System.Web.HttpUtility.HtmlDecode( DataBinder.Eval(Container.DataItem,"msg_content_text").ToString().Replace("\r\n", "<br/>")) %>
                                                </div>
                                    </div>
                                </ItemTemplate>
                                </asp:DataList>
            </fieldset>
            
           
            </td>
    </tr>
        
	</table>
	 <fieldset class="fieldset2">
	        <legend>History</legend>
            
            <asp:GridView ID="gv_order_product_history" runat="server" 
                AutoGenerateColumns="False" 
                onrowdatabound="gv_order_product_history_RowDataBound" Width="100%">
                <RowStyle Font-Size="8.5pt" />
                <Columns>
                    <asp:BoundField HeaderText="SKU" DataField="product_serial_no" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="name" DataField="product_name"/>
                    <asp:BoundField HeaderText="unit price$" DataField="order_product_sold" >
                    <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundField>
                    <asp:BoundField DataField="order_product_sum" HeaderText="Sum" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Cmd" DataField="add_del" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Datetime" DataField="create_datetime" >
                    <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle Font-Size="8.5pt" />
            </asp:GridView>
	 </fieldset> 
 	<asp:UpdatePanel>
 	    <ContentTemplate>
 	    <asp:Literal runat="server" ID="literal_run"></asp:Literal>
 	    </ContentTemplate>
 	</asp:UpdatePanel>
</asp:Content>