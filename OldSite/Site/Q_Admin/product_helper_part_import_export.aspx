<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_part_import_export.aspx.cs" Inherits="Q_Admin_product_helper_part_import_export" Title="Untitled Page" %>

<%@ Register src="UC/MenuChildName.ascx" tagname="MenuChildName" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:MenuChildName ID="MenuChildName1" runat="server" />
    <div style="text-align:center; font-size: 12pt;">
        <hr size="1" />
        <table style="width:100%;">
            <tr>
                <td align="right">
        <asp:CheckBox ID="CheckBox_showit" runat="server" Text="Showit" TextAlign="Left" 
                        Checked="True" />
                    <br />
        regdate(format:2008-08-18):<asp:TextBox ID="txt_regdate"
            runat="server" Width="100px"></asp:TextBox>
                </td>
                <td align="left">
        <asp:Button ID="btn_export_part" runat="server" Text="Download Part" 
            onclick="btn_export_part_Click" Width="272px" Height="44px" />
                </td>
            </tr>
        </table>
        <div style="width: 320px; margin-top: 5px" class="note">当"showit"打勾时，只下载showit=1的产品<br />
            regdate: 创建产品的日期，方便导出近期所创建的产品<br />
            如果不填，将导出本类所有产品</div>
        <hr size="1" />
        <div style="border:1px solid #ccc;width: 300px;text-align:center;margin: auto;padding: 1em;" align="center" >
                
                            <asp:TextBox ID="txt_part_quantity" runat="server" Columns="4" Text="1"></asp:TextBox>
                            <asp:Button
                                ID="btn_create_new_part" runat="server" Text="New Part" 
                                onclick="btn_create_new_part_Click" />
         
                            <asp:Literal ID="literal_new_part" runat="server"></asp:Literal>
         
        </div>
        <hr size="1" />
        <asp:FileUpload ID="FileUpload_edit_part" runat="server" />
        <asp:Button ID="btn_upload" runat="server" Text="Part Upload" onclick="btn_upload_Click" 
                        onclientclick="ParentLoadWait()" />
        <div style="width: 650px; margin-top: 5px; text-align:left" class="note">
           <pre>1.  上传时，请将excel文件的sheet$名称改为"table"
           
2.  字段说明：
        sku:            part 编码（唯一值）
	middle_name:    part 中名称
	short_name：    part 短名称
	showit：        是否显示产品（值，1 显示  0 不显示）
	manufacturer：  part 生产商
	manufacturer_url：part 生产商网站址
	manufacturer_part_number：part 生产商编号# 
	supplier_sku:
	priority:       产品显示顺序(数字)
	hot:            hot 产品标志（值，1 或 0 ，1表示hot） 
	new：           new part 产品标志（值, 1 或 0 ，1表示new 产品）
	split_line：    是否是绿色标题标志（值, 1 或 0 ，1此产品是绿色标题）
	long_name：     part 长名称
	img_sum：       产品图标数量(数字)
	keywords：      产品关键字 
	other_product_sku： 其他产品的图标，如果输入其他产品的SKU， 此产品将使用输入产品的图片(数字)
	export：        是否输出，确定此产品是否可以输出到xml文件的标志。（值, 1 或 0 ，1输出）
        cost:           进价
        special_cost_price:          <span style="color:Blue;">现金价</span>
        store_sum:          库存
        model:              型号
           </pre>
               
            
        </div>
       <hr size="1" />
       <!-- 
       <h2>上传新产品(upload new part)</h2>
                   <asp:FileUpload id="fileupload_new_part" runat="server" />
                    <asp:Button runat="server" ID="btn_save_new_part" Text="Upload New Part" 
                        onclick="btn_save_new_part_Click" Visible="False" />
                        <div class="note" style="width: 340px">
                         价格系数：<asp:Label runat="server" id="lbl_price_coefficient"></asp:Label>&nbsp;<asp:Literal runat="server" ID="literal_modify_price"></asp:Literal>&nbsp;
                        </div>
       <div class="note" style="width: 650px; margin-top: 5px; text-align:left">
            <pre>
<%--1.  此处上传的产品将在 <a href="/q_admin/product_helper_import_store_price.aspx?menu_id=6" style="color:blue" onclick="parent.window.location.href=this.href; return false;">这里下载</a>--%>
2.  上传时，请将excel文件的sheet$名称改为"table"
3.  第一行的字母请勿删除或修改。

上传文档说明:
    manufacturer_part_number: 工厂SKU （唯一值）
    short_name:     产品短名称
    middle_name:    产品中名称
    long_name:      产品长名称
    cost:           进价
    store_sum:      上家存库数量
    showit:         上传后前台是否立刻显示(值: 1 显示  0 不显示)
    category_name:  产品所属分类名称，仅供参考，不上传
            </pre>
       </div>
       -->
    </div>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>


