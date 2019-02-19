<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="product_helper_import_store_price_2.aspx.cs" Inherits="Q_Admin_product_helper_import_store_price_2" Title="Untitled Page" %>

<%@ Register src="UC/IncStat.ascx" tagname="IncStat" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="text-align:center; padding-top: 2em"> 
   
          
        <asp:FileUpload runat="server" ID="file_upload_store_price" />
        <asp:Button runat="server" ID="btn_upload" Text="Upload" 
            onclick="btn_upload_Click" onclientclick="ParentLoadWait()" />
        <hr size="1" />
        <asp:DropDownList runat="server" ID="ddr_ltd_category"></asp:DropDownList>
        <asp:Button runat="server" ID="btn_download_info" 
            onclick="btn_download_info_Click" Text="Download" Width="188px" />
        <hr size="1" />
        <div style="width: 600px; text-align:left" class="note">
        
<pre>
1. 上传文只能是xls 类型
2. 上传前请将sheet$ 改为 table 
3. 文件内容格式, 第一行请用以下字母 <a href="/excel.rar" target="_parent">点击下载格式文件</a>
    Ltd_code    上家编码
    Ltd_sku     上家SKU(如果此SKU不存在资料库，将当作新产品进行插入, 本公司产品只做更新处理)
    Ltd_cost    进价
    Ltd_stock   上家库存
    Ltd_manufacture_code    工厂SKU(上传时，如果为空，此列将不更新，本公司产品不进行更新此列)
    Ltd_part_name   产品名称
<hr size="1" />
上家编码:
    1   LU          
    2   supercom    
    3   asii        
    4   EPROM       
    5   DAIWA       
    6   MUTUAL      
    7   OCZ        
    8   COMTRONIX   
    9   SINOTECH    
    10  MINIMICRO   
    11  ALC 
    12  SAMTACH 
    13  MMAX
    15  CanadaComputer
    16  Dandh
    17  D2A </pre>
        </div>     
      
        <hr size="1" />
        <asp:Button runat="server" ID="btn_run" Text="点击此按钮，使cost , stock 生效" 
            onclick="btn_run_Click" onclientclick="ParentLoadWait();"/>
        <asp:GridView ID="GridView1" runat="server">
        </asp:GridView>
        <hr size="1" />
        <uc1:IncStat ID="IncStat1" runat="server" />
    </div><br /><br />
</asp:Content>

