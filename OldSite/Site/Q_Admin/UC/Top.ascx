<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top.ascx.cs" Inherits="Q_Admin_UC_Top" %>
<%@ Register Src="AlertMessage.ascx" TagName="AlertMessage" TagPrefix="uc1" %>

<!---->
<div style="background: #f2f2f2; height: 25px;">
    <img src="/Q_Admin/Images/user.gif" />&nbsp;&nbsp;UserName:  &nbsp;&nbsp;
                <asp:Label ID="lbl_username" runat="server"></asp:Label>
    &nbsp;&nbsp;&nbsp;
    <img src="/Q_Admin/Images/modif_pass.gif" align="absmiddle" />&nbsp;<a href="/q_admin/settings_modif_password.aspx">[modify password]</a>&nbsp;<img
        src="/Q_Admin/Images/exit.gif" align="absmiddle" />&nbsp;&nbsp;<a onclick="if(confirm('are you sure?')) {window.location.href='/q_admin/logout.aspx';}" style="cursor: pointer;">[logout]</a>

    <a href="/site/" target="_blank">[To Home]</a>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>

    <div style="float: right; padding: 4px; display: none;">
        <a href="/Chat/chatserver.aspx" target="_blank">
            <span id="taskArea" style="color: Green; font-weight: bold; font-size: 7pt">
                <img src='/q_admin/images/icons/small/black/people.png'></span></a>

    </div>
</div>

<script type="text/javascript">
//function topTimer()
//{
//    var n = 1;
//   $.timer(120000,function (timer){
//           GetTaskCount();
//      }
//  );
//}



//    function msgRun() {

//        if ($('#taskArea').html() == "")
//            $('#taskArea').html("<img src ='images/icons/small/black/people.png' >");
//        else {
//            $('#taskArea').html("");
//        }
//        //alert("D");
//        setTimeout(function () { msgRun() }, 1000);
//    }
//    msgRun();
//topTimer();
</script>

