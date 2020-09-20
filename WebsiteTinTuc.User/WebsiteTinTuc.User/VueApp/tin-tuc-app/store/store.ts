import Vue from 'vue';
import Vuex, { StoreOptions } from 'vuex';
import { RootState } from './state';
import { HashtagModule } from './modules/Hashtag/hashtag-module';
import { CompanyModule } from "./modules/Company/company-module";
import { PostModule } from "./modules/Post/post-module";
import { HomeModule } from "./modules/Home/home-module";

Vue.use(Vuex);

const store: StoreOptions<RootState> = {
    state: {
        version: '1.0.0'
    },
    modules: {
        HashtagModule,
        CompanyModule,
        PostModule,
        HomeModule
    }
};

export default new Vuex.Store<RootState>(store);
