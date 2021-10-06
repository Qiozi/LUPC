<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_edit_detail_new.aspx.cs" Inherits="Q_Admin_orders_edit_detail_new" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .zoomin {
            background-image: url('/soft_img/app/zoom-in.jpg');
            background-repeat: no-repeat;
            background-position: -1 0;
        }

        .zoomout {
            background-image: url('/soft_img/app/zoom-in.jpg');
            background-repeat: no-repeat;
            background-position: -23 0;
        }

        fieldset {
            font-size: 8.5pt;
        }

            fieldset table tr td {
                font-size: 8.5pt;
            }

        #table_part_list td {
            font-size: 8.5pt;
        }

        #table_part_list th {
            font-size: 9pt;
        }

        #div_sys_list tr td:hover {
            background: #ccc
        }

        #table_price td {
            font-size: 8.5pt;
        }

        .title {
            font-weight: bold;
            vertical-align: top;
            color: #7D5D4A;
            width: 100px;
            white-space: nowrap;
        }

        .titlePrice {
            font-weight: bold;
            vertical-align: top;
            color: #7D5D4A;
            width: 100px;
            white-space: nowrap;
            text-align: right
        }

        .price1 {
            width: 80px;
            text-align: right;
        }

        .btnRight {
            width: 120px;
            height: 30px;
        }

        #rightButtonArea {
            padding: 5px;
            margin: 5px;
            position: absolute;
            top: 7px;
            right: 5px;
        }

        .style1 {
            height: 26px;
            width: 91px;
        }

        .style2 {
            width: 91px;
        }
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
        $(document).ready(function () {
            $(window).scroll(function () {
                var offsetTop = $(window).scrollTop() + 7 + "px";
                $("#rightButtonArea").animate({ top: offsetTop }, { duration: 500, queue: false });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <%--<input  type="button" value="打开百度" onclick="ShowIframe('百度','http://www.baidu.com',800,450)"><br>  
<input  type="button" value="HTML字符串" onclick="ShowHtmlString('字符串','<B>Hello,PopWin',400,200)"><br>  
<input  type="button" value="信息提示框" onclick="ShowAlert('提示框','<B>Hello,PopWin',200,100)"><br>  
<input  type="button" value="是否确认框" onclick="ShowConfirm('确定','是否删除','Button4','',340,80)"> --%>

    <%= ViewWarn%>
    <input type="hidden" name="htmlOrderCode" id="htmlOrderCode" value="<%= ReqOrderCode %>" />

    <table cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td bgcolor="White" valign="top" width="85%">
                <table cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td valign="top">
                            <span id="personalInfoArea" runat="server">
                                <fieldset class="fieldset2">
                                    <legend>PERSONAL INFORMATION</legend>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td><%= ViewPersonalInfomation %></td>
                                            <td style="text-align: right; vertical-align: baseline">
                                                <input type="button" value="Modify" onclick="ShowIframe('编辑用户信息', '/q_admin/orders_edit_detail_personal_information.aspx?is_new=1&OrderCode=' + $('#htmlOrderCode').val(), 500, 450)"></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </span>

                            <fieldset class="fieldset2">
                                <legend>SHIPPING ADDRESS</legend>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td><%= ViewShippingAddress%></td>
                                        <td style="text-align: right; vertical-align: baseline">
                                            <input type="button" value="Modify" onclick="ShowIframe('Edit Shipping Address', '/q_admin/orders_edit_detail_shipping_address.aspx?is_new=1&OrderCode=' + $('#htmlOrderCode').val(), 500, 350)"></td>
                                    </tr>
                                </table>
                            </fieldset>

                            <span id="creditCardArea" runat="server">
                                <fieldset class="fieldset2">
                                    <legend>CREDIT CARD(Billing Address)</legend>
                                    <table cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td><%= ViewCreditCard%> </td>
                                            <td style="text-align: right; vertical-align: baseline">
                                                <input type="button" value="Modify"
                                                    onclick="ShowIframe('Edit Credit Card Info...', '/q_admin/orders_edit_detail_Credit_Card.aspx?is_new=1&OrderCode=' + $('#htmlOrderCode').val(), 500, 450)"></td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </span>
                            <fieldset class="fieldset2">
                                <legend>OTHER</legend>
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td><%= ViewOtherInfo%></td>
                                        <td style="text-align: right; vertical-align: baseline">
                                            <input type="button" value="Modify"
                                                onclick="ShowIframe('Edit OTHER', '/q_admin/orders_edit_detail_change_other.aspx?is_new=1&OrderCode=' + $('#htmlOrderCode').val(), 500, 600)"></td>
                                    </tr>
                                </table>
                            </fieldset>

                            <fieldset class="fieldset2">
                                <legend>PRODUCT LIST</legend>
                                <div style="text-align: right;">
                                    Part#:<asp:TextBox runat="server" ID="txt_input_part"></asp:TextBox>
                                    <asp:Button runat="server" ID="btn_Add_Part" Text="ADD"
                                        OnClick="btn_Add_Part_Click" /><br />
                                    Part Name:<asp:TextBox runat="server" ID="txt_input_part_name" Columns="80"></asp:TextBox>
                                    Sell$:<asp:TextBox runat="server" ID="txt_input_part_sell"></asp:TextBox>
                                    <asp:Button runat="server" ID="btn_Add_Part_Name" Text="ADD"
                                        OnClick="btn_Add_Part_Name_Click" />
                                </div>
                                <hr size="1" />
                                <asp:ListView runat="server" ID="lv_part_list"
                                    ItemPlaceholderID="itemPlaceHolderid" OnItemCommand="lv_part_list_ItemCommand">
                                    <LayoutTemplate>
                                        <div style="background: #ccc">
                                            <table cellspacing="1" class="table_td_white" style="width: 100%" id="table_part_list">
                                                <tr>
                                                    <th>SKU
                                                    </th>
                                                    <th>Name</th>
                                                    <th>Cost</th>

                                                    <th>Price</th>
                                                    <th>Sold</th>
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
                                            <td style="width: 80px;">
                                                <asp:HiddenField runat="server" ID="_hf_serial_no" Value='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                                <asp:Literal runat="server" ID="_literal_system_code" Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Literal>
                                            </td>
                                            <td><%# DataBinder.Eval(Container.DataItem, "product_name")%></td>
                                            <td style="padding-right: 2px; text-align: right; width: 70px;">
                                                <asp:Literal runat="server" ID="_literal_order_product_cost" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_cost")%>'></asp:Literal></td>
                                            <td style="padding-right: 2px; text-align: right; width: 70px;">
                                                <asp:Literal runat="server" ID="_literal_order_product_price" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_price")%>'></asp:Literal></td>
                                            <td style="width: 70px;">
                                                <asp:TextBox ID="_txt_sold_part" runat="server" CssClass="input" __designer:wfdid="w8" Columns="10" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_sold")%>'></asp:TextBox></td>
                                            <td style="width: 70px;">
                                                <ul class="ul_parent">
                                                    <li>
                                                        <asp:TextBox ID="_txt_sum_part" runat="server" CssClass="input" __designer:wfdid="w9"
                                                            Columns="2" Text='<%# DataBinder.Eval(Container.DataItem,"order_product_sum") %>'></asp:TextBox>
                                                        <div class="float_area">
                                                            <table cellspacing="2" align="left">
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_1" Text="1" CssClass="a_btn" CommandArgument="1" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_2" Text="2" CssClass="a_btn" CommandArgument="2" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_3" Text="3" CssClass="a_btn" CommandArgument="3" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_4" Text="4" CssClass="a_btn" CommandArgument="4" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_5" Text="5" CssClass="a_btn" CommandArgument="5" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_6" Text="6" CssClass="a_btn" CommandArgument="6" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_7" Text="7" CssClass="a_btn" CommandArgument="7" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_8" Text="8" CssClass="a_btn" CommandArgument="8" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_9" Text="9" CssClass="a_btn" CommandArgument="9" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    <td>
                                                                        <asp:LinkButton runat="server" ID="_lb_10" Text="10" CssClass="a_btn" CommandArgument="10" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </li>
                                                </ul>
                                            </td>
                                            <td style="padding-right: 2px; text-align: right; width: 70px;">
                                                <asp:Literal runat="server" ID="literal_sub_total" Text='<%# DataBinder.Eval(Container.DataItem,"subtotal_2") %>'></asp:Literal>
                                            </td>
                                            <td style="width: 70px; text-align: center">
                                                <asp:ImageButton runat="server" ID="imgBtn_delete" ImageAlign="Middle" ImageUrl="~/Q_Admin/Images/exit.gif"
                                                    CommandName="DeletePart" OnClientClick="if(!confirm('Sure?')) return false; else { return true;} "
                                                    CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                                <asp:ImageButton runat="server" ID="imgBtn_save" ImageAlign="Middle" ImageUrl="~/Q_Admin/Images/save_1.gif"
                                                    CommandName="SavePart" CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                </asp:ListView>

                                <!-- sys -->
                                <br />
                                <br />
                                <div style="text-align: right">
                                    eBay ItemID:
                                    <input type="text" name="ebayItemid" maxlength="15" size="40" />System:<asp:TextBox runat="server" ID="txt_sys_sku" Columns="20"></asp:TextBox>
                                    <asp:Button runat="server" ID="btn_add_sys" Text="Add"
                                        OnClick="btn_add_sys_Click" OnClientClick="return addSys($(this));" />
                                    <asp:Button ID="btn_duplicate_sys" runat="server"
                                        OnClick="btn_duplicate_sys_Click" OnClientClick=""
                                        Text="Duplicate Sys To New" />
                                </div>
                                <div id="div_sys_list">
                                    <asp:ListView runat="server" ID="lv_sys_list" ItemPlaceholderID="itemPlaceHolderid"
                                        OnItemCommand="lv_sys_list_ItemCommand"
                                        OnItemDataBound="lv_sys_list_ItemDataBound">
                                        <LayoutTemplate>
                                            <div style="background: #ccc">
                                                <table cellspacing="1" class="table_td_white" style="width: 100%" id="table_part_list">
                                                    <tr>

                                                        <th>SKU
                                                        </th>
                                                        <th>Name</th>
                                                        <th>Cost</th>

                                                        <th>Price</th>
                                                        <th>Sold</th>
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
                                                <%--<td style="width:15px"><asp:Button ID="_btn_view_detail" runat="server"  
                                                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>' 
                                                                                        CommandName="ViewSystemDetail" 
                                                                                        Text="+"/> /td  <>--%>

                                                <td style="width: 80px;">
                                                    <asp:HiddenField runat="server" ID="_hf_serial_no" Value='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>' />
                                                    <asp:Literal runat="server" ID="_literal_system_code" Text='<%# DataBinder.Eval(Container.DataItem, "product_serial_no") %>'></asp:Literal>
                                                </td>
                                                <td><%# DataBinder.Eval(Container.DataItem, "product_name")%></td>
                                                <td style="padding-right: 2px; text-align: right; width: 70px;">
                                                    <asp:Literal runat="server" ID="_literal_order_product_cost" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_cost")%>'></asp:Literal>
                                                </td>
                                                <td style="padding-right: 2px; text-align: right; width: 70px;">
                                                    <asp:Literal runat="server" ID="_literal_order_product_price" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_price")%>'></asp:Literal>
                                                </td>
                                                <td style="width: 70px;">
                                                    <asp:TextBox ID="_txt_sold_part" runat="server" CssClass="input" __designer:wfdid="w8" Columns="10" Text='<%# DataBinder.Eval(Container.DataItem, "order_product_sold")%>'></asp:TextBox></td>
                                                <td style="width: 70px;">
                                                    <ul class="ul_parent">
                                                        <li>
                                                            <asp:TextBox ID="_txt_sum_part" runat="server" CssClass="input" __designer:wfdid="w9"
                                                                Columns="2" Text='<%# DataBinder.Eval(Container.DataItem,"order_product_sum") %>'></asp:TextBox>
                                                            <div class="float_area">
                                                                <table cellspacing="2" align="left">
                                                                    <tr>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_1" Text="1" CssClass="a_btn" CommandArgument="1" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_2" Text="2" CssClass="a_btn" CommandArgument="2" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_3" Text="3" CssClass="a_btn" CommandArgument="3" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_4" Text="4" CssClass="a_btn" CommandArgument="4" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_5" Text="5" CssClass="a_btn" CommandArgument="5" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_6" Text="6" CssClass="a_btn" CommandArgument="6" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_7" Text="7" CssClass="a_btn" CommandArgument="7" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_8" Text="8" CssClass="a_btn" CommandArgument="8" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_9" Text="9" CssClass="a_btn" CommandArgument="9" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                        <td>
                                                                            <asp:LinkButton runat="server" ID="_lb_10" Text="10" CssClass="a_btn" CommandArgument="10" CommandName="SetPartSum"></asp:LinkButton></td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </li>
                                                    </ul>
                                                </td>
                                                <td style="padding-right: 2px; text-align: right; width: 70px;">
                                                    <asp:Literal runat="server" ID="literal_sub_total" Text='<%# DataBinder.Eval(Container.DataItem,"subtotal_2") %>'></asp:Literal>
                                                </td>
                                                <td style="width: 70px; text-align: center;">

                                                    <asp:ImageButton runat="server" ID="imgBtn_delete" ImageAlign="Middle" ImageUrl="~/Q_Admin/Images/exit.gif"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>'
                                                        CommandName="DeleteSys" OnClientClick="if(!confirm('Sure?')) return false; else { return true;} " />
                                                    <asp:ImageButton runat="server" ID="imgBtn_save" ImageAlign="Middle" ImageUrl="~/Q_Admin/Images/save_1.gif"
                                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem,"serial_no") %>'
                                                        CommandName="SaveSys" />

                                                </td>
                                            </tr>
                                            <asp:Panel runat="server" ID="_panel_sys_detail" Visible="false">
                                                <tr>
                                                    <td colspan="9" style="background-color: #ffffff">
                                                        <div style="text-align: right">
                                                            luc sku<asp:TextBox runat="server" ID="_txt_part_sku"></asp:TextBox>
                                                            qty<asp:TextBox runat="server" ID="_txt_part_quantity" MaxLength="1" Text="1" Columns="1" Width="15"></asp:TextBox>
                                                            <asp:Button runat="server" ID="_btn_add_part_to_sys" OnClientClick="" CommandName="AddPartToSys" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "product_serial_no")%>' Text="Add" />
                                                        </div>
                                                        <table cellspacing="1" align="right" width="95%" style="border-top: 1px solid #ccc;">

                                                            <asp:Repeater runat="server" ID="_rpt_sys_detail" OnItemCommand="rpt_ItemCommand">
                                                                <ItemTemplate>
                                                                    <tr>
                                                                        <td style="text-align: center; width: 50px;">
                                                                            <%# DataBinder.Eval(Container.DataItem, "product_serial_no")%>
                                                                        </td>
                                                                        <td><i><%# DataBinder.Eval(Container.DataItem, "product_name")%></i></td>
                                                                        <td style="text-align: right; width: 60px;"><%# DataBinder.Eval(Container.DataItem, "product_current_price")%></td>
                                                                        <td style="color: green; text-align: center; width: 30px;"><%# DataBinder.Eval(Container.DataItem, "part_quantity").ToString() == "1" ? "" : "+"+DataBinder.Eval(Container.DataItem, "part_quantity").ToString()%></td>
                                                                        <td style="text-align: right; width: 60px;"><%# decimal.Parse(DataBinder.Eval(Container.DataItem, "product_current_sold").ToString()) * decimal.Parse( DataBinder.Eval(Container.DataItem, "part_quantity").ToString())%></td>
                                                                        <td style="width: 60px;">
                                                                            <asp:LinkButton runat="server" ID="_lb_sys_detail_remove" Text="Remove" CommandName="DelSysDetail"
                                                                                CommandArgument='<%# DataBinder.Eval(Container.DataItem, "sys_tmp_detail")+"|"+ DataBinder.Eval(Container.DataItem, "sys_tmp_code")+"|"+ DataBinder.Eval(Container.DataItem, "product_serial_no")   %>'
                                                                                OnClientClick="if(!confirm('Sure?')) return false; else { return true;} "></asp:LinkButton>
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
                                <!-- sys end -->
                            </fieldset>

                            <%= ViewPriceArea %>
                            <hr size="1" style="clear: both;" />
                        </td>
                        <td valign="top">
                            <p style="text-align: right;">
                            </p>
                        </td>
                    </tr>
                </table>





                <!-- part list ----begin ---->
                <!-- system list ---- end  ----->
            </td>
            <td valign="top">
                <!-- right begin -->

                <div id="rightButtonArea">
                    <table>
                        <tr>
                            <!--  <td valign="top" style="padding-top: 0px;">
                              <asp:Button runat="server" ID="btnZoomInOut" BorderWidth="0" Width="26" 
                                    Height="25" ToolTip="<- Zoom out" CssClass="zoomin" 
                                    onclick="btnZoomInOut_Click"/>
                               
                            </td> -->
                            <td valign="top" style="background: #f2f2f2; border: 1px solid #ccc;">
                                <table align="center">
                                    <tr>
                                        <td align="right" class="style1">Order#</td>
                                        <td><b><%= ReqOrderCode %></b></td>
                                    </tr>
                                    <tr>
                                        <td align="right" class="style2">Customer#</td>
                                        <td><b><%= ViewCustomerID %></b></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr size="1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button runat="server" ID="btn_prev" CssClass="btnRight" Text="Prev" OnClick="btn_prev_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <input type="button" id="btn_view_order" class='btnRight' value="View Order" onclick='winOpen("sales_order_detail.aspx?order_code=" + <%= Request.QueryString["order_code"].ToString() %>, "order_detail", 1000, 900, 100, 100);' /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <input type="button" class='btnRight' value="Email Order Confirm" onclick='ShowIframe("Email Order Confirm", "email_simple_invoice.aspx?order_code=<%= Request.QueryString["order_code"].ToString() %>& email=<%= SendEmail %>",800,600)'></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <input type="button" class='btnRight' value="Print Invoice" onclick='ShowIframe("Print Invoice", "sale_print_order.aspx?order_code=<%= Request.QueryString["order_code"].ToString() %>",800,600)'></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Button runat="server" CssClass="btnRight" ID="btn_accept_down_invoice" Text="允许客户下载INVOICE"
                                                OnClick="btn_accept_down_invoice_Click" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <input type='button' value='View History (<%= ViewHistoryCount %>)' class='btnRight' onclick="ShowIframe('Order (<%= ReqOrderCode %>) History',' / q_admin / orders_edit_detail_history.aspx ? is_new = 1 & order_code=<%= ReqOrderCode %>',800,450)"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <input type='button' value='Send Message' class='btnRight' onclick="ShowIframe('Order (<%= ReqOrderCode %>) Message',' / q_admin / orders_edit_detail_customer_msg.aspx ? is_new = 1 & order_code=<%= ReqOrderCode %>',800,450);"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr size="1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <input type='button' value='Paypal' class='btnRight' onclick="ShowIframe('Order (<%= ReqOrderCode %>) Paypal',' / q_admin / paypal_card_do_direct_payment.asp ? order_code =<%= ReqOrderCode %>',800,650);"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:ListView runat="server" ID="lv_pay_record" ItemPlaceholderID="itemPlaceholderID">
                                                <LayoutTemplate>
                                                    <table cellpadding="0" cellspacing="0">
                                                        <tr runat="server" id="itemPlaceholderID"></tr>
                                                    </table>
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <tr>
                                                        <td style="color: #666666; text-align: right">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "pay_regdate")%></td>
                                                        <td style="text-align: right; color: #663300">$<%# DataBinder.Eval(Container.DataItem, "pay_cash")%></td>
                                                        <td style="color: #663300">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "pay_record_name") %></td>
                                                    </tr>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>

                </div>
                <%= ViewCustomerMsg%>
                <!-- right end -->
            </td>
        </tr>

    </table>
    <a name="msg"></a>
    <asp:DataList
        ID="dl_msg_list" runat="server" Width="85%" BackColor="White"
        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="0px" CellPadding="4"
        ForeColor="Black" GridLines="Vertical">
        <FooterStyle BackColor="#CCCC99" />
        <AlternatingItemStyle BackColor="White" />
        <ItemStyle BackColor="#F7F7DE" />
        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <ItemTemplate>
            <div style="color: #2E2EFF;">

                <div style="float: left; color: #000">
                    <%# DataBinder.Eval(Container.DataItem,"msg_author").ToString() == "Me" ?"Customer":DataBinder.Eval(Container.DataItem,"msg_author").ToString() %>
                </div>
                <div style="float: right; color: #000">
                    <%# DataBinder.Eval(Container.DataItem,"regdate") %>
                </div>
                <div style="min-height: 40px; color: #2E2EFF; clear: both; vertical-align: middle; padding-left: 2em; margin-top: 5px;">
                    <%# System.Web.HttpUtility.HtmlDecode( DataBinder.Eval(Container.DataItem,"msg_content_text").ToString().Replace("\r\n", "<br/>")) %>
                </div>
            </div>
        </ItemTemplate>
    </asp:DataList>

    <script type="text/javascript">

        //alert("ddds   ".replace(/(^\s*|\s*$)/g, "").length);
        function addSys(e) {

            var itemid = $('input[name=ebayItemid]').val().replace(/(^\s*|\s*$)/g, "");
            if (itemid.length == 12) {
                ShowIframe('eBay system detail', '/q_admin/orders_edit_detail_ebay_item.aspx?itemid=' + itemid + '&OrderCode=' + $('#htmlOrderCode').val(), 800, 600)
                return false;
            }
            else
                return true;
        }
        function getPaypalRecord() {

        }
    </script>
</asp:Content>
