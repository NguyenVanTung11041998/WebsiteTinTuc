import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import Hashtag from '../entities/hashtag';

interface HashtagState extends ListState<Hashtag> {
    hashtag: Hashtag;
    hashtags: Hashtag[];
}
class HashtagModule extends ListModule<HashtagState, any, Hashtag>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Hashtag>(),
        loading: false,
        hashtag: new Hashtag(),
        hashtags: new Array<Hashtag>()
    }
    actions = {
        async getAllPaging(context: ActionContext<HashtagState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/Hashtag/GetAllHashtagPaging', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<Hashtag>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createHashtag(context: ActionContext<HashtagState, any>, payload: any) {
            await Ajax.post('/api/services/app/Hashtag/CreateHashtag', payload.data);
        },
        async updateHashtag(context: ActionContext<HashtagState, any>, payload: any) {
            await Ajax.put('/api/services/app/Hashtag/UpdateHashtag', payload.data);
        },
        async delete(context: ActionContext<HashtagState, any>, payload: any) {
            await Ajax.delete('/api/services/app/Hashtag/Delete?Id=' + payload.data);
        },
        async get(context: ActionContext<HashtagState, any>, payload: any) {
            let reponse = await Ajax.get('/api/services/app/Hashtag/GetHashtagById?Id=' + payload.id);
            return reponse.data.result as Hashtag;
        },
        async getAllHashtags({ commit }) {
            let response = await Ajax.get('/api/services/app/Hashtag/GetAllHashtags');
            const data = response.data.result as Hashtag[];
            commit("setAllHashtag", data);
        }
    };
    mutations = {
        setCurrentPage(state: HashtagState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: HashtagState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setHashtag(state: HashtagState, hashtag: Hashtag) {
            state.hashtag = hashtag;
        },
        setAllHashtag(state: HashtagState, hashtags: Hashtag[]) {
            state.hashtags = hashtags;
        }
    }
}
const hashtagModule = new HashtagModule();
export default hashtagModule;