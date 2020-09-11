<template>
  <div>
    <h1>Thêm mới / Chỉnh sửa</h1>
    <br />
    <Form ref="postForm" label-position="top" :rules="postRule" :model="post">
      <Row :gutter="24">
        <Col span="12">
          <FormItem :label="L('Tiêu đề bài viết:')" prop="title">
            <Input
              v-model="post.title"
              placeholder="Nhập tiêu đề..."
              :maxlength="300"
              :minlength="2"
            />
          </FormItem>
        </Col>
      </Row>
      <Row :gutter="24">
        <Col span="8">
          <FormItem :label="L('Yêu cầu:')">
            <RadioGroup v-model="post.jobType">
              <Radio :label="0">Part time</Radio>
              <Radio :label="1">Full time</Radio>
            </RadioGroup>
          </FormItem>
        </Col>
        <Col span="8">
          <FormItem :label="L('Kinh nghiệm:')">
            <RadioGroup v-model="post.experienceType">
              <Radio :label="0">Không yêu cầu</Radio>
              <Radio :label="1">Số tháng</Radio>
              <Radio :label="2">Số năm</Radio>
            </RadioGroup>
          </FormItem>
        </Col>
        <Col span="8" v-if="post.experienceType">
          <FormItem :label="L('Thời gian:')">
            <InputNumber
              style="width: 20%;"
              placeholder="Nhập số..."
              :min="0"
              :step="1"
              :editable="true"
              v-model="post.timeExperience"
            />
          </FormItem>
        </Col>
      </Row>
      <Row :gutter="24">
        <Col span="4">
          <FormItem :label="L('Có thời hạn ứng tuyển:')">
            <RadioGroup v-model="isEndDate">
              <Radio :label="0">Không</Radio>
              <Radio :label="1">Có</Radio>
            </RadioGroup>
          </FormItem>
        </Col>
        <Col span="8" v-if="isEndDate">
          <FormItem :label="L('Ngày kết thúc:')">
            <DatePicker
              type="date"
              v-model="post.endDate"
              placeholder="Chọn ngày"
              style="width: 200px"
            />
          </FormItem>
        </Col>
      </Row>
      <Row :gutter="24">
        <Col span="4">
          <FormItem :label="L('Có thể thương lượng:')">
            <RadioGroup v-model="post.moneyType">
              <Radio :label="0">Không</Radio>
              <Radio :label="1">Có</Radio>
            </RadioGroup>
          </FormItem>
        </Col>
        <Col span="8" v-if="post.moneyType == 0">
          <FormItem :label="L('Tối thiểu (VND):')">
            <InputNumber
              style="width: 75%;"
              placeholder="Nhập số..."
              :min="0"
              :step="1000"
              :editable="true"
              v-model="post.minMoney"
            />
          </FormItem>
        </Col>
        <Col span="8" v-if="post.moneyType == 0">
          <FormItem :label="L('Tối đa (VND):')">
            <InputNumber
              style="width: 75%;"
              placeholder="Nhập số..."
              :min="0"
              :step="1000"
              :editable="true"
              v-model="post.maxMoney"
            />
          </FormItem>
        </Col>
      </Row>
      <Row :gutter="24">
        <Col span="8">
          <FormItem :label="L('Kỹ năng')">
            <Select
              v-model="hashtagOfPost"
              multiple
              v-if="post || hashtags"
              class="select-cate"
            >
              <Option
                v-for="(item, index) in hashtags"
                :value="item.id"
                :key="index"
                >{{ item.name }}
              </Option>
            </Select>
          </FormItem>
        </Col>
        <Col span="8">
          <FormItem :label="L('Cấp độ')">
            <Select v-model="post.levelId" v-if="levels" class="select-cate">
              <Option
                v-for="(item, index) in levels"
                :value="item.id"
                :key="index"
                >{{ item.name }}
              </Option>
            </Select>
          </FormItem>
        </Col>
        <Col span="8">
          <FormItem :label="L('Công ty')">
            <Select
              v-model="post.companyId"
              v-if="companies"
              class="select-cate"
            >
              <Option
                v-for="(item, index) in companies"
                :value="item.id"
                :key="index"
                >{{ item.name }}
              </Option>
            </Select>
          </FormItem>
        </Col>
      </Row>
      <div class="row">
        <div class="col-12">
          <FormItem :label="L('Nội dung')">
            <editor
              api-key="no-api-key"
              v-model="post.content"
              :init="settingPlugin"
            />
          </FormItem>
        </div>
      </div>
      <div class="row footer">
        <Button @click="cancel">{{ L("Cancel") }}</Button>
        <Button @click="save" type="primary">{{ L("OK") }}</Button>
      </div>
    </Form>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Post from "../../../store/entities/post";
