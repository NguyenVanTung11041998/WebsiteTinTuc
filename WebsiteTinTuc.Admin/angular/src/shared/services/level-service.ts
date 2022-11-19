import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {BaseApiService} from './base-api-service';
import {Observable} from 'rxjs';
import {ResponseApi} from '../models/response-api';
import {PageModel} from '../models/page-model';
import {LevelDto} from '../models/level';
import {CreateUpdateLevel} from '../models/create-update-level';

@Injectable({
    providedIn: 'root',
})
export class LevelService extends BaseApiService {
    constructor(http: HttpClient) {
        super(http);
    }

    getPagingLevel(
        pageSize: number,
        currentPage: number,
        searchText: string,
    ): Observable<ResponseApi<PageModel<LevelDto>>> {
        return this.http.get<ResponseApi<PageModel<LevelDto>>>(
            this.baseUrl +
            '/api/services/app/Level/GetAllLevelPaging?PageSize=' +
            pageSize +
            '&CurrentPage=' +
            currentPage +
            (searchText ? '&SearchText=' + searchText : '')
        );
    }

    get(id: string): Observable<ResponseApi<LevelDto>> {
        return this.http.get<ResponseApi<LevelDto>>(
            this.baseUrl + '/api/services/app/Level/GetLevelById?id=' + id
        );
    }

    deleteLevel(id: string): Observable<any> {
        return this.http.delete(
            this.baseUrl + '/api/services/app/Level/DeleteLevel?id=' + id
        );
    }

    create(level: CreateUpdateLevel): Observable<ResponseApi<LevelDto>> {
        return this.http.post<ResponseApi<LevelDto>>(
            this.baseUrl + '/api/services/app/Level/CreateLevel', level
        );
    }

    edit(level: CreateUpdateLevel): Observable<ResponseApi<LevelDto>> {
        return this.http.put<ResponseApi<LevelDto>>(
            this.baseUrl + '/api/services/app/Level/UpdateLevel', level
        );
    }
}
