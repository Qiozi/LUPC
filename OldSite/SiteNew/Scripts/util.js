var util = {
    webInit: function (data) {
        // empty
    },
    wuHttpLoad: function (url, data, callbackFn) {
        $.ajax({
            type: 'get',
            url: url,
            data: data,
            success: function (data) {
                callbackFn(data);
            }
        });
    },
    addToCart: function addToCart(sku) {
        $.get('/ShoppingCartTo.aspx', {
            sku: sku
        }, function (data) {
            data = JSON.parse(data);
            if (data.Success) {
                window.location.href = data.ToUrl;
            }
        });
    },
    alertError: function (txt) {
        swal({
            title: "Error!",
            text: txt,
            type: "error",
            confirmButtonText: "Close",
            timer: 2000
        });
    },
    alertSuccess: function (txt) {
        swal({
            title: "success!",
            text: txt,
            type: "success",
            confirmButtonText: "Close",
            timer: 2000
        });
    },
    alertWarning: function (txt) {
        swal({
            title: txt + "!",
            text: '',
            type: "warning",
            confirmButtonText: "Close",
            timer: 2000
        });
    },
    getRadioValue: function (radioName) {
        var result = "0";
        var chkRadio = document.getElementsByClassName(radioName);
        for (var i = 0; i < chkRadio.length; i++) {
            if (chkRadio[i].checked)
                result = chkRadio[i].value;
        }
        return result;
    },
    searchGo: function (key) {
        if (key === "") {
            this.alertWarning("please input keyword.");
            return;
        }
        var chk2 = this.getRadioValue("SearchCateItem");

        window.location.href = '/Search.aspx?cate=' + chk2 + '&key=' + key;
    },
    searchCategoryBtnGroup: function (the, cateTypeElement) {
        the.parent().find('.btn-danger').eq(0).removeClass("btn-danger").addClass("btn-default");
        the.addClass("btn-danger");
        cateTypeElement.val(the.data('v'));
    },
    getShippingQty: function (the) {
        this.wuHttpLoad('/cmds/getShippingCartQty.aspx', {}, function (response) {
            the.text(response)
        });
    }
}