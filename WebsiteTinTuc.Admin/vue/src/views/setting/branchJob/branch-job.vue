<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Từ khóa')+':'" style="width:100%">
                <Input v-model="pageRequest.keyWord" :placeholder="L('Tên Ngành nghề')" />
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Button @click="create" icon="android-add" type="primary" size="large">{{L('Thêm mới')}}</Button>
            <Button
              icon="ios-search"
              type="primary"
              size="large"
              @click="getPage"
              class="toolbar-btn"
            >{{L('Find')}}</Button>
          </Row>
        </Form>
        <div class="row margin-top-10">
          <Table
            :loading="loading"
            :columns="columns"
            :no-data-text="L('Không có dữ liệu')"
            border
            :data="list"
          ></Table>
          <Page
            show-sizer
            class-name="fengpage"
            :total="totalCount"
            class="margin-top-10"
            @on-change="pageChange"
            @on-page-size-change="pagesizeChange"
            :page-size="pageSize"
            :current="currentPage"
          ></Page>
        </div>
      </div>
    </Card>
    <create-or-edit-branch-job v-model="createOrEditModalShow" @save-success="saveSuccess" />
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "../../../store/entities/page-request";
import Hashtag from "../../../store/entities/hashtag";
import GrantedPermission from "../../../store/constants/granted-permission";
import PermissionNames from "../../../store/constants/permission-names";
import Nationality from "../../../store/entities/nationality";
import BranchJob from "@/store/entities/branch-job";
import CreateOrEditBranchJob from "./create-or-edit-branch-job.vue";
class PageBranchJobsRequest extends PageRequest {}

@Component({
  components: { CreateOrEditBranchJob },
})
export default class BranchJobs extends AbpBase {
  pageRequest = new PageBranchJobsRequest();
  createOrEditModalShow: boolean = false;
  async created() {
    await this.getPage();
  }
  create() {
    const branchJob = { name: "" } as BranchJob;
    this.$store.commit("branchJob/setBranchJob", branchJob);
    this.createOrEditModalShow = true;
  }
  edit() {
    this.createOrEditModalShow = true;
  }
  get list() {
    return this.$store.state.branchJob.list;
  }
  get loading() {
    return this.$store.state.branchJob.loading;
  }
  pageChange(page: number) {
    this.$store.commit("branchJob/setCurrentPage", page);
    this.getPage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("branchJob/setPageSize", pagesize);
    this.getPage();
  }
  async getPage() {
    let param = {
      searchText: this.pageRequest.keyWord,
      currentPage: this.currentPage,
      pageSize: this.pageSize,
    };
    await this.$store.dispatch({
      type: "branchJob/getAllBranchJobPaging",
      data: param,
    });
  }
  get pageSize() {
    return this.$store.state.branchJob.pageSize;
  }
  get totalCount() {
    return this.$store.state.branchJob.totalCount;
  }
  get currentPage() {
    return this.$store.state.branchJob.currentPage;
  }
  async saveSuccess() {
    await this.getPage();
  }
  columns = [
    {
      title: this.L("Tên ngành nghề"),
      key: "name",
    },
    {
      title: this.L("Thời gian tạo"),
      key: "creationTime",
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.creationTime).toLocaleString());
      },
    },
    {
      title: this.L("Hành động"),
      key: "Actions",
      width: 150,
      render: (h: any, params: any) => {
        return h("div", [
          h(
            "Button",
            {
              props: {
                type: "primary",
                size: "small",
              },
              style: {
                marginRight: "5px",
                display: GrantedPermission.isGranted(
                  PermissionNames.Pages_Update_BranchJob
                )
                  ? ""
                  : "none",
              },
              on: {
                click: () => {
                  const branchJob = { ...params.row };
                  this.$store.commit("branchJob/setBranchJob", branchJob);
                  this.edit();
                },
              },
            },
            this.L("Sửa")
          ),
          h(
            "Button",
            {
              props: {
                type: "error",
                size: "small",
              },
              style: GrantedPermission.isGranted(
                PermissionNames.Pages_Delete_BranchJob
              )
                ? ""
                : "display: none;",
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Thông báo"),
                    content: this.L(`Xóa branchJob ${params.row.name}`),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "branchJob/deleteBranchJob",
                        data: params.row.id,
                      });
                      await this.getPage();
                      this.$Message.success(
                        `Xóa branchJob ${params.row.name} thành công`
                      );
                    },
                  });
                },
              },
            },
            this.L("Xóa")
          ),
        ]);
      },
    },
  ];
}
</script>