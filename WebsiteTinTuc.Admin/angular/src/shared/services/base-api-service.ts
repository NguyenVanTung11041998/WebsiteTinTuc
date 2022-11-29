import { AppConsts } from './../../shared/AppConsts';
import { HttpClient } from '@angular/common/http';
import { InjectionToken } from '@angular/core';
export const API_BASE_URL = new InjectionToken<string>('API_BASE_URL');

export abstract class BaseApiService {
	protected baseUrl = AppConsts.remoteServiceBaseUrl;

	protected http: HttpClient;

	constructor(http: HttpClient) {
		this.http = http;
	}
}
