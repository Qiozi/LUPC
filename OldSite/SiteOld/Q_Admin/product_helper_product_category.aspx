<%@ Page Language="C#" MasterPageFile="~/Q_Admin/MasterPage.master" AutoEventWireup="true" CodeFile="product_helper_product_category.aspx.cs" Inherits="Q_Admin_product_helper_product_category" Title="Part Category Setting" %>

<%@ Register Src="UC/main_menus.ascx" TagName="main_menus" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="width:70%">
        <fieldset>
            <legend>一级</legend><asp:Button ID="btnCreate1" runat="server" Text="Create" OnClick="btnCreate1_Click" />
            <asp:Button ID="btnSave1" runat="server" Text="Save" OnClick="btnSave1_Click" />
                     <asp:Button ID="btn_create_left_menu" runat="server" 
                         onclick="btn_create_left_menu_Click" Text="保存修改后，请点击此按钮，后台下拉才会生效" />
                <hr />
            <asp:DataGrid ID="dg1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnItemCommand="dg1_ItemCommand" OnItemDataBound="dg1_ItemDataBound">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <Columns>
                    <asp:BoundColumn DataField="menu_child_serial_no" HeaderText="ID"></asp:BoundColumn>
                    
                    <asp:TemplateColumn HeaderText="name">
                        <itemtemplate>
<asp:TextBox id="_txtName" runat="server" Width="150px" Text='<%#DataBinder.Eval(Container.DataItem,"menu_child_name") %>' CssClass="input" __designer:wfdid="w2"></asp:TextBox> 
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="menu_is_exist_sub" HeaderText="子类"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="类型（配件，整机）">
                        <itemtemplate>
                        <asp:CheckBox id="_cbPageCategory" runat="server" Text="配件（否为整机）" Checked='<%#DataBinder.Eval(Container.DataItem,"page_category").ToString()=="1" ? true:false %>'></asp:CheckBox>

</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="show it">
                        <itemtemplate>
<asp:CheckBox id="_cbShowit" runat="server" __designer:wfdid="w16" Text="show it" Checked='<%#DataBinder.Eval(Container.DataItem,"tag").ToString()=="1" ? true:false %>'></asp:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="排序">
                        <itemtemplate>
<asp:TextBox id="_txtOrder" runat="server" __designer:wfdid="w17" Columns="8" CssClass="input" Text='<%#DataBinder.Eval(Container.DataItem,"menu_child_order") %>'></asp:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="是否是笔记本">
                        <itemtemplate>
<asp:CheckBox id="_cb_noebook" runat="server" __designer:wfdid="w3"  Checked='<%#DataBinder.Eval(Container.DataItem,"is_noebook").ToString()=="1" ? true:false %>'></asp:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:ButtonColumn CommandName="Delete" Text="删除"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="Create" Text="Sub Category"></asp:ButtonColumn>
                </Columns>
                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
        </asp:DataGrid>
        </fieldset>    
    </div>
    
    <div style="width:70%">
        <fieldset>
            <legend>二级： <asp:Label runat="server" ID="lblTitle2" CssClass="tab1"></asp:Label></legend>
        
        <asp:Button runat="server" ID="btnCreate2" Text="Create" OnClick="btnCreate2_Click"/><asp:Button runat="server" ID="btnSave2" Text="Save" OnClick="btnSave2_Click"/>
        <hr />
         <asp:DataGrid ID="dg2" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" OnItemCommand="dg2_ItemCommand" OnItemDataBound="dg1_ItemDataBound" >
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <Columns>
                    <asp:BoundColumn DataField="menu_child_serial_no" HeaderText="ID"></asp:BoundColumn>
                    
                    <asp:TemplateColumn HeaderText="name">
                        <itemtemplate>
<asp:TextBox id="_txtName2" runat="server" CssClass="input" Text='<%#DataBinder.Eval(Container.DataItem,"menu_child_name") %>' Width="150px"></asp:TextBox> 
</itemtemplate>
                    </asp:TemplateColumn>
                     <asp:BoundColumn DataField="menu_is_exist_sub" HeaderText="子类"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="类型（配件，整机）">
                        <itemtemplate>
                        <asp:CheckBox id="_cbPageCategory2" runat="server" Text="配件（否为整机）" Checked='<%#DataBinder.Eval(Container.DataItem,"page_category").ToString()=="1" ? true:false %>'></asp:CheckBox>

