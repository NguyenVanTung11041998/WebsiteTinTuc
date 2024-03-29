import Vue from 'vue';
import App from './app.vue';
import BootstrapVue from 'bootstrap-vue';
import ViewUI from 'view-design';
import iView from 'iview';
import { router } from './router/index';
import 'famfamfam-flags/dist/sprite/famfamfam-flags.css';
import './theme.less';
import Ajax from './lib/ajax';
import Util from './lib/util';
import SignalRAspNetCoreHelper from './lib/SignalRAspNetCoreHelper';
Vue.use(iView);
Vue.use(require('vue-moment'));
import store from './store/index';
Vue.config.productionTip = false;
import { appRouters, otherRouters } from './router/router';
if (!abp.utils.getCookieValue('Abp.Localization.CultureName')) {
  let language = navigator.language;
  abp.utils.setCookieValue('Abp.Localization.CultureName', language, new Date(new Date().getTime() + 5 * 365 * 86400000), abp.appPath);
}

import 'view-design/dist/styles/iview.css';
Vue.use(BootstrapVue);
import locale from 'view-design/dist/locale/en-US';
import GrantedPermission from './store/constants/granted-permission';
Vue.use(ViewUI, {locale: locale});

Ajax.get('/AbpUserConfiguration/GetAll').then(data => {
  Util.abp = Util.extend(true, Util.abp, data.data.result);
  GrantedPermission.grantedPermissions = Object.getOwnPropertyNames(Util.abp.auth.grantedPermissions);

  new Vue({
    render: h => h(App),
    router: router,
    store: store,
    data: {
      currentPageName: ''
    },
    async mounted() {
      this.currentPageName = this.$route.name as string;
      await this.$store.dispatch({
        type: 'session/init'
      });
      if (!!this.$store.state.session.user && this.$store.state.session.application.features['SignalR']) {
        if (this.$store.state.session.application.features['SignalR.AspNetCore']) {
          SignalRAspNetCoreHelper.initSignalR();
        }
      }
      this.$store.commit('app/initCachepage');
      this.$store.commit('app/updateMenulist');
    },
    created() {
      let tagsList: Array<any> = [];
      appRouters.map((item) => {
        if (item.children.length <= 1) {
          tagsList.push(item.children[0]);
        } else {
          tagsList.push(...item.children);
        }
      });
      this.$store.commit('app/setTagsList', tagsList);
    }
  }).$mount('#app')
})

