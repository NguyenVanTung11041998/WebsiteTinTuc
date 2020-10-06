import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';

import HomeState from './home-state';
import CompanyPostModel, { HomeFilter } from '../../interfaces/home';
import HOME_SERVICE from '@/tin-tuc-app/services/home';
import PageRequest from '../../interfaces/page-request';


const state: HomeState = {
    companyPostProminents: [],
    companyPosts: [],
    pagePaginate: 1,
    pagePaginateButtom: 1,
    posts: []
};

const getters: GetterTree<HomeState, RootState> = {
    companyPostProminents(state: HomeState) {
        return state.companyPostProminents;
    },
    companyPosts(state: HomeState) {
        return state.companyPosts;
    },
    pagePaginate(state: HomeState) {
        return state.pagePaginate;
    },
    pagePaginateButtom(state: HomeState) {
        return state.pagePaginateButtom;
    },
    posts(state: HomeState) {
        return state.posts;
    }
};

const mutations: MutationTree<HomeState> = {
    SET_COMPANY_POST_PROMINENT(state: HomeState, data: CompanyPostModel[]) {
        state.companyPostProminents = data;
    },
    SET_COMPANY_POST(state: HomeState, data: CompanyPostModel[]) {
        state.companyPosts = data;
    },
    SET_PAGE_NUMBER_PROMINENT(state: HomeState, page: number) {
        state.pagePaginate = page;
    },
    SET_PAGE_NUMBER(state: HomeState, page: number) {
        state.pagePaginateButtom = page;
    },
    SET_POSTS(state: HomeState, posts: CompanyPostModel[]) {
        state.posts = posts;
    }
};

const actions: ActionTree<HomeState, RootState> = {
    async getAllCompanyPostPaging({ commit }, filter: HomeFilter) {
        const response = await HOME_SERVICE.getAllCompanyPostPaging(filter);
        const data = response.result.items as CompanyPostModel[];
        const totalCount = response.result.totalCount as number;
        const page = totalCount % filter.pageSize == 0
                            ? totalCount / filter.pageSize
                            : Math.floor(totalCount / filter.pageSize) + 1;
        if (filter.isHot) {
            commit('SET_COMPANY_POST_PROMINENT', data);
            commit('SET_PAGE_NUMBER_PROMINENT', page);
        }
        else {
            commit('SET_COMPANY_POST', data);
            commit('SET_PAGE_NUMBER', page);
        }
    },
    async getTopNewPost({ commit }, count: number) {
        const response = await HOME_SERVICE.getTopNewPost(count);
        const data = response.result as CompanyPostModel[];
        commit('SET_POSTS', data);
    },
    async getPostPaging({ commit }, filter: PageRequest) {
        const response = await HOME_SERVICE.getPostPaging(filter);
        const data = await response.result.items as CompanyPostModel[];
        const totalCount = response.result.totalCount as number;
        const page = totalCount % filter.pageSize == 0
                            ? totalCount / filter.pageSize
                            : Math.floor(totalCount / filter.pageSize) + 1;

        commit('SET_POSTS', data);
        commit('SET_PAGE_NUMBER', page);
    }
};

export const HomeModule: Module<HomeState, RootState> = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
