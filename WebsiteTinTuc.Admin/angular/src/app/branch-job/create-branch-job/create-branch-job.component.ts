import {
	Component,
	EventEmitter,
	Injector,
	OnDestroy,
	OnInit,
	Output,
} from '@angular/core';
import { AppComponentBase } from '../../../shared/app-component-base';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { finalize } from 'rxjs/operators';
import { BranchJobDto } from '../../../shared/models/branch-job';
import { BranchJobService } from '../../../shared/services/branch-job-service';
import { Validators, Editor, Toolbar } from 'ngx-editor';
import { AbstractControl, FormControl, FormGroup } from '@angular/forms';
@Component({
	selector: 'app-create-branch-job',
	templateUrl: './create-branch-job.component.html',
	styleUrls: ['./create-branch-job.component.css'],
})
export class CreateBranchJobComponent
	extends AppComponentBase
	implements OnInit, OnDestroy
{
	editor: Editor;
	toolbar: Toolbar = [
		['bold', 'italic'],
		['underline', 'strike'],
		['code', 'blockquote'],
		['ordered_list', 'bullet_list'],
		[{ heading: ['h1', 'h2', 'h3', 'h4', 'h5', 'h6'] }],
		['link', 'image'],
		['text_color', 'background_color'],
		['align_left', 'align_center', 'align_right', 'align_justify'],
	];

	saving = false;
	branchJob: BranchJobDto = new BranchJobDto();
	@Output() onSave = new EventEmitter<any>();

	constructor(
		injector: Injector,
		private branchJobService: BranchJobService,
		public bsModalRef: BsModalRef
	) {
		super(injector);
	}

	ngOnInit(): void {
		this.editor = new Editor();
	}
	ngOnDestroy(): void {
		this.editor.destroy();
	}
	save(): void {
		this.saving = true;
		this.branchJobService
			.create(this.branchJob)
			.pipe(
				finalize(() => {
					this.saving = false;
				})
			)
			.subscribe(
				() => {
					this.notify.info(this.l('Thêm thành công!'));
					this.bsModalRef.hide();
					this.onSave.emit();
				},
				(error) => {
					this.notify.error(error.error.error.message, 'Lỗi');
				}
			);
	}
}
