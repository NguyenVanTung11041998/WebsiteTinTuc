import RouteName from '@/tin-tuc-app/constants/route-name';
import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';
import AppState from './app.-state';



const state: AppState = {
    path: "/"
};

const getters: GetterTree<AppState, RootState> = {
    path(state: AppState) {
        return state.path;
    }
};

const mutations: MutationTree<AppState> = {
    SET_PATH(state: AppState, path: string) {
        state.path = path;
    }
};

const actions: ActionTree<AppState, RootState> = {
};

export const AppModule: Module<AppState, RootState> = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
