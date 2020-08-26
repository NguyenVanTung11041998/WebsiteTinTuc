import { ActionContext } from 'vuex';
import ListModule from './list-module';
import ListState from './list-state';
import Ajax from '../../lib/ajax';
import PageResult from '@/store/entities/page-result';
import Nationality from '../entities/nationality';

interface NationalityState extends ListState<Nationality> {
    nationality: Nationality
}
class NationalityModule extends ListModule<NationalityState, any, Nationality>{
    state = {
        totalCount: 0,
        currentPage: 1,
        pageSize: 10,
        list: new Array<Nationality>(),
        loading: false,
        nationality: new Nationality()
    }
    actions = {

    };
    mutations = {
        setCurrentPage(state: NationalityState, page: number) {
            state.currentPage = page;
        },
        setPageSize(state: NationalityState, pagesize: number) {
            state.pageSize = pagesize;
        },
        setNationality(state: NationalityState, nationality: Nationality) {
            state.nationality = nationality;
        }
    }
}
const nationalityModule = new NationalityModule();
export default nationalityModule;