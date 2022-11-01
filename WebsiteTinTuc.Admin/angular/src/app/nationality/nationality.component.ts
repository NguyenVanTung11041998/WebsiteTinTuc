import {Component, Injector, OnInit} from '@angular/core';
import {AppComponentBase} from '../../shared/app-component-base';
import {NationalityDto} from '../../shared/models/nationality';
import {BsModalService} from 'ngx-bootstrap/modal';
import {NationalityService} from '../../shared/services/nationality-service';
import {HashtagDto} from '../../shared/models/hashtag';
import {finalize} from 'rxjs/operators';

@Component({
    selector: 'app-nationality',
    templateUrl: './nationality.component.html',
    styleUrls: ['./nationality.component.css']
})
export class NationalityComponent extends AppComponentBase implements OnInit {
    public currentPage = 1;
    public pageSize = 2;
    public searchText = '';
    public isTableLoading = false;

    public nationalitys: NationalityDto[];
    public advancedFiltersVisible = false;
    public totalCount = 0;

    constructor(injector: Injector, private nationalityService: NationalityService, private _modalService: BsModalService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getDataPagingNationality();
    }
    getDataPage(page: number) {
        this.currentPage = page;
        this.getDataPagingNationality();
    }
    refresh() {
        this.currentPage = 1;
        this.getDataPagingNationality();
    }
    getDataPagingNationality() {
        this.isTableLoading = true;
        this.nationalityService
            .getPagingNationality(this.pageSize, this.currentPage, this.searchText)
            .subscribe((res) => {
                this.nationalitys = res.result.items;
                this.totalCount = res.result.totalCount;
                this.isTableLoading = false;
            });
    }
    deleteNationality(nationality: NationalityDto): void {
        abp.message.confirm(
            this.l(`Bạn có muốn xoá ${nationality.name} không ?`),
            'Thông báo',
            (result: boolean) => {
                if (result) {
                    this.nationalityService
                        .deleteNationality(nationality.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(
                                    this.l('Xoá thành công!')
                                );
                                this.refresh();
                            })
                        )
                        .subscribe(() => {
                        });
                }
            }
        );
    }

}
