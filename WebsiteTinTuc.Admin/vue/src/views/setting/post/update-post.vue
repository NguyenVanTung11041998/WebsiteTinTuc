<template>
    <div>
        <h1>Thêm mới / Chỉnh sửa</h1><br />
        <Form ref="postForm" label-position="top" :rules="postRule" :model="post">
            <div class="row">
                <div class="col-6">
                    <FormItem :label="L('Tiêu đề bài viết:')" prop="title">
                        <Input v-model="post.title" placeholder="Nhập tiêu đề..." :maxlength="300" :minlength="2" />
                    </FormItem>
                    <FormItem :label="L('Mô tả bài viết:')" prop="description">
                        <Input v-model="post.description" type="textarea" :rows="3" placeholder="Nhập mô tả..." :maxlength="230" :minlength="175" />
                    </FormItem>
                    <div>
                        <span>Sự kiện:</span>
                        <Checkbox v-model="post.isEvent" style="margin-left: 10px;" label=""></Checkbox>
                    </div>
                </div>
                <div class="col-6">
                    <FormItem :label="L('Thể loại:')">
                        <Select v-model="postCategories" multiple v-if="post || allCategories" class="select-cate">
                            <Option v-for="(item, index) in allCategories" :value="item.id" :key="index">{{ item.name }}</Option>
                        </Select>
                    </FormItem>
                    <FormItem :label="L('Hashtag')">
                        <Select v-model="hashtags" multiple v-if="post || allHashtags" class="select-cate">
                            <Option v-for="(item, index) in allHashtags" :value="item.id" :key="index">{{ item.name }}</Option>
                        </Select>
                    </FormItem>
                    <FormItem :label="L('Ảnh Thumbnail:')" prop="thumbnail">
                        <div class="image-cover mx-0 mb-2" v-if="post.thumbnail">
                            <img :src="post.thumbnail ? getLinkPath(post.thumbnail) : '#'" alt="Thumbnail" />
                            <span>
                                <img src="../../../assets/x-button.png"
                                     alt
                                     @click="removeImage(post.thumbnail)" />
                            </span>
                        </div>

                        <div class="input-button" :class="{'d-none' : post.thumbnail}">
                            <b-form-file class
                                         id="uploadIcon"
                                         v-model="thumbnailFile"
                                         size="sm"
                                         @change="onChangeThumbnail"
                                         plain></b-form-file>
                            <label for="uploadIcon" class="custom-input">
                                <span></span>
                                <span></span>
                            </label>
                        </div>
                    </FormItem>
                </div>
            </div>
            <div class="row">
                <div class="col-12">
                    <FormItem :label="L('Nội dung')">
                        <editor api-key="no-api-key"
                                v-model="post.content"
                                :init="{
                                height: 500,
                                menubar: true,
                                plugins: [
                                'advlist autolink lists link image charmap print preview anchor',
                                'searchreplace visualblocks code fullscreen',
                                'insertdatetime media table paste code help wordcount'
                                ],
                                toolbar1: 'undo redo | formatselect | bold italic backcolor| image media | \
                                        charmap | table | Link | alignleft aligncenter alignright alignjustify | \
                                        bullist numlist outdent indent ',
                                toolbar2: 'removeformat |  paste code | searchreplace | wordcount | fullscreen | help',
                                automatic_upload: true,
                                image_title: true,
                                paste_data_images: true,
                                images_upload_url: '/api/services/app/Post/UploadImage',
                                images_upload_handler: saveImage
                            }"></editor>
                    </FormItem>
                </div>
            </div>
            <div class="row footer">
                <Button @click="cancel">{{L('Cancel')}}</Button>
                <Button @click="save" type="primary">{{L('OK')}}</Button>
            </div>
        </Form>
    </div>
