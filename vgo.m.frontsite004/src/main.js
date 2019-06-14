// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import App from './App'
import router from './router'
import dateformat from 'dateformat'
import Service from './service'
import './components/MintUI'
import 'mint-ui/lib/style.min.css'
import Config from './service/config'

Vue.config.productionTip = false

Vue.use(Config)
Vue.use(Service, { baseURL: Config })

Vue.filter('dateFilter', function (value) {
  if (!value) {
    return ''
  }
  return dateformat(value, 'yyyy-mm-dd')
})

Vue.filter('datetimeFilter', function (value) {
  if (!value) {
    return ''
  }
  return dateformat(value, 'yyyy-mm-dd HH:MM:ss')
})

/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  components: { App },
  template: '<App/>',
  created () {
    console.info('v1.3.20190418')
    // 此处可兼容新旧版本
    if (!window.$globalconfig) {
      this.$service.api.getConfigs().then(d => {
        console.info(this.$globalconfig)
        this.$globalconfig.platname = d.platname
        this.$globalconfig.copyright = d.common_page_footer
        this.$globalconfig.hotline = d.common_page_hotline
        this.$globalconfig.service_qq = d.common_page_serviceqq
      })
    }
  }
})
