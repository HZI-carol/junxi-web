$(window).ready(function () {
    $('.news-category .button').click(function (e) {
        $('.news-category .button').removeClass('active')
        $(this).addClass('active')
    })
    setTimeout(function() {
        $('.module-loading').hide()
    }, 2000)
})