</template>
<script lang="ts">
    import { Component, Vue, Inject, Prop, Watch } from 'vue-property-decorator';
    import Util from '../../../lib/util';
    import AbpBase from '../../../lib/abpbase';
    import Post from "../../../store/entities/post";
    import { isEqual, forEach, map, clone } from "lodash";
    import IObjectFile from "../../../store/interfaces/IObjectFile";
    import { Action, Getter, namespace } from "vuex-class";
    import PageRequest from "../../../store/entities/page-request";
    import Editor from '@tinymce/tinymce-vue';

    class PagePostRequest extends PageRequest {
        keyword: string = "";
    }
    @Component({
        components: { Editor }
    })
    export default class UpdatePost extends AbpBase {
        @Prop({ type: Boolean, default: false }) value: boolean;
        @Getter("allCategories", { namespace: "post" }) public allCategories!: any;
        @Getter("post", { namespace: "post" }) public post!: any;
        @Getter("allHashtags", { namespace: "post" }) public allHashtags!: any;

        public deleteFiles: any = [];
        public thumbnailFile: File = null;
        public assetsFile: File[] = [];
        public postCategories: any = [];
        public hashtags: any = [];
        pageRequest: PagePostRequest = new PagePostRequest();

        created() {
            if (this.post && this.post.categories) {
                this.postCategories = map(this.post.categories, 'id');
                this.hashtags = map(this.post.hashtags, 'id');
            }
        }

        get imageUrl() {
            return this.$store.state.post.imageUrl;
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

        async getPage() {
            await this.$store.dispatch({
                type: 'post/getPosts',
                data: this.pageRequest
            });
        }

        saveImage(blobInfo, success) {
            const requestData = new FormData();
            requestData.append('file', blobInfo.blob());
            this.$store.dispatch({
                type: 'post/uploadImage',
                data: requestData
            });

            success(Util.getLinkPath(this.imageUrl));
        }

        removeImage(file: IObjectFile, index = 0) {
            if (file.fileType == 2) {
                this.post.thumbnail = null;
                this.thumbnailFile = null;
            }
            if (file.fileType == 1) {
                this.deleteFiles.push(file.id);
                this.post.assets = this.post.assets.filter(
                    item => !isEqual(file, item)
                );
                this.assetsFile = this.assetsFile.filter(
                    item => !isEqual(file.file, item)
                );
            }
        }
        resetModel() {
            this.deleteFiles = [];
            this.thumbnailFile = null;
            this.assetsFile = [];
            this.postCategories = [];
        }

        async save() {
            if (this.post.title && this.post.content) {
                const requestData = new FormData();
                if (this.post && this.post.id)
                    requestData.append("id", this.post.id);
                forEach(this.postCategories, (cate: string) => {
                    requestData.append("categories", cate);
                });
                forEach(this.hashtags, (hashtag: string) => {
                    requestData.append("hashtags", hashtag);
                });
                requestData.append("isEvent", this.post.isEvent ? this.post.isEvent : "false");
                requestData.append('title', this.post.title);
                requestData.append('description', this.post.description);
                requestData.append('content', this.post.content);
                if (this.post && this.post.thumbnail) {
                    const thumbnailFile = this.post.thumbnail;
                    if (thumbnailFile) {
                        requestData.append("thumbnail", thumbnailFile.file);
                    }
                }

                forEach(this.deleteFiles, (f: string) => {
                    requestData.append("deleteFiles", f);
                });
                await this.$store.dispatch({
                    type: 'post/updatePost',
                    data: requestData
                });

                (this.$refs.postForm as any).resetFields();
                this.$emit('save-success');
                this.$emit('input', false);
                await this.getPage();
            }
        }

        cancel() {
            (this.$refs.postForm as any).resetFields();
            this.$emit('input', false);
        }

        getLinkPath(fileObject: IObjectFile) {
            return fileObject.path
                ? Util.getLinkPath(fileObject.path)
                : window.URL.createObjectURL(fileObject.file);
        }

        onChangeThumbnail(event: any) {
            this.post.thumbnail = {
                id: "",
                file: event.target.files[0],
                fileType: 2
            };
        }

        onChangeAssets(event: any) {
            this.post.assets = this.post.assets || [];
            for (let i = 0; i < event.target.files.length; i++) {
                this.post.assets.push({
                    id: "",
                    file: event.target.files[i],
                    fileType: 1
                });
            }
        }

        postRule = {
            title: [{ required: true, message: this.L('This field is required', undefined, this.L('name')), trigger: 'blur' }],
            content: [{ required: true, message: this.L('This field is required', undefined, this.L('content')), trigger: 'blur' }],
            description: [{ required: true, message: this.L('This field is required', undefined, this.L('description')), trigger: 'blur' }]
        }
    }
</script>
<style lang="scss">
    .input-content {
        width: 100%;
    }

    .image-cover {
        float: left;
        display: inline-block;
        height: 80px;
        width: 80px;
        overflow: hidden;
        border-radius: 4px;
        margin-right: 20px;
        img

    {
        width: 100%;
        height: 100%;
        object-fit: cover;
        object-position: center;
    }

    span {
        display: inline-block;
        height: 10px;
        width: 10px;
        position: absolute;
        bottom: 100%;
        overflow: hidden;
        transition: 0.2s;
        cursor: pointer;
        &:hover

    {
        transform: scale(1.2);
    }

    img {
        position: absolute;
        bottom: 0;
        left: 0;
        height: 100%;
        width: 100%;
        object-position: center;
        object-fit: cover;
    }

    }
    }

    .input-button {
        display: flex;
        justify-content: center;
        align-items: center;
        width: 80px;
        height: 80px;
        overflow: hidden;
        position: relative;
        border-radius: 4px;
        margin-right: 20px;
        input

    {
        position: absolute;
        z-index: -1;
    }

    .custom-input {
        position: relative;
        width: 90%;
        height: 90%;
        margin: 0;
        cursor: pointer;
        border: 2px dashed #a5a5a5;
        border-radius: 50%;
        transition: 0.1s;
        &:hover

    {
        width: 100%;
        height: 100%;
    }

    span:first-child {
        display: inline-block;
        height: 2px;
        width: 60%;
        background-color: #a5a5a5;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%) rotate(90deg);
    }

    span:last-child {
        display: inline-block;
        height: 2px;
        width: 60%;
        background-color: #a5a5a5;
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
    }

    }
    }

    #uploadIcon {
        display: none;
    }

    #assets {
        display: none;
    }
</style>