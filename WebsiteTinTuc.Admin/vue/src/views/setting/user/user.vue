<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <Col span="6">
              <FormItem :label="L('Keyword') + ':'" style="width:100%">
                <Input
                  v-model="pagerequest.keyword"
                  :placeholder="L('UserName') + '/' + L('Name')"
                />
              </FormItem>
            </Col>
            <Col span="6">
              <FormItem :label="L('IsActive') + ':'" style="width:100%">
                <!--Select should not set :value="'All'" it may not trigger on-change when first select 'NoActive'(or 'Actived') then select 'All'-->
                <Select :placeholder="L('Select')" @on-change="isActiveChange">
                  <Option value="All">{{ L("Tất cả") }}</Option>
                  <Option value="Actived">{{ L("Hoạt động") }}</Option>
                  <Option value="NoActive">{{ L("Không hoạt động") }}</Option>
                </Select>
              </FormItem>
            </Col>
          </Row>
          <Row>
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
              @click="create"
              icon="android-add"
              type="primary"
              size="large"
              >{{ L("Add") }}</Button
            >
            <Button
              icon="ios-search"
              type="primary"
              size="large"
              @click="getpage"
              class="toolbar-btn"
              >{{ L("Find") }}</Button
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
    <create-user v-model="createModalShow" @save-success="getpage" />
    <edit-user v-model="editModalShow" @save-success="getpage" />
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "@/lib/util";
import AbpBase from "@/lib/abpbase";
import PageRequest from "@/store/entities/page-request";
import CreateUser from "./create-user.vue";
import EditUser from "./edit-user.vue";
class PageUserRequest extends PageRequest {
  keyword: string;
  isActive: boolean = null; //nullable
  from: Date;
  to: Date;
}

@Component({
  components: { CreateUser, EditUser },
})
export default class Users extends AbpBase {
  edit() {
    this.editModalShow = true;
  }
  //filters
  pagerequest: PageUserRequest = new PageUserRequest();
  creationTime: Date[] = [];

  createModalShow: boolean = false;
  editModalShow: boolean = false;
  get list() {
    return this.$store.state.user.list;
  }
  get loading() {
    return this.$store.state.user.loading;
  }
  create() {
    this.createModalShow = true;
  }
  isActiveChange(val: string) {
    console.log(val);
    if (val === "Actived") {
      this.pagerequest.isActive = true;
    } else if (val === "NoActive") {
      this.pagerequest.isActive = false;
    } else {
      this.pagerequest.isActive = null;
    }
  }
  pageChange(page: number) {
    this.$store.commit("user/setCurrentPage", page);
    this.getpage();
  }
  pagesizeChange(pagesize: number) {
    this.$store.commit("user/setPageSize", pagesize);
    this.getpage();
  }
  async getpage() {
    this.pagerequest.maxResultCount = this.pageSize;
    this.pagerequest.skipCount = (this.currentPage - 1) * this.pageSize;
    //filters

    if (this.creationTime.length > 0) {
      this.pagerequest.from = this.creationTime[0];
    }
    if (this.creationTime.length > 1) {
      this.pagerequest.to = this.creationTime[1];
    }

    await this.$store.dispatch({
      type: "user/getAll",
      data: this.pagerequest,
    });
  }
  get pageSize() {
    return this.$store.state.user.pageSize;
  }
  get totalCount() {
    return this.$store.state.user.totalCount;
  }
  get currentPage() {
    return this.$store.state.user.currentPage;
  }
  columns = [
    {
      title: this.L("UserName"),
      key: "userName",
    },
    {
      title: this.L("Name"),
      key: "name",
    },
    {
      title: this.L("IsActive"),
      render: (h: any, params: any) => {
        return h("span", params.row.isActive ? this.L("Yes") : this.L("No"));
      },
    },
    {
      title: this.L("CreationTime"),
      key: "creationTime",
      render: (h: any, params: any) => {
        return h(
          "span",
          new Date(params.row.creationTime).toLocaleDateString()
        );
      },
    },
    {
      title: this.L("Actions"),
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
                click: () => {
                  this.$store.commit("user/edit", params.row);
                  this.edit();
                },
              },
            },
            this.L("Edit")
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
                    content: this.L("Xóa người dùng"),
                    okText: this.L("Yes"),
                    cancelText: this.L("No"),
                    onOk: async () => {
                      await this.$store.dispatch({
                        type: "user/delete",
                        data: params.row,
                      });
                      await this.getpage();
                    },
                  });
                },
              },
            },
            this.L("Delete")
          ),
        ]);
      },
    },
  ];
  async created() {
    this.getpage();
    await this.$store.dispatch({
      type: "user/getRoles",
    });
  }
}
</script>
