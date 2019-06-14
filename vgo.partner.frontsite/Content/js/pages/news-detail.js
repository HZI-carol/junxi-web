$(window).ready(function () {
  $('.news-category .item').click(function () {
    $('.news-category .item').removeClass('active')
    $(this).addClass('active')
  })
})