import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';
import RecruitmentState from './recruitment-state';
import RECRUIMENT_SERVICES from '../../../services/recruitment';

const state: RecruitmentState = {
};

const getters: GetterTree<RecruitmentState, RootState> = {
};

const mutations: MutationTree<RecruitmentState> = {
};

const actions: ActionTree<RecruitmentState, RootState> = {
    async createCV({ }, params: FormData) {
        await RECRUIMENT_SERVICES.createCV(params);
    }
};

export const RecruitmentModule: Module<RecruitmentState, RootState> = {
    namespaced: true,
    state,
    getters,
    actions,
    mutations
};
