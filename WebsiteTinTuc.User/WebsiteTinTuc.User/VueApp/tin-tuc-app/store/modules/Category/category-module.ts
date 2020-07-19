import CATEGORY_SERVICES from '@/tin-tuc-app/services/category';

import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';

import { CategoryState } from './category-state';
import { Category } from '../../interfaces/category';


const state: CategoryState = {
	categories: []
};

const getters: GetterTree<CategoryState, RootState> = {
    categories(state: CategoryState) {
        return state.categories;
    }
};

const mutations: MutationTree<CategoryState> = {
    SET_CATEGORYDATA(state: CategoryState, data: Category[]) {
        state.categories = data;
    }
};

const actions: ActionTree<CategoryState, RootState> = {
	async getAllCategory({ commit }): Promise<any> {
        const response = await CATEGORY_SERVICES.getAllCategory();
		commit('SET_CATEGORYDATA', response.result);
        return response;
	}
};

export const CategoryModule: Module<CategoryState, RootState> = {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
