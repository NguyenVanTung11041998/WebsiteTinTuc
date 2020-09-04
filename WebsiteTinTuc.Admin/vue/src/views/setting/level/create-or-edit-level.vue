<template>
  <div>
    <Modal
      :title="level.id ? L('Sửa level') : L('Thêm level')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
    >
      <Form ref="levelForm" label-position="top" :rules="levelRule" :model="level">
        <FormItem :label="L('Name')" prop="name">
          <Input v-model="level.name" :maxlength="32" />
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
export default class CreateOrEditLevel extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  get level() {
    return this.$store.state.hashtag.hashtag;
  }
  save() {
    (this.$refs.levelForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: `level/${!this.level.id ? "createLevel" : "updateLevel"}`,
          data: this.level,
        });
        (this.$refs.levelForm as any).resetFields();
        this.$Message.success(
          `${!this.level.id ? "Thêm" : "Sửa"} level thành công`
        );
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.levelForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }
  levelRule = {
    name: [
      {
        required: true,
        message: this.L("Bạn phải nhập tên level", undefined, this.L("Name")),
        trigger: "blur",
      },
    ],
  };
}
</script>
