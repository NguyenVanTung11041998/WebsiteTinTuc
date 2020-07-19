import request from '@/tin-tuc-app/constants/request'

const getAllHashtag = (): Promise<any> => {
	return request({
		method: 'GET',
		url: '/api/services/app/Hashtag/GetAllHashtags',
	});
}

const HASHTAG_SERVICES = {
	getAllHashtag: getAllHashtag
}

Object.freeze(HASHTAG_SERVICES);

export default HASHTAG_SERVICES;