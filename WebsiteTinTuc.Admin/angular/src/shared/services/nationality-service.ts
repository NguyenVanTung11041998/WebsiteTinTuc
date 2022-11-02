import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {BaseApiService} from './base-api-service';
import {Observable} from 'rxjs';
import {ResponseApi} from '../models/response-api';
import {PageModel} from '../models/page-model';
import {NationalityDto} from '../models/nationality';

@Injectable({
    providedIn: 'root'
})
export class NationalityService extends BaseApiService {

    constructor(http: HttpClient) {
        super(http);
    }

    create(fileToUpload: FormData): Observable<ResponseApi<NationalityDto>> {
        return this.http.post<ResponseApi<NationalityDto>>(
            this.baseUrl + '/api/services/app/Nationality/CreateNationality', fileToUpload
        );
    }

    getPagingNationality(
        pageSize: number,
        currentPage: number,
        searchText: string
    ): Observable<ResponseApi<PageModel<NationalityDto>>> {
        return this.http.get<ResponseApi<PageModel<NationalityDto>>>(
            this.baseUrl +
            '/api/services/app/Nationality/GetAllNationalityPaging?PageSize=' +
            pageSize +
            '&CurrentPage=' +
            currentPage +
            (searchText ? '&SearchText=' + searchText : '')
        );
    }

    deleteNationality(id: string): Observable<any> {
        return this.http.delete(
            this.baseUrl + '/api/services/app/Nationality/DeleteNationality?id=' + id
        );
    }
}
