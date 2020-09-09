<template>
  <div>
    <h1>{{ !company.id ? "Thêm mới" : "Chỉnh sửa" }}</h1>
    <br />
    <Form ref="companyForm" label-position="top" :rules="companyRule" :model="company">
      <div class="row">
        <Row :gutter="24">
          <Col span="12">
            <FormItem :label="L('Tên công ty:')" prop="name">
              <Input
                v-model="company.name"
                placeholder="Nhập tên công ty..."
                :maxlength="300"
                :minlength="2"
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem :label="L('Tên đầy đủ công ty:')" prop="fullNameCompany">
              <Input
                v-model="company.fullNameCompany"
                placeholder="Nhập tên đầy đủ công ty..."
                :maxlength="300"
                :minlength="2"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="24">
          <Col span="12">
            <FormItem :label="L('Số điện thoại:')" prop="phone">
              <Input
                v-model="company.phone"
                placeholder="Nhập số điện thoại..."
                :maxlength="11"
                :minlength="10"
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem :label="L('Email:')" prop="email">
              <Input
                v-model="company.email"
                placeholder="Nhập Email..."
                type="email"
                :maxlength="100"
                :minlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="24">
          <Col span="12">
            <FormItem :label="L('Địa chỉ:')" prop="location">
              <Input
                v-model="company.location"
                placeholder="Nhập Địa chỉ..."
                :maxlength="100"
                :minlength="10"
              />
            </FormItem>
          </Col>
          <Col span="12">
            <FormItem :label="L('Địa chỉ đầy đủ:')" prop="locationDescription">
              <Input
                v-model="company.locationDescription"
                placeholder="Nhập Địa chỉ đầy đủ..."
                :maxlength="200"
                :minlength="10"
              />
            </FormItem>
          </Col>
        </Row>
        <Row :gutter="24">
          <Col span="4">
            <FormItem :label="L('Số thành viên tối thiểu:')" prop="minScale">
              <InputNumber
                style="width: 75%;"
                placeholder="Min..."
                :min="0"
                :step="1"
                v-model="company.minScale"
              />
            </FormItem>
          </Col>
          <Col span="4">
            <FormItem :label="L('Số thành viên tối đa:')" prop="maxScale">
              <InputNumber
                style="width: 75%;"
                placeholder="Max..."
                :min="0"
                :step="1"
                v-model="company.maxScale"
              />
            </FormItem>
          </Col>
          <Col span="10">
            <FormItem :label="L('Website:')" prop="website">
              <Input
                v-model="company.website"
                placeholder="Nhập tên website..."
                :maxlength="200"
                :minlength="4"
              >
                <Select v-model="httpSelect" slot="prepend" style="width: 80px">
                  <Option :value="0">http://</Option>
                  <Option :value="1">https://</Option>
                </Select>
              </Input>
            </FormItem>
          </Col>
        </Row>

        <Row :gutter="24">
          <Col span="8">
            <FormItem :label="L('Hashtag')">
              <Select
                v-model="hashtagOfCompany"
                multiple
                v-if="company || hashtags"
                class="select-cate"
              >
                <Option
                  v-for="(item, index) in hashtags"
                  :value="item.id"
                  :key="index"
                >{{ item.name }}</Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="8">
            <FormItem :label="L('Ngành nghề')">
              <Select
                v-model="branchJobOfComany"
                multiple
                v-if="company || branchJobs"
                class="select-cate"
              >
                <Option
                  v-for="(item, index) in branchJobs"
                  :value="item.id"
                  :key="index"
                >{{ item.name }}</Option>
              </Select>
            </FormItem>
          </Col>
          <Col span="4">
            <FormItem :label="L('Quốc tịch:')" prop="nationalityId">
              <Select v-model="company.nationalityId" class="select-cate">
                <Option
                  v-for="(item, index) in nationalities"
                  :value="item.id"
                  :key="index"
                >{{ item.name }}</Option>
              </Select>
            </FormItem>
          </Col>
        </Row>
        <FormItem :label="L('Ảnh Thumbnail:')" prop="thumbnail">
          <div class="image-cover mx-0 mb-2" v-if="company.thumbnail">
            <img :src="company.thumbnail ? getLinkPath(company.thumbnail) : '#'" alt="Thumbnail" />
            <span>
              <img src="../../../assets/x-button.png" @click="removeImage(company.thumbnail)" />
            </span>
          </div>
          <div
            class="input-button"
            v-if="!company.thumbnail"
            :class="{'d-none' : company.thumbnail}"
          >
            <b-form-file
              id="uploadIcon"
              v-model="thumbnailFile"
              size="sm"
              @change="onChangeThumbnail"
              plain
            />
            <label for="uploadIcon" class="custom-input">
              <span></span>
              <span></span>
            </label>
          </div>
        </FormItem>
        <FormItem :label="L('Ảnh:')" prop="images">
          <div v-if="company.images && company.images.length > 0">
            <Row :gutter="24">
              <Col span="4" v-for="(item, index) in company.images" :key="index">
                <div class="col-image">
                  <img :src="item ? getLinkPath(item) : '#'" alt="Image" />
                  <span>
                    <div class="button-image">
                      <img src="../../../assets/x-button.png" @click="removeImage(item, index)" />
                    </div>
                  </span>
                </div>
              </Col>
            </Row>
          </div>
          <div class="uploadOtherImages">
            <b-form-file
              id="uploadOtherImages"
              v-model="images"
              size="sm"
              @change="onChangeImage"
              multiple
              plain
            />
            <label for="uploadOtherImages" class="custom-input">
              <span></span>
              <span></span>
            </label>
          </div>
        </FormItem>
      </div>
      <div class="row">
        <div class="col-12">
          <FormItem :label="L('Chế độ đãi ngộ')">
            <editor api-key="no-api-key" v-model="company.treatment" :init="settingEditor" />
          </FormItem>
        </div>
      </div>
      <div class="row">
        <div class="col-12">
          <FormItem :label="L('Mô tả công ty')">
            <editor api-key="no-api-key" v-model="company.description" :init="settingEditor" />
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
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import { isEqual, forEach, map, clone } from "lodash";
import IObjectFile from "../../../store/interfaces/IObjectFile";
import { Action, Getter, namespace } from "vuex-class";
import PageRequest from "../../../store/entities/page-request";
import Editor from "@tinymce/tinymce-vue";
import { FileType } from "../../../store/enums/file-type";
import Hashtag from "../../../store/entities/hashtag";
import Company from "../../../store/entities/company";
import PathNames from "../../../store/constants/path-names";

