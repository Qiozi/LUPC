<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sale_stat_month_report.aspx.cs" Inherits="Q_Admin_sale_stat_month_report" %>
<%@ Register Src="UC/main_menus.ascx" TagName="main_menus" TagPrefix="uc3" %>
<%@ Register Src="UC/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register src="UC/Navigation.ascx" tagname="Navigation" tagprefix="uc1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>Stat month report</title>
    <script type="text/javascript" src="js/winOpen.js"></script>
    <script type="text/javascript" language="javascript" src="JS/Float.js"></script>
</head>
<body  id="SystemDiagram_Layer">
    <form id="form1" runat="server">

    
    <asp:ScriptManager ID="ScriptManagerExportMonthReport" runat="server">
    </asp:ScriptManager>
   
     <div>
        <div>
            <uc1:Top ID="Top1" runat="server" />            
        </div>
        <div>
            <uc3:main_menus ID="Main_menus1" runat="server" />
       </div> 
     <uc1:Navigation ID="Navigation1" runat="server" />  
     <table width="100%">
        <tr>
            <td valign="top">
                <iframe src="sale_stat_month_report_sub.aspx" style="width: 100%;  height: 700px;border-top:1px solid #cccccc; " frameborder="0"></iframe>
            </td>
            <td style="width:570px" valign="top">
                <div id="div_ExportInfo" style="border: 1px solid #cccccc; height: 700px; width: 570px;background-color: #f2f2f2 ;">
                                        
                </div>
            </td>
        </tr>
    </table>
  </form>
</body>
</html>

