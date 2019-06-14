import Vue from 'vue'
import { Button, Lazyload, Swipe, SwipeItem, Field } from 'mint-ui'

Vue.use(Lazyload)
Vue.component(Field.name, Field)
Vue.component(Swipe.name, Swipe)
Vue.component(Button.name, Button)
Vue.component(SwipeItem.name, SwipeItem)
