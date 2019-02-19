'use strict';

var listCount = 0;

$(function () {
    $('#topKeyArea').affix({
        offset: {
            top: 100,
            bottom: function bottom() {
                return this.bottom = $('#page-bottom').outerHeight(true);
            }
        }
    });
    $('#topKeyArea').find('table').eq(0).css({ width: $('.container').outerWidth() - 64 });
    $('#navBtnArea').css({ width: $('.container').outerWidth() - 32 });

    $('.showFilterBtn').on('click', function () {
        var the = $(this);
        if ($('#keyListArea').css('display') == 'none') {
            $('#keyListArea').css({ display: '' });
            the.html("<span class=\"glyphicon glyphicon-chevron-up\"></span>");
        } else {
            $('#keyListArea').css({ display: 'none' });
            the.html("<span class=\"glyphicon glyphicon-chevron-down\"></span>");
        }
    });
});

