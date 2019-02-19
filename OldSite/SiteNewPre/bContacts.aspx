<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="bContacts.aspx.cs" Inherits="bContacts" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style>
        h4 {
            background: #467417;
            padding: 10px;
            color: #FFFFA2;
        }

        td {
            border: 1px solid #ccc;
            padding: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: #74C774; padding: 0px; color: #996633">
        <ol class="breadcrumb" style="margin-top: 0em; background: #74C774; margin-left: 2em;">
            <li><a href="default.aspx" style="color: White;">
                <h3>Home</h3>
            </a></li>
            <li class="active" style="font-size: 24px; color: White;">Contacts </li>
        </ol>
        <div style="background-color: White; margin: -1em 3em 3em 3em; padding: 50px;">
            <table style="border-collapse: collapse; border: none;" cellpadding="5">
                <tbody>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2" width="15%"><b>3Com</b></td>
                        <td class="text_hui_11" width="35%">Toll-Free: 1-800-NET-3Com
                            <br>
                            Technical Support: 1-800-231-8770
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2" width="15%"><b>LG Electronics
                            <br>
                        </b></td>
                        <td class="text_hui_11" width="35%">Phone: (905) 568-6800
                            <br>
                            Toll-Free: 1-888-LG-Canada
                            <br>
                            Customer Service: 1-888-LG CANADA (1-888-542-2623) </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Acer Canada</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-565-2237
                            <br>
                            Technical Support: 1-800-452-2237
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>LiteOn
                            <br>
                        </b></td>
                        <td class="text_hui_11">Technical Support: (408) 935-5353 113 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Acoustic Bass
                            <br>
                            &nbsp;</b></td>
                        <td class="text_hui_11">Technical Support: (626)293-8712
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Logitech</b></td>
                        <td class="text_hui_11">Phone: 1-800-231-7717
                            <br>
                            Customer Support: 702-269-3457 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Adaptec</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-959-7274
                            <br>
                            Technical Support: 408-934-7274
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Matrox</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-361-1408
                            <br>
                            Technical Support: 514-685-0270
                            <br>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Altec Lansing</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-887-1075
                            <br>
                            Technical Support: 1-800-887-1075
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Maxtor</b></td>
                        <td class="text_hui_11">Phone: 1-800-2-MAXTOR
                            <br>
                            Technical Support: 1-800-2-MAXTOR </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>AMD</b></td>
                        <td class="text_hui_11">Phone: 905-856-3377
                            <br>
                            Technical Support: 905-856-8554
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Microsoft
                            <br>
                            &nbsp;</b></td>
                        <td class="text_hui_11">Technical Support: 905-568-4494 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>APC</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-443-4519
                            <br>
                            Technical Support: 1-800-800-4272
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>MidiLand</b></td>
                        <td class="text_hui_11">Phone: 1-888-592-1168
                            <br>
                            Technical Support: 1-888-592-1168 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>ASUS</b></td>
                        <td class="text_hui_11">Phone: (510) 739-3777
                            <br>
                            Technical Support: (502) 995-0883
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Mitsumi</b></td>
                        <td class="text_hui_11">Phone: 1-800-MITSUMI
                            <br>
                            Technical Support: 1-800-MITSUMI </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>ATI</b></td>
                        <td class="text_hui_11">Phone: 905-882-2600
                            <br>
                            Technical Support: 905-882-2626 </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>MSI</b></td>
                        <td class="text_hui_11">Phone: (626) 913 0828
                            <br>
                            Technical Support: (626) 581 3001 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>BenQ</b></td>
                        <td class="text_hui_11">Phone: 909-569-0700
                            <br>
                            Technical Support: 800-452-2237 </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Philips Electronics Ltd.</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-387-0564
                            <br>
                            Technical Support: 1-800-479-6696 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Creative Labs</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-998-5227
                            <br>
                            Technical Support: 1-800-998-1000</td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Pioneer </b></td>
                        <td class="text_hui_11">Technical Support: 1-800-421-1613 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>D-Link</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-DLINK-CA
                            <br>
                            Technical Support: 1-800-361-5265
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>PowerColor </b></td>
                        <td class="text_hui_11">Technical Support: <a href="mailto:tech@power-color.com"><font color="#000000">tech@power-color.com</font></a></td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>ElanVital</b></td>
                        <td class="text_hui_11">Phone: 886-3-460-2910 #373
                            <br>
                            Fax: 886-3-460-4380
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Powercom</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-600-8931
                            <br>
                            Technical Support: 1-800-600-8931 714-632-8889 ext. 114 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Enlight</b></td>
                        <td class="text_hui_11">Phone: 562-781-9898
                            <br>
                            Fax: 562-781-1158
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Samsung
                            <br>
                        </b></td>
                        <td class="text_hui_11">Phone: 905-542-3535
                            <br>
                            Toll-Free: 1-800-SAMSUNG
                            <br>
                            Technical Support: 905-819-5121 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Envision (EPI)</b></td>
                        <td class="text_hui_11">Phone: (510) 770-9988
                            <br>
                            Technical Support: (888) 838-6388
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Seagate
                            <br>
                        </b></td>
                        <td class="text_hui_11">Phone: 1-405-936-1400 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>EPSON</b></td>
                        <td class="text_hui_11">Phone: 1-800-GO-EPSON
                            <br>
                            Technical Support: 905-709-9475 </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Shuttle</b></td>
                        <td class="text_hui_11">Phone: 626-820-9000
                            <br>
                            Fax: 626-820-5060 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Fujitsu</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-263-8716
                            <br>
                            Technical Support: 1-800-263-7091
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>SmartLink</b></td>
                        <td class="text_hui_11">Phone: (626) 330-3600
                            <br>
                            Technical Support: (626) 330-3600 Ext. 107 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Gigabyte</b></td>
                        <td class="text_hui_11">Phone: (626) 854-9338
                            <br>
                            Fax: (626) 854-9339
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>SMC </b></td>
                        <td class="text_hui_11">Phone: (949) 707-2400
                            <br>
                            Toll-Free: 1-800-SMC-4YOU
                            <br>
                            Technical Support: 1-800-SMC-4YOU Option 1
                            <br>
                            RMA's: 1-800-SMC-4YOU </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>IBM</b></td>
                        <td class="text_hui_11">Phone: 1-800-IBM-4YOU
                            <br>
                            Technical Support: 1-800-IBM-SERV<br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>SonicBLUE</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-468-5846
                            <br>
                            Technical Support: 541-967-2450 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Intel</b></td>
                        <td class="text_hui_11">Phone: 1-800-628-8686
                            <br>
                            Technical Support: 1-800-321-4044
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Sony</b></td>
                        <td class="text_hui_11">Phone: 416-499-1414
                            <br>
                            Technical Support: 416-495-2995 1-800-476-6972
                            <br>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Key Tronic
                            <br>
                            &nbsp;</b></td>
                        <td class="text_hui_11">Technical Support: 1-800-262-6006
                            <br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Targus</b></td>
                        <td class="text_hui_11">Phone: 714-523-5429
                            <br>
                            Customer Service: 714-523-5429 238/271 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Koss</b></td>
                        <td class="text_hui_11">Phone: 1-800-449-3315
                            <br>
                            Fax: 1-888-427-5677<br>
                        </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>US Robotics</b></td>
                        <td class="text_hui_11">Phone: (847) 874-2000
                            <br>
                            Toll-Free: (877) 710-0884 (toll-free in the U.S. only) </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Lava</b></td>
                        <td class="text_hui_11">Phone: 416-674-5942
                            <br>
                            Technical Support: 416-674-5942 </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Western Digital</b></td>
                        <td class="text_hui_11">FaxBack: 714-932-4300
                            <br>
                            Technical Support: 1-800-448-8470 </td>
                    </tr>
                    <tr>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Lexmark</b></td>
                        <td class="text_hui_11">Toll-Free: 1-800-LEXMARK
                            <br>
                            Technical Support: 1-800-LEXMARK </td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Wisetech </b></td>
                        <td class="text_hui_11">Technical Support: (626)293-8712 </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td class="text_hui_11">&nbsp;</td>
                        <td class="text_blue_11" bgcolor="#CDE3F2"><b>Yamaha</b></td>
                        <td class="text_hui_11">Phone: 416-298-1311
                            <br>
                            Technical Support: 1-800-840-6746 </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
</asp:Content>
