<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sale_stat_month_report_sub.aspx.cs" Inherits="Q_Admin_sale_stat_month_report_sub" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript" src="../js/helper.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>

     <div style="text-align:center" >
            
                 Year<asp:DropDownList runat="server" ID="ddl_year"></asp:DropDownList>
                 Month<asp:DropDownList runat="server" ID="ddl_month"></asp:DropDownList>
                    <asp:Button runat="server" ID="btn_submit" Text="Go" 
                        onclick="btn_submit_Click" />
                &nbsp;<asp:Button runat="server" ID="btn_export_excel" Text="Export Excel" 
                        onclick="btn_export_excel_Click" />
                &nbsp;<input type="button" id="btn_change_priority" value="Change Priority" 
                        onclick="js_callpage('change_state_priority.aspx');" />
                </div>
                <hr size="1" />
                    Count:  <asp:Label runat="server" ID="lbl_order_count" Text="0" 
            Font-Bold="True" Font-Size="11pt"></asp:Label>
                    <span style="margin: 0px 10px 0px 10px">AMNT Total:  
        <asp:Label runat="server" ID="lbl_amnt_total" Text = "$0.00" Font-Bold="True" 
            Font-Size="11pt"></asp:Label></span>
                    TAX Total:  <asp:Label runat="server" ID="lbl_tax_total" 
            Text = "$0.00" Font-Bold="True" Font-Size="11pt"></asp:Label>
                &nbsp;&nbsp;&nbsp; 
        <br />
                Gst Total:  
        <asp:Label runat="server" ID="lbl_gst_total" 
            Text = "$0.00" Font-Bold="True" Font-Size="11pt" ForeColor="#FF6600"></asp:Label>
                &nbsp;&nbsp;&nbsp; PST Total:  
        <asp:Label runat="server" ID="lbl_pst_total" 
            Text = "$0.00" Font-Bold="True" Font-Size="11pt" ForeColor="#FF6600"></asp:Label>
                &nbsp;&nbsp;&nbsp; HST Total:  
        <asp:Label runat="server" ID="lbl_hst_total" 
            Text = "$0.00" Font-Bold="True" Font-Size="11pt" ForeColor="#FF6600"></asp:Label>
                &nbsp;&nbsp;&nbsp; TAXABLE Total:  
        <asp:Label runat="server" ID="lbl_taxable_total" 
            Text = "$0.00" Font-Bold="True" Font-Size="11pt" ForeColor="Maroon"></asp:Label>
                <hr size="1" />
                <ul style="width: 1160px" id="ul_table_heard">  
                <asp:Repeater runat="server" ID="rpt_order_list" 
                        onitemdatabound="rpt_order_list_ItemDataBound" 
                        onitemcommand="rpt_order_list_ItemCommand">
                    <HeaderTemplate>
                        <li style="width: 100%; clear: left;">
                            <ul class="ul_row">
                                <li style="width: 40px;background-color:#DAB5A2;">ID</li>
                                <li style="width:60px;background-color:#DAB5A2;">ORDER#</li>      
                                <li style="width:60px;background-color:#DAB5A2;">DATE</li>  
                                <li style="width: 180px;text-align:center;background-color:#DAB5A2;">PAY</li>
                                <li style="width:180px;background-color:#DAB5A2;">NAME</li>
                                <li style="width:120px; text-align:center;background-color:#DAB5A2;">SHIPPING STATE</li>
                                <li style="width: 40px;text-align:center;background-color:#DAB5A2;">CUT#</li>
                                <li style="width:80px; text-align:right;background-color:#DAB5A2;">AMNT$</li>
                                <li style="width:80px; text-align:right;background-color:#DAB5A2;">TAX$</li>
                                <li style="width:60px; text-align:right;background-color:#DAB5A2;">GST$</li>
                                <li style="width:60px; text-align:right;background-color:#DAB5A2;">PST$</li>
                                <li style="width:60px; text-align:right;background-color:#DAB5A2;">HST$</li>
                                <li style="width:80px; text-align:right;background-color:#DAB5A2;">TAXABLE$</li>
                                <li style="width:60px;text-align:center;background-color:#DAB5A2;">EXPORT</li>
                            </ul>
                        </li>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li>
                            <ul >
                                <li style="width: 40px"> 
                                
                                    <asp:HiddenField runat="server" ID="_hf_priority" Value='<%# DataBinder.Eval(Container.DataItem, "priority") %>' />
                                    <asp:HiddenField runat="server" ID="_hf_country" Value='<%# DataBinder.Eval(Container.DataItem, "country") %>' />
                                    <div onclick="OpenOrderDetail('<%# DataBinder.Eval(Container.DataItem, "order_code") %>')" style="cursor:pointer"><asp:Literal runat="server" ID="_lt_id" Text='<%# DataBinder.Eval(Container.DataItem, "order_helper_serial_no")%>'></asp:Literal></div> </li>
                                <li style="width:60px">&nbsp; <asp:Literal runat="server" ID="_lt_order_code" Text='<%# DataBinder.Eval(Container.DataItem, "order_code")%>'></asp:Literal></li>
                                 <li style="width:60px; text-align:center;"><%# DataBinder.Eval(Container.DataItem, "order_date") %></li>  
                                <li style="width:180px">&nbsp;<asp:Literal runat="server" ID="_lt_pay_method" Text='<%# DataBinder.Eval(Container.DataItem, "pay_method")%>'></asp:Literal></li>
                                <li style="width:180px"><div style="cursor:pointer" onclick="winOpen('sales_customer_history.aspx?customer_id=<%# DataBinder.Eval(Container.DataItem, "customer_serial_no")%>','order_history', 1000, 600, 300, 300)">&nbsp;<asp:Literal runat="server" ID="_lt_name" Text='<%# DataBinder.Eval(Container.DataItem, "name")%>'></asp:Literal></div></li>
                                <li style="width:120px">&nbsp;<asp:Literal runat="server" ID="_lt_shipping_state" Text='<%# DataBinder.Eval(Container.DataItem, "customer_shipping_state")%>'></asp:Literal></li>
                                <li style="width: 40px" >&nbsp;<asp:Literal runat="server" ID="_lt_customer_serial_no" Text='<%# DataBinder.Eval(Container.DataItem, "customer_serial_no")%>'></asp:Literal></li>
                                <li style="width: 80px;text-align:right; " class="gard"> $<asp:Literal runat="server" ID="_lt_grand_total" Text='<%# DataBinder.Eval(Container.DataItem, "grand_total")%>'></asp:Literal></li>
                                <li style="width: 80px;text-align:right; " class="gard">
                                    $<asp:Literal runat="server" ID="_lt_tax_charge" Text='<%# DataBinder.Eval(Container.DataItem, "tax_charge")%>'></asp:Literal>
                                    </li>
                                <li style="width:60px; text-align:right; " class="Wheat">$<asp:Literal runat="server" ID="_lt_gst" Text='<%# DataBinder.Eval(Container.DataItem, "GST")%>'></asp:Literal></li>
                                <li style="width:60px; text-align:right; " class="Wheat">$<asp:Literal runat="server" ID="_lt_pst" Text='<%# DataBinder.Eval(Container.DataItem, "PST")%>'></asp:Literal></li>
                                <li style="width:60px; text-align:right; " class="Wheat">$<asp:Literal runat="server" ID="_lt_hst" Text='<%# DataBinder.Eval(Container.DataItem, "HST")%>'></asp:Literal></li>
                                <li style="width:80px; text-align:right; " class="Gold">$<asp:Literal runat="server" ID="_lt_taxable_total" Text='<%# DataBinder.Eval(Container.DataItem, "TAXABLE_total")%>'></asp:Literal></li>
                                <li style="width: 60px"><asp:CheckBox runat="server" ID="_cb_export_checked" Checked='<%# DataBinder.Eval(Container.DataItem, "tax_export").ToString()=="1" ? true:false%>' OnCheckedChanged="CheckBox1_CheckedChanged" AutoPostBack="true"/></li>
                           </ul>                
                        </li>                        
                    </ItemTemplate>
                </asp:Repeater>
                </ul>
                <div style="clear:both; display:none" id="export_info">
                    
                    <asp:GridView runat="server" ID="gridView_export_info" 
                        AutoGenerateColumns="False" CellPadding="4" Font-Size="8pt" ForeColor="#333333" 
                        GridLines="None" onrowdatabound="gridView_export_info_RowDataBound">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField DataField="text" HeaderText="State">
                                <ItemStyle Font-Size="8pt" />
                            </asp:BoundField>
                            <asp:BoundField DataField="sum" HeaderText="sum">
                                <ItemStyle Font-Size="8pt" HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="gst" HeaderText="gst" 
                                DataFormatString="{0:###,###.00}">
                                <ItemStyle Font-Size="8pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="pst" HeaderText="pst" 
                                DataFormatString="{0:###,###.00}">
                                <ItemStyle Font-Size="8pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="hst" HeaderText="hst" 
                                DataFormatString="{0:###,###.00}">
                                <ItemStyle Font-Size="8pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="TAX-TOTAL" HeaderText="TAX-TOTAL" 
                                DataFormatString="{0:###,###.00}">
                                <ItemStyle BackColor="#FFCC66" Font-Size="8pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="taxable_total" HeaderText="taxable_total" 
                                DataFormatString="{0:###,###.00}">
                                <ItemStyle Font-Size="8pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                            <asp:BoundField DataField="AMNT-TOTAL" HeaderText="AMNT-TOTAL" 
                                DataFormatString="{0:###,###.00}">
                                <ItemStyle BackColor="#FF9900" Font-Size="8pt" HorizontalAlign="Right" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
                <asp:Literal runat="server" ID="literal_run_script"></asp:Literal>
    </div>
    </form>
</body>
</html>
