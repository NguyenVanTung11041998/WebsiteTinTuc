<template>
  <div>
    <prominent-company />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import ProminentCompany from "@/tin-tuc-app/components/Home/ProminentCompany.vue";

@Component({
  name: "Home",
  components: {
    ProminentCompany
  },
})
export default class Home extends Vue {
  pageTitle: string = "Công Ty Nổi Bật";
}
</script>

<style lang="scss" scoped>
</style>
