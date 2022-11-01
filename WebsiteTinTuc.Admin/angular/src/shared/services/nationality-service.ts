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
  getPagingNationality(
      pageSize: number,
      currentPage: number,
      searchText: string
  ):Observable<ResponseApi<PageModel<NationalityDto>>>{
       return  this.http.get<ResponseApi<PageModel<NationalityDto>>>(
           this.baseUrl +
           '/api/services/app/Nationality/GetAllNationalityPaging?PageSize='+
           pageSize +
           '&CurrentPage=' +
           currentPage +
           (searchText ? '&SearchText=' + searchText : '')
       );
  }

}
