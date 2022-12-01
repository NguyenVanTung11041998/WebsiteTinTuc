import { Component, Injector, OnInit } from "@angular/core";
import { PageSizeModel } from "../../shared/models/page-size-model";
import { BranchJobDto } from "../../shared/models/branch-job";
import { AppComponentBase } from "../../shared/app-component-base";
import { BsModalRef, BsModalService } from "ngx-bootstrap/modal";
import { BranchJobService } from "../../shared/services/branch-job-service";
import { finalize } from "rxjs/operators";
import { CreateBranchJobComponent } from "./create-branch-job/create-branch-job.component";
import { EditBranchJobComponent } from "./edit-branch-job/edit-branch-job.component";

@Component({
  selector: "app-branch-job",
  templateUrl: "./branch-job.component.html",
  styleUrls: ["./branch-job.component.css"],
})
export class BranchJobComponent extends AppComponentBase implements OnInit {
  public currentPage = 1;
  public pageSize = 5;
  public searchText = "";
  public isTableLoading = false;
  public branchJobs: BranchJobDto[];
  public advancedFiltersVisible = false;
  public totalCount = 0;
  public pageSizeSelects: PageSizeModel[] = [];

  constructor(
    injector: Injector,
    private branchJobService: BranchJobService,
    private _modalService: BsModalService
  ) {
    super(injector);
    for (let i = 5; i <= 100; i += 5) {
      this.pageSizeSelects.push({
        value: i,
        name: `${i} Bản ghi / Page`,
      } as PageSizeModel);
    }
  }
  ngOnInit(): void {
    this.getDataPaging();
  }
  getDataPage(page: number) {
    this.currentPage = page;
    this.getDataPaging();
  }
  onChange() {
    this.getDataPaging();
  }
  refresh() {
    this.currentPage = 1;
    this.getDataPaging();
  }
  getDataPaging() {
    this.branchJobService
      .getPagingLevel(this.pageSize, this.currentPage, this.searchText)
      .subscribe((res) => {
        this.branchJobs = res.result.items;
        this.totalCount = res.result.totalCount;
      });
  }
  deleteBranchJob(branchJob: BranchJobDto): void {
    abp.message.confirm(
      this.l(`Bạn có muốn xoá ${branchJob.name} không ?`),
      "Thông báo",
      (result: boolean) => {
        if (result) {
          this.branchJobService
            .deleteBranchJob(branchJob.id)
            .pipe(
              finalize(() => {
                abp.notify.success(this.l("Xoá thành công!"));
                this.refresh();
              })
            )
            .subscribe(
              () => {},
              (error) => {
                this.notify.error(error.error.error.message);
              }
            );
        }
      }
    );
  }

  createBranchJob(): void {
    this.showDialog();
  }

  editBranchJob(branchJob: BranchJobDto): void {
    this.showDialog(branchJob.id);
  }

  showDialog(id?: string): void {
    let diaLog: BsModalRef;
    if (!id) {
      diaLog = this._modalService.show(CreateBranchJobComponent, {
        class: "modal-lg",
      });
    } else {
      diaLog = this._modalService.show(EditBranchJobComponent, {
        class: "modal-lg",
        initialState: {
          id: id,
        },
      });
    }

    diaLog.content.onSave.subscribe(() => {
      this.refresh();
    });
  }
}
