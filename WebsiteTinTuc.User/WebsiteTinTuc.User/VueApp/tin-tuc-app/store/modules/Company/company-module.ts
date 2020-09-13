import COMPANY_SERVICES from '@/tin-tuc-app/services/company';

import { RootState } from '@/tin-tuc-app/store/state';
import { ActionTree, GetterTree, Module, MutationTree } from 'vuex';

import CompanyState from './company-state';
import Company, { ProminentCompanyModel } from '../../interfaces/company';


const state: CompanyState = {
	prominentCompanies: []
};

const getters: GetterTree<CompanyState, RootState> = {
    prominentCompanies(state: CompanyState) {
        return state.prominentCompanies;
    }
};

const mutations: MutationTree<CompanyState> = {
    SET_COMPANY_PROMINENT(state: CompanyState, data: ProminentCompanyModel[]) {
        state.prominentCompanies = data;
    }
};

const actions: ActionTree<CompanyState, RootState> = {
	async getTopCompanyProminent({ commit }, count?: number) {
		const response = await COMPANY_SERVICES.getTopCompanyProminent(count);
		const data = response.result as ProminentCompanyModel[];
		commit('SET_COMPANY_PROMINENT', data);
	}
};

export const CompanyModule: Module<CompanyState, RootState> = {
	namespaced: true,
	state,
	getters,
	actions,
	mutations
};
