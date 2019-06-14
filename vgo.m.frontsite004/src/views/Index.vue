<template>
  <div class="common_body">
    <!-- 轮播图 -->
    <div class="lbt">
      <mt-swipe :auto="4000">
        <mt-swipe-item :key="index" v-for="(item, index) in bannerList">
          <div class="item"><img :src="item"/></div>
        </mt-swipe-item>
      </mt-swipe>
    </div>
    <!-- 轮播图  end-->

    <!-- 推荐栏目 -->
    <div class="recommend">
      <ul>
        <li>
          <router-link class="link" to="/pano">
            <i class="iconfont icon-VRquanjingtu"></i>
            <div>VR全景</div>
          </router-link>
        </li>
        <li>
          <router-link class="link" to="/author">
            <i class="iconfont icon-zuozhe"></i>
            <div>{{$globalconfig.author_alias}}</div>
          </router-link>
        </li>
        <li>
          <router-link class="link" to="/news">
            <i class="iconfont icon-tuwen"></i>
            <div>新闻</div>
          </router-link>
        </li>
        <li>
          <router-link class="link" to="/support">
            <i class="iconfont icon-dianhua"></i>
            <div>关于我们</div>
          </router-link>
        </li>
      </ul>
    </div>
    <!-- 推荐栏目 end-->

    <!-- 推荐作品 -->
    <pano-list :list='panoList' title="精彩世界 全景展示"/>
    <!-- 推荐作品  end-->

    <!-- 行业资讯 -->
    <div class="indunews">
      <div class="ti">最新动态</div>
      <div class="msg_detail">
        <div class="content">
          <ul>
            <li :key="item.id" v-for="item in newsList">
              <a @click="$router.push(`/news/${item.id}`)">
                <img v-lazy="item.full_cover_url"/>
                <div class="title">{{item.title}}</div>
                <div class="desc">作者：{{item.author}}</div>
                <div class="time">
                  <p>{{item.created | datetimeFilter}}</p>
                </div>
              </a>
            </li>
          </ul>
          <div @click="$router.push('/news')" class="common_more">
            <span>查看更多</span>
          </div>
        </div>
      </div>
    </div>
    <!-- 行业资讯 end -->
  </div>
</template>

<script>
  export default {
    components: {
      PanoList: () => import('@/components/PanoList')
    },
    data () {
      return {
        bannerList: [],
        panoList: [],
        videoList: [],
        isChangeNavBg: false,
        newsList: []
      }
    },
    created () {
      this.bannerList = this.$globalconfig.banners
      this.getIndexPanoList()
      this.getNewsList()
    },
    methods: {
      getIndexPanoList () {
        this.$service.api.getIndexPanoList(8).then(d => {
          this.panoList = d
        })
      },
      getNewsList () {
        return this.$service.api.getNewsList(1, 5).then(d => {
          this.newsList = d.list
        })
      }
    }
  }
</script>
<style lang="stylus">
  @import '../scss/variable.styl'
  .app-index
    .common_body
      margin-top: 50px

      .lbt
        height: (9 / 16 * 100vw)
        width: 100vw
        position: relative

        .mint-swipe
          .mint-swipe-item
            .item
              position: relative
              width: 100%
              height: 100%
              overflow: hidden

              img
                height: 100%
                position: absolute
                left: 50%
                transform: translateX(-50%)

      .recommend
        background-color: white
        padding: 15px 10px 5px

        ul
          overflow: hidden

          li
            display: block
            float: left
            width: 25%
            text-align: center

            .link
              text-align: center

              i
                display: inline-block
                width: 40px
                height: 40px
                line-height: 40px
                text-align: center
                border-radius: 50%
                color: #fff
                background-image: linear-gradient(180deg, #fff720 0%, #3cd500 60%)
                font-size: 26px

              div
                line-height: 30px

      .content
        .lists
          border: 1px solid #eee

      .indunews
        background: white
        margin-top: 10px
        padding: 0 5px

        .ti
          font-size: 18px
          color: grey
          height: 50px
          line-height: 50px
          background-color: white
          text-align: center
          border-bottom: 1px solid #eee

        .msg_detail
          margin-top: 10px
          background-color: #fff

          .content
            background-color: white
            margin: 0 0 26px 0

            a
              display: block
              position: relative
              padding: 5px 10px 0 130px
              background: white
              border-bottom: 1px solid #eee
              min-height: 75px

              img
                position: absolute
                left: 10px
                top: 12px
                width: 110px
                height: 65px

          .title
            font-size: 14px
            line-height: 25px

          .desc
            line-height: 20px
            margin-left: 2px
            font-size: 12px
            color: #999

          .time
            height: 35px
            line-height: 35px
            padding-right: 10px
            text-align: right
            color: #999

      .nav
        background-color: white
        text-align: center
        border-bottom: 1px solid #eee
        display: block

        ul
          height: 50px
          line-height: 50px

          li
            width: 25%
            float: left
            float: left

            &.cur
              color: $theme
              border-bottom: 3px solid $theme
</style>
