import HASHTAG_SERVICES from '@/tin-tuc-app/services/hashtag';

import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';

import HashtagState from './hashtag-state';
import Hashtag from '../../interfaces/hashtag';


const state: HashtagState = {
	hashtags: []
};

const getters: GetterTree<HashtagState, RootState> = {
    hashtags(state: HashtagState) {
        return state.hashtags;
    }
};

const mutations: MutationTree<HashtagState> = {
    SET_HASHTAGDATA(state: HashtagState, data: Hashtag[]) {
        state.hashtags = data;
    }
};

const actions: ActionTree<HashtagState, RootState> = {
	async getAllHashtagIsHot({ commit }) {
		const response = await HASHTAG_SERVICES.getAllHashtagIsHot();
		const data = response.result as Hashtag[];
		commit('SET_HASHTAGDATA', data);
	}
};

export const HashtagModule: Module<HashtagState, RootState> = {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
