import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {AppComponentBase} from '../../../shared/app-component-base';
import {LevelDto} from '../../../shared/models/level';
import {LevelService} from '../../../shared/services/level-service';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {finalize} from 'rxjs/operators';
import {ResponseApi} from '../../../shared/models/response-api';

@Component({
    selector: 'app-edit-level',
    templateUrl: './edit-level.component.html',
    styleUrls: ['./edit-level.component.css']
})
export class EditLevelComponent extends AppComponentBase implements OnInit {

    saving = false;
    level: LevelDto = new LevelDto();
    @Output() onSave = new EventEmitter<any>();
    id: string;
    constructor(
        injector: Injector,
        private levelService: LevelService,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.levelService.get(this.id).subscribe((result: ResponseApi<LevelDto>) => {
            this.level = result.result;
        });
    }

    save(): void {
        this.saving = true;
        this.levelService
            .edit(this.level)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('Sửa thành công!'));
                this.bsModalRef.hide();
                this.onSave.emit();
            }, error => {
                this.notify.error(error.error.error.message, 'Lỗi');
            });
    }

}
