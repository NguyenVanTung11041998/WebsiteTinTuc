import request from '@/tin-tuc-app/constants/request';

const getAllHashtagIsHot = (): Promise<any> => {
	return request({
		method: 'GET',
		url: '/api/services/app/Hashtag/GetAllHashtagIsHot',
	});
}

const HASHTAG_SERVICES = {
	getAllHashtagIsHot: getAllHashtagIsHot
}

Object.freeze(HASHTAG_SERVICES);

export default HASHTAG_SERVICES;