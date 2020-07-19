import URL from './url';
import axios, { AxiosError, AxiosRequestConfig, AxiosResponse } from 'axios'
// import Cookies from 'js-cookie'

const BASE_URL = URL;
const TIMEOUT = 60000;

const transformRequestOptions = (params: any) => {
	let options = '';
	for (const key in params) {
		if (typeof params[key] !== 'object' && params[key]) {
			options += `${key}=${params[key]}&`;
		} else if (
			typeof params[key] === 'object' &&
			params[key] &&
			params[key].length
		) {
			params[key].forEach((el: any) => {
				options += `${key}=${el}&`;
			});
		}
	}
	return options ? options.slice(0, -1) : options;
};

const request = axios.create({});
request.interceptors.request.use((config: AxiosRequestConfig) => {
	// config.paramsSerializer = params => transformRequestOptions(params);
	const token = localStorage.getItem('GateGame.AuthToken');
	// TODO Update authorization
	// config.headers.common.Authorization = `Bearer ${Cookies.get('Abp.AuthToken')}`;
	config.headers.common.Authorization = `Bearer ${token}`;
	
	config.baseURL = BASE_URL;
	config.timeout = TIMEOUT;
	return config;
});

request.interceptors.response.use(
	(response: AxiosResponse) => {
		return response.data;
	},
	(error: AxiosError) => {
		const originalRequest: AxiosRequestConfig = error.config;

		if (isAuthError(error)) {
			// TO DO: refresh token
			return Promise.reject(error);
		}

		return Promise.reject(error);
	}
);

function isAuthError(error: AxiosError): boolean | undefined {
	return error.response && error.response.status === 401;
}

export default request;

export function getErrorModal(
	error: AxiosError,
	title?: string,
	body?: string,
	visible: boolean = true
): { title?: string; visible: boolean; body?: string; reason?: string } {
	if (error.response && error.response.status) {
		return {
			title: title,
			visible: visible,
			body: body,
			reason: error.response.data as string | 'Unexpected error'
		};
	} else {
		return {
			title: 'Connection Failure',
			body: 'Sorry we are unable to complete your request',
			reason: 'Network error',
			visible: visible
		};
	}
}
