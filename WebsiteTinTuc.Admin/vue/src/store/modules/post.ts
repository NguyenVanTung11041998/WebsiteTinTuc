import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import Hashtag from '../entities/hashtag';
import Post from '../entities/post';

interface PostState extends ListState<Post> {
    post: Post;
    hashtags: Hashtag[];
    imageUrl: string;
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
        imageUrl: '#'
    };
    getters = {
        post(state: PostState) {
            return state.post;
        },
        hashtags(state: PostState) {
            return state.hashtags;
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
        async create(context: ActionContext<PostState, any>, payload: any) {
            await Ajax.post('/api/services/app/Post/CreatePost', payload.data);
        },
        async update(context: ActionContext<PostState, any>, payload: any) {
            await Ajax.put('/api/services/app/Post/EditPost', payload.data);
        },
        async delete(context: ActionContext<PostState, any>, payload: any) {
            await Ajax.delete('/api/services/app/Post/DeletePost?Id=' + payload.data);
        },
        async getPostById(context: ActionContext<PostState, any>, payload: any) {
            let response = await Ajax.get('/api/services/app/Post/GetPostById?Id=' + payload.id);
            const data = response.data.result as Post;
            context.state.post = data;
            return data;
        },
        async getAllHashtags(context: ActionContext<PostState, any>) {
            let response = await Ajax.get('/api/services/app/Hashtag/GetAllHashtags');
            const data = response.data.result as Hashtag[];
            context.state.hashtags = data;
            return data;
        },
        async uploadImage({ commit }, payload: any) {
            const response = await Ajax.post('/api/services/app/Post/UploadImage', payload.data);
            commit('setImage', response.data.result);
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
        },
        setImage(state: PostState, imageUrl: string) {
            state.imageUrl = imageUrl;
        }
    }
}
const postModule = new PostModule();
export default postModule;