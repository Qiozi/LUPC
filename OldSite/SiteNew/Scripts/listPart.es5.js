'use strict';

var listCount = 0;
var page = 1;

$(function () {
    //$('#btn-list-style-1').addClass("active");
    var listStyle = $.cookie('prodListStyle');
    if (listStyle === null) listStyle = 1;
    //alert(listStyle);
    $('#topKeyArea').affix({
        offset: {
            top: 100,
            bottom: function bottom() {
                //alert((this.bottom = $('#page-bottom').outerHeight(true)));
                return this.bottom = $('#page-bottom').outerHeight(true);
            }
        }
    });
    $('#topKeyArea').find('table').eq(0).css({ width: $('.container').outerWidth() - 64 });
    $('#navBtnArea').css({ width: $('.container').outerWidth() - 32 });

    //$(window).scroll(function () {
    //    if ($('#storeListStyle').val() == 2) {
    //        if ($(document).height() - $(this).scrollTop() - $(this).height() < 50) {
    //            loadList($('#storeListStyle').val());
    //        }
    //    }
    //});
    //loadList(listStyle);
    $("img.lazy").lazyload();
});

function navAffix() {}

function showFilter(the) {
    if ($('#keyListArea').css('display') === 'none') {
        $('#keyListArea').css({ display: '' });
        the.html("<span class=\"glyphicon glyphicon-chevron-up\"></span>");
    } else {
        $('#keyListArea').css({ display: 'none' });
        the.html("<span class=\"glyphicon glyphicon-chevron-down\"></span>");
    }
    $('#topKeyArea').css({ width: $(this).parent().css('width') });
}
console.log($('.closebutton').length);
if ($('.closebutton').length === 0) showFilter($(".showKeyAreaBtn"));

