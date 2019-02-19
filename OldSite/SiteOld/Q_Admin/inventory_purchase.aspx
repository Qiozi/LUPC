<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="inventory_purchase.aspx.cs" Inherits="Q_Admin_inventory_purchase" Title="Untitled Page"  ResponseEncoding="GB2312" EnableEventValidation ="false"%>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script type="text/javascript" >
   function getPorductSKU()
    {
	  var result = window.showModalDialog("get_product_skus.asp", '', "dialogLeft:200px; dialogTop:230px; dialogWidth:660px; dialogHeight:650px; status:no");
	  //  var result = window.open("get_product_skus.asp", '', "dialogLeft:200px; dialogTop:230px; dialogWidth:660px; dialogHeight:650px; status:no");
	    if(result != null)
	    {	
		    document.getElementById("ctl00_ContentPlaceHolder1_txtpurchase_product_list").value = result;
	    }
    }
    </script>
    <uc1:Navigation ID="Navigation1" runat="server" />

   
    <table style="width: 100%" cellpadding="0" cellspacing="0">
        <tr>
            <td style="width: 40%" valign="top">
             <asp:Panel runat="server" ID="panel1"  SkinID="back_blue">
                    <asp:panel runat="server" ID="panel_title1" SkinID="panel_title1">
                         &nbsp;&nbsp;&gt;&gt; store                            
                    </asp:panel> 
                    <table >
		                        <tr>
			                        <td  style="WIDTH: 128px"> invoice#
			                        </td>
			                        <td>
			                            
				                        <anthem:TextBox runat="Server" id="txtpurchase_invoice" CssClass="input"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> net_amount
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_net_amount" CssClass="input"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> gst
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_gst" CssClass="input"></anthem:TextBox>
                                        %</td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> pst
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_pst" CssClass="input"></anthem:TextBox>
                                        %</td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> paid_amount
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_paid_amount" CssClass="input"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> check_no
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_check_no" CssClass="input"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> bank
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_bank" CssClass="input"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> date
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_date" CssClass="input"  onFocus="calendar()"></anthem:TextBox>
			                        </td>
		                        </tr>
		                       
		                        <tr>
			                        <td  style="WIDTH: 128px"> vendor</td>
			                        <td>
				                        <anthem:DropDownList runat="Server" id="ddl_vendor_serial_no" CssClass="input"></anthem:DropDownList>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> staff</td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtstaff_serial_no" CssClass="input"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr>
			                        <td  style="WIDTH: 128px"> product_list
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_product_list" CssClass="input"></anthem:TextBox>
                                        <input type="button" value="..." onClick="getPorductSKU();"></td>
		                        </tr> <tr>
			                        <td  style="WIDTH: 128px"> note
			                        </td>
			                        <td>
				                        <anthem:TextBox runat="Server" id="txtpurchase_note" CssClass="input" Columns="40" Rows="5" TextMode="MultiLine"></anthem:TextBox>
			                        </td>
		                        </tr>
		                        <tr><td colspan="2" align="center"><asp:Label Runat="server" ID="lblNote" CssClass="note"></asp:Label><anthem:Button CssClass="btn" runat="server" id="btn_save" text="add" OnClick="btn_save_Click1" />
                    </table></asp:Panel> 
            </td>
            <td valign="top" style="width: 5px">
                &nbsp;</td>
            <td valign="top" >
                <anthem:DataGrid ID="dg_purchase" SkinID="dg1" runat="server" OnItemCommand="dg_purchase_ItemCommand" Width="100%" AllowPaging="True" AutoGenerateColumns="False" PageSize="15" OnPageIndexChanged="dg_purchase_PageIndexChanged">
                     <HeaderStyle CssClass="trTitle_2"  />
                    <ItemStyle CssClass="tdItem_2" />
                    <Columns>
                        <asp:BoundColumn DataField="purchase_serial_no" HeaderText="ID">
                           <itemstyle cssclass="displayNone" /> 
                            <headerstyle cssclass="displayNone" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="purchase_invoice" HeaderText="Invoice#"></asp:BoundColumn>
                        <asp:BoundColumn DataField="vendor_serial_no" HeaderText="compnay name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="purchase_date" HeaderText="date" DataFormatString="{0:dd/MM/yyyy}" ></asp:BoundColumn>
                        <asp:BoundColumn DataField="purchase_net_amount" HeaderText="net amount"></asp:BoundColumn>
                        <asp:BoundColumn DataField="purchase_paid_amount" HeaderText="paid amount"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="Select" Text="Pay"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="Select" Text="SCAN"></asp:ButtonColumn>
                        <asp:ButtonColumn CommandName="Select" Text="Modify"></asp:ButtonColumn>
                    </Columns>
                    <SelectedItemStyle BackColor="#F2F2F2" ForeColor="#FF9900" />
                    <AlternatingItemStyle BackColor="#EEEEEE" />
                    <PagerStyle Mode="NumericPages" />
                </anthem:DataGrid></td>
        </tr>
        <tr>
            <td style="width: 40%; height: 5px" valign="top">
            </td>
            <td style="width: 5px" valign="top">
            </td>
            <td valign="top" >
            </td>
        </tr>
        <tr>
            <td style="width: 40%" valign="top">
             <asp:Panel runat="server" ID="panel2"  SkinID="back_blue" Width="100%">
                    <asp:panel runat="server" ID="panel3" SkinID="panel_title1" Width="100%">
                         &nbsp;&nbsp;&gt;&gt; Scan in           
                    </asp:panel> 
                    <table width="100%">
                            <tr>
                                <td style="width: 63px">
                                    Invoice
                                </td>
                                    <td>
                                        <anthem:Label ID="lbl_invoice" runat="server"></anthem:Label></td>
                            </tr>
                        <tr>
                            <td style="width: 63px">
                                Vendor</td>
                            <td>
                                <anthem:Label ID="lbl_vendor" runat="server"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td style="height: 16px; width: 63px;">
                                Product</td>
                            <td style="height: 16px"><anthem:DropDownList runat="server" id="ddl_product_serial_no" CssClass="input">
                            </anthem:DropDownList></td>
                        </tr>
                        <tr>
                            <td style="width: 63px">
                                Cost</td>
                            <td>
                                <anthem:TextBox ID="txt_product_in_cost" runat="server" CssClass="input"></anthem:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 63px">
                                保修终止日期</td>
                            <td>
                                <anthem:TextBox ID="txt_product_in_end_date" runat="server" CssClass="input" onfocus="calendar()"></anthem:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 63px">
                                number:
                                <anthem:TextBox ID="txt_number" runat="server" Columns="8" CssClass="input"></anthem:TextBox>
                                <anthem:Button ID="btn_bring" runat="server" OnClick="btn_bring_Click" Text="产生编号" />
                            </td>
                            <td>
                                <anthem:TextBox ID="txt_product_sns" runat="server" Columns="30" CssClass="input"
                                    Rows="8" TextMode="MultiLine" AutoCallBack="True" OnTextChanged="txt_product_sns_TextChanged"></anthem:TextBox>
                                <anthem:Label ID="lbl_product_count" runat="server"></anthem:Label></td>
                        </tr>
                        <tr>
                            <td align="center" colspan="2">
                 <anthem:Button ID="btn_save_scan_in" runat="server" Text="Save" OnClick="btn_save_scan_in_Click" /></td>
                        </tr>
                    </table>
                 </asp:Panel>
            
            </td>
            <td style="width: 5px" valign="top">
            </td>
            <td valign="top">
            <asp:Panel runat="server" ID="panel4"  SkinID="back_blue" Width="100%">
                    <asp:panel runat="server" ID="panel5" SkinID="panel_title1" Width="100%">
                         &nbsp;&nbsp;&gt;&gt; Pay 
                    </asp:panel> 
            </asp:Panel>
                <anthem:Button ID="btn_new_pay" runat="server" Text="New" OnClick="btn_new_pay_Click" />
                <anthem:Button ID="btn_save_pay" runat="server" Text="Save" OnClick="btn_save_pay_Click" /><br />
                <anthem:DataGrid ID="dg_purchase_pay" runat="server" AutoGenerateColumns="False" Width="100%" SkinID="dg1" OnItemDataBound="dg_purchase_pay_ItemDataBound">
                    <HeaderStyle CssClass="trTitle_3"  />
                    <ItemStyle CssClass="tdItem_2" />
                    <Columns>
                        <asp:BoundColumn DataField="purchase_pay_serial_no" HeaderText="ID">
                                <headerstyle cssclass="displayNone" />
                                <itemstyle cssclass="displayNone" />
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="amount">
                            <itemtemplate>
