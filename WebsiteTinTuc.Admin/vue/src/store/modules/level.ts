import ListState from "./list-state";
import Level from "../entities/level";
import ListModule from "./list-module";
import { ActionContext } from "vuex";
import Ajax from "../../lib/ajax";
import PageResult from "../entities/page-result";

interface LevelState extends ListState<Level> {
    level: Level;
}
class LevelModule extends ListModule<LevelState, any, Level>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Level>(),
        loading: false,
        level: new Level()
    }
    actions = {
        async getAllLevelPaging(content: ActionContext<LevelState, any>, payload: any) {
            content.state.loading = true;
            let res = await Ajax.get('/api/services/app/Level/GetAllLevelPaging', { params: payload.data });
            content.state.loading = false;
            let page = res.data.result as PageResult<Level>;
            content.state.totalCount = page.totalCount;
            content.state.list = page.items;
        },
        async createLevel(content: ActionContext<LevelState, any>, payload: any) {
            await Ajax.post('/api/services/app/Level/CreateLevel', payload.data);
        },
        async updateLevel(content: ActionContext<LevelState, any>, payload: any) {
            await Ajax.put('/api/services/app/Level/UpdateLevel', payload.data);
        },
        async deleteLevel(content: ActionContext<LevelState, any>, payload: any) {
            await Ajax.delete(`/api/services/app/Level/DeleteLevel?id=${payload.data}`);
        }
    }
    mutations = {
        setCurrentPage(state: LevelState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: LevelState, pageSize: number) {
            state.pageSize = pageSize;
        },
        setLevel(state: LevelState, level: Level) {
            state.level = level;
        }
    }
}
const levelModule = new LevelModule();
export default levelModule;