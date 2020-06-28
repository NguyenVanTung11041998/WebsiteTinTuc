<template>
  <div>
    <Modal
      :title="category.id ? L('Sửa danh mục') : L('Thêm danh mục')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
    >
      <Form ref="categoryForm" label-position="top" :rules="categoryRule" :model="category">
        <FormItem :label="L('Name')" prop="name">
          <Input v-model="category.name" :maxlength="32" />
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
@Component
export default class CreateOrEditCategory extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  get category() {
    return this.$store.state.category.category;
  }
  save() {
    (this.$refs.categoryForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: "category/createOrEdit",
          data: this.category
        });
        (this.$refs.categoryForm as any).resetFields();
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.categoryForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }
  categoryRule = {
    name: [
      {
        required: true,
        message: this.L(
          "Bạn phải nhập tên danh mục",
          undefined,
          this.L("Name")
        ),
        trigger: "blur"
      }
    ]
  };
}
</script>
