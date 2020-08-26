<template>
  <div>
    <Card dis-hover>
      <div class="page-body">
        <Form ref="queryForm" :label-width="80" label-position="left" inline>
          <Row :gutter="16">
            <Col span="8">
              <FormItem :label="L('Từ khóa')+':'" style="width:100%">
                <Input v-model="pageRequest.keyWord" :placeholder="L('Tên hashtag')" />
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
          <!-- <Table
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
          ></Page>-->
        </div>
      </div>
    </Card>
    <create-or-edit-hashtag v-model="createOrEditModalShow" @save-success="saveSuccess" />
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
class PageNationalitiesRequest extends PageRequest {}

@Component({
  components: {},
})
export default class Nationalities extends AbpBase {
  pageRequest = new PageNationalitiesRequest();
}
</script>