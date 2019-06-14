import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

let $import = null
if (process.env.NODE_ENV === 'production') {
  $import = file => () => import('@/views/' + file + '.vue')
} else {
  $import = file => require('@/views/' + file + '.vue').default
}

export default new Router({
  routes: [
    {
      path: '/',
      name: 'Index',
      component: $import('Index')
    },
    {
      path: '/pano',
      name: 'Pano',
      component: $import('Pano')
    },
    {
      path: '/author',
      name: 'Author',
      component: $import('Author')
    },
    {
      path: '/author/:id',
      name: 'AuthorDetail',
      component: $import('AuthorDetail')
    },
    {
      path: '/authorpanotask',
      name: 'AuthorPanoTask',
      component: $import('AuthorPanoTask')
    },
    {
      path: '/news',
      name: 'News',
      component: $import('News')
    },
    {
      path: '/news/:id',
      name: 'NewsDetail',
      component: $import('NewsDetail')
    },
    {
      path: '/support',
      name: 'Support',
      component: $import('Support')
    }
  ]
})
