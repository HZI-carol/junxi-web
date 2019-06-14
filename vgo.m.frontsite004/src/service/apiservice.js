/* eslint-disable camelcase,no-undef */
import Service from './service'

export default class Apiservice extends Service {
  /**
   * 获取首页作品
   * @param count
   * @returns {Promise}
   */
  getIndexPanoList (count) {
    return this.httpGet('api/frontsite/indexpanos', { count })
  }

  /**
   * 获取作品列表
   * @param page
   * @param pageSize
   * @param keyword
   * @param tag_id
   * @param user_id
   * @param orderBy
   * @returns {Promise}
   */
  getPanoList (page, pageSize, keyword = '', tag_id = '', user_id = '', orderBy = '') {
    user_id = user_id === '' ? -1 : user_id
    return this.httpGet(`api/frontsite/panos/${page}/${pageSize}`, { tag_id, keyword, user_id, orderBy })
  }

  /**
   * 获取新闻类型列表
   * @returns {Promise}
   */
  getNewsTypeList () {
    return this.httpGet('api/frontsite/newstypes')
  }

  /**
   * 获取新闻列表
   * @param page
   * @param pageSize
   * @param type_id
   * @returns {Promise}
   */
  getNewsList (page, pageSize, type_id = '') {
    type_id = type_id === '' ? -1 : type_id
    return this.httpGet(`api/frontsite/news/${page}/${pageSize}`, { type_id })
  }

  /**
   * 获取新闻详情
   * @param id
   * @returns {Promise}
   */
  getNewsDetail (id) {
    return this.httpGet(`api/frontsite/news/${id}`)
  }

  /**
   * 获取标签列表
   * @returns {Promise}
   */
  getTagList () {
    return this.httpGet('api/frontsite/tags')
  }

  /**
   * 获取作者列表
   * @param pageIndex
   * @param pageSize
   * @param keyword
   * @returns {Promise}
   */
  getAuthorList (page, pageSize, keyword = '') {
    return this.httpGet(`api/frontsite/users/${page}/${pageSize}`, { keyword })
  }

  /**
   * 获取作者详情
   * @param id
   * @returns {Promise}
   */
  getAuthor (id) {
    return this.httpGet(`api/frontsite/users/${id}`)
  }

  /**
   * 获取系统配置
   * @returns {Promise}
   */
  getConfigs () {
    return this.httpGet('api/frontsite/configs')
  }

  /**
   * 提交约拍信息
   * @param data
   * @returns {Promise<any>}
   */
  addPanoTask (data) {
    return this.httpPost('api/frontsite/users/panotasks', data)
  }

  /**
   * 设置微信分享
   * @param wxData
   */
  setShareData (wxData) {
    wx.ready(function () {
      // 监听“分享给朋友”，按钮点击、自定义分享内容及分享结果接口
      wx.onMenuShareAppMessage({
        title: wxData.title,
        desc: wxData.desc,
        link: wxData.link,
        imgUrl: wxData.imgUrl
      })

      // 监听“分享到朋友圈”按钮点击、自定义分享内容及分享结果接口
      wx.onMenuShareTimeline({
        title: wxData.title,
        link: wxData.link,
        imgUrl: wxData.imgUrl
      })

      // 监听“分享到QQ”按钮点击、自定义分享内容及分享结果接口
      wx.onMenuShareWeibo({
        title: wxData.title,
        desc: wxData.desc,
        link: wxData.link,
        imgUrl: wxData.imgUrl
      })

      // 监听“分享到微博”按钮点击、自定义分享内容及分享结果接口
      wx.onMenuShareWeibo({
        title: wxData.title,
        desc: wxData.desc,
        link: wxData.link,
        imgUrl: wxData.imgUrl
      })

      wx.showOptionMenu()
    })
  }

  /**
   * 获取微信配置
   * @returns {Promise}
   */
  setJssdkConfig (wxData) {
    console.info(wxData)
    this.httpGet('api/wxplat/jssdk/wxconfig', { url: location.href.split('#')[0] }).then(data => {
      wx.config({
        debug: false,
        appId: data.appId,
        timestamp: data.timestamp,
        nonceStr: data.nonceStr,
        signature: data.signature,
        jsApiList: [
          'checkJsApi',
          'showOptionMenu',
          'onMenuShareTimeline',
          'onMenuShareAppMessage',
          'onMenuShareQQ',
          'onMenuShareWeibo'
        ]
      })
      this.setShareData(wxData)
    })
  }
}
