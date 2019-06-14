<template>
  <div>
    <div class="copyright">
      {{$globalconfig.copyright}}
    </div>
    <div class="footer">
      <ul>
        <li>
          <router-link to="/pano">
            <i class="iconfont icon-VRquanjingtu"></i>
            <div>VR全景</div>
          </router-link>
        </li>
        <li>
          <router-link to="/author">
            <i class="iconfont icon-zuozhe"></i>
            <div>{{$globalconfig.author_alias}}</div>
          </router-link>
        </li>
        <li>
          <router-link to="/">
            <i class="iconfont icon-shouye"></i>
            <div>主页</div>
          </router-link>
        </li>
        <li>
          <router-link to="/news">
            <i class="iconfont icon-xinwen"></i>
            <div>新闻</div>
          </router-link>
        </li>
        <li>
          <router-link to="/support">
            <i class="iconfont icon-kefu1"></i>
            <div>关于我们</div>
          </router-link>
        </li>
      </ul>
    </div>
  </div>
</template>

<script>
  export default {}
</script>
<style lang="stylus">
  @import '../../scss/variable.styl';
  .footer {
    border-top: 1px solid #dcdcdc;
    position: fixed;
    bottom: 0;
    left: 0;
    background: white;
    z-index: 999;
    text-align: center;
    font-size: 12px;
    padding-bottom: 0px;
    padding-top: 2px;
    width: 100%;

    ul {
      li {
        width: 20%;
        float: left;

        .router-link-exact-active {
          color: #9c0;
        }

        a {
          display: block;

          i {
            font-size: 26px;
          }

          div {
            line-height: 24px;
          }
        }
      }
    }
  }
</style>
