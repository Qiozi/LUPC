<%@ Page Language="C#" MasterPageFile="~/Q_Admin/None.master" AutoEventWireup="true" CodeFile="main_navigation.aspx.cs" Inherits="Q_Admin_main_navigation" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="line-height: 20px">
    aaaa
    </div>
    <ul id="top_main_nav">
        <li>
            <a id="main_btn_1" href="/default.asp" title="part manage" target="frame_middle" onclick="ChnageMainBtn(this);">PART MANAGE</a>
        </li>
        <li>
            <a id="main_btn_2" href="/default.asp" title="orders manage" target="frame_middle" onclick="ChnageMainBtn(this);">ORDERS</a> 
        </li>
        <li>
            <a id="main_btn_3" href="/default.asp" title="system setting" target="frame_middle" onclick="ChnageMainBtn(this);">SYSTEM HELPER</a>
        </li>
    </ul>
<script type="text/javascript">
    function ChnageMainBtn(the)
    {
        var els = document.getElementsByTagName("A");
        for(var i=0; i<els.length; i++)
        {
            if(els[i].id.substr(0,8) == "main_btn")
            {
                els[i].className="a_main_btn";
            }
            
        }
        the.className="a_main_btn_selected";
    }
</script>
</asp:Content>

