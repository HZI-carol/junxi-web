/**
 * 手机端配置
 * @type {*|{}}
 */
const vgo = window.VGO_MOBILE_CONFIG || {}
const $gc = window.$globalconfig || {
  URLS: {}
}
/**
 * 判断是否http地址
 * @param str
 * @returns {boolean}
 */
// const isHttpUrl = (str) => {
//   const reg = new RegExp('(http|https)://[a-zA-Z0-9-.]+.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9-._?,\'/\\+&$%$#=~])*$')
//   return reg.test(str)
// }
const config = {
  /**
   * logo地址, 正式环境为接口站点的目录下
   */
  logo: $gc.LOGO || vgo.logo || 'content/images/logo.png',
  /**
   * 平台名称
   */
  platname: $gc.PLATFORM_NAME || '',
  /**
   * 接口请求地址
   */
  apiurl: $gc.URLS.API || vgo.apiurl || 'http://localhost:22945/',
  /**
   * 咨询热线
   */
  hotline: $gc.SERVICE_HOTLINE || '',
  /**
   * 服务qq
   */
  service_qq: $gc.SERVICE_QQ || '',
  /**
   * 版权
   */
  copyright: $gc.COPYRIGHT || '',
  /**
   * banner图片集合
   */
  banners: vgo.banners || [],
  /**
   * 作者别名
   */
  author_alias: vgo.author_alias || '作者',
  /**
   * 是否显示约拍按钮
   */
  show_author_panotask: vgo.show_author_panotask || false,
  /**
   * 禁止logo上添加跳转链接
   */
  disable_logo_link: vgo.disable_logo_link || false,
  /**
   * 隐藏作者详情页底部导航
   */
  hide_author_detail_footer: vgo.hide_author_detail_footer || false
}
// 设置是否部署状态
if (process && process.env.NODE_ENV === 'production') {
  config.prod = true
} else {
  config.logo = 'http://localhost:22945/krpano/images/logo.png'
  config.apiurl = 'http://localhost:22945/'
}

if (!config.apiurl.endsWith('/')) {
  config.apiurl += '/'
}

export default {
  install (Vue, options) {
    Vue.$globalconfig = config
    Vue.prototype.$globalconfig = config
    Vue.mixin({
      created () {
        this.$globalconfig = config
      }
    })
  }
}
