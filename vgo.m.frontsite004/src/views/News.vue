<template>
  <div class="common_body">
    <div class="nav">
      <ul>
        <li :class="type === item.id ? 'cur' : ''" :key="item.id"
            @click="selectTab(item.id)" v-for="item in typeList">
          {{item.name}}
        </li>
      </ul>
    </div>
    <div class="msg_detail">
      <div id="advice">
        <div class="content">
          <ul>
            <li :key="item.bbs_id" v-for="item in list">
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
          <div @click="getMore" class="common_more" v-if="total > list.length">
            <span>查看更多</span>
          </div>
          <div class="common_more" v-if="total === 0">
            <span>暂无数据</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        type: 0,
        page: 1,
        total: 0,
        typeList: [],
        list: []
      }
    },
    created () {
      this.getTypeList()
    },
    methods: {
      getTypeList () {
        this.$service.api.getNewsTypeList().then(d => {
          this.typeList = d
          if (d.length > 0) {
            this.selectTab(d[0].id)
          }
        })
      },
      getList () {
        this.$service.api.getNewsList(this.page, 10, this.type).then(d => {
          this.list.push(...d.list)
          this.total = d.count
        })
      },
      getMore () {
        if (this.total > this.list.length) {
          this.page++
          this.getList()
        }
      },
      selectTab (type) {
        this.type = type
        this.page = 1
        this.total = 0
        this.list = []
        this.getList()
      }
    }
  }
</script>
<style lang="stylus">
  .app-news
    .common_body
      .nav
        background-color: white
        text-align: center
        border-bottom: 1px solid #eee

        ul
          display flex
          height: 50px
          line-height: 50px

          li
            width: 50%

            &.cur
              color: #2dbb55
              border-bottom: 3px solid #2dbb55

      .msg_detail
        margin-top: 10px
        background-color: #fff

        .content
          background-color: white;
          margin: 0 0 26px 0;

          a
            display: block;
            position: relative;
            padding: 5px 10px 0 130px;
            background: white;
            border-bottom: 1px solid #eee;
            min-height: 75px;

            img
              position: absolute;
              left: 10px;
              top: 12px;
              width: 110px;
              height: 65px;

            .title
              font-size: 14px;
              line-height: 25px;

            .desc
              line-height: 20px;
              margin-left: 2px;
              font-size: 12px;
              color: #999;

            .time
              height: 35px;
              line-height: 35px;
              padding-right: 10px;
              text-align: right;
              color: #999;
</style>
