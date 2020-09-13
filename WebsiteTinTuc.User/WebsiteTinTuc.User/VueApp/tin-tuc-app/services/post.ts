import request from '@/tin-tuc-app/constants/request';

const getTopPostProminent = (count?: number): Promise<any> => {
	return request({
		method: 'GET',
		url: `/api/services/app/Post/GetTopPostProminent?count=${count ? count : ""}`,
	});
}

const POST_SERVICES = {
	getTopPostProminent: getTopPostProminent
}

Object.freeze(POST_SERVICES);

export default POST_SERVICES;