import CONSTANT_VARIABLE from '@/tin-tuc-app/constants/constant-variable';
import ACCOUNT_SERVICES from '@/tin-tuc-app/services/account';
import AUTHENTICATE_SERVICES from '@/tin-tuc-app/services/authenticate';

import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';
import AccountDto from '../../interfaces/account';
import Authenticate, { AppUserLoginInfomation, AuthenticateRequest } from '../../interfaces/authenticate';
import AccountState from './account-state';



const state: AccountState = {
    userLoginInfo: null,
    loginStatus: false,
    user: null
};

const getters: GetterTree<AccountState, RootState> = {
    userLoginInfo(state: AccountState) {
        return state.userLoginInfo;
    },
    loginStatus(state: AccountState) {
        return state.loginStatus;
    },
    user(state: AccountState) {
        return state.user;
    }
};

const mutations: MutationTree<AccountState> = {
    SET_USER_LOGIN_INFO(state: AccountState, data: Authenticate | null) {
        state.userLoginInfo = data;
    },
    SET_LOGIN_STATUS(state: AccountState, status: boolean) {
        state.loginStatus = status;
    },
    SET_USER(state: AccountState, user: AccountDto) {
        state.user = user;
    }
};

const actions: ActionTree<AccountState, RootState> = {
    async login({ commit }, parmas: AuthenticateRequest) {
        const response = await AUTHENTICATE_SERVICES.login(parmas);
        const data = response.result as Authenticate;
        commit('SET_USER_LOGIN_INFO', data);
        commit('SET_LOGIN_STATUS', true);
        localStorage.setItem(CONSTANT_VARIABLE.APP_TOKEN, data.accessToken);
        localStorage.setItem(CONSTANT_VARIABLE.APP_USERID, `${data.userId}`);
    },
    async getCurrentLoginInformations({ commit }) {
        const response = await AUTHENTICATE_SERVICES.getCurrentLoginInformations();
        const data = response.result as AppUserLoginInfomation;
        if (data.user) {
            commit('SET_LOGIN_STATUS', true);
        }
        else {
            localStorage.setItem(CONSTANT_VARIABLE.APP_TOKEN, "");
            localStorage.setItem(CONSTANT_VARIABLE.APP_USERID, "");
            commit('SET_USER_LOGIN_INFO', null);
            commit('SET_LOGIN_STATUS', false);
        }
    },
    async getUserById({ commit }, id: number) {
        const response = await ACCOUNT_SERVICES.getUserById(id);
        const data = response.result as AccountDto;
        commit('SET_USER', data);
    }
};

export const AccountModule: Module<AccountState, RootState> = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
