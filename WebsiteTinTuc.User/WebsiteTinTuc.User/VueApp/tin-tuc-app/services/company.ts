import request from '@/tin-tuc-app/constants/request';

const getTopCompanyProminent = (count?: number): Promise<any> => {
	return request({
		method: 'GET',
		url: `/api/services/app/Company/GetTopCompanyProminent?count=${count ? count : ""}`,
	});
}

const COMPANY_SERVICES = {
	getTopCompanyProminent: getTopCompanyProminent
}

Object.freeze(COMPANY_SERVICES);

export default COMPANY_SERVICES;