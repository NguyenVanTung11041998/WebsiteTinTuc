import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {AppComponentBase} from '../../../shared/app-component-base';
import {LevelDto} from '../../../shared/models/level';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {LevelService} from '../../../shared/services/level-service';
import {finalize} from 'rxjs/operators';

@Component({
    selector: 'app-create-level',
    templateUrl: './create-level.component.html',
    styleUrls: ['./create-level.component.css']
})
export class CreateLevelComponent extends AppComponentBase implements OnInit {

    saving = false;
    level: LevelDto = new LevelDto();
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private levelService: LevelService,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }

    save(): void {
        this.saving = true;
        this.levelService
            .create(this.level)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('Thêm thành công!'));
                this.bsModalRef.hide();
                this.onSave.emit();
            }, error => {
                this.notify.error(error.error.error.message, 'Lỗi');
            });
    }
}
