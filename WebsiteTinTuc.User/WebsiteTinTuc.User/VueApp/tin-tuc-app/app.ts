import Vue from 'vue';
import VueRouter from 'vue-router'

Vue.config.performance = true;

Vue.use(VueRouter);

import store from './store/store';
import { router } from './router/router';
import BootstrapVue from 'bootstrap-vue';
import '@fortawesome/fontawesome-free/css/all.css';
import '@fortawesome/fontawesome-free/js/all.js';

import 'bootstrap';
import 'bootstrap/dist/css/bootstrap.min.css';
import Notifications from 'vue-notification';

Vue.use(BootstrapVue);
Vue.use(Notifications);

import App from './App.vue';

new Vue({
  store,
  router,
  render: h => h(App)
}).$mount('#app')
