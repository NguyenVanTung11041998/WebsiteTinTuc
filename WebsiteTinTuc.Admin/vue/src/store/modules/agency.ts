import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import Agency from '../entities/agency';

interface AgencyState extends ListState<Agency> {
    agency: Agency
}
class AgencyModule extends ListModule<AgencyState, any, Agency>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Agency>(),
        loading: false,
        agency: new Agency()
    }
    actions = {
        async getAll(context: ActionContext<AgencyState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/Agency/GetAgencyPaging', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<Agency>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createAgency(context: ActionContext<AgencyState, any>, payload: any) {
            await Ajax.post('/api/services/app/Agency/CreateAgency', payload.data);
        },
        async updateAgency(context: ActionContext<AgencyState, any>, payload: any) {
            await Ajax.put('/api/services/app/Agency/UpdateAgency', payload.data);
        },
        async delete(context: ActionContext<AgencyState, any>, payload: any) {
            await Ajax.delete('/api/services/app/Agency/DeleteAgency?Id=' + payload.data);
        },
        async get(context: ActionContext<AgencyState, any>, payload: any) {
            let reponse = await Ajax.get('/api/services/app/Agency/GetAgencyById?Id=' + payload.id);
            return reponse.data.result as Agency;
        }
    };
    mutations = {
        setCurrentPage(state: AgencyState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: AgencyState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setAgency(state: AgencyState, Agency: Agency) {
            state.agency = Agency;
        }
    }
}
const agencyModule = new AgencyModule();
export default agencyModule;