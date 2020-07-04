import { ActionContext } from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import Hashtag from '../entities/hashtag'
import Post from '../entities/post';
import Category from '../entities/category';

interface PostState extends ListState<Post> {
    post: Post;
    hashtags: Hashtag[];
    categories: Category[];
}
class PostModule extends ListModule<PostState, any, Post>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Post>(),
        loading: false,
        post: new Post(),
        hashtags: new Array<Hashtag>(),
        categories: new Array<Category>()
    };
    getters = {
        post(state: PostState) {
            return state.post;
        }
    };
    actions = {
        async getAll(context: ActionContext<PostState, any>, payload: any) {
            context.state.loading = true;
            let response = await Ajax.get('/api/services/app/Post/GetAllPostPaging', { params: payload.data });
            context.state.loading = false;
            let page = response.data.result as PageResult<Post>;
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
            let response = await Ajax.get('/api/services/app/Post/GetPostById?Id=' + payload.id);
            return response.data.result as Hashtag;
        },
        async getAllCategories(context: ActionContext<PostState, any>) {
            let response = await Ajax.get('/api/services/app/Category/ GetAllCategory');
            const data = response.data.result as Category[];
            context.state.categories = data;
            return data;
        },
        async getAllHashtags(context: ActionContext<PostState, any>) {
            let response = await Ajax.get('/api/services/app/Hashtag/GetAllHashtags');
            const data = response.data.result as Hashtag[];
            context.state.hashtags = data;
            return data;
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