import {HashtagDto} from './../models/hashtag';
import {Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {BaseApiService} from './base-api-service';
import {ResponseApi} from '@shared/models/response-api';
import {PageModel} from '@shared/models/page-model';
import {CreateUpdateHashtag} from '@shared/models/create-update-hashtag';

@Injectable({
	providedIn: 'root',
})
export class HashtagService extends BaseApiService {
	constructor(http: HttpClient) {
		super(http);
	}

	getPagingHashtag(
		pageSize: number,
		currentPage: number,
		searchText: string
	): Observable<ResponseApi<PageModel<HashtagDto>>> {
		return this.http.get<ResponseApi<PageModel<HashtagDto>>>(
			this.baseUrl +
				'/api/services/app/Hashtag/GetAllHashtagPaging?PageSize=' +
				pageSize +
				'&CurrentPage=' +
				currentPage +
				(searchText ? '&SearchText=' + searchText : '')
		);
	}
    get(id:string) :Observable<ResponseApi<HashtagDto>>{
        return this.http.get<ResponseApi<HashtagDto>>(
            this.baseUrl + '/api/services/app/Hashtag/GetHashtagById?id=' + id
        );
    }
    put(hashtag: CreateUpdateHashtag) :Observable<ResponseApi<HashtagDto>>{
        return this.http.put<ResponseApi<HashtagDto>>(
            this.baseUrl + '/api/services/app/Hashtag/UpdateHashtag', hashtag
        );
    }
    deleteHashtag(id: string) :Observable<any> {
        return this.http.delete(
            this.baseUrl + '/api/services/app/Hashtag/Delete?id=' + id
        );
    }
}
