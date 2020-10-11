<template>
  <div>
    <company-home-info v-if="!isLoading" :company="company" />
  </div>
</template>

<script lang="ts">
import { Vue, Component, Watch } from "vue-property-decorator";
import ProminentCompany from "@/tin-tuc-app/components/Home/ProminentCompany.vue";
import ListCompanyPostHome from "@/tin-tuc-app/components/Home/ListCompanyPostHome.vue";
import Post from "@/tin-tuc-app/components/Home/Post.vue";
import { Action, Getter } from "vuex-class";
import CompanyPostModel from "../store/interfaces/home";
import CompanyHome, {
  CompanyHomeInfoRequest,
} from "../store/interfaces/company-home";
import CompanyHomeInfo from "@/tin-tuc-app/components/CompanyHome/CompanyHomeInfo.vue";

@Component({
  name: "CompanyInfoHome",
  components: { CompanyHomeInfo },
})
export default class CompanyInfoHome extends Vue {
  @Action("getCompanyByUrl", { namespace: "CompanyHomeModule" })
  private getCompanyByUrl!: (params: CompanyHomeInfoRequest) => void;
  @Getter("company", { namespace: "CompanyHomeModule" })
  private company!: CompanyHome;
  private isLoading = false;

  private get newId(): string {
    return this.$route.params.id;
  }

  private async created() {
    await this.getData();
  }

  @Watch("newId")
  private async getData() {
    this.isLoading = true;
    let params = {
      url: this.$route.params.id,
      currentPage: 1,
      pageSize: 10,
      searchText: "",
    } as CompanyHomeInfoRequest;
    await this.getCompanyByUrl(params);
    this.isLoading = false;
  }
}
</script>

<style lang="scss" scoped></style>
