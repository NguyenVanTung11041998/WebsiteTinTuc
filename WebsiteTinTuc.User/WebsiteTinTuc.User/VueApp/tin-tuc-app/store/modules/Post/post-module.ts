import COMPANY_SERVICES from '@/tin-tuc-app/services/company';

import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';

import PostState from './post-state';
import Post, { PostTopProminent } from '../../interfaces/post';
import POST_SERVICES from '@/tin-tuc-app/services/post';


const state: PostState = {
    prominentPosts: []
};

const getters: GetterTree<PostState, RootState> = {
    prominentPosts(state: PostState) {
        return state.prominentPosts;
    }
};

const mutations: MutationTree<PostState> = {
    SET_POST_PROMINENT(state: PostState, data: PostTopProminent[]) {
        state.prominentPosts = data;
    }
};

const actions: ActionTree<PostState, RootState> = {
    async getTopCompanyProminent({ commit }, count?: number) {
        const response = await POST_SERVICES.getTopPostProminent(count);
        const data = response.result as PostTopProminent[];
        commit('SET_POST_PROMINENT', data);
    }
};

export const PostModule: Module<PostState, RootState> = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
