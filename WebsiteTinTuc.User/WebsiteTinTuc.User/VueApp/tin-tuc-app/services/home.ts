import request from '@/tin-tuc-app/constants/request';
import { HomeFilter } from '../store/interfaces/home';

const getAllCompanyPostPaging = (filter: HomeFilter): Promise<any> => {
    return request({
        method: 'GET',
        url: "/api/services/app/Home/GetAllCompanyPostPaging",
        params: filter
    });
}

const HOME_SERVICES = {
    getAllCompanyPostPaging: getAllCompanyPostPaging
}

Object.freeze(HOME_SERVICES);

export default HOME_SERVICES;