var listCount = 0;
$(function () {
    $('.cate-item').on('click', function () {
        var $this = $(this);
        $this.parent().find('.cate-item').removeClass('actived');
        $this.addClass('actived');

        $('.cate-item-area').slideUp('show', function () {
            // $('.cate-item-area').css({ display: 'none' });                                   
        });
        setTimeout(function () {
            $('.cate-item-area').each(function () {
                if ($(this).data('cid') === $this.data('cid')) {
                    // $(this).css({ display: '' });
                    $(this).slideDown('750');

                }
            })
        }, 500);
    });


    $("img").lazyload({
        placeholder: "/pro_img/ebay_gallery/9/999999_ebay_list_t_1.jpg",
        effect: "fadeIn"
    });

    $('.leftMenuCateItemBox').on('mouseover mouseout', function (event) {
        var _parent = $(this);
        var _this = $(this).find('a').eq(0);
        if (event.type === "mouseover") {
            _parent.css({ 'border-right': '0' });
            _this.css({ display: 'inline-block' });
            _this.animate({ width: '210px', display: 'inline-block' }, 500);
        } else if (event.type === "mouseout") {

            setTimeout(function () {
                _this.animate({ width: '100px' }, 500, function () {
                    _parent.css({ 'border-right': '1px solid #ccc' });
                    _this.css({ display: 'none' });
                });
            }, 3000)
        }
    })

    var mySwiper = new Swiper('.swiper-container', {
        autoplay: 5000,//可选选项，自动滑动
        prevButton: '.swiper-button-prev',
        nextButton: '.swiper-button-next',
    })

    function reAffix() {
        setTimeout(function () {
            //$('#nav').data('bs.affix').options.offset = $('#nav').offset().top
            $('#leftCateList').affix({
                offset: {
                    top: function () {
                        console.log($('#rptSysList').offset().top);
                        var top = $('#rptSysList').offset().top;
                        //console.log(top)
                        if (top < 300) {
                            top += 500;
                        }
                        this.top = top;
                    }
                }
            });
        }, 100);
    }



    function init() {
        $('body').scrollspy({ target: '#leftCateList' });
        reAffix();
    }
    setTimeout(init, 1000);

    window.onresize = function () {
        reAffix();
      //  console.log($('#leftCateList')[0].offsetTop)
    }
});