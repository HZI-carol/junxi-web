

$(window).ready(function () {
    // 摄影师详情
    $('.profile-button').click(function () {
        $('.detail-dialog').addClass('show')
    })
    $('.detail-dialog .close-dialog').click(function () {
        $('.detail-dialog').removeClass('show')
    })
    $('.detail-dialog .content').click(function (e) {
        e.stopPropagation()
    })
    $('.detail-dialog').click(function (e) {
        $('.detail-dialog').removeClass('show')
        e.stopPropagation()
    })
    // 约拍
    $('.appointment-button').click(function () {
        $('.appoint-dialog').fadeIn()
    })
    $('.appoint-dialog .close-dialog').click(function () {
        $('.appoint-dialog').fadeOut()
    })
    $('.appoint-dialog .content').click(function (e) {
        e.stopPropagation()
    })
    $('.appoint-dialog').click(function (e) {
        $('.appoint-dialog').fadeOut()
        e.stopPropagation()
    })
    // 关闭加载
    setTimeout(function () {
        $('.module-loading').hide()
    }, 2000)
})

