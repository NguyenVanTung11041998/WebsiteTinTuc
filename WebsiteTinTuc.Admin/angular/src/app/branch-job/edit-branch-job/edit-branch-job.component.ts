import {
  Component,
  EventEmitter,
  Injector,
  OnInit,
  Output,
} from "@angular/core";
import { AppComponentBase } from "../../../shared/app-component-base";
import { BsModalRef } from "ngx-bootstrap/modal";
import { ResponseApi } from "../../../shared/models/response-api";
import { finalize } from "rxjs/operators";
import { BranchJobDto } from "../../../shared/models/branch-job";
import { BranchJobService } from "../../../shared/services/branch-job-service";

@Component({
  selector: "app-edit-branch-job",
  templateUrl: "./edit-branch-job.component.html",
  styleUrls: ["./edit-branch-job.component.css"],
})
export class EditBranchJobComponent extends AppComponentBase implements OnInit {
  saving = false;
  branchJob: BranchJobDto = new BranchJobDto();
  @Output() onSave = new EventEmitter<any>();
  id: string;
  constructor(
    injector: Injector,
    private branchJobService: BranchJobService,
    public bsModalRef: BsModalRef
  ) {
    super(injector);
  }

  ngOnInit(): void {
    this.branchJobService
      .get(this.id)
      .subscribe((result: ResponseApi<BranchJobDto>) => {
        this.branchJob = result.result;
      });
  }

  save(): void {
    this.saving = true;
    this.branchJobService
      .edit(this.branchJob)
      .pipe(
        finalize(() => {
          this.saving = false;
        })
      )
      .subscribe(
        () => {
          this.notify.info(this.l("Sửa thành công!"));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        (error) => {
          this.notify.error(error.error.error.message, "Lỗi");
        }
      );
  }
}