import { isEqual, forEach, map, clone } from "lodash";
import IObjectFile from "../../../store/interfaces/IObjectFile";
import { Action, Getter, namespace } from "vuex-class";
import PageRequest from "../../../store/entities/page-request";
import Editor from "@tinymce/tinymce-vue";
import { FileType } from "../../../store/enums/file-type";
import Hashtag from "../../../store/entities/hashtag";
import PathNames from "../../../store/constants/path-names";
import { MoneyType } from "../../../store/enums/money-type";
import { JobType } from "../../../store/enums/job-type";
import { ExperienceType } from "../../../store/enums/experience-type";

@Component({
  components: { Editor },
})
export default class UpdatePost extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  public postDefault = {
    title: "",
    content: "",
    experienceType: ExperienceType.NoExperience,
    jobType: JobType.PartTime,
    minMoney: 0,
    maxMoney: 0,
    id: null,
    timeExperience: 0,
    endDate: null,
    moneyType: MoneyType.Negotiable,
  } as Post;

  public post = new Post();
  public isEndDate = 0;
  public hashtagOfPost: string[] = [];
  public settingPlugin = {
    height: 500,
    menubar: true,
    plugins: [
      "advlist autolink lists link image charmap print preview anchor",
      "searchreplace visualblocks code fullscreen",
      "insertdatetime media table paste code help wordcount",
    ],
    toolbar1:
      "undo redo | formatselect | bold italic backcolor| image media | \
                                        charmap | table | Link | alignleft aligncenter alignright alignjustify | \
                                        bullist numlist outdent indent ",
    toolbar2:
      "removeformat | paste code | searchreplace | wordcount | fullscreen | help",
    automatic_upload: true,
    image_title: true,
    paste_data_images: true,
    images_upload_url: "/api/services/app/Post/UploadImage",
    images_upload_handler: this.saveImage,
  };

  async created() {
    this.post = { ...this.postDefault };
    const id = this.$route.params.id;
    if (id) {
      this.post = await this.$store.dispatch({
        type: "post/getPostById",
        id: id,
      });
      this.hashtagOfPost = map(this.post.hashtags, "hashtagId");
    }
    await this.getAllHashtags();
    await this.getAllLevels();
    await this.getAllCompanies();
  }

  async getAllHashtags() {
    await this.$store.dispatch({
      type: "hashtag/getAllHashtags",
    });
  }

  async getAllCompanies() {
    await this.$store.dispatch({
      type: "company/getAllCompanies",
    });
  }

  async getAllLevels() {
    await this.$store.dispatch({
      type: "level/getAllLevels",
    });
  }

  get companies() {
    return this.$store.state.company.companies;
  }

  get hashtags() {
    return this.$store.state.hashtag.hashtags;
  }

  get levels() {
    return this.$store.state.level.levels;
  }

  get imageUrl() {
    return this.$store.state.post.imageUrl;
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

  async save() {
    if (this.post.title && this.post.content) {
      const hashtagIdDeletes = map(this.post.hashtags, "id");
      const post = {
        title: this.post.title,
        content: this.post.content,
        experienceType: this.post.experienceType,
        companyId: this.post.companyId,
        levelId: this.post.levelId,
        hashtagIds: this.hashtagOfPost,
        jobType: this.post.jobType,
        minMoney: this.post.minMoney,
        maxMoney: this.post.maxMoney,
        id: this.post.id,
        endDate: this.post.endDate,
        moneyType: this.post.moneyType,
        timeExperience: this.post.timeExperience,
        hashtagIdDeletes: hashtagIdDeletes
      };
      await this.$store.dispatch({
        type: `post/${this.post.id ? "update" : "create"}`,
        data: post,
      });

      (this.$refs.postForm as any).resetFields();
      this.$emit("save-success");
      this.$emit("input", false);
      this.$Message.success(
        `${this.post.id ? "Sửa thành công" : "Thêm thành công"}`
      );
      this.$router.push({ name: PathNames.Post });
    }
  }

  cancel() {
    (this.$refs.postForm as any).resetFields();
    this.$emit("input", false);
    this.$router.push({ name: PathNames.Post });
  }

  getLinkPath(fileObject: IObjectFile) {
    return fileObject.path
      ? Util.getLinkPath(fileObject.path)
      : window.URL.createObjectURL(fileObject.file);
  }

  async saveImage(blobInfo, success) {
    const requestData = new FormData();
    requestData.append("file", blobInfo.blob());
    await this.$store.dispatch({
      type: "post/uploadImage",
      data: requestData,
    });

    success(Util.getLinkPath(this.imageUrl));
  }

  postRule = {
    title: [
      {
        required: true,
        message: this.L("This field is required", undefined, this.L("name")),
        trigger: "blur",
      },
    ],
    content: [
      {
        required: true,
        message: this.L("This field is required", undefined, this.L("content")),
        trigger: "blur",
      },
    ],
  };
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
  img {
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
    &:hover {
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
  input {
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
    &:hover {
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
