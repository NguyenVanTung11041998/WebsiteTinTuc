<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="100" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Từ khóa') + ':'" style="width:100%">
                <Input
                  v-model="pageRequest.keyword"
                  :placeholder="L('Tên ứng viên')"
                  @on-enter="getPage"
                />
              </FormItem>
            </Col>
            <Col span="10">
              <FormItem :label="L('CreationTime') + ':'" style="width:100%">
                <DatePicker
                  v-model="creationTime"
                  type="datetimerange"
                  format="yyyy-MM-dd"
                  style="width: 100%; margin-left: 20px"
                  placement="bottom-end"
                  :placeholder="L('Chọn ngày')"
                />
              </FormItem>
            </Col>
          </Row>
          <Row>
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
    <view-cV v-model="viewModalShow" />
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import PathNames from "../../../store/constants/path-names";
import { Guid } from "guid-typescript";
import ViewCV from "./view-cv.vue";
import CV from "../../../store/entities/cv";

class PageRecruitmentRequest {
  keyword: string = "";
}

@Component({
  components: { ViewCV },
})
export default class Recruitments extends AbpBase {
  pageRequest: PageRecruitmentRequest = new PageRecruitmentRequest();
  private creationTime: Date[] = [];
  viewModalShow = false;

  pageChange(page: number) {
    this.$store.commit("recruitment/setCurrentPage", page);
    this.getPage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("recruitment/setPageSize", pagesize);
    this.getPage();
  }

  async getPage() {
    let from: Date = null;
    let to: Date = null;

    if (this.creationTime.length > 0) {
      from = this.creationTime[0];
    }
    if (this.creationTime.length > 1) {
      to = this.creationTime[1];
    }

    let param = {
      currentPage: this.currentPage,
      pageSize: this.pageSize,
      searchText: this.pageRequest.keyword,
      startDate: from,
      endDate: to,
      postId: this.$route.params.id,
    };

    await this.$store.dispatch({
      type: "recruitment/getAllPaging",
      data: param,
    });
  }

  get list() {
    return this.$store.state.recruitment.list;
  }
  get loading() {
    return this.$store.state.recruitment.loading;
  }
  get pageSize() {
    return this.$store.state.recruitment.pageSize;
  }
  get totalCount() {
    return this.$store.state.recruitment.totalCount;
  }
  get currentPage() {
    return this.$store.state.recruitment.currentPage;
  }

  columns = [
    {
      title: this.L("Họ tên ứng viên"),
      key: "name",
      width: 200,
    },
    {
      title: this.L("Ngày ứng tuyển"),
      key: "creationTime",
      width: 180,
      render: (h: any, params: any) => {
        return h("span", new Date(params.row.creationTime).toLocaleString());
      },
    },
    {
      title: this.L("Số điện thoại"),
      key: "phoneNumber",
      width: 130,
    },
    {
      title: this.L("Email"),
      key: "email",
      width: 230
    },
    {
      title: this.L("Trạng thái"),
      key: "isRead",
      render: (h: any, params: any) => {
        return h("span", params.row.isRead ? "Đã xem" : "Chưa xem");
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
              },
              on: {
                click: async () => {
                  const data = { ...params.row } as CV;
                  if (!data.isRead) {
                    await this.$store.dispatch({
                      type: "recruitment/readCV",
                      id: data.id,
                    });
                    params.row.isRead = true;
                  }
                  this.$store.commit("recruitment/setCV", data);
                  this.viewCV();
                },
              },
            },
            this.L("Xem")
          ),
        ]);
      },
    },
  ];

  viewCV() {
    this.viewModalShow = true;
  }

  async created() {
    await this.getPage();
  }
}
</script>
