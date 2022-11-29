import {Component, Injector, OnInit} from '@angular/core';
import {AppComponentBase} from '@shared/app-component-base';
import {HashtagDto} from '../../shared/models/hashtag';
import {HashtagService} from '../../shared/services/hashtag-service';
import {finalize} from 'rxjs/operators';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {EditHashtagDialogComponent} from './edit-hashtag/edit-hashtag-dialog.component';
import {CreateHashtagDialogComponent} from './create-hashtag/create-hashtag-dialog.component';

@Component({
    selector: 'app-hashtag',
    templateUrl: './hashtag.component.html',
    styleUrls: ['./hashtag.component.css'],
})
export class HashtagComponent extends AppComponentBase implements OnInit {
    public currentPage = 1;
    public pageSize = 2;
    public searchText = '';
    public isTableLoading = false;

    public hastags: HashtagDto[];
    public advancedFiltersVisible = false;
    public totalCount = 0;

    constructor(injector: Injector, private hastagService: HashtagService, private _modalService: BsModalService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getDataPaging();
    }

    getDataPaging() {
        this.hastagService
            .getPagingHashtag(this.pageSize, this.currentPage, this.searchText)
            .subscribe((res) => {
                this.hastags = res.result.items;
                this.totalCount = res.result.totalCount;
            });
    }

    getDataPage(page: number) {
        this.currentPage = page;
        this.getDataPaging();
    }

    refresh() {
        this.currentPage = 1;
        this.getDataPaging();
    }

    showDialog(id?: string): void {
        let diaLog: BsModalRef;
        if (!id) {
            diaLog = this._modalService.show(
                CreateHashtagDialogComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            diaLog = this._modalService.show(
                EditHashtagDialogComponent,
                {
                    class: 'modal-lg',
                    initialState: {
                        id: id,
                    },
                }
            );
        }

        diaLog.content.onSave.subscribe(() => {
            this.refresh();
        });
    }

    editHashtag(hashtag: HashtagDto): void {
        this.showDialog(hashtag.id);
    }

    createHashtag(): void {
        this.showDialog();
    }

    deleteHastag(hashtag: HashtagDto): void {
        abp.message.confirm(
            this.l(`Bạn có muốn xoá ${hashtag.name} không ?`),
            'Thông báo',
            (result: boolean) => {
                if (result) {
                    this.hastagService
                        .deleteHashtag(hashtag.id)
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
