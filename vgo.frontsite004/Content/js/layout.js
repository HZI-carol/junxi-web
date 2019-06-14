$(window).ready(function () {
    $(window).scroll(function () {
        if ($(window).scrollTop() > 100) {
            $(".module-header").css({ 'backgroundColor': 'rgba(0, 0, 0, 0.9)' });
        } else {
            $(".module-header").css({ 'backgroundColor': 'rgba(0, 0, 0, 0.4)' });
        }
    })
    //$('.confirm-order .close').click(function (params) {
    //    $('.confirm-order').fadeOut()
    //})
    //$('.order-detail .close').click(function (params) {
    //    $('.order-detail').fadeOut()
    //})
    //$('.options-carts').click(function (params) {
    //    $('.shopping-carts').addClass('active')
    //})
    //$('.shopping-carts .close').click(function (params) {
    //    $('.shopping-carts').removeClass('active')
    //})
})