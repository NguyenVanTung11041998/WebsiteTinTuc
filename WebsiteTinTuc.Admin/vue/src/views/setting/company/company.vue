<template>
  <div>
    <div>
      <Card dis-hover>
        <div class="page-body">
          <Form ref="queryForm" :label-width="100" label-position="left" inline>
            <Row :gutter="16">
              <Col span="8">
                <FormItem :label="L('Từ khóa')+':'" style="width:100%">
                  <Input
                    v-model="pageRequest.keyword"
                    :placeholder="L('Tên bài viết')"
                    @on-enter="getPage"
                  />
                </FormItem>
              </Col>
            </Row>
            <Row>
              <Button @click="create" type="primary" size="large">{{L('Thêm mới')}}</Button>
              <Button
                icon="ios-search"
                type="primary"
                size="large"
                @click="getPage"
                class="toolbar-btn"
              >{{L('Tìm kiếm')}}</Button>
            </Row>
          </Form>
          <div class="margin-top-10">
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

class PageCompanyRequest extends PageRequest {
  keyword: string = "";
}

@Component({
  components: { CreateOrEditCompany }
})
export default class Companies extends AbpBase {
  pageRequest: PageCompanyRequest = new PageCompanyRequest();
  edit() {
    this.$router.push({ name: PathNames.UpdateCompany });
  }

  async create() {
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
  async getAllHashtags() {
    await this.$store.dispatch({
      type: "hashtag/getAllHashtags",
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
  get CompanyById() {
    return this.$store.state.company.companyById;
  }
  columns = [
    {
      title: this.L("Tiêu đề bài viết"),
      key: "title",
      width: 200,
    },
    {
      title: this.L("Ngày đăng"),
      key: "creationTime",
      width: 150,
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.creationTime).toLocaleString());
      },
    },
    {
      title: this.L("Mô tả bài viết"),
      key: "description",
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
              },
              on: {
                click: async () => {
                  await this.getCompanyById(params.row.id);
                  await this.getAllHashtags();
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
                        data: params.row,
                      });
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