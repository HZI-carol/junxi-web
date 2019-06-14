<template>
  <div :class="'app-' + ($route.name || 'index').toLowerCase()" id="app">
    <app-nav/>
    <router-view/>
    <app-footer v-show="showFooter"/>
  </div>
</template>

<script>
  import AppNav from './components/shared/AppNav'
  import AppFooter from './components/shared/AppFooter'

  export default {
    components: {
      AppNav,
      AppFooter
    },
    data () {
      return {
        showFooter: true
      }
    },
    watch: {
      $route (to) {
        this.showFooter = !(this.$globalconfig.hide_author_detail_footer && to.name === 'AuthorDetail')
      }
    }
  }
</script>

<style lang="stylus">
  @import './scss/_reset.css';
  @import './scss/index.styl';
  .app-index
    .top_navbar
      background: linear-gradient(rgba(0, 0, 0, .6), transparent)

      &.bg
        background: rgba(0, 0, 0, 0.4)

    & > .common_body
      margin-top: 0 !important
</style>
