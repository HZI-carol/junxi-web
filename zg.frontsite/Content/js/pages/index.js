$(function () {
    $(".con-catalogue li,.module-news-selector>ul li, .module-company span").eq(0).addClass('active');
    $(".con-catalogue li,.module-news-selector>ul li, .module-company span").click(function () {
        //全景项目  点击按钮变样式
        $(this).siblings('li').removeClass('active');  // 删除其他兄弟元素的样式
        $(this).siblings('span').removeClass('active');  // 删除其他兄弟元素的样式
        $(this).addClass('active');
    });

    $(".bodys .related").hide();  //hide()隐藏
    $(".works-box>ul>li").click(function () {
        var _index = $(this).index();
        //  点击按钮变样式
        $(this).siblings('.works-box>ul>li').removeClass('active');  // 删除其他兄弟元素的样式
        $(this).addClass('active');

        $(".bodys div").eq(_index).show().siblings().hide();//siblings()同辈  兄弟
    });

    //首页banner轮播1
    var mySwiper = new Swiper('#swiper1', {
        pagination: '#swiper1-pagination',
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


    //判断 当数据小于3 时不轮播
    var len = $('.swiper-slide').length
    var flag = true
    var nextButton = $(".swiper-button-next")
    var prevButton = $('.swiper-button-prev')
    if (len <= 3) {
        flag = false
        nextButton.hide()
        prevButton.hide()
        $('.swiper-wrapper').addClass('reset-swiper')
    }

    //首页优秀装企轮播2

    var swiper = new Swiper('#swiper2', {
        autoplay: 4000,
        loop: flag,
        loopedSlides: 3,
        spaceBetween: 20,
        centeredSlides: true,
        slidesPerView: 'auto',
        nextButton: nextButton,
        prevButton: prevButton,
        preventLinksPropagation: false,
        autoplayDisableOnInteraction: false,
        paginationClickable: true,
    })


    //全景查看底部轮播3
    var swiper = new Swiper('#swiper3', {
        autoplay: 4000,
        loop: flag,
        spaceBetween: 20,
        centeredSlides: true,
        slidesPerView: 'auto',
        nextButton: '.swiper-button-next',
        prevButton: '.swiper-button-prev',
        preventLinksPropagation: false,
        autoplayDisableOnInteraction: false,
        paginationClickable: true,
    });

    //头部软件套餐专用
    $(".head-menu ul li:nth-child(4),.footer-center>.about ul li:nth-child(3)").click(function () {
        alert("该模块正在建设中")
    })
})
