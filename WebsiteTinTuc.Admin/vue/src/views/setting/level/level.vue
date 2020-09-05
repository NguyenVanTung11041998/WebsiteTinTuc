<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Từ khóa')+':'" style="width: 100%">
                <Input v-model="pageRequest.keyWord" :planceholder="L('Level')" />
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Col span="4">
              <Button @click="create" icon="android-add" type="primary" size="large">{{L('Thêm mới')}}</Button>
            </Col>
            <Col span="4">
              <Button
                icon="ios-search"
                type="primary"
                size="large"
                class="tollbar-btn"
                @click="getPage"
              >{{L('Tìm kiếm')}}</Button>
            </Col> 
          </Row>
        </Form>
        <div class="row margin-top-10">
          <Table
            :loading="loading"
            :columns="columns"
            :no-data-text="L('Không có dữ liệu')"
            :data="list"
            border
          ></Table>
          <Page
            show-sizer
            class-name="fengpage"
            class="margin-top-10"
            :total="totalCount"
            @on-change="pageChange"
            @on-page-size-change="pagesizeChange"
            :page-size="pageSize"
            :curent="currentPage"
          ></Page>
        </div>
      </div>
    </Card>
    <create-or-edit-level v-model="createOrEditModalShow" @save-success="saveSuccess" />
  </div>
</template>
<script lang="ts">
import PageRequest from "../../../store/entities/page-request";
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import AbpBase from "@/lib/abpbase";
import GrantedPermission from "@/store/constants/granted-permission";
import PermissionNames from "@/store/constants/permission-names";
import Level from "@/store/entities/level";
import CreateOrEditLevel from "./create-or-edit-level.vue";
class PageLevelRequest extends PageRequest {}
@Component({
  components: { CreateOrEditLevel },
})
export default class Levels extends AbpBase {
  pageRequest: PageLevelRequest = new PageLevelRequest();
  createOrEditModalShow: boolean = false;

  async created() {
    this.getPage();
  }
  create() {
    const level = { name: "" } as Level;
    this.$store.commit("level/setLevel", level);
    this.createOrEditModalShow = true;
  }
  get list() {
    return this.$store.state.level.list;
  }
  get loading() {
    return this.$store.state.level.loading;
  }
  get pageSize() {
    return this.$store.state.level.pageSize;
  }
  get totalCount() {
    return this.$store.state.level.totalCount;
  }
  get currentPage() {
    return this.$store.state.level.currentPage;
  }
  async getPage() {
    let param = {
      searchText: this.pageRequest.keyWord,
      currentPage: this.currentPage,
      pageSize: this.pageSize,
    };
    await this.$store.dispatch({
      type: "level/getAllLevelPaging",
      data: param,
    });
  }
  async pageChange(page: number) {
    await this.$store.commit("level/setCurrentPage", page);
    this.getPage();
  }
  async pagesizeChange(pageSize: number) {
    await this.$store.commit("level/setPageSize", pageSize);
    this.getPage();
  }

  async saveSuccess() {
    await this.getPage();
  }

  edit() {
    this.createOrEditModalShow = true;
  }

  columns = [
    {
      title: this.L("Tên level"),
      key: "name",
    },
    {
      title: this.L("Thời gian tạo"),
      key: "creationTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          `${new Date(params.row.creationTime).toLocaleTimeString()} ${new Date(params.row.creationTime).toLocaleDateString()}`
        );
      }
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
                  PermissionNames.Pages_Update_Level
                )
                  ? ""
                  : "none",
              },
              on: {
                click: () => {
                  const level = { ...params.row };
                  this.$store.commit("level/setLevel", level);
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
                PermissionNames.Pages_Delete_Level
              )
                ? ""
                : "none",
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Thông báo"),
                    content: this.L(`Xóa level ${params.row.name}`),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "level/deleteLevel",
                        data: params.row.id,
                      });
                      this.getPage();
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