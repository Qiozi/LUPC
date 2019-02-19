<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" ValidateRequest="false" CodeFile="EditPartDetail.aspx.cs" Inherits="Q_Admin_EditPartDetail" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        table tr td {
            font-size: 8.5pt;
        }

        input {
            font-size: 8.5pt;
            color: #5D5DBE
        }

        .style1 {
            width: 105px;
        }

        .style2 {
            height: 24px;
        }

        .style3 {
            width: 105px;
            height: 24px;
        }
    </style>
    <script type="text/javascript" src="../js_css/jquery-1.9.1.js"></script>
    <script type="text/javascript" src="../js_css/jquery-ui-1.10.2.custom.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="text-align: center;">

        <div style="padding: 1em; background: #f2f2f2; border: 1px solid #ccc; margin-bottom: 1em; text-align: left">
           <%-- SKU, MFP#:<asp:TextBox runat="server" ID="txt_SetSKU"></asp:TextBox>
            <asp:Button runat="server" ID="btn_SetSKU" Text="Go" OnClick="btn_SetSKU_Click"></asp:Button>--%>
           SKU, MFP#:<input type="text" /><a class="" onclick="window.location.href='/q_admin/editPartDetail.aspx?id='+ $(this).prev().val();">Go</a>
            <div style="float: right;">
                <asp:Literal runat="server" ID="litPreNextButton"></asp:Literal>
            </div>
        </div>

        <asp:Button runat="server" ID="btn_save" Text="Save" OnClientClick="ParentLoadWait();"
            OnClick="btn_save_Click" />


    </div>
    <hr size="1" style="clear: both;" />
    <asp:DropDownList runat="server" ID="ddl_category_1" AutoPostBack="true"
        OnSelectedIndexChanged="ddl_category_1_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList runat="server" ID="ddl_category_2" AutoPostBack="true"
        OnSelectedIndexChanged="ddl_category_2_SelectedIndexChanged">
    </asp:DropDownList>
    <asp:DropDownList runat="server" ID="ddl_category_3" AutoPostBack="true"
        OnSelectedIndexChanged="ddl_category_3_SelectedIndexChanged">
    </asp:DropDownList>

    <div style="background: #f2f2f2; padding: 5px; text-align: center; border: 1px solid #C7C7F1; font-size: 10pt; font-weight: bold;">
        <asp:Label runat="server" ID="lbl_current_category_title" ForeColor="Blue"></asp:Label>
        &nbsp;::::
                <asp:Label ID="lbl_sku" runat="server" Font-Bold="True" Text="SKU"></asp:Label>
    </div>

    <asp:Panel runat="server" ID="panel_part_info">
        <table width="100%">
            <tr>
                <td align="center" colspan="4" style="border-bottom: 1px solid #C7C7F1; font-size: 10pt; font-weight: bold;"></td>

                <td style="border-left: 1px solid #C7C7F1; padding: 3px;" rowspan="21"
                    valign="top">
                    <asp:Panel runat="server" ID="panel_match_sku">
                        <ul class="ul_parent">
                            <li class="displayBlockTitle">Match SKU
                                                <div style="border: 1px solid #ff9900; width: 400px; height: 500px; left: -300; text-align: center; clear: both">
                                                    <asp:Button runat="server" ID="btn_save_match_sku" Text="Save " OnClick="btn_save_match_sku_Click" />
                                                    <hr size="1" />
                                                    <table class="table_small_font" cellpadding="0" cellspacing="0">
                                                        <asp:Repeater runat="server" ID="gv_inc_info">
                                                            <ItemTemplate>
                                                                <tr>
                                                                    <td style="border-bottom: 1px solid #DADAF6; padding-left: 10px;">
                                                                        <asp:HiddenField runat="server" ID="_hf_other_inc_id" Value='<%# DataBinder.Eval(Container.DataItem, "id")%>' />
                                                                        <%# DataBinder.Eval(Container.DataItem, "text")%></td>
                                                                    <td style="border-bottom: 1px solid #DADAF6">
                                                                        <asp:TextBox runat="server" ID="_txt_other_inc_sku" Text='<%# DataBinder.Eval(Container.DataItem, "other_inc_sku") %>'></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </ItemTemplate>
                                                        </asp:Repeater>
                                                    </table>
                                                </div>
                            </li>
                        </ul>
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table_small_font">
                            <Columns>
                                <asp:BoundField DataField="inc_name" HeaderText="INC Name" />
                                <asp:BoundField DataField="Price" HeaderText="Price$/Cost$"
                                    DataFormatString="{0:$###,###.00}">
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Qty" HeaderText="Qty" />
                                <asp:BoundField DataField="last_regdate" HeaderText="DateTime" />
                            </Columns>
                        </asp:GridView>
                    </asp:Panel>

                    <div>
                        <iframe id="iframeSpecific2" name="iframeSpecific2" src="/q_admin/manager/product/Specifics.aspx?sku=<%=ReqSku %>" style="width: 98%; height: 400px; border: 1px solid #ccc;"></iframe>
                    </div>
                </td>
            </tr>
            <tr>
                <td width="120">Special Cash</td>
                <td style="width: 250px;">
                    <asp:TextBox runat="server" ID="txt_product_current_special_cash_price"
                        Columns="20" MaxLength="50" ReadOnly="true" CssClass="input_right"></asp:TextBox>
                    discount:
                    <asp:Label ID="lbl_discount" runat="server" Text="0" ForeColor="#FF9900"></asp:Label>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                <td style="width: 80px">Cost</td>
                <td class="style1" width="200">
                    <asp:TextBox runat="server" ID="txt_product_current_cost"
                        Columns="20" MaxLength="50" CssClass="input_right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Price</td>
                <td style="width: 150px;">
                    <asp:TextBox runat="server" ID="txt_product_current_price"
                        Columns="20" MaxLength="50" CssClass="input_right" Enabled="False"></asp:TextBox>
                </td>
                <td style="width: 80px">
                    <b>adjustment</b></td>
                <td class="style1">
                    <asp:TextBox runat="server" ID="txt_adjustment"
                        Columns="20" MaxLength="50" CssClass="input_right"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Store Sum</td>
                <td>
                    <asp:TextBox runat="server" ID="txt_product_store_sum" Columns="20"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td>adjustment end date</td>
                <td>
                    <asp:TextBox runat="server" ID="txtAdjustmentEndDate" Columns="20"
                        MaxLength="50" /></td>
            </tr>
            <tr>
                <td>Size</td>
                <td style="color: #ff9900">
                    <asp:DropDownList ID="ddl_size" runat="server">
                    </asp:DropDownList></td>
                <td>priority</td>
                <td class="style1">
                    <asp:TextBox runat="server" ID="txt_priority"
                        Columns="20" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td colspan="3">
                    <asp:CheckBox runat="server" ID="cb_new" Text="New" />
                    &nbsp;
                    <asp:CheckBox runat="server" ID="cb_hot" Text="Hot" />
                    &nbsp;
                    <asp:CheckBox runat="server" ID="cb_split_line" Text="Split Line" />
                    &nbsp;
                    <asp:CheckBox runat="server" ID="cb_non" Text="Selected None" />
                    &nbsp;
                    <asp:CheckBox runat="server" ID="cb_export" Text="export" />
                    &nbsp;
                    <asp:CheckBox runat="server" ID="cb_showit" Text="Showit" />
                    <br />
                    <asp:CheckBox ID="cb_fixed" runat="server" Text="Fixed" />
                    <asp:CheckBox ID="cb_for_sys" runat="server" Text="for sys" />
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td class="style1">&nbsp;</td>
            </tr>
            <tr>
                <td>Short Name</td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txt_short_name" Columns="80"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Middle Name</td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txt_middle_name" Columns="80"
                        MaxLength="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Long Name</td>
                <td colspan="3">
                    <asp:TextBox runat="server" ID="txt_long_name" Columns="50"
                        MaxLength="200" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>eBay Name</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_product_ebay_name" runat="server" Columns="50"
                        MaxLength="80" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>eBay Name2</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_product_ebay_name_2" runat="server" Columns="50"
                        MaxLength="80" TextMode="MultiLine" Rows="5"></asp:TextBox>
                    only notebook</td>
            </tr>
            <tr>
                <td>eBay describe</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_part_ebay_describe" runat="server" Columns="50"
                        MaxLength="254" TextMode="MultiLine" Rows="5"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>name for sys&nbsp;</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_keywords0" runat="server" Columns="80" MaxLength="30"
                        Width="212px"></asp:TextBox>
                    * system title</td>
            </tr>
            <tr>
                <td>Keywords</td>
                <td colspan="3">
                    <asp:TextBox ID="txt_keywords" runat="server" Columns="80" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>UPC</td>
                <td>
                    <asp:TextBox ID="txt_UPC" runat="server" Columns="30" MaxLength="50"></asp:TextBox>
                </td>
                <td>Weight</td>
                <td>
                    <asp:TextBox ID="txt_weight" runat="server" Columns="20" MaxLength="50"></asp:TextBox></td>
            </tr>

            <tr>
                <td>MFP</td>
                <td>
                    <asp:TextBox runat="server" ID="txt_producter"
                        Columns="30" MaxLength="50"></asp:TextBox>
                </td>
                <td>supplier_sku</td>
                <td class="style1">
                    <asp:TextBox runat="server" ID="txt_supplier_sku" Columns="20" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>MFP#</td>
                <td>
                    <asp:TextBox runat="server" ID="txt_manufactuere_part_number" Columns="30"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td>Model</td>
                <td class="style1">
                    <asp:TextBox ID="txt_model" runat="server" Columns="20" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style2">MFP URL</td>
                <td class="style2">
                    <asp:TextBox runat="server" ID="txt_producter_url" Columns="30"
                        MaxLength="50"></asp:TextBox>
                </td>
                <td class="style2">screen_size</td>
                <td class="style3">
                    <asp:TextBox ID="txt_screen_size" runat="server" Columns="20" MaxLength="50"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Other SKU:</td>
                <td>
                    <asp:TextBox ID="txt_other_sku" runat="server" Columns="30" MaxLength="50"></asp:TextBox>
                </td>
                <td>gallery sum:</td>
                <td class="style1">
                    <asp:TextBox ID="txt_product_img_sum" runat="server" Columns="20"
                        MaxLength="50">0</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Price SKU:</td>
                <td>
                    <asp:TextBox ID="txt_price_sku" runat="server" Columns="30" MaxLength="50"></asp:TextBox>
                </td>
                <td>P SKU Quantity</td>
                <td class="style1">
                    <asp:TextBox ID="txt_price_sku_quantity" runat="server" Columns="20"
                        MaxLength="50">0</asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel runat="server" ID="panel_comment">
        <table>
            <tr>
                <td>
                    <asp:Label runat="server" ID="lbl_img_sku" Text="Image Sku:"></asp:Label><asp:TextBox runat="server" ID="txt_img_sku"></asp:TextBox>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" ID="lbl_img_quantity" Text="Image Quantity:"></asp:Label><asp:TextBox runat="server" ID="txt_img_quantity"></asp:TextBox>

                    <div>
                        <asp:Label ID="lblWarn" runat="server" Text="Label"></asp:Label>
                    </div>

                    <br />
                    <asp:TextBox runat="server" ID="txt_desc" Columns="100" Rows="10"
                        TextMode="MultiLine"></asp:TextBox>
                    <br />
                    <b>Short Comment(Max length < 1800 )</b>
                    <br />
                    <asp:TextBox runat="server" ID="txt_desc_short" Columns="100" Rows="10"
                        TextMode="MultiLine"></asp:TextBox>
                </td>

            </tr>
        </table>
    </asp:Panel>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

    <script type="text/javascript">
        $(function () {
            $("#<%=txtAdjustmentEndDateClientID %>").datepicker({
                numberOfMonths: 2,
                showButtonPanel: true
            });

        });
    </script>
</asp:Content>

