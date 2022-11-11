import {Component, Injector, OnInit} from '@angular/core';
import {AppComponentBase} from '../../shared/app-component-base';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {LevelService} from '../../shared/services/level-service';
import {LevelDto} from '../../shared/models/level';
import {finalize} from 'rxjs/operators';
import {CreateLevelComponent} from './create-level/create-level.component';
import {EditLevelComponent} from './edit-level/edit-level.component';

@Component({
    selector: 'app-level',
    templateUrl: './level.component.html',
    styleUrls: ['./level.component.css']
})
export class LevelComponent extends AppComponentBase implements OnInit {
    public currentPage = 1;
    public pageSize = 4;
    public searchText = '';
    public isTableLoading = false;
    public levels: LevelDto[];
    public advancedFiltersVisible = false;
    public totalCount = 0;

    constructor(injector: Injector, private levelService: LevelService, private _modalService: BsModalService) {
        super(injector);
    }

    ngOnInit(): void {
        this.getDataPaging();
    }

    getDataPaging() {
        this.levelService
            .getPagingLevel(this.pageSize, this.currentPage, this.searchText)
            .subscribe((res) => {
                this.levels = res.result.items;
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

    createLevel(): void {
        this.showDialog();
    }
    editLevel(level: LevelDto): void {
        this.showDialog(level.id);
    }
    showDialog(id?: string): void {
        let diaLog: BsModalRef;
        if (!id) {
            diaLog = this._modalService.show(
                CreateLevelComponent,
                {
                    class: 'modal-lg',
                }
            );
        } else {
            diaLog = this._modalService.show(
                EditLevelComponent,
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

    deleteLevel(level: LevelDto): void {
        abp.message.confirm(
            this.l(`Bạn có muốn xoá ${level.name} không ?`),
            'Thông báo',
            (result: boolean) => {
                if (result) {
                    this.levelService
                        .deleteLevel(level.id)
                        .pipe(
                            finalize(() => {
                                abp.notify.success(
                                    this.l('Xoá thành công!')
                                );
                                this.refresh();
                            })
                        )
                        .subscribe(() => {
                        }, error => {
                            this.notify.error(error.error.error.message);
                        });
                }
            }
        );
    }
}