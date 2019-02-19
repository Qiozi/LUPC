<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_query_keyword.aspx.cs" Inherits="Q_Admin_product_helper_query_keyword" Title="Category Keyword" %>

<%@ Register src="UC/CategoryDropDownLoad.ascx" tagname="CategoryDropDownLoad" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <table>
                <tr>
                        <td><uc1:CategoryDropDownLoad ID="CategoryDropDownLoad1" runat="server" CFT="AllAll" 
                                /></td>
                        <td><asp:Button ID="btn_go" runat="server" Text="Go" onclick="btn_go_Click" /></td>
                </tr>
        </table>
        <hr size="1" />
        <h3><asp:Label runat="server" ID="lbl_category_name"></asp:Label></h3>
        <hr size="1" />
        <asp:Button runat="server" ID="btn_new_query_cate" Text="New Group Name" 
            onclick="btn_new_query_cate_Click" Visible="False" />
        <asp:ListView runat="server" ID="lv_keyword_cate_list" 
            ItemPlaceholderID="itemPlaceholderID" 
            onitemcommand="lv_keyword_cate_list_ItemCommand" 
            onitemdatabound="lv_keyword_cate_list_ItemDataBound">
                <LayoutTemplate>
                        <div style="background:#cccccc">
                                <table class="table_td_white" cellpadding="2" cellspacing="1" width="100%">
                                        <tr runat="server" id="itemPlaceholderID">
                                             
                                        </tr>
                                </table>
                        </div>
                </LayoutTemplate>
                <ItemTemplate>
                        <tr>
                                <td style="background:#f2f2f2;width: 250px">
                                        <asp:HiddenField runat="server" ID="_hf_id" Value='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
                                        <asp:TextBox runat="server" Columns="30"  ID="_txt_parent_keyword" Text='<%# DataBinder.Eval(Container.DataItem, "keyword") %>'></asp:TextBox>
                                        <asp:Button runat="server" ID="_btn_save" Text="Save" Visible="True" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'
                                         CommandName='SaveParentKeyword' />
                                </td>
                                <td width="200">
                                        <asp:TextBox runat="server" ID="_txt_keyword"></asp:TextBox>
                                        <asp:Button runat="server" ID="btn_New_keyword" Text="New" CommandName="NewChildCateKeyword" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' />
                         
                                </td>
                                <td>
                                        <asp:Repeater runat="server" ID="_rpt_sub_keyword" OnItemCommand="rpt_ItemCommand" >
                                            <ItemTemplate>
                                            <div style="float:left; margin: 2px 4px 2px 4px;">
                                                    <ul class="ul_parent">
                                                            <li>
                                                            <%# DataBinder.Eval(Container.DataItem, "keyword") %>
                                                            <div style="border:1px solid #ff9900;"><asp:ImageButton runat="server"  CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>' CommandName="DeleteSubKeyword" ImageUrl="~/soft_img/tags/(02,41).png" AlternateText="delete" /></div>
                                                            </li>
                                                    </ul>
                                            </div>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                </td>
                                <td>
                                               <input type="button" value="Sort" onclick="ShowIframe('Edit Sort','/q_admin/product_keyword_sort.aspx?id=<%# DataBinder.Eval(Container.DataItem, "id") %>',600,650)" />

                                </td>
                        </tr>
                </ItemTemplate>
        </asp:ListView>
        <hr size="1" />
        搜索的目录ID集（用 ,  号分割）:<asp:TextBox runat="server" ID="txt_container_category" Visible="false" Columns="50"></asp:TextBox>
        <asp:Button runat="server" ID="btn_save_cates_id" Text="save" 
            onclick="btn_save_cates_id_Click" />
        <br />
        <asp:Literal runat="server" ID="literal_categorys"></asp:Literal>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
</asp:Content>

