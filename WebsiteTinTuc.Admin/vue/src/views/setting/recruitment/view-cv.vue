<template>
  <div>
    <Modal
      title="Chi tiết CV"
      :value="value"
      @on-visible-change="visibleChange"
    >
      <div>
        <p class="text-info">
          Họ tên: <b>{{ cv.name }}</b>
        </p>
        <p class="text-info" v-if="cv.userName">
          Username: <b>{{ cv.userName }}</b>
        </p>
        <p class="text-info">Số điện thoại: {{ cv.phoneNumber }}</p>
        <p class="text-info">Email: {{ cv.email }}</p>
        <p v-if="cv.link" class="text-info">Link CV: <a :href="getLinkPath()">Tải về</a></p>
        <div v-if="cv.portfolio" class="text-info">
          <p class="text-info">Portfolio</p>
          <textarea
            :rows="6"
            style="width: 100%"
            v-model="cv.portfolio"
            readonly
          />
        </div>
      </div>
      <div slot="footer">
        <Button @click="cancel">{{ L("Cancel") }}</Button>
      </div>
    </Modal>
  </div>
</template>
<script lang="ts">
import { Component, Vue, Inject, Prop, Watch } from "vue-property-decorator";
import Util from "../../../lib/util";
import AbpBase from "../../../lib/abpbase";
import CV from "../../../store/entities/cv";
@Component
export default class ViewCV extends AbpBase {
  @Prop({ type: Boolean, default: false }) value: boolean;
  get cv() {
    return this.$store.state.recruitment.cv as CV;
  }
  cancel() {
    this.$emit("input", false);
  }
  visibleChange(value: boolean) {
    if (!value) {
      this.$emit("input", value);
    }
  }

  getLinkPath() {
      return Util.getLinkPath(this.cv.link);
  }
}
</script>

<style scoped>
.text-info {
  font-size: 14px;
  margin-bottom: 10px;
}
</style>
