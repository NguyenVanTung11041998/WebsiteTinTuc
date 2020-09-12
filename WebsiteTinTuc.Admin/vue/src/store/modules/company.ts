import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import Company from '../entities/company';

interface CompanyState extends ListState<Company> {
    company: Company;
    companies: Company[];
}
class CompanyModule extends ListModule<CompanyState, any, Company>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Company>(),
        loading: false,
        company: new Company(),
        companies: new Array<Company>()
    }
    actions = {
        async getAll(context: ActionContext<CompanyState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/Company/GetCompanyPaging', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<Company>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createCompany(context: ActionContext<CompanyState, any>, payload: any) {
            await Ajax.post('/api/services/app/Company/CreateCompany', payload.data);
        },
        async updateCompany(context: ActionContext<CompanyState, any>, payload: any) {
            await Ajax.put('/api/services/app/Company/UpdateCompany', payload.data);
        },
        async delete(context: ActionContext<CompanyState, any>, payload: any) {
            await Ajax.delete('/api/services/app/Company/DeleteCompany?Id=' + payload.data);
        },
        async get(context: ActionContext<CompanyState, any>, payload: any): Promise<Company> {
            let reponse = await Ajax.get('/api/services/app/Company/GetCompanyById?Id=' + payload.id);
            return reponse.data.result as Company;
        },
        async getAllCompanies(context: ActionContext<CompanyState, any>) {
            const response = await Ajax.get('/api/services/app/Company/GetAllCompanies');
            context.state.companies = response.data.result as Company[];
        },
        async settingHotOfCompany(context: ActionContext<CompanyState, any>, payload) {
            await Ajax.put(`/api/services/app/Company/SettingHotOfCompany?Id=${payload.id}`);
        }
    };
    mutations = {
        setCurrentPage(state: CompanyState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: CompanyState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setCompany(state: CompanyState, company: Company) {
            state.company = company;
        }
    }
}
const companyModule = new CompanyModule();
export default companyModule;