<template>
  <div class="pano-list">
    <div class="title" v-if="title">{{title}}</div>
    <div class="content">
      <ul>
        <li :key="item.pano_id" v-for="item in list">
          <div class="lists">
            <a :href="item.panoview_url" target="_blank">
              <div class="img-box">
                <img v-lazy="item.icon_src"/>
              </div>
              <div class="auth">
                <img class="pic"
                     v-lazy="item.user_avatar"/><span>{{item.user_nickname}}</span>
              </div>
              <p class="omit">{{item.pano_name}}</p>
            </a>
          </div>
        </li>
      </ul>
      <div @click="$emit('more')" class="common_more" v-if="list.length < total">
        <span>查看更多</span>
      </div>
      <div class="common_more" v-if="total === 0">
        <span>暂无数据</span>
      </div>
    </div>
  </div>
</template>
<script>
  export default {
    props: {
      list: {
        type: Array,
        default: null
      },
      title: {
        type: String,
        default: ''
      },
      total: {
        type: Number,
        default: -1
      }
    }
  }
</script>
<style lang="stylus">
  .pano-list {
    background: white;
    margin-top: 10px;

    .title {
      font-size: 18px;
      color: grey;
      height: 50px;
      line-height: 50px;
      background-color: white;
      text-align: center;
      border-bottom: 1px solid #eee;
      margin-bottom: 10px;
    }

    .content {
      height: 100%;

      ul {
        overflow: hidden;

        li {
          float: left;
          width: 47%;
          box-sizing: border-box;
          margin: 0 2% 10px;

          &:nth-child(2n) {
            margin: 0 0 10px 0;
          }

          .lists {
            border: 1px solid #eee;
          }

          a {
            display: block;
            height: 248px;
            width: 100%;

            .img-box {
              overflow: hidden;
              position: relative;
              height: 188px;

              img {
                position: absolute;
                left: 50%;
                height: 100%;
                transform: translateX(-50%);
              }
            }

            .auth {
              width: 70%;
              position: relative;
              padding: 10px 5px 10px 55px;

              span {
                color: grey;
              }

              .pic {
                position: absolute;
                top: -20px;
                left: 5px;
                border: 2px solid white;
                width: 45px;
                height: 45px;
                -webkit-border-radius: 50%;
                -moz-border-radius: 50%;
                border-radius: 50%;
                box-shadow: 0 0 5px #999999;
                background-color: #eaeaea;
              }
            }

            p {
              padding: 0 0 5px 10px;
            }
          }
        }
      }
    }
  }
</style>
