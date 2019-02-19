$(function () {

    $('.iCheckRadio').iCheck({
        checkboxClass: 'icheckbox_square',
        radioClass: 'iradio_square-green',
        increaseArea: '20%' // optional

    });
    // $('.typeahead').typeahead()

    var searchSelectCate = function () {
        var $input = $(".typeahead");

        //$.get("/Computer/ForSearch/" + id + ".json", function (data) {
        //    $input.typeahead({ source: data, delay: 500 });
        //    console.log(data);
        //}, 'json');
        $input.typeahead({
            items: 20,
            source: function (query, process) {
                //query是输入的值
                var id = util.getRadioValue("SearchCateItem");
                console.log(id);
                if (id != "0") {
                    $.get("/Computer/ForSearch/" + id + ".json", function (e) {
                        process(e);
                    }, "json");
                }
                else {
                    process([]);
                }
            },
            updater: function (item) {
                return item;
            }
        });
    }

    $('.SearchCateItem').on('ifChecked', function () {
        searchSelectCate();
    });
    searchSelectCate();

    setTimeout(function () {
        util.getShippingQty($('.cart-badge'));
    }, 1000)
});
