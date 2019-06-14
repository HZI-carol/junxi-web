<template>
  <div class="common_body">
    <!-- 分类导航 -->
    <section class="me">
      <div class="cell" v-for="item in tagList" :key="item.value">
        <span class="_cl" @click="tagClick(true, item)"
              :class="item.value === tag_value || item.value === selectTag.value ? 'cur' : ''">
          {{item.text}}
          <img src="../../public/static/images/slide.png" v-if="item.childtags.length > 0"/>
        </span>
        <div class="bomb_box" :class="isOpenChildTag ? 'is-open' : ''">
          <div class="head">
            <div class="l"><span>{{selectTag.text}}</span></div>
            <div class="r"><img src="../../public/static/images/close.png" class="close" @click="closeChildTag"/></div>
          </div>
          <div class="cont">
            <ul>
              <li v-for="child in selectTag.childtags" :key="child.value"
                  :class="child.value === tag_value ? 'cur' : ''"
                  @click="tagClick(false, child)">
                <div><i></i><span>{{child.text}}</span><i class="e"></i></div>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </section>
    <!-- 分类导航 end-->
    <pano-list :list='list' :total='total' @more='getMore'/>
  </div>
</template>

<script>
  export default {
    components: {
      PanoList: () => import('@/components/PanoList')
    },
    data () {
      return {
        tagList: [
          {
            text: '全部',
            value: '',
            childtags: []
          }
        ],
        list: [],
        page: 1,
        total: 0,
        keyword: '',
        tag_id: '',
        tag_value: '',
        // 是否显示子标签
        isOpenChildTag: false,
        // 选择的带有子节点的标签
        selectTag: {}
      }
    },
    created () {
      this.keyword = this.$route.query.keyword || ''
      this.getTagList()
      this.getList()
    },
    watch: {
      $route (to, from) {
        this.keyword = to.query.keyword || ''
        this.tag_id = ''
        this.tag_value = ''
        this.search()
      }
    },
    methods: {
      getTagList () {
        this.$service.api.getTagList().then(d => {
          this.tagList.push(...d)
        })
      },
      getList () {
        this.$service.api.getPanoList(this.page, 10, this.keyword, this.tag_id).then(d => {
          this.list.push(...d.list)
          this.total = d.count
        })
      },
      tagClick (isparent, item) {
        // 若为当前选中标签则返回
        if (item.value === this.tag_value) {
          return
        }
        if (isparent && item.childtags.length > 0) {
          this.isOpenChildTag = true
          this.selectTag = item
        } else {
          this.isOpenChildTag = false
          if (isparent) {
            this.selectTag = {}
          }
          this.tag_id = item.value === 0 ? '' : item.text
          this.tag_value = item.value
          this.search()
        }
      },
      closeChildTag () {
        this.isOpenChildTag = false
        if (!this.selectTag.value) {
          this.selectTag = {}
        }
      },
      search () {
        this.page = 1
        this.list = []
        this.total = 0
        this.getList()
      },
      getMore () {
        if (this.list.length < this.total) {
          this.page++
          this.getList()
        }
      }
    }
  }
</script>

<style lang="stylus" scoped>
  @import '../scss/variable.styl';
  .app-pano
    .common_body
      .me
        height 40px
        line-height 40px
        width 100%
        background-color rgba($theme, 0.72)
        display -webkit-box
        justify-content space-between
        flex-flow row nowrap
        align-items center
        overflow-x scroll

        .cell
          max-width 10em
          text-align center
          color white
          padding 0 1em

          img
            width 9px
            height 4.5px
            padding 0 0 2px 4px

          .cur
            color #2dbb55 !important

          .bomb_box
            display none
            width 100%
            height 100%
            background #f0f1f1
            z-index 9999
            position fixed
            left 0
            right 0
            bottom 0

            .cont
              width 100%
              height 100%
              overflow-y auto

              .cur
                background #2dbb55

              ul
                li
                  display block
                  height 40px
                  background #fff
                  width 48%
                  margin 1% 1%
                  float left
                  text-align left

              li
                i
                  width 16px
                  height 16px
                  display inline-block
                  vertical-align middle
                  margin-top 15px

                span
                  display inline-block
                  height 40px
                  vertical-align middle
                  font-size 12px
                  line-height 40px
                  color black

                .e
                  background #ffffff
                  width 5px
                  height 5px
                  border 3px solid #d1d1d1
                  float right
                  margin-right 10px
                  -o-border-radius 50%
                  -moz-border-radius 50%
                  -webkit-border-radius 50%
                  border-radius 50%

            &.is-open
              display block !important

            .head
              height 50px
              line-height 50px
              background #323436

              span
                padding-left 16px
                font-size 14px

              .close
                width 18px !important
                height 18px !important
                padding 16px 10px 0 0 !important
</style>
