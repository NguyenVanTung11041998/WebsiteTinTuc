import { Store, Module, ActionContext } from 'vuex'
import ListModule from './list-module'
import ListState from './list-state'
import Ajax from '../../lib/ajax'
import PageResult from '@/store/entities/page-result';
import Category from '../entities/category'

interface CategoryState extends ListState<Category> {
    category: Category
}
class CategoryModule extends ListModule<CategoryState, any, Category>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Category>(),
        loading: false,
        category: new Category()
    }
    actions = {
        async getAll(context: ActionContext<CategoryState, any>, payload: any) {
            context.state.loading = true;
            let reponse = await Ajax.get('/api/services/app/Category/GetAllCategoryPaging', { params: payload.data });
            context.state.loading = false;
            let page = reponse.data.result as PageResult<Category>;
            context.state.totalCount = page.totalCount;
            context.state.list = page.items;
        },
        async createOrEdit(context: ActionContext<CategoryState, any>, payload: any) {
            await Ajax.post('/api/services/app/Category/CreateOrEdit', payload.data);
        },
        async delete(context: ActionContext<CategoryState, any>, payload: any) {
            await Ajax.delete('/api/services/app/Category/Delete?Id=' + payload.data);
        },
        async get(context: ActionContext<CategoryState, any>, payload: any) {
            let reponse = await Ajax.get('/api/services/app/Category/GetCategoryById?Id=' + payload.id);
            return reponse.data.result as Category;
        }
    };
    mutations = {
        setCurrentPage(state: CategoryState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: CategoryState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setCategory(state: CategoryState, category: Category) {
            state.category = category;
        }
    }
}
const categoryModule = new CategoryModule();
export default categoryModule;