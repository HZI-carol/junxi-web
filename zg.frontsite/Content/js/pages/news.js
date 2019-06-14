$(window).ready(function () {
    $('.news-category .button').click(function (e) {
        $('.news-category .button').removeClass('active')
        $(this).addClass('active')
    })
    setTimeout(function () {
        $('.module-loading').hide()
    }, 2000);

    $(".module-news-selector>ul li").eq(0).addClass('active');
    $(".module-news-selector>ul li").click(function () {
        //  点击按钮变样式
        $(this).siblings('li').removeClass('active');  // 删除其他兄弟元素的样式
        $(this).addClass('active');

    });
    //头部banner轮播1
    var mySwiper = new Swiper('#swiper1', {
        pagination: '#swiper1 .swiper-pagination',
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        loop: true,
        autoplay: 4000,//可选选项，自动滑动
        preventLinksPropagation: false,
        autoplayDisableOnInteraction: false,
        paginationClickable: true,
    });
    //鼠标移入 暂停轮播 移出开始轮播
    $('.swiper-slide').mouseenter(function () {
        mySwiper.stopAutoplay();
    })
    $('.swiper-slide').mouseleave(function () {
        mySwiper.startAutoplay();
    })
    //头部软件套餐专用
    $(".head-menu ul li:nth-child(4),.footer-center>.about ul li:nth-child(3)").click(function () {
        alert("该模块正在建设中")
    })

})
