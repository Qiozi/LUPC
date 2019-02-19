<%@ Page Language="C#" AutoEventWireup="true" CodeFile="email_templete.aspx.cs" Inherits="email_templete" %>

<%@ Register Assembly="Anthem" Namespace="Anthem" TagPrefix="anthem" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LU Computers</title>
   <style type="text/css">
    <!--
    .STYLE1 {
	    font-size: 9.0pt;
	    color: black;
    }
    .STYLE2 {
	    font-size: 10.5pt;
	    color: black;
    }
    -->
    </style> 
</head>

<body>
    <form id="form1" runat="server">
    <div>
    <table class=EC_MsoNormalTable border=0 cellspacing=0 cellpadding=0 bgcolor="#417CB6" style="background:#417CB6">
 <tr>
  <td style="padding:1.5pt 1.5pt 1.5pt 1.5pt">
  <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
  </td>
 </tr>
 <tr>
  <td width="100%" style="width:100.0%;padding:1.5pt 1.5pt 1.5pt 1.5pt">
  <table class=EC_MsoNormalTable border=0 cellspacing=0 cellpadding=0 width="100%" style="width:100.0%">
   <tr>
    <td style="padding:0in 0in 0in 0in">
    <table class=EC_MsoNormalTable border=0 cellspacing=0 cellpadding=0 width="100%" bgcolor=white style="width:100.0%;background:white">
     <tr>
      <td style="padding:4.5pt 4.5pt 4.5pt 4.5pt">
      <table class=EC_MsoNormalTable border=0 cellpadding=0 width="100%" style="width:100.0%">
       <tr>
        <td align="left" style="padding:.75pt .75pt .75pt .75pt">
        <p class=EC_MsoNormal><b><font size=4 color=red face=Verdana><span style="font-size:14.5pt;font-family:Verdana;color:red;font-weight:bold">Thank
        You For Your Order!<br>
        This is NOT a receipt.</span></font></b></p>
        </td>
        <!--td style="padding:.75pt .75pt .75pt .75pt">
        <p class=EC_MsoNormal align=right style="text-align:right"><b><font size=1 color=black face=Verdana><span style="font-size:7.5pt;font-family:Verdana;color:black;font-weight:bold">Lu Computers <br>
        1875 Leslie Street Unit 24<br>
        Toronto, ON M3B 2M5<br>
        <br>
        <a>E-mail Customer Service</a></span></font></b></p>
        </td-->
       </tr>
      </table>
      <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
      </td>
     </tr>
     <tr>
      <td align="left" style="padding:4.5pt 4.5pt 4.5pt 4.5pt">
      <p class=EC_MsoNormal><font size=1 color=black face=Verdana><span style="font-size:7.5pt;font-family:Verdana;color:black">This is the
      information used to process your order. This page is not printable from
      Netscape. You will receive a receipt by email when your order is
      processed successfully. You will also receive a hard copy of the receipt
      inside your shipment. </span></font></p>
      <p><font size=1 color=black face=Verdana><span style="font-size:7.5pt;font-family:Verdana;color:black">If you wish to inquire about the status
      of your order, please log in to <a>www.lucomputers.com</a>
      and click on user’s menu.</span></font></p>
      <div class=EC_MsoNormal align=center style="text-align:center"><font size=3 face="宋体"><span style="font-size:12.0pt">
      <hr size=1 width="100%" align=center>
      </span></font></div>
      <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
      </td>
     </tr>
     <tr>
      <td style="padding:4.5pt 4.5pt 4.5pt 4.5pt">
      <div align=center>
      <table class=EC_MsoNormalTable border=0 cellspacing=0 cellpadding=0 width="100%" style="width:100.0%">
       <tr>
        <td style="padding:0in 0in 0in 0in">
        <div align=center>
        <table class=EC_MsoNormalTable border=0 cellpadding=0 width="100%" style="width:100.0%">
         <tr height=15 style="height:11.25pt">
          <td width="15%" bgcolor="#AEF1FF" style="width:15.0%;background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">DATE </span></font></b></p>
          </td>
          <td width="10%" bgcolor="#AEF1FF" style="width:10.0%;background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">STATUS </span></font></b></p>
          </td>
          <td width="10%" bgcolor="#AEF1FF" style="width:10.0%;background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">ORDER #</span></font></b></p>
          </td>
          <td width="10%" bgcolor="#AEF1FF" style="width:10.0%;background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">TRACK #</span></font></b></p>
              
          </td>
         </tr>
         <tr height=15 style="height:11.25pt">
		
          <td width="15%" height=15 bgcolor=white style="width:15.0%;background:white;padding:0in 0in 0in 0in;height:11.25pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" ID="lbl_status_date" /></span></font></b></p>
          </td>
          <td width="10%" height=15 bgcolor=white style="width:10.0%;background:white;padding:0in 0in 0in 0in;height:11.25pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_status_name" /></span></font></b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"></span></font></p>
          </td>
          <td width="10%" height=15 bgcolor=white style="width:10.0%;background:white;padding:0in 0in 0in 0in;height:11.25pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_order_code" /></span></font></b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"></span></font></p>
          </td>
          <td width="10%" height=15 bgcolor=white style="width:10.0%;background:white;padding:0in 0in 0in 0in;height:11.25pt">
          <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold">
              NONE&nbsp;</span></font></b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"></span></font></p>
          </td>
         </tr>
        </table>
        </div>
        <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
        </td>
       </tr>
       <tr>
        <td style="padding:0in 0in 0in 0in">
        <div align=center>
        <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
         <tr height=15 style="height:11.25pt">
          <td height=15 bgcolor="#AEF1FF" style="background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11.25pt">
          <p class=EC_MsoNormal><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">CUSTOMER INFO</span></font></b></p>
          </td>
          <td height=15 bgcolor="#AEF1FF" style="background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11.25pt">
          <p class=EC_MsoNormal><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">SHIPPING INFO</span></font></b></p>
          </td>
         </tr>
         <tr>
          <td width="50%" valign=top style="width:50.0%;padding:0in 0in 0in 0in">
          <div align=center>
          
          
            <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">User</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">name:</span></font></p>            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_fullname"/></span></font></b></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">First Name:</span></font></p>            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_first_name"/></span></font></b></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Last Name:</span></font></p>            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_last_name" /></span></font></b></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Email:</span></font></p>            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_email1"/></span></font></b></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Phone 1:</span></font></p>            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_home_tel"/></span></font></b></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Phone 2:</span></font></p>            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_work_tel" /></span></font></b></p>            </td>
           </tr>
          </table>
          </div>
          <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
          </td>
          <td width="50%" valign=top style="width:50.0%;padding:0in 0in 0in 0in">
          <div align=center>
          <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Ship to:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">XXXXXX</span></font><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_fullname_2"/></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Address</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">1:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_address1" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Address</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">2:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_address2" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">City,</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">State:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_city_state_zip" /><anthem:Label runat="server" ID="lbl_state_serial_no" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Country:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_country" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Zipcode:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_zip_code" /> </span></font></b></p>
            </td>
           </tr>
          </table>
          </div>
          <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
          </td>
         </tr>
        </table>
        </div>
        <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
        </td>
       </tr>
       <tr>
        <td style="padding:0in 0in 0in 0in">
        <div align=center>
        <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
         <tr height=15 style="height:11.25pt">
          <td height=15 bgcolor="#AEF1FF" style="background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11.25pt">
          <p class=EC_MsoNormal><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">PAYMENT INFO</span></font></b></p>
          </td>
          <td height=15 bgcolor="#AEF1FF" style="background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11.25pt">
          <p class=EC_MsoNormal align=right style="text-align:right"><b><font size=2 color=black face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:black;font-weight:bold">GRAND
          TOTAL:</span></font><font size=1 color=black face=Tahoma><font size=1 color=black face=Tahoma><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_total" /></span></font></b></font></font><span class="STYLE2"><span style="font-family:Tahoma;font-weight:bold"> </span></span></b></p>
          </td>
         </tr>
         <tr>
          <td width="50%" valign=top style="width:50.0%;padding:0in 0in 0in 0in">
          <div align=center>
          <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Payment</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Type:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_pay_method" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">CCard</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Type:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_cc_type" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">CCard</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Number:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_cc_code" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">CCard</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Expiry:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_customer_expiry" /></span></font></b></p>
            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="20%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">CCard</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Phone:</span></font></p>
            </td>
            <td width="80%" height=10 align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_cc_phone" /></span></font></b></p>
            </td>
           </tr>
          </table>
          </div>
          <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
          </td>
          <td width="50%" valign=top style="width:50.0%;padding:0in 0in 0in 0in">
          <div align=center>
          <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
           <tr height=10 style="height:7.5pt">
            <td width="30%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Sub</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Total:</span></font></p>            </td>
            <td width="75%" height=10 bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"><span class="EC_MsoNormal" style="text-align:right"><font size=1 color=black face=Tahoma><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_sub_total" /></span></font></b></font></span></span></font></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="25%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Shipping:</span></font></p>            </td>
            <td width="75%" height=10 bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"><span class="EC_MsoNormal" style="text-align:right"><font size=1 color=black face=Tahoma><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_shipping" /></span></font></b></font></span></span></font></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="25%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">PST</span></font> <font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">(8.00</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">%):</span></font></p>            </td>
            <td width="75%" height=10 bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_pst"/></span></font></b></span></font></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="25%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">GST</span></font> <font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">(6.00</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">%):</span></font></p>            </td>
            <td width="75%" height=10 bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_gst" /></span></font></b></span></font></p>            </td>
           </tr>
           <tr height=10 style="height:7.5pt">
            <td width="25%" height=10 bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Sur</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Charge</span></font> <font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">(0.00</span></font><span class="STYLE1"><span style="font-family:Tahoma"> </span></span><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">flat):</span></font></p>            </td>
            <td width="75%" height=10 bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 3.75pt;height:7.5pt">
            <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_cc_surcharge" /></span></font></b></span></font></p>            </td>
           </tr>
          </table>
          </div>
          <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
          </td>
         </tr>
        </table>
        </div>
        <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
        </td>
       </tr>
       <tr>
        <td style="padding:0in 0in 0in 0in">
        <div align=center>
        <table class=EC_MsoNormalTable border=0 cellspacing=1 cellpadding=0 width="100%" style="width:100.0%">
         <tr height=15 style="height:11.25pt">
          <td colspan=2 height=15 bgcolor="#AEF1FF" style="background:#AEF1FF;padding:0in 0in 0in 7.5pt;height:11.25pt">
          <p class=EC_MsoNormal><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">CUSTOMER COMMENT / NOTE:</span></font></b></p>
          </td>
         </tr>
         <tr>
          <td width="20%" bgcolor="#D1FBFE" style="width:20.0%;background:#D1FBFE;padding:0in 0in 0in 0in">
          <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">Comment:</span></font></p>
          </td>
          <td width="80%" align="left" bgcolor=white style="width:80.0%;background:white;padding:0in 0in 0in 0in">
          <p><font size=2 color=red face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:red">x</span></font></p>
          </td>
         </tr>
        </table>
        </div>
        <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
        </td>
       </tr>
      </table>
      </div>
      <p class=EC_MsoNormal>&nbsp;</p>
      <div align=center>
      <table class=EC_MsoNormalTable border=0 cellpadding=0 width="100%" style="width:100.0%">
       <tr>
        <td colspan=6 bgcolor=black style="background:black;padding:0in 0in 0in 0in">
        <p class=EC_MsoNormal><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">?</span></font></p>
        </td>
       </tr>
       <tr height=20 style="height:15.0pt">
        <td colspan=3 height=20 bgcolor="#FFFFAE" style="background:#FFFFAE;padding:0in 0in 0in 0in;height:15.0pt">
        <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">Computer System(s)</span></font></b></p>
        </td>
        <td height=20 bgcolor="#FFFFAE" style="background:#FFFFAE;padding:0in 0in 0in 0in;height:15.0pt">
        <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">Price</span></font></b></p>
        </td>
        <td width="1%" height=20 bgcolor="#FFFFAE" style="width:1.0%;background:#FFFFAE;padding:0in 0in 0in 0in;height:15.0pt">
        <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold"># </span></font></b></p>
        </td>
        <td width="1%" height=20 bgcolor="#FFFFAE" style="width:1.0%;background:#FFFFAE;padding:0in 0in 0in 0in;height:15.0pt">
        <p class=EC_MsoNormal align=center style="text-align:center"><b><font size=2 color="#033479" face=Tahoma><span style="font-size:10.5pt;font-family:Tahoma;color:#033479;font-weight:bold">Charge</span></font></b></p>
        </td>
       </tr>
       <tr style="font-weight:none">
        <td height="17" colspan=3 bgcolor="#DFDFDF" style="background:#DFDFDF;padding:0in 3.75pt 0in 3.75pt">
        <p class=EC_MsoNormal>&nbsp;</p>
        </td>
        <td bgcolor="#DFDFDF" style="background:#DFDFDF;padding:0in 3.75pt 0in 3.75pt">
        <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><font size=1 color=black face=Tahoma><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_sub_total_1" /></span></font></b></font></font></p>
        </td>
        <td bgcolor="#DFDFDF" style="background:#DFDFDF;padding:0in 3.75pt 0in 3.75pt">
        <p class=EC_MsoNormal align=center style="text-align:center"><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">1</span></font></p>
        </td>
        <td bgcolor="#DFDFDF" style="background:#DFDFDF;padding:0in 3.75pt 0in 3.75pt">
        <p class=EC_MsoNormal align=right style="text-align:right"><font size=1 color=black face=Tahoma><font size=1 color=black face=Tahoma><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_sub_total_2" /></span></font></b></font></font><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"></span></font></p>
        </td>
       </tr>
	
       <tr>
        <td colspan=6 bgcolor=black style="background:white;padding:0in 0in 0in 0in; height: 117px;">
        <p class=EC_MsoNormal><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black; background-color: #ffffff;">
            <anthem:DataGrid ID="DataGrid1" runat="server">
            </anthem:DataGrid></span></font></p>
        </td>
       </tr>
       <tr height=30 style="height:22.5pt">
        <td colspan=3 height=30 bgcolor="#FFFFAE" style="background:#FFFFAE;padding:0in 0in 0in 0in;height:22.5pt">
        <p class=EC_MsoNormal align=right style="text-align:right"><b><font size=1 color="#033479" face=Tahoma style="font-size:px"><span style="font-size:9.0pt;font-family:Tahoma;color:#033479;font-weight:bold">Sub Total:</span></font></b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"></span></font></p>
        </td>
        <td colspan=3 height=30 bgcolor="#FFFFAE" style="background:#FFFFAE;padding:0in 0in 0in 0in;height:22.5pt">
        <p class=EC_MsoNormal align=right style="text-align:right"><b><font size=3 color=black face=Tahoma><span style="font-size:11.5pt;font-family:Tahoma;color:black;font-weight:bold"></span></font><font size=1 color=black face=Tahoma><font size=1 color=black face=Tahoma><b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black;font-weight:bold"><anthem:Label runat="server" id="lbl_sub_total_3" /></span></font></b></font></font></b><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black"></span></font></p>
        </td>
       </tr>
       <tr>
        <td colspan=6 bgcolor=black style="background:black;padding:0in 0in 0in 0in">
        <p class=EC_MsoNormal><font size=1 color=black face=Tahoma><span style="font-size:9.0pt;font-family:Tahoma;color:black">?</span></font></p>
        </td>
       </tr>
       <tr height=0>
        <td width=92 style="border:none"></td>
        <td width=503 style="border:none"></td>
        <td width=4 style="border:none"></td>
        <td width=51 style="border:none"></td>
        <td width=21 style="border:none"></td>
        <td width=61 style="border:none"></td>
       </tr>
      </table>
      </div>
      <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
      </td>
     </tr>
     <tr>
      <td align="left" style="padding:4.5pt 4.5pt 4.5pt 4.5pt">
      <p class=EC_MsoNormal><font size=1 color=black face=Verdana><span style="font-size:7.5pt;font-family:Verdana;color:black">If you have not
      paid for your order, please proceed now according to the payment method
      you have chosen. Should you need any assistance, feel free to <u>contact
      us</u> during business hours.</span></font></p>
      <p><b><font size=1 color=black face=Verdana><span style="font-size:7.5pt;font-family:Verdana;color:black;font-weight:bold">Toll Free:</span></font></b><font size=1 color=black face=Verdana><span style="font-size:7.5pt;font-family:Verdana;color:black"> 1 (866) 999-7828, <b><span style="font-weight:bold">Toronto
      Local:</span></b> (416) 446-7828<br>
      <b><span style="font-weight:bold">Business Hours: </span></b>Monday - Friday 10.00 AM - 7.30 PM EST,  Saturday 11.00 AM - 4.00 PM EST</span></font></p>
      </td>
     </tr>
    </table>
    <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
    </td>
   </tr>
  </table>
  <p class=EC_MsoNormal><font size=3 face="宋体"><span style="font-size:12.0pt"></span></font></p>
  </td>
 </tr>
</table>
    </div>
    </form>
</body>
</html>
