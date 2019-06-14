var tabId = 1

$(window).ready(function () {
  $('.about-category .item').click(function () {
    showTab(tabId, $(this).attr('tab'))
    tabId = $(this).attr('tab')
    $('.about-category .item').removeClass('active')
    $(this).addClass('active')
  })
  if (tabId == 1) {
    getStatus($(".company-intro>div[class^='section']"))
    $(window).scroll(function() {
      getStatus($(".company-intro>div[class^='section']"))
    })
  }
  $('.tip-dialog .close-dialog').click(function (e) {
    $('.module-tip').hide()
  })
  $('.tip-dialog .refresh').click(function (e) {
    window.location.reload()
  })
})

function getStatus(Dom) {
  var windowHeight = $(window).height();   //获取浏览器窗口高度
  for (let i = 0; i < Dom.length ; i++) {
    if ($(document).scrollTop() >= $(Dom[i]).offset().top - windowHeight/1.3) {
      $(Dom[i]).addClass('on')
    }
  }
}

function removeOn(Dom) {
  for (let i = 0; i < Dom.length ; i++) {
    $(Dom[i]).removeClass('on')
  }
}

function showTab(oldId, newId) {
  var domArr = $('.about>div[class^="company-"]')
    $(domArr[oldId-1]).fadeOut('fast', function (params) {
      $(domArr[newId-1]).fadeIn('fast', function (params) {
        if (tabId == 2) {
          getStatus($(".company-course>div[class^='section']"))
          removeOn($(".company-honour>div[class^='section']"))
          removeOn($(".company-intro>div[class^='section']"))
        }
        if (tabId == 3) {
          getStatus($(".company-honour>div[class^='section']"))
          removeOn($(".company-course>div[class^='section']"))
          removeOn($(".company-intro>div[class^='section']"))
        }
        if (tabId == 1) {
          getStatus($(".company-intro>div[class^='section']"))
          removeOn($(".company-course>div[class^='section']"))
          removeOn($(".company-honour>div[class^='section']"))
        }
      })
    })
}
