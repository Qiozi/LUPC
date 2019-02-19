<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="ebay_system_edit.aspx.cs" Inherits="Q_Admin_ebayMaster_ebay_system_edit" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="../JS/lib/jquery-1.3.2.min.js"  type="text/javascript"></script>
    <script src="../JS/lib/jquery.history_remote.pack.js"   type="text/javascript"></script>
    <script src="../JS/lib/jquery.tabs.pack.js"  type="text/javascript"></script>
    <style>
           table tr td a.ok{display:block; line-height: 20px; text-align: center; background:green; color:White; width: 80px;}
           table tr td a.step{display:block;line-height: 20px; text-align: center; background:#ffffff; color:#000000;width: 80px;}
           table.nav td { background:#ffffff;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<h3><asp:Label runat="server" ID="lbl_luc_sku"></asp:Label></h3>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
<hr size="1" style="color:White"/>

 <div id="container-5">
            <ul>
                <li><a href="#fragment-13"><span>Edit</span></a></li>
                <li><a href="#fragment-14"><span>Step 2</span></a></li>
                <li><a href="#fragment-15"><span>Step 3</span></a></li>
            </ul>
            <div id="fragment-13">
                        
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div style="background:#ccc; width: 333px;">
                        <table cellspacing="1" width="100%" class="nav">
                                <tr>
                                        <td>
                                                <asp:LinkButton runat="server" Text="输入SKU" CommandArgument="1" ID="lb_step1" 
                                                    CssClass="ok" onclick="lb_step1_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                                <asp:LinkButton runat="server" Text="修改零件名称" CommandArgument="2" ID="lb_step2" 
                                                    CssClass="step" onclick="lb_step2_Click"></asp:LinkButton>
                                        </td>
                                        <td>
                                                <asp:LinkButton runat="server" Text="修改价格" CommandArgument="3" ID="lb_step3" 
                                                    CssClass="step" onclick="lb_step3_Click"></asp:LinkButton>
                                        </td>
                                        <td><asp:LinkButton runat="server" Text="生成模板" CommandArgument="3" ID="lb_step4" 
                                                CssClass="step" onclick="lb_step4_Click"></asp:LinkButton></td>
                                </tr>
                        </table>
                        </div><asp:Panel ID="panel_step1" runat="server" >
                        
                        <br />
                        <asp:ListView runat="server" ID="lv_ebay_system" ItemPlaceholderID="itemPlaceholderID">
                                <LayoutTemplate>
                                        <table>
                                                <tr runat="server" id="itemPlaceholderID"></tr>
                                        </table>
                                </LayoutTemplate>
                                <ItemTemplate>
                                    
                                        <tr>
                                                <td>
                                                        <asp:Label runat="server" ID="_lbl_comment" Text='<%# DataBinder.Eval(Container.DataItem, "comment") %>'></asp:Label>
                                                        
                                                        <asp:HiddenField runat="server" ID="_hf_comment_id" value='<%# DataBinder.Eval(Container.DataItem, "id") %>'/>
                                                </td>
                                                <td><asp:TextBox runat="server" ID="_txt_luc_sku"></asp:TextBox></td>
                                        </tr>
                                </ItemTemplate>
                        </asp:ListView>
                        <asp:Button runat="server" ID="btn_save_step1" onclick="btn_save_step1_Click"  OnClientClick="$('#container-5').tabs(2);"
                            Text="Next" />
                        </asp:Panel>
                        
                        <asp:Panel ID="panel_step2" runat="server"  Visible="false">

                                 <asp:ListView runat="server" ID="lv_ebay_system_2" ItemPlaceholderID="itemPlaceholderID">
                                        <LayoutTemplate>
                                                <table>
                                                        <tr runat="server" id="itemPlaceholderID"></tr>
                                                </table>
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                                <tr>
                                                        <td><%# DataBinder.Eval(Container.DataItem, "comment") %>
                                                                <asp:HiddenField runat="server" ID="_hf_comment_id" value='<%# DataBinder.Eval(Container.DataItem, "comment_id") %>'/>
                                                        </td>
                                                        <td><%# DataBinder.Eval(Container.DataItem, "luc_sku") %></td>
                                                        <td><asp:TextBox Columns="100"  runat="server" ID="_txt_luc_name" Text='<%# DataBinder.Eval(Container.DataItem, "name") %>'></asp:TextBox></td>
                                                </tr>
                                        </ItemTemplate>
                                </asp:ListView>       
                        </asp:Panel>
                        
                        <asp:Panel ID="panel_step3" runat="server"  Visible="false">3
                        </asp:Panel>
                        
                        <asp:Panel ID="panel_step4" runat="server"  Visible="false">d
                        </asp:Panel>
                      </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div id="fragment-14">
                        
            </div>
            <div id="fragment-15">
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
                Lorem ipsum dolor sit amet, consectetuer adipiscing elit, sed diam nonummy nibh euismod tincidunt ut laoreet dolore magna aliquam erat volutpat.
            </div>
            
</div>
       <asp:Literal ID="Literal1" runat="server"></asp:Literal>             
<script type="text/javascript">
$(function() { $('#container-5').tabs({ fxSlide: true, fxFade: true, fxSpeed: 'normal' });});

</script>

</asp:Content>

