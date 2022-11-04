import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {finalize} from 'rxjs/operators';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {AppComponentBase} from '@shared/app-component-base';
import {HashtagDto} from '../../../shared/models/hashtag';
import {HashtagService} from '../../../shared/services/hashtag-service';

@Component({
    templateUrl: 'create-hashtag-dialog.component.html'
})
export class CreateHashtagDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    hashtag: HashtagDto = new HashtagDto();

    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private hashtagService: HashtagService,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.hashtag.isHot = true;
    }

    save(): void {
        this.saving = true;

        this.hashtagService
            .create(this.hashtag)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('Thêm thành công!'));
                this.bsModalRef.hide();
                this.onSave.emit();
            });
    }
}
