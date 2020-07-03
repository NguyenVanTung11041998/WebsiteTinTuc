import { ActionContext } from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import Hashtag from '../entities/hashtag'
import Post from '../entities/post';

interface PostState extends ListState<Post> {
    post: Post;
}
class PostModule extends ListModule<PostState, any, Post>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Post>(),
        loading: false,
        post: new Post()
    };
    getters = {
        post(state: PostState) {
            return state.post;
        }
    };
    actions = {
        async getAll(context: ActionContext<PostState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/Post/GetAllPostPaging', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<Post>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createOrEdit(context: ActionContext<PostState, any>, payload: any) {
            await Ajax.post('/api/services/app/Post/CreateOrEdit', payload.data);
        },
        async delete(context: ActionContext<PostState, any>, payload: any) {
            await Ajax.delete('/api/services/app/Post/Delete?Id=' + payload.data);
        },
        async get(context: ActionContext<PostState, any>, payload: any) {
            let reponse = await Ajax.get('/api/services/app/Post/GetHashtagById?Id=' + payload.id);
            return reponse.data.result as Hashtag;
        }
    };
    mutations = {
        setCurrentPage(state: PostState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: PostState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setPost(state: PostState, post: Post) {
            state.post = post;
        }
    }
}
const postModule = new PostModule();
export default postModule;