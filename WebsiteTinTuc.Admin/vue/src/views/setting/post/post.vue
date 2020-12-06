<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Tiêu đề') + ':'" style="width:100%">
                <Input
                  v-model="pageRequest.keyword"
                  :placeholder="L('Tên bài viết')"
                  @on-enter="getPage"
                />
              </FormItem>
            </Col>
            <Col span="8">
              <FormItem :label="L('Tên công ty') + ':'" style="width:100%">
                <Input
                  v-model="pageRequest.companyName"
                  :placeholder="L('Tên công ty')"
                  @on-enter="getPage"
                />
              </FormItem>
            </Col>
          </Row>
          <Row :gutter="24">
            <Col span="8">
              <FormItem :label="L('Ngày bắt đầu')" style="width:100%">
                <DatePicker
                  v-model="pageRequest.startDate"
                  format="dd-MM-yyyy"
                  :placeholder="L('Ngày bắt đầu')"
                  @on-enter="getPage"
                />
              </FormItem>
            </Col>
            <Col span="8">
              <FormItem :label="L('Ngày kết thúc')" style="width:100%">
                <DatePicker
                  v-model="pageRequest.endDate"
                  format="dd-MM-yyyy"
                  :placeholder="L('Ngày kết thúc')"
                  @on-enter="getPage"
                />
              </FormItem>
            </Col>
            <Col span="8">
              <FormItem :label="L('Loại')" style="width:100%">
                <Select v-model="pageRequest.jobType">
                  <Option :value="-1">Tất cả</Option>
                  <Option :value="0">Part time</Option>
                  <Option :value="1">Full time</Option>
                </Select>
              </FormItem>
            </Col>
          </Row>
          <Row>
            <Button @click="create" type="primary" size="large">
              {{ L("Thêm mới") }}
            </Button>
            <Button
              icon="ios-search"
              type="primary"
              size="large"
              @click="getPage"
              class="toolbar-btn"
              >{{ L("Tìm kiếm") }}</Button
            >
          </Row>
          <div style="margin-top: 10px;">
            <span>Có {{ totalCount }} bản ghi</span>
          </div>
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
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PageRequest from "../../../store/entities/page-request";
import Post from "../../../store/entities/post";
import PathNames from "../../../store/constants/path-names";
import { JobType } from "../../../store/enums/job-type";
import { Guid } from "guid-typescript";

class PagePostRequest extends PageRequest {
  keyword: string = "";
  companyName: string = "";
  startDate?: Date = null;
  endDate?: Date = null;
  jobType?: JobType = -1;
}

@Component({
  components: {},
})
export default class Posts extends AbpBase {
  pageRequest: PagePostRequest = new PagePostRequest();

  edit(id: string) {
    this.$router.push({ name: PathNames.UpdatePost, params: { id } });
  }

  create() {
    this.$router.push({ name: PathNames.CreatePost });
  }

  pageChange(page: number) {
    this.$store.commit("post/setCurrentPage", page);
    this.getPage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("post/setPageSize", pagesize);
    this.getPage();
  }

  async getPostById(id) {
    await this.$store.dispatch({
      type: "post/getPostById",
      id: id,
    });
  }

  async getAllHashtags() {
    await this.$store.dispatch({
      type: "post/getAllHashtags",
    });
  }

  async getPage() {
    if (
      this.pageRequest.startDate &&
      this.pageRequest.endDate &&
      this.pageRequest.startDate > this.pageRequest.endDate
    ) {
      this.$Message.error("Ngày bắt đầu phải nhỏ hơn ngày kết thúc");
      return;
    }

    let param = {
      currentPage: this.currentPage,
      pageSize: this.pageSize,
      searchText: this.pageRequest.keyword,
      companyName: this.pageRequest.companyName,
      startDate: this.pageRequest.startDate,
      endDate: this.pageRequest.endDate,
      jobType: this.pageRequest.jobType != -1 ? this.pageRequest.jobType : null
    };

    await this.$store.dispatch({
      type: "post/getAll",
      data: param,
    });
  }

  get list() {
    return this.$store.state.post.list;
  }
  get loading() {
    return this.$store.state.post.loading;
  }
  get pageSize() {
    return this.$store.state.post.pageSize;
  }
  get totalCount() {
    return this.$store.state.post.totalCount;
  }
  get currentPage() {
    return this.$store.state.post.currentPage;
  }
  get postById() {
    return this.$store.state.post.postById;
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
      title: this.L("Tên công ty"),
      key: "companyName",
    },
    {
      title: this.L("Ngày hết hạn"),
      key: "endDate",
      render: (h: any, params: any) => {
        return h(
          "span",
          params.row.endDate
            ? `${new Date(params.row.endDate).toLocaleDateString()}`
            : ""
        );
      },
    },
    {
      title: this.L("Thao tác"),
      key: "Actions",
      width: 250,
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
              on: {
                click: async () => {
                  this.$Modal.confirm({
                    title: this.L("Thông báo"),
                    content: this.L("Xóa bài đăng"),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "post/delete",
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
          h(
            "Button",
            {
              props: {
                type: "success",
                size: "small",
              },
              style: {
                marginLeft: "5px",
              },
              on: {
                click: () => {
                  this.viewCv(params.row.id);
                },
              },
            },
            this.L("Xem ứng tuyển")
          ),
        ]);
      },
    },
  ];

  viewCv(postId: string) {
    this.$router.push({
      name: PathNames.Recruitment,
      params: { id: postId },
    });
  }

  async created() {
    await this.getPage();
  }
}
</script>
