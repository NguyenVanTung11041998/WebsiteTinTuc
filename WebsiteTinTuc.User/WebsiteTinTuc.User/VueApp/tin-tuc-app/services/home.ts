import request from '@/tin-tuc-app/constants/request';
import { HomeFilter, PostFilter } from '../store/interfaces/home';
import PageRequest from '../store/interfaces/page-request';

const getAllCompanyPostPaging = (filter: HomeFilter): Promise<any> => {
    return request({
        method: 'GET',
        url: "/api/services/app/Home/GetAllCompanyPostPaging",
        params: filter
    });
}

const getTopNewPost = (count: number): Promise<any> => {
    return request({
        method: 'GET',
        url: `/api/services/app/Home/GetTopNewPost?count=${count}`
    });
}

const getPostPaging = (filter: PostFilter): Promise<any> => {
    return request({
        method: 'GET',
        url: `/api/services/app/Home/GetPostPaging`,
        params: filter
    });
}

const HOME_SERVICES = {
    getAllCompanyPostPaging: getAllCompanyPostPaging,
    getTopNewPost: getTopNewPost,
    getPostPaging: getPostPaging
}

Object.freeze(HOME_SERVICES);

export default HOME_SERVICES;