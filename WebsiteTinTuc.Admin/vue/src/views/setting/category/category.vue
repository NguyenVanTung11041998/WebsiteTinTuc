<template>
    <div>
        <Card dis-hover>
            <div class="page-body">
                <Form ref="queryForm" :label-width="80" label-position="left" inline>
                    <Row :gutter="16">
                        <Col span="6">
                        <FormItem :label="L('Keyword')+':'" style="width:100%">
                            <Input v-model="pageRequest.keyword" :placeholder="L('Tên danh mục')" />
                        </FormItem>
                        </Col>
                    </Row>
                    <Row>
                        <Button @click="create" icon="android-add" type="primary" size="large">{{L('Thêm mới')}}</Button>
                        <Button icon="ios-search"
                                type="primary"
                                size="large"
                                @click="getPage"
                                class="toolbar-btn">
                            {{L('Find')}}
                        </Button>
                    </Row>
                </Form>
                <div class="margin-top-10">
                    <Table :loading="loading"
                           :columns="columns"
                           :no-data-text="L('Không có dữ liệu')"
                           border
                           :data="list"></Table>
                    <Page show-sizer
                          class-name="fengpage"
                          :total="totalCount"
                          class="margin-top-10"
                          @on-change="pageChange"
                          @on-page-size-change="pagesizeChange"
                          :page-size="pageSize"
                          :current="currentPage"></Page>
                </div>
            </div>
        </Card>
        <create-or-edit-category v-model="createOrEditModalShow" @save-success="saveSuccess" />
    </div>
</template>
<script lang="ts">
    import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
    import Util from "@/lib/util";
    import AbpBase from "@/lib/abpbase";
    import PageRequest from "@/store/entities/page-request";
    import CreateOrEditCategory from "./create-or-edit-category.vue";
    import Category from "../../../store/entities/category";
    class PageCategoryRequest extends PageRequest {
        keyword: string;
    }

    @Component({
        components: { CreateOrEditCategory }
    })
    export default class Categories extends AbpBase {
        edit() {
            this.createOrEditModalShow = true;
        }
        //filters
        pageRequest: PageCategoryRequest = new PageCategoryRequest();

        createOrEditModalShow: boolean = false;
        get list() {
            return this.$store.state.category.list;
        }
        get loading() {
            return this.$store.state.category.loading;
        }

        create() {
            const category = { name: "" } as Category;
            this.$store.commit("category/setCategory", category);
            this.createOrEditModalShow = true;
        }

        pageChange(page: number) {
            this.$store.commit("category/setCurrentPage", page);
            this.getPage();
        }

        pagesizeChange(pagesize: number) {
            this.$store.commit("category/setPageSize", pagesize);
            this.getPage();
        }

        async saveSuccess() {
            await this.getPage();
        }

        async getPage() {
            let param = {
                searchText: this.pageRequest.keyword,
                currentPage: this.currentPage,
                pageSize: this.pageSize
            };
            await this.$store.dispatch({
                type: "category/getAll",
                data: param
            });
        }
        get pageSize() {
            return this.$store.state.category.pageSize;
        }
        get totalCount() {
            return this.$store.state.category.totalCount;
        }
        get currentPage() {
            return this.$store.state.category.currentPage;
        }
        columns = [
            {
                title: this.L("Tên danh mục"),
                key: "name"
            },
            {
                title: this.L("Thời gian tạo"),
                key: "creationTime",
                render: (h: any, params: any) => {
                    return h("span", new Date(params.row.creationTime).toLocaleString());
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
                                    size: "small"
                                },
                                style: {
                                    marginRight: "5px"
                                },
                                on: {
                                    click: () => {
                                        const category = { ...params.row };
                                        this.$store.commit("category/setCategory", category);
                                        this.edit();
                                    }
                                }
                            },
                            this.L("Sửa")
                        ),
                        h(
                            "Button",
                            {
                                props: {
                                    type: "error",
                                    size: "small"
                                },
                                on: {
                                    click: async () => {
                                        this.$Modal.confirm({
                                            title: this.L("Thông báo"),
                                            content: this.L(`Xóa danh mục ${params.row.name}`),
                                            okText: this.L("Yes"),
                                            cancelText: this.L("No"),
                                            onOk: async () => {
                                                await this.$store.dispatch({
                                                    type: "category/delete",
                                                    data: params.row.id
                                                });
                                                await this.getPage();
                                            }
                                        });
                                    }
                                }
                            },
                            this.L("Xóa")
                        )
                    ]);
                }
            }
        ];
        async created() {
            await this.getPage();
        }
    }
</script>