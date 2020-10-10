import COMPANY_HOME_SERVICES from '@/tin-tuc-app/services/company-home';

import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';
import CompanyHomeState from './company-home-state';
import CompanyHome, { CompanyHomeInfoRequest, PostCompanyHome, PostHomeInfoDto } from "../../interfaces/company-home";
import PageResultDto from '../../interfaces/page-result';


const state: CompanyHomeState = {
    company: {} as CompanyHome,
    pagePaginate: 0,
    posts: [],
    post: {} as PostHomeInfoDto
};

const getters: GetterTree<CompanyHomeState, RootState> = {
    company(state: CompanyHomeState) {
        return state.company;
    },
    pagePaginate(state: CompanyHomeState) {
        return state.pagePaginate;
    },
    posts(state: CompanyHomeState) {
        return state.posts;
    },
    post(state: CompanyHomeState) {
        return state.post;
    }
};

const mutations: MutationTree<CompanyHomeState> = {
    SET_COMPANY(state: CompanyHomeState, data: CompanyHome) {
        state.company = data;
    },
    SET_PAGINATION_PAGE(state: CompanyHomeState, page: number) {
        state.pagePaginate = page;
    },
    SET_POSTS(state: CompanyHomeState, posts: PostCompanyHome[]) {
        state.posts = posts;
    },
    SET_POST(state: CompanyHomeState, post: PostHomeInfoDto) {
        state.post = post;
    }
};

const actions: ActionTree<CompanyHomeState, RootState> = {
    async getCompanyByUrl({ commit }, params: CompanyHomeInfoRequest) {
        const response = await COMPANY_HOME_SERVICES.getCompanyByUrl(params);
        const data = response.result as CompanyHome;
        const page = data.posts.totalCount % params.pageSize == 0
            ? data.posts.totalCount / params.pageSize
            : Math.floor(data.posts.totalCount / params.pageSize) + 1;

        commit('SET_COMPANY', data);
        commit('SET_PAGINATION_PAGE', page);
        commit('SET_POSTS', data.posts.items);
    },
    async getPostOfCompanyPaging({ commit }, params: CompanyHomeInfoRequest) {
        const response = await COMPANY_HOME_SERVICES.getPostOfCompanyPaging(params);
        const data = response.result as PageResultDto<PostCompanyHome>;
        const page = data.totalCount % params.pageSize == 0
            ? data.totalCount / params.pageSize
            : Math.floor(data.totalCount / params.pageSize) + 1;

        commit('SET_PAGINATION_PAGE', page);
        commit('SET_POSTS', data.items);
    },
    async getPostByUrl({ commit }, params: CompanyHomeInfoRequest) {
        const response = await COMPANY_HOME_SERVICES.getPostByUrl(params);
        const data = response.result as PostHomeInfoDto;
        const page = data.posts.totalCount % params.pageSize == 0
            ? data.posts.totalCount / params.pageSize
            : Math.floor(data.posts.totalCount / params.pageSize) + 1;

        commit('SET_POST', data);
        commit('SET_PAGINATION_PAGE', page);
        commit('SET_POSTS', data.posts.items);
    }
};

export const CompanyHomeModule: Module<CompanyHomeState, RootState> = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
