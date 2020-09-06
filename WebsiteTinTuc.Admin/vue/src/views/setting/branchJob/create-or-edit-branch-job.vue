<template>
  <div>
    <Modal
      :title="branchJob.id ? L('Sửa BranchJob') : L('Thêm BranchJob')"
      :value="value"
      @on-ok="save"
      @on-visible-change="visibleChange"
    >
      <Form ref="branchJobForm" label-position="top" :rules="hashtagRule" :model="branchJob">
        <FormItem :label="L('Name')" prop="name">
          <Input v-model="branchJob.name" @keypress.native.enter="save" :maxlength="32" />
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
export default class CreateOrEditBranchJob extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  get branchJob() {
    return this.$store.state.branchJob.branchJob;
  }
  save() {
    (this.$refs.branchJobForm as any).validate(async (valid: boolean) => {
      if (valid) {
        await this.$store.dispatch({
          type: `branchJob/${
            !this.branchJob.id ? "createBranchJob" : "updateBranchJob"
          }`,
          data: this.branchJob,
        });
        (this.$refs.branchJobForm as any).resetFields();
        this.$Message.success(
          `${!this.branchJob.id ? "Thêm" : "Sửa"} branchJob thành công`
        );
        this.$emit("save-success");
        this.$emit("input", false);
      }
    });
  }
  cancel() {
    (this.$refs.branchJobForm as any).resetFields();
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }
  hashtagRule = {
    name: [
      {
        required: true,
        message: this.L(
          "Bạn phải nhập tên BranchJob",
          undefined,
          this.L("Name")
        ),
        trigger: "blur",
      },
    ],
  };
}
</script>
