(window["webpackJsonp"]=window["webpackJsonp"]||[]).push([["chunk-178617da"],{"383a":function(t,e,n){},7542:function(t,e,n){"use strict";var s=n("383a"),i=n.n(s);i.a},"9c36":function(t,e,n){"use strict";n.r(e);var s=function(){var t=this,e=t.$createElement,n=t._self._c||e;return n("div",{staticClass:"common_body news-detail"},[n("div",{staticClass:"title"},[t._v(t._s(t.news.title))]),n("div",{staticClass:"author"},[n("div",[t._v("作者："+t._s(t.news.author))]),n("div",{staticStyle:{"margin-left":"2em"}},[t._v(t._s(t._f("dateFilter")(t.news.created)))])]),n("div",{staticClass:"content",domProps:{innerHTML:t._s(t.news.content)}})])},i=[],a={data:function(){return{news:{}}},created:function(){this.getDetail(this.$route)},methods:{getDetail:function(t){var e=this,n=t.params.id;n&&this.$service.api.getNewsDetail(n).then(function(t){e.news=t})}}},c=a,o=(n("7542"),n("2877")),r=Object(o["a"])(c,s,i,!1,null,null,null);e["default"]=r.exports}}]);