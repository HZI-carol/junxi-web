window.onload = function () {
    var iHeight = []
    var boxWrap = document.querySelectorAll('.wrapper')
    var playBtn = document.querySelector('#playBtn')
    var extArr = {
        marketing: [
            { text: '赌大小', icon: 'ddx' },
            { text: '圣诞树摇礼物', icon: 'sdylw' },
            { text: '微信钓鱼', icon: 'diaoyu' },
            { text: '捞金鱼', icon: 'ljy' },
            { text: '微贺卡', icon: 'whk' },
            { text: '点亮圣诞树', icon: 'light' },
            { text: '幸运卡牌', icon: 'xykp' },
            { text: '七夕鹊桥会', icon: 'qqh' },
            { text: '抢月饼', icon: 'qyb' },
            { text: '微信种树', icon: 'zhongshu' },
            { text: '决战三公', icon: 'juezhan' },
            { text: '海岛夺宝', icon: 'duobao' },
            { text: '扭扭蛋', icon: 'nnd' },
            { text: '求爱大作战', icon: 'qadzz' },
            { text: '摇一摇抽奖', icon: 'yiycj' },
            { text: '微助力', icon: 'wzl' },
            { text: '点球大战', icon: 'dianqiudasai' },
            { text: '颠球高手', icon: 'dqdz' },
            { text: '圣诞扫货', icon: 'sdsh' },
            { text: '元宵大作战', icon: 'yuanxiaodazuozhan' },
            { text: '点灯笼', icon: 'diandenglong' },
            { text: '集字', icon: 'jizi' },
            { text: '营销电话', icon: 'yingxiaodianhua' }
        ],
        industry: [
            { text: "V名片", icon: 'wmp' },
            { text: "微留言", icon: 'jxsgl' },
            { text: "问卷调研", icon: 'wdy' },
            { text: "微房产", icon: 'wfc' },
            { text: "微酒店", icon: 'wjd' },
            { text: "我们结婚了", icon: 'wxt' },
            { text: "微餐饮", icon: 'wcy' },
            { text: "我为歌狂", icon: 'KTV' },
            { text: "微汽车", icon: 'wqc' },
            { text: "微招聘", icon: 'wzhaopin' },
            { text: "微官网", icon: 'wgw' },
            { text: "拼多多", icon: 'wtg' },
            { text: "英雄帖", icon: 'wyq' },
            { text: "微投票", icon: 'wtp' },
            { text: "微相册", icon: 'md' },
            { text: "微论坛", icon: 'wb' }
        ]
    }

    var createMarketing = (function () {
        var mkDom = document.querySelector('#marketing')
        mkDom.innerHTML = renderPlugin(extArr.marketing)
    })()

    var createIndustry = (function () {
        var mkDom = document.querySelector('#industry')
        mkDom.innerHTML = renderPlugin(extArr.industry)
    })()

    // 营销插件
    function renderPlugin(extList) {
        var list = []
        for (var i = 0; i < extList.length; i++) {
            let item = extList[i]
            list.push(
                '<li class="item">' +
                '<div class="icon"><img src="/content/images/ext/' + item.icon + '.png" alt=""></div>' +
                '<p>' + item.text + '</p>' +
                '</li>'
            )
        }
        return list.join('')
    }

    // 插件选项卡切换
    var tab = (function () {
        var tabWrap = document.querySelector('#tabWrap')
        var tabHdItem = tabWrap.querySelectorAll('.tab-item')
        var tabContItem = tabWrap.querySelectorAll('.tab-cont')

        for (var i = 0; i < tabHdItem.length; i++) {
            tabHdItem[i].index = i
            // 初始化选中状态
            tabHdItem[i].className = i == 0 ? 'tab-item active' : 'tab-item'
            tabContItem[i].className = i == 0 ? 'tab-cont show animated slideInUp' : 'tab-cont'

            // 点击改变选择状态
            tabHdItem[i].onclick = function () {
                for (var i = 0; i < tabHdItem.length; i++) {
                    tabHdItem[i].className = i == this.index ? 'tab-item active' : 'tab-item'
                    tabContItem[i].className = i == this.index ? 'tab-cont show animated slideInUp' : 'tab-cont'
                }
            }
        }
    })()

    // 计算高度
    var calcHeight = (function () {
        let height = 0
        // iHeight.push(height)

        for (let i = 0; i < boxWrap.length; i++) {
            let ih = boxWrap[i].offsetHeight
            height += ih
            iHeight.push(height)
        }
    })()

    // 页面滚动加载动画事件
    function handleScroll() {
        var scrollTop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop
        var clientHeight = document.documentElement.clientHeight

        for (let i = 0, len = iHeight.length; i < len; i++) {
            let h = iHeight[i]
            if (scrollTop + (clientHeight / 1.5) > h) {
                boxWrap[i].className = 'wrapper active'
            } else {
                boxWrap[i].className = 'wrapper'
            }
        }
    }

    window.addEventListener('scroll', handleScroll, false)
}