</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="show it">
                        <itemtemplate>
<asp:CheckBox id="_cbShowit2" runat="server" Text="show it" Checked='<%#DataBinder.Eval(Container.DataItem,"tag").ToString()=="1" ? true:false %>'></asp:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="排序">
                        <itemtemplate>
<asp:TextBox id="_txtOrder2" runat="server" Columns ="8" CssClass="input" Text='<%#DataBinder.Eval(Container.DataItem,"menu_child_order") %>'></asp:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                   <asp:TemplateColumn HeaderText="是否是笔记本">
                        <itemtemplate> 
                        <asp:CheckBox id="_cb_noebook_child" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"is_noebook").ToString()=="1" ? true:false %>'></asp:CheckBox>
                    </itemtemplate>
                    </asp:TemplateColumn>
                
                     <asp:TemplateColumn HeaderText="是否是虚目录">
                         <ItemTemplate>
                             <asp:CheckBox ID="_cb_is_not_real" runat="server" Text="虚目录" Checked='<%#DataBinder.Eval(Container.DataItem,"is_virtual").ToString()=="True" ? true:false %>'/>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:ButtonColumn CommandName="Delete" Text="删除"></asp:ButtonColumn>
                    <asp:ButtonColumn CommandName="Create" Text="Sub Category"></asp:ButtonColumn>
                </Columns>
                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
        </asp:DataGrid>
        </fieldset>
        
         <fieldset>
            <legend>三级： <asp:Label runat="server" ID="lblTitle3" CssClass="tab1"></asp:Label></legend>
        
        <asp:Button runat="server" ID="Button1" Text="Create" OnClick="Button1_Click"/><asp:Button runat="server" ID="Button2" Text="Save" OnClick="Button2_Click"/>
        <hr />
         <asp:DataGrid ID="dg3" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%">
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <SelectedItemStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <Columns>
                    <asp:BoundColumn DataField="menu_child_serial_no" HeaderText="ID"></asp:BoundColumn>
                    
                    <asp:TemplateColumn HeaderText="name">
                        <itemtemplate>
<asp:TextBox id="_txtName3" runat="server" CssClass="input" Text='<%#DataBinder.Eval(Container.DataItem,"menu_child_name") %>' Width="150"></asp:TextBox> 
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="类型（配件，整机）">
                        <itemtemplate>
                        <asp:CheckBox id="_cbPageCategory3" runat="server" Text="配件（否为整机）" Checked='<%#DataBinder.Eval(Container.DataItem,"page_category").ToString()=="1" ? true:false %>'></asp:CheckBox>

</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="show it">
                        <itemtemplate>
<asp:CheckBox id="_cbShowit3" runat="server" Text="show it" Checked='<%#DataBinder.Eval(Container.DataItem,"tag").ToString()=="1" ? true:false %>'></asp:CheckBox>
</itemtemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="排序">
                        <itemtemplate>
<asp:TextBox id="_txtOrder3" runat="server" Columns="8" CssClass="input" Text='<%#DataBinder.Eval(Container.DataItem,"menu_child_order") %>'></asp:TextBox>
</itemtemplate>
                    </asp:TemplateColumn>
                   <asp:TemplateColumn HeaderText="是否是笔记本">
                        <itemtemplate>
                          <asp:CheckBox id="_cb_noebook_sub" runat="server"  Checked='<%#DataBinder.Eval(Container.DataItem,"is_noebook").ToString()=="1" ? true:false %>'></asp:CheckBox>
                   </itemtemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="是否是虚目录">
                         <ItemTemplate>
                             <asp:CheckBox ID="_cb_is_not_real" runat="server" Text="虚目录" Checked='<%#DataBinder.Eval(Container.DataItem,"is_virtual").ToString()=="True" ? true:false %>'/>
                         </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:ButtonColumn CommandName="Delete" Text="删除"></asp:ButtonColumn>
                </Columns>
                <ItemStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <EditItemStyle BackColor="#999999" />
                <AlternatingItemStyle BackColor="White" ForeColor="#284775" />
        </asp:DataGrid>
        </fieldset>
        
    </div>

    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    

</asp:Content>

