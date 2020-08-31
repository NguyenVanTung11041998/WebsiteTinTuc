import ListState from "./list-state";
import BranchJob from "../entities/branch-job";
import ListModule from "./list-module";
import { ActionContext } from "vuex";
import Ajax from '../../lib/ajax';
import PageResult from "../entities/page-result";
interface BranchJobState extends ListState<BranchJob> {
    branchJob: BranchJob
}
class BranchJobModule extends ListModule<BranchJobState, any, BranchJob>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<BranchJob>(),
        loading: false,
        branchJob: new BranchJob()
    }
    actions = {
        async getAllBranchJobs(context: ActionContext<BranchJobState, any>, payload: any) {
            context.state.loading = true;
            let res = await Ajax.get('/api/services/app/BranchJob/GetAllBranchJobPaging', { params: payload.data });
            context.state.loading = false;
            let page = res.data.result as PageResult<BranchJob>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createBranchJob(context: ActionContext<BranchJob, any>, payload: any) {
            await Ajax.post("/api/services/app/BranchJob/CreateBranchJob", payload.data);
        },
        async updateBranchJob(context: ActionContext<BranchJob, any>, payload: any) {
            await Ajax.put("/api/services/app/BranchJob/UpdateBranchJob", payload.data);
        },
        async deleteBranchJob(context: ActionContext<BranchJob, any>, payload: any) {
            await Ajax.delete(`/api/services/app/BranchJob/Delete?Id= ${payload.data}`);
        }
    };
    mutations = {
        setCurrentPage(state: BranchJobState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: BranchJobState, pageSize: number) {
            state.pageSize = pageSize;
        },
        setBranchJob(state: BranchJobState, branchJob: BranchJob) {
            state.branchJob = branchJob;
        }
    }
}
const branchJobModule = new BranchJobModule();
export default branchJobModule;