<%@ Page Title="" Language="C#" MasterPageFile="~/Q_Admin/Manager/MasterPage.master" AutoEventWireup="true" CodeFile="EditSpecifics.aspx.cs" Inherits="Q_Admin_Manager_Product_EditSpecifics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #tableList input {
            margin: 0;
        }

        #tableList td {
            vertical-align: middle;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container container-fluid">
        <div>
            <button class="btn btnReloadCategorySpecifices" data-cid="<%= ReqeBayCateId %>">Reload Remote</button>
        </div>
        <div>
            <table class="table" id="tableList">
                <asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_ItemDataBound" OnItemCommand="rptList_ItemCommand">
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:TextBox runat="server" CssClass="form-control mg-0" ID="_txtName" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox runat="server" CssClass="form-control mg-0" ID="_txtValue" Text='<%# DataBinder.Eval(Container.DataItem, "Text") %>'></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList CssClass="form-control mg-0" runat="server" ID="_ddlRemm"></asp:DropDownList>
                                <asp:Button runat="server" ID="_btn" Text="Set" CommandName="Set" />
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </table>
            <div class="text-center">
                <asp:Button runat="server" ID="submitTo" Text="Save" CssClass="btn" OnClick="submitTo_Click" />
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="footScript" runat="Server">
    <script>
        $(function () {
            $('.btnReloadCategorySpecifices').on('click', function () {
                $.get('/q_admin/manager/ebay/formebaygetcategoryspecifices.aspx', { cid: $(this).data('cid') }, function () {
                    alert("OK")
                });
            });
        });
    </script>
</asp:Content>

