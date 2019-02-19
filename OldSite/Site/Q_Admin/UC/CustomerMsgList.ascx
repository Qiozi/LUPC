<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CustomerMsgList.ascx.cs" Inherits="Q_Admin_UC_CustomerMsgList" %>

<div style="width:700px;float:left; border-right: 0px dotted #B9C7F1; margin-right: 1em;">
<asp:DataList ID="DataList1" runat="server">    
    <ItemTemplate>
        <ul style="width:100%; border:1px solid #D1DAF6;padding:1px; margin-top: 5px;margin-left: -3px; ">
            <li style="float:left; padding:5px; background:#D1DAF6; width:300px; text-align:left" ><asp:Label runat="server" ID="lbl_regdate" Text='<%# DataBinder.Eval(Container.DataItem, "regdate") %>'></asp:Label></li>
            <li style="float:left; padding:5px; background:#D1DAF6; width:377px"><a  onclick="OpenOrderDetail('<%# DataBinder.Eval(Container.DataItem, "msg_order_code") %>')" style="cursor:pointer"><%# DataBinder.Eval(Container.DataItem, "msg_order_code")%></a></li>
            <li style="clear:both">
                <pre style="border-top:1px solid #cccccc;padding: 5px; text-align: left; white-space: pre-wrap;white-space: -moz-pre-wrap;white-space: -pre-wrap;white-space: -o-pre-wrap;word-wrap: break-word;white-space:normal; word-break:break-all;<%# DataBinder.Eval(Container.DataItem, "today_color") %>"><asp:Label runat="server" ID="_lbl_content" Text='<%# DataBinder.Eval(Container.DataItem, "msg_content_text") %>'></asp:Label>
                </pre>
            </li>
            
        </ul>        
    </ItemTemplate>
</asp:DataList>
</div>
