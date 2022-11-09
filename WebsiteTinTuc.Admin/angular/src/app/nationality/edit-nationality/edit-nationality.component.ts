import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {AppComponentBase} from '../../../shared/app-component-base';
import {NationalityService} from '../../../shared/services/nationality-service';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {finalize} from 'rxjs/operators';
import {AppConsts} from '../../../shared/AppConsts';

@Component({
    selector: 'app-edit-nationality',
    templateUrl: './edit-nationality.component.html',
    styleUrls: ['./edit-nationality.component.css']
})
export class EditNationalityComponent extends AppComponentBase implements OnInit {
    name: string;
    id: string;
    saving = false;
    fileToUpload: File | null = null;
    imgSrc: string | ArrayBuffer = '';
    @Output() onSave = new EventEmitter<any>();
    isShow = false;
    constructor(
        injector: Injector,
        private nationalityService: NationalityService,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
        this.imgSrc = AppConsts.remoteServiceBaseUrl + '/' + this.imgSrc;
        console.log(this.imgSrc);
    }

    removeImage() {
        this.imgSrc = null;
        this.fileToUpload = null;
        (document.getElementById('file') as any).value = '';
        this.isShow =  !this.isShow;
    }

    handleFileInput(e) {
        this.fileToUpload = e.target.files.item(0);
        const reader = new FileReader();
        reader.readAsDataURL(this.fileToUpload);
        reader.onload = () => {
            this.imgSrc = reader.result;
        };
    }

    save(): void {
        this.saving = true;
        const form = new FormData();
        form.append('Name', this.name);
        form.append('Id', this.id);
        if (this.fileToUpload) {
            form.append('Image', this.fileToUpload);
        }
        this.nationalityService
            .put(form)
            .pipe(
                finalize(() => {
                    this.saving = false;
                })
            )
            .subscribe(() => {
                this.notify.info(this.l('Sửa thành công!'));
                this.bsModalRef.hide();
                this.onSave.emit();
            });
    }
}
