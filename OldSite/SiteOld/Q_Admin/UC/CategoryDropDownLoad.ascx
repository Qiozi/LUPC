<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CategoryDropDownLoad.ascx.cs" Inherits="Q_Admin_UC_CategoryDropDownLoad" %>

<ul class="ul_parent_2">
        <li>
            <table cellpadding="0" cellspacing="0" style="border:0px solid #ccc; width: 150px;">
                    <tr>
                            <td><asp:HiddenField ID="txt_id" runat="server" 
                onvaluechanged="txt_id_ValueChanged"></asp:HiddenField>
                <asp:TextBox ID="txt_text" runat="server" Columns="25" 
                 ReadOnly="true"></asp:TextBox>
                 <div style="left:auto; top: 16px; display:none" id="uc_dropDownList_category_selected">
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </div></td>
                            <td><img src="/images/arrow_5.gif" alt="Press" title="Press" style="height: 19px;width: 15px; cursor:pointer" onclick="document.getElementById('uc_dropDownList_category_selected').style.display = '';" /></td>
                    </tr>
            </table>
            
        </li>
</ul>