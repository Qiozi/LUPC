<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="bManufacturers.aspx.cs" Inherits="bManufacturers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <style>
        h4
        {
            background: #467417;
            padding: 10px;
            color: #FFFFA2;
        }
        td{border: 1px solid #ccc;padding:5px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<div class="container" style="background: #74C774; padding: 0px; color: #996633">
        <ol class="breadcrumb" style="margin-top: 0em; background: #74C774; margin-left: 2em;">
            <li><a href="default.aspx" style="color: White;">
                <h3>
                    Home</h3>
            </a></li>
            <li class="active" style="font-size: 24px; color: White;">Manufacturers</li>
        </ol>
        <div style="background-color: White; margin: -1em 3em 3em 3em; padding: 50px;">
           <table style="border-collapse:collapse; border:none;" cellpadding="5"><tbody>
                                
                               
                                  <td width="20%"><strong><a href="http://www.adatausa.com" target="_blank">A-Data</a> </strong></td>
                                  <td >Memory Sticks</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.acer.ca" target="_blank">Acer</a> </strong></td>
                                  <td >Monitors, Notebooks, Projectors, Servers, Systems, TVs</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.adaptec.com" target="_blank">Adaptec</a> </strong></td>
                                  <td >I/O Controllers</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.advueu.com" target="_blank">Advueu</a> </strong></td>
                                  <td >DVD Players</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.aicipc.com/support_warranty.asp">AIC</a> </strong></td>
                                  <td >Cases</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.alteclansing.com">Altec Lansing</a> </strong></td>
                                  <td >Speakers</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.amd.com/us-en" target="_blank">AMD</a> </strong></td>
                                  <td >CPU</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.apacer.com" target="_blank">Apacer</a> </strong></td>
                                  <td >Card Readers, Memory Sticks, MP3 Players</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.apcc.com" target="_blank">APC</a> </strong></td>
                                  <td >UPS</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.asrock.com/index_english.html" target="_blank">Asrock</a> </strong></td>
                                  <td >Motherboards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.asus.com" target="_blank">ASUS</a> </strong></td>
                                  <td >Cases, Case/CPU/System Accessories, CD Recorders/Rewritable, DVD-ROM Drives,   DVD-RW, Monitors, Motherboards, Network Products, Notebooks, PC Systems, PDAs,   Power Supplies, Servers, Systems, Video Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.chenbro.com" target="_blank">Chenbro</a> </strong></td>
                                  <td >Cases, Case/CPU/System Accessories, Power Supplies</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.athenatech.us" target="_blank">Citicase</a> </strong></td>
                                  <td >Cases</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.coolermaster.com" target="_blank">CoolerMaster</a> </strong></td>
                                  <td >Case/CPU/System Accessories</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.creativelabs.com" target="_blank">Creative</a> </strong></td>
                                  <td >Home Networking Products, Keyboards, Media Players, Mice, MP3 Players, Sound   Cards, Speakers, Webcams</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.crucial.com" target="_blank">Crucial</a> </strong></td>
                                  <td >Card Readers, Memory, Memory Sticks</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.dlink.ca" target="_blank">D-Link</a> </strong></td>
                                  <td >Home Networking Products, Internet Security, Network Products, Storage</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.enlightcorp.com" target="_blank">Enlight</a> </strong></td>
                                  <td >Cases, Power Supplies</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.fujitsu.ca" target="_blank">Fujitsu</a> </strong></td>
                                  <td >Hard Drives, Keyboards, Notebooks, Scanners</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.gigabyte.com.tw" target="_blank">Gigabyte</a> </strong></td>
                                  <td >Cases, Case/CPU/System Accessories, Motherboards, Video Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.hauppage.com" target="_blank">Hauppage</a> </strong></td>
                                  <td >Software</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.hp.com" target="_blank">HP</a> </strong></td>
                                  <td >DVD-RW</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.can.ibm.com" target="_blank">IBM</a> </strong></td>
                                  <td >Servers, Storage</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.infrant.com" target="_blank">Infrant</a> </strong></td>
                                  <td >Storage (NAS)</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.intel.com/ca" target="_blank">Intel</a> </strong></td>
                                  <td >CPU, Network Products, Servers</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.kosselectronics.ca" target="_blank">Koss</a> </strong></td>
                                  <td >CD Players, Clock Radios, DVD Players, MP3 Players</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.lavalink.com" target="_blank">Lava</a> </strong></td>
                                  <td >IO Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.lenovo.com/ca/en/" target="_blank">Lenovo</a> </strong></td>
                                  <td >CD-ROM Drives, CD Recorders/Rewritable, DVD-ROM Drives, DVD-RW, Hard Drives,   Keyboards, Mice, Monitors, Notebooks, PC Systems, Power Supplies, Projectors,   Systems</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.lexmark.com" target="_blank">Lexmark</a> </strong></td>
                                  <td >Multi-Function Devices, Multi-Function Devices, Printers</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.lge.ca" target="_blank">LG</a> </strong></td>
                                  <td >CD-ROM Drives, CD Recorders/Rewritable, DVD-ROM Drives, DVD-RW, Monitors,   Notebooks</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.liteonamericas.com" target="_blank">LiteOn</a> </strong></td>
                                  <td >CD-ROM Drives, CD Recorders/Rewritable, DVD-ROM Drives, DVD-RW, DVD   Recorders</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.matrox.com" target="_blank">Matrox</a> </strong></td>
                                  <td >Video Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.mediamounts.com" target="_blank">MediaMounts</a> </strong></td>
                                  <td >Monitor Mounting Solutions</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.microsoft.com" target="_blank">Microsoft</a> </strong></td>
                                  <td >Keyboards, Mice, Software</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.mitsumi.com" target="_blank">Mitsumi</a> </strong></td>
                                  <td >Floppy Drives, Keyboards, Mice</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.msicomputer.com" target="_blank">MSI</a> </strong></td>
                                  <td >Consumer Electronics Accessories, Home/Car Electronics, Media Players,   Motherboards, Notebooks, Video Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.netgear.com" target="_blank">NetGear</a> </strong></td>
                                  <td >Home Networking Products, Network Products, Storage, Storage (NAS)</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.pctreasures.net" target="_blank">PC   Treasures</a> </strong></td>
                                  <td >Software</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.channel.philips.com" target="_blank">Philips</a> </strong></td>
                                  <td >Monitors</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.powercolor.com.tw" target="_blank">PowerColor</a> </strong></td>
                                  <td >Video Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.qnap.com" target="_blank">Qnap</a> </strong></td>
                                  <td >Storage, Storage (NAS)</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.rjtech.net/technical.html" target="_blank">RJ Tech</a> </strong></td>
                                  <td >Digital Photo Frames, DVD Players, DVD Recorders</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.samsungcanada.com" target="_blank">Samsung</a> </strong></td>
                                  <td >CD Recorders/Rewritable, DVD-RW, Monitors, Multi-Function Devices,   Printers</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.sanyocanada.com" target="_blank">Sanyo</a> </strong></td>
                                  <td >Batteries, Boomboxes, Camcorders, Clock Radios, Cordless Phones, Digital   Photo Frames, Hi-Fi Stereos, Home Theatre Systems, Rice Cookers, Telephones,   TVs, Voice Recorders</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.seagate.com" target="_blank">Seagate</a> </strong></td>
                                  <td >Hard Drives</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.shuttle.com" target="_blank">Shuttle</a> </strong></td>
                                  <td >Case/CPU/System Accessories, Monitors, PC Systems, Systems</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.silverstonetek.com" target="_blank">Silverstone</a> </strong></td>
                                  <td >Cases, Case/CPU/System Accessories, Power Supplies</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.archtek.com" target="_blank">SmartLink</a> </strong></td>
                                  <td >Modems</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.sparkle.com.tw" target="_blank">Sparkle</a> </strong></td>
                                  <td >Video Cards</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.sparklepower.com" target="_blank">SPI</a> </strong></td>
                                  <td >Power Supplies</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.targus.ca" target="_blank">Targus</a> </strong></td>
                                  <td >Carrying Cases, Keyboards, Mice</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.thermaltakeusa.com" target="_blank">Thermaltake</a> </strong></td>
                                  <td >Cases, Case/CPU/System Accessories, Power Supplies</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.touch-systems.ca" target="_blank">Touch</a> </strong></td>
                                  <td >Notebooks, Servers, Systems</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.usr.com" target="_blank">US   Robotics</a> </strong></td>
                                  <td >Modems</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.18002526123.com" target="_blank">Venturer</a> </strong></td>
                                  <td >DVD Players, Home Theatre Systems, Karaoke Systems</td>
                                </tr><tr>
                                  <td><strong><a href="http://www.wdc.com" target="_blank">WD</a> </strong></td>
                                  <td >Hard Drives, Storage</td>
                                </tr>
                              </tbody>
                              </table>
        </div>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server"></asp:Content>