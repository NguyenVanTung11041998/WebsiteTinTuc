import {Component, EventEmitter, Injector, OnInit, Output} from '@angular/core';
import {BsModalRef} from 'ngx-bootstrap/modal';
import {finalize} from 'rxjs/operators';
import {NationalityService} from '../../../shared/services/nationality-service';
import {AppComponentBase} from '../../../shared/app-component-base';

@Component({
    selector: 'app-creat-nationality',
    templateUrl: './creat-nationality.component.html',
    styleUrls: ['./creat-nationality.component.css']
})
export class CreatNationalityComponent extends AppComponentBase implements OnInit {
    name: string ;
    saving = false;
    fileToUpload: File | null = null;
    imgSrc: string | ArrayBuffer = '';
    isHidden = false;
    @Output() onSave = new EventEmitter<any>();

    constructor(
        injector: Injector,
        private nationalityService: NationalityService,
        public bsModalRef: BsModalRef
    ) {
        super(injector);
    }

    ngOnInit(): void {
    }
    handleFileInput(e) {
        this.fileToUpload = e.target.files.item(0);
        const reader = new FileReader();
        reader.readAsDataURL(this.fileToUpload);
        reader.onload = () => {
            this.imgSrc = reader.result;
        };
        this.isHidden = !this.isHidden;
    }
    removeImage() {
        this.imgSrc = null;
        this.fileToUpload = null;
        (document.getElementById('file') as any).value = '';
        this.isHidden = !this.isHidden;
    }
    save(): void {
        this.saving = true;
        const form = new FormData();
        form.append('Name', this.name);
        if (this.fileToUpload) {
            form.append('Image', this.fileToUpload);
        }
        this.nationalityService
            .create(form)
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
