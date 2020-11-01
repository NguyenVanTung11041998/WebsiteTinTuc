import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import ConfigModel from '../entities/config';

interface ConfigState extends ListState<ConfigModel> {
    config: ConfigModel;
}
class ConfigModule extends ListModule<ConfigState, any, ConfigModel>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<ConfigModel>(),
        loading: false,
        config: new ConfigModel
    };
    getters = {
        config(state: ConfigState) {
            return state.config;
        }
    };
    actions = {
        async getConnectionString({ commit }) {
            const response = await Ajax.get("/api/services/app/Configuration/GetConnectionString");
            const data = response.data.result as ConfigModel;
            commit("setConfig", data);
            return data;
        },
        async changeConnectionString(context: ActionContext<ConfigState, any>, payload: any) {
            await Ajax.post("/api/services/app/Configuration/ChangeConnectionString", payload.data);
        },
        async resetConnectionStringDefault({ commit }) {
            const response = await Ajax.post("/api/services/app/Configuration/ResetConnectionStringDefault");
            const data = response.data.result as ConfigModel;
            commit("setConfig", data);
            return data;
        }
    };
    mutations = {
        setCurrentPage(state: ConfigState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: ConfigState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setConfig(state: ConfigState, config: ConfigModel) {
            state.config = config;
        }
    }
}
const configModule = new ConfigModule();
export default configModule;