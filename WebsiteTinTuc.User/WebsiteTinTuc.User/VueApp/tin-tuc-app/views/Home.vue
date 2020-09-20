<template>
  <div>
    <prominent-company />
    <list-company-post-home />
  </div>
</template>

<script lang="ts">
import { Vue, Component } from "vue-property-decorator";
import ProminentCompany from "@/tin-tuc-app/components/Home/ProminentCompany.vue";
import ListCompanyPostHome from "@/tin-tuc-app/components/Home/ListCompanyPostHome.vue";

@Component({
  name: "Home",
  components: {
    ProminentCompany,
    ListCompanyPostHome,
  },
})
export default class Home extends Vue {
}
</script>

<style lang="scss" scoped></style>
