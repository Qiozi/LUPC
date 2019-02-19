<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="detail_sys_customize.aspx.cs" Inherits="detail_sys_customize" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <style>
        .nav-tabs > li.active a {
            background-color: #E8A28B;
            color: White;
        }

            .nav-tabs > li.active a:active {
                background-color: #E8A28B;
                color: White;
            }

        .sys-part-title > span.badge {
            background: white;
            color: #333;
        }

        .PlaneSysPartDetail {
            width: 100%;
            height: 400px;
            overflow: scroll;
        }

            .PlaneSysPartDetail a.list-group-item {
                padding: 2px;
            }

        .listArea a {
            cursor: pointer;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div class="container" style="background: white;">
        <div class="panel panel-default">
            <div class="panel-heading" style="padding-bottom: 0px;">
                <ul class="list-inline">
                    <li>
                        <div>
                            <span class="glyphicon glyphicon-home"></span>
                            <a href="/default.aspx">&nbsp;Home</a>
                            <span class="glyphicon glyphicon-menu-right"></span>
                            <a id="dLabel" data-target="#" href="/default.aspx"></a>
                            <asp:Literal runat="server" ID="ltCateNameParent"></asp:Literal>
                            <span class="glyphicon glyphicon-menu-right"></span>

                        </div>

                    </li>
                    <li>
                        <div class="dropdown">
                            <a id="A1" data-target="#" href="/list_part.aspx?cid=<%= CateID %>" data-toggle="dropdown" aria-haspopup="true" role="button" aria-expanded="false">
                                <asp:Literal runat="server" ID="ltCateName"></asp:Literal>
                                <span class="caret"></span>
                                <span class="glyphicon glyphicon-menu-right"></span>
                            </a>

                            SKU: <%= ReqSKU %>
                            <ul class="dropdown-menu" role="menu" aria-labelledby="dLabel">
                                <asp:Literal runat="server" ID="ltCates"></asp:Literal>
                            </ul>
                        </div>
                    </li>
                </ul>
            </div>
        </div>
    </div>

    <div class="container" style="height: 450px; background-color: white;">
        <div style='height: 8px;'></div>
        <div class="row">
            <div class="col-md-5 text-center" style="background: white;">
                <div class="text-center" style="border: 1px solid #ccc; margin: 1em -1em 0 0;">
                    <img id="sysCaseLogo" src="<%= LU.BLL.ImageHelper.Get("images/loaderc.gif") %>" width="400" alt='...' />
                </div>
            </div>
            <div class="col-md-7">
                <div class="row">
                    <div style="padding: 1em;">
                        <ul class="list-group ">
                            <li class="list-group-item">Price<span class="badge regularPrice"></span></li>
                            <li class="list-group-item">Special Cash Price<span class="badge specialCashPrice"></span></li>
                            <%--<li class="list-group-item">eBay Price<span class="badge ebayPrice"></span></li>--%>
                            <li class="list-group-item"><a class='btn btn-default' onclick="saveQuote();return false;" data-toggle="modal" data-target="#showQuote">Quote Number, Press here to obtain</a></li>
                        </ul>
                    </div>
                </div>

                <p class="note">
                    SPECIAL CASH PRICE is promotional offer, valid on pay methods of cash,Interact,bank transfer,money order, etc. Cash price does not waive sales taxes if applicable. 
                </p>
                <p class="note">
                    Every unique computer takes 1-7 days to be preassembled and tested before installed into the computer chassis. System includes meticulous hand assembly and precision cable routing. We tune system performance to its best and complete driver updates. All manufacturer documentations and disks are included.
                </p>
                <a class="btn btn-default" onclick="printSys();return false;" data-toggle="modal" data-target="#showSys"><span class="glyphicon glyphicon-print"></span>Print this customized System</a>
                <a class="btn btn-default" onclick="addToCart();return false;"><span class="glyphicon glyphicon-shopping-cart"></span>Add to Shopping Cart</a>

            </div>
        </div>
    </div>
    <div class="container text-center " style="background: white;">
        <p class="well">
            <a class="btn btn-default">I like this system to be posted on eBay, and I'll order with eBay.  </a>
            <br />
            <strong>More System on <a href='http://stores.ebay.ca/LU-Computers-Inc/Desktop-Computers-/_i.html?_fsub=1685774017&_sid=180084747&_trksid=p4634.c0.m322'>eBay</a> or <a href='list_sys.aspx?cid=413'>Web Site</a> </strong>
        </p>

    </div>
    <div class="container clearfix listArea" style="background: white;">
        <div class="row">
            <div class="col-md-3 left-col ">
                <div class="pin-wrapper" style="height: 130px;">
                    <div class="pinned" style="width: 261px; top: 45px; position: fixed;">
                        <div style="height: 45px;"></div>

                        <div class="list-group">
                            <div class="list-group-item list-group-item-success">Price<span class="badge regularPrice">...</span></div>
                            <div class="list-group-item list-group-item-success">Special Cash Price <span class="badge specialCashPrice">...</span></div>
                            <%--    <div class="list-group-item list-group-item-success">eBay Price <span class="badge ebayPrice">...</span></div>--%>
                           <%-- <a class="list-group-item"><span class="glyphicon glyphicon-question-sign"></span>Ask seller a question</a>--%>
                            <a class="list-group-item" onclick="showSys();return false;" data-toggle="modal" data-target="#showSys"><span class="glyphicon glyphicon-list-alt"></span>View my customized system</a>
                            <a class="list-group-item" onclick="printSys();return false;" data-toggle="modal" data-target="#showSys"><span class="glyphicon glyphicon-print"></span>Print this customized system</a>
                            <a class="list-group-item" onclick="emailSys();return false;" data-toggle="modal" data-target="#showSys"><span class="glyphicon glyphicon-envelope"></span>Email this customized system</a>
                            <a class="list-group-item active" onclick="addToCart();return false;"><span class="glyphicon glyphicon-shopping-cart"></span>Add to Shopping Cart</a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-9" id="sysDetailListArea">
                <div role="tabpanel">

                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist" style="border-bottom: 1px solid #E8A28B;">
                        <li role="presentation" class="active"><a href="#MajorComponents" onclick="return false;" aria-controls="Major Components" role="tab" data-toggle="tab">Major Components</a></li>
                      <%--  <li role="presentation"><a href="#Accessories" aria-controls="Accessories" role="tab" data-toggle="tab">Accessories</a></li>--%>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content" style="border-left: 1px solid #E8A28B; border-right: 1px solid #E8A28B; border-bottom: 1px solid #E8A28B; padding: 5px;">
                        <div role="tabpanel" class="tab-pane active" id="MajorComponents">
                            <div class="list-group">
                                <%= SysListString %>
                            </div>
                        </div>
                        <div role="tabpanel" class="tab-pane" id="Accessories">
                            <div class="list-group">
                                <%= SysListStringAccessories %>
                            </div>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>

    <div class="modal fade bs-example-modal-lg" id='showSys'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">System: </h4>
                </div>
                <div class="modal-body">
                    <iframe id="iframeSys" name="iframeSys" src="" frameborder="0" style="height: 600px; width: 100%;"></iframe>
                </div>
                <div class="modal-footer">

                    <p id="modelWindowEmailBtnGroup">
                        <div class="form-inline">
                            <div class="form-group">
                                <label for="exampleInputEmail2">Email: </label>
                                <input type="email" class="form-control" id="exampleInputEmail2" placeholder="lu@example.com">
                            </div>

                            <a type="button" onclick="sendMail($(this));" class="btn btn-default"><span class="glyphicon glyphicon-envelope"></span>Send</a>
                            <a type="button" onclick="printing();" class="btn btn-default"><span class="glyphicon glyphicon-print"></span>Print</a>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </p>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->


    <div class="modal fade bs-example-modal-lg" id='showQuote'>
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Save Quote: </h4>
                </div>
                <div class="modal-body">
                    <img src="<%= LU.BLL.ImageHelper.Get("images/loaderc.gif") %>" />
                </div>
                <div class="modal-footer">
                    <p id="P1">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </p>
                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->



</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="Server">
    <script src="/scripts/jquery.pin.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $(".pinned").pin({
                containerSelector: ".listArea"
            })

            changePrice();
            changeCaseLogo();
        });

        function changeCaseLogo() {

            $('.sys-part-title').each(function () {
                if ($.trim($(this).children(":first").text()) == "Case") {
                    $('#sysCaseLogo').attr('src', '<%= LU.BLL.Config.ResHost %>pro_img/COMPONENTS/' + $(this).attr('partsku') + '_g_1.jpg');
                }
            });
        }

        function showPartDetailList(the) {
            var partsku = the.attr('partsku');
            var groupid = the.attr('groupid');

            var detailArea = the.next();

            if (detailArea.css('display') == 'none') {
                detailArea.css({ display: '' });
                the.find('.badge').eq(0).html('<span class="glyphicon glyphicon-chevron-right"></span>');
                if ($.trim(detailArea.html()) == "") {
                    detailArea.html("<img src='<%= LU.BLL.ImageHelper.Get("images/loaderc.gif")%>'>");
                    $.get("/cmds/systemProd.aspx", { cmd: 'getGroupDetail', partsku: partsku, t: Math.random(), partgroupid: groupid, syssku: '<%= ReqSKU%>' }, function (msg) {
                        detailArea.html(msg)
                    });

                }
            }
            else {
                detailArea.css({ display: 'none' });
                the.find('.badge').eq(0).html('<span class="glyphicon glyphicon-chevron-down"></span>');

            }
            $(".pinned").pin({
                containerSelector: ".listArea"
            })

        }

        function changePrice() {
            var priceSum = 0;
            $('.sys-part-title').each(function () {
                var priceItem = $(this).find('.itemPrice').eq(0).text();
                var partPrice = priceItem.replace("$", "").replace(' ', "").replace(",", "");
                priceSum += parseFloat(partPrice);
            });

            $('.regularPrice').html("$" + priceSum.toFixed(2));
            $('.specialCashPrice').html("$" + (priceSum * 0.98).toFixed(2));
        }

        function selectedPart(the) {
            var sysSku = the.attr("sysSku");
            var partSku = the.attr("partSku");
            var groupId = the.attr("partgroupid");
            var priceStr = the.find('.itemPrice').eq(0).html();
            var title = the.find('.itemTitle').eq(0).html();

            the.parent().find('a').each(function () {
                $(this).removeClass("list-group-item-info");

            });
            the.addClass("list-group-item-info");
            the.parent().parent().prev().find('.itemPrice').eq(0).html(priceStr);
            the.parent().parent().prev().find('.note').eq(0).html(title);
            the.parent().parent().prev().attr('partsku', partSku);
            //alert(partSku + "|" + the.parent().parent().prev().attr('partsku'));
            changePrice();
            changeCaseLogo();
        }

        function printing() {
            iframeSys.window.print();
        }

        // email sys
        function emailSys() {
            generateSysAndView('email');
        }
        // print sys
        function printSys() {
            generateSysAndView('print');
        }
        // show sys
        function showSys() {
            generateSysAndView('');
        }
        // add to cart
        function addToCart() {
            generateSysAndView('addtocart');
        }
        function generateSysAndView(t) {
            var parts = "";
            $('.sys-part-title').each(function () {
                var partsku = $(this).attr("partsku");
                var groupid = $(this).attr("groupid");
                var commentid = $(this).attr("commentid");

                if (parts == "")
                    parts += partsku + "|" + groupid + "|" + commentid;
                else
                    parts += "," + partsku + "|" + groupid + "|" + commentid;
            });

            $.ajax({
                type: "Post",
                url: "/cmds/systemProd.aspx",
                data: { cmd: "createSysFormCustomize", parts: parts, syssku: '<%= ReqSKU%>' },
                error: function (r) {
                    alert(r);
                },
                success: function (m, s) {
                    if (s == "success") {

                        if (t == "saveQuote") {
                            $('#showQuote').find(".modal-body").eq(0).html("<strong>This Configuration is already saved.</strong><br><small>It can be accessed by Quote Number:</small> <strong style='font-size:20px;'>" + m + "</strong>");
                        }
                        else if (t == "addtocart") {
                          //  window.location.href = ;
                            $.get('/ShoppingCartTo.aspx?sku=' + m, {}, function (data) {
                                var data = JSON.parse(data);
                                if (data.Success) {
                                    window.location.href = data.ToUrl;
                                }
                            });
                        }
                        else
                            $('#iframeSys').attr("src", "/detail_sys_print.aspx?sku=" + m);
                    }

                    else {
                    }
                }
            });
        }

        function saveQuote() {
            $('#showQuote').find(".modal-body").eq(0).html("<img src=\"<%= LU.BLL.ImageHelper.Get("images/loaderc.gif") %>\" />");

            generateSysAndView("saveQuote");
        }

        function sendMail(the) {
            // alert($(document.getElementById('iframeSys').contentWindow.document.body).html().replace(/\</g, "&lt;").replace(/\>/g, "&gt;"));
            // alert(the.prev().find('input').eq(0).val());

            if (the.prev().find('input').eq(0).val() == "") {
                alert("Please input email.");
                return;
            }
            $.post("/cmds/sentMail.aspx", {
                email: the.prev().find('input').eq(0).val()
            , htmlCont: $(document.getElementById('iframeSys').contentWindow.document.body).html().replace(/\</g, "&lt;").replace(/\>/g, "&gt;")
            }, function (data) {
                alert(data);
            });
        }


    </script>

</asp:Content>

