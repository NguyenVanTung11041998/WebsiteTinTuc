import Vue from 'vue';
import Vuex from 'vuex';
Vue.use(Vuex);
import app from './modules/app';
import session from './modules/session';
import account from './modules/account';
import user from './modules/user';
import role from './modules/role';
import tenant from './modules/tenant';
import category from './modules/category';
import hashtag from './modules/hashtag';
import post from './modules/post';
const store = new Vuex.Store({
    state: {
        //
    },
    mutations: {
        //
    },
    actions: {

    },
    getters: {
        
    },
    modules: {
        app,
        session,
        account,
        user,
        role,
        tenant,
        category,
        hashtag,
        post
    }
});

export default store;