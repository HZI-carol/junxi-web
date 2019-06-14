import axios from 'axios'
import Apiservice from './apiservice'

let $service

export default {
  install (Vue, options) {
    axios.defaults.baseURL = Vue.$globalconfig.apiurl
    Vue.mixin({
      created () {
        if (!this.$http) {
          this.$http = axios
        }
        if (!$service) {
          const api = new Apiservice(this)
          $service = { api }
        }
        this.$service = $service
      }
    })
  }
}
