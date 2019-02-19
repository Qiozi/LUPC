<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_part_rebate.aspx.cs" Inherits="Q_Admin_product_part_rebate" Title="Rebate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div>
    <p><b>[<asp:Label runat="server" ID="lbl_sku"></asp:Label>]</b></p>
    <p><asp:Label runat="server" ID="lbl_part_name"></asp:Label></p>
    <p><i style="color:green;">MFP#: <asp:Label runat="server" ID="lbl_mfp"></asp:Label></i></p>
</div>
<hr size="1" />
   <b>Step 1.</b> 
    <div style="padding-left: 80px;">
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="Button_upload" runat="server"
            Text="Upload" onclick="Button_upload_Click" />
    </div>
    <div style='border-bottom: 1px dotted #cccccc;'></div>
   <b>Step 2. </b> 
    <table style="margin-left:5px;">
        <tr>
            <td>Begin Date:</td>
            <td><asp:TextBox ID="txt_begin_date" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td>
                <ul class="ul_parent">
                    <li> <span class="displayBlockTitle"><img src="http://www.lucomputers.com/images/arrow_6.gif" /></span> 
                        <div>
                        <asp:Calendar ID="Calendar1" runat="server" onselectionchanged="Calendar2_SelectionChanged"
                         ondayrender="Calendar2_DayRender">
                        </asp:Calendar>
                        </div>
                    </li>
                </ul>
           </td>
        </tr>
        <tr>
            <td>End Date:</td>
            <td><asp:TextBox ID="txt_end_date" runat="server" ReadOnly="true"></asp:TextBox></td>
            <td>
                <ul class="ul_parent">
                    <li> <span class="displayBlockTitle"><img src="http://www.lucomputers.com/images/arrow_6.gif" /></span> 
                        <div>
                        <asp:Calendar ID="Calendar2" runat="server" onselectionchanged="Calendar_SelectionChanged"
                         ondayrender="Calendar2_DayRender">
                        </asp:Calendar>
                        </div>
                    </li>
                </ul>
            </td>   
        </tr>
        <tr>
            <td>Save Price:</td>
            <td><asp:TextBox ID="txt_save_price" runat="server"></asp:TextBox></td>
            <td></td> 
        </tr>
        <tr>
            <td>File Name:</td>
            <td><asp:TextBox ID="txt_file_name" runat="server"></asp:TextBox></td>
            <td></td> 
        </tr>
        <tr>
            <td>Comment</td>
            <td><asp:TextBox ID="txt_comment" runat="server"></asp:TextBox></td>
            <td>&nbsp;</td> 
        </tr>
        <tr>
            <td></td>
            <td><asp:button ID="btn_save" runat="server" Text="Save" onclick="btn_save_Click"></asp:button></td>
            <td></td> 
        </tr>
    </table>
<hr size="1" />
<h3>History</h3>
<asp:Repeater runat="server" ID="rpt_rebate_list">
    <HeaderTemplate>
        <table>
    </HeaderTemplate>
    <ItemTemplate>
                <tr>
                        <td style="display:none" class="is_valid"><%# DataBinder.Eval(Container.DataItem, "v") %></td>
                        <td style="display:none" class="is_showit"><%# DataBinder.Eval(Container.DataItem, "show_it") %></td>
                        <td style=""><a href='/q_admin/product_part_rebate.aspx?id=<%# DataBinder.Eval(Container.DataItem, "sale_promotion_serial_no") %>&cmd=close' onclick="return js_callpage_cus(this.href, 'del_rebate', 200,200);" title="showit=0">Close</a></td>
                        <td style="text-align:right; width:100px;">$<%# DataBinder.Eval(Container.DataItem, "save_cost")%></td>
                        <td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "begin_date") %></td>
                        <td style="text-align:right"><%# DataBinder.Eval(Container.DataItem, "end_date")%></td>
                        <td><a href='http://www.lucomputers.com/pro_img/rebate_pdf/<%# DataBinder.Eval(Container.DataItem, "pdf_filename")%>' target="_blank"><%# DataBinder.Eval(Container.DataItem, "pdf_filename")%></a></td>
                </tr>
    </ItemTemplate>
    <FooterTemplate>
        </table>
    </FooterTemplate>
</asp:Repeater>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
<script>
    $().ready(function(){
        $('.is_valid').each(function(){
            if($(this).text() =="0")
                $(this).parent().find('td').css("color","#cccccc");
            else
                $(this).parent().find('td').css('color', 'green');
                
                $(this).parent().find('td').css('border-bottom', '1px dotted #cccccc');
            });
        
        $('.is_showit').each(function(){
            if($(this).text() == "0")
                $(this).parent().find('td').css("text-decoration", "line-through");

        });
    });
</script>
</asp:Content>