<anthem:TextBox id="_txt_amount" runat="server" __designer:dtid="1125899906842730" CssClass="input" Columns="10" __designer:wfdid="w16"></anthem:TextBox>
</itemtemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="pay method">
                            <itemtemplate>
<anthem:DropDownList id="_ddl_pay_method" runat="server" __designer:dtid="1125899906842726" CssClass="input" __designer:wfdid="w20"></anthem:DropDownList>
</itemtemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="check#">
                            <itemtemplate>
<anthem:TextBox id="_txt_check_code" runat="server" __designer:dtid="1125899906842730" CssClass="input" Columns="10" __designer:wfdid="w17"></anthem:TextBox>
</itemtemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="date">
                            <itemtemplate>
<anthem:TextBox id="_txt_date" runat="server"  onfocus="calendar()" __designer:dtid="1125899906842730" CssClass="input" Columns="10" __designer:wfdid="w18"></anthem:TextBox>
</itemtemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="balance">
                            <itemtemplate>
<anthem:TextBox id="_txt_balance" runat="server" __designer:dtid="1125899906842730" CssClass="input" Columns="10" __designer:wfdid="w19"></anthem:TextBox>
</itemtemplate>
                        </asp:TemplateColumn>
                    </Columns>
                </anthem:DataGrid></td>
        </tr>
    </table>
  
</asp:Content>

