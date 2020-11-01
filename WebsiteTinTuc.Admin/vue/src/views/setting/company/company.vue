<template>
  <div>
    <div>
      <Card dis-hover>
        <div class="page-body">
          <Form ref="queryForm" :label-width="100" label-position="left" inline>
            <Row :gutter="16">
              <Col span="8">
                <FormItem :label="L('Từ khóa') + ':'" style="width:100%">
                  <Input
                    v-model="pageRequest.keyword"
                    :placeholder="L('Tên bài viết')"
                    @on-enter="getPage"
                  />
                </FormItem>
              </Col>
            </Row>
            <Row>
              <Button @click="create" type="primary" size="large">{{
                L("Thêm mới")
              }}</Button>
              <Button
                icon="ios-search"
                type="primary"
                size="large"
                @click="getPage"
                class="toolbar-btn"
                >{{ L("Tìm kiếm") }}</Button
              >
            </Row>
          </Form>
          <div class="margin-top-10">
            <Table
              :loading="loading"
              :columns="columns"
              :no-data-text="L('Không có dữ liệu')"
              border
              :data="list"
            />
            <Page
              show-sizer
              class-name="fengpage"
              :total="totalCount"
              class="margin-top-10"
              @on-change="pageChange"
              @on-page-size-change="pagesizeChange"
              :page-size="pageSize"
              :current="currentPage"
            />
          </div>
        </div>
      </Card>
    </div>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "../../../store/entities/page-request";
import CreateOrEditCompany from "./create-or-edit-company.vue";
import Company from "../../../store/entities/company";
import PathNames from "../../../store/constants/path-names";
import GrantedPermission from "../../../store/constants/granted-permission";
import PermissionNames from "../../../store/constants/permission-names";

class PageCompanyRequest extends PageRequest {
  keyword: string = "";
}

@Component({
  components: { CreateOrEditCompany },
})
export default class Companies extends AbpBase {
  pageRequest: PageCompanyRequest = new PageCompanyRequest();
  edit(id: string) {
    this.$router.push({ name: PathNames.UpdateCompany, params: { id } });
  }

  create() {
    this.$router.push({ name: PathNames.CreateCompany });
  }
  pageChange(page: number) {
    this.$store.commit("company/setCurrentPage", page);
    this.getPage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("company/setPageSize", pagesize);
    this.getPage();
  }

  async getCompanyById(id) {
    await this.$store.dispatch({
      type: "company/getCompanyById",
      id: id,
    });
  }

  async getPage() {
    let param = {
      currentPage: this.currentPage,
      pageSize: this.pageSize,
      searchText: this.pageRequest.keyword,
    };

    await this.$store.dispatch({
      type: "company/getAll",
      data: param,
    });
  }
  get list() {
    return this.$store.state.company.list;
  }
  get loading() {
    return this.$store.state.company.loading;
  }
  get pageSize() {
    return this.$store.state.company.pageSize;
  }
  get totalCount() {
    return this.$store.state.company.totalCount;
  }
  get currentPage() {
    return this.$store.state.company.currentPage;
  }

  columns = [
    {
      title: this.L("Tên công ty"),
      key: "name",
      width: 200,
    },
    {
      title: this.L("Ngày tạo"),
      key: "creationTime",
      width: 150,
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.creationTime).toLocaleString());
      },
    },
    {
      title: this.L("Địa chỉ"),
      key: "locationDescription",
    },
    {
      title: this.L("Nổi bật"),
      render: (h: any, params: any) => {
        return h("div", [
          h("span", params.row.isHot ? "Có" : "Không"),
          h(
            "Button",
            {
              props: {
                type: "success",
                size: "small",
              },
              style: {
                marginLeft: "20px",
                display: GrantedPermission.isGranted(
                  PermissionNames.Pages_Setting_Hot_Company
                )
                  ? ""
                  : "none",
              },
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Thông báo"),
                    content: this.L("Bạn có muốn thay đổi không?"),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "company/settingHotOfCompany",
                        id: params.row.id,
                      });
                      this.$Message.success(
                        `Thay đổi nổi bật công ty ${params.row.name} thành công`
                      );
                      await this.getPage();
                    },
                  });
                },
              },
            },
            this.L("Thay đổi")
          ),
        ]);
      },
    },
    {
      title: this.L("Thao tác"),
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
                  PermissionNames.Pages_Update_Company
                )
                  ? ""
                  : "none",
              },
              on: {
                click: () => {
                  this.edit(params.row.id);
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
              style: {
                display: GrantedPermission.isGranted(
                  PermissionNames.Pages_Delete_Company
                )
                  ? ""
                  : "none",
              },
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Tips"),
                    content: this.L("DeleteCompanyConfirm"),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "company/delete",
                        data: params.row.id,
                      });
                      this.$Message.success(
                        `Xóa công ty ${params.row.name} thành công`
                      );
                      await this.getPage();
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

  async created() {
    await this.getPage();
  }
}
</script>
