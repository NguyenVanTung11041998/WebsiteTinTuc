import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { BaseApiService } from "./base-api-service";
import { Observable } from "rxjs";
import { ResponseApi } from "../models/response-api";
import { PageModel } from "../models/page-model";
import { BranchJobDto } from "../models/branch-job";

@Injectable({
  providedIn: "root",
})
export class BranchJobService extends BaseApiService {
  constructor(http: HttpClient) {
    super(http);
  }

  getPagingLevel(
    pageSize: number,
    currentPage: number,
    searchText: string
  ): Observable<ResponseApi<PageModel<BranchJobDto>>> {
    return this.http.get<ResponseApi<PageModel<BranchJobDto>>>(
      this.baseUrl +
        "/api/services/app/BranchJob/GetAllBranchJobPaging?PageSize=" +
        pageSize +
        "&CurrentPage=" +
        currentPage +
        (searchText ? "&SearchText=" + searchText : "")
    );
  }
  deleteBranchJob(id: string): Observable<any> {
    return this.http.delete(
      this.baseUrl + "/api/services/app/BranchJob/Delete?id=" + id
    );
  }
  get(id: string): Observable<ResponseApi<BranchJobDto>> {
    return this.http.get<ResponseApi<BranchJobDto>>(
      this.baseUrl + "/api/services/app/BranchJob/GetBranchJobById?id=" + id
    );
  }
}
