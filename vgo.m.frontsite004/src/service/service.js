import { Indicator, Toast } from 'mint-ui'
import qs from 'qs'

let configured = false

export default class Service {
  constructor ($vue) {
    this.$http = $vue.$http
    this.$router = $vue.$router
    if (!configured) {
      this.setup($vue)
      configured = true
    }
  }

  /**
   * setup axios defaults config
   */
  setup ($vue) {
    // Add a request interceptor
    this.$http.interceptors.request.use(config => {
      Indicator.open('玩命加载中...')
      return config
    }, function (error) {
      Indicator.close()
      return Promise.reject(error)
    })
    // Add a response interceptor
    this.$http.interceptors.response.use((response) => {
      Indicator.close()
      return response.data
    }, (error) => {
      Indicator.close()
      let message = error.message || '请求出错，请重试～'
      Toast(message)
      return Promise.reject(error)
    })
  }

  /**
   * 返回结果处理
   * @param res
   * @param resolve
   * @private
   */
  _handleResult (res, resolve, reject) {
    if (res.code === 100 || res.code === '100') {
      resolve(res.data)
    } else {
      Toast(res.msg || '请求出错，请重试～')
      reject(res)
    }
  }

  /**
   * get方式请求接口
   * @param url
   * @param data
   * @returns {Promise}
   */
  httpGet (url, data) {
    return new Promise((resolve, reject) => {
      this.$http.get(url, { params: data }).then(res => {
        this._handleResult(res, resolve, reject)
      }).catch(err => reject(err))
    })
  }

  /**
   * post 'application/x-www-form-urlencoded' formdata
   * @param url
   * @param data
   * @returns {Promise<any>}
   */
  httpPost (url, data) {
    return new Promise((resolve, reject) => {
      this.$http.post(url, qs.stringify(data || {})).then(res => {
        this._handleResult(res, resolve, reject)
      }).catch(err => reject(err))
    })
  }
}
