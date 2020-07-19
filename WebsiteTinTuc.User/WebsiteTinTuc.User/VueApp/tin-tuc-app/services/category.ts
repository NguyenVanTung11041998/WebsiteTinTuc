import request from '@/tin-tuc-app/constants/request'

const getAllCategory = (): Promise<any> => {
	return request({
		method: 'GET',
		url: '/api/services/app/Category/GetAllCategory',
	});
}

const CATEGORY_SERVICES = {
	getAllCategory: getAllCategory
}

Object.freeze(CATEGORY_SERVICES);

export default CATEGORY_SERVICES;