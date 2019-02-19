<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top2.ascx.cs" Inherits="UC_Top2" %>
<div class="topNavBox">
    <div class="topNav">
        <div class="container">
            <div class="pull-left">WELCOME!&nbsp;<span style="color:blue;"><%= CustName %></span><a href="/MyAccount.aspx"><%= MyAccount %></a></div>
            <div class="pull-right toprow displayFlex">
                <a class="btn btn-link <%=!IsLogin?" hidden":"" %>" href="/Logout.aspx"><span class="glyphicon glyphicon-log-out"></span>&nbsp;Logout </a>
                <a class="btn btn-link <%=IsLogin?" hidden":"" %>" href="<%= string.Concat(LU.BLL.Config.Host, "Login.aspx")  %>">Sign In</a>
                <a class="btn btn-link <%=IsLogin?" hidden":"" %>" href="<%= string.Concat(LU.BLL.Config.Host, "Register.aspx")  %>">Sign Up</a>
                <a class="btn btn-link a-item" title='Canada' href="<%= string.Concat(LU.BLL.Config.Host,"ReturnHome.aspx?cid=1") %>" style="<%= IsCanada ? "": " color: #ccc;" %>">
                    <img src="<%= string.Concat(LU.BLL.Config.ResHost, IsCanada?"images/ca24.png":"images/ca24-2.png") %>" style="width: 24px; margin-top: -3px;" />
                </a>
                <a class="btn btn-link a-item" title='USA' href="<%= string.Concat(LU.BLL.Config.Host,"ReturnHome.aspx?cid=2") %>">
                    <img src="<%= string.Concat(LU.BLL.Config.ResHost, IsCanada?"images/usa24-2.png":"images/usa24.png") %>" style="width: 24px; margin-top: -3px;" />
                </a>
            </div>
        </div>
    </div>

</div>
<div class="topNavMain fixedsticky">
    <div class="container">
        <div class="displayFlex">
            <div class="">
                <a href="<%= LU.BLL.Config.Host %>">
                    <img src="<%= WebLogo %>" />
                </a>
            </div>
            <div class="searchBox a-item" data-toggle="modal" data-target=".bs-example-modal-sm">
                <div class="input-group">
                    <input type="text" class="form-control searchInput" readonly="readonly">
                    <span class="input-group-addon"><span class="glyphicon glyphicon-search"></span></span>
                </div>
            </div>
            <div class="a-item">
            </div>
            <div class="">
                <a title='Shopping Cart' class="btn btn-link aitem" href="<%= string.Concat(LU.BLL.Config.Host, "ShoppingCart.aspx")  %>">
                    <span class="glyphicon glyphicon-shopping-cart"></span>
                    <span class='cart-badge badge'></span>
                </a>
            </div>
            <div class="person-box a-item" style="display: none;">
                <a class="btn btn-link aitem" href="<%= string.Concat(LU.BLL.Config.Host, "MyAccount.aspx")  %>">
                    <%-- <svg class="icon " style="font-size: 3rem;" aria-hidden="true">
                            <use xlink:href="#icon-wode"></use>
                        </svg>--%>
                    <span class="glyphicon glyphicon-user"></span>
                </a>
            </div>
        </div>
    </div>
</div>
<div class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel"
    aria-hidden="true">
    <div class="modal-dialog modal-md">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                    <span aria-hidden="true">&times;</span></button>
                <h4 class="modal-title" id="exampleModalLabel">
                    <span class='glyphicon glyphicon-search'></span>Search</h4>
            </div>
            <div class="modal-body">

                <div class="input-group " style="margin: 2rem;">
                    <!-- /btn-group -->
                    <input type="text" class="form-control typeahead"
                        data-provide="typeahead"
                        data-items="45"
                        maxlength="45"
                        autocomplete="off"
                        placeholder="MF Part#/Keyword/LU SKU#/LU Quote#/eBay item#"
                        onkeydown="if(event.keyCode==13){util.searchGo($(this).val());return false;}">
                    <span class="input-group-btn">
                        <a class="btn btn-default" id="searchBtn" onclick="util.searchGo($('.typeahead').val());return false;">Go!</a>
                    </span>
                </div>
                <div class="top-search-category-box">
                    <asp:Repeater runat="server" ID="rptList" OnItemDataBound="rptList_ItemDataBound">
                        <ItemTemplate>
                            <div class="search-category-item">
                                <h4 class="search-category-title"><%# DataBinder.Eval(Container.DataItem,"Title") %></h4>
                                <div class="search-category-sub-box">
                                    <asp:Repeater runat="server" ID="_rpSub">
                                        <ItemTemplate>
                                            <div class="search-category-sub-item"
                                                <%# DataBinder.Eval(Container.DataItem,"Id").ToString() == "412"?" style='width:280px;' ":"" %>>
                                                <label>
                                                    <input class="iCheckRadio SearchCateItem"
                                                        type="radio"
                                                        name="SearchCateItem"
                                                        value="<%# DataBinder.Eval(Container.DataItem,"Id") %>"
                                                        <%# CurrSearchCateId == int.Parse(DataBinder.Eval(Container.DataItem,"Id").ToString())?" checked":"" %> />
                                                    <%# DataBinder.Eval(Container.DataItem,"Title") %>
                                                </label>
                                            </div>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</div>
