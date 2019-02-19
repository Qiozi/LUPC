$(function () {
    $(document).scroll(function () {
        var scrollTop = document.documentElement.scrollTop || window.pageYOffset || document.body.scrollTop;
        //var topBox = $('.topNavMain');
        // console.log(scrollTop)
        //if (scrollTop > 100) {
        //    $('.topNav').hide(1500);           
        //    $('.person-box').show(1000);
        //}
        //else {
        //    $('.topNav').show(1500);
        //    $('.person-box').hide(1000);
        //}

        //if (scrollTop > 500) {
        //    $(".btnToTop").fadeIn(500);

        //    if (topBox.css('height') === '100px') {
        //        topBox.css({ height: '99px', 'border-bottom': '1px solid blue' });
        //    }
        //}
        //else {
        //    $(".btnToTop").fadeOut(500);
        //    if (topBox.css('height') === '99px') {
        //        topBox.css({ height: '100px', 'border-bottom': '0' });
        //    }
        //}
    });

    $('.bs-example-modal-sm').on('show.bs.modal', function (event) {
        // var modal = $(this)
        setTimeout(function () {
            $('.typeahead').focus();
        }, 100)
    });
});