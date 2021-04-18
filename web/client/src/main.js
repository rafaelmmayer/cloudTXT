import Vue from 'vue'
import App from './App.vue'
import Resource from 'vue-resource'

Vue.config.productionTip = false
Vue.use(Resource)

new Vue({
  render: h => h(App),
}).$mount('#app')
