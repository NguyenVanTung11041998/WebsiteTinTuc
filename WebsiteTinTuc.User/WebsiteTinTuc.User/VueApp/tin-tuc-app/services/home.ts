import request from '@/tin-tuc-app/constants/request';
import { HomeFilter } from '../store/interfaces/home';

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

const HOME_SERVICES = {
    getAllCompanyPostPaging: getAllCompanyPostPaging,
    getTopNewPost: getTopNewPost
}

Object.freeze(HOME_SERVICES);

export default HOME_SERVICES;