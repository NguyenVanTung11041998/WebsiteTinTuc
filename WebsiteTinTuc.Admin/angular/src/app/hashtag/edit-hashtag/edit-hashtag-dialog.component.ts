import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {finalize} from 'rxjs/operators';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {AppComponentBase} from '@shared/app-component-base';
import {HashtagDto} from '../../../shared/models/hashtag';
import {HashtagService} from '../../../shared/services/hashtag-service';
import {ResponseApi} from '../../../shared/models/response-api';

@Component({
    templateUrl: 'edit-hashtag-dialog.component.html'
})
export class EditHashtagDialogComponent extends AppComponentBase
    implements OnInit {
    saving = false;
    hashtag: HashtagDto = new HashtagDto();
    id: string;

    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private hashtagService: HashtagService,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.hashtagService.get(this.id).subscribe((result: ResponseApi<HashtagDto>) => {
            this.hashtag = result.result;
        });
    }

    save(): void {
        this.saving = true;
        this.hashtagService
            .put(this.hashtag)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('Cập nhật thành công'));
                this.bsModalRef.hide();
                this.onSave.emit();
            });
    }
}
