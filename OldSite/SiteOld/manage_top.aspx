<%@ Page Language="C#" AutoEventWireup="true" CodeFile="manage_top.aspx.cs" Inherits="mamage_top" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>LU Computers</title>
    <script type="text/javascript" src="Q_Admin/JS/Loading.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div><div style="text-align:center">
     <anthem:Button ID="Button1" runat="server" Text="Save" onclick="Button1_Click"  PostCallBackFunction="Anthem_PostCallBack" PreCallBackFunction="Anthem_PreCallBack"/>
        <hr size="1" />
        
        
        <anthem:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundColumn DataField="top_id" HeaderText="ID"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="SKU">
                    <ItemTemplate>
                        <anthem:TextBox ID="_txt_top_sku" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "top_sku")  %>'> 
                        &nbsp;&nbsp; 
                        </anthem:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Comment">
                    <ItemTemplate>
                        <anthem:TextBox ID="_txt_top_comment" runat="server" 
                            Text='<%# DataBinder.Eval(Container.DataItem, "top_comment")  %>'>
&nbsp;&nbsp;
                        </anthem:TextBox>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
        </anthem:DataGrid>
        </div>
    </div>
    </form>
</body>
</html>
