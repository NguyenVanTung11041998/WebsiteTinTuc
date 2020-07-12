<template>
    <div>
        <div v-if="!updateModalShow">
            <Card dis-hover>
                <div class="page-body">
                    <Form ref="queryForm" :label-width="100" label-position="left" inline>
                        <Row :gutter="16">
                            <Col span="8">
                                <FormItem :label="L('Từ khóa')+':'" style="width:100%">
                                    <Input v-model="pageRequest.keyword" :placeholder="L('Tên bài viết')" @on-enter="getPage" />
                                </FormItem>
                            </Col>
                        </Row>
                        <Row>
                            <Button @click="create" type="primary" size="large">{{L('Thêm mới')}}</Button>
                            <Button icon="ios-search" type="primary" size="large" @click="getPage" class="toolbar-btn">{{L('Tìm kiếm')}}</Button>
                        </Row>
                    </Form>
                    <div class="margin-top-10">
                        <Table :loading="loading" :columns="columns" :no-data-text="L('Không có dữ liệu')" border :data="list">
                        </Table>
                        <Page show-sizer class-name="fengpage" :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage"></Page>
                    </div>
                </div>
            </Card>
        </div>
        <div v-if="updateModalShow"><update-post v-model="updateModalShow" @save-success="getPage" /></div>
    </div>
</template>
<script lang="ts">
    import { Component, Vue, Inject, Prop, Watch } from 'vue-property-decorator';
    import Util from '../../../lib/util';
    import AbpBase from '../../../lib/abpbase';
    import PageRequest from '../../../store/entities/page-request';
    import UpdatePost from './update-post.vue';
    import Post from "../../../store/entities/post";

    class PagePostRequest extends PageRequest {
        keyword: string = '';
    }

    @Component({
        components: { UpdatePost }
    })
    export default class Posts extends AbpBase {
        pageRequest: PagePostRequest = new PagePostRequest();
        updateModalShow: boolean = false;

        edit() {
            this.updateModalShow = true;
        }

        async create() {
            let post = { objectFile: null } as Post;
            this.$store.commit("post/setPost", post);
            await this.getAllCategories();
            await this.getAllHashtags();
            this.edit();
        }
        pageChange(page: number) {
            this.$store.commit('post/setCurrentPage', page);
            this.getPage();
        }
        pagesizeChange(pagesize: number) {
            this.$store.commit('post/setPageSize', pagesize);
            this.getPage();
        }

        async getPostById(id) {
            await this.$store.dispatch({
                type: "post/getPostById",
                id: id
            })
        }

        async getAllHashtags() {
            await this.$store.dispatch({
                type: "post/getAllHashtags"
            });
        }

        async getPage() {
            let param = {
                currentPage: this.currentPage,
                pageSize: this.pageSize,
                searchText: this.pageRequest.keyword
            }

            await this.$store.dispatch({
                type: 'post/getAll',
                data: param
            });
        }
        async getAllCategories() {
            await this.$store.dispatch({
                type: "post/getAllCategories"
            });
        }
        get list() {
            return this.$store.state.post.list;
        };
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
                title: this.L('Tiêu đề bài viết'),
                key: 'title',
                width: 200
            },
            {
                title: this.L('Ngày đăng'),
                key: 'creationTime',
                width: 150
            },
            {
                title: this.L('Mô tả bài viết'),
                key: 'description'
            },
            {
                title: this.L('Số lượt xem'),
                key: 'numberOfViews',
                width: 130
            },
            {
                title: this.L('Số lượt thích'),
                key: 'numberOfLikes',
                width: 150
            },
            {
                title: this.L('Thao tác'),
                key: 'Actions',
                width: 150,
                render: (h: any, params: any) => {
                    return h('div', [
                        h('Button', {
                            props: {
                                type: 'primary',
                                size: 'small'
                            },
                            style: {
                                marginRight: '5px'
                            },
                            on: {
                                click: async () => {
                                    await this.getPostById(params.row.id);
                                    await this.getAllCategories();
                                    await this.getAllHashtags();
                                    this.edit();
                                }
                            }
                        }, this.L('Sửa')),
                        h('Button', {
                            props: {
                                type: 'error',
                                size: 'small'
                            },
                            on: {
                                click: async () => {
                                    this.$Modal.confirm({
                                        title: this.L('Tips'),
                                        content: this.L('DeletePostConfirm'),
                                        okText: this.L('Yes'),
                                        cancelText: this.L('No'),
                                        onOk: async () => {
                                            await this.$store.dispatch({
                                                type: 'post/delete',
                                                data: params.row
                                            });
                                            await this.getPage();
                                        }
                                    })
                                }
                            }
                        }, this.L('Xóa'))
                    ]);
                }
            }
        ]

        async created() {
            await this.getPage();
        }
    }
</script>