class PagePostRequest extends PageRequest {
  keyword: string = "";
}
@Component({
  components: { Editor },
})
export default class CreateOrEditCompany extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;

  public deleteFiles: string[] = [];
  public thumbnailFile: File = null;
  public images: File[] = [];
  public hashtagOfCompany: string[] = [];
  public branchJobOfComany: string[] = [];
  public httpSelect = 0;
  public settingEditor = {
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

  defaultCompany = {
    name: "",
    treatment: "",
    nationalityId: null,
    description: "",
    phone: "",
    locationDescription: "",
    fullNameCompany: "",
    id: null,
    hashtags: [],
    email: "",
    location: "",
    minScale: 0,
    maxScale: 1,
    thumbnail: null,
    images: [],
    branchJobCompanies: [],
    website: "",
  } as Company;

  company = new Company();

  async created() {
    this.company = this.defaultCompany;
    const id = this.$route.params.id;
    if (id) {
      this.company = await this.$store.dispatch({
        type: "company/get",
        id: id,
      });
    }

    await this.getAllHashtags();
    await this.getAllNationalities();
    await this.getAllBranchJobs();
    if (this.company && this.company.hashtags) {
      this.hashtagOfCompany = map(this.company.hashtags, "hashtagId");
    }
    if (this.company && this.company.branchJobCompanies) {
      this.branchJobOfComany = map(
        this.company.branchJobCompanies,
        "branchJobId"
      );
    }
  }

  async getCompanyById(id: string) {
    await this.$store.dispatch({
      type: "company/get",
      payload: id,
    });
  }

  get nationalities() {
    return this.$store.state.nationality.nationalities;
  }

  async getAllHashtags() {
    await this.$store.dispatch({
      type: "hashtag/getAllHashtags",
    });
  }

  async getAllNationalities() {
    await this.$store.dispatch({
      type: "nationality/getAllNationality",
    });
  }

  async getAllBranchJobs() {
    await this.$store.dispatch({
      type: "branchJob/getAllBranchJob",
    });
  }

  get imageUrl() {
    return this.$store.state.company.imageUrl;
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

  get hashtags() {
    return this.$store.state.hashtag.hashtags;
  }

  get branchJobs() {
    return this.$store.state.branchJob.branchJobs;
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

  removeImage(file: IObjectFile, index?: number) {
    if (file.fileType == FileType.Thumbnail) {
      this.company.thumbnail = null;
      this.thumbnailFile = null;
      if (file.id) {
        this.deleteFiles.push(file.id);
      }
    } else {
      if (file.id) {
        this.deleteFiles.push(file.id);
      }
      this.company.images.splice(index, 1);
      this.images.splice(index, 1);
    }
  }
  resetModel() {
    this.deleteFiles = [];
    this.thumbnailFile = null;
    this.images = [];
    this.branchJobOfComany = [];
    this.hashtagOfCompany = [];
  }

  async save() {
    if (this.company && this.company.name) {
      const requestData = new FormData();

      if (this.company.id) {
        requestData.append("id", this.company.id);
      }
      requestData.append("name", this.company.name);
      requestData.append("description", this.company.description);
      requestData.append("phone", this.company.phone);
      requestData.append("email", this.company.email);
      requestData.append("fullNameCompany", this.company.fullNameCompany);
      requestData.append(
        "locationDescription",
        this.company.locationDescription
      );
      requestData.append("location", this.company.location);
      requestData.append("website", this.company.website);
      requestData.append("treatment", this.company.treatment);
      requestData.append("nationalityId", this.company.nationalityId);
      if (this.company.maxScale != null) {
        requestData.append("maxScale", `${this.company.maxScale}`);
      }

      if (this.company.minScale != null) {
        requestData.append("minScale", `${this.company.minScale}`);
      }

      if (
        this.company &&
        this.company.thumbnail &&
        this.company.thumbnail.file
      ) {
        requestData.append("thumbnail", this.company.thumbnail.file);
      }

      if (this.images && this.images.length > 0) {
        forEach(this.company.images, (item: IObjectFile) => {
          if (item.file) {
            requestData.append("files", item.file);
          }
        });
      }

      forEach(this.deleteFiles, (x: string) => {
        requestData.append("ImageDeletes", x);
      });
      const hashtagIdDelete = map(this.company.hashtags, "id");
      forEach(hashtagIdDelete, (x: string) => {
        requestData.append("hashtagDeletes", x);
      });

      const branchJobIdDelete = map(this.company.branchJobCompanies, "id");
      forEach(branchJobIdDelete, (x: string) => {
        requestData.append("branchJobCompanyDeletes", x);
      });

      forEach(this.hashtagOfCompany, (item: string) => {
        requestData.append("hashtagIds", item);
      });

      forEach(this.branchJobOfComany, (item: string) => {
        requestData.append("branchJobCompanyIds", item);
      });

      await this.$store.dispatch({
        type: `company/${this.company.id ? "updateCompany" : "createCompany"}`,
        data: requestData,
      });

      (this.$refs.companyForm as any).resetFields();
      this.$emit("save-success");
      this.$emit("input", false);

      this.$Message.success(
        `${
          !this.company.id
            ? "Thêm mới công ty thành công"
            : "Sửa công ty thành công"
        }`
      );
      this.$router.push({ name: PathNames.Company });
    }
  }

  cancel() {
    (this.$refs.companyForm as any).resetFields();
    this.$emit("input", false);
    this.$router.push({ name: PathNames.Company });
  }

  getLinkPath(fileObject: IObjectFile) {
    return fileObject.path
      ? Util.getLinkPath(fileObject.path)
      : window.URL.createObjectURL(fileObject.file);
  }

  onChangeThumbnail(event: any) {
    this.company.thumbnail = {
      id: "",
      file: event.target.files[0],
      fileType: FileType.Thumbnail,
    } as IObjectFile;
  }

  onChangeImage(event: any) {
    this.company.images = this.company.images || [];
    for (let i = 0; i < event.target.files.length; i++) {
      this.company.images.push({
        id: "",
        file: event.target.files[i],
        fileType: FileType.Image,
      } as IObjectFile);
    }
  }

  companyRule = {
    name: [
      {
        required: true,
        message: this.L("This field is required", undefined, this.L("name")),
        trigger: "blur",
      },
    ],
    fullNameCompany: [
      {
        required: true,
        message: this.L("This field is required", undefined, this.L("content")),
        trigger: "blur",
      },
    ],
    location: [
      {
        required: true,
        message: this.L(
          "This field is required",
          undefined,
          this.L("location")
        ),
        trigger: "blur",
      },
    ],
    locationDescription: [
      {
        required: true,
        message: this.L(
          "This field is required",
          undefined,
          this.L("locationDescription")
        ),
        trigger: "blur",
      },
    ],
    description: [
      {
        required: true,
        message: this.L(
          "This field is required",
          undefined,
          this.L("description")
        ),
        trigger: "blur",
      },
    ],
    nationalityId: [
      {
        required: true,
        message: this.L(
          "This field is required",
          undefined,
          this.L("nationality")
        ),
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

.uploadOtherImages {
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

.col-image {
  position: relative;
  img {
    width: 100%;
    max-height: 100px;
    object-fit: cover;
    object-position: center;
  }

  .button-image {
    position: absolute;
    top: -20px;
    left: 100%;
    width: 8%;
    cursor: pointer;
  }
}

#uploadIcon {
  display: none;
}

#uploadOtherImages {
  display: none;
}

#assets {
  display: none;
}
</style>