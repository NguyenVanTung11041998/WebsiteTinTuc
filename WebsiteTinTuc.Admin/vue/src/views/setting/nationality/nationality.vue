<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Từ khóa')+':'" style="width:100%">
                <Input v-model="pageRequest.keyWord" :placeholder="L('Tên Nationality')" />
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
    <create-or-edit-nationality v-model="createOrEditModalShow" @save-success="saveSuccess" />
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
import CreateOrEditNationality from "./create-or-edit-nationality.vue";
import Nationality from "../../../store/entities/nationality";
class PageNationalitiesRequest extends PageRequest {}

@Component({
  components: { CreateOrEditNationality },
})
export default class Nationalities extends AbpBase {
  pageRequest = new PageNationalitiesRequest();
  createOrEditModalShow: boolean = false;
  async created() {
    await this.getPage();
  }
  create() {
    const nationality = { name: "", image: null } as Nationality;
    this.$store.commit("nationality/setNationality", nationality);
    this.createOrEditModalShow = true;
  }
  edit() {
    this.createOrEditModalShow = true;
  }
  get list() {
    return this.$store.state.nationality.list;
  }
  get loading() {
    return this.$store.state.nationality.loading;
  }
  pageChange(page: number) {
    this.$store.commit("nationality/setCurrentPage", page);
    this.getPage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("nationality/setPageSize", pagesize);
    this.getPage();
  }
  async getPage() {
    let param = {
      searchText: this.pageRequest.keyWord,
      currentPage: this.currentPage,
      pageSize: this.pageSize,
    };
    await this.$store.dispatch({
      type: "nationality/getAllNationalityPaging",
      data: param,
    });
  }
  get pageSize() {
    return this.$store.state.nationality.pageSize;
  }
  get totalCount() {
    return this.$store.state.nationality.totalCount;
  }
  get currentPage() {
    return this.$store.state.nationality.currentPage;
  }
  async saveSuccess() {
    await this.getPage();
  }
  columns = [
    {
      title: this.L("Tên Quốc tịch"),
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
                  PermissionNames.Pages_Update_Nationality
                )
                  ? ""
                  : "none",
              },
              on: {
                click: () => {
                  const nationality = { ...params.row };
                  this.$store.commit("nationality/setNationality", nationality);
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
                PermissionNames.Pages_Delete_Nationality
              )
                ? ""
                : "display: none;",
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Thông báo"),
                    content: this.L(`Xóa nationality ${params.row.name}`),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "nationality/deleteNationality",
                        data: params.row.id,
                      });
                      await this.getPage();
                      this.$Message.success(
                        `Xóa nationality ${params.row.name} thành công`
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