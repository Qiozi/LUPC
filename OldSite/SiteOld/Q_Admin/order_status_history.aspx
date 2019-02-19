<%@ Page Language="C#" AutoEventWireup="true" CodeFile="order_status_history.aspx.cs" Inherits="Q_Admin_order_status_history" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <table align="center">
                <tr>
                    
                    <td></td>
                    <td>
                        <anthem:TextBox runat="server" ID="txt_note" Columns="30" CssClass="input" Rows="5" TextMode="MultiLine" ></anthem:TextBox>
                    </td>
                    <td>
                    <anthem:DropDownList ID="ddl_status" runat="server">
                    </anthem:DropDownList>
                    <br />
                     <br />
                    <asp:Panel runat="server" ID="panel1" SkinID="btn" Width="150px">
                            <anthem:LinkButton runat="server" ID="lb_save_status" Text="Save" OnClick="lb_save_status_Click" ></anthem:LinkButton>
                    </asp:Panel>
                    </td>

                </tr>
            </table>
            <hr size="1" />
            <h3>
                Order &nbsp; Number:&nbsp;&nbsp;<anthem:Label runat="server" ID="lbl_order_code"></anthem:Label>
            </h3>
    </div>
        <anthem:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Width="100%" OnItemDataBound="DataGrid1_ItemDataBound">
            <HeaderStyle CssClass="trTitle" />
            <Columns>
                <asp:BoundColumn DataField="facture_state_serial_no" HeaderText="status"></asp:BoundColumn>
                <asp:BoundColumn DataField="order_facture_state_create_date" HeaderText="datetime"></asp:BoundColumn>
                <asp:BoundColumn DataField="order_facture_note" HeaderText="note"></asp:BoundColumn>
            </Columns>
        </anthem:DataGrid>
    </form>
</body>
</html>
