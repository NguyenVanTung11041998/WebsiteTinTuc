import {
  Component,
  EventEmitter,
  Injector,
  OnInit,
  Output,
} from "@angular/core";
import { AppComponentBase } from "../../../shared/app-component-base";
import { BsModalRef } from "ngx-bootstrap/modal";
import { finalize } from "rxjs/operators";
import { BranchJobDto } from "../../../shared/models/branch-job";
import { BranchJobService } from "../../../shared/services/branch-job-service";

@Component({
  selector: "app-create-branch-job",
  templateUrl: "./create-branch-job.component.html",
  styleUrls: ["./create-branch-job.component.css"],
})
export class CreateBranchJobComponent
  extends AppComponentBase
  implements OnInit
{
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

  ngOnInit(): void {}

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
          this.notify.info(this.l("Thêm thành công!"));
          this.bsModalRef.hide();
          this.onSave.emit();
        },
        (error) => {
          this.notify.error(error.error.error.message, "Lỗi");
        }
      );
  }
}
