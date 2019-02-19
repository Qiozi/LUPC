var util = {
    httpError: function () {
        swal({
            title: '',
            type: 'warning',
            text: '服务器请求出错，请稍后再试。',
            timer: 2000,
            showConfirmButton: true
        });
    },
    initWebData: function (scope, response) {
        console.log('init')
    },
    alertSuccess: function (txt) {
        swal({
            title: '',
            type: 'success',
            text: txt,
            timer: 2000,
            showConfirmButton: true
        });
    },
    alertError: function (txt) {
        swal({
            title: '',
            type: 'warning',
            text: txt,
            timer: 2000,
            showConfirmButton: true
        });
    },
    fade: function () {
        return {
            enter: function (element, done) {
                element.css('display', 'none');
                $(element).fadeIn(1000, function () {
                    done();
                });
            },
            leave: function (element, done) {
                $(element).fadeOut(1000, function () {
                    done();
                });
            },
            move: function (element, done) {
                element.css('display', 'none');
                $(element).slideDown(500, function () {
                    done();
                });
            }
        }
    }
}