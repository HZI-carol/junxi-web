<template>
  <div>
    <div :class="['top_navbar' ,{'bg': isChangeNavBg}]">
      <!--导航栏-->
      <div class="logo">
        <a :href="$globalconfig.disable_logo_link ? 'javascript:;': '/'">
          <img :src="$globalconfig.logo"/>
        </a>
      </div>
      <div class="search-wrap">
        <div :class="['search',{active: isFocus}]">
          <input @blur="isFocus = false" @focus="isFocus = true" class="text"
                 placeholder="搜索VR全景" type="text" v-model="keyword"/>
          <div @click="searchPano" class="iconfont icon-search btn"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        keyword: '',
        isFocus: false,
        isChangeNavBg: false
      }
    },
    created () {
      window.addEventListener('scroll', (e) => {
        let scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
        if (scrollTop >= 60) {
          this.isChangeNavBg = true
        } else {
          this.isChangeNavBg = false
        }
      })
    },
    methods: {
      searchPano () {
        this.$router.push(`/pano?keyword=${this.keyword}`)
      }
    }
  }
</script>

<style lang="stylus">
  @import '../../scss/variable.styl';
  .menu.is-open
    right: 0

  .open_menu
    right: 0 !important
    -webkit-transition: all .5s ease
    transition: all .5s ease

  .menu_blank
    display: none
    background: #000
    opacity: 0.4
    width: 100%
    height: 100%
    position: fixed
    left: 0
    top: 0
    bottom: 0
    z-index: 1000

  .menu
    position: fixed
    top: 0
    right: -480px
    z-index: 9999
    width: 170px
    height: 100%
    background: #323436
    transition: all 0.5s cubic-bezier(1, -0.26, 0, 1.23)

    .title
      color: #fff
      height: 50px
      font-size: 1.2em
      line-height: 50px
      text-align: center
      background-color: $theme

    ul
      li
        display: block
        height: 49px
        line-height: 50px
        border-bottom: 1px solid #44474a
        overflow: hidden
        background-color: #323436
        transition: background 0.3s ease-out

        a
          /*color: #fff*/
          display: block
          width: 100%
          height: 49px
          overflow: hidden
          color: #cecece

          i
            margin: 0 5px 0 20px

  .top_navbar
    display: flex
    position: fixed
    justify-content: space-between
    top: 0
    z-index: 999
    padding: 10px 0 0 0
    background: $theme
    height: 40px
    width: 100%

    .logo
      left: 10px
      position: relative

      img
        height: 30px

    .search-wrap
      flex 1
      box-sizing border-box
      padding-right 1em

    .search
      width 122px
      float right
      position: relative
      background-color: #eee
      height: 30px
      opacity 0.5
      padding: 0 35px 0 15px
      border-radius: 20px
      box-sizing border-box
      transition all 0.38s

      &.active
        opacity 0.8
        width 80%

      .text
        width: 100%
        height: 30px
        background: 0
        border: 0

      .btn
        background: 0
        border: 0
        position: absolute
        right: 0
        top: 0
        width: 30px
        height: 30px
        text-align center
        line-height 30px
        cursor: pointer

      i
        position: absolute
        right: 10px
        top: 7px
        color: #000
        pointer-events: none

        img
          padding-top: 3px
          width: 15px
          height: 15px

    .menu_btn
      padding: 5px 10px 0 0
      margin-left 10px

      i
        font-size: 18px
        color: #fff
</style>
