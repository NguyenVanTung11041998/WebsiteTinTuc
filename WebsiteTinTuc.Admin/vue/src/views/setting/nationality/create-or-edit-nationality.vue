<template>
  <div>
    <Modal :value="value" @on-ok="save" @on-visible-change="visibleChange">
      <h1>{{nationality.id ? "Sửa Quốc tịch" : "Thêm Quốc tịch"}}</h1>
      <hr />
      <Form ref="nationalityForm" label-position="top" :model="nationality">
        <FormItem :label="L('Name:')" prop="name">
          <Input v-model="nationality.name" :maxlength="32" />
        </FormItem>
        <FormItem :label="L('Ảnh:')" prop="thumbnail">
          <div class="image-cover mx-0 mb-2" v-if="nationality.image">
            <img :src="nationality.image ? getLinkPath(nationality.image) : '#'" alt="Thumbnail" />
            <span>
              <img src="../../../assets/x-button.png" @click="removeImage(nationality.image)" />
            </span>
          </div>
          <div
            class="input-button"
            v-if="!nationality.image"
            :class="{'d-none' : nationality.image}"
          >
            <b-form-file
              id="uploadIcon"
              v-model="thumbnailFile"
              size="sm"
              @change="onChangeImage"
              plain
            />
            <label for="uploadIcon" class="custom-input">
              <span></span>
              <span></span>
            </label>
          </div>
        </FormItem>
      </Form>
      <div slot="footer">
        <Button @click="cancel">{{L('Cancel')}}</Button>
        <Button @click="save" type="primary">{{L('OK')}}</Button>
      </div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import Post from "../../../store/entities/post";
import { Getter } from "vuex-class";
import Nationality from "../../../store/entities/nationality";
import { FileType } from "../../../store/enums/file-type";
import IObjectFile from "../../../store/interfaces/IObjectFile";
@Component
export default class CreateOrEditNationality extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  public thumbnailFile: File = null;
  get nationality() {
    return this.$store.state.nationality.nationality;
  }
  cancel() {
    (this.$refs.nationalityForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }
  removeImage(file: IObjectFile) {
    this.nationality.image = null;
    this.thumbnailFile = null;
  }
  getLinkPath(fileObject: IObjectFile) {
    return fileObject.path
      ? Util.getLinkPath(fileObject.path)
      : window.URL.createObjectURL(fileObject.file);
  }
  onChangeImage(event: any) {
    this.nationality.image = {
      id: "",
      file: event.target.files[0],
      fileType: FileType.image,
    } as IObjectFile;
  }
  async save() {
    (this.$refs.nationalityForm as any).validate(async (valid: boolean) => {
      if (valid) {
        let request = new FormData();
        request.append("name", this.nationality.name);
        if (this.nationality.image) {
          request.append("image", this.nationality.image.file);
        }
        if (this.nationality.id) {
          request.append("id", this.nationality.id);
        }
        await this.$store.dispatch({
          type: `nationality/${
            !this.nationality.id ? "createNationality" : "updateNationality"
          }`,
          data: request,
        });
        (this.$refs.nationalityForm as any).resetFields();
        this.$Message.success(
          `${!this.nationality.id ? "Thêm" : "Sửa"} nationality thành công`
        );
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
}
</script>
