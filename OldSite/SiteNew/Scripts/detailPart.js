$(function () {
    $('.question-submit').on('click', function () {
        $.ajax({
            type: "post",
            url: "/cmds/SaveQuestion.aspx",
            data: {
                sku: $('input[name=sku]').val(),
                username: $('#questEmail').val(),
                subject: $('#questSubject').val(),
                questBody: $('#questBody').val()
            },
            error: function (r, s, t) {
                util.alertError(r);
            },
            success: function (msg, s) {
                if (s == "success") {
                    util.alertSuccess(msg);
                    $('#questEmail').val('');
                    $('#questSubject').val('');
                    $('#questBody').val('');
                    $('#myQuestionModal').modal('hide')
                }
            }
        });
    });
    $('#btnToCart').on('click', function () {
        stockValid(true);
        $.get("/ShoppingCartTo.aspx", {
            sku: $('input[name=sku]').val()
        }, function (data) {
            var data = JSON.parse(data);
            if (data.Success) {
                window.location.href = data.ToUrl;
            }
        });
    });
    $('#topKeyArea').affix({
        offset: {
            top: 50,
            bottom: function () {
                //alert((this.bottom = $('#page-bottom').outerHeight(true)));
                return (this.bottom = $('#page-bottom').outerHeight(true));
            }
        }
    });

    //$('#topKeyArea')
    //    .find('table')
    //    .eq(0)
    //    .css({
    //        width: $('.container').outerWidth() - 64
    //    });
    $.get("/cmds/prod.aspx", {
        cmd: 'getPartCateAllQty',
        cid: $('input[name=cateId]').val(),
        sku: $('input[name=sku]').val()
    }, function (data) {
        $('#partCateAllQtyArea').html(data);
    });

    function stockValid(y) {
        $('span').each(function () {
            if ($(this).html() == "Out of Stock") {
                $('#btnToCart').addClass('disabled').css({ 'background-color': '#ccc', 'color': '#333' }).append("(Out of Stock)");
                if (y)
                    util.alertError('Out of Stock')
            }
        })
    }
    stockValid(false);
});