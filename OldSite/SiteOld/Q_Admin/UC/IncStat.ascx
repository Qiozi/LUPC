<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IncStat.ascx.cs" Inherits="Q_Admin_UC_IncStat" %>
<%--<table style="width: 700px;"><tr><td>
<span style="float:left;">
    (<i>每天运行一次</i>)amount datetime: <asp:Label runat="server" 
        ID="literal_amount_datetime" Font-Bold="True"></asp:Label>
</span>
<span style="float:right">
    <asp:Button runat="server" ID="btn_run_amount" 
        OnClientClick="ParentLoadWait();" Text="Run Amount" 
        onclick="btn_run_amount_Click" Visible="False" />
 
</span>   <br  style="clear:both"/>
<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" 
        onrowdatabound="GridView1_RowDataBound" Width="100%" 
        CssClass="table_small_font" Visible="False">
    <Columns>
        <asp:BoundField DataField="id" HeaderText="ID">
        <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:BoundField DataField="other_inc_name" HeaderText="Inc Name" />
        <asp:BoundField DataField="inc_record" HeaderText="Record" >
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="inc_record_valid" HeaderText="Record_Valid" >
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="inc_record_match" HeaderText="Record_Match" >
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="bigger_than_lu" HeaderText="&gt; LU" >
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="less_than_lu" HeaderText="&lt; LU" >
        <ItemStyle ForeColor="Red" HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="equal_than_lu" HeaderText="= LU" >
        <ItemStyle HorizontalAlign="Right" />
        </asp:BoundField>
        <asp:BoundField DataField="last_run_date" HeaderText="Watch Datetime" />
    </Columns>
    <HeaderStyle Font-Size="8.5pt" HorizontalAlign="Center" />
</asp:GridView>
</td></tr></table>
--%>
