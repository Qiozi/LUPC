<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="orders_ebay_view.aspx.cs" Inherits="Q_Admin_orders_ebay_view" Title="view ebay" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css" media="print">
       .noPrint
        {
            display: none;
        }
     </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="cmd_btn_list" class="noPrint">        
        <input type="button" onclick="window.print();" value="Print" />
        <input type="button" onclick="window.close();" value="Close" />
    </div>
    <div style="height:50px;">&nbps;</div>
    <asp:Repeater runat="server" ID="rpt_order_info">
        <ItemTemplate>
            <table cellspacing="0" id="order_info">
		<tr>
			<td class="title">sales_record_number</td>
			<td><%# DataBinder.Eval(Container.DataItem, "sales_record_number") %></td>
			<td class="title">order_code</td>
			<td style="color:Blue;"><%# DataBinder.Eval(Container.DataItem, "sales_record_number") %></td>
		</tr><tr>
			<td class="title">user_id</td>
			<td ><%# DataBinder.Eval(Container.DataItem, "user_id")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">buyer_fullname</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_fullname")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">buyer_phone_number</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_phone_number")%></td>
			<td class="title">buyer_email</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_email")%></td>
		</tr><tr>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">buyer_address1</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_address1")%></td>
			<td class="title">buyer_address2</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_address2")%></td>
		</tr><tr>
			<td class="title">buyer_city</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_city")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">buyer_province</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_province")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">buyer_postal_code</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_postal_code")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">buyer_country</td>
			<td><%# DataBinder.Eval(Container.DataItem, "buyer_country")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
		<tr>
		  <td>&nbsp;</td>
		  <td>&nbsp;</td>
		  <td>&nbsp;</td>
		  <td>&nbsp;</td>
  </tr>
		<tr>

			<td class="title">item_number</td>
			<td><%# DataBinder.Eval(Container.DataItem, "item_number")%></td>
			<td class="title">custom_label</td>
			<td><%# DataBinder.Eval(Container.DataItem, "custom_label")%></td>
		</tr><tr>
			<td class="title">item_title</td>
			<td colspan="2" style="color: #ff9900;"><%# DataBinder.Eval(Container.DataItem, "item_title")%></td>
			<td style="color:Blue;">(qty: <%# DataBinder.Eval(Container.DataItem, "quantity")%>)</td>
		</tr><tr>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">sale_date</td>
			<td><%# DataBinder.Eval(Container.DataItem, "sale_date")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">checkout_date</td>
			<td><%# DataBinder.Eval(Container.DataItem, "checkout_date")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">paid_on_date</td>
			<td><%# DataBinder.Eval(Container.DataItem, "paid_on_date")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">shipping_service</td>
			<td><%# DataBinder.Eval(Container.DataItem, "shipping_service")%></td>
			<td class="title">shipped_on_date</td>
			<td><%# DataBinder.Eval(Container.DataItem, "shipped_on_date")%></td>
		</tr><tr>
		  <td class="title">payment_method</td>
		  <td><%# DataBinder.Eval(Container.DataItem, "payment_method")%></td>
		  <td class="title">paypal_transaction_id</td>
		  <td><%# DataBinder.Eval(Container.DataItem, "paypal_transaction_id")%></td>
  </tr><tr>

			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">sale_price</td>
			<td class="price"><%# DataBinder.Eval(Container.DataItem, "sale_price")%></td>
			<td class="price_unit"><%# DataBinder.Eval(Container.DataItem, "sale_price_unit")%></td>
			<td></td>
		</tr><tr>

			<td class="title">shipping_and_handling</td>
			<td class="price"><%# DataBinder.Eval(Container.DataItem, "shipping_and_handling")%></td>
			<td class="price_unit"><%# DataBinder.Eval(Container.DataItem, "shipping_and_handling_unit")%></td>
			<td></td>
		</tr><tr>
			<td class="title">insurance</td>
			<td class="price"><%# DataBinder.Eval(Container.DataItem, "insurance")%></td>
			<td class="price_unit"><%# DataBinder.Eval(Container.DataItem, "insurance_unit")%></td>
			<td></td>
		</tr><tr>
			<td class="title">cash_on_delivery_fee</td>
			<td class="price"><%# DataBinder.Eval(Container.DataItem, "cash_on_delivery_fee")%></td>
			<td class="price_unit"><%# DataBinder.Eval(Container.DataItem, "cash_on_delivery_fee_unit")%></td>
			<td></td>
		</tr><tr>

			<td class="title">total_price</td>
			<td style="color:Blue;" class="price"><%# DataBinder.Eval(Container.DataItem, "total_price")%></td>
			<td class="price_unit"><%# DataBinder.Eval(Container.DataItem, "total_price_unit")%></td>
			<td></td>
		</tr><tr>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">feedback_left</td>
			<td><%# DataBinder.Eval(Container.DataItem, "feedback_left")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">feedback_received</td>
			<td><%# DataBinder.Eval(Container.DataItem, "feedback_received")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">notes_to_yourself</td>
			<td><%# DataBinder.Eval(Container.DataItem, "notes_to_yourself")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>

			<td class="title">cash_on_delivery_option</td>
			<td><%# DataBinder.Eval(Container.DataItem, "cash_on_delivery_option")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr><tr>
			<td class="title">transaction_id</td>
			<td><%# DataBinder.Eval(Container.DataItem, "transaction_id")%></td>
			<td>&nbsp;</td>
			<td>&nbsp;</td>
		</tr>
	</table>
        </ItemTemplate>
    </asp:Repeater>
    <hr size="1" />
    <h3><asp:Label runat="server" ID="lbl_prod_type"></asp:Label></h3>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <script type="text/javascript">
        $().ready(function(){
            $('td.title').css("font-weight","bold").each(function(){
                $(this).html($(this).html().replace(new RegExp("_", "g")," "));
                //alert($(this).html());
            });
            $('#order_info td').css("border-bottom", "1px dotted #cccccc").each(function(){
                if($(this).html()=="")
                    $(this).html("&nbsp;");
            });
            
            $('td.price').css("text-align", "right");
            
            $("#cmd_btn_list").floatdiv("lefttop");
        });
    </script>
</asp:Content>

