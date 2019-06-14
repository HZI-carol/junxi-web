<template>
  <div class="common_body author-detail">
    <div class="pootographer-info">
      <div class="avatar">
        <img :src="author.avatar">
      </div>
      <div class="detail">
        <div class="top">
          <div class="nickname omit">{{author.nickname}}</div>
          <mt-button v-if="$globalconfig.show_author_panotask" type="primary" size="small" @click='contant'>
            在线预约
          </mt-button>
        </div>
        <div class="bottom">
          <div class="products omit">作品:<span class="num omit">{{author.panonum}}</span></div>
          <div class="popularity omit">人气:<span class="num omit">{{author.seecount}}</span></div>
          <div class="praise omit">赞:<span class="num omit">{{author.praisecount}}</span></div>
        </div>
      </div>
    </div>
    <!-- 全景作品 -->
    <pano-list :list='panoList' :total="totalPano" @more="getMore" style="margin-top: 0" title="全景作品"/>
  </div>
</template>

<script>
  export default {
    components: {
      PanoList: () => import('@/components/PanoList')
    },
    data () {
      return {
        author: {},
        page: 1,
        panoList: [],
        totalPano: 0
      }
    },
    created () {
      this.getDetail(this.$route)
    },
    methods: {
      getDetail ($route) {
        const id = $route.params.id
        if (id) {
          this.$service.api.getAuthor(id).then(d => {
            this.author = d
            this.getList()
          })
        }
      },
      getList () {
        this.$service.api.getPanoList(this.page, 10, '', '', this.author.user_id).then(pd => {
          this.panoList.push(...pd.list)
          this.totalPano = pd.count
        })
      },
      getMore () {
        this.page++
        this.getList()
      },
      contant () {
        this.$router.push({ path: '/authorpanotask', query: { user_id: this.author.user_id } })
      }
    }
  }
</script>

<style scoped>
  .author-detail {
    overflow-x: hidden;
  }

  .pootographer-info {
    display: flex;
    align-items: center;
    justify-content: space-between;
    height: 100px;
    padding: 0 10px;
    width: 100%;
    position: relative;
    background: #fff;
    box-sizing: border-box;
  }

  .pootographer-info:after {
    position: absolute;
    bottom: 0;
    left: 50%;
    content: '';
    width: 100vw;
    min-width: 1920px;
    height: 0;
    border-bottom: 1px solid #e8e8e8;
    transform: translateX(-50%);
  }

  .pootographer-info .avatar {
    flex: 0 0 75px;
    height: 75px;
    border-radius: 50%;
    overflow: hidden;
    border: 1px solid #e7ff9e;
  }

  .pootographer-info .avatar img {
    width: 100%;
    height: 100%;
  }

  .pootographer-info .detail {
    box-sizing: border-box;
    padding-left: 20px;
    flex: 0 0 calc(100% - 75px);
    display: flex;
    flex-direction: column;
    overflow: hidden;
  }

  .pootographer-info .detail .top {
    display: flex;
    justify-content: space-between;
  }

  .pootographer-info .detail .top .mint-button {
    height: 34px;
  }

  .pootographer-info .detail .top .nickname {
    flex: 0 0 calc(100% - 130px);
    font-size: 24px;
    color: #333;
    margin-right: 10px;
  }

  .pootographer-info .detail .bottom {
    margin: 10px 0;
    display: -webkit-box;
    display: -ms-flexbox;
    display: flex;
  }

  .pootographer-info .detail .bottom .products,
  .pootographer-info .detail .bottom .popularity,
  .pootographer-info .detail .bottom .praise {
    font-size: 14px;
    color: #666;
    margin-right: 10px;
  }

  .pootographer-info .detail .bottom .products .num,
  .pootographer-info .detail .bottom .popularity .num,
  .pootographer-info .detail .bottom .praise .num {
    margin-left: 9px;
    color: #9c0;
  }
</style>
