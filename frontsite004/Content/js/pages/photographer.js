

$(window).ready(function () {
  getStatus($(".business>div[class^='section']"))
  $(window).scroll(function() {
    getStatus($(".business>div[class^='section']"))
  })
  setTimeout(function() {
    $('.module-loading').hide()
  }, 2000)
})

function getStatus(Dom) {
  var windowHeight = $(window).height();   //获取浏览器窗口高度
  for (let i = 0; i < Dom.length ; i++) {
    if ($(document).scrollTop() >= $(Dom[i]).offset().top - windowHeight/1.5) {
      $(Dom[i]).addClass('on')
    }
  }
}