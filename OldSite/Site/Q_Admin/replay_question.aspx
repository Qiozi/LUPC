<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="replay_question.aspx.cs" Inherits="Q_Admin_replay_question" Title="Replay Question" EnableEventValidation="false" ResponseEncoding="gb2312" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<%@ Register Src="UC/Navigation.ascx" TagName="Navigation" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:Navigation ID="Navigation1" runat="server" NavigationText="Replay Question" />
    
   
    <table >
        <tr>
            <td valign="top" style="width: 60%;">
            
            
                <anthem:DataGrid runat="server" ID="dg_question_list" AllowPaging="True" 
                    AutoGenerateColumns="False" PageSize="20" 
                    onitemcommand="dg_question_list_ItemCommand" 
                    onpageindexchanged="dg_question_list_PageIndexChanged" CellPadding="4" 
                    ForeColor="#333333" GridLines="None"  SelectedItemStyle-ForeColor="#ff9900" 
                    Width="100%" >
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <ItemStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundColumn DataField="aq_serial_no" HeaderText="ON"></asp:BoundColumn>
                        <asp:BoundColumn DataField="create_datetime" HeaderText="Datetime" 
                         >
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="aq_email" HeaderText="Email"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="ViewProduct" DataTextField="product_serial_no" 
                            HeaderText="product"></asp:ButtonColumn>
                        <asp:BoundColumn DataField="aq_title" HeaderText="subject">
                        </asp:BoundColumn>                      
                        
                        <asp:BoundColumn DataField="aq_send" HeaderText="status"></asp:BoundColumn>
                        <asp:ButtonColumn CommandName="Replay" Text="Replay"></asp:ButtonColumn>
                    </Columns>
                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <EditItemStyle BackColor="#2461BF" />
                    <PagerStyle Mode="NumericPages" ForeColor="#ff9900"  Font-Bold="true"  />
                    
                    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <AlternatingItemStyle BackColor="White" />
                </anthem:DataGrid>
            </td>  
            <td valign="top" style="border-left: 5px solid white;">
                
                <anthem:TextBox runat="server" ID="lbl_email_body" Columns="60" Rows="15" 
                    TextMode="MultiLine"></anthem:TextBox>
              
                <div style="text-align: left; line-height:25px">
                    to: <anthem:Label runat="server" ID="lbl_to_email" ForeColor="#CC0000"></anthem:Label>
                </div>
               <div style="text-align: left; line-height:25px">
                subject:<anthem:TextBox runat="server" ID="txt_subject" Columns="60" Rows="15" 
                    ></anthem:TextBox>
                    </div>
                <anthem:TextBox runat="server" ID="txt_replay_body" Columns="60" Rows="15" 
                    TextMode="MultiLine"></anthem:TextBox>
                 <br />
                 <div style="text-align: center;">
                <anthem:Button runat="server" ID="btn_reset" Text="reset" onclick="btn_reset_Click"/>
                     <anthem:Button runat="server" ID="btn_replay" Text="save/sent" 
                         onclick="btn_replay_Click" />
                </div>
            </td>     
        </tr>

    </table>
   
</asp:Content>

