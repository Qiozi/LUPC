<%@ Page Title="" Language="C#" EnableViewState="false" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Src="UC/bottom.ascx" TagName="bottom" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <meta name="description" content="LU Computers is an established company providing computers and parts to home users and business in the US and Canada. We, having presences in Toronto and New York, aim to deliver fast and courteous service to our customers." />
    <link href="Content/fixedsticky.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="page-web-home">
        <div class="swiper-container" style="background-color: #000000;">
            <div class="swiper-wrapper">
                <div class="swiper-slide text-center">
                    <a href="/detail_part.aspx?sku=35262">
                        <img src="/images-ad/asus-35262.jpg" alt="Second slide" style="width: 100%" border="0" />
                    </a>
                </div>
                <div class="swiper-slide text-center">
                    <a href="/detail_part.aspx?sku=32635">
                        <img src="/images-ad/32635.jpg" alt="First slide" style="width: 100%" border="0" />
                    </a>
                </div>
                <div class="swiper-slide text-center">
                    <a href="/computers/video-cards.html?&k=85|nVidia GTX1080">
                        <img src="/images-ad/nvgtx1080.jpg" alt="Second slide" style="width: 100%" border="0" />
                    </a>
                </div>

            </div>
            <div class="swiper-button-prev"></div>
            <div class="swiper-button-next"></div>
        </div>

        <div class="topButtonArea">
            <nav class="navbar navbar-default" id="bs-example-navbar-collapse-1">
                <div class="container">
                    <!-- Collect the nav links, forms, and other content for toggling -->
                    <div class="collapse navbar-collapse">
                        <ul class="nav navbar-nav">
                            <asp:Repeater runat="server" ID="rptTopMenu" OnItemDataBound="rptTopMenu_ItemDataBound">
                                <ItemTemplate>
                                    <li class="dropdown item-level1">
                                        <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><%# DataBinder.Eval(Container.DataItem,"Title").ToString().ToUpper() %><span class="caret"></span></a>
                                        <ul class="dropdown-menu <%# (LU.Model.Enums.CateType)DataBinder.Eval(Container.DataItem,"CateType") == LU.Model.Enums.CateType.Part?"cate-list-dropdown":"" %>">
                                            <asp:Repeater ID="_rpt" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <a href='<%=LU.BLL.Config.Host %><%# ((int)DataBinder.Eval(Container.DataItem,"CateType")).ToString() == "0"? "list_sys.aspx?cid=":"list_part.aspx?cid=" %><%# DataBinder.Eval(Container.DataItem,"Id") %>'>
                                                            <span class="glyphicon glyphicon-play" style="font-size: 12px"></span>
                                                            <%# DataBinder.Eval(Container.DataItem,"Title") %>
                                                        </a>
                                                    </li>
                                                </ItemTemplate>
                                            </asp:Repeater>
                                            <asp:Repeater ID="_rptPart" runat="server">
                                                <ItemTemplate>
                                                    <li>
                                                        <a href='<%=LU.BLL.Config.Host %><%# DataBinder.Eval(Container.DataItem,"CateType").ToString() == "0"? "list_sys.aspx?cid=":"list_part.aspx?cid=" %><%# DataBinder.Eval(Container.DataItem,"Id") %>'>
                                                            <span class="glyphicon glyphicon-play" style="font-size: 12px"></span>
                                                            <%# DataBinder.Eval(Container.DataItem,"Title") %>
                                                        </a>

                                                </ItemTemplate>
                                                <AlternatingItemTemplate>

                                                    <a href='<%=LU.BLL.Config.Host %><%# DataBinder.Eval(Container.DataItem,"CateType").ToString() == "0"? "list_sys.aspx?cid=":"list_part.aspx?cid=" %><%# DataBinder.Eval(Container.DataItem,"Id") %>'>
                                                        <span class="glyphicon glyphicon-play" style="font-size: 12px"></span>
                                                        <%# DataBinder.Eval(Container.DataItem,"Title") %>
                                                    </a>
                                                    </li>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                        </ul>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>

                        </ul>
                        <ul class="nav navbar-nav navbar-right">
                            <li class="item-level1">
                                <a class="btn btn-link" href="<%= string.Concat(LU.BLL.Config.Host,"/OnSales.aspx") %>" style="font-size: 1.5rem;">
                                    <span class="glyphicon glyphicon-star"></span>
                                    On Sales
                                </a>
                            </li>
                            <li class="item-level1">
                                <a class="btn btn-link" href="<%= string.Concat(LU.BLL.Config.Host,"/Rebate.aspx") %>" style="font-size: 1.5rem;">
                                    <span class="glyphicon glyphicon-list"></span>
                                    Rebate
                                </a>
                            </li>
                        </ul>
                    </div>
                    <!-- /.navbar-collapse -->
                </div>
                <!-- /.container-fluid -->
            </nav>
        </div>

        <div class="container pd-0 bottom-distance" style="margin-top: 2rem; background-color: white; display: flex;">
            <div class="pd-r-0" style="width: 150px;">
                <div style="width: 150px;">&nbsp;</div>
                <div id="leftCateList" data-spy="affix" data-offset-top="500" data-offset-bottom="270" style="margin-top: -20px;">
                    <ul class="nav">
                        <li class="appNoDisplay">
                            <a href='#prodListArea' data-toggle="tooltip" data-placement="left" title="ALL Category">
                                <i class="iconfont">&#xe612;</i> <span class="t-kill"></span></a>
                        </li>
                        <asp:Repeater runat="server" ID="rptNavCateNameList">
                            <ItemTemplate>
                                <li class="appNoDisplay leftMenuCateItemBox">

                                    <a style="display: none; width: 150px; margin-top: -1px; float: right; position: absolute; border: 1px solid #ccc; border-left: 0; z-index: 99;"
                                        href="<%= LU.BLL.Config.Host %>list_part.aspx?cid=<%# DataBinder.Eval(Container.DataItem, "IsSystem").ToString() == "True" ?  DataBinder.Eval(Container.DataItem, "CateId") : DataBinder.Eval(Container.DataItem, "Value") %>">More</a>
                                    <a class="" style='z-index: 99;' href='#c<%# DataBinder.Eval(Container.DataItem, "Value") %>' data-toggle='tooltip' data-placement='top' title='<%# DataBinder.Eval(Container.DataItem, "Value") %>'>
                                        <%# DataBinder.Eval(Container.DataItem, "Text") %>
                                    </a>
                                </li>
                            </ItemTemplate>
                        </asp:Repeater>
                    </ul>
                </div>
            </div>
            <div class="row mg-b pd-0 homePageMain">
                <div class="col-md-12 pd-0" style="padding-left: 15px; background-color: #fcfefd;">
                    <div class="sys-list">
                        <asp:Repeater runat="server" ID="rptSysList" ClientIDMode="Static" OnItemDataBound="rptSysList_ItemDataBound">
                            <ItemTemplate>
                                <blockquote class="sysTitle" id="c<%# DataBinder.Eval(Container.DataItem, "Id") %>">
                                    <p>
                                        <%# DataBinder.Eval(Container.DataItem, "CateTitle") %>
                                        <a class="pull-right" href='<%# DataBinder.Eval(Container.DataItem, "eBayHref") %>' target='_blank'><%= LU.BLL.Util.eBayFont() %>: 
                                            <%# DataBinder.Eval(Container.DataItem, "eBayCode") %>
                                        </a>
                                    </p>
                                    <footer>
                                        <a href='<%#LU.BLL.Util.SysUrl(int.Parse(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>'>
                                            <%-- <i class="iconfont">&#xe612;</i>--%> <%# DataBinder.Eval(Container.DataItem, "Title") %>
                                        </a>
                                    </footer>
                                </blockquote>
                                <div class="row">
                                    <div class="col-md-3 syslogoArea">
                                        <a href="<%#LU.BLL.Util.SysUrl(int.Parse(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>">
                                            <img src='<%# DataBinder.Eval(Container.DataItem, "LogoUrl") %>' /></a>
                                        <div class="text-center">
                                            <h2>
                                                <span class="<%# DataBinder.Eval(Container.DataItem, "Price").ToString() == DataBinder.Eval(Container.DataItem, "Sold").ToString()?" hidden":"" %>">$<%# DataBinder.Eval(Container.DataItem, "Price") %></span><span class="price" style="font-size: 2rem;">$<%# DataBinder.Eval(Container.DataItem, "Sold") %></span><small><%# DataBinder.Eval(Container.DataItem, "PriceUnit") %></small></h2>
                                            <%--<a href='<%#LU.BLL.Util.SysUrl(int.Parse(DataBinder.Eval(Container.DataItem, "Id").ToString())) %>' class='btn btn-default'>
                                                <span class="glyphicon glyphicon-menu-right"></span>More Info
                                            </a>--%>
                                            <a class="btn btn-default btn-danger" id="A2" href="<%# string.Concat(LU.BLL.Config.Host,"detail_sys_customize.aspx?sku=", DataBinder.Eval(Container.DataItem, "Id")) %>">
                                                <span class="glyphicon glyphicon-cog"></span>&nbsp;Customize It
                                            </a>
                                        </div>
                                    </div>
                                    <div class="col-md-9 syspartlistArea">
                                        <asp:Repeater runat="server" ID="_rpt1">
                                            <HeaderTemplate>
                                                <table class="table">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr>
                                                    <td class="text-right appNoDisplay">*                                                   
                                                    </td>
                                                    <td class="ellipsis">
                                                        <a style='color: #333;' href="<%# DataBinder.Eval(Container.DataItem, "WebHref") %>">
                                                            <span class="appNoDisplay"><%# DataBinder.Eval(Container.DataItem, "Title") %></span>
                                                            <span class="appDisplay"><%# DataBinder.Eval(Container.DataItem, "WebTitle") %></span>
                                                        </a>
                                                    </td>
                                                    <td style="width: 60px"><a class='pull-right <%# string.IsNullOrEmpty((DataBinder.Eval(Container.DataItem, "eBayHref")??"").ToString())?" hidden":"" %>' href='<%# DataBinder.Eval(Container.DataItem, "eBayHref") %>' target='_blank'>
                                                        <%= LU.BLL.Util.eBayFont() %>
                                                    </a></td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                    </div>
                    <div style="line-height: 2rem; font-size: 2rem; text-align: center; padding: 1rem; background-color: honeydew; margin-right: 15px; margin-bottom: 15px; font-weight: bold;">
                        HOT
                    </div>
                    <div class="part-list">
                        <asp:Repeater runat="server" ID="rptProdList" OnItemDataBound="rptProdList_ItemDataBound">
                            <ItemTemplate>
                                <div class="text-center cate-title-box">
                                    <div id='c<%# DataBinder.Eval(Container.DataItem, "Value") %>' class='cateTitle'>
                                    </div>
                                </div>
                                <asp:Repeater runat="server" ID="_rpt">
                                    <HeaderTemplate>
                                        <div class="row part-list-box">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <div class="col-xs-12 col-sm-6 col-md-6" style="padding-left: 0px;">
                                            <span class="label label-warning onsaletag <%#DataBinder.Eval(Container.DataItem,"Discount").ToString()!="0.00"?"":" hidden" %>">On Sale</span>
                                            <div class="part-list-item cursor" onclick="window.location.href='<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem,"Sku").ToString()), DataBinder.Eval(Container.DataItem,"MFP").ToString()) %>';">
                                                <div>
                                                    <div class=" pd " style="display: flex; width: 100%; overflow: hidden; padding: 0 9px;">
                                                        <div class="text-center" style="width: 40%;">
                                                            <img class="lazy" src='<%= LU.BLL.Config.ResHost %>pro_img/ebay_gallery/9/999999_ebay_list_t_1.jpg'
                                                                data-original="<%# DataBinder.Eval(Container.DataItem,"Avatar") %>" height="135" alt="...">
                                                        </div>
                                                        <div style="width: 60%">
                                                            <h5 class="cate-title">
                                                                <%# DataBinder.Eval(Container.DataItem, "Brand") %>
                                                                <small class="pull-right price"><%# DataBinder.Eval(Container.DataItem, "SoldText") %></small>
                                                            </h5>
                                                            <div class="part-title-line part-name-box"><%# DataBinder.Eval(Container.DataItem,"ProduName") %></div>
                                                            <div style="display: flex; justify-content: flex-end;">
                                                                <div>
                                                                    <a class="btnBuy"
                                                                        href="<%# LU.BLL.Util.PartUrl(int.Parse(DataBinder.Eval(Container.DataItem,"Sku").ToString()), DataBinder.Eval(Container.DataItem,"MFP").ToString()) %>">Buy
                                                                    </a>
                                                                </div>
                                                            </div>
                                                            <a class='row ebay-btn-box' style="display: flex;" href="<%# LU.BLL.eBayProvider.GetEBayHref(DataBinder.Eval(Container.DataItem, "eBayCode").ToString()) %>" target="_blank">
                                                                <div class="" style="flex-grow: 1;">
                                                                    <%= LU.BLL.Util.eBayFont() %>
                                                                </div>
                                                                <div style="flex-grow: 1;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "eBayCode") %>
                                                                </div>
                                                                <div class="price-color-ebay text-right" style="flex-grow: 1;">
                                                                    <%# DataBinder.Eval(Container.DataItem, "eBayPriceText") %>
                                                                </div>
                                                            </a>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>

                                        <%# (1 == Container.ItemIndex%2 )?"</div><div class=\"row part-list-box\">":"" %>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </div>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </ItemTemplate>
                        </asp:Repeater>
                        <%--<asp:Literal runat="server" ID="ltProdList"></asp:Literal>--%>
                    </div>
                </div>
            </div>

        </div>

        <div class="logo-area pd" id="prodListArea">
            <div class="container">
                <div class=" text-center fontBold" style="font-size: 2rem;">Partners / Shop By Brand </div>
                <div class="logo-box">
                    <asp:Repeater runat="server" ID="rptBrandLogo">
                        <ItemTemplate>
                            <a href="<%# LU.BLL.Util.BrandUrl(DataBinder.Eval(Container.DataItem, "producter_name").ToString()) %>" title="<%# DataBinder.Eval(Container.DataItem, "producter_name") %>"
                                class="logo-box-item">
                                <img src="<%# DataBinder.Eval(Container.DataItem, "logo_url") %>" />
                            </a>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
     <%= WebExtensions.CombresLink("siteHomeJs") %>
</asp:Content>
