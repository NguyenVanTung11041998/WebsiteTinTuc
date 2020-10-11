import request from '@/tin-tuc-app/constants/request';
import CompanyHome, { CompanyHomeInfoRequest } from '../store/interfaces/company-home';

const getCompanyByUrl = (params: CompanyHomeInfoRequest): Promise<any> => {
	return request({
		method: 'GET',
        url: `/api/services/app/CompanyHome/GetCompanyByUrl`,
        params: params
	});
}

const getPostOfCompanyPaging = (params: CompanyHomeInfoRequest): Promise<any> => {
    return request({
		method: 'GET',
        url: `/api/services/app/CompanyHome/GetPostOfCompanyPaging`,
        params: params
	});
}

const getPostByUrl = (params: CompanyHomeInfoRequest): Promise<any> => {
    return request({
		method: 'GET',
        url: `/api/services/app/CompanyHome/GetPostByUrl`,
        params: params
	});
}

const COMPANY_HOME_SERVICES = {
    getCompanyByUrl: getCompanyByUrl,
    getPostOfCompanyPaging: getPostOfCompanyPaging,
    getPostByUrl: getPostByUrl
}

Object.freeze(COMPANY_HOME_SERVICES);

export default COMPANY_HOME_SERVICES;