<template>
  <div class="common_body">
    <div class="msg_detail">
      <div id="tel">
        <div class="title">{{$globalconfig.platname}}</div>
        <div class="content">
          <div class="row" v-if="$globalconfig.hotline">
            <span class="info">咨询热线：{{$globalconfig.hotline}}</span>
            <a :href="'tel:'+$globalconfig.hotline" class="options">
              <mt-button size="small" type="primary">立即咨询</mt-button>
            </a>
          </div>
          <div class="row" v-if="$globalconfig.service_qq">
            <span>客服Q Q：{{$globalconfig.service_qq}}</span>
            <a :href='`http://wpa.qq.com/msgrd?v=3&uin=${$globalconfig.service_qq}&site=qq&menu=yes`'
               class="options">
              <mt-button size="small" type="primary">Q Q联系</mt-button>
            </a>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
  }
</script>

<style lang='stylus'>
  .app-support
    .nav
      background-color: white
      text-align: center
      border-bottom: 1px solid #eee
      display: block

      ul
        height: 50px
        line-height: 50px

        li
          width: 50%
          float: left

          &.cur
            color: #2dbb55
            border-bottom: 3px solid #2dbb55

    .msg_detail
      margin-top: 10px
      background-color: #fff

      .is-show
        display: block !important

      .title
        border: 1px solid #EEE
        padding: 10px
        text-align: center
        font-size: 20px
        font-weight: bold

      .content
        padding: 0 10px
        border: 1px solid #eee

        .row
          line-height: 30px
          font-size: 14px
          display flex
          justify-content space-between
          border-top 1px solid #eee
          margin-bottom 10px
          padding-top 10px

          &:first-child
            border-top none
</style>
