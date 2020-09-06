import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import Nationality from '../entities/nationality';

interface NationalityState extends ListState<Nationality> {
    nationality: Nationality;
    nationalities: Nationality[];
}
class NationalityModule extends ListModule<NationalityState, any, Nationality>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Nationality>(),
        loading: false,
        nationality: new Nationality(),
        nationalities: new Array<Nationality>()
    }
    actions = {
        async getAllNationalityPaging(context: ActionContext<NationalityState, any>, payload: any) {
            context.state.loading = true;
            let res = await Ajax.get('api/services/app/Nationality/GetAllNationalityPaging', { params: payload.data });
            context.state.loading = false;
            let page = res.data.result as PageResult<Nationality>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createNationality(context: ActionContext<NationalityState, any>, payload: any) {
            await Ajax.post('api/services/app/Nationality/CreateNationality', payload.data);
        },
        async updateNationality(context: ActionContext<NationalityState, any>, payload: any) {
            await Ajax.put('api/services/app/Nationality/UpdateNationality', payload.data);
        },
        async deleteNationality(context: ActionContext<NationalityState, any>, payload: any) {
            await Ajax.delete('api/services/app/Nationality/DeleteNationality?id=' + payload.data);
        },
        async getAllNationality({ commit }) {
            const res = await Ajax.get('api/services/app/Nationality/GetAll');
            const data = res.data.result as Nationality[];
            commit("setNationalities", data);
        }
    };
    mutations = {
        setCurrentPage(state: NationalityState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: NationalityState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setNationality(state: NationalityState, nationality: Nationality) {
            state.nationality = nationality;
        },
        setNationalities(state: NationalityState, nationalities: Nationality[]) {
            state.nationalities = nationalities;
        }
    }
}
const nationalityModule = new NationalityModule();
export default nationalityModule;