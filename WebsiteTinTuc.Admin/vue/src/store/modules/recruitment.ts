import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import CV from '../entities/cv';

interface RecruitmentState extends ListState<CV> {
    cv: CV;
}
class RecruitmentModule extends ListModule<RecruitmentState, any, CV>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<CV>(),
        loading: false,
        cv: new CV
    }
    actions = {
        async getAllPaging(context: ActionContext<RecruitmentState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/Recruitment/GetAllRecruitmentPaging', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<CV>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async readCV(context: ActionContext<RecruitmentState, any>, payload: any) {
            await Ajax.post(`/api/services/app/Recruitment/ReadCV?id=${payload.id}`)
        }
    };
    mutations = {
        setCurrentPage(state: RecruitmentState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: RecruitmentState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setCV(state: RecruitmentState, cv: CV) {
            state.cv = cv;
        }
    }
}
const recruitmentModule = new RecruitmentModule();
export default recruitmentModule;