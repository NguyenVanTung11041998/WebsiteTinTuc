import request from '@/tin-tuc-app/constants/request';
import { AuthenticateRequest } from '../store/interfaces/authenticate';

const login = (params: AuthenticateRequest): Promise<any> => {
    return request({
        method: 'POST',
        url: '/api/TokenAuth/Authenticate',
        data: params
    });
}

const getCurrentLoginInformations = (): Promise<any> => {
	return request({
		method: 'GET',
		url: `/api/services/app/Session/GetCurrentLoginInformations`
	});
}

const AUTHENTICATE_SERVICES = {
    login: login,
    getCurrentLoginInformations: getCurrentLoginInformations
}

Object.freeze(AUTHENTICATE_SERVICES);

export default AUTHENTICATE_SERVICES;