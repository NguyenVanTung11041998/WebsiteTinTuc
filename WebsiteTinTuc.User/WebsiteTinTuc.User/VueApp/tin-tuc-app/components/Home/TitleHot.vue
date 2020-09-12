<template>
  <div>
    <h2 class="title-hot">{{ title }}</h2>
  </div>
</template>

<script lang="ts">
import { Component, Prop, Vue } from "vue-property-decorator";
@Component({
  name: "TitleHot",
  components: {},
})
export default class TitleHot extends Vue {
    @Prop({ type: String, default: "" }) private readonly title!: string;
}
</script>

<style lang="scss" scoped>
  .title-hot {
    font-size: 1.75em;
    font-weight: bold;
  }
</style>
