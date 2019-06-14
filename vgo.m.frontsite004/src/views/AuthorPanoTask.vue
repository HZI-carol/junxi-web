<template>
  <div class="common_body">
    <section class="appointment">
      <div class="title ">
        <span class="line">在线预约</span>
      </div>
      <div class="form">
        <mt-field label="姓名" placeholder="请输入姓名" v-model="model.personname" maxlength="20"></mt-field>
        <mt-field label="联系电话" placeholder="请输入联系电话" type="phone" v-model="model.contact" maxlength="30"></mt-field>
        <mt-field label="约拍内容" placeholder="请输入约拍内容" type="textarea" rows="8" v-model="model.content"
                  maxlength="500"></mt-field>
      </div>
      <div class="buttom">
        <mt-button type="primary" size="large" @click='submit'>提 交</mt-button>
      </div>
    </section>
  </div>
</template>

<script>
  import { Toast } from 'mint-ui'

  export default {
    data () {
      return {
        model: {
          fk_user_id: '',
          personname: '',
          contact: '',
          content: ''
        },
        author: {}
      }
    },
    created () {
      console.info(location.href)
      this.model.fk_user_id = this.$route.query.user_id
      if (isNaN(this.model.fk_user_id)) {
        this.$router.push('/author')
      }
      const link = location.href.split('?')[0] + '#/authorpanotask?user_id=' + this.model.fk_user_id
      const icon = this.$globalconfig.apiurl + '/krpano/images/logo_code.png'
      this.$service.api.setJssdkConfig({
        imgUrl: icon,
        link: link,
        desc: '预约拍摄',
        title: '在线约拍'
      })
    },
    methods: {
      submit () {
        if (!this.model.personname) {
          Toast('请填写姓名')
          return
        }
        if (!this.model.contact) {
          Toast('请填写联系电话')
          return
        }
        if (!this.model.content) {
          Toast('请填写约拍内容')
          return
        }
        this.$service.api.addPanoTask(this.model).then(res => {
          Toast({
            message: '操作成功',
            iconClass: 'icon icon-success',
            duration: 2000
          })
          this.model.personname = ''
          this.model.contact = ''
          this.model.content = ''
        })
      }
    }
  }
</script>

<style scoped>

  .common_body .appointment {
    background-color: #fff;
  }

  .common_body .appointment .title {
    font-size: 20px;
    line-height: 60px;
    color: #999;
    text-align: center;
    border-bottom: 1px solid #f5f5f5;
  }

  .common_body .appointment .author {
    border-bottom: 1px solid #f5f5f5;
    line-height: 50px;
    display: flex;
  }

  .common_body .appointment .author .label {
    font-size: 16px;
    color: #9c0;
    width: 105px;
    padding-left: 10px;
  }

  .common_body .appointment .author .nickname {
    font-size: 16px;
    color: #999;
  }

  .common_body .appointment .mint-field {
    border-bottom: 1px solid #f5f5f5;
  }

  .common_body .appointment .buttom {
    display: flex;
    justify-content: space-around;
    align-items: center;
    height: 60px;
  }

  .common_body .appointment .buttom .mint-button {
    width: 100px;
    height: 36px;
  }

  .line {
    position: relative;
  }

  .line:after {
    content: '';
    position: absolute;
    width: 60px;
    height: 2px;
    background-color: #9c0;
    left: -70px;
    top: 50%;
  }

  .line:before {
    content: '';
    position: absolute;
    width: 60px;
    height: 2px;
    background-color: #9c0;
    right: -70px;
    top: 50%;
  }
</style>
