<template>
  <div class="common_body">
    <section class="auth">
      <div>
        <ul>
          <li :key="item.user_id" @click="$router.push(`/author/${item.user_id}`)"
              v-for="item in list">
            <div class="pic">
              <img v-lazy="item.avatar"/>
            </div>
            <div class="auth_cont">
              <div class="auth_cont_o">{{item.nickname}}</div>
              <div class="auth_cont_t">作品：{{item.panonum}}</div>
              <div class="auth_cont_t">
                <div class="l"><img src="../../public/static/images/people.png"/>{{item.seecount}}</div>
                <div class="r"><img src="../../public/static/images/heart.png"/>{{item.praisecount}}</div>
              </div>
            </div>
          </li>
        </ul>
      </div>
    </section>
    <div @click="getMore" class="common_more" v-if="hasMore">
      <span>查看更多</span>
    </div>
    <div class="common_more" v-if="list.length === 0">
      <span>暂无数据</span>
    </div>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        page: 1,
        list: [],
        hasMore: true
      }
    },
    created () {
      this.getList()
    },
    methods: {
      getList () {
        this.$service.api.getAuthorList(this.page, 6, '').then(d => {
          this.list.push(...d.list)
          this.hasMore = d.count > this.list.length
        })
      },
      getMore () {
        if (this.hasMore) {
          this.page++
          this.getList()
        }
      }
    }
  }
</script>

<style scoped>

  .common_body .auth {
    padding: 10px;
  }

  .common_body .auth li {
    padding: 15px;
    position: relative;
    margin-bottom: 10px;
    background-color: #fff;
    border-radius: 10px;
    height: 60px;
  }

  .common_body .auth .pic {
    float: left;
    width: 66px;
    margin-top: 2px;
  }

  .common_body .auth .pic img {
    width: 55px;
    height: 55px;
    border-radius: 50%;
  }

  .common_body .auth .auth_cont {
    font-size: 12px;
    float: left;
    border-left: 1px solid #eee;
    padding-left: 10px;
    height: 60px;
    width: 75%;
  }

  .common_body .auth .auth_cont .auth_cont_o {
    font-size: 14px;
    margin-bottom: 4px;
  }

  .common_body .auth .auth_cont .auth_cont_t {
    margin-top: 4px;
    color: grey;
    width: 100%;
    float: left;
  }

  .common_body .auth .auth_cont .auth_cont_t img {
    width: 12px;
    height: 12px;
    margin-right: 3px;
  }
</style>
