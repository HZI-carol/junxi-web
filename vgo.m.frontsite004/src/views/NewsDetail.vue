<template>
  <div class="common_body news-detail">
    <div class="title">{{news.title}}</div>
    <div class="author">
      <div>作者：{{news.author}}</div>
      <div style="margin-left: 2em">{{news.created | dateFilter}}</div>
    </div>
    <div class="content" v-html="news.content"></div>
  </div>
</template>

<script>
  export default {
    data () {
      return {
        news: {}
      }
    },
    created () {
      this.getDetail(this.$route)
    },
    methods: {
      getDetail ($route) {
        const id = $route.params.id
        if (id) {
          this.$service.api.getNewsDetail(id).then(d => {
            this.news = d
          })
        }
      }
    }
  }
</script>

<style lang="stylus">

  .news-detail {
    padding: 10px;

    .title {
      text-align: center;
      font-size: 20px;
      font-weight: bold;
    }

    .author {
      padding: 10px;
      display: flex;
    }

    .content {
      padding-top: 1em;

      img {
        max-width: calc(100vw - 20px);
      }
    }
  }

</